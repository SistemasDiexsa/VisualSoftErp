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
    public class ControldeimplementacionCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intImplementacionID { get; set; }
        public string strProgramasID { get; set; }
        public DateTime fFecha { get; set; }
        public int intUsuariosID { get; set; }
        public string strReporte { get; set; }
        public int intStatus { get; set; }
        public string strSolucion { get; set; }
        public DateTime fFechaProceso { get; set; }
        public DateTime fFechaTerminado { get; set; }
        public string strAgente { get; set; }
        public string strVersion { get; set; }
        public int intFueradealcance { get; set; }
        #endregion

        #region Constructor
        public ControldeimplementacionCL()
        {
            intImplementacionID = 0;
            strProgramasID = string.Empty;
            fFecha = DateTime.Now;
            intUsuariosID = 0;
            strReporte = string.Empty;
            intStatus = 0;
            strSolucion = string.Empty;
            fFechaProceso = DateTime.Now;
            fFechaTerminado = DateTime.Now;
            strAgente = string.Empty;
            strVersion = string.Empty;
            intFueradealcance = 0;
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
        public DataTable ControldeimplementacionGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "controldeimplementacionGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //controldeimplementacionGrid

        public DataTable ControldeimplementacionGridDetalle()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ControldeimplementacionGridDetalle";
                cmd.Parameters.AddWithValue("@prmProgramasID", strProgramasID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //controldeimplementacionGrid

        public string ControldeimplementacionCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ControldeimplementacionCRUD";
                cmd.Parameters.AddWithValue("@prmImplementacionID", intImplementacionID);
                cmd.Parameters.AddWithValue("@prmProgramasID", strProgramasID);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.Parameters.AddWithValue("@prmUsuariosID", intUsuariosID);
                cmd.Parameters.AddWithValue("@prmReporte", strReporte);
                cmd.Parameters.AddWithValue("@prmStatus", intStatus);
                cmd.Parameters.AddWithValue("@prmSolucion", strSolucion);
                cmd.Parameters.AddWithValue("@prmFechaProceso", fFechaProceso);
                cmd.Parameters.AddWithValue("@prmFechaTerminado", fFechaTerminado);
                cmd.Parameters.AddWithValue("@prmAgente", strAgente);
                cmd.Parameters.AddWithValue("@prmVersion", strVersion);
                cmd.Parameters.AddWithValue("@prmFueradealcance", intFueradealcance);
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
        } //ControldeimplementacionCrud

        public string ControldeimplementacionLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ControldeimplementacionLlenaCajas";
                cmd.Parameters.AddWithValue("@prmImplementacionID", intImplementacionID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intImplementacionID = Convert.ToInt32(dr["ImplementacionID"]);
                    strProgramasID = dr["ProgramasID"].ToString();
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    strReporte = dr["Reporte"].ToString();
                    intFueradealcance = Convert.ToInt32(dr["Fueradealcance"]);
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

        public string ControldeimplementacionEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ControldeimplementacionEliminar";
                cmd.Parameters.AddWithValue("@prmImplementacionID", intImplementacionID);
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