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
    public class DepositosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public string strRazon { get; set; }
        public int intFolio { get; set; }
        public string strSerieFac { get; set; }
        public int intFac { get; set; }
        public DateTime fFecha { get; set; }
        public decimal dImporte { get; set; }
        public int intClientesID { get; set; }
        public int intCliente2 { get; set; }
        public int intCuentasBancariaID { get; set; }
        public decimal intTipodecambio { get; set; }
        public int intBancosId { get; set; }
        public string strCuentaOrdenante { get; set; }
        public string strC_Formapago { get; set; }
        public int intNumeroOperacion { get; set; }
        public int intC_TipoCadenaPago { get; set; }
        public string strMonedaDelPago { get; set; }
        public string strStatus { get; set; }
        public string strHora { get; set; }
        public DateTime fFechaReal { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public string strDoc { get; set; } 
        public int intID { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        public decimal dTipodecambio { get; set; }  
        public int iParcialidad { get; set; }
        public int intAño { get; set; }
        public int intMes { get; set; }
        public int intDepositoRecepcion { get; set; }
        #endregion

        #region Constructor
        public DepositosCL()
        {
            strSerieFac = string.Empty;
            intFac = 0;
            strSerie = string.Empty;
            intFolio = 0;
            fFecha = DateTime.Now;
            dImporte = 0;
            intClientesID = 0;
            intCuentasBancariaID = 0;
            intTipodecambio = 0;
            intBancosId = 0;
            strCuentaOrdenante = string.Empty;
            strC_Formapago = string.Empty;
            intNumeroOperacion = 0;
            intC_TipoCadenaPago = 0;
            strMonedaDelPago = string.Empty;
            strStatus = string.Empty;
            strHora = string.Empty;
            fFechaReal = DateTime.Now;
            dTipodecambio = 0;
            intID = 0;
            intDepositoRecepcion = 0;
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
        public DataTable DepositosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "depositosGrid";
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
        } //depositosGrid

        public string DepositosGeneraAntiguedaddesaldos()
        {
            
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Depositosfacturasporcobrarparadepositos";
                cmd.Parameters.AddWithValue("@prmCliente", intClientesID);
                cmd.Parameters.AddWithValue("@prmUsu", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmCte2", intCliente2);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                string result;
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
        } //depositosCargaFacturas

        public string SaldoTesk()
        {

            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DepositosDetalleRedondeadosTesk_Actualiza";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmSerieFac", strSerieFac);
                cmd.Parameters.AddWithValue("@prmFac", intFac);
                cmd.Parameters.AddWithValue("@prmSaldo", dImporte);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                string result;
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
        } //depositosCargaFacturas

        public DataTable DepositosCargaFacturas()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Depositosleefacturasporcobrartmp";                
                cmd.Parameters.AddWithValue("@prmUsu", intUsuarioID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //depositosCargaFacturas

        public DataTable AntSaldos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CxCAntiguedaddeSaldosRep";
                cmd.Parameters.AddWithValue("@prmEmp", 0);
                cmd.Parameters.AddWithValue("@prmSuc", 0);
                cmd.Parameters.AddWithValue("@prmFechacorte", DateTime.Now);
                cmd.Parameters.AddWithValue("@prmClienteIn", intClientesID);
                cmd.Parameters.AddWithValue("@prmClienteFin", intClientesID);
                cmd.Parameters.AddWithValue("@prmAgenteIn", 0);
                cmd.Parameters.AddWithValue("@prmAgenteFin", 99999);
                cmd.Parameters.AddWithValue("@prmNivelinfo", 0);
                cmd.Parameters.AddWithValue("@prmDummy", 0);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //depositosCargaFacturas

        public string DepositosCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DepositosCRUD";
                cmd.Parameters.AddWithValue("@prmDepositos", dtm);
                cmd.Parameters.AddWithValue("@prmDepositosdetalle", dtd);
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);
                cmd.Parameters.AddWithValue("@prmPrograma", strPrograma);
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
        } //DepositosCrud

        public string DepositosLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DepositosllenaCajas";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strSerie = dr["Serie"].ToString();
                    intFolio = Convert.ToInt32(dr["Folio"]);
                    fFecha = Convert.ToDateTime(dr["Fecha"]);
                    dImporte = Convert.ToDecimal(dr["Importe"]);
                    intClientesID = Convert.ToInt32(dr["ClientesID"]);
                    intCuentasBancariaID = Convert.ToInt32(dr["CuentasBancariaID"]);
                    intTipodecambio = Convert.ToInt32(dr["CuentasBancariaID"]);
                    dTipodecambio =Convert.ToDecimal(dr["Tipodecambio"]);
                    intBancosId = Convert.ToInt32(dr["BancosId"]);
                    strCuentaOrdenante = dr["CuentaOrdenante"].ToString();
                    strC_Formapago = dr["C_Formapago"].ToString();
                    intNumeroOperacion = Convert.ToInt32(dr["NumeroOperacion"]);
                    intC_TipoCadenaPago = Convert.ToInt32(dr["C_TipoCadenaPago"]);
                    strMonedaDelPago = dr["MonedaDelPago"].ToString();
                    strStatus = dr["Status"].ToString();
                    strHora = dr["Hora"].ToString();
                    fFechaReal = Convert.ToDateTime(dr["FechaReal"]);
                    intDepositoRecepcion = dr["DepositoRecepcion"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DepositoRecepcion"]);
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

        public string DepositosCancelar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DepositosCancelar";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmRazon", strRazon);
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
                return "DepositosCancelarSQl:" + ex.Message;
            }
        } // Public Class Eliminar

        public string Depositosparcialidades()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DepositosParcialidades";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    iParcialidad = Convert.ToInt32(dr["Parcialidad"].ToString());
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
        } // Public Class Eliminar


        //Siguiente ID para Depositos cv
        public string DocumentosSiguienteID()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DocumentosSiguienteID";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmDoc", strDoc);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intID = Convert.ToInt32(dr["SigID"]);
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

        //Buscara la siguiente serie de acuerdo al usuario cv
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
        }              
        

        #endregion
    }
}