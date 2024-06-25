using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Clases
{
    public class CiclicosCL
    {
        #region PROPIEDADES
        public int intUsuario { get; set; }
        public int intAlmacen {  get; set; }
        public string strArticulo { get; set; }
        public int intConteo { get; set; }
        public int intExistencia { get; set; }
        public int intDiferencia { get; set; }
        #endregion PROPIEDADES

        #region CONSTRUCTOR
        public CiclicosCL() 
        {
            intUsuario = 0;
            intAlmacen = 0;
            strArticulo = string.Empty;
            intConteo = 0;
            intExistencia = 0;
            intDiferencia = 0;
        }
        #endregion CONSTRUCTOR

        #region METODOS
        public string InventarioCiclicoCrud()
        {
            DataTable dt = new DataTable();
            string result = string.Empty;
            using (SqlConnection connection = new SqlConnection(globalCL.gv_strcnn))
            {
                try
                {
                    connection.Open();
                    string SP = "InventarioCiclicoCrud";
                    using (SqlCommand cmd = new SqlCommand(SP, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmAlm", intAlmacen);
                        cmd.Parameters.AddWithValue("@prmArt", strArticulo);
                        cmd.Parameters.AddWithValue("@prmConteo", intConteo);
                        cmd.Parameters.AddWithValue("@prmUsuario", intUsuario);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                            result = dt.Rows[0]["result"].ToString();
                            intExistencia = Convert.ToInt32(dt.Rows[0]["Existencia"]);
                            intDiferencia = Convert.ToInt32(dt.Rows[0]["Diferencia"]);
                        }
                    }
                } 
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            return result;
        }
        #endregion METODOS
    }
}
