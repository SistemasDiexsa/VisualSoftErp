using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Clases
{
    public class combosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        //ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public int intClave;
        public string strDes;
        public string strTabla;
        public int iCondicion;
        public int iPadre;
        public int intSubFamilias;
        public int intFam;
        #endregion
        #region Constructor
        public combosCL()
        {
            intClave = 0;
            strDes = string.Empty;
            strTabla = string.Empty;
            iCondicion = 0;
            iPadre = 0;
            intSubFamilias = 0;
            intFam = 0;
        }
        #endregion
        #region Metodos
        public DataTable CargaCombos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = globalCL.gv_strcnn; //strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CargarCombos";
                cmd.Parameters.AddWithValue("@prmTabla", strTabla);
                cmd.Parameters.AddWithValue("@prmCondicion", iCondicion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);

                cnn.Close();

                return dt;

            }
            catch (Exception ex)
            {
                return dt;
                
            }


        }//CargarCombos()

        public DataTable CargaCombosSepomex()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = globalCL.gv_strcnn; //strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppSepomexCombos";
                cmd.Parameters.AddWithValue("@prmTabla", strTabla);
                cmd.Parameters.AddWithValue("@prmPadre", iPadre);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);

                cnn.Close();

                return dt;

            }
            catch (Exception ex)
            {
                return dt;

            }


        }//CargarCombosSepomex()


        public LookUpEdit ActualizaCombo(LookUpEdit cbo,string tabla,string sNullText)
        {
            combosCL cl = new combosCL();
            cbo.Properties.ValueMember = "Clave";
            cbo.Properties.DisplayMember = "Des";
            cl.strTabla = tabla;
            cbo.Properties.DataSource = cl.CargaCombos();
            cbo.Properties.ForceInitialize();
            cbo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cbo.Properties.ForceInitialize();
            cbo.Properties.PopulateColumns();
            cbo.Properties.Columns["Clave"].Visible = false;
            if (sNullText.Length>0)
            {
                cbo.Properties.NullText = sNullText;
            }
            else
            {
                cbo.ItemIndex = 0;
            }

            

            return cbo;
        }

        public DataTable CargaCombosCondicion()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CargarCombos";
                cmd.Parameters.AddWithValue("@prmTabla", strTabla);
                cmd.Parameters.AddWithValue("@prmCondicion", intClave);
                cmd.Parameters.AddWithValue("@prmSubFam", intSubFamilias);
                cmd.Parameters.AddWithValue("@prmFam", intFam);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);

                cnn.Close();

                return dt;

            }
            catch (Exception ex)
            {
                return dt;

            }


        }//CargarCombosCondicion()

        

        public DataTable CargaCombosCondicionSepomex()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppSepomexCombos";
                cmd.Parameters.AddWithValue("@prmTabla", strTabla);
                cmd.Parameters.AddWithValue("@prmPadre", intClave);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);

                cnn.Close();

                return dt;

            }
            catch (Exception ex)
            {
                return dt;

            }


        }//CargaCombosCondicionSepomex()



        #endregion
    }
}
