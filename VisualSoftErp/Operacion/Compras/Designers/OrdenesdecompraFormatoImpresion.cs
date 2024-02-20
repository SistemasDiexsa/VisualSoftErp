using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.DataAccess.ConnectionParameters;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Compras.Designers
{
    public partial class OrdenesdecompraFormatoImpresion : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
    
        int Pais = 0;
    

        public OrdenesdecompraFormatoImpresion()
        {
            InitializeComponent();
          

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
        }

        private void xrLabelEmp_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (stipoLogo == "L")
            {
                xrLabelEmp.LocationF = new PointF(10, xrPictureBoxLogo.LocationF.Y + xrPictureBoxLogo.HeightF + 1);
            }
        }

        private void xrLabelDir_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (stipoLogo == "L")
            {
                xrLabelDir.LocationF = new PointF(10, xrPictureBoxLogo.LocationF.Y + xrPictureBoxLogo.HeightF + 25);
            }
        }

        private void xrLabelTel_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (stipoLogo == "L")
            {
                xrLabelTel.LocationF = new PointF(10, xrPictureBoxLogo.LocationF.Y + xrPictureBoxLogo.HeightF + 45);
            }
        }

        private void xrLabelCorreo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (stipoLogo == "L")
            {
                //xrLabelCorreo.LocationF = new PointF(10, xrPictureBoxLogo.LocationF.Y + xrPictureBoxLogo.HeightF + 65);
            }
        }

        private void xrLabelPagina_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (stipoLogo == "L")
            {
                xrLabelPagina.LocationF = new PointF(10, xrPictureBoxLogo.LocationF.Y + xrPictureBoxLogo.HeightF + 85);
            }
        }

        private void xrLabelProveedor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais>1)
                xrLabelProveedor.Text = "Supplier";            
        }

       

        private void LblEmbarcarA_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                LblEmbarcarA.Text = "SHIP TO";
        }

        private void label1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                label1.Text = "Purchase order";
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                xrLabel6.Text = "Number";
        }

        private void LblFecha_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                LblFecha.Text = "Date";
        }

        private void LblMoneda_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                LblMoneda.Text = "Currency";
        }

        private void LblTerminosventa_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                LblTerminosventa.Text = "Terms";
        }

        private void LblFacturarA_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                LblFacturarA.Text = "Invoice to";
        }

        private void xrLabel8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                xrLabel8.Text = "Attention";
        }

        private void xrLabel9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel9.Text = "Email to";
        }

        private void LblObservaciones_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                LblObservaciones.Text = "Observations";
        }

        private void tableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                tableCell4.Text = "ITEM";
        }

        private void tableCell27_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                tableCell27.Text = "QTY";
        }

        private void tableCell28_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                tableCell28.Text = "Unit";
        }

        private void tableCell29_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                tableCell29.Text = "Description";
        }

        private void tableCell30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                tableCell30.Text = "Price";
        }

        private void tableCell31_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                tableCell31.Text = "Amount";
        }

        private void LblSubtotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                LblSubtotal.Visible = false;
        }

        private void xrLabel19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                xrLabel19.Visible = false;
        }

        private void LblIva_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                LblIva.Visible = false;
        }

        private void xrLabel21_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                xrLabel21.Visible = false;
        }

        private void LblNeto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 1)
                LblNeto.Text = "Total";
        }

        private void LblCompras_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            LblCompras.Text = "Purchasing";
        }

        private void pageInfo1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 0)
                pageInfo1.Visible = false;
        }

        private void xrTableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 0)
                xrTableCell5.Text = "Shippment";
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

        private void xrLabel17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 0)
                xrLabel17.Visible = false;
        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 0)
                xrLabel16.Visible = false;
        }

        private void lblPais_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Pais = Convert.ToInt32(lblPais.Text);
            if (Pais > 1)
                PageFooter.HeightF = 135;
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Pais = Convert.ToInt32(GetCurrentColumnValue("Pais"));
        }

        private void xrLabel26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 0)
                xrLabel26.Visible = false;
        }

        private void xrLabel27_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 0)
                xrLabel27.Visible = false;
        }

        private void xrLabel28_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 0)
                xrLabel28.Visible=false;
        }

        private void xrLabel29_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Pais > 0)
                xrLabel29.Visible = false;
        }
    }
}
