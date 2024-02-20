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
    public partial class Pedidosformatoimpresion : DevExpress.XtraReports.UI.XtraReport
    {
        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
        int Agente = 0;
        int items = 0;

        public Pedidosformatoimpresion()
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

            Agente = Convert.ToInt32(GetCurrentColumnValue("AgentesID"));
        }

        private void xrPanel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
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

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (stipoLogo == "L")
            {
                xrLabelEmpPH.LocationF = new PointF(10, xrPictureBoxLogo.LocationF.Y + xrPictureBoxLogo.HeightF + 1);
            }
        }

        private void xrPictureBoxfirma_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string pathLogo = System.Configuration.ConfigurationManager.AppSettings["Firmas"].ToString();
            pathLogo = pathLogo + Agente.ToString() + ".png";
            //xrPictureBoxfirma.ImageUrl = pathLogo;
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

        private void lblItems_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
           
        }
    }
}
