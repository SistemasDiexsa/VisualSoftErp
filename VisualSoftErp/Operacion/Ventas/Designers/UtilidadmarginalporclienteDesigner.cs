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
    public partial class UtilidadmarginalporclienteDesigner : DevExpress.XtraReports.UI.XtraReport
    {

        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
        decimal decIVA = 0,  decUtiliT, decSubT, decUtilidad = 0, decCosto = 0, decSubtotal = 0;

        public UtilidadmarginalporclienteDesigner()
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

        private void calculatedFieldUtilidadTotal_GetValue(object sender, GetValueEventArgs e)
        {
            if (decUtiliT == 0) { }
            else
            {
                e.Value = decUtiliT;
            }
        }

        private void tableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell1.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell1.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell2.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell2.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void tableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void tableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell5.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell5.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell1.BackColor = Color.FromArgb(dbr, dbg, dbb);
            xrTableCell1.ForeColor = Color.FromArgb(dfr, dfg, dfb);
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

        private void calculatedFieldIvaTotal_GetValue(object sender, GetValueEventArgs e)
        {
           

            if (decSubT > 0)
                e.Value = Math.Round(decUtiliT / decSubT, 4);
            else
                e.Value = 0;                
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

        private void tableCell1_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell1.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell1.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell2_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell2.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell2.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell5_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell5.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell5.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void xrTableCell1_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell1.BackColor = Color.FromArgb(dbr, dbg, dbb);
            xrTableCell1.ForeColor = Color.FromArgb(dfr, dfg, dfb);
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

        private void calculatedFieldIva_GetValue(object sender, GetValueEventArgs e)
        {
            decSubtotal = Convert.ToDecimal(GetCurrentColumnValue("SubTotal"));
            decCosto = Convert.ToDecimal(GetCurrentColumnValue("Costo"));
            decUtilidad = Math.Round(decSubtotal - decCosto, 2);

            if (decSubtotal > 0)
                decIVA = Math.Round((decUtilidad / decSubtotal), 4);
            else
                decIVA = 0;
            e.Value = decIVA;

            decSubT += decSubtotal;
        }

        private void calculatedFieldUtilidad_GetValue(object sender, GetValueEventArgs e)
        {

            decSubtotal = Convert.ToDecimal(GetCurrentColumnValue("SubTotal"));
            decCosto = Convert.ToDecimal(GetCurrentColumnValue("Costo"));
            decUtilidad = Math.Round(decSubtotal - decCosto, 2);

            e.Value = decUtilidad;

            decUtiliT += decUtilidad;
        }   

       

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decSubtotal = Convert.ToDecimal(GetCurrentColumnValue("SubTotal"));
            decCosto = Convert.ToDecimal(GetCurrentColumnValue("Costo"));
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
