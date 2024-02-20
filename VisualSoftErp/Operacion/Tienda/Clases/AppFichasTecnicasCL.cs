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
    public class AppFichasTecnicasCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intFamiliasID { get; set; }
        public int intSubFamiliasID { get; set; }
        public int intSeq { get; set; }
        public string strDescripcion { get; set; }
        public string strNombreimagen { get; set; }
        #endregion

        #region Constructor
        public AppFichasTecnicasCL()
        {
            intFamiliasID = 0;
            intSubFamiliasID = 0;
            intSeq = 0;
            strDescripcion = string.Empty;
            strNombreimagen = string.Empty;
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
        public DataTable AppFichasTecnicasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "appfichastecnicasGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //appfichastecnicasGrid

        public string AppFichasTecnicasCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppFichasTecnicasCRUD";
                cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
                cmd.Parameters.AddWithValue("@prmSubFamiliasID", intSubFamiliasID);
                cmd.Parameters.AddWithValue("@prmDescripcion", strDescripcion);
                cmd.Parameters.AddWithValue("@prmNombreimagen", strNombreimagen);
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
        } //AppFichasTecnicasCrud

        public string AppFichasTecnicasLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppFichasTecnicasllenaCajas";
                cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
                cmd.Parameters.AddWithValue("@prmSubFamiliasID", intSubFamiliasID);
                cmd.Parameters.AddWithValue("@prmSeq", intSeq);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intFamiliasID = Convert.ToInt32(dr["FamiliasID"]);
                    intSubFamiliasID = Convert.ToInt32(dr["SubFamiliasID"]);
                    intSeq = Convert.ToInt32(dr["Seq"]);
                    strDescripcion = dr["Descripcion"].ToString();
                    strNombreimagen = dr["Nombreimagen"].ToString();
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

        public string AppFichasTecnicasEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppFichasTecnicasEliminar";
                cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
                cmd.Parameters.AddWithValue("@prmSubFamiliasID", intSubFamiliasID);
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