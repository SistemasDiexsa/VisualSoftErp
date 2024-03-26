using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Clases.HerrramientasCLs;
using DevExpress.DataAccess.ConnectionParameters;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.CxC.Designers
{
    public partial class cxcAntiguedaddesaldosRep : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
     

        public cxcAntiguedaddesaldosRep()
        {
            InitializeComponent();
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        private void xrPictureBoxLogo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string pathLogo = System.Configuration.ConfigurationManager.AppSettings["logoEmpresa"].ToString();
            xrPictureBoxLogo.ImageUrl = pathLogo;

            if (stipoLogo == "L")
            {
                xrPictureBoxLogo.WidthF = 237;
                xrPictureBoxLogo.HeightF = 67;
            }
        }

        private void tableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell8.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell8.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell9.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell9.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell12.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell12.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell13.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell13.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell14.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell14.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell15.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell15.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell16.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell16.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell18.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell18.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell19.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell19.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell20.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell20.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell21_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell21.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell21.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell22.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell22.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell23.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell23.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void xrLabel17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

       

        private void xrPanel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
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

        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            try
            {
                string VisualSoftErpConnectionString = globalCL.gv_strcnn;
                CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(VisualSoftErpConnectionString);

                if (e.ConnectionName == "VisualSoftErpConnectionString")
                    e.ConnectionParameters = connectionParameters;

            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }
    }
}
