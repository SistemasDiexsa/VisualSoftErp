using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.CxC.Designers;
using VisualSoftErp.Operacion.CxP.Informes;

namespace VisualSoftErp.Operacion.CxC.Informes
{
    public partial class CorteDia : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public CorteDia()
        {
            InitializeComponent();
            tpFecha.Value = DateTime.Now;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiPrevio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

        private void swFechaRango_Toggled(object sender, EventArgs e)
        {
            if(swFechaRango.IsOn)
                vsFiltroFechas1.Visible = true;
            else
                vsFiltroFechas1.Visible = false;
        }

        private void Reporte()
        {
            DateTime fechaInicial;
            DateTime fechaFinal;
            if(swFechaRango.IsOn)
            {
                fechaInicial = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                fechaFinal = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
            }
            else
            {
                fechaInicial = tpFecha.Value.Date;
                fechaFinal = tpFecha.Value.Date;
            }

            globalCL cl = new globalCL();
            string result = cl.Datosdecontrol();
            int impDirecto = result == "OK" ? cl.iImpresiondirecta : 0;
            if (impDirecto == 1)
            {
                CorteDiaDesigner rep = new CorteDiaDesigner();
                rep.Parameters["parameter1"].Value = fechaInicial;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = fechaFinal;
                rep.Parameters["parameter2"].Visible = false;
                ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                rpt.Print();
                return;
            }
            else
            {
                CorteDiaDesigner rep = new CorteDiaDesigner();
                rep.Parameters["parameter1"].Value = fechaInicial;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = fechaFinal;
                rep.Parameters["parameter2"].Visible = false;
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                navigationFrame.SelectedPageIndex = 1;
            }
        }

    }
}