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
using DevExpress.XtraReports.UI;
using VisualSoftErp.Operacion.CxC.Designers;
using VisualSoftErp.Operacion.CxC.Clases;

namespace VisualSoftErp.Operacion.CxC.Informes
{
    public partial class Carteravencida : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Cliente = 0;
        int ClienteIn = 0;
        int ClienteFin = 0;
        int Agente = 0;
        int AgenteIn = 0;
        int AgenteFin = 0;

        void ProcessException(Exception ex, string message)
        {
            // Log exceptions here. For instance:
            System.Diagnostics.Debug.WriteLine("[{0}]: Exception occured. Message: '{1}'. Exception Details:\r\n{2}",
                DateTime.Now, message, ex);
        }
        public Carteravencida()
        {
            InitializeComponent();
            Cargacombos();
            dateEditFechaCorte.Text = DateTime.Now.ToShortDateString();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();



            cboClientes.Properties.ValueMember = "Clave";
            cboClientes.Properties.DisplayMember = "Des";
            cl.strTabla = "Clientes";
            src.DataSource = cl.CargaCombos();
            cboClientes.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboClientes.Properties.ForceInitialize();
            cboClientes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboClientes.Properties.ForceInitialize();
            cboClientes.Properties.PopulateColumns();
            cboClientes.Properties.Columns["Clave"].Visible = false;
            cboClientes.Properties.NullText = "Seleccione un Cliente";

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
            cboAgentes.Properties.NullText = "Seleccione un Agente";


        }

        private void Reporte()
        {
            try
            {
                tiposdecambioCL clt = new tiposdecambioCL();
                clt.strMoneda = "USD";
                clt.fFecha = Convert.ToDateTime(dateEditFechaCorte.Text);
                string result = clt.tiposdecambioLlenaCajas();
                if (result != "OK")
                    MessageBox.Show("No se ha capturado tipo de cambio para esta fecha de corte");

                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtDias.Text))
                    txtDias.Text = "0";

                Cliente = Convert.ToInt32(cboClientes.EditValue);
                Agente = Convert.ToInt32(cboAgentes.EditValue);


                if (Cliente== 0)
                {
                    ClienteIn = 1;
                    ClienteFin = 9999;
                }else
                {
                    ClienteIn = Cliente;
                    ClienteFin = Cliente;
                }


                if (Agente == 0)
                {
                    AgenteIn = 1;
                    AgenteFin = 9999;
                }
                else
                {
                    AgenteIn = Cliente;
                    AgenteFin = Cliente;
                }

                int impDirecto = 0;
                globalCL cl = new globalCL();
                result = cl.Datosdecontrol();
                if (result == "OK")
                {
                    impDirecto = cl.iImpresiondirecta;
                }
                else
                {
                    impDirecto = 0;
                }

                #region TRY REPORTE
                try
                {
                    CarteraVencidaResumenPorUen repR = new CarteraVencidaResumenPorUen();
                    Carteravencidadesigner rep = new Carteravencidadesigner();
                    if (impDirecto == 1)
                    {

                        rep.Parameters["parameter1"].Value = 0;
                        rep.Parameters["parameter1"].Visible = false;
                        rep.Parameters["parameter2"].Value = 0;
                        rep.Parameters["parameter2"].Visible = false;
                        rep.Parameters["parameter3"].Value = Convert.ToDateTime(dateEditFechaCorte.Text);
                        rep.Parameters["parameter3"].Visible = false;
                        rep.Parameters["parameter4"].Value = ClienteIn;
                        rep.Parameters["parameter4"].Visible = false;
                        rep.Parameters["parameter5"].Value = ClienteFin;
                        rep.Parameters["parameter5"].Visible = false;
                        rep.Parameters["parameter6"].Value = AgenteIn;
                        rep.Parameters["parameter6"].Visible = false;
                        rep.Parameters["parameter7"].Value = AgenteFin;
                        rep.Parameters["parameter7"].Visible = false;
                        rep.Parameters["parameter8"].Value = 0;
                        rep.Parameters["parameter8"].Visible = false;
                        rep.Parameters["parameter9"].Value = txtDias.Text;
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
                        rep.Parameters["parameter3"].Value = cl.fechaSQL(Convert.ToDateTime(dateEditFechaCorte.Text)); //Convert.ToDateTime(dateEditFechaCorte.Text);
                        rep.Parameters["parameter3"].Visible = false;
                        rep.Parameters["parameter4"].Value = ClienteIn;
                        rep.Parameters["parameter4"].Visible = false;
                        rep.Parameters["parameter5"].Value = ClienteFin;
                        rep.Parameters["parameter5"].Visible = false;
                        rep.Parameters["parameter6"].Value = AgenteIn;
                        rep.Parameters["parameter6"].Visible = false;
                        rep.Parameters["parameter7"].Value = AgenteFin;
                        rep.Parameters["parameter7"].Visible = false;
                        rep.Parameters["parameter8"].Value = 0;
                        rep.Parameters["parameter8"].Visible = false;
                        rep.Parameters["parameter9"].Value = txtDias.Text;
                        rep.Parameters["parameter9"].Visible = false;

                        documentViewer1.DocumentSource = rep;
                        rep.CreateDocument();
                        ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                        navigationFrame.SelectedPageIndex = 1;
                    }


                    bbiResumen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                    //Resumen UEN

                    if (impDirecto == 1)
                    {

                        repR.Parameters["parameter1"].Value = 0;
                        repR.Parameters["parameter1"].Visible = false;
                        repR.Parameters["parameter2"].Value = 0;
                        repR.Parameters["parameter2"].Visible = false;
                        repR.Parameters["parameter3"].Value = Convert.ToDateTime(dateEditFechaCorte.Text);
                        repR.Parameters["parameter3"].Visible = false;
                        repR.Parameters["parameter4"].Value = ClienteIn;
                        repR.Parameters["parameter4"].Visible = false;
                        repR.Parameters["parameter5"].Value = ClienteFin;
                        repR.Parameters["parameter5"].Visible = false;
                        repR.Parameters["parameter6"].Value = AgenteIn;
                        repR.Parameters["parameter6"].Visible = false;
                        repR.Parameters["parameter7"].Value = AgenteFin;
                        repR.Parameters["parameter7"].Visible = false;
                        repR.Parameters["parameter8"].Value = 0;
                        repR.Parameters["parameter8"].Visible = false;
                        repR.Parameters["parameter9"].Value = txtDias.Text;
                        repR.Parameters["parameter9"].Visible = false;

                        ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(repR);
                        rpt.Print();
                        return;
                    }
                    else
                    {
                        repR.Parameters["parameter1"].Value = 0;
                        repR.Parameters["parameter1"].Visible = false;
                        repR.Parameters["parameter2"].Value = 0;
                        repR.Parameters["parameter2"].Visible = false;
                        repR.Parameters["parameter3"].Value = Convert.ToDateTime(dateEditFechaCorte.Text);
                        repR.Parameters["parameter3"].Visible = false;
                        repR.Parameters["parameter4"].Value = ClienteIn;
                        repR.Parameters["parameter4"].Visible = false;
                        repR.Parameters["parameter5"].Value = ClienteFin;
                        repR.Parameters["parameter5"].Visible = false;
                        repR.Parameters["parameter6"].Value = AgenteIn;
                        repR.Parameters["parameter6"].Visible = false;
                        repR.Parameters["parameter7"].Value = AgenteFin;
                        repR.Parameters["parameter7"].Visible = false;
                        repR.Parameters["parameter8"].Value = 0;
                        repR.Parameters["parameter8"].Visible = false;
                        repR.Parameters["parameter9"].Value = txtDias.Text;
                        repR.Parameters["parameter9"].Visible = false;
                        documentViewer2.DocumentSource = repR;
                        repR.CreateDocument();
                        ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                        //navigationFrame.SelectedPageIndex = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en línea " + ex.LineNumber().ToString() + ":\n\n" + ex.Message);
                }
                #endregion TRY REPORTE
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void bbiPrevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Reporte();
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

        private void bbiResumen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiResumen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDetallado.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navigationFrame.SelectedPageIndex = 2;
        }

        private void bbiDetallado_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiResumen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiDetallado.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cliente = Convert.ToInt32(cboClientes.EditValue);
            Agente = Convert.ToInt32(cboAgentes.EditValue);


            if (Cliente == 0)
            {
                ClienteIn = 1;
                ClienteFin = 9999;
            }
            else
            {
                ClienteIn = Cliente;
                ClienteFin = Cliente;
            }


            if (Agente == 0)
            {
                AgenteIn = 1;
                AgenteFin = 9999;
            }
            else
            {
                AgenteIn = Cliente;
                AgenteFin = Cliente;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            clientesRetenidosCL cl = new clientesRetenidosCL();
            cl.dFecha = Convert.ToDateTime(dateEditFechaCorte.Text);
            cl.intCteIni = ClienteIn;
            cl.intCteFin = ClienteFin;
            cl.intAgeIni=AgenteIn;
            cl.intAgeFin = AgenteFin;
            cl.intNivel = 0;

            gridControl1.DataSource = cl.carteraVencidaGrid();
            navigationFrame.SelectedPageIndex = 4;

            bbiGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiXLS.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiRegGrid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiXLS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiXLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }
    }
}