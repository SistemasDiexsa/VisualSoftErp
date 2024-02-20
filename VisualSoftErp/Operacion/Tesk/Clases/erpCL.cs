using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases;

namespace TeskApiTest.Clases
{
    public class erpCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
            //ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public int intDato { get; set; }
        public int intFolio { get; set; }
        public int intSeq { get; set; }
        public string strTabla { get; set; }
        public string strSerie { get; set; }
        public string strPoliza { get; set; }
        public string strCta { get; set; }
        public int intEjer { get; set; }
        public int intMes { get; set; }
        public int intTipoDoc { get; set; }
        public decimal decSubtotal { get; set; }
        public decimal decIva { get; set; }
        public decimal decNeto { get; set; }
        public string strRfc { get; set; }
        public string strMoneda { get; set; }


        #endregion
        #region Constructor
        public erpCL()
        {
            intDato = 0;
            intFolio = 0;
            strTabla = string.Empty;
            strSerie = string.Empty;
            intSeq = 0;
            intEjer = 0;
            intMes = 0;
            strRfc = string.Empty;
            strMoneda = string.Empty;
        }
        #endregion
        #region Métodos
        public DataTable Datos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Tesk_" + strTabla;
                if (strTabla == "Proveedores")
                {
                    cmd.Parameters.AddWithValue("@prmOpcion", 2);
                }
                cmd.Parameters.AddWithValue("@prmDato", intDato);

                if (strTabla=="Cfdi" || strTabla== "CancelaCfdi" || strTabla=="CancelaNotasdecredito" || strTabla == "CancelaCobranza")
                {
                    cmd.Parameters.AddWithValue("@prmEjer", intEjer);
                    cmd.Parameters.AddWithValue("@prmMes", intMes);
                }
                
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);

                cnn.Close();

                return dt;

            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return dt;

            }


        }//Clientes()
        public DataSet CobranzaDeposito()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Tesk_" + strTabla;
                cmd.Parameters.AddWithValue("@prmDato", 0);
                cmd.Parameters.AddWithValue("@prmEjer", intEjer);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(ds);
                cnn.Close();
                return ds;

            }
            catch (Exception) { return ds; }
        } //Datosdelaorden
        public string teskSincronizaErp()
        {
            try
            {
                if (strCta == null)
                    strCta = string.Empty;

                if (strPoliza == null)
                    strPoliza = "";

                if (intSeq == null)
                    intSeq = 0;

                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Tesk_ActualizaStatus";
                cmd.Parameters.AddWithValue("@prmTabla", strTabla);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmPoliza", strPoliza);
                cmd.Parameters.AddWithValue("@prmCuenta", strCta);
                cmd.Parameters.AddWithValue("@prmSeq", intSeq);
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
                return ex.Message;
            }
        } //cyb_BancosCrud

        public string teskConciliacionCrud()
        {
            try
            {
               
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Tesk_Conciliar_Crud";
                cmd.Parameters.AddWithValue("@prmTipoDoc", intTipoDoc);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmSubtotal", decSubtotal);
                cmd.Parameters.AddWithValue("@prmIva", decIva);
                cmd.Parameters.AddWithValue("@prmNeto", decNeto);
                cmd.Parameters.AddWithValue("@prmAño", intEjer);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.Parameters.AddWithValue("@prmRfc", strRfc);
                cmd.Parameters.AddWithValue("@prmMoneda", strMoneda);
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
                return ex.Message;
            }
        } //cyb_BancosCrud

        public DataSet ConciliaTesk()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Tesk_Concilia";
                cmd.Parameters.AddWithValue("@prmAño", intEjer);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(ds);
                cnn.Close();
                return ds;

            }
            catch (Exception) { return ds; }
        } //Datosdelaorden
        #endregion
    }
}
