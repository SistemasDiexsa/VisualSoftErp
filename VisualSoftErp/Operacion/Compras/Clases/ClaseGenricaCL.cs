using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Compras.Clases
{
    public class ClaseGenricaCL
    {
        string strCnn = globalCL.gv_strcnn;
        public int intClave;
        public string strDes;
        public string strTabla;

        public string Clave { get; set; }
        public string Des { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }


        public DataTable CargaCombosFamiliasByIdLinea(int IdLinea)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FamiliasByIdLinea";
                cmd.Parameters.AddWithValue("@prmLineaId", IdLinea);
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


        }//CargarCombos()
    }
}
