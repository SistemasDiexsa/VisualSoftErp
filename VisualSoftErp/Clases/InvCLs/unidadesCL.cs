using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Clases
{
    public class unidadesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intUnidadesID;
        public string strNombre; 
        public string strClaveSat;
        #endregion
        #region Constructor
        public unidadesCL()
        {
            strNombre = String.Empty;
            strClaveSat = String.Empty;
        }
        #endregion
        #region Metodos

        public DataTable UnidadesGrid()
        {
            DataTable dt = new DataTable(); 
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UnidadesGrid";
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


        }//lineasGrid()

        public string UnidadesCrud()
        {

            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UnidadesCRUD";
                cmd.Parameters.AddWithValue("@prmUnidadesdemedidaID", intUnidadesID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmClaveSat", strClaveSat);
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
                return "UnidadesCrud: " + ex.Message;
            }

        }//public class lineas CRUD

        public string UnidadesllenaCajas()
        {

            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UnidadesllenaCajas";
                cmd.Parameters.AddWithValue("@prmUnidadesID", intUnidadesID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strNombre = dr["Nombre"].ToString();
                    strClaveSat = dr["ClaveSat"].ToString();
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
                return "UnidadesllenaCajas: " + ex.Message;
            }
        }//public class UnidadesllenaCajas()

        public string UnidadesEliminar()
        {

            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UnidadesEliminar";
                cmd.Parameters.AddWithValue("@prmUnidadesdemedidaID", intUnidadesID);
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
                return "UnidadesEliminar: " + ex.Message;
            }

        }//public class UnidadesEliminar
        #endregion
    }
}
