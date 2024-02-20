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
using VisualSoftErp.Operacion.Inventarios.Designers;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.Inventarios.Informes
{
    public partial class Inventariocomparativo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Inventariocomparativo()
        {
            InitializeComponent();
            txtFechadeconteo.Text = DateTime.Now.ToString();
            txtMarbete.Text = "0";
            txtNumero.Text = "1";
            CargaCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void CargaCombos()
        {
            List<tipoCL> tipoL = new List<tipoCL>();

            tipoL.Add(new tipoCL() { Clave = 1, Des = "Solo diferencias" });
            tipoL.Add(new tipoCL() { Clave = 0, Des = "Todos" });
            cboOpcion.Properties.ValueMember = "Clave";
            cboOpcion.Properties.DisplayMember = "Des";
            cboOpcion.Properties.DataSource = tipoL;
            cboOpcion.Properties.ForceInitialize();
            cboOpcion.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboOpcion.Properties.PopulateColumns();
            cboOpcion.Properties.Columns["Clave"].Visible = false;
            cboOpcion.ItemIndex = 0;          
        }

        private string Valida()
        {
            if (txtFechadeconteo.Text == null)
            {
                return "El campo fecha de conteo no puede ir vacio";
            }

            if (txtMarbete.Text == "")
            {
                return "El campo Marbete no puede ir vacio";
            }

            if (txtNumero.Text == "")
            {
                return "El campo numero no puede ir vacio";
            }


            if (txtFechadeconteo.Text == null)
            {
                return "El campo fecha de corte no puede ir vacio";
            }



            return "OK";
        }

        public class tipoCL
        {
            public int Clave { get; set; }
            public string Des { get; set; }
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

                globalCL clG = new globalCL();
                if (!clG.esNumerico(txtMarbete.Text))
                {
                    txtMarbete.Text = "0";
                }
                if (!clG.esNumerico(txtNumero.Text))
                {
                    MessageBox.Show("Teclee el número de la toma de inventario");
                    return;
                }

                if (!clG.esFecha(txtFechadeconteo.Text))
                {
                    MessageBox.Show("Teclee la fecha de conteo del inventario");
                    return;
                }

                DateTime fecha = Convert.ToDateTime(txtFechadeconteo.Text);
                int Marbete = Convert.ToInt32(txtMarbete.Text);
                int MarbI = Marbete;
                int MarbF = Marbete;

                if (Marbete == 0)  //Tdoos
                {
                    MarbI = 0;
                    MarbF = 99999;
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

                InventariocomparativoDesigner rep = new InventariocomparativoDesigner();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = fecha;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = Convert.ToInt32(txtNumero.Text);
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = MarbI;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = MarbF;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToInt32(cboOpcion.EditValue);
                    rep.Parameters["parameter5"].Visible = false;
              
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = fecha;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = Convert.ToInt32(txtNumero.Text);
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = MarbI;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = MarbF;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToInt32(cboOpcion.EditValue);
                    rep.Parameters["parameter5"].Visible = false;

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
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Generando informe...");
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiRegresarImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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