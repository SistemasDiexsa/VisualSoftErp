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
using VisualSoftErp.Clases.VentasCLs;
using System.Configuration;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraExport.Helpers;
using VisualSoftErp.Operacion.Ventas.Designers;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;
using VisualSoftErp.Operacion.Ventas.Clases;
using VisualSoftErp.Clases.HerrramientasCLs;
using DevExpress.XtraNavBar;

namespace VisualSoftErp.Catalogos.Ventas
{
    public partial class Cotizaciones : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public BindingList<detalleCL> detalle;

        string tipoImpresion;
        string strCLIENTEoPros;
        string RazonCan;
        int intFolio;
        string strSerie;
        //variables que no tienen textbox para guardar 
        int intAgenteID;
        int intStatus;
        string strStatus;
        int intMotivoNoaprobacion;
        int intSeqHistorial;
        public bool blNuevo;
        int intClienteID;
        string gridOrigen = string.Empty;
        string strTo=string.Empty;
        string strFrom = string.Empty;
        int intLista;
        int AñoFiltro;
        int MesFiltro;

        public Cotizaciones()
        {
            InitializeComponent();

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;


            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Cotizaciones";

            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;
            AgregaAñosNavBar();
            LlenarGrid(AñoFiltro,MesFiltro);
            gridViewPrincipal.ActiveFilter.Clear();

            txtFecha.Text = DateTime.Now.ToShortDateString();

            CargaCombos();

            gridViewHistorial.OptionsBehavior.ReadOnly = true;
            gridViewHistorial.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;

            gridOrigen = "Principal";

            officeNavigationBar.SelectedItem = officeNavigationBar.Items[0];

            gridViewDetalle.OptionsView.RowAutoHeight = true;

           


                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
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
                    navBarGroupAños.ItemLinks.Add(item1);
                    item1 = new NavBarItem();

                }

                navBarControl.ActiveGroup = navBarControl.Groups[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show("AgregaAñosNavBar:" + ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            navigationFrame.SelectedPageIndex = 1;
            navBarControl.Visible = false;
            officeNavigationBar.Visible = false;
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
            clg.strGridLayout = "gridCotizacionesDetalle";
            clg.restoreLayout(gridViewDetalle);

            
        }

        public class detalleCL
        {
            public decimal Cantidad { get; set; }
            public string Articulo { get; set; }
            public string Des { get; set; }
            public string Um { get; set; }
            public decimal Precio { get; set; }
            public decimal Importe { get; set; }
            public decimal PDescto { get; set; }
            public decimal Descuento { get; set; }
            public decimal Iva { get; set; }
            public decimal Piva { get; set; }
            public decimal Pieps { get; set; }
            public decimal Neto { get; set; }
        }

        private void LlenarGrid(int año,int mes)///gridprincipal
        {
            CotizacionesCL cl = new CotizacionesCL();
            cl.intAño = AñoFiltro;
            cl.intMes = MesFiltro;

            gridControlPrincipal.DataSource = cl.CotizacionesGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCotizaciones";
            clg.restoreLayout(gridViewPrincipal);
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
            gridViewPrincipal.ViewCaption = "Cotizaciones de " + clg.NombreDeMes(MesFiltro, 0) + " del " + AñoFiltro.ToString();
        } //LlenarGrid()

        private void Nuevo()
        {

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Captura", "Preparando detalle...");
            navigationFrame.SelectedPageIndex = 1;

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
            bbiImpresionTroya.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCanceladas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEnviar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navBarControl.Visible = false;
            officeNavigationBar.Visible = false;
            
            radioGroup1.SelectedIndex = 0;
            gridOrigen = "Detalle";

            //Se obtiene el Id de la cotizacion
            blNuevo = true;
            SiguienteID();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }//Nuevo

        private void SiguienteID()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            CotizacionesCL cl = new CotizacionesCL();
            cl.strSerie = serie;
            cl.strDoc = "Cotizaciones";

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

        private void CargaCombos()
        {

            try
            {
                CargarCombosClienteoProspecto();

                combosCL cl = new combosCL();

                cl.strTabla = "Agentes";
                cboAgente.Properties.ValueMember = "Clave";
                cboAgente.Properties.DisplayMember = "Des";
                cboAgente.Properties.DataSource = cl.CargaCombos();
                cboAgente.Properties.ForceInitialize();
                cboAgente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboAgente.Properties.PopulateColumns();
                cboAgente.Properties.Columns["Clave"].Visible = false;
                cboAgente.Properties.Columns["Encabezado"].Visible = false;
                cboAgente.Properties.Columns["Piedepagina"].Visible = false;
                cboAgente.Properties.Columns["Email"].Visible = false;
                cboAgente.EditValue = cboAgente.Properties.GetDataSourceValue(cboAgente.Properties.ValueMember, 0);

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

                cl.strTabla = "Motivonoaprobacion";

                repositoryItemLookUpEditNoAprobacion.ValueMember = "Clave";
                repositoryItemLookUpEditNoAprobacion.DisplayMember = "Des";
                repositoryItemLookUpEditNoAprobacion.DataSource = cl.CargaCombos();
                repositoryItemLookUpEditNoAprobacion.ForceInitialize();
                repositoryItemLookUpEditNoAprobacion.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                repositoryItemLookUpEditNoAprobacion.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;


                cl.strTabla = "Mediosdecontacto";
                cboMediosdecontactoID.Properties.ValueMember = "Clave";
                cboMediosdecontactoID.Properties.DisplayMember = "Des";
                cboMediosdecontactoID.Properties.DataSource = cl.CargaCombos();
                cboMediosdecontactoID.Properties.ForceInitialize();
                cboMediosdecontactoID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboMediosdecontactoID.Properties.PopulateColumns();
                cboMediosdecontactoID.Properties.Columns["Clave"].Visible = false;

                cl.strTabla = "Serie";
                cboSerie.Properties.ValueMember = "Clave";
                cboSerie.Properties.DisplayMember = "Clave";
                cboSerie.Properties.DataSource = cl.CargaCombos();
                cboSerie.Properties.ForceInitialize();
                cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboSerie.Properties.PopulateColumns();
                cboSerie.Properties.Columns["Des"].Visible = false;
                cboSerie.EditValue = cboMonedas.Properties.GetDataSourceValue(cboMonedas.Properties.ValueMember, 0);
            }
            catch(Exception ex)
            {
                MessageBox.Show("CargaCombos" + ex.Message);
            }


        }//CargaCombos

        private void CargarCombosClienteoProspecto()
        {
            combosCL cl = new combosCL();
            if (strCLIENTEoPros == null)
            {
                strCLIENTEoPros = "C";
            }
            if (strCLIENTEoPros == "C")
            {
                cl.strTabla = "Clientes";
            } 
            else
            {
                cl.strTabla = "Prospecto";
            }
            cboNumero.Properties.ValueMember = "Clave";
            cboNumero.Properties.DisplayMember = "Des";
            cboNumero.Properties.DataSource = cl.CargaCombos();
            cboNumero.Properties.ForceInitialize();
            cboNumero.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboNumero.Properties.PopulateColumns();
            cboNumero.Properties.Columns["Clave"].Visible = false;
            cboNumero.Properties.Columns["AgentesID"].Visible = false;
            cboNumero.Properties.Columns["Plazo"].Visible = false;
            cboNumero.Properties.Columns["Listadeprecios"].Visible = false;
            cboNumero.Properties.Columns["Exportar"].Visible = false;
            cboNumero.Properties.Columns["cFormapago"].Visible = false;
            cboNumero.Properties.Columns["cFormapagoDepositos"].Visible = false;
            cboNumero.Properties.Columns["cMetodopago"].Visible = false;
            cboNumero.Properties.Columns["BancoordenanteID"].Visible = false;
            cboNumero.Properties.Columns["Cuentaordenante"].Visible = false;
            cboNumero.Properties.Columns["UsoCfdi"].Visible = false;
            cboNumero.Properties.Columns["PIva"].Visible = false;
            cboNumero.Properties.Columns["PIeps"].Visible = false;
            cboNumero.Properties.Columns["PRetiva"].Visible = false;
            cboNumero.Properties.Columns["PRetIsr"].Visible = false;
            cboNumero.Properties.Columns["EMail"].Visible = false;
            cboNumero.Properties.Columns["Moneda"].Visible = false;
            cboNumero.Properties.Columns["Desglosardescuentoalfacturar"].Visible = false;
            cboNumero.Properties.Columns["DescuentoBase"].Visible = false;
            cboNumero.Properties.Columns["DesctoPP"].Visible = false;




            if (strCLIENTEoPros == "C")
            {
                cboNumero.Properties.NullText = "Seleccione un cliente";
            }
            else
            {
                cboNumero.Properties.NullText = "Seleccione un prospecto";
            }
        }

        private void Guardar()
        {
           
            try
            {

                string result = Valida();
                if (result!="OK")
                {
                    MessageBox.Show(result);
                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    return;
                }

                //Declarar dataTables (son tablas en memoria)
                System.Data.DataTable dtCotizaciones = new System.Data.DataTable("Cotizaciones");
                dtCotizaciones.Columns.Add("Serie", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtCotizaciones.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtCotizaciones.Columns.Add("Tipocop", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("ClientesID", Type.GetType("System.Int32"));
                dtCotizaciones.Columns.Add("AgentesID", Type.GetType("System.Int32"));
                dtCotizaciones.Columns.Add("Tiempodeentrega", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("Condicionesdepago", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("Vigencia", Type.GetType("System.Int32"));
                dtCotizaciones.Columns.Add("Libreabordo", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("Atenciona", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("Concopiapara", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("Email", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("MonedasID", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("Encabezado", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("Piedepagina", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("Status", Type.GetType("System.Int32"));
                dtCotizaciones.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dtCotizaciones.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtCotizaciones.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dtCotizaciones.Columns.Add("Documentoasignado", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("Documentoserie", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("Documentofolio", Type.GetType("System.Int32"));
                dtCotizaciones.Columns.Add("MotivosnoaprobacioncotizacionesID", Type.GetType("System.Int32"));
                dtCotizaciones.Columns.Add("Noaprobacioncomentarios", Type.GetType("System.String"));
                dtCotizaciones.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dtCotizaciones.Columns.Add("Zonainstalacion", Type.GetType("System.String"));

                System.Data.DataTable dtCotizacionesdetalle = new System.Data.DataTable("Cotizacionesdetalle");
                dtCotizacionesdetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtCotizacionesdetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtCotizacionesdetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtCotizacionesdetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtCotizacionesdetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtCotizacionesdetalle.Columns.Add("Descripcion", Type.GetType("System.String"));
                dtCotizacionesdetalle.Columns.Add("Precio", Type.GetType("System.Decimal"));
                dtCotizacionesdetalle.Columns.Add("Importe", Type.GetType("System.Decimal"));
                dtCotizacionesdetalle.Columns.Add("IvaImporte", Type.GetType("System.Decimal"));
                dtCotizacionesdetalle.Columns.Add("IvaPorcentaje", Type.GetType("System.Decimal"));
                dtCotizacionesdetalle.Columns.Add("PDescto", Type.GetType("System.Decimal"));
                dtCotizacionesdetalle.Columns.Add("Descuento", Type.GetType("System.Decimal"));

                string art;
                int intSeq;
                decimal dCantidad;
                int intArticulosID;
                string strDescripcion;
                decimal dPrecio;
                decimal dImporte;
                decimal dIvaImporte;
                decimal dIvaPorcentaje;
                decimal dSubTotal = 0;
                decimal dIvaTotal = 0;
                decimal dNetoTotal = 0;
                decimal dTDescuento = 0;
                decimal dPDescto = 0;
                decimal dDescuento = 0;

                if (blNuevo)
                {
                    SiguienteID();   //Se vuelve a llamar por que se usará multiusuario
                }

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
                        dPrecio = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Precio"));
                        dImporte = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importe"));
                        dIvaImporte = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Iva"));
                        dIvaPorcentaje = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Piva"));
                        dPDescto = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "PDescto"));
                        dDescuento = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Descuento"));

                        dtCotizacionesdetalle.Rows.Add(cboSerie.EditValue, txtFolio.Text, intSeq,
                                                        dCantidad, intArticulosID, strDescripcion,
                                                        dPrecio, dImporte, dIvaImporte, dIvaPorcentaje,dPDescto,dDescuento);


                        dSubTotal += dImporte;
                        dIvaTotal += dIvaImporte;
                        dNetoTotal += dIvaImporte + dImporte - dDescuento;
                        dTDescuento += dDescuento;
                    }

                }

                string pTipocop = radioGroup1.EditValue.ToString();
                int intStatus = 1;
                int Agente = Convert.ToInt32(cboNumero.GetColumnValue("AgentesID"));
                int pDocumentoasignado = 0;
                string pDocumentoserie = "";
                int pDocumentofolio = 0;
                int pMotivosnoaprobacioncotizacionesID = 1;
                string pNoaprobacioncomentarios = "";

                dtCotizaciones.Rows.Add(cboSerie.EditValue, txtFolio.Text, txtFecha.Text,
                    pTipocop, cboNumero.EditValue, Agente, txtTiempodeentrega.Text,
                    txtCondicionesdepago.Text, txtVigencia.Text, txtLab.Text,
                    txtAtenciona.Text, txtCcp.Text, txtMail.Text, cboMonedas.EditValue,
                    txtEncabezado.Text, txtPiedep.Text, intStatus, dSubTotal, dIvaTotal, dNetoTotal,
                    pDocumentoasignado, pDocumentoserie, pDocumentofolio,
                    pMotivosnoaprobacioncotizacionesID, pNoaprobacioncomentarios, dTDescuento, "");

                CotizacionesCL cl = new CotizacionesCL();

                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.dtm = dtCotizaciones;
                cl.dtd = dtCotizacionesdetalle;
                cl.intUsuarioID = 1;
                cl.strMaquina = Environment.MachineName;
                result = cl.CotizacionesCrud();
                if (result=="OK")
                {
                    strSerie = cl.strSerie;
                    intFolio = cl.intFolio;
                    MessageBox.Show("Guardado correctamente");
                    tipoImpresion = "Troya";
                    Imprime();

                    if (blNuevo)
                    {
                        Inicialisalista();
                        radioGroup1.SelectedIndex = 0;
                        cboNumero.ItemIndex = 0;
                        txtTiempodeentrega.Text = string.Empty;
                        txtCondicionesdepago.Text = string.Empty;
                        txtVigencia.Text = string.Empty;
                        txtLab.Text = string.Empty;
                        txtAtenciona.Text = string.Empty;
                        txtCcp.Text = string.Empty;
                        txtMail.Text = string.Empty;
                        txtEncabezado.Text = string.Empty;
                        txtPiedep.Text = string.Empty;
                    }
                    
                }else
                {
                    MessageBox.Show("Al guardar:" + result);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }

        }//Guardar

        private void CambiarStatus()
        {
            try
            {

                CotizacionesCL cl = new CotizacionesCL();

                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intUsuarioID = globalCL.gv_UsuarioID; 
                cl.strMaquina = Environment.MachineName;
                cl.strRazon = RazonCan;
                cl.strStatus = strStatus;
                cl.intMotivosnoaprobacioncotizacionesID = Convert.ToInt32(cboMotivonoaprob.EditValue);  
                
                string result = cl.CotizaciionesCambiarStatus();

                if (result == "OK")
                {
                    MessageBox.Show("Movimiento " + strStatus + " se ha realizado correctamente");
                    ribbonPageGroup2.Visible = false;
                    bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //txtRazon.ToString = string.Empty;
                    switch (strStatus)
                    {
                        case "Rechazar":
                            bbiRechazar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            bbiAceptar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            break;
                        case "Aceptar":
                            bbiRechazar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            bbiAceptar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            break;
                    }
                    LlenarGrid(AñoFiltro,MesFiltro);
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cancelar: " + ex.Message);
            }

        }//cancelar  
        
        private void ExportarAPDF()
        {
            // A path to export a report.
            string pathCot = System.Configuration.ConfigurationManager.AppSettings["pathCotizaciones"].ToString();
            string reportPath = pathCot + strSerie + intFolio.ToString() + ".Pdf";

            if (tipoImpresion == "Normal")
            {
                using (XtraReport report = new CotizacionesFormatoImpresion())
                {
                    //parametros
                    report.Parameters["parameter1"].Value = strSerie;
                    report.Parameters["parameter2"].Value = intFolio;
                    report.Parameters["parameter1"].Visible = false;
                    report.Parameters["parameter2"].Visible = false;

                    // Specify PDF-specific export options.
                    PdfExportOptions pdfOptions = report.ExportOptions.Pdf;

                    // Specify the quality of exported images.
                    pdfOptions.ConvertImagesToJpeg = false;
                    pdfOptions.ImageQuality = PdfJpegImageQuality.Medium;

                    // Specify the PDF/A-compatibility.
                    pdfOptions.PdfACompatibility = PdfACompatibility.PdfA3b;                   

                    // Specify the document options.
                    pdfOptions.DocumentOptions.Application = "VisualSoftErp";
                    pdfOptions.DocumentOptions.Author = "VisualSoft";
                    pdfOptions.DocumentOptions.Keywords = "DevExpress, Reporting, PDF";
                    pdfOptions.DocumentOptions.Producer = Environment.UserName.ToString();
                    pdfOptions.DocumentOptions.Subject = "Document Subject";
                    pdfOptions.DocumentOptions.Title = "Document Title";

                    // Checks the validity of PDF export options 
                    // and return a list of any detected inconsistencies.
                    IList<string> result = pdfOptions.Validate();
                    if (result.Count > 0)
                        Console.WriteLine(String.Join(Environment.NewLine, result));
                    else
                    {
                        report.ExportToPdf(reportPath, pdfOptions);
                        EnviaCorreo(reportPath);
                    }
                }
            } else
            {
                using (XtraReport report = new CotizacionesFormatoImpresionTroya())
                {
                    //parametros
                    report.Parameters["parameter1"].Value = strSerie;
                    report.Parameters["parameter2"].Value = intFolio;
                    report.Parameters["parameter1"].Visible = false;
                    report.Parameters["parameter2"].Visible = false;

                    // Specify PDF-specific export options.
                    PdfExportOptions pdfOptions = report.ExportOptions.Pdf;

                    // Specify the quality of exported images.
                    pdfOptions.ConvertImagesToJpeg = false;
                    pdfOptions.ImageQuality = PdfJpegImageQuality.Medium;

                    // Specify the PDF/A-compatibility.
                    pdfOptions.PdfACompatibility = PdfACompatibility.PdfA3b;                   

                    // Specify the document options.
                    pdfOptions.DocumentOptions.Application = "VisualSoftErp";
                    pdfOptions.DocumentOptions.Author = "VisualSoft";
                    pdfOptions.DocumentOptions.Keywords = "DevExpress, Reporting, PDF";
                    pdfOptions.DocumentOptions.Producer = Environment.UserName.ToString();
                    pdfOptions.DocumentOptions.Subject = "Document Subject";
                    pdfOptions.DocumentOptions.Title = "Document Title";

                    // Checks the validity of PDF export options 
                    // and return a list of any detected inconsistencies.
                    IList<string> result = pdfOptions.Validate();
                    if (result.Count > 0)
                        Console.WriteLine(String.Join(Environment.NewLine, result));
                    else
                    {
                        report.ExportToPdf(reportPath, pdfOptions);
                        EnviaCorreo(reportPath);
                    }
                }
            }
                
            
        }

        private string Valida()
        {
            if (cboSerie.EditValue == null)
            {
                return "El campo Serie no puede ir vacio";
            }

            if (cboNumero.EditValue == null)
            {
                return "El campo ClientesID no puede ir vacio";
            }

           
            if (txtCondicionesdepago.Text.Length == 0)
            {
                return "El campo Condicionesdepago no puede ir vacio";
            }
            if (txtVigencia.Text.Length == 0)
            {
                txtVigencia.Text = "0";
            }
           
           
            if (txtMail.Text.Length == 0)
            {
                return "El campo Email no puede ir vacio";
            }
            if (cboMonedas.EditValue == null)
            {
                return "El campo MonedasID no puede ir vacio";
            }
            if (txtEncabezado.Text.Length == 0)
            {
                return "El campo Encabezado no puede ir vacio";
            }
           
            if (txtEncabezado.Text.Length == 0)
            {
                return "El campo Encabezado no puede ir vacio";
            }

            if (gridViewDetalle.RowCount == 0)
            {
                return "No hay registros en la cotizacion";
            }

            globalCL clg = new globalCL();


            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month, "VTA");
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";
            }

            return "OK";
        } //Valida

        private void LlenaCajas()
        {

            // Llenacajas SP normal como en catalogos para que lleness los campos de arriba
            //GridControlDetalle.DataSource = cl.LlenaGridDetalle   -->  SP traiga los datos de detalle para una cotizacion (folio)
            CotizacionesCL cl = new CotizacionesCL();
            cl.intFolio = intFolio;
            cl.strSerie = strSerie;
            String Result = cl.CotizacionesLlenaCajas();
            if (Result == "OK")
            {
                txtFecha.Text = cl.fFecha.ToShortDateString();
                radioGroup1.EditValue = cl.strTipocop.ToString();
                cboNumero.EditValue = cl.intClientesID;
                intAgenteID = cl.intAgentesID;
                txtTiempodeentrega.Text = cl.strTiempodeentrega;
                txtCondicionesdepago.Text = cl.strCondicionesdepago;
                txtVigencia.Text = Convert.ToString(cl.intVigencia);
                txtLab.Text = cl.strLibreabordo;
                txtAtenciona.Text = cl.strAtenciona;
                txtCcp.Text = cl.strConcopiapara;
                txtMail.Text = cl.strEmail;
                cboMonedas.EditValue = cl.strMonedasID;
                txtEncabezado.Text = cl.strEncabezado;
                txtPiedep.Text = cl.strPiedepagina;
                intStatus = cl.intStatus;
                cboSerie.EditValue = cl.strSerie;
                txtFolio.Text = cl.intFolio.ToString();
                txtPDescuento.Text = cl.decPDescto.ToString();
                cboAgente.EditValue = cl.intAgentesID;
            }
            else
            {
                MessageBox.Show(Result);
            }

        }//llenaCajas

        private void DetalleLlenaCajas()
        {
            try
            {
                CotizacionesCL cl = new CotizacionesCL();
                cl.intFolio = intFolio;
                cl.strSerie = strSerie;
                gridControlDetalle.DataSource = cl.CotizacionesDetalleGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }

        }//DetalleLlenaCajas

        private void Editar()
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Captura", "Preparando detalle...");
            LlenaCajas();
            DetalleLlenaCajas();

            BotonesEdicion();
            blNuevo = false;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }//Editar

        private void BotonesEdicion()
        {
            navigationFrame.SelectedPageIndex = 1;

            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCanceladas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEnviar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navBarControl.Visible = false;

            switch (intStatus)
            {
                case 1:
                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiAceptar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiRechazar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiImpresionTroya.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    break;

                case 3:
                    bbiAceptar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiRechazar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiImpresionTroya.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    break;

                case 4:
                    bbiAceptar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiRechazar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiImpresionTroya.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    break;
                case 5:
                    bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    break;
            }


           
            navBarControl.Visible = false;
            officeNavigationBar.Visible = false;
            gridOrigen = "Detalle";


            //Se obtiene el Id de la cotizacion
            //SiguienteID();


        }//BotonesEdicion

        private void LimpiaCajas()
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            radioGroup1.EditValue = "";
            cboNumero.EditValue = null;

            txtTiempodeentrega.Text = string.Empty;
            txtCondicionesdepago.Text = string.Empty;
            txtVigencia.Text = string.Empty;
            txtLab.Text = string.Empty;
            txtAtenciona.Text = string.Empty;
            txtCcp.Text = string.Empty;
            txtMail.Text = string.Empty;
            cboMonedas.EditValue = null;
            txtEncabezado.Text = string.Empty;
            txtPiedep.Text = string.Empty;

        }//limpiaCajas

        private void Historial()
        {
            BotonesHistorial();
            HistorialLlenarGrid();
                    
            lblFolio.Text = strSerie + intFolio.ToString();
            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            cboMediosdecontactoID.EditValue = 0;
            txtComentarios.Text = string.Empty;

            intSeqHistorial = 0;

        }//Historial

        private void BotonesHistorial()
        {
            navigationFrame.SelectedPageIndex = 4;
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup4.Visible = true;

            officeNavigationBar.Visible = false;


            navBarControl.Visible = false;

        } //BotonesHistorial

        private void HistorialLlenarGrid()
        {
            CotizacionesCL cl = new CotizacionesCL();
            cl.intFolio = intFolio;
            cl.strSerie = strSerie;
            gridControlHistorial.DataSource = cl.CotizacioneshistorialGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCotizacioneshistorial";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void GuardarHistorial()
        {
            try
            {
                String Result = ValidaHistorial();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }
                CotizacionesCL cl = new CotizacionesCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intSeq = intSeqHistorial;
                cl.strFecha = txtFecha.Text;
                cl.strHora = DateTime.Now.ToString("hh:mm");
                cl.intMediosdecontactoID = Convert.ToInt32(cboMediosdecontactoID.EditValue);
                cl.strComentarios = txtComentarios.Text;
                Result = cl.CotizacioneshistorialCrud();
                if (Result == "OK")
                {
                    HistorialLlenarGrid();

                    MessageBox.Show("Guardado Correctamente");

                    LimpiaCajasHistorial();
                    intSeqHistorial = 0;
                }
                else
                {
                    MessageBox.Show(Result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        }//GuardarHistorial

        private void LimpiaCajasHistorial()
        {

            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            cboMediosdecontactoID.EditValue = null;
            txtComentarios.Text = string.Empty;
        }//LimpiaCajasHistorial

        private string ValidaHistorial()
        {

            if (dateEditFecha.Text.Length == 0)
            {
                return "El campo fecha no puede ir vacio";
            }
            if (cboMediosdecontactoID.EditValue == null)
            {
                return "El campo MediosdecontactoID no puede ir vacio";
            }
            if (txtComentarios.Text.Length == 0)
            {
                return "El campo Comentarios no puede ir vacio";
            }

            if (Convert.ToDateTime(dateEditFecha.Text) < Convert.ToDateTime(txtFecha.Text))
            {
                return "La fecha del historial no puede ser menor a la de la cotización";
            }
            if (Convert.ToDateTime(dateEditFecha.Text)>DateTime.Now)
            {
                return "La fecha del historial no puede ser a futuro";
            }

            return "OK";
        } //Valida

        private void EditarHistorial()
        {
            bbiModificarHistorial.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            llenaCajasHistorial();
        }//EditarHistorial

        private void llenaCajasHistorial()
        {
            CotizacionesCL cl = new CotizacionesCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            cl.intSeq = intSeqHistorial;
            String Result = cl.CotizacioneshistorialLlenaCajas();
            if (Result == "OK")
            {
                lblFolio.Text = strSerie + intFolio.ToString();
                dateEditFecha.Text = DateTime.Now.ToShortDateString();
                cboMediosdecontactoID.EditValue = cl.intMediosdecontactoID;
                txtComentarios.Text = cl.strComentarios;
            }
            else
            {
                MessageBox.Show(Result);
            }
        }//LlenaCajasHistorial

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();

        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Pantalla principal", "Cargando grid...");

            navigationFrame.SelectedPageIndex = 0;

            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiAceptar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRechazar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiHistorial.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEnviar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiImpresionTroya.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navBarControl.Visible = true;

            navBarControl.Visible = true;
            officeNavigationBar.Visible = true;

            LlenarGrid(AñoFiltro,MesFiltro);
            

            intFolio = 0;
            strSerie = null;

            gridOrigen = "Principal";

            globalCL clg = new globalCL();
            string result = string.Empty;
            clg.strGridLayout = "gridCotizacionesDetalle";
            result = clg.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCotizaciones" +
                "";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

           
            this.Close();
        }

        private void bbiVistaP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridViewPrincipal.ShowRibbonPrintPreview();
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                //Sacamos datos del artículo
                if (e.Column.Name == "gridColumnArticulo")
                {
                    articulosCL cl = new articulosCL();
                    PreciosCL clp = new PreciosCL();
                    string art = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulo").ToString();
                    if (art.Length > 0) //validamos que haya algo en la celda
                    {
                        cl.intArticulosID = Convert.ToInt32(art);
                        string result = cl.articulosLlenaCajas();
                        if (result == "OK")
                        {
                            gridViewDetalle.SetFocusedRowCellValue("Des", cl.strNombre);
                            gridViewDetalle.SetFocusedRowCellValue("Um", cl.strUM);
                            gridViewDetalle.SetFocusedRowCellValue("Piva", cl.dPtjeIva);

                        }

                        //Se trae el precio
                        clp.strSerie = cboSerie.EditValue.ToString();
                        clp.iCliente = Convert.ToInt32(cboNumero.EditValue);
                        clp.iArtID = cl.intArticulosID;
                        clp.iLp = intLista;
                        result = clp.Precio();
                        if (result == "OK")
                        {
                            gridViewDetalle.SetFocusedRowCellValue("Precio", clp.dPrecio);
                        }
                    }
                }

                //Calculamos el importe multiplicando la cantidad por el precio
                if (e.Column.Name == "gridColumnPrecio" || e.Column.Name == "gridColumnCantidad" || e.Column.Name == "gridColumnPDescto")
                {
                    decimal cant = 0;
                    decimal pu = 0;
                    decimal imp = 0;
                    decimal iva = 0;
                    decimal piva = 0;
                    decimal dDescto = 0;
                    decimal dPDescto = 0;

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

                    imp = Math.Round(cant * pu, 2); //Calculamos el importe y lo redondeamos a dos decimales
                    dPDescto = Convert.ToInt32(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "PDescto"));
                    if (dPDescto>0)
                    {
                        dDescto = Math.Round(imp * (dPDescto / 100), 2);
                    }
                    
                  
                    iva = Math.Round((imp-dDescto) * (piva / 100), 2);

                    gridViewDetalle.SetFocusedRowCellValue("Importe", imp); //Le ponemos el valor a la celda Importe del grid
                    gridViewDetalle.SetFocusedRowCellValue("Descuento", dDescto); //Le ponemos el valor a la celda Importe del grid
                    gridViewDetalle.SetFocusedRowCellValue("Iva", iva); //Le ponemos el valor a la celda Iva del grid
                    gridViewDetalle.SetFocusedRowCellValue("Neto", iva + imp - dDescto); //Le ponemos el valor a la celda neto del grid

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GridviewDetalle Changed: " + ex);
            }
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            Guardar();
        }

        private void radioGroup1_EditValueChanged(object sender, EventArgs e)
        {
            strCLIENTEoPros = radioGroup1.EditValue.ToString();
            CargarCombosClienteoProspecto();
        }

        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            if (e.Group.Name != "navBarGroupAños")
                navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }

        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
        }    

        #region boton Cancelar,Rechazar,Aceptar

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0 && strSerie == null)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                strStatus = "Cancelar";
                ribbonPageGroup2.Visible = true;
                txtRazon.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                cboMotivonoaprob.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }


        }

        private void bbiRechazar_ItemClick(object sender, ItemClickEventArgs e)
        {
            intFolio = Convert.ToInt32(txtFolio.Text);
            strSerie = cboSerie.EditValue.ToString();


            if (intFolio == 0 && strSerie == null)
            {
                MessageBox.Show("bbiRechazar: no pueden ser nulos los campos");
            }
            else
            {
                strStatus = "Rechazar";
                ribbonPageGroup2.Visible = true;
                cboMotivonoaprob.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                txtRazon.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            }
        }

        private void bbiAceptar_ItemClick(object sender, ItemClickEventArgs e)
        {
            intFolio = Convert.ToInt32(txtFolio.Text);
            strSerie = cboSerie.EditValue.ToString();
            strStatus = "Aceptar";
            RazonCan = "Cotizacion Aceptada";
            if (intFolio == 0 && strSerie == null)
            {
                MessageBox.Show("bbiAceptar: No existe Folio y Serie ");
            }
            else
            {
                CambiarStatus();
            }
        }

        private void bbiCerrarCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strStatus = string.Empty;
            ribbonPageGroup2.Visible = false;
            strSerie = null;
            intFolio = 0;
        }

        private void txtRazon_EditValueChanged(object sender, EventArgs e)
        {
            BarEditItem item = sender as BarEditItem;
            object newValue = item.EditValue;
            RazonCan = newValue.ToString();
        }

        private void bbiProcederCancelar_ItemClick(object sender, ItemClickEventArgs e)
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
            if (strStatus == "Rechazar")
            {
                if (cboMonedas.EditValue == null && RazonCan == null)
                {
                    MessageBox.Show("Motivo no puede ir vacio");
                }
                else
                {
                    AceptarCambio();
                }
            }
            else if (strStatus == "Aceptar")
            {
                AceptarCambio();
            }

        }

        private void AceptarCambio()
        {
            DialogResult result = MessageBox.Show("Desea " + strStatus + " el Folio: " + intFolio.ToString(), strStatus, MessageBoxButtons.YesNo);
            if (result.ToString() == "Yes")
            {
                CambiarStatus();
            }
        }

        #endregion

        #region gridPrincipal
        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            string strStatus = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Estado"));
            intStatus = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status"));
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            strTo = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Email"));
            if (gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Concopiapara").ToString().Length>0)
            {
                strTo = strTo + ";" + gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Concopiapara").ToString();
            }
            

            if (strStatus == "Cancelada" || strStatus == "Rechazada")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else if (strStatus == "Registrada")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
             ////   bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }

        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                string status = view.GetRowCellValue(e.RowHandle, "Estado").ToString();

                if (status == "Cancelada")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                e.Appearance.ForeColor = Color.Black;
            }

        }
        #endregion

        #region Filtros de bbi inferior
        private void bbiRegistradas_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Registrada'";
        }

        private void bbiAceptadas_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Aceptada'";
        }

        private void bbiRechazadas_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Rechazada'";
        }

        private void bbiExpiradas_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Expirada'";
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

        #region ImprimirCotizacion
        private void Imprime()
        {
           
                try
                {
                    RibbonPagePrint.Visible = true;
                    ribbonPage1.Visible = false;
                    navigationFrame.SelectedPageIndex = 2;
                    officeNavigationBar.Visible = false;

                    reporte();
                    navBarControl.Visible = false;
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Imprime: " + ex.Message);
                }
            
        }
        private void bbiIImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }

            tipoImpresion = "Normal";
            Imprime();
            

        }

        private void reporte()
        {
            try
            {
                XtraReport report;
                
                if (tipoImpresion == "Normal")
                    report = new CotizacionesFormatoImpresion();
                else
                    report = new CotizacionesFormatoImpresionTroya();

                report.Parameters["parameter1"].Value = strSerie;
                report.Parameters["parameter2"].Value = intFolio;
                report.Parameters["parameter1"].Visible = false;
                report.Parameters["parameter2"].Visible = false;

                this.documentViewer1.DocumentSource = report;
                report.CreateDocument(false);
                documentViewer1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void bbiRegresarImp_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridOrigen == "Principal")
            {
                navBarControl.Visible = true;
                officeNavigationBar.Visible = true;
                navigationFrame.SelectedPageIndex = 0;
            }               
            else
                navigationFrame.SelectedPageIndex = 1;
            
            ribbonPage1.Visible = true;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText("Home");
            RibbonPagePrint.Visible = false;
            
            
        }
        #endregion

        #region HistotialCotizacion
        private void bbiHistorial_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Historial();
            }
        }

        private void bbiGuardarHistorial_ItemClick(object sender, ItemClickEventArgs e)
        {
            GuardarHistorial();
        }

        private void bbiEditarHistorial_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intSeqHistorial == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                EditarHistorial();
            }
        }

        private void gridViewHistorial_RowClick(object sender, RowClickEventArgs e)
        {
            intSeqHistorial = Convert.ToInt32(gridViewHistorial.GetRowCellValue(gridViewHistorial.FocusedRowHandle, "Seq"));
        }

        private void bbiRegresarHistorial_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            navBarControl.Visible = true;
            officeNavigationBar.Visible = true;
            ribbonPageGroup4.Visible = false;
            ribbonPageGroup1.Visible = true;

            if (gridOrigen=="Principal")
            {
                navigationFrame.SelectedPageIndex = 0;
                //ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText("Home");
            }
            else
            {
                navigationFrame.SelectedPageIndex = 1;
            }
            

           
            
            intSeqHistorial = 0;
        }
        #endregion

        private void Cotizaciones_Load(object sender, EventArgs e)
        {

        }

        private void cboNumero_EditValueChanged(object sender, EventArgs e)
        {

            try
            {
                //proceso de DE para obtener valor del combo
                DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;

                DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

                object orow = cboNumero.Properties.GetDataSourceRowByKeyValue(cboNumero.EditValue);
                if (orow != null)
                {
                    intClienteID = Convert.ToInt32(((DataRowView)row)["Clave"]);
                    intLista = Convert.ToInt32(((DataRowView)row)["Listadeprecios"]);
                    cboAgente.EditValue=Convert.ToInt32(((DataRowView)row)["AgentesID"]);
                }                   
            }

            catch (Exception ex)

            {

                MessageBox.Show("cboFamilias: " + ex.Message);

            }

            clientesCL cl = new clientesCL();

            cl.intClientesID = intClienteID;
            string result = cl.ClientesLlenaCajas();
            if (result == "OK")
            {
                cboMonedas.EditValue = cl.strMoneda;
                if (cl.intPlazo>0)
                {
                    txtCondicionesdepago.Text = "Crédito " + cl.intPlazo.ToString() + " días";
                }
                else
                {
                    txtCondicionesdepago.Text = "Contado";
                }
                txtMail.Text = cl.strEMail;
         
            }

        }

        private void bbiEnviar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Envío de correo", "Exportando pdf...");
            ExportarAPDF();
            //DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void EnviaCorreo(string fileName)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Correos", "Enviando...");
                globalCL cl = new globalCL();
 
                string result = cl.EnviaCorreoCotizacion(strTo, fileName, strSerie + intFolio.ToString(),strFrom);
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

        private void officeNavigationBar_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            string elemento = e.Item.Name.ToString();

            switch (elemento)
            {
                case "nviRegistradas":
                    gridViewPrincipal.ActiveFilterString = "[Estado]='Registrada'";
                    gridViewPrincipal.ViewCaption = "Cotizaciones registradas";
                    break;
                case "nviAceptadas":
                    gridViewPrincipal.ActiveFilterString = "[Estado]='Aceptada'";
                    gridViewPrincipal.ViewCaption = "Cotizaciones acepatadas";
                    break;
                case "nviRechazadas":
                    gridViewPrincipal.ActiveFilterString = "[Estado]='Rechazada'";
                    gridViewPrincipal.ViewCaption = "Cotizaciones rechazadas";
                    break;
                case "nviExpiradas":
                    gridViewPrincipal.ActiveFilterString = "[Estado]='Expiradas'";
                    gridViewPrincipal.ViewCaption = "Cotizaciones expiradas";
                    break;
                case "nviCanceladas":
                    gridViewPrincipal.ActiveFilterString = "[Estado]='Cancelada'";
                    gridViewPrincipal.ViewCaption = "Cotizaciones canceladas";
                    break;
                case "nviTodas":
                    gridViewPrincipal.ActiveFilter.Clear();
                    gridViewPrincipal.ViewCaption = "Cotizaciones";
                    break;
            }
        }

        private void btnPDescuento_Click(object sender, EventArgs e)
        {
            try
            {
                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtPDescuento.Text))
                {
                    MessageBox.Show("Teclee un % de descuento mayor de cero");
                    return;
                }

                decimal pDescto = Convert.ToDecimal(txtPDescuento.Text);
                decimal dImp = 0;
                decimal dDescuento = 0;
                decimal cant = 0;
                decimal pu = 0;                
                decimal iva = 0;
                decimal piva = 0;               

                for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++)
                {
                    gridViewDetalle.FocusedRowHandle = i;

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

                    dImp = Math.Round(cant * pu, 2);
                    dDescuento = Math.Round(dImp * (pDescto / 100), 2);

                    gridViewDetalle.SetFocusedRowCellValue("PDescto", pDescto);
                    gridViewDetalle.SetFocusedRowCellValue("Descuento", dDescuento);                   

                    iva = Math.Round((dImp - dDescuento) * (piva / 100), 2);

                    gridViewDetalle.SetFocusedRowCellValue("Importe", dImp); //Le ponemos el valor a la celda Importe del grid
                    gridViewDetalle.SetFocusedRowCellValue("Iva", iva); //Le ponemos el valor a la celda Iva del grid
                    gridViewDetalle.SetFocusedRowCellValue("Neto", iva + dImp - dDescuento); //Le ponemos el valor a la celda neto del grid
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridControlDetalle_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                view.DeleteSelectedRows();
                e.Handled = true;
            }
        }

        private void bbiImpresionTroya_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }

            tipoImpresion = "Troya";
            Imprime();
        }

        private void cboAgente_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;

            DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

            object orow = cboAgente.Properties.GetDataSourceRowByKeyValue(cboAgente.EditValue);
            if (orow != null)
            {
                txtEncabezado.Text = ((DataRowView)row)["Encabezado"].ToString();
                txtPiedep.Text = ((DataRowView)row)["Piedepagina"].ToString();
                strFrom = ((DataRowView)row)["Email"].ToString();
            }
        }

        private void navBarControl_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
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
    }
}