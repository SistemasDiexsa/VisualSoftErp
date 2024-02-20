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
    public class CasaLeyAddendaCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intPedido { get; set; }
        public string strRemision { get; set; }
        public DateTime fFechaEntrada { get; set; }
        public string strNumeroEntrada { get; set; }
        public string strCentro { get; set; }

        public string strBuyer { get; set; }
        public string strDeptID { get; set; }
        public string strEmail { get; set; }
        public string strPoDate { get; set; }
        public string strPoID { get; set; }
        public string strVendorID { get; set; }
        public string strVersion { get; set; }
        #endregion

        #region Constructor
        public CasaLeyAddendaCL()
        {
            strSerie = string.Empty;
            intPedido = 0;
            strRemision = string.Empty;
            fFechaEntrada = DateTime.Now;
            strNumeroEntrada = string.Empty;
            strCentro = string.Empty;
            strBuyer = string.Empty;
            strDeptID = string.Empty;
            strEmail = string.Empty;
            strPoDate = string.Empty;
            strVendorID = string.Empty;
            strVendorID = string.Empty;

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
        public DataTable CasaLeyAddendaGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "casaleyaddendaGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //casaleyaddendaGrid

        public string CasaLeyAddendaCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CasaLeyAddendaCRUD";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmPedido", intPedido);
                cmd.Parameters.AddWithValue("@prmRemision", strRemision);
                cmd.Parameters.AddWithValue("@prmFechaEntrada", fFechaEntrada);
                cmd.Parameters.AddWithValue("@prmNumeroEntrada", strNumeroEntrada);
                cmd.Parameters.AddWithValue("@prmCentro", strCentro);
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
        } //CasaLeyAddendaCrud

        public string CasaLeyAddendaLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CasaLeyAddendallenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmPedido", intPedido);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strSerie = dr["Serie"].ToString();
                    intPedido = Convert.ToInt32(dr["Pedido"]);
                    strRemision = dr["Remision"].ToString();
                    fFechaEntrada = Convert.ToDateTime(dr["FechaEntrada"]);
                    strNumeroEntrada = dr["NumeroEntrada"].ToString();
                    strCentro = dr["Centro"].ToString();
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

        public string CasaLeyAddendaEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CasaLeyAddendaEliminar";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmPedido", intPedido);
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

        public string ZoneAddendaLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ZoneAddendaLlenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intPedido);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strBuyer = dr["Buyer"].ToString();
                    strDeptID = dr["DeptID"].ToString();
                    strEmail = dr["Email"].ToString();
                    strPoDate = dr["PoDate"].ToString();
                    strPoID = dr["PoID"].ToString();
                    strVendorID = dr["VendorID"].ToString();
                    strVersion = dr["Version"].ToString();

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

        #endregion
    }
}