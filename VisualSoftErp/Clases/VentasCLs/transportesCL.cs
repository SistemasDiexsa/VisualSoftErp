using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VisualSoftErp.Clases
{
    public class transportesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intTransportesID { get; set; }
        public string strNombre { get; set; }
        public string strCobertura { get; set; }
        public string strPorcobrar { get; set; }
        public string strTipo { get; set; }
        public string strHorario { get; set; }
        #endregion

        #region Constructor
        public transportesCL()
        {
            int intTransportesid = 0;
            string strNombre = string.Empty;
            string strCobertura = string.Empty;
            string strPorcobrar = string.Empty;
            string strTipo = string.Empty;
            string strHorario = string.Empty;
        }
        #endregion

        #region Metodos
        public DataTable transportesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "transportesGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //transportesGrid

        public string TransportesCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TransportesCRUD";
                cmd.Parameters.AddWithValue("@prmTransportesID", intTransportesID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmCobertura", strCobertura);
                cmd.Parameters.AddWithValue("@prmPorcobrar", strPorcobrar);
                cmd.Parameters.AddWithValue("@prmTipo", strTipo);
                cmd.Parameters.AddWithValue("@prmHorario", strHorario);
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
                return "TransportesCrud: " + ex.Message;
            }
        } //TransportesCrud

        public string transportesLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "transportesllenaCajas";
                cmd.Parameters.AddWithValue("@prmTransportesID", intTransportesID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intTransportesID = Convert.ToInt32(dr["TransportesID"]);
                    strNombre = dr["Nombre"].ToString();
                    strCobertura = dr["Cobertura"].ToString();
                    strPorcobrar = dr["Porcobrar"].ToString();
                    strTipo = dr["Tipo"].ToString();
                    strHorario = dr["Horario"].ToString();
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
                return "transportesLlenaCajas: " + ex.Message;
            }
        } // public class LlenaCajas

        public string transportesEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "transportesEliminar";
                cmd.Parameters.AddWithValue("@prmTransportesID", intTransportesID);
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
                return "transportesEliminar: "+ex.Message;
            }
        } // Public Class Eliminar

        #endregion
    }
}