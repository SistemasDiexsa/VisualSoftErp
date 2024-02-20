using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VisualSoftErp.Clases.HerrramientasCLs;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class Opciones : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Opciones()
        {
            InitializeComponent();
           

            LlenaCombos();
            LLenaCajas();


            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }
        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }

        public class opcionesTipoLogo
        {
            public string Clave { get; set; }
            public string Des { get; set; }
        }
        public class opcionesFormula
        {
            public string Clave { get; set; }
            public string Des { get; set; }
        }

        private void LlenaCombos()
        {
            combosCL cl = new combosCL();

            List<opcionesTipoLogo> Tipologo = new List<opcionesTipoLogo>();
            Tipologo.Add(new opcionesTipoLogo() { Clave = "C", Des = "Cuadrado / redondo" });
            Tipologo.Add(new opcionesTipoLogo() { Clave = "L", Des = "Alargado" });

            List<opcionesFormula> Formula = new List<opcionesFormula>();
            Formula.Add(new opcionesFormula() { Clave = "D", Des = "Dividir" });
            Formula.Add(new opcionesFormula() { Clave = "M", Des = "Multiplicar" });

            cboTipologo.Properties.ValueMember = "Clave";
            cboTipologo.Properties.DisplayMember = "Des";
            cboTipologo.Properties.DataSource = Tipologo;
            cboTipologo.Properties.ForceInitialize();
            cboTipologo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTipologo.Properties.PopulateColumns();
            cboTipologo.Properties.Columns["Clave"].Visible = false;

            cl.strTabla = "Proveedores";
            cboFacturara.Properties.ValueMember = "Clave";
            cboFacturara.Properties.DisplayMember = "Des";
            cboFacturara.Properties.DataSource = cl.CargaCombos();
            cboFacturara.Properties.ForceInitialize();
            cboFacturara.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFacturara.Properties.PopulateColumns();
            cboFacturara.Properties.Columns["Clave"].Visible = false;
            cboFacturara.ItemIndex = 0;

            cl.strTabla = "Proveedores";
            cboEmbarcara.Properties.ValueMember = "Clave";
            cboEmbarcara.Properties.DisplayMember = "Des";
            cboEmbarcara.Properties.DataSource = cl.CargaCombos();
            cboEmbarcara.Properties.ForceInitialize();
            cboEmbarcara.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboEmbarcara.Properties.PopulateColumns();
            cboEmbarcara.Properties.Columns["Clave"].Visible = false;
            cboEmbarcara.ItemIndex = 0;

            cl.strTabla = "cyb_Chequeras";
            cboCuentasbancariaID.Properties.ValueMember = "Clave";
            cboCuentasbancariaID.Properties.DisplayMember = "Des";
            cboCuentasbancariaID.Properties.DataSource = cl.CargaCombos();
            cboCuentasbancariaID.Properties.ForceInitialize();
            cboCuentasbancariaID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCuentasbancariaID.Properties.PopulateColumns();
            cboCuentasbancariaID.Properties.Columns["Clave"].Visible = false;
            cboCuentasbancariaID.Properties.Columns["MonedasID"].Visible = false;
            cboCuentasbancariaID.ItemIndex = 0;

            cboFormulamargen.Properties.ValueMember = "Clave";
            cboFormulamargen.Properties.DisplayMember = "Des";
            cboFormulamargen.Properties.DataSource = Formula;
            cboFormulamargen.Properties.ForceInitialize();
            cboFormulamargen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFormulamargen.Properties.PopulateColumns();
            cboFormulamargen.Properties.Columns["Clave"].Visible = false;
        }


        private void LLenaCajas()
        {
            DatosdecontrolCL cl = new DatosdecontrolCL();
            string result = cl.DatosdecontrolLeer();
            if (result=="OK")
            {
                txtDBR.Text = cl.dbr.ToString();
                txtDBG.Text = cl.dbg.ToString();
                txtDBB.Text = cl.dbb.ToString();
                txtDFR.Text = cl.dfr.ToString();
                txtDFG.Text = cl.dfg.ToString();
                txtDFB.Text = cl.dfb.ToString();

                colorPickEditDetalleBack.Color= Color.FromArgb(cl.dbr, cl.dbg, cl.dbb);
                colorPickEditDetalleFore.Color = Color.FromArgb(cl.dfr, cl.dfg,cl.dfb);

                //Grupos
                txtGBR.Text = cl.gbr.ToString();
                txtGBG.Text = cl.gbg.ToString();
                txtGBB.Text = cl.gbb.ToString();
                txtGFR.Text = cl.gfr.ToString();
                txtGFG.Text = cl.gfg.ToString();
                txtGFB.Text = cl.gfb.ToString();

                colorPickEditGrupoBack.Color = Color.FromArgb(cl.gbr, cl.gbg, cl.gbb);
                colorPickEditGrupoFore.Color = Color.FromArgb(cl.gfr, cl.gfg, cl.gfb);

                //Mailing encabezado
                txtMEBR.Text = cl.iCorreoEncaBackColorR.ToString();
                txtMEBG.Text = cl.iCorreoEncaBackColorG.ToString();
                txtMEBB.Text = cl.iCorreoEncaBackColorB.ToString();
                txtMEFR.Text = cl.iCorreoEncaForeColorR.ToString();
                txtMEFG.Text = cl.iCorreoEncaForeColorG.ToString();
                txtMEFB.Text = cl.iCorreoEncaForeColorB.ToString();
                txtMPBR.Text = cl.iCorreoPieBackColorR.ToString();
                txtMPBG.Text = cl.iCorreoPieBackColorG.ToString();
                txtMPBB.Text = cl.iCorreoPieBackColorB.ToString();
                txtMPFR.Text = cl.iCorreoPieForeColorR.ToString();
                txtMPFG.Text = cl.iCorreoPieForeColorG.ToString();
                txtMPFB.Text = cl.iCorreoPieForeColorB.ToString();

                colorPickEditMEB.Color = Color.FromArgb(cl.iCorreoEncaBackColorR, cl.iCorreoEncaBackColorG, cl.iCorreoEncaBackColorB);
                colorPickEditMEF.Color = Color.FromArgb(cl.iCorreoEncaForeColorR, cl.iCorreoEncaForeColorG, cl.iCorreoEncaForeColorB);
                colorPickEditMPB.Color = Color.FromArgb(cl.iCorreoPieBackColorR, cl.iCorreoPieBackColorG, cl.iCorreoPieBackColorB);
                colorPickEditMPF.Color = Color.FromArgb(cl.iCorreoPieForeColorR, cl.iCorreoPieForeColorG, cl.iCorreoPieForeColorB);

                cboTipologo.EditValue = cl.sTipologo;

                toggleSwitchEnvioAutomatico.IsOn = cl.iEnvioCfdiAuto == 1 ? true : false;
                toggleSwitchAbrirOutlook.IsOn = cl.iAbrirOutlook == 1 ? true : false;
                toggleSwitchVistaPreliminar.IsOn = cl.iVistapreviacfdi == 1 ? true : false;
                swManejarultimosprecios.IsOn = cl.iManejarultimosprecios == 1 ? true : false;
                swPermitirexistencianegativa.IsOn = cl.iPermitirexistencianegativa == 1 ? true : false;
                swManejarlistasdeprecios.IsOn = cl.iManejarlistasdeprecios == 1 ? true : false;
                swPermitircambiarpreciosenpedidos.IsOn = cl.iPermitirmodificarprecioenpedidos == 1 ? true : false;
                swPermitircambiarpreciosenremisiones.IsOn = cl.iPermitirmodificarprecioenremisiones == 1 ? true : false;
                swPermitircambiarpreciosenfacturas.IsOn = cl.iPermitirmodificarprecioenfacturas == 1 ? true : false;
                swManejariva.IsOn = cl.iManejariva == 1 ? true : false;
                swManejarretenciondeiva.IsOn = cl.iManejarretiva == 1 ? true : false;
                swmanejarretenciondeisr.IsOn = cl.iManejarretisr == 1 ? true : false;
                txtPorcentajedeiva.Text = cl.dPorcentajedeiva.ToString();
                txtPorcentajederetenciondeiva.Text = cl.dPorcentajederetiva.ToString();
                txtPorcentajederetenciondeisr.Text = cl.dPorcentajederetisr.ToString();
                swManejarieps.IsOn = cl.iManejarieps == 1 ? true : false;
                cboEmbarcara.EditValue = cl.iEmbarcara;
                cboFacturara.EditValue = cl.iFacturara;
                txtAtenciona.Text = cl.sAtenciona;
                txtCorrespondenciaa.Text = cl.sCorrespondenciaa;
                swOCUltimoprecio.IsOn = cl.iOCUltimoprecio == 1 ? true : false;
                cboCuentasbancariaID.EditValue = cl.intDepositoschequerabeneficiarioID;
                swCambiarelagentealvender.IsOn = cl.iCambiarelagentealvender == 1 ? true : false;
                txtMargenarribadelcosto.Text = cl.decMargenarribadelcosto.ToString();
                cboFormulamargen.EditValue = cl.strFormulaMargen;
                swValidarcreditocliente.IsOn = cl.iValidarcreditocliente == 1 ? true : false;
                swValidarbajocosto.IsOn = cl.iValidarbajocosto == 1 ? true : false;
                toggleSwitchImpDir.IsOn = cl.iImpresionDirecta == 1 ? true : false;

            } else
            {
                MessageBox.Show("Llenacajas: " + result);
            }
        }

        

     
        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private string valida()
        {
            try
            {

                if (txtDBR.Text.Length==0)
                {
                    txtDBR.Text = "0";
                }
                if (txtDBG.Text.Length == 0)
                {
                    txtDBG.Text = "0";
                }
                if (txtDBB.Text.Length == 0)
                {
                    txtDBB.Text = "0";
                }
                if (txtDFR.Text.Length == 0)
                {
                    txtDFR.Text = "0";
                }
                if (txtDFG.Text.Length == 0)
                {
                    txtDFG.Text = "0";
                }
                if (txtDFB.Text.Length == 0)
                {
                    txtDFB.Text = "0";
                }

                //grupo
                if (txtGBR.Text.Length == 0)
                {
                    txtGBR.Text = "0";
                }
                if (txtGBG.Text.Length == 0)
                {
                    txtGBG.Text = "0";
                }
                if (txtGBB.Text.Length == 0)
                {
                    txtGBB.Text = "0";
                }
                if (txtGFR.Text.Length == 0)
                {
                    txtGFR.Text = "0";
                }
                if (txtGFG.Text.Length == 0)
                {
                    txtGFG.Text = "0";
                }
                if (txtGFB.Text.Length == 0)
                {
                    txtGFB.Text = "0";
                }



                if (cboTipologo.ItemIndex==-1)
                {
                    return "Seleccion el tipo de logotipo";
                }

                globalCL cl = new globalCL();
                if (swManejariva.IsOn)
                {
                    decimal piva = Convert.ToDecimal(txtPorcentajedeiva.Text);

                    if (cl.esNumerico(txtPorcentajedeiva.Text))
                    {
                        if (piva != 16 && piva != 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("El % de iva parece incorrecto, continua?", "% de Iva", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.No)
                            {
                                return "% de iva incorrecto";
                            }
                        }
                    }
                    else
                    {
                        return "Teclee el % de iva";
                    }
                }
                else
                {
                    txtPorcentajedeiva.Text = "0";
                }

                if (swManejarretenciondeiva.IsOn)
                {
                    if (cl.esNumerico(txtPorcentajederetenciondeiva.Text))
                    {
                        if (Convert.ToDecimal(txtPorcentajederetenciondeiva.Text) != 4 && Convert.ToDecimal(txtPorcentajederetenciondeiva.Text) != 6)
                        {
                            DialogResult dialogResult = MessageBox.Show("El % de retención de iva parece incorrecto, continua?", "% de Iva", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.No)
                            {
                                return "% de retención de iva incorrecto";
                            }
                        }
                    }
                    else
                    {
                        return "Teclee el % de retención de iva";
                    }
                }
                else
                {
                    txtPorcentajederetenciondeiva.Text = "0";
                }

                if (swmanejarretenciondeisr.IsOn)
                {
                    if (cl.esNumerico(txtPorcentajederetenciondeisr.Text))
                    {
                        if (Convert.ToDecimal(txtPorcentajederetenciondeisr.Text) != 10)
                        {
                            DialogResult dialogResult = MessageBox.Show("El % de retención de isr parece incorrecto, continua?", "% de Iva", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.No)
                            {
                                return "% de retención de isr incorrecto";
                            }
                        }
                    }
                    else
                    {
                        return "Teclee el % de retención de isr";
                    }
                }
                else
                {
                    txtPorcentajederetenciondeisr.Text = "0";
                }

                if (!cl.esNumerico(txtMargenarribadelcosto.Text))
                {
                    txtMargenarribadelcosto.Text = "0";
                }


                return "OK";
            }
            catch (Exception ex)
            {
                return "Valida: " + ex.Message;
            }
        }

        private void Guardar()
        {
            try
            {
                string result = valida();
                if (result != "OK")
                {
                    MessageBox.Show(result);
                    return;
                }

                DatosdecontrolCL cl = new DatosdecontrolCL();
                cl.dbr = Convert.ToInt32(txtDBR.Text);
                cl.dbg = Convert.ToInt32(txtDBG.Text);
                cl.dbb = Convert.ToInt32(txtDBB.Text);
                cl.dfr = Convert.ToInt32(txtDFR.Text);
                cl.dfg = Convert.ToInt32(txtDFG.Text);
                cl.dfb = Convert.ToInt32(txtDFB.Text);

                //grupos
                cl.gbr = Convert.ToInt32(txtGBR.Text);
                cl.gbg = Convert.ToInt32(txtGBG.Text);
                cl.gbb = Convert.ToInt32(txtGBB.Text);
                cl.gfr = Convert.ToInt32(txtGFR.Text);
                cl.gfg = Convert.ToInt32(txtGFG.Text);
                cl.gfb = Convert.ToInt32(txtGFB.Text);

                //Mailinig
                cl.iCorreoEncaBackColorR = Convert.ToInt32(txtMEBR.Text); 
                cl.iCorreoEncaBackColorG = Convert.ToInt32(txtMEBG.Text);
                cl.iCorreoEncaBackColorB = Convert.ToInt32(txtMEBB.Text);
                cl.iCorreoEncaForeColorR = Convert.ToInt32(txtMEFR.Text);
                cl.iCorreoEncaForeColorG = Convert.ToInt32(txtMEFG.Text);
                cl.iCorreoEncaForeColorB = Convert.ToInt32(txtMEFB.Text);

                cl.iCorreoPieBackColorR = Convert.ToInt32(txtMPBR.Text);
                cl.iCorreoPieBackColorG = Convert.ToInt32(txtMPBG.Text);
                cl.iCorreoPieBackColorB = Convert.ToInt32(txtMPBB.Text);
                cl.iCorreoPieForeColorR = Convert.ToInt32(txtMPFR.Text);
                cl.iCorreoPieForeColorG = Convert.ToInt32(txtMPFG.Text);
                cl.iCorreoPieForeColorB = Convert.ToInt32(txtMPFB.Text);

                cl.sTipologo = cboTipologo.EditValue.ToString();
                cl.iEnvioCfdiAuto = toggleSwitchEnvioAutomatico.IsOn ? 1 : 0;
                cl.iAbrirOutlook = toggleSwitchAbrirOutlook.IsOn ? 1: 0;
                cl.iVistapreviacfdi = toggleSwitchVistaPreliminar.IsOn ? 1 : 0;
                cl.iManejarultimosprecios = swManejarultimosprecios.IsOn ? 1 : 0;
                cl.iManejarlistasdeprecios = swManejarlistasdeprecios.IsOn ? 1 : 0;
                cl.iPermitirmodificarprecioenpedidos = swPermitircambiarpreciosenpedidos.IsOn ? 1 : 0;
                cl.iPermitirmodificarprecioenremisiones = swPermitircambiarpreciosenremisiones.IsOn ? 1 : 0;
                cl.iPermitirmodificarprecioenfacturas = swPermitircambiarpreciosenfacturas.IsOn ? 1 : 0;
                cl.iManejariva = swManejariva.IsOn ? 1 : 0;
                cl.iManejarretiva = swManejarretenciondeiva.IsOn ? 1 : 0;
                cl.iManejarretisr = swmanejarretenciondeisr.IsOn ? 1 : 0;
                cl.dPorcentajedeiva = Convert.ToDecimal(txtPorcentajedeiva.Text);
                cl.dPorcentajederetiva = Convert.ToDecimal(txtPorcentajederetenciondeiva.Text);
                cl.dPorcentajederetisr = Convert.ToDecimal(txtPorcentajederetenciondeisr.Text);
                cl.iManejarieps = swManejarieps.IsOn ? 1 : 0;
                cl.iEmbarcara = Convert.ToInt32(cboEmbarcara.EditValue);
                cl.iFacturara = Convert.ToInt32(cboFacturara.EditValue);
                cl.sAtenciona = txtAtenciona.Text;
                cl.sCorrespondenciaa = txtCorrespondenciaa.Text;
                cl.iOCUltimoprecio = swOCUltimoprecio.IsOn ? 1 : 0;
                cl.intDepositoschequerabeneficiarioID = Convert.ToInt32(cboCuentasbancariaID.EditValue);
                cl.iCambiarelagentealvender = swCambiarelagentealvender.IsOn ? 1 : 0;
                cl.iPermitirexistencianegativa = swPermitirexistencianegativa.IsOn ? 1 : 0;
                cl.decMargenarribadelcosto = Convert.ToDecimal(txtMargenarribadelcosto.Text);
                cl.strFormulaMargen = cboFormulamargen.EditValue.ToString();
                cl.iValidarcreditocliente = swValidarcreditocliente.IsOn ? 1 : 0;
                cl.iValidarbajocosto = swValidarbajocosto.IsOn ? 1 : 0;
                cl.iImpresionDirecta = toggleSwitchImpDir.IsOn ? 1 : 0;


                result = cl.DatosdecontrolColoresGuardar();
                MessageBox.Show(result);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        }

        private void labelControl12_Click(object sender, EventArgs e)
        {

        }

        private void colorPickEditDetalleBack_EditValueChanged(object sender, EventArgs e)
        {
            ColorPickEdit cpe = (ColorPickEdit)sender;
            string sColor = cpe.Text;
            string[] words = sColor.Split(',');
            txtDBR.Text = words[0];
            txtDBG.Text = words[1];
            txtDBB.Text = words[2];

            
        }

        private void colorPickEditDetalleFore_EditValueChanged(object sender, EventArgs e)
        {
            ColorPickEdit cpe = (ColorPickEdit)sender;
            string sColor = cpe.Text;
            string[] words = sColor.Split(',');
            txtDFR.Text = words[0];
            txtDFG.Text = words[1];
            txtDFB.Text = words[2];
        }

        private void swManejarlistasdeprecios_Toggled(object sender, EventArgs e)
        {
            if (swManejarlistasdeprecios.IsOn)
            {
                swManejarultimosprecios.IsOn = false;
            }
        }

        private void swManejarultimosprecios_Toggled(object sender, EventArgs e)
        {
            if (swManejarultimosprecios.IsOn)
            {
                swManejarlistasdeprecios.IsOn = false;
            }
        }

        private void labelControl43_Click(object sender, EventArgs e)
        {

        }

        private void colorPickEditGrupoBack_EditValueChanged(object sender, EventArgs e)
        {
            ColorPickEdit cpe = (ColorPickEdit)sender;
            string sColor = cpe.Text;
            string[] words = sColor.Split(',');
            txtGBR.Text = words[0];
            txtGBG.Text = words[1];
            txtGBB.Text = words[2];
        }

        private void colorPickEditGrupoFore_EditValueChanged(object sender, EventArgs e)
        {
            ColorPickEdit cpe = (ColorPickEdit)sender;
            string sColor = cpe.Text;
            string[] words = sColor.Split(',');
            txtGFR.Text = words[0];
            txtGFG.Text = words[1];
            txtGFB.Text = words[2];
        }

        private void colorPickEditMEB_EditValueChanged(object sender, EventArgs e)
        {
            ColorPickEdit cpe = (ColorPickEdit)sender;
            string sColor = cpe.Text;
            string[] words = sColor.Split(',');
            txtMEBR.Text = words[0];
            txtMEBG.Text = words[1];
            txtMEBB.Text = words[2];
        }

        private void colorPickEditMEF_EditValueChanged(object sender, EventArgs e)
        {
            ColorPickEdit cpe = (ColorPickEdit)sender;
            string sColor = cpe.Text;
            string[] words = sColor.Split(',');
            txtMEFR.Text = words[0];
            txtMEFG.Text = words[1];
            txtMEFB.Text = words[2];
        }

        private void colorPickEditMPB_EditValueChanged(object sender, EventArgs e)
        {
            ColorPickEdit cpe = (ColorPickEdit)sender;
            string sColor = cpe.Text;
            string[] words = sColor.Split(',');
            txtMPBR.Text = words[0];
            txtMPBG.Text = words[1];
            txtMPBB.Text = words[2];
        }

        private void colorPickEditMPF_EditValueChanged(object sender, EventArgs e)
        {
            ColorPickEdit cpe = (ColorPickEdit)sender;
            string sColor = cpe.Text;
            string[] words = sColor.Split(',');
            txtMPFR.Text = words[0];
            txtMPFG.Text = words[1];
            txtMPFB.Text = words[2];
        }
    }
}