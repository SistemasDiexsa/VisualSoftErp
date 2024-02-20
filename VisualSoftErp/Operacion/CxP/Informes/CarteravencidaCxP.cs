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
using VisualSoftErp.Operacion.CxP.Designers;

namespace VisualSoftErp.Operacion.CxP.Informes
{
    public partial class CarteravencidaCxP : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CarteravencidaCxP()
        {
            InitializeComponent();
            txtFecha.Text = DateTime.Now.ToShortDateString();
            CargaCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();
            cl.strTabla = "Proveedoresrep";

            src.DataSource = cl.CargaCombos();
            cboProveedoresID.Properties.ValueMember = "Clave";
            cboProveedoresID.Properties.DisplayMember = "Des";
            cboProveedoresID.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboProveedoresID.Properties.ForceInitialize();
            cboProveedoresID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedoresID.Properties.PopulateColumns();
            cboProveedoresID.Properties.Columns["Clave"].Visible = false;
            cboProveedoresID.ItemIndex = 0;
   

        }

        private void LimpiaCajas()
        {
            cboProveedoresID.EditValue = null;
            txtFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void Reporte()
        {
            try
            {

                globalCL clg = new globalCL();
                int dias = 0;

                if (!clg.esNumerico(txtDias.Text))
                    txtDias.Text = "0";

                dias = Convert.ToInt32(txtDias.Text);

                if (cboProveedoresID.EditValue == null)
                {
                    MessageBox.Show("Seleccione un proveedor");
                    return;
                }

                int intProvIni = 0, intProvFin=0;

                if (Convert.ToInt32(cboProveedoresID.EditValue) == 0)
                {
                    intProvIni = 0;
                    intProvFin = 99999;
                }
                else
                {
                    intProvIni = Convert.ToInt32(cboProveedoresID.EditValue);
                    intProvFin = Convert.ToInt32(cboProveedoresID.EditValue);
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

                CxpCarteraVencidaDesigner rep = new CxpCarteraVencidaDesigner();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(txtFecha.Text);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = intProvIni;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = intProvFin;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = 0;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = dias;
                    rep.Parameters["parameter7"].Visible = false;

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
                    rep.Parameters["parameter4"].Value = intProvIni;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = intProvFin;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = 0;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = dias;
                    rep.Parameters["parameter7"].Visible = false;

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
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Generando informe...");
            this.Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
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