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
        public DataTable ArticulosMargenes { get; set; } = new DataTable
        {
            Columns =
            {
                new DataColumn("ArticulosID", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Costo", typeof(decimal)),
                new DataColumn("Porcentaje", typeof(decimal)),
                new DataColumn("Margen", typeof(int))
            }
        };
        #endregion PROPIEDADES

        #region CONSTRUCTOR
        public CosteosArticulosCL()
        {

        }
        #endregion CONSTRUCTOR

        #region METODOS
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
                        cmd.CommandType = CommandType.StoredProcedure;
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
                        cmd.Parameters.AddWithValue("@prmCosteoArticulosType", ArticulosMargenes);
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
        #endregion METODOS

    }
}
