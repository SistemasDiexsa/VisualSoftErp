using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Ventas.Designers
{
    public partial class ArticulosAlmacenDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        public ArticulosAlmacenDesigner()
        {
            InitializeComponent();
        }
        
        // Conexión a DB
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

        // Label para Fecha
        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string date = DateTime.Now.ToString("dddd dd 'de' MMMM 'del' yyyy");
            xrLabel2.Text = date;
        }

        // Ruta de Logo Empresa
        private void xrPictureBoxLogo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string url = ConfigurationManager.AppSettings["logoEmpresa"].ToString();
            xrPictureBoxLogo.ImageUrl = url;
        }

    }
}
