using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Clases.HerrramientasCLs;
using VisualSoftErp.Clases;
using DevExpress.DataAccess.ConnectionParameters;

namespace VisualSoftErp.Operacion.Compras.Designers
{
    public partial class ComprasrelaciondetalladaDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
  

        public ComprasrelaciondetalladaDesigner()
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

        private void tableCell24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell24.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell24.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell25_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void tableCell26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell26.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell26.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell19_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell19.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell19.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell20_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell20.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell20.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell22_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell22.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell22.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell23_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell23.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell23.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell24_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell24.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell24.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell26_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell26.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell26.ForeColor = Color.FromArgb(dfr, dfg, dfb);
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
    }
}
