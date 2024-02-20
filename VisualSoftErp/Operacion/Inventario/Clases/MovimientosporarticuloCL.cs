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
    class MovimientosporarticuloCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intEmp { get; set; }
        public int intSuc { get; set; }
        public int intArtID { get; set; }
        public int intAlmID { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public int intFam { get; set; }
        public int intSubFam { get; set; }
        #endregion

        #region Constructor
        public MovimientosporarticuloCL()
        {
            intEmp = 0;
            intSuc = 0;
            intArtID = 0;
            intAlmID = 0;
            FechaIni = DateTime.Now;
            FechaFin = DateTime.Now;
            intFam = 0;
            intSubFam = 0;
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
        public DataTable MovimientosporarticuloGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "InventariosMovimientosporarticulo";
                cmd.Parameters.AddWithValue("@prmEmp", intEmp);
                cmd.Parameters.AddWithValue("@prmSuc", intSuc);
                cmd.Parameters.AddWithValue("@prmArtID", intArtID);
                cmd.Parameters.AddWithValue("@prmAlmacen", intAlmID);
                cmd.Parameters.AddWithValue("@prmFechaIn", FechaIni);
                cmd.Parameters.AddWithValue("@prmFechaFin", FechaFin);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //entradasysalidasdetalleGrid

        //public string EntradasysalidasdetalleCrud()
        //{
        //    try
        //    {
        //        string result = string.Empty;
        //        SqlConnection cnn = new SqlConnection();
        //        cnn.ConnectionString = strCnn;
        //        cnn.Open();

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = "EntradasysalidasdetalleCRUD";
        //        cmd.Parameters.AddWithValue("@prmSerie", strSerie);
        //        cmd.Parameters.AddWithValue("@prmTiposdemovimientoinvID", intTiposdemovimientoinvID);
        //        cmd.Parameters.AddWithValue("@prmFolio", intFolio);
        //        cmd.Parameters.AddWithValue("@prmSeq", intSeq);
        //        cmd.Parameters.AddWithValue("@prmCantidad", intCantidad);
        //        cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Connection = cnn;
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            dr.Read();
        //            result = dr["result"].ToString();
        //        }
        //        else
        //        {
        //            result = "no read";
        //        }
        //        dr.Close();
        //        cnn.Close();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //} //EntradasysalidasdetalleCrud
        public DataTable MovimientosporarticuloEspecialGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "InventariosMovimientosporarticuloEspecial";
                cmd.Parameters.AddWithValue("@prmEmp", intEmp);
                cmd.Parameters.AddWithValue("@prmSuc", intSuc);
                cmd.Parameters.AddWithValue("@prmAlmacen", intAlmID);
                cmd.Parameters.AddWithValue("@prmArtID", intArtID);
                cmd.Parameters.AddWithValue("@prmFam", intFam);
                cmd.Parameters.AddWithValue("@prmSubFam", intSubFam);
                cmd.Parameters.AddWithValue("@prmFechaIn", FechaIni);
                cmd.Parameters.AddWithValue("@prmFechaFin", FechaFin);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //MovimientosporarticuloEspecialGrid

        #endregion
    }
}
