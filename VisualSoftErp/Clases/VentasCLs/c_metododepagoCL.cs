using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VisualSoftErp.Clases
{
    public class c_metodopagoCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strClave { get; set; }
        public string strDes { get; set; }
        public int intdo;
        #endregion

        #region Constructor
        public c_metodopagoCL()
        {
            string strClave = string.Empty;
            string strDes = string.Empty;
            intdo = 0;
        }
        #endregion

        #region Metodos
        public DataTable c_metodopagoGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "c_metodopagoGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //c_metodopagoGrid

        public string c_MetodoPagoCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "c_MetodoPagoCRUD";
                cmd.Parameters.AddWithValue("@prmClave", strClave);
                cmd.Parameters.AddWithValue("@prmDes", strDes);
                cmd.Parameters.AddWithValue("@do", intdo);
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
                return "c_MetodoPagoCrud: " + ex.Message;
            }
        } //c_MetodoPagoCrud

        public string c_metodopagoLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "c_metodopagollenaCajas";
                cmd.Parameters.AddWithValue("@prmClave", strClave);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strClave = dr["Clave"].ToString();
                    strDes = dr["Des"].ToString();
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
                return "c_metodopagoLlenaCajas: "+ex.Message;
            }
        } // public class LlenaCajas

        public string c_metodopagoEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "c_metodopagoEliminar";
                cmd.Parameters.AddWithValue("@prmClave", strClave);
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