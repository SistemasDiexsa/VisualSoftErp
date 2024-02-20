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
    public class ProveedoresCL
    {
        #region Propiedades
        string strCnn = ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public int intProveedoresID { get; set; }
        public string strNombre { get; set; }
        public string strDireccion { get; set; }
        public int intCiudadesID { get; set; }
        public string strRfc { get; set; }
        public string strContacto { get; set; }
        public int intTipo { get; set; }
        public int intPLazo { get; set; }
        public decimal intCreditoautorizado { get; set; }
        public string strTelefono { get; set; }
        public string strEmail { get; set; }
        public string strMonedasID { get; set; }
        public DateTime fFechaderegistro { get; set; }
        public int intTiempodeentrega { get; set; }
        public int intDiasTraslado { get; set; }
        public int intPiva { get; set; }
        public string strObservaciones { get; set; }
        public string strClasificacion { get; set; }
        public decimal decRetiva { get; set; }
        public decimal decRetisr { get; set; }
        public int intEsdeservicio { get; set; }
        public string strCorrespondenciaA { get; set; }
        #endregion

        #region Constructor
        public ProveedoresCL()
        {
            intProveedoresID = 0;
            strNombre = string.Empty;
            strDireccion = string.Empty;
            intCiudadesID = 0;
            strRfc = string.Empty;
            strContacto = string.Empty;
            intTipo = 0;
            intPLazo = 0;
            intCreditoautorizado = 0;
            strTelefono = string.Empty;
            strEmail = string.Empty;
            strMonedasID = string.Empty;
            fFechaderegistro = DateTime.Now;
            intTiempodeentrega = 0;
            intDiasTraslado = 0;
            intPiva = 0;
            strObservaciones = string.Empty;
            strClasificacion = string.Empty;
            decRetiva = 0;
            decRetisr = 0;
            intEsdeservicio = 0;
            strCorrespondenciaA = string.Empty;
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
        public DataTable ProveedoresGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "proveedoresGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //proveedoresGrid

        public string ProveedoresCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresCRUD";
                cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmDireccion", strDireccion);
                cmd.Parameters.AddWithValue("@prmCiudadesID", intCiudadesID);
                cmd.Parameters.AddWithValue("@prmRfc", strRfc);
                cmd.Parameters.AddWithValue("@prmContacto", strContacto);
                cmd.Parameters.AddWithValue("@prmTipo", intTipo);
                cmd.Parameters.AddWithValue("@prmPLazo", intPLazo);
                cmd.Parameters.AddWithValue("@prmCreditoautorizado", intCreditoautorizado);
                cmd.Parameters.AddWithValue("@prmTelefono", strTelefono);
                cmd.Parameters.AddWithValue("@prmEmail", strEmail);
                cmd.Parameters.AddWithValue("@prmMonedasID", strMonedasID);
                cmd.Parameters.AddWithValue("@prmFechaderegistro", fFechaderegistro);
                cmd.Parameters.AddWithValue("@prmTiempodeentrega", intTiempodeentrega);
                cmd.Parameters.AddWithValue("@prmDiasTraslado", intDiasTraslado);
                cmd.Parameters.AddWithValue("@prmPiva", intPiva);
                cmd.Parameters.AddWithValue("@prmObservaciones", strObservaciones);
                cmd.Parameters.AddWithValue("@prmClasificacion", strClasificacion);
                cmd.Parameters.AddWithValue("@prmRetiva", decRetiva);
                cmd.Parameters.AddWithValue("@prmRetisr", decRetisr);
                cmd.Parameters.AddWithValue("@prmEsdeservicio", intEsdeservicio);
                cmd.Parameters.AddWithValue("@prmCorrespondenciaA", strCorrespondenciaA);
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
        } //ProveedoresCrud

        public string ProveedoresLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresllenaCajas";
                cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intProveedoresID = Convert.ToInt32(dr["ProveedoresID"]);
                    strNombre = dr["Nombre"].ToString();
                    strDireccion = dr["Direccion"].ToString();
                    intCiudadesID = Convert.ToInt32(dr["CiudadesID"]);
                    strRfc = dr["Rfc"].ToString();
                    strContacto = dr["Contacto"].ToString();
                    intTipo = Convert.ToInt32(dr["Tipo"]);
                    intPLazo = Convert.ToInt32(dr["PLazo"]);
                    intCreditoautorizado = Convert.ToInt32(dr["PLazo"]);
                    strTelefono = dr["Telefono"].ToString();
                    strEmail = dr["Email"].ToString();
                    strMonedasID = dr["MonedasID"].ToString();
                    fFechaderegistro = Convert.ToDateTime(dr["Fechaderegistro"]);
                    intTiempodeentrega = Convert.ToInt32(dr["Tiempodeentrega"]);
                    intDiasTraslado = Convert.ToInt32(dr["DiasTraslado"]);
                    intPiva = Convert.ToInt32(dr["Piva"]);
                    strObservaciones = dr["Observaciones"].ToString();
                    strClasificacion = dr["Clasificacion"].ToString();
                    decRetiva = Convert.ToDecimal(dr["Retiva"]);
                    decRetisr = Convert.ToDecimal(dr["Retisr"]);
                    intEsdeservicio = Convert.ToInt32(dr["Esdeservicio"]);
                    strCorrespondenciaA = dr["CorrespondenciaA"].ToString();
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

        public string ProveedoresEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresEliminar";
                cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
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