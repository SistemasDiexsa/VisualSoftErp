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
    public class agentesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intAgentesID { get; set; }
        public string strNombre { get; set; }
        public string strTelefono { get; set; }
        public string strEmail { get; set; }
        public string strPuesto { get; set; }
        public string strEncabezado { get; set; }
        public string strPiedepagina { get; set; }
        #endregion

        #region Constructor
        public agentesCL()
        {
            intAgentesID = 0;
            strNombre = string.Empty;
            strTelefono = string.Empty;
            strEmail = string.Empty;
            strPuesto = string.Empty;
            strEncabezado = string.Empty;
            strPiedepagina = string.Empty;
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
            catch (Exception)
            {
                return "";
            }
        }
        public DataTable AgentesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "agentesGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //agentesGrid

        public string AgentesCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AgentesCRUD";
                cmd.Parameters.AddWithValue("@prmAgentesID", intAgentesID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmTelefono", strTelefono);
                cmd.Parameters.AddWithValue("@prmPuesto", strPuesto);
                cmd.Parameters.AddWithValue("@prmEncabezado", strEncabezado);
                cmd.Parameters.AddWithValue("@prmPiedepagina", strPiedepagina);
                cmd.Parameters.AddWithValue("@prmEmail", strEmail);
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
        } //AgentesCrud

        public string AgentesLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AgentesllenaCajas";
                cmd.Parameters.AddWithValue("@prmAgentesID", intAgentesID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intAgentesID = Convert.ToInt32(dr["AgentesID"]);
                    strNombre = dr["Nombre"].ToString();
                    strTelefono = dr["Telefono"].ToString();
                    strPuesto = dr["Puesto"].ToString();
                    strEncabezado = dr["Encabezado"].ToString();
                    strPiedepagina = dr["Piedepagina"].ToString();
                    strEmail = dr["Email"].ToString();
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

        public string AgentesEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AgentesEliminar";
                cmd.Parameters.AddWithValue("@prmAgentesID", intAgentesID);
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

        #endregion
    }
}