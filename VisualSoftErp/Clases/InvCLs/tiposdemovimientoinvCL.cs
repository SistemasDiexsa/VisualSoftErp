using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VisualSoftErp.Clases
{
    public class tiposdemovimientoinvCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intTiposdemovimientoinvID { get; set; }
        public string strNombre { get; set; }
        public string strTipo { get; set; }
        public int intReservado { get; set; }
        #endregion

        #region Constructor
        public tiposdemovimientoinvCL()
        {
            int intTiposdemovimientoinvid = 0;
            string strNombre = string.Empty;
            string strTipo = string.Empty;
            int intReservado = 0;
        }
        #endregion

        #region Metodos
        public DataTable tiposdemovimientoinvGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "tiposdemovimientoinvGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //tiposdemovimientoinvGrid

        public string TiposdemovimientoinvCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TiposdemovimientoinvCRUD";
                cmd.Parameters.AddWithValue("@prmTiposdemovimientoinvID", intTiposdemovimientoinvID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmTipo", strTipo);
                cmd.Parameters.AddWithValue("@prmReservado", intReservado);
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
                return "TiposdemovimientoinvCrud: " + ex.Message;
            }
        } //TiposdemovimientoinvCrud

        public string tiposdemovimientoinvLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "tiposdemovimientoinvllenaCajas";
                cmd.Parameters.AddWithValue("@prmTiposdemovimientoinvID", intTiposdemovimientoinvID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intTiposdemovimientoinvID = Convert.ToInt32(dr["TiposdemovimientoinvID"]);
                    strNombre = dr["Nombre"].ToString();
                    strTipo = dr["Tipo"].ToString();
                    intReservado = Convert.ToInt32(dr["Reservado"]);
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
                return "tiposdemovimientoinvLlenaCajas: " + ex.Message;
            }
        } // public class LlenaCajas

        public string tiposdemovimientoinvEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "tiposdemovimientoinvEliminar";
                cmd.Parameters.AddWithValue("@prmTiposdemovimientoinvID", intTiposdemovimientoinvID);
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
                return "tiposdemovimientoinvEliminar: " + ex.Message;
            }
        } // Public Class Eliminar

        #endregion
    }
}