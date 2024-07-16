using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Ventas.Designers
{
    public partial class ComisionesDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        private decimal groupSum;
        public ComisionesDesigner()
        {
            InitializeComponent();
        }

        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            try
            {
                string VisualSoftErpConnectionString = globalCL.gv_strcnn;
                CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(VisualSoftErpConnectionString);

                if (e.ConnectionName == "VisualSoftErpConnectionString")
                    e.ConnectionParameters = connectionParameters;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }

        private void xrPictureBoxLogo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string pathLogo = System.Configuration.ConfigurationManager.AppSettings["logoEmpresa"].ToString();
            xrPictureBoxLogo.ImageUrl = pathLogo;
        }

        // Label para la fecha
        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                TextInfo textInfo = new CultureInfo("es-ES", false).TextInfo;
                DateTime fechaInicial = Convert.ToDateTime(parameter1.Value);
                DateTime fechaFinal = Convert.ToDateTime(parameter2.Value);
                if ((fechaInicial.Month == fechaFinal.Month) && (fechaInicial.Year == fechaFinal.Year))
                {
                    string mes = fechaInicial.ToString("MMMM");
                    string anio = fechaInicial.ToString("yyyy");
                    mes = textInfo.ToTitleCase(mes);
                    
                    xrLabel2.Text = mes + " del " + anio;
                }
                else if ((fechaInicial.Month != fechaFinal.Month) && (fechaInicial.Year == fechaFinal.Year))
                {
                    string anio = fechaInicial.ToString("yyyy");
                    string mesInicial = fechaInicial.ToString("MMMM");
                    mesInicial = textInfo.ToTitleCase(mesInicial);
                    string mesFinal = fechaFinal.ToString("MMMM");
                    mesFinal = textInfo.ToTitleCase(mesFinal);
                    
                    xrLabel2.Text = "De " + mesInicial + " a " + mesFinal + " del " + anio;
                }
                else if ((fechaInicial.Month != fechaFinal.Month) && (fechaInicial.Year != fechaFinal.Year))
                {
                    string anioInicial = fechaInicial.ToString("yyyy");
                    string mesInicial = fechaInicial.ToString("MMMM");
                    mesInicial = textInfo.ToTitleCase(mesInicial);
                    string anioFinal = fechaFinal.ToString("yyyy");
                    string mesFinal = fechaFinal.ToString("MMMM");
                    mesFinal = textInfo.ToTitleCase(mesFinal);
                    
                    xrLabel2.Text = "De " + mesInicial + " del " + anioInicial + " a " + mesFinal + " del " + anioFinal;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al generar reporte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        // Celda para el alcance de cada concepto
        private void xrTableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (cell != null)
            {
                decimal meta = Convert.ToDecimal(GetCurrentColumnValue("Meta"));
                decimal comision = Convert.ToDecimal(GetCurrentColumnValue("Comision"));

                if (comision != 0 && meta != 0)
                {
                    decimal porcentajeCumplido = comision / meta;
                    cell.Text = porcentajeCumplido.ToString("P2");
                }
                else
                {
                    cell.Text = "N/A";
                }
            }
        }

        // Celda para el total de cada concepto
        private void tableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = (XRTableCell)sender;
            if(cell != null)
            {
                decimal meta = Convert.ToDecimal(GetCurrentColumnValue("Meta"));
                decimal comision = Convert.ToDecimal(GetCurrentColumnValue("Comision"));
                decimal porcentaje = Convert.ToDecimal(GetCurrentColumnValue("Porcentaje"));
                
                if (meta != 0)
                {
                    decimal alcance = (comision / meta) * 100;
                    if (alcance >= 100)
                    {
                        decimal total = comision * (porcentaje / 100) ;
                        groupSum += total;
                        cell.Text = total.ToString("C2");
                    }
                    else
                    {
                        cell.Text = "N/A";
                    }
                }
                else
                {
                    decimal total = comision * porcentaje;
                    groupSum += total;
                    cell.Text = total.ToString("C2");
                }
                
            }
        }

        // Celda para la sumatoria de totales
        private void xrLabel3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            globalCL globalCL = new globalCL();
            XRLabel xrLabel = (XRLabel)sender;
            if(xrLabel != null)
            {
                xrLabel.Text = groupSum.ToString("C2");
                groupSum = 0;
            }
        }
    }
}
