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
    public class ListasdeprecioCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intListasdeprecioID { get; set; }
        public string strNombre { get; set; }
        public string strMonedasID { get; set; }
        public DateTime fFechaultimaactualizacion { get; set; }
        public DataTable dtm { get; set; }
        public DataTable dtd { get; set; }
        public int intUsuarioID { get; set; }
        public string strMaquina { get; set; }
        public string strPrograma { get; set; }
        public decimal dPrecio { get; set; }
        public string strSeTomaPrecioPublico { get; set; }
        public int intArt { get; set; }
        #endregion

        #region Constructor
        public ListasdeprecioCL()
        {
            intListasdeprecioID = 0;
            strNombre = string.Empty;
            strMonedasID = string.Empty;
            fFechaultimaactualizacion = DateTime.Now;
            dPrecio = 0;
            intArt = 0;
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
        public DataTable ListasdeprecioGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "listasdeprecioGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //listasdeprecioGrid

        public DataTable ListasdeprecioLineasFamiliasGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ListasdeprecioLineasFamiliasGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception)
            {
                return dt;
            }
        } //listasdeprecioGrid
        public DataTable ListasdeprecioLineasFamiliasEditarGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ListasdeprecioLineasFamiliasEditarGrid";
                cmd.Parameters.AddWithValue("@prmLp", intListasdeprecioID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //ListasdeprecioLineasFamiliasEditarGrid

        public string ListasdeprecioCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ListasdeprecioCRUD";
                cmd.Parameters.AddWithValue("@prmListasdeprecio", dtm);
                cmd.Parameters.AddWithValue("@prmListasdepreciodetalle", dtd);
                cmd.Parameters.AddWithValue("@prmListasdeprecioID", intListasdeprecioID);
                cmd.Parameters.AddWithValue("@prmUsuario", globalCL.gv_UsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", Environment.MachineName);
                cmd.Parameters.AddWithValue("@prmPrograma", "0410");
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
        } //ListasdeprecioCrud

        public string ListasdeprecioLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ListasdepreciollenaCajas";
                cmd.Parameters.AddWithValue("@prmListasdeprecioID", intListasdeprecioID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intListasdeprecioID = Convert.ToInt32(dr["ListasdeprecioID"]);
                    strNombre = dr["Nombre"].ToString();
                    strMonedasID = dr["MonedasID"].ToString();
                    fFechaultimaactualizacion = Convert.ToDateTime(dr["Fechaultimaactualizacion"]);
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
        public string ListasdeprecioLeeunPrecio()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ListasdepreciosLeepreciodeUnArticulo";
                cmd.Parameters.AddWithValue("@prmLp", intListasdeprecioID);
                cmd.Parameters.AddWithValue("@prmArt", intArt);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    dPrecio = Convert.ToDecimal(dr["Precio"]);
                    strSeTomaPrecioPublico = dr["result"].ToString();

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



        public string ListasdeprecioEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ListasdeprecioEliminar";
                cmd.Parameters.AddWithValue("@prmListasdeprecioID", intListasdeprecioID);
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