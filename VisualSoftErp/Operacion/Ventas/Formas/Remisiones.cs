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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using VisualSoftErp.Operacion.Ventas.Designers;

namespace VisualSoftErp.Catalogos.Ventas
{
    public partial class Remisiones : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string RazonCan;
        int intFolio;
        string strSerie;

        public BindingList<detalleCL> detalle;
        int intClienteID;
        int intAgenteID;
   
        int intExportacion;
        public bool blNuevo;
        int intStatus;

        string strformadepago;
        decimal dIvaPorcentaje;

        string strStatus;
        string strRazon;

        public Remisiones()
        {
            InitializeComponent();

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Remisiones";

            LlenarGrid();

            txtFecha.Text = DateTime.Now.ToShortDateString();

            CargaCombos();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()///gridprincipal
        {
            RemisionesCL cl = new RemisionesCL();
            gridControlPrincipal.DataSource = cl.RemisionesGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridRemisiones";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

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
        }

        public class detalleCL
        {
            public string Articulo { get; set; }
            public string Des { get; set; }
            public string Cantidad { get; set; }
            public string Um { get; set; }
            public string Precio { get; set; }
            public string Importe { get; set; }
            public string Neto { get; set; }
            public string Ultimocosto { get; set; }
            public string Iva { get; set; }
            public string Pedimento { get; set; }
            public string Piva { get; set; }

        }

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
            cboCliente.Properties.NullText = "Seleccione un cliente ";

            cl.strTabla = "Monedas";
            cboMonedas.Properties.ValueMember = "Clave";
            cboMonedas.Properties.DisplayMember = "Des";
            cboMonedas.Properties.DataSource = cl.CargaCombos();
            cboMonedas.Properties.ForceInitialize();
            cboMonedas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedas.Properties.PopulateColumns();
            cboMonedas.Properties.Columns["Clave"].Visible = false;
            cboMonedas.EditValue = cboMonedas.Properties.GetDataSourceValue(cboMonedas.Properties.ValueMember, 0);

            cl.strTabla = "Almacenes";
            cboAlmacen.Properties.ValueMember = "Clave";
            cboAlmacen.Properties.DisplayMember = "Des";
            cboAlmacen.Properties.DataSource = cl.CargaCombos();
            cboAlmacen.Properties.ForceInitialize();
            cboAlmacen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacen.Properties.PopulateColumns();
            cboAlmacen.Properties.Columns["Clave"].Visible = false;
            cboAlmacen.EditValue = cboMonedas.Properties.GetDataSourceValue(cboMonedas.Properties.ValueMember, 0);

            cl.strTabla = "Motivosdesalidas";
            cboMotivoSalida.Properties.ValueMember = "Clave";
            cboMotivoSalida.Properties.DisplayMember = "Des";

            cboMotivoSalida.Properties.DataSource = cl.CargaCombos();
            cboMotivoSalida.Properties.ForceInitialize();
            cboMotivoSalida.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMotivoSalida.Properties.PopulateColumns();
            cboMotivoSalida.Properties.Columns["Clave"].Visible = false;
            cboMotivoSalida.EditValue = cboMonedas.Properties.GetDataSourceValue(cboMonedas.Properties.ValueMember, 0);

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
            navBarControl.Visible = false;
            ribbonStatusBar.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
            CargaCombos();
            blNuevo = true;

            cboAlmacen.ItemIndex = 1;
            cboMotivoSalida.ItemIndex = 1;
          
            SiguienteID();
        }//Nuevo

        private void SiguienteID()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            RemisionesCL cl = new RemisionesCL();
            cl.strSerie = serie;
            cl.strDoc = "Remisiones";

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

        private void LimpiaCajas()
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();

            cboCliente.EditValue = null;
            cboFormadePago.SelectedItem = 0;
            txtPlazo.Text = string.Empty;
            txtAgente.Text = string.Empty;

            txtOrdendecompra.Text = string.Empty;
            txtSolicitadopor.Text = string.Empty;
            txtNotas.Text = string.Empty;
            swExportacion.IsOn = false;
            cboMonedas.EditValue = null;
            cboAlmacen.EditValue = null;
            cboMotivonoaprob.EditValue = null;
            cboMotivoSalida.EditValue = null;

        }//limpiaCajas

        private void Guardar()
        {
            Valida();
            try
            {
                //Declarar dataTables (son tablas en memoria)
                System.Data.DataTable dtRemisiones = new System.Data.DataTable("Remisiones");
                dtRemisiones.Columns.Add("Serie", Type.GetType("System.String"));
                dtRemisiones.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtRemisiones.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtRemisiones.Columns.Add("ClientesID", Type.GetType("System.Int32"));
                dtRemisiones.Columns.Add("AgentesID", Type.GetType("System.Int32"));
                dtRemisiones.Columns.Add("PedidosID", Type.GetType("System.Int32"));
                dtRemisiones.Columns.Add("Paridad", Type.GetType("System.Decimal"));
                dtRemisiones.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dtRemisiones.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtRemisiones.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dtRemisiones.Columns.Add("Status", Type.GetType("System.Int32"));
                dtRemisiones.Columns.Add("Plazo", Type.GetType("System.Int32"));
                dtRemisiones.Columns.Add("Fechacancelacion", Type.GetType("System.DateTime"));
                dtRemisiones.Columns.Add("Razoncancelacion", Type.GetType("System.String"));
                dtRemisiones.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtRemisiones.Columns.Add("PIva", Type.GetType("System.Decimal"));
                dtRemisiones.Columns.Add("MonedasID", Type.GetType("System.String"));
                dtRemisiones.Columns.Add("AlmacenesID", Type.GetType("System.Int32"));
                dtRemisiones.Columns.Add("Exportacion", Type.GetType("System.Int32"));
                dtRemisiones.Columns.Add("Ordendecompra", Type.GetType("System.String"));
                dtRemisiones.Columns.Add("Solicitadopor", Type.GetType("System.String"));
                dtRemisiones.Columns.Add("Formadepago", Type.GetType("System.Int32"));
                dtRemisiones.Columns.Add("CotizacionSerie", Type.GetType("System.String"));
                dtRemisiones.Columns.Add("CotizacionFolio", Type.GetType("System.Int32"));
                dtRemisiones.Columns.Add("Observaciones", Type.GetType("System.String"));
                dtRemisiones.Columns.Add("MotivosdesalidaID", Type.GetType("System.Int32"));
               

                System.Data.DataTable dtRemisionesdetalle = new System.Data.DataTable("Remisionesdetalle");
                dtRemisionesdetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtRemisionesdetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtRemisionesdetalle.Columns.Add("SEQ", Type.GetType("System.Int32"));
                dtRemisionesdetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtRemisionesdetalle.Columns.Add("Descripcion", Type.GetType("System.String"));
                dtRemisionesdetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtRemisionesdetalle.Columns.Add("Precio", Type.GetType("System.Decimal"));
                dtRemisionesdetalle.Columns.Add("Importe", Type.GetType("System.Decimal"));
                dtRemisionesdetalle.Columns.Add("ImporteNeto", Type.GetType("System.Decimal"));
                dtRemisionesdetalle.Columns.Add("Ultimocosto", Type.GetType("System.Decimal"));
                dtRemisionesdetalle.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtRemisionesdetalle.Columns.Add("Pedimento", Type.GetType("System.String"));
                dtRemisionesdetalle.Columns.Add("PIva", Type.GetType("System.Decimal"));
               

                string art;
                int intSeq;
                decimal dCantidad;
                int intArticulosID;
                string strDescripcion;
                decimal dPrecio;
                decimal dImporte;
                decimal dIvaImporte;

                decimal dSubTotal = 0;
                decimal dIvaTotal = 0;
                decimal dNetoTotal = 0;

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
                        //dIvaPorcentaje = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Piva"));
                        /////parametros susituidos
                        dtRemisionesdetalle.Rows.Add(cboSerie.EditValue, txtFolio.Text, intSeq,
                            intArticulosID, strDescripcion, dCantidad, dPrecio, dImporte,
                            dNetoTotal, dPrecio //pUltimocosto , 
                            , dIvaImporte, strDescripcion //pPedimento, 
                            , dIvaPorcentaje);


                        dSubTotal += dImporte;
                        dIvaTotal += dIvaImporte;
                        dNetoTotal += dIvaImporte + dImporte;
                    }

                }

                int intClienteID = Convert.ToInt32(cboCliente.EditValue);
                int intStatus = 1;
                int intAgente = intAgenteID;
                int intPedidosID = 1;
                int intParidad = 1;
                int intPlazo = Convert.ToInt32(txtPlazo.Text);
                DateTime fechadecancelacion = Convert.ToDateTime(null);
                string razondecancelacion = null;
                int intUsuarioID = 1;
                string strMonedasID = cboMonedas.EditValue.ToString();
                int intAlmacenID = Convert.ToInt32(cboAlmacen.EditValue);
                int inMotivoID = Convert.ToInt32(cboMotivoSalida.EditValue);
                string pNoaprobacioncomentarios = "";
                int intExportacion = swExportacion.IsOn ? 1 : 0;

                int intFormadePago = cboFormadePago.SelectedIndex;

                string strCotizacionSerie =cboSerie.EditValue.ToString();
                int intCotizacionFolio = Convert.ToInt32(txtFolio.Text);


                dtRemisiones.Rows.Add(cboSerie.EditValue, txtFolio.Text, txtFecha.Text, intClienteID,
                intAgente, intPedidosID, intParidad, dSubTotal,
                dIvaTotal, dNetoTotal, intStatus, intPlazo, fechadecancelacion,
                razondecancelacion, intUsuarioID, dIvaPorcentaje, strMonedasID, intAlmacenID,
                intExportacion, txtOrdendecompra.Text, txtSolicitadopor.Text,
                intFormadePago, strCotizacionSerie, intCotizacionFolio,
                txtNotas.Text, inMotivoID);


                RemisionesCL cl = new RemisionesCL();

                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.dtm = dtRemisiones;
                cl.dtd = dtRemisionesdetalle;
                cl.intUsuarioID = 1;
                cl.strMaquina = Environment.MachineName;
                string result = cl.RemisionesCrud();
                MessageBox.Show(result);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }

        }//Guardar

        private string Valida()
        {
            if (cboSerie.EditValue == null)
            {
                return "El campo Serie no puede ir vacio";
            }

            if (cboCliente.EditValue == null)
            {
                return "El campo ClientesID no puede ir vacio";
            }

            if (txtPlazo.Text.Length == 0)
            {
                return "El campo Tiempodeentrega no puede ir vacio";
            }
            if (txtOrdendecompra.Text.Length == 0)
            {
                return "El campo Condicionesdepago no puede ir vacio";
            }
            if (txtNotas.Text.Length == 0)
            {
                return "El campo Vigencia no puede ir vacio";
            }
            if (cboFormadePago.EditValue == null)
            {
                return "El campo ClientesID no puede ir vacio";
            }
            if (cboAlmacen.EditValue == null)
            {
                return "El campo ClientesID no puede ir vacio";
            }
            if (cboMotivoSalida.EditValue == null)
            {
                return "El campo ClientesID no puede ir vacio";
            }
            if (cboMonedas.EditValue == null)
            {
                return "El campo ClientesID no puede ir vacio";
            }


            return "OK";
        } //Valida

        private void CambiarStatus()
        {
            try
            {

                RemisionesCL cl = new RemisionesCL();

                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intUsuarioID = 1;  // ------------------------- No dejarlo fijo -------------------
                cl.strMaquina = Environment.MachineName;
                cl.strRazon = RazonCan;
                cl.strStatus = strStatus;
                

                string result = cl.RemisionesCambiarStatus();

                if (result == "OK")
                {
                    MessageBox.Show("Movimiento " + strStatus + " se ah realizado correctamente");
                    ribbonPageGroup2.Visible = false;
                    bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    strRazon = string.Empty;
                    switch (strStatus)
                    {
                        case "Cancelar":
                            bbiPagarsinfactura.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            bbiFacturar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            break;
                        case "Facturar":
                            bbiPagarsinfactura.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            bbiFacturar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            break;
                        case "Pagarsinfacturar":
                            bbiPagarsinfactura.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            bbiFacturar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            break;
                    }
                    LlenarGrid();
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

        private void LlenaCajas()
        {

            RemisionesCL cl = new RemisionesCL();         
            cl.intFolio = intFolio;
            cl.strSerie = strSerie;
            String Result = cl.RemisionesLlenaCajas();
            if (Result == "OK")
            {
                txtFolio.Text = cl.intFolio.ToString();
                cboSerie.EditValue = cl.strSerie.ToString();
                cboCliente.EditValue = cl.intClientesID;
                txtFecha.Text = cl.fFecha.ToShortDateString();
                cboFormadePago.SelectedIndex = cl.intFormadepago;
                txtPlazo.Text = cl.intPlazo.ToString();

                txtOrdendecompra.Text = cl.strOrdendecompra;
                txtSolicitadopor.Text = cl.strSolicitadopor;
                txtNotas.Text = cl.strObservaciones;
                if (cl.intExportacion == 1) { swExportacion.IsOn = true; }
                else { swExportacion.IsOn = false; }
                txtFecha.Text = cl.fFecha.ToShortDateString();
                cboAlmacen.EditValue = cl.intAlmacenesID;
                cboMonedas.EditValue = cl.strMonedasID;
                cboMotivoSalida.EditValue = cl.intMotivosdesalidaID;

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
                RemisionesCL cl = new RemisionesCL();
                cl.intFolio = intFolio;
                cl.strSerie = strSerie;
                gridControlDetalle.DataSource = cl.RemisionesDetalleGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }

        }//DetalleLlenaCajas

        private void Editar()
        {
            LlenaCajas();
            DetalleLlenaCajas();

            BotonesEdicion();
            blNuevo = false;
        }//Editar

        private void BotonesEdicion()
        {

            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
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
                    bbiFacturar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiPagarsinfactura.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    break;

                case 3:
                    bbiFacturar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiPagarsinfactura.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    break;

                case 4:
                    bbiFacturar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiPagarsinfactura.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    break;
                case 5:
                    bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    break;
            }


            navigationFrame.SelectedPageIndex = 1;
            navBarControl.Visible = false;
            ribbonStatusBar.Visible = false;


            //Se obtiene el Id de la cotizacion
            //SiguienteID();


        }//BotonesEdicion      

        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }

        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
        }

        #region Filtros de bbi Meses

        private void navBarItemEne_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 1";
        }

        private void navBarItemFeb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 2";
        }

        private void navBarItemMar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 3";
        }

        private void navBarItemAbr_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 4";
        }

        private void navBarItemMay_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 5";
        }

        private void navBarItemJun_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 6";
        }

        private void navBarItemJul_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 7";
        }

        private void navBarItemAgo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 8";
        }

        private void navBarItemsep_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 9";
        }

        private void navBarItemOct_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 10";
        }

        private void navBarItemNov_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 11";
        }

        private void navBarItemDic_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 12";
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

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
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
                           // gridViewDetalle.SetFocusedRowCellValue("Piva", cl.intPtjeIva);

                        }
                    }
                }

                //Calculamos el importe multiplicando la cantidad por el precio
                if (e.Column.Name == "gridColumnPrecio" || e.Column.Name == "gridColumnCantidad")
                {
                    decimal cant = 0;
                    decimal pu = 0;
                    decimal imp = 0;
                    decimal iva = 0;
                    decimal piva = 0;

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
                    iva = Math.Round(imp * (dIvaPorcentaje/ 100), 2);

                    gridViewDetalle.SetFocusedRowCellValue("Importe", imp); //Le ponemos el valor a la celda Importe del grid
                    gridViewDetalle.SetFocusedRowCellValue("Iva", iva); //Le ponemos el valor a la celda Iva del grid
                    gridViewDetalle.SetFocusedRowCellValue("Neto", iva + imp); //Le ponemos el valor a la celda neto del grid

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GridviewDetalle Changed: " + ex);
            }
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
                txtAgente.Text = cl.strAgente;
                intAgenteID = cl.intAgentesID;

                if (cl.intPlazo > 0) { }
                else { cboFormadePago.ReadOnly = true; }
                if (cl.intPlazo == 0) { }
                else { cboFormadePago.ReadOnly = false; cboFormadePago.SelectedIndex = 2; }

                if (cl.intExportar > 0) { }
                else { swExportacion.ReadOnly = true; }
                if (cl.intExportar == 0) { }
                else { swExportacion.ReadOnly = false; }
                dIvaPorcentaje = cl.intPIva;

            }

        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
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

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            string strStatus = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Estado"));
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            intStatus = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status"));

            if (strStatus == "Cancelada")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else if (strStatus == "Pagadasinfactura" || strStatus == "Facturada" || strStatus == "Registrada")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
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
                if (status == "Facturada")
                {
                    e.Appearance.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                e.Appearance.ForeColor = Color.Black;
            }

        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

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
            navBarControl.Visible = true;

            navBarControl.Visible = true;
            ribbonStatusBar.Visible = true;

            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;

            intFolio = 0;
            strSerie = null;
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridRemisiones" +
                "";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiVista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridViewPrincipal.ShowRibbonPrintPreview();

        }

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
            if (strStatus == "Pagarsinfactura")
            {
                AceptarCambio();
            }
            else if (strStatus == "Facturar")
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

        private void bbiPagarsinfactura_ItemClick(object sender, ItemClickEventArgs e)
        {
            intFolio = Convert.ToInt32(txtFolio.Text);
            strSerie = cboSerie.EditValue.ToString();
            strStatus = "Pagarsinfacturar";
            RazonCan = "Remision Pagada sin factura";
            if (intFolio == 0 && strSerie == null)
            {
                MessageBox.Show("bbiPagarsinfactura: No existe Folio y Serie ");
            }
            else
            {
                AceptarCambio();
            }
        }

        private void bbiFacturar_ItemClick(object sender, ItemClickEventArgs e)
        {
            intFolio = Convert.ToInt32(txtFolio.Text);
            strSerie = cboSerie.EditValue.ToString();
            strStatus = "Facturar";
            RazonCan = "Remision Facturada";
            if (intFolio == 0 && strSerie == null)
            {
                MessageBox.Show("bbiAceptar: No existe Folio y Serie ");
            }
            else
            {
                AceptarCambio();
            }
        }

        private void bbiIImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                try
                {
                    RibbonPagePrint.Visible = true;
                    ribbonPage1.Visible = false;
                    navigationFrame.SelectedPageIndex = 2;
                    ribbonStatusBar.Visible = false;

                    reporte();
                    navBarControl.Visible = false;
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Imprime: " + ex.Message);
                }
            }
        }

        private void reporte()
        {
            try
            {
                RemisionesImprimirFormato report = new RemisionesImprimirFormato();
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
            navigationFrame.SelectedPageIndex = 0;
            navBarControl.Visible = true;
            ribbonStatusBar.Visible = true;
            ribbonPage1.Visible = true;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText("Home");
            RibbonPagePrint.Visible = false;
            intFolio = 0;
            strSerie = string.Empty;
        }

        
    }
}