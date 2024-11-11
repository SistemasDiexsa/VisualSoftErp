using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using Facturacion.Clases;

namespace VisualSoftErp.Clases
{
    public class BFCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intFolio { get; set; }  
        public int intProductoTerminado { get; set; }
        public int intSeq { get; set; }
        public DateTime fFecha { get; set; }
        public string strPT { get; set; }
        public Decimal decCantidad { get; set; }
        public DateTime fFechaSistema { get; set; }
        public decimal dLitros { get; set; }
        public string strStatus { get; set; }
        public DateTime fFechaCan { get; set; }
        public string strRazonCan { get; set; }
        public int intUsuariosID { get; set; }
        public int intArticulosID { get; set; }
        public int intUsuarioID { get; set; }
        public int intID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        public string strDoc { get; set; }
        public string strRazon { get; set; }
        public string strObservaciones { get; set; }
        public string strNombre { get; set; }
        public int intFactorUm2 { get; set; }
        public int intEfectoCuprum { get; set; }
        public int intAño { get; set; }
        public int intMes { get; set; }
        public DataTable dtComponentes { get; set; }
        #endregion

        #region Constructor
        public BFCL()
        {
            intFolio = 0;
            intSeq = 0;
            fFecha = DateTime.Now;
            strPT = string.Empty;
            decCantidad = 0;
            fFechaSistema = DateTime.Now;
            dLitros = 0;
            strStatus = string.Empty;
            fFechaCan = DateTime.Now;
            strRazonCan = string.Empty;
            intUsuariosID = 0;
            intArticulosID = 0;
            intAño = 0;
            intMes = 0;
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
        public DataTable BFGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "bfGrid";
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //bfGrid

        public string BFCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BFCRUD";
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.Parameters.AddWithValue("@prmCantidad", decCantidad);
                cmd.Parameters.AddWithValue("@prmLitros", dLitros);
                cmd.Parameters.AddWithValue("@prmUsuariosID", intUsuariosID);
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                cmd.Parameters.AddWithValue("@prmObservaciones", strObservaciones);
                cmd.Parameters.AddWithValue("@prmEfectoCuprum", intEfectoCuprum);
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
        } //BFCrud

        public string BFLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BFllenaCajas";
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intFolio = Convert.ToInt32(dr["Folio"]);
                    intSeq = Convert.ToInt32(dr["Seq"]);
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    strPT = dr["PT"].ToString();
                    decCantidad = Convert.ToInt32(dr["Cantidad"]);
                    fFechaSistema = Convert.ToDateTime(dr["FechaSistema"]);
                    dLitros = Convert.ToInt32(dr["Litros"]);
                    strStatus = dr["Status"].ToString();
                    //fFechaCan = Convert.ToDateTime(dr["FechaCan"]);
                    strRazonCan = dr["RazonCan"].ToString();
                    intUsuariosID = Convert.ToInt32(dr["UsuariosID"]);
                    intArticulosID = Convert.ToInt32(dr["ArticulosID"]);
                    strObservaciones = dr["Observaciones"].ToString();
                    strNombre = dr["Nombre"].ToString();
                    intFactorUm2 = Convert.ToInt32(dr["FactorUM2"]);

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

        public string BFEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BFEliminar";
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
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
        } // Public Class Eliminar

        public string ValidarProductoTerminado()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ValidarPTenComponentes";
                cmd.Parameters.AddWithValue("@prmArticulosID", intProductoTerminado);
  
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
        } // public class LlenaCajas

        public string ValidarExistenciasComponentesProductoTerminado()
        {
            string result = string.Empty;
            try
            {
                using (SqlConnection cnn = new SqlConnection(globalCL.gv_strcnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("ValidarExistenciasComponentesProductoTerminado", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmArticulosID", intProductoTerminado);
                        cmd.Parameters.AddWithValue("@prmCantidad", decCantidad);
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

        public string BFCancelar()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BFCancelar";
      
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
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

        public string ProduccionHojaFormatoPrev()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string result = string.Empty;
            try
            {
                using (SqlConnection cnn = new SqlConnection(globalCL.gv_strcnn))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("ProduccionHojaFormatoPrev", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            ad.Fill(ds);
                        }
                    }
                }
                dtComponentes = ds.Tables[0];
                dt = ds.Tables[1];
                result = dt.Rows[0]["result"].ToString();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        #endregion
    }
}