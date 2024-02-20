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
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Ventas.Designers;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class Ventasporfamiliaarticulo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Agente = 0;
        int Canaldeventa = 0;
        int Articulo = 0;
        int Linea = 0;
        int Familia = 0;
        int Subfamilia = 0;
        int Emp = 0;

        public Ventasporfamiliaarticulo()
        {
            InitializeComponent();
            Cargacombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }


        private void Reporte()
        {
            try
            {
                Agente = Convert.ToInt32(cboAgente.EditValue);
                Canaldeventa = Convert.ToInt32(cboCanaldeventas.EditValue);
                Articulo = Convert.ToInt32(cboArticulos.EditValue);
                Linea = Convert.ToInt32(cboLinea.EditValue);
                Familia = Convert.ToInt32(cboFamilia.EditValue);
                Subfamilia = Convert.ToInt32(cboFamilia.EditValue);

                if (swTop.IsOn)
                    Emp = 1;
                else
                    Emp = 0;




                int impDirecto = 0;
                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                if (result == "OK")
                {
                    impDirecto = cl.iImpresiondirecta;
                }
                else
                {
                    impDirecto = 0;
                }

                switch (Convert.ToInt16(rdoNivelinformacion.EditValue))
                {
                    case 1:
                        ImpresionVentasFamiliaArticulosResumen(impDirecto);
                        break;
                    case 2:
                        ImpresionVentasFamiliaArticulosDetalle(impDirecto);
                        break;

                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void ImpresionVentasFamiliaArticulosResumen(int ImpDirecto)
        {
            VentasDesignerVentasporLineaFamilia rep = new VentasDesignerVentasporLineaFamilia();
            if (ImpDirecto == 1)
            {

                rep.Parameters["parameter1"].Value = Emp;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = 0;
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Linea;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Familia;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal); ;
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = Agente;
                rep.Parameters["parameter7"].Visible = false;
                rep.Parameters["parameter8"].Value = Canaldeventa;
                rep.Parameters["parameter8"].Visible = false;
                rep.Parameters["parameter9"].Value = 0;
                rep.Parameters["parameter9"].Visible = false;
                rep.Parameters["parameter10"].Value = 99999;     //Duumy
                rep.Parameters["parameter10"].Visible = false;
                rep.Parameters["parameter11"].Value = Subfamilia;     //Duumy
                rep.Parameters["parameter11"].Visible = false;
                ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                rpt.Print();
                return;
            }
            else
            {
                rep.Parameters["parameter1"].Value = Emp;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = 0;
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Linea;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Familia;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = Agente;
                rep.Parameters["parameter7"].Visible = false;
                rep.Parameters["parameter8"].Value = Canaldeventa;
                rep.Parameters["parameter8"].Visible = false;
                rep.Parameters["parameter9"].Value = "0";
                rep.Parameters["parameter9"].Visible = false;
                rep.Parameters["parameter10"].Value = Subfamilia;     //Duumy
                rep.Parameters["parameter10"].Visible = false;
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                navigationFrame.SelectedPageIndex = 1;
            }
        }

        private void ImpresionVentasFamiliaArticulosDetalle(int ImpDirecto)
        {
            VentasDesignerVentasporLineaFamiliaDetalldo rep = new VentasDesignerVentasporLineaFamiliaDetalldo();
            int intArtIn = 0;
            int intArtFin = 0;

            if (cboArticulos.EditValue == null)
            {
                intArtIn = 0;
                intArtFin = 99999;
            }
            else
            {
                intArtIn = Articulo;
                intArtFin = Articulo;
            }

            if (ImpDirecto == 1)
            {

                rep.Parameters["parameter1"].Value = Emp;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = 0;
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Linea;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Familia;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal); ;
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = Agente;
                rep.Parameters["parameter7"].Visible = false;
                rep.Parameters["parameter8"].Value = Canaldeventa;
                rep.Parameters["parameter8"].Visible = false;
                rep.Parameters["parameter9"].Value = intArtIn;
                rep.Parameters["parameter9"].Visible = false;
                rep.Parameters["parameter10"].Value = intArtFin;     //Duumy
                rep.Parameters["parameter10"].Visible = false;
                rep.Parameters["parameter11"].Value = "0";     //Duumy
                rep.Parameters["parameter11"].Visible = false;
                ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                rpt.Print();
                return;
            }
            else
            {
                rep.Parameters["parameter1"].Value = Emp;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = 0;
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Linea;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Familia;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal); ;
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = Agente;
                rep.Parameters["parameter7"].Visible = false;
                rep.Parameters["parameter8"].Value = Canaldeventa;
                rep.Parameters["parameter8"].Visible = false;
                rep.Parameters["parameter9"].Value = intArtIn;
                rep.Parameters["parameter9"].Visible = false;
                rep.Parameters["parameter10"].Value = intArtFin;     //Duumy
                rep.Parameters["parameter10"].Visible = false;
                rep.Parameters["parameter11"].Value = "0";     //Duumy
                rep.Parameters["parameter11"].Visible = false;
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                navigationFrame.SelectedPageIndex = 1;
            }
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();
            cboAgente.Properties.ValueMember = "Clave";
            cboAgente.Properties.DisplayMember = "Des";
            cl.strTabla = "Agentes";
            src.DataSource = cl.CargaCombos();
            cboAgente.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAgente.Properties.ForceInitialize();
            cboAgente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgente.Properties.ForceInitialize();
            cboAgente.Properties.PopulateColumns();
            cboAgente.Properties.Columns["Clave"].Visible = false;
            cboAgente.ItemIndex = 0;


            //Canalesdeventa
            cboCanaldeventas.Properties.ValueMember = "Clave";
            cboCanaldeventas.Properties.DisplayMember = "Des";
            cl.strTabla = "Canalesdeventa";
            src.DataSource = cl.CargaCombos();
            cboCanaldeventas.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboCanaldeventas.Properties.ForceInitialize();
            cboCanaldeventas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCanaldeventas.Properties.ForceInitialize();
            cboCanaldeventas.Properties.PopulateColumns();
            cboCanaldeventas.Properties.Columns["Clave"].Visible = false;
            cboCanaldeventas.ItemIndex = 0;


            cboLinea.Properties.ValueMember = "Clave";
            cboLinea.Properties.DisplayMember = "Des";
            cl.strTabla = "Lineas";
            src.DataSource = cl.CargaCombos();
            cboLinea.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboLinea.Properties.ForceInitialize();
            cboLinea.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboLinea.Properties.ForceInitialize();
            cboLinea.Properties.PopulateColumns();
            cboLinea.Properties.Columns["Clave"].Visible = false;
            cboLinea.ItemIndex = 0;

            cboFamilia.Properties.NullText = "";
            cboSubFamilias.Properties.NullText = "";
            cboArticulos.Properties.NullText = "";

        }

        private void CargaComboFamilias()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            cl.strTabla = "FamiliasLineas";
            
            cl.intClave = Convert.ToInt32(cboLinea.EditValue);
            src.DataSource = cl.CargaCombosCondicion();
            cboFamilia.Properties.ValueMember = "Clave";
            cboFamilia.Properties.DisplayMember = "Des";
            cboFamilia.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboFamilia.Properties.ForceInitialize();
            cboFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilia.Properties.PopulateColumns();
            cboFamilia.Properties.Columns["Clave"].Visible = false;
            cboFamilia.ItemIndex = 0;



        }

        private void CargaComboSubfamilias()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            cl.strTabla = "SubFamiliasXFamilia";
            cl.intClave = Convert.ToInt32(cboFamilia.EditValue);
            src.DataSource = cl.CargaCombosCondicion();
            
            cboSubFamilias.Properties.ValueMember = "Clave";
            cboSubFamilias.Properties.DisplayMember = "Des";
            cboSubFamilias.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboSubFamilias.Properties.ForceInitialize();
            cboSubFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSubFamilias.Properties.PopulateColumns();
            cboSubFamilias.Properties.Columns["Clave"].Visible = false;
            cboSubFamilias.ItemIndex = 0;
        }

        private void CargaComboArticulos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            cl.strTabla = "ArticulosxSubFamilia";
            cl.intFam = Convert.ToInt32(cboFamilia.EditValue);
            cl.intSubFamilias= Convert.ToInt32(cboSubFamilias.EditValue);
            src.DataSource = cl.CargaCombosCondicion();
            
            cl.intSubFamilias = Convert.ToInt32(cboSubFamilias.EditValue);
            cboArticulos.Properties.ValueMember = "Clave";
            cboArticulos.Properties.DisplayMember = "Des";
            cboArticulos.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboArticulos.Properties.ForceInitialize();
            cboArticulos.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulos.Properties.PopulateColumns();
            try
            {
                cboArticulos.Properties.Columns["Clave"].Visible = false;
            }
            catch (Exception)
            {

            }
            
            cboArticulos.ItemIndex = 0;
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiRegresarImpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiVistaprevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Generando informe...");
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void rdoNivelinformacion_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(rdoNivelinformacion.EditValue) == 1)
            {
                cboArticulos.Enabled = false;
            }
            else
            {
                cboArticulos.Enabled = true;
            }
        }

        private void rdoNivelinformacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(rdoNivelinformacion.EditValue) == 1)
            {
                cboArticulos.Enabled = false;
            }
            else
            {
                cboArticulos.Enabled = true;
            }
        }

        private void cboLinea_EditValueChanged(object sender, EventArgs e)
        {
            if (cboLinea.EditValue == null) { }
            else { CargaComboFamilias(); }
        }

        private void cboFamilia_EditValueChanged(object sender, EventArgs e)
        {
            if (cboFamilia.EditValue == null) { }
            else { CargaComboSubfamilias(); }
        }

        private void cboSubFamilias_EditValueChanged(object sender, EventArgs e)
        {
            if (cboSubFamilias.EditValue == null) { }
            else
            {
                if (cboSubFamilias.EditValue == null)
                {

                }
                else
                {
                    CargaComboArticulos();
                }

            }
        }

        private void cboFamilia_EditValueChanged_1(object sender, EventArgs e)
        {
            CargaComboSubfamilias();
        }

        private void cboSubFamilias_EditValueChanged_1(object sender, EventArgs e)
        {
            CargaComboArticulos();
        }
    }
}