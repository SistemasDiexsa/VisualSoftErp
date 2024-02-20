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
    public class IncrementalesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public string strSerie { get; set; }
        public int intFolio { get; set; }
        public string strAduana { get; set; }
        public string strPedimento { get; set; }
        public DateTime fFecha { get; set; }
        public decimal dTipodecambio { get; set; }
        public decimal dFactordeintegracion { get; set; }
        public decimal dGastosaduanalesUSA { get; set; }
        public decimal dFleteextranjero { get; set; }
        public decimal dGastosaduanalesMX { get; set; }
        public decimal dFletenacional { get; set; }
        public decimal dDTA { get; set; }
        public decimal dIvadelpedimento { get; set; }
        public decimal dIGI { get; set; }
        public decimal dPRV { get; set; }
        public decimal dTotalincremental { get; set; }
        public decimal dBaseparacalcularPtje { get; set; }
        public decimal dPtjeincremental { get; set; }
        #endregion

        #region Constructor
        public IncrementalesCL()
        {
            strSerie = string.Empty;
            intFolio = 0;
            strAduana = string.Empty;
            fFecha = DateTime.Now;
            dTipodecambio = 0;
            dGastosaduanalesUSA = 0;
            dFleteextranjero = 0;
            dGastosaduanalesMX = 0;
            dFletenacional = 0;
            dDTA = 0;
            dIvadelpedimento = 0;
            dIGI = 0;
            dPRV = 0;
            dTotalincremental = 0;
            dBaseparacalcularPtje = 0;
            dPtjeincremental = 0;
            dFactordeintegracion = 0;
            strPedimento = string.Empty;
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
        public DataTable IncrementalesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "incrementalesGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //incrementalesGrid

        public string IncrementalesCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "IncrementalesCRUD";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.Parameters.AddWithValue("@prmAduana", strAduana);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.Parameters.AddWithValue("@prmTipodecambio", dTipodecambio);
                cmd.Parameters.AddWithValue("@prmFactordeintegracion", dFactordeintegracion);
                cmd.Parameters.AddWithValue("@prmGastosaduanalesUSA", dGastosaduanalesUSA);
                cmd.Parameters.AddWithValue("@prmFleteextranjero", dFleteextranjero);
                cmd.Parameters.AddWithValue("@prmGastosaduanalesMX", dGastosaduanalesMX);
                cmd.Parameters.AddWithValue("@prmFletenacional", dFletenacional);
                cmd.Parameters.AddWithValue("@prmDTA", dDTA);
                cmd.Parameters.AddWithValue("@prmIvadelpedimento", dIvadelpedimento);
                cmd.Parameters.AddWithValue("@prmIGI", dIGI);
                cmd.Parameters.AddWithValue("@prmPRV", dPRV);
                cmd.Parameters.AddWithValue("@prmTotalincremental", dTotalincremental);
                cmd.Parameters.AddWithValue("@prmBaseparacalcularPtje", dBaseparacalcularPtje);
                cmd.Parameters.AddWithValue("@prmPtjeincremental", dPtjeincremental);
                cmd.Parameters.AddWithValue("@prmPedimento", strPedimento);
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
        } //IncrementalesCrud

        public string BuscarIncrementalporSerieyFolio()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "IncrementalesBuscarSerieFolio";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    result = "OK";

                   
                        strSerie = dr["Serie"].ToString();
                        intFolio = Convert.ToInt32(dr["Folio"]);
                        strAduana = dr["Aduana"].ToString();
                        fFecha = Convert.ToDateTime(dr["Fecha"]);
                        dTipodecambio = Convert.ToDecimal(dr["Tipodecambio"]);
                        dGastosaduanalesUSA = Convert.ToDecimal(dr["GastosaduanalesUSA"]);
                        dFleteextranjero = Convert.ToDecimal(dr["Fleteextranjero"]);
                        dGastosaduanalesMX = Convert.ToDecimal(dr["GastosaduanalesMX"]);
                        dFletenacional = Convert.ToDecimal(dr["Fletenacional"]);
                        dDTA = Convert.ToDecimal(dr["DTA"]);
                        dIvadelpedimento = Convert.ToDecimal(dr["Ivadelpedimento"]);
                        dIGI = Convert.ToDecimal(dr["IGI"]);
                        dPRV = Convert.ToDecimal(dr["PRV"]);
                        dTotalincremental = Convert.ToDecimal(dr["Totalincremental"]);
                        dBaseparacalcularPtje = Convert.ToDecimal(dr["BaseparacalcularPtje"]);
                        dPtjeincremental = Convert.ToDecimal(dr["Ptjeincremental"]);
                        dFactordeintegracion = Convert.ToDecimal(dr["Factordeintegracion"]);
                    strPedimento = dr["Pedimento"].ToString();
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

        public string IncrementalesEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "IncrementalesEliminar";
                cmd.Parameters.AddWithValue("@prmSerieFolio", strSerie);
                cmd.Parameters.AddWithValue("@prmFolio", intFolio);
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

        #endregion
    }
}