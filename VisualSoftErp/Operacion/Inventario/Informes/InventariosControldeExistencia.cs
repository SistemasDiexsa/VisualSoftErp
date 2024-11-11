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
using VisualSoftErp.Operacion.Inventarios.Designers;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Operacion.Compras.Clases;

namespace VisualSoftErp.Operacion.Inventarios.Informes
{
    public partial class InventariosControldeExistencia : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public InventariosControldeExistencia()
        {
            InitializeComponent();

            dateEditFecha.Text = DateTime.Now.ToShortDateString();
         

            CargaCombos();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargaCombos(){
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();
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

            cboAlmacen.Properties.ValueMember = "Clave";
            cboAlmacen.Properties.DisplayMember = "Des";
            cl.strTabla = "Almacenes";
            src.DataSource = cl.CargaCombos();
            cboAlmacen.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAlmacen.Properties.ForceInitialize();
            cboAlmacen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacen.Properties.ForceInitialize();
            cboAlmacen.Properties.PopulateColumns();
            cboAlmacen.Properties.Columns["Clave"].Visible = false;
            cboAlmacen.ItemIndex = 0;

            cboTipoArticulo.Properties.ValueMember = "Clave";
            cboTipoArticulo.Properties.DisplayMember = "Des";
            cl.strTabla = "Tiposdearticulo";
            src.DataSource = cl.CargaCombos();
            cboTipoArticulo.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboTipoArticulo.Properties.ForceInitialize();
            cboTipoArticulo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTipoArticulo.Properties.ForceInitialize();
            cboTipoArticulo.Properties.PopulateColumns();
            cboTipoArticulo.Properties.Columns["Clave"].Visible = false;
            cboTipoArticulo.ItemIndex = 0;

            List<ClaseGenricaCL> ClaseOp = new List<ClaseGenricaCL>();
            ClaseOp.Add(new ClaseGenricaCL() { Clave = "0", Des = "Todos" });
            ClaseOp.Add(new ClaseGenricaCL() { Clave = "1", Des = "Mínimo" });
            ClaseOp.Add(new ClaseGenricaCL() { Clave = "2", Des = "Máximo" });

            cboOpcion.Properties.ValueMember = "Clave";
            cboOpcion.Properties.DisplayMember = "Des";
            cboOpcion.Properties.DataSource = ClaseOp;
            cboOpcion.Properties.ForceInitialize();
            cboOpcion.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboOpcion.Properties.ForceInitialize();
            cboOpcion.Properties.PopulateColumns();
            cboOpcion.Properties.Columns["Clave"].Visible = false;
            cboOpcion.ItemIndex = 0;

        }

        private void CargaComboFamilia()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();
            cboFamilias.Properties.ValueMember = "Clave";
            cboFamilias.Properties.DisplayMember = "Des";
            cl.strTabla = "FamiliaxLinea";
            cl.iCondicion = Convert.ToInt32(cboLinea.EditValue);
            src.DataSource = cl.CargaCombos();
            cboFamilias.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboFamilias.Properties.ForceInitialize();
            cboFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilias.Properties.ForceInitialize();
            cboFamilias.Properties.PopulateColumns();
            cboFamilias.Properties.Columns["Clave"].Visible = false;
            cboFamilias.ItemIndex = 0;
        }

        private void CargaComboSubFamilias()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();
            cbosubFamilia.Properties.ValueMember = "Clave";
            cbosubFamilia.Properties.DisplayMember = "Des";
            cl.strTabla = "SubfamiliasXfamilias";
            cl.iCondicion = Convert.ToInt32(cboFamilias.EditValue);
            src.DataSource = cl.CargaCombos();
            cbosubFamilia.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cbosubFamilia.Properties.ForceInitialize();
            cbosubFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cbosubFamilia.Properties.ForceInitialize();
            cbosubFamilia.Properties.PopulateColumns();
            cbosubFamilia.Properties.Columns["Clave"].Visible = false;
            cbosubFamilia.ItemIndex = 0;
        }

        private void Reporte()
        {
            try
            {

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

                InventariosControldeExistenciaDesigner rep = new InventariosControldeExistenciaDesigner();
                if (impDirecto == 1)
                {

                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboLinea.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboFamilias.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToInt32(cbosubFamilia.EditValue);
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToInt32(cboAlmacen.EditValue);
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = Convert.ToInt32(cboTipoArticulo.EditValue);
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = Convert.ToInt32(cboOpcion.EditValue);
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = Convert.ToDateTime(dateEditFecha.Text);
                    rep.Parameters["parameter9"].Visible = false;

                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboLinea.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboFamilias.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToInt32(cbosubFamilia.EditValue);
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToInt32(cboAlmacen.EditValue);
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = Convert.ToInt32(cboTipoArticulo.EditValue);
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = Convert.ToInt32(cboOpcion.EditValue);
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = Convert.ToDateTime(dateEditFecha.Text);
                    rep.Parameters["parameter9"].Visible = false;
                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                    navigationFrame.SelectedPageIndex = 1;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void bbiVistaprevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Generando informe...");
            this.Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
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

        private void cboLinea_EditValueChanged(object sender, EventArgs e)
        {
            if (cboLinea.EditValue == null) { }
            else
            {
                CargaComboFamilia();
            }
        }

        private void cboFamilias_EditValueChanged(object sender, EventArgs e)
        {
            if (cboFamilias.EditValue == null) { }
            else
            {
                CargaComboSubFamilias();
            }
        }

       
    }
}