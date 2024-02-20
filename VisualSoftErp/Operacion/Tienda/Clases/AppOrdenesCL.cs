using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Tienda.Clases
{
    public class AppOrdenesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intOrdenID;
        public string strMensaje;
        public int intStatus;
        string strSerie;
        #endregion
        #region Constructor
        public AppOrdenesCL()
        {
            strMensaje = string.Empty;
            intOrdenID = 0;
            intStatus = 0;
            strSerie = string.Empty;
        }
        #endregion
        #region Métodos
        public string InsertaOrdenTest()
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("AppOrdenesID", Type.GetType("System.Int32"));
                dt.Columns.Add("AppFromasdepagoID", Type.GetType("System.Int32"));
                dt.Columns.Add("Importe", Type.GetType("System.Decimal"));
                dt.Columns.Add("Referencia", Type.GetType("System.String"));

                dt.Rows.Add(0, 1, 200, "ABC");

                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppOrdenesInsertar";
                cmd.Parameters.AddWithValue("@prmUsuariosID", 8);
                cmd.Parameters.AddWithValue("@prmFormasdepago", dt);
                cmd.Parameters.AddWithValue("@prmDireccionesID", 1);
                cmd.Parameters.AddWithValue("@prmTransportesID", 1);
                cmd.Parameters.AddWithValue("@prmFlete", 120);
                cmd.Parameters.AddWithValue("@prmSubtotal", 1300);
                cmd.Parameters.AddWithValue("@prmIva", 130);
                cmd.Parameters.AddWithValue("@prmTotal", 1500);
                cmd.Parameters.AddWithValue("@prmFechaEntrega", DateTime.Now);
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
        } //AppBannersCrud
        public string OrdenCambiaStatus()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppOrdenActualizaStatus";
                cmd.Parameters.AddWithValue("@prmOrdenID", intOrdenID);
                cmd.Parameters.AddWithValue("@prmStatus", intStatus);
                cmd.Parameters.AddWithValue("@prmMensaje", strMensaje);
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
        } //OrdenCambiaStatus
        public string OrdenesConfirmar()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppOrdenesConfirmar";
                cmd.Parameters.AddWithValue("@prmOrdenesID", intOrdenID);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmUsuario", globalCL.gv_UsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", Environment.MachineName);
                cmd.Parameters.AddWithValue("@prmMensaje", strMensaje);
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
        } //OrdenesConfirmar
        public DataTable Ordenesporatender()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppOrdenesPorAtenderLista";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //Ordenesporatender
        public DataSet Datosdelaorden()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppOrdenesResumenCheckOut";
                cmd.Parameters.AddWithValue("@prmOrdenesID", intOrdenID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(ds);
                cnn.Close();
                return ds;

            }
            catch (Exception ex) { return ds; }
        } //Datosdelaorden
        public DataTable DatosdelaordenFP()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppOrdenesResumenFPCheckOut";
                cmd.Parameters.AddWithValue("@prmOrdenesID", intOrdenID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;


            }
            catch (Exception ex) { return dt; }
        } //appbannersGrid
        #endregion
    }
}
