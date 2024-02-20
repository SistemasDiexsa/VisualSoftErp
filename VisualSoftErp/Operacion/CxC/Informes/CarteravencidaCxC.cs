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
    public partial class CarteravencidaCxC : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CarteravencidaCxC()
        {
            InitializeComponent();

            txtFecha.Text = DateTime.Now.ToShortDateString();
            CargaCombos();
            LimpiaCajas();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();
            cl.strTabla = "Clientesrep";
            src.DataSource = cl.CargaCombos();
            cboClientesID.Properties.ValueMember = "Clave";
            cboClientesID.Properties.DisplayMember = "Des";
            cboClientesID.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboClientesID.Properties.ForceInitialize();
            cboClientesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboClientesID.Properties.PopulateColumns();
            cboClientesID.Properties.Columns["Clave"].Visible = false;
            cboClientesID.Properties.NullText = "Seleccionar un cliente";

            cl.strTabla = "Agentesrep";
            src.DataSource = cl.CargaCombos();
            cboAgentesID.Properties.ValueMember = "Clave";
            cboAgentesID.Properties.DisplayMember = "Des";
            cboAgentesID.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAgentesID.Properties.ForceInitialize();
            cboAgentesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgentesID.Properties.PopulateColumns();
            cboAgentesID.Properties.Columns["Clave"].Visible = false;
            cboAgentesID.Properties.NullText = "Seleccionar un agente";
        }

        private void LimpiaCajas()
        {
            cboClientesID.EditValue = null;
            cboAgentesID.EditValue = null;
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtDias.Text = "0";

        }

        private string Valida()
        {

            if (cboAgentesID.EditValue == null)
            {
                return "El campo Agentes no puede ir vacio";
            }
            if (cboClientesID.EditValue == null)
            {
                return "El campo Clientes no puede ir vacio";
            }
            if (txtDias.Text == "")
            {
                return "El campo Dias no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void Reporte()
        {
            try
            {
                String Result = Valida();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }

                int intClienteIni = 0, intClienteFin = 0;
                int intAgentesIni = 0, intAgentesFin = 0;

                if (Convert.ToInt32(cboClientesID.EditValue) == 0)
                {
                    intClienteIni = 0;
                    intClienteFin = 99999;
                }
                else
                {
                    intClienteIni = Convert.ToInt32(cboClientesID.EditValue);
                    intClienteFin = Convert.ToInt32(cboClientesID.EditValue);
                }

                if (Convert.ToInt32(cboAgentesID.EditValue) == 0)
                {
                    intAgentesIni = 0;
                    intAgentesFin = 99999;
                }
                else
                {
                    intAgentesIni = Convert.ToInt32(cboAgentesID.EditValue);
                    intAgentesFin = Convert.ToInt32(cboAgentesID.EditValue);
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

                CxCCarteraVencidaDesigner rep = new CxCCarteraVencidaDesigner();
                if (impDirecto == 1)
                {

                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(txtFecha.Text);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = intClienteIni;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = intClienteFin;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = intAgentesIni;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = intAgentesFin;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = 0;
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = "";
                    rep.Parameters["parameter9"].Visible = false;
                    rep.Parameters["parameter10"].Value = Convert.ToInt32(txtDias.Text);
                    rep.Parameters["parameter10"].Visible = false;

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
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(txtFecha.Text);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = intClienteIni;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = intClienteFin;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = intAgentesIni;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = intAgentesFin;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = 0;
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = "";
                    rep.Parameters["parameter9"].Visible = false;
                    rep.Parameters["parameter10"].Value = Convert.ToInt32(txtDias.Text);
                    rep.Parameters["parameter10"].Visible = false;

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
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPage1.Text);
            navigationFrame.SelectedPageIndex = 0;
        }
    }
}