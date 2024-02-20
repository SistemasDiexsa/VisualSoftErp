using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Inventarios.Clases
{
    class InventariofisicoregistrarconteoCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public DateTime fFecha { get; set; }
        public int intNumero { get; set; }
        public int intMarbete { get; set; }
        public int intArticulosID { get; set; }
        public int intUbicacionesID { get; set; }
        public int intFamiliasID { get; set; }
        public decimal intExistenciadelsistema { get; set; }
        public decimal intConteofisico { get; set; }
        public int intAlmacenesID { get; set; }
        public DateTime fFechaReal { get; set; }
        public int intUsuariosID { get; set; }
        public int intStatus { get; set; }
        public DataTable dtd { get; set; }
        #endregion

        #region Constructor
        public InventariofisicoregistrarconteoCL()
        {
            fFecha = DateTime.Now;
            intNumero = 0;
            intMarbete = 0;
            intArticulosID = 0;
            intUbicacionesID = 0;
            intFamiliasID = 0;
            intExistenciadelsistema = 0;
            intConteofisico = 0;
            intAlmacenesID = 0;
            fFechaReal = DateTime.Now;
            intUsuariosID = 0;
            intStatus = 0;
            dtd = null;
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
            catch (Exception)
            {
                return "";
            }
        }
        public DataTable InventariofisicoconteoGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InventariofisicoconteoGrid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //inventariofisicoGrid

        public DataTable InventarioFisicoListarpararegistrarconteo()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@prmFecha", fFecha);
                cmd.Parameters.AddWithValue("@prmNumero", intNumero);
                cmd.Parameters.AddWithValue("@prmMarbete", intMarbete);
                cmd.Parameters.AddWithValue("@prmArtID", intArticulosID);
                cmd.CommandText = "InventarioFisicoListarpararegistrarconteo";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //inventariofisicoGrid

        public string InventariofisicoconteoCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InventariofisicoconteoCrud";
                cmd.Parameters.AddWithValue("@prmInvFis", dtd);            
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
        } //InventariofisicoCrud


        #endregion
    }
}
