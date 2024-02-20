using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Clases.HerrramientasCLs;
using DevExpress.DataAccess.ConnectionParameters;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Inventarios.Informes
{
    public partial class MovimientosporarticuloDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
        decimal decExistencia = 0;
        decimal decEntradas = 0;
        decimal decSalidas = 0;
        bool tomeExiAnt = false;

        public MovimientosporarticuloDesigner()
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
            

        }

        private void xrLblEmp_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            

        }

        private void tableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell3.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell3.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell4.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell4.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void tableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
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
            tableCell2.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell2.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //decExistencia = Convert.ToDecimal(GetCurrentColumnValue("UltimaExistencia"));
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decEntradas = Convert.ToDecimal(GetCurrentColumnValue("Entrada"));
            decSalidas = Convert.ToDecimal(GetCurrentColumnValue("Salida"));
            decExistencia = decExistencia + decEntradas + decSalidas;
        }

        private void calculatedFieldExistencia_GetValue(object sender, GetValueEventArgs e)
        {
            e.Value = decExistencia;
        }

        private void tableCell111_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell111.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell111.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void xrPictureBoxLogo_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string pathLogo = System.Configuration.ConfigurationManager.AppSettings["logoEmpresa"].ToString();
            xrPictureBoxLogo.ImageUrl = pathLogo;

            if (stipoLogo == "L")
            {
                xrPictureBoxLogo.WidthF = 237;
                xrPictureBoxLogo.HeightF = 47;
            }
        }

        private void xrLblEmp_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!tomeExiAnt)
            {
                decExistencia = Convert.ToDecimal(GetCurrentColumnValue("UltimaExistencia"));
                tomeExiAnt = true;
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            
        }
    }
}
