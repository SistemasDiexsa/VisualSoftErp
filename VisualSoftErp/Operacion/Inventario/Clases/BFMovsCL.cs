using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace VisualSoftErp.Clases
{
    public class BFMovsCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intFolio { get; set; }
        public string strTipo { get; set; }
        public int intSeq { get; set; }
        public int intRen { get; set; }
        public int intArticulosID { get; set; }
        public decimal intCantidad { get; set; }
        public DateTime fFecha { get; set; }
        public string strHora { get; set; }
        public string strObs { get; set; }
        public string strStatus { get; set; }
        public int intAlmacenesID { get; set; }
        public int intUsuariosID { get; set; }
        public int intUsuarioCancelaID { get; set; }
        public DateTime fFechaCancelacion { get; set; }
        public string strRazonCancelacion { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; } public string strRazon { get; set; }
        public int intFactorUm2 { get; set; }
        public int intAño { get; set; }
        public int intMes { get; set; }
        #endregion

        #region Constructor
        public BFMovsCL()
        {
            intFolio = 0;
            strTipo = string.Empty;
            intSeq = 0;
            intRen = 0;
            intArticulosID = 0;
            intCantidad = 0;
            fFecha = DateTime.Now;
            strHora = string.Empty;
            strObs = string.Empty;
            strStatus = string.Empty;
            intAlmacenesID = 0;
            intUsuariosID = 0;
            intUsuarioCancelaID = 0;
            fFechaCancelacion = DateTime.Now;
            strRazonCancelacion = string.Empty;
          

        }
        #endregion

        #region Metodos
        private string loadConnectionString()
        {
            try
            {
                XmlDocument oxml = new XmlDocument();
                oxml.Load(@"C:\VisualSoftErp\xml\conexion.xml");
                XmlNodeList sConfiguracion = oxml.GetElementsByTagName("Configuracion");
                XmlNodeList sGenerales = ((XmlElement)sConfiguracion[0]).GetElementsByTagName("Generales");
                XmlNodeList sStr_Conn = ((XmlElement)sGenerales[0]).GetElementsByTagName("Str_Conn");
                return sStr_Conn[0].InnerText;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public DataTable BFMovsGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "bfmovsGrid";
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //bfmovsGrid

        public DataTable BFMPGridDETALLE()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BFMPGridDETALLE";
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmTMov", strTipo);
                cmd.Parameters.AddWithValue("@prmSeq", intSeq);             
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //bfmovsGrid

        public DataTable BFMovsGridDETALLE()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BFMovsGridDETALLE";
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmTipo", strTipo);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //bfmovsGrid


        public string BFMovsCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BFMovsCRUD";
            
                cmd.Parameters.AddWithValue("@prmBFMovs", dtm);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmTipo", strTipo);
                cmd.Parameters.AddWithValue("@prmSeq", intSeq);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);
         
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr.GetOrdinal("result") != -1)
                        result = dr["result"].ToString();
                    else
                        result = "La columna result no existe en la respuesta del SP";
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
                string line = ex.LineNumber().ToString();
                return "Error: " + ex.Message + "\nEn línea: " + line;
            }
        } //BFMovsCrud

        public DataTable LlenaCajasBfMovs()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BFMovsLlenaCajas";
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmTipo", strTipo);
                cmd.Parameters.AddWithValue("@prmSeq", intSeq);
                cmd.Parameters.AddWithValue("@prmCant", intCantidad);
                cmd.Parameters.AddWithValue("@prmFactorPT", intFactorUm2);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        }

        public string BFMovsLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BFMovsllenaCajas";
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmTipo", strTipo);
                cmd.Parameters.AddWithValue("@prmSeq", intSeq);
                cmd.Parameters.AddWithValue("@prmCant", intCantidad);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intFolio = Convert.ToInt32(dr["Folio"]);
                    strTipo = dr["Tipo"].ToString();
                    intSeq = Convert.ToInt32(dr["Seq"]);
                    intRen = Convert.ToInt32(dr["Ren"]);
                    intArticulosID = Convert.ToInt32(dr["ArticulosID"]);
                    //intCantidad = Convert.ToInt32(dr["ArticulosID"]);
                    //fFecha = Convert.ToDateTime(dr["ArticulosID"]);
                    //strHora = dr["Hora"].ToString();
                    //strObs = dr["Obs"].ToString();
                    //strStatus = dr["Status"].ToString();
                    //intAlmacenesID = Convert.ToInt32(dr["AlmacenesID"]);
                    //intUsuariosID = Convert.ToInt32(dr["UsuariosID"]);
                    //intUsuarioCancelaID = Convert.ToInt32(dr["UsuarioCancelaID"]);
                    //fFechaCancelacion = Convert.ToDateTime(dr["UsuarioCancelaID"]);
                    //strRazonCancelacion = dr["RazonCancelacion"].ToString();
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
        } // public class LlenaCajas

        public string BFMovsEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BFMovsEliminar";
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmTipo", strTipo);
                cmd.Parameters.AddWithValue("@prmSeq", intSeq);
                cmd.Parameters.AddWithValue("@prmRen", intRen);
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
        } // Public Class Eliminar

        public string BFMovsCancelar()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BFMovsCancelar";
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmTipo", strTipo);
                cmd.Parameters.AddWithValue("@prmSeq", intSeq);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);
                cmd.Parameters.AddWithValue("@prmRazondecancelacion", strRazon);
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
        }

        public string BFMovsSigID()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "bfMovsSiguienteSeq";
                cmd.Parameters.AddWithValue("@prmHP", intFolio);
                cmd.Parameters.AddWithValue("@prmMov", strTipo);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();


                    intSeq = Convert.ToInt32(dr["Seq"]);

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

        #endregion
    }
}