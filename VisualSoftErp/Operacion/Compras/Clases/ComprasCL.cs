using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace VisualSoftErp.Clases
{
    public class ComprasCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public string strTabla { get; set; }
        public int intFolio { get; set; }
        public DateTime fFecha { get; set; }
        public string strOCSerie { get; set; }
        public int intOCFolio { get; set; }
        public int intProveedoresID { get; set; }
        public int intArt { get; set; }
        public string strMonedasID { get; set; }
        public string strFP { get; set; }
        public string strMP { get; set; }
        public string strUso { get; set; }

        public int intAlmacen { get; set; }
        public string strFactura { get; set; }
        public DateTime ffechafactura { get; set; }
        public decimal dectipodecambio { get; set; }
        public decimal decSubtotal { get; set; }
        public decimal decIva { get; set; }
        public decimal decRetIva { get; set; }
        public decimal decRetIsr { get; set; }
        public int intIvaEstricto { get; set; }
        public int intRetIvaEstricto { get; set; }
        public decimal decIeps { get; set; }
        public decimal decNeto { get; set; }
        public int intStatus { get; set; }
        public int intPlazo { get; set; }
        public DateTime fFechacancelacion { get; set; }
        public string strRazoncancelacion { get; set; }
        public decimal strDescuento { get; set; }
        public int intPoliza { get; set; }
        public int intNodeducible { get; set; }
        public string strContrarecibosSerie { get; set; }
        public int intContrarecibosFolio { get; set; }
        public string strRecepcionSerie { get; set; }
        public int intRecepcionFolio { get; set; }
        public string teskPoliza { get; set; }
        public int intValidadoPor { get; set; }
        public DateTime fFechaValidado { get; set; }
        public DateTime fFechaReal { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public DataTable dpxa { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        public string strDoc { get; set; }
        public string strTipoProv { get; set; }
        public string strRfc { get; set; }
        public string strUUID { get; set; }
        public string strClave { get; set; }
        public decimal dTC { get; set; }
        public int intAño { get; set; }
        public int intGrupo { get; set; }
        public int intMes { get; set; }
        public int intDiasTrasladoProv { get; set; }
        public int intDiasSurtidoProv { get; set; }
        public int intDiasStockProv { get; set; }
        public int intMesesdeconsumoProv { get; set; }
        public int intPago { get; set; }

        public int intSubFam { get; set; }
        public int intFam { get; set; }
        public int intLinea { get; set; }


        public DataTable dtConsumo { get; set; }
        public int intPedido { get; set; }
        public DataTable dtResult { get; set; }

        public decimal decCargoVario { get; set; }
        public decimal decAjusteFactura { get; set; }
        #endregion

        #region Constructor
        public ComprasCL()
        {
            strTabla = string.Empty;
            teskPoliza = string.Empty;
            intIvaEstricto = 0;
            intRetIvaEstricto = 0;
            strSerie = string.Empty;
            intFolio = 0;
            fFecha = DateTime.Now;
            strOCSerie = string.Empty;
            intOCFolio = 0;
            intProveedoresID = 0;
            strMonedasID = string.Empty;
            intAlmacen = 0;
            strFactura = string.Empty;
            ffechafactura = DateTime.Now;
            dectipodecambio = 0;
            decSubtotal = 0;
            decIva = 0;
            decIeps = 0;
            decNeto = 0;
            intStatus = 0;
            intPlazo = 0;
            fFechacancelacion = DateTime.Now;
            strRazoncancelacion = string.Empty;
            strDescuento = 0;
            intPoliza = 0;
            intNodeducible = 0;
            strContrarecibosSerie = string.Empty;
            intContrarecibosFolio = 0;
            strRecepcionSerie = string.Empty;
            intRecepcionFolio = 0;
            intValidadoPor = 0;
            fFechaValidado = DateTime.Now;
            fFechaReal = DateTime.Now;
            strTipoProv = string.Empty;
            strRfc = string.Empty;
            dTC = 0;
        }
        #endregion

        #region Metodos
        private string loadConnectionString()
        {
            try
            {
                XmlDocument oxml = new XmlDocument();
                oxml.Load(@"C:\VisualSoftErp\xml\conexion.xml");
                XmlNodeList sConfiguracion = oxml.GetElementsByTagName("Configuracion");
                XmlNodeList sGenerales = ((XmlElement)sConfiguracion[0]).GetElementsByTagName("Generales");
                XmlNodeList sStr_Conn = ((XmlElement)sGenerales[0]).GetElementsByTagName("Str_Conn");
                return sStr_Conn[0].InnerText;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public DataTable CargaUnaOC()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasCargaOC";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //CargaUnaOC

        public string DocumentosSiguienteID()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DocumentosSiguienteID";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmDoc", strDoc);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intFolio = Convert.ToInt32(dr["SigID"]);
                    result = "OK";
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
        }

        public string Leeproveedorporrfc()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresLeePorRfc";
                cmd.Parameters.AddWithValue("@prmRfc", strRfc);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intProveedoresID = Convert.ToInt32(dr["ProveedoresID"]);
                    intPlazo = Convert.ToInt32(dr["Plazo"]);
                    strTipoProv = dr["Tipodeproveedor"].ToString();
                    decIva= Convert.ToDecimal(dr["Piva"]);
                    decRetIva = Convert.ToDecimal(dr["Retiva"]);
                    decRetIsr = Convert.ToDecimal(dr["Retisr"]);
                    strMonedasID = dr["MonedasID"].ToString();
                    strFP = dr["c_FormaPago"].ToString();
                    strMP = dr["c_MetodoPago"].ToString();
                    strUso = dr["c_UsoCfdi"].ToString();
                    intIvaEstricto = Convert.ToInt32(dr["IvaEstricto"]);
                    intRetIvaEstricto = Convert.ToInt32(dr["RetIvaEstricto"]);
                    result = "OK";
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
        }

        public DataTable ComprasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "comprasGrid";
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //comprasGrid

        public DataTable CargaOrdendeCompra()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprascargaOC";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //comprasGrid

        public string ComprasCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasCRUD";
                cmd.Parameters.AddWithValue("@prmCompras", dtm);
                cmd.Parameters.AddWithValue("@prmComprasdetalle", dtd);
                cmd.Parameters.AddWithValue("@prmProveedoresArticulos", dpxa);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);
                cmd.Parameters.AddWithValue("@prmPrograma", strPrograma);
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
        } //ComprasCrud

        public string GuardaDatosFactura()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasDatosFacturaCrud";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmFac", strFactura);
                cmd.Parameters.AddWithValue("@prmFechaFac", fFecha);
                cmd.Parameters.AddWithValue("@prmTC", dTC);
                cmd.Parameters.AddWithValue("@prmPlazo", intPlazo);
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
        } //ComprasCrud

        public string ComprasLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasllenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strSerie = dr["Serie"].ToString();
                    intFolio = Convert.ToInt32(dr["Folio"]);
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    strOCSerie = dr["OCSerie"].ToString();
                    intOCFolio = Convert.ToInt32(dr["OCFolio"]);
                    intProveedoresID = Convert.ToInt32(dr["ProveedoresID"]);
                    strMonedasID = dr["MonedasID"].ToString();
                    intAlmacen = Convert.ToInt32(dr["AlmacenesID"]);
                    strFactura = dr["Factura"].ToString();
                    ffechafactura = Convert.ToDateTime(dr["fechafactura"]);
                    dectipodecambio = Convert.ToDecimal(dr["tipodecambio"]);
                    decSubtotal = Convert.ToDecimal(dr["Subtotal"]);
                    decIva = Convert.ToDecimal(dr["Iva"]);
                    decIeps = Convert.ToDecimal(dr["Ieps"]);
                    decNeto = Convert.ToDecimal(dr["Neto"]);
                    intStatus = Convert.ToInt32(dr["Status"]);
                    intPlazo = Convert.ToInt32(dr["Plazo"]);
                    fFechacancelacion = Convert.ToDateTime(dr["Fechacancelacion"]);
                    strRazoncancelacion = dr["Razoncancelacion"].ToString();
                    strDescuento = Convert.ToDecimal(dr["Descuento"]);
                    intPoliza = Convert.ToInt32(dr["Poliza"]);
                    intNodeducible = Convert.ToInt32(dr["Nodeducible"]);
                    strContrarecibosSerie = dr["ContrarecibosSerie"].ToString();
                    intContrarecibosFolio = Convert.ToInt32(dr["ContrarecibosFolio"]);
                    strRecepcionSerie = dr["RecepcionSerie"].ToString();
                    intRecepcionFolio = Convert.ToInt32(dr["RecepcionFolio"]);
                    intValidadoPor = Convert.ToInt32(dr["ValidadoPor"]);
                    fFechaValidado = Convert.ToDateTime(dr["FechaValidado"]);
                    fFechaReal = Convert.ToDateTime(dr["FechaReal"]);
                    strUUID = dr["UUID"].ToString();
                    intPago = Convert.ToInt32(dr["Pago"]);
                    teskPoliza = dr["teskPoliza"].ToString();

                    result = "OK";
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
        } // public class LlenaCajas

        public string ObtenerPlazoProveedor()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresLlenaCajas";
                cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intPlazo = Convert.ToInt32(dr["Plazo"]);

                    result = "OK";
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
        } // public class LlenaCajas

        public string ObtenArticuloIDPxA()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresArticulosLeeID";
                cmd.Parameters.AddWithValue("@prmProv", intProveedoresID);
                cmd.Parameters.AddWithValue("@prmClave", strClave);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intArt = Convert.ToInt32(dr["ArticulosID"]);

                    result = "OK";
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
        } // public class LlenaCajas

        public DataTable ComprasDetalleGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasdetalleLlenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //remisionesGrid

        public string ComprasCancelar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasCancelar";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmRazon", strRazoncancelacion);
                cmd.Parameters.AddWithValue("@prmUsuario", globalCL.gv_UsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", Environment.MachineName);
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
                return "ComprasCancelar en clase: " + ex.Message;
            }
        } // Public Class Eliminar

        public string AsignaPolizaTesk()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TeskAsignaPoliza";
                cmd.Parameters.AddWithValue("@prmTabla", strTabla);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmPoliza", teskPoliza);
                cmd.Parameters.AddWithValue("@prmUsuario", globalCL.gv_UsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", Environment.MachineName);
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
                return "ComprasCancelar en clase: " + ex.Message;
            }
        } // Public Class Eliminar


        // ------------------------- Auxiliar de compras ---------------
        public DataTable Articulosdeungrupoconconsumo()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AuxiliardeComprasListaArticulosdeunGrupoConConsumo";
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmGrupo", intGrupo);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //CargaUnaOC
        public string AuxiliarImportaciones(int Op)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasAuxiliarImportacion";
                cmd.Parameters.AddWithValue("@prmProv", intProveedoresID);
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.Parameters.AddWithValue("@prmOp", Op);
                cmd.CommandTimeout = 900; //15 min
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();


                dtResult = dt;
                return "OK";

            }
            catch (Exception ex) 
            {
                dtResult = null;
                return ex.Message; 
            }
        } //comprasGrid

        public string ComprasAuxiliarDatosProveedor()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasAuxiliarImportacionDatosdeProveedor";
                cmd.Parameters.AddWithValue("@prmProv", intProveedoresID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    intDiasTrasladoProv = Convert.ToInt32(dr["Diasdetraslado"]);
                    intDiasSurtidoProv = Convert.ToInt32(dr["Diasdesurtido"]);
                    intDiasStockProv = Convert.ToInt32(dr["DiasStock"]);
                    intMesesdeconsumoProv = Convert.ToInt32(dr["Mesesdeconsumo"]);

                    result = "OK";
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
        } // ComprasAuxiliarDatosProveedor

        // ------------------------------------------- Auxiliar Nacional ------------------------------------
        public DataTable AuxiliarNacionalConsumo()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasAuxiliarNacionalConsumo";
                cmd.Parameters.AddWithValue("@prmFechaFinal", fFecha);
                cmd.Parameters.AddWithValue("@prmLinea", intLinea);
                cmd.Parameters.AddWithValue("@prmFam", intFam);
                cmd.Parameters.AddWithValue("@prmSubFam", intSubFam);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //Auxiliar nacional consumo
        public string AuxiliarNacionalConsumoGuardar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PresupuestoporarticuloCRUD";
                cmd.Parameters.AddWithValue("@prmPpto", dtConsumo);
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
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
        } // ComprasAuxiliarDatosProveedor
        public DataTable AuxiliarNacionalAuxiliar()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasAuxiliarNacional";
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.Parameters.AddWithValue("@prmFam", intFam);
                cmd.Parameters.AddWithValue("@prmSubFam", intSubFam);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.Parameters.AddWithValue("@prmPedido", intPedido);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return dt;
            }
        } //Auxiliar nacional auxiliar
        public DataTable AuxiliarNacionalImplosion()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasAuxiliarNacionalImplosion";
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.Parameters.AddWithValue("@prmComponenteID", intArt);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 90;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return dt;
            }
        } //Auxiliar Nacional Implosion

        public DataTable AuxiliarNacionalPedidosDetalleUnArticulo()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosSurtidosPorFacturarUnArticulo";
                cmd.Parameters.AddWithValue("@prmArt", intArt);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 90;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return dt;
            }
        } //Auxiliar Nacional Implosion

        public DataTable AuxiliarNacionalImplosionPedidos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasAuxiliarNacionalImplosionPedidos";
                cmd.Parameters.AddWithValue("@prmComponenteID", intArt);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 90;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return dt;
            }
        } //Auxiliar Nacional Implosion
        public DataTable AuxiliarNacionalMinMax()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasAuxiliarNacionalPorMinimoMaximo";
                cmd.Parameters.AddWithValue("@prmDias", intDiasTrasladoProv);
                cmd.Parameters.AddWithValue("@prmLinea", intLinea);
                cmd.Parameters.AddWithValue("@prmFam", intFam);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return dt;
            }
        } //Auxiliar nacional auxiliar
        public DataTable ComprasRelacion()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasRelacionRep";
                cmd.Parameters.AddWithValue("@prmEmp", 0);
                cmd.Parameters.AddWithValue("@prmSuc", 0);
                cmd.Parameters.AddWithValue("@prmFI", fFecha);
                cmd.Parameters.AddWithValue("@prmFF", fFechaReal);
                cmd.Parameters.AddWithValue("@prmProv", 0);
                cmd.Parameters.AddWithValue("@prmOrigen", 1);
                cmd.Parameters.AddWithValue("@prmDummy", "");

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //remisionesGrid

        public DataTable CargaOCaunComponente()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();



                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "OrdenesdecompraRelacionRep";
                cmd.Parameters.AddWithValue("@prmEmp", 1);
                cmd.Parameters.AddWithValue("@prmSuc", 0);
                cmd.Parameters.AddWithValue("@prmFI", DateTime.Now);
                cmd.Parameters.AddWithValue("@prmFF", DateTime.Now);
                cmd.Parameters.AddWithValue("@prmProv", 0);
                cmd.Parameters.AddWithValue("@prmStatus", 0);
                cmd.Parameters.AddWithValue("@prmArt", intArt);
                cmd.Parameters.AddWithValue("@prmFechaAtrasada", 0);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //comprasGrid

        public DataTable CargaRM()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RecepciondemercanciacargaparaValidar";
                cmd.Parameters.AddWithValue("@prmProv", intProveedoresID);                
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //CaragRM

        public string ComprasValidar()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RecepciondemercanciaValida_Crud";
                cmd.Parameters.AddWithValue("@prmRM", dtm);
                cmd.Parameters.AddWithValue("@prmProv", intProveedoresID);
                cmd.Parameters.AddWithValue("@prmFac", strFactura);
                cmd.Parameters.AddWithValue("@prmFechaFac", fFecha);
                cmd.Parameters.AddWithValue("@prmMoneda", strMonedasID);
                cmd.Parameters.AddWithValue("@prmTC", dTC);
                cmd.Parameters.AddWithValue("@prmPlazo", intPlazo);
                cmd.Parameters.AddWithValue("@prmNeto", decNeto);
                cmd.Parameters.AddWithValue("@prmUsuValido", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", Environment.MachineName);
                cmd.Parameters.AddWithValue("@prmUUID", strUUID);
                cmd.Parameters.AddWithValue("@prmCargoVario", decCargoVario);
                cmd.Parameters.AddWithValue("@prmAjusteFactura", decAjusteFactura);

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
        } //ComprasCrud

        #endregion
    }
}