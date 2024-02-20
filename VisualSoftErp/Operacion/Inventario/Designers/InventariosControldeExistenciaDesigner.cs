using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Clases.HerrramientasCLs;
using DevExpress.DataAccess.ConnectionParameters;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Inventarios.Designers
{
    public partial class InventariosControldeExistenciaDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
        decimal decMax = 0, decMin = 0, decExistencia = 0;
     

        public InventariosControldeExistenciaDesigner()
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

        private void calculatedFieldDifMax_GetValue(object sender, GetValueEventArgs e)
        {
            e.Value = Math.Round(decMax - decExistencia, 2);
            if (decExistencia > decMax) { tableCell10.ForeColor = Color.Blue; }
            else if (decExistencia <= decMin) { tableCell10.ForeColor = Color.Red; }
        }

        private void calculatedFieldDifMin_GetValue(object sender, GetValueEventArgs e)
        {
            e.Value = Math.Round(decMin - decExistencia, 2);
            if (decExistencia > decMax) { tableCell10.ForeColor = Color.Blue; }
            else if (decExistencia <= decMin) { tableCell10.ForeColor = Color.Red; }

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

        private void tableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell1.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell1.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell2.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell2.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell3.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell3.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell4.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell4.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell5.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell5.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            //xrTableCell1.BackColor = Color.FromArgb(dbr, dbg, dbb);
            //xrTableCell1.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            //xrTableCell2.BackColor = Color.FromArgb(dbr, dbg, dbb);
            //xrTableCell2.ForeColor = Color.FromArgb(dfr, dfg, dfb);

        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decMax = Convert.ToDecimal(GetCurrentColumnValue("Maximo"));
            decMin = Convert.ToDecimal(GetCurrentColumnValue("Minimo"));
            decExistencia = Convert.ToDecimal(GetCurrentColumnValue("Existencia"));

            if (decExistencia > decMax) { tableCell10.ForeColor = Color.Blue; }
            else if (decExistencia <= decMin) { tableCell10.ForeColor = Color.Red; }
        }
    }
}
