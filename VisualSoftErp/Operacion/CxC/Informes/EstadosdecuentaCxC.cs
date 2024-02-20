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

namespace VisualSoftErp.Operacion.CxC.Informes
{
    public partial class EstadosdecuentaCxC : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Cliente = 0;
        public EstadosdecuentaCxC()
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



            cboClientes.Properties.ValueMember = "Clave";
            cboClientes.Properties.DisplayMember = "Des";
            cl.strTabla = "Clientes";
            src.DataSource = cl.CargaCombos();
            cboClientes.Properties.DataSource = src;
            cboClientes.Properties.ForceInitialize();
            cboClientes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboClientes.Properties.ForceInitialize();
            cboClientes.Properties.PopulateColumns();
            cboClientes.Properties.Columns["Clave"].Visible = false; //con esta propiedad puedo ocultar campos en el cbo
            cboClientes.Properties.Columns["AgentesID"].Visible = false;
            cboClientes.Properties.Columns["Plazo"].Visible = false;
            cboClientes.Properties.Columns["Listadeprecios"].Visible = false;
            cboClientes.Properties.Columns["Exportar"].Visible = false;
            cboClientes.Properties.Columns["cFormapago"].Visible = false;
            cboClientes.Properties.Columns["cMetodopago"].Visible = false;
            cboClientes.Properties.Columns["BancoordenanteID"].Visible = false;
            cboClientes.Properties.Columns["Cuentaordenante"].Visible = false;
            cboClientes.Properties.Columns["PIva"].Visible = false;
            cboClientes.Properties.Columns["PIeps"].Visible = false;
            cboClientes.Properties.Columns["PRetiva"].Visible = false;
            cboClientes.Properties.Columns["EMail"].Visible = false;
            cboClientes.Properties.Columns["UsoCfdi"].Visible = false;
            cboClientes.Properties.Columns["PRetIsr"].Visible = false;
            cboClientes.Properties.Columns["cFormapagoDepositos"].Visible = false;
            cboClientes.Properties.Columns["Moneda"].Visible = false;
            cboClientes.Properties.NullText = "Seleccione un Cliente";


        }

        private void Reporte()
        {
            try
            {
                if (cboClientes.EditValue == null)
                {
                    MessageBox.Show("Seleccione un cliente");
                    return;
                }
                Cliente = Convert.ToInt32(cboClientes.EditValue);

              
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

                EstadosdecuentaDesignerCxC rep = new EstadosdecuentaDesignerCxC();
                if (impDirecto == 1)
                {

                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Cliente;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = "x";
                    rep.Parameters["parameter6"].Visible = false;

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
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Cliente;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = "x";
                    rep.Parameters["parameter6"].Visible = false;
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
    }
}