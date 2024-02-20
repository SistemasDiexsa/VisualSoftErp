using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VisualSoftErp.Clases
{
    public class tiposdecambioCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strMoneda { get; set; }
        public DateTime fFecha { get; set; }
        public decimal dParidad { get; set; }
        #endregion

        #region Constructor
        public tiposdecambioCL()
        {
            string strMoneda = string.Empty;
            DateTime fFecha = DateTime.Now;
            decimal dParidad = 0;
        }
        #endregion

        #region Metodos
        public DataTable tiposdecambioGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "tiposdecambioGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //tiposdecambioGrid

        public string TiposdecambioCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TiposdecambioCRUD";
                cmd.Parameters.AddWithValue("@prmMoneda", strMoneda);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.Parameters.AddWithValue("@prmParidad", dParidad);
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
                return "TiposdecambioCrud: " + ex.Message;
            }
        } //TiposdecambioCrud

        public string tiposdecambioLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "tiposdecambiollenaCajas";
                cmd.Parameters.AddWithValue("@prmMoneda", strMoneda);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strMoneda = dr["MonedasID"].ToString();
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    dParidad = Convert.ToDecimal(dr["Paridad"]);
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
                return "tiposdecambioLlenaCajas: " + ex.Message;
            }
        } // public class LlenaCajas

        public string tiposdecambioEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "tiposdecambioEliminar";
                cmd.Parameters.AddWithValue("@prmMoneda", strMoneda);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.Parameters.AddWithValue("@prmParidad", dParidad);
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
                return "tiposdecambioEliminar: " + ex.Message;
            }
        } // Public Class Eliminar

        #endregion
    }
}