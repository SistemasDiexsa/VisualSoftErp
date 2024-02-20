using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace VisualSoftErp.Clases
{
    public class familiasCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intFamiliasID { get; set; }
        public string strNombre { get; set; }
        public int intLineasID { get; set; }
        public int intIncluireninventarios { get; set; }
        public int intArribos { get; set; }
        public string strPathimagen { get; set; }
        public decimal dMargenarribadelcosto { get; set; }
        public int intIncluirentienda { get; set; }
        public int intTipodenavegacion { get; set; }
        #endregion

        #region Constructor
        public familiasCL()
        {
            intFamiliasID = 0;
            strNombre = string.Empty;
            intLineasID = 0;
            intIncluireninventarios = 0;
            intArribos = 0;
            strPathimagen = string.Empty;
            dMargenarribadelcosto = 0;
            intIncluirentienda = 0;
            intTipodenavegacion = 0;
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
        public DataTable FamiliasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "familiasGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //familiasGrid

        public string FamiliasCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FamiliasCRUD";
                cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmLineasID", intLineasID);
                cmd.Parameters.AddWithValue("@prmIncluireninventarios", intIncluireninventarios);
                cmd.Parameters.AddWithValue("@prmArribos", intArribos);
                cmd.Parameters.AddWithValue("@prmPathimagen", strPathimagen);
                cmd.Parameters.AddWithValue("@prmMargenarribadelcosto", dMargenarribadelcosto);
                cmd.Parameters.AddWithValue("@prmIncluirentienda", intIncluirentienda);
                cmd.Parameters.AddWithValue("@prmTipodenavegacion", intTipodenavegacion);
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
        } //FamiliasCrud

        public string FamiliasLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FamiliasllenaCajas";
                cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strNombre = dr["Nombre"].ToString();
                    intLineasID = Convert.ToInt32(dr["LineasID"]);
                    intIncluireninventarios = Convert.ToInt32(dr["Incluireninventarios"]);
                    intArribos = Convert.ToInt32(dr["Arribos"]);
                    strPathimagen = dr["Pathimagen"].ToString();
                    dMargenarribadelcosto = Convert.ToDecimal(dr["Margenarribadelcosto"]);
                    intIncluirentienda = Convert.ToInt32(dr["Incluirentienda"]);
                    intTipodenavegacion = Convert.ToInt32(dr["Tipodenavegacion"]);
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

        public string FamiliasEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FamiliasEliminar";
                cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
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

    }//public class familiasCL
}
