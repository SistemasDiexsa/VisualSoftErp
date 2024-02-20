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
    public partial class EstadisticadeventasporlineafamiliaarticuloDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
        decimal decTotal = 0;
        decimal decPromedio = 0;
        decimal decPorcentaje = 0;
        int unidades = 0;
        int mes;

        public EstadisticadeventasporlineafamiliaarticuloDesigner()
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

        private void tableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //tableCell2.BackColor = Color.FromArgb(dbr, dbg, dbb);
            //tableCell2.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //tableCell4.BackColor = Color.FromArgb(dbr, dbg, dbb);
            //tableCell4.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //tableCell5.BackColor = Color.FromArgb(dbr, dbg, dbb);
            //tableCell5.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell6.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell6.ForeColor = Color.FromArgb(dfr, dfg, dfb);
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

        private void tableCell17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell17.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell17.ForeColor = Color.FromArgb(dfr, dfg, dfb);
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

        private void xrTableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell4.BackColor = Color.FromArgb(dbr, dbg, dbb);
            xrTableCell4.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void xrTableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrTableCell5.BackColor = Color.FromArgb(dbr, dbg, dbb);
            //xrTableCell5.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrTableCell6.BackColor = Color.FromArgb(dbr, dbg, dbb);
            //xrTableCell6.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void Porcentaje_GetValue(object sender, GetValueEventArgs e)
        {
            e.Value = decPorcentaje;
        }

        private void Promedio_GetValue(object sender, GetValueEventArgs e)
        {
            decTotal= Convert.ToDecimal(GetCurrentColumnValue("Totalrenglon"));
            if (Convert.ToDecimal(GetCurrentColumnValue("Totalgeneral")) > 0)
                decPorcentaje = decTotal / Convert.ToDecimal(GetCurrentColumnValue("Totalgeneral"));
            else
                decPorcentaje = 0;
            e.Value = decTotal / mes;
            
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

        private void xrLabelMonetario_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrEmpID.Text == "1")
                xrLabelMonetario.Text = "UNIDADES";
        }

        private void xrEmpID_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            unidades = Convert.ToInt32(xrEmpID.Text);
        }

        private void EstadisticadeventasporlineafamiliaarticuloDesigner_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            object xafParameters =
            ((DevExpress.XtraReports.UI.XtraReport)sender).Parameters["parameter4"].Value;
            mes = Convert.ToInt32(xafParameters);
        }
    }
}
