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
    public class CasaLey_ArticulosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strArticulo { get; set; }
        public string strCodigoBarras { get; set; }
        public string strCodigoCasaLey { get; set; }
        public string strUMC { get; set; }
        public int intArt { get; set; }
        #endregion

        #region Constructor
        public CasaLey_ArticulosCL()
        {
            strArticulo = string.Empty;
            strCodigoBarras = string.Empty;
            strCodigoCasaLey = string.Empty;
            strUMC = string.Empty;
            intArt = 0;
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
        public DataTable CasaLey_ArticulosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "casaley_articulosGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //casaley_articulosGrid

        public string CasaLey_ArticulosCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CasaLey_ArticulosCRUD";
                cmd.Parameters.AddWithValue("@prmArticulosID", intArt);
                cmd.Parameters.AddWithValue("@prmCodigoBarras", strCodigoBarras);
                cmd.Parameters.AddWithValue("@prmCodigoCasaLey", strCodigoCasaLey);
                cmd.Parameters.AddWithValue("@prmUMC", strUMC);
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
        } //CasaLey_ArticulosCrud

        public string CasaLey_ArticulosLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CasaLey_ArticulosllenaCajas";
                cmd.Parameters.AddWithValue("@prmArticulosID", intArt);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intArt = Convert.ToInt32(dr["ArticulosID"]);
                    strCodigoBarras = dr["CodigoBarras"].ToString();
                    strCodigoCasaLey = dr["CodigoCasaLey"].ToString();
                    strUMC = dr["UMC"].ToString();
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

        public string CasaLey_ArticulosEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CasaLey_ArticulosEliminar";
                cmd.Parameters.AddWithValue("@prmArticulo", strArticulo);
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