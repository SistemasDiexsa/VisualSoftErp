using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Inventarios.Clases
{
    class InventariofisicorepCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public DateTime fFecha { get; set; }
        public int intNumero { get; set; }
        public int intMarbete { get; set; }
        public int intEmp { get; set; }
        public int intUbicacionesID { get; set; }
        public int intFamiliasID { get; set; }
        public int intExistenciadelsistema { get; set; }
        public int intSuc { get; set; }
        public int intFamI { get; set; }
        public int intFamF { get; set; }
        public int intAlmacenesID { get; set; }
        public string strGenerar { get; set; }
        public int intUsuario { get; set; }
        #endregion

        #region Constructor
        public InventariofisicorepCL()
        {
            fFecha = DateTime.Now;
            intNumero = 0;
            intMarbete = 0;
            intEmp = 0;
            intUbicacionesID = 0;
            intFamiliasID = 0;
            intExistenciadelsistema = 0;
            intSuc = 0;
            intFamI = 0;
            intFamF = 0;
            intAlmacenesID = 0;
            strGenerar = string.Empty;
            intUsuario = 0;
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

        public string InventariofisicoValidar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InventarioFisicoValidar";
                cmd.Parameters.AddWithValue("@prmNumero", intNumero);
                cmd.Parameters.AddWithValue("@prmMarbete", intMarbete);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intExistenciadelsistema = Convert.ToInt32(dr["Existenciadelsistema"]);
                    
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

        public DataTable InventariosInventarioFisicoRep()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InventariosInventarioFisicoRep";
                cmd.Parameters.AddWithValue("@prmEmp", intEmp);
                cmd.Parameters.AddWithValue("@prmSuc", intSuc);
                cmd.Parameters.AddWithValue("@prmFamI", intFamI);
                cmd.Parameters.AddWithValue("@prmFamF", intFamI);
                cmd.Parameters.AddWithValue("@prmAlmacen", intAlmacenesID);
                cmd.Parameters.AddWithValue("@prmUbicacion", intUbicacionesID);
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.Parameters.AddWithValue("@prmNumero", intNumero);
                cmd.Parameters.AddWithValue("@prmMarbete", intMarbete);
                cmd.Parameters.AddWithValue("@prmGenerarArchivo", strGenerar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //inventariofisicoGrid

        public DataSet InventarioCilicoEstadistica()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InventarioCiclicoEstadistica";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(ds);
                cnn.Close();
                return ds;

            }
            catch (Exception ex) { return ds; }
        } //inventariofisicoGrid

        public string InventarioFisicoActualizaExistenciadelSistema()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InventarioFisicoActualizaMovimientosAtrasados";
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.Parameters.AddWithValue("@prmNumero", intNumero);
                cmd.Parameters.AddWithValue("@prmMarbete", intMarbete);
                cmd.Parameters.AddWithValue("@prmUsuario", intUsuario);
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
        #endregion
    }
}
