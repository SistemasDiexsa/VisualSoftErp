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
using VisualSoftErp.Operacion.CxC.Designers;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.CxC.Informes
{
    public partial class CuentasporcobrarCxC : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Cliente = 0;

        public CuentasporcobrarCxC()
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
            cboClientes.Properties.DataSource = clg.AgregarOpcionTodos(src);
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
            cboClientes.ItemIndex = 0;      
        }

        private void Reporte()
        {
            try
            {

                Cliente = Convert.ToInt32(cboClientes.EditValue);

                DateTime fechaInicial;
                if (swAtrasadas.IsOn)
                {
                    fechaInicial = Convert.ToDateTime(DateTime.Now.Date.AddDays(-1500));
                }
                else
                {
                    fechaInicial= Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                }

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

                CuentasporcobrarDesignerCxC rep = new CuentasporcobrarDesignerCxC();
                if (impDirecto == 1)
                {

                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = fechaInicial;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Cliente;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = "x";
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
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
                    rep.Parameters["parameter3"].Value = fechaInicial;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Cliente;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = "x";
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
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


        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiRegresarImpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiPrevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Reporte();
        }
    }
}