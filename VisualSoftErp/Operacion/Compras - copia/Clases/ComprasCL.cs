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
    public class ComprasCL
    {
        #region Propiedades
        string strCnn = ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public DateTime fFecha { get; set; }
        public string strOCSerie { get; set; }
        public int intOCFolio { get; set; }
        public int intProveedoresID { get; set; }
        public string strMonedasID { get; set; }
        public string strFactura { get; set; }
        public DateTime ffechafactura { get; set; }
        public decimal dectipodecambio { get; set; }
        public decimal decSubtotal { get; set; }
        public decimal decIva { get; set; }
        public decimal decIeps { get; set; }
        public decimal decNeto { get; set; }
        public int intStatus { get; set; }
        public int intPlazo { get; set; }
        public DateTime fFechacancelacion { get; set; }
        public string strRazoncancelacion { get; set; }
        public decimal strDescuento { get; set; }
        public int intPoliza { get; set; }
        public int intNodeducible { get; set; }
        public string strContrarecibosSerie { get; set; }
        public int intContrarecibosFolio { get; set; }
        public string strRecepcionSerie { get; set; }
        public int intRecepcionFolio { get; set; }
        public int intValidadoPor { get; set; }
        public DateTime fFechaValidado { get; set; }
        public DateTime fFechaReal { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        public string strDoc { get; set; }
        #endregion

        #region Constructor
        public ComprasCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            fFecha = DateTime.Now;
            strOCSerie = string.Empty;
            intOCFolio = 0;
            intProveedoresID = 0;
            strMonedasID = string.Empty;
            strFactura = string.Empty;
            ffechafactura = DateTime.Now;
            dectipodecambio = 0;
            decSubtotal = 0;
            decIva = 0;
            decIeps = 0;
            decNeto = 0;
            intStatus = 0;
            intPlazo = 0;
            fFechacancelacion = DateTime.Now;
            strRazoncancelacion = string.Empty;
            strDescuento = 0;
            intPoliza = 0;
            intNodeducible = 0;
            strContrarecibosSerie = string.Empty;
            intContrarecibosFolio = 0;
            strRecepcionSerie = string.Empty;
            intRecepcionFolio = 0;
            intValidadoPor = 0;
            fFechaValidado = DateTime.Now;
            fFechaReal = DateTime.Now;
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

        public string DocumentosSiguienteID()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DocumentosSiguienteID";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmDoc", strDoc);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intFolio = Convert.ToInt32(dr["SigID"]);
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
        }

        public DataTable ComprasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "comprasGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //comprasGrid

        public string ComprasCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasCRUD";
                cmd.Parameters.AddWithValue("@prmCompras", dtm);
                cmd.Parameters.AddWithValue("@prmComprasdetalle", dtd);
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
        } //ComprasCrud

        public string ComprasLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasllenaCajas";
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
                    strOCSerie = dr["OCSerie"].ToString();
                    intOCFolio = Convert.ToInt32(dr["OCFolio"]);
                    intProveedoresID = Convert.ToInt32(dr["ProveedoresID"]);
                    strMonedasID = dr["MonedasID"].ToString();
                    strFactura = dr["Factura"].ToString();
                    ffechafactura = Convert.ToDateTime(dr["fechafactura"]);
                    dectipodecambio = Convert.ToDecimal(dr["tipodecambio"]);
                    decSubtotal = Convert.ToDecimal(dr["Subtotal"]);
                    decIva = Convert.ToDecimal(dr["Iva"]);
                    decIeps = Convert.ToDecimal(dr["Ieps"]);
                    decNeto = Convert.ToDecimal(dr["Neto"]);
                    intStatus = Convert.ToInt32(dr["Status"]);
                    intPlazo = Convert.ToInt32(dr["Plazo"]);
                    fFechacancelacion = Convert.ToDateTime(dr["Fechacancelacion"]);
                    strRazoncancelacion = dr["Razoncancelacion"].ToString();
                    strDescuento = Convert.ToDecimal(dr["Descuento"]);
                    intPoliza = Convert.ToInt32(dr["Poliza"]);
                    intNodeducible = Convert.ToInt32(dr["Nodeducible"]);
                    strContrarecibosSerie = dr["ContrarecibosSerie"].ToString();
                    intContrarecibosFolio = Convert.ToInt32(dr["ContrarecibosFolio"]);
                    strRecepcionSerie = dr["RecepcionSerie"].ToString();
                    intRecepcionFolio = Convert.ToInt32(dr["RecepcionFolio"]);
                    intValidadoPor = Convert.ToInt32(dr["ValidadoPor"]);
                    fFechaValidado = Convert.ToDateTime(dr["FechaValidado"]);
                    fFechaReal = Convert.ToDateTime(dr["FechaReal"]);
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

        public string ObtenerPlazoProveedor()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ProveedoresLlenaCajas";
                cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intPlazo = Convert.ToInt32(dr["Plazo"]);

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

        public DataTable ComprasDetalleGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasdetalleLlenaCajas";
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

        public string ComprasEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComprasCancelar";
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