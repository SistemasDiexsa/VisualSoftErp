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
using VisualSoftErp.Operacion.Ventas.Designers;
using VisualSoftErp.Operacion.Ventas.Clases;
using VisualSoftErp.Clases.HerrramientasCLs;
using VisualSoftErp.Operacion.Inventarios.Clases;
using DevExpress.XtraGrid;

namespace VisualSoftErp.Catalogos.Ventas
{
    public partial class Pedidos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        bool blnSincreditodisponible;
        int intPlazo;
        int intLista;
        int intFolio;
        int intExistenciaNegativa;
        int intBajocosto;
        int intCxC;
        string strSerie;
        string origenImp;
        string strStatus = string.Empty;

        public BindingList<detalleCL> detalle;
        int intClienteID;
        int intAgenteID;

        public bool blNuevo;
        int intStatus;
        decimal dIvaPorcentaje;


        decimal subtotal;
                

        decimal dPIva;
        decimal dPIeps;

        decimal PtjeRetIsr;
        decimal PtjeRetIva;
        decimal RetPIva;
        decimal RetPIsr;

        

        int intUsuarioID = globalCL.gv_UsuarioID;
        
        public Pedidos()
        {
            InitializeComponent();


            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPedidosDetalle";
            clg.restoreLayout(gridViewDetalle);
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


        private void MedioAmbiente()
        {
            DatosdecontrolCL cl = new DatosdecontrolCL();
            string result = cl.DatosdecontrolLeer();
            if (result == "OK")
            {
                if (cl.iCambiarelagentealvender == 1)
                    cboAgente.Enabled = true;
                else
                    cboAgente.Enabled = false;
            }
        }
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

            
            cl.ActualizaCombo(cboAgente, "Agentes", "");



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
            //repositoryItemLookUpEditNoAprobacion.Properties.NullText = "Seleccione un articulo";

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
            gridControlDetalle.ForceInitialize();

           


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
            public decimal TotalArticulo { get; set; }
            public string FechadeEntrega { get; set; }
        }

        private void Nuevo()
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Configurando detalle", "espere por favor");
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
            barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiImpresionKit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navBarControl.Visible = false;
            
            navigationFrame.SelectedPageIndex = 1;
            blNuevo = true;
            cboCliente.ReadOnly = false;
            gridControlDetalle.Enabled = false;
            BuscarSerie();
            SiguienteID();
            lblStatus.Visible = false;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
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
            cboAgente.EditValue = null;
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
            txtFechaEntrega.Text = DateTime.Now.ToShortDateString();
            bbiInfoCxC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            string strNom = string.Empty;
            string result;
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
                result = cl.BuscarOrdendecompra();
                // MessageBox.Show(result);
                if (result == "OK")
                {

                    if (cl.strOc == txtOrdendecompra.Text)
                    {
                        DialogResult dialogResult = MessageBox.Show("Ya existe una orden de compra para este cliente: " + cl.strOrigen + " " + cl.strSerie + cl.intFolio.ToString(),
                            "Advertencia", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {                       
                            return "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Valida/Buscarordendecompra:" + ex);
            }

            //Se verifica el crédito del cliente
            intCxC = 0;
            clientesCL clc = new clientesCL();
            clc.intClientesID = Convert.ToInt32(cboCliente.EditValue);
            result = clc.clientesVerificaCreditoDisponible();
            if (result!="OK")
            {
                return "No se pude verificar el crédito del cliente: " + result;
            }
            else
            {

                globalCL clg = new globalCL();

                decimal netoPedido = Convert.ToDecimal(clg.Dejasolonumero(txtNeto.Text));
                if (clc.decDisponible-netoPedido<0)
                {
                    DialogResult dialogResult = MessageBox.Show("El crédito disponible del cliente no cubre el Neto de este pedido, desea continuar?",
                            "Advertencia", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        return "";
                    }

                    blnSincreditodisponible = true;
                    intCxC = 1;
                }
                else
                {
                    if (clc.intFacturasvencidas>0) {
                        DialogResult dialogResult = MessageBox.Show("El cliente tiene " + clc.intFacturasvencidas.ToString() + ", desea continuar?",
                            "Advertencia", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            return "";
                        }

                        blnSincreditodisponible = true;
                        intCxC = 1;

                    }
                    else {
                        blnSincreditodisponible = false;
                    }
                }
            }

            //-------------------------------------------------   Se valida la existencia y el costo  ---------------------------------------
            intBajocosto = 0;
            intExistenciaNegativa = 0;
            decimal Cant = 0;
            decimal Precio = 0;
            bool PermitirExistenciaNegativa = true;
            DatosdecontrolCL cld = new DatosdecontrolCL();
            result = cld.DatosdecontrolLeer();
            if (result == "OK")
            {
                if (cld.iPermitirexistencianegativa==0)
                {
                    PermitirExistenciaNegativa = false;
                }
            }

            
                int iArt = 0;
                ExistenciasCL cle = new ExistenciasCL();
                cle.intAlm = Convert.ToInt32(cboAlmacen.EditValue);

                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    if (gridViewDetalle.GetRowCellValue(i, "Articulo") != null)
                    {
                        strNom = gridViewDetalle.GetRowCellValue(i, "Des").ToString();
                        iArt = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Articulo"));
                        cle.intArt = iArt;
                        if (!PermitirExistenciaNegativa)
                        {
                            result = cle.ExistenciaDisponible();
                            if (result == "OK")
                            {
                                Cant = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Cantidad"));
                                if (Cant > cle.decExDisponible)
                                {
                                    DialogResult dialogResult = MessageBox.Show("La existencia disponible: " + cle.decExDisponible.ToString() + " del artículo: " + strNom + " no es suficiente, desea continuar? ",
                                "Advertencia", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.No)
                                    {
                                        return "";
                                    }

                                    intExistenciaNegativa = 1;
                                }
                            }
                            else
                            {
                                return "Al leer existencia negativa: " + result;
                            }
                        } //Existencia negativa

                        //------ Costo bajo ----
                        result = cle.UltimoCosto();
                        if (result == "OK")
                        {
                            Precio = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Precio"));
                            if (Precio > cle.decCostoconseguridad)
                            {
                                DialogResult dialogResult = MessageBox.Show("El precio del artículo: " + strNom + " es mayor al costo de seguridad",
                           "Advertencia", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.No)
                                {
                                    return "";
                                }

                                intBajocosto = 1;
                            }
                        }
                        else
                        {
                            return "No se pudo leer el último costo";
                        }
                    }                
            }

                return "OK";
        } //Valida

        private void Guardar()
        {
            string result = Valida();
            if (result != "OK")
            {
                if (result.Length>0)
                {
                    MessageBox.Show(result);
                }                
                return;
            };
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
                int pStatusexistencia = intExistenciaNegativa;
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

                int pStatuscxc = intCxC;
                int pStatusexistenciaparcial = 0;
                int pStatusbajocosto = 0;
                int pDepurado = 0;
                int pUsuariodepurado = 0;

                int intClienteID = Convert.ToInt32(cboCliente.EditValue);

                int intStatus = 4; //Listo para surtir
                if (blnSincreditodisponible)
                {
                    intStatus = 1;  //Detenido por crédito
                    pStatuscxc = 1;
                }

                string strMonedasID = cboMonedas.EditValue.ToString();

                int intExportacion = swExportacion.IsOn ? 1 : 0;
                int pAlmacenesID = Convert.ToInt32(cboAlmacen.EditValue);

                DateTime FechaCancelacion = Convert.ToDateTime(txtFecha.Text);
                string strRazonCancelacion = "", pRazondepurado = "";
                string pObservaciones = txtObservaciones.Text;
                DateTime pFechaestimadadeentrega = Convert.ToDateTime(txtFechaEntrega.Text);


                
                decimal pTipodecambio = 1;

                string strCondicionesdepago = cboCondicionesdepago.SelectedIndex.ToString();
                intPlazo = Convert.ToInt32(txtPlazo.Text);

                dtPedidos.Rows.Add(
                    cboSerie.EditValue, 
                    Convert.ToInt32(txtFolio.Text), 
                    Convert.ToDateTime(txtFecha.Text),
                    intClienteID, 
                    intAgenteID, 
                    dSubTotal, 
                    dIvaTotal,
                    RetPIva, 
                    dIepsTotal, 
                    dNetoTotal, 
                    dDescuento, 
                    pObservaciones,
                    strCondicionesdepago, 
                    intPlazo, 
                    intStatus, 
                    Convert.ToDateTime(txtFecha.Text),
                    strRazonCancelacion, 
                    intUsuarioID, 
                    cboTransportes.EditValue,
                    strMonedasID, 
                    pAlmacenesID, 
                    txtOrdendecompra.Text,
                    pFechaestimadadeentrega, 
                    pTipodecambio, 
                    intExportacion,
                    pStatuscxc, 
                    pStatusexistencia, 
                    pStatusexistenciaparcial,
                    pStatusbajocosto, 
                    pDepurado, 
                    pRazondepurado,
                    Convert.ToDateTime(txtFecha.Text), 
                    pUsuariodepurado);


                PedidosCL cl = new PedidosCL();
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.dtm = dtPedidos;
                cl.dtd = dtPedidosDetalle;
                cl.intUsuarioID = 1;
                cl.strMaquina = Environment.MachineName;

                result = cl.PedidosCrud();

                if (result == "OK")
                {
                    strSerie = cl.strSerie;
                    intFolio = cl.intFolio;
                    MessageBox.Show("Guardado correctamente");
                    origenImp = "Guardar";
                    Imprime();                    
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
            bbiInfoCxC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
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
            bbiImpresionKit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            


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

                lblStatus.Visible = true;

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
            //DialogResult result = MessageBox.Show("Desea " + strStatus + " el Folio: " + intFolio.ToString(), strStatus, MessageBoxButtons.YesNo);
            //if (result.ToString() == "Yes")
            //{
            //    Cancelar();
            //}
        }//AceptarCambio 

        private void Cancelar()
        {
            try
            {

                PedidosCL cl = new PedidosCL();

                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intUsuarioID = globalCL.gv_UsuarioID; 
                cl.strMaquina = Environment.MachineName;
                cl.strRazon = txtRazonCancelar.Text;
                cl.strStatus = strStatus;

                

                string result = cl.PedidosCambiarStatus();

                if (result == "OK")
                {
                    MessageBox.Show("Cancelado correctamente");
                    bbiProceder.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    txtRazonCancelar.Text = string.Empty;
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
            gridControlHistorial.DataSource = cl.PedidosBitacoraGrid(); //pendiente
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPedidosBitacora";
            clg.restoreLayout(gridViewBitacora);
            //Global, manda el nombre del grid para la clase Global           
        } //LlenarGrid()

        private void Imprime()
        {

            try
            {
                RibbonPagePrint.Visible = true;
                ribbonPage1.Visible = false;
                navigationFrame.SelectedPageIndex = 2;
               
                reporte();
                navBarControl.Visible = false;
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Imprime: " + ex.Message);
            }

        }

        private void reporte()
        {
            try
            {
                Pedidosformatoimpresion report = new Pedidosformatoimpresion();
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

        private void reportekit()
        {
            try
            {
                PedidosFormatoImpresionKits report = new PedidosFormatoImpresionKits();
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
                object orow = cboCliente.Properties.GetDataSourceRowByKeyValue(cboCliente.EditValue);
                if (orow != null)
                {

                    gridControlDetalle.Enabled = true;

                    intLista = Convert.ToInt32(((DataRowView)orow)["Listadeprecios"]);
                    intClienteID = Convert.ToInt32(((DataRowView)orow)["Clave"]);
                    intAgenteID = Convert.ToInt32(((DataRowView)orow)["AgentesID"]);
                    cboAgente.EditValue = intAgenteID;

                    int intPlazo = Convert.ToInt32(((DataRowView)orow)["Plazo"]);
                    if (intPlazo == 0)  //Contado
                    {
                        cboCondicionesdepago.ReadOnly = true;
                        cboCondicionesdepago.SelectedIndex = 1;

                    }
                    else
                    {
                        cboCondicionesdepago.ReadOnly = false;
                        cboCondicionesdepago.SelectedIndex = 0;

                    }
                    txtPlazo.Enabled = false;
                    txtPlazo.Text = intPlazo.ToString();


                    int intExportar = Convert.ToInt32(((DataRowView)orow)["Exportar"]);
                    if (intExportar == 0)
                    {
                        swExportacion.IsOn = false;
                        swExportacion.ReadOnly = true;
                    }
                    else
                    {
                        swExportacion.IsOn = true;
                        swExportacion.ReadOnly = false;
                    }

                    dPIva = Convert.ToDecimal(((DataRowView)orow)["PIva"]);
                    dPIeps = Convert.ToDecimal(((DataRowView)orow)["PIeps"]);
                    PtjeRetIva = Convert.ToDecimal(((DataRowView)orow)["PRetiva"]);
                    PtjeRetIsr = Convert.ToDecimal(((DataRowView)orow)["PRetIsr"]);

                    string moneda = ((DataRowView)orow)["Moneda"].ToString();
                    cboMonedas.EditValue = moneda;

                    DatosdecontrolCL cld = new DatosdecontrolCL();
                    string result = cld.DatosdecontrolLeer();
                    if (result == "OK")
                    {
                        if (cld.iCambiarelagentealvender == 1)
                        {
                            cboAgente.Enabled = true;
                        }
                        else
                        {
                            cboAgente.Enabled = false;
                        }
                    }

                    bbiInfoCxC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
            }

            catch (Exception ex)

            {

                MessageBox.Show("cboCliente_EditValueChanged: " + ex.Message);

            }

            
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                
                //Sacamos datos del artículo
                if (e.Column.Name == "gridColumnArticulo")
                {
                    PreciosCL clp = new PreciosCL();
                    articulosCL cl = new articulosCL();
                    string art = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulo").ToString();
                    if (art.Length > 0) //validamos que haya algo en la celda
                    {
                        cl.intArticulosID = Convert.ToInt32(art);
                        string result = cl.articulosLlenaCajas();
                        if (result == "OK")
                        {

                            if (swExportacion.IsOn == true)
                            {
                                cl.intPtjeIva = 0;
                                cl.intPtjeIeps = 0;
                            }

                            gridViewDetalle.SetFocusedRowCellValue("Des", cl.strNombre);
                            gridViewDetalle.SetFocusedRowCellValue("Um", cl.strUM);
                            gridViewDetalle.SetFocusedRowCellValue("Piva", cl.intPtjeIva);
                            gridViewDetalle.SetFocusedRowCellValue("Pieps", cl.intPtjeIeps);
                            gridViewDetalle.SetFocusedRowCellValue("FechadeEntrega", txtFechaEntrega.Text);

                            //Se trae el precio
                            clp.strSerie = cboSerie.EditValue.ToString();
                            clp.iCliente = Convert.ToInt32(cboCliente.EditValue);
                            clp.iArtID = cl.intArticulosID;
                            clp.iLp = intLista;
                            result = clp.Precio();
                            if (result == "OK")
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

                    if (swExportacion.IsOn == false)
                    {
                        iva = Math.Round(imp * (piva / 100), 2);
                    }
                    else
                    {
                        iva = 0;
                        piva = 0;
                        ieps = 0;
                        pieps = 0;
                    }

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
                txtNeto.Text = neto.ToString("c2");


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

        private void cierraPopCancelar()
        {
            popUpCancelar.Visible = false;
            txtLogin.Text = string.Empty;
            txtPassword.Text = string.Empty;
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

                if (swExportacion.IsOn)
                {
                    piva = 0;
                }

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
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPedidosDetalle";
            string result = clg.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
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
            barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiImpresionKit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiInfoCxC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navBarControl.Visible = true;
            cboCliente.ReadOnly = false;
            navBarControl.Visible = true;
           
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;

            intFolio = 0;
            strSerie = null;


        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
           
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            intStatus = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status"));
            lblStatus.Text = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Estado").ToString();

            if (intStatus < 5)
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                string status = view.GetRowCellValue(e.RowHandle, "Estado").ToString();

                if (status == "Cancelado")
                {
                    e.Appearance.ForeColor = Color.Red;
                }

            }
            catch (Exception)
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

                popUpCancelar.Visible = true;
                txtLogin.Focus();
                txtRazonCancelar.Text = "";
                groupControl1.Text = "Cancelar pedido " + strSerie + intFolio.ToString();

                //strStatus = "Cancelar";

                //ribbonPageGroupCancelar.Visible = true;
                //ribbonPageGroup1.Visible = false;
                //navBarControl.Visible = false;
                //navigationFrame.SelectedPageIndex = 3;
                //txtRazonCancelacion.Text = string.Empty;
                //LlenarGridbitacora();
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
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Configurando detalle", "espere por favor...");
                Editar();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
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

        

        private void bbiCerrarCancelarr_ItemClick(object sender, ItemClickEventArgs e)
        {
           // ribbonPageGroupCancelar.Visible = false;
            navBarControl.Visible = true;
            ribbonPageGroup1.Visible = true;
            navigationFrame.SelectedPageIndex = 0;
            strSerie = null;
            intFolio = 0;
            LlenarGrid();
            bbiProceder.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            bbiProceder.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            BotonesEdicion();
            bbiEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navBarControl.Visible = false;
            navigationFrame.SelectedPageIndex = 5;
            txtRazonCancelar.Text = string.Empty;
            LlenarGridbitacora();
        }

        private void bbiIImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }
            origenImp = "Principal";
            Imprime();
        }

        private void bbiRegresarImp_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (origenImp == "Principal")
            {
                navigationFrame.SelectedPageIndex = 0;

                navBarControl.Visible = true;
              
                ribbonPage1.Visible = true;
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText("Home");
                RibbonPagePrint.Visible = false;
            }
            else
            {
                //limpiar variables universales
                dPIva = 0;
                dPIeps = 0;
                PtjeRetIsr = 0;
                PtjeRetIva = 0;
                RetPIsr = 0;
                RetPIva = 0;
                //LimpiaCajas();
                //Inicialisalista();
                ribbonPage1.Visible = true;
                //Limpiaca cajas de texto del detalle
                //Limipas el grid del detalle Inicialista
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText("Home");
                if (blNuevo)
                {
                    Nuevo();
                }
                else
                {
                    Editar();
                }
                
            }
            
        }

        private void bbiImpresionKit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }

            Imprimekit();
        }

        private void Imprimekit()
        {
            try
            {
                RibbonPagePrint.Visible = true;
                ribbonPage1.Visible = false;
                navigationFrame.SelectedPageIndex = 2;
               
                reportekit();
                navBarControl.Visible = false;
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Imprime: " + ex.Message);
            }
        }

        private void cboCondicionesdepago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCondicionesdepago.SelectedIndex == 0)
            {
                txtPlazo.Text = intPlazo.ToString();
            }
            else
            {
                intPlazo = Convert.ToInt32(txtPlazo.Text);
                txtPlazo.Text = "0";
            }
        }

        private void cboAgente_EditValueChanged(object sender, EventArgs e)
        {
            object orow = cboAgente.Properties.GetDataSourceRowByKeyValue(cboAgente.EditValue);
            if (orow != null)
            {
                intAgenteID = Convert.ToInt32(((DataRowView)orow)["Clave"]);
            }
        }

        private void gridControlPrincipal_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            cierraPopCancelar();
        }

        private void btnAut_Click(object sender, EventArgs e)
        {

            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLogin.Text;
            clU.strClave = txtPassword.Text;
            clU.strPermiso = "Cancelarpedidos";
            string result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }

            if (txtRazonCancelar.Text.Length==0)
            {
                MessageBox.Show("Teclee la razón de cancelación");
                return;
            }

            cierraPopCancelar();
            Cancelar();
        }

        private void bbiInfoCxC_ItemClick(object sender, ItemClickEventArgs e)
        {
            InformacionCxC();
        }

        private void InformacionCxC()
        {
            clientesCL cl = new clientesCL();
            cl.intClientesID = Convert.ToInt32(cboCliente.EditValue);
            string result = cl.clientesVerificaCreditoDisponible();
            if (result != "OK")
            {
                MessageBox.Show("No se pudo verificar el crédito del cliente");
                return;
            }



            globalCL clg = new globalCL();
            if (!clg.esNumerico(clg.Dejasolonumero(txtNeto.Text)))
            {
                txtNeto.Text = "0";
            }

            txtCxCPlazo.Text = txtPlazo.Text;
            txtCxCCreAut.Text = cl.decCreditoAutorizado.ToString("c2");
            txtCxCPSSinfac.Text = cl.decPedidosSurtidosSinFacturar.ToString("c2");
            txtCxCEstePedido.Text = Convert.ToDecimal(clg.Dejasolonumero(txtNeto.Text)).ToString("c2");
            txtCxCCxC.Text = cl.decCxC.ToString("c2");

           
            decimal Disponible = cl.decDisponible - Convert.ToDecimal(clg.Dejasolonumero(txtNeto.Text));
            txtCxCDisponible.Text = Disponible.ToString("c2");

            gridControlCxC.DataSource = cl.ClientesAntiguedaddesaldos();

            Rectangle rect = Screen.GetWorkingArea(this);
            popUpCxC.ShowPopup(new Point(rect.Width / 2 - popUpCxC.Width / 2, rect.Height / 2 - popUpCxC.Height / 2));
            popUpCxC.Visible = true;


        }

        private void btnCxCCerrar_Click(object sender, EventArgs e)
        {            
            popUpCxC.Visible = false;
            popUpCxC.HidePopup();
        }

      
        private void officeNavigationBar1_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            switch (e.Item.Name.ToString())
            {
                case "nbiCxC":
                    gridViewPrincipal.ActiveFilterString = "[Status]=1";
                    break;
                case "nviSinexistencia":
                    gridViewPrincipal.ActiveFilterString = "[Status]=2";
                    break;
                case "nviBajocosto":
                    gridViewPrincipal.ActiveFilterString = "[Status]=3";
                    break;
                case "nvisurtir":
                    gridViewPrincipal.ActiveFilterString = "[Status]=4";
                    break;
                case "nviSurtidosinfacturar":
                    gridViewPrincipal.ActiveFilterString = "[Status]=6";
                    break;
                case "nviFacturado":
                    gridViewPrincipal.ActiveFilterString = "[Status]=7";
                    break;
                case "nviEntregado":
                    gridViewPrincipal.ActiveFilterString = "[Status]=8";
                    break;
                case "nviCancelados":
                    gridViewPrincipal.ActiveFilterString = "[Status]=10";
                    break;
                case "nviTodo":
                    gridViewPrincipal.ActiveFilter.Clear();
                    break;
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
    }
}