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
using VisualSoftErp.Operacion.Inventarios.Designers;

namespace VisualSoftErp.Operacion.Inventarios.Informes
{
    public partial class RelaciondepagosCxP : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string strNiveldeinformacion;
        int impDirecto = 0;
        DateTime FI, FF;
        public RelaciondepagosCxP()
        {
            InitializeComponent();

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
            cboProveedor.Properties.ValueMember = "Clave";
            cboProveedor.Properties.DisplayMember = "Des";
            cboProveedor.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboProveedor.Properties.ForceInitialize();
            cboProveedor.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedor.Properties.PopulateColumns();
            cboProveedor.Properties.Columns["Clave"].Visible = false;
            cboProveedor.ItemIndex = 0;

            cl.strTabla = "cyb_Chequeras";
            src.DataSource = cl.CargaCombos();
            cboChequera.Properties.ValueMember = "Clave";
            cboChequera.Properties.DisplayMember = "Des";
            cboChequera.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboChequera.Properties.ForceInitialize();
            cboChequera.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboChequera.Properties.PopulateColumns();
            cboChequera.Properties.Columns["Clave"].Visible = false;
            cboChequera.ItemIndex = 0;



        }

        private string Valida()
        {


            if (cboProveedor.EditValue == null)
            {
                return "El campo Cliente no puede ir vacio";
            }

            if (cboChequera.EditValue == null)
            {
                return "El campo Agente no puede ir vacio";
            }



            return "OK";
        }

        public class tipoCL
        {
            public string Clave { get; set; }
            public string Des { get; set; }
        }

        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FI = DateTime.Now;
            FF = DateTime.Now;
            Reporte();
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

                FI = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                FF = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);

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

                RelaciondepagosDesigner rep = new RelaciondepagosDesigner();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = FI;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = FF;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToInt32(cboProveedor.EditValue);
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToInt32(cboChequera.EditValue);
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
                    rep.Parameters["parameter3"].Value = FI;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = FF;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToInt32(cboProveedor.EditValue);
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToInt32(cboChequera.EditValue);
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = "";
                    rep.Parameters["parameter7"].Visible = false;
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


        private void bbiRegresarImp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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