using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Tienda.Clases;

namespace VisualSoftErp.Operacion.Tienda
{
    public partial class TiendaOrdenes : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        int OrdenID;
        string Accion;
        public TiendaOrdenes()
        {
            InitializeComponent();
            AgregaElementos();
            splitContainerControl1.Visible = false;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }

        private void AgregaElementos()
        {
            accordionControlElement1.Elements.Clear();

            AppOrdenesCL cl = new AppOrdenesCL();
            DataTable dt = new DataTable();
            
            dt = cl.Ordenesporatender();

            AccordionControlElement item;
            foreach (DataRow row in dt.Rows)
            {
                item = new AccordionControlElement() { Name = row["AppOrdenesID"].ToString(), Text = row["AppOrdenesID"].ToString() + " " + row["Fecha"].ToString() + " " + row["Nombre"].ToString(), Style = ElementStyle.Item };
                accordionControlElement1.Elements.Add(item);
            }
                      

        }
        private void llenaCajas()
        {
            AppOrdenesCL cl = new AppOrdenesCL();
            cl.intOrdenID = OrdenID;
            DataSet ds=cl.Datosdelaorden();

            //Datos de la orden
            DataTable dtO = new DataTable();
            dtO = ds.Tables[0];
            lblFolio.Text = dtO.Rows[0][0].ToString();
            lblFecha.Text = Convert.ToDateTime(dtO.Rows[0][1]).ToLongDateString();
            lblStatus.Text = dtO.Rows[0][2].ToString();
            lblST.Text = Convert.ToDecimal(dtO.Rows[0][3]).ToString("c2");
            lblFlete.Text = Convert.ToDecimal(dtO.Rows[0][4]).ToString("c2");
            lblIva.Text = Convert.ToDecimal(dtO.Rows[0][5]).ToString("c2");
            lblTotal.Text = Convert.ToDecimal(dtO.Rows[0][6]).ToString("c2");
            lblEntregar.Text = Convert.ToDateTime(dtO.Rows[0][7]).ToShortDateString();

            //Datos del cliente
            DataTable dtCte = new DataTable();
            dtCte = ds.Tables[1];

            lblNombre.Text = dtCte.Rows[0][0].ToString();
            lblEmail.Text = dtCte.Rows[0][1].ToString();
            lblTel.Text = dtCte.Rows[0][2].ToString();
            lblPais.Text = dtCte.Rows[0][3].ToString();
            lblEstado.Text = dtCte.Rows[0][4].ToString();
            lblCd.Text = dtCte.Rows[0][5].ToString();
            lblCol.Text = dtCte.Rows[0][6].ToString();
            lblDir.Text = dtCte.Rows[0][7].ToString();

            //Detalle de la orden
            gridControl1.DataSource = ds.Tables[2];

            //FP
            gridControl2.DataSource = cl.DatosdelaordenFP();

            splitContainerControl1.Visible = true;
        }


        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            
        }

        private void accordionControl1_ElementClick(object sender, ElementClickEventArgs e)
        {
            globalCL clg = new globalCL();
            if (clg.esNumerico(e.Element.Name))
            {
                OrdenID = Convert.ToInt32(e.Element.Name);
                llenaCajas();
            }
            
        }

        private void tileBarItem1_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            this.Close();
        }

        private void tileBarItemCancelar_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            groupControl1.Text = "Datos de cancelación";
            Accion = "Cancelar";
            btnProceder.Enabled = true;
            groupControl1.Visible = true;
        }

        private void tileBarItemConfirmar_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            groupControl1.Text = "Datos de confirmación";
            Accion = "Confirmar";
            txtMensaje.Text = "Estimado cliente, su pedido ha sido confirmado";
            btnProceder.Enabled = true;
            groupControl1.Visible = true;
        }

        private void btnProceder_Click(object sender, EventArgs e)
        {          
            Proceder();
        }

        private void Proceder()
        {
            AppOrdenesCL cl = new AppOrdenesCL();
            cl.intOrdenID = OrdenID;
            cl.intStatus = Accion == "Confirmar" ? 2 : 5;
            cl.strMensaje = txtMensaje.Text;

            //if (cl.strMensaje.Length == 0)
            //{
            //    MessageBox.Show("Teclee el mensaje al cliente");
            //    return;
            //}

            string result;

            if (cl.intStatus == 5)
                result = cl.OrdenCambiaStatus();
            else
                result = cl.OrdenesConfirmar();

            if (result.Substring(0,2) == "OK")
            {
                if (cl.intStatus == 5)
                    MessageBox.Show("Actualizado correctamente, se le enviará correo al cliente");
                else
                {
                    string strPedido = result.Substring(3);
                    MessageBox.Show("Pedido " + strPedido + " generado, se le enviará correo al cliente");
                }
                    
                splitContainerControl1.Visible = false;
                groupControl1.Visible = false;
                EnviarCorreo(cl.intStatus);
                txtMensaje.Text = string.Empty;

                AgregaElementos();
                splitContainerControl1.Visible = false;
            }
            else
                MessageBox.Show(result);
        }

       

        private void EnviarCorreo(int status)
        {
            vsFK.vsFinkok vs = new vsFK.vsFinkok();

            string strEmail = lblEmail.Text;
            string strpdf = string.Empty;
            string strXml = string.Empty;
            string strFooter = string.Empty;

            string strHost = ConfigurationManager.AppSettings["EMailHost"].ToString();
            string strPort = ConfigurationManager.AppSettings["Emailport"].ToString();
            string strUserName = ConfigurationManager.AppSettings["Emailusername"].ToString();
            string strPassword = ConfigurationManager.AppSettings["Emailpassword"].ToString();
            string strSSL = ConfigurationManager.AppSettings["Emailssl"].ToString();

            vs.emailFrom = ConfigurationManager.AppSettings["EMailFrom"].ToString();
            vs.emailFromName = ConfigurationManager.AppSettings["EMailFromName"].ToString();
            vs.emailSmtpUserName = strUserName;
            vs.emailSmtpPassword = strPassword;
            vs.emailSmtpHost = strHost;
            vs.emailSmtpPort = strPort;
            vs.emailSSL = strSSL;

            string strLink = ConfigurationManager.AppSettings["Link"].ToString();

            vs.emailSubject = Accion == "Confirmar" ? "ORDEN " + OrdenID.ToString() + " CONFIRMADA" : " CANCELADA";
            vs.emailTO = strEmail;  //"jzambrano@live.com.mx";
            vs.emailXML = strXml;
            vs.emailPDF = strpdf;
            vs.Bcc = "";

            string strBody = string.Empty;

            strBody = "Estimado cliente<br><br>";
            strBody = strBody + "&nbsp;&nbsp;&nbsp;" + lblNombre.Text + " <br>";
            strBody = strBody + "<br>";
            if (Accion=="Confirmar")
                strBody = strBody + "Su órden #" + OrdenID.ToString() + " ha sido confirmada <br>"; 
            else
                strBody = strBody + "Su órden #" + OrdenID.ToString() + " ha sido cancelada <br>";
            strBody = strBody +  "<br>";
            strBody = strBody + txtMensaje.Text + "<br>";

            vs.emailBody = strBody;

            string strresult = vs.Enviar_Correo();

            MessageBox.Show(strresult);

            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }

        private void lblDir_Click(object sender, EventArgs e)
        {

        }

        private void lblCol_Click(object sender, EventArgs e)
        {

        }

        private void lblCd_Click(object sender, EventArgs e)
        {

        }

        private void lblEstado_Click(object sender, EventArgs e)
        {

        }

        private void lblPais_Click(object sender, EventArgs e)
        {

        }
    }
}
