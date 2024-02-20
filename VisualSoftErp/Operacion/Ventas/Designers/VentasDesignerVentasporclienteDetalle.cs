using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Clases.HerrramientasCLs;
using DevExpress.DataAccess.ConnectionParameters;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Ventas.Designers
{
    public partial class VentasDesignerVentasporclienteDetalle : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
        decimal decSubtotalAjustado = 0;
        decimal decTotalNcr = 0;
        decimal decTotSub = 0;
        decimal decTotalTotalNcr = 0;
        decimal ST = 0;

        public VentasDesignerVentasporclienteDetalle()
        {
            InitializeComponent();
            stipoLogo = "C";
            DatosdecontrolCL cl = new DatosdecontrolCL();
            string result = cl.DatosdecontrolLeer();
            if (result == "OK")
            {
                dbr = cl.dbr;
                dbg = cl.dbg;
                dbb = cl.dbb;
                dfr = cl.dfr;
                dfg = cl.dfg;
                dfb = cl.dfb;
                stipoLogo = cl.sTipologo;
            }
        }

        private void xrPictureBoxLogo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string pathLogo = System.Configuration.ConfigurationManager.AppSettings["logoEmpresa"].ToString();
            xrPictureBoxLogo.ImageUrl = pathLogo;

            if (stipoLogo == "L")
            {
                xrPictureBoxLogo.WidthF = 237;
                xrPictureBoxLogo.HeightF = 47;
            }
        }

        private void xrLblEmp_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (stipoLogo == "L")
            {
                xrLblEmp.LocationF = new PointF(10, xrPictureBoxLogo.LocationF.Y + xrPictureBoxLogo.HeightF + 1);
            }
            else
            {
                xrLblEmp.LocationF = new PointF(xrPictureBoxLogo.LocationF.X + xrPictureBoxLogo.WidthF + 80, 5);
            }
        }

        private void tableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell5.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell5.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell6.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell6.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell9.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell9.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell2.BackColor = Color.FromArgb(dbr, dbg, dbb);
            xrTableCell2.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell8.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell8.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //decSubtotalAjustado = Convert.ToDecimal(GetCurrentColumnValue("SubTotal")) - Convert.ToDecimal(GetCurrentColumnValue("TotalNCR"));
        }

        private void SubtotalAjustado_GetValue(object sender, GetValueEventArgs e)
        {
           
        }

        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            try
            {
                string VisualSoftErpConnectionString = globalCL.gv_strcnn;
                CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(VisualSoftErpConnectionString);

                if (e.ConnectionName == "VisualSoftErpConnectionString")
                {
                    e.ConnectionParameters = connectionParameters;
                }

            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }

        private void xrLabel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decTotSub = Convert.ToDecimal(label4.Summary.GetResult());
            
            decSubtotalAjustado = decTotSub - decTotalNcr;
            ((XRLabel)sender).Text = decSubtotalAjustado.ToString("c2");

        

        }

        private void label6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (label6.Text.Length > 0)
            {
                decTotalNcr = Convert.ToDecimal(label6.Text.Replace("$", ""));
                
            }                
            else
                decTotalNcr = 0;
        }

        private void label6_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
           //decTotalNcr = Convert.ToDecimal(e.Result);
           
           
        }

        private void label4_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            // decTotSub = Convert.ToDecimal(e.CalculatedValues.);
            
        }

        private void xrLabel1_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
                       
        }

        private void label6_AfterPrint(object sender, EventArgs e)
        {
            
        }

        private void tableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (tableCell16.Text.Length > 0)
            //    decTotalNcr = Convert.ToDecimal(tableCell16.Text.Replace("$", ""));
            //else
            //    decTotalNcr = 0;
        }

        private void label11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            label11.Text = decTotalTotalNcr.ToString("c2");
        }

        private void tableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decTotalNcr = Convert.ToDecimal(GetCurrentColumnValue("TotalNCR"));
            decTotalTotalNcr += decTotalNcr;


        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {           
            ((XRLabel)sender).Text = (ST - decTotalTotalNcr).ToString("c2");
        }

        private void label9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void label9_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            ST = Convert.ToDecimal(e.Result);
        }

        private void tableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
             ST += Convert.ToDecimal(GetCurrentColumnValue("SubTotal"));
        }
    }
}
