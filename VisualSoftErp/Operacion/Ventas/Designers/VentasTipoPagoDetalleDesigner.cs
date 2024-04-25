using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Compras.Clases;

namespace VisualSoftErp.Operacion.Ventas.Designers
{
    public partial class VentasTipoPagoDetalleDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        public VentasTipoPagoDetalleDesigner()
        {
            InitializeComponent();
        }

        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            string cnn = globalCL.gv_strcnn;
            CustomStringConnectionParameters parameters = new CustomStringConnectionParameters(cnn);
            if (e.ConnectionName == "VisualSoftErpConnectionString")
                e.ConnectionParameters = parameters;
        }

        private void xrPictureBoxLogo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBoxLogo.ImageUrl = ConfigurationManager.AppSettings["logoEmpresa"].ToString();
        }
        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToInt32(parameter1.Value) == 0)
                xrLabel2.Text = "Cliente: Todos";
        }
        
        private void xrLabel3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToInt32(parameter2.Value) == 0)
                xrLabel3.Text = "Agente: Todos";
        }

        private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            switch (Convert.ToInt32(parameter3.Value))
            {
                case 0:
                    xrLabel4.Text = "Status: Todos";
                    break;
                case 1:
                    xrLabel4.Text = "Status: Registrada";
                    break;
                case 2:
                    xrLabel4.Text = "Status: Facturada";
                    break;
                case 3:
                    xrLabel4.Text = "Status: Pagadas Sin Factura";
                    break;
                case 4:
                    xrLabel4.Text = "Status: Expirada";
                    break;
                case 5:
                    xrLabel4.Text = "Status: Cancelada";
                    break;
            }
        }

        private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            switch(parameter4.Value.ToString())
            {
                case "0":
                    xrLabel5.Text = "Forma de Pago: Todos";
                    break;
                case "01":
                    xrLabel5.Text = "Forma de Pago: Efectivo";
                    break;
                case "02":
                    xrLabel5.Text = "Forma de Pago: Cheque Nominativo";
                    break;
                case "03":
                    xrLabel5.Text = "Forma de Pago: Transferencia Electrónica de Fondos";
                    break;
                case "99":
                    xrLabel5.Text = "Forma de Pago: POR DEFINIR";
                    break;
                case "CC":
                    xrLabel5.Text = "Forma de Pago: Crédito";
                    break;
                case "17":
                    xrLabel5.Text = "Forma de Pago: Compensación";
                    break;
                case "04":
                    xrLabel5.Text = "Forma de Pago: Tarjeta de Crédito";
                    break;
                case "28":
                    xrLabel5.Text = "Forma de Pago: Tarjeta de Débito";
                    break;
                case "DE":
                    xrLabel5.Text = "Forma de Pago: Depurado";
                    break;
                case "15":
                    xrLabel5.Text = "Forma de Pago: Condonación";
                    break;
                case "30":
                    xrLabel5.Text = "Forma de Pago: Aplicación de Anticipos";
                    break;
                case "27":
                    xrLabel5.Text = "Forma de Pago: A Satisfacción del Acreedor";
                    break;
            }
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string dtInicial = (Convert.ToDateTime(parameter5.Value)).ToString("dd-MM-yyyy");
            string dtFinal = (Convert.ToDateTime(parameter6.Value)).ToString("dd-MM-yyyy");
            xrLabel6.Text = "Fechas: " + dtInicial + " - " + dtFinal;
        }
    }
}
