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
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using VisualSoftErp.Clases.VentasCLs;
using VisualSoftErp.Operacion.Ventas.Designers;
using VisualSoftErp.Operacion.Ventas.Clases;
using VisualSoftErp.Clases.HerrramientasCLs;
using VisualSoftErp.Operacion.Inventarios.Clases;
using DevExpress.XtraGrid;
using DevExpress.XtraNavBar;
using System.Collections;
using VisualSoftErp.Operacion.Ventas.Formas;

namespace VisualSoftErp.Catalogos.Ventas
{
    public partial class Pedidos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        decimal descuentoBase;
        bool blnSincreditodisponible;
        int intBajocosto;
        int intPlazo;
        int intLista;
        int intFolio;
        DateTime dfecha;
        int intExistenciaNegativa;
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
        string strOrigen = string.Empty;
        decimal subtotal;               
        decimal dPIva;
        decimal dPIeps;
        decimal PtjeRetIsr;
        decimal PtjeRetIva;
        decimal RetPIva;
        decimal RetPIsr;
        int intUsuarioID = globalCL.gv_UsuarioID;
        string strSerieCot = string.Empty;
        int intFolioCot;
        int AñoFiltro;
        int MesFiltro;
        decimal vencido = 0;
        int Transporte = 0;
        string addenda = string.Empty;
        int sucursal = 0;
        string strAccionReversa = string.Empty;
        int intExportar = 0;
        bool blnHaySuc;
        bool huboCambiodeprecio = false;
        bool blnPuedeCambiarPrecios = false;

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

            gridViewCotizaciones.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewCotizaciones.OptionsSelection.MultiSelect = false;
            gridViewCotizaciones.OptionsBehavior.ReadOnly = true;
            gridViewCotizaciones.OptionsBehavior.Editable = false;
            
            CargaCombos();

            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;
            LlenarGrid(AñoFiltro, MesFiltro);

            AgregaAñosNavBar();

            Permisosdeusuario();

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
                    navBarGroupAño.ItemLinks.Add(item1);
                    item1 = new NavBarItem();
                   
                }

                navBarControl.ActiveGroup = navBarControl.Groups[0];

            }
            catch(Exception ex)
            {
                MessageBox.Show("AgregaAñosNavBar:" + ex.Message);
            }
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
        private void LlenarGrid(int Año,int Mes)///gridprincipal
        {
            PedidosCL cl = new PedidosCL();

            cl.intAño = Año;
            cl.intMes = Mes;
            gridControlPrincipal.DataSource = cl.PedidosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPedidos";
            clg.restoreLayout(gridViewPrincipal);
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
            gridViewPrincipal.ViewCaption = "Pedidos de " + clg.NombreDeMes(Mes,3) + " del " + Año.ToString();

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
            //cboCliente.Properties.Columns["Clave"].Visible = false;
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
            cboCliente.Properties.Columns["DescuentoBase"].Visible = false;
            cboCliente.Properties.Columns["DesctoPP"].Visible = false;
            cboCliente.Properties.Columns["TransportesID"].Visible = false;
            cboCliente.Properties.Columns["Addenda"].Visible = false;
            cboCliente.Properties.Columns["Desglosardescuentoalfacturar"].Visible = false;
            cboCliente.Properties.Columns["CanalesdeventaID"].Visible = false;

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
            public decimal Preciodelista { get; set; }
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
            lblFac.Visible = false;
            strOrigen = "Nuevo";
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
            bbiCotizaciones.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navBarControl.Visible = false;

            lblRazonCambioPrecio.Visible = false;
            txtRazonCambioPrecio.Visible = false;
            txtRazonCambioPrecio.Text = String.Empty;

            navigationFrame.SelectedPageIndex = 1;
            blNuevo = true;
            cboCliente.ReadOnly = false;
            gridControlDetalle.Enabled = false;
            BuscarSerie();
            SiguienteID();
            lblStatus.Visible = false;
            intFolioCot = 0;
            cboSuc.ItemIndex = -1;
            lblOrdenTienda.Text = string.Empty;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }//Nuevo

        private string BuscarSerie()
        {
            SerieCL cl = new SerieCL();
            cl.intUsuarioID = intUsuarioID;
            String Result = cl.BuscarSerieporUsuario();
            if (Result == "OK")
            {
                //if (cl.strSerie == "")
                //{ cboSerie.ReadOnly = false; }
                //else
                //{
                    cboSerie.EditValue = cl.strSerie;
                    cboSerie.ReadOnly = true;
                //}

            }
            else
            {
                MessageBox.Show(Result);
            }
            return "";
        } //BuscaSerie

        private void LimpiaCajas()
        {
            //txtRazonCambioPrecio.Text = string.Empty;
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
            sucursal = 0;
            cboCanalVentas.EditValue = null;
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
            try
            {
                decimal pu;
                decimal pl;
                huboCambiodeprecio = false;

                if (blnHaySuc)
                {
                        if (cboSuc.ItemIndex == -1)
                        {                            
                            return "Seleccione una sucursal";                         
                        }                    
                } 
                else
                    sucursal = 0;

                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtIva.Text.Replace("$", "")))
                {
                    return "El iva está vacío";
                   
                }
                if (Convert.ToDecimal(txtIva.Text.Replace("$", "")) == 0)
                {
                    DialogResult dialogResult2 = MessageBox.Show("El iva es cero, desea continuar?",
                                "Advertencia", MessageBoxButtons.YesNo);
                    if (dialogResult2 == DialogResult.No)
                    {
                        return "";
                    }
                }



                string strNom = string.Empty;
                string result;
                if (cboSerie.Text.Length == 0)
                {
                    string serie = System.Configuration.ConfigurationManager.AppSettings["Serie"].ToString();
                    if (serie != cboSerie.Text)
                    {
                        return "El campo Serie no puede ir vacio";
                    }
                }
                if (txtFolio.Text.Length == 0)
                {
                    return "El campo folio no puede ir vacio";
                }
                if (txtOrdendecompra.Text.Length == 0)
                {
                    return "El campo órden de compra no puede ir vacio";
                }
                if (cboCanalVentas.EditValue == null)
                {
                    return "Capture el canal de venta";
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
                DatosdecontrolCL cld = new DatosdecontrolCL();
                result = cld.DatosdecontrolLeer();
                if (cld.iValidarcreditocliente == 1)
                {

                    clientesCL clc = new clientesCL();
                    clc.intClientesID = Convert.ToInt32(cboCliente.EditValue);
                    result = clc.clientesVerificaCreditoDisponible();
                    if (result != "OK")
                    {
                        return "No se puede verificar el crédito del cliente: " + result;
                    }
                    else
                    {
                        if (clc.intBloqueadoporcartera==1)
                        {
                            DialogResult dialogResultBlo = MessageBox.Show("Este cliente tiene bloqueado el crédito (no se podrá surtir), desea continuar?",
                                    "Advertencia", MessageBoxButtons.YesNo);
                            if (dialogResultBlo == DialogResult.No)
                            {
                                return "";
                            }
                        }

                        clg = new globalCL();
                        decimal netoPedido = Convert.ToDecimal(clg.Dejasolonumero(txtNeto.Text));
                        if (clc.decDisponible - netoPedido < 0)
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
                            if (clc.intFacturasvencidas > 0)
                            {
                                DialogResult dialogResult = MessageBox.Show("El cliente tiene " + clc.intFacturasvencidas.ToString() + ", desea continuar?",
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
                                blnSincreditodisponible = false;
                            }
                        }
                    }
                }




                //-------------------------------------------------   Se valida la existencia y el costo y el cambio de precios  ---------------------------------------
                intBajocosto = 0;
                intExistenciaNegativa = 0;
                decimal Cant = 0;
                decimal Precio = 0;
                bool PermitirExistenciaNegativa = true;
                


                if (cld.iPermitirexistencianegativa == 0)
                {
                    PermitirExistenciaNegativa = false;
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
                        pu = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Precio"));
                        pl = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Preciodelista"));

                        //Se valida precio cero
                        if (pu==0)
                        {
                            MessageBox.Show("El artículo " + strNom + " tiene precio cero");                            
                        }
                        //Se valida si el precio lo cambian
                        if (pl > pu)
                        {
                            huboCambiodeprecio = true;
                        }

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
                        if (cld.iValidarbajocosto == 1)
                        {
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
                }

                // Descomentar para ponerlo en productivo
                //if (huboCambiodeprecio)
                //{
                //    XtraInputBoxArgs args = new XtraInputBoxArgs();
                //    args.Caption = "Precios";
                //    args.Prompt = "Teclee la clave para cambiar precios";
                //    args.DefaultButtonIndex = 0;                    
                //    args.Showing += Args_Showing;

                //    var resultIB = XtraInputBox.Show(args).ToString();
                //    string x = resultIB.ToString();
                //}


                if (addenda != "0" && addenda != "Ninguna")
                {
                    PedidosCL cl = new PedidosCL();
                    cl.strSerie = cboSerie.EditValue.ToString();
                    cl.intFolio = Convert.ToInt32(txtFolio.Text);
                    cl.strAddenda = addenda;
                    result = cl.PedidosExisteAddenda();
                    if (result != "OK")
                        return "Este cliente ocupa Addenda y no se ha capturado";
                }

                
                result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month, "VTA");
                if (result == "C")
                {
                    return "Este mes está cerrado, no se puede actualizar";
                }

                return "OK";
            }
            catch(Exception ex)
            {
                int linenum = ex.LineNumber();
                return ex.Message + " LN:" + linenum.ToString();
            }
            
        } //Valida

        private void Args_Showing(object sender, XtraMessageShowingArgs e)
        {
            e.Form.Icon = this.Icon;
        }
        
        private void Guardar()
        {
           
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
                dtPedidos.Columns.Add("Sucursal", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("Fechareal", Type.GetType("System.DateTime"));
                dtPedidos.Columns.Add("Publicoengeneral", Type.GetType("System.Int32"));
                dtPedidos.Columns.Add("CanalesdeventaID", Type.GetType("System.Int32"));

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

                string art;
                int intSeq;
                decimal dCantidad;
                int intArticulosID;
                string strDescripcion;
                decimal dPrecio;
                decimal dPreciodelista;
                decimal dImporte;
                decimal dIvaImporte;
                DateTime FechadeEntregaArt;
                decimal dPorcentajededescuento = 0, dDescuento = 0, dPIeps, dIeps;
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
                        dPreciodelista = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Preciodelista"));                        
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
                            dPreciodelista, dIvaImporte, dImporte, dIvaPorcentaje,
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
                    pUsuariodepurado,
                    sucursal,
                    DateTime.Now,
                    swPublico.IsOn ? 1:0,
                    cboCanalVentas.EditValue
                    );
                
                PedidosCL cl = new PedidosCL();
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.dtm = dtPedidos;
                cl.dtd = dtPedidosDetalle;
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                cl.strMaquina = Environment.MachineName;
                cl.strSerieCot = strSerieCot;
                cl.intFolioCot = intFolioCot;
                cl.strRazonCambioPrecio = txtRazonCambioPrecio.Text;

                string result = cl.PedidosCrud();

                if (result == "OK")
                {
                    strSerie = cl.strSerie;
                    intFolio = cl.intFolio;
                    MessageBox.Show("Guardado correctamente");
                    origenImp = "Guardar";
                    Imprime();                    
                }
                else
                {
                    MessageBox.Show("Error al guardar:" + result);
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
            strOrigen = "Editar";
        }//ver

        private void BotonesEdicion()
        {

            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //bbiImpresionKit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            


            switch (intStatus)
            {
                case 1: case 2: case 3: case 4:
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
                lblFac.Text = "FAC:" + cl.Factura;
                lblFac.Visible = true;
                lblOrdenTienda.Text = cl.strOrdenTienda;
                txtFolio.Text = cl.intFolio.ToString();
                cboSerie.EditValue = cl.strSerie.ToString();
                cboCliente.EditValue = cl.intClientesID;
                txtOrdendecompra.Text = cl.strOc;
                txtPlazo.Text = cl.intPlazo.ToString();
                txtFecha.Text = cl.fFecha.ToShortDateString();
                cboCondicionesdepago.SelectedIndex = Convert.ToInt32(cl.strCondicionesdepago);
                cboCanalVentas.EditValue = cl.intCanalesdeventaID;

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

                if (cl.intFolioCot > 0)
                    lblCot.Text = "COTIZACIÓN: " + cl.strSerieCot + cl.intFolioCot.ToString();
                else
                    lblCot.Text = "";

                if (intExportar == 0 && swExportacion.IsOn || intExportar==1 && !swExportacion.IsOn)
                {

                    DialogResult dialogResult = MessageBox.Show("Este pedido está marcado para exportación, y el cliente no tiene habilitado Exportacion, desea poder cambiarlo en el pedido?",
                                                              "Advertencia", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        swExportacion.IsOn = false;
                        swExportacion.ReadOnly = true;
                    }
                    else
                    {
                        swExportacion.ReadOnly = false;
                    }

                    
                }

                swPublico.IsOn = cl.intPublicoengeneral == 1 ? true : false;

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
                Totales();

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

                globalCL clg = new globalCL();


                string result = clg.GM_CierredemodulosStatus(dfecha.Year, dfecha.Month, "VTA");
                if (result == "C")
                {
                    MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                    return;
                }

                PedidosCL cl = new PedidosCL();

                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intUsuarioID = globalCL.gv_UsuarioID; 
                cl.strMaquina = Environment.MachineName;
                cl.strRazon = txtRazonCancelar.Text;
                cl.strStatus = strStatus;

                

                result = cl.PedidosCambiarStatus();

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
                    LlenarGrid(AñoFiltro,MesFiltro);
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
            //gridViewPrincipal.ActiveFilter.Clear();
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
                Ribboncontrol1.MergeOwner.SelectedPage = Ribboncontrol1.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
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

        private void ImprimeProforma()
        {

            try
            {
                RibbonPagePrint.Visible = true;
                ribbonPage1.Visible = false;
                navigationFrame.SelectedPageIndex = 2;

                reporteProforma();
                navBarControl.Visible = false;
                Ribboncontrol1.MergeOwner.SelectedPage = Ribboncontrol1.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Imprime: " + ex.Message);
            }

        }
        private void reporteProforma()
        {
            try
            {
                PedidosImpresionProformaDesigner report = new PedidosImpresionProformaDesigner();
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


                    addenda= ((DataRowView)orow)["Addenda"].ToString();
                    if (addenda=="Ninguna")                    
                        bbiAddendas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    else
                        bbiAddendas.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                    intLista = Convert.ToInt32(((DataRowView)orow)["Listadeprecios"]);
                    intClienteID = Convert.ToInt32(((DataRowView)orow)["Clave"]);
                    intAgenteID = Convert.ToInt32(((DataRowView)orow)["AgentesID"]);
                    Transporte = Convert.ToInt32(((DataRowView)orow)["TransportesID"]);
                    cboCanalVentas.EditValue = Convert.ToInt32(((DataRowView)orow)["CanalesdeventaID"]);
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

                    cboTransportes.EditValue = Transporte;

                    intExportar = Convert.ToInt32(((DataRowView)orow)["Exportar"]);
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
                    descuentoBase = Convert.ToDecimal(((DataRowView)orow)["DescuentoBase"]);

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
                    CargaSucursales();
                    bbiInfoCxC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
            }

            catch (Exception ex)

            {

                MessageBox.Show("cboCliente_EditValueChanged: " + ex.Message);

            }

            
        }

        private void CargaSucursales()
        {
            try
            {
                

                combosCL cl = new combosCL();
                cl.strTabla = "ClientesSucursales";
                cl.iCondicion = intClienteID;
                cboSuc.Properties.ValueMember = "Clave";
                cboSuc.Properties.DisplayMember = "Des";
                DataTable dt = new DataTable();
                dt = cl.CargaCombos();
                if (dt.Rows.Count == 0)
                    blnHaySuc = false;
                else
                    blnHaySuc = true;

                cboSuc.Properties.DataSource = dt; // cl.CargaCombos();
                cboSuc.Properties.ForceInitialize();
                cboSuc.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboSuc.Properties.PopulateColumns();
                cboSuc.Properties.Columns["Clave"].Visible = false;
                cboSuc.Properties.NullText = "Seleccione una sucursal";

                //cboSuc.ItemIndex = 1;
                if (cboSuc != null)
                {

                    lblSuc.Visible = true;
                    cboSuc.Visible = true;
                }
                else
                {
                    lblSuc.Visible = false;
                    cboSuc.Visible = false;
                }
            }
            catch(Exception ex)
            {
                
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
                            gridViewDetalle.SetFocusedRowCellValue("Piva", cl.dPtjeIva);
                            gridViewDetalle.SetFocusedRowCellValue("Pieps", cl.dPtjeIeps);
                            gridViewDetalle.SetFocusedRowCellValue("FechadeEntrega", txtFechaEntrega.Text);
                            gridViewDetalle.SetFocusedRowCellValue("Pdescuento", descuentoBase);

                            //Se valida iva cero
                            if (cl.dPtjeIva==0)
                            {
                                DialogResult dialogResult = MessageBox.Show("Este artículo viene con % de iva cero, desea continuar?",
                                                              "Advertencia", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.No)
                                {
                                    return;
                                }
                            }
                           

                            //Se trae el precio
                            clp.strSerie = cboSerie.EditValue.ToString();
                            clp.iCliente = Convert.ToInt32(cboCliente.EditValue);
                            clp.iArtID = cl.intArticulosID;
                            clp.iLp = intLista;
                            result = clp.Precio();
                            if (result == "OK")
                            {
                                gridViewDetalle.SetFocusedRowCellValue("Precio", clp.dPrecio);
                                gridViewDetalle.SetFocusedRowCellValue("Preciodelista", clp.dPrecio);
                                
                                if (clp.strSeTomaPrecioPublico != "OK")
                                {
                                    MessageBox.Show(clp.strSeTomaPrecioPublico);
                                }
                            }
                            else
                            {
                                MessageBox.Show("CellValueChanged clp.Precio: " + result);
                            }
                        } else
                        {
                            MessageBox.Show("CellValueChanged LlenaCajas:" + result);
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
                    if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Pdescuento") == null)
                        gridViewDetalle.SetFocusedRowCellValue("Pdescuento", 0);
                    else
                        if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Pdescuento").ToString().Length==0)
                            gridViewDetalle.SetFocusedRowCellValue("Pdescuento", 0);
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
                string valor;

                if (gridViewDetalle.GetRowCellValue(renglon, "Pdescuento") == null)
                {
                    valor = "0";
                }
                else
                {
                    valor = gridViewDetalle.GetRowCellValue(renglon, "Pdescuento").ToString();
                }

                bool success = Decimal.TryParse(valor, out porcentaje);
                //porcentaje = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(renglon, "Pdescuento"));
                if (gridViewDetalle.GetRowCellValue(renglon, "Importe") == null)
                {
                    valor = "0";
                }
                else
                {
                    valor = gridViewDetalle.GetRowCellValue(renglon, "Importe").ToString();
                }
                success = Decimal.TryParse(valor, out importe);


                //importe = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(renglon, "Importe"));
                if (gridViewDetalle.GetRowCellValue(renglon, "Piva") == null)
                {
                    valor = "0";
                }
                else
                {
                    valor = gridViewDetalle.GetRowCellValue(renglon, "Piva").ToString();
                }
                success = Decimal.TryParse(valor, out piva);
                //piva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(renglon, "Piva"));

                if (swExportacion.IsOn)
                {
                    piva = 0;
                }

                descuento = Math.Round(importe * (porcentaje / 100), 4);
                iva = Math.Round((importe - descuento) * (piva / 100), 4);
                neto = Math.Round(importe - descuento + iva, 4);

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
            string result = Valida();
            if (result != "OK")
            {
                if (result.Length > 0)
                {
                    MessageBox.Show(result);
                }
                return;
            };

            if (huboCambiodeprecio)
            {
                if (txtRazonCambioPrecio.Text.Length == 0)
                {
                    MessageBox.Show("Teclee la razón del cambio de precio");
                    lblRazonCambioPrecio.Visible = true;
                    txtRazonCambioPrecio.Visible = true;
                }
                else
                    Guardar();

                
            }
            else
                Guardar();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (strOrigen=="Nuevo" || strOrigen=="Editar") 
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
                //bbiImpresionKit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiInfoCxC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiCotizaciones.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                navBarControl.Visible = true;
                cboCliente.ReadOnly = false;
                navBarControl.Visible = true;
           
                LlenarGrid(AñoFiltro,MesFiltro);
                navigationFrame.SelectedPageIndex = 0;

                intFolio = 0;
                strSerie = null;

            }
            else
            {
                strOrigen = "Nuevo";
                bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiCotizaciones.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiSelCot.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                navigationFrame.SelectedPageIndex = 1;
            }

        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
           
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            intStatus = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status"));
            lblStatus.Text = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Estado").ToString();
            dfecha = Convert.ToDateTime(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Fecha"));

            if (intStatus < 5)
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (intStatus==6)
            {
                bbiLiberar.Visibility= DevExpress.XtraBars.BarItemVisibility.Always;
                bbiProforma.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                bbiLiberar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiProforma.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                string status = string.Empty;
                GridView view = (GridView)sender;
                try
                {
                    if (view.GetRowCellValue(e.RowHandle, "Estado") == null)
                        status = "";
                    else
                        status = view.GetRowCellValue(e.RowHandle, "Estado").ToString();

                   
                }
                catch (Exception)
                {

                }

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
                strAccionReversa = "Cancelar";
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
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 1";
            MesFiltro = 1;
            LlenarGrid(AñoFiltro,MesFiltro);
        }

        private void navBarItemFeb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 2";
            MesFiltro = 2;
            LlenarGrid(AñoFiltro, MesFiltro);

        }

        private void navBarItemMar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
//            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 3";
            MesFiltro = 3;
            LlenarGrid(AñoFiltro, MesFiltro);

        }

        private void navBarItemAbr_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            MesFiltro = 4;
            LlenarGrid(AñoFiltro, MesFiltro);

            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 4";
        }

        private void navBarItemMay_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            MesFiltro = 5;
            LlenarGrid(AñoFiltro, MesFiltro);

            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 5";
        }

        private void navBarItemJun_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            MesFiltro = 6;
            LlenarGrid(AñoFiltro, MesFiltro);

            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 6";
        }

        private void navBarItemJul_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            MesFiltro = 7;
            LlenarGrid(AñoFiltro, MesFiltro);

            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 7";
        }

        private void navBarItemAgo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            MesFiltro = 8;
            LlenarGrid(AñoFiltro, MesFiltro);

            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 8";
        }

        private void navBarItemsep_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            MesFiltro = 9;
            LlenarGrid(AñoFiltro, MesFiltro);

            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 9";
        }

        private void navBarItemOct_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            MesFiltro = 10;
            LlenarGrid(AñoFiltro, MesFiltro);

            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 10";
        }

        private void navBarItemNov_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            MesFiltro = 11;
            LlenarGrid(AñoFiltro, MesFiltro);

            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 11";
        }

        private void navBarItemDic_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            MesFiltro = 12;
            LlenarGrid(AñoFiltro, MesFiltro);

            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 12";
        }

        private void navBarItemTodos_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            MesFiltro = 0;
            LlenarGrid(AñoFiltro, MesFiltro);

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
            LlenarGrid(AñoFiltro,MesFiltro);
            gridViewPrincipal.ActiveFilter.Clear();
            bbiProceder.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            bbiProceder.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            BotonesEdicion();
            bbiEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navBarControl.Visible = false;
            navigationFrame.SelectedPageIndex = 4;
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
                Ribboncontrol1.MergeOwner.SelectedPage = Ribboncontrol1.MergeOwner.TotalPageCategory.GetPageByText("Home");
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
                Ribboncontrol1.MergeOwner.SelectedPage = Ribboncontrol1.MergeOwner.TotalPageCategory.GetPageByText("Home");
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
                Ribboncontrol1.MergeOwner.SelectedPage = Ribboncontrol1.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
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
                if (txtPlazo.Text.Length == 0)
                    txtPlazo.Text = "0";
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            cierraPopCancelar();
        }

        private void btnAut_Click(object sender, EventArgs e)
        {

            if (txtRazonCancelar.Text.Length == 0)
            {
                MessageBox.Show("Teclee la razón");
                return;
            }

            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLogin.Text;
            clU.strClave = txtPassword.Text;

            if (strAccionReversa=="Cancelar")
                clU.strPermiso = "Cancelarpedidos";
            else
                clU.strPermiso = "Cancelarpedidossurtidos";
            string result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }

            

            cierraPopCancelar();

            if (strAccionReversa == "Cancelar")
                Cancelar();
            else
                LiberaSurtido();
        }

        private void LiberaSurtido()
        {
            PedidosCL cl = new PedidosCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            cl.intUsuarioID = globalCL.gv_UsuarioID;
            cl.strMaquina = Environment.MachineName;
            cl.strRazon = txtRazonCancelar.Text;
            string result = cl.PedidosLiberaSurtido();
            if (result == "OK")
            {
                MessageBox.Show("Liberado correctamente");
                bbiLiberar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                LlenarGrid(AñoFiltro,MesFiltro);
            }
            else
            {
                MessageBox.Show(result);
            }
            
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

            vencido = 0;
            gridControlCxC.DataSource = cl.ClientesAntiguedaddesaldos();

            decimal saldo;
            int dias;
            for (int i =0;i<=gridViewCxC.RowCount-1;i++)
            {
                dias = Convert.ToInt32(gridViewCxC.GetRowCellValue(i, "DiasVenc"));
                saldo = Convert.ToInt32(gridViewCxC.GetRowCellValue(i, "Importe"));
                if (dias>0)
                    vencido += saldo;
            }

            txtVencido.Text = vencido.ToString("c2");

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

        private void bbiCotizaciones_ItemClick(object sender, ItemClickEventArgs e)
        {
            CargarCotizaciones();
        }
        private void CargarCotizaciones()
        {
            try
            {
                strOrigen = "Cot";
                PedidosCL cl = new PedidosCL();
                cl.intClientesID = intClienteID;
                gridControlCotizaciones.DataSource = cl.CotizacionesPedidosGrid();
                navigationFrame.SelectedPageIndex = 5;
                bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiCotizaciones.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiSelCot.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewCotizaciones_RowClick(object sender, RowClickEventArgs e)
        {
            strSerieCot = gridViewCotizaciones.GetRowCellValue(gridViewCotizaciones.FocusedRowHandle, "Serie").ToString();
            intFolioCot = Convert.ToInt32(gridViewCotizaciones.GetRowCellValue(gridViewCotizaciones.FocusedRowHandle,"Folio"));
        }

        private void bbiSelCot_ItemClick(object sender, ItemClickEventArgs e)
        {
            LLenaPedidodeCot();
        }

        private void LLenaPedidodeCot()
        {
            try
            {
                PedidosCL cl = new PedidosCL();
                cl.intFolio = intFolioCot;
                cl.strSerie = strSerieCot;

                DataTable dt = new DataTable();
                dt = cl.CotizacionesDetalleGrid();

                string strMoneda = string.Empty;
               
               foreach (DataRow dr in dt.Rows)
               {
                    intClienteID = Convert.ToInt32(dr["ClientesID"]);
                    strMoneda = dr["MonedasID"].ToString();

                    break;
               }


                gridControlDetalle.DataSource = dt;

                cboCliente.EditValue = intClienteID;
                cboMonedas.EditValue = strMoneda;

                bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiCotizaciones.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiSelCot.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                Totales();
                navigationFrame.SelectedPageIndex = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCxCCerrar_Click_1(object sender, EventArgs e)
        {
            popUpCxC.Visible = false;
            popUpCxC.HidePopup();
        }

        private void navBarControl_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            globalCL clg = new globalCL();
            string Name = e.Link.ItemName.ToString();
            if (clg.esNumerico(Name))
            {
                AñoFiltro = Convert.ToInt32(Name);
                LlenarGrid(AñoFiltro, MesFiltro);
            }
            
            
        }

        private void gridViewCxC_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                
          
                GridView view = (GridView)sender;
                int dias = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "DiasVenc"));
                

                if (dias>0)
                {
                    e.Appearance.ForeColor = Color.Red;
                  
                }
                
            }
            catch (Exception)
            {
                e.Appearance.ForeColor = Color.Black;
            }

         
        }

        private void bbiAddendas_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (addenda)
            {
                case "6":                    
                    AbreAddendaCasaLey();
                    break;
                case "WalMart":
                    break;
                case "Soriana":
                    break;
                case "Amece":
                    break;
                case "7":   //Auto Zone
                    AbreAddendaZone();
                    break;
            }
        }

        private void AbreAddendaZone()
        {
            globalCL.gv_Serie = cboSerie.EditValue.ToString();
            globalCL.gv_Folio = Convert.ToInt32(txtFolio.Text);

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            DevExpress.XtraEditors.XtraForm frm = new ZoneAddenda();
            DialogResult dialogResult;
            dialogResult = frm.ShowDialog(this);
            frm.Dispose();
        }

        private void AbreAddendaCasaLey()
        {
            globalCL.gv_Serie = cboSerie.EditValue.ToString();
            globalCL.gv_Folio = Convert.ToInt32(txtFolio.Text);
           
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            DevExpress.XtraEditors.XtraForm frm = new CasaleyAddenda();
            DialogResult dialogResult;
            dialogResult = frm.ShowDialog(this);            
            frm.Dispose();
        }

        private void cboSuc_EditValueChanged(object sender, EventArgs e)
        {
            object orow = cboSuc.Properties.GetDataSourceRowByKeyValue(cboSuc.EditValue);
            if (orow != null)
            {                
                sucursal = Convert.ToInt32(((DataRowView)orow)["Clave"]);
            }
        }

        private void bbiLiberar_ItemClick(object sender, ItemClickEventArgs e)
        {
            strAccionReversa = "Liberar";
            popUpCancelar.Visible = true;
            txtLogin.Focus();
            txtRazonCancelar.Text = "";
            groupControl1.Text = "Liberar pedido " + strSerie + intFolio.ToString();
        }

        private void bbiProforma_ItemClick(object sender, ItemClickEventArgs e)
        {
            Proforma();
        }

        private void Proforma()
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Seleccione un renglón");
                return;
            }
            origenImp = "Principal";
            ImprimeProforma();
        }

        private void bbiCCE_ItemClick(object sender, ItemClickEventArgs e)
        {
            globalCL.gv_Serie = strSerie;
            globalCL.gv_Folio = intFolio;

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            DevExpress.XtraEditors.XtraForm frm = new ComercioExterior();
            DialogResult dialogResult;
            dialogResult = frm.ShowDialog(this);
            frm.Dispose();

        }

        private void btnRazonCerrar_Click(object sender, EventArgs e)
        {
            //popUpRazonCambioPrecio.Hide();
            //txtRazonCambioPrecio.Text = string.Empty;
        }

        private void btnRazonOk_Click(object sender, EventArgs e)
        {
            //if (txtRazonCambioPrecio.Text.Length==0)
            //{
            //    MessageBox.Show("Teclee la razón del cambio de precio");                
            //}
            //else
            //{
            //    popupRazon.Hide();
            //    Guardar();
            //}
        }

        private void Permisosdeusuario()
        {
            try
            {
                UsuariosCL clU = new UsuariosCL();
                clU.strLogin = globalCL.gv_UsuarioID.ToString();
                clU.strClave = txtPassword.Text;

               
                clU.strPermiso = "cambiarprecios";
                string result = clU.UsuariosPermisos();
                if (result != "OK")
                    blnPuedeCambiarPrecios = false;
                else
                    blnPuedeCambiarPrecios = true;
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiVista_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ShowRibbonPrintPreview();
        }
    }
}