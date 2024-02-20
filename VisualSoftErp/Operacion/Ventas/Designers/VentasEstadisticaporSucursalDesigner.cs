using DevExpress.CodeParser;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace VisualSoftErp.Operacion.Ventas.Designers
{
    public partial class VentasEstadisticaporSucursalDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        public int IMPCANT { get; set; }
        public VentasEstadisticaporSucursalDesigner()
        {
            InitializeComponent();
        }

        private void xrPictureBoxLogo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string pathLogo = System.Configuration.ConfigurationManager.AppSettings["logoEmpresa"].ToString();
            xrPictureBoxLogo.ImageUrl = pathLogo;
        }

        private void cambiarFormato(object sender)
        {
            if (sender is XRControl control)
            {
                if (IMPCANT == 0)
                {
                    control.TextFormatString = "{0:C0}";
                }
                else if (IMPCANT == 1)
                {
                    control.TextFormatString = "{0:0}";
                }
            }
        }

        private void CambiarFormatoEnCeldas(IEnumerable<XRControl> controles)
        {
            foreach (XRControl control in controles)
            {
                if (control is XRTableCell)
                {    
                    if (control.Name != "xrLabel36" && control.Name != "xrLabel38" && control.Name != "xrLabel34" && control.Name != "tableCell14" && control.Name != "xrTableCell6") 
                        this.cambiarFormato(control);
                }
                if(control is XRLabel)
                {
                    if (control.Name != "xrLabel36" && control.Name != "xrLabel38" && control.Name != "xrLabel34" && control.Name != "xrLabel15" && control.Name != "tableCell14" && control.Name != "xrTableCell6") 
                        this.cambiarFormato(control);
                }
            }
        }

        private void VentasEstadisticaporSucursalDesigner_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            CambiarFormatoEnCeldas(((XtraReport)sender).AllControls<XRControl>());
        }

        private void xrLabel38_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (IMPCANT == 0) this.xrLabel38.Text = "Importe";
            else if (IMPCANT == 1) this.xrLabel38.Text = "Cantidad";
        }

        private void xrLabel36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string mes = string.Empty;

            switch (parameter4.Value)
            {
                case 1:
                    mes = "Enero";
                    break;
                case 2:
                    mes = "Febrero";
                    break;
                case 3:
                    mes = "Marzo";
                    break;
                case 4:
                    mes = "Abril";
                    break;
                case 5:
                    mes = "Mayo";
                    break;
                case 6:
                    mes = "Junio";
                    break;
                case 7:
                    mes = "Julio";
                    break;
                case 8:
                    mes = "Agosto";
                    break;
                case 9:
                    mes = "Septiembre";
                    break;
                case 10:
                    mes = "Octubre";
                    break;
                case 11:
                    mes = "Noviembre";
                    break;
                case 12:
                    mes = "Diciembre";
                    break;
                default:
                    break;
            }
            xrLabel36.Text = mes;
        }
    }
}
