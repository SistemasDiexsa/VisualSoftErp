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
using VisualSoftErp.Herramientas.Clases;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class Addendas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Addendas()
        {
            InitializeComponent();
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

       

        private void CasaLey()
        {
            //try
            //{



            //    StringBuilder sb = new StringBuilder();
            //    string xmlfile = @"C:\AVC\CV\Diexsa\XML33\2020\SEP\107362timbrado.xml";

            //
            //string Proveedor = string.Empty;
            //    string ProvSap = string.Empty;
            //    string Centro = string.Empty;
            //    string NumEnt = string.Empty;
            //    DateTime FechaEnt;
            //    string Remision = string.Empty;

            //    string serie = string.Empty;
            //    int FolioFac = 0;




            //    addendasCL cl = new addendasCL();
            //    cl.strSerie = "";
            //    cl.intFolio = 107790;  //Pedido

            //    string result = cl.CasaLeyGrales();
            //    if (result != "OK")
            //    {
            //        MessageBox.Show("Error:" + result);
            //        return;
            //    }

            //    vsFK.vsFinkok vs = new vsFK.vsFinkok();

            //    string valor = vs.ExtraeValor(xmlfile, "cfdi:Comprobante", "Folio");
            //    globalCL clg = new globalCL();
            //    if (!clg.esNumerico(valor))
            //    {
            //        MessageBox.Show("No se pudo extraer el Folio del XML");
            //        return;
            //    }

            //    FolioFac = Convert.ToInt32(valor);


            //    //Se trae el detalle de la factura y se mete a un datatable para recorrerlo
            //    System.Data.DataTable dt = new System.Data.DataTable("Facturasdetalle");
            //    dt.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
            //    dt.Columns.Add("UMC", Type.GetType("System.String"));
            //    dt.Columns.Add("Descuento", Type.GetType("System.Decimal"));
            //    dt.Columns.Add("ValorUnitario", Type.GetType("System.Decimal"));
            //    dt.Columns.Add("Iva", Type.GetType("System.Decimal"));
            //    dt.Columns.Add("PIva", Type.GetType("System.Decimal"));
            //    dt.Columns.Add("CodigoBarras", Type.GetType("System.String"));
            //    dt.Columns.Add("CodigoCasaLey", Type.GetType("System.String"));


            //    decimal Cantidad = 0;
            //    string UMC = string.Empty;
            //    decimal Descuento = 0;
            //    decimal Precio = 0;
            //    decimal Iva = 0;
            //    decimal TasaIva = 0;
            //    string CB = string.Empty;
            //    string CCL = string.Empty;
            //    int Ren = 0;

            //    strSerie = "";
            //    FolioFactura = FolioFac;
            //    dt = cl.CasaLeyFacturasDetalle();

            //    Proveedor = cl.strProveedor;
            //    ProvSap = cl.strProvSap;
            //    Centro = cl.strCentro;
            //    NumEnt = cl.strNumEnt;
            //    FechaEnt = cl.dFechaEnt;
            //    Remision = cl.strRem;

            //    sb.AppendLine("<cfdi:Addenda>");
            //    sb.AppendLine("<cley:ADDENDA_CLEY xmlns:cley=\"http://servicios.casaley.com.mx/factura_electronica\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xsi:schemaLocation=\"http://servicios.casaley.com.mx/factura_electronica http://servicios.casaley.com.mx/factura_electronica/XSD_ADDENDA_CASALEY.xsd\" VERSION=\"1.0\" CORREO=\"antonieta.beltran@casaley.com.mx\">");
            //    sb.AppendLine("<cley:MERCADERIAS>");
            //

            //sb.AppendLine("<cley:CD PROVEEDOR=\"" + Proveedor + "\" CENTRO=\"" + Centro + "\" NUMERO_ENTRADA=\"" + NumEnt + "\" FECHA_DE_ENTRADA=\"" + clg.FechaSQL(FechaEnt) + "\" PROVEEDOR_SAP=\"" + ProvSap + "\" NO_REMISION=\"" + Remision + "\" DESCUENTO=\"0.00\"/>");


            //    //Detalle
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        Cantidad = Convert.ToDecimal(dr["Cantidad"]);
            //        UMC = dr["UMC"].ToString();
            //        Descuento = Convert.ToDecimal(dr["Descuento"]);
            //        Precio = Convert.ToDecimal(dr["ValorUnitario"]);
            //        Iva = Convert.ToDecimal(dr["Iva"]);
            //        TasaIva = Convert.ToDecimal(dr["PIva"]);
            //        CB = dr["CodigoBarras"].ToString();
            //        CCL = dr["CodigoCasaLey"].ToString();


            //        Ren = Ren + 1;

            //        sb.Append("<cley:DETALLE RENGLON=\"" + Ren.ToString() + "\" ");
            //        sb.Append("CANTIDAD=\"" + Cantidad.ToString("####0") + "\" ");
            //        sb.Append("UMC=\"" + UMC + "\" ");
            //        sb.Append("DESCUENTO =\"" + Descuento.ToString("#####0.00") + "\" ");
            //        sb.Append("PRECIO_LISTA =\"" + Precio.ToString("#####0.00") + "\" ");
            //        sb.Append("IMPUESTO_IVA =\"" + Iva.ToString("#####0.00") + "\" ");
            //        sb.AppendLine("TASA_IVA =\"" + TasaIva.ToString() + "\">");
            //        sb.AppendLine("<cley:CODBAR_ARTICULO COD_BAR=\"" + CB + "\" />");
            //        sb.AppendLine("<cley:CLEY_ARTICULO COD_ARTICULO=\"" + CCL + "\" />");
            //        sb.AppendLine("</cley:DETALLE>");

            //    }
                
                

            //    //Eof:Detalle

            //    sb.AppendLine("</cley:MERCADERIAS>");
            //    sb.AppendLine("</cley:ADDENDA_CLEY>");


            //    vs.Rutacarpeta= @"C:\AVC\CV\Diexsa\XML33\2020\SEP\";
            //    vs.NombreArchivo = FolioFac.ToString() +  "timbradaAddenda";
            //    vs.RutaXmlTimbrado= @"C:\AVC\CV\Diexsa\XML33\2020\SEP\107362timbrado.xml";
            //    vs.Addenda = sb.ToString();
            //    vs.PegaAddenda();


  
           

            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void bbiCasaLey_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addendasCL cl = new addendasCL();
            //string result = cl.AddendaCasaLey("",106953,)
        }
    }
}