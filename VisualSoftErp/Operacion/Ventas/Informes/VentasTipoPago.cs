using DevExpress.Charts.Native;
using DevExpress.CodeParser;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
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
using VisualSoftErp.Operacion.Compras.Clases;
using VisualSoftErp.Operacion.CxP.Informes;
using DevExpress.Pdf.Native.BouncyCastle.Ocsp;
using VisualSoftErp.Operacion.Ventas.Designers;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class VentasTipoPago : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public VentasTipoPago()
        {
            InitializeComponent();
            CargarCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargarCombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            #region COMBO AGENTES
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
            #endregion COMBO AGENTES

            #region COMBO CLIENTES
            cboClientes.Properties.ValueMember = "Clave";
            cboClientes.Properties.DisplayMember = "Des";
            cl.strTabla = "Clientes";
            src.DataSource = cl.CargaCombos();
            cboAgentes.Properties.DataSource = clg.AgregarOpcionTodos(src);
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
            #endregion COMBO CLIENTES

            #region COMBO STATUS
            List<ClaseGenricaCL> tipoL = new List<ClaseGenricaCL>();
            tipoL.Add(new ClaseGenricaCL() { Clave = "0", Des = "Todos" });
            tipoL.Add(new ClaseGenricaCL() { Clave = "1", Des = "Registrada" });
            tipoL.Add(new ClaseGenricaCL() { Clave = "2", Des = "Facturada" });
            tipoL.Add(new ClaseGenricaCL() { Clave = "3", Des = "Pagadas Sin Factura" });
            tipoL.Add(new ClaseGenricaCL() { Clave = "4", Des = "Expirada" });
            tipoL.Add(new ClaseGenricaCL() { Clave = "5", Des = "Cancelada" });
            cboStatus.Properties.ValueMember = "Clave";
            cboStatus.Properties.DisplayMember = "Des";
            cboStatus.Properties.DataSource = tipoL;
            cboStatus.Properties.ForceInitialize();
            cboStatus.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboStatus.Properties.PopulateColumns();
            cboStatus.Properties.Columns["Clave"].Visible = false;
            cboStatus.Properties.Columns["Value"].Visible = false;
            cboStatus.Properties.Columns["Description"].Visible = false;
            cboStatus.ItemIndex = 0;
            #endregion COMBO STATUS

            #region COMBO FORMA DE PAGO
            List<ClaseGenricaCL> list = new List<ClaseGenricaCL>();
            list.Add(new ClaseGenricaCL() { Clave = "0",  Des = "Todos" });
            list.Add(new ClaseGenricaCL() { Clave = "01", Des = "Efectivo" });
            list.Add(new ClaseGenricaCL() { Clave = "02", Des = "Cheque Nominativo" });
            list.Add(new ClaseGenricaCL() { Clave = "03", Des = "Transferencia Electrónica de Fondos" });
            list.Add(new ClaseGenricaCL() { Clave = "99", Des = "POR DEFINIR" });
            list.Add(new ClaseGenricaCL() { Clave = "CC", Des = "Crédito" });
            list.Add(new ClaseGenricaCL() { Clave = "17", Des = "Compensación" });
            list.Add(new ClaseGenricaCL() { Clave = "04", Des = "Tarjeta de Crédito" });
            list.Add(new ClaseGenricaCL() { Clave = "28", Des = "Tarjeta de Débito" });
            list.Add(new ClaseGenricaCL() { Clave = "DE", Des = "Depurado" });
            list.Add(new ClaseGenricaCL() { Clave = "15", Des = "Condonacion" });
            list.Add(new ClaseGenricaCL() { Clave = "30", Des = "Aplicacion de Anticipos" });
            list.Add(new ClaseGenricaCL() { Clave = "27", Des = "A satisfacción del Acreedor" });
            cboFormaPago.Properties.ValueMember = "Clave";
            cboFormaPago.Properties.DisplayMember = "Des";
            cboFormaPago.Properties.DataSource = list;
            cboFormaPago.Properties.ForceInitialize();
            cboFormaPago.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFormaPago.Properties.PopulateColumns();
            cboFormaPago.Properties.Columns["Clave"].Visible = false;
            cboFormaPago.Properties.Columns["Value"].Visible = false;
            cboFormaPago.Properties.Columns["Description"].Visible = false;
            cboFormaPago.ItemIndex = 0;
            #endregion COMBO FORMA DE PAGO
        }

        private void bbiCerrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiPrevio_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiRegresar_ItemClick(object sender, ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame1.SelectedPageIndex = 0;
        }

        private void Reporte()
        {
            globalCL cl = new globalCL();
            string result = cl.Datosdecontrol();
            int impDirecto = result == "OK" ? cl.iImpresiondirecta : 0;
            if(impDirecto == 1)
            {
                if(swResumenDetalle.IsOn)
                {
                    VentasTipoPagoResumenDesigner rep = new VentasTipoPagoResumenDesigner();
                    rep.Parameters["parameter1"].Value = Convert.ToInt32(cboClientes.EditValue);
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = Convert.ToInt32(cboAgentes.EditValue);
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboStatus.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboFormaPago.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter6"].Visible = false;
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    VentasTipoPagoDetalleDesigner rep = new VentasTipoPagoDetalleDesigner();
                    rep.Parameters["parameter1"].Value = Convert.ToInt32(cboClientes.EditValue);
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = Convert.ToInt32(cboAgentes.EditValue);
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboStatus.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboFormaPago.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter6"].Visible = false;
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
            }
            else if(impDirecto == 0) 
            {
                if(swResumenDetalle.IsOn)
                {
                    VentasTipoPagoResumenDesigner rep = new VentasTipoPagoResumenDesigner();
                    rep.Parameters["parameter1"].Value = Convert.ToInt32(cboClientes.EditValue);
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = Convert.ToInt32(cboAgentes.EditValue);
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboStatus.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboFormaPago.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter6"].Visible = false;
                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                    navigationFrame1.SelectedPageIndex = 1;
                }
                else
                {
                    VentasTipoPagoDetalleDesigner rep = new VentasTipoPagoDetalleDesigner();
                    rep.Parameters["parameter1"].Value = Convert.ToInt32(cboClientes.EditValue);
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = Convert.ToInt32(cboAgentes.EditValue);
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboStatus.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboFormaPago.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter6"].Visible = false;
                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                    navigationFrame1.SelectedPageIndex = 1;
                }
            }
        }
    }
}