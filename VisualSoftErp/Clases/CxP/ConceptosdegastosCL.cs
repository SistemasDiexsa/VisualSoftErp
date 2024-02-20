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
    public class ConceptosdegastosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intConceptosdegastosID { get; set; }
        public string strNombre { get; set; }
        public string strCuenta { get; set; }
        public decimal dRetIva { get; set; }
        public decimal dRetIsr { get; set; }
        public string strCuentaRetIva { get; set; }
        public string strCuentaRetIsr { get; set; }
        #endregion

        #region Constructor
        public ConceptosdegastosCL()
        {
            intConceptosdegastosID = 0;
            strNombre = string.Empty;
            strCuenta = string.Empty;
            dRetIva = 0;
            dRetIsr = 0;
            strCuentaRetIva = string.Empty;
            strCuentaRetIsr = string.Empty;
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
        public DataTable ConceptosdegastosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "conceptosdegastosGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //conceptosdegastosGrid

        public string ConceptosdegastosCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ConceptosdegastosCRUD";
                cmd.Parameters.AddWithValue("@prmConceptosdegastosID", intConceptosdegastosID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmCuenta", strCuenta);
                cmd.Parameters.AddWithValue("@prmRetIva", dRetIva);
                cmd.Parameters.AddWithValue("@prmRetIsr", dRetIsr);
                cmd.Parameters.AddWithValue("@prmCuentaRetIva", strCuentaRetIva);
                cmd.Parameters.AddWithValue("@prmCuentaRetIsr", strCuentaRetIsr);
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
        } //ConceptosdegastosCrud

        public string ConceptosdegastosLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ConceptosdegastosllenaCajas";
                cmd.Parameters.AddWithValue("@prmConceptosdegastosID", intConceptosdegastosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intConceptosdegastosID = Convert.ToInt32(dr["ConceptosdegastosID"]);
                    strNombre = dr["Nombre"].ToString();
                    strCuenta = dr["Cuenta"].ToString();
                    dRetIva = Convert.ToDecimal(dr["RetIva"]);
                    dRetIsr = Convert.ToDecimal(dr["RetIsr"]);
                    strCuentaRetIva = dr["CuentaRetIva"].ToString();
                    strCuentaRetIsr = dr["CuentaRetIsr"].ToString();
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

        public string ConceptosdegastosEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ConceptosdegastosEliminar";
                cmd.Parameters.AddWithValue("@prmConceptosdegastosID", intConceptosdegastosID);
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