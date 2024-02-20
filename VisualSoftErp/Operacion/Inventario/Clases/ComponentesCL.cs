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
    public class ComponentesCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intArticulosID { get; set; }
        public int intComponente { get; set; }
        public decimal intCantidad { get; set; }
        public string strPresentacion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal dUltimoCosto { get; set; }
        public DataTable dtm { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        public decimal intSugerido { get; set; }
        public DateTime dFecha { get; set; }
        #endregion

        #region Constructor
        public ComponentesCL()
        {
            intArticulosID = 0;
            intComponente = 0;
            intCantidad = 0;
            strPresentacion = string.Empty;
            Fecha = DateTime.Now;
            dUltimoCosto = 0;
            dtm = null;
            intUsuarioID = 0;
            strMaquina = string.Empty;
            strPrograma = string.Empty;
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
        public DataTable ComponentesGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComponentesGrid";
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //componentesGrid

        public DataTable ComponentesGridParaAuxNacional()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComponentesAuxNacGRID";
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                cmd.Parameters.AddWithValue("@prmFecha", dFecha);
                cmd.Parameters.AddWithValue("@prmSugerido", intSugerido);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //componentesGrid

        public string ComponentesCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComponentesCRUD";
                cmd.Parameters.AddWithValue("@prmComponentes", dtm);
                cmd.Parameters.AddWithValue("@prmProductoTerminado", intArticulosID);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaquina);

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
        } //ComponentesCrud

        public string ComponentesLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComponentesllenaCajas";
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                cmd.Parameters.AddWithValue("@prmComponente", intComponente);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intArticulosID = Convert.ToInt32(dr["ArticulosID"]);
                    intComponente = Convert.ToInt32(dr["Componente"]);
                    intCantidad = Convert.ToInt32(dr["Componente"]);
                    strPresentacion = dr["Presentacion"].ToString();
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

        public string fnObtenerUltimoCosto()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select dbo.fnObtenerUltimoCosto(" + intArticulosID + ", " + "'" + Fecha + "'" + ") as UltimoCosto";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    dUltimoCosto = Convert.ToInt32(dr["UltimoCosto"]);
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

        public string ComponentesEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ComponentesEliminar";
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                cmd.Parameters.AddWithValue("@prmComponente", intComponente);
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