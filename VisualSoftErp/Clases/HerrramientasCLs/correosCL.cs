using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Clases.HerrramientasCLs
{
    public class correosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strDoc { get; set; }
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public string strEMail { get; set; }
        #endregion
        #region Constructor
        public correosCL()
        {
            strDoc = string.Empty;
            strSerie = string.Empty;
            intFolio = 0;
            strEMail = string.Empty;
        }
        #endregion
        #region Metodos
        public DataTable CorreosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CorreosenviadosGRID";
                cmd.Parameters.AddWithValue("@prmDoc", strDoc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception)
            { return dt; }
        } //Correos
        public DataTable CorreosDetalleGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CorreosenviadosdetalleGRID";
                cmd.Parameters.AddWithValue("@prmDoc", strDoc);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception)
            { return dt; }
        } //Correos detalle
        public string Obtenercorreodecliente()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CorreosenviadosLeeCorreodeCliente";
                cmd.Parameters.AddWithValue("@prmDoc", strDoc);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strEMail = dr["EMail"].ToString();
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
        } // Public Class Eliminar
        #endregion
    }
}
