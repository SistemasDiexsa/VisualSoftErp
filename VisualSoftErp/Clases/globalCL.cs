using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using VisualSoftErp.Clases.HerrramientasCLs;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Net.NetworkInformation;
using System.Xml;
using System.Net;

namespace VisualSoftErp.Clases
{

    public class globalCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn; 
        //ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public static DevExpress.XtraBars.Ribbon.RibbonPage gv_RibbonPage;
        public static int gv_UsuarioID;
        public static string gv_UsuarioNombre;
        public static string gv_NombreEmpresa;
        public static string gv_strcnn;
        public static int gv_Folio;
        public static string gv_Serie;
        public static string gv_AnticiposOrigen { get; set; }

        public int iImpresiondirecta;
        public string sTipologo;
        public int iAbrirOutlook;
        public string sCondicion;
        public string sTabla;
        public int iSigID;
        public string sDocumento;
        public string sSerie;
        public int iFolio;
        public string sPara;
        public string sSubject;
        public DateTime dFechaDoc;
        public string strGridLayout;
        public DataTable dtxml;
        public int intEjer { get; set; }
        public int intMes { get; set; }
        public string strMaq { get; set; }
        public string strTipoProv { get; set; }
        public int intFolio {get; set;}
        public string strFac { get; set; }
        public string strUUID { get; set; }
        public string strEmpresaNom { get; set; }
        public string strEmpresaDir { get; set; }
        public string strEmpresaRfc { get; set; }
        public string strEmpresaTel { get; set; }
        public string strEmpresaWWW { get; set; }
        public int intEmpresaID { get; set; }
        public string strEmpresaRazonComercial { get; set; }
        public string strEmpresaFrasePie { get; set; }

        public string strPrograma { get; set; }
        public int intCargar { get; set; }

        public string strClave { get; set; }
        public string strDes { get; set; }
        public string strTabla { get; set; }
        public string strSerie;
        public string strDoc;

        public int intOrdenesPorAtender { get; set; }
        public int intSalidasporautorizar { get; set; }
        public int intSolicitudesDeRegistro { get; set; }

        public DateTime fechaUltimoDiadelMes { get; set; }        

        #endregion
        #region Constructor
        public globalCL()
        {
            strGridLayout = string.Empty;
            iImpresiondirecta = 0;
            sTipologo = string.Empty;
            iAbrirOutlook = 0;
            sCondicion = string.Empty;
            sTabla = string.Empty;
            iSigID = 0;
            strTipoProv = string.Empty;
            intFolio = 0;
            strFac = string.Empty;
            strUUID = string.Empty;

            strClave = string.Empty;
            strDes = string.Empty;
            strTabla = string.Empty;
            strEmpresaRazonComercial = string.Empty;
            strEmpresaFrasePie = string.Empty;

            strPrograma = string.Empty;
            intCargar = 0;

            intOrdenesPorAtender = 0;
            intSalidasporautorizar = 0;
            intSolicitudesDeRegistro = 0;


        }
        #endregion
        #region Metodos

        public string NombreDeMes(int Mes)
        {
            switch (Mes)
            {
                case 1:
                    return "ENE";
                case 2:
                    return "FEB";
                case 3:
                    return "MAR";
                case 4:
                    return "ABR";
                case 5:
                    return "MAY";
                case 6:
                    return "JUN";
                case 7:
                    return "JUL";
                case 8:
                    return "AGO";
                case 9:
                    return "SEP";
                case 10:
                    return "OCT";
                case 11:
                    return "NOV";
                case 12:
                    return "DIC";
            }

            return ""; //Aqui nunca va llegar, solo se pone para que no marque error la funcion
        }

        public DataTable AgregarOpcionTodos(BindingSource src)
        {
            try
            {
                

                DataTable table = (DataTable)src.DataSource;
                DataRow dr = table.NewRow();
                dr["Des"] = "Todos";
                dr["Clave"] = 0;
                table.Rows.InsertAt(dr, 0);
                return table;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public DataTable AgregarOpcionNoManeja(BindingSource src)
        {
            try
            {
                DataTable table = (DataTable)src.DataSource;
                DataRow dr = table.NewRow();
                dr["Des"] = "No maneja";
                dr["Clave"] = 0;
                table.Rows.InsertAt(dr, 0);
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable AgregarOpcionc_FormaPago(BindingSource src)
        {
            try
            {
                DataTable table = (DataTable)src.DataSource;
                DataRow dr = table.NewRow();
                dr["Des"] = "La del CFDI de ingreso";
                dr["Clave"] = 0;
                table.Rows.InsertAt(dr, 0);
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string UltimoDiaDelMes(int Año,int Mes)
        {
            try
            {
                string sFecha=DateTime.Now.ToShortDateString();
                string sMes = Mes < 10 ? "0" + Mes.ToString() : Mes.ToString();
                switch (Mes)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        sFecha = Año.ToString() + "-" + sMes + "-" + "31";
                        break;
                    case 2:
                        sFecha = Año.ToString() + "-" + sMes + "-" + "28";
                        break;
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        sFecha = Año.ToString() + "-" + sMes + "-" + "30";
                        break;

                }

                fechaUltimoDiadelMes= Convert.ToDateTime(sFecha);
                return "OK";
            }
            catch(Exception ex)
            {
                return "Error ultimo dia del mes:" + ex.Message;
            }
        }
        public string fechaSQL(DateTime fecha)
        {
            try
            {
                string año = string.Empty;
                string mes = string.Empty;
                string dia = string.Empty;

                año = fecha.Year.ToString();
                if (fecha.Month < 10)
                    mes = "0" + fecha.Month.ToString();
                else
                    mes = fecha.Month.ToString();
                if (fecha.Day < 10)
                    dia = "0" + fecha.Day.ToString();
                else
                    dia = fecha.Day.ToString();

                string strfechaSQL = año + "-" + mes + "-" + dia;

                return strfechaSQL;

            }
            catch (Exception ex)
            {
                MessageBox.Show("fechaSQL: " + ex.Message);
                return string.Empty;
            }
        }
        public bool esNumerico(string dato)
        {
            try
            {
                decimal datoNumerico = Convert.ToDecimal(dato);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool esEmail(string dato)
        {
            

            //Falta regexe para multiples

            string email = dato;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }

        public string SaveGridLayout(DevExpress.XtraGrid.Views.Grid.GridView grid)
        {
            string path = ConfigurationManager.AppSettings["gridlayout"].ToString();
            string filename = path + strGridLayout + ".xml";
            grid.SaveLayoutToXml(filename);
            return "OK";
        }
        public string restoreLayout(DevExpress.XtraGrid.Views.Grid.GridView grid)
        {
            string path = ConfigurationManager.AppSettings["gridlayout"].ToString();
            string filename = path + strGridLayout + ".xml";
            if (File.Exists(filename))
            {
                grid.RestoreLayoutFromXml(filename);
            }
            return "OK";
        }
        public String GM_CierredemodulosStatus(int ejercicio,int mes,String Modulo)
        {
            try
            {
                String strResult = string.Empty;

                CierresdemodulosCL cl = new CierresdemodulosCL();
                cl.intEjercicio = ejercicio;
                cl.intMes = mes;
                cl.strModulo = Modulo;

                strResult = cl.CierresdemodulosStatus();

                if (strResult=="0")
                {
                    return "C";
                }else if (strResult=="1")
                {
                    return "A";
                } else
                {
                    return strResult;
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }

        public string Datosdecontrol()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DatosdecontrolLeer";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();                   
                    iImpresiondirecta = Convert.ToInt32(dr["Impresiondirecta"]);
                    sTipologo = dr["Tipologo"].ToString();
                    result = "OK";
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // Datosdecontrol
        public string xmlBoxBuscaCompraoCR()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "XmlBoxBuscaCompraoContrarecibo";
                cmd.Parameters.AddWithValue("@prmTipoProv", strTipoProv);
                cmd.Parameters.AddWithValue("@prmFac", strFac);
                cmd.Parameters.AddWithValue("@prmUUID", strUUID);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    intFolio = Convert.ToInt32(dr["Folio"]);
                    if (intFolio>0)
                        result = "OK";
                    else
                        result = "Sin captura";
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // Datosdecontrol

        public string XmlBoxGuardar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "XmlBoxCRUD";
                cmd.Parameters.AddWithValue("@prmXmlBox", dtxml);
                cmd.Parameters.AddWithValue("@prmEjer", intEjer);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.Parameters.AddWithValue("@prmUsuario", gv_UsuarioID);
                cmd.Parameters.AddWithValue("@prmMaquina", strMaq);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr["result"].ToString();
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // Datosdecontrol

        public string EmpresaleeDatos()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EmpresaLeeDatos";
                cmd.Parameters.AddWithValue("@prmEmp", intEmpresaID);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    strEmpresaNom = dr["Nombre"].ToString();
                    strEmpresaDir = dr["Direccion"].ToString();
                    strEmpresaRfc = dr["Rfc"].ToString();
                    strEmpresaTel = dr["Telefono"].ToString();
                    strEmpresaWWW = dr["www"].ToString();
                    strEmpresaRazonComercial = dr["Razoncomercial"].ToString();
                    strEmpresaFrasePie = dr["Frasepiecorreo"].ToString();

                    result = "OK";
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // Datosdecontrol

        public string CorreosEnviadosCrud()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CorreosenviadosCRUD";
                cmd.Parameters.AddWithValue("@prmDocumento",sDocumento);
                cmd.Parameters.AddWithValue("@prmSerie",sSerie);
                cmd.Parameters.AddWithValue("@prmFolio",iFolio);
                cmd.Parameters.AddWithValue("@prmPara",sPara);
                cmd.Parameters.AddWithValue("@prmSubject",sSubject);
                cmd.Parameters.AddWithValue("@prmFechadocumento",dFechaDoc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr["result"].ToString();
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // CorreosEnviadosCrud

        public string BuscaSiguienteID()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SiguienteID";
                cmd.Parameters.AddWithValue("@prmTabla", sTabla);
                cmd.Parameters.AddWithValue("@prmCondicion", sCondicion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    
                    iSigID = Convert.ToInt32(dr["SigID"]);
                    result = "OK";
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // Datosdecontrol

        public string Dejasolonumero(String dato)
        {
            string strDatoNuevo = string.Empty;

            for (int i=0; i <= dato.Length-1; i++)
            {
                if (dato.Substring(i,1) != "," && dato.Substring(i,1) != "$")
                {
                    strDatoNuevo = strDatoNuevo + dato.Substring(i, 1);
                }
            }

            return strDatoNuevo;
        }
      
        public string NombreDeMes(int Mes,int Largo)
        {
            string sMes = string.Empty;
            switch (Mes)
            {
                case 1:
                    sMes = "ENERO";
                    break;
                case 2:
                    sMes = "FEBRERO";
                    break;
                case 3:
                    sMes = "MARZO";
                    break;
                case 4:
                    sMes = "ABRIL";
                    break;
                case 5:
                    sMes = "MAYO";
                    break;
                case 6:
                    sMes = "JUNIO";
                    break;
                case 7:
                    sMes= "JULIO";
                    break;
                case 8:
                    sMes = "AGOSTO";
                    break;
                case 9:
                     sMes="SEPTIEMBRE";
                    break;
                case 10:
                    sMes= "OCTUBRE";
                    break;
                case 11:
                    sMes= "NOVIEMBRE";
                    break;
                case 12:
                    sMes = "DICIEMBRE";
                    break;
            }

            if (Largo==3)
            {
                if (sMes.Length>0)
                    sMes = sMes.Substring(0, 3);
                
            }

            return sMes;
        }
        public void AbreOutlook(string serie, int Folio, DateTime Fecha, string strTo)
        {
            Microsoft.Office.Interop.Outlook.Application m_Outlook = null;

            if (Process.GetProcessesByName("OUTLOOK").Count() > 0)
            {

                MessageBox.Show("Cierre outlook antes de envíar el correo");
                return;
                //m_Outlook = Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;

            }
            else
            {
                m_Outlook = new Microsoft.Office.Interop.Outlook.Application();
            }

            try
            {
                //Creamos un Objeto que hará referencia a nuestra aplicación Outlook 

                string rutaXML = string.Empty;
                string rutaPDF = string.Empty;
                string sYear = string.Empty;
                string sMes = string.Empty;

                sYear = Fecha.Year.ToString();
                sMes = NombreDeMes(Fecha.Month,3);


                rutaXML = ConfigurationManager.AppSettings["pathxml"].ToString() + "\\" + sYear + "\\" + sMes + "\\" + serie + Folio + ".xml";
                if (!File.Exists(rutaXML))
                {
                    MessageBox.Show("No existe el archivo: " + rutaXML);
                    return;
                }

                rutaPDF = ConfigurationManager.AppSettings["pathpdf"].ToString() + "\\" + sYear + "\\" + sMes + "\\";
                if (!Directory.Exists(rutaPDF))
                {
                    Directory.CreateDirectory(rutaPDF);
                }
                rutaPDF = rutaPDF + serie + Folio + ".pdf";


                string strMail = string.Empty;                
                string strBody = string.Empty;

                //Creamos un objeto tipo Mail
                Microsoft.Office.Interop.Outlook.MailItem objMail;

                //Creamos una instancia de un objeto tipo MailItem
                objMail = m_Outlook.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                //Asignamos las propiedades a nuestra Instancial del objeto MailItem
                objMail.To = strTo;
                objMail.Subject = "Envio de su comprobante fiscal (CFDI)";

                strBody = "Buen día\n\n";
                strBody = strBody + "Anexo al presente la factura, en archivos XML Timbrado y PDF, para su programación de pago.\n\n\n";
                strBody = strBody + "Saludos";

                objMail.Body = strBody;
                //Archivo adjunto
                String sSourceXml = string.Empty;
                String sDisplayName = serie + Folio.ToString();
                String sBodyLen = objMail.Body.Length.ToString();
                Microsoft.Office.Interop.Outlook.Attachments oAttachs = objMail.Attachments;
                Microsoft.Office.Interop.Outlook.Attachment oAttach;

                oAttach = oAttachs.Add(rutaXML, Type.Missing, sBodyLen + 1, sDisplayName);
                oAttach = oAttachs.Add(rutaPDF, Type.Missing, sBodyLen + 1, sDisplayName);

                DatosdecontrolCL cld = new DatosdecontrolCL();
                string result = cld.DatosdecontrolLeer();
                if (result=="OK")
                {
                    iAbrirOutlook = cld.iAbrirOutlook;
                    if (cld.iAbrirOutlook==1)
                    {
                        objMail.Display();
                    }
                    else
                    {
                        
                        objMail.Send();
                        
                    }
                }
                else
                {
                    iAbrirOutlook = 1;
                    objMail.Display();
                }
                                
                m_Outlook = null;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                m_Outlook = null;
            }
        }

        public void VerificaComprobante(string serie, int Folio, DateTime Fecha)
        {
            Mandracosoft.VerificacionDeComprobantes vc = new Mandracosoft.VerificacionDeComprobantes();
            vsFK.vsFinkok vs = new vsFK.vsFinkok();

            string rutaXML = string.Empty;
            string sYear = string.Empty;
            string sMes = string.Empty;
            string sRfcEmisor = string.Empty;
            string sRfcReceptor = string.Empty;
            string sUUID = string.Empty;

            if (serie.Length>10)
            {
                rutaXML = serie;  // Se usa para cuando se llama desde ComprasXML
            }
            else
            {
            
                sYear = Fecha.Year.ToString();
                sMes = NombreDeMes(Fecha.Month,3);


                rutaXML = ConfigurationManager.AppSettings["pathxml"].ToString() + "\\" + sYear + "\\" + sMes + "\\" + serie + Folio + "timbrado.xml";
                if (!File.Exists(rutaXML))
                {
                    MessageBox.Show("No existe el archivo: " + rutaXML);
                    return;
                }

            }

            sUUID = vs.ExtraeValor(rutaXML, "tfd:TimbreFiscalDigital", "UUID");
            sRfcEmisor = vs.ExtraeValor(rutaXML, "cfdi:Emisor", "Rfc");
            sRfcReceptor = vs.ExtraeValor(rutaXML, "cfdi:Receptor", "Rfc");

            vc.Uuid = sUUID;
            vc.Emisor = sRfcEmisor;
            vc.Receptor = sRfcReceptor;

            vc.Go();
            vc.ShowDialog();
        } //Verifica comprobante

        public string EnviaCorreo(string strEmailTo,string strSerie,int intFolio,DateTime dFecha,string sDoc)
        {
            try
            {
                string strEmail = strEmailTo;
                string strpdf = string.Empty;
                string strXml = string.Empty;
                string strFooter = ConfigurationManager.AppSettings["Footer"].ToString();

                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                string Año = dFecha.Year.ToString();
                int Mes = dFecha.Month;

                string strTimbrado = "";
                if (strSerie.Length == 0)
                    strTimbrado = "Timbrado";

                strpdf = ConfigurationManager.AppSettings["pathpdf"].ToString() + Año + "\\" + NombreDeMes(Convert.ToInt32(Mes),3) + "\\" + strSerie + intFolio.ToString() + strTimbrado + ".pdf";
                strXml = ConfigurationManager.AppSettings["pathxml"].ToString() + Año + "\\" + NombreDeMes(Convert.ToInt32(Mes),3) + "\\" + strSerie + intFolio.ToString() + strTimbrado + ".xml";

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

                vs.emailSubject = "Envío de CFDI " + strSerie + intFolio.ToString();
                vs.emailTO = strEmail;
                vs.emailXML = strXml;
                vs.emailPDF = strpdf;
                vs.Bcc = "";

                string strBody = "Estimado cliente le hacemos llegar el pdf y xml del cfdi, cualquier duda favor de comunicarse con nosotros";

                vs.emailBody = strBody;

                string strresult = vs.Enviar_Correo();
                
                if (strresult.Substring(0,28)== "Correo enviado correctamente")
                {
                    sDocumento = sDoc;
                    sSerie = strSerie;
                    iFolio = intFolio;
                    sPara = strEmailTo;
                    sSubject= "Envío de CFDI " + strSerie + intFolio.ToString();
                    dFechaDoc = dFecha;
                    string result2=CorreosEnviadosCrud();
                    if (result2!="OK")
                    {
                        return "Enviado correctamente, pero al guardar el envio: " + result2;

                    }
                }

                return strresult;

                

            } //try
            catch (Exception ex)
            {
                return "Enviacorreo :" + ex.Message;
            }


        } //Envia correo

        public string EnviaCorreoSalida(string strEmailTo, string strSerie, int intFolio)
        {
            try
            {
                string strEmail = strEmailTo;
                string strpdf = string.Empty;
                string strXml = string.Empty;
                string strFooter = ConfigurationManager.AppSettings["Footer"].ToString();

                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                

            
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

                vs.emailSubject = "Salida por autorizar" + strSerie + intFolio.ToString();
                vs.emailTO = strEmail;
                vs.emailXML = strXml;
                vs.emailPDF = strpdf;
                vs.Bcc = "";

                string strBody = "Se solicita autorización para la salida: " + strSerie + intFolio.ToString();

                vs.emailBody = strBody;

                string strresult = vs.Enviar_Correo();

                if (strresult.Substring(0, 28) == "Correo enviado correctamente")
                {
                    return "OK";
                }
                else
                    return strresult;



            } //try
            catch (Exception ex)
            {
                return "Enviacorreosalida :" + ex.Message;
            }


        } //Envia correo

        public string EnviaCorreoCotizacion(string strEmailTo, string strFileName, string strSerieFolio,string strFrom)
        {


            try
            {

                string strEmail = strEmailTo;
                string strpdf = strFileName;
                string strXml = string.Empty;
                string strFooter = ConfigurationManager.AppSettings["Footer"].ToString();

                vsFK.vsFinkok vs = new vsFK.vsFinkok();


                string strHost = ConfigurationManager.AppSettings["EMailHost"].ToString();
                string strPort = ConfigurationManager.AppSettings["Emailport"].ToString();
                string strUserName = ConfigurationManager.AppSettings["Emailusername"].ToString();
                string strPassword = ConfigurationManager.AppSettings["Emailpassword"].ToString();
                string strSSL = ConfigurationManager.AppSettings["Emailssl"].ToString();

                vs.emailFrom = strFrom;
                    //ConfigurationManager.AppSettings["EMailFrom"].ToString();
                vs.emailFromName = ConfigurationManager.AppSettings["EMailFromName"].ToString();
                vs.emailSmtpUserName = strUserName;
                vs.emailSmtpPassword = strPassword;
                vs.emailSmtpHost = strHost;
                vs.emailSmtpPort = strPort;
                vs.emailSSL = strSSL;
                vs.Bcc = "";

                string strLink = ConfigurationManager.AppSettings["Link"].ToString();

                vs.emailSubject = "Envío de cotización: " + strSerieFolio;
                vs.emailTO = strEmail;
                vs.emailXML = strXml;
                vs.emailPDF = strpdf;

                vs.emailFrom = strUserName;

                string strBody = "Estimado cliente le hacemos llegar el presupuesto solicitado, cualquier duda favor de comunicarse con nosotros";

                vs.emailBody = strBody;

                string strresult = vs.Enviar_Correo();
                return strresult;

            } //try
            catch (Exception ex)
            {
                return "Enviacorreocotizacion :" + ex.Message;
            }


        } //Envia correo

        public void Bitacora(string strMensaje)
        {
            try
            {
                try
                {
                    //Pass the filepath and filename to the StreamWriter Constructor
                    StreamWriter sw = new StreamWriter("C:\\VisualsoftErp\\Bitacora.txt",append:true);

                    //Write a line of text
                    string mensaje = DateTime.Now.ToShortDateString() + " : " + DateTime.Now.ToShortTimeString() + " : " + strMensaje;
                    sw.WriteLine(mensaje);

                    

                    //Close the file
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    Console.WriteLine("Executing finally block.");
                }
            }
            catch(Exception)
            {

            }
        }

        public string RenombrarXMLTimbrado(string Serie,int Folio,DateTime Fecha)
        {
            try
            {
                                       
                string pRutaXML = System.Configuration.ConfigurationManager.AppSettings["pathxml"];

                string sYear = Fecha.Year.ToString();
                int Mes = Fecha.Month;
                pRutaXML = pRutaXML + sYear + "\\" + NombreDeMes(Mes,3) + "\\" + Serie + Folio.ToString() + ".XML";
                string pRutaXMLPorCancelar = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString() + sYear + "\\" + NombreDeMes(Mes,3) + "\\" + Serie + Folio.ToString() + "_PORCANCELAR.XML";

                if (!File.Exists(pRutaXML))
                {
                    return "No se encontró el XML: " + pRutaXML;
                }

                File.Move(pRutaXML, pRutaXMLPorCancelar);

                return "OK";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public Boolean HayInternet()
        {
            try
            {

                Ping pinger = new Ping();
                PingReply lPingReply;
                lPingReply = pinger.Send("52.12.141.94");
              //  MessageBox.Show(lPingReply.Status.ToString());
                if (lPingReply.Status == IPStatus.Success)
                {
                    return true;
                }
                lPingReply = pinger.Send("visualsoft.com.mx");
                //  MessageBox.Show(lPingReply.Status.ToString());
                if (lPingReply.Status == IPStatus.Success)
                {
                    return true;
                }
                return false;
            }
            catch(PingException) {
                return false;
            }            
        }

        public string leeUUIDNuevo(string strDoc,string serieNueva,string folioNuevo,int Cte)
        {
            try
            {
                if (folioNuevo.Length == 0)
                {                    
                    return "Teclee el folio nuevo";
                }

                FacturasCL cl = new FacturasCL();
                cl.strSerie = serieNueva;
                cl.intFolio = Convert.ToInt32(folioNuevo);
                cl.strDoc = strDoc;

                string result = cl.LeeDocFecha();
                if (result != "OK")
                {
                    return "No existe el cfdi nuevo";
                }
                if (cl.intClientesID != Cte)
                {
                    return "Este folio no es del mismo cliente";
                }

                string ArchivoXml;
                string RutaXML = System.Configuration.ConfigurationManager.AppSettings["pathxml"];
                ArchivoXml = RutaXML + cl.fFecha.Year.ToString() + "\\" + NombreDeMes(cl.fFecha.Month) + "\\" + serieNueva + folioNuevo + "timbrado.xml";
                if (!File.Exists(ArchivoXml))
                    ArchivoXml = RutaXML + cl.fFecha.Year.ToString() + "\\" + NombreDeMes(cl.fFecha.Month) + "\\" + serieNueva + folioNuevo + ".xml";

                vsFK.vsFinkok vs = new vsFK.vsFinkok();
                string strUUID = vs.ExtraeValor(ArchivoXml, "tfd:TimbreFiscalDigital", "UUID");
                if (strUUID.Length > 0)
                    return strUUID;
                else
                    return "Error: No se pudo leer el nuevo uuid";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string CancelaTimbrado(string strSerie,string RutaXml,string NombreArchivo,string strMotivo,string strUUIDNuevo)
        {
            try
            {

                if (strMotivo != "01" && strMotivo != "02" && strMotivo != "03" && strMotivo != "04")
                {
                    return "Capture el motivo de cancelación";
                }
                if (strMotivo == "01")
                {
                    if (strUUIDNuevo.Length == 0)
                    {
                        return "Si el motivo es 01, el UUID nuevo no puede ir en blanco";
                    }
                    if (strUUIDNuevo.Substring(0, 5) == "Error")
                    {
                        return "Si el motivo es 01, el UUID nuevo debe existir";
                    }
                }

                string PathyNombre = RutaXml; // + NombreArchivo;
               
                if (!File.Exists(PathyNombre))
                {
                    return "No existe el archivo: " + PathyNombre;
                }

                string strnombrearchivo = NombreArchivo;

                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                string pRutaCer = System.Configuration.ConfigurationManager.AppSettings["pathcer"];
                string pRutaKey = System.Configuration.ConfigurationManager.AppSettings["pathkey"];
                string pRutaOpenSSL = System.Configuration.ConfigurationManager.AppSettings["pathopenssl"];

                string rfc = vs.ExtraeValor(PathyNombre, "cfdi:Emisor", "Rfc");
                if (rfc == null)
                {
                    return "No se pudo leer el Rfc del XML: " + PathyNombre;
                }
                string strUUID = vs.ExtraeValor(PathyNombre, "tfd:TimbreFiscalDigital", "UUID");
                if (strUUID == null)
                {
                    return "No se pudo extraer el UUID del xml: " + PathyNombre;
                }

                string strLlave = string.Empty;

                cfdiCL clc = new cfdiCL();

                clc.pSerie = strSerie;
                string strresult = clc.DatosCfdiEmisor();
                if (strresult == "OK")
                {
                    strLlave = clc.pLlaveCFD;
                }
                else
                {
                    return "No se pudo leer cfdiEmisor";
                }

                if (strMotivo != "01")
                    strUUIDNuevo = string.Empty;
                else
                {
                    if (strUUID == strUUIDNuevo)
                        return "El uuid a cancelar y el nuevo no pueden ser iguales";
                }

                if (rfc == "LAN7008173R5")
                {
                    strresult = vs.CancelaTimbradoDemostracion(pRutaCer, pRutaKey, pRutaOpenSSL, strLlave, RutaXml, rfc, strUUID, strnombrearchivo,strMotivo,strUUIDNuevo);
                }
                else
                {
                    strresult = vs.CancelaTimbrado(pRutaCer, pRutaKey, pRutaOpenSSL, strLlave, RutaXml, rfc, strUUID, strnombrearchivo, strMotivo, strUUIDNuevo);
                }


                if (strresult == "Cancelacion exitosa" || strresult == "CFDI previamente cancelado")
                {
                    return "OK";
                }
                else
                {
                    return strresult;
                }

            }
            catch (Exception ex)
            {
                return "Cancela timbrado:" + ex.Message;
            }
        }

        public DataTable LeeXml(string filename)
        {

            DataTable dt = new DataTable();

            dt.Columns.Add("ClaveProdServ", System.Type.GetType("System.String"));
            dt.Columns.Add("NoIdentificacion", System.Type.GetType("System.String"));
            dt.Columns.Add("Cantidad", System.Type.GetType("System.String"));
            dt.Columns.Add("ClaveUnidad", System.Type.GetType("System.String"));
            dt.Columns.Add("Unidad", System.Type.GetType("System.String"));
            dt.Columns.Add("Descripcion", System.Type.GetType("System.String"));
            dt.Columns.Add("ValorUnitario", System.Type.GetType("System.String"));
            dt.Columns.Add("Importe", System.Type.GetType("System.String"));
            dt.Columns.Add("Rfc", System.Type.GetType("System.String"));
            dt.Columns.Add("Cliente", System.Type.GetType("System.String"));
            dt.Columns.Add("Fecha", System.Type.GetType("System.String"));
            dt.Columns.Add("Moneda", System.Type.GetType("System.String"));

            string Rfc = string.Empty;
            string Cliente = string.Empty;
            string Fecha = string.Empty;
            string Moneda = string.Empty;

            vsFK.vsFinkok vs = new vsFK.vsFinkok();
            Rfc = vs.ExtraeValor(filename, "cfdi:Receptor", "Rfc");
            Fecha = vs.ExtraeValor(filename, "cfdi:Comprobante", "Fecha");
            Moneda = vs.ExtraeValor(filename, "cfdi:Comprobante", "Moneda");

            cfdiCL cl = new cfdiCL();

            cl.pReceptorRegFed = Rfc;
            string result = cl.LeeClienteporRfc();
            if (result=="OK")
            {
                Cliente = cl.pReceptorNombre;
            }
            else
            {
                Cliente = result;
            }

            string sclaveProdServ = string.Empty;
            string sNoIdentificacion = string.Empty;
            string sCantidad = string.Empty;
            string sClaveUnidad = string.Empty;
            string sUnidad = string.Empty;
            string sDescripcion = string.Empty;
            string sValorUnitario = string.Empty;
            string sImporte = string.Empty;

            string attrName = string.Empty;
            string attrImptoName = string.Empty;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {

                if (element.Name.Equals("cfdi:Conceptos"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        //MessageBox.Show(node.Name.ToString());
                        if (node.Name.Equals("cfdi:Concepto"))
                        {

                            //MessageBox.Show("Dentro de cpto:" + node.Name.ToString());



                            foreach (XmlAttribute attr in node.Attributes)
                            {
                                attrName = attr.Name;

                                switch (attrName)
                                {
                                    case "ClaveProdServ":
                                        sclaveProdServ = attr.Value.ToString();
                                        break;
                                    case "NoIdentificacion":
                                        sNoIdentificacion = attr.Value.ToString();
                                        break;
                                    case "Cantidad":
                                        sCantidad = attr.Value.ToString();
                                        break;
                                    case "ClaveUnidad":
                                        sClaveUnidad = attr.Value.ToString();
                                        break;
                                    case "Unidad":
                                        sUnidad = attr.Value.ToString();
                                        break;
                                    case "Descripcion":
                                        sDescripcion = attr.Value.ToString();
                                        break;
                                    case "ValorUnitario":
                                        sValorUnitario = attr.Value.ToString();
                                        break;
                                    case "Importe":
                                        sImporte = attr.Value.ToString();
                                        break;
                                }
                            }



                            dt.Rows.Add(sclaveProdServ, sNoIdentificacion, sCantidad, sClaveUnidad, sUnidad, sDescripcion, sValorUnitario, sImporte,Rfc,Cliente,Fecha,Moneda);

                            //Impuestos
                            foreach (XmlNode nl in node.ChildNodes)
                            {
                                //MessageBox.Show(nl.Name.ToString());
                                foreach (XmlNode nl2 in nl.ChildNodes)
                                {
                                    //MessageBox.Show(nl2.Name.ToString());
                                    foreach (XmlNode nl3 in nl2.ChildNodes)
                                    {
                                        if (nl3.Name.Equals("cfdi:Traslado"))
                                        {
                                            foreach (XmlAttribute attrI in nl3.Attributes)
                                            {
                                                attrImptoName = attrI.Name;
                                                //MessageBox.Show(attrImptoName + " " + attrI.Value.ToString());
                                            }

                                        }

                                    }
                                }
                            }


                        }


                    }
                }
            } // foreach (XmlElement element in xmlDoc.DocumentElement) 

            return dt;

        } //LeeXML

        public DataTable XmlBoxGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "XmlBoxGRID";
                cmd.Parameters.AddWithValue("@prmEjer", intEjer);
                cmd.Parameters.AddWithValue("@prmMes", intMes);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //XmlBoxGrid
        public DataTable NavbarAños()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NavBarAñosRegistrados";
                cmd.Parameters.AddWithValue("@prmTabla", strTabla);              
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //agentesGrid

        public string CatalogoSAT_LeeNombre()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CatalogosSAT_LeeNombre";
                cmd.Parameters.AddWithValue("@prmTabla", strTabla);
                cmd.Parameters.AddWithValue("@prmclave", strClave);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    strDes = dr["Des"].ToString();
                    result = "OK";
                }
                else
                {
                    result = "no read";
                }
                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }//CatalogoSAT_LeeNombre()

        public bool accesoSoloLectura()
        {
            try
            {
                bool result = false;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AccesosSoloLectura";
                cmd.Parameters.AddWithValue("@prmUsu", globalCL.gv_UsuarioID);
                cmd.Parameters.AddWithValue("@prmProg", strPrograma);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (Convert.ToInt32(dr["Sololectura"]) == 1)
                        result = true;
                    else
                        result = false;                                        
                }
                else
                {
                    result = true;
                }
                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception)
            {
                return true;
            }

        }//CatalogoSAT_LeeNombre()
        public int CargaInicialLeer()
        {
            try
            {
               

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CargaInicialLeer";
                cmd.Parameters.AddWithValue("@prmUsu", gv_UsuarioID);
                cmd.Parameters.AddWithValue("@prmPro", strPrograma);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intCargar = Convert.ToInt32(dr["Cargar"]);
                 
                }
                else
                {
                    intCargar = 1;  //Si no existe el registro se carga, x default siempre se carga a menos que seleccionen que no
                }

                dr.Close();
                cnn.Close();
                return intCargar;
            }
            catch (Exception ex)
            {
                return 1;
            }
        } // Datosdecontrol
        public string DocumentosSiguienteID()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DocumentosSiguienteID";
                cmd.Parameters.AddWithValue("@prmSerie", strSerie);
                cmd.Parameters.AddWithValue("@prmDoc", strDoc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    iFolio = Convert.ToInt32(dr["SigID"]);
                    result = "OK";
                }
                else
                {
                    result = "no read";
                }
                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string AppOrdenesporatender()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AppOrdenesPorAtender";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intOrdenesPorAtender = Convert.ToInt32(dr["Ordenes"]);
                    intSolicitudesDeRegistro = Convert.ToInt32(dr["Solicitudes"]);
                    result = "OK";
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // AppOrdendesporatender
        public string Salidasporautorizar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EntradasySalidasPorAutorizar";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    intSalidasporautorizar = Convert.ToInt32(dr["PorAutorizar"]);
                    result = "OK";
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } // AppOrdendesporatender

        public bool esFecha(string sFecha)
        {
            DateTime temp;
            if (DateTime.TryParse(sFecha, out temp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string FechaSQL(DateTime Fecha)
        {
            try
            {
                string Año = string.Empty;
                string Mes = string.Empty;
                string Dia = string.Empty;
                string FechaSQL = string.Empty;

                Año = Fecha.Year.ToString();

                if (Fecha.Month < 10)
                    Mes = "0" + Fecha.Month.ToString();
                else
                    Mes = Fecha.Month.ToString();

                if (Fecha.Day < 10)
                    Dia = "0" + Fecha.Day.ToString();
                else
                    Dia = Fecha.Day.ToString();


                FechaSQL = Año + "-" + Mes + "-" + Dia;

                return FechaSQL;

            }
            catch (Exception ex)
            {
                return "";
            }
        }


        #endregion

        #region FTP
        public string descargaFtp(string pathLocal,string Archivo)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://www.ocupoalgo.com:21/diexsa/" + Archivo);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential("ftp_diexsa", "Allende2023.");

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                using (Stream ftpStream = response.GetResponseStream())
                using (Stream fileStream = File.Create(pathLocal + Archivo))
                {
                    ftpStream.CopyTo(fileStream);
                }

                response.Close();

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string subeFtp(string localFileFullPath,string fileName)
        {
            try
            {

                using (var client = new WebClient())
                {
                    //string localFile = @"C:\VisualSoftErp\Xml33\2023\ene\NCR10918.xml";
                    client.Credentials = new NetworkCredential("ftp_diexsa", "Allende2023.");
                    client.UploadFile("ftp://www.ocupoalgo.com:21/diexsa/" + fileName, WebRequestMethods.Ftp.UploadFile, localFileFullPath);
                }

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
        #endregion
    }
}
