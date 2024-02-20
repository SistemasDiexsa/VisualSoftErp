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
    public class AppOfertasCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intAppOfertasID { get; set; }
        public int intArticulosID { get; set; }
        public decimal dPrecio { get; set; }
        public string strMoendasID { get; set; }
        public string strDescripción { get; set; }
        public DateTime fVigenciaInicio { get; set; }
        public DateTime fVigenciaFin { get; set; }
        #endregion

        #region Constructor
        public AppOfertasCL()
        {
            intAppOfertasID = 0;
            intArticulosID = 0;
            dPrecio = 0;
            strMoendasID = string.Empty;
            strDescripción = string.Empty;
            fVigenciaInicio = DateTime.Now;
            fVigenciaFin = DateTime.Now;
        }
        #endregion

        #region Metodos
        
        public DataTable AppOfertasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "appofertasGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //appofertasGrid

        public string AppOfertasCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppOfertasCRUD";
                cmd.Parameters.AddWithValue("@prmAppOfertasID", intAppOfertasID);
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                cmd.Parameters.AddWithValue("@prmPrecio", dPrecio);
                cmd.Parameters.AddWithValue("@prmMoendasID", strMoendasID);
                cmd.Parameters.AddWithValue("@prmDescripción", strDescripción);
                cmd.Parameters.AddWithValue("@prmVigenciaInicio", fVigenciaInicio);
                cmd.Parameters.AddWithValue("@prmVigenciaFin", fVigenciaFin);
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
        } //AppOfertasCrud

        public string AppOfertasLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppOfertasllenaCajas";
                cmd.Parameters.AddWithValue("@prmAppOfertasID", intAppOfertasID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intAppOfertasID = Convert.ToInt32(dr["AppOfertasID"]);
                    intArticulosID = Convert.ToInt32(dr["ArticulosID"]);
                    dPrecio = Convert.ToInt32(dr["ArticulosID"]);
                    strMoendasID = dr["MoendasID"].ToString();
                    strDescripción = dr["Descripción"].ToString();
                    fVigenciaInicio = Convert.ToDateTime(dr["VigenciaInicio"]);
                    fVigenciaFin = Convert.ToDateTime(dr["VigenciaFin"]);
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

        public string AppOfertasEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppOfertasEliminar";
                cmd.Parameters.AddWithValue("@prmAppOfertasID", intAppOfertasID);
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