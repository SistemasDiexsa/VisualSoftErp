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

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class Antiguedaddesaldoscxc : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Antiguedaddesaldoscxc()
        {
            InitializeComponent();
            Cargacombos();
            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();
            cboAgente.Properties.ValueMember = "Clave";
            cboAgente.Properties.DisplayMember = "Des";
            cl.strTabla = "Agentes";
            src.DataSource = cl.CargaCombos();
            cboAgente.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAgente.Properties.ForceInitialize();
            cboAgente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgente.Properties.ForceInitialize();
            cboAgente.Properties.PopulateColumns();
            cboAgente.Properties.Columns["Clave"].Visible = false;
            cboAgente.ItemIndex = 0;


            //Canalesdeventa
            cboCliente.Properties.ValueMember = "Clave";
            cboCliente.Properties.DisplayMember = "Des";
            cl.strTabla = "ClientesRep";
            src.DataSource = cl.CargaCombos();
            cboCliente.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopulateColumns();
            cboCliente.Properties.Columns["Clave"].Visible = false; //con esta propiedad puedo ocultar campos en el cbo
            //cboCliente.Properties.Columns["AgentesID"].Visible = false;
            //cboCliente.Properties.Columns["Plazo"].Visible = false;
            //cboCliente.Properties.Columns["Listadeprecios"].Visible = false;
            //cboCliente.Properties.Columns["Exportar"].Visible = false;
            //cboCliente.Properties.Columns["cFormapago"].Visible = false;
            //cboCliente.Properties.Columns["cMetodopago"].Visible = false;
            //cboCliente.Properties.Columns["BancoordenanteID"].Visible = false;
            //cboCliente.Properties.Columns["Cuentaordenante"].Visible = false;
            //cboCliente.Properties.Columns["PIva"].Visible = false;
            //cboCliente.Properties.Columns["PIeps"].Visible = false;
            //cboCliente.Properties.Columns["PRetiva"].Visible = false;
            //cboCliente.Properties.Columns["EMail"].Visible = false;
            //cboCliente.Properties.Columns["UsoCfdi"].Visible = false;
            //cboCliente.Properties.Columns["PRetIsr"].Visible = false;
            //cboCliente.Properties.Columns["cFormapagoDepositos"].Visible = false;
            //cboCliente.Properties.Columns["Moneda"].Visible = false;
            cboCliente.ItemIndex = 0;
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiProcesar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Informes","Generando informe...");
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Reporte()
        {
            try
            {
                int Agente = Convert.ToInt32(cboAgente.EditValue);
                int Cliente = Convert.ToInt32(cboCliente.EditValue);

                int CteI=Cliente;
                int CteF = Cliente;
                int AgeI=Agente;
                int AgeF = Agente;

                if (Cliente==0)
                {
                    CteI = 0;
                    CteF = 99999;
                }
                if (Agente==0)
                {
                    AgeI = 0;
                    AgeF = 99999;
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

                cxcAntiguedaddesaldosRep rep = new cxcAntiguedaddesaldosRep();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(dateEditFecha.Text);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = CteI;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = CteF;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = AgeI;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = AgeF;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = 0;     //Nivel info
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = "x";     //Duumy
                    rep.Parameters["parameter9"].Visible = false;
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
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(dateEditFecha.Text);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = CteI;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = CteF;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = AgeI;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = AgeF;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = 0;     //Nivel info
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = "x";     //Duumy
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

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }
    }
}