using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.ConnectionParameters;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Ventas.Designers
{
    public partial class CotizacionesFormatoImpresionTroya : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
        int Agente = 0;
        public CotizacionesFormatoImpresionTroya()
        {
            InitializeComponent();
        }

        private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string tipo = GetCurrentColumnValue("TiposdearticuloID").ToString();
            if (tipo == "1")
                xrLabel6.Text = "TOTAL MATERIALES";
            else
                xrLabel6.Text = "TOTAL DE MANO DE OBRA";
        }

        private void calculatedField1_GetValue(object sender, GetValueEventArgs e)
        {
            string tipo = GetCurrentColumnValue("TiposdearticuloID").ToString();
            if (tipo=="1")
            {                
                e.Value = "Muy bien";
            }
        }

        private void xrLabel13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decimal descto = Convert.ToDecimal(GetCurrentColumnValue("Descuento"));
            if (descto==0)
            {
                xrLabel13.Text = "";
            }
        }

        private void xrLabel14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decimal descto = Convert.ToDecimal(GetCurrentColumnValue("Descuento"));
            if (descto == 0)
            {
                xrLabel14.Text = "";
            }
        }

        private void xrLabel5_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decimal descto = Convert.ToDecimal(GetCurrentColumnValue("Descuento"));
            if (descto == 0)
            {
                //xrLabel5.Visible = false;
            }
        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decimal descto = Convert.ToDecimal(GetCurrentColumnValue("Descuento"));
            if (descto == 0)
            {
                xrLabel2.Visible = false;
            }
        }

        private void xrPictureBoxLogo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string pathLogo = System.Configuration.ConfigurationManager.AppSettings["logoEmpresa"].ToString();
            xrPictureBoxLogo.ImageUrl = pathLogo;

            if (stipoLogo == "L")
            {
                xrPictureBoxLogo.WidthF = 237;
                xrPictureBoxLogo.HeightF = 67;
            }

            Agente = Convert.ToInt32(GetCurrentColumnValue("AgentesID"));
        }

        private void xrLabelEmp_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (stipoLogo == "L")
            {
                xrLabelEmp.LocationF = new PointF(10, xrPictureBoxLogo.LocationF.Y + xrPictureBoxLogo.HeightF + 1);
            }
        }

        private void label4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decimal descto = Convert.ToDecimal(GetCurrentColumnValue("Descuento"));
            if (descto == 0)
            {
                label4.Visible = false;
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
    }
    
}
