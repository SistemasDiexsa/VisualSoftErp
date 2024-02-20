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
    public class AccesosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;

        public int intUsuariosID { get; set; }
        public string strProgramaID { get; set; }
        public int intFavorito { get; set; }
        public int intSololectura { get; set; }
        public DataTable dtm { get; set; }
      
        public string strMaquina { get; set; }
        #endregion

        #region Constructor
        public AccesosCL()
        {
            int intUsuariosid = 0;
            string strProgramaid = string.Empty;
            int intFavorito = 0;
            int intSololectura = 0;
            dtm = null;

            strMaquina = string.Empty;
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
        public DataTable AccesosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "accesosGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //accesosGrid

        public string AccesosCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AccesosCRUD";
                cmd.Parameters.AddWithValue("@prmUsuariosID", intUsuariosID);
                cmd.Parameters.AddWithValue("@prmProgramaID", strProgramaID);
                cmd.Parameters.AddWithValue("@prmFavorito", intFavorito);
                cmd.Parameters.AddWithValue("@prmSololectura", intSololectura);
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
                return "AccesosCrud: " + ex.Message;
            }
        } //AccesosCrud

        public string AccesosLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AccesosllenaCajas";
                cmd.Parameters.AddWithValue("@prmUsuariosID", intUsuariosID);
                cmd.Parameters.AddWithValue("@prmProgramaID", strProgramaID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intUsuariosID = Convert.ToInt32(dr["UsuariosID"]);
                    strProgramaID = dr["ProgramaID"].ToString();
                    intFavorito = Convert.ToInt32(dr["Favorito"]);
                    intSololectura = Convert.ToInt32(dr["Sololectura"]);
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

        public string AccesosEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AccesosEliminar";
                cmd.Parameters.AddWithValue("@prmUsuariosID", intUsuariosID);
                cmd.Parameters.AddWithValue("@prmProgramaID", strProgramaID);
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

        public DataTable AccesosGridPrincipal()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AccesosGridPorusuario";
                cmd.Parameters.AddWithValue("@prmUsu", intUsuariosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //accesosGrid

        public string AccesosaProgramasCRUD()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AccesosCrudPorUsuario";
                cmd.Parameters.AddWithValue("@prmAccesos", dtm);
                cmd.Parameters.AddWithValue("@prmUsuariosID", intUsuariosID);
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