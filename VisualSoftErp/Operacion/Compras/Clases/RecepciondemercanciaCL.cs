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
    public class RecepciondemercanciaCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public string strComprasserie { get; set; }
        public int intComprasFolio { get; set; }
        public int intContrarecibosFolio { get; set; }
        public int intContrarecibosSeq { get; set; }
        public string strOrdendecompraSerie { get; set; }
        public int intOrdedecomprafolio { get; set; }
        public int intStatus { get; set; }
        public DateTime fFecha { get; set; }
        public DateTime fFechaCancelacion { get; set; }
        public string strMotivocancelacion { get; set; }
        public string strObservaciones { get; set; }
        public int intValidado { get; set; }
        public DateTime fFechavalidacion { get; set; }
        public int IdProveedor { get; set; }       
        public DataTable dtRMDet { get; set; }
        public DataTable dtRM { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        public string strDoc { get; set; }
        public int intAño { get; set; }
        public int intMes { get; set; }
        #endregion

        #region Constructor
        public RecepciondemercanciaCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            strComprasserie = string.Empty;
            intComprasFolio = 0;
            intContrarecibosFolio = 0;
            intContrarecibosSeq = 0;
            strOrdendecompraSerie = string.Empty;
            intOrdedecomprafolio = 0;
            intStatus = 0;
            fFecha = DateTime.Now;
            fFechaCancelacion = DateTime.Now;
            strMotivocancelacion = string.Empty;
            strObservaciones = string.Empty;
            intValidado = 0;
            fFechavalidacion = DateTime.Now;
            intAño = 0;
            intMes = 0;
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
        public DataTable RecepciondemercanciaGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "recepciondemercanciaGrid";
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //recepciondemercanciaGrid

        public DataTable RecepciondemercanciaOCGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RecepciondemercanciaDetalleGrid";
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
        } //recepciondemercanciaGrid

        public string RecepciondemercanciasCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RecepciondemercanciaCRUD";
                cmd.Parameters.AddWithValue("@prmRecepciondemercancia", dtRM);
                cmd.Parameters.AddWithValue("@prmRecepciondemercanciaDetalle", dtRMDet);
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

        public string RecepciondemercanciasCambiaFecha()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RecepciondemercanciaCambiaFecha";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
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

        public string RecepciondemercanciaLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RecepciondemercanciallenaCajas";
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
                    strComprasserie = dr["Comprasserie"].ToString();
                    intComprasFolio = Convert.ToInt32(dr["ComprasFolio"]);
                    intContrarecibosFolio = Convert.ToInt32(dr["ContrarecibosFolio"]);
                    intContrarecibosSeq = Convert.ToInt32(dr["ContrarecibosSeq"]);
                    strOrdendecompraSerie = dr["OrdendecompraSerie"].ToString();
                    intOrdedecomprafolio = Convert.ToInt32(dr["Ordedecomprafolio"]);
                    intStatus = Convert.ToInt32(dr["Status"]);
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    fFechaCancelacion = Convert.ToDateTime(dr["FechaCancelacion"]);
                    strMotivocancelacion = dr["Motivocancelacion"].ToString();
                    strObservaciones = dr["Observaciones"].ToString();
                    intValidado = Convert.ToInt32(dr["Validado"]);
                    fFechavalidacion = Convert.ToDateTime(dr["Fechavalidacion"]);
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

        public DataTable OrdenComprasPorProveedor()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Recepciondemercanciacargaordenesdecompraporrecibir";
                cmd.Parameters.AddWithValue("@prmProv", IdProveedor);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //remisionesGrid

        public DataTable OrdenComprasDetalle()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RecepciondemercanciaRecibe";
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

        

        public string RecepciondemercanciaCancelar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RecepciondemercanciaCancelar";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", Environment.MachineName);
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