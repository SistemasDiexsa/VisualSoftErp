using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace VisualSoftErp.Clases.VentasCLs
{
    public class PedidosCL
    {
        #region Propiedades
        string strCnn = ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public DateTime fFecha { get; set; }
        public int intClientesID { get; set; }
        public int intAgentesID { get; set; }
        public decimal intSubtotal { get; set; }
        public decimal intIva { get; set; }
        public decimal intRetIva { get; set; }
        public decimal intIeps { get; set; }
        public decimal intNeto { get; set; }
        public decimal intDescuento { get; set; }
        public string strObservaciones { get; set; }
        public string strCondicionesdepago { get; set; }
        public int intPlazo { get; set; }
        public int intStatus { get; set; }
        public DateTime fFechacancelacion { get; set; }
        public string strRazoncancelacion { get; set; }
        public int intUsuariosID { get; set; }
        public int intTransportesID { get; set; }
        public string strMonedasID { get; set; }
        public int intAlmacenesID { get; set; }
        public string strOc { get; set; }
        public DateTime fFechaestimadadeentrega { get; set; }
        public decimal fTipodecambio { get; set; }
        public int intExportacion { get; set; }
        public int intStatuscxc { get; set; }
        public int intStatusexistencia { get; set; }
        public int intStatusexistenciaparcial { get; set; }
        public int intStatusbajocosto { get; set; }
        public int intDepurado { get; set; }
        public string strRazondepurado { get; set; }
        public DateTime fFechadepurado { get; set; }
        public int intUsuariodepurado { get; set; }
        public int intID { get; set; }
        public string strDoc { get; set; }
        public string strFechadepurado { get; set; }

        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }

        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }

        public string strIVA { get; set; }
        public string strRetIva { get; set; }
        public string strRetIsr { get; set; }
        public decimal dSubTotal { get; set; }
        public decimal dDescuento { get; set; }

        public string strStatus { get; set; }
        public string strRazon { get; set; }
        #endregion

        #region Constructor
        public PedidosCL()
        {
            string strSerie = string.Empty;
            int intFolio = 0;
            DateTime fFecha = DateTime.Now;
            int intClientesid = 0;
            int intAgentesid = 0;
            decimal intSubtotal = 0;
            decimal intIva = 0;
            decimal intRetiva = 0;
            decimal intIeps = 0;
            decimal intNeto = 0;
            decimal intDescuento = 0;
            string strObservaciones = string.Empty;
            string strCondicionesdepago = string.Empty;
            int intPlazo = 0;
            int intStatus = 0;
            DateTime fFechacancelacion = DateTime.Now;
            string strRazoncancelacion = string.Empty;
            int intUsuariosid = 0;
            int intTransportesid = 0;
            string strMonedasid = string.Empty;
            int intAlmacenesid = 0;
            string strOc = string.Empty;
            DateTime fFechaestimadadeentrega = DateTime.Now;
            decimal fTipodecambio = 0;
            int intExportacion = 0;
            int intStatuscxc = 0;
            int intStatusexistencia = 0;
            int intStatusexistenciaparcial = 0;
            int intStatusbajocosto = 0;
            int intDepurado = 0;
            string strRazondepurado = string.Empty;
            DateTime fFechadepurado = DateTime.Now;
            int intUsuariodepurado = 0;
            string strIVA = string.Empty;
            string strRetIva = string.Empty;
            string strRetIsr = string.Empty;
            decimal dSubtotal = 0;
            decimal dDescuento = 0;

            DataTable dtm = null;
            DataTable dtd = null;
            int intUsuarioID = 0;
            string strMaquina = string.Empty;
            string strPrograma = string.Empty; string strFechadepurado = string.Empty;
            string strStatus = string.Empty;
            string strRazon = string.Empty;
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

        public DataTable PedidosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "pedidosGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //pedidosGrid

        public string PedidosCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosCRUD";
                cmd.Parameters.AddWithValue("@prmPedidos", dtm);
                cmd.Parameters.AddWithValue("@prmPedidosDetalle", dtd);
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
        } //PedidosCrud

        public string PedidosLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosllenaCajas";
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
                    intAgentesID = Convert.ToInt32(dr["AgentesID"]);
                    intSubtotal = Convert.ToInt32(dr["AgentesID"]);
                    intIva = Convert.ToInt32(dr["AgentesID"]);
                    intRetIva = Convert.ToInt32(dr["AgentesID"]);
                    intIeps = Convert.ToInt32(dr["AgentesID"]);
                    intNeto = Convert.ToInt32(dr["AgentesID"]);
                    intDescuento = Convert.ToInt32(dr["AgentesID"]);
                    strObservaciones = dr["Observaciones"].ToString();
                    strCondicionesdepago = dr["Condicionesdepago"].ToString();
                    intPlazo = Convert.ToInt32(dr["Plazo"]);
                    intStatus = Convert.ToInt32(dr["Status"]);
                    fFechacancelacion = Convert.ToDateTime(dr["Fechacancelacion"]);
                    strRazoncancelacion = dr["Razoncancelacion"].ToString();
                    intUsuariosID = Convert.ToInt32(dr["UsuariosID"]);
                    intTransportesID = Convert.ToInt32(dr["TransportesID"]);
                    strMonedasID = dr["MonedasID"].ToString();
                    intAlmacenesID = Convert.ToInt32(dr["AlmacenesID"]);
                    strOc = dr["Oc"].ToString();
                    fFechaestimadadeentrega = Convert.ToDateTime(dr["Fechaestimadadeentrega"]);
                    fTipodecambio = Convert.ToDecimal(dr["Tipodecambio"]);
                    intExportacion = Convert.ToInt32(dr["Exportacion"]);
                    intStatuscxc = Convert.ToInt32(dr["Statuscxc"]);
                    intStatusexistencia = Convert.ToInt32(dr["Statusexistencia"]);
                    intStatusexistenciaparcial = Convert.ToInt32(dr["Statusexistenciaparcial"]);
                    intStatusbajocosto = Convert.ToInt32(dr["Statusbajocosto"]);
                    intDepurado = Convert.ToInt32(dr["Depurado"]);
                    strRazondepurado = dr["Razondepurado"].ToString();
                    strFechadepurado = dr["Razondepurado"].ToString();
                    intUsuariodepurado = Convert.ToInt32(dr["Usuariodepurado"]);

                    dSubTotal = Convert.ToDecimal(dr["SubTotal"]);
                    dDescuento = Convert.ToDecimal(dr["Descuento"]);
                    strIVA = dr["Iva"].ToString();
                    strRetIva = dr["RetIva"].ToString();
             
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

        public DataTable PedidosDetalleGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosDetalleLlenaCajas";
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

        public string BuscarOrdendecompra()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosBuscarOC";
                cmd.Parameters.AddWithValue("@prmClientesID", intClientesID);
                cmd.Parameters.AddWithValue("@prmOC", strOc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strOc = dr["Oc"].ToString();
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
            #endregion
        }

        public string PedidosCambiarStatus()
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
                        cmd.CommandText = "PedidosCancelar";
                        break;


                }
                
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

        public DataTable PedidosBitacoraGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosBitacoraGrid";
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


    }
}
