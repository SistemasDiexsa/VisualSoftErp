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
    public class ArticulosGruposAuxiliarCL
    {

        #region Propiedades
        string strCnn = ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public int intGruposID { get; set; }
        public string strNombre { get; set; }
        public decimal dAdicionalalmes { get; set; }
        public int intDiasStock { get; set; }
        #endregion

        #region Constructor
        public ArticulosGruposAuxiliarCL()
        {
            intGruposID = 0;
            strNombre = string.Empty;
            dAdicionalalmes = 0;
            intDiasStock = 0;
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
        public DataTable ArticulosGruposAuxiliarGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "articulosgruposauxiliarGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //articulosgruposauxiliarGrid

        public DataTable ArticulosGruposAuxiliarGridDetalle()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ArticulosGruposdeArticulosListar";
                cmd.Parameters.AddWithValue("@prmGruposID", intGruposID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //articulosgruposauxiliarGrid

        public string ArticulosGruposAuxiliarCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ArticulosGruposAuxiliarCRUD";
                cmd.Parameters.AddWithValue("@prmGruposID", intGruposID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmAdicionalalmes", dAdicionalalmes);
                cmd.Parameters.AddWithValue("@prmDiasStock", intDiasStock);
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
        } //ArticulosGruposAuxiliarCrud

        public string ArticulosGruposAuxiliarLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ArticulosGruposAuxiliarllenaCajas";
                cmd.Parameters.AddWithValue("@prmGruposID", intGruposID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intGruposID = Convert.ToInt32(dr["GruposID"]);
                    strNombre = dr["Nombre"].ToString();
                    dAdicionalalmes = Convert.ToDecimal(dr["Adicionalalmes"]);
                    intDiasStock = Convert.ToInt32(dr["DiasStock"]);
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

        public string ArticulosGruposAuxiliarEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ArticulosGruposAuxiliarEliminar";
                cmd.Parameters.AddWithValue("@prmGruposID", intGruposID);
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