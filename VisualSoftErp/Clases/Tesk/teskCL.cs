using VisualSoftErp.Clases.Tesk.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases;
using VisualSoftErp.Clases.Tesk.Response;
using VisualSoftErp.Interfaces;
using VisualSoftErp.Clases.Tesk.Request;
using TeskApiTest.Request;

namespace VisualSoftErp.Clases.Tesk.Clases
{
    public class teskCL
    {
        #region Propiedades
        //string strCnn = ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public int intDato { get; set; }
        public int intOpcion { get; set; }
        public int intFolio { get; set; }
        public string strTabla;
        public string strSerie { get; set; }
        public string strPoliza { get; set; }
        public string strCta { get; set; }

        string Autorization;
        string CodigoEmpresa= "Edes006339";
        #endregion
        #region Constructor
        public teskCL()
        {
            intDato = 0;
            intFolio = 0;
            strTabla = string.Empty;
            strSerie = string.Empty;
        }
        #endregion
        #region Métodos
        private void obtenToken()
        {
            try
            {
                tesk_MalocClient cliente = new tesk_MalocClient();
                teskloginCL request = new teskloginCL();
                request.user = "jzambrano@live.com.mx";
                request.pass = "Diexsa2021";

                string url = "https://app.tesk.mx/diexsa/v1/";

                
                teskloginresponse datos = cliente.PeticionesWSPost<teskloginresponse>(url, "autenticar", request);
                Autorization = datos.token;
                
            }
            catch(Exception ex)
            {
                Autorization = "Error:" + ex.Message;
            }
        }
        #region Proveedores
        public string PostProveedoresDiexsa()
        {
            string strErr = string.Empty;
            try
            {
                strErr = "OK";
                int proveedorID;


                tesk_MalocClient cliente = new tesk_MalocClient();

                postProveedoresRequest dataRequest = new postProveedoresRequest();
                PContacto contacto = new PContacto();
                PDirecciondetalle direcciondetalle = new PDirecciondetalle();
                PLocalidad localidad = new PLocalidad();
                PDireccion direccion = new PDireccion();

                DataTable dt = new DataTable();
                //intDato = 0;
                intOpcion = 1;
                strTabla = "Proveedores";
                dt = Datos();

                if (dt.Rows.Count > 0)
                {
                    obtenToken();
                }
                else
                {
                    return "Este proveedor ya existe en tesk";
                }

                foreach (DataRow dr in dt.Rows)
                {
                    proveedorID = Convert.ToInt32(dr["clave"]);

                    dataRequest.clave = dr["Clave"].ToString();
                    dataRequest.cuentaTipoProveedor = dr["cuentaTipoProveedor"].ToString();
                    dataRequest.diasDeCredito = Convert.ToInt32(dr["diasDeCredito"]);
                    dataRequest.nombre = dr["nombre"].ToString();
                    dataRequest.rfc = dr["rfc"].ToString();
                    dataRequest.tiempoEntrega = Convert.ToInt32(dr["tiempoEntrega"]);
                    dataRequest.tipoCuenta = dr["tipoCuenta"].ToString();
                    dataRequest.tipoOperacionImpuestos = dr["tipoOperacionImpuestos"].ToString();
                    dataRequest.tipoPersona = dr["tipoPersona"].ToString();
                    dataRequest.tipoTerceroCodigo = dr["tipoTerceroCodigo"].ToString();

                    contacto.celular = "0";
                    contacto.correo = "x@x.com";
                    contacto.correoCC = "x@x.com";
                    contacto.telefono = "0";

                    direcciondetalle.calle = "x";
                    direcciondetalle.ciudad = "x";
                    direcciondetalle.codigoPostal = "64000";
                    direcciondetalle.colonia = "";
                    direcciondetalle.numeroExt = "0";
                    direcciondetalle.numeroInt = "";

                    localidad.estadoCodigo = "19";
                    localidad.municipioCodigo = "039";
                    localidad.paisCodigo = "MEX";

                    direccion.contacto = contacto;
                    direccion.direccion = direcciondetalle;
                    direccion.localidad = localidad;

                    dataRequest.direccion = direccion;

                    string url = "https://app.tesk.mx/diexsa/v1/";

                    erroresResponse datos = cliente.PeticionesWSPostWithHeaders<erroresResponse>(url, "proveedores", Autorization, CodigoEmpresa, dataRequest);
                    if (datos != null)
                    {
                        if (datos.mensaje.Substring(0, 29) != "Ya existe una cuenta contable")
                        {
                            if (datos.mensaje != "El RFC que intenta guardar en tesk ya existe")
                            {
                                

                                strErr = "Proveedor:" + dataRequest.nombre + System.Environment.NewLine;

                                int num = Convert.ToInt32(datos.validacionResponse.errores.Count);

                                for (int i = 0; i <= num - 1; i++)
                                {
                                    strErr = strErr + datos.validacionResponse.errores[i].campo + " : " + datos.validacionResponse.errores[i].error[0].ToString() + System.Environment.NewLine;
                                }
                                strErr = strErr + "-------------------------------------------------------";
                                
                            }
                            else
                            {
                                //Temporal
                                //Se actualiza campo tesk en visualsoft, para saber que ya está sincronizado
                                strTabla = "Proveedores";
                                strSerie = "";
                                intFolio = proveedorID;

                                string result = teskSincronizaErp();

                                if (result != "OK")
                                {
                                    strErr = "Al sincronizar proveedor: " + proveedorID.ToString() + " " + result + Environment.NewLine;
                                    
                                }


                            }

                        }
                        else
                        {
                            //Se actualiza campo tesk en visualsoft, para saber que ya está sincronizado
                            strTabla = "Proveedores";
                            strSerie = "";
                            intFolio = proveedorID;

                            string result = teskSincronizaErp();

                            if (result != "OK")
                            {
                                strErr = "Al sincronizar proveedor: " + proveedorID.ToString() + " " + result + Environment.NewLine;
                                
                            }
                        }
                    }
                    else
                    {


                        //Se actualiza campo tesk en visualsoft, para saber que ya está sincronizado
                        strTabla = "Proveedores";
                        strSerie = "";
                        intFolio = proveedorID;
                        strCta = cliente.strCuentaContable.Substring(13, 17);

                        string result = teskSincronizaErp();

                        if (result != "OK")
                        {
                            strErr = "Al sincronizar proveedor: " + proveedorID.ToString() + " " + result + Environment.NewLine;
                            
                        }
                    }

                }

                return strErr;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        public DataTable Datos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = globalCL.gv_strcnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Tesk_" + strTabla;
                cmd.Parameters.AddWithValue("@prmOpcion", 1);
                cmd.Parameters.AddWithValue("@prmDato", intDato);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);

                cnn.Close();

                return dt;

            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return dt;

            }


        }//Clientes()
        public DataSet CobranzaDeposito()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = globalCL.gv_strcnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Tesk_" + strTabla;
                cmd.Parameters.AddWithValue("@prmDato", 1);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(ds);
                cnn.Close();
                return ds;

            }
            catch (Exception) { return ds; }
        } //Datosdelaorden
        public string teskSincronizaErp()
        {
            try
            {
                if (strCta == null)
                    strCta = string.Empty;

                if (strPoliza == null)
                    strPoliza = "";

                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = globalCL.gv_strcnn; 
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Tesk_ActualizaStatus";
                cmd.Parameters.AddWithValue("@prmTabla", strTabla);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmPoliza", strPoliza);
                cmd.Parameters.AddWithValue("@prmCuenta", strCta);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr["result"].ToString();
                }
                else
                {
                    result = "no read";
                }
                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } //teskSincronizaErp
        #endregion
    }
}
