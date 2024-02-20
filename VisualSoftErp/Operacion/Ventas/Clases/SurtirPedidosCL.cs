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
    public class SurtirPedidosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public int intSurtidoSeq { get; set; }
        public int intPedidosdetalleseq { get; set; }
        public decimal intCantidad { get; set; }
        public int intArticulofisicoID { get; set; }
        public string strSerieFactura { get; set; }
        public int intFactura { get; set; }
        public string strLote { get; set; }
        public string strPallet { get; set; }
        public DateTime fFecha { get; set; }
        public string strPlantaentrega { get; set; }
        public int intObservacionesID { get; set; }
        public int intUsuariosID { get; set; }

        public DataTable dtd { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strHora { get; set; }
        #endregion

        #region Constructor
        public SurtirPedidosCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            intSurtidoSeq = 0;
            intPedidosdetalleseq = 0;
            intCantidad = 0;
            intArticulofisicoID = 0;
            strSerieFactura = string.Empty;
            intFactura = 0;
            strLote = string.Empty;
            strPallet = string.Empty;
            fFecha = DateTime.Now;
            strPlantaentrega = string.Empty;
            intObservacionesID = 0;
            intUsuariosID = 0;
            dtd = null;
            intUsuarioID = 0;
            strMaquina = string.Empty;
            strHora = string.Empty;
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

        public DataTable PedidosSurtidosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosSurtirPedidosGrid";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //pedidossurtidosGrid

        public DataTable PedidosSurtidosLlenaLista()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosPorSurtirDetalle";
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
        } //pedidossurtidosGrid

        public String PedidosSurtidosSiguienteSeq()
        {
            DataTable dt = new DataTable();
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosSurtidosSeq";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intSurtidoSeq = Convert.ToInt32(dr["SurtidoSeq"]);
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
        } //pedidossurtidosGrid

        public string PedidosSurtidosCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PedidosSurtidosCRUD";
                
                cmd.Parameters.AddWithValue("@prmPedidosSurtidos", dtd);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmHora", strHora);
                cmd.Parameters.AddWithValue("@prmUsuarioID", intUsuarioID);
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
        } //PedidosSurtidosCrud

       
        #endregion
    }
}