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
    public class SerieCL
    {
        #region Propiedades
        //string strCnn = ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        string strCnn = globalCL.gv_strcnn;
        public int intUsuarioID;
        public string strSerie;
        public int intFolio;
        #endregion
        #region Constructor
        public SerieCL()
        {
            intUsuarioID = 0;
            strSerie = string.Empty;
            intFolio = 0;
           
        }
        #endregion
        #region Metodos
        public string BuscarSerieporUsuario()
        {
            DataTable dt = new DataTable();
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BuscarSerie";
                cmd.Parameters.AddWithValue("@prmUsuarioID", intUsuarioID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strSerie = Convert.ToString(dr["Serie"]);
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
        } //CargarCombos()


        public string BuscarFoliodeProduccion()
        {
            DataTable dt = new DataTable();
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BuscarFoliodeProduccion";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intFolio = Convert.ToInt32(dr["SigFolio"]);
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
        }

    }
    
    #endregion
}
