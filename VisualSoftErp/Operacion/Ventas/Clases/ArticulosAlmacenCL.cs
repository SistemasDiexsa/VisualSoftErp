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
    public class ArticulosAlmacenCL
    {
        #region PROPIEDADES
        
        private string strCnn = globalCL.gv_strcnn;
        public int intLineasID { get; set; }
        public int intFamiliasID { get; set; }
        public int intSubFamiliasID { get; set; }
        public int intActivos { get; set; }
        public int intAlmacenesID { get; set; }
        public int intPagina { get; set; }
        
        #endregion PROPIEDADES


        #region CONSTRUCTOR
        
        public ArticulosAlmacenCL() 
        {
            intLineasID = 0;
            intFamiliasID = 0;
            intSubFamiliasID = 0;
            intActivos = 0;
            intAlmacenesID = 0;
            intPagina = 0;
        }
        
        #endregion CONSTRUCTOR


        #region METODOS
        
        public DataTable ArticulosAlmacenGRID()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cnn = new SqlConnection(strCnn))
                {
                    cnn.Open();
                    using(SqlCommand cmd = new SqlCommand("ArticulosAlmacenGRID", cnn))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmActivos", intActivos);
                        cmd.Parameters.AddWithValue("@prmLineasID", intLineasID);
                        cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
                        cmd.Parameters.AddWithValue("@prmSubFamiliasID", intSubFamiliasID);
                        cmd.Parameters.AddWithValue("@prmAlmacenesID", intAlmacenesID);
                        cmd.Parameters.AddWithValue("@prmPagina", intPagina);

                        using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
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
        
        #endregion METODOS
    }
}
