using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VisualSoftErp.Clases
{
    public class motivosdesalidasCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intMotivosdesalidasID { get; set; }
        public string strNombre { get; set; }
        public int intSeFactura { get; set; }
        #endregion

        #region Constructor
        public motivosdesalidasCL()
        {
            int intMotivosdesalidasid = 0;
            string strNombre = string.Empty;
            int intSefactura = 0;
        }
        #endregion

        #region Metodos
        public DataTable motivosdesalidasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "motivosdesalidasGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //motivosdesalidasGrid

        public string MotivosdesalidasCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "MotivosdesalidasCRUD";
                cmd.Parameters.AddWithValue("@prmMotivosdesalidasID", intMotivosdesalidasID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmSeFactura", intSeFactura);
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
                return "MotivosdesalidasCrud: " + ex.Message;
            }
        } //MotivosdesalidasCrud

        public string motivosdesalidasLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "motivosdesalidasllenaCajas";
                cmd.Parameters.AddWithValue("@prmMotivosdesalidasID", intMotivosdesalidasID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    
                    strNombre = dr["Nombre"].ToString();
                    intSeFactura = Convert.ToInt32(dr["SeFactura"]);
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
                return "motivosdesalidasLlenaCajas: " + ex.Message;
            }
        } // public class LlenaCajas

        public string motivosdesalidasEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "motivosdesalidasEliminar";
                cmd.Parameters.AddWithValue("@prmMotivosdesalidasID", intMotivosdesalidasID);
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
                return "motivosdesalidasEliminar: " + ex.Message;
            }
        } // Public Class Eliminar

        #endregion
    }
}