using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Windows.Forms;

namespace VisualSoftErp.Clases
{
    public class CierresdemodulosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intEjercicio { get; set; }
        public int intMes { get; set; }      
        public int intUsuario { get; set; }
        public string strCOM { get; set; }
        public string strCXP { get; set; }
        public string strINV { get; set; }
        public string strVTA { get; set; }
        public string strCXC { get; set; }
        public string strModulo { get; set; }

        public List<string> List1;
   
        #endregion

        #region Constructor
        public CierresdemodulosCL()
        {
            int intEjercicio = 0;
            int intMes = 0;
            string strModulo = string.Empty;
            string strStatus = string.Empty;
            
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

        public DataTable CierresdemodulosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "cierresdemodulosGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //cierresdemodulosGrid

        public string CierresdemodulosCrud()
        {
            try
            {
                String strComputerName = System.Environment.MachineName;
               

                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CierresDeModulos_CRUD";
                cmd.Parameters.AddWithValue("@prmEjercicio", intEjercicio);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.Parameters.AddWithValue("@prmStatusCOM", strCOM);
                cmd.Parameters.AddWithValue("@prmStatusCXP", strCXP);
                cmd.Parameters.AddWithValue("@prmStatusINV", strINV);
                cmd.Parameters.AddWithValue("@prmStatusVTA", strVTA);
                cmd.Parameters.AddWithValue("@prmStatusCXC", strCXC);
                cmd.Parameters.AddWithValue("@prmUsu", intUsuario);
                cmd.Parameters.AddWithValue("@prmMaq", strComputerName);               
	
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
        } //CierresdemodulosCrud

        public string CierresdemodulosStatus()
        {
            try
            {
                string result = string.Empty;
                ListBox listBox1 = new ListBox();

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CierresdemodulosRevisaStatus";
                cmd.Parameters.AddWithValue("@prmEjercicio", intEjercicio);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.Parameters.AddWithValue("@prmModulo", strModulo);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
               
                if (dr.HasRows)
                {
                    dr.Read();

                    result = dr["Status"].ToString();
                                                         
                }
                else
                {
                    result = "1";
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

        

        public string CierresdemodulosEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CierresdemodulosEliminar";
                cmd.Parameters.AddWithValue("@prmEjercicio", intEjercicio);
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
        } // Public Class Eliminar

        #endregion
    }
}