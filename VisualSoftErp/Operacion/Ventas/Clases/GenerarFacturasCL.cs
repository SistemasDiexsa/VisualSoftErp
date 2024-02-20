using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Ventas.Clases
{
    class GenerarFacturasCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public DateTime fFecha { get; set; }
        public string strc_FormaPago { get; set; }
        public string strCondicionesdepago { get; set; }
        public decimal strSubTotal { get; set; }
        public decimal strDescuento { get; set; }
        public string strc_Moneda { get; set; }
        public decimal strTipoCambio { get; set; }
        public decimal strTotal { get; set; }
        public string strc_Tipodecomprobante { get; set; }
        public string strc_MetodoPago { get; set; }
        public string strLugarExpedicionCP { get; set; }
        public string strConfirmacion { get; set; }
        public string strTipoRelacion { get; set; }
        public string strCfdiRelacionadoUUID { get; set; }
        public int intClientesID { get; set; }
        public string strc_UsoCFDI { get; set; }
        public decimal strTotalmpuestosRetenidos { get; set; }
        public decimal strTotalImpuestosTrasladados { get; set; }
        public string strUUID { get; set; }
        public int intAgentesID { get; set; }
        public int intStatus { get; set; }
        public int intPlazo { get; set; }
        public DateTime fFechaCancelacion { get; set; }
        public string strRazonCancelacion { get; set; }
        public int intUsuariosID { get; set; }
        public int intAlmacenesID { get; set; }
        public int intExportacion { get; set; }
        public string strObservaciones { get; set; }
        public string strPredial { get; set; }
        public string strOc { get; set; }
        public string strSeriefacturarelacionada { get; set; }
        public int intFoliofacturarelacionada { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        public int intDesglosaDescuento { get; set; }
        #endregion

        #region Constructor
        public GenerarFacturasCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            fFecha = DateTime.Now;
            strc_FormaPago = string.Empty;
            strCondicionesdepago = string.Empty;
            strSubTotal = 0;
            strDescuento = 0;
            strc_Moneda = string.Empty;
            strTipoCambio = 0;
            strTotal = 0;
            strc_Tipodecomprobante = string.Empty;
            strc_MetodoPago = string.Empty;
            strLugarExpedicionCP = string.Empty;
            strConfirmacion = string.Empty;
            strTipoRelacion = string.Empty;
            strCfdiRelacionadoUUID = string.Empty;
            intClientesID = 0;
            strc_UsoCFDI = string.Empty;
            strTotalmpuestosRetenidos = 0;
            strTotalImpuestosTrasladados = 0;
            strUUID = string.Empty;
            intAgentesID = 0;
            intStatus = 0;
            intPlazo = 0;
            fFechaCancelacion = DateTime.Now;
            strRazonCancelacion = string.Empty;
            intUsuariosID = 0;
            intAlmacenesID = 0;
            intExportacion = 0;
            strObservaciones = string.Empty;
            strPredial = string.Empty;
            strOc = string.Empty;
            strSeriefacturarelacionada = string.Empty;
            intFoliofacturarelacionada = 0;
            intDesglosaDescuento = 0;
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
        public DataTable GenerarFacturasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosSurtidosPorFacturar";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //facturasGrid

        public string GenerarFacturasCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GenerarFacturasCRUD";
                cmd.Parameters.AddWithValue("@prmFac", dtm);
                cmd.Parameters.AddWithValue("@prmFacDet", dtd);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmPedidoFolio", intFoliofacturarelacionada);
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
        } //FacturasCrud

        public string FacturasLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasllenaCajas";
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
                    strc_FormaPago = dr["c_FormaPago"].ToString();
                    strCondicionesdepago = dr["Condicionesdepago"].ToString();
                    //strSubTotal = dr["Condicionesdepago"].ToString();
                    //strDescuento = dr["Condicionesdepago"].ToString();
                    //strc_Moneda = dr["c_Moneda"].ToString();
                    //strTipoCambio = dr["c_Moneda"].ToString();
                    //strTotal = dr["c_Moneda"].ToString();
                    strc_Tipodecomprobante = dr["c_Tipodecomprobante"].ToString();
                    strc_MetodoPago = dr["c_MetodoPago"].ToString();
                    strLugarExpedicionCP = dr["LugarExpedicionCP"].ToString();
                    strConfirmacion = dr["Confirmacion"].ToString();
                    strTipoRelacion = dr["TipoRelacion"].ToString();
                    strCfdiRelacionadoUUID = dr["CfdiRelacionadoUUID"].ToString();
                    intClientesID = Convert.ToInt32(dr["ClientesID"]);
                    strc_UsoCFDI = dr["c_UsoCFDI"].ToString();
                    //strTotalmpuestosRetenidos = dr["c_UsoCFDI"].ToString();
                    //strTotalImpuestosTrasladados = dr["c_UsoCFDI"].ToString();
                    strUUID = dr["UUID"].ToString();
                    intAgentesID = Convert.ToInt32(dr["AgentesID"]);
                    intStatus = Convert.ToInt32(dr["Status"]);
                    intPlazo = Convert.ToInt32(dr["Plazo"]);
                    fFechaCancelacion = Convert.ToDateTime(dr["FechaCancelacion"]);
                    strRazonCancelacion = dr["RazonCancelacion"].ToString();
                    intUsuariosID = Convert.ToInt32(dr["UsuariosID"]);
                    intAlmacenesID = Convert.ToInt32(dr["AlmacenesID"]);
                    intExportacion = Convert.ToInt32(dr["Exportacion"]);
                    strObservaciones = dr["Observaciones"].ToString();
                    strPredial = dr["Predial"].ToString();
                    strOc = dr["Oc"].ToString();
                    strSeriefacturarelacionada = dr["Seriefacturarelacionada"].ToString();
                    intFoliofacturarelacionada = Convert.ToInt32(dr["Foliofacturarelacionada"]);
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

        public DataTable FacturasLeePedidoSurtido()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasLeePedidoSurtido";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmDesglosarDecuento", intDesglosaDescuento);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //remisionesGrid

        #endregion
    }
}
