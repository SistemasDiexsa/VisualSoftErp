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
    public class OrdenesdecomprasCL
    {
        #region Propiedades
        string strCnn = ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public DateTime fFecha { get; set; }
        public int intProveedoresID { get; set; }
        public decimal intSubtotal { get; set; }
        public decimal intIva { get; set; }
        public decimal intIeps { get; set; }
        public decimal intNeto { get; set; }
        public int intStatus { get; set; }
        public DateTime fFechacancelacion { get; set; }
        public string strRazoncancelacion { get; set; }
        public int intUsuariosID { get; set; }
        public string strObservaciones { get; set; }
        public int intEmbarcara { get; set; }
        public int intFacturara { get; set; }
        public string strMailto { get; set; }
        public string strAtenciona { get; set; }
        public string strCondiciones { get; set; }
        public string strLab { get; set; }
        public int intVia { get; set; }
        public string strMonedasID { get; set; }
        public int intTiempodeentrega { get; set; }
        public int intAutorizadopor { get; set; }
        public string strMaquinaautorizado { get; set; }
        public int intRequisicion { get; set; }
        public int intDepurada { get; set; }
        public DateTime fFechadepurada { get; set; }
        public int intUsuariodepuro { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        public string CorrespondenciaA { get; set; }
        public int DiasTraslado { get; set; }
        #endregion

        #region Constructor
        public OrdenesdecomprasCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            fFecha = DateTime.Now;
            intProveedoresID = 0;
            intSubtotal = 0;
            intIva = 0;
            intIeps = 0;
            intNeto = 0;
            intStatus = 0;
            fFechacancelacion = DateTime.Now;
            strRazoncancelacion = string.Empty;
            intUsuariosID = 0;
            strObservaciones = string.Empty;
            intEmbarcara = 0;
            intFacturara = 0;
            strMailto = string.Empty;
            strAtenciona = string.Empty;
            strCondiciones = string.Empty;
            strLab = string.Empty;
            intVia = 0;
            strMonedasID = string.Empty;
            intTiempodeentrega = 0;
            intAutorizadopor = 0;
            strMaquinaautorizado = string.Empty;
            intRequisicion = 0;
            intDepurada = 0;
            fFechadepurada = DateTime.Now;
            intUsuariodepuro = 0;
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
        public DataTable OrdenesdecomprasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ordenesdecomprasGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //ordenesdecomprasGrid

        public DataTable OrdebesdecomprasDetalleGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "OrdenesdecomprasdetalleLlenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //remisionesGrid

        public string OrdenesdecomprasCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "OrdenesdecomprasCRUD";
                cmd.Parameters.AddWithValue("@prmOrdenesdecompras", dtm);
                cmd.Parameters.AddWithValue("@prmOrdenesdecomprasDetalle", dtd);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);
                cmd.Parameters.AddWithValue("@prmPrograma", strPrograma);
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
        } //OrdenesdecomprasCrud

        public string OrdenesdecomprasLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "OrdenesdecomprasllenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strSerie = dr["Serie"].ToString();
                    intFolio = Convert.ToInt32(dr["Folio"]);
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    intProveedoresID = Convert.ToInt32(dr["ProveedoresID"]);
                    intSubtotal = Convert.ToInt32(dr["Subtotal"]);
                    intIva = Convert.ToInt32(dr["Iva"]);
                    intIeps = Convert.ToInt32(dr["Ieps"]);
                    intNeto = Convert.ToInt32(dr["Neto"]);
                    intStatus = Convert.ToInt32(dr["Status"]);
                    fFechacancelacion = Convert.ToDateTime(dr["Fechacancelacion"]);
                    strRazoncancelacion = dr["Razoncancelacion"].ToString();
                    intUsuariosID = Convert.ToInt32(dr["UsuariosID"]);
                    strObservaciones = dr["Observaciones"].ToString();
                    intEmbarcara = Convert.ToInt32(dr["Embarcara"]);
                    intFacturara = Convert.ToInt32(dr["Facturara"]);
                    strMailto = dr["Mailto"].ToString();
                    strAtenciona = dr["Atenciona"].ToString();
                    strCondiciones = dr["Condiciones"].ToString();
                    strLab = dr["Lab"].ToString();
                    intVia = Convert.ToInt32(dr["Via"]);
                    strMonedasID = dr["MonedasID"].ToString();
                    intTiempodeentrega = Convert.ToInt32(dr["Tiempodeentrega"]);
                    DiasTraslado= Convert.ToInt32(dr["DiasTraslado"]);
                    CorrespondenciaA = dr["Correspondeciaa"].ToString();

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

        public string OrdenesdecomprasEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "OrdenesdecomprasEliminar";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
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