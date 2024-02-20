using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Preview;
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
using VisualSoftErp.Catalogos.CXC;
using VisualSoftErp.Catalogos;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Ventas.Designers;
using DevExpress.XtraBars.Ribbon;
using VisualSoftErp.Operacion.Compras.Clases;

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class VentaEstadoClienteCanal : DevExpress.XtraBars.Ribbon.RibbonForm   
    {
        public VentaEstadoClienteCanal()
        {
            
            InitializeComponent();
            Cargacombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            // COMBO TIPO DE REPORTE
            List<ClaseGenricaCL> ListadoTiposReporte = new List<ClaseGenricaCL>();
            ListadoTiposReporte.Add(new ClaseGenricaCL() { Clave = "0", Des = "Canal de Venta" });
            ListadoTiposReporte.Add(new ClaseGenricaCL() { Clave = "1", Des = "Estado, Canal de Venta" });
            ListadoTiposReporte.Add(new ClaseGenricaCL() { Clave = "2", Des = "Estado, Cliente, Canal de Venta" });
            cboTipoReporte.Properties.ValueMember = "Clave";
            cboTipoReporte.Properties.DisplayMember = "Des";
            cboTipoReporte.Properties.DataSource = ListadoTiposReporte;
            cboTipoReporte.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTipoReporte.Properties.ForceInitialize();
            cboTipoReporte.Properties.PopulateColumns();
            cboTipoReporte.Properties.Columns["Clave"].Visible = false;
            cboTipoReporte.Properties.Columns["Value"].Visible = false;
            cboTipoReporte.Properties.Columns["Description"].Visible = false;
            cboTipoReporte.Properties.NullText = "Seleccione el tipo de reporte";

            // COMBO ESTADOS
            cl.strTabla = "Estados";
            src.DataSource = cl.CargaCombos();
            cboEdo.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboEdo.Properties.ValueMember = "Clave";
            cboEdo.Properties.DisplayMember = "Des";
            cboEdo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboEdo.Properties.ForceInitialize();
            cboEdo.Properties.PopulateColumns();
            cboEdo.Properties.Columns["Clave"].Visible = false;
            cboEdo.Properties.NullText = "Seleccione un Estado";

            // COMBO CANALES DE VENTA
            cl.strTabla = "Canalesdeventa";
            src.DataSource = cl.CargaCombos();
            cboCanaldeventa.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboCanaldeventa.Properties.ValueMember = "Clave";
            cboCanaldeventa.Properties.DisplayMember = "Des";
            cboCanaldeventa.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCanaldeventa.Properties.ForceInitialize();
            cboCanaldeventa.Properties.PopulateColumns();
            cboCanaldeventa.Properties.Columns["Clave"].Visible = false;
            cboCanaldeventa.Properties.NullText = "Seleccione un Canal de venta";
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPageIndex = 0;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPage1.Text);
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiPrevio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Generando informe...");
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Reporte()
        {
            string valido =  Valida();
            
            if (valido != "OK")
            {
                MessageBox.Show(valido);
                return;
            }

            globalCL cl = new globalCL();
            string result = cl.Datosdecontrol();
            int impDirecto = result == "OK" ? cl.iImpresiondirecta : 0;

            int tipoReporte = Convert.ToInt32(cboTipoReporte.EditValue);
            switch (tipoReporte)
            {
                // Canal de Venta
                case 0:
                    VentasXCanalVentasDesigner reporte = new VentasXCanalVentasDesigner();

                    reporte.DefaultPrinterSettingsUsing.UseLandscape = false;
                    reporte.Landscape = true;
                    reporte.Parameters["parameter1"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    reporte.Parameters["parameter1"].Visible = false;
                    reporte.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    reporte.Parameters["parameter2"].Visible = false;
                    reporte.Parameters["parameter3"].Value = Convert.ToInt32(cboCanaldeventa.EditValue);
                    reporte.Parameters["parameter3"].Visible = false;
                    reporte.Parameters["parameter4"].Value = "";
                    reporte.Parameters["parameter4"].Visible = false;

                    if (impDirecto == 1) // Directo a Impresora
                    {
                        ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(reporte);
                        rpt.Print();
                    }
                    else
                    {
                        documentViewer1.DocumentSource = reporte;
                        reporte.CreateDocument();
                        ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                        navigationFrame.SelectedPageIndex = 1;
                    }
                    break;
                // Estado, Canal de Venta
                case 1:
                    try
                    {
                        VentaEstadoCanalVenta rep = new VentaEstadoCanalVenta();
                        rep.DefaultPrinterSettingsUsing.UseLandscape = false;
                        rep.Landscape = true;
                        rep.Parameters["parameter1"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                        rep.Parameters["parameter1"].Visible = false;
                        rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                        rep.Parameters["parameter2"].Visible = false;
                        rep.Parameters["parameter3"].Value = Convert.ToInt32(cboCanaldeventa.EditValue);
                        rep.Parameters["parameter3"].Visible = false;
                        rep.Parameters["parameter4"].Value = Convert.ToString(cboCanaldeventa.Text);
                        rep.Parameters["parameter4"].Visible = false;

                        if (impDirecto == 1) // Directo a Impresora
                        {
                            ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                            rpt.Print();
                        }
                        else
                        {
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
                    break;
                // Estado, Cliente, Canal de Venta
                case 2:
                    try
                    {
                        VentaEstadoClienteCanalDesigner rep = new VentaEstadoClienteCanalDesigner();
                        rep.DefaultPrinterSettingsUsing.UseLandscape = false;
                        rep.Landscape = true;
                        rep.Parameters["parameter1"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                        rep.Parameters["parameter1"].Visible = false;
                        rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                        rep.Parameters["parameter2"].Visible = false;
                        rep.Parameters["parameter3"].Value = Convert.ToInt32(cboCanaldeventa.EditValue);
                        rep.Parameters["parameter3"].Visible = false;
                        rep.Parameters["parameter4"].Value = Convert.ToInt32(cboEdo.EditValue);
                        rep.Parameters["parameter4"].Visible = false;
                        rep.Parameters["parameter5"].Value = Convert.ToString(cboEdo.Text);
                        rep.Parameters["parameter5"].Visible = false;
                        rep.Parameters["parameter6"].Value = Convert.ToString(cboCanaldeventa.Text);
                        rep.Parameters["parameter6"].Visible = false;

                        if (impDirecto == 1) // Directo a Impresora
                        {
                            ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                            rpt.Print();
                        }
                        else
                        {
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
                    break;
                default:
                    break;
            }
        }

        private string Valida()
        {
            string result = "OK";
            int value = Convert.ToInt32(cboTipoReporte.EditValue);

            if(cboTipoReporte.EditValue == null)
            {
                result = "Seleccione el tipo de reporte a generar";
                return result;
            }

            if (value > 0 && cboCanaldeventa.EditValue == null)
            {
                result = "El campo Canales de venta no puede ir vacio";
                return result;
            }

            if (value == 2 && cboEdo.EditValue == null)
            {
                result = "El campo Estado no puede ir vacio";
                return result;
            }
            return result;
        }

        private void cboTipoReporte_EditValueChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(cboTipoReporte.EditValue);
            switch (value)
            {
                case 0:
                    cboCanaldeventa.Visible = false;
                    cboEdo.Visible = false;
                    break;
                case 1:
                    cboCanaldeventa.Visible = true;
                    cboEdo.Visible = false;
                    break;
                case 2:
                    cboCanaldeventa.Visible = true;
                    cboEdo.Visible = true;
                    break;
                default:
                    break;
            }    
        }
    }
}