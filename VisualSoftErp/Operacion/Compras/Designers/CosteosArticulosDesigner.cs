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
    public partial class CosteosArticulosDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        public CosteosArticulosDesigner()
        {
            InitializeComponent();
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
