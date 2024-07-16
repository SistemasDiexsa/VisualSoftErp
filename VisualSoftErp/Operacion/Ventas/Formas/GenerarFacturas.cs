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
using DevExpress.XtraBars;
using System.Configuration;
using System.IO;
using VisualSoftErp.Clases;
using VisualSoftErp.Clases.HerrramientasCLs;
using DevExpress.XtraGrid.Views.Grid;
using VisualSoftErp.Operacion.Ventas.Clases;
using VisualSoftErp.Herramientas.Clases;
using DevExpress.XtraPdfViewer.Bars;

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class GenerarFacturas : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        //Pasar a vsErp
        string struso;
        string strmp;
        string strfp;

        string strTo = string.Empty;
        string sMoneda;
        DateTime dFecha;

        string RazonCan;
        int intPedidoFolio;
        string strPedidoSerie;

        public BindingList<detalleCL> detalle;
        int intClienteID;
        int intDesglosardescuentoalfacturar;
        int intAgenteID;
        int intplazo;
        int intExportacion;
        public bool blNuevo;
        int intStatus;

        string strformadepago;
        decimal dIvaPorcentaje;


        decimal subtotal;

        string strStatus;
        string strRazon;

        decimal dPIva;
        decimal dPIeps;

        decimal PtjeRetIsr;
        decimal PtjeRetIva;
        decimal RetPIva;
        decimal RetPIsr;
        int intUsuarioID = globalCL.gv_UsuarioID;
        string strTipoRelacion;

        string addenda = string.Empty;

        string serieElectronica = string.Empty;
        string serieFac = string.Empty;

        bool blnCCE = false;

        string strUUIDRel = string.Empty;

        System.Data.DataTable dtFacturasDetalle = new System.Data.DataTable("FacturasDetalle"); //FacturasLeePedidoSurtido

        int intPublicoengeneral;

        public GenerarFacturas()
        {
            InitializeComponent();

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            gridViewDetalle.OptionsBehavior.ReadOnly = true;
            gridViewDetalle.OptionsBehavior.Editable = false;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Generar Facturas";

            BuscarSerie();


            LlenarGrid();

            txtFecha.Text = DateTime.Now.ToShortDateString();

            CargaCombos();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private string BuscarSerie()
        {
            SerieCL cl = new SerieCL();
            cl.intUsuarioID = intUsuarioID;
            String Result = cl.BuscarSerieporUsuario();
            if (Result == "OK")
            {
                    cboSerie.EditValue = cl.strSerie;
                    cboSerie.ReadOnly = true;

            }
            else
            {
                MessageBox.Show(Result);
            }
            return "";
        } //BuscaSerie

        private void LlenarGrid()///gridprincipal
        {
            GenerarFacturasCL cl = new GenerarFacturasCL();
            cl.strSerie = cboSerie.EditValue.ToString();
            cl.intDesglosaDescuento = intDesglosardescuentoalfacturar;
            gridControlPrincipal.DataSource = cl.GenerarFacturasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridGeneraFacturas";
            clg.restoreLayout(gridViewPrincipal);

            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            gridViewPrincipal.Columns["SubTotal"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewPrincipal.Columns["SubTotal"].DisplayFormat.FormatString = "c2";
            gridViewPrincipal.Columns["Iva"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewPrincipal.Columns["Iva"].DisplayFormat.FormatString = "c2";
            gridViewPrincipal.Columns["Neto"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewPrincipal.Columns["Neto"].DisplayFormat.FormatString = "c2";
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();

            cl.strTabla = "Clientes";
            cboCliente.Properties.ValueMember = "Clave";
            cboCliente.Properties.DisplayMember = "Des";
            cboCliente.Properties.DataSource = cl.CargaCombos();
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCliente.Properties.PopulateColumns();
            cboCliente.Properties.Columns["Clave"].Visible = false;
            cboCliente.Properties.Columns["AgentesID"].Visible = false;
            cboCliente.Properties.Columns["Plazo"].Visible = false;
            cboCliente.Properties.Columns["Desglosardescuentoalfacturar"].Visible = false;
            cboCliente.Properties.Columns["SerieEle"].Visible = false;
            cboCliente.Properties.Columns["CanalesdeventaID"].Visible = false;
            cboCliente.Properties.NullText = "Seleccione un cliente";// con esta liena podemos poner una desceripcion al cbo 

            cl.strTabla = "Monedas";
            cboMonedas.Properties.ValueMember = "Clave";
            cboMonedas.Properties.DisplayMember = "Des";
            cboMonedas.Properties.DataSource = cl.CargaCombos();
            cboMonedas.Properties.ForceInitialize();
            cboMonedas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedas.Properties.PopulateColumns();
            cboMonedas.Properties.Columns["Clave"].Visible = false;
            cboMonedas.EditValue = cboMonedas.Properties.GetDataSourceValue(cboMonedas.Properties.ValueMember, 0);

            cl.strTabla = "Articulos";
            repositoryItemLookUpEditArticulo.ValueMember = "Clave";
            repositoryItemLookUpEditArticulo.DisplayMember = "Des";
            repositoryItemLookUpEditArticulo.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulo.ForceInitialize();
            repositoryItemLookUpEditArticulo.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

            cl.strTabla = "Serie";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Clave";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Des"].Visible = false;
            cboSerie.EditValue = cboSerie.Properties.GetDataSourceValue(cboSerie.Properties.ValueMember, 0);

            cl.strTabla = "FormadePago";
            cboFormadePago.Properties.ValueMember = "Clave";
            cboFormadePago.Properties.DisplayMember = "Des";
            cboFormadePago.Properties.DataSource = cl.CargaCombos();
            cboFormadePago.Properties.ForceInitialize();
            cboFormadePago.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFormadePago.Properties.PopulateColumns();
            cboFormadePago.Properties.Columns["Clave"].Visible = false;
            cboFormadePago.EditValue = cboFormadePago.Properties.GetDataSourceValue(cboFormadePago.Properties.ValueMember, 0);

            cl.strTabla = "MetododePago";
            cboMetododepago.Properties.ValueMember = "Clave";
            cboMetododepago.Properties.DisplayMember = "Des";
            cboMetododepago.Properties.DataSource = cl.CargaCombos();
            cboMetododepago.Properties.ForceInitialize();
            cboMetododepago.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMetododepago.Properties.PopulateColumns();
            cboMetododepago.Properties.Columns["Clave"].Visible = false;
            cboMetododepago.EditValue = cboMetododepago.Properties.GetDataSourceValue(cboMetododepago.Properties.ValueMember, 0);

            cl.strTabla = "Usodecfdi";
            cboUsocfdi.Properties.ValueMember = "Clave";
            cboUsocfdi.Properties.DisplayMember = "Des";
            cboUsocfdi.Properties.DataSource = cl.CargaCombos();
            cboUsocfdi.Properties.ForceInitialize();
            cboUsocfdi.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboUsocfdi.Properties.PopulateColumns();
            cboUsocfdi.Properties.Columns["Clave"].Visible = false;
            cboUsocfdi.EditValue = cboUsocfdi.Properties.GetDataSourceValue(cboUsocfdi.Properties.ValueMember, 0);

            cl.strTabla = "Tipoderelacion";
            cboRelacion.Properties.ValueMember = "Clave";
            cboRelacion.Properties.DisplayMember = "Des";
            cboRelacion.Properties.DataSource = cl.CargaCombos();
            cboRelacion.Properties.ForceInitialize();
            cboRelacion.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboRelacion.Properties.PopulateColumns();
            cboRelacion.Properties.Columns["Clave"].Visible = false;
            cboRelacion.Properties.NullText = "Seleccione un tipo de relacion";// con esta liena podemos poner una desceripcion al cbo 

            cl.strTabla = "Almacenes";
            cboAlmacen.Properties.ValueMember = "Clave";
            cboAlmacen.Properties.DisplayMember = "Des";
            cboAlmacen.Properties.DataSource = cl.CargaCombos();
            cboAlmacen.Properties.ForceInitialize();
            cboAlmacen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacen.Properties.PopulateColumns();
            cboAlmacen.Properties.Columns["Clave"].Visible = false;
            cboAlmacen.EditValue = cboAlmacen.Properties.GetDataSourceValue(cboAlmacen.Properties.ValueMember, 0);

            cl.strTabla = "Canalesdeventa";
            cboCanalVentas.Properties.ValueMember = "Clave";
            cboCanalVentas.Properties.DisplayMember = "Des";
            cboCanalVentas.Properties.DataSource = cl.CargaCombos();
            cboCanalVentas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCanalVentas.Properties.ForceInitialize();
            cboCanalVentas.Properties.PopulateColumns();
            cboCanalVentas.Properties.Columns["Clave"].Visible = false;
            cboCanalVentas.Properties.NullText = "Seleccione una Canal de venta";
        }//CargaCombo

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;

            gridViewDetalle.OptionsBehavior.ReadOnly = true;
            gridViewDetalle.OptionsBehavior.Editable = false;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridGeneraFacturasdetalle";
            clg.restoreLayout(gridViewDetalle);

            gridViewDetalle.OptionsView.RowAutoHeight = true;
        }

        public class detalleCL
        {
            public string Articulo { get; set; }
            public string Des { get; set; }
            public decimal Cantidad { get; set; }
            public string Um { get; set; }
            public decimal Precio { get; set; }
            public decimal Importe { get; set; }
            public decimal Pdescuento { get; set; }
            public decimal Neto { get; set; }
            public decimal Iva { get; set; }
            public decimal IEPS { get; set; }
            public decimal Piva { get; set; }
            public decimal Pieps { get; set; }
            public decimal Descuento { get; set; }
            public string ClaveProdServ { get; set; }
            public string c_ClaveUnidad { get; set; }
            public string TotalArticulo { get; set; }
        }//

        private void Facturar()
        {
            LlenaCajas();
            //DetalleLlenaCajas();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiHistorial.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVer.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ribbonPageGroup1.Visible = false;
            navBarControl.Visible = false;
            ribbonStatusBar.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
            blNuevo = true;
            if (serieElectronica.Length==0)
                SiguienteID();
            //Se sugiere pesos, pero quizas luego dependa mejor de la moneda que pudieramos capturar en clientes
            cboMonedas.ItemIndex = 0;
            txtTipodecambio.Text = "1";
        }//Nuevo

        private void LimpiaCajas()
        {
            cboCliente.EditValue = null;
            txtAgente.Text = string.Empty;
            txtOrdendecompra.Text = string.Empty;
            cboCondicionesdepago.SelectedItem = 0;
            swExportacion.IsOn = false;
            cboMonedas.EditValue = null;
            txtTipodecambio.Text = string.Empty;           
            txtFolio.Text = string.Empty;
            cboFormadePago.EditValue = null;
            cboMetododepago.EditValue = null;
            cboUsocfdi.EditValue = null;
            cboRelacion.EditValue = null;
            txtFolioacturarelacionada.Text = string.Empty;
            txtPredial.Text = string.Empty;
            txtPdescto.Text = string.Empty;
            txtSuma.Text = string.Empty;
            txtDescuento.Text = string.Empty;
            txtPdescto.Text = string.Empty;
            txtSubtotal.Text = string.Empty;
            txtIva.Text = string.Empty;
            txtIeps.Text = string.Empty;
            txtRetiva.Text = string.Empty;
            txtRetIsr.Text = string.Empty;
            txtNeto.Text = string.Empty;
            txtObservaciones.Text = string.Empty;

        }//LimpiaCajas

        private void SiguienteID()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            FacturasCL cl = new FacturasCL();
            cl.strSerie = serie;
            cl.strDoc = "Facturas";

            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {
                txtFolio.Text = cl.intID.ToString();
            }
            else
            {
                MessageBox.Show("SiguienteID :" + result);
            }

        }//SguienteID

        private string Valida()
        {
            if (cboSerie.Text.Length == 0)
            {
                string sserie = System.Configuration.ConfigurationManager.AppSettings["Serie"].ToString();
                if (sserie!=cboSerie.Text)
                {
                    return "El campo Serie no puede ir vacio";
                }
            }
            if (txtFolio.Text.Length == 0)
            {
                return "El campo folio no puede ir vacio";
            }
            if (cboCondicionesdepago.Text.Length == 0)
            {
                return "El campo Condicionesdepago no puede ir vacio";
            }
            if (txtSubtotal.Text.Length == 0)
            {
                return "El campo SubTotal no puede ir vacio";
            }
            if (txtNeto.Text.Length == 0)
            {
                return "El campo Total no puede ir vacio";
            }
            if (cboCliente.EditValue == null)
            {
                return "El campo ClientesID no puede ir vacio";
            }
            if (gridViewDetalle.RowCount == 1)
            {
                return "Capture al menos un renglon";
            }
            if (cboCanalVentas.EditValue == null)
            {
                return "Capture el canal de venta";
            }
            if (txtFolioacturarelacionada.Text.Length==0)
            {
                if (cboRelacion.ItemIndex >= 0)
                {
                    return "Teclee la factura relacionada";
                }
                else
                {
                    txtFolioacturarelacionada.Text = "0";
                }
            }
            else
            {
                globalCL cl = new globalCL();
                if (!cl.esNumerico(txtFolioacturarelacionada.Text))
                {
                    return "Si va utilizar la factura relacionada, debe ser númerica";
                }

                if (Convert.ToInt32(txtFolioacturarelacionada.Text)>0)
                {
                    if (cboRelacion.ItemIndex<0)
                    {
                        return "Seleccione el tipo de relación";
                    }
                }
            }

            globalCL clg = new globalCL();
            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month, "VTA");
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";
            }

            return "OK";
        } //Valida

        private string uuidRelacionado()
        {
            try
            {
                FacturasCL cl = new FacturasCL();
                cl.strSerie = txtSeriefacturarelacionada.Text;
                cl.intFolio = Convert.ToInt32(txtFolioacturarelacionada.Text);
                string result = cl.FacturasLeeFecha();
                if (result == "OK")
                {

                    if (cl.intClientesID != Convert.ToInt32(cboCliente.EditValue))
                    {
                        return "El cliente es diferente al cliente del CFDI relacionado";

                    }

                    string suuid = string.Empty;
                    string sYear = string.Empty;
                    string sMes = string.Empty;
                    string sPathXML = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString();

                    sYear = cl.fFecha.Year.ToString();

                    globalCL clg = new globalCL();

                    sMes = clg.NombreDeMes(cl.fFecha.Month);


                    sPathXML = sPathXML + sYear + "\\" + sMes + "\\" + txtSeriefacturarelacionada.Text + txtFolioacturarelacionada.Text + "timbrado.XML";
                    if (File.Exists(sPathXML))
                    {
                        vsFK.vsFinkok vs = new vsFK.vsFinkok();
                        suuid = vs.ExtraeValor(sPathXML, "tfd:TimbreFiscalDigital", "UUID");
                        if (suuid.Length == 0)
                        {
                            return "No se pudo leer el UUID del XML";
                        }
                        else
                        {
                            strUUIDRel = suuid;
                            return "OK";
                        }
                    }
                    else
                    {
                        return "No se encontró el archivo: " + sPathXML;
                    }



                }
                else
                {
                    return "No se encontró la factura relacionada: " + txtSeriefacturarelacionada.Text + txtFolioacturarelacionada.Text;
                }

            }
            catch (Exception ex)
            {
                return "uuidRelacionado: " + ex.Message;
            }
        }

        private string Timbrar()
        {
            try
            {
                string art;
                int intSeq;
                decimal dCantidad;
                int intArticulosID;
                string strDescripcion;
                decimal dPrecioLista;
                decimal dImporte;
                decimal dIvaImporte;
                string strClaveProdServ;
                string pc_ClaveUnidad;
                string strUnidadM;
                decimal pValorUnitario = 0; // aqui inicia cuando debugeo
                string pNumeroPedimento = "NA"; //preguntar
                decimal dPorcentajededescuento = 0, dDescuento = 0, dPIeps, dIeps, pUltimoCosto = 0;
                decimal dSubTotal = 0; //Borrar si no se usa
                decimal dIvaTotal = 0; //Borrar si no se usa
                decimal dNetoTotal = 0;
                decimal dSumaDesc = Convert.ToDecimal(txtDescuento.Text.Replace("$", ""));

                #region DataTable para facturacion detalle
                dtFacturasDetalle = new DataTable();
                dtFacturasDetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtFacturasDetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtFacturasDetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtFacturasDetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtFacturasDetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("ClaveProdServ", Type.GetType("System.String"));
                dtFacturasDetalle.Columns.Add("c_ClaveUnidad", Type.GetType("System.String"));
                dtFacturasDetalle.Columns.Add("Unidad", Type.GetType("System.String"));
                dtFacturasDetalle.Columns.Add("Descripcion", Type.GetType("System.String"));
                dtFacturasDetalle.Columns.Add("ValorUnitario", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("Importe", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("Porcentajededescuento", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("PIva", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("PIeps", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("NumeroPedimento", Type.GetType("System.String"));
                dtFacturasDetalle.Columns.Add("UltimoCosto", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("Preciodelista", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("PRetIva", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("PRetIsr", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("RetIva", Type.GetType("System.Decimal"));
                dtFacturasDetalle.Columns.Add("RetIsr", Type.GetType("System.Decimal"));
                #endregion dt para facturacion detalle

                cfdiCL cl = new cfdiCL();
                cl.pSerie = cboSerie.EditValue.ToString();
                cl.pCliente = Convert.ToInt32(cboCliente.EditValue);
                if (serieElectronica.Length==0)
                    SiguienteID();
                cl.pFolio = Convert.ToInt32(txtFolio.Text);
                cl.pFormaPago = cboFormadePago.EditValue.ToString();
                cl.pPlazo = Convert.ToInt32(txtPlazo.Text);
                cl.pMoneda = cboMonedas.EditValue.ToString();
                cl.pTipocambio = Convert.ToDecimal(txtTipodecambio.Text);
                cl.pMetodoPago = cboMetododepago.EditValue.ToString();

                if (cboRelacion.ItemIndex == -1)
                {
                    cl.pTiporelacion = "";
                }
                else
                {
                    cl.pTiporelacion = cboRelacion.EditValue.ToString();
                }

                cl.pUuidrelacionado = "";
                if (cl.pTiporelacion.Length > 0)
                {
                    string result2=uuidRelacionado();
                    if (result2 != "OK")
                        return result2;
                    else
                        cl.pUuidrelacionado = strUUIDRel;
                }
                cl.pUsoCfdi = cboUsocfdi.EditValue.ToString();

                dPrecioLista = 0;  //Cambiar

                //Recorrer el griddetalle para extraer los datos y pasarlos al dt
                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    art = gridViewDetalle.GetRowCellValue(i, "Articulo").ToString();
                    if (art.Length > 0)
                    {
                        intSeq = i;
                        dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Cantidad"));
                        intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Articulo"));
                        strDescripcion = gridViewDetalle.GetRowCellValue(i, "Des").ToString();
                        pValorUnitario = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Precio"));
                        dImporte = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importe"));
                        dIvaImporte = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Iva"));
                        dIvaPorcentaje = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Piva"));
                        dPIeps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Pieps"));
                        dIeps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "IEPS"));
                        strClaveProdServ = gridViewDetalle.GetRowCellValue(i, "ClaveProdServ").ToString();
                        pc_ClaveUnidad = gridViewDetalle.GetRowCellValue(i, "c_ClaveUnidad").ToString();
                        strUnidadM = gridViewDetalle.GetRowCellValue(i, "Um").ToString();
                        dDescuento = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Descuento"));
                        dPorcentajededescuento = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Pdescuento"));

                        dtFacturasDetalle.Rows.Add(
                        serieFac,
                        txtFolio.Text,
                        intSeq,
                        intArticulosID,
                        dCantidad,
                        strClaveProdServ,
                        pc_ClaveUnidad,
                        strUnidadM,
                        strDescripcion,
                        pValorUnitario,
                        dImporte,
                        dPorcentajededescuento,
                        dDescuento,
                        dIvaPorcentaje,
                        dIvaImporte,
                        dPIeps,
                        dIeps,
                        pNumeroPedimento,
                        pUltimoCosto,
                        dPrecioLista,
                        PtjeRetIva,
                        PtjeRetIsr,
                        RetPIva,
                        RetPIsr);

                        dNetoTotal += ((((dIvaImporte + dIeps + dImporte) - dDescuento) - RetPIva) - RetPIsr);
                    }
                }//for

                string result = string.Empty;
                cl.pFolioPedido = intPedidoFolio;
                if (serieElectronica.Length == 0 && !swNoTimbrar.IsOn)
                    result = cl.GeneraCfdi33(dtFacturasDetalle,blnCCE, intPublicoengeneral,lblCfdiVer.Text);// este es lo ultimo que se manda llamar despues de llenar los valores que ocupa la clase cfdicl
                else
                    result = "OK";



                // usar estas variables para meterlas al datatable de facturas generales para ignorar las cajas de texto, manda xml 
                //puuid = vs.ExtraeValor(strrutaxmltimbrado, "tfd:TimbreFiscalDigital", "UUID");
                //pexttotal = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "Total");
                //pextsubtotal = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "SubTotal");
                //pextiva = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Impuestos", "TotalImpuestosTrasladados");
                //pextretiva = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Impuestos", "TotalImpuestosRetenidos");
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Al timbrar: " + ex.Message);
                return "Error";
            }


        }

        private void Guardar()
        {
            try
            {
                String Result = Valida();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }

                if (serieElectronica.Length == 0)
                    serieFac = cboSerie.EditValue.ToString();
                else
                    serieFac = serieElectronica;

                if (swNoTimbrar.IsOn)
                {
                    DialogResult resultnt = MessageBox.Show("Seleccionó NO timbrar, esta correcto?", strStatus, MessageBoxButtons.YesNo);
                    if (resultnt.ToString() != "Yes")
                    {
                        return;
                    }
                }
                
                Result = Timbrar();   // Se manda llamar Timbrar aunque sea serie C (timbrada en eDoc) por que se ocupa llenar DtDetalle, pero al final no timbra
                if (Result != "OK")
                {
                    MessageBox.Show("Timbrar() regresa: " + Result, "Error al Timbrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string sCondicion = String.Empty;
                System.Data.DataTable dtFacturas = new System.Data.DataTable("Facturas");
                #region dtFacturas
                dtFacturas.Columns.Add("Serie", Type.GetType("System.String"));
                dtFacturas.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtFacturas.Columns.Add("c_FormaPago", Type.GetType("System.String"));
                dtFacturas.Columns.Add("Condicionesdepago", Type.GetType("System.String"));
                dtFacturas.Columns.Add("SubTotal", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("c_Moneda", Type.GetType("System.String"));
                dtFacturas.Columns.Add("TipoCambio", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("Total", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("c_Tipodecomprobante", Type.GetType("System.String"));
                dtFacturas.Columns.Add("c_MetodoPago", Type.GetType("System.String"));
                dtFacturas.Columns.Add("LugarExpedicionCP", Type.GetType("System.String"));
                dtFacturas.Columns.Add("Confirmacion", Type.GetType("System.String"));
                dtFacturas.Columns.Add("TipoRelacion", Type.GetType("System.String"));
                dtFacturas.Columns.Add("CfdiRelacionadoUUID", Type.GetType("System.String"));
                dtFacturas.Columns.Add("ClientesID", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("c_UsoCFDI", Type.GetType("System.String"));
                dtFacturas.Columns.Add("TotalmpuestosRetenidos", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("TotalImpuestosTrasladados", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("UUID", Type.GetType("System.String"));
                dtFacturas.Columns.Add("AgentesID", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("Status", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("Plazo", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("FechaCancelacion", Type.GetType("System.DateTime"));
                dtFacturas.Columns.Add("RazonCancelacion", Type.GetType("System.String"));
                dtFacturas.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("AlmacenesID", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("Exportacion", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("Observaciones", Type.GetType("System.String"));
                dtFacturas.Columns.Add("Predial", Type.GetType("System.String"));
                dtFacturas.Columns.Add("Oc", Type.GetType("System.String"));
                dtFacturas.Columns.Add("Seriefacturarelacionada", Type.GetType("System.String"));
                dtFacturas.Columns.Add("Foliofacturarelacionada", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("Tesk", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("Publicoengeneral", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("CanalesdeventaID", Type.GetType("System.Int32"));
                #endregion dtFacturas



                string art;
                int intSeq;
                decimal dCantidad;
                int intArticulosID;
                string strDescripcion;
                decimal dPrecio;
                decimal dImporte;
                decimal dIvaImporte;

                string strClaveProdServ;
                string pc_ClaveUnidad;
                string strUnidadM;
                decimal pValorUnitario = 0;
                string pNumeroPedimento = "NA"; //preguntar
                decimal dPorcentajededescuento = 0, dDescuento = 0,
                           dPIeps, dIeps, pUltimoCosto = 0;

                decimal dSubTotal = 0;
                decimal dIvaTotal = 0;
                decimal dNetoTotal = 0;

                
              
                int intClienteID = Convert.ToInt32(cboCliente.EditValue);
                int intStatus = 1;
                int intAgente = intAgenteID;

                decimal intParidad = Convert.ToDecimal(txtTipodecambio.Text);

                int intUsuarioID = globalCL.gv_UsuarioID;
                string strMonedasID = cboMonedas.EditValue.ToString();


                int intExportacion = swExportacion.IsOn ? 1 : 0;
                string strCotizacionSerie = "";
                int intCotizacionFolio = 0;

               
                string pConfirmacion = "NA";
                string pc_Tipodecomprobante = "I";
                decimal pTotalmpuestosRetenidos = (Convert.ToDecimal(txtRetiva.Text.Replace("$", "")) + Convert.ToDecimal(txtRetIsr.Text.Replace("$", "")));
                decimal pTotalImpuestosTrasladados = (Convert.ToDecimal(txtIva.Text.Replace("$", "")) + Convert.ToDecimal(txtIeps.Text.Replace("$", "")));
                string pUUID = "NA", strCfdiRelacionadoUUID= "NA";
                int pAlmacenesID = Convert.ToInt32(cboAlmacen.EditValue);
                DateTime FechaCancelacion = Convert.ToDateTime(null);
                string strRazonCancelacion = null;
                string pObservaciones = txtObservaciones.Text;



                string strTipoRelacion = string.Empty;

                if (txtFolioacturarelacionada.Text!="0")
                {
                    strTipoRelacion=cboRelacion.EditValue.ToString();
                }
                else
                {
                    strTipoRelacion = "";
                }
                strCfdiRelacionadoUUID = "";
                if (strTipoRelacion.Length > 0)
                {
                    uuidRelacionado();
                }


                dSubTotal = Convert.ToDecimal(txtSubtotal.Text.Replace("$", ""));

                string serie = cboSerie.EditValue.ToString();


                globalCL clg = new globalCL();

                string strrutaxmltimbrado = System.Configuration.ConfigurationManager.AppSettings["pathxml"];
                strrutaxmltimbrado = strrutaxmltimbrado + Convert.ToDateTime(txtFecha.Text).Year + "\\" + clg.NombreDeMes(Convert.ToDateTime(txtFecha.Text).Month,3) + "\\" + serie + txtFolio.Text + "timbrado.xml";
                string uuid = string.Empty;
                string valor = string.Empty;
                decimal dSumaDesc = 0;
                string pLugarExpedicionCP=string.Empty;
                
                

                if (serieElectronica.Length==0)   //----- No se timbró por que se timbra con Edoc
                {
                    vsFK.vsFinkok vs = new vsFK.vsFinkok();
                    uuid = vs.ExtraeValor(strrutaxmltimbrado, "tfd:TimbreFiscalDigital", "UUID");
                    dNetoTotal = Convert.ToDecimal(vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "Total"));
                    dSubTotal = Convert.ToDecimal(vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "SubTotal"));
                    pTotalImpuestosTrasladados = Convert.ToDecimal(vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Impuestos", "TotalImpuestosTrasladados"));
                    pTotalmpuestosRetenidos = Convert.ToDecimal(vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Impuestos", "TotalImpuestosRetenidos"));
                    valor = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "Descuento");
                    if (Decimal.TryParse(valor, out dSumaDesc))
                    {

                    }
                    
                    pLugarExpedicionCP = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "LugarExpedicion");

                    
                }
                else
                {
                    uuid = "";
                    dNetoTotal = Convert.ToDecimal(txtNeto.Text.Replace("$", ""));
                    dSubTotal = Convert.ToDecimal(txtSubtotal.Text.Replace("$", ""));
                    pTotalImpuestosTrasladados = Convert.ToDecimal(txtIva.Text.Replace("$", ""));
                    dSumaDesc = 0;
                    pLugarExpedicionCP = "64810";

                    serieFac = serieElectronica;
                }

               

                dtFacturas.Rows.Add(
                    serieFac, 
                    Convert.ToInt32(txtFolio.Text), 
                    txtFecha.Text,
                    cboFormadePago.EditValue, 
                    cboCondicionesdepago.SelectedIndex,
                    dSubTotal, 
                    dSumaDesc, 
                    cboMonedas.EditValue, 
                    intParidad,
                    dNetoTotal, 
                    pc_Tipodecomprobante, 
                    cboMetododepago.EditValue,
                    pLugarExpedicionCP, 
                    pConfirmacion, 
                    strTipoRelacion,
                    strCfdiRelacionadoUUID, 
                    intClienteID, 
                    cboUsocfdi.EditValue,
                    pTotalmpuestosRetenidos, 
                    pTotalImpuestosTrasladados,
                    uuid, 
                    intAgenteID, 
                    intStatus, 
                    txtPlazo.Text,
                    FechaCancelacion, 
                    strRazonCancelacion, 
                    intUsuarioID,
                    pAlmacenesID, 
                    intExportacion, 
                    txtObservaciones.Text, 
                    txtPredial.Text, 
                    txtOrdendecompra.Text,
                    txtSeriefacturarelacionada.Text, 
                    Convert.ToInt32(txtFolioacturarelacionada.Text),
                    0,
                    intPublicoengeneral,
                    cboCanalVentas.EditValue
                );


                FacturasCL cl = new FacturasCL();

                cl.strSerie = serieFac;
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.dtm = dtFacturas;
                cl.dtd = dtFacturasDetalle;
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                cl.strMaquina = Environment.MachineName;
                cl.strOrigen = "P";
                cl.sPedidoSerie = strPedidoSerie;
                cl.intPedidoFolio = intPedidoFolio;



                string result = cl.FacturasCrud();
                
                if (result == "OK")
                {
                    //Addendas
                    if (addenda != "Ninguna" && serieElectronica.Length==00)
                    {
                        addendasCL cladd = new addendasCL();
                        switch (addenda)
                        {
                            case "6": //Casa Ley
                                result = cladd.AddendaCasaLey(serieFac, Convert.ToInt32(txtFolio.Text), Convert.ToDateTime(txtFecha.Text));
                                break;
                            case "WalMart":
                                break;
                            case "Soriana":
                                break;
                            case "Amece":
                                break;
                            case "7":
                                result = cladd.AddendaZone(serieFac, Convert.ToInt32(txtFolio.Text),  Convert.ToDateTime(txtFecha.Text));
                                break;
                        }
                    }


                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiVer.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //MessageBox.Show("Se ha generado la factura correctamente, se generará PDF");

                    if (serieElectronica.Length==0)
                        Impresion(serieFac, Convert.ToInt32(txtFolio.Text), Convert.ToDateTime(txtFecha.Text), cboMonedas.EditValue.ToString());

                    dPIva = 0;
                    dPIeps = 0;
                    PtjeRetIsr = 0;
                    PtjeRetIva = 0;
                    RetPIsr = 0;
                    RetPIva = 0;

                   
                    if (serieElectronica.Length == 0)
                    {
                        DialogResult resultD = MessageBox.Show("Guardado correctamente, desea enviarlo por correo?", "Enviar por correo", MessageBoxButtons.YesNo);
                        if (resultD.ToString() == "Yes")
                        {
                            EnviaCorreo(serieFac, Convert.ToInt32(txtFolio.Text), Convert.ToDateTime(txtFecha.Text));
                        }

                        LimpiaCajas();
                        Inicialisalista();
                        SiguienteID();
                    }
                        
                    else
                    {
                       

                        LimpiaCajas();
                        Inicialisalista();

                    }
                        
                }
                else
                {                    
                    //diccionarioErroresCL cle = new diccionarioErroresCL();
                    //cle.strmensaje = result;
                    //string strMsg = cle.mensajePropio();
                    MessageBox.Show("Error al guardar:" + result);
                }
            }
            catch (Exception ex)
            {
                int linenum = ex.LineNumber();
                MessageBox.Show("Guardar: " + ex.Message + " LN:" + linenum.ToString());
            }
        } //Guardar

        private void EnviaCorreo(string serie,int folio,DateTime fecha)
        {
            
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Enviando correo...");
                globalCL cl = new globalCL();
                string result = cl.EnviaCorreo(strTo, serie, folio, fecha, "F");
               
                MessageBox.Show(result);
                
            
        }

        private void Ver()
        {
            LimpiaCajas();
            DetalleLlenaCajas(); //primero traer el grid de detalle para no afetar los totales
            LlenaCajas();
            cboCliente.ReadOnly = true;
            txtOrdendecompra.ReadOnly = true;
            bbiVer.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            BotonesEdicion();
            blNuevo = false;
        }//ver

        private void BotonesEdicion()
        {

            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCanceladas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
   

            switch (intStatus)
            {
                case 1:
                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    break;

                case 5:
                    bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    break;
            }


            navigationFrame.SelectedPageIndex = 1;
            navBarControl.Visible = false;
            ribbonStatusBar.Visible = false;

        }//BotonesEdicion 

        private void LlenaCajas()
        {
            try
            {
                GenerarFacturasCL cl = new GenerarFacturasCL();
                cl.intFolio = intPedidoFolio;
                cl.strSerie = strPedidoSerie;
                cl.intDesglosaDescuento = 1;   //intDesglosardescuentoalfacturar;  6Nov20, se pone 1 x default
                dtFacturasDetalle = cl.FacturasLeePedidoSurtido();
                if (dtFacturasDetalle != null)
                {
                    lblCfdiVer.Text = dtFacturasDetalle.Rows[0]["CfdiVer"].ToString();

                    lblPedido.Text = "PEDIDO: " + Convert.ToString(dtFacturasDetalle.Rows[0]["Serie"])  + Convert.ToString(dtFacturasDetalle.Rows[0]["Folio"]);
                    cboSerie.EditValue = Convert.ToString(dtFacturasDetalle.Rows[0]["Serie"]);
                    cboCliente.EditValue = Convert.ToInt32(dtFacturasDetalle.Rows[0]["ClientesID"]);
                    cboCanalVentas.EditValue = Convert.ToInt32(dtFacturasDetalle.Rows[0]["CanalesdeventaID"]);

                    txtFecha.Text = DateTime.Now.ToShortDateString();

                    txtAgente.Text = dtFacturasDetalle.Rows[0]["AgentesID"].ToString();
                    intAgenteID = Convert.ToInt32(dtFacturasDetalle.Rows[0]["AgentesID"]);
                    //cboCondicionesdepago.SelectedItem = cl.strCondicionesdepago;
                    //if (cl.intExportacion == 1) { swExportacion.IsOn = true; }
                    //else { swExportacion.IsOn = false; }

                    cl.intExportacion= Convert.ToInt32(dtFacturasDetalle.Rows[0]["Exportacion"]);

                    swExportacion.IsOn = cl.intExportacion == 1 ? true : false;

                    cboMonedas.EditValue = Convert.ToString(dtFacturasDetalle.Rows[0]["MonedasID"]);
                    //txtTipodecambio.Text = cl.dTipoCambio.ToString();

                    cboFormadePago.EditValue = Convert.ToInt32(dtFacturasDetalle.Rows[0]["cFormapago"]);
                    cboMetododepago.EditValue = Convert.ToString(dtFacturasDetalle.Rows[0]["cMetodopago"]);
                    cboUsocfdi.EditValue = Convert.ToString(dtFacturasDetalle.Rows[0]["Usocfdi"]);

                    cboRelacion.EditValue = null;
                    txtFolioacturarelacionada.Text = string.Empty; // Convert.ToString(dtFacturasDetalle.Rows[0]["Folio"]);
                    txtSeriefacturarelacionada.Text = ""; // Convert.ToString(dtFacturasDetalle.Rows[0]["Serie"]);

                    txtSuma.Text = Convert.ToString(dtFacturasDetalle.Rows[0]["Subtotal"]);
                    txtRetiva.Text = Convert.ToString(dtFacturasDetalle.Rows[0]["RetIva"]);
                    // txtRetIsr.Text = Convert.ToString(dtFacturasDetalle.Rows[0]["Subtotal"]);
                    txtNeto.Text = Convert.ToString(dtFacturasDetalle.Rows[0]["TotalArticulo"]);
                    cboAlmacen.EditValue = Convert.ToInt32(dtFacturasDetalle.Rows[0]["AlmacenesID"]);

                    txtOrdendecompra.Text = dtFacturasDetalle.Rows[0]["OC"].ToString();
                    //txtPredial.Text = cl.strPredial;
                    txtDescuento.Text = Convert.ToString(dtFacturasDetalle.Rows[0]["Descuento"]);
                    txtSubtotal.Text = (Convert.ToDecimal(dtFacturasDetalle.Rows[0]["Subtotal"])-Convert.ToDecimal(dtFacturasDetalle.Rows[0]["Descuento"])).ToString();

                    txtPdescto.Text = ((Convert.ToDecimal(dtFacturasDetalle.Rows[0]["Descuento"]) / Convert.ToDecimal(dtFacturasDetalle.Rows[0]["Subtotal"]))*100).ToString();
                    txtIva.Text = Convert.ToString(dtFacturasDetalle.Rows[0]["Iva"]);
                    txtRetiva.Text = Convert.ToString(dtFacturasDetalle.Rows[0]["RetIva"]);
                    txtObservaciones.Text = string.Empty;

                    gridControlDetalle.DataSource = dtFacturasDetalle;
                    Totales();

                    //Se verifica si hay CCE
                    PedidosComercioExteriorCL clce = new PedidosComercioExteriorCL();
                    clce.strSerie = strPedidoSerie;
                    clce.intFolio = intPedidoFolio;
                    string result = clce.PedidosComercioExteriorLlenaCajas();
                    if (result == "OK")
                    {
                        blnCCE = true;
                        lblCCE.Visible = true;
                    }
                    else
                    {
                        blnCCE = false;
                        lblCCE.Visible = false;
                    }

                    intPublicoengeneral = Convert.ToInt32(dtFacturasDetalle.Rows[0]["Publicoengeneral"]);
                   

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pedidosleer: " + ex);
            }
        }//llenacajas

        private void DetalleLlenaCajas()
        {
            try
            {
                GenerarFacturasCL cl = new GenerarFacturasCL();
                cl.intFolio = intPedidoFolio;
                cl.strSerie = strPedidoSerie;
                gridControlDetalle.DataSource = cl.FacturasLeePedidoSurtido();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }


        }//detalleLlenacajas

        private void AceptarCambio()
        {
            DialogResult result = MessageBox.Show("Desea " + strStatus + " el Folio: " + intPedidoFolio.ToString(), strStatus, MessageBoxButtons.YesNo);
            if (result.ToString() == "Yes")
            {
                CambiarStatus();
            }
        }//AceptarCambio 

        private void CambiarStatus()
        {
           
        }//cambiarstatus     

        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }

        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intPedidoFolio == 0)
            { MessageBox.Show("Seleccione un pedido"); }
            else
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Cargando información del pedido", "espere por favor");
                Facturar();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        private void cboCliente_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                object orow = cboCliente.Properties.GetDataSourceRowByKeyValue(cboCliente.EditValue);
                if (orow != null)
                {
                    intClienteID = Convert.ToInt32(((DataRowView)orow)["Clave"]);
                    intDesglosardescuentoalfacturar = Convert.ToInt32(((DataRowView)orow)["Desglosardescuentoalfacturar"]); 
                    addenda = ((DataRowView)orow)["Addenda"].ToString();
                    serieElectronica = ((DataRowView)orow)["SerieEle"].ToString();
                    cboCanalVentas.EditValue = Convert.ToInt32(((DataRowView)orow)["CanalesdeventaID"]);
                    if (serieElectronica.Length > 0)
                    {
                        lblSerieEle.Text = serieElectronica;
                        txtFolio.Enabled = true;
                        txtFolio.Text = string.Empty;
                        txtFolio.ReadOnly = false;
                        lblSerieEle.Visible = true;
                    }
                    else
                    {
                        lblSerieEle.Text = "";
                        txtFolio.Enabled = false;
                        lblSerieEle.Visible = false;
                        txtFolio.ReadOnly = true;
                    }
                }
                    //proceso de DE para obtener valor del combo
                    //DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;

                //DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

                //Object value = row["Clave"];

                //intClienteID = Convert.ToInt32(value);
                //intDesglosardescuentoalfacturar = Convert.ToInt32(row["Desglosardescuentoalfacturar"]);
                //addenda = row["Addenda"].ToString();
                
            }

            catch (Exception ex)

            {
                //MessageBox.Show("cboFamilias: " + ex.Message);
            }

            clientesCL cl = new clientesCL();

            cl.intClientesID = intClienteID;
            string result = cl.ClientesLlenaCajas();
            if (result == "OK")
            {
                //txtAgente.Text = cl.strAgente;
                //intAgenteID = cl.intAgentesID;


                if (cl.intPlazo > 0)
                {
                    cboCondicionesdepago.SelectedIndex = 1;
             
                }
                else
                {
                    cboCondicionesdepago.SelectedIndex = 2;
              
                }

               
                cboFormadePago.EditValue = cl.strcFormapago;
                cboMetododepago.EditValue = cl.strcMetodopago;
                cboUsocfdi.EditValue = cl.strUsocfdi;
                txtPlazo.Text = cl.intPlazo.ToString();

                dPIva = cl.intPIva;
                dPIeps = cl.intPIeps;
                PtjeRetIva = cl.intPRetiva;
                PtjeRetIsr = cl.intPRetisr;
                intplazo = cl.intPlazo;
                strTo = cl.strEMail;

            }
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                //Sacamos datos del artículo
                if (e.Column.Name == "gridColumnArticulo")
                {
                    articulosCL cl = new articulosCL();
                    string art = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulo").ToString();
                    if (art.Length > 0) //validamos que haya algo en la celda
                    {
                        cl.intArticulosID = Convert.ToInt32(art);
                        string result = cl.articulosLlenaCajas();
                        if (result == "OK")
                        {
                            gridViewDetalle.SetFocusedRowCellValue("Des", cl.strNombre);
                            gridViewDetalle.SetFocusedRowCellValue("Um", cl.strUM);
                            gridViewDetalle.SetFocusedRowCellValue("Piva", dPIva);
                            gridViewDetalle.SetFocusedRowCellValue("Pieps", cl.intPtjeIeps);

                            //gridViewDetalle.SetFocusedRowCellValue("ClaveProdServ", cl.strClaveSat);
                            //gridViewDetalle.SetFocusedRowCellValue("c_ClaveUnidad", cl.strUMclavesat);



                        }
                    }
                }

                //Calculamos el importe multiplicando la cantidad por el precio
                if (e.Column.Name == "gridColumnPrecio" || e.Column.Name == "gridColumnCantidad" || e.Column.Name == "gridColumnPtjeDescto")
                {
                    decimal cant = 0;
                    decimal pu = 0;
                    decimal imp = 0;
                    decimal iva = 0;
                    decimal piva = 0;
                    decimal pieps = 0;
                    decimal ieps = 0;
                    decimal NETO = 0;

                    //Extraemos el valor del grid, celda Cantidad
                    string valor = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Cantidad").ToString();
                    bool success = Decimal.TryParse(valor, out cant);  //Usamos tryParse por si la cantidad no es numerica

                    //Extraemos el valor del grid, celda Precio
                    if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Precio") == null)
                    {
                        valor = "0";
                    }
                    else
                    {
                        valor = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Precio").ToString();
                    }
                    success = Decimal.TryParse(valor, out pu);

                    //Extraemos el valor del grid, celda % iva
                    if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Piva") == null)
                    {
                        valor = "0";
                    }
                    else
                    {
                        valor = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Piva").ToString();
                    }
                    success = Decimal.TryParse(valor, out piva);

                    if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Pieps") == null)
                    {
                        valor = "0";
                    }
                    else
                    {
                        valor = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Pieps").ToString();
                    }
                    success = Decimal.TryParse(valor, out pieps);

                    imp = Math.Round(cant * pu, 2); //Calculamos el importe y lo redondeamos a dos decimales
                    iva = Math.Round(imp * (dPIva / 100), 2); //IVA SALE DE CLIENTE *dPIva* PROVIENE DE CLIENTE
                    ieps = Math.Round(imp * (pieps / 100), 2); // IEPS SALE DE ARTICULO 
                    NETO = iva + ieps + imp;
                    gridViewDetalle.SetFocusedRowCellValue("Importe", imp); //Le ponemos el valor a la celda Importe del grid
                    gridViewDetalle.SetFocusedRowCellValue("Iva", iva); //Le ponemos el valor a la celda Iva del grid
                    gridViewDetalle.SetFocusedRowCellValue("Neto", NETO); //Le ponemos el valor a la celda neto del grid  
                    gridViewDetalle.SetFocusedRowCellValue("TotalArticulo", NETO); //Le ponemos el valor a la celda neto del grid
                    gridViewDetalle.SetFocusedRowCellValue("IEPS", ieps);
                    CalculaDescuento(gridViewDetalle.FocusedRowHandle);



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GridviewDetalle Changed: " + ex);
            }
        }

        private void Totales()
        {
            try
            {
                decimal importe = 0;
                decimal iva = 0;
                decimal iepsNeto = 0;
                decimal neto = 0;
                decimal descuento = 0;

                for (int i = 0; i <= gridViewDetalle.RowCount-1; i++)
                {

                    importe = importe + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importe"));
                    iva = iva + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Iva"));
                    neto = neto + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "TotalArticulo")); //PREGNTAR POR NETO SOLO SUMA IVA Y PAS DE IEPS
                    descuento = descuento + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Descuento"));
                    iepsNeto = iepsNeto + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "IEPS"));

                }

                RetPIva = Math.Round((importe - descuento) * (PtjeRetIva / 100), 2);
                RetPIsr = Math.Round((importe - descuento) * (PtjeRetIsr / 100), 2);
                subtotal = (importe - descuento);
                neto = (((subtotal + iva + iepsNeto) - RetPIva) - RetPIsr);

                txtSuma.Text = importe.ToString();
                txtDescuento.Text = descuento.ToString();

                txtSubtotal.Text = (importe - descuento).ToString();
                txtIva.Text = iva.ToString();
                txtIeps.Text = iepsNeto.ToString();
                txtRetiva.Text = RetPIva.ToString();
                txtRetIsr.Text = RetPIsr.ToString();
                txtNeto.Text = neto.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Totales: " + ex);
            }
        }

        private void labelControl14_Click(object sender, EventArgs e)
        {

        }

        private void gridViewDetalle_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Totales();
        }

        private void CalculaDescuento(int renglon)
        {
            try
            {
                decimal porcentaje = 0;
                decimal descuento = 0;
                decimal importe = 0;
                decimal neto = 0;
                decimal piva = 0;
                decimal pipes = 0;
                decimal iva = 0;
                decimal ieps = 0;

                porcentaje = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(renglon, "Pdescuento"));
                importe = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(renglon, "Importe"));
                piva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(renglon, "Piva"));
                pipes = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(renglon, "Pieps"));
                descuento = Math.Round(importe * (porcentaje / 100), 2);
                iva = Math.Round((importe - descuento) * (piva / 100), 2);
                ieps = Math.Round((importe - descuento) * (pipes / 100), 2);
                neto = Math.Round(importe - descuento + iva + ieps, 2);

                gridViewDetalle.FocusedRowHandle = renglon;

                gridViewDetalle.SetFocusedRowCellValue("Iva", iva); //Le ponemos el valor a la celda Iva del grid
                gridViewDetalle.SetFocusedRowCellValue("IEPS", ieps); //Le ponemos el valor a la celda Iva del grid
                gridViewDetalle.SetFocusedRowCellValue("TotalArticulo", neto); //Le ponemos el valor a la celda neto del grid
                gridViewDetalle.SetFocusedRowCellValue("Descuento", descuento); //Le ponemos el valor a la celda descuento del grid
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AplicarDescuento()
        {
            try
            {
                decimal Porcentajedescuento = Convert.ToDecimal(txtPdescto.Text);

                for (int i = 0; i < gridViewDetalle.RowCount; i++)
                {
                    gridViewDetalle.FocusedRowHandle = i;
                    gridViewDetalle.SetFocusedRowCellValue("Pdescuento", Porcentajedescuento); //Le ponemos el valor a la celda descuento del grid
                    //CalculaDescuento(i);
                }
                Totales();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            AplicarDescuento();
        }

        private void cboMonedas_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //proceso de DE para obtener valor del combo
                DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;

                DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

                Object value = row["Clave"];
                string valor = value.ToString();

                if (valor == "MXN")
                {
                    txtTipodecambio.Text = "1";
                    txtTipodecambio.ReadOnly = true;
                }
                else { txtTipodecambio.Text = "0"; txtTipodecambio.ReadOnly = false; }

            }

            catch (Exception ex)

            {

                //MessageBox.Show("cboFamilias: " + ex.Message);

            }
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Facturación","Timbrando xml y generando PDF");
            Guardar();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            botonesRegresar();


        }

        private void botonesRegresar()
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Cargando grid principal", "espere por favor");
            navigationFrame.SelectedPageIndex = 0;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiFacturar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiPagarsinfactura.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVer.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            ribbonPageGroup1.Visible = true;
            navBarControl.Visible = true;
            cboCliente.ReadOnly = false;
            navBarControl.Visible = true;
            ribbonStatusBar.Visible = true;

            LlenarGrid();


            intPedidoFolio = 0;
            strPedidoSerie = null;

            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPage1.Text);

        
            try
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
            catch (Exception)
            {

            }

            
        }


        private void btnVer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intPedidoFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Ver();
            }
        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            string strStatus = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Estado"));
            intPedidoFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strPedidoSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            intStatus = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status"));
         

            if (strStatus == "Registrada")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }

        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
               // GridView view = (GridView)sender;
                //string status = view.GetRowCellValue(e.RowHandle, "Estado").ToString();

                //if (status == "Cancelada")
                //{
                //    e.Appearance.ForeColor = Color.Red;
                //}

            }
            catch (Exception ex)
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intPedidoFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                strStatus = "Cancelar";
                ribbonPageGroup2.Visible = true;
                txtRazon.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
        }

        private void bbiCerrarCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strStatus = string.Empty;
            ribbonPageGroup2.Visible = false;
            strPedidoSerie = null;
            intPedidoFolio = 0;
        }

        private void bbiProcederCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (strStatus == "Cancelar")
            {
                if (RazonCan == null)
                {
                    MessageBox.Show("la razon de cancelacion no puede ir vacio");
                }
                else
                {
                    AceptarCambio();
                }

            }
            if (strStatus == "Pagarsinfactura")
            {
                AceptarCambio();
            }
            else if (strStatus == "Facturar")
            {
                AceptarCambio();
            }
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridGeneraFacturas" +
                "";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            //Detalle

            clg.strGridLayout = "gridGeneraFacturasdetalle";
            result = clg.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            this.Close();
        }

      
        #region Filtros de bbi Meses
        private void navBarItemEne_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = $"GetMonth([FechaSurtido]) = 1";
        }

        private void navBarItemFeb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = $"GetMonth([FechaSurtido]) = 2";
        }

        private void navBarItemMar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = $"GetMonth([FechaSurtido]) = 3";
        }

        private void navBarItemAbr_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = $"GetMonth([FechaSurtido]) = 4";
        }

        private void navBarItemMay_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = $"GetMonth([FechaSurtido]) = 5";
        }

        private void navBarItemJun_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = $"GetMonth([FechaSurtido]) = 6";
        }

        private void navBarItemJul_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = $"GetMonth([FechaSurtido]) = 7";
        }

        private void navBarItemAgo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = $"GetMonth([FechaSurtido]) = 8";
        }

        private void navBarItemsep_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = $"GetMonth([FechaSurtido]) = 9";
        }

        private void navBarItemOct_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = $"GetMonth([FechaSurtido]) = 10";
        }

        private void navBarItemNov_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = $"GetMonth([FechaSurtido]) = 11";
        }

        private void navBarItemDic_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = $"GetMonth([FechaSurtido]) = 12";
        }

        private void navBarItemTodos_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilter.Clear();
        }

        #endregion

        #region Filtros Inferiores
        private void bbiFacturadas_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Facturada'";
        }

        private void bbiRegistradas_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Registrada'";
        }

        private void bbiPagadasinfactura_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Pagadasinfactura'";
        }

        private void bbiCanceladas_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Cancelada'";
        }

        private void bbiTodas_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilter.Clear();
        }
        #endregion

        private void bbiVista_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ShowRibbonPrintPreview();
        }   

        private void bbiIImprimir_ItemClick(object sender, ItemClickEventArgs e) { 

        
        }

        //Pasar a vsErp
        private void Impresion(string serie, int Folio, DateTime Fecha, string Moneda)
        {
            try
            {
                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                string rutaXML = string.Empty;
                string rutaPDF = string.Empty;
                string sYear = string.Empty;
                string sMes = string.Empty;

                globalCL glc = new globalCL();

                sYear = Fecha.Year.ToString();
                sMes = glc.NombreDeMes(Fecha.Month,3);


                rutaXML = ConfigurationManager.AppSettings["pathxml"].ToString() + "\\" + sYear + "\\" + sMes + "\\" + serie + Folio + "timbrado.xml";
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
                rutaPDF = rutaPDF + serie + Folio + "timbrado.pdf";

                vs.RutaXmlTimbrado = rutaXML;
                vs.RutaPdfTimbrado = rutaPDF;


                vs.Moneda33 = Moneda;
                vs.ArchivoTcr = "Facturadiexsa.vx25";
                string pRutaTcr;
                pRutaTcr = ConfigurationManager.AppSettings["pathtcr"].ToString();
                vs.rutaQR = vs.RutaPdfTimbrado.Substring(0, vs.RutaPdfTimbrado.Length - 4) + ".jpg";
                vs.QRCodebar(vs.RutaXmlTimbrado, vs.rutaQR);

                vs.RutaTcr = pRutaTcr;
                string result = string.Empty;
                if (blNuevo)
                {


                    DatosdecontrolCL cld = new DatosdecontrolCL();
                    result = cld.DatosdecontrolLeer();
                    if (result == "OK")
                    {
                        if (cld.iVistapreviacfdi == 1)
                        {
                            vs.VistaPrevia = "S";
                        }
                        else
                        {
                            vs.VistaPrevia = "N";
                        }
                    }
                    else
                    {
                        vs.VistaPrevia = "S";
                    }
                }
                else
                {
                    vs.VistaPrevia = "S";
                }


                vs.ImpresoraNombre = "";
                vs.Copias = "1";
                vs.Moneda33 = Moneda;
                vs.Cliente = Convert.ToString(intClienteID);
                vs.Leyenda1 = ".";

                vs.CampoExtra2 = txtObservaciones.Text;



                string CliDatos = string.Empty;
                cfdiCL cli = new cfdiCL();
                cli.pCliente = intClienteID;
                result = cli.DatosReceptor();
                if (result == "OK")
                {
                    CliDatos = cli.pReceptorDireccion + System.Environment.NewLine;
                    CliDatos = CliDatos + cli.pReceptorCiudad + System.Environment.NewLine;
                    CliDatos = CliDatos + "CP: " + cli.pReceptorCP;
                }
                else
                {
                    CliDatos = "";
                }

                vs.CampoExtra4 = CliDatos;


                string EmpDatos = string.Empty;
                globalCL cl = new globalCL();
                cl.intEmpresaID = 1;  //Por lo pronto todas seran #1
                result = cl.EmpresaleeDatos();
                if (result == "OK")
                {
                    EmpDatos = cl.strEmpresaDir + System.Environment.NewLine;
                    EmpDatos = EmpDatos + cl.strEmpresaTel + System.Environment.NewLine;
                    EmpDatos = EmpDatos + cl.strEmpresaWWW;
                }
                else
                {
                    EmpDatos = "";
                }

                string MetodoPago = string.Empty;
                globalCL clg = new globalCL();
                clg.strTabla = "Uso";
                clg.strClave = struso;
                result = clg.CatalogoSAT_LeeNombre();
                if (result == "OK")
                {
                    vs.CampoExtra8 = clg.strDes;
                }
                else
                {
                    vs.CampoExtra8 = struso;
                }

                clg.strTabla = "MP";
                clg.strClave = strmp;
                result = clg.CatalogoSAT_LeeNombre();
                if (result == "OK")
                {
                    vs.CampoExtra6 = clg.strDes;
                }
                else
                {
                    vs.CampoExtra6 = strmp;
                }

                clg.strTabla = "FP";
                clg.strClave = strfp;
                result = clg.CatalogoSAT_LeeNombre();
                if (result == "OK")
                {
                    vs.CampoExtra7 = clg.strDes;
                }
                else
                {
                    vs.CampoExtra7 = strfp;
                }

                vs.CampoExtra3 = "Orden de compra:" + txtOrdendecompra.Text;
                vs.CampoExtra9 = "Tipo de comprobante";

                vs.CampoExtra10 = EmpDatos;
                vs.CampoExtra7 = "PEDIDO: " + strPedidoSerie + intPedidoFolio.ToString();


                //if (strVerCfdi == "3.3")
                //    result = vs.ImprimeFormatoTagCode();
                //else
                //{
                //vs.ReferenciaLibre = "PEDIDO: " + strPedidoSerie + intPedidoFolio.ToString() + " | ORDEN DE COMPRA: " + txtOrdendecompra.Text;
                vs.OC = "OC: " + txtOrdendecompra.Text;
                vs.Pedido = "PEDIDO: " + strPedidoSerie + intPedidoFolio.ToString();
                vs.EmpresaOtros = EmpDatos;
                    vs.EmpresaLogo = ConfigurationManager.AppSettings["logoEmpresa"].ToString();
                    vs.VistaPrevia = "SI";
                    vs.SCEObservaciones = txtObservaciones.Text;
                    result = vs.generaPDF();
                    if (result == "OK")
                    {
                        //MessageBox.Show("Start: " + rutaPDF);
                        //System.Diagnostics.Process.Start(rutaPDF);
                        BotonesEdicion();
                        navigationFrame.SelectedPageIndex = 5;
                        pdfRibbonPage1.Visible = true;
                        ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(pdfRibbonPage1.Text);
                        this.pdfViewer1.LoadDocument(rutaPDF);
                    }
                    else
                        MessageBox.Show("Al generar pdf:" + result);
                //}
             

            }
            catch (Exception ex)
            {
                MessageBox.Show("Impresión: " + ex.Message);
            }
        }

        

        private void gridControlPrincipal_Click(object sender, EventArgs e)
        {

        }

        
        private void GeneraAddendaCasaLey(string serie,int folio,DateTime fecha)
        {
            globalCL clg = new globalCL();
            //string result = clg.AddendaZone(serie, folio,fecha,intPedidoFolio);
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void cboSerie_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lblSerieEle_Click(object sender, EventArgs e)
        {

        }

        private void labelControl18_Click(object sender, EventArgs e)
        {

        }

        private void txtFolio_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }

        private void txtFecha_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lblPedido_Click(object sender, EventArgs e)
        {

        }

        private void bbiRegresaPdf_ItemClick(object sender, ItemClickEventArgs e)
        {
            botonesRegresar();
        }

        private void swNoTimbrar_Toggled(object sender, EventArgs e)
        {
            if (swNoTimbrar.IsOn)
            {
                txtFolio.Enabled = true;
                txtFolio.ReadOnly = false;
            }
            else 
            { 
                txtFolio.Enabled = false; 
                txtFolio.ReadOnly = true;
            }  
        }
    }
}