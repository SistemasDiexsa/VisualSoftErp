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
    public class ZoneArticulosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intArticulosID { get; set; }
        public string strSKU { get; set; }
        public string strPart_Number { get; set; }
        public string strAlt_Part_Number { get; set; }
        public string strDescription { get; set; }
        #endregion

        #region Constructor
        public ZoneArticulosCL()
        {
            intArticulosID = 0;
            strSKU = string.Empty;
            strPart_Number = string.Empty;
            strAlt_Part_Number = string.Empty;
            strDescription = string.Empty;
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
        public DataTable ZoneArticulosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "zonearticulosGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //zonearticulosGrid

        public string ZoneArticulosCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ZoneArticulosCRUD";
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                cmd.Parameters.AddWithValue("@prmSKU", strSKU);
                cmd.Parameters.AddWithValue("@prmPart_Number", strPart_Number);
                cmd.Parameters.AddWithValue("@prmAlt_Part_Number", strAlt_Part_Number);
                cmd.Parameters.AddWithValue("@prmDescription", strDescription);
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
        } //ZoneArticulosCrud

        public string ZoneArticulosLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ZoneArticulosllenaCajas";
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intArticulosID = Convert.ToInt32(dr["ArticulosID"]);
                    strSKU = dr["SKU"].ToString();
                    strPart_Number = dr["Part_Number"].ToString();
                    strAlt_Part_Number = dr["Alt_Part_Number"].ToString();
                    strDescription = dr["Description"].ToString();
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

        public string ZoneArticulosEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ZoneArticulosEliminar";
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
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