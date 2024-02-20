using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VisualSoftErp.Clases
{
    public class almacenesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intAlmacenesID { get; set; }
        public string strNombre { get; set; }
        public int intActivo { get; set; }
        #endregion

        #region Constructor
        public almacenesCL()
        {
            int intAlmacenesid = 0;
            string strNombre = string.Empty;
            int intActivo = 0;
        }
        #endregion

        #region Metodos
        public DataTable almacenesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "almacenesGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //almacenesGrid

        public string AlmacenesCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AlmacenesCRUD";
                cmd.Parameters.AddWithValue("@prmAlmacenesID", intAlmacenesID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmActivo", intActivo);
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
                return "AlmacenesCrud: "+ex.Message;
            }
        } //AlmacenesCrud

        public string almacenesLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AlmacenesllenaCajas";
                cmd.Parameters.AddWithValue("@prmAlmacenesID", intAlmacenesID);
                
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strNombre = dr["Nombre"].ToString();
                    intAlmacenesID = Convert.ToInt32(dr["AlmacenesID"]);
                    
                    intActivo = Convert.ToInt32(dr["Activo"]);
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
                return "almacenesLlenaCajas: "+ex.Message;
            }
        } // public class LlenaCajas

        public string almacenesEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "almacenesEliminar";
                cmd.Parameters.AddWithValue("@prmAlmacenesID", intAlmacenesID);
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
                return "almacenesEliminar: "+ex.Message;
            }
        } // Public Class Eliminar

        #endregion
    }
}