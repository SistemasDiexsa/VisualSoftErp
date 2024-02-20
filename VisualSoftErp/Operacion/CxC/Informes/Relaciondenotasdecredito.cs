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
    public partial class Relaciondenotasdecredito : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Relaciondenotasdecredito()
        {
            InitializeComponent();
            CargaCombos();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void CargaCombos()
        {
            globalCL clg = new globalCL();
            combosCL cl = new combosCL();
            BindingSource src = new BindingSource();
            cl.strTabla = "Clientesrep";
            src.DataSource = cl.CargaCombos();
            cboCliente.Properties.ValueMember = "Clave";
            cboCliente.Properties.DisplayMember = "Des";
            cboCliente.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCliente.Properties.PopulateColumns();
            cboCliente.Properties.Columns["Clave"].Visible = false;
            cboCliente.ItemIndex = 0;

            cl.strTabla = "Agentes";
            src.DataSource = cl.CargaCombos();
            cboAgente.Properties.ValueMember = "Clave";
            cboAgente.Properties.DisplayMember = "Des";
            cboAgente.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAgente.Properties.ForceInitialize();
            cboAgente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgente.Properties.PopulateColumns();
            cboAgente.Properties.Columns["Clave"].Visible = false;
            cboAgente.Properties.Columns["Encabezado"].Visible = false;
            cboAgente.Properties.Columns["Piedepagina"].Visible = false;
            cboAgente.Properties.Columns["Email"].Visible = false;
            cboAgente.ItemIndex = 0;
        }

        private string Valida()
        {

            if (cboCliente.EditValue == null)
            {
                return "El campo Articulo no puede ir vacio";
            }
            if (cboAgente.EditValue == null)
            {
                return "El campo AlmacenesID no puede ir vacio";
            }
            return "OK";
        }

        private void Reporte()
        {
            String Result = Valida();
            if (Result != "OK")
            {
                MessageBox.Show(Result);
                return;
            }

            try
            {
                DateTime FI, FF;
                FI = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                FF = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);


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

                RelaciondenotasdecreditoDesigner rep = new RelaciondenotasdecreditoDesigner();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(FI.ToShortDateString());
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToDateTime(FF.ToShortDateString());
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToInt32(cboCliente.EditValue);
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToInt32(cboAgente.EditValue);
                    rep.Parameters["parameter6"].Visible = false;

                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(FI.ToShortDateString());
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToDateTime(FF.ToShortDateString());
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToInt32(cboCliente.EditValue);
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToInt32(cboAgente.EditValue);
                    rep.Parameters["parameter6"].Visible = false;


                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                    navigationFrame.SelectedPageIndex = 1;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Generando informe...");
            cboCliente.Enabled=false;
            cboAgente.Enabled = false;
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.Close();
        }

        private void bbiRegresarImp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            cboCliente.Enabled = true;
            cboAgente.Enabled = true;
            navigationFrame.SelectedPageIndex = 0;
        }
    }
}