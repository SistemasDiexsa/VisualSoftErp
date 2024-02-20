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
    public class AnticiposCxCCL
    {
        #region Propiedades
        string strCnn = ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public int intAnticiposCxPID { get; set; }
        public int intClientesID { get; set; }
        public DateTime fFecha { get; set; }
        public int intChequerasID { get; set; }
        public decimal dImportechequera { get; set; }
        public string strReferencia { get; set; }
        public string strMonedaCehquera { get; set; }
        public decimal dTipodecambio { get; set; }
        public decimal dImporteClientes { get; set; }
        public string strDescripcion { get; set; }
        public string strStatus { get; set; }
        public string strcFormaPago { get; set; }
        public int intCxPAnticiposID { get; set; }
        #endregion

        #region Constructor
        public AnticiposCxCCL()
        {
            intAnticiposCxPID = 0;
            intClientesID = 0;
            fFecha = DateTime.Now;
            intChequerasID = 0;
            dImportechequera = 0;
            strReferencia = string.Empty;
            strMonedaCehquera = string.Empty;
            dTipodecambio = 0;
            dImporteClientes = 0;
            strDescripcion = string.Empty;
            strStatus = string.Empty;
            strcFormaPago = string.Empty;
            intCxPAnticiposID = 0;
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
        public DataTable AnticiposCxCGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "anticiposcxcGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //anticiposcxpGrid

        public DataTable AnticiposCxCPendientes()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AnticiposCxCPendientes";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;
            }
            catch (Exception ex) { return dt; }
        }

        public string AnticiposCxPCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AnticiposCxCCRUD";
                cmd.Parameters.AddWithValue("@prmAnticiposCxCID", intAnticiposCxPID);
                cmd.Parameters.AddWithValue("@prmClientesID", intClientesID);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.Parameters.AddWithValue("@prmChequerasID", intChequerasID);
                cmd.Parameters.AddWithValue("@prmImportechequera", dImportechequera);
                cmd.Parameters.AddWithValue("@prmReferencia", strReferencia);
                cmd.Parameters.AddWithValue("@prmMonedaCehquera", strMonedaCehquera);
                cmd.Parameters.AddWithValue("@prmTipodecambio", dTipodecambio);
                cmd.Parameters.AddWithValue("@prmImporteCliente", dImporteClientes);
                cmd.Parameters.AddWithValue("@prmDescripcion", strDescripcion);
                cmd.Parameters.AddWithValue("@prmStatus", strStatus);
                cmd.Parameters.AddWithValue("@prmcFormaPago", strcFormaPago);
                //cmd.Parameters.AddWithValue("@prmCxPAnticiposID", intCxPAnticiposID);
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
        } //AnticiposCxPCrud

        //public string AnticiposCxPLlenaCajas()
        //{
        //    try
        //    {
        //        string result = string.Empty;

        //        SqlConnection cnn = new SqlConnection();
        //        cnn.ConnectionString = strCnn;
        //        cnn.Open();

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = "AnticiposCxPllenaCajas";
        //        cmd.Parameters.AddWithValue("@prmAnticiposCxPID", intAnticiposCxPID);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Connection = cnn;

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            dr.Read();
        //            intAnticiposCxPID = Convert.ToInt32(dr["AnticiposCxPID"]);
        //            intProveedoresID = Convert.ToInt32(dr["ProveedoresID"]);
        //            fFecha = Convert.ToDateTime(dr["Fecha"]);
        //            intChequerasID = Convert.ToInt32(dr["ChequerasID"]);
        //            intImportechequera = Convert.ToInt32(dr["ChequerasID"]);
        //            strReferencia = dr["Referencia"].ToString();
        //            strMonedaCehquera = dr["MonedaCehquera"].ToString();
        //            strTipodecambio = dr["MonedaCehquera"].ToString();
        //            strMonedaProveedor = dr["MonedaCehquera"].ToString();
        //            strDescripcion = dr["Descripcion"].ToString();
        //            strStatus = dr["Status"].ToString();
        //            strcFormaPago = dr["cFormaPago"].ToString();
        //            intCxPAnticiposID = Convert.ToInt32(dr["CxPAnticiposID"]);
        //            result = "OK";
        //        }
        //        else
        //        {
        //            result = "no read";
        //        }

        //        dr.Close();
        //        cnn.Close();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //} // public class LlenaCajas

        public string AnticiposCxPEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AnticiposCxPEliminar";
                cmd.Parameters.AddWithValue("@prmAnticiposCxPID", intAnticiposCxPID);
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