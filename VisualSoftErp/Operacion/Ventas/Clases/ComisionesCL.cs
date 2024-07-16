using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Catalogos;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Ventas.Clases
{
    public class ComisionesCL
    {
        #region VARIABLES

        private string strCnn = globalCL.gv_strcnn;
        // tb_ConceptosComisiones
        public int intConceptosComisionesID { get; set; }
        public string strNombre { get; set; }
        public int intFormasCalculoComisionesID { get; set; }
        public int intClientesNuevos { get; set; }
        public int intCanalVentasID { get; set; }
        public int intLineasID { get; set; }
        public int intFamiliasID { get; set; }
        public int intSubFamiliasID { get; set; }
        public int intArticulosID { get; set; }
        public int intAgentesID { get; set; }
        public int intConceptoConjunto { get; set; }

        //tb_ComisionesAgentes
        public DataTable ComisionesAgentes { get; set; } = new DataTable
        {
            Columns =
            {
                new DataColumn("ComisionesAgentesID", typeof(int)),
                new DataColumn("AgentesID", typeof(int)),
                new DataColumn("ConceptosComisionesID", typeof(int)),
                new DataColumn("Porcentaje", typeof(decimal))
            }
        };

        #endregion VARIABLES


        #region CONSTRUCTOR

        public ComisionesCL() 
        {
            intConceptosComisionesID = 0;
            strNombre = string.Empty;
            intFormasCalculoComisionesID = 0;
            intClientesNuevos = 0;
            intLineasID = 0;
            intFamiliasID = 0;
            intSubFamiliasID = 0;
            intArticulosID = 0;
            intConceptoConjunto = 0;
        }

        #endregion CONSTRUCTOR


        #region METODOS

        public DataTable ConceptosComisionesGRID()
        {
            DataTable dt = new DataTable();
            try
            {
                using(SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using(SqlCommand cmd = new SqlCommand("ConceptosComisionesGRID", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
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

        public string ConceptosComisionesCRUD()
        {
            try
            {
                string result = string.Empty;

                using(SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using(SqlCommand cmd = new SqlCommand("ConceptosComisionesCRUD", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmConceptosComisionesID", intConceptosComisionesID);
                        cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                        cmd.Parameters.AddWithValue("@prmFormasCalculoComisionesID", intFormasCalculoComisionesID);
                        cmd.Parameters.AddWithValue("@prmClientesNuevos", intClientesNuevos);
                        cmd.Parameters.AddWithValue("@prmCanalVentasID", intCanalVentasID);
                        cmd.Parameters.AddWithValue("@prmLineasID", intLineasID);
                        cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
                        cmd.Parameters.AddWithValue("@prmSubFamiliasID", intSubFamiliasID);
                        cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                        cmd.Parameters.AddWithValue("@prmAgentesID", intAgentesID);
                        cmd.Parameters.AddWithValue("@prmConceptoConjunto", intConceptoConjunto);

                        using(SqlDataReader reader = cmd.ExecuteReader())
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
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ConceptosComisionesLlenarCajas()
        {
            try
            {
                string result = string.Empty;
                using(SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using(SqlCommand cmd = new SqlCommand("ConceptosComisionesLlenarCajas", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmConceptosComisionesID", intConceptosComisionesID);

                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                intConceptosComisionesID = Convert.ToInt32(reader["ConceptosComisionesID"]);
                                strNombre = reader["Nombre"].ToString();
                                intFormasCalculoComisionesID = Convert.ToInt32(reader["FormasCalculoComisionesID"]);
                                intClientesNuevos = Convert.ToInt32(reader["ClientesNuevos"]);
                                intCanalVentasID = Convert.ToInt32(reader["CanalVentasID"]);
                                intLineasID = Convert.ToInt32(reader["LineasID"]);
                                intFamiliasID = Convert.ToInt32(reader["FamiliasID"]);
                                intSubFamiliasID = Convert.ToInt32(reader["SubFamiliasID"]);
                                intArticulosID = Convert.ToInt32(reader["ArticulosID"]);
                                intAgentesID = Convert.ToInt32(reader["AgentesID"]);
                                intConceptoConjunto = Convert.ToInt32(reader["ConceptoConjunto"]);
                                result = "OK";
                            }
                            else
                                result = "no read";
                        }
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string ConceptosComisionesEliminar()
        {
            try
            {
                string result = string.Empty;
                using(SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using(SqlCommand cmd = new SqlCommand("ConceptosComisionesEliminar", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmConceptosComisionesID", intConceptosComisionesID);

                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                result = reader["result"].ToString();
                            }
                            else
                                result = "No read";
                        }
                    }
                }
                return result;
            }
            catch (Exception ex) 
            {
                return ex.Message;
            }
        }

        public DataTable ComisionesAgentesGRID()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("ComisionesAgentesGRID", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmAgentesID", intAgentesID);
                        
                        using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch(Exception ex)
            {
                return dt;
            }
        }

        public string ComisionesAgentesCRUD()
        {
            try
            {
                string result = string.Empty;
                using (SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("ComisionesAgentesCRUD", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmAgentesID", intAgentesID);
                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@prmComisionesAgentes", ComisionesAgentes);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.ComisionesAgentes_Type";

                        using(SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                result = dr["result"].ToString();
                            }
                            else
                                result = "no read";
                        }
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        
        #endregion METODOS
    }
}
