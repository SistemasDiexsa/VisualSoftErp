using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.CxC.Designers
{
    public partial class CorteDiaDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        public CorteDiaDesigner()
        {
            InitializeComponent();
        }

        private void labelFecha_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string fechaInicial = Convert.ToDateTime(parameter1.Value).ToString("dddd dd 'de' MMMM 'del' yyyy");
            string fechaFinal = Convert.ToDateTime(parameter2.Value).ToString("dddd dd 'de' MMMM 'del' yyyy");

            if (fechaInicial != fechaFinal)
                labelFecha.Text = "FECHA:   " + fechaInicial + " a " + fechaFinal;
            else if (fechaFinal == fechaInicial)
                labelFecha.Text = "FECHA: " + fechaInicial;
        }

        private void xrSubreportFacturas_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRSubreport subRep = sender as XRSubreport;
            if(subRep != null)
            {
                CorteDiaFacturasDesigner subreport = new CorteDiaFacturasDesigner();
                subRep.ReportSource = subreport;
                if (subRep.ReportSource != null)
                {
                    subreport.parameter1.Value = parameter1.Value;
                    subreport.parameter2.Value = parameter2.Value;
                }
            }
        }

        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            string VisualSoftErpConnectionString = globalCL.gv_strcnn;
            CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(VisualSoftErpConnectionString);

            if (e.ConnectionName == "VisualSoftErpConnectionString")
                e.ConnectionParameters = connectionParameters;
            
        }
    }
}
