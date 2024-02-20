using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VisualSoftErp.Clases
{
    public class motivosnoaprobacioncotizacionesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intMotivosnoaprobacioncotizacionesID { get; set; }
        public string strNombre { get; set; }
        #endregion

        #region Constructor
        public motivosnoaprobacioncotizacionesCL()
        {
            int intMotivosnoaprobacioncotizacionesid = 0;
            string strNombre = string.Empty;
        }
        #endregion

        #region Metodos
        public DataTable motivosnoaprobacioncotizacionesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "motivosnoaprobacioncotizacionesGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //motivosnoaprobacioncotizacionesGrid

        public string MotivosnoaprobacioncotizacionesCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "MotivosnoaprobacioncotizacionesCRUD";
                cmd.Parameters.AddWithValue("@prmMotivosnoaprobacioncotizacionesID", intMotivosnoaprobacioncotizacionesID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
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
                return "MotivosnoaprobacioncotizacionesCrud: " + ex.Message;
            }
        } //MotivosnoaprobacioncotizacionesCrud

        public string motivosnoaprobacioncotizacionesLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "motivosnoaprobacioncotizacionesllenaCajas";
                cmd.Parameters.AddWithValue("@prmMotivosnoaprobacioncotizacionesID", intMotivosnoaprobacioncotizacionesID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();                    
                    strNombre = dr["Nombre"].ToString();
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
                return "motivosnoaprobacioncotizacionesLlenaCajas: " + ex.Message;
            }
        } // public class LlenaCajas

        public string motivosnoaprobacioncotizacionesEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "motivosnoaprobacioncotizacionesEliminar";
                cmd.Parameters.AddWithValue("@prmMotivosnoaprobacioncotizacionesID", intMotivosnoaprobacioncotizacionesID);
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
                return "motivosnoaprobacioncotizacionesEliminar: " + ex.Message;
            }
        } // Public Class Eliminar

        #endregion
    }
}