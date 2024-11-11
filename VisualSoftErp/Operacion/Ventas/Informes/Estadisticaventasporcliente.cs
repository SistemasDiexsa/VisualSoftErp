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
using VisualSoftErp.Operacion.Compras.Clases;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Operacion.Ventas.Designers;

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class Estadisticaventasporcliente : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Agente = 0;
        int Año = 0;
        int Mes = 0;
        int Cliente = 0;
        int Canaldeventa = 0;
        int CantidadOImporte = 0;// 0 igual importe 1 = cantidad
        public Estadisticaventasporcliente()
        {
            InitializeComponent();
            Cargacombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            List<ClaseGenricaCL> ListadoMeses = new List<ClaseGenricaCL>();
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "0", Des = "Todos" });
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

            #region AGENTES
            BindingSource src = new BindingSource();
            cboAgentes.Properties.ValueMember = "Clave";
            cboAgentes.Properties.DisplayMember = "Des";
            cl.strTabla = "Agentes";
            src.DataSource = cl.CargaCombos();
            cboAgentes.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAgentes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgentes.Properties.ForceInitialize();
            cboAgentes.Properties.PopulateColumns();
            cboAgentes.Properties.Columns["Clave"].Visible = false;
            cboAgentes.Properties.Columns["Encabezado"].Visible = false;
            cboAgentes.Properties.Columns["Piedepagina"].Visible = false;
            cboAgentes.Properties.Columns["Email"].Visible = false;
            cboAgentes.ItemIndex = 0;
            #endregion AGENTES

            #region CLIENTES
            cboClientes.Properties.ValueMember = "Clave";
            cboClientes.Properties.DisplayMember = "Des";
            cl.strTabla = "Clientes";
            src.DataSource = cl.CargaCombos();
            cboClientes.Properties.DataSource = clg.AgregarOpcionTodos(src);
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
            cboClientes.Properties.Columns["Desglosardescuentoalfacturar"].Visible = false;
            cboClientes.Properties.Columns["TransportesID"].Visible = false;
            cboClientes.Properties.Columns["Addenda"].Visible = false;
            cboClientes.Properties.Columns["SerieEle"].Visible = false;
            cboClientes.Properties.Columns["CanalesdeventaID"].Visible = false;
            cboClientes.ItemIndex = 0;
            #endregion CLIENTES

            #region AÑOS
            cboAños.Properties.ValueMember = "Clave";
            cboAños.Properties.DisplayMember = "Des";
            cl.strTabla = "Añosventas";
            src.DataSource = cl.CargaCombos();
            cboAños.Properties.DataSource = src.DataSource;
            cboAños.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAños.Properties.ForceInitialize();
            cboAños.Properties.PopulateColumns();
            cboAños.Properties.Columns["Clave"].Visible = false;
            cboAños.ItemIndex = 0;
            #endregion AÑOS

            #region MESES
            cboMeses.Properties.ValueMember = "Clave";
            cboMeses.Properties.DisplayMember = "Des";
            cboMeses.Properties.DataSource = ListadoMeses;
            cboMeses.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMeses.Properties.ForceInitialize();
            cboMeses.Properties.PopulateColumns();
            cboMeses.Properties.Columns["Clave"].Visible = false;
            cboMeses.Properties.Columns["Description"].Visible = false;
            cboMeses.Properties.Columns["Value"].Visible = false;
            cboMeses.ItemIndex = DateTime.Now.Month;
            #endregion MESES

            #region CANAL DE VENTA
            cboCanaldeventas.Properties.ValueMember = "Clave";
            cboCanaldeventas.Properties.DisplayMember = "Des";
            cl.strTabla = "Canalesdeventa";
            src.DataSource = cl.CargaCombos();
            cboCanaldeventas.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboCanaldeventas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCanaldeventas.Properties.ForceInitialize();
            cboCanaldeventas.Properties.PopulateColumns();
            cboCanaldeventas.Properties.Columns["Clave"].Visible = false;
            cboCanaldeventas.ItemIndex = 0;
            #endregion CANAL DE VENTA
        }

        private void Reporte()
        {
            try
            {
                 Agente = Convert.ToInt32(cboAgentes.EditValue);
                 Canaldeventa = Convert.ToInt32(cboCanaldeventas.EditValue);
                 Cliente = Convert.ToInt32(cboClientes.EditValue);
                 Año = Convert.ToInt32(cboAños.EditValue);
                 Mes = Convert.ToInt32(cboMeses.EditValue);

                int impDirecto = 0;
                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                if (result == "OK")
                    impDirecto = cl.iImpresiondirecta;
                else
                    impDirecto = 0;

                string IMPCANT = string.Empty;
                if (swCantidadOImporte.IsOn)
                    IMPCANT = "Importe";
                else
                    IMPCANT = "Cantidad";

                EstadisticaventasporclienteDesigner rep = new EstadisticaventasporclienteDesigner();
                rep.DefaultPrinterSettingsUsing.UseLandscape = false;
                rep.Landscape = true;
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
                rep.Parameters["parameter7"].Value = Canaldeventa;    //Duumy
                rep.Parameters["parameter7"].Visible = false;
                rep.Parameters["parameter8"].Value = Canaldeventa;    //Duumy
                rep.Parameters["parameter8"].Visible = false;
                rep.Parameters["parameter9"].Value = "Resumen";    // resmen o detalle
                rep.Parameters["parameter9"].Visible = false;
                rep.Parameters["parameter10"].Value = IMPCANT;   // importe o cantidad
                rep.Parameters["parameter10"].Visible = false;

                if (impDirecto == 1)
                {
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
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
        }
        
        private void ReporteDetallado()
        {
            try
            {
                Agente = Convert.ToInt32(cboAgentes.EditValue);
                Canaldeventa = Convert.ToInt32(cboCanaldeventas.EditValue);
                Cliente = Convert.ToInt32(cboClientes.EditValue);
                Año = Convert.ToInt32(cboAños.EditValue);
                Mes = Convert.ToInt32(cboMeses.EditValue);
                CantidadOImporte = swCantidadOImporte.IsOn ? 0 : 1;

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

                VentasEstadisticaPorClienteDetalladoDesigner rep = new VentasEstadisticaPorClienteDetalladoDesigner();


                rep.DefaultPrinterSettingsUsing.UseLandscape = false;
                rep.Landscape = true;

                if (impDirecto == 1)
                {

                    string IMPCANT;
                    if (swCantidadOImporte.IsOn)
                    {
                        IMPCANT = "Importe";
                    }
                    else
                    {
                        IMPCANT = "Cantidad";
                    }

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
                    rep.Parameters["parameter8"].Value = CantidadOImporte.ToString();     //Duumy
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = "Detallado";    // resmen o detalle
                    rep.Parameters["parameter9"].Visible = false;
                    rep.Parameters["parameter10"].Value = IMPCANT;   // importe o cantidad
                    rep.Parameters["parameter10"].Visible = false;
                    rep.Parameters["parameter11"].Value = cboAños.Text;  //  año
                    rep.Parameters["parameter11"].Visible = false;
                    rep.Parameters["parameter12"].Value = cboMeses.Text;   // mes
                    rep.Parameters["parameter12"].Visible = false;

                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    string IMPCANT;
                    if (swCantidadOImporte.IsOn)
                    {
                        IMPCANT = "Importe";
                    }
                    else
                    {
                        IMPCANT = "Cantidad";
                    }

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
                    rep.Parameters["parameter8"].Value = CantidadOImporte.ToString();      //Duumy
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = "Detallado";    // resmen o detalle
                    rep.Parameters["parameter9"].Visible = false;
                    rep.Parameters["parameter10"].Value = IMPCANT;   // importe o cantidad
                    rep.Parameters["parameter10"].Visible = false;
                    rep.Parameters["parameter11"].Value = cboAños.Text;  //  año
                    rep.Parameters["parameter11"].Visible = false;
                    rep.Parameters["parameter12"].Value = cboMeses.Text;   // mes
                    rep.Parameters["parameter12"].Visible = false;
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

        private void bbiPrevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            if(swResumenDetalle.IsOn)
                this.Reporte();
            else
                this.ReporteDetallado();
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
    }
}