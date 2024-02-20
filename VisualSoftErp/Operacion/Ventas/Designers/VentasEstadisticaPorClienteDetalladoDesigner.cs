using DevExpress.CodeParser;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace VisualSoftErp.Operacion.Ventas.Designers
{
    public partial class VentasEstadisticaPorClienteDetalladoDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        decimal Total = 0;
        public VentasEstadisticaPorClienteDetalladoDesigner()
        {
            InitializeComponent();
        }
   

        private void xrTableCell3_BeforePrint_1(object sender, CancelEventArgs e)
        {

            //xrTableCell3.Text = Total.ToString();
            //Total= 0;   
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

            //if (GetCurrentColumnValue("ENE") != null)
            //    Total += Convert.ToDecimal(GetCurrentColumnValue("ENE"));
            //if (GetCurrentColumnValue("FEB") != null)
            //    Total += Convert.ToDecimal(GetCurrentColumnValue("FEB"));
            //if (GetCurrentColumnValue("MAR") != null)
            //    Total += Convert.ToDecimal(GetCurrentColumnValue("MAR"));
            //if (GetCurrentColumnValue("ABR") != null)
            //    Total += Convert.ToDecimal(GetCurrentColumnValue("ABR"));
            //if (GetCurrentColumnValue("MAY") != null)
            //    Total += Convert.ToDecimal(GetCurrentColumnValue("MAY"));
            //if (GetCurrentColumnValue("JUN") != null)
            //    Total += Convert.ToDecimal(GetCurrentColumnValue("JUN"));
            //if (GetCurrentColumnValue("JUL") != null)
            //    Total += Convert.ToDecimal(GetCurrentColumnValue("JUL"));
            //if (GetCurrentColumnValue("AGO") != null)
            //    Total += Convert.ToDecimal(GetCurrentColumnValue("AGO"));
            //if (GetCurrentColumnValue("SEP") != null)
            //    Total += Convert.ToDecimal(GetCurrentColumnValue("SEP"));
            //if (GetCurrentColumnValue("OCT") != null)
            //    Total += Convert.ToDecimal(GetCurrentColumnValue("OCT"));
            //if (GetCurrentColumnValue("NOV") != null)
            //    Total += Convert.ToDecimal(GetCurrentColumnValue("NOV"));
            //if (GetCurrentColumnValue("DIC") != null)
            //    Total += Convert.ToDecimal(GetCurrentColumnValue("DIC"));
        }

        private void xrPictureBoxLogo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string pathLogo = System.Configuration.ConfigurationManager.AppSettings["logoEmpresa"].ToString();
            xrPictureBoxLogo.ImageUrl = pathLogo;
                      
        }
    }
}