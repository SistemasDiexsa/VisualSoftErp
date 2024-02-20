using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
using System.Xml;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.CxP.Clases;

namespace VisualSoftErp.Operacion.CxP.Formas
{
   
    public partial class ContrarecibosFromXML : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string strRfcEmpresa;
        decimal pIvaProv;
        string strUUID;
        DataTable dt;
        int intFolio;
        string strSerie;
        public ContrarecibosFromXML()
        {
            InitializeComponent();

            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.ReadOnly = true;

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridContrarecibosXml";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string strLayoutPath = System.Configuration.ConfigurationManager.AppSettings["gridlayout"].ToString() + "xmlcompras.txt";
                string data = string.Empty;
                if (File.Exists(strLayoutPath))
                {
                    data = System.IO.File.ReadAllText(strLayoutPath);
                    data = data.Substring(0, data.Length - 2) + "\\";
                }

                if (data.Length == 0)
                    xtraOpenFileDialog1.InitialDirectory = "C:\\";
                else
                    xtraOpenFileDialog1.InitialDirectory = data;

                xtraOpenFileDialog1.ShowDragDropConfirmation = true;
                xtraOpenFileDialog1.AutoUpdateFilterDescription = false;
                xtraOpenFileDialog1.Filter = "XML files (*.xml)|*.xml";
                xtraOpenFileDialog1.Multiselect = true;

                DialogResult dr = xtraOpenFileDialog1.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    dt = new DataTable();
                    dt.Columns.Add("Serie");
                    dt.Columns.Add("Folio");
                    dt.Columns.Add("Fecha");
                    dt.Columns.Add("FormaPago");
                    dt.Columns.Add("SubTotal");
                    dt.Columns.Add("TipoCambio");
                    dt.Columns.Add("Moneda");
                    dt.Columns.Add("Total");
                    dt.Columns.Add("TipoDeComprobante");
                    dt.Columns.Add("MetodoPago");
                    dt.Columns.Add("EmisorRfc");
                    dt.Columns.Add("EmisorNombre");
                    dt.Columns.Add("UsoCFDI");
                    dt.Columns.Add("TotalImpuestosRetenidos");
                    dt.Columns.Add("TotalImpuestosTrasladados");
                    dt.Columns.Add("UUID");
                    dt.Columns.Add("Error");
                    dt.Columns.Add("ProveedoresID");
                    dt.Columns.Add("Plazo");
                    dt.Columns.Add("Descripcion");
                    dt.Columns.Add("PIva");
                    dt.Columns.Add("PRetIva");
                    dt.Columns.Add("PRetIsr");
                    dt.Columns.Add("Observación");
                    dt.Columns.Add("Guardado");
                    dt.Columns.Add("xml");
                    dt.Columns.Add("Descuento");


                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Newsa.net", "Analizando xml");

                    string sfile = xtraOpenFileDialog1.FileName;
                    string sDir = Path.GetDirectoryName(sfile);

                    foreach(String file in xtraOpenFileDialog1.FileNames)
                    {
                        CargaXML(file);
                    }

                    gridControl1.DataSource = dt;

                    if (dt.Rows.Count > 0)
                        bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                    globalCL clg = new globalCL();
                    clg.strGridLayout = "gridContrarecibosXml";
                    clg.restoreLayout(gridView1);

                    using (StreamWriter writer = new StreamWriter(strLayoutPath))
                    {
                        writer.WriteLine(sDir);
                    }

                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargaXML(string filename)
        {
            try
            {                
                string attrName = string.Empty;
                string strSerie = string.Empty;
                string strFolio = string.Empty;
                string strFecha = string.Empty;
                string strProv = string.Empty;
                string strRfcEmisor = string.Empty;
                string strRfcReceptor = string.Empty;
                string strTC = string.Empty;
                string strSubtotal = string.Empty;
                string strIva = string.Empty;
                string strRetIva = string.Empty;
                string strRetIvaProv = string.Empty;
                string strRetIsrProv = string.Empty;
                string strMonedaprov = string.Empty;
                string strFPProv = string.Empty;
                string strMPProv = string.Empty;
                string strUsoProv = string.Empty;
                string strRetIsr = string.Empty;
                string strNeto = string.Empty;
                string strTipoProv = string.Empty;
                string strEmisorNombre = string.Empty;
                string strReceptorNombre = string.Empty;
                string strFP = string.Empty;
                string strMoneda = string.Empty;
                string strTipo = string.Empty;
                string strMP = string.Empty;
                string strUso = string.Empty;
                decimal pDescto = 0;
                string strObs = string.Empty;


                cfdiCL cl = new cfdiCL();
                cl.pSerie = System.Configuration.ConfigurationManager.AppSettings["Serie"].ToString();
                string result = cl.DatosCfdiEmisor();

                if (result == "OK")
                {
                    strRfcEmpresa = cl.pEmisorRegFed;
                }
                else
                {
                    MessageBox.Show("No se pudo leer los datos de la empresa");
                    return;
                }

                ContraRecibosCL clcon = new ContraRecibosCL();

                //dt.Columns.Add("Cantidad", System.Type.GetType("System.String"));
                //dt.Columns.Add("Articulo", System.Type.GetType("System.String"));
                //dt.Columns.Add("Precio", System.Type.GetType("System.Decimal"));
                //dt.Columns.Add("Importe", System.Type.GetType("System.Decimal"));
                //dt.Columns.Add("Iva", System.Type.GetType("System.Decimal"));
                //dt.Columns.Add("Neto", System.Type.GetType("System.Decimal"));
                //dt.Columns.Add("Ieps", System.Type.GetType("System.Decimal"));
                //dt.Columns.Add("Costoum2", System.Type.GetType("System.Decimal"));
                //dt.Columns.Add("Factorum2", System.Type.GetType("System.Decimal"));
                //dt.Columns.Add("Piva", System.Type.GetType("System.Decimal"));
                //dt.Columns.Add("Pieps", System.Type.GetType("System.Decimal"));
                //dt.Columns.Add("Descuento", System.Type.GetType("System.Decimal"));
                //dt.Columns.Add("Pdescuento", System.Type.GetType("System.Decimal"));
                //dt.Columns.Add("Descripcion", System.Type.GetType("System.String"));
                //dt.Columns.Add("OCSerie", System.Type.GetType("System.String"));
                //dt.Columns.Add("OCNumero", System.Type.GetType("System.String"));
                //dt.Columns.Add("OCSeq", System.Type.GetType("System.String"));
                //dt.Columns.Add("NoIdentificacion", System.Type.GetType("System.String"));

                


                string sclaveProdServ = string.Empty;
                string sNoIdentificacion = string.Empty;
                string sCantidad = string.Empty;
                string sClaveUnidad = string.Empty;
                string sUnidad = string.Empty;
                string sDescripcion = string.Empty;
                string sValorUnitario = string.Empty;
                string sImporte = string.Empty;
                string sDescuento = string.Empty;
                int intProv = 0;
                int intPlazo = 0;
                int intIvaEstricto = 0;
                int intRetIvaEstricto = 0;
                decimal iva = 0;
                decimal neto = 0;
                string UUID = string.Empty;
                string strArt = string.Empty;
                string strError = "NO";
                string strTasaOCuotaTraslado = string.Empty;
                string fileSoloName = string.Empty;

                string attrImptoName = string.Empty;

                globalCL clg = new globalCL();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filename);

                fileSoloName = Path.GetFileName(filename);

                //Concepto
                foreach (XmlElement element in xmlDoc.DocumentElement)
                {
                    if (element.Name.ToUpper().Equals("CFDI:CONCEPTOS"))
                    {
                        foreach (XmlNode node in element.ChildNodes)
                        {
                            //MessageBox.Show(node.Name.ToString());
                            if (node.Name.ToUpper().Equals("CFDI:CONCEPTO"))
                            {

                                //MessageBox.Show("Dentro de cpto:" + node.Name.ToString());

                                sDescuento = "0";

                                foreach (XmlAttribute attr in node.Attributes)
                                {
                                    attrName = attr.Name.ToUpper();

                                    switch (attrName)
                                    {
                                        case "CLAVEPRODSERV":
                                            sclaveProdServ = attr.Value.ToString();
                                            break;
                                        case "NOIDENTIFICACION":
                                            sNoIdentificacion = attr.Value.ToString();
                                            break;
                                        case "CANTIDAD":
                                            sCantidad = attr.Value.ToString();
                                            break;
                                        case "CLAVEUNIDAD":
                                            sClaveUnidad = attr.Value.ToString();
                                            break;
                                        case "UNIDAD":
                                            sUnidad = attr.Value.ToString();
                                            break;
                                        case "DESCRIPCION":
                                            sDescripcion = attr.Value.ToString();
                                            break;
                                        case "VALORUNITARIO":
                                            sValorUnitario = attr.Value.ToString();
                                            break;
                                        case "IMPORTE":
                                            sImporte = attr.Value.ToString();
                                            break;
                                        case "DESCUENTO":
                                            sDescuento = attr.Value.ToString();
                                            break;
                                    } //Switch
                                }  // Atributes                             

                            } // Nodo concepto
                            if (sDescripcion.Length > 0)
                                break;
                        } //foreach conceptos
                    }

                    if (sDescripcion.Length > 0)
                        break;
                } // foreach (XmlElement element in xmlDoc.DocumentElement) Concepto

                //Datos generales
                foreach (XmlNode nodeG in xmlDoc.ChildNodes)
                {                   
                    if (nodeG.Name.ToUpper().Equals("CFDI:COMPROBANTE"))
                    {
                        foreach (XmlAttribute attr in nodeG.Attributes)
                        {
                            attrName = attr.Name.ToString();
                           
                            switch (attrName.ToUpper())
                            {
                                case "SERIE":
                                    strSerie = attr.Value.ToString();
                                    break;
                                case "FOLIO":
                                    strFolio = attr.Value.ToString();
                                    break;
                                case "FECHA":
                                    strFecha = attr.Value.ToString();
                                    break;
                                case "FORMAPAGO":
                                    strFP = attr.Value.ToString();
                                    break;
                                case "SUBTOTAL":
                                    strSubtotal = attr.Value.ToString();
                                    break;
                                case "TIPOCAMBIO":
                                    strTC = attr.Value.ToString();
                                    break;
                                case "MONEDA":
                                    strMoneda = attr.Value.ToString();
                                    break;
                                case "TOTAL":
                                    strNeto = attr.Value.ToString();
                                    break;
                                case "TIPODECOMPROBANTE":
                                    strTipo = attr.Value.ToString();
                                    break;
                                case "METODOPAGO":
                                    strMP = attr.Value.ToString();
                                    break;
                            }
                        }
                        foreach (XmlNode nodeD in nodeG.ChildNodes)
                        {
                            if (nodeD.Name.ToUpper().Equals("CFDI:EMISOR"))
                            {
                                foreach (XmlAttribute attr in nodeD.Attributes)
                                {
                                    attrName = attr.Name.ToString().ToUpper();
                                    switch (attrName)
                                    {
                                        case "RFC":
                                            strRfcEmisor = attr.Value.ToString();
                                            break;
                                        case "NOMBRE":
                                            strEmisorNombre = attr.Value.ToString();
                                            break;
                                    }
                                }
                            }

                            if (nodeD.Name.ToUpper().Equals("CFDI:COMPLEMENTO"))
                            {
                                foreach (XmlNode nodeU in nodeD.ChildNodes)
                                {
                                    if (nodeU.Name.ToUpper().Equals("TFD:TIMBREFISCALDIGITAL"))
                                    {
                                        foreach (XmlAttribute attr in nodeU.Attributes)
                                        {
                                            attrName = attr.Name.ToString().ToUpper();
                                            if (attrName == "UUID")
                                            {
                                                strUUID = attr.Value.ToString();
                                            }
                                        }
                                    }
                                }


                            }

                            if (nodeD.Name.ToUpper().Equals("CFDI:RECEPTOR"))
                            {
                                foreach (XmlAttribute attr in nodeD.Attributes)
                                {
                                    attrName = attr.Name.ToString().ToUpper();
                                    switch (attrName)
                                    {
                                        case "RFC":
                                            strRfcReceptor = attr.Value.ToString();
                                            break;
                                        case "NOMBRE":
                                            strReceptorNombre = attr.Value.ToString();
                                            break;
                                        case "USOCFDI":
                                            strUso = attr.Value.ToString();
                                            break;
                                    }
                                }
                            }  // Receptor
                            if (nodeD.Name.ToUpper().Equals("CFDI:IMPUESTOS"))
                            {
                                foreach (XmlAttribute attr in nodeD.Attributes)
                                {
                                    attrName = attr.Name.ToString().ToUpper();
                                    switch (attrName)
                                    {
                                        case "TOTALIMPUESTOSTRASLADADOS":
                                            strIva = attr.Value.ToString();
                                            break;
                                        case "TOTALIMPUESTOSRETENIDOS":
                                            strRetIva = attr.Value.ToString();
                                            break;
                                    }
                                }

                                //--
                                foreach (XmlNode nodedd in nodeD.ChildNodes)
                                {
                                    //MessageBox.Show(node.Name.ToString());
                                    if (nodedd.Name.ToUpper().Equals("CFDI:TRASLADOS"))
                                    {
                                        foreach (XmlNode nodeddd in nodedd.ChildNodes)
                                        {
                                            if (nodeddd.Name.ToUpper().Equals("CFDI:TRASLADO"))
                                            {
                                                foreach (XmlAttribute attr in nodeddd.Attributes)
                                                {

                                                    attrName = attr.Name.ToUpper();

                                                    switch (attrName)
                                                    {
                                                        case "TASAOCUOTA":
                                                            strTasaOCuotaTraslado = attr.Value.ToString();
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                        
                                    }
                                }
                                    //--
                            }  // Receptor
                        }
                    } //Comprobante
                } //foreach (XmlNode nodeG in xmlDoc.ChildNodes)

                ComprasCL clc = new ComprasCL();
                if (strTipo == "I" && strRfcEmpresa == strRfcReceptor)
                {

                    clc.strRfc = strRfcEmisor;
                    result = clc.Leeproveedorporrfc();
                    if (result == "OK")
                    {
                        intPlazo = clc.intPlazo;
                        intProv = clc.intProveedoresID;
                        pIvaProv = clc.decIva;
                        strRetIvaProv = clc.decRetIva.ToString();
                        strRetIsrProv = clc.decRetIsr.ToString();
                        strMonedaprov = clc.strMonedasID;
                        strFPProv = clc.strFP;
                        strMPProv = clc.strMP;
                        strUsoProv = clc.strUso;
                        intIvaEstricto = clc.intIvaEstricto;
                        intRetIvaEstricto = clc.intRetIvaEstricto;

                        if (clc.strTipoProv == "0")
                        {
                            strError="Este XML es de un proveedor de bienes, registrelo en compras";
                        }
                        else
                        {
                            clcon.intOrigen = 2;
                            clcon.strRfc = strRfcEmisor;
                            clcon.strUuid = strUUID;
                            clcon.strFac = strFolio;
                            result = clcon.ExisteFactura();
                            if (result == "OK")
                            {
                                if (clcon.intFolio > 0)
                                {
                                    strError = "01 Esta factura ya fué capturada en el contrarecibo " + clcon.intFolio.ToString() + " el día: " + clcon.fFecha.ToShortDateString();
                                }
                            }
                            else
                            {
                                strError = result;
                            }
                        }



                    }
                    else
                    {
                        strError="Este proveedor no está registrado o el rfc está incorrecto: " + strRfcEmisor + " " + strEmisorNombre;
                    }

                    
                    result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(strFecha).Year, Convert.ToDateTime(strFecha).Month, "CXP");
                    if (result == "C")
                    {
                        strError = "Este mes está cerrado, no se puede actualizar";
                    }



                    if (!clg.esNumerico(strRetIva))
                        strRetIva = "0";
                    if (!clg.esNumerico(strRetIsr))
                        strRetIsr = "0";
                    if (!clg.esNumerico(strIva))
                        strIva = "0";
                    if (!clg.esNumerico(strTasaOCuotaTraslado))
                        strTasaOCuotaTraslado = "0";
                    if (!clg.esNumerico(strTC))
                        strTC = "0";
                    if (!clg.esNumerico(strTC))
                        strRetIvaProv = "0";
                

                    if (strTasaOCuotaTraslado == "")
                        strTasaOCuotaTraslado = "0";

                    if (strError.Length==0)  //Por que ya trae un error mas relevante
                    {
                        if (Convert.ToDecimal(strTasaOCuotaTraslado) * 100 != pIvaProv)
                        {
                            if (intIvaEstricto == 1)
                                strError = "Iva diferente al del catalogo de proveedores";
                            else
                                strObs = "Iva diferente al del catálogo de proveedores";
                        }

                        if (strRetIva.Length == 0)
                            strRetIva = "0";

                        if (Convert.ToDecimal(strRetIva) != Convert.ToDecimal(strRetIvaProv))
                        {
                            if (intRetIvaEstricto == 1)
                            {
                                if (strRetIvaProv == "0.0000")
                                {
                                    strError = "Retencion de Iva diferente al del catalogo de proveedores";
                                }
                            }
                            else
                            {
                                if (strRetIvaProv == "0.0000")
                                {
                                    strObs = "Retencion de Iva diferente al del catalogo de proveedores";
                                }
                            }

                        }

                    if (strMonedaprov != strMoneda)
                    {
                        strObs = "Moneda diferente al del catalogo de proveedores";
                    }
                    if (strFP != strFPProv)
                    {
                        strObs = "Forma de pago diferente al del catalogo de proveedores";
                    }
                    if (strMP != strMPProv)
                    {
                        strObs = "Método de pago diferente del catálogo de proveedores";
                    }
                    if (strUso != strUsoProv)
                    {
                        strObs = "Uso de Cfdi diferente del catálogo de proveedores";
                    }
                } //if (strError.Length==0)  //Por que ya trae un error mas relevante

                    dt.Rows.Add(strSerie, strFolio, strFecha, strFP, strSubtotal, strTC, strMoneda, strNeto, strTipo, strMP,
                        strRfcEmisor, strEmisorNombre, strUso, strRetIva,strIva,  strUUID,strError, intProv,intPlazo, sDescripcion, 
                        Convert.ToDecimal(strTasaOCuotaTraslado)*100, strRetIvaProv,strRetIsrProv,strObs, "",fileSoloName,sDescuento);
                }
                //else
                //{
                //    strError = "XML no es de ingreso o no es para el rfc: " + strRfcEmpresa;
                //}
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string _Obs = (string)view.GetRowCellValue(e.RowHandle, "Observacion");
            string _mark = (string)view.GetRowCellValue(e.RowHandle, "Error");
            string _Save = (string)view.GetRowCellValue(e.RowHandle, "Guardado");

            if (_Obs != null)
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Orange;
            }

            if (_mark != "NO" && _mark != null)
            {
                if (_mark.Substring(0, 2) == "01")
                {
                    e.Appearance.BackColor = Color.SteelBlue;
                    e.Appearance.ForeColor = Color.White;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.ForeColor = Color.Red;
                }
                
            }else {
                if (_mark != null)
                {
                    if (_mark.Substring(0, 2) == "01")
                    {
                        e.Appearance.BackColor = Color.SteelBlue;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
                
            }

            if (_Save == "OK")
            {
                e.Appearance.BackColor = Color.Green;
                e.Appearance.ForeColor = Color.White;
            }

            
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            string dato = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, e.Column).ToString();
            Clipboard.SetDataObject(dato);
        }

        private void Guardar()
        {
            try
            {
                string Result = string.Empty;

                string sCondicion = String.Empty;
                //DT para Contrarecibos
                System.Data.DataTable dtContrarecibos = new System.Data.DataTable("Contrarecibos");
                dtContrarecibos.Columns.Add("Serie", Type.GetType("System.String"));
                dtContrarecibos.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtContrarecibos.Columns.Add("ProveedoresID", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("Plazo", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("MonedasID", Type.GetType("System.String"));
                dtContrarecibos.Columns.Add("Tipodecambio", Type.GetType("System.Decimal"));
                dtContrarecibos.Columns.Add("Status", Type.GetType("System.String"));
                dtContrarecibos.Columns.Add("Fechacancelacion", Type.GetType("System.DateTime"));
                dtContrarecibos.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("UsuarioCanceló", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("Fechareal", Type.GetType("System.DateTime"));
                dtContrarecibos.Columns.Add("Poliza", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("RazonCancelacion", Type.GetType("System.String"));
                dtContrarecibos.Columns.Add("Descripcion", Type.GetType("System.String"));
                dtContrarecibos.Columns.Add("Tesk", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("TeskPoliza", Type.GetType("System.String"));

                //DT para ContrarecibosDetalle
                System.Data.DataTable dtContrarecibosdetalle = new System.Data.DataTable("Pagoscxpdetalle");
                dtContrarecibosdetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtContrarecibosdetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtContrarecibosdetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtContrarecibosdetalle.Columns.Add("SerieFactura", Type.GetType("System.String"));
                dtContrarecibosdetalle.Columns.Add("Factura", Type.GetType("System.String"));
                dtContrarecibosdetalle.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtContrarecibosdetalle.Columns.Add("Importegravado", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("Importeexcento", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("PIva", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("PRetIva", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("PRetIsr", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("PIeps", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("RetIva", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("RetIsr", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("UUID", Type.GetType("System.String"));
                dtContrarecibosdetalle.Columns.Add("Tesk", Type.GetType("System.Int32"));
                dtContrarecibosdetalle.Columns.Add("TeskPoliza", Type.GetType("System.String"));
                dtContrarecibosdetalle.Columns.Add("ArchivoXml", Type.GetType("System.String"));

                Result =SiguienteID();
                if (Result != "OK")
                {
                    gridView1.SetFocusedRowCellValue("Guardado", Result);
                    gridView1.SetFocusedRowCellValue("Error", "Procesado");
                    return;
                }
                
                
                int intRen = 0;
                string dato = String.Empty;                
                string strFactura = String.Empty;
                int intSeq = 0;
                decimal dTipocambio = Convert.ToDecimal(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TipoCambio"));
                DateTime fdfecha = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Fecha"));
                decimal dImporteG = 0;
                decimal dImporteE = 0;
                decimal dPIva = 0;   //---- Falta
                decimal dPRetIva = 0; //--- Falta
                decimal dPRetIsr = 0; //--- Falta
                decimal dPIpes = 0; //se le dara un trato diferente
                decimal dIva = 0;
                decimal dRetIva = 0;
                decimal dRetIsr = 0;
                decimal dIeps = 0;  
                decimal dNeto = 0;
                string strserieFac = string.Empty;
                string strxml = string.Empty;
                decimal dDescto = 0;

                globalCL clg = new globalCL();

                string strUUID = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UUID").ToString();

                strserieFac = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Serie").ToString();
                        strFactura = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Folio").ToString();
                        fdfecha = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Fecha"));
                        dImporteG = Convert.ToDecimal(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SubTotal"));

                if (!clg.esNumerico(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Descuento").ToString()))
                    dDescto = 0;
                else
                    dDescto = Convert.ToDecimal(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Descuento"));
                dImporteE = 0;

                dImporteG = dImporteG - dDescto;

                if (!clg.esNumerico(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TotalImpuestosTrasladados").ToString()))
                            dIva = 0;
                else
                    dIva = Convert.ToDecimal(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TotalImpuestosTrasladados"));

                if (!clg.esNumerico(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TotalImpuestosRetenidos").ToString()))
                    dRetIva = 0;
                else
                    dRetIva = Convert.ToDecimal(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TotalImpuestosRetenidos"));

               
                        dPIva= Convert.ToDecimal(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PIva"));
                        dPRetIva = Convert.ToDecimal(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PRetIva"));
                        dPRetIsr = Convert.ToDecimal(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PRetIsr"));
                //dRetIsr = Convert.ToDecimal(grid.GetRowCellValue(grid.FocusedRowHandle, "UUID")); falta                        
                dNeto = Convert.ToDecimal(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Total"));
                strxml = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "xml").ToString();

                if (dNeto > 0)
                        {
                            dtContrarecibosdetalle.Rows.Add(
                            strSerie,
                            intFolio,
                            intSeq,
                            strserieFac,
                            strFactura,
                            fdfecha,
                            dImporteG,
                            dImporteE,
                            dPIva,
                            dPRetIva,
                            dPRetIsr,
                            dPIpes,
                            dIva,
                            dRetIva,
                            dRetIsr,
                            dIeps,
                            dNeto,
                            strUUID,
                            0,
                            "",
                            strxml
                            );

                            intSeq = intSeq + 1;
                        }
                    
                
                    
                DateTime fFecha = fdfecha;
                int intProveedoresID = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProveedoresID"));
                string strPlazo = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Plazo").ToString();
                string monedaproveedor= gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Moneda").ToString();
                string strStatus = "1";
                DateTime fFechaReal = Convert.ToDateTime(DateTime.Now);
                DateTime fFechaCancelacion = Convert.ToDateTime(DateTime.Now); //cambiar cuando haga lo de cancelar
                int intUsuarioCancelo = 0;
                int intPoliza = 0;
                string strRazoncancelacion = string.Empty;
                dato = String.Empty;

                dtContrarecibos.Rows.Add(
                    strSerie,
                    intFolio,
                    fFecha,
                    intProveedoresID,
                    strPlazo,
                    monedaproveedor,
                    dTipocambio,
                    strStatus,
                    fFechaCancelacion,
                    globalCL.gv_UsuarioID,
                    intUsuarioCancelo,
                    fFechaReal,
                    intPoliza,
                    strRazoncancelacion,
                    gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Descripcion").ToString(),
                    0,
                    "");

                if (intFolio==0)
                {
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Guardado", "Folio 0");
                }

                ContraRecibosCL cl = new ContraRecibosCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.dtm = dtContrarecibos;
                cl.dtd = dtContrarecibosdetalle;
                cl.intUsuarioID = 1;
                cl.strMaquina = Environment.MachineName;
                cl.strPrograma = "0221";

                if (dtContrarecibos.Rows.Count>0 && dtContrarecibosdetalle.Rows.Count>0)
                {
                    string result = cl.ContrarecibosCrud();
                    if (result == "OK")
                    {
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Guardado", "OK: " + intFolio.ToString());                        
                    }
                    else
                    {
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Guardado", result);
                    }
                }
                else
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Guardado", "Sin datos");
            }
            catch (Exception ex)
            {
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Guardado", ex.Message);
            }
        } //Guardar

        private string SiguienteID()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();


            ContraRecibosCL cl = new ContraRecibosCL();
            cl.strSerie = serie;
            cl.strDoc = "ContraRecibos";

            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {
                strSerie = serie;
                intFolio = cl.intID;
                return "OK";
            }
            else
            {
                return result;
            }

        }//SiguienteID

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult Result = MessageBox.Show("Iniciar proceso?", "Generar", MessageBoxButtons.YesNo);
            if (Result.ToString() == "Yes")
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Newsa.net", "Guardando...");
                foreach (int i in gridView1.GetSelectedRows())
                {
                    gridView1.FocusedRowHandle = i;
                    if (gridView1.GetRowCellValue(i,"Error").ToString()=="NO")
                        Guardar();
                }
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                MessageBox.Show("Proceso terminado");
            }
        }
    }
}
