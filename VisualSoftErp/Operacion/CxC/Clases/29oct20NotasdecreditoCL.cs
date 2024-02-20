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
    public class NotasdecreditoCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public string strSerieCfdi { get; set; }
        public int intFolioCfdi { get; set; }
        public decimal dTotalimpuestosret { get; set; }
        public decimal dTotalimpuestostras { get; set; }
        public DateTime fFecha { get; set; }
        public int intClientesID { get; set; }
        public int intObservacionesID { get; set; }
        public int intAgentesID { get; set; }
        public decimal intSubtotal { get; set; }
        public decimal intIva { get; set; }
        public decimal intRetIva { get; set; }
        public decimal intRetIsr { get; set; }
        public decimal intPIva { get; set; }
        public decimal intPRetIva { get; set; }
        public decimal intPRetIsr { get; set; }
        public decimal intNeto { get; set; }
        public int intStatus { get; set; }
        public DateTime fFechacancelacion { get; set; }
        public string strRazoncancelacion { get; set; }
        public int intUsuariosID { get; set; }
        public string strMonedasID { get; set; }
        public decimal strTipodecambio { get; set; }
        public int intEsdevolucion { get; set; }
        public int intEntradaTipodemovimientoinvID { get; set; }
        public string strEntradaserie { get; set; }
        public int intEntradafolio { get; set; }
        public int intPolizasID { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intUsuarioID { get; set; }
        public int intID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        public string strDoc { get; set; }
        public string strRazon { get; set; }
        #endregion

        #region Constructor
        public NotasdecreditoCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            strSerieCfdi = string.Empty;
            intFolioCfdi = 0;
            dTotalimpuestosret = 0;
            dTotalimpuestostras = 0;
            fFecha = DateTime.Now;
            intClientesID = 0;
            intObservacionesID = 0;
            intAgentesID = 0;
            intSubtotal = 0;
            intIva = 0;
            intRetIva = 0;
            intRetIsr = 0;
            intPIva = 0;
            intPRetIva = 0;
            intPRetIsr = 0;
            intNeto = 0;
            intStatus = 0;
            fFechacancelacion = DateTime.Now;
            strRazoncancelacion = string.Empty;
            intUsuariosID = 0;
            strMonedasID = string.Empty;
            strTipodecambio = 0;
            intEsdevolucion = 0;
            intEntradaTipodemovimientoinvID = 0;
            strEntradaserie = string.Empty;
            strDoc = string.Empty; strRazon = string.Empty;
            intEntradafolio = 0;
            intPolizasID = 0;
            intID = 0;
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
        public DataTable NotasdecreditoGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NotasdecreditoGRID";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //notasdecreditoGrid

        public string NotasdecreditoCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NotasdecreditoCRUD";
                cmd.Parameters.AddWithValue("@prmNotasdecredito", dtm);
                cmd.Parameters.AddWithValue("@prmNotasdecreditodetalle", dtd);
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
        } //NotasdecreditoCrud

        public string NotasdecreditoLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NotasdecreditollenaCajas";
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
                    intClientesID = Convert.ToInt32(dr["ClientesID"]);
                    intObservacionesID = Convert.ToInt32(dr["ObservacionesID"]);
                    intAgentesID = Convert.ToInt32(dr["AgentesID"]);
                    intSubtotal = Convert.ToInt32(dr["AgentesID"]);
                    intIva = Convert.ToInt32(dr["AgentesID"]);
                    intRetIva = Convert.ToInt32(dr["AgentesID"]);
                    intRetIsr = Convert.ToInt32(dr["AgentesID"]);
                    intPIva = Convert.ToInt32(dr["AgentesID"]);
                    intPRetIva = Convert.ToInt32(dr["AgentesID"]);
                    intPRetIsr = Convert.ToInt32(dr["AgentesID"]);
                    intNeto = Convert.ToInt32(dr["AgentesID"]);
                    intStatus = Convert.ToInt32(dr["Status"]);
                    fFechacancelacion = Convert.ToDateTime(dr["Fechacancelacion"]);
                    strRazoncancelacion = dr["Razoncancelacion"].ToString();
                    intUsuariosID = Convert.ToInt32(dr["UsuariosID"]);
                    strMonedasID = dr["MonedasID"].ToString();
                    strTipodecambio = Convert.ToDecimal(dr["Tipodecambio"]);
                    intEsdevolucion = Convert.ToInt32(dr["Esdevolucion"]);
                    intEntradaTipodemovimientoinvID = Convert.ToInt32(dr["EntradaTipodemovimientoinvID"]);
                    strEntradaserie = dr["Entradaserie"].ToString();
                    intEntradafolio = Convert.ToInt32(dr["Entradafolio"]);
                    intPolizasID = Convert.ToInt32(dr["PolizasID"]);
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

        public DataTable NotasdecreditodetalleLlenaCajas()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NotasdecreditodetalleLlenaCajas";
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
        } //notasdecreditoGrid

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

        public string BuscarImpuestosporSerieyFolioCfdi()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasLeeIvayRet";
                cmd.Parameters.AddWithValue("@prmSerie", strSerieCfdi);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    dTotalimpuestosret = Convert.ToDecimal(dr["TotalmpuestosRetenidos"]);
                    dTotalimpuestostras = Convert.ToDecimal(dr["TotalImpuestosTrasladados"]);

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

        public string NotasdecreditoCancelar()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NotasdecreditoCancelar";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);
                cmd.Parameters.AddWithValue("@prmRazondecancelacion", strRazon);
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
        }

        #endregion
    }
}