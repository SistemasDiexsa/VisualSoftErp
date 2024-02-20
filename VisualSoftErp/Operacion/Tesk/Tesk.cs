using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeskApiTest.Clases;
using TeskApiTest.Request;
using VisualSoftErp.Clases;
using VisualSoftErp.Clases.Tesk.Request;
using VisualSoftErp.Clases.Tesk.Response;
using VisualSoftErp.Interfaces;

namespace VisualSoftErp.Operacion.Tesk
{
    public partial class Tesk : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        DataTable dtResult;
        string Autorization = string.Empty;
        string CodigoEmpresa = string.Empty;
        bool firstTime;
        public Tesk()
        {
            InitializeComponent();
            
            txtEjer.Text = DateTime.Now.Year.ToString();
            txtMes.Text = DateTime.Now.Month.ToString();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void InicializaProgress()
        {
            

            progressBarControl1.Properties.Step = 1;
            progressBarControl1.Properties.PercentView = true;
            progressBarControl1.Properties.Minimum = 0;
            progressBarControl1.Properties.Maximum = 0;



            progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            progressBarControl1.Properties.EndColor = System.Drawing.Color.SteelBlue;
            progressBarControl1.Properties.StartColor = System.Drawing.Color.PowderBlue;
            progressBarControl1.Properties.ShowTitle = true;
            progressBarControl1.Properties.LookAndFeel.SetStyle(DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat, false, false);

            

        }

        private bool validaEjercicio()
        {
            globalCL clg = new globalCL();
            if (!clg.esNumerico(txtEjer.Text))
            {
                MessageBox.Show("Tecle el ejercicio");
                return false;
            }
            if (!clg.esNumerico(txtMes.Text))
            {
                MessageBox.Show("Tecle el mes");
                return false;
            }

            if (Convert.ToInt32(txtMes.Text) < 1 || Convert.ToInt32(txtMes.Text) > 12)
            {
                MessageBox.Show("Teclee un mes correcto");
                return false;
            }

            globalCL cl = new globalCL();

            string result = cl.GM_CierredemodulosStatus(Convert.ToInt32(txtEjer.Text), Convert.ToInt32(txtMes.Text), "VTA");
            if (result == "C")
            {
                MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                return false;
            }


            return true;
        }
                
        private void bbiCFDI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (!validaEjercicio())
                return;

            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Subir información", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Conectando con tesk...");



            IniTabla();
            string result = Login();

            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "CFDI";

            CargaCfdi();

            gridControl1.DataSource = dtResult;

            MessageBox.Show("Proceso terminado");
        }

        private void IniTabla()
        {
            dtResult = new DataTable();
            dtResult.Columns.Add("Mensaje", Type.GetType("System.String"));
            firstTime = true;
            InicializaProgress();
        }

        private void CargaCfdi()
        {
            string strErr = string.Empty;
            string strSerie = string.Empty;
            string strFolio = string.Empty;
            try
            {

                erpCL cl = new erpCL();
                vsFK.vsFinkok vs = new vsFK.vsFinkok();
                string xmltext = string.Empty;
                string articulo = string.Empty;
                string mensajeErr = string.Empty;
                string serie = string.Empty;

                string tipoComprobante = string.Empty;
                string extension = string.Empty;
                MalocClient cliente = new MalocClient();

                cfdiRequest dataRequest = new cfdiRequest();
                string url = "https://app.tesk.mx/diexsa/v1/facturas/";

                string xmlpath = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString();
                //xmlpath = xmlpath + DateTime.Now.Year + "\\" + Nombredemes(DateTime.Now.Month);
                string strFile = string.Empty;

                DateTime fecha;

                DataTable dt = new DataTable();
                cl.intDato = 0;
                cl.strTabla = "Cfdi";
                cl.intEjer = Convert.ToInt32(txtEjer.Text);
                cl.intMes = Convert.ToInt32(txtMes.Text);

                dt = cl.Datos();
                progressBarControl1.Properties.Maximum = dt.Rows.Count;

                if (dt.Rows.Count == 0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("No hay información: Cfdi encontrados:" + dt.Rows.Count.ToString());
                    return;
                }                            

                globalCL clg = new globalCL();

                //DirectoryInfo di = new DirectoryInfo(@xmlpath);
                foreach (DataRow dr in dt.Rows)  //(var fi in di.GetFiles())
                {
                    strSerie = dr["Serie"].ToString();
                    strFolio = dr["Folio"].ToString();
                    fecha = Convert.ToDateTime(dr["fecha"]);
                    strFile = xmlpath + fecha.Year + "\\" + clg.NombreDeMes(fecha.Month) + "\\" + strSerie + strFolio + "timbrado.xml";

                    //extension = Path.GetExtension(fi.Name);
                    if (File.Exists(strFile))
                    {

                        tipoComprobante = vs.ExtraeValor(strFile, "cfdi:Comprobante", "TipoDeComprobante");

                        if (tipoComprobante == "I")
                        {
                            xmltext = File.ReadAllText(strFile);
                            //xmltext = xmltext.Replace("DEX9201028BA", "EKU9003173C9");  -- Esto se uso para demo

                            dataRequest.cfdiXml = xmltext;


                            erroresResponse datos = cliente.PeticionesWSPostWithHeadersReturnZip<erroresResponse>(url, "ContabilizarCfdi", Autorization, CodigoEmpresa, dataRequest);
                            if (firstTime)
                            {
                                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                                firstTime = false;
                            }
                            if (datos != null)
                            {
                                mensajeErr = strSerie + strFolio + " : " + datos.mensaje;
                                bitacoraErrores(mensajeErr, "PostCfdi");
                                //mensajeErr = datos.mensaje.Substring(60, 47).ToString();
                                //if (mensajeErr == "no se encontró concepto necesario con el código")
                                //{                                    
                                //    strErr = "Algun artículo del CFDI: " + strSerie + strFolio + " no se encuentra en Servicios";
                                //    bitacoraErrores(strErr, "PostCfdi");
                                //}
                                //else
                                //{
                                //    mensajeErr = datos.mensaje.Substring(56, 45).ToString();
                                //    if (mensajeErr == "No se encontró la cuenta contable de clientes")
                                //    {
                                //        strErr = "El cliente del CFDI: " + strSerie + strFolio + " no se encuentra en clientes";
                                //        bitacoraErrores(strErr, "PostCfdi");
                                //    }
                                //    else
                                //    {
                                //        mensajeErr = datos.mensaje.Substring(60, 49).ToString();
                                //        if (mensajeErr == "No se puede guardar factura de un período cerrado")
                                //        {
                                //            strErr = "No se puede guardar factura de un período cerrado: " + strSerie + strFolio + " no se encuentra en clientes";
                                //            bitacoraErrores(strErr, "PostCfdi");
                                //        }
                                //    }
                                //}

                            }
                            else
                            {
                                //Se actualiza campo tesk en visualsoft, para saber que ya está sincronizado

                                //serie = vs.ExtraeValor(xmlpath + "\\" + fi.Name, "cfdi:Comprobante", "Serie");
                                //folio = Convert.ToInt32(vs.ExtraeValor(xmlpath + "\\" + fi.Name, "cfdi:Comprobante", "Folio"));

                                cl.strTabla = "Facturas";
                                cl.strSerie = strSerie;
                                cl.intFolio = Convert.ToInt32(strFolio);
                                

                                string result = cl.teskSincronizaErp();

                                if (result != "OK")
                                {
                                    mensajeErr = "Al sincronizar factura: " + strSerie + strFolio + " " + result;
                                    bitacoraErrores(mensajeErr, "cfdi");
                                }
                            }

                            string x = "";
                        }

                    }
                    else
                    {
                        mensajeErr = "No se encontro el xml: " + strSerie + strFolio.ToString();
                        bitacoraErrores(mensajeErr, "cfdi");
                    }

                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();
                }

                mensajeErr = "Termino: " + DateTime.Now.ToShortTimeString();
                bitacoraErrores(strErr, "PostCfdi");

                string y = "";
            }
            catch (Exception ex)
            {
                strErr = "Error PostCfdi: " + strSerie + strFolio + " " + ex.Message;
                bitacoraErrores(strErr, "PostCfdi");
            }
        }  //CargaCfdi

        private void cancelaCfdi()
        {
            string strErr = string.Empty;
            string mensajeErr = string.Empty;
            string serie = string.Empty;
            int folio = 0;

            try
            {
                erpCL cl = new erpCL();

                MalocClient cliente = new MalocClient();

                DataTable dt = new DataTable();
                cl.intDato = 0;
                cl.strTabla = "CancelaCfdi";
                cl.intEjer = Convert.ToInt32(txtEjer.Text);
                cl.intMes = Convert.ToInt32(txtMes.Text);

                dt = cl.Datos();
                if (dt.Rows.Count == 0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("No hay información");
                    return;
                }
                progressBarControl1.Properties.Maximum = dt.Rows.Count;

                mensajeErr = "Inicio: " + DateTime.Now.ToShortTimeString();
                bitacoraErrores(strErr, "CancelarCfdi");

                //DirectoryInfo di = new DirectoryInfo(@xmlpath);
                foreach (DataRow dr in dt.Rows)
                {
                    serie = dr["Serie"].ToString();
                    folio = Convert.ToInt32(dr["Folio"]);

                    cancelaCfdiRequest dataRequest = new cancelaCfdiRequest();
                    dataRequest.fechaCancelacionSat = Convert.ToDateTime(dr["FechaCancelacion"]).ToShortDateString();
                    dataRequest.fechaPolizaCancelacion = Convert.ToDateTime(dr["FechaCancelacion"]).ToShortDateString();
                    dataRequest.folio = folio;
                    dataRequest.selloCancelacionSat = "";
                    dataRequest.serie = serie;

                    string url = "https://app.tesk.mx/diexsa/v1/facturas/";
                    erroresResponse datos = cliente.PeticionesWSPostWithHeadersReturnZip<erroresResponse>(url, "cancelar", Autorization, CodigoEmpresa, dataRequest);
                    if (firstTime)
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        firstTime = false;
                    }
                    if (datos != null)
                    {
                        mensajeErr = "Al cancelar factura: " + serie + folio.ToString() + " " + datos.mensaje;
                        bitacoraErrores(strErr, "cfdi");
                    }
                    else
                    {
                        cl.strTabla = "CancelarFacturas";
                        cl.strSerie = serie;
                        cl.intFolio = Convert.ToInt32(folio);

                        string result = cl.teskSincronizaErp();

                        if (result != "OK")
                        {
                            mensajeErr = "Al sincronizar cancelar factura: " + serie + folio + " " + result;
                            bitacoraErrores(strErr, "cfdi");
                        }
                    }

                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();
                }
            }
            catch (Exception ex)
            {
                strErr = "cancelaCfdi: " + ex.Message;
                bitacoraErrores(strErr, "cancelaCfdi");
            }
        }  //CancelaCfdi

        private void bitacoraErrores(string sError, string tabla)
        {
            string año = DateTime.Now.Year.ToString();
            string mes = DateTime.Now.Month.ToString();
            string dia = DateTime.Now.Day.ToString();

            if (Convert.ToInt32(mes) < 10)
                mes = "0" + mes;
            if (Convert.ToInt32(dia) < 10)
                dia = "0" + dia;

            dtResult.Rows.Add(sError);

            //string bitacora = ConfigurationManager.AppSettings["bitacora"].ToString() + "\\" + tabla + "_" + año + mes + dia + ".txt";

            //if (!File.Exists(bitacora))
            //{
            //    // Create a file to write to.
            //    using (StreamWriter sw = File.CreateText(bitacora))
            //    {
            //        sw.WriteLine(sError);
            //    }
            //}
            //else
            //{
            //    using (StreamWriter sw = File.AppendText(bitacora))
            //    {
            //        sw.WriteLine(sError);
            //    }
            //}

        }  //BitacoraErrores

        private void bbiClientes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Subir información", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Conectando con tesk...");

           

            IniTabla();
            string result=Login();

            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "CLIENTES";

            PostClienteDiexsa();

            gridControl1.DataSource = dtResult;

            
        }
        private void PostClienteDiexsa()
        {
            try
            {
                

                int clienteID;

                erpCL cl = new erpCL();
                MalocClient cliente = new MalocClient();

                postClienteModelRequest dataRequest = new postClienteModelRequest();
                Contacto contacto = new Contacto();
                Direcciondetalle direcciondetalle = new Direcciondetalle();
                Localidad localidad = new Localidad();
                Direccion direccion = new Direccion();

                DataTable dt = new DataTable();
                cl.intDato = 0;
                cl.strTabla = "Clientes";
                dt = cl.Datos();

                if (dt.Rows.Count == 0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("No hay información");
                    return;
                }

                progressBarControl1.Properties.Maximum = dt.Rows.Count;


                foreach (DataRow dr in dt.Rows)
                {
                    clienteID = Convert.ToInt32(dr["ClientesID"]);

                    contacto.celular = dr["Celular"].ToString();
                    contacto.correo = dr["Correo"].ToString();
                    contacto.correoCC = dr["Correo"].ToString();
                    contacto.telefono = dr["Telefono"].ToString();

                    direcciondetalle.calle = dr["Direccion"].ToString();
                    direcciondetalle.ciudad = dr["Ciudad"].ToString();
                    direcciondetalle.codigoPostal = dr["CP"].ToString();
                    direcciondetalle.colonia = "";
                    direcciondetalle.numeroExt = dr["numeroExt"].ToString();
                    direcciondetalle.numeroInt = "";

                    localidad.estadoCodigo = dr["estadoCodigo"].ToString();
                    localidad.municipioCodigo = dr["municipioCodigo"].ToString();
                    localidad.paisCodigo = dr["paisCodigo"].ToString();

                    direccion.contacto = contacto;
                    direccion.direccion = direcciondetalle;
                    direccion.localidad = localidad;

                    dataRequest.correoCC = dr["Correo"].ToString();
                    dataRequest.cuentaTipoCliente = dr["cuentaTipoCliente"].ToString();
                    dataRequest.curp = dr["curp"].ToString();
                    dataRequest.departamentoCodigo = dr["departamentoCodigo"].ToString();
                    dataRequest.diasDeCredito = Convert.ToInt32(dr["diasDeCredito"]);
                    dataRequest.direccion = direccion;
                    dataRequest.nombre = dr["Nombre"].ToString();
                    dataRequest.primerApellido = dr["PrimerApellido"].ToString();
                    dataRequest.rfc = dr["Rfc"].ToString();
                    dataRequest.segundoApellido = dr["segundoApellido"].ToString();
                    dataRequest.sucursalCodigo = dr["sucursalCodigo"].ToString();
                    dataRequest.taxId = dr["taxId"].ToString();
                    dataRequest.tipoPersona = dr["tipoPersona"].ToString();

                    string url = "https://app.tesk.mx/diexsa/v1/";

                    erroresResponse datos = cliente.PeticionesWSPostWithHeaders<erroresResponse>(url, "clientes", Autorization, CodigoEmpresa, dataRequest);

                    if (firstTime)
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        firstTime = false;
                    }

                    if (datos != null)
                    {
                        if (datos.mensaje != null)
                        {


                            if (datos.mensaje.Substring(0, 29) != "Ya existe una cuenta contable")
                            {
                                if (datos.mensaje != "El RFC que intenta guardar ya existe")
                                {
                                    string strErr = string.Empty;

                                    strErr = "CODIGO:002, Cliente:" + dataRequest.nombre + " ";

                                    int num = Convert.ToInt32(datos.validacionResponse.errores.Count);

                                    for (int i = 0; i <= num - 1; i++)
                                    {
                                        strErr = strErr + datos.validacionResponse.errores[i].campo + " : " + datos.validacionResponse.errores[i].error[0].ToString();
                                    }
                                    bitacoraErrores(strErr, "Clientes");
                                }
                                else
                                {
                                    string strErr = "CODIGO:001, Cliente:" + dataRequest.nombre;
                                    strErr = strErr + "RFC ya existe " + dataRequest.rfc;
                                    bitacoraErrores(strErr, "Clientes");
                                }
                            }
                            else
                            {
                                //Temporal
                                //Se actualiza campo tesk en visualsoft, para saber que ya está sincronizado
                                cl.strTabla = "Clientes";
                                cl.strSerie = "";
                                cl.intFolio = clienteID;

                                string result = cl.teskSincronizaErp();

                                if (result != "OK")
                                {
                                    string strErr = "Al sincronizar cliente: " + clienteID.ToString() + " " + result;
                                    bitacoraErrores(strErr, "Clientes");
                                }
                                //Eof:Temporal
                            }
                        }
                        else
                        {
                            //Temporal
                            //Se actualiza campo tesk en visualsoft, para saber que ya está sincronizado
                            cl.strTabla = "Clientes";
                            cl.strSerie = "";
                            cl.intFolio = clienteID;
                            cl.strCta = cliente.strCuentaContable.Substring(13, 17);

                            string result = cl.teskSincronizaErp();

                            if (result != "OK")
                            {
                                string strErr = "Al sincronizar cliente: " + clienteID.ToString() + " ";
                                bitacoraErrores(strErr, "Clientes");
                            }
                            //Eof:Temporal
                        }
                    }
                    else
                    {
                        //Se actualiza campo tesk en visualsoft, para saber que ya está sincronizado
                        cl.strTabla = "Clientes";
                        cl.strSerie = "";
                        cl.intFolio = clienteID;
                        cl.strCta = cliente.strCuentaContable.Replace("/clientes/", "");

                        string result = cl.teskSincronizaErp();

                        if (result != "OK")
                        {
                            string strErr = "Al sincronizar cliente: " + clienteID.ToString() + " ";
                            bitacoraErrores(strErr, "Clientes");
                        }
                    }

                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();

                } //for

            }
            catch (Exception ex)
            {

                bitacoraErrores(ex.Message, "Clientes");
            }
        } //Clientes

        private string Login()
        {
            try
            {
                CodigoEmpresa = "Edes006339"; //Productivo
                                            //tipoEmpresa = "Edes006142"; //Demo

                MalocClient cliente = new MalocClient();
                teskloginCL request = new teskloginCL();
                request.user = "jzambrano@live.com.mx";
                request.pass = "Diexsa2021";

                string url = "https://app.tesk.mx/diexsa/v1/";

                if (Autorization.Length == 0)
                {
                    teskloginresponse datos = cliente.PeticionesWSPostTesk<teskloginresponse>(url, "autenticar", request);
                    if (Autorization != null)
                        Autorization = datos.token;
                    else
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        return "No se pudo obtener el token";
                    }
                }

                if (Autorization == null)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    return "No se pudo obtener el token";
                }                    
                else
                    return "OK";
            }
            catch(Exception ex)
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                return ex.Message;
                
            }
            
            
        } //Login

        private void PostProveedoresDiexsa()
        {
            try
            {
                int proveedorID;

                erpCL cl = new erpCL();
                MalocClient cliente = new MalocClient();

                postProveedoresRequest dataRequest = new postProveedoresRequest();
                PContacto contacto = new PContacto();
                PDirecciondetalle direcciondetalle = new PDirecciondetalle();
                PLocalidad localidad = new PLocalidad();
                PDireccion direccion = new PDireccion();

                DataTable dt = new DataTable();
                cl.intDato = 0;
                cl.strTabla = "Proveedores";
                dt = cl.Datos();

                if (dt.Rows.Count == 0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("No hay información");
                    return;
                }

                progressBarControl1.Properties.Maximum = dt.Rows.Count;

                foreach (DataRow dr in dt.Rows)
                {
                    proveedorID = Convert.ToInt32(dr["clave"]);

                    dataRequest.clave = dr["Clave"].ToString();
                    dataRequest.cuentaTipoProveedor = dr["cuentaTipoProveedor"].ToString();
                    dataRequest.diasDeCredito = Convert.ToInt32(dr["diasDeCredito"]);
                    dataRequest.nombre = dr["nombre"].ToString();
                    dataRequest.rfc = dr["rfc"].ToString();
                    dataRequest.tiempoEntrega = Convert.ToInt32(dr["tiempoEntrega"]);
                    dataRequest.tipoCuenta = dr["tipoCuenta"].ToString();
                    dataRequest.tipoOperacionImpuestos = dr["tipoOperacionImpuestos"].ToString();
                    dataRequest.tipoPersona = dr["tipoPersona"].ToString();
                    dataRequest.tipoTerceroCodigo = dr["tipoTerceroCodigo"].ToString();

                    contacto.celular = "0";
                    contacto.correo = "x@x.com";
                    contacto.correoCC = "x@x.com";
                    contacto.telefono = "0";

                    direcciondetalle.calle = "x";
                    direcciondetalle.ciudad = "x";
                    direcciondetalle.codigoPostal = "64000";
                    direcciondetalle.colonia = "";
                    direcciondetalle.numeroExt = "0";
                    direcciondetalle.numeroInt = "";

                    localidad.estadoCodigo = "19";
                    localidad.municipioCodigo = "039";
                    localidad.paisCodigo = "MEX";

                    direccion.contacto = contacto;
                    direccion.direccion = direcciondetalle;
                    direccion.localidad = localidad;

                    dataRequest.direccion = direccion;

                    string url = "https://app.tesk.mx/diexsa/v1/";

                    erroresResponse datos = cliente.PeticionesWSPostWithHeaders<erroresResponse>(url, "proveedores", Autorization, CodigoEmpresa, dataRequest);
                    if (firstTime)
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        firstTime = false;
                    }
                    if (datos != null)
                    {
                        if (datos.mensaje.Substring(0, 29) != "Ya existe una cuenta contable")
                        {
                            if (datos.mensaje != "El RFC que intenta guardar ya existe")
                            {
                                string strErr = string.Empty;

                                strErr = "Proveedor:" + dataRequest.nombre;

                                int num = Convert.ToInt32(datos.validacionResponse.errores.Count);

                                for (int i = 0; i <= num - 1; i++)
                                {
                                    strErr = strErr + datos.validacionResponse.errores[i].campo + " : " + datos.validacionResponse.errores[i].error[0].ToString();
                                }
                                bitacoraErrores(strErr, "Proveedores");
                            }
                            else
                            {
                                //Temporal
                                //Se actualiza campo tesk en visualsoft, para saber que ya está sincronizado
                                cl.strTabla = "Proveedores";
                                cl.strSerie = "";
                                cl.intFolio = proveedorID;

                                string result = cl.teskSincronizaErp();

                                if (result != "OK")
                                {
                                    string strErr = "Al sincronizar proveedor: " + proveedorID.ToString() + " " + result;
                                    bitacoraErrores(strErr, "Proveedores");
                                }

                                //Eof: Temporal



                            }

                        }
                        else
                        {
                            //Se actualiza campo tesk en visualsoft, para saber que ya está sincronizado
                            cl.strTabla = "Proveedores";
                            cl.strSerie = "";
                            cl.intFolio = proveedorID;

                            string result = cl.teskSincronizaErp();

                            if (result != "OK")
                            {
                                string strErr = "Al sincronizar proveedor: " + proveedorID.ToString() + " " + result;
                                bitacoraErrores(strErr, "Proveedores");
                            }
                        }
                    }
                    else
                    {


                        //Se actualiza campo tesk en visualsoft, para saber que ya está sincronizado
                        cl.strTabla = "Proveedores";
                        cl.strSerie = "";
                        cl.intFolio = proveedorID;
                        cl.strCta = cliente.strCuentaContable.Substring(13, 17);

                        string result = cl.teskSincronizaErp();

                        if (result != "OK")
                        {
                            string strErr = "Al sincronizar proveedor: " + proveedorID.ToString() + " " + result;
                            bitacoraErrores(strErr, "Proveedores");
                        }
                    }

                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();

                }

            }
            catch (Exception ex)
            {
                string y = ex.Message;
            }
        } //Proveedortes

        private void bbiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiProveedores_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Subir información", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Conectando con tesk...");



            IniTabla();
            string result = Login();

            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "PROVEEDORES";

            PostProveedoresDiexsa();

            gridControl1.DataSource = dtResult;
        } //Proveedores

        private void PostServiciosDixsa()
        {
            try
            {
                string mensajeErr = string.Empty;
                int serviciosID;

                erpCL cl = new erpCL();
                MalocClient cliente = new MalocClient();

                postServiciosModelRequest dataRequest = new postServiciosModelRequest();


                DataTable dt = new DataTable();
                cl.intDato = 0;
                cl.strTabla = "Servicios";
                dt = cl.Datos();
                if (dt.Rows.Count == 0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("No hay información");
                    return;
                }
                progressBarControl1.Properties.Maximum = dt.Rows.Count;

                foreach (DataRow dr in dt.Rows)
                {
                    serviciosID = Convert.ToInt32(dr["ArticulosID"]);

                    dataRequest.codigo = dr["Articulo"].ToString();
                    dataRequest.codigoServSat = dr["ClaveSat"].ToString();
                    dataRequest.codigoUnidadSat = dr["UnidadSat"].ToString();
                    dataRequest.cuentaTipoIngreso = dr["cuentaTipoIngreso"].ToString();
                    dataRequest.descripcion = dr["descripcion"].ToString();
                    dataRequest.ivaIncluido = false;
                    dataRequest.precioUnitario = Convert.ToDecimal(dr["PrecioUnitario"]);
                    dataRequest.tipoIva = dr["tipoIva"].ToString();
                    dataRequest.tipoRetencionIsr = dr["tipoRetencionIsr"].ToString();
                    dataRequest.tipoRetencionIva = dr["tipoRetencionIva"].ToString();


                    string url = "https://app.tesk.mx/diexsa/v1/";

                    erroresResponse datos = cliente.PeticionesWSPostWithHeaders<erroresResponse>(url, "servicios", Autorization, CodigoEmpresa, dataRequest);
                    if (firstTime)
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        firstTime = false;
                    }
                    if (datos != null)
                    {
                        mensajeErr = datos.mensaje.Substring(0, 47);
                        if (mensajeErr != "Existe un producto/servicio con el mismo código")
                        {
                            mensajeErr = datos.mensaje.Substring(0, 37);
                            if (mensajeErr == "El código prod/serv SAT no es válido ")
                            {
                                mensajeErr = "CODIGO:020 " + mensajeErr + " SKU:" + dataRequest.codigo;
                                bitacoraErrores(mensajeErr, "Servicios");
                            }
                            else
                            {
                                if (mensajeErr== "El código unidad medida SAT no es vál")
                                {
                                    mensajeErr = "CODIGO:022 " + mensajeErr + " SKU:" + dataRequest.codigo;
                                    if (mensajeErr.Substring(0, 15) == "CodigoUnidadSat")
                                        mensajeErr = "CODIGO:021 " + mensajeErr;
                                    bitacoraErrores(mensajeErr, "Servicios");
                                }
                                else
                                {
                                    mensajeErr = datos.validacionResponse.errores[0].campo.ToString() + " SKU:" + dataRequest.codigo;
                                    if (mensajeErr.Substring(0, 15) == "CodigoUnidadSat")
                                        mensajeErr = "CODIGO:021 " + mensajeErr;
                                    bitacoraErrores(mensajeErr, "Servicios");
                                }
                                
                            }
                        }
                        else
                        {
                            if (datos.mensaje== "Algunos datos introducidos son incorrectos, verificar")
                            {
                                mensajeErr = datos.validacionResponse.errores[0].campo.ToString() + " SKU:" + dataRequest.codigo;
                                bitacoraErrores(mensajeErr, "Servicios");
                            }
                            else
                            {
                                //Temporla
                                //Se actualiza campo tesk en visualsoft, para saber que ya está sincronizado
                                cl.strTabla = "Servicios";
                                cl.strSerie = "";
                                cl.intFolio = serviciosID;

                                string result = cl.teskSincronizaErp();

                                if (result != "OK")
                                {
                                    string strErr = "Al sincronizar servicio: " + serviciosID.ToString() + " ";
                                    bitacoraErrores(strErr, "Servicios");
                                }
                                //Eof:Temporal
                            }

                        }

                    }
                    else
                    {
                        //Se actualiza campo tesk en visualsoft, para saber que ya está sincronizado
                        cl.strTabla = "Servicios";
                        cl.strSerie = "";
                        cl.intFolio = serviciosID;

                        string result = cl.teskSincronizaErp();

                        if (result != "OK")
                        {
                            string strErr = "Al sincronizar servicio: " + serviciosID.ToString() + " " + result;
                            bitacoraErrores(strErr, "Servicios");
                        }
                    }

                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();

                }

            }
            catch (Exception ex)
            {
                string y = ex.Message;
            }

        } // Servicios

        private void bbiServicios_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Subir información", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Conectando con tesk...");



            IniTabla();
            string result = Login();

            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "ARTICULOS";

            PostServiciosDixsa();

            gridControl1.DataSource = dtResult;
        } //Servicios

        private void cobranzaDeposito()
        {
            string strMensaje = string.Empty;
            string serie = string.Empty;
            string poliza = string.Empty;

            try
            {
                string sMonedaDep = string.Empty;
                decimal totalME = 0;
                decimal totalGeneral = 0;
                decimal totalCobros = 0;

                int folioDep = 0;
                int folioDepDet = 0;
                erpCL cl = new erpCL();
                MalocClient cliente = new MalocClient();

                DataSet ds = new DataSet();

                DataTable dt = new DataTable();
                DataTable dtDet = new DataTable();
                cl.intDato = 0;
                cl.strTabla = "cobranzaDeposito";
                cl.intEjer = Convert.ToInt32(txtEjer.Text);
                cl.intMes = Convert.ToInt32(txtMes.Text);
                ds = cl.CobranzaDeposito();

                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("No hay información");
                    return;
                }

                dtDet = ds.Tables[1];

                progressBarControl1.Properties.Maximum = dtDet.Rows.Count;


                cobranzaDepositoRequest dataRequest = new cobranzaDepositoRequest();
                facturasCobradasModel fcDato = new facturasCobradasModel();
                List<facturasCobradasModel> fcLista = new List<facturasCobradasModel>();

                foreach (DataRow dr in dt.Rows)
                {
                    dataRequest = new cobranzaDepositoRequest();
                    fcLista = new List<facturasCobradasModel>();

                    serie = fcDato.serie = dr["Serie"].ToString();
                    folioDep = Convert.ToInt32(dr["folioDep"]);

                    dataRequest.codigoFormaPagoSat = dr["codigoFormapagoSat"].ToString();
                    dataRequest.cuentaDepositadoEn = dr["cuentaDepositadoEn"].ToString();
                    dataRequest.fechaDeposito = Convert.ToDateTime(dr["fechaDeposito"]).ToShortDateString();
                    dataRequest.fechaPoliza = Convert.ToDateTime(dr["fechaDeposito"]).ToShortDateString();

                    sMonedaDep = dr["MonedaDeposito"].ToString();



                    if (Convert.ToDecimal(dr["importeMe"]) == 0)
                        dataRequest.importeMn = Convert.ToDecimal(dr["importeMn"]);


                    dataRequest.referencia = dr["Referencia"].ToString();

                    totalGeneral = 0;


                    if (Convert.ToDecimal(dr["importeMe"]) > 0)
                        totalGeneral += Math.Round(Convert.ToDecimal(dr["importeMe"]) * Convert.ToDecimal(dr["tipoCambioPagoMe"]), 2, MidpointRounding.AwayFromZero);
                    else
                        totalGeneral += Convert.ToDecimal(dr["importeMn"]);

                    if (Convert.ToDecimal(dr["tipoCambioPagoMe"]) > 1)
                        dataRequest.tipoCambioPagoMe = Convert.ToDecimal(dr["tipoCambioPagoMe"]);

                    totalCobros = 0;
                    totalME = 0;

                    foreach (DataRow drDet in dtDet.Rows)
                    {
                        folioDepDet = Convert.ToInt32(drDet["folioDep"]);
                        if (folioDep == folioDepDet)
                        {
                            //{
                            fcDato = new facturasCobradasModel();

                            fcDato.folio = Convert.ToInt32(drDet["folio"]);
                            if (Convert.ToDecimal(drDet["importeCobroMe"]) > 0)
                                fcDato.importeCobroMe = Convert.ToDecimal(drDet["importeCobroMe"]);

                            if (Convert.ToDecimal(drDet["importeCobroMn"]) > 0)
                                fcDato.importeCobroMn = Convert.ToDecimal(drDet["importeCobroMn"]);

                            fcDato.observaciones = drDet["Observaciones"].ToString();
                            fcDato.serie = drDet["Serie"].ToString();

                            totalME += Convert.ToDecimal(drDet["importeCobroMe"]);

                            //Voy a usar solo MN para pasar, pero hay que ver como saber si la factura fue en MXN o USD
                            //if (Convert.ToDecimal(drDet["importeCobroMe"]) > 0)
                            //    totalCobros += Convert.ToDecimal(drDet["importeCobroMe"]);
                            //else
                            if (Convert.ToDecimal(drDet["importeCobroMe"]) > 0)
                            {
                                if (sMonedaDep == "USD")
                                    totalCobros += Convert.ToDecimal(drDet["importeCobroMe"]);
                                else
                                    totalCobros += Math.Round(Convert.ToDecimal(drDet["importeCobroMe"]) * Convert.ToDecimal(dr["tipoCambioPagoMe"]), 2, MidpointRounding.AwayFromZero);
                            }
                            else
                                totalCobros += Convert.ToDecimal(drDet["importeCobroMn"]);

                            fcLista.Add(fcDato);
                        }
                    } // foreach Detalle


                    if (totalCobros != totalGeneral)
                    {
                        List<movimientosAjusteModel> malista = new List<movimientosAjusteModel>();
                        movimientosAjusteModel ma = new movimientosAjusteModel();

                        if (totalGeneral > totalCobros)
                        {
                            ma.abono = Math.Round((totalGeneral - totalCobros), 2);
                            ma.cuentaContable = "403 0001";
                        }

                        else
                        {
                            ma.cargo = Math.Round((totalCobros - totalGeneral), 2);
                            ma.cuentaContable = "403 0001";            //Debe ser otra
                        }

                        ma.concepto = "Diferencia";
                        ma.departamentoCodigo = string.Empty;
                        ma.sucursalCodigo = string.Empty;
                        malista.Add(ma);

                        dataRequest.movimientosAjuste = malista;
                    }

                    dataRequest.importeMe = totalME; // Convert.ToDecimal(dr["importeMe"]);

                    dataRequest.facturasCobradas = fcLista;

                    string url = "https://app.tesk.mx/diexsa/v1/cobranza/";
                    respuestaCobranza datos = cliente.PeticionesWSPostWithHeadersString<respuestaCobranza>(url, "deposito", Autorization, CodigoEmpresa, dataRequest);
                    if (firstTime)
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        firstTime = false;
                    }

                    if (datos != null)
                    {
                        if (datos.mensaje != null)
                        {
                            strMensaje = "cobranzaDeposito: " + folioDep.ToString() + " " + datos.mensaje.Replace("\n\r","");
                            strMensaje = strMensaje.Replace("\r\n", "");
                            bitacoraErrores(strMensaje, "cobranzaDeposito");
                        }
                        else
                        {
                            if (datos.polizaCobranza != null)
                            {
                                poliza = datos.polizaCobranza.Substring(0, datos.polizaCobranza.Length - 11);
                                poliza = poliza.Trim();

                                cl.strTabla = "cobranzaDeposito";
                                cl.strSerie = serie;
                                cl.intFolio = Convert.ToInt32(folioDep);
                                cl.strPoliza = poliza;

                                string result = cl.teskSincronizaErp();

                                if (result != "OK")
                                {
                                    strMensaje = "Al sincronizar cobranzaDeposito factura: " + serie + folioDep.ToString() + " " + result;
                                    bitacoraErrores(strMensaje, "cobranzaDeposito");
                                }
                            }


                        }

                    }
                    else
                    {

                        if (datos.polizaCobranza != null)
                        {
                            poliza = datos.polizaCobranza.Substring(0, datos.polizaCobranza.Length - 11);
                            poliza = poliza.Trim();

                            cl.strTabla = "cobranzaDeposito";
                            cl.strSerie = serie;
                            cl.intFolio = Convert.ToInt32(folioDep);
                            cl.strPoliza = poliza;

                            string result = cl.teskSincronizaErp();

                            if (result != "OK")
                            {
                                strMensaje = "Al sincronizar cobranzaDeposito factura: " + serie + folioDep.ToString() + " " + result;
                                bitacoraErrores(strMensaje, "cobranzaDeposito");
                            }
                        }
                        else
                        {
                            cl.strTabla = "cobranzaDeposito";
                            cl.strSerie = serie;
                            cl.intFolio = Convert.ToInt32(folioDep);
                            cl.strPoliza = poliza;

                            string result = cl.teskSincronizaErp();

                            if (result != "OK")
                            {
                                strMensaje = "Al sincronizar cobranzaDeposito folio: " + serie + folioDep.ToString() + " " + result;
                                bitacoraErrores(strMensaje, "cobranzaDeposito");
                            }
                        }


                    }

                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();

                } //for each Master

            }
            catch (Exception ex)
            {
                if (firstTime)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    firstTime = false;
                }

                strMensaje = "cobranzaDeposito: " + ex.Message;
                bitacoraErrores(strMensaje, "cobranzaDeposito");

            }
        } // CobranzaDepósitos

        private void bbiCobranza_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!validaEjercicio())
                return;

            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Subir información", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Conectando con tesk...");



            IniTabla();
            string result = Login();

            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "DEPOSITOS";

            cobranzaDeposito();

            gridControl1.DataSource = dtResult;
            MessageBox.Show("Proceso terminado");
        }

        private void bbiCfdiCancelados_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (!validaEjercicio())
                return;

            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Subir información", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Conectando con tesk...");



            IniTabla();
            string result = Login();

            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "CFDI CANCELADOS";

            cancelaCfdi();

            gridControl1.DataSource = dtResult;
            MessageBox.Show("Proceso terminado");
        }  //cfdiCancelados

        private void cancelaDepositoCobranza()
        {
            string strErr = string.Empty;
            string mensajeErr = string.Empty;
            string serie = string.Empty;
            int folio = 0;

            try
            {
                erpCL cl = new erpCL();

                MalocClient cliente = new MalocClient();

                DataTable dt = new DataTable();
                cl.intDato = 0;
                cl.strTabla = "CancelaCobranza";
                cl.intEjer = Convert.ToInt32(txtEjer.Text);
                cl.intMes = Convert.ToInt32(txtMes.Text);

                dt = cl.Datos();
                if (dt.Rows.Count == 0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("No hay información");
                    return;
                }
                progressBarControl1.Properties.Maximum = dt.Rows.Count;

                mensajeErr = "Inicio: " + DateTime.Now.ToShortTimeString();
                bitacoraErrores(strErr, "CancelaCobranza");

                //DirectoryInfo di = new DirectoryInfo(@xmlpath);
                foreach (DataRow dr in dt.Rows)
                {
                    serie = dr["Serie"].ToString();
                    folio = Convert.ToInt32(dr["Folio"]);

                    cancelaCobranzaRequest dataRequest = new cancelaCobranzaRequest();

                    if ((Convert.ToDateTime(dr["Fecha"]).Year== Convert.ToDateTime(dr["FechaCancelacion"]).Year) && (Convert.ToDateTime(dr["Fecha"]).Month == Convert.ToDateTime(dr["FechaCancelacion"]).Month))
                        dataRequest.cancelar = false;
                    else
                        dataRequest.cancelar = true;
                    dataRequest.fechaPoliza = Convert.ToDateTime(dr["Fecha"]).ToShortDateString();
                    dataRequest.fechaPolizaCancelacion = Convert.ToDateTime(dr["FechaCancelacion"]).ToShortDateString();
                    dataRequest.numeroPoliza = Convert.ToInt32(dr["Poliza"]);
                    dataRequest.tipoPoliza = dr["Tipo"].ToString();

                    string url = "https://app.tesk.mx/diexsa/v1/cobranza/";
                    respuestaCobranza datos = cliente.PeticionesWSPostWithHeadersString<respuestaCobranza>(url, "cancelardeposito", Autorization, CodigoEmpresa, dataRequest);
                    if (firstTime)
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        firstTime = false;
                    }
                    if (datos != null)
                    {
                        mensajeErr = "Al CancelaCobranza: " + serie + folio.ToString() + " " + datos.mensaje;
                        bitacoraErrores(mensajeErr, "CancelaCobranza");
                    }
                    else
                    {
                        cl.strTabla = "CancelarCobranza";
                        cl.strSerie = serie;
                        cl.intFolio = Convert.ToInt32(folio);

                        string result = cl.teskSincronizaErp();

                        if (result != "OK")
                        {
                            mensajeErr = "Al sincronizar cancelar cobranza: " + serie + folio + " " + result;
                            bitacoraErrores(mensajeErr, "cfdi");
                        }
                    }

                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();
                }
            }
            catch (Exception ex)
            {
                strErr = "CancelaCobranza: " + ex.Message;
                bitacoraErrores(strErr, "CancelaCobranza");
            }
        }

        private void bbiCancelaCobranza_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!validaEjercicio())
                return;

            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Subir información", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Conectando con tesk...");



            IniTabla();
            string result = Login();

            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "CANCELA COBRANZA";

            cancelaDepositoCobranza();

            gridControl1.DataSource = dtResult;
            MessageBox.Show("Proceso terminado");
        }  //Cancela cobranza

        private void aplicarNotaCredito()
        {
            string strMensaje = string.Empty;
            string serie = string.Empty;
            int folio = 0;
            string poliza = string.Empty;

            try
            {
                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                string xmlpath = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString();
                string rutaxml = string.Empty;

                int año = 0;
                int mes = 0;

                globalCL clg = new globalCL();

                int folioDet = 0;
                string strUuid = string.Empty;
                string strUso = "GastosGeneral";
                erpCL cl = new erpCL();
                MalocClient cliente = new MalocClient();

                DataSet ds = new DataSet();

                DataTable dt = new DataTable();
                DataTable dtDet = new DataTable();
                cl.intDato = 0;
                cl.strTabla = "notasdecredito";
                cl.intEjer = Convert.ToInt32(txtEjer.Text);
                cl.intMes = Convert.ToInt32(txtMes.Text);



                ds = cl.CobranzaDeposito();

                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("No hay información");
                    return;
                }

                dtDet = ds.Tables[1];
                progressBarControl1.Properties.Maximum = dt.Rows.Count;


                aplicarNotaCreditoClienteRequest dataRequest = new aplicarNotaCreditoClienteRequest();
                facturasAplicadaselementos fcDato = new facturasAplicadaselementos();
                List<facturasAplicadaselementos> fcLista = new List<facturasAplicadaselementos>();

                foreach (DataRow dr in dt.Rows)
                {
                    dataRequest = new aplicarNotaCreditoClienteRequest();
                    fcLista = new List<facturasAplicadaselementos>();

                    serie = dr["Serie"].ToString();
                    folio = Convert.ToInt32(dr["folio"]);

                    año = Convert.ToDateTime(dr["fechaCfdi"]).Year;
                    mes = Convert.ToDateTime(dr["fechaCfdi"]).Month;
                    rutaxml = xmlpath + año + "\\" + clg.NombreDeMes(mes) + "\\" + serie + folio.ToString() + ".xml";

                    strUuid = vs.ExtraeValor(rutaxml, "tfd:TimbreFiscalDigital", "UUID");

                    if (strUuid.Length > 0)
                    {
                        dataRequest.cfdiXml = File.ReadAllText(rutaxml);
                        dataRequest.cuentaCliente = dr["cuentaCliente"].ToString();
                        dataRequest.fechaCfdi = Convert.ToDateTime(dr["fechaCfdi"]).ToShortDateString();
                        dataRequest.fechaPoliza = Convert.ToDateTime(dr["fechaCfdi"]).ToShortDateString();
                        dataRequest.folio = folio;
                        dataRequest.folioFiscal = strUuid;
                        dataRequest.importe = Convert.ToDecimal(dr["importe"]);
                        dataRequest.lugarExpedicion = dr["lugarExpedicion"].ToString();
                        dataRequest.serie = dr["serie"].ToString();
                        dataRequest.usoDeCfdi = strUso;

                        foreach (DataRow drDet in dtDet.Rows)
                        {
                            folioDet = Convert.ToInt32(drDet["folioNcr"]);
                            if (folio == folioDet)
                            {
                                //{
                                fcDato = new facturasAplicadaselementos();

                                fcDato.cuentaContableIngresoTasa16 = drDet["cuentaContableIngresoTasa16"].ToString();
                                fcDato.serie = drDet["serie"].ToString();
                                fcDato.folio = Convert.ToInt32(drDet["folio"]);
                                fcDato.iepsCobrado = 0;
                                fcDato.importeAplicado = Convert.ToDecimal(drDet["importeAplicado"]);
                                fcDato.ingresoTasa0 = 0;
                                fcDato.ingresoTasa16 = Convert.ToDecimal(drDet["ingresoTasa16"]);
                                fcDato.ingresoTasa8 = 0;
                                fcDato.ingresoTasaExcento = 0;
                                fcDato.ivaCobrado = Convert.ToDecimal(drDet["ivaCobrado"]);

                                fcLista.Add(fcDato);
                            }
                        } // foreach Detalle

                        dataRequest.facturasAplicadas = fcLista;

                        string url = "https://app.tesk.mx/diexsa/v1/factura/";
                        erroresResponse datos = cliente.PeticionesWSPostWithHeaders<erroresResponse>(url, "aplicarNotaCredito", Autorization, CodigoEmpresa, dataRequest);
                        if (firstTime)
                        {
                            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                            firstTime = false;
                        }
                        if (datos != null)
                        {
                            if (datos.mensaje != null)
                            {
                                if (datos.mensaje.Substring(0, 11) != "Se encontró")
                                {
                                    if (datos.mensaje== "No se pudo generar la póliza")
                                    {
                                        strMensaje = " Folio: " + serie + folio.ToString() + " " + datos.mensaje.Replace("\n\r", " ");
                                        bitacoraErrores(strMensaje, "aplicarNotasCredito");
                                    }
                                    else
                                    {                                    
                                        if (datos.mensaje.Substring(0,42)== "Algunos datos introducidos son incorrectos")
                                        {
                                            strMensaje = "CODIGI:070 Folio: " + serie + folio.ToString() + " " + datos.validacionResponse.errores[0].campo.ToString();
                                            bitacoraErrores(strMensaje, "aplicarNotasCredito");
                                        }
                                        else
                                        {
                                            strMensaje = " Folio: " + serie + folio.ToString() + " " + datos.mensaje.Replace("\n\r", " ");
                                            bitacoraErrores(strMensaje, "aplicarNotasCredito");
                                        }
                                    }
                                }
                                else
                                {
                                        if (datos.poliza == null)
                                            cl.strPoliza = "-1";
                                        else
                                    {
                                        poliza = datos.poliza.Substring(0, datos.poliza.Length - 11);
                                        poliza = poliza.Trim();

                                        cl.strPoliza = poliza;
                                    }
                                    
                                        cl.strTabla = "notasdecredito";
                                        cl.strSerie = serie;
                                        cl.intFolio = Convert.ToInt32(folio);

                                        string result = cl.teskSincronizaErp();

                                        if (result != "OK")
                                        {
                                            strMensaje = "Al sincronizar aplicarNotasCredito folio: " + serie + folio.ToString() + " " + result;
                                            bitacoraErrores(strMensaje, "aplicarNotasCredito");
                                        }
                                    

                                        
                                }
                            }
                            else
                            {
                                poliza = datos.poliza.Substring(0, datos.poliza.Length - 11);
                                poliza = poliza.Trim();

                                cl.strPoliza = poliza;
                                cl.strTabla = "notasdecredito";
                                cl.strSerie = serie;
                                cl.intFolio = Convert.ToInt32(folio);

                                string result = cl.teskSincronizaErp();

                                if (result != "OK")
                                {
                                    strMensaje = "Al sincronizar aplicarNotasCredito folio: " + serie + folio.ToString() + " " + result;
                                    bitacoraErrores(strMensaje, "aplicarNotasCredito");
                                }
                            }
                        }
                        else
                        {
                            poliza = datos.poliza.Substring(0, datos.poliza.Length - 11);
                            poliza = poliza.Trim();

                            cl.strPoliza = poliza;
                            cl.strTabla = "notasdecredito";
                            cl.strSerie = serie;
                            cl.intFolio = Convert.ToInt32(folio);

                            string result = cl.teskSincronizaErp();

                            if (result != "OK")
                            {
                                strMensaje = "Al sincronizar aplicarNotasCredito folio: " + serie + folio.ToString() + " " + result;
                                bitacoraErrores(strMensaje, "aplicarNotasCredito");
                            }
                        }
                    }
                    else
                    {
                        strMensaje = "AplicarNotasCredito folio: " + serie + folio.ToString() + " No encontre XML";
                        bitacoraErrores(strMensaje, "aplicarNotasCredito");
                    }

                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();

                } //for each Master
            }
            catch (Exception ex)
            {
                strMensaje = "aplicarNotasCredito: " + ex.Message;
                bitacoraErrores(strMensaje, "aplicarNotasCredito");
                //Console.WriteLine(ex.Message);

            }
        }

        private void bbiNCR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!validaEjercicio())
                return;

            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Subir información", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Conectando con tesk...");



            IniTabla();
            string result = Login();

            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "NOTAS DE CREDITO";

            aplicarNotaCredito();

            gridControl1.DataSource = dtResult;
            MessageBox.Show("Proceso terminado");
        }  //Ncr

        private void cancelaNcr()
        {
            string mensajeErr = string.Empty;
            string serie = string.Empty;
            int folio = 0;
            try
            {
                erpCL cl = new erpCL();
                DataTable dt = new DataTable();
                cl.intDato = 3;
                cl.strTabla = "CancelaNotasdecredito";
                cl.intEjer = Convert.ToInt32(txtEjer.Text);
                cl.intMes = Convert.ToInt32(txtMes.Text);

                dt = cl.Datos();
                if (dt.Rows.Count == 0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("No hay información");
                    return;
                }
                progressBarControl1.Properties.Maximum = dt.Rows.Count;

                mensajeErr = "Inicio: " + DateTime.Now.ToShortTimeString();
                bitacoraErrores(mensajeErr, "CancelarNCR");

                MalocClient cliente = new MalocClient();

                cancelaCfdiRequest dataRequest = new cancelaCfdiRequest();

                foreach (DataRow dr in dt.Rows)
                {

                    serie = dr["Serie"].ToString();
                    folio = Convert.ToInt32(dr["folio"]);

                    dataRequest.fechaCancelacionSat = Convert.ToDateTime(dr["Fechacancelacion"]).ToShortDateString();
                    dataRequest.fechaPolizaCancelacion = Convert.ToDateTime(dr["Fechacancelacion"]).ToShortDateString();
                    dataRequest.folio = folio;
                    dataRequest.selloCancelacionSat = "";
                    dataRequest.serie = serie;


                    string url = "https://app.tesk.mx/diexsa/v1/facturas/";
                    erroresResponse datos = cliente.PeticionesWSPostWithHeadersReturnZip<erroresResponse>(url, "cancelarnc", Autorization, CodigoEmpresa, dataRequest);
                    if (firstTime)
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        firstTime = false;
                    }
                    if (datos != null)
                    {
                        if (datos.mensaje != null)
                        {
                            if (datos.mensaje != "La factura ya fue previamente cancelada")
                            {
                                mensajeErr = "Al cancelar NCR: " + serie + folio.ToString() + " " + datos.mensaje;
                                bitacoraErrores(mensajeErr, "CancelarNCR");
                            }
                            else
                            {
                                cl.strTabla = "CancelarNCR";
                                cl.strSerie = serie;
                                cl.intFolio = Convert.ToInt32(folio);

                                string result = cl.teskSincronizaErp();

                                if (result != "OK")
                                {
                                    mensajeErr = "Al sincronizar cancelar NCR: " + serie + folio + " " + result;
                                    bitacoraErrores(mensajeErr, "cfdi");
                                }
                            }

                        }
                        else
                        {
                            cl.strTabla = "CancelarNCR";
                            cl.strSerie = serie;
                            cl.intFolio = Convert.ToInt32(folio);

                            string result = cl.teskSincronizaErp();

                            if (result != "OK")
                            {
                                mensajeErr = "Al sincronizar cancelar NCR: " + serie + folio + " " + result;
                                bitacoraErrores(mensajeErr, "cfdi");
                            }
                        }

                    }
                    else
                    {
                        cl.strTabla = "CancelarNCR";
                        cl.strSerie = serie;
                        cl.intFolio = Convert.ToInt32(folio);

                        string result = cl.teskSincronizaErp();

                        if (result != "OK")
                        {
                            mensajeErr = "Al sincronizar cancelar NCR: " + serie + folio + " " + result;
                            bitacoraErrores(mensajeErr, "cfdi");
                        }
                    }

                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();
                }
            }
            catch (Exception ex)
            {
                mensajeErr = "CancelarNCR: " + ex.Message;
                bitacoraErrores(mensajeErr, "CancelarNCR");
            }
        }

        private void bbiNcrCan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!validaEjercicio())
                return;

            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Subir información", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Conectando con tesk...");



            IniTabla();
            string result = Login();

            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "NOTAS DE CREDITO CANCELADAS";

            cancelaNcr();

            gridControl1.DataSource = dtResult;
            MessageBox.Show("Proceso terminado");
        }  //NCR_Can

        private void comprasBienes()
        {
            string strErr = string.Empty;
            string strMensaje = string.Empty;
            string poliza = string.Empty;
            try
            {
                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                string xmlpath = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString();
                string rutaxml = string.Empty;

                string NomProv = string.Empty;
                decimal totalIva = 0;
                decimal subTotal16 = 0;
                decimal subTotal = 0;
                decimal ivaIncrementales = 0;
                decimal incremental = 0;
                string strCtaIncr = string.Empty;
                string strCtaIvaGastosImp = string.Empty;
                int Pais = 0;
                int año = 0;
                int mes = 0;
                int folio = 0;
                int folioDet = 0;
                string strUuid = string.Empty;
                string Rfc = string.Empty;
                erpCL cl = new erpCL();
                MalocClient cliente = new MalocClient();
                decimal ST = 0;

                DataSet ds = new DataSet();

                DataTable dt = new DataTable();
                DataTable dtDet = new DataTable();
                cl.intDato = 0;
                cl.strTabla = "compras";
                cl.intEjer = Convert.ToInt32(txtEjer.Text);
                cl.intMes = Convert.ToInt32(txtMes.Text);
                ds = cl.CobranzaDeposito();

                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("No hay información");
                    return;
                }

                dtDet = ds.Tables[1];

                progressBarControl1.Properties.Maximum = dt.Rows.Count;



                comprasRequest dataRequest = new comprasRequest();
                conceptosListas Dato = new conceptosListas();
                List<conceptosListas> fcLista = new List<conceptosListas>();

                foreach (DataRow dr in dt.Rows)
                {
                    subTotal = 0;
                    subTotal16 = 0;

                    dataRequest = new comprasRequest();
                    fcLista = new List<conceptosListas>();

                    NomProv = dr["nombreProveedor"].ToString();

                    folio = Convert.ToInt32(dr["folio"]);

                    if (chkFechaActual.Checked)
                    {
                        año = DateTime.Now.Year;
                        mes = DateTime.Now.Month;
                    }
                    else
                    {
                        año = Convert.ToDateTime(dr["fechaCfdi"]).Year;
                        mes = Convert.ToDateTime(dr["fechaCfdi"]).Month;
                    }
                        
                    //rutaxml = xmlpath + año + "\\" + Nombredemes(mes) + "\\NCR" + folio.ToString() + ".xml";

                    //strUuid = vs.ExtraeValor(rutaxml, "tfd:TimbreFiscalDigital", "UUID");
                    //strUso = vs.ExtraeValor(rutaxml, "cfdi:Receptor", "UsoCFDI");

                    //dataRequest.cfdiXml = File.ReadAllText(rutaxml);
                    dataRequest.costoIva = false;
                    incremental = Convert.ToDecimal(dr["Incremental"]);
                    ivaIncrementales = Convert.ToDecimal(dr["IvaPedimento"]);
                    dataRequest.cuentaTipoProveedor = dr["cuentaTipoProveedor"].ToString();

                    if (chkFechaActual.Checked)
                    {
                        dataRequest.fechaCfdi = DateTime.Now.ToShortDateString();
                        dataRequest.fechaPoliza = DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        dataRequest.fechaCfdi = Convert.ToDateTime(dr["fechaCfdi"]).ToShortDateString();
                        dataRequest.fechaPoliza = Convert.ToDateTime(dr["fechaCfdi"]).ToShortDateString();
                    }
                        
                    //dataRequest.folio = folio;
                    dataRequest.formaPago = "Ninguno";
                    dataRequest.metodoPago = "PPD";
                    dataRequest.moneda = dr["moneda"].ToString();
                    dataRequest.serie = dr["serie"].ToString() + dr["factura"].ToString();
                    dataRequest.tipoCambio = Convert.ToDecimal(dr["tipoCambio"]);
                    dataRequest.totalIva = Convert.ToDecimal(dr["totalIva"]);
                    dataRequest.usoDeCfdi = "Ninguno";
                    Pais = Convert.ToInt32(dr["Pais"]);
                    Rfc = dr["Rfc"].ToString().ToUpper();



                    strCtaIncr = dr["cuentaGastosImportacion"].ToString();
                    strCtaIvaGastosImp = dr["CuentaIvaGastosImportacion"].ToString();

                    dataRequest.cuentaAcreedor = "";
                    dataRequest.pagoTercero = false;
                    dataRequest.valorActosExento = 0;
                    dataRequest.valorActosExentoImportacion = 0;
                    dataRequest.valorActosNoAfecto = 0;
                    dataRequest.valorActosTasa4 = 0;
                    dataRequest.valorActosTasa8 = 0;
                    dataRequest.retencionIsr = 0;
                    dataRequest.retencionIva = 0;



                    if (dataRequest.totalIva > 0)
                    {
                        dataRequest.valorActosTasa0Importacion = 0;
                        dataRequest.valorActosTasa16Importacion = 0;
                        dataRequest.valorActosTasa0 = 0;
                    }
                    else
                    {
                        dataRequest.valorActosTasa0Importacion = 0;
                        dataRequest.valorActosTasa0 = Convert.ToDecimal(dr["subTotal"]) + incremental;
                        dataRequest.valorActosTasa16 = 0;
                        dataRequest.valorActosTasa16Importacion = 0;
                        dataRequest.totalIva = ivaIncrementales;
                    }

                    subTotal = 0;
                    totalIva = 0;

                    foreach (DataRow drDet in dtDet.Rows)
                    {
                        folioDet = Convert.ToInt32(drDet["folio"]);
                        if (folio == folioDet)
                        {
                            Dato = new conceptosListas();

                            Dato.cantidad = Convert.ToDecimal(drDet["cantidad"]);
                            if (Rfc != "XEXX010101000")  //if (Pais == 1)
                                Dato.cuentaEgreso = drDet["cuentaEgresoNac"].ToString();
                            else
                                Dato.cuentaEgreso = drDet["cuentaEgresoExt"].ToString();

                            Dato.descripcion = drDet["descripcion"].ToString();
                            Dato.descuento = Convert.ToDecimal(drDet["descuento"]);
                            Dato.precioUnitario = Convert.ToDecimal(drDet["precioUnitario"]);
                            Dato.tipoIva = drDet["tipoIva"].ToString();

                            subTotal += Math.Round((Dato.cantidad * Dato.precioUnitario)- Dato.descuento, 2, MidpointRounding.AwayFromZero);
                            ST = Math.Round((Dato.cantidad * Dato.precioUnitario) - Dato.descuento, 2, MidpointRounding.AwayFromZero);

                            if (Dato.tipoIva == "Iva16")
                            {
                                subTotal16 += Math.Round(Dato.cantidad * Dato.precioUnitario,2, MidpointRounding.AwayFromZero);
                                subTotal16 -= Dato.descuento;
                                //totalIva += (Math.Round(Math.Round(Dato.cantidad * Dato.precioUnitario)-Dato.descuento,2) * Convert.ToDecimal(0.16), 2, MidpointRounding.AwayFromZero);
                                totalIva += Math.Round(ST * Convert.ToDecimal(0.16), 2, MidpointRounding.AwayFromZero);
                            }



                            fcLista.Add(Dato);
                        }
                    } // foreach Detalle


                    dataRequest.subTotal = subTotal + totalIva;
                    dataRequest.valorActosTasa16 = Math.Round(subTotal16,2);
                    dataRequest.totalIva = totalIva;
                    dataRequest.total = dataRequest.subTotal;
                    dataRequest.conceptos = fcLista;

                    string url = "https://app.tesk.mx/diexsa/v1/gasto/";
                    respuestaCompras datos = cliente.PeticionesWSPostWithHeaders<respuestaCompras>(url, "contabilizar", Autorization, CodigoEmpresa, dataRequest);
                    if (firstTime)
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        firstTime = false;
                    }
                    if (datos.mensaje != null)
                    {
                        strMensaje = "compras: " + folio.ToString() + " : " + " : " + NomProv + " " + datos.mensaje;
                        bitacoraErrores(strMensaje, "compras");
                    }
                    else
                    {
                        poliza = datos.poliza.Substring(0, datos.poliza.Length - 11);
                        poliza = poliza.Trim();

                        cl.strTabla = "compras";
                        cl.strSerie = dataRequest.serie;
                        cl.intFolio = Convert.ToInt32(folio);
                        cl.strPoliza = poliza;

                        string result = cl.teskSincronizaErp();

                        if (result != "OK")
                        {
                            strMensaje = "Al sincronizar Compras factura: " + dataRequest.serie + folio.ToString() + " " + result;
                            bitacoraErrores(strMensaje, "Compras");
                        }
                    }

                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();
                } //for each Master
            }
            catch (Exception ex)
            {
                strErr = "Compras: " + ex.Message;
                bitacoraErrores(strErr, "Compras");
                Console.WriteLine(ex.Message);

            }
        }  //Compras

        private void bbiCompras_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (!validaEjercicio())
                return;

            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Subir información", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Conectando con tesk...");



            IniTabla();
            string result = Login();

            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "COMPRAS";

            comprasBienes();

            gridControl1.DataSource = dtResult;
            MessageBox.Show("Proceso terminado");
        } //bbiCompras

        private void contrarecibosPorFactura()
        {
            string strErr = string.Empty;
            string strMensaje = string.Empty;
            string poliza = string.Empty;
            int folio = 0;


            try
            {
                string nombre = string.Empty;
                string result = string.Empty;
                string xmlpath = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString();
                string rutaxml = string.Empty;
                string des = string.Empty;
                decimal retIva = 0;
                decimal subTotal16 = 0;
                decimal subTotal = 0;
                decimal iva = 0;
                string strCtaIncr = string.Empty;
                string strCtaIvaGastosImp = string.Empty;
                int Pais = 0;
                int año = 0;
                int mes = 0;
                int Seq = 0;
                DateTime fechaActual=DateTime.Now;

                int folioDet = 0;
                string strUuid = string.Empty;
                string strUso = "GastosGeneral";
                erpCL cl = new erpCL();
                MalocClient cliente = new MalocClient();

                DataSet ds = new DataSet();

                DataTable dt = new DataTable();
                DataTable dtDet = new DataTable();
                cl.intDato = 0;
                cl.strTabla = "contrarecibosporfactura";
                cl.intEjer = Convert.ToInt32(txtEjer.Text);
                cl.intMes = Convert.ToInt32(txtMes.Text);
                ds = cl.CobranzaDeposito();

                dt = ds.Tables[0];
               

                if (dt.Rows.Count ==0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("No hay información");
                }

                progressBarControl1.Properties.Maximum = dt.Rows.Count;


                comprasRequest dataRequest = new comprasRequest();
                conceptosListas Dato = new conceptosListas();
                List<conceptosListas> fcLista = new List<conceptosListas>();

                float number;

                foreach (DataRow dr in dt.Rows)
                {
                    subTotal = 0;
                    subTotal16 = 0;

                    dataRequest = new comprasRequest();
                    fcLista = new List<conceptosListas>();

                    folio = Convert.ToInt32(dr["folio"]);
                    Seq = Convert.ToInt32(dr["Seq"]);

                    
                    //rutaxml = xmlpath + año + "\\" + Nombredemes(mes) + "\\NCR" + folio.ToString() + ".xml";

                    //strUuid = vs.ExtraeValor(rutaxml, "tfd:TimbreFiscalDigital", "UUID");
                    //strUso = vs.ExtraeValor(rutaxml, "cfdi:Receptor", "UsoCFDI");

                    //dataRequest.cfdiXml = File.ReadAllText(rutaxml);
                    dataRequest.costoIva = false;
                    dataRequest.cuentaTipoProveedor = dr["cuentaTipoProveedor"].ToString();

                    if (chkFechaActual.Checked)
                    {
                        año = fechaActual.Year;
                        mes = fechaActual.Month;
                        dataRequest.fechaCfdi = fechaActual.ToShortDateString();
                        dataRequest.fechaPoliza = fechaActual.ToShortDateString();
                    }
                    else
                    {
                        año = Convert.ToDateTime(dr["fechaCfdi"]).Year;
                        mes = Convert.ToDateTime(dr["fechaCfdi"]).Month;
                        dataRequest.fechaCfdi = Convert.ToDateTime(dr["fechaCfdi"]).ToShortDateString();
                        dataRequest.fechaPoliza = Convert.ToDateTime(dr["fechaCfdi"]).ToShortDateString();
                    }
                        


                    nombre = dr["Nombre"].ToString();
                    string strF = dr["factura"].ToString();

                    //bool success = float.TryParse(dr["factura"].ToString(), out number);

                    //if (success)
                    //    if (Convert.ToDecimal(dr["factura"]) > 2147483647)  //Por que 2147483647 es el máximo en enteros y hay xml con folio mayor
                    //        dataRequest.folio = folio;         
                    //    else
                    //        dataRequest.folio = Convert.ToInt32(dr["factura"].ToString()); //Convert.ToInt32(strCadenaNum);
                    //else
                    //    dataRequest.folio = folio;

                    dataRequest.serie = dr["SerieFactura"].ToString() + dr["factura"].ToString();

                    dataRequest.formaPago = "Ninguno";
                    dataRequest.metodoPago = "PPD";
                    dataRequest.moneda = dr["moneda"].ToString();

                    dataRequest.tipoCambio = Convert.ToDecimal(dr["tipoCambio"]);

                    dataRequest.usoDeCfdi = "Ninguno";
                    Pais = Convert.ToInt32(dr["Pais"]);
                    des = dr["Descripcion"].ToString();
                    strCtaIvaGastosImp = dr["cuentaEgreso"].ToString();

                    dataRequest.cuentaAcreedor = "";
                    dataRequest.pagoTercero = false;
                    dataRequest.valorActosExento = 0;
                    dataRequest.valorActosExentoImportacion = 0;
                    dataRequest.valorActosNoAfecto = 0;
                    dataRequest.valorActosTasa4 = 0;
                    dataRequest.valorActosTasa8 = 0;
                    dataRequest.retencionIsr = 0;
                    dataRequest.retencionIva = 0;


                    iva = 0;
                    retIva = 0;

                    Dato = new conceptosListas();

                    Dato.cantidad = Convert.ToDecimal(dr["cantidad"]);
                    Dato.cuentaEgreso = strCtaIvaGastosImp;

                    Dato.descripcion = des;
                    Dato.descuento = 0;
                    Dato.precioUnitario = Convert.ToDecimal(dr["precioUnitario"]);
                    Dato.tipoIva = dr["tipoIva"].ToString();

                    retIva += Convert.ToDecimal(dr["RetIva"]);

                    iva += Convert.ToDecimal(dr["Iva"]);

                    if (Dato.tipoIva == "Iva16")
                        subTotal16 += (Dato.cantidad * Dato.precioUnitario);

                    subTotal += (Dato.cantidad * Dato.precioUnitario);

                    fcLista.Add(Dato);


                    dataRequest.retencionIva = retIva;
                    dataRequest.totalIva = iva;

                    if (dataRequest.totalIva > 0)
                    {
                        dataRequest.valorActosTasa0Importacion = 0;
                        dataRequest.valorActosTasa16Importacion = 0;
                        dataRequest.valorActosTasa0 = 0;
                    }
                    else
                    {
                        dataRequest.valorActosTasa0Importacion = 0;
                        dataRequest.valorActosTasa0 = subTotal;
                        dataRequest.valorActosTasa16 = 0;
                        dataRequest.valorActosTasa16Importacion = 0;
                        dataRequest.totalIva = 0;
                    }

                    dataRequest.subTotal = subTotal + iva;
                    dataRequest.valorActosTasa16 = subTotal16;

                    dataRequest.total = dataRequest.subTotal - retIva; // subTotal + Convert.ToDecimal(dr["totalIva"]) + incremental;

                    dataRequest.conceptos = fcLista;

                    string url = "https://app.tesk.mx/diexsa/v1/gasto/";
                    erroresResponseContrarecibos datos = cliente.PeticionesWSPostWithHeaders<erroresResponseContrarecibos>(url, "contabilizar", Autorization, CodigoEmpresa, dataRequest);
                    if (firstTime)
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        firstTime = false;
                    }

                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();

                    if (datos.mensaje != null)
                    {
                        try
                        {
                            if (datos.validacionResponse != null)
                            {
                                strMensaje = datos.validacionResponse.errores[0].campo;
                                if (strMensaje == "Conceptos[0].CuentaEgreso")
                                    strMensaje = "CODIGO:090 Folio: " + folio.ToString() + " : " + nombre + "  " + datos.validacionResponse.errores[0].campo + "   " + datos.validacionResponse.errores[0].error[0].ToString();
                                else
                                    strMensaje = "contrarecibos: " + folio.ToString() + " : " + nombre + "  " + datos.validacionResponse.errores[0].campo + "   " + datos.validacionResponse.errores[0].error[0].ToString();

                            }
                            else
                            {
                                strMensaje = datos.mensaje;
                            }
                        }
                        catch
                        {
                            strMensaje = "contrarecibos: " + folio.ToString() + " : " + nombre +  " " + datos.mensaje;
                        }
                        bitacoraErrores(strMensaje, "contrarecibos");
                    }
                    else
                    {
                        if (datos.poliza != null)
                        {
                            poliza = datos.poliza.Substring(0, datos.poliza.Length - 11);
                            poliza = poliza.Trim();

                            cl.strTabla = "Contrarecibosdetalle";

                            cl.strSerie = "";
                            cl.intFolio = Convert.ToInt32(folio);
                            cl.strPoliza = poliza;
                            cl.intSeq = Seq;
                            result = cl.teskSincronizaErp();

                            if (result != "OK")
                            {
                                strMensaje = "Al sincronizar contrarecibos folio: " + dataRequest.serie + folio.ToString() + " " + result;
                                bitacoraErrores(strMensaje, "contrarecibos");
                            }
                        }
                        else
                        {
                            strMensaje = "Poliza null: " + dataRequest.serie + folio.ToString() + " : " + nombre + "  " + result;
                            bitacoraErrores(strMensaje, "contrarecibos");
                        }

                        

                    }
                } //for each Master
            }
            catch (Exception ex)
            {
                strErr = "contrarecibos: " + ex.Message;
                bitacoraErrores(strErr, "contrarecibos");
                Console.WriteLine(ex.Message);

            }
        }  //Contrarecibos por factura

        private void bbiCargosCxP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (!validaEjercicio())
                return;

            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Subir información", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Conectando con tesk...");



            IniTabla();
            string result = Login();

            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "CARGOS CXP";

            contrarecibosPorFactura();

            gridControl1.DataSource = dtResult;
            MessageBox.Show("Proceso terminado");
        } //bbiCargos

        private void pagarFacturas() //Pagos a cxp
        {
            string strMensaje = string.Empty;
            string serie = string.Empty;
            string poliza = string.Empty;
            int folioDep = 0;
            bool success;
            int number;
            string NombreProv = string.Empty;
            decimal TC = 0;
            string strSeq = string.Empty;
            string strFechaCxP = string.Empty;

            try
            {
                decimal totalPago = 0;
                decimal totalCobro = 0;


                bool MonedaExtranjera = false;

                int folioDepDet = 0;
                erpCL cl = new erpCL();
                MalocClient cliente = new MalocClient();

                DataSet ds = new DataSet();

                DataTable dt = new DataTable();
                DataTable dtDet = new DataTable();
                cl.intDato = 0;
                cl.strTabla = "pagarFacturas";
                cl.intEjer = Convert.ToInt32(txtEjer.Text);
                cl.intMes = Convert.ToInt32(txtMes.Text);
                ds = cl.CobranzaDeposito();

                dt = ds.Tables[0];
                dtDet = ds.Tables[1];



                if (dt.Rows.Count == 0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("No hay información");
                }

                progressBarControl1.Properties.Maximum = dt.Rows.Count;


                pagarFacturasRequest dataRequest = new pagarFacturasRequest();
                pagarFacturasDetalle fcDato = new pagarFacturasDetalle();
                List<pagarFacturasDetalle> fcLista = new List<pagarFacturasDetalle>();

                foreach (DataRow dr in dt.Rows)
                {
                    MonedaExtranjera = false;

                    dataRequest = new pagarFacturasRequest();
                    fcLista = new List<pagarFacturasDetalle>();

                    serie = dr["Serie"].ToString();
                    folioDep = Convert.ToInt32(dr["folio"]);

                    if (dr["codigoBancoSatDestino"].ToString().Length > 0)
                    {
                        dataRequest.codigoBancoSatDestino = dr["codigoBancoSatDestino"].ToString();
                        dataRequest.numeroCuentaDestino = dr["numeroCuentaDestino"].ToString();
                    }
                    else
                    {
                        dataRequest.codigoBancoSatDestino = string.Empty;
                        dataRequest.numeroCuentaDestino = string.Empty;
                    }

                    dataRequest.cuentaBancoCaja = dr["cuentaBancoCaja"].ToString();
                    dataRequest.cuentaProveedor = dr["cuentaTipoProveedor"].ToString();

                    
                    dataRequest.fechaPago = Convert.ToDateTime(dr["fechaPago"]).ToShortDateString();
                    dataRequest.formaPagoSat = dr["formaPagoSat"].ToString();
                    dataRequest.metodoPago = dr["metodoPago"].ToString();

                    dataRequest.referencia = dr["Referencia"].ToString();

                   

                    NombreProv = dr["Nombre"].ToString();

                    totalCobro = 0;

                    foreach (DataRow drDet in dtDet.Rows)
                    {

                        

                        strSeq = drDet["Seq"].ToString();

                        folioDepDet = Convert.ToInt32(drDet["Folio"]);
                        if (folioDep == folioDepDet)
                        {
                            strFechaCxP = drDet["Fecha"].ToString().Substring(0, 10);

                            if (dr["MonedasID"].ToString() != "MXN" || drDet["MonedaFac"].ToString() != "MXN")
                                MonedaExtranjera = true;
                            else
                                MonedaExtranjera = false;

                            //{
                            fcDato = new pagarFacturasDetalle();

                            if (Convert.ToInt32(drDet["TIPODEMOVIMIENTOCXPID"])==1 || Convert.ToInt32(drDet["TIPODEMOVIMIENTOCXPID"]) == 8)
                                if (chkCxpRef.Checked)
                                    fcDato.folio = drDet["CxPReferencia"].ToString();
                                else
                                    fcDato.folio = drDet["FolioFactura"].ToString();
                            else
                            {
                                //success = Int32.TryParse(drDet["FolioFactura"].ToString(), out number);

                                //if (success)
                                //if (Convert.ToDateTime(drDet["Fecha"]).Year < 2022 && Convert.ToDateTime(drDet["Fecha"]).Month > 4)
                                if (chkSerieFolio.Checked)
                                    fcDato.folio = drDet["CxPReferencia"].ToString();
                                //drDet["SerieFactura"].ToString() + drDet["FolioFactura"].ToString(); //Convert.ToInt32(strCadenaNum);
                                else
                                    fcDato.folio = drDet["FolioFactura"].ToString();

                                //else
                                //    fcDato.folio = drDet["folioContrarecibo"].ToString();
                            }

                            //fcDato.folio = drDet["FolioFactura"].ToString();


                            if (!MonedaExtranjera)
                            {
                                fcDato.importeAPagarMn = Convert.ToDecimal(drDet["SUPAGO"]);  
                                if (TC>0) 
                                    fcDato.importeAPagarMe = Math.Round(Convert.ToDecimal(drDet["SUPAGO"]) / TC, 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                MonedaExtranjera = true;
                                fcDato.importeAPagarMe = Convert.ToDecimal(drDet["SUPAGO"]);

                                //Si el pago es en MXN se ocupa mandar el importeAPagarME
                                if (dr["MonedasID"].ToString() == "MXN")
                                    fcDato.importeAPagarMn = Math.Round(Convert.ToDecimal(drDet["SUPAGO"]) * Convert.ToDecimal(dr["tipoCambioPagoMe"]), 2, MidpointRounding.AwayFromZero);

                                TC = Convert.ToDecimal(dr["tipoCambioPagoMe"]);
                                dataRequest.tipoCambioPagoMe = Convert.ToDecimal(dr["tipoCambioPagoMe"]);
                                dataRequest.netoAPagarMe = Math.Round(Convert.ToDecimal(dr["Importe"]) / TC, 2, MidpointRounding.AwayFromZero);
                                totalPago = dataRequest.netoAPagarMe;

                            }

                            totalCobro += Convert.ToDecimal(drDet["SUPAGO"]);

                            fcLista.Add(fcDato);
                        }
                    } // foreach Detalle

                    totalCobro= Math.Round(totalCobro, 2, MidpointRounding.AwayFromZero);

                    if (MonedaExtranjera)
                    {
                        //TC = Convert.ToDecimal(dr["tipoCambioPagoMe"]);
                        //dataRequest.tipoCambioPagoMe = Convert.ToDecimal(dr["tipoCambioPagoMe"]);
                        //dataRequest.netoAPagarMe = Math.Round(Convert.ToDecimal(dr["Importe"]) / TC, 2, MidpointRounding.AwayFromZero);
                        //totalPago = dataRequest.netoAPagarMe;


                    }
                    else
                    {
                        totalPago = Convert.ToDecimal(dr["Importe"]);
                        dataRequest.netoAPagarMn = Convert.ToDecimal(dr["Importe"]);
                    }
                    

                    //Ajustes
                    if (totalPago != totalCobro)
                    {
                        List<pagarFacturasAjustes> malista = new List<pagarFacturasAjustes>();
                        pagarFacturasAjustes ma = new pagarFacturasAjustes();
                        if (totalPago < totalCobro) //Pago mayor que suma de saldos de facturas
                        {
                            ma.abono = Math.Round((totalCobro - totalPago), 2);
                            ma.cuentaContable = "601 0083";
                        }
                        else
                        {
                            ma.cargo = Math.Round((totalPago - totalCobro), 2);
                            ma.cuentaContable = "601 0083";            //Debe ser otra
                        }

                        ma.concepto = "Diferencia";
                        ma.departamentoCodigo = string.Empty;
                        ma.sucursalCodigo = string.Empty;
                        malista.Add(ma);

                        dataRequest.movimientosAjuste = malista;

                    }

                    dataRequest.facturasAPagar = fcLista;

                    string url = "https://app.tesk.mx/diexsa/v1/pagos/";
                    erroresResponse datos = cliente.PeticionesWSPostWithHeadersString<erroresResponse>(url, "pagarFacturas", Autorization, CodigoEmpresa, dataRequest);


                    if (firstTime)
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        firstTime = false;
                    }
                    progressBarControl1.PerformStep();
                    progressBarControl1.Update();
                    if (datos != null)
                    {
                        if (datos.mensaje != null)
                        {
                            if (datos.validacionResponse != null)
                                strMensaje = "pagarFacturas: " + folioDep.ToString() + " Seq:" + strSeq + " " + NombreProv + " FechaCxP: " + strFechaCxP + " " + datos.validacionResponse.errores[0].error[0].ToString() + ' ' + datos.validacionResponse.errores[0].campo.ToString();
                            else
                                strMensaje = "pagarFacturas: " + folioDep.ToString() + " Seq:" + strSeq + " " + NombreProv + " FechaCxP: " + strFechaCxP + " " + " " + datos.mensaje.Replace("\n\r", "");
                            bitacoraErrores(strMensaje, "pagarFacturas");
                        }
                        else
                        {
                            if (datos.poliza != null)
                            {
                                poliza = datos.poliza.Substring(0, datos.poliza.Length - 11);
                                poliza = poliza.Trim();

                                cl.strTabla = "pagarFacturas";
                                cl.strSerie = serie;
                                cl.intFolio = Convert.ToInt32(folioDep);
                                cl.strPoliza = poliza;

                                string result = cl.teskSincronizaErp();

                                if (result != "OK")
                                {
                                    strMensaje = "Al sincronizar pagarFacturas: " + serie + folioDep.ToString() + " " + result;
                                    bitacoraErrores(strMensaje, "pagarFacturas");
                                }
                            }
                        }

                    }
                    else
                    {

                        if (datos != null)
                        {

                        

                        poliza = datos.poliza.Substring(0, datos.poliza.Length - 11);
                        poliza = poliza.Trim();


                        cl.strTabla = "pagarFacturas";
                        cl.strSerie = serie;
                        cl.intFolio = Convert.ToInt32(folioDep);
                        cl.strPoliza = poliza;

                        string result = cl.teskSincronizaErp();

                        if (result != "OK")
                        {
                            strMensaje = "Al sincronizar pagarFacturas: " + serie + folioDep.ToString() + " " + result;
                            bitacoraErrores(strMensaje, "pagarFacturas");
                        }
                        }
                    }

                    

                } //for each Master
            }
            catch (Exception ex)
            {
                strMensaje = "pagarFacturas: " + folioDep.ToString() + " " + NombreProv + " " + ex.Message;
                bitacoraErrores(strMensaje, "pagarFacturas");

            }
        }  //PagarFacturasCxP

        private void bbiPagosCxP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (!validaEjercicio())
                return;

            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Subir información", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Conectando con tesk...");



            IniTabla();
            string result = Login();

            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "PAGOS CXP";

            pagarFacturas();

            gridControl1.DataSource = dtResult;
            MessageBox.Show("Proceso terminado");
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}