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
    public class PagosCxPCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public int intSeq { get; set; }
        public int intFolioCargo { get; set; }
        public DateTime fFecha { get; set; }
        public decimal dImporte { get; set; }
        public int intProveedoresID { get; set; }
        public int intCuentasBancariaID { get; set; }
        public decimal intTipodecambio { get; set; }
        public int intBancosId { get; set; }
        public string strCuentaOrdenante { get; set; }
        public string strC_Formapago { get; set; }
        public int intNumeroOperacion { get; set; }
        public int intC_TipoCadenaPago { get; set; }
        public string strMonedaDelPago { get; set; }
        public string strStatus { get; set; }
        public string strHora { get; set; }
        public DateTime fFechaReal { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public string strDoc { get; set; } 
        public int intID { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        public decimal dTipodecambio { get; set; }  
        public int iParcialidad { get; set; }
        public int intFoliochequeotransferencia { get; set; }
        public int intOrigen { get; set; }
        public int intAño { get; set; }
        public int intMes { get; set; }
        public string strRef { get; set; }
        public string strPoliza { get; set; }

        #endregion

        #region Constructor
        public PagosCxPCL()
        {
            strPoliza = string.Empty;
            intSeq = 0;
            intFolioCargo = 0;
            strSerie = string.Empty;
            intFolio = 0;
            fFecha = DateTime.Now;
            dImporte = 0;
            intProveedoresID = 0;
            intCuentasBancariaID = 0;
            intTipodecambio = 0;
            intBancosId = 0;
            strCuentaOrdenante = string.Empty;
            strC_Formapago = string.Empty;
            intNumeroOperacion = 0;
            intC_TipoCadenaPago = 0;
            strMonedaDelPago = string.Empty;
            strStatus = string.Empty;
            strHora = string.Empty;
            fFechaReal = DateTime.Now;
            dTipodecambio = 0;
            intFoliochequeotransferencia = 0;
            intOrigen = 0;
            intID = 0;
            strRef = string.Empty;
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
        public DataTable PagoscxpGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PagoscxpGRID";
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
        } //depositosGrid

        public string PagosGeneraAntiguedaddesaldos()  
        {
            
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PagosFacturasporpagarparapagos";
                cmd.Parameters.AddWithValue("@prmProveedor", intProveedoresID);
                cmd.Parameters.AddWithValue("@prmUsu", intUsuarioID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                string result;
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
        } //depositosCargaFacturas

        public DataTable PagoscxpCargaFacturas()  //Actualizar a pagos
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PagoscxpLeeFacturasporcobrarTmp";                
                cmd.Parameters.AddWithValue("@prmUsu", intUsuarioID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //depositosCargaFacturas

        public DataTable PagoscxpLlenaCajasDetalle()  //Actualizar a pagos
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PagoscxpLlenaCajasDetalle";
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
        } //depositosCargaFacturas

        public string PagoxcxpCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PagoscxpCRUD";
                cmd.Parameters.AddWithValue("@prmPagoscxp", dtm);
                cmd.Parameters.AddWithValue("@prmPagoscxpdetalle", dtd);
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
        } //PagoscxpCrud

        public string PagoscxpLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PagoscxpLlenaCajas";
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
                    dImporte = Convert.ToDecimal(dr["Importe"]);
                    intProveedoresID = Convert.ToInt32(dr["ProveedoresID"]);
                    intCuentasBancariaID = Convert.ToInt32(dr["CuentasbancariaID"]);                    
                    dTipodecambio =Convert.ToDecimal(dr["Tipodecambio"]);
                    intBancosId = Convert.ToInt32(dr["BancodelproveedorID"]);
                    strCuentaOrdenante = dr["Cuentadelproveedor"].ToString();
                    strC_Formapago = dr["C_Formapago"].ToString();
                    intFoliochequeotransferencia = Convert.ToInt32(dr["Foliochequeotransferencia"]);                    
                    strMonedaDelPago = dr["Monedadelpago"].ToString();
                    strStatus = dr["Status"].ToString();                    
                    fFechaReal = Convert.ToDateTime(dr["FechaReal"]);
                    intOrigen = Convert.ToInt32(dr["Origen"]);
                    strPoliza= dr["Poliza"].ToString();
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

        public string PagoscxpCancelar()   //Actualizar a pagos
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PagoscxpCancelar";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);
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
        } // Public Class Eliminar

        public string Depositosparcialidades() //Actualizar a pagos
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DepositosParcialidades";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    iParcialidad = Convert.ToInt32(dr["Parcialidad"].ToString());
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
        } // Public Class Eliminar


        //Siguiente ID para Depositos cv
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
                    intID = Convert.ToInt32(dr["SigID"]);
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

        //Buscara la siguiente serie de acuerdo al usuario cv
        public string BuscarSerieporUsuario()
        {
            DataTable dt = new DataTable();
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BuscarSerie";
                cmd.Parameters.AddWithValue("@prmUsuarioID", intUsuarioID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strSerie = Convert.ToString(dr["Serie"]);
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

        public DataTable PagosSacaFacturaParaTesk()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TeskPagosProveedoresSacaFactura ";
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
        } //depositosGrid

        //public string PagosSacaFacturaParaTesk()
        //{

        //    try
        //    {
        //        SqlConnection cnn = new SqlConnection();
        //        cnn.ConnectionString = strCnn;
        //        cnn.Open();

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = "TeskPagosProveedoresSacaFactura ";
        //        cmd.Parameters.AddWithValue("@prmSerie", strSerie);
        //        cmd.Parameters.AddWithValue("@prmFolio", intFolio);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Connection = cnn;

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        string result;
        //        if (dr.HasRows)
        //        {
        //            dr.Read();
        //            strRef = dr["Referencia"].ToString();
        //            fFecha = Convert.ToDateTime(dr["Fecha"]);
        //            intFolioCargo = Convert.ToInt32(dr["Folio"]);
        //            result = "OK";
        //        }
        //        else
        //        {
        //            result = "no read";
        //        }
        //        dr.Close();
        //        cnn.Close();
        //        return result;

        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //} //depositosCargaFacturas
        public string PagoscxpdetalleRedonondeosTesk()
        {

            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PagoscxpdetalleRedonondeosTesk_Actualiza";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmSeq", intSeq);
                cmd.Parameters.AddWithValue("@prmSaldo", dImporte);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                string result;
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
        } //PagoscxpdetalleRedonondeosTesk
        public string PagosActualizaFacturaParaTesk()
        {

            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TeskCorrigeFacturaCxP ";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolioCargo", intFolioCargo);
                cmd.Parameters.AddWithValue("@prmFac", strRef);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                string result;
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
        } //PagosActualizaFacturaParaTesk
        #endregion
    }
}