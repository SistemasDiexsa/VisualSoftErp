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
    public partial class VentasDesignerVentasporLineaFamiliaDetalldo : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
        decimal decDiferencia = 0;

        public VentasDesignerVentasporLineaFamiliaDetalldo()
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

        private void tableCell9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell9.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell9.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell10.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell10.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell11.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell11.ForeColor = Color.FromArgb(dfr, dfg, dfb);
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

        private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell1.BackColor = Color.FromArgb(dbr, dbg, dbb);
            xrTableCell1.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decDiferencia = Convert.ToDecimal(GetCurrentColumnValue("SubTotal")) - Convert.ToDecimal(GetCurrentColumnValue("TP"));
        }

        private void Diferencia_GetValue(object sender, GetValueEventArgs e)
        {
            e.Value = decDiferencia;
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
