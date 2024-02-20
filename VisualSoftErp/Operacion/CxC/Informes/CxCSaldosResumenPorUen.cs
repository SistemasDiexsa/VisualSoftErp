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
    public partial class CxCSaldosResumenPorUen : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CxCSaldosResumenPorUen()
        {
            InitializeComponent();

            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            LimpiaCajas();
            CargaCombos();
            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();
            cl.strTabla = "TiposUEN";
            src.DataSource = cl.CargaCombos();
            cboTiposUEN.Properties.ValueMember = "Clave";
            cboTiposUEN.Properties.DisplayMember = "Des";
            cboTiposUEN.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboTiposUEN.Properties.ForceInitialize();
            cboTiposUEN.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTiposUEN.Properties.PopulateColumns();
            cboTiposUEN.Properties.Columns["Clave"].Visible = false;
            cboTiposUEN.ItemIndex = 0;
        }


        private void LimpiaCajas()
        {

            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            cboTiposUEN.EditValue = null;

        }

        private string Valida()
        {


            if (cboTiposUEN.EditValue == null)
            {
                return "El campo Uen no puede ir vacio";
            }
            if (dateEditFecha.Text == null)
            {
                return "El campo Fecha no puede ir vacio";
            }

            globalCL clg = new globalCL();
            if (!clg.esFecha(dateEditFecha.Text))
                return "Selccione una fecha correcta";

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

                CxCSaldosResumenPorUenDesigner rep = new CxCSaldosResumenPorUenDesigner();
                if (impDirecto == 1)
                {

                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboTiposUEN.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToDateTime(dateEditFecha.Text);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = "";
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
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboTiposUEN.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToDateTime(dateEditFecha.Text);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = "";
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