using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Inventarios.Clases
{
    class ExistenciasCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public DateTime Fechadecorte { get; set; }
        public int intFamiliasID { get; set; }
        public int intLineasID { get; set; }
        public int intObsoleto { get; set; }
        public int intAlm { get; set; }
        public int intArt { get; set; }
        public decimal decExPedidaSinFacturar { get; set; }
        public decimal decExistencia { get; set; }
        public decimal decExDisponible { get; set; }
        public decimal decMargendeseguridad { get; set; }
        public decimal decUltimoCosto { get; set; }
        public decimal decCostoconseguridad { get; set; }
        public int intDesactivados { get; set; }
        #endregion
        #region Constructor
        public ExistenciasCL()
        {
            intFamiliasID = 0;
            intObsoleto = 0;
            intLineasID = 0;
            Fechadecorte = DateTime.Now;
            intAlm = 0;
            intArt = 0;
            decExPedidaSinFacturar = 0;
            decExistencia = 0;
            decExDisponible = 0;
            decMargendeseguridad = 0;
            decUltimoCosto = 0;
            decCostoconseguridad = 0;
        }
        #endregion
        #region Metodos
        public DataTable ExistenciasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Existencias";
                cmd.Parameters.AddWithValue("@prmLinea", intLineasID);
                cmd.Parameters.AddWithValue("@prmFam", intFamiliasID);
                cmd.Parameters.AddWithValue("@prmFecha", Fechadecorte);
                cmd.Parameters.AddWithValue("@prmObsoleto", 0);
                cmd.Parameters.AddWithValue("@prmDesactivados", intDesactivados);

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


        }//ExistenciasGrid()

        public string ExistenciaDisponible()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InventariosExistenciaDisponible";
                cmd.Parameters.AddWithValue("@prmAlm", intAlm);
                cmd.Parameters.AddWithValue("@prmArt", intArt);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    decExDisponible = Convert.ToDecimal(dr["ExistenciaDisponible"]);
                    decExPedidaSinFacturar = Convert.ToDecimal(dr["ExPedidaSinFacturas"]);
                    decExistencia = Convert.ToDecimal(dr["Existencia"]);
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
        } //Existencia disponible
        public string UltimoCosto()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ArticulosUltimoCosto";
                cmd.Parameters.AddWithValue("@prmArt", intArt);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    decUltimoCosto = Convert.ToDecimal(dr["Ultimocosto"]);
                    decMargendeseguridad = Convert.ToDecimal(dr["Margendeseguridad"]);
                    decCostoconseguridad = Convert.ToDecimal(dr["Costoconseguridad"]);
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
        }  //Ultimocosto

        #endregion
    }
}
