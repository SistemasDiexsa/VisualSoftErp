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
using VisualSoftErp.Operacion.Compras.Designers;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.Compras.Informes
{
    public partial class Relaciondecomprasporarticulo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Articulos = 0;
        public Relaciondecomprasporarticulo()
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
            
            cboArticulos.Properties.ValueMember = "Clave";
            cboArticulos.Properties.DisplayMember = "Des";
            cl.strTabla = "Articulos";
            src.DataSource = cl.CargaCombos();
            cboArticulos.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboArticulos.Properties.ForceInitialize();
            cboArticulos.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulos.Properties.ForceInitialize();
            cboArticulos.Properties.PopulateColumns();
            cboArticulos.Properties.Columns["Clave"].Visible = false;
            cboArticulos.ItemIndex = 0;

        }

        private void Reporte()
        {
            try
            {
                
                Articulos = Convert.ToInt32(cboArticulos.EditValue);
               


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


                RelaciondecomprasporarticuloDesigner rep = new RelaciondecomprasporarticuloDesigner();
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
                    rep.Parameters["parameter5"].Value = Articulos;
                    rep.Parameters["parameter5"].Visible = false;
                  
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
                    rep.Parameters["parameter5"].Value = Articulos;
                    rep.Parameters["parameter5"].Visible = false;
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


        private void bbiRegresarImpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiPrevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Generando informe", "Espere por favor...");
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
    }
}