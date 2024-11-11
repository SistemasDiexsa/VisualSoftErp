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
using System.Configuration;
using System.Text.RegularExpressions;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using VisualSoftErp.Clases.HerrramientasCLs;
using VisualSoftErp.Operacion.Ventas.Clases;
using VisualSoftErp.Operacion.Ventas.Designers;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Herramientas.Clases;
using DevExpress.XtraNavBar;
using DevExpress.CodeParser;

namespace VisualSoftErp.Catalogos.Ventas
{
    public partial class Facturas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intUsuarioID = globalCL.gv_UsuarioID;

        string strNombreCliente;        
        string strTo = string.Empty;
        string sMoneda;
        DateTime dFecha;

        string RazonCan;
        int intFolio;
        string strSerie;
        string struso;
        string strmp;
        string strfp;
        string strAddenda;
        string strOC;
        string strPedido;

        int AñoFiltro;
        int MesFiltro;

        public BindingList<detalleCL> detalle;
        int intClienteID;
        int intAgenteID;
        int intplazo;
        int intLista;
        int intExportacion=0;
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
        System.Data.DataTable dtFacturasDetalle = new System.Data.DataTable("FacturasDetalle");

        string strTipoRelacion;
        int intPagina;

        public Facturas()
        {
            InitializeComponent();

            

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Facturas";

            

            txtFecha.Text = DateTime.Now.ToShortDateString();

            AgregaAñosNavBar();

            CargaCombos();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;
            intPagina = 1;
            LlenarGrid(AñoFiltro, MesFiltro);

            pdfRibbonPage1.Visible = false;

        }

        private void AgregaAñosNavBar()
        {
            try
            {
                globalCL cl = new globalCL();
                System.Data.DataTable dt = new System.Data.DataTable("Años");
                dt.Columns.Add("Año", Type.GetType("System.Int32"));
                cl.strTabla = "Pedidos";
                dt = cl.NavbarAños();

                NavBarItem item1 = new NavBarItem();


                foreach (DataRow dr in dt.Rows)
                {
                    item1.Caption = dr["Año"].ToString();
                    item1.Name = dr["Año"].ToString();
                    navBarGroupAño.ItemLinks.Add(item1);
                    item1 = new NavBarItem();

                }

                navBarControl.ActiveGroup = navBarControl.Groups[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show("AgregaAñosNavBar:" + ex.Message);
            }
        }

        private string ValidaAmbienteSat()
        {
            try
            {
                //XSLT
                try
                {
                    string dato = ConfigurationManager.AppSettings["pathxslt"].ToString();
                    if (!File.Exists(dato))
                    {
                        return "No se encuentra el archivo: " + dato;
                    }

                }
                catch(Exception)
                {
                    return "Revise que exista en AppSettings la variable pathxslt";
                }

                //XML               
                try
                {
                    string dato = ConfigurationManager.AppSettings["pathxml"].ToString();
                    if (!Directory.Exists(dato))
                    {
                        return "No se encuentra la carpeta: " + dato;
                    }

                }
                catch (Exception)
                {
                    return "Revise que exista en AppSettings la variable pathxml";
                }
                //PDF              
                try
                {
                    string dato = ConfigurationManager.AppSettings["pathpdf"].ToString();
                    if (!Directory.Exists(dato))
                    {
                        return "No se encuentra la carpeta: " + dato;
                    }

                }
                catch (Exception)
                {
                    return "Revise que exista en AppSettings la variable pathpdf";
                }
                //CER              
                try
                {
                    string dato = ConfigurationManager.AppSettings["pathcer"].ToString();
                    if (!File.Exists(dato))
                    {
                        return "No se encuentra el certificado: " + dato;
                    }

                }
                catch (Exception)
                {
                    return "Revise que exista en AppSettings la variable pathcer";
                }
                //KEY            
                try
                {
                    string dato = ConfigurationManager.AppSettings["pathkey"].ToString();
                    if (!File.Exists(dato))
                    {
                        return "No se encuentra el archivo de la llave privada: " + dato;
                    }

                }
                catch (Exception)
                {

                    return "Revise que exista en AppSettings la variable pathkey";
                }
                //KEY            
                try
                {
                    string dato = ConfigurationManager.AppSettings["pathkey"].ToString();
                    if (!File.Exists(dato))
                    {
                        return "No se encuentra el archivo de la llave privada: " + dato;
                    }

                }
                catch (Exception)
                {
                    return "Revise que exista en AppSettings la variable pathkey";
                }
                //TCR            
                try
                {
                    string dato = ConfigurationManager.AppSettings["pathtcr"].ToString();
                    dato = dato + "Facturadiexsa.vx25";
                    if (!File.Exists(dato))
                    {
                        return "No se encuentra el archivo de impresion: " + dato;
                    }

                }
                catch (Exception)
                {
                    return "Revise que exista en AppSettings la variable pathkey";
                }
                //OPEN SSL         
                try
                {
                    string dato = ConfigurationManager.AppSettings["pathopenssl"].ToString();
                    if (!Directory.Exists(dato))
                    {
                        return "No se encuentra el folder: " + dato;
                    }

                }
                catch (Exception)
                {
                    return "Revise que exista en AppSettings la variable pathopenssl";
                }

                return "OK";
            }
            catch(Exception ex)
            {
                return "ValidaAmbienteSat: " + ex.Message;
            }
        }

        private void LlenarGrid(int Año, int Mes)///gridprincipal
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Cargando datos...");
            //CrearIndicesPaginas();

            FacturasCL cl = new FacturasCL();
            cl.intAño = Año;
            cl.intMes = Mes;
            //cl.intPagina = intPagina;
            gridControlPrincipal.DataSource = cl.FacturasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridFacturas";
            clg.restoreLayout(gridViewPrincipal);

            string strMes = clg.NombreDeMes(Mes);
            gridViewPrincipal.ViewCaption = "Facturas del " + Año.ToString() + " de " + strMes;
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            // AdjustPaginationLinks();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

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
            cboCliente.Properties.Columns["Listadeprecios"].Visible = false;
            cboCliente.Properties.Columns["Exportar"].Visible = false;
            cboCliente.Properties.Columns["cFormapago"].Visible = false;
            cboCliente.Properties.Columns["cMetodopago"].Visible = false;
            cboCliente.Properties.Columns["UsoCfdi"].Visible = false;
            cboCliente.Properties.Columns["PIva"].Visible = false;
            cboCliente.Properties.Columns["PIeps"].Visible = false;
            cboCliente.Properties.Columns["PRetiva"].Visible = false;
            cboCliente.Properties.Columns["PRetIsr"].Visible = false;
            cboCliente.Properties.Columns["EMail"].Visible = false;
            cboCliente.Properties.Columns["BancoordenanteID"].Visible = false;
            cboCliente.Properties.Columns["Cuentaordenante"].Visible = false;
            cboCliente.Properties.Columns["cFormapagoDepositos"].Visible = false;
            cboCliente.Properties.Columns["Moneda"].Visible = false;
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

            cl.strTabla = "Agentes";
            cboAgente.Properties.ValueMember = "Clave";
            cboAgente.Properties.DisplayMember = "Des";
            cboAgente.Properties.DataSource = cl.CargaCombos();
            cboAgente.Properties.ForceInitialize();
            cboAgente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgente.Properties.PopulateColumns();
            cboAgente.Properties.Columns["Clave"].Visible = false;
            cboAgente.EditValue = cboMonedas.Properties.GetDataSourceValue(cboMonedas.Properties.ValueMember, 0);

            //cl.strTabla = "Motivonoaprobacion";

            //repositoryItemLookUpEditNoAprobacion.ValueMember = "Clave";
            //repositoryItemLookUpEditNoAprobacion.DisplayMember = "Des";
            //repositoryItemLookUpEditNoAprobacion.DataSource = cl.CargaCombos();
            //repositoryItemLookUpEditNoAprobacion.ForceInitialize();
            //repositoryItemLookUpEditNoAprobacion.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            //repositoryItemLookUpEditNoAprobacion.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

            cl.strTabla = "SerieI";
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

        }

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
            gridColumnImporte.OptionsColumn.ReadOnly = true;
            gridColumnImporte.OptionsColumn.AllowFocus = false;

            gridColumnIva.OptionsColumn.ReadOnly = true;
            gridColumnIva.OptionsColumn.AllowFocus = false;

            gridColumnNeto.OptionsColumn.ReadOnly = true;
            gridColumnNeto.OptionsColumn.AllowFocus = false;

            gridColumnUm.OptionsColumn.ReadOnly = true;
            gridColumnUm.OptionsColumn.AllowFocus = false;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridFacturasdetalle";
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
        }

        private void Nuevo()
        {
            Inicialisalista();
            LimpiaCajas();
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
            bbiEnviarporcorreo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVerificar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiPrefactura.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navBarControl.Visible = false;
            ribbonStatusBar.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
            //CargaCombos();
            blNuevo = true;
            cboCliente.ReadOnly = false;
            txtOrdendecompra.ReadOnly = false;
            SiguienteID();

            //Se sugiere pesos, pero quizas luego dependa mejor de la moneda que pudieramos capturar en clientes
            cboMonedas.ItemIndex = 0;
            txtTipodecambio.Text = "1";
        }//Nuevo

        private void LimpiaCajas()
        {
            cboCliente.EditValue = null;
            cboAgente.EditValue = null;
            txtOrdendecompra.Text = string.Empty;
            cboCondicionesdepago.SelectedItem = 0;
            swExportacion.IsOn = false;
            cboMonedas.EditValue = null;
            txtTipodecambio.Text = string.Empty;
            cboSerie.EditValue = null;
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
            txtReiva.Text = string.Empty;
            txtRetIsr.Text = string.Empty;
            txtNeto.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
          


        }//LimpiaCajas

        private void SiguienteID()
        {
            string serie = cboSerie.Text;
                //ConfigurationManager.AppSettings["serie"].ToString();

            FacturasCL cl = new FacturasCL();
            cl.strSerie = serie;
            cl.strDoc = "Facturas";

            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {

                cboSerie.EditValue = serie;
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
                return "El campo Serie no puede ir vacio";
            }
            if (txtFolio.Text.Length == 0)
            {
                return "El campo c_FormaPago no puede ir vacio";
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

            globalCL clg = new globalCL();


            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month, "VTA");
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";
            }


            return "OK";
        } //Valida

        private void PreFactura()
        {
            try
            {

                //dt para factura general 
                System.Data.DataTable dtFacturas = new System.Data.DataTable("Facturas");
                dtFacturas.Columns.Add("Serie", Type.GetType("System.String"));
                dtFacturas.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtFacturas.Columns.Add("c_FormaPago", Type.GetType("System.String"));
                dtFacturas.Columns.Add("Condicionesdepago", Type.GetType("System.String"));
                dtFacturas.Columns.Add("SubTotal", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("Descuentototal", Type.GetType("System.Decimal"));
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
                dtFacturas.Columns.Add("Foliofacturarelacionada", Type.GetType("System.Int32"));     //Master hasta aqui
                dtFacturas.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtFacturas.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("ClaveProdServ", Type.GetType("System.String"));
                dtFacturas.Columns.Add("c_ClaveUnidad", Type.GetType("System.String"));
                dtFacturas.Columns.Add("Unidad", Type.GetType("System.String"));
                dtFacturas.Columns.Add("Descripcion", Type.GetType("System.String"));
                dtFacturas.Columns.Add("ValorUnitario", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("Importe", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("Porcentajededescuento", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("PIva", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("PIeps", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("NumeroPedimento", Type.GetType("System.String"));
                dtFacturas.Columns.Add("UltimoCosto", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("Preciodelista", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("PRetIva", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("PRetIsr", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("RetIva", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("RetIsr", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("NombreCliente", Type.GetType("System.String"));
                dtFacturas.Columns.Add("RegimenFiscal", Type.GetType("System.String"));
                dtFacturas.Columns.Add("TotalIva", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("TotalIsr", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("TotalIeps", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("TotalRetiva", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("TotalNeto", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("TotalImporte", Type.GetType("System.Decimal"));
                dtFacturas.Columns.Add("NombreEmpresa", Type.GetType("System.String"));
                dtFacturas.Columns.Add("Importeenletra", Type.GetType("System.String"));

                //DataSet ds = new DataSet();
                //ds.Tables.Add(dtFacturas);
                //ds.WriteXmlSchema(@"C:\VisualSoftErp\Layouts\prefactura.xsd"); //save an XML schema  
                string strRegimenFiscal = string.Empty;
                string NomEmp = "";
                string pLugarExpedicionCP = string.Empty;
                ////sacar nombre de la empresa
                cfdiCL cl = new cfdiCL();
                cl.pSerie = cboSerie.EditValue.ToString();
                String Result = cl.DatosCfdiEmisor();
                if (Result == "OK")
                {
                    NomEmp = cl.pEmisorNombre;
                    strRegimenFiscal = cl.pEmisorRegimen;
                    pLugarExpedicionCP = cl.pEmisorCP;
                }
                else
                {
                    MessageBox.Show(Result);
                }

                string art;
                int intSeq;
                decimal dCantidad;
                int intArticulosID;
                string strDescripcion;
                // decimal dPrecioLista = 0;
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

                decimal intParidad = Convert.ToDecimal(txtTipodecambio.Text);
                //decimal dNetoTotal=0;
                string pc_Tipodecomprobante = "I";
                string strMetododepago = cboMetododepago.EditValue.ToString();
                
                string pConfirmacion = "Null";
                decimal pTotalmpuestosRetenidos = 0;
                decimal pTotalImpuestosTrasladados = 0;
                int intuuid = 0,
                    intAgente = intAgenteID,
                    intStatus = 1, intFolioFacturaRelacionada;

                DateTime FechaCancelacion = Convert.ToDateTime(null);
                string strRazonCancelacion = "";
                int intAlmacenes = Convert.ToInt32(cboAlmacen.EditValue);
                if (txtFolioacturarelacionada.Text == "") { intFolioFacturaRelacionada = 0; }
                else { intFolioFacturaRelacionada = Convert.ToInt32(txtFolioacturarelacionada); }

                decimal TotalSubtotal = 0, TotalIEPS = 0, TotalIVA = 0, TotalISR = 0, TotalRetIVA = 0, TotalNeto = 0;
               
                //Notas
                //Ver el procedimiento Guardar como se llena dtFacturas y copiar ese codigo aqúi y llenar las variables antede de pasarlas al DT
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

                        TotalNeto += Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "TotalArticulo"));
                        TotalSubtotal += dImporte;
                        TotalIVA += dIvaImporte;
                        TotalIEPS += dIeps;
                        TotalISR = 0;

                        //Aqui llenar todo el datatable dtfacturas que se definió arriba
                        dtFacturas.Rows.Add(
                        cboSerie.EditValue,
                        txtFolio.Text,
                        txtFecha.Text,
                    cboFormadePago.EditValue,
                    cboCondicionesdepago.SelectedIndex,
                    dSubTotal,
                    dSumaDesc,
                    cboMonedas.EditValue,
                    intParidad,
                    dNetoTotal,
                    pc_Tipodecomprobante,
                    strMetododepago,
                    pLugarExpedicionCP,
                    pConfirmacion,
                    strTipoRelacion,
                    txtFolioacturarelacionada.Text,
                    intClienteID,
                    cboUsocfdi.EditValue,
                    pTotalmpuestosRetenidos,
                    pTotalImpuestosTrasladados,
                    intuuid,
                    intAgenteID,
                    intStatus,
                    txtPlazo.Text,
                    FechaCancelacion,
                    strRazonCancelacion,
                    intUsuarioID,
                    intAlmacenes,
                    intExportacion,
                    txtObservaciones.Text,
                    txtPredial.Text,
                    txtOrdendecompra.Text,
                    txtSeriefacturarelacionada.Text,
                   intFolioFacturaRelacionada,

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
                        pValorUnitario,
                        PtjeRetIva,
                        PtjeRetIsr,
                        RetPIva,
                        RetPIsr,
                        strNombreCliente,
                        strRegimenFiscal,
                        TotalIVA,
                        TotalISR,
                        TotalIEPS,
                        TotalRetIVA,
                        TotalNeto,
                        TotalSubtotal,
                        NomEmp);


                    }
                }//for

                //Impresion
                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dtFacturas);


                // Create a report and bind it to a dataset.
                PrefacturaDesigner report = new PrefacturaDesigner();
                report.DataSource = ds1;

                // Show the print preview.
                ReportPrintTool pt = new ReportPrintTool(report);
                pt.ShowPreview();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Guardar()
        {
           
            try
            {
                //dt para factura general 
                System.Data.DataTable dtFacturas = new System.Data.DataTable("Facturas");
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
                dtFacturas.Columns.Add("CanalesdeventaID", Type.GetType("System.Int32"));

                //Extraemos totales del xml para guardar en la factura
                string serie= cboSerie.EditValue.ToString();


                string strrutaxmltimbrado = System.Configuration.ConfigurationManager.AppSettings["pathxml"];
                strrutaxmltimbrado = strrutaxmltimbrado + Convert.ToDateTime(txtFecha.Text).Year + "\\" + NombreDeMes(Convert.ToDateTime(txtFecha.Text).Month) + "\\" + serie + txtFolio.Text + "timbrado.xml";
                string uuid = string.Empty;
                string valor = string.Empty;

                vsFK.vsFinkok vs = new vsFK.vsFinkok();
                uuid = vs.ExtraeValor(strrutaxmltimbrado, "tfd:TimbreFiscalDigital", "UUID");
                decimal dNetoTotal = Convert.ToDecimal(vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "Total"));
                decimal dSubTotal = Convert.ToDecimal(vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "SubTotal"));
                decimal pTotalImpuestosTrasladados = Convert.ToDecimal(vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Impuestos", "TotalImpuestosTrasladados"));
                decimal pTotalmpuestosRetenidos = Convert.ToDecimal(vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Impuestos", "TotalImpuestosRetenidos"));
                valor = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "Descuento");

                decimal dSumaDesc = 0;
                if (Decimal.TryParse(valor, out dSumaDesc))
                {
                    //Se aigna el valor en el Out y si no ya lo asignamos en cero
                }
                string pLugarExpedicionCP;
                pLugarExpedicionCP = vs.ExtraeValor(strrutaxmltimbrado, "cfdi:Comprobante", "LugarExpedicion");

                intClienteID = Convert.ToInt32(cboCliente.EditValue);
                int intStatus = 1;
                int intAgente = intAgenteID;
                decimal intParidad = Convert.ToDecimal(txtTipodecambio.Text);
                int intUsuarioID = globalCL.gv_UsuarioID;  // Aqui cambiar ------------------------
                string strMonedasID = cboMonedas.EditValue.ToString();
                int intExportacion = swExportacion.IsOn ? 1 : 0;
                string strCotizacionSerie = cboSerie.EditValue.ToString();
                int intCotizacionFolio = 0; // Convert.ToInt32(txtFolio.Text);      //Cambiar aqui
                
                string pConfirmacion = "";
                string pc_Tipodecomprobante = "";

                int pAlmacenesID = Convert.ToInt32(cboAlmacen.EditValue);               
                DateTime FechaCancelacion = Convert.ToDateTime(null);
                string strRazonCancelacion = "";                
              
                if (txtFolioacturarelacionada.Text == string.Empty || txtFolioacturarelacionada.Text=="0")
                {
                    strTipoRelacion = "";
                    txtSeriefacturarelacionada.Text = "";
                    txtFolioacturarelacionada.Text = "0";
                }
                else
                { strTipoRelacion = cboRelacion.EditValue.ToString(); }

                pc_Tipodecomprobante = "I";

                intAgenteID = Convert.ToInt32(cboAgente.EditValue);

                dtFacturas.Rows.Add(
                    serie,              
                    txtFolio.Text,
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
                    txtFolioacturarelacionada.Text, 
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
                    txtFolioacturarelacionada.Text,
                    0,
                    cboCanalVentas.EditValue
                );

                FacturasCL cl = new FacturasCL();
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.dtm = dtFacturas;
                cl.dtd = dtFacturasDetalle;
                cl.intUsuarioID = 1;
                cl.strMaquina = Environment.MachineName;
                cl.strOrigen = "D";
                cl.sPedidoSerie = "";
                cl.intPedidoFolio = 0;
                string result = cl.FacturasCrud();                
                if (result == "OK")
                {
                    struso = cboUsocfdi.EditValue.ToString();
                    strmp = cboMetododepago.EditValue.ToString();
                    strfp = cboFormadePago.EditValue.ToString();

                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiVer.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    Impresion(serie, Convert.ToInt32(txtFolio.Text), Convert.ToDateTime(txtFecha.Text), cboMonedas.EditValue.ToString());
                    //limpiar variables universales
                    dPIva = 0;
                    dPIeps = 0;
                    PtjeRetIsr = 0;
                    PtjeRetIva = 0;
                    RetPIsr = 0;
                    RetPIva = 0;
                    Inicialisalista();
                    txtOrdendecompra.Text = "";
                    
                    txtPlazo.Text = "";
                    cboCondicionesdepago.SelectedIndex = 0;
                    txtObservaciones.Text = "";                   
                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                        
                    DatosdecontrolCL clg = new DatosdecontrolCL();
                    result = clg.DatosdecontrolLeer();
                    if (result=="OK")
                    {
                        
                        if (clg.iEnvioCfdiAuto==1)
                        {
                            MessageBox.Show("Guardada correctamente, se enviará por correo");
                            intFolio = Convert.ToInt32(txtFolio.Text);
                            strSerie= cboSerie.EditValue.ToString();
                            dFecha = Convert.ToDateTime(txtFecha.Text);         
                            EnviaCorreo();
                            intFolio = 0;
                        }
                        else
                        {
                            MessageBox.Show("Guardada correctamente");
                        }
                    } else
                    {
                        MessageBox.Show("Guardada correctamente");
                    }

                }
                else
                {
                    globalCL clg = new globalCL();
                    clg.Bitacora("Al guardar " + strSerie + intFolio.ToString() + ":" + result);
                    MessageBox.Show("Al guardar:" + result);                    
                }
            }
            catch (Exception ex)
            {
                globalCL clg = new globalCL();
                clg.Bitacora("Al guardar Exception " + strSerie + intFolio.ToString() + ":" + ex.Message);
                MessageBox.Show("Guardar: " + ex.Message);
            }

        }//Guardar

        private string NombreDeMes(int Mes)
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
            bbiEnviarporcorreo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiMovsFac.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

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
            FacturasCL cl = new FacturasCL();
            cl.intFolio = intFolio;
            cl.strSerie = strSerie;
            String Result = cl.FacturasLlenaCajas();
            if (Result == "OK")
            {
                txtFolio.Text = cl.intFolio.ToString();
                cboSerie.EditValue = cl.strSerie.ToString();
                cboCliente.EditValue = cl.intClientesID;
                txtFecha.Text = cl.fFecha.ToShortDateString();

                cboAgente.EditValue = cl.intAgentesID;
                cboCanalVentas.EditValue = cl.intCanalesdeventaID;

                cboCondicionesdepago.SelectedItem = cl.strCondicionesdepago;
                if (cl.intExportacion == 1) { swExportacion.IsOn = true; }
                else { swExportacion.IsOn = false; }
                cboMonedas.EditValue = cl.strc_Moneda;
                txtTipodecambio.Text = cl.dTipoCambio.ToString();

                cboFormadePago.EditValue = cl.strc_FormaPago;
                cboMetododepago.EditValue = cl.strc_MetodoPago;
                cboUsocfdi.EditValue = cl.strc_UsoCFDI;
                cboRelacion.EditValue = cl.strTipoRelacion;
                txtFolioacturarelacionada.Text = cl.strCfdiRelacionadoUUID;

                txtSubtotal.Text = cl.dSubTotal.ToString();
                txtReiva.Text = cl.strRetIva;
                txtRetIsr.Text = cl.strRetIsr;
                txtNeto.Text = cl.dTotal.ToString();
                cboAlmacen.EditValue = cl.intAlmacenesID;

                txtOrdendecompra.Text = cl.strOC;
                txtPredial.Text = cl.strPredial;
                txtDescuento.Text = cl.dDescuento.ToString();
                txtPdescto.Text = cl.dPorcentajededescuento.ToString();
                txtIva.Text = cl.strIVA;
                txtReiva.Text = cl.strRetIva;
                txtRetIsr.Text = cl.strRetIsr;
                txtObservaciones.Text = cl.strObservaciones;
                txtPlazo.Text = cl.intPlazo.ToString();
            }
            else
            {
                MessageBox.Show(Result);
            }
        }//llenacajas

        private void DetalleLlenaCajas()
        {
            try
            {
                FacturasCL cl = new FacturasCL();
                cl.intFolio = intFolio;
                cl.strSerie = strSerie;
                gridControlDetalle.DataSource = cl.FacturasDetalleGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }


        }//detalleLlenacajas

        private void AceptarCambio()
        {
            DialogResult result = MessageBox.Show("Desea " + strStatus + " el Folio: " + intFolio.ToString(), strStatus, MessageBoxButtons.YesNo);
            if (result.ToString() == "Yes")
            {
                CambiarStatus();
            }
        }//AceptarCambio 

        private string CancelaTimbrado(DateTime Fecha)
        {
            try
            {
                string rutaXML = string.Empty;
                string rutaPDF = string.Empty;
                string sYear = string.Empty;
                string sMes = string.Empty;
                string strresult = string.Empty;

                sYear = Fecha.Year.ToString();
                sMes = NombreDeMes(Fecha.Month);


                rutaXML = ConfigurationManager.AppSettings["pathxml"].ToString() +  sYear + "\\" + sMes + "\\" + strSerie + intFolio.ToString() + "timbrado.xml";
                if (!File.Exists(rutaXML))
                {
                    return "No existe el archivo: " + rutaXML;
                }

                string strnombrearchivo = strSerie + intFolio.ToString() + "timbrado.xml";

                vsFK.vsFinkok vs = new vsFK.vsFinkok();
               
                string pRutaCer = System.Configuration.ConfigurationManager.AppSettings["pathcer"];
                string pRutaKey = System.Configuration.ConfigurationManager.AppSettings["pathkey"];
                string pRutaOpenSSL = System.Configuration.ConfigurationManager.AppSettings["pathopenssl"];

                string rfc = vs.ExtraeValor(rutaXML, "cfdi:Emisor", "Rfc");
                if (rfc==null)
                {
                    return "No se pudo leer el Rfc del XML: " + rutaXML;
                }
                string strUUID = vs.ExtraeValor(rutaXML, "tfd:TimbreFiscalDigital", "UUID");
                if (strUUID == null)
                {
                    return "No se pudo extraer el UUID del xml: " + rutaXML;
                }

                //string strLlave = string.Empty;
                //cfdiCL clc = new cfdiCL();
                //clc.pSerie = strSerie;
                //strresult = clc.DatosCfdiEmisor();
                //if (strresult=="OK")
                //{
                //    strLlave = clc.pLlaveCFD;
                //}
                //else
                //{
                //    return "No se pudo leer cfdiEmisor";
                //}

                //if (rfc == "EKU9003173C9")
                //{
                //    strresult = vs.CancelaTimbradoDemostracion(pRutaCer, pRutaKey, pRutaOpenSSL, strLlave, rutaXML, rfc, strUUID, strnombrearchivo,txtMotivo22.Text,txtUUIDNuevo22.Text);
                //}
                //else
                //{
                //    strresult = vs.CancelaTimbrado(pRutaCer, pRutaKey, pRutaOpenSSL, strLlave, rutaXML, rfc, strUUID, strnombrearchivo, txtMotivo22.Text, txtUUIDNuevo22.Text);
                //}

                globalCL clg = new globalCL();
                strresult = clg.CancelaTimbrado(strSerie, rutaXML, strnombrearchivo, txtMotivo22.Text, txtUUIDNuevo22.Text);

                if (strresult == "Cancelacion exitosa" || strresult == "CFDI previamente cancelado")
                {
                    return "OK";
                }
                else
                {
                    return strresult;
                }   
            }
            catch(Exception ex)
            {
                return "Cancela timbrado:" + ex.Message;
            }
        }

        private void CambiarStatus()
        {
            try
            {
                globalCL clg = new globalCL();
                string result = clg.GM_CierredemodulosStatus(DateTime.Now.Year, DateTime.Now.Month, "VTA");
                if (result == "C")
                {
                    MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                    return;
                }



                result = string.Empty;
                if (strSerie.Length==0) //-- Solo cuando la serie es vacia se timbra, si trae una letra es que se timbra en EDOC 9Jun21
                {
                    result = CancelaTimbrado(dFecha);
                    if (result != "OK")
                    {
                        MessageBox.Show(result);
                        return;
                    }
                }
                

                FacturasCL cl = new FacturasCL();

                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                cl.strMaquina = Environment.MachineName;
                cl.strRazon = txtRazonNueva22.Text;
                cl.strStatus = strStatus;

                result = cl.FacturasCambiarStatus();

                if (result == "OK")
                {
                    MessageBox.Show("Movimiento " + strStatus + " se ha realizado correctamente");
                    ribbonPageGroup2.Visible = false;
                    bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    strRazon = string.Empty;
                    switch (strStatus)
                    {
                        case "Cancelar":
                            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            break;
                    }
                    LlenarGrid(AñoFiltro, MesFiltro);
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CambiarStatus: " + ex.Message);
            }

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
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Configurando detalle", "espere por favor");
            string result = ValidaAmbienteSat();
            if (result != "OK")
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                MessageBox.Show(result);               
                return;
            }

            Nuevo();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void cboCliente_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //proceso de DE para obtener valor del combo
                DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;

                DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

                Object value = row["Clave"];

                intClienteID = Convert.ToInt32(value);

                object orow = cboCliente.Properties.GetDataSourceRowByKeyValue(cboCliente.EditValue);
                if (orow != null)
                {
                    intLista = Convert.ToInt32(((DataRowView)row)["Listadeprecios"]);
                    intAgenteID = Convert.ToInt32(((DataRowView)row)["AgentesID"]);
                    cboAgente.EditValue = intAgenteID;
                    intplazo = Convert.ToInt32(((DataRowView)row)["Plazo"]);
                    intExportacion = Convert.ToInt32(((DataRowView)row)["Exportar"]);
                    cboFormadePago.EditValue = ((DataRowView)row)["cFormapago"].ToString();
                    cboMetododepago.EditValue = ((DataRowView)row)["cMetodopago"].ToString();
                    cboUsocfdi.EditValue = ((DataRowView)row)["UsoCfdi"].ToString();
                    dPIva = Convert.ToDecimal(((DataRowView)row)["PIva"]);
                    dPIeps = Convert.ToDecimal(((DataRowView)row)["PIeps"]);
                    PtjeRetIva = Convert.ToDecimal(((DataRowView)row)["PRetiva"]);
                    PtjeRetIsr = Convert.ToDecimal(((DataRowView)row)["PRetIsr"]);                    
                    strTo = ((DataRowView)row)["EMail"].ToString();
                    strNombreCliente = ((DataRowView)row)["Des"].ToString();
                    intClienteID = Convert.ToInt32(((DataRowView)row)["Clave"]);
                    cboCanalVentas.EditValue = Convert.ToInt32(((DataRowView)row)["CanalesdeventaID"]);
                }

                if (blNuevo)
                {
                    DatosdecontrolCL clg = new DatosdecontrolCL();
                    string result = clg.DatosdecontrolLeer();
                    if (result == "OK")
                    {
                        if (clg.iCambiarelagentealvender == 1)
                        {
                            cboAgente.Enabled = true;
                        }
                        else
                        {
                            cboAgente.Enabled = false;
                        }
                    }
                }
                else
                {
                    cboAgente.Enabled = false;
                }
               



                    //clientesCL cl = new clientesCL();

                    //cl.intClientesID = intClienteID;
                    //string result = cl.ClientesLlenaCajas();
                    //if (result == "OK")
                    //{
                    //txtAgente.Text = cl.strAgente;
                    //intAgenteID = cl.intAgentesID;


                    if (intplazo > 0)
                   {
                       cboCondicionesdepago.SelectedIndex = 1;
                       cboCondicionesdepago.ReadOnly = true;
                   }
                   else
                   {
                       cboCondicionesdepago.SelectedIndex = 2;
                       cboCondicionesdepago.ReadOnly = false;
                   }

                    txtPlazo.Text = intplazo.ToString();

                   swExportacion.ReadOnly = intExportacion == 1 ? true : false; 


               // if (cl.intExportar > 0) { }
               // else { swExportacion.ReadOnly = true; }
               // if (cl.intExportar == 0) { }
               // else { swExportacion.ReadOnly = false; }

                
                //txtPlazo.Text = cl.intPlazo.ToString();

                    //dPIva = cl.intPIva;
                    //dPIeps = cl.intPIeps;
                    //PtjeRetIva = cl.intPRetiva;
                    //PtjeRetIsr = cl.intPRetisr;
                    //intplazo = cl.intPlazo;
                    //strTo = cl.strEMail;

                
            }
            catch (Exception)

            {
                
            }
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                string result=string.Empty;
                //Sacamos datos del artículo
                if (e.Column.Name == "gridColumnArticulo")
                {
                    PreciosCL clp = new PreciosCL();
                    articulosCL cl = new articulosCL();
                    
                   
                    string art = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulo").ToString();
                    if (art.Length > 0) //validamos que haya algo en la celda
                    {
                        cl.intArticulosID = Convert.ToInt32(art);
                        result = cl.articulosLlenaCajas();
                        if (result == "OK")
                        {
                            gridViewDetalle.SetFocusedRowCellValue("Des", cl.strNombre);
                            gridViewDetalle.SetFocusedRowCellValue("Um", cl.strUM);
                            gridViewDetalle.SetFocusedRowCellValue("Piva", dPIva);
                            gridViewDetalle.SetFocusedRowCellValue("Pieps", cl.intPtjeIeps);

                            gridViewDetalle.SetFocusedRowCellValue("ClaveProdServ", cl.strClaveSat);
                            gridViewDetalle.SetFocusedRowCellValue("c_ClaveUnidad", cl.strUMclavesat);

                            //Se trae el precio
                            clp.strSerie = cboSerie.EditValue.ToString();
                            clp.iCliente = Convert.ToInt32(cboCliente.EditValue);
                            clp.iArtID = cl.intArticulosID;
                            clp.iLp = intLista;
                            result = clp.Precio();
                            if (result=="OK")
                            {
                                gridViewDetalle.SetFocusedRowCellValue("Precio", clp.dPrecio);
                            }

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

                for (int i = 0; i < gridViewDetalle.RowCount; i++)
                {

                    importe = importe + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importe"));
                    iva = iva + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Iva"));
                    neto = neto + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "TotalArticulo")); //PREGNTAR POR NETO SOLO SUMA IVA Y PAS DE IEPS
                    descuento = descuento + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Descuento"));
                    iepsNeto = iepsNeto + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "IEPS"));

                }

                RetPIva = Math.Round((importe-descuento) * (PtjeRetIva / 100), 2);
                RetPIsr = Math.Round((importe - descuento) * (PtjeRetIsr / 100), 2);
                subtotal = (importe - descuento);
                neto = (((subtotal + iva + iepsNeto) - RetPIva) - RetPIsr);

                txtSuma.Text = importe.ToString();
                txtDescuento.Text = descuento.ToString();

                txtSubtotal.Text = (importe - descuento).ToString();
                txtIva.Text = iva.ToString();
                txtIeps.Text = iepsNeto.ToString();
                txtReiva.Text = RetPIva.ToString();
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
                ieps= Math.Round((importe - descuento) * (pipes / 100), 2);
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

            DialogResult resultg = MessageBox.Show("Desea genera el XML y guardar la factura?", strStatus, MessageBoxButtons.YesNo);
            if (resultg.ToString() == "No")
            {
                return;
            }

            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Generando XML...","espere por favor");
            string result = Valida();
            if (result == "OK")
            {
                result = Timbrar();
                if (result == "OK")
                {
                    
                    globalCL clg = new globalCL();
                
                    for (int i=0;i<100;i++)
                    {
                        if (!clg.HayInternet())
                        {
                            DialogResult dialogResult = MessageBox.Show("Desea re-intentar? (Sí da click en NO, deberá cancelar el XML timbrado)", "Falla de internet", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.No)
                            {
                                result=clg.RenombrarXMLTimbrado(cboSerie.EditValue.ToString(),Convert.ToInt32(txtFolio.Text),Convert.ToDateTime(txtFecha.Text));
                                if (result=="OK")
                                {
                                    MessageBox.Show("Archivo renombrado correctamente, vaya a Herramientas-Cancelaciones directas y cancele");
                                    clg.Bitacora("Facturas. Se renombra XML de Folio:" + cboSerie.EditValue.ToString() + txtFolio.Text + " por falla de internet. Usuario:" + globalCL.gv_UsuarioID.ToString());
                                }
                                return;
                            }
                        }
                        else
                        {
                            break;
                        }
                       
                    }                   
                    Guardar();
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                }    
                else
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("Al timbrar: " + result, "Error al timbrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                MessageBox.Show(result, "Error en datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

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

                //dt para facturacion detalle
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

                cfdiCL cl = new cfdiCL();
                cl.pSerie = cboSerie.EditValue.ToString();
                cl.pCliente = Convert.ToInt32(cboCliente.EditValue);
                SiguienteID();
                cl.pFolio = Convert.ToInt32(txtFolio.Text);
                cl.pFormaPago = cboFormadePago.EditValue.ToString();                
                cl.pPlazo = Convert.ToInt32(txtPlazo.Text);
                cl.pMoneda = cboMonedas.EditValue.ToString();
                cl.pTipocambio = Convert.ToDecimal(txtTipodecambio.Text);
                cl.pMetodoPago = cboMetododepago.EditValue.ToString();
                
                if (cboRelacion.ItemIndex==-1)
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
                    uuidRelacionado();
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
                        cboSerie.EditValue, 
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
                

                string result = cl.GeneraCfdi33(dtFacturasDetalle,false,0,"3.3");// este es lo ultimo que se manda llamar despues de llenar los valores que ocupa la clase cfdicl
            
               
                return result;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Timbrar: " + ex.Message);
                return "Error";
            }
                
           
        }
        
        private string uuidRelacionado()
        {
            //hacer rutina para buscar xml y sacar el uuid 
            return "";
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            bbiEnviarporcorreo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVerificar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiPrefactura.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiMovsFac.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            navBarControl.Visible = true;
            cboCliente.ReadOnly = false;
            navBarControl.Visible = true;
            ribbonStatusBar.Visible = true;

            LlenarGrid(AñoFiltro, MesFiltro);


            intFolio = 0;
            strSerie = null;
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
            if (intFolio == 0)
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
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            intStatus = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status"));
            dFecha = Convert.ToDateTime(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Fecha"));
            sMoneda = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "c_Moneda").ToString();
            strTo = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "EMail").ToString();
            txtObservaciones.Text = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Observaciones").ToString();
            intClienteID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ClientesID"));
            struso = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "c_UsoCFDI").ToString();
            strmp = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "c_MetodoPago").ToString();
            strfp = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "c_FormaPago").ToString();
            strAddenda = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Addenda").ToString();
            strOC = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "OC").ToString();
            strPedido = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Pedido").ToString();


            if (strStatus == "Registrada")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                if (view.GetRowCellValue(e.RowHandle, "Estado") != null)
                {
                    string status = view.GetRowCellValue(e.RowHandle, "Estado").ToString();

                    if (status == "Cancelada")
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
                

            }
            catch (Exception ex)
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                strStatus = "Cancelar";
                //ribbonPageGroup2.Visible = true;
                //txtRazonOld.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                groupControlCan.Visible = true;
                txtMotivo22.Focus();
            }
        }

        private void bbiCerrarCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strStatus = string.Empty;
            ribbonPageGroup2.Visible = false;
            strSerie = null;
            intFolio = 0;
        }

        private void bbiProcederCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //*** No se usa 26Ene22 ***
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
            clg.strGridLayout = "gridFacturas" +
                "";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            //Detalle
            
            clg.strGridLayout = "gridFacturasdetalle";
            result = clg.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            this.Close();
        }

        private void txtRazon_EditValueChanged(object sender, EventArgs e)
        {
            BarEditItem item = sender as BarEditItem;
            object newValue = item.EditValue;
            RazonCan = newValue.ToString();
        }

        #region Filtros de bbi Meses

        private void navBarItemEne_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 1";
        }

        private void navBarItemFeb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 2";
        }

        private void navBarItemMar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 3";
        }

        private void navBarItemAbr_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 4";
        }

        private void navBarItemMay_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 5";
        }

        private void navBarItemJun_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 6";
        }

        private void navBarItemJul_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 7";
        }

        private void navBarItemAgo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 8";
        }

        private void navBarItemsep_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 9";
        }

        private void navBarItemOct_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 10";
        }

        private void navBarItemNov_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 11";
        }

        private void navBarItemDic_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 12";
        }

        private void navBarItemTodos_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilter.Clear();
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

        private void Impresion(string serie,int Folio,DateTime Fecha,string Moneda)
        {
            try
            {
                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                string rutaXML = string.Empty;
                string rutaPDF = string.Empty;
                string sYear = string.Empty;
                string sMes = string.Empty;
                string strVerCfdi = string.Empty;

                sYear = Fecha.Year.ToString();
                sMes = NombreDeMes(Fecha.Month);

                globalCL clg = new globalCL();
                string result = string.Empty;

                string pathXml = ConfigurationManager.AppSettings["pathxml"].ToString() + "\\" + sYear + "\\" + sMes + "\\";

                rutaXML = ConfigurationManager.AppSettings["pathxml"].ToString() + "\\" + sYear + "\\" + sMes + "\\" + serie + Folio + "timbrado.xml";
                if (!File.Exists(rutaXML))
                {
                    result= clg.descargaFtp(pathXml, serie + Folio + "timbrado.xml");
                    if (result != "OK")
                    {
                        MessageBox.Show("No existe el archivo: " + rutaXML);
                        return;
                    }                   
                }

                rutaPDF = ConfigurationManager.AppSettings["pathpdf"].ToString()  + sYear + @"\" + sMes + @"\";
                if (!Directory.Exists(rutaPDF))
                {
                    Directory.CreateDirectory(rutaPDF);
                }
                rutaPDF = rutaPDF + serie + Folio + ".pdf";

                vs.RutaXmlTimbrado = rutaXML;
                vs.RutaPdfTimbrado = rutaPDF;


                vs.Moneda33 = Moneda;
                if (serie == "NCA")
                    vs.ArchivoTcr = "NCARGO.vx25";
                else
                    vs.ArchivoTcr = "Facturadiexsa.vx25";
                string pRutaTcr;
                pRutaTcr = ConfigurationManager.AppSettings["pathtcr"].ToString();
                vs.rutaQR = vs.RutaPdfTimbrado.Substring(0, vs.RutaPdfTimbrado.Length - 4) + ".jpg";
                vs.QRCodebar(vs.RutaXmlTimbrado, vs.rutaQR);

                vs.RutaTcr = pRutaTcr;
               
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

                if (txtObservaciones.Text == "\r\n")
                    txtObservaciones.Text = ".";

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

                vs.CampoExtra3 = "Orden de compra:" + strOC;
                vs.CampoExtra9 = "Tipo de comprobante";

                vs.CampoExtra10 = EmpDatos;

                vs.CampoExtra7 = "PEDIDO: " + strPedido;

                strVerCfdi = vs.ExtraeValor(rutaXML, "cfdi:Comprobante", "Version");

                //if (strVerCfdi=="3.3")
                //    result= vs.ImprimeFormatoTagCode();
                //else
                //{
                //vs.ReferenciaLibre = "PEDIDO: " + strPedido + " | ORDEN DE COMPRA: " + strOC;
                vs.OC = "OC: " + strOC;
                vs.Pedido = "PEDIDO: " + strPedido;
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
                        RibbonControl1.MergeOwner.SelectedPage = RibbonControl1.MergeOwner.TotalPageCategory.GetPageByText(pdfRibbonPage1.Text);
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

        private void bbiIImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            Impresion(strSerie, intFolio, dFecha, sMoneda);
        }

        private void EnviaCorreo()
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Enviando correo...");
                globalCL cl = new globalCL();
                //cl.AbreOutlook(strSerie, intFolio, dFecha, strTo);
                string result = cl.EnviaCorreo(strTo, strSerie, intFolio, dFecha,"F");
                if (!blNuevo)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                }
                
                if (cl.iAbrirOutlook == 0)
                {
                    MessageBox.Show(result);
                }
            }
        }
        
        private void bbiEnviarporcorreo_ItemClick(object sender, ItemClickEventArgs e)
        {
            EnviaCorreo();
        }

        private void bbiVerificar_ItemClick(object sender, ItemClickEventArgs e)
        {
            globalCL cl = new globalCL();
            cl.VerificaComprobante(strSerie,intFolio,dFecha);
        }

        private void gridViewPrincipal_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void bbiPrefactura_ItemClick(object sender, ItemClickEventArgs e)
        {
            PreFactura();
        }

        private void bbiAddenda_ItemClick(object sender, ItemClickEventArgs e)
        {
    
            addendasCL cl = new addendasCL();

            if (strAddenda=="Ninguna")
            {
                MessageBox.Show("Este cliente no maneja addenda");
                return;
            }
            string result = string.Empty;
            switch (strAddenda)
            {
                case "6": //Casa Ley
                    result = cl.AddendaCasaLey(strSerie, intFolio, dFecha);
                    break;
                case "7":  //AutoZone
                    result = cl.AddendaZone(strSerie, intFolio, dFecha);
                    break;
            }

           
            MessageBox.Show(result);
        }

        private void bbiMovsFac_ItemClick(object sender, ItemClickEventArgs e)
        {
            Movimientosdeunafactura();
        }

        private void Movimientosdeunafactura()
        {
            FacturasCL cl = new FacturasCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            lblMovsFac.Text = "Movimientos de la factura: " + strSerie + intFolio.ToString();
            bbiVer.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiAddenda.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVerificar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            gridControlMovsFac.DataSource = cl.FacturasMovimientos();

           
            decimal Saldo = 0;
            for (int i = 0; i <= gridViewMovsFac.RowCount - 1; i++)
            {
                Saldo = Saldo + Convert.ToDecimal(gridViewMovsFac.GetRowCellValue(i, "Neto"));
                gridViewMovsFac.FocusedRowHandle = i;
                gridViewMovsFac.SetFocusedRowCellValue("Saldo", Saldo);
            }


            BotonesEdicion();
            navigationFrame.SelectedPageIndex = 3;

        }

        private void cboSerie_EditValueChanged(object sender, EventArgs e)
        {
            SiguienteID();
        }

        private void navBarControl_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            intPagina = 1;
            globalCL clg = new globalCL();
            string Name = e.Link.ItemName.ToString();
            if (clg.esNumerico(Name))
            {
                AñoFiltro = Convert.ToInt32(Name);
                LlenarGrid(AñoFiltro, MesFiltro);
            }
            else
            {
                switch (Name)
                {
                    case "navBarItemEne":
                        MesFiltro = 1;
                        break;
                    case "navBarItemFeb":
                        MesFiltro = 2;
                        break;
                    case "navBarItemMar":
                        MesFiltro = 3;
                        break;
                    case "navBarItemAbr":
                        MesFiltro = 4;
                        break;
                    case "navBarItemMay":
                        MesFiltro = 5;
                        break;
                    case "navBarItemJun":
                        MesFiltro = 6;
                        break;
                    case "navBarItemJul":
                        MesFiltro = 7;
                        break;
                    case "navBarItemAgo":
                        MesFiltro = 8;
                        break;
                    case "navBarItemsep":
                        MesFiltro = 9;
                        break;
                    case "navBarItemOct":
                        MesFiltro = 10;
                        break;
                    case "navBarItemNov":
                        MesFiltro = 11;
                        break;
                    case "navBarItemDic":
                        MesFiltro = 12;
                        break;
                    case "navBarItemTodos":
                        MesFiltro = 0;
                        break;
                }
                LlenarGrid(AñoFiltro, MesFiltro);

            }
        }

        private void btnCerrarCan22_Click(object sender, EventArgs e)
        {
            groupControlCan.Visible = false;
        }

        private void labelControl32_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar22_Click(object sender, EventArgs e)
        {
            if (strStatus == "Cancelar")
            {
                
                if (txtRazonNueva22.Text == null)
                {
                    MessageBox.Show("la razon de cancelacion no puede ir vacio");
                    return;
                }

                UsuariosCL clU = new UsuariosCL();
                clU.strLogin = txtLogin.Text;
                clU.strClave = txtPassword.Text;
                clU.strPermiso = "cancelarfacturas";

                string result = clU.UsuariosPermisos();
                if (result != "OK")
                {
                    MessageBox.Show(result);
                    return;
                }


                AceptarCambio();

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

        private void btnBuscaNuevoUUID_Click(object sender, EventArgs e)
        {
            globalCL clg = new globalCL();
            txtUUIDNuevo22.Text = clg.leeUUIDNuevo("F",txtSerieNueva22.Text,txtFolioNuevo22.Text,intClienteID);            
        }

        private void bbiRegresaPdf_ItemClick(object sender, ItemClickEventArgs e)
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
            bbiEnviarporcorreo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVerificar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiPrefactura.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiMovsFac.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            navBarControl.Visible = true;
            cboCliente.ReadOnly = false;
            navBarControl.Visible = true;
            ribbonStatusBar.Visible = true;
            
            RibbonControl1.MergeOwner.SelectedPage = RibbonControl1.MergeOwner.TotalPageCategory.GetPageByText(ribbonPage1.Text);

            intFolio = 0;
            strSerie = null;
            try
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
            catch (Exception)
            {

            }
        }

        #region PAGINADO

        private void btnAntPagina_Click(object sender, EventArgs e)
        {
            if (intPagina > 1)
            {
                intPagina--;
                LlenarGrid(AñoFiltro, MesFiltro);
            }
        }

        private void btnSigPagina_Click(object sender, EventArgs e)
        {
            if (gridViewPrincipal.RowCount == 100)
            {
                intPagina++;
                LlenarGrid(AñoFiltro, MesFiltro);
            }
        }

        private void CrearIndicesPaginas()
        {
            globalCL global = new globalCL();
            global.strTabla = "FacturasGRID";
            global.intAnio = AñoFiltro;
            global.intMes = MesFiltro;
            int intNumeroFilas = global.NumeroFilasGrid();

            int itemsPerPage = 500;
            int totalPages = (int)Math.Ceiling(intNumeroFilas / (double)itemsPerPage);

            flowLayoutPanelPaginas.Controls.Clear();

            for (int i = 1; i <= totalPages; i++)
            {
                LinkLabel link = new LinkLabel();
                link.Text = i.ToString();
                link.Font = new Font(link.Font.FontFamily, 12);
                link.Tag = i;
                link.Click += new EventHandler(Pagina_Click);
                flowLayoutPanelPaginas.Controls.Add(link);
            }
        }

        private void Pagina_Click(object sender, EventArgs e)
        {
            LinkLabel link = (LinkLabel)sender;
            if (intPagina != (int)link.Tag)
            {
                intPagina = (int)link.Tag;
                LlenarGrid(AñoFiltro, MesFiltro);
            }
        }

        #endregion PAGINADO
    }
}