using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.CxC.Designers
{
    public partial class CorteDiaFacturasDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        public CorteDiaFacturasDesigner()
        {
            InitializeComponent();
        }

        private void sqlDataSource1_ConfigureDataConnection_1(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            string VisualSoftErpConnectionString = globalCL.gv_strcnn;
            CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(VisualSoftErpConnectionString);

            if (e.ConnectionName == "VisualSoftErpConnectionString")
                e.ConnectionParameters = connectionParameters;
        }
    }
}
