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
using VisualSoftErp.Operacion.Ventas.Designers;
using DevExpress.XtraReports.UI;
using System.Threading;


namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class RemisionesRelacion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public RemisionesRelacion()
        {
            InitializeComponent();
            Cargacombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src=new BindingSource();
            cboCliente.Properties.ValueMember = "Clave";
            cboCliente.Properties.DisplayMember = "Des";
            cl.strTabla = "Clientes";
            src.DataSource = cl.CargaCombos();
            cboCliente.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopulateColumns();
            cboCliente.Properties.Columns["Clave"].Visible = false;
            cboCliente.Properties.NullText = "Seleccione un Clientes";
        }

        private void Reporte()
        {
            try
            {
                int Cliente = Convert.ToInt32(cboCliente.EditValue);

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

                RemisionesDesignerRelacion rep = new RemisionesDesignerRelacion();
                if (impDirecto == 1)
                {

                    rep.Parameters["parameter1"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Cliente;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = "x";     //Duumy
                    rep.Parameters["parameter4"].Visible = false;
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Cliente;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = "x";     //Duumy
                    rep.Parameters["parameter4"].Visible = false;
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

        private void bbiVistaPrevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Reporte();
        }

        private void bbiRegresarImpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageAcciones.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}