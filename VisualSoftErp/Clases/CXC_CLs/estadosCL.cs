using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VisualSoftErp.Clases
{
    public class estadosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intEstadosID { get; set; }
        public int intZonasID { get; set; }
        public string strNombre { get; set; }
        public int intClaveSepomex { get; set; }
        #endregion

        #region Constructor
        public estadosCL()
        {
            int intEstadosid = 0;
            int intZonasid = 0;
            string strNombre = string.Empty;
            int intClavesepomex = 0;
        }
        #endregion

        #region Metodos
        public DataTable estadosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "estadosGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //estadosGrid

        public string EstadosCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EstadosCRUD";
                cmd.Parameters.AddWithValue("@prmEstadosID", intEstadosID);
                cmd.Parameters.AddWithValue("@prmZonasID", intZonasID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmClaveSepomex", intClaveSepomex);
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
        } //EstadosCrud

        public string estadosLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "estadosllenaCajas";
                cmd.Parameters.AddWithValue("@prmEstadosID", intEstadosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intEstadosID = Convert.ToInt32(dr["EstadosID"]);
                    intZonasID = Convert.ToInt32(dr["ZonasID"]);
                    strNombre = dr["Nombre"].ToString();
                    intClaveSepomex = Convert.ToInt32(dr["ClaveSepomex"]);
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

        public string estadosEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "estadosEliminar";
                cmd.Parameters.AddWithValue("@prmEstadosID", intEstadosID);
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