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
using System.IO;
using System.Xml;

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class ComplementosdepagoXML : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public ComplementosdepagoXML()
        {
            InitializeComponent();
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

        private void bbiCargarxml_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string ruta = System.Configuration.ConfigurationManager.AppSettings["gridlayout"].ToString();
            ruta = ruta + "xmlCPRuta.txt";
            string dirIni = string.Empty;

            if (File.Exists(ruta))
            {
                string data = System.IO.File.ReadAllText(@ruta);
                if (data.Length > 0)
                {
                    dirIni = data.Substring(0,data.Length-2);
                }
                else
                {
                    dirIni = "C:\\";
                }
            }

            xtraOpenFileDialog1.InitialDirectory = dirIni;
            xtraOpenFileDialog1.ShowDragDropConfirmation = true;
            xtraOpenFileDialog1.AutoUpdateFilterDescription = false;
            xtraOpenFileDialog1.Filter = "XML files (*.xml)|*.xml";
            xtraOpenFileDialog1.Multiselect = false;

            DialogResult dr = xtraOpenFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string file = xtraOpenFileDialog1.FileName;
                string sDir = Path.GetDirectoryName(file);
                LeeXml(file);
            }
        }

        private void LeeXml(string filename)
        {

            DataTable dt = new DataTable();
            DataTable dtDr = new DataTable();
            DataTable dtOne = new DataTable();

            dt.Columns.Add("Fechapago", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("FormadepagoP", System.Type.GetType("System.String"));
            dt.Columns.Add("MonedaP", System.Type.GetType("System.String"));
            dt.Columns.Add("Monto", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("NumOperacion", System.Type.GetType("System.String"));
            dt.Columns.Add("RfcEmisorCtaOrd", System.Type.GetType("System.String"));
            dt.Columns.Add("NomBancoOrdExt", System.Type.GetType("System.String"));
            dt.Columns.Add("CtaOrdenante", System.Type.GetType("System.String"));
            dt.Columns.Add("RfcEmisorCtaBeneficiario", System.Type.GetType("System.String"));
            dt.Columns.Add("CtaBeneficiario", System.Type.GetType("System.String"));

            dtDr.Columns.Add("IdDocumento", System.Type.GetType("System.String"));
            dtDr.Columns.Add("Serie", System.Type.GetType("System.String"));
            dtDr.Columns.Add("Folio", System.Type.GetType("System.Decimal"));
            dtDr.Columns.Add("MonedaDR", System.Type.GetType("System.String"));
            dtDr.Columns.Add("MetodoDePagoDR", System.Type.GetType("System.String"));
            dtDr.Columns.Add("NumParcialidad", System.Type.GetType("System.String"));
            dtDr.Columns.Add("ImpSaldoAnt", System.Type.GetType("System.Decimal"));
            dtDr.Columns.Add("ImpPagado", System.Type.GetType("System.Decimal"));
            dtDr.Columns.Add("ImpSaldoInsoluto", System.Type.GetType("System.Decimal"));

            //----
            dtOne.Columns.Add("SerieCP", System.Type.GetType("System.String"));
            dtOne.Columns.Add("FolioCP", System.Type.GetType("System.String"));
            dtOne.Columns.Add("RfcEmisorCP", System.Type.GetType("System.String"));
            dtOne.Columns.Add("RfcReceptorCP", System.Type.GetType("System.String"));
            dtOne.Columns.Add("Fechapago", System.Type.GetType("System.DateTime"));
            dtOne.Columns.Add("FormadepagoP", System.Type.GetType("System.String"));
            dtOne.Columns.Add("MonedaP", System.Type.GetType("System.String"));
            dtOne.Columns.Add("Monto", System.Type.GetType("System.Decimal"));
            dtOne.Columns.Add("NumOperacion", System.Type.GetType("System.String"));
            dtOne.Columns.Add("RfcEmisorCtaOrd", System.Type.GetType("System.String"));
            dtOne.Columns.Add("NomBancoOrdExt", System.Type.GetType("System.String"));
            dtOne.Columns.Add("CtaOrdenante", System.Type.GetType("System.String"));
            dtOne.Columns.Add("RfcEmisorCtaBeneficiario", System.Type.GetType("System.String"));
            dtOne.Columns.Add("CtaBeneficiario", System.Type.GetType("System.String"));
            dtOne.Columns.Add("IdDocumento", System.Type.GetType("System.String"));
            dtOne.Columns.Add("Serie", System.Type.GetType("System.String"));
            dtOne.Columns.Add("Folio", System.Type.GetType("System.Decimal"));
            dtOne.Columns.Add("MonedaDR", System.Type.GetType("System.String"));
            dtOne.Columns.Add("MetodoDePagoDR", System.Type.GetType("System.String"));
            dtOne.Columns.Add("NumParcialidad", System.Type.GetType("System.String"));
            dtOne.Columns.Add("ImpSaldoAnt", System.Type.GetType("System.Decimal"));
            dtOne.Columns.Add("ImpPagado", System.Type.GetType("System.Decimal"));
            dtOne.Columns.Add("ImpSaldoInsoluto", System.Type.GetType("System.Decimal"));



            string sFechaPago = string.Empty;
            string sFormaDePagoP = string.Empty;
            string sMonedaP = string.Empty;
            string sMonto = string.Empty;
            string sNumOperacion = string.Empty;
            string sRfcEmisorCtaOrd = string.Empty;
            string sNomBancoOrdExt = string.Empty;
            string sCtaOrdenante = string.Empty;
            string sRfcEmisorCtaBen = string.Empty;
            string sCtaBeneficiario = string.Empty;

            string sIdDocumento = string.Empty;
            string sSerie = string.Empty;
            string sFolio = string.Empty;
            string sMonedaDR = string.Empty;
            string sMetodoDePagoDR = string.Empty;
            string sNumParcialidad = string.Empty;
            string sImpSaldoAnt = string.Empty;
            string sImpPagado = string.Empty;
            string sImpSaldoInsoluto = string.Empty;
                        
            string attrName = string.Empty;
            string attrImptoName = string.Empty;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            foreach (XmlNode nodeG in xmlDoc.ChildNodes)
            {
                //MessageBox.Show(nodeG.Name.ToString())
                

;               if (nodeG.Name.Equals("cfdi:Comprobante"))
                {
                    foreach (XmlAttribute attr in nodeG.Attributes)
                    {
                        attrName = attr.Name.ToString();
                        switch (attrName)
                        {
                            case "Serie":
                                txtSerie.Text = attr.Value.ToString();
                                break;
                            case "Folio":
                                txtFolio.Text = attr.Value.ToString();
                                break;
                            case "Fecha":
                                txtFecha.Text = attr.Value.ToString();
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
                                        txtEmisorRfc.Text = attr.Value.ToString();
                                        break;
                                    case "Nombre":
                                        txtEmisorNombre.Text = attr.Value.ToString();
                                        break;
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
                                        txtReceptorRfc.Text = attr.Value.ToString();
                                        break;
                                    case "Nombre":
                                        txtReceptorNombre.Text = attr.Value.ToString();
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                
                foreach (XmlNode node in element.ChildNodes)
                {
                   // MessageBox.Show(node.Name.ToString());

                    if (node.HasChildNodes)
                    {
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            string elemento2 = node2.Name.ToString();
                            if (elemento2 == "pago10:Pago")
                            {
                                foreach (XmlAttribute attr in node2.Attributes)
                                {
                                    attrName = attr.Name;
                                    switch (attrName)
                                    {
                                        case "FechaPago":
                                            sFechaPago = attr.Value.ToString();
                                            break;
                                        case "FormaDePagoP":
                                            sFormaDePagoP = attr.Value.ToString();
                                            break;
                                        case "MonedaP":
                                            sMonedaP = attr.Value.ToString();
                                            break;
                                        case "Monto":
                                            sMonto = attr.Value.ToString();
                                            break;
                                        case "NumOperacion":
                                            sNumOperacion = attr.Value.ToString();
                                            break;
                                        case "RfcEmisorCtaOrd":
                                            sRfcEmisorCtaOrd = attr.Value.ToString();
                                            break;
                                        case "NomBancoOrdExt":
                                            sNomBancoOrdExt = attr.Value.ToString();
                                            break;
                                        case "CtaOrdenante":
                                            sCtaOrdenante = attr.Value.ToString();
                                            break;
                                        case "RfcEmisorCtaBen":
                                            sRfcEmisorCtaBen = attr.Value.ToString();
                                            break;
                                        case "CtaBeneficiario":
                                            sCtaBeneficiario = attr.Value.ToString();
                                            break;
                                    } //foreach (XmlAttribute attr in node2.Attributes)
                                }


                                dt.Rows.Add(Convert.ToDateTime(sFechaPago), sFormaDePagoP, sMonedaP, Convert.ToDecimal(sMonto), sNumOperacion, sRfcEmisorCtaOrd, sNomBancoOrdExt, sCtaOrdenante, sRfcEmisorCtaBen, sCtaBeneficiario);
                                foreach (XmlNode node3 in node2.ChildNodes)
                                {
                                    if (node3.Name.Equals("pago10:DoctoRelacionado"))
                                    {
                                        foreach (XmlAttribute attr in node3.Attributes)
                                        {
                                            attrName = attr.Name;
                                           
                                            switch (attrName)
                                            {
                                                case "IdDocumento":
                                                    sIdDocumento = attr.Value.ToString();
                                                    break;
                                                case "Serie":
                                                    sSerie = attr.Value.ToString();
                                                    break;
                                                case "Folio":
                                                    sFolio = attr.Value.ToString();
                                                    break;
                                                case "MonedaDR":
                                                    sMonedaDR = attr.Value.ToString();
                                                    break;
                                                case "MetodoDePagoDR":
                                                    sMetodoDePagoDR = attr.Value.ToString();
                                                    break;
                                                case "NumParcialidad":
                                                    sNumParcialidad = attr.Value.ToString();
                                                    break;
                                                case "ImpSaldoAnt":
                                                    sImpSaldoAnt = attr.Value.ToString();
                                                    break;
                                                case "ImpPagado":
                                                    sImpPagado = attr.Value.ToString();
                                                    break;
                                                case "RfcEmisorCtaBen":
                                                    sRfcEmisorCtaBen = attr.Value.ToString();
                                                    break;
                                                case "ImpSaldoInsoluto":
                                                    sImpSaldoInsoluto = attr.Value.ToString();
                                                    break;
                                            } //foreach (XmlAttribute attr in node2.Attributes)
                                        }
                                        dtDr.Rows.Add(sIdDocumento, sSerie, sFolio, sMonedaDR, sMetodoDePagoDR, sNumParcialidad, Convert.ToDecimal(sImpSaldoAnt), Convert.ToDecimal(sImpPagado), Convert.ToDecimal(sImpSaldoInsoluto));
                                        dtOne.Rows.Add(txtSerie.Text,txtFolio.Text,txtEmisorRfc.Text,txtReceptorRfc.Text
                                            ,Convert.ToDateTime(sFechaPago), sFormaDePagoP, sMonedaP, Convert.ToDecimal(sMonto), sNumOperacion, sRfcEmisorCtaOrd, 
                                            sNomBancoOrdExt, sCtaOrdenante, sRfcEmisorCtaBen, sCtaBeneficiario
                                            , sIdDocumento, sSerie, sFolio, sMonedaDR, sMetodoDePagoDR, sNumParcialidad, Convert.ToDecimal(sImpSaldoAnt)
                                            , Convert.ToDecimal(sImpPagado), Convert.ToDecimal(sImpSaldoInsoluto));
                                    }

                                }
                                string tipo2 = node2.NodeType.ToString();

                            }
                        }
                    }

                }
            }
            gridControlPago.DataSource = dt;
            gridControlDoctosRelacionado.DataSource = dtDr;
            gridControlOne.DataSource = dtOne;

            //Se guarda ruta del xml
            string ruta = System.Configuration.ConfigurationManager.AppSettings["gridlayout"].ToString();
            ruta = ruta + "xmlCPRuta.txt";
            using (StreamWriter writer = new StreamWriter(ruta))
            {
                string sDir = Path.GetDirectoryName(filename) + "\\";
                writer.WriteLine(sDir);
            }

            return;
               

  
            
        } //LeeXML
    }
}