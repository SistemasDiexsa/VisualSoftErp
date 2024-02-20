using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace VisualSoftErp.Clases.CCP_CLs
{
    class AppSolicitudderegistroCL
    {
        #region Propiedades
        string strCnn = ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public int intAppSolicitudderegistroID { get; set; }
        public string strNombre { get; set; }
        public string strCorreo { get; set; }
        public string strTelefono { get; set; }
        public string strCiudad { get; set; }
        public string strComoseenterodenosotros { get; set; }
        public string strComentarios { get; set; }
        public int intAppUsuariosID { get; set; }
        public DateTime fFechaReal { get; set; }
        public int intVerificado { get; set; }
        public DateTime fFechaVerificado { get; set; }
        public int intUsuarioVerifico { get; set; }
        #endregion

        #region Constructor
        public AppSolicitudderegistroCL()
        {
            intAppSolicitudderegistroID = 0;
            strNombre = string.Empty;
            strCorreo = string.Empty;
            strTelefono = string.Empty;
            strCiudad = string.Empty;
            strComoseenterodenosotros = string.Empty;
            strComentarios = string.Empty;
            intAppUsuariosID = 0;
            fFechaReal = DateTime.Now;
            intVerificado = 0;
            fFechaVerificado = DateTime.Now;
            intUsuarioVerifico = 0;
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
        public DataTable AppSolicitudderegistroGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "appsolicitudderegistroGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //appsolicitudderegistroGrid

        public string AppSolicitudderegistroVERIF()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppSolicitudderegistroVERIF";
                cmd.Parameters.AddWithValue("@prmAppSolicitudderegistroID", intAppSolicitudderegistroID);
                cmd.Parameters.AddWithValue("@prmVerificado", intVerificado);
                cmd.Parameters.AddWithValue("@prmFechaVerificado", fFechaVerificado);
                cmd.Parameters.AddWithValue("@prmUsuarioVerifico", intUsuarioVerifico);
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
        } //AppSolicitudderegistroCrud

        public string AppSolicitudderegistroLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppSolicitudderegistrollenaCajas";
                cmd.Parameters.AddWithValue("@prmAppSolicitudderegistroID", intAppSolicitudderegistroID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intAppSolicitudderegistroID = Convert.ToInt32(dr["AppSolicitudderegistroID"]);
                    strNombre = dr["Nombre"].ToString();
                    strCorreo = dr["Correo"].ToString();
                    strTelefono = dr["Telefono"].ToString();
                    strCiudad = dr["Ciudad"].ToString();
                    strComoseenterodenosotros = dr["Comoseenterodenosotros"].ToString();
                    strComentarios = dr["Comentarios"].ToString();
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

        public string AppSolicitudderegistroEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppSolicitudderegistroEliminar";
                cmd.Parameters.AddWithValue("@prmAppSolicitudderegistroID", intAppSolicitudderegistroID);
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
