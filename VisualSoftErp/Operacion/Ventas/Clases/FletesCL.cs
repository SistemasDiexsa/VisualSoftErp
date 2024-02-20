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
    public class FletesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public DateTime strFecha { get; set; }
        public decimal dSubtotal { get; set; }
        public decimal dIva { get; set; }
        public decimal dRetiva { get; set; }
        public decimal dNeto { get; set; }
        public string strGuia { get; set; }
        public string strNumerodecontrol { get; set; }
        public decimal dSeguro { get; set; }
        public decimal dManeaje { get; set; }
        public int intPorcobrar { get; set; }
        public string strOcurreoAdomicilio { get; set; }
        public int intTransportesID { get; set; }
        public int intCiudadorigenID { get; set; }
        public int intCiudaddestinoID { get; set; }
        public string strNotas { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }  public string strDoc { get; set; }public string strExiste { get; set; }
        public string strRazon { get; set; }
        #endregion

        #region Constructor
        public FletesCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            strFecha = DateTime.Now;
            dSubtotal = 0;
            dIva = 0;
            dRetiva = 0;
            dNeto = 0;
            strGuia = string.Empty;
            strNumerodecontrol = string.Empty;
            dSeguro = 0;
            dManeaje = 0;
            intPorcobrar = 0;
            strOcurreoAdomicilio = string.Empty;
            intTransportesID = 0;
            intCiudadorigenID = 0;
            intCiudaddestinoID = 0;
            strNotas = string.Empty; strDoc = string.Empty; strExiste = string.Empty;
            strRazon = string.Empty;
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

        public DataTable FletesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FletesGRID";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //fletesGrid

        public string FletesCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FletesCRUD";
                cmd.Parameters.AddWithValue("@prmFletes", dtm);
                cmd.Parameters.AddWithValue("@prmFletesdetalle", dtd);
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
        } //FletesCrud

        public string FletesLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FletesllenaCajas";
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
                    strFecha = Convert.ToDateTime(dr["Fecha"]);
                    dSubtotal = Convert.ToDecimal(dr["Subtotal"]);
                    dIva = Convert.ToDecimal(dr["Iva"]);
                    dRetiva = Convert.ToDecimal(dr["Retiva"]);
                    dNeto = Convert.ToDecimal(dr["Neto"]);
                    strGuia = dr["Guia"].ToString();
                    strNumerodecontrol = dr["Numerodecontrol"].ToString();
                    dSeguro = Convert.ToDecimal(dr["Seguro"]);                
                    dManeaje = Convert.ToDecimal(dr["Meneaje"]);
                    intPorcobrar = Convert.ToInt32(dr["Porcobrar"]);
                    strOcurreoAdomicilio = dr["OcurreoAdomicilio"].ToString();
                    intTransportesID = Convert.ToInt32(dr["TransportesID"]);
                    intCiudadorigenID = Convert.ToInt32(dr["CiudadorigenID"]);
                    intCiudaddestinoID = Convert.ToInt32(dr["CiudaddestinoID"]);
                    strNotas = dr["Notas"].ToString();
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

        public string FletesValidarDoc()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DocumentosValidarFlete";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmDoc", strDoc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strExiste = dr["Serie"].ToString();                  
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

        public DataTable FletesDetalleGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FletesDetalleGrid";
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
        } //fletesGrid

        public string FletesCancelar()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FletesCancelar";
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