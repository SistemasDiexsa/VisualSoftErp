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
    public class PresupuestosporagenteCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intEjericio { get; set; }
        public int intAgentesID { get; set; }
        public decimal dEne { get; set; }
        public decimal dFeb { get; set; }
        public decimal dMar { get; set; }
        public decimal dAbr { get; set; }
        public decimal dMay { get; set; }
        public decimal dJun { get; set; }
        public decimal dJul { get; set; }
        public decimal dAgo { get; set; }
        public decimal dSep { get; set; }
        public decimal dOct { get; set; }
        public decimal dNov { get; set; }
        public decimal dDic { get; set; }
        #endregion

        #region Constructor
        public PresupuestosporagenteCL()
        {
            int intEjericio = 0;
            int intAgentesid = 0;
            decimal dEne = 0;
            decimal dFeb = 0;
            decimal dMar = 0;
            decimal dAbr = 0;
            decimal dMay = 0;
            decimal dJun = 0;
            decimal dJul = 0;
            decimal dAgo = 0;
            decimal dSep = 0;
            decimal dOct = 0;
            decimal dNov = 0;
            decimal dDic = 0;
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
        public DataTable PresupuestosporagenteGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "presupuestosporagenteGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //presupuestosporagenteGrid

        public string PresupuestosporagenteCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PresupuestosporagenteCRUD";
                cmd.Parameters.AddWithValue("@prmEjericio", intEjericio);
                cmd.Parameters.AddWithValue("@prmAgentesID", intAgentesID);
                cmd.Parameters.AddWithValue("@prmEne", dEne);
                cmd.Parameters.AddWithValue("@prmFeb", dFeb);
                cmd.Parameters.AddWithValue("@prmMar", dMar);
                cmd.Parameters.AddWithValue("@prmAbr", dAbr);
                cmd.Parameters.AddWithValue("@prmMay", dMay);
                cmd.Parameters.AddWithValue("@prmJun", dJun);
                cmd.Parameters.AddWithValue("@prmJul", dJul);
                cmd.Parameters.AddWithValue("@prmAgo", dAgo);
                cmd.Parameters.AddWithValue("@prmSep", dSep);
                cmd.Parameters.AddWithValue("@prmOct", dOct);
                cmd.Parameters.AddWithValue("@prmNov", dNov);
                cmd.Parameters.AddWithValue("@prmDic", dDic);
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
        } //PresupuestosporagenteCrud

        public string PresupuestosporagenteLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PresupuestosporagentellenaCajas";
                cmd.Parameters.AddWithValue("@prmEjericio", intEjericio);
                cmd.Parameters.AddWithValue("@prmAgentesID", intAgentesID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intEjericio = Convert.ToInt32(dr["Ejericio"]);
                    intAgentesID = Convert.ToInt32(dr["AgentesID"]);
                    dEne = Convert.ToDecimal(dr["Ene"]);
                    dFeb = Convert.ToDecimal(dr["Feb"]);
                    dMar = Convert.ToDecimal(dr["Mar"]);
                    dAbr = Convert.ToDecimal(dr["Abr"]);
                    dMay = Convert.ToDecimal(dr["May"]);
                    dJun = Convert.ToDecimal(dr["Jun"]);
                    dJul = Convert.ToDecimal(dr["Jul"]);
                    dAgo = Convert.ToDecimal(dr["Ago"]);
                    dSep = Convert.ToDecimal(dr["Sep"]);
                    dOct = Convert.ToDecimal(dr["Oct"]);
                    dNov = Convert.ToDecimal(dr["Nov"]);
                    dDic = Convert.ToDecimal(dr["Dic"]);
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

        public string PresupuestosporagenteEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PresupuestosporagenteEliminar";
                cmd.Parameters.AddWithValue("@prmEjericio", intEjericio);
                cmd.Parameters.AddWithValue("@prmAgentesID", intAgentesID);
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