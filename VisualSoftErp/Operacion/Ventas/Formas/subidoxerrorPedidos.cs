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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using VisualSoftErp.Clases.VentasCLs;

namespace VisualSoftErp.Catalogos.Ventas
{
    public partial class Pedidos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Pedidos()
        {
            InitializeComponent();
            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Pedidos";

            LlenarGrid();



            CargaCombos();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        string RazonCan;
        int intFolio;
        string strSerie;

        public BindingList<detalleCL> detalle;
        int intClienteID;
        int intAgenteID;
        int intplazo;

        public bool blNuevo;
        int intStatus;
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

        string strTipoRelacion;

        int intUsuarioID = 1; //NO DEJAR FIJO 


        private void LlenarGrid()///gridprincipal
        {
            PedidosCL cl = new PedidosCL();
            gridControlPrincipal.DataSource = cl.PedidosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPedidos";
            clg.restoreLayout(gridViewPrincipal);
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
            cboCliente.Properties.NullText = "Seleccione un cliente";


            cl.strTabla = "Monedas";
            cboMonedas.Properties.ValueMember = "Clave";
            cboMonedas.Properties.DisplayMember = "Des";
            cboMonedas.Properties.DataSource = cl.CargaCombos();
            cboMonedas.Properties.ForceInitialize();
            cboMonedas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedas.Properties.PopulateColumns();
            cboMonedas.Properties.Columns["Clave"].Visible = false;
            cboMonedas.Properties.NullText = "Seleccione una moneda";

            cl.strTabla = "Articulos";
            repositoryItemLookUpEditArticulo.ValueMember = "Clave";
            repositoryItemLookUpEditArticulo.DisplayMember = "Des";
            repositoryItemLookUpEditArticulo.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulo.ForceInitialize();
            repositoryItemLookUpEditArticulo.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditArticulo.Properties.NullText = "Seleccione un articulo";


            cl.strTabla = "Motivonoaprobacion";

            repositoryItemLookUpEditNoAprobacion.ValueMember = "Clave";
            repositoryItemLookUpEditNoAprobacion.DisplayMember = "Des";
            repositoryItemLookUpEditNoAprobacion.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditNoAprobacion.ForceInitialize();
            repositoryItemLookUpEditNoAprobacion.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditNoAprobacion.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditNoAprobacion.Properties.NullText = "Seleccione un articulo";

            cl.strTabla = "Serie";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Clave";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Des"].Visible = false;
            cboSerie.EditValue = cboSerie.Properties.GetDataSourceValue(cboSerie.Properties.ValueMember, 0);



            cl.strTabla = "Almacenes";
            cboAlmacen.Properties.ValueMember = "Clave";
            cboAlmacen.Properties.DisplayMember = "Des";
            cboAlmacen.Properties.DataSource = cl.CargaCombos();
            cboAlmacen.Properties.ForceInitialize();
            cboAlmacen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacen.Properties.PopulateColumns();
            cboAlmacen.Properties.Columns["Clave"].Visible = false;
            cboAlmacen.Properties.NullText = "Seleccione un almacen";

            cl.strTabla = "Transportes";
            cboTransportes.Properties.ValueMember = "Clave";
            cboTransportes.Properties.DisplayMember = "Des";
            cboTransportes.Properties.DataSource = cl.CargaCombos();
            cboTransportes.Properties.ForceInitialize();
            cboTransportes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTransportes.Properties.PopulateColumns();
            cboTransportes.Properties.Columns["Clave"].Visible = false;
            cboTransportes.Properties.NullText = "Seleccione un transporte";
            txtFechaEntrega.Properties.NullText = "Seleccione fecha de entrega";

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
        }

        public class detalleCL
        {
            public string Articulo { get; set; }
            public string Des { get; set; }
            public string Cantidad { get; set; }
            public string Um { get; set; }
            public string Precio { get; set; }
            public string Importe { get; set; }
            public string Pdescuento { get; set; }
            public string Neto { get; set; }
            public string Iva { get; set; }
            public string IEPS { get; set; }
            public string Piva { get; set; }
            public string Pieps { get; set; }
            public string Descuento { get; set; }
            public string TotalArticulo { get; set; }
            public string FechadeEntrega { get; set; }
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
            bbiEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navBarControl.Visible = false;
            ribbonStatusBar.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
            CargaCombos();
            blNuevo = true;
            cboCliente.ReadOnly = false;
            BuscarSerie();
            SiguienteID();
        }//Nuevo

        private string BuscarSerie()
        {
            SerieCL cl = new SerieCL();
            cl.intUsuarioID = intUsuarioID;
            String Result = cl.BuscarSerieporUsuario();
            if (Result == "OK")
            {
                if (cl.strSerie == "")
                { cboSerie.ReadOnly = false; }
                else
                {
                    cboSerie.EditValue = cl.strSerie;
                    cboSerie.ReadOnly = true;
                }

            }
            else
            {
                MessageBox.Show(Result);
            }
            return "";
        } //BuscaSerie

        private void LimpiaCajas()
        {
            cboCliente.EditValue = null;
            txtAgente.Text = string.Empty;
            txtOrdendecompra.Text = string.Empty;
            cboCondicionesdepago.SelectedIndex = 0;
            swExportacion.IsOn = false;
            cboMonedas.ItemIndex = 0;
            swPosfechado.IsOn = false;
            cboSerie.EditValue = null;
            txtFolio.Text = string.Empty;
            txtPdescto.Text = string.Empty;
            txtSuma.Text = string.Empty;
            txtDescuento.Text = string.Empty;
            txtSubtotal.Text = string.Empty;
            txtIva.Text = string.Empty;
            txtIeps.Text = string.Empty;
            txtReiva.Text = string.Empty;
            txtRetIsr.Text = string.Empty;
            txtNeto.Text = string.Empty;
            cboAlmacen.ItemIndex = 0;
            cboTransportes.ItemIndex = 0;
            txtFecha.Text = DateTime.Now.ToShortDateString();
        }//LimpiaCajas

        private void SiguienteID()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            FacturasCL cl = new FacturasCL();
            cl.strSerie = serie;
            cl.strDoc = "Pedidos";

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
                return "El campo Serie no puede ir vacio";
            }
            if (txtFolio.Text.Length == 0)
            {
                return "El campo c_FormaPago no puede ir vacio";
            }
            if (txtOrdendecompra.Text.Length == 0)
            {
                return "El campo c_FormaPago no puede ir vacio";
            }
            try
            {
                PedidosCL cl = new PedidosCL();



                cl.intClientesID = intClienteID;
                cl.strOc = txtOrdendecompra.Text;
                string result = cl.BuscarOrdendecompra();
                // MessageBox.Show(result);
                if (result == "OK")
                {

                    if (cl.strOc == txtOrdendecompra.Text)
                    {
                        DialogResult dialogResult = MessageBox.Show("Ya existe una orden de compra: " + txtOrdendecompra.Text + " registrada con el mismo cliente, quiere continuar?",
                            "Advertencia", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            txtOrdendecompra.Text = cl.strOc;
                        }
                        else
                        {
                            return "";
                        }
                    }


                }
            }
            catch (Exception es)
            { MessageBox.Show("Valida/Buscarordendecompra:" + es); }

            return "OK";
        } //Valida

        private void Guardar()
        {
            Valida();
            try
            {
                System.Data.DataTable dtPedidos = new System.Data.DataTable("Pedidos");
                dtPedidos.Columns.Add("Serie", Type.GetType("System.String"));
                dtPedidos.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtPedidos.Columns.Add("ClientesID", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("AgentesID", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dtPedidos.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtPedidos.Columns.Add("RetIva", Type.GetType("System.Decimal"));
                dtPedidos.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtPedidos.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dtPedidos.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dtPedidos.Columns.Add("Observaciones", Type.GetType("System.String"));
                dtPedidos.Columns.Add("Condicionesdepago", Type.GetType("System.String"));
                dtPedidos.Columns.Add("Plazo", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("Status", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("Fechacancelacion", Type.GetType("System.DateTime"));
                dtPedidos.Columns.Add("Razoncancelacion", Type.GetType("System.String"));
                dtPedidos.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("TransportesID", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("MonedasID", Type.GetType("System.String"));
                dtPedidos.Columns.Add("AlmacenesID", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("Oc", Type.GetType("System.String"));
                dtPedidos.Columns.Add("Fechaestimadadeentrega", Type.GetType("System.DateTime"));
                dtPedidos.Columns.Add("Tipodecambio", Type.GetType("System.Decimal"));
                dtPedidos.Columns.Add("Exportacion", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("Statuscxc", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("Statusexistencia", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("Statusexistenciaparcial", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("Statusbajocosto", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("Depurado", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("Razondepurado", Type.GetType("System.String"));
                dtPedidos.Columns.Add("Fechadepurado", Type.GetType("System.DateTime"));
                dtPedidos.Columns.Add("Usuariodepurado", Type.GetType("System.Int32"));

                // dtPedidos.Rows.Add(pSerie, pFolio, pFecha, pClientesID, pAgentesID, pSubtotal, pIva, pRetIva, pIeps, pNeto, pDescuento, pObservaciones, pCondicionesdepago, pPlazo, pStatus, pFechacancelacion, pRazoncancelacion, pUsuariosID, pTransportesID, pMonedasID, pAlmacenesID, pOc, pFechaestimadadeentrega, pTipodecambio, pExportacion, pStatuscxc, pStatusexistencia, pStatusexistenciaparcial, pStatusbajocosto, pDepurado, pRazondepurado, pFechadepurado, pUsuariodepurado);


                System.Data.DataTable dtPedidosDetalle = new System.Data.DataTable("PedidosDetalle");
                dtPedidosDetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtPedidosDetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtPedidosDetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtPedidosDetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtPedidosDetalle.Columns.Add("Descripcion", Type.GetType("System.String"));
                dtPedidosDetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtPedidosDetalle.Columns.Add("Precio", Type.GetType("System.Decimal"));
                dtPedidosDetalle.Columns.Add("Preciodelista", Type.GetType("System.Decimal"));
                dtPedidosDetalle.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtPedidosDetalle.Columns.Add("Importe", Type.GetType("System.Decimal"));
                dtPedidosDetalle.Columns.Add("PIva", Type.GetType("System.Decimal"));
                dtPedidosDetalle.Columns.Add("Porcentajededescuento", Type.GetType("System.Decimal"));
                dtPedidosDetalle.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dtPedidosDetalle.Columns.Add("PIeps", Type.GetType("System.Decimal"));
                dtPedidosDetalle.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtPedidosDetalle.Columns.Add("Fechaestimadadeentrega", Type.GetType("System.DateTime"));
                dtPedidosDetalle.Columns.Add("ArticulosfisicoID", Type.GetType("System.Int32"));
                dtPedidosDetalle.Columns.Add("Lote", Type.GetType("System.String"));
                dtPedidosDetalle.Columns.Add("Pallet", Type.GetType("System.String"));
                dtPedidosDetalle.Columns.Add("Statusexistencia", Type.GetType("System.Int32"));

                // dtPedidosDetalle.Rows.Add(pSerie, pFolio, pSeq, pArticulosID, pDescripcion, pCantidad, pPrecio, pPreciodelista, pIva, pImporte, pPIva, pPorcentajededescuento, pDescuento, pPIeps, pIeps, pFechaestimadadeentrega, pArticulosfisicoID, pLote, pPallet, pStatusexistencia);

                string art;
                int intSeq;
                decimal dCantidad;
                int intArticulosID;
                string strDescripcion;
                decimal dPrecio;
                decimal dImporte;
                decimal dIvaImporte;
                DateTime FechadeEntregaArt;
                decimal dPorcentajededescuento = 0, dDescuento = 0,
                           dPIeps, dIeps;
                string pLote = "", pPallet = "";
                int pStatusexistencia = 3;
                decimal dSubTotal = 0;
                decimal dIvaTotal = 0;
                decimal dIepsTotal = 0;
                decimal dDescuentoTotal = 0;
                decimal dNetoTotal = 0;

                decimal dSumaDesc = Convert.ToDecimal(txtDescuento.Text.Replace("$", ""));

                if (blNuevo)
                {
                    SiguienteID();
                }


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
                        dPIeps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Pieps"));
                        dIeps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "IEPS"));
                        FechadeEntregaArt = Convert.ToDateTime(gridViewDetalle.GetRowCellValue(i, "FechadeEntrega"));
                        dDescuento = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Descuento"));
                        dPorcentajededescuento = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Pdescuento"));


                        dtPedidosDetalle.Rows.Add(cboSerie.EditValue, Convert.ToInt32(txtFolio.Text), intSeq,
                            intArticulosID,
                            strDescripcion, dCantidad, dPrecio,
                            dPrecio, dIvaImporte, dImporte, dIvaPorcentaje,
                            dPorcentajededescuento, dDescuento,
                            dPIeps, dIeps, FechadeEntregaArt,
                            intArticulosID, pLote, pPallet,
                            pStatusexistencia);


                        dIepsTotal += dIeps; //no esta en cotizaciones 
                        dDescuentoTotal += dDescuento;
                        dSubTotal += dImporte;
                        dIvaTotal += dIvaImporte;
                        dNetoTotal += (((dIvaImporte + dIeps + dImporte) - RetPIva) - RetPIsr);


                    }
                }

                int intClienteID = Convert.ToInt32(cboCliente.EditValue);
                int intStatus = 1;

                string strMonedasID = cboMonedas.EditValue.ToString();

                int intExportacion = swExportacion.IsOn ? 1 : 0;
                int pAlmacenesID = Convert.ToInt32(cboAlmacen.EditValue);

                DateTime FechaCancelacion = Convert.ToDateTime(txtFecha.Text);
                string strRazonCancelacion = "", pRazondepurado = "";
                string pObservaciones = txtObservaciones.Text;
                DateTime pFechaestimadadeentrega = Convert.ToDateTime(txtFechaEntrega.Text);


                int pStatuscxc = 0, pStatusexistenciaparcial = 0, pStatusbajocosto = 0, pDepurado = 0,
                   pUsuariodepurado = 0;
                decimal pTipodecambio = 1;

                string strCondicionesdepago = cboCondicionesdepago.SelectedIndex.ToString();
                int intPlazo = Convert.ToInt32(txtPlazo.Text);

                dtPedidos.Rows.Add(cboSerie.EditValue, Convert.ToInt32(txtFolio.Text), Convert.ToDateTime(txtFecha.Text),
                    intClienteID, intAgenteID, dSubTotal, dIvaTotal,
                    RetPIva, dIepsTotal, dNetoTotal, dDescuento, pObservaciones,
                    strCondicionesdepago, intPlazo, intStatus, Convert.ToDateTime(txtFecha.Text),
                    strRazonCancelacion, intUsuarioID, cboTransportes.EditValue,
                    strMonedasID, pAlmacenesID, txtOrdendecompra.Text,
                    pFechaestimadadeentrega, pTipodecambio, intExportacion,
                    pStatuscxc, pStatusexistencia, pStatusexistenciaparcial,
                    pStatusbajocosto, pDepurado, pRazondepurado,
                    Convert.ToDateTime(txtFecha.Text), pUsuariodepurado);


                PedidosCL cl = new PedidosCL();
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.dtm = dtPedidos;
                cl.dtd = dtPedidosDetalle;
                cl.intUsuarioID = 1;
                cl.strMaquina = Environment.MachineName;
                string result = cl.PedidosCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado correctamente");
                    //limpiar variables universales
                    dPIva = 0;
                    dPIeps = 0;
                    PtjeRetIsr = 0;
                    PtjeRetIva = 0;
                    RetPIsr = 0;
                    RetPIva = 0;
                    LimpiaCajas();
                    Inicialisalista();
                    //Limpiaca cajas de texto del detalle
                    //Limipas el grid del detalle Inicialista

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }

        }//Guardar

        private void Editar()
        {
            LlenaCajas();
            DetalleLlenaCajas();

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
            bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;



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
            PedidosCL cl = new PedidosCL();
            cl.intFolio = intFolio;
            cl.strSerie = strSerie;
            String Result = cl.PedidosLlenaCajas();
            if (Result == "OK")
            {
                txtFolio.Text = cl.intFolio.ToString();
                cboSerie.EditValue = cl.strSerie.ToString();
                cboCliente.EditValue = cl.intClientesID;
                txtOrdendecompra.Text = cl.strOc;
                txtPlazo.Text = cl.intPlazo.ToString();
                txtFecha.Text = cl.fFecha.ToShortDateString();
                cboCondicionesdepago.SelectedIndex = Convert.ToInt32(cl.strCondicionesdepago);

                if (cl.intExportacion == 1) { swExportacion.IsOn = true; }
                else { swExportacion.IsOn = false; }
                cboMonedas.EditValue = cl.strMonedasID;
                txtFechaEntrega.Text = cl.fFechaestimadadeentrega.ToShortDateString();
                //Posfechado pendiente

                cboAlmacen.EditValue = cl.intAlmacenesID;
                cboTransportes.EditValue = cl.intTransportesID;
                //Descuento suma y totaldescuento pendiente
                txtSubtotal.Text = cl.dSubTotal.ToString();
                txtReiva.Text = cl.strRetIva;
                txtIva.Text = cl.strIVA;

                txtObservaciones.Text = cl.strObservaciones;



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
                PedidosCL cl = new PedidosCL();
                cl.intFolio = intFolio;
                cl.strSerie = strSerie;
                gridControlDetalle.DataSource = cl.PedidosDetalleGrid();
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

        private void CambiarStatus()
        {
            try
            {

                PedidosCL cl = new PedidosCL();

                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intUsuarioID = 1;  // ------------------------- No dejarlo fijo -------------------
                cl.strMaquina = Environment.MachineName;
                cl.strRazon = txtRazonCancelacion.Text;
                cl.strStatus = strStatus;


                string result = cl.PedidosCambiarStatus();

                if (result == "OK")
                {
                    MessageBox.Show("Movimiento " + strStatus + " se ah realizado correctamente");
                    bbiProceder.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    txtRazonCancelacion.Text = string.Empty;
                    switch (strStatus)
                    {
                        case "Cancelar":
                            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            break;
                    }
                    LlenarGrid();
                    LlenarGridbitacora();
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

        private void LlenarGridbitacora()///gridprincipal
        {
            PedidosCL cl = new PedidosCL();
            cl.intFolio = intFolio;
            cl.strSerie = strSerie;
            gridControlBitacora.DataSource = cl.PedidosBitacoraGrid();
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPedidosBitacora";
            clg.restoreLayout(gridViewBitacora);
            //Global, manda el nombre del grid para la clase Global           
        } //LlenarGrid()


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
            Nuevo();
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
                else { cboCondicionesdepago.ReadOnly = true; }
                if (cl.intPlazo == 0) { }
                else { cboCondicionesdepago.SelectedIndex = 2; }

                if (cl.intExportar > 0) { }
                else { swExportacion.ReadOnly = true; }
                if (cl.intExportar == 0) { }
                else { swExportacion.ReadOnly = false; }


                txtPlazo.Text = cl.intPlazo.ToString();

                dPIva = cl.intPIva;
                dPIeps = cl.intPIeps;
                PtjeRetIva = cl.intPRetiva;
                PtjeRetIsr = cl.intPRetisr;
                intplazo = cl.intPlazo;

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
                            gridViewDetalle.SetFocusedRowCellValue("Piva", cl.intPtjeIva);
                            gridViewDetalle.SetFocusedRowCellValue("Pieps", cl.intPtjeIeps);
                            gridViewDetalle.SetFocusedRowCellValue("FechadeEntrega", txtFechaEntrega.Text);
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

                    if (swExportacion.IsOn == true)
                    { iva = Math.Round(imp * (piva / 100), 2); }
                    else { iva = 0; }

                    ieps = Math.Round(imp * (pieps / 100), 2);
                    NETO = iva + ieps + imp;
                    gridViewDetalle.SetFocusedRowCellValue("Importe", imp);
                    gridViewDetalle.SetFocusedRowCellValue("Iva", iva);
                    gridViewDetalle.SetFocusedRowCellValue("Neto", NETO);
                    gridViewDetalle.SetFocusedRowCellValue("TotalArticulo", NETO);
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
                    neto = neto + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "TotalArticulo"));
                    descuento = descuento + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Descuento"));
                    iepsNeto = iepsNeto + Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "IEPS"));

                }

                RetPIva = Math.Round(importe * (PtjeRetIva / 100), 2);
                RetPIsr = Math.Round(importe * (PtjeRetIsr / 100), 2);
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
                decimal iva = 0;

                porcentaje = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(renglon, "Pdescuento"));
                importe = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(renglon, "Importe"));
                piva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(renglon, "Piva"));
                descuento = Math.Round(importe * (porcentaje / 100), 2);
                iva = Math.Round((importe - descuento) * (piva / 100), 2);
                neto = Math.Round(importe - descuento + iva, 2);

                gridViewDetalle.FocusedRowHandle = renglon;

                gridViewDetalle.SetFocusedRowCellValue("Iva", iva); //Le ponemos el valor a la celda Iva del grid
                gridViewDetalle.SetFocusedRowCellValue("Neto", neto); //Le ponemos el valor a la celda neto del grid
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



            }

            catch (Exception ex)

            {

                //MessageBox.Show("cboFamilias: " + ex.Message);

            }
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
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
            bbiEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navBarControl.Visible = true;
            cboCliente.ReadOnly = false;
            navBarControl.Visible = true;
            ribbonStatusBar.Visible = true;

            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;

            intFolio = 0;
            strSerie = null;


        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            string strStatus = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Estado"));
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            intStatus = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status"));

            if (strStatus == "Registrada")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else { bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never; }
            
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

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                strStatus = "Cancelar";

                ribbonPageGroupCancelar.Visible = true;
                ribbonPageGroup1.Visible = false;
                navBarControl.Visible = false;
                navigationFrame.SelectedPageIndex = 4;
                txtRazonCancelacion.Text = string.Empty;
                LlenarGridbitacora();
            }
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPedidos" +
                "";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();

            clg.strGridLayout = "gridPedidosBitacora" +
                "";
            result = clg.SaveGridLayout(gridViewBitacora);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
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

        private void bbiVista_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ShowRibbonPrintPreview();
        }

        private void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
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

        private void cboCondicionesdepago_EditValueChanged(object sender, EventArgs e)
        {
            //    try
            //    {
            //        //proceso de DE para obtener valor del combo
            //        DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;

            //        DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

            //        Object value = row["Clave"];

            //        intClienteID = Convert.ToInt32(value);

            //    }

            //    catch (Exception ex)

            //    {

            //        //MessageBox.Show("cboFamilias: " + ex.Message);

            //    }
        }

        private void bbiProceder_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (strStatus == "Cancelar")
            {
                if (txtRazonCancelacion.Text == "")
                {
                    MessageBox.Show("la razon de cancelacion no puede ir vacio");
                }
                else
                {
                    AceptarCambio();
                }

            }

        }

        private void bbiCerrarCancelarr_ItemClick(object sender, ItemClickEventArgs e)
        {
            ribbonPageGroupCancelar.Visible = false;
            ribbonPageGroup1.Visible = true;
            navigationFrame.SelectedPageIndex = 0;
            strSerie = null;
            intFolio = 0;
            LlenarGrid();
            bbiProceder.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            navigationFrame.SelectedPageIndex = 4;
            splitContainerControl4.Panel1.Visible = false;
        }
    }
}