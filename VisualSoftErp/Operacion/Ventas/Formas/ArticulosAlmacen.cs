using DevExpress.Pdf.Native.BouncyCastle.Ocsp;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Compras.Formas;
using VisualSoftErp.Operacion.Ventas.Designers;

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class ArticulosAlmacen : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        
        public ArticulosAlmacen()
        {
            InitializeComponent();
        }

        private void CargarCombos()
        {
            BindingSource src = new BindingSource();
            combosCL combos = new combosCL();
            globalCL globalCL = new globalCL();

            #region LINEAS
            combos.strTabla = "Lineas";
            cboLineas.Properties.ValueMember = "Clave";
            cboLineas.Properties.DisplayMember = "Des";
            src.DataSource = combos.CargaCombos();
            cboLineas.Properties.DataSource = globalCL.AgregarOpcionTodos(src);
            cboLineas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboLineas.Properties.ForceInitialize();
            cboLineas.Properties.PopulateColumns();
            cboLineas.Properties.Columns["Clave"].Visible = false;
            cboLineas.Properties.NullText = "Seleccione una Línea de Artículos";
            cboLineas.EditValue = null;
            #endregion LINEAS

            #region FAMILIAS
            CargarCombosFamilias();
            #endregion FAMILIAS

            #region SUBFAMILIAS
            CargarCombosSubFamilias();
            #endregion SUBFAMILIAS
        }

        private void CargarCombosFamilias()
        {
            if(cboLineas.EditValue != null)
            {
                combosCL combos = new combosCL();
                globalCL globalCL = new globalCL();
                BindingSource src = new BindingSource();

                combos.strTabla = "FamiliasLineas";
                combos.iCondicion = Convert.ToInt32(cboLineas.EditValue);
                cboFamilias.Properties.ValueMember = "Clave";
                cboFamilias.Properties.DisplayMember = "Des";
                src.DataSource = combos.CargaCombos();
                cboFamilias.Properties.DataSource = globalCL.AgregarOpcionTodos(src);
                cboFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboFamilias.Properties.ForceInitialize();
                cboFamilias.Properties.PopulateColumns();
                cboFamilias.Properties.Columns["Clave"].Visible = false;
                cboFamilias.Properties.NullText = "Seleccione un familia";
                cboFamilias.EditValue = null;
            }
        }

        private void CargarCombosSubFamilias()
        {
            if (cboFamilias.EditValue != null)
            {
                combosCL combos = new combosCL();
                BindingSource src = new BindingSource();
                globalCL global = new globalCL();

                combos.strTabla = "SubFamiliasXFamilia";
                combos.iCondicion = Convert.ToInt32(cboFamilias.EditValue);
                cboSubFamilias.Properties.ValueMember = "Clave";
                cboSubFamilias.Properties.DisplayMember = "Des";
                src.DataSource = combos.CargaCombos();
                cboSubFamilias.Properties.DataSource = global.AgregarOpcionTodos(src);
                cboSubFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboSubFamilias.Properties.ForceInitialize();
                cboSubFamilias.Properties.PopulateColumns();
                cboSubFamilias.Properties.NullText = "Seleccione una SubFamilias de Artículos";
                cboSubFamilias.EditValue = null;
            }
        }

        private string Validar()
        {
            string result = "OK";
            if (cboLineas.EditValue == null)
                result = "Favor de seleccionar una línea de artículos";

            if (cboFamilias.EditValue == null)
                result = "Favor de seleccionar una familia de artículos";

            if (cboSubFamilias.EditValue == null)
                result = "Favor de seleccionar una subfamilia de artículos";

            if (radioGroupActivo.EditValue == null)
                result = "Favor de seleccionar si serán los artículos activos, inactivos o todos";

            return result;
        }

        private void Reporte()
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            try
            {
                ArticulosAlmacenDesigner rep = new ArticulosAlmacenDesigner();
                rep.Parameters["parameter1"].Value = Convert.ToInt32(radioGroupActivo.EditValue);
                rep.Parameters["parameter2"].Value = Convert.ToInt32(cboLineas.EditValue);
                rep.Parameters["parameter3"].Value = Convert.ToInt32(cboFamilias.EditValue);
                rep.Parameters["parameter4"].Value = Convert.ToInt32(cboSubFamilias.EditValue);

                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                int impDirecto = result == "OK" ? cl.iImpresiondirecta : 0;

                if (impDirecto == 1)
                {
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                }
                else
                {
                    rep.CreateDocument();
                    documentViewer1.DocumentSource = rep;
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImprimir.Text);
                    navigationFrameArticulosAlmacen.SelectedPage = navigationPageReporte;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Línea: " + ex.LineNumber() + "\n" + ex.Message, "Error al Cargar Reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void ArticulosAlmacen_Load(object sender, EventArgs e)
        {
            if (ribbonControl.MergeOwner != null)
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            CargarCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void navBarControlMenu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NavBarItemLink item = e.Link;
            if (item != null)
            {
                switch (item.ItemName)
                {
                    case "navBarItemDatos":
                        ribbonControl.SelectedPage = ribbonPageHome;
                        navigationFrameArticulosAlmacen.SelectedPage = navigationPageDatos;
                        break;
                    case "navBarItemReporte":
                        string result = Validar();
                        if (result != "OK")
                        {
                            MessageBox.Show(result, "Error al Cargar Reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        Reporte();
                        break;
                }
            }
        }

        private void officeNavigationBarMenu_ItemClick(object sender, NavigationBarItemEventArgs e)
        {
            OfficeNavigationBar officeNavigationBar = (OfficeNavigationBar)sender;

            if (officeNavigationBar != null)
            {
                NavigationBarItem item = officeNavigationBar.SelectedItem;
                switch (item.Name)
                {
                    case "navigationBarItemDatos":
                        ribbonControl.SelectedPage = ribbonPageHome;
                        navigationFrameArticulosAlmacen.SelectedPage = navigationPageDatos;
                        break;
                    case "navigationBarItemReporte":
                        string result = Validar();
                        if (result != "OK")
                        {
                            MessageBox.Show(result, "Error al Cargar Reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        Reporte();
                        break;
                }
            }
        }

        private void bbiCerrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiVistaPrevia_ItemClick(object sender, ItemClickEventArgs e)
        {
            string result = Validar();
            if (result != "OK")
            {
                MessageBox.Show(result, "Error al Cargar Reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Reporte();
        }

        private void bbiRegresar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ribbonControl.MergeOwner != null)
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrameArticulosAlmacen.SelectedPage = navigationPageDatos;
        }

        private void cboLineas_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit combo = (LookUpEdit)sender;
            if (combo != null)
                CargarCombosFamilias();
        }

        private void cboFamilias_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit combo = (LookUpEdit)sender;
            if(combo != null)
                CargarCombosSubFamilias();
        }

    }
}