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
    public class TipodemovimientocxpCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intTiposdemovimientocxpID { get; set; }
        public string strNombre { get; set; }
        public string strTipo { get; set; }
        public int intReservado { get; set; }
        #endregion

        #region Constructor
        public TipodemovimientocxpCL()
        {
            intTiposdemovimientocxpID = 0;
            strNombre = string.Empty;
            strTipo = string.Empty;
            intReservado = 0;
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
        public DataTable TipodemovimientocxpGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "tipodemovimientocxpGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //tipodemovimientocxpGrid

        public string TipodemovimientocxpCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TipodemovimientocxpCRUD";
                cmd.Parameters.AddWithValue("@prmTiposdemovimientocxpID", intTiposdemovimientocxpID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmTipo", strTipo);
                cmd.Parameters.AddWithValue("@prmReservado", intReservado);
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
        } //TipodemovimientocxpCrud

        public string TipodemovimientocxpLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TipodemovimientocxpllenaCajas";
                cmd.Parameters.AddWithValue("@prmTiposdemovimientocxpID", intTiposdemovimientocxpID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intTiposdemovimientocxpID = Convert.ToInt32(dr["TiposdemovimientocxpID"]);
                    strNombre = dr["Nombre"].ToString();
                    strTipo = dr["Tipo"].ToString();
                    intReservado = Convert.ToInt32(dr["Reservado"]);
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

        public string TipodemovimientocxpEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TipodemovimientocxpEliminar";
                cmd.Parameters.AddWithValue("@prmTiposdemovimientocxpID", intTiposdemovimientocxpID);
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