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
    public class FacturasCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intID { get; set; }
        public string strDoc { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strOrigen { get; set; }
        public string sPedidoSerie { get; set; }
        public int intPedidoFolio { get; set; }
        public DateTime fFecha { get; set; }
        public string strc_FormaPago { get; set; }
        public string strCondicionesdepago { get; set; }
        public decimal dSubTotal { get; set; }
        public decimal dDescuento { get; set; }
        public string strc_Moneda { get; set; }
        public decimal dTipoCambio { get; set; }
        public decimal dTotal { get; set; }
        public string strc_Tipodecomprobante { get; set; }
        public string strc_MetodoPago { get; set; }
        public string strLugarExpedicionCP { get; set; }
        public string strConfirmacion { get; set; }
        public string strTipoRelacion { get; set; }
        public string strCfdiRelacionadoUUID { get; set; }
        public int intClientesID { get; set; }
        public string strc_UsoCFDI { get; set; }
        public decimal dTotalmpuestosRetenidos { get; set; }
        public decimal dTotalImpuestosTrasladados { get; set; }
        public decimal dPorcentajededescuento { get; set; }
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
        public string strOC { get; set; }

        public string strIVA { get; set; }
        public string strRetIva { get; set; }
        public string strRetIsr { get; set; }

        public string strStatus { get; set; }
        public string strRazon { get; set; }

        public int intEje { get; set; }
        public int intMes { get; set; }
        public int intAño { get; set; }
        public int intDias { get; set; }
        public int intCanalesdeventaID { get; set; }
        public int intPagina { get; set; }

        #endregion

        #region Constructor
        public FacturasCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            dtm = null;
            dtd = null;
            intID = 0;
            intUsuarioID = 0;

            strMaquina = string.Empty;
            DateTime fFecha = DateTime.Now;
            string strC_formapago = string.Empty;
            string strCondicionesdepago = string.Empty;
            decimal dSubtotal = 0;
            decimal dDescuento = 0;
            string strC_moneda = string.Empty;
            decimal dTipocambio = 0;
            decimal dTotal = 0;
            string strC_tipodecomprobante = string.Empty;
            string strC_metodopago = string.Empty;
            string strLugarexpedicioncp = string.Empty;
            string strConfirmacion = string.Empty;
            string strTiporelacion = string.Empty;
            string strCfdirelacionadouuid = string.Empty;
            int intClientesid = 0;
            string strC_usocfdi = string.Empty;
            decimal dTotalmpuestosretenidos = 0;
            decimal dTotalimpuestostrasladados = 0;
            decimal dPorcentajededescuento = 0;
            string strUuid = string.Empty;
            int intAgentesid = 0;
            int intStatus = 0;
            int intPlazo = 0;
            DateTime fFechacancelacion = DateTime.Now;
            string strRazoncancelacion = string.Empty;
            int intUsuariosid = 0;
            int intAlmacenesid = 0;
            int intExportacion = 0;
            string strObservaciones = string.Empty;
            string strPredial = string.Empty;
            string strOC = string.Empty;
            string strIVA = string.Empty;
            string strRetIva = string.Empty;
            string strRetIsr = string.Empty;

            string strStatus = string.Empty;
            string strRazon = string.Empty;

            strOrigen = string.Empty;
            sPedidoSerie = string.Empty;
            intPedidoFolio = 0;

            intEje = 0;
            intMes = 0;
            intDias = 0;
            intPagina = 0;
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
        
        public DataTable FacturasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasGRID";
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                // cmd.Parameters.AddWithValue("@prmPagina", intPagina);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //facturasGrid

        public DataTable FacturasMovimientos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasVerSusMovimientos";
                cmd.Parameters.AddWithValue("@prmserie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //facturasGrid

        public string FacturasCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasCrud";
                cmd.Parameters.AddWithValue("@prmFac", dtm);
                cmd.Parameters.AddWithValue("@prmFacDet", dtd);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);
                cmd.Parameters.AddWithValue("@prmOrigen", strOrigen);
                cmd.Parameters.AddWithValue("@prmPedidoSerie", sPedidoSerie);
                cmd.Parameters.AddWithValue("@prmPedidoFolio", intPedidoFolio);

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
        }//REmisionescrud

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
                    dSubTotal = Convert.ToDecimal(dr["SubTotal"]);
                    dDescuento = Convert.ToDecimal(dr["Descuento"]);
                    dPorcentajededescuento = Convert.ToDecimal(dr["Porcentajededescuento"]);
                   
                    strc_Moneda = dr["c_Moneda"].ToString();
                    dTipoCambio = Convert.ToDecimal(dr["TipoCambio"]);
                    dTotal = Convert.ToDecimal(dr["Total"]);
                    //     strc_Tipodecomprobante = dr["c_Tipodecomprobante"].ToString();
                    strc_MetodoPago = dr["c_MetodoPago"].ToString();
                    strLugarExpedicionCP = dr["LugarExpedicionCP"].ToString();
                    strConfirmacion = dr["Confirmacion"].ToString();
                    strTipoRelacion = dr["TipoRelacion"].ToString();
                    strCfdiRelacionadoUUID = dr["CfdiRelacionadoUUID"].ToString();
                    intClientesID = Convert.ToInt32(dr["ClientesID"]);
                    strc_UsoCFDI = dr["c_UsoCFDI"].ToString();
                    dTotalmpuestosRetenidos = Convert.ToDecimal(dr["TotalmpuestosRetenidos"]);
                    dTotalImpuestosTrasladados = Convert.ToDecimal(dr["TotalImpuestosTrasladados"]);
                    strUUID = dr["UUID"].ToString();
                    intAgentesID = Convert.ToInt32(dr["AgentesID"]);
                    intStatus = Convert.ToInt32(dr["Status"]);
                    intPlazo = Convert.ToInt32(dr["Plazo"]);
                    intCanalesdeventaID = Convert.ToInt32(dr["CanalesdeventaID"]);
                    //  fFechaCancelacion = Convert.ToDateTime(dr["FechaCancelacion"]);
                    //     strRazonCancelacion = dr["RazonCancelacion"].ToString();
                    intUsuariosID = Convert.ToInt32(dr["UsuariosID"]);
                    intAlmacenesID = Convert.ToInt32(dr["AlmacenesID"]);
                    intExportacion = Convert.ToInt32(dr["Exportacion"]);
                    //     strObservaciones = dr["Observaciones"].ToString(); ///no hay campo
                    strPredial = dr["Predial"].ToString();
                    strOC = dr["OC"].ToString();
                    strObservaciones = dr["Observaciones"].ToString();
          
                    strIVA = dr["Iva"].ToString();
                    strRetIva = dr["RetIva"].ToString();
                    strRetIsr = dr["RetIsr"].ToString();

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

        public string LeeDocFecha()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "cfdiUUIDNuevoLeer";
                cmd.Parameters.AddWithValue("@prmDoc", strDoc);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
           
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    intClientesID = Convert.ToInt32(dr["ClientesID"]);

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

        public DataTable FacturasDetalleGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasDetalleLlenaCajas";
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

        public string FacturasCambiarStatus()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                switch (strStatus)
                {
                    case "Cancelar":
                        cmd.CommandText = "FacturasCancelar";
                        break;
                }
                //hay que cambiar
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

        public DataTable Clientesquenohancomprado()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasClientesquecompraronultimomes";
                cmd.Parameters.AddWithValue("@prmEje", intEje);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.Parameters.AddWithValue("@prmDias", intDias);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception)
            {
                return dt;
            }
        } //facturasGrid

        public string ClientesquenohancompradoFacturaAnterior()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();          
                cmd.CommandText = "FacturasClientesquecompraronultimomesFacturaAnterior";
                cmd.Parameters.AddWithValue("@prmCte", intClientesID);
                cmd.Parameters.AddWithValue("@prmEje", intEje);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strSerie= dr["Serie"].ToString();
                    intFolio = Convert.ToInt32(dr["Folio"]);
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
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

        public string FacturasLeeFecha()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FacturasLeeFecha";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    intClientesID = Convert.ToInt32(dr["ClientesID"]);
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
        }//FacturasLeeFecha

        #endregion
    }
}