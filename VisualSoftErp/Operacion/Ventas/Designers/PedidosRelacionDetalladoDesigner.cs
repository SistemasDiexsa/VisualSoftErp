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
    public partial class PedidosRelacionDetalladoDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;

        public PedidosRelacionDetalladoDesigner()
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

        private void tableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
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

           

            xrTableCell2.BackColor = Color.FromArgb(dbr, dbg, dbb);
            xrTableCell2.ForeColor = Color.FromArgb(dfr, dfg, dfb);

          
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
