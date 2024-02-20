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
    class RemisionesCL
    {
        #region propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intID { get; set; }
        public string strDoc { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }

        public DateTime fFecha { get; set; }
        public int intClientesID { get; set; }
        public int intAgentesID { get; set; }
        public int intPedidosID { get; set; }
        public decimal intParidad { get; set; }
        public decimal intSubtotal { get; set; }
        public decimal intIva { get; set; }
        public decimal intNeto { get; set; }
        public int intStatus { get; set; }
        public int intPlazo { get; set; }
        public DateTime fFechacancelacion { get; set; }
        public string strRazoncancelacion { get; set; }
        public int intUsuariosID { get; set; }
        public decimal intPIva { get; set; }
        public string strMonedasID { get; set; }
        public int intAlmacenesID { get; set; }
        public int intExportacion { get; set; }
        public string strOrdendecompra { get; set; }
        public string strSolicitadopor { get; set; }
        public int intFormadepago { get; set; }
        public string strCotizacionSerie { get; set; }
        public int intCotizacionFolio { get; set; }
        public string strObservaciones { get; set; }
        public int intMotivosdesalidaID { get; set; }

        public string strStatus { get; set; }
        public string strRazon { get; set; }
        public int intMotivosnoaprobacioncotizacionesID { get; set; }

        #endregion

        #region constructor

        public RemisionesCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            dtm = null;
            dtd = null;
            intID = 0;
            intUsuarioID = 0;
            strMaquina = string.Empty;

            DateTime fFecha = DateTime.Now;
            int intClientesid = 0;
            int intAgentesid = 0;
            int intPedidosid = 0;
            decimal intParidad = 0;
            decimal intSubtotal = 0;
            decimal intIva = 0;
            decimal intNeto = 0;
            int intStatus = 0;
            int intPlazo = 0;
            DateTime fFechacancelacion = DateTime.Now;
            string strRazoncancelacion = string.Empty;
            int intUsuariosid = 0;
            decimal intPiva = 0;
            string strMonedasid = string.Empty;
            int intAlmacenesid = 0;
            int intExportacion = 0;
            string strOrdendecompra = string.Empty;
            string strSolicitadopor = string.Empty;
            int intFormadepago = 0;
            string strCotizacionserie = string.Empty;
            int intCotizacionfolio = 0;
            string strObservaciones = string.Empty;
            int intMotivosdesalidaid = 0;

            string strStatus = string.Empty;
            string strRazon = string.Empty;
            int intMotivosnoaprobacioncotizacionesID;


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

        public DataTable RemisionesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "remisionesGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //remisionesGrid

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

        public string RemisionesCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RemisionesCrud";
                cmd.Parameters.AddWithValue("@prmRem", dtm);
                cmd.Parameters.AddWithValue("@prmRemDet", dtd);
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
        }//REmisionescrud

        public string RemisionesLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RemisionesllenaCajas";
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
                    intPedidosID = Convert.ToInt32(dr["PedidosID"]);
                    intParidad = Convert.ToInt32(dr["PedidosID"]);
                    intSubtotal = Convert.ToInt32(dr["PedidosID"]);
                    intIva = Convert.ToInt32(dr["PedidosID"]);
                    intNeto = Convert.ToInt32(dr["PedidosID"]);
                    intStatus = Convert.ToInt32(dr["Status"]);
                    intPlazo = Convert.ToInt32(dr["Plazo"]);
                    fFechacancelacion = Convert.ToDateTime(dr["Fechacancelacion"]);
                    strRazoncancelacion = dr["Razoncancelacion"].ToString();
                    intUsuariosID = Convert.ToInt32(dr["UsuariosID"]);
                    intPIva = Convert.ToInt32(dr["UsuariosID"]);
                    strMonedasID = dr["MonedasID"].ToString();
                    intAlmacenesID = Convert.ToInt32(dr["AlmacenesID"]);
                    intExportacion = Convert.ToInt32(dr["Exportacion"]);
                    strOrdendecompra = dr["Ordendecompra"].ToString();
                    strSolicitadopor = dr["Solicitadopor"].ToString();
                    intFormadepago = Convert.ToInt32(dr["Formadepago"]);
                    strCotizacionSerie = dr["CotizacionSerie"].ToString();
                    intCotizacionFolio = Convert.ToInt32(dr["CotizacionFolio"]);
                    strObservaciones = dr["Observaciones"].ToString();
                    intMotivosdesalidaID = Convert.ToInt32(dr["MotivosdesalidaID"]);
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

        public DataTable RemisionesDetalleGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RemisionesdetalleLlenaCajas";
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

        public string RemisionesCambiarStatus()
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
                    case "Facturar":
                        cmd.CommandText = "RemisionesFacturar";
                        break;
                    case "Pagarsinfacturar" +
                    "":
                        cmd.CommandText = "RemisionesPagar";
                        break;
                    case "Cancelar":
                        cmd.CommandText = "RemisionesCancelar";
                        break;


                }
                //hay que canmbiar
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
