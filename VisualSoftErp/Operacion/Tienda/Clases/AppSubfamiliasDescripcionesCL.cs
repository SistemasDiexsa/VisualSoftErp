using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Tienda.Clases
{
    public class AppSubfamiliasDescripcionesCL
    {
        #region Propiedades
        string strCnn =  globalCL.gv_strcnn;
        public int intFamiliasID { get; set; }
        public int intSubfamiliasID { get; set; }
        public int intSeq { get; set; }
        public string strDescripcion { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intUsuarioID { get; set; }

        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        #endregion

        #region Constructor
        public AppSubfamiliasDescripcionesCL()
        {
            intFamiliasID = 0;
            intSubfamiliasID = 0;
            intSeq = 0;
            strDescripcion = string.Empty;
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
        public DataTable AppSubfamiliasDescripcionesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppSubfamiliasDescripcionesGRID";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //appsubfamiliasdescripcionesGrid

        public DataTable AppSubfamiliasDescripcionesLlenaCajasDetalle()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppSubfamiliasDescripcionesLlenaCajasDetalle";
                cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
                cmd.Parameters.AddWithValue("@prmSubfamiliasID", intSubfamiliasID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //notasdecreditoGrid

        public string AppSubfamiliasDescripcionesCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppSubfamiliasDescripcionesCRUD";
                cmd.Parameters.AddWithValue("@prmAppSubfamiliasDescripciones", dtm);
                cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
                cmd.Parameters.AddWithValue("@prmSubfamiliasID", intSubfamiliasID);
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
        } //AppSubfamiliasDescripcionesCrud

        

        public string AppSubfamiliasDescripcionesEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppSubfamiliasDescripcionesEliminar";
                cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
                cmd.Parameters.AddWithValue("@prmSubfamiliasID", intSubfamiliasID);
                cmd.Parameters.AddWithValue("@prmSeq", intSeq);
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
