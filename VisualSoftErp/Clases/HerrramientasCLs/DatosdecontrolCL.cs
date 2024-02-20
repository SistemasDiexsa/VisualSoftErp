using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Clases.HerrramientasCLs
{
    public class DatosdecontrolCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int dbr;
        public int dbg;
        public int dbb;
        public int dfr;
        public int dfg;
        public int dfb;
        public int gbr;
        public int gbg;
        public int gbb;
        public int gfr;
        public int gfg;
        public int gfb;
        public string sTipologo;
        public int iEnvioCfdiAuto;
        public int iAbrirOutlook;
        public int iVistapreviacfdi;
        public int iManejarultimosprecios;
        public int iPermitirexistencianegativa;
        public int iManejarlistasdeprecios;
        public int iPermitirmodificarprecioenpedidos;
        public int iPermitirmodificarprecioenremisiones;
        public int iPermitirmodificarprecioenfacturas;
        public int iManejariva;
        public int iManejarretiva;
        public int iManejarretisr;
        public decimal dPorcentajedeiva;
        public decimal dPorcentajederetiva;
        public decimal dPorcentajederetisr;
        public int iManejarieps;
        public int iEmbarcara;
        public int iFacturara;
        public string sAtenciona;
        public string sCorrespondenciaa;
        public int iOCUltimoprecio;
        public int intDepositoschequerabeneficiarioID;
        public int iCambiarelagentealvender;
        public decimal decMargenarribadelcosto;
        public string strFormulaMargen;
        public int iValidarcreditocliente;
        public int iValidarbajocosto;
        public int iImpresionDirecta;
        public int iCorreoEncaBackColorR;
        public int iCorreoEncaBackColorG;
        public int iCorreoEncaBackColorB;
        public int iCorreoEncaForeColorR;
        public int iCorreoEncaForeColorG;
        public int iCorreoEncaForeColorB;
        public int iCorreoPieBackColorR;
        public int iCorreoPieBackColorG;
        public int iCorreoPieBackColorB;
        public int iCorreoPieForeColorR;
        public int iCorreoPieForeColorG;
        public int iCorreoPieForeColorB;
        public decimal decOCmontominio;

        //       @prmDBR as int,
        //@prmDBG as int,
        //@prmDBB as int,
        //@prmDFR as int,
        //@prmDFG as int,
        //@prmDFB as int,
        //@prmGBR as int,
        //@prmGBG as int,
        //@prmGBB as int,
        //@prmGFR as int,
        //@prmGFG as int,
        //@prmGFB as int
        #endregion
        #region Constructor
        public DatosdecontrolCL()
        {
            dbr = 0;
            dbg = 0;
            dbb = 0;
            dfr = 0;
            dfg = 0;
            dfb = 0;
            gbr = 0;
            gbg = 0;
            gbb = 0;
            gfr = 0;
            gfg = 0;
            gfb = 0;
            iEnvioCfdiAuto = 0;
            iAbrirOutlook = 0;
            iVistapreviacfdi = 0;

            iManejarultimosprecios=0;
            iManejarlistasdeprecios = 0;
            iPermitirmodificarprecioenpedidos = 0;
            iPermitirmodificarprecioenremisiones = 0;
            iPermitirmodificarprecioenfacturas = 0;
            iManejariva = 0;
            iManejarretiva = 0;
            iManejarretisr = 0;
            dPorcentajedeiva = 0;
            dPorcentajederetiva = 0;
            dPorcentajederetisr = 0;
            iManejarieps = 0;
            iPermitirexistencianegativa = 0;

            sTipologo = string.Empty;

            iEmbarcara = 0;
            iFacturara = 0;
            sAtenciona = string.Empty;
            sCorrespondenciaa = string.Empty;
            iOCUltimoprecio = 0;

            iCambiarelagentealvender = 0;
            decMargenarribadelcosto = 0;
            strFormulaMargen = string.Empty;
            iValidarcreditocliente = 0;
            iValidarbajocosto = 0;

            iValidarbajocosto=0;
            iCorreoEncaBackColorR = 0;
            iCorreoEncaBackColorG = 0;
            iCorreoEncaBackColorB = 0;
            iCorreoEncaForeColorR = 0;
            iCorreoEncaForeColorG = 0;
            iCorreoEncaForeColorB = 0;
            iCorreoPieBackColorR = 0;
            iCorreoPieBackColorG = 0;
            iCorreoPieBackColorB = 0;
            iCorreoPieForeColorR=0;
            iCorreoPieForeColorG = 0;
            iCorreoPieForeColorB = 0;
            decOCmontominio = 0;

        }
        #endregion
        #region Metodos
        public string DatosdecontrolColoresGuardar()
        {
            try
            {
                string result = string.Empty;                

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DatosdecontrolColoresCrud";
                cmd.Parameters.AddWithValue("@prmDBR", dbr);
                cmd.Parameters.AddWithValue("@prmDBG", dbg);
                cmd.Parameters.AddWithValue("@prmDBB", dbb);
                cmd.Parameters.AddWithValue("@prmDFR", dfr);
                cmd.Parameters.AddWithValue("@prmDFG", dfg);
                cmd.Parameters.AddWithValue("@prmDFB", dfb);
                cmd.Parameters.AddWithValue("@prmGBR", gbr);
                cmd.Parameters.AddWithValue("@prmGBG", gbg);
                cmd.Parameters.AddWithValue("@prmGBB", gbb);
                cmd.Parameters.AddWithValue("@prmGFR", gfr);
                cmd.Parameters.AddWithValue("@prmGFG", gfg);
                cmd.Parameters.AddWithValue("@prmGFB", gfb);
                cmd.Parameters.AddWithValue("@prmTipologo", sTipologo);
                cmd.Parameters.AddWithValue("@prmEnvioCfdiAut", iEnvioCfdiAuto);
                cmd.Parameters.AddWithValue("@prmAbrirOutlook", iAbrirOutlook);
                cmd.Parameters.AddWithValue("@prmVistapreviacfdi", iVistapreviacfdi);
                cmd.Parameters.AddWithValue("@prmManejarultimosprecios", iManejarultimosprecios);
                cmd.Parameters.AddWithValue("@prmManejarlistasdeprecios", iManejarlistasdeprecios);
                cmd.Parameters.AddWithValue("@prmPermitirmodificarprecioenpedidos", iPermitirmodificarprecioenpedidos);
                cmd.Parameters.AddWithValue("@prmPermitirmodificarprecioenremisiones", iPermitirmodificarprecioenremisiones);
                cmd.Parameters.AddWithValue("@prmPermitirmodificarprecioenfacturas", iPermitirmodificarprecioenfacturas);
                cmd.Parameters.AddWithValue("@prmManejariva", iManejariva);
                cmd.Parameters.AddWithValue("@prmManejarretiva", iManejarretiva);
                cmd.Parameters.AddWithValue("@prmManejarretisr", iManejarretisr);
                cmd.Parameters.AddWithValue("@prmPorcentajedeiva", dPorcentajedeiva);
                cmd.Parameters.AddWithValue("@prmPorcentajederetiva", dPorcentajederetiva);
                cmd.Parameters.AddWithValue("@prmPorcentajederetisr", dPorcentajederetisr);
                cmd.Parameters.AddWithValue("@prmManejarieps", iManejarieps);
                cmd.Parameters.AddWithValue("@prmEmbarcara", iEmbarcara);
                cmd.Parameters.AddWithValue("@prmFacturara", iFacturara);
                cmd.Parameters.AddWithValue("@prmAtenciona", sAtenciona);
                cmd.Parameters.AddWithValue("@prmCorrespondenciaa", sCorrespondenciaa);
                cmd.Parameters.AddWithValue("@prmOCUltimoPrecio", iOCUltimoprecio);
                cmd.Parameters.AddWithValue("@prmDepositoschequerabeneficiarioID", intDepositoschequerabeneficiarioID);
                cmd.Parameters.AddWithValue("@prmCambiarelagentealvender", iCambiarelagentealvender);
                cmd.Parameters.AddWithValue("@prmPermitirexistencianegativa", iPermitirexistencianegativa);
                cmd.Parameters.AddWithValue("@prmMargenarribadelcosto", decMargenarribadelcosto);
                cmd.Parameters.AddWithValue("@prmFormulaMargen", strFormulaMargen);
                cmd.Parameters.AddWithValue("@prmValidarcreditocliente", iValidarcreditocliente);
                cmd.Parameters.AddWithValue("@prmValidarbajocosto", iValidarcreditocliente);
                cmd.Parameters.AddWithValue("@prmCorreoEncaBackColorR", iCorreoEncaBackColorR);
                cmd.Parameters.AddWithValue("@prmCorreoEncaBackColorG", iCorreoEncaBackColorG);
                cmd.Parameters.AddWithValue("@prmCorreoEncaBackColorB", iCorreoEncaBackColorB);
                cmd.Parameters.AddWithValue("@prmCorreoEncaForeColorR", iCorreoEncaForeColorR);
                cmd.Parameters.AddWithValue("@prmCorreoEncaForeColorG", iCorreoEncaForeColorG);
                cmd.Parameters.AddWithValue("@prmCorreoEncaForeColorB", iCorreoEncaForeColorB);
                cmd.Parameters.AddWithValue("@prmCorreoPieBackColorR", iCorreoPieBackColorR);
                cmd.Parameters.AddWithValue("@prmCorreoPieBackColorG", iCorreoPieBackColorG);
                cmd.Parameters.AddWithValue("@prmCorreoPieBackColorB", iCorreoPieBackColorB);
                cmd.Parameters.AddWithValue("@prmCorreoPieForeColorR", iCorreoPieForeColorR);
                cmd.Parameters.AddWithValue("@prmCorreoPieForeColorG", iCorreoPieForeColorG);
                cmd.Parameters.AddWithValue("@prmCorreoPieForeColorB", iCorreoPieForeColorB);
                cmd.Parameters.AddWithValue("@prmImpresionDirecta", iImpresionDirecta);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();

                    result = dr["Result"].ToString();

                }
                else
                {
                    result = "NO READ";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // public class DatosdecontrolColores
        public string DatosdecontrolLeer()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DatosdecontrolLeer";               
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    dbr = Convert.ToInt32(dr["DetalleBackR"]);
                    dbg = Convert.ToInt32(dr["DetalleBackG"]);
                    dbb = Convert.ToInt32(dr["DetalleBackB"]);
                    dfr = Convert.ToInt32(dr["DetalleForeR"]);
                    dfg = Convert.ToInt32(dr["DetalleForeG"]);
                    dfb = Convert.ToInt32(dr["DetalleForeB"]);

                    gbr = Convert.ToInt32(dr["GruposBackR"]);
                    gbg = Convert.ToInt32(dr["GruposBackG"]);
                    gbb = Convert.ToInt32(dr["GruposBackB"]);
                    gfr = Convert.ToInt32(dr["GruposForeR"]);
                    gfg = Convert.ToInt32(dr["GruposForeG"]);
                    gfb = Convert.ToInt32(dr["GruposForeB"]);

                    sTipologo = dr["Tipologo"].ToString();
                    iEnvioCfdiAuto = Convert.ToInt32(dr["Correodecfdiautomatico"]);
                    iAbrirOutlook = Convert.ToInt32(dr["AbrirOutlook"]);
                    iVistapreviacfdi = Convert.ToInt32(dr["Vistapreviacfdi"]);
                    iManejarultimosprecios = Convert.ToInt32(dr["Manejarultimosprecios"]);
                    iManejarlistasdeprecios = Convert.ToInt32(dr["Manejarlistasdeprecios"]);
                    iPermitirmodificarprecioenpedidos = Convert.ToInt32(dr["Permitirmodificarprecioenpedidos"]);
                    iPermitirmodificarprecioenremisiones = Convert.ToInt32(dr["Permitirmodificarprecioenremisiones"]);
                    iPermitirmodificarprecioenfacturas = Convert.ToInt32(dr["Permitirmodificarprecioenfacturas"]);
                    iManejariva = Convert.ToInt32(dr["Manejariva"]);
                    iManejarretiva = Convert.ToInt32(dr["Manejarretiva"]);
                    iManejarretisr = Convert.ToInt32(dr["Manejarretisr"]);
                    dPorcentajedeiva = Convert.ToInt32(dr["Porcentajedeiva"]);
                    dPorcentajederetiva = Convert.ToInt32(dr["Porcentajederetiva"]);
                    dPorcentajederetisr = Convert.ToInt32(dr["Porcentajederetisr"]);
                    iManejarieps = Convert.ToInt32(dr["Manejarieps"]);
                    iEmbarcara = Convert.ToInt32(dr["Embarcara"]);
                    iFacturara = Convert.ToInt32(dr["Facturara"]);
                    sAtenciona = dr["Atenciona"].ToString();
                    sCorrespondenciaa = dr["Correspondenciaa"].ToString();
                    iOCUltimoprecio= Convert.ToInt32(dr["UltimospreciosOC"]);
                    intDepositoschequerabeneficiarioID = Convert.ToInt32(dr["DepositoschequerabeneficiarioID"]);
                    iCambiarelagentealvender = Convert.ToInt32(dr["Cambiarelagentealvender"]);
                    iPermitirexistencianegativa = Convert.ToInt32(dr["Permitirexistencianegativa"]);
                    decMargenarribadelcosto = Convert.ToDecimal(dr["Margenarribadelcosto"]);
                    strFormulaMargen = dr["Formulamargenarribadelcosto"].ToString();
                    iValidarcreditocliente = Convert.ToInt32(dr["Validarcreditocliente"]);
                    iValidarbajocosto = Convert.ToInt32(dr["Validarbajocosto"]);
                    iCorreoEncaBackColorR = Convert.ToInt32(dr["CorreoEncaBackColorR"]);
                    iCorreoEncaBackColorG = Convert.ToInt32(dr["CorreoEncaBackColorG"]);
                    iCorreoEncaBackColorB = Convert.ToInt32(dr["CorreoEncaBackColorB"]);
                    iCorreoEncaForeColorR = Convert.ToInt32(dr["CorreoEncaForeColorR"]);
                    iCorreoEncaForeColorG = Convert.ToInt32(dr["CorreoEncaForeColorG"]);
                    iCorreoEncaForeColorB = Convert.ToInt32(dr["CorreoEncaForeColorB"]);
                    iCorreoPieBackColorR = Convert.ToInt32(dr["CorreoPieBackColorR"]);
                    iCorreoPieBackColorG = Convert.ToInt32(dr["CorreoPieBackColorG"]);
                    iCorreoPieBackColorB = Convert.ToInt32(dr["CorreoPieBackColorB"]);
                    iCorreoPieForeColorR = Convert.ToInt32(dr["CorreoPieForeColorR"]);
                    iCorreoPieForeColorG = Convert.ToInt32(dr["CorreoPieForeColorG"]);
                    iCorreoPieForeColorB = Convert.ToInt32(dr["CorreoPieForeColorB"]);
                    decOCmontominio = Convert.ToDecimal(dr["Ordenesdecompramontominimo"]);
                    iImpresionDirecta = Convert.ToInt32(dr["ImpresionDirecta"]);

                    result = "OK";

                }
                else
                {
                    result = "NO READ";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // public class DatosdecontrolColores
        #endregion
    }
}
