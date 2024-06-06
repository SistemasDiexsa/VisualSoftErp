using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
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
using VisualSoftErp.Operacion.Ventas.Designers;

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class VentasLineas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public VentasLineas()
        {
            InitializeComponent();
            CargarCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiCerrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiRegresar_ItemClick(object sender, ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame1.SelectedPageIndex = 0;
        }

        private void bbiVistaPrevia_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            this.Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargarCombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            #region COMBO LINEAS
            BindingSource src = new BindingSource();
            cboLineas.Properties.ValueMember = "Clave";
            cboLineas.Properties.DisplayMember = "Des";
            cl.strTabla = "Lineas";
            src.DataSource = cl.CargaCombos();
            cboLineas.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboLineas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboLineas.Properties.ForceInitialize();
            cboLineas.Properties.PopulateColumns();
            cboLineas.Properties.Columns["Clave"].Visible = false;
            cboLineas.Properties.NullText = "Seleccione una linea";
            cboLineas.EditValue = null;
            #endregion COMBO LINEAS
        }

        private string Valida()
        {
            string result = "OK";
            if (cboLineas.EditValue == null)
                result = "Favor de seleccionar una línea";

            return result;
        }

        private void Reporte()
        {
            string result = Valida();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }

            globalCL cl = new globalCL();
            result = cl.Datosdecontrol();
            int impDirecto = result == "OK" ? cl.iImpresiondirecta : 0;
            if (impDirecto == 1)
            {
                VentasLineasDesigner rep = new VentasLineasDesigner();
                rep.Parameters["parameter1"].Value = Convert.ToInt32(cboLineas.EditValue);
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                rep.Parameters["parameter3"].Visible = false;
                ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                rpt.Print();
                return;
            }
            else
            {
                VentasLineasDesigner rep = new VentasLineasDesigner();
                rep.Parameters["parameter1"].Value = Convert.ToInt32(cboLineas.EditValue);
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                rep.Parameters["parameter3"].Visible = false;
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                navigationFrame1.SelectedPageIndex = 1;
            }
        }

        
    }
}