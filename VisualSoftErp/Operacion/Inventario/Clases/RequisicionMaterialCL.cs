using DevExpress.DashboardWin.Bars;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Inventario.Clases
{
    public class RequisicionMaterialCL
    {
        #region PROPIEDADES
        public int intFolio { get; set; }
        public int intAño { get; set; }
        public int intMes { get; set; }
        public string strRazonCancelacion { get; set; }
        public int intProveedoresID { get; set; }
        public DataTable dtRequisicionMaterial { get; set; } = new DataTable()
        {
            Columns =
            {
                new DataColumn("intFolio", typeof(int)),
                new DataColumn("dateFecha", typeof(DateTime)),
                new DataColumn("intStatus", typeof(int)),
                new DataColumn("strObservaciones", typeof(string)),
                new DataColumn("strOrdenCompraSerie", typeof(string)),
                new DataColumn("intOrdenCompraFolio", typeof(int)),
                new DataColumn("intUsuariosID", typeof(int)),
                new DataColumn("intUsuarioAutorizacionID", typeof(int)),
                new DataColumn("strMaquinaAutorizado", typeof(string)),
                new DataColumn("intUsuarioCancelacionID", typeof(int)),
                new DataColumn("strMaquinaCancelacion", typeof(string)),
                new DataColumn("dateFechaCancelacion", typeof(DateTime)),
                new DataColumn("strRazonCancelacion", typeof(string))
            }
        };
        public DataTable dtRequisicionMaterialDetalle { get; set; } = new DataTable()
        {
            Columns =
            {
                new DataColumn("intFolio", typeof(int)),
                new DataColumn("intArticulosID", typeof(int)),
                new DataColumn("decCantidadRequerida", typeof(decimal)),
                new DataColumn("dateFechaRequerida", typeof(DateTime)),
            }
        };
        #endregion PROPIEDADES


        #region CONSTRUCTOR
        public RequisicionMaterialCL() 
        {
        
        }
        #endregion CONSTRUCTOR


        #region METODOS
        public DataTable RequisicionMaterialGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cnn = new SqlConnection(globalCL.gv_strcnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("RequisicionMaterialGrid", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmAño", intAño);
                        cmd.Parameters.AddWithValue("@prmMes", intMes);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
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

        public string RequisicionMaterialCrud()
        {
            string result = string.Empty;
            try
            {
                using (SqlConnection cnn = new SqlConnection(globalCL.gv_strcnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("RequisicionMaterialCrud", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmRequisicionMaterial", dtRequisicionMaterial);
                        cmd.Parameters.AddWithValue("@prmRequisicionMaterialDetalle", dtRequisicionMaterialDetalle);
                        using (SqlDataReader dr = cmd.ExecuteReader())
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
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string RequisicionMaterialLlenaCajas()
        {
            string result = string.Empty;
            try
            {
                DataSet dataSet = new DataSet();
                using (SqlConnection cnn = new SqlConnection(globalCL.gv_strcnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("RequisicionMaterialLlenaCajas", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))   
                        {
                            adapter.Fill(dataSet);
                        }
                    }
                }

                // Master
                foreach(DataRow dr in dataSet.Tables[0].Rows)
                {
                    DataRow row = dtRequisicionMaterial.NewRow();
                    result = dr["result"].ToString();
                    row["intFolio"] = Convert.ToInt32(dr["Folio"]);
                    row["dateFecha"] = Convert.ToDateTime(dr["Fecha"]);
                    row["intStatus"] = Convert.ToInt32(dr["Status"]);
                    row["strObservaciones"] = dr["Observaciones"].ToString();
                    row["strOrdenCompraSerie"] = dr["OrdenCompraSerie"].ToString();
                    row["intOrdenCompraFolio"] = Convert.ToInt32(dr["OrdenCompraFolio"]);
                    row["intUsuariosID"] = Convert.ToInt32(dr["UsuariosID"]);
                    row["intUsuarioAutorizacionID"] = Convert.ToInt32(dr["UsuarioAutorizacionID"]);
                    row["strMaquinaAutorizado"] = dr["MaquinaAutorizado"].ToString();
                    
                    if (dr["FechaCancelacion"] == DBNull.Value) row["dateFechaCancelacion"] = DBNull.Value;
                    else row["dateFechaCancelacion"] = Convert.ToDateTime(dr["FechaCancelacion"]);
                    
                    row["strRazonCancelacion"] = dr["RazonCancelacion"].ToString();
                    dtRequisicionMaterial.Rows.Add(row);
                }

                // Detalle
                foreach (DataRow dr in dataSet.Tables[1].Rows)
                {
                    DataRow row = dtRequisicionMaterialDetalle.NewRow();
                    row["intFolio"] = Convert.ToInt32(dr["Folio"]);
                    row["intArticulosID"] = Convert.ToInt32(dr["ArticulosID"]);
                    row["decCantidadRequerida"] = Convert.ToDecimal(dr["CantidadRequerida"]);
                    row["dateFechaRequerida"] = Convert.ToDateTime(dr["FechaRequerida"]);
                    dtRequisicionMaterialDetalle.Rows.Add(row);
                }
            }
            catch (Exception ex) 
            {
                result = ex.Message;
            }
            return result;
        }
        
        public string RequisicionMaterialCancelar()
        {
            string result = string.Empty;
            try
            {
                using (SqlConnection cnn = new SqlConnection(globalCL.gv_strcnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("RequisicionMaterialCancelar", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                        cmd.Parameters.AddWithValue("@prmRazonCancelacion", strRazonCancelacion);
                        cmd.Parameters.AddWithValue("@prmUsuarioCancelacionID", globalCL.gv_UsuarioID);
                        cmd.Parameters.AddWithValue("@prmMaquinaCancelacion", Environment.MachineName);
                        using (SqlDataReader dr = cmd.ExecuteReader())
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
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string RequisicionMaterialAutorizar()
        {
            string result = string.Empty;
            try
            {
                using (SqlConnection cnn = new SqlConnection(globalCL.gv_strcnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("RequisicionMaterialAutorizar", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                        cmd.Parameters.AddWithValue("@prmUsuarioAutorizacionID", globalCL.gv_UsuarioID);
                        cmd.Parameters.AddWithValue("@prmMaquinaAutorizacion", Environment.MachineName);
                        using (SqlDataReader dr = cmd.ExecuteReader())
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
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string RequisicionMterialLlenaCajasOC()
        {
            string result = string.Empty;
            try
            {
                DataSet dataSet = new DataSet();
                using (SqlConnection cnn = new SqlConnection(globalCL.gv_strcnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("RequisicionMterialLlenaCajasOC", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                        cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataSet);
                        }
                    }
                }

                // Master
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    DataRow row = dtRequisicionMaterial.NewRow();
                    result = dr["result"].ToString();
                    row["intFolio"] = Convert.ToInt32(dr["Folio"]);
                    row["dateFecha"] = Convert.ToDateTime(dr["Fecha"]);
                    row["intStatus"] = Convert.ToInt32(dr["Status"]);
                    row["strObservaciones"] = dr["Observaciones"].ToString();
                    row["strOrdenCompraSerie"] = dr["OrdenCompraSerie"].ToString();
                    row["intOrdenCompraFolio"] = Convert.ToInt32(dr["OrdenCompraFolio"]);
                    row["intUsuariosID"] = Convert.ToInt32(dr["UsuariosID"]);
                    row["intUsuarioAutorizacionID"] = Convert.ToInt32(dr["UsuarioAutorizacionID"]);
                    row["strMaquinaAutorizado"] = dr["MaquinaAutorizado"].ToString();

                    if (dr["FechaCancelacion"] == DBNull.Value) row["dateFechaCancelacion"] = DBNull.Value;
                    else row["dateFechaCancelacion"] = Convert.ToDateTime(dr["FechaCancelacion"]);

                    row["strRazonCancelacion"] = dr["RazonCancelacion"].ToString();
                    dtRequisicionMaterial.Rows.Add(row);
                }

                // Detalle
                foreach (DataRow dr in dataSet.Tables[1].Rows)
                {
                    DataRow row = dtRequisicionMaterialDetalle.NewRow();
                    row["intFolio"] = Convert.ToInt32(dr["Folio"]);
                    row["intArticulosID"] = Convert.ToInt32(dr["ArticulosID"]);
                    row["decCantidadRequerida"] = Convert.ToDecimal(dr["CantidadRequerida"]);
                    row["dateFechaRequerida"] = Convert.ToDateTime(dr["FechaRequerida"]);
                    dtRequisicionMaterialDetalle.Rows.Add(row);
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
