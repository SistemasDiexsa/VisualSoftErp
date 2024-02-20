using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Clases.HerrramientasCLs;
using VisualSoftErp.Clases;
using DevExpress.DataAccess.ConnectionParameters;

namespace VisualSoftErp.Operacion.Inventarios.Designers
{
    public partial class bfMovsEstadisticaDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;


        public bfMovsEstadisticaDesigner()
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
            //tableCell5.BackColor = Color.FromArgb(dbr, dbg, dbb);
            //tableCell5.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell6.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell6.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell8.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell8.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell9.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell9.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell10.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell10.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell11.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell11.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell12.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell12.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell13.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell13.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell14.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell14.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell15.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell15.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell16.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell16.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell17.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell17.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell18.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell18.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell19.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell19.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell20.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell20.ForeColor = Color.FromArgb(dfr, dfg, dfb);
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
