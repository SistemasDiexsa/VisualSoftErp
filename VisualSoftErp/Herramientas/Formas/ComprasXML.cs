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
using System.Xml;
using DevExpress.XtraGrid;
using System.IO;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class ComprasXML : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        DataTable dt = new DataTable();
        DataTable dtx = new DataTable();
        string strRfcEmpresa = string.Empty;
        string strPath = string.Empty;

        public ComprasXML()
        {
            InitializeComponent();
            txtEjer.Text = DateTime.Now.Year.ToString();
            txtMes.Text = DateTime.Now.Month.ToString();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }
        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
        }

        private void bbiCargarXml_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();

            if (!clg.esNumerico(txtEjer.Text)) {
                MessageBox.Show("Teclee el ejercicio");
                return;
            }
            if (!clg.esNumerico(txtMes.Text)) {
                MessageBox.Show("Teclee el mes");
                return;
            }

           

            string strLayoutPath = System.Configuration.ConfigurationManager.AppSettings["gridlayout"].ToString() + "xmlboxcompras.txt";
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

                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();

                dtx = new DataTable();
                dtx.Columns.Add("UUID", Type.GetType("System.String"));
                dtx.Columns.Add("Rfc", Type.GetType("System.String"));
                dtx.Columns.Add("Nombre", Type.GetType("System.String"));
                dtx.Columns.Add("Serie", Type.GetType("System.String"));
                dtx.Columns.Add("FolioInterno", Type.GetType("System.String"));
                dtx.Columns.Add("Fecha", Type.GetType("System.String"));
                dtx.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dtx.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dtx.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtx.Columns.Add("RetIva", Type.GetType("System.Decimal"));
                dtx.Columns.Add("RetIsr", Type.GetType("System.Decimal"));
                dtx.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtx.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dtx.Columns.Add("Tipo", Type.GetType("System.String"));
                dtx.Columns.Add("FechaSinHora", Type.GetType("System.String"));
                dtx.Columns.Add("Tipodecomprobante", Type.GetType("System.String"));
                dtx.Columns.Add("Moneda", Type.GetType("System.String"));
                dtx.Columns.Add("Tipodecambio", Type.GetType("System.String"));
                dtx.Columns.Add("Path", Type.GetType("System.String"));
                dtx.Columns.Add("Ejercicio", Type.GetType("System.Int32"));
                dtx.Columns.Add("Mes", Type.GetType("System.Int32"));
                dtx.Columns.Add("Version", Type.GetType("System.String"));
                dtx.Columns.Add("FP", Type.GetType("System.String"));
                dtx.Columns.Add("MP", Type.GetType("System.String"));
                dtx.Columns.Add("USO", Type.GetType("System.String"));
                dtx.Columns.Add("CompraSerie", Type.GetType("System.String"));
                dtx.Columns.Add("CompraFolio", Type.GetType("System.Int32"));
                dtx.Columns.Add("Tipodeproveedor", Type.GetType("System.String"));


                dt = new DataTable();
               
                dt.Columns.Add("UUID", Type.GetType("System.String"));
                dt.Columns.Add("Rfc", Type.GetType("System.String"));
                dt.Columns.Add("Nombre", Type.GetType("System.String"));
                dt.Columns.Add("Serie", Type.GetType("System.String"));
                dt.Columns.Add("FolioInterno", Type.GetType("System.String"));
                dt.Columns.Add("Fecha", Type.GetType("System.String"));
                dt.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dt.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dt.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dt.Columns.Add("RetIva", Type.GetType("System.Decimal"));
                dt.Columns.Add("RetIsr", Type.GetType("System.Decimal"));
                dt.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dt.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dt.Columns.Add("Tipo", Type.GetType("System.String"));
                dt.Columns.Add("FechaSinHora", Type.GetType("System.String"));
                dt.Columns.Add("Tipodecomprobante", Type.GetType("System.String"));
                dt.Columns.Add("Moneda", Type.GetType("System.String"));
                dt.Columns.Add("Tipodecambio", Type.GetType("System.String"));
                dt.Columns.Add("Path", Type.GetType("System.String"));
                dt.Columns.Add("Ejercicio", Type.GetType("System.Int32"));
                dt.Columns.Add("Mes", Type.GetType("System.Int32"));
                dt.Columns.Add("Version", Type.GetType("System.String"));
                dt.Columns.Add("FP", Type.GetType("System.String"));
                dt.Columns.Add("MP", Type.GetType("System.String"));
                dt.Columns.Add("USO", Type.GetType("System.String"));
                dt.Columns.Add("CompraSerie", Type.GetType("System.String"));
                dt.Columns.Add("CompraFolio", Type.GetType("System.Int32"));
                dt.Columns.Add("Tipodeproveedor", Type.GetType("System.String"));

                foreach (string file in xtraOpenFileDialog1.FileNames)
                {
                    LeeXmlGrales(file);
                }

                CargaGrid();

                using (StreamWriter writer = new StreamWriter(strLayoutPath))
                {
                    //xmlboxcompras
                    string file = xtraOpenFileDialog1.FileName;
                    string sDir = Path.GetDirectoryName(file);
                    writer.WriteLine(sDir);
                }

                bbiImpresion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiVerificaXml.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            
            }
        }

        private void CargaGrid()
        {
            gridControl1.DataSource = dt;
            navigationFrame.SelectedPageIndex = 1;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridxmlrecibidos";
            clg.restoreLayout(gridView1);
        }

        private void LeeXmlGrales(string filename)
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
                string strNeto = string.Empty;
                string strTipoProv = string.Empty;
                string strEmisorNombre = string.Empty;
                string strReceptorNombre = string.Empty;
                string strFP = string.Empty;
                string strMoneda = string.Empty;
                string strTipo = string.Empty;
                string strMP = string.Empty;
                string strUso = string.Empty;
                string strDescuento = string.Empty;
                string struuid = string.Empty;
                string strieps = string.Empty;
                string strTipoCoP = "P";
                DateTime dFechaSinHora;
                string strPath = string.Empty;
                int intEjer=0;
                int intMes = 0;
                string strVer = string.Empty;
                string strFechaSinHora = string.Empty;
                globalCL clg = new globalCL();
                string nomMes = string.Empty;
                string nombreArchivo = string.Empty;
                string strRetIsr = "0";  //Falta este dato
                string strCompraSerie = string.Empty;
                int intCompraFolio = 0;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filename);


                foreach (XmlNode nodeG in xmlDoc.ChildNodes)
                {
                    //MessageBox.Show(nodeG.Name.ToString())

                    if (nodeG.Name.Equals("cfdi:Comprobante"))
                    {
                        foreach (XmlAttribute attr in nodeG.Attributes)
                        {
                            attrName = attr.Name.ToString();
                            switch (attrName)
                            {
                                case "Serie":
                                    strSerie = attr.Value.ToString();
                                    break;
                                case "Folio":
                                    strFolio = attr.Value.ToString();
                                    break;
                                case "version":
                                    strVer = attr.Value.ToString();
                                    break;
                                case "Fecha":
                                    strFecha = attr.Value.ToString();
                                    break;
                                case "FormaPago":
                                    strFP = attr.Value.ToString();
                                    break;
                                case "SubTotal":
                                    strSubtotal = attr.Value.ToString();
                                    break;
                                case "Descuento":
                                    strDescuento = attr.Value.ToString();
                                    break;
                                case "Moneda":
                                    strMoneda = attr.Value.ToString();
                                    break;
                                case "Total":
                                    strNeto = attr.Value.ToString();
                                    break;
                                case "TipoDeComprobante":
                                    strTipo = attr.Value.ToString();
                                    break;
                                case "MetodoPago":
                                    strMP = attr.Value.ToString();
                                    break;
                            }
                        }
                        foreach (XmlNode nodeD in nodeG.ChildNodes)
                        {
                            if (nodeD.Name.Equals("cfdi:Emisor"))
                            {
                                foreach (XmlAttribute attr in nodeD.Attributes)
                                {
                                    attrName = attr.Name.ToString();
                                    switch (attrName)
                                    {
                                        case "Rfc":
                                            strRfcEmisor = attr.Value.ToString();
                                            break;
                                        case "Nombre":
                                            strEmisorNombre = attr.Value.ToString();
                                            break;
                                    }
                                }
                            }
                            if (nodeD.Name.Equals("cfdi:Complemento"))
                            {
                                foreach (XmlNode nodeU in nodeD.ChildNodes)
                                {
                                    if (nodeU.Name.Equals("tfd:TimbreFiscalDigital"))
                                    {
                                        foreach (XmlAttribute attr in nodeU.Attributes)
                                        {
                                            attrName = attr.Name.ToString();
                                            if (attrName == "UUID")
                                            {
                                                struuid = attr.Value.ToString();
                                            }
                                        }
                                    }
                                }


                            }
                            if (nodeD.Name.Equals("cfdi:Receptor"))
                            {
                                foreach (XmlAttribute attr in nodeD.Attributes)
                                {
                                    attrName = attr.Name.ToString();
                                    switch (attrName)
                                    {
                                        case "Rfc":
                                            strRfcReceptor = attr.Value.ToString();
                                            break;
                                        case "Nombre":
                                            strReceptorNombre = attr.Value.ToString();
                                            break;
                                        case "UsoCFDI":
                                            strUso = attr.Value.ToString();
                                            break;
                                    }
                                }
                            }  // Receptor
                            if (nodeD.Name.Equals("cfdi:Impuestos"))
                            {
                                foreach (XmlAttribute attr in nodeD.Attributes)
                                {
                                    attrName = attr.Name.ToString();
                                    switch (attrName)
                                    {
                                        case "TotalImpuestosTrasladados":
                                            strIva = attr.Value.ToString();
                                            break;
                                        case "TotalImpuestosRetenidos":
                                            strRetIva = attr.Value.ToString();
                                            break;
                                    }
                                }
                            }  // Receptor
                        }
                    } //Comprobante
                } //foreach (XmlNode nodeG in xmlDoc.ChildNodes)

                if (strTipo == "I" && strRfcEmpresa == strRfcReceptor)
                {
                    ComprasCL cl = new ComprasCL();
                    cl.strRfc = strRfcEmisor;
                    string result = cl.Leeproveedorporrfc();
                    if (result == "OK")
                    {
                        strTipoProv = cl.strTipoProv;
                        switch (strTipoProv)
                        {
                            case "0":
                                strTipoProv = "Bienes";
                                break;
                            case "1":
                                strTipoProv = "Servicio";
                                break;
                        }
                    }
                    else
                    {
                        strTipoProv = "No registrado";
                    }


                    if (strDescuento=="")
                    {
                        strDescuento = "0";
                    }
                    if (strieps=="")
                    {
                        strieps = "0";
                    }
                    strFechaSinHora = strFecha.Substring(0, 4) + "-" + strFecha.Substring(5, 2) + "-" + strFecha.Substring(8, 2);
                    dFechaSinHora = Convert.ToDateTime(strFechaSinHora);
                    intEjer = dFechaSinHora.Year;
                    intMes = dFechaSinHora.Month;

                    strPath = System.Configuration.ConfigurationManager.AppSettings["pathXmlSat"].ToString();
                    if (!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(strPath);
                    }

                    strPath = strPath + "Recibidos\\";
                    if (!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(strPath);
                    }
                    strPath = strPath + intEjer + "\\";
                    if (!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(strPath);
                    }
                    
                    nomMes = clg.NombreDeMes(intMes,3);

                    strPath = strPath + nomMes + "\\";
                    if (!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(strPath);
                    }

                    nombreArchivo = strRfcEmisor + "_" + strSerie + strFolio + "_" + Path.GetFileName(filename);
                    //string sDirOriginal = Path.GetDirectoryName(filename);

                    //Se copia a un directorio controlado
                    strPath = strPath + nombreArchivo;
                    File.Copy(filename, strPath,true);

                    if (Convert.ToInt32(txtEjer.Text)==intEjer && Convert.ToInt32(txtMes.Text)==intMes)
                    {
                        if (!clg.esNumerico(strSubtotal))
                            strSubtotal = "0";
                        if (!clg.esNumerico(strDescuento))
                            strDescuento = "0";
                        if (!clg.esNumerico(strIva))
                            strIva = "0";
                        if (!clg.esNumerico(strRetIva))
                            strRetIva = "0";
                        if (!clg.esNumerico(strRetIsr))
                            strRetIsr = "0";
                        if (!clg.esNumerico(strieps))
                            strieps = "0";
                        if (!clg.esNumerico(strNeto))
                            strNeto = "0";

                        //Se checa si ya se metio la compra
                        clg.strUUID = struuid;
                        clg.strTipoProv = strTipoProv;
                        clg.strFac = strSerie + strFolio;

                        result = clg.xmlBoxBuscaCompraoCR();
                        if (result=="OK")
                        {
                            strCompraSerie = "OK";
                            intCompraFolio = clg.intFolio;
                        }
                        else
                        {
                            strCompraSerie = "";
                            intCompraFolio = 0;
                        }



                        dtx.Rows.Clear();
                        dtx.Rows.Add(struuid, strRfcEmisor, strEmisorNombre, strSerie, strFolio, strFecha, strSubtotal, strDescuento,
                        strIva, strRetIva,
                        strRetIsr, strieps, strNeto, strTipoCoP, strFechaSinHora, strTipo, strMoneda, strTC, strPath,
                        intEjer, intMes, strVer, strFP, strMP, strUso,strCompraSerie,intCompraFolio,strTipoProv);


                        dt.Rows.Add(struuid, strRfcEmisor, strEmisorNombre, strSerie, strFolio, strFecha, strSubtotal, strDescuento,
                        strIva, strRetIva,
                        strRetIsr, strieps, strNeto, strTipoCoP, strFechaSinHora, strTipo, strMoneda, strTC, strPath,
                        intEjer, intMes, strVer, strFP, strMP, strUso, strCompraSerie, intCompraFolio,strTipoProv);

                        GuardaXML();
                    }               

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void noseusaLeeXml(string filename)
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



                            dt.Rows.Add(sclaveProdServ, sNoIdentificacion, sCantidad, sClaveUnidad, sUnidad, sDescripcion, sValorUnitario, sImporte);

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

            //gridControlPrincipal.DataSource = dt;
            //gridView1.OptionsView.ShowAutoFilterRow = true;
            CargaGrid();           
        } //LeeXML

        void GuardaXML()
        {
            try
            {
                globalCL clg = new globalCL();
                clg.dtxml = dtx;
                clg.intEjer = Convert.ToInt32(txtEjer.Text);
                clg.intMes = Convert.ToInt32(txtMes.Text);
                clg.strMaq = Environment.MachineName;
                string result = clg.XmlBoxGuardar();
                if (result!="OK")
                {
                    MessageBox.Show("Error al guardar:" + result);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Al guardar:" + ex.Message);
            }
        }

        private void gridControlPrincipal_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) Console.WriteLine(file);
        }

        private void splitContainerControl1_Panel1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void gridControlPrincipal_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void ComprasXML_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void ComprasXML_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) Console.WriteLine(file);
        }

        private void gridControlPrincipal_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void splitContainerControl1_Panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void bbiVistaprevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void bbiCargarCompra_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string strLayoutPath = System.Configuration.ConfigurationManager.AppSettings["gridlayout"].ToString() + "xmlcompras.txt";
            string data = string.Empty;
            if (File.Exists(strLayoutPath))
            {
                data = System.IO.File.ReadAllText(strLayoutPath);
                data = data.Substring(0, data.Length - 2) + "\\";
            }

            if (data.Length==0)
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
                string file = xtraOpenFileDialog1.FileName;
                string sDir = Path.GetDirectoryName(file);
                //LeeXml(file);

               
                using (StreamWriter writer = new StreamWriter(strLayoutPath))
                {
                    writer.WriteLine(sDir);
                }
            }
        }


        private void bbiLeerXml_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();

            globalCL clg = new globalCL();

            if (!clg.esNumerico(txtEjer.Text))
            {
                MessageBox.Show("Teclee el ejercicio");
                return;
            }
            if (!clg.esNumerico(txtMes.Text))
            {
                MessageBox.Show("Teclee el mes");
                return;
            }

            clg.intEjer = Convert.ToInt32(txtEjer.Text);
            clg.intMes = Convert.ToInt32(txtMes.Text);

            gridControl1.DataSource = clg.XmlBoxGrid();
            navigationFrame.SelectedPageIndex = 1;
            
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            clg.strGridLayout = "gridxmlrecibidos";
            clg.restoreLayout(gridView1);
          

            bbiImpresion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVerificaXml.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Impresion()
        {
            try
            {
                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                string rutaXML = string.Empty;
                string rutaPDF = string.Empty;
                string sYear = string.Empty;
                string sMes = string.Empty;



                rutaXML = strPath;
                if (!File.Exists(rutaXML))
                {
                    MessageBox.Show("Xml no encontrado: " + rutaXML);
                    return;
                }

                rutaPDF = strPath.Substring(0, strPath.Length - 4) + ".pdf";
            
                vs.RutaXmlTimbrado = rutaXML;
                vs.RutaPdfTimbrado = rutaPDF;
                vs.ArchivoTcr = "Factura.vx25";
                string pRutaTcr;
                pRutaTcr = System.Configuration.ConfigurationManager.AppSettings["pathtcr"].ToString();
                vs.rutaQR = vs.RutaPdfTimbrado.Substring(0, vs.RutaPdfTimbrado.Length - 4) + ".jpg";
                vs.QRCodebar(vs.RutaXmlTimbrado, vs.rutaQR);

                vs.RutaTcr = pRutaTcr;
                
                vs.ImpresoraNombre = "";
                vs.Copias = "1";               
                vs.Cliente = ".";
                vs.Leyenda1 = ".";

                vs.CampoExtra2 = ".";
                vs.CampoExtra10 = ".";
                vs.VistaPrevia = "S";
                vs.CampoExtra8 = "SINLOGO";
               // string result = vs.ImprimeFormatoTagCode();
                //if (result!="OK")
                //{
                //    MessageBox.Show(result);
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("Impresión: " + ex.Message);
            }
        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            
        }

        private void bbiImpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Generando pdf...","Por favor espere");
            Impresion();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                strPath = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Path").ToString();
            }
            catch(Exception ex)
            {
               // MessageBox.Show("Asegúrese que la columna path esté disponible");
            }
        }

        private void bbiVerificaXml_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL cl = new globalCL();
            cl.VerificaComprobante(strPath, 0, DateTime.Now);
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridxmlrecibidos";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            this.Close();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiImpresion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVerificaXml.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 0;

        }
    }
}