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
    public class cyb_ChequerasCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intChequerasID { get; set; }
        public int intBancosID { get; set; }
        public string strCuentabancaria { get; set; }
        public string strSucursal { get; set; }
        public string strTelefono { get; set; }
        public string strTitular { get; set; }
        public string strMonedasID { get; set; }
        public int intChequeinicial { get; set; }
        public int intChequefinal { get; set; }
        public int intCuentasID { get; set; }
        public int intCuentacontablecomplementaria { get; set; }
        public int intStatus { get; set; }
        public string strLogotipo { get; set; }
        public decimal decComisiontarjeta { get; set; }
        public int intMasutilizada { get; set; }
        #endregion

        #region Constructor
        public cyb_ChequerasCL()
        {
            intChequerasID = 0;
            intBancosID = 0;
            strCuentabancaria = string.Empty;
            strSucursal = string.Empty;
            strTelefono = string.Empty;
            strTitular = string.Empty;
            strMonedasID = string.Empty;
            intChequeinicial = 0;
            intChequefinal = 0;
            intCuentasID = 0;
            intCuentacontablecomplementaria = 0;
            intStatus = 0;
            strLogotipo = string.Empty;
            decComisiontarjeta = 0;
            intMasutilizada = 0;
        }
        #endregion

        #region Metodos
        private string loadConnectionString()
        {
            try
            {
                XmlDocument oxml = new XmlDocument();
                oxml.Load(@"C:\VisualSoftCyB\xml\conexion.xml");
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
        public DataTable cyb_ChequerasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "cyb_chequerasGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //cyb_chequerasGrid

        public string cyb_ChequerasCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "cyb_ChequerasCRUD";
                cmd.Parameters.AddWithValue("@prmChequerasID", intChequerasID);
                cmd.Parameters.AddWithValue("@prmBancosID", intBancosID);
                cmd.Parameters.AddWithValue("@prmCuentabancaria", strCuentabancaria);
                cmd.Parameters.AddWithValue("@prmSucursal", strSucursal);
                cmd.Parameters.AddWithValue("@prmTelefono", strTelefono);
                cmd.Parameters.AddWithValue("@prmTitular", strTitular);
                cmd.Parameters.AddWithValue("@prmMonedasID", strMonedasID);
                cmd.Parameters.AddWithValue("@prmChequeinicial", intChequeinicial);
                cmd.Parameters.AddWithValue("@prmChequefinal", intChequefinal);
                cmd.Parameters.AddWithValue("@prmCuentasID", intCuentasID);
                cmd.Parameters.AddWithValue("@prmCuentacontablecomplementaria", intCuentacontablecomplementaria);
                cmd.Parameters.AddWithValue("@prmStatus", intStatus);
                cmd.Parameters.AddWithValue("@prmLogotipo", strLogotipo);
                cmd.Parameters.AddWithValue("@prmComisiontarjeta", decComisiontarjeta);
                cmd.Parameters.AddWithValue("@prmMasutilizada", intMasutilizada);
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
        } //cyb_ChequerasCrud

        public string cyb_ChequerasLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "cyb_ChequerasllenaCajas";
                cmd.Parameters.AddWithValue("@prmChequerasID", intChequerasID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intChequerasID = Convert.ToInt32(dr["ChequerasID"]);
                    intBancosID = Convert.ToInt32(dr["BancosID"]);
                    strCuentabancaria = dr["Cuentabancaria"].ToString();
                    strSucursal = dr["Sucursal"].ToString();
                    strTelefono = dr["Telefono"].ToString();
                    strTitular = dr["Titular"].ToString();
                    strMonedasID = dr["MonedasID"].ToString();
                    intChequeinicial = Convert.ToInt32(dr["Chequeinicial"]);
                    intChequefinal = Convert.ToInt32(dr["Chequefinal"]);
                    intCuentasID = Convert.ToInt32(dr["CuentasID"]);
                    intCuentacontablecomplementaria = Convert.ToInt32(dr["Cuentacontablecomplementaria"]);
                    intStatus = Convert.ToInt32(dr["Status"]);
                    strLogotipo = dr["Logotipo"].ToString();
                    decComisiontarjeta = Convert.ToDecimal(dr["Comisiontarjeta"]);
                    intMasutilizada = Convert.ToInt32(dr["Masutilizada"]);
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

        public string cyb_ChequerasEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "cyb_ChequerasEliminar";
                cmd.Parameters.AddWithValue("@prmChequerasID", intChequerasID);
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


        public string cyb_ChequerasCancelar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "cyb_ChequerasCancelar";
                cmd.Parameters.AddWithValue("@prmChequerasID", intChequerasID);
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
        } 

        #endregion
    }
}