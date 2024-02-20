using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Clases;
using VisualSoftErp.Catalogos;
using VisualSoftErp.Catalogos.CXC;
using VisualSoftErp.Operacion.Compras.Clases;
using VisualSoftErp.Operacion.Compras.Designers;

namespace VisualSoftErp.Operacion.Compras.Informes
{
    public partial class EstadisticaPorProveedor : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public EstadisticaPorProveedor()
        {
            InitializeComponent();
            CargarCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargarCombos()
        {
            combosCL combosCL = new combosCL();
            globalCL globalCL = new globalCL();
            BindingSource src = new BindingSource();

            // COMBO AÑOS
            cboAños.Properties.ValueMember = "Clave";
            cboAños.Properties.DisplayMember = "Des";
            combosCL.strTabla = "Añosventas";
            src.DataSource = combosCL.CargaCombos();
            cboAños.Properties.DataSource = src.DataSource;
            cboAños.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAños.Properties.ForceInitialize();
            cboAños.Properties.PopulateColumns();
            cboAños.Properties.Columns["Clave"].Visible = false;
            cboAños.ItemIndex = 0;

            // COMBO MESES
            List<ClaseGenricaCL> ListadoMeses = new List<ClaseGenricaCL>();
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "1", Des = "Enero" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "2", Des = "Febrero" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "3", Des = "Marzo" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "4", Des = "Abril" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "5", Des = "Mayo" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "6", Des = "Junio" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "7", Des = "Julio" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "8", Des = "Agosto" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "9", Des = "Septiembre" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "10", Des = "Octubre" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "11", Des = "Noviembre" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "12", Des = "Diciembre" });
            cboMeses.Properties.ValueMember = "Clave";
            cboMeses.Properties.DisplayMember = "Des";
            cboMeses.Properties.DataSource = ListadoMeses;
            cboMeses.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMeses.Properties.ForceInitialize();
            cboMeses.Properties.PopulateColumns();
            cboMeses.Properties.Columns["Clave"].Visible = false;
            cboMeses.ItemIndex = DateTime.Now.Month;

            // COMBO PROVEEDORES
            cboProveedores.Properties.ValueMember = "Clave";
            cboProveedores.Properties.DisplayMember = "Des";
            combosCL.strTabla = "Proveedores";
            src.DataSource = combosCL.CargaCombos();
            cboProveedores.Properties.DataSource = globalCL.AgregarOpcionTodos(src);
            cboProveedores.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedores.Properties.ForceInitialize();
            cboProveedores.Properties.PopulateColumns();
            cboProveedores.Properties.Columns["Clave"].Visible = false;
            cboProveedores.Properties.Columns["Piva"].Visible = false;
            cboProveedores.Properties.Columns["Plazo"].Visible = false;
            cboProveedores.Properties.Columns["Tiempodeentrega"].Visible = false;
            cboProveedores.Properties.Columns["Diastraslado"].Visible = false;
            cboProveedores.Properties.Columns["Lab"].Visible = false;
            cboProveedores.Properties.Columns["Via"].Visible = false;
            cboProveedores.Properties.Columns["BancosID"].Visible = false;
            cboProveedores.Properties.Columns["Cuentabancaria"].Visible = false;
            cboProveedores.Properties.Columns["C_Formapago"].Visible = false;
            cboProveedores.Properties.Columns["MonedasID"].Visible = false;
            cboProveedores.Properties.Columns["Retiva"].Visible = false;
            cboProveedores.Properties.Columns["Retisr"].Visible = false;
            cboProveedores.Properties.NullText = "Seleccione un Proveedor";
        }   //  Carga todos los combos de la pantalla
        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }   // Cierra la pantalla Estadistica por Proveedor
        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPageIndex = 0;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
        }   // Cierra la vista previa del reporte

        private void swResumenDetalle_Toggled(object sender, EventArgs e)
        {
            if (swResumenDetalle.IsOn)
            {
                swCantidadOImporte.IsOn = true;
                swCantidadOImporte.Visible = false;
            }
            else
                swCantidadOImporte.Visible = true;
        }   // Hace visible o invisible el switch de importe/cantidad
        private void bbiPrevio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            if (swResumenDetalle.IsOn)
                this.ReporteResumen();
            else
                this.ReporteDetalle();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }   // Valida si el reporte es resumen o detalle

        private void ReporteResumen()
        {
            try
            {
                globalCL globalCL = new globalCL();
                string results = globalCL.Datosdecontrol();
                int impDirecto = results == "OK" ? globalCL.iImpresiondirecta : 0;

                EstadisticaPorProveedorResumenDesigner rep = new EstadisticaPorProveedorResumenDesigner();
                rep.IMPCANT = swCantidadOImporte.IsOn ? 0 : 1;
                rep.DefaultPrinterSettingsUsing.UseLandscape = false;
                rep.Landscape = true;

                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = 0; // Debe de enviar ID Empresa
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = Convert.ToInt32(cboAños.EditValue);
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboMeses.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboProveedores.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = rep.IMPCANT;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = string.Empty;
                    rep.Parameters["parameter6"].Visible = false;

                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                }
                else
                {
                    rep.Parameters["parameter1"].Value = 0; // Debe de enviar ID Empresa
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = Convert.ToInt32(cboAños.EditValue);
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboMeses.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboProveedores.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = rep.IMPCANT;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = string.Empty;
                    rep.Parameters["parameter6"].Visible = false;

                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                    navigationFrame.SelectedPageIndex = 1;
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en ReporteResumen: " + ex.Message);
            }
        }

        private void ReporteDetalle()
        {
            try
            {
                globalCL globalCL = new globalCL();
                string results = globalCL.Datosdecontrol();
                int impDirecto = results == "OK" ? globalCL.iImpresiondirecta : 0;
                int IMPCANT = swCantidadOImporte.IsOn ? 0 : 1;

                EstadisticaPorProveedorDetalladoDesigner rep = new EstadisticaPorProveedorDetalladoDesigner();
                rep.IMPCANT = swCantidadOImporte.IsOn ? 0 : 1;
                rep.DefaultPrinterSettingsUsing.UseLandscape = false;
                rep.Landscape = true;

                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = 0; // Debe de enviar ID Empresa
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = Convert.ToInt32(cboAños.EditValue);
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboMeses.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboProveedores.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = rep.IMPCANT;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = string.Empty;
                    rep.Parameters["parameter6"].Visible = false;

                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                }
                else
                {
                    rep.Parameters["parameter1"].Value = 0; // Debe de enviar ID Empresa
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = Convert.ToInt32(cboAños.EditValue);
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboMeses.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboProveedores.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = rep.IMPCANT;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = string.Empty;
                    rep.Parameters["parameter6"].Visible = false;

                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                    navigationFrame.SelectedPageIndex = 1;
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en ReporteDetalle: " + ex.Message);
            }
        }
    }
}