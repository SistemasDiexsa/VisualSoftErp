using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VisualSoftErp.Clases
{
    public class tiposdemovimientocxcCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intTiposdemovimientocxcID { get; set; }
        public string strNombre { get; set; }
        public string strTipo { get; set; }
        public int intReservado { get; set; }
        #endregion

        #region Constructor
        public tiposdemovimientocxcCL()
        {
            int intTiposdemovimientocxcid = 0;
            string strNombre = string.Empty;
            string strTipo = string.Empty;
            int intReservado = 0;
        }
        #endregion

        #region Metodos
        public DataTable tiposdemovimientocxcGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Tipodemovimientocxcgrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //tiposdemovimientocxcGrid

        public string TiposdemovimientocxcCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TiposdemovimientocxcCRUD";
                cmd.Parameters.AddWithValue("@prmTiposdemovimientocxcID", intTiposdemovimientocxcID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmTipo", strTipo);
                cmd.Parameters.AddWithValue("@prmReservado", 0);
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
        } //TiposdemovimientocxcCrud

        public string tiposdemovimientocxcLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "tiposdemovimientocxcllenaCajas";
                cmd.Parameters.AddWithValue("@prmTiposdemovimientocxcID", intTiposdemovimientocxcID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intTiposdemovimientocxcID = Convert.ToInt32(dr["TiposdemovimientocxcID"]);
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
                return ex.Message;
            }
        } // public class LlenaCajas

        public string tiposdemovimientocxcEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "tiposdemovimientocxcEliminar";
                cmd.Parameters.AddWithValue("@prmTiposdemovimientocxcID", intTiposdemovimientocxcID);
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