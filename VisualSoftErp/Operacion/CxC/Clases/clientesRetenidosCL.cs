using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.CxC.Clases
{
    public class clientesRetenidosCL
    {
        #region Propiedades
        string strCnn = ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public int intAñoAnt { get; set; }
        public int intAñoAct { get; set; }
        public string strMesesAnt { get; set; }
        public string strMesesNvo { get; set; }
        public string strTipos { get; set; }
        public string strCanales { get; set; }
        public DateTime dFecha { get; set; }
        public int intCteIni { get; set; }
        public int intCteFin { get; set; }
        public int intAgeIni { get; set; }
        public int intAgeFin { get; set; }
        public int intNivel { get; set; }


        #endregion
        #region Constructor
        public clientesRetenidosCL() { }
        #endregion
        #region Metódos
        public DataTable clientesRetenidos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ClientesRetenidos";
                cmd.Parameters.AddWithValue("@prmAñoAnt", intAñoAnt);
                cmd.Parameters.AddWithValue("@prmAñoAct", intAñoAct);
                cmd.Parameters.AddWithValue("@prmMesesAnt", strMesesAnt);
                cmd.Parameters.AddWithValue("@prmMesesNvo", strMesesNvo);
                cmd.Parameters.AddWithValue("@prmCanales", strCanales);
                cmd.Parameters.AddWithValue("@prmTipos", strTipos);
                cmd.Parameters.AddWithValue("@prmUsuario", globalCL.gv_UsuarioID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //anticiposcxpGrid

        public DataTable carteraVencidaGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CxCCarteraVencidaRep";
                cmd.Parameters.AddWithValue("@prmEmp", 0);
                cmd.Parameters.AddWithValue("@prmSuc", 0);
                cmd.Parameters.AddWithValue("@prmFechacorte", dFecha);
                cmd.Parameters.AddWithValue("@prmClienteIn", intCteIni);
                cmd.Parameters.AddWithValue("@prmClienteFin", intCteFin);
                cmd.Parameters.AddWithValue("@prmAgenteIn", intAgeIni);
                cmd.Parameters.AddWithValue("@prmAgenteFin", intAgeFin);
                cmd.Parameters.AddWithValue("@prmNivelinfo", intNivel);
                cmd.Parameters.AddWithValue("@prmDummy", string.Empty);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout= 1000;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) {
                string x = ex.Message;
                return dt; }
        } //anticiposcxpGrid
        #endregion
    }
}
