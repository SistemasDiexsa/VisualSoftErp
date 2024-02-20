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
using VisualSoftErp.Operacion.CxP.Designers;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.CxP.Informes
{
    public partial class CxPAntiguedaddesaldos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CxPAntiguedaddesaldos()
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

            //Canalesdeventa
            cboProveedor.Properties.ValueMember = "Clave";
            cboProveedor.Properties.DisplayMember = "Des";
            cl.strTabla = "Proveedoresrep";
            src.DataSource = cl.CargaCombos();
            cboProveedor.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboProveedor.Properties.ForceInitialize();
            cboProveedor.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedor.Properties.ForceInitialize();
            cboProveedor.Properties.PopulateColumns();
            cboProveedor.Properties.Columns["Clave"].Visible = false;
            cboProveedor.ItemIndex = 0;
        }

        private void bbiProcesar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }   


        private void Reporte()
        {
            try
            {
                
                int Proveedor = Convert.ToInt32(cboProveedor.EditValue);

                int ProvI = Proveedor;
                int ProvF = Proveedor;
           

                if (Proveedor == 0)
                {
                    ProvI = 0;
                    ProvF = 99999;
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

                AntiguedaddesaldosCxP rep = new AntiguedaddesaldosCxP();
                if (impDirecto == 1)
                {

                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(dateEditFecha.Text);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = ProvI;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = ProvF;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = 1;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = "";
                    rep.Parameters["parameter7"].Visible = false;
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
                    rep.Parameters["parameter4"].Value = ProvI;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = ProvF;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = 1;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = "";
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

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}