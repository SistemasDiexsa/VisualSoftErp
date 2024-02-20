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
using VisualSoftErp.Operacion.CxP.Designers;

namespace VisualSoftErp.Operacion.CxC.Informes
{
    public partial class EstadosdecuentaCxP : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Cliente = 0;
        public EstadosdecuentaCxP()
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



            cboProveedor.Properties.ValueMember = "Clave";
            cboProveedor.Properties.DisplayMember = "Des";
            cl.strTabla = "Proveedoresrep";
            src.DataSource = cl.CargaCombos();
            cboProveedor.Properties.DataSource = src;
            cboProveedor.Properties.ForceInitialize();
            cboProveedor.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedor.Properties.ForceInitialize();
            cboProveedor.Properties.PopulateColumns();
            cboProveedor.Properties.Columns["Clave"].Visible = false; //con esta propiedad puedo ocultar campos en el cbo    
            //cboProveedor.Properties.Columns["Piva"].Visible = false;
            //cboProveedor.Properties.Columns["Plazo"].Visible = false;
            //cboProveedor.Properties.Columns["Tiempodeentrega"].Visible = false;
            //cboProveedor.Properties.Columns["Lab"].Visible = false;
            //cboProveedor.Properties.Columns["Via"].Visible = false;
            //cboProveedor.Properties.Columns["BancosID"].Visible = false;
            //cboProveedor.Properties.Columns["Cuentabancaria"].Visible = false;
            //cboProveedor.Properties.Columns["C_Formapago"].Visible = false;
            //cboProveedor.Properties.Columns["MonedasID"].Visible = false;
            //cboProveedor.Properties.Columns["Retiva"].Visible = false;
            //cboProveedor.Properties.Columns["Retisr"].Visible = false;

            cboProveedor.Properties.NullText = "Seleccione un proveedor";


        }

        private void Reporte()
        {
            try
            {
                if (cboProveedor.EditValue == null)
                {
                    MessageBox.Show("Seleccione un cliente");
                    return;
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

                CxPEstadodecuentaDesigner rep = new CxPEstadodecuentaDesigner();
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
                    rep.Parameters["parameter5"].Value = Convert.ToInt32(cboProveedor.EditValue); ;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = "";
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
                    rep.Parameters["parameter5"].Value = Convert.ToInt32(cboProveedor.EditValue); ;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = "";
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