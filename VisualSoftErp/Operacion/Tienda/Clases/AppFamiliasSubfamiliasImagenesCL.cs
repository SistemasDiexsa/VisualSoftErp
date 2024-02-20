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
    public class AppFamiliasSubfamiliasImagenesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intAppFamiliasSubfamiliasImagenesID { get; set; }
        public int intFamiliasID { get; set; }
        public int intSubFamiliasID { get; set; }
        public string strImagen { get; set; }
        #endregion

        #region Constructor
        public AppFamiliasSubfamiliasImagenesCL()
        {
            intAppFamiliasSubfamiliasImagenesID = 0;
            intFamiliasID = 0;
            intSubFamiliasID = 0;
            strImagen = string.Empty;
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
        public DataTable AppFamiliasSubfamiliasImagenesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "appfamiliassubfamiliasimagenesGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //appfamiliassubfamiliasimagenesGrid

        public string AppFamiliasSubfamiliasImagenesCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppFamiliasSubfamiliasImagenesCRUD";
                cmd.Parameters.AddWithValue("@prmAppFamiliasSubfamiliasImagenesID", intAppFamiliasSubfamiliasImagenesID);
                cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
                cmd.Parameters.AddWithValue("@prmSubFamiliasID", intSubFamiliasID);
                cmd.Parameters.AddWithValue("@prmImagen", strImagen);
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
        } //AppFamiliasSubfamiliasImagenesCrud

        public string AppFamiliasSubfamiliasImagenesLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppFamiliasSubfamiliasImagenesllenaCajas";
                cmd.Parameters.AddWithValue("@prmAppFamiliasSubfamiliasImagenesID", intAppFamiliasSubfamiliasImagenesID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intAppFamiliasSubfamiliasImagenesID = Convert.ToInt32(dr["AppFamiliasSubfamiliasImagenesID"]);
                    intFamiliasID = Convert.ToInt32(dr["FamiliasID"]);
                    intSubFamiliasID = Convert.ToInt32(dr["SubFamiliasID"]);
                    strImagen = dr["Imagen"].ToString();
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

        public string AppFamiliasSubfamiliasImagenesEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppFamiliasSubfamiliasImagenesEliminar";
                cmd.Parameters.AddWithValue("@prmID", intAppFamiliasSubfamiliasImagenesID);
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