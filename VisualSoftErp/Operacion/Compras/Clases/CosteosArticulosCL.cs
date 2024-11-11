using DevExpress.XtraSpreadsheet.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Compras.Clases
{
    public class CosteosArticulosCL
    {
        #region PROPIEDADES
        private string strCnn = globalCL.gv_strcnn;
        public DataTable ListasCosteo { get; set; } = new DataTable
        {
            Columns =
            {
                new DataColumn("ListasCosteoID", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("CanalVentasID", typeof(int)),
                new DataColumn("Independiente", typeof(int)),
                new DataColumn("ListasCosteoIDIndependiente", typeof(int)),
                new DataColumn("Categoria", typeof(string))
            }
        };
        public DataTable ListasCosteoDetalle { get; set; } = new DataTable
        {
            Columns =
            {
                new DataColumn("ArticulosID", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("MonedasID", typeof(string)),
                new DataColumn("Costo", typeof(decimal)),
                new DataColumn("TipoCambio", typeof(decimal)),
                new DataColumn("Incremental", typeof(decimal)),
                new DataColumn("Merma", typeof(int)),
                new DataColumn("Margen", typeof(int)),
                new DataColumn("CostoTotal", typeof(decimal)),
                new DataColumn("PrecioActual", typeof(decimal))
            }
        };
        public int intLineasID { get; set; }
        public int intFamiliasID { get; set; }
        public int intSubFamiliasID { get; set; }
        public int intActivo { get; set; }
        public string strTabla { get; set; }
        public int intListasCosteoID { get; set; }
        public int intListasCosteoIDIndependiente { get; set; }
        public int intListasPrecioID { get; set; }
        #endregion PROPIEDADES


        #region CONSTRUCTOR
        public CosteosArticulosCL()
        {
            intLineasID = 0;
            intFamiliasID = 0;
            intSubFamiliasID = 0;
            intActivo = 0;
            strTabla = string.Empty;
            intListasCosteoID = 0;
            intListasCosteoIDIndependiente = 0;
            intListasPrecioID = 0;
        }
        #endregion CONSTRUCTOR


        #region METODOS
        public DataTable CosteoArticulosListasCosteo()
        {
            DataTable dt = new DataTable();
            try
            {
                using(SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("CosteoArticulosListasCosteo", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmCategoria", strTabla);
                        using(SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                dt.Columns.Add("ERROR", typeof(string));
                DataRow row = dt.NewRow();
                row["ERROR"] = $"{ex.Message} at line \n\n {new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber()}";
                dt.Rows.Add(row);
            }
            return dt;
        }
        
        public DataTable LlenarCajasListasCosteo()
        {
            DataTable dt = new DataTable();
            try
            {
                using(SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using(SqlCommand cmd = new SqlCommand("CosteoArticulosLlenarCajasListasCosteo", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmListasCosteoID", intListasCosteoID);
                        using(SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                dt.Columns.Add("ERROR", typeof(string));
                DataRow row = dt.NewRow();
                row["ERROR"] = $"{ex.Message} at line \n\n {new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber()}";
                dt.Rows.Add(row);
            }
            return dt;
        }

        // DEVUELVE LA LISTA DE ARTICULOS DE UNA LISTA DE COSTEO O UNA LISTA VACÍA PARA CREAR UNA NUEVA LISTA DE COSTEO
        public DataTable CosteoArticulosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                using(SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("CosteoArticulosGrid", cnn)) 
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmTabla", strTabla);
                        cmd.Parameters.AddWithValue("@prmListasCosteo", ListasCosteo);
                        cmd.Parameters.AddWithValue("@prmListasPrecioID", intListasPrecioID);
                        using(SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt.Columns.Add("ERROR", typeof(string));
                DataRow row = dt.NewRow();
                row["ERROR"] = $"{ex.Message} at line \n\n {new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber()}";
                dt.Rows.Add(row);
            }
            return dt;
        }

        public DataTable CosteoArticulosGridIndependiente()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("CosteoArticulosGridIndependiente", cnn))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmTabla", strTabla);
                        cmd.Parameters.AddWithValue("@prmListasCosteo", ListasCosteo);
                        cmd.Parameters.AddWithValue("@prmListasCosteoIDIndependiente", intListasCosteoIDIndependiente);
                        cmd.Parameters.AddWithValue("@prmListasPrecioID", intListasPrecioID);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt.Columns.Add("ERROR", typeof(string));
                DataRow row = dt.NewRow();
                row["ERROR"] = $"{ex.Message} at line \n\n {new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber()}";
                dt.Rows.Add(row);
            }
            return dt;
        }

        public string CosteoArticulosCrud()
        {
            string result = string.Empty;
            try
            {
                using(SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using(SqlCommand cmd = new SqlCommand("CosteoArticulosCrud", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmListasCosteo", ListasCosteo);
                        cmd.Parameters.AddWithValue("@prmListasCosteoDetalle", ListasCosteoDetalle);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                result = reader["result"].ToString();
                            }
                            else
                                result = "no read";
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                result = ex.Message;
            }
            return result;
        }

        public string CosteoArticulosModificarListasPrecios()
        {
            string result = string.Empty;
            try
            {
                using (SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("CosteoArticulosModificarListasPrecios", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmListasCosteoDetalle", ListasCosteoDetalle);
                        cmd.Parameters.AddWithValue("@prmListasPrecioID", intListasPrecioID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                result = reader["result"].ToString();
                            }
                            else
                                result = "no read";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        
        public string CosteoArticulosEliminar()
        {
            string result = string.Empty;
            try
            {
                using (SqlConnection cnn = new SqlConnection (strCnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("CosteoArticulosEliminar", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmListasCosteoID", intListasCosteoID);
                        using (SqlDataReader reader = cmd.ExecuteReader ())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                result = reader["result"].ToString();
                            }
                            else
                                result = "no read";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        #endregion METODOS

    }
}
