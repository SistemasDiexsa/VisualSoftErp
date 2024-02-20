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
    public partial class UtilidadmarginalporlineafamiliaarticuloDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
        decimal decIVA = 0,  decUtiliT, decSubT, decUtilidad = 0, decCosto = 0, decSubtotal = 0;
        decimal totVentaFam = 0;
        decimal totCostoFam = 0;
        decimal totVentaLinea = 0;
        decimal totCostoLinea = 0;
        

        private void calculatedFieldUtilidadTotal_GetValue(object sender, GetValueEventArgs e)
        {
            if (decUtiliT == 0) { }
            else
            {
                e.Value = decUtiliT;
            }
        }

        private void calculatedFieldIvaTotal_GetValue(object sender, GetValueEventArgs e)
        {

            if (decSubT > 0)
                e.Value = Math.Round((decUtiliT / decSubT), 4); 
            else
                e.Value = 0;
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

        private void tableCell7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell7.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell7.ForeColor = Color.FromArgb(dfr, dfg, dfb);
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

        private void tableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell10.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell10.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void xrTableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell3.BackColor = Color.FromArgb(dbr, dbg, dbb);
            xrTableCell3.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void calculatedFieldPtjeFam_GetValue(object sender, GetValueEventArgs e)
        {
            decimal uti = Math.Round(totVentaFam - totCostoFam,2);

            if (totVentaFam > 0)
                e.Value = Math.Round(uti / totVentaFam, 4);
            else
                e.Value = 0;

            totVentaFam = 0;
            totCostoFam = 0;
        }

        private void calculatedFieldIva_GetValue(object sender, GetValueEventArgs e)
        {
            decSubtotal = Convert.ToDecimal(GetCurrentColumnValue("Venta"));
            decCosto = Convert.ToDecimal(GetCurrentColumnValue("Costo"));
            decUtilidad = Math.Round(decSubtotal - decCosto, 2);

            if (decSubtotal > 0)
                decIVA = Math.Round((decUtilidad / decSubtotal), 4);
            else
                decIVA = 0;

            e.Value = decIVA;

            decSubT += decSubtotal;
        }

        private void calculatedFieldPtjeLinea_GetValue(object sender, GetValueEventArgs e)
        {
            decimal uti = Math.Round(totVentaLinea - totCostoLinea, 2);
            if (totVentaLinea > 0)
                e.Value = Math.Round(uti / totVentaLinea, 4);
            else
                e.Value = 0;

            totVentaLinea = 0;
            totCostoLinea = 0;
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
                sqlDataSource1.ConnectionOptions.DbCommandTimeout = 0;

            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }

        private void tableCell5_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell5.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell5.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell6_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell6.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell6.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell7_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell7.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell7.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell8_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell8.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell8.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell9_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell9.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell9.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell10_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell10.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell10.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void xrTableCell3_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell3.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell3.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void calculatedFieldUtilidad_GetValue(object sender, GetValueEventArgs e)
        {
            decSubtotal = Convert.ToDecimal(GetCurrentColumnValue("Venta"));
            decCosto = Convert.ToDecimal(GetCurrentColumnValue("Costo"));
            decUtilidad = Math.Round(decSubtotal - decCosto, 2);

            e.Value = decUtilidad;

            decUtiliT += decUtilidad;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decSubtotal = Convert.ToDecimal(GetCurrentColumnValue("Venta"));
            decCosto = Convert.ToDecimal(GetCurrentColumnValue("Costo"));

            totVentaFam = totVentaFam + decSubtotal;
            totCostoFam = totCostoFam + decCosto;
            totVentaLinea = totVentaLinea + decSubtotal;
            totCostoLinea = totCostoLinea + decCosto;
        }

        public UtilidadmarginalporlineafamiliaarticuloDesigner()
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
    }
}
