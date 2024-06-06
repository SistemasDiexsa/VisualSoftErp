using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Compras.Designers
{
    public partial class PrecioInsumosRepDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        public PrecioInsumosRepDesigner()
        {
            InitializeComponent();
        }

        private void xrLabel3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToInt32(parameter1.Value) == 0)
                xrLabel3.Text = "Todas las líneas";
        }

        private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToInt32(parameter2.Value) == 0)
                xrLabel5.Text = "Todas las familias";
        }

        private void xrLabel7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToInt32(parameter3.Value) == 0)
                xrLabel7.Text = "Todas las subfamilias";
        }

        private void xrLabel10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string fecha = Convert.ToDateTime(parameter4.Value).ToString("dd - MMMM - yyyy");
            xrLabel10.Text = fecha;
        }

        private void xrPictureBoxLogo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBoxLogo.ImageUrl = ConfigurationManager.AppSettings["logoEmpresa"].ToString();
        }

        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            string strCnn = globalCL.gv_strcnn;
            CustomStringConnectionParameters parameters = new CustomStringConnectionParameters(strCnn);
            if (e.ConnectionName == "VisualSoftErpConnectionString")
                e.ConnectionParameters = parameters;
        }
    }
}
