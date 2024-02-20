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
    public class MetaporarticuloCL
    {
        #region Propiedades
        string strCnn  = globalCL.gv_strcnn;
        public int intAño { get; set; }
        public int intArticulosID { get; set; }
        public decimal dENEP { get; set; }
        public decimal dFEBP { get; set; }
        public decimal dMARP { get; set; }
        public decimal dABRP { get; set; }
        public decimal dMAYP { get; set; }
        public decimal dJUNP { get; set; }
        public decimal dJULP { get; set; }
        public decimal dAGOP { get; set; }
        public decimal dSEPP { get; set; }
        public decimal dOCTP { get; set; }
        public decimal dNOVP { get; set; }
        public decimal dDICP { get; set; }

        public int intEmp { get; set; }
        public int intSuc { get; set; }
        public int intEje { get; set; }
        public int intMes { get; set; }
        public int intLin { get; set; }
        public int intFam { get; set; }
        public int intTipo { get; set; }


        #endregion

        #region Constructor
        public MetaporarticuloCL()
        {
            intAño = 0;
            intArticulosID = 0;
            dENEP = 0;
            dFEBP = 0;
            dMARP = 0;
            dABRP = 0;
            dMAYP = 0;
            dJUNP = 0;
            dJULP = 0;
            dAGOP = 0;
            dSEPP = 0;
            dOCTP = 0;
            dNOVP = 0;
            dDICP = 0;
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

        public DataTable MetaporarticuloGridDetalle()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "VentasEstadisticaPorLineaFamiliaArticuloParaPronostico";
                cmd.Parameters.AddWithValue("@prmEmp", intEmp);
                cmd.Parameters.AddWithValue("@prmSuc", intSuc);
                cmd.Parameters.AddWithValue("@prmEje", intEje);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.Parameters.AddWithValue("@prmLin", intLin);
                cmd.Parameters.AddWithValue("@prmFam", intFam);
                cmd.Parameters.AddWithValue("@prmTipo", intTipo);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //metaporarticuloGrid

        public string MetaporarticuloCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "MetaporarticuloCRUD";
                cmd.Parameters.AddWithValue("@prmAño", intAño);
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                cmd.Parameters.AddWithValue("@prmENEP", dENEP);
                cmd.Parameters.AddWithValue("@prmFEBP", dFEBP);
                cmd.Parameters.AddWithValue("@prmMARP", dMARP);
                cmd.Parameters.AddWithValue("@prmABRP", dABRP);
                cmd.Parameters.AddWithValue("@prmMAYP", dMAYP);
                cmd.Parameters.AddWithValue("@prmJUNP", dJUNP);
                cmd.Parameters.AddWithValue("@prmJULP", dJULP);
                cmd.Parameters.AddWithValue("@prmAGOP", dAGOP);
                cmd.Parameters.AddWithValue("@prmSEPP", dSEPP);
                cmd.Parameters.AddWithValue("@prmOCTP", dOCTP);
                cmd.Parameters.AddWithValue("@prmNOVP", dNOVP);
                cmd.Parameters.AddWithValue("@prmDICP", dDICP);
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
        } //MetaporarticuloCrud

        public string MetaporarticuloLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "MetaporarticulollenaCajas";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    //dr.Read();
                    //intAño = Convert.ToInt32(dr["Año"]);
                    //intArticulosID = Convert.ToInt32(dr["ArticulosID"]);
                    //dENEP = Convert.ToDecimal(dr["ENEP"]);
                    //decFEBP = Convert.ToDecimal(dr["FEBP"]);
                    //decMARP = Convert.ToDecimal(dr["MARP"]);
                    //decABRP = Convert.ToDecimal(dr["ABRP"]);
                    //decMAYP = Convert.ToDecimal(dr["MAYP"]);
                    //decJUNP = Convert.ToDecimal(dr["JUNP"]);
                    //decJULP = Convert.ToDecimal(dr["JULP"]);
                    //decAGOP = Convert.ToDecimal(dr["AGOP"]);
                    //decSEPP = Convert.ToDecimal(dr["SEPP"]);
                    //decOCTP = Convert.ToDecimal(dr["OCTP"]);
                    //decNOVP = Convert.ToDecimal(dr["NOVP"]);
                    //decDICP = Convert.ToDecimal(dr["DICP"]);
                    //result = "OK";
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

        public string MetaporarticuloEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "MetaporarticuloEliminar";
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