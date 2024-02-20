using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VisualSoftErp.Clases;
using VisualSoftErp.Clases.HerrramientasCLs;

namespace VisualSoftErp.Operacion.CxC.Formas
{
    public partial class MailingCxC : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intCte = 0;
        string strEmail = string.Empty;
        string strNom = string.Empty;
        bool blnUno;
        int intCuantos = 0;

        public MailingCxC()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridView1.OptionsSelection.MultiSelect = true;
            LLenaGrid();
            gridView1.OptionsView.ShowAutoFilterRow = true;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
       
        private void LLenaGrid()
        {
            clientesCL cl = new clientesCL();
            gridControl1.DataSource = cl.ClientesGrid();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Mailing","Enviando correos...");
            intCuantos = 0;
            foreach (int i in gridView1.GetSelectedRows())
            {
                intCuantos++;
            }

            if (intCuantos > 1)
                blnUno = false;
            else
                blnUno = true;

            intCuantos = 0;

            foreach (int i in gridView1.GetSelectedRows())
            {
                intCte = Convert.ToInt32(gridView1.GetRowCellValue(i, "ClientesID"));
                strNom = gridView1.GetRowCellValue(i, "Nombre").ToString();
                strEmail = gridView1.GetRowCellValue(i, "EMail").ToString();
                EnviaCorreo();
            }

            

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            if (!blnUno)
                MessageBox.Show(intCuantos.ToString() + " correos envíados");
               
        }

        private void EnviaCorreo()
        {
            try
            {
                string strCarpeta = System.Configuration.ConfigurationManager.AppSettings["MailingCarpeta"].ToString();
                decimal total = 0;
                decimal tottot = 0;
                string strFecha = "Al: " + DateTime.Now.ToShortDateString();

                string strRazonComercial = string.Empty;
                string strWWW = string.Empty;
                string strTel = string.Empty;
                string strDir = string.Empty;
                string strFrase = string.Empty;

                globalCL clg = new globalCL();
                clg.intEmpresaID = 1;     // Ver si se ocupa variar
                string result = clg.EmpresaleeDatos();
                if (result=="OK")
                {
                    strRazonComercial = clg.strEmpresaRazonComercial;
                    strWWW = clg.strEmpresaWWW;
                    strTel = clg.strEmpresaTel;
                    strDir = clg.strEmpresaDir;
                    strFrase = clg.strEmpresaFrasePie;
                }

                //Datos de control
                DatosdecontrolCL cld = new DatosdecontrolCL();
                //Encabezado back color
                result = cld.DatosdecontrolLeer();
                int ebr = cld.iCorreoEncaBackColorR;
                int ebg = cld.iCorreoEncaBackColorG;
                int ebb = cld.iCorreoEncaBackColorB;
                string ebchex = string.Format("{0:X2}{1:X2}{2:X2}", ebr, ebg, ebb);
                //Encabezado fore color
                int efr = cld.iCorreoEncaForeColorR;
                int efg = cld.iCorreoEncaForeColorG;
                int efb = cld.iCorreoEncaForeColorB;
                string efchex = string.Format("{0:X2}{1:X2}{2:X2}", efr, efg, efb);
                //Pie back color
                int pbr = cld.iCorreoPieBackColorR;
                int pbg = cld.iCorreoPieBackColorG;
                int pbb = cld.iCorreoPieBackColorB;
                string pbchex = string.Format("{0:X2}{1:X2}{2:X2}", pbr, pbg, pbb);
                //Pie fore color
                int pfr = cld.iCorreoPieForeColorR;
                int pfg = cld.iCorreoPieForeColorG;
                int pfb = cld.iCorreoPieForeColorB;
                string pfchex = string.Format("{0:X2}{1:X2}{2:X2}", pfr, pfg, pfb);


                DataTable dt = new DataTable();
                DepositosCL cl = new DepositosCL();
                cl.intClientesID = intCte;
                dt = cl.AntSaldos();

                if (dt.Rows.Count==0)
                {
                    if (blnUno)
                        MessageBox.Show("No hay cxc para este cliente");
                    return;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("<!DOCTYPE html>");
                sb.Append("<html lang=\"en\">");
                sb.Append("<head>");
                sb.Append("<meta charset=\"utf-8\">");
                sb.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
                sb.Append("<meta name=\"viewport\" content=\"width = device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0\">");
                sb.Append("<title>Titulo del mail</title>");                            
                sb.Append("<link href=\"https://www.visualsoft.com.mx/VisualSoftErp/Mailing/" + strCarpeta + "/css/styles.css\" rel=\"stylesheet\">");               
                sb.Append("<link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700\" rel=\"stylesheet\">");               
                sb.Append("</head>");
                sb.Append("<div style=\"width:100%\">");  //body
                sb.Append("<div style=\"background: #" + ebchex + "; width: 800px; text-align: center; border: none\">"); //Encabezado
                //sb.Append("<div class=\"encabezado\">"); //Encabezado
                //Tabla del encabezado
                sb.Append("<table style=\"background: #" + ebchex + "; width: 800px; text-align: center; border: none\">");
                //sb.Append("<table style=\"background-color:rgba(" + br + "," + bg + "," + bb + ",0.5); width: 800px; text-align: center; border: none\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"text-align:left; border: none\"><img src=\"https://visualsoft.com.mx/VisualSoftErp/Mailing/" + strCarpeta + "/images/logo.png\"></td>");
                sb.Append("<td style=\"border: none; color: #" + efchex + "; text-align: right; font-size: 18px\">ESTADO DE CUENTA</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border: none; color: #" + efchex + "; text-align: left; font-size: 18px\">" + strRazonComercial + "</td>");
                sb.Append("<td style=\"border: none; color: #" + efchex + "; text-align: right; font-size: 14px\">" + strFecha + "</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                //Eof: Tabla del encabezado
                sb.Append("</div>");  //Eof:Encabezado            
                sb.Append("<section class=\"container-data\">");
                sb.Append("<p>Estimado cliente:</p>");
                sb.Append("<h4>");
                sb.Append(strNom);
                sb.Append("</h4>");
                sb.Append("<p>Le hacemos llegar la antigüedad de saldos</p>");
                //Informacion
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<th>Mov</th>");
                sb.Append("<th>Folio</th>");
                sb.Append("<th>Fecha</th>");
                sb.Append("<th>Plazo</th>");
                sb.Append("<th>Vence</th>");
                sb.Append("<th>Días</th>");
                sb.Append("<th style=\"text-align:right\">Saldo</th>");
                sb.Append("<th>Moneda</th>");
                sb.Append("</tr>");
                
                foreach (DataRow dr in dt.Rows)
                {
                    total=Convert.ToDecimal(dr["XV"]) +Convert.ToDecimal(dr["30"]) +Convert.ToDecimal(dr["60"]) +Convert.ToDecimal(dr["90"]) +Convert.ToDecimal(dr["+90"]);

                    sb.Append("<tr>");
                    sb.Append("<td>" + dr["NomTMov"].ToString() + "</td>");
                    sb.Append("<td>" + dr["Referencia"].ToString() + "</td>");
                    sb.Append("<td>" + dr["Fecha"].ToString().Substring(0,10) + "</td>");
                    sb.Append("<td>" + dr["Plazo"].ToString() + "</td>");
                    sb.Append("<td>" + dr["FechaVence"].ToString().Substring(0,10) + "</td>");
                    sb.Append("<td>" + dr["DiasVenc"].ToString() + "</td>");
                    if (Convert.ToInt32(dr["DiasVenc"])<0)
                        sb.Append("<td style=\"text-align:right\">" + String.Format("{0:C2}", total)  + "</td>");
                    else
                        sb.Append("<td style=\"text-align:right; color: red;\">" + String.Format("{0:C2}", total) + "</td>");
                    sb.Append("<td>" + dr["Moneda"] + "</td>");
                    sb.Append("</tr>");

                    tottot += total;
                }

                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td>TOTAL</td>");
                sb.Append("<td style=\"text-align:right; font-weight: bold;\">" + String.Format("{0:C2}", tottot) + "</td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
                sb.Append("</table>");  
                //Eof:Informacion
                sb.Append("</section>");
                sb.Append("<h3>");
                sb.Append("Si tiene cualquier duda o aclaración favor de ponerse en contacto con nosotros.");
                sb.Append("</h3>");
                sb.Append("<br>");
                sb.Append("<footer>");
                sb.Append("<div style=\"background: #" + pbchex + "; width: 800px; text-align: center; border: none\">");
                //Tabla
                sb.Append("<table style=\"background: #" + pbchex + "; width: 800px; text-align: center; border: none\">");
                sb.Append("<tr>");
                sb.Append("<td style=\"border: none; color: #" + pfchex + "; text-align: left; font-size: 12px\">" + strRazonComercial + "</td>");
                sb.Append("<td style=\"border: none; color: #" + pfchex + "; text-align: right; font-size: 12px\">" + strFrase + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"border: none; color: #" + pfchex + "; text-align: left; font-size: 12px\">" + strDir + "</td>");
                sb.Append("<td style=\"border: none; color: #" + pfchex + "; text-align: right; font-size: 12px\">" + strTel + "</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                //Eof: Tabla
                sb.Append("</div>");  //Eof:Encabezado                      
                sb.Append("</footer>");
                sb.Append("</div>");
                sb.Append("</html>");

                vsFK.vsFinkok vs = new vsFK.vsFinkok();
                string strHost = System.Configuration.ConfigurationManager.AppSettings["EMailHost"].ToString();
                string strPort = System.Configuration.ConfigurationManager.AppSettings["EMailPort"].ToString();
                string strUserName = System.Configuration.ConfigurationManager.AppSettings["EMailUsername"].ToString();
                string strPassword = System.Configuration.ConfigurationManager.AppSettings["EMailPassword"].ToString();
                string strSSL = System.Configuration.ConfigurationManager.AppSettings["EMailSSL"].ToString();
                string stremailTO = strEmail;
                    //= System.Configuration.ConfigurationManager.AppSettings["emailTo"].ToString();

                vs.emailFrom = "javier@cocodrilosoft.net";
                vs.emailFromName = strRazonComercial;
                vs.emailSmtpUserName = strUserName;
                vs.emailSmtpPassword = strPassword;
                vs.emailSmtpHost = strHost;
                vs.emailSmtpPort = strPort;
                vs.emailSSL = strSSL;

                vs.emailSubject = "Estado de cuenta al " + DateTime.Now.ToShortDateString();
                vs.emailTO = stremailTO; //"jzambrano@live.com.mx";
                vs.emailXML = "";
                vs.emailPDF = "";
                vs.Bcc = "";

                string strBody = string.Empty;
                
                vs.emailBody = sb.ToString();

                string strresult = vs.Enviar_Correo();
                if (blnUno)
                {
                    MessageBox.Show(strresult);
                }
                intCuantos++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            
        }
    }
}