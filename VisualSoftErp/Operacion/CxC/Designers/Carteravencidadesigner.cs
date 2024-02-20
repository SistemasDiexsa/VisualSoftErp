using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Clases.HerrramientasCLs;
using DevExpress.DataAccess.ConnectionParameters;
using VisualSoftErp.Clases;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.DataAccess.Sql;

namespace VisualSoftErp.Operacion.CxC.Designers
{
    public partial class Carteravencidadesigner : DevExpress.XtraReports.UI.XtraReport
    {

        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
        decimal decDiferencia = 0;
        decimal decVentaReal = 0;
        string uen;




        public Carteravencidadesigner()
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

        private void tableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
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

        private void tableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell15.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell15.ForeColor = Color.FromArgb(dfr, dfg, dfb);
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

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell2.BackColor = Color.FromArgb(dbr, dbg, dbb);
            xrTableCell2.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell11_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell11.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell11.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell12_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell12.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell12.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell15_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell15.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell15.ForeColor = Color.FromArgb(dfr, dfg, dfb);
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

        private void lblUen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void label4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            switch (uen)
            {
                case "1":
                    label4.BackColor = Color.Blue;
                    label4.ForeColor = Color.White;
                    break;
                case "2":
                    label4.BackColor = Color.ForestGreen;
                    label4.ForeColor = Color.White;
                    break;
                case "3":
                    break;
            }

        }

        private void xrLabel23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            uen = xrLabel23.Text;
        }

        private void Carteravencidadesigner_DesignerLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
            (sender as XRDesignPanel).ComponentAdded += DesignPanel_ComponentAdded;
        }
        private void DesignPanel_ComponentAdded(object sender, System.ComponentModel.Design.ComponentEventArgs e)
        {
            if (e.Component is SqlDataSource)
                (e.Component as SqlDataSource).ConnectionOptions.DbCommandTimeout = 1500;
        }
    }
}
