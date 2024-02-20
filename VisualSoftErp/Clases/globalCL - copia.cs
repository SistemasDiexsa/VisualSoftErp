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
using VisualSoftErp.Operacion.Compras.Clases;

namespace VisualSoftErp.Clases
{

    public class globalCL
    {
        #region Propiedades
        string strCnn = ConfigurationManager.ConnectionStrings["VisualSoftErpConnectionString"].ConnectionString;
        public static DevExpress.XtraBars.Ribbon.RibbonPage gv_RibbonPage;
        public static int gv_UsuarioID;
        public static string gv_UsuarioNombre;
        public static string gv_NombreEmpresa;
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
        }
        #endregion
        #region Metodos

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

        public LookUpEdit comboMeses()
        {
            LookUpEdit cbo = new LookUpEdit();

            combosCL cl = new combosCL();
            List<ClaseGenricaCL> mesescl = new List<ClaseGenricaCL>();
            mesescl.Add(new ClaseGenricaCL() { Clave = "1", Des = "Enero" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "2", Des = "Febrero" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "3", Des = "Marzo" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "4", Des = "Abril" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "5", Des = "Mayo" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "6", Des = "Junio" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "7", Des = "Julio" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "8", Des = "Agosto" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "9", Des = "Septiembre" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "10", Des = "Octubre" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "11", Des = "Noviembre" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "12", Des = "Diciembre" });


            cl.strTabla = "";
            cbo.Properties.ValueMember = "Clave";
            cbo.Properties.DisplayMember = "Des";
            cbo.Properties.DataSource = mesescl;
            cbo.Properties.ForceInitialize();
            cbo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cbo.Properties.PopulateColumns();
            cbo.Properties.Columns["Clave"].Visible = false;


            return cbo;
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
            return true;

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
                cmd.CommandText = "cyb_DatosdecontrolLeer";
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
                sMes = NombreDeMes(Fecha.Month);


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
                sMes = NombreDeMes(Fecha.Month);


                rutaXML = ConfigurationManager.AppSettings["pathxml"].ToString() + "\\" + sYear + "\\" + sMes + "\\" + serie + Folio + ".xml";
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

                strpdf = ConfigurationManager.AppSettings["pathpdf"].ToString() + Año + "\\" + NombreDeMes(Convert.ToInt32(Mes)) + "\\" + strSerie + intFolio.ToString() + ".pdf";
                strXml = ConfigurationManager.AppSettings["pathxml"].ToString() + Año + "\\" + NombreDeMes(Convert.ToInt32(Mes)) + "\\" + strSerie + intFolio.ToString() + ".xml";

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

        public string EnviaCorreoCotizacion(string strEmailTo, string strFileName, string strSerieFolio)
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

                vs.emailFrom = ConfigurationManager.AppSettings["EMailFrom"].ToString();
                vs.emailFromName = ConfigurationManager.AppSettings["EMailFromName"].ToString();
                vs.emailSmtpUserName = strUserName;
                vs.emailSmtpPassword = strPassword;
                vs.emailSmtpHost = strHost;
                vs.emailSmtpPort = strPort;
                vs.emailSSL = strSSL;

                string strLink = ConfigurationManager.AppSettings["Link"].ToString();

                vs.emailSubject = "Envío de cotización: " + strSerieFolio;
                vs.emailTO = strEmail;
                vs.emailXML = strXml;
                vs.emailPDF = strpdf;

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
                pRutaXML = pRutaXML + sYear + "\\" + NombreDeMes(Mes) + "\\" + Serie + Folio.ToString() + ".XML";
                string pRutaXMLPorCancelar = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString() + sYear + "\\" + NombreDeMes(Mes) + "\\" + Serie + Folio.ToString() + "_PORCANCELAR.XML";

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

        public string CancelaTimbrado(string strSerie,string RutaXml,string NombreArchivo)
        {
            try
            {
                string PathyNombre = RutaXml + NombreArchivo;
               
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

                if (rfc == "LAN7008173R5")
                {
                    strresult = vs.CancelaTimbradoDemostracion(pRutaCer, pRutaKey, pRutaOpenSSL, strLlave, RutaXml, rfc, strUUID, strnombrearchivo);
                }
                else
                {
                    strresult = vs.CancelaTimbrado(pRutaCer, pRutaKey, pRutaOpenSSL, strLlave, RutaXml, rfc, strUUID, strnombrearchivo);
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
        } //agentesGrid
        #endregion
    }
}
