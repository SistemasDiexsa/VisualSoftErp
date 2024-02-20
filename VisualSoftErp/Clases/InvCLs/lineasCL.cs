using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VisualSoftErp.Clases
{
    public class lineasCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intLineasID;
        public string strNombre;
        public decimal decMargenarribadelcosto;
        public int intCodigoArticulo;
        #endregion

        #region Constructor
        public lineasCL()
        {
            intLineasID = 0;
            strNombre = string.Empty;
            decMargenarribadelcosto = 0;
        }
        #endregion

        #region Metodos
        public DataTable lineasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "LineasGrid";
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

        public string LineasCrud()
        {
            
            try
            {
                string result = string.Empty;
               
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "LineasCRUD";
                cmd.Parameters.AddWithValue("@prmLineasId", intLineasID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmMargenarribadelcosto", decMargenarribadelcosto);
                cmd.Parameters.AddWithValue("@prmCodigoArticulo", intCodigoArticulo);
                
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
                return "LineasCrud :" + ex.Message;
            }

         }//public class lineas CRUD

        public string LineasllenaCajas()
        {

            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "LineasllenaCajas";
                cmd.Parameters.AddWithValue("@prmLineasId", intLineasID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strNombre = dr["Nombre"].ToString();
                    decMargenarribadelcosto = Convert.ToDecimal(dr["Margenarribadelcosto"]);
                    intCodigoArticulo = dr["CodigoArticulo"] != DBNull.Value ? Convert.ToInt32(dr["CodigoArticulo"]) : 0;
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
                return "LineasllenaCajas: " + ex.Message;
            }
        }//public class llena cajas

        public string LineasEliminar()
        {

            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "LineasEliminar";
                cmd.Parameters.AddWithValue("@prmLineasId", intLineasID);               
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
                return "LineasEliminar :" + ex.Message;
            }

        }//public class LineasEliminar
        #endregion

    } //public class lineasCL

    }
