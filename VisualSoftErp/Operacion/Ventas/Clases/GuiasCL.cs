using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.XtraSpreadsheet.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Ventas.Clases
{
    public class GuiasCL
    {
        #region PROPIEDADES
        string strCnn = globalCL.gv_strcnn;
        public int intMes { get; set; }
        public int intAño { get; set; }
        public int intFolio { get; set; }
        public string strSerieFactura { get; set; }
        public int intFolioFactura {  get; set; }
        public DataTable dtGuias { get; set; }
        public DataTable dtGuiasDetalle { get; set; }
        public string strDoc { get; set; }
        public int intNumGuia { get; set; }
        public DateTime dateFecha { get; set; }
        public int intTransportesID { get; set; }
        public decimal decSubtotal { get; set; }
        public decimal decTotal { get; set; }
        public decimal decIva { get; set; }
        public decimal decRetIva { get; set; }
        #endregion

        #region CONSTRUCTOR
        public GuiasCL()
        {
            intMes = 0;
            intAño = 0;
            intFolio = 0;
            strSerieFactura = string.Empty;
            intFolioFactura = 0;
            strDoc = string.Empty;
            intNumGuia = 0;
            intTransportesID = 0;
            decSubtotal = 0;
            decTotal = 0;
            decIva = 0;
            decRetIva = 0;
        }
        #endregion

        #region METODOS
        public DataTable GuiasGrid ()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GuiasGrid";
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        }

        public string GuiasCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GuiasCRUD";
                cmd.Parameters.AddWithValue("@prmFolioGuia", intFolio);
                cmd.Parameters.AddWithValue("@prmGuias", dtGuias);
                cmd.Parameters.AddWithValue("@prmGuiasDetalle", dtGuiasDetalle);
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
        }

        public string ExistenciaFactura()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ExistenciaFactura";
                cmd.Parameters.AddWithValue("@prmFolioFactura", intFolioFactura);
                cmd.Parameters.AddWithValue("@prmSerieFactura", strSerieFactura);
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
        }

        public string DocumentosSiguienteID()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DocumentosSiguienteID";
                cmd.Parameters.AddWithValue("@prmSerie", strSerieFactura);
                cmd.Parameters.AddWithValue("@prmDoc", strDoc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intFolio = Convert.ToInt32(dr["SigID"]);
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
        }

        public string LlenaCajas()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GuiasLlenaCajas";
                cmd.Parameters.AddWithValue("@prmFolioGuias", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intNumGuia = Convert.ToInt32(dr["NumerodeGuia"]);
                    dateFecha = Convert.ToDateTime(dr["Fecha"]);
                    intTransportesID = Convert.ToInt32(dr["TransportesID"]);
                    decSubtotal = Convert.ToDecimal(dr["Subtotal"]);
                    decIva = Convert.ToDecimal(dr["Iva"]);
                    decRetIva = Convert.ToDecimal(dr["RetIva"]);
                    decTotal = Convert.ToDecimal(dr["Total"]);
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
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public DataTable LlenaCajasDetalle()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GuiasLlenaCajasDetalle";
                cmd.Parameters.AddWithValue("@prmFolioGuias", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public string Eliminar()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GuiasEliminar";
                cmd.Parameters.AddWithValue("@prmFolioGuias", intFolio);
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
        }

        #endregion
    }
}
