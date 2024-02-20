using DevExpress.XtraBars.Ribbon;
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
using VisualSoftErp.Operacion.Compras.Clases;
using VisualSoftErp.Operacion.Ventas.Designers;

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class VentasEstadisticaPorSucursal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Agente = 0;
        int Año = 0;
        int Mes = 0;
        int Cliente = 0;
        int Canaldeventa = 0;
        int CantidadOImporte = 0;
        public VentasEstadisticaPorSucursal()
        {
            InitializeComponent();
            CargarCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargarCombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            // COMBO AGENTES
            BindingSource src = new BindingSource();
            cboAgentes.Properties.ValueMember = "Clave";
            cboAgentes.Properties.DisplayMember = "Des";
            cl.strTabla = "Agentes";
            src.DataSource = cl.CargaCombos();
            cboAgentes.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAgentes.Properties.ForceInitialize();
            cboAgentes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgentes.Properties.ForceInitialize();
            cboAgentes.Properties.PopulateColumns();
            cboAgentes.Properties.Columns["Clave"].Visible = false;
            cboAgentes.Properties.Columns["Encabezado"].Visible = false;
            cboAgentes.Properties.Columns["Piedepagina"].Visible = false;
            cboAgentes.Properties.Columns["Email"].Visible = false;
            cboAgentes.ItemIndex = 0;

            // COMBO CLIENTES
            cboClientes.Properties.ValueMember = "Clave";
            cboClientes.Properties.DisplayMember = "Des";
            cl.strTabla = "Clientes";
            src.DataSource = cl.CargaCombos();
            cboClientes.Properties.DataSource = src.DataSource;
            cboClientes.Properties.ForceInitialize();
            cboClientes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboClientes.Properties.ForceInitialize();
            cboClientes.Properties.PopulateColumns();
            cboClientes.Properties.Columns["Clave"].Visible = false;
            cboClientes.Properties.Columns["AgentesID"].Visible = false;
            cboClientes.Properties.Columns["Plazo"].Visible = false;
            cboClientes.Properties.Columns["Listadeprecios"].Visible = false;
            cboClientes.Properties.Columns["Exportar"].Visible = false;
            cboClientes.Properties.Columns["cFormapago"].Visible = false;
            cboClientes.Properties.Columns["cMetodopago"].Visible = false;
            cboClientes.Properties.Columns["UsoCfdi"].Visible = false;
            cboClientes.Properties.Columns["PIva"].Visible = false;
            cboClientes.Properties.Columns["PIeps"].Visible = false;
            cboClientes.Properties.Columns["PRetiva"].Visible = false;
            cboClientes.Properties.Columns["PRetIsr"].Visible = false;
            cboClientes.Properties.Columns["EMail"].Visible = false;
            cboClientes.Properties.Columns["BancoordenanteID"].Visible = false;
            cboClientes.Properties.Columns["Cuentaordenante"].Visible = false;
            cboClientes.Properties.Columns["cFormapagoDepositos"].Visible = false;
            cboClientes.Properties.Columns["Moneda"].Visible = false;
            cboClientes.Properties.Columns["DescuentoBase"].Visible = false;
            cboClientes.Properties.Columns["DesctoPP"].Visible = false;
            cboClientes.ItemIndex = 0;

            // COMBO AÑOS
            cboAños.Properties.ValueMember = "Clave";
            cboAños.Properties.DisplayMember = "Des";
            cl.strTabla = "Añosventas";
            src.DataSource = cl.CargaCombos();
            cboAños.Properties.DataSource = src.DataSource;
            cboAños.Properties.ForceInitialize();
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
            cboMeses.Properties.ForceInitialize();
            cboMeses.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMeses.Properties.ForceInitialize();
            cboMeses.Properties.PopulateColumns();
            cboMeses.Properties.Columns["Clave"].Visible = false;
            cboMeses.ItemIndex = DateTime.Now.Month;

            // COMBO CANALES DE VENTA
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
        }  //   Carga todos los combos de la pantalla

        private void swResumenDetalle_Toggled(object sender, EventArgs e)
        {
            if (swResumenDetalle.IsOn)
            {
                swCantidadOImporte.IsOn = true;
                swCantidadOImporte.Visible = false;
            }
            else
            {
                swCantidadOImporte.Visible = true;
            }
        }  // Hace visible o invisible al switch cantidad/importe

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }   // Cierra la pantalla de Estadisticas por Sucursal

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPageIndex = 0;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
        }   // Cierra la vista previa del reporte

        private void bbiPrevio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            if (swResumenDetalle.IsOn)
                this.ReporteResumen();
            else
                this.ReporteDetalle();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }   // Valida si el Reporte es Resumen o Detalle

        private void ReporteResumen()
        {
            try
            {
                Agente = Convert.ToInt32(cboAgentes.EditValue);
                Canaldeventa = Convert.ToInt32(cboCanaldeventas.EditValue);
                Cliente = Convert.ToInt32(cboClientes.EditValue);
                Año = Convert.ToInt32(cboAños.EditValue);
                Mes = Convert.ToInt32(cboMeses.EditValue);

                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                int impDirecto = result == "OK" ? cl.iImpresiondirecta : 0;
                int IMPCANT = swCantidadOImporte.IsOn ? 0 : 1;

                VentasEstadisticaporSucursalDesigner rep = new VentasEstadisticaporSucursalDesigner();
                rep.IMPCANT = IMPCANT;
                rep.DefaultPrinterSettingsUsing.UseLandscape = false;
                rep.Landscape = true;

                if (impDirecto == 1)  //Directo a la impresora
                {
                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Año;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Mes;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Cliente;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Agente;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = Canaldeventa;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = IMPCANT;
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = "";     // Dummy
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
                    rep.Parameters["parameter3"].Value = Año;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Mes;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Cliente;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Agente;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = Canaldeventa;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = IMPCANT;
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = "";     // Dummy
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

        private void ReporteDetalle()
        {
            try
            {
                Agente = Convert.ToInt32(cboAgentes.EditValue);
                Canaldeventa = Convert.ToInt32(cboCanaldeventas.EditValue);
                Cliente = Convert.ToInt32(cboClientes.EditValue);
                Año = Convert.ToInt32(cboAños.EditValue);
                Mes = Convert.ToInt32(cboMeses.EditValue);

                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                int impDirecto = result == "OK" ? cl.iImpresiondirecta : 0;
                int IMPCANT = swCantidadOImporte.IsOn ? 0 : 1;

                VentasEstadisticaporSucursalDetalladoDesigner rep = new VentasEstadisticaporSucursalDetalladoDesigner();
                rep.IMPCANT = IMPCANT;
                rep.DefaultPrinterSettingsUsing.UseLandscape = false;
                rep.Landscape = true;

                if (impDirecto == 1)  //Directo a la impresora
                {
                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Año;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Mes;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Cliente;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Agente;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = Canaldeventa;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = IMPCANT;
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = "";     // Dummy
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
                    rep.Parameters["parameter3"].Value = Año;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Mes;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Cliente;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Agente;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = Canaldeventa;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = IMPCANT;
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = "";     // Dummy
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
    }
}