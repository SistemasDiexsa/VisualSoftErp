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
using VisualSoftErp.Operacion.Ventas.Designers;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using VisualSoftErp.Clases.HerrramientasCLs;
using DevExpress.XtraPdfViewer.Bars;

namespace VisualSoftErp.Operacion.CxC.Formas
{
    public partial class Notasdecredito : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        decimal dPivaG, dPRetIvaG, dPRetIsrG;
        string strSerie;
        int intFolio;
        string strNCliente;
        int intClienteID;
        string strMonedasID;
        int intUsuarioID = globalCL.gv_UsuarioID;
        public BindingList<detalleCL> detalle;
        bool blNuevo;
        int intStatus;
        string strMoneda = string.Empty;
        DateTime dFecha;
        string strTo = string.Empty;
        int timbrado = 0;

        public Notasdecredito()
        {
            InitializeComponent();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Notas de credito";

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            LlenarGrid();
            CargaCombos();
            txtFecha.EditValue = DateTime.Now;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

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

        private void SiguienteID()
        {
           

            NotasdecreditoCL cl = new NotasdecreditoCL();
            cl.strSerie = cboSerie.EditValue.ToString();
            cl.strDoc = "Notasdecredito";
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

      

        private void CargaCombos()
        {


            combosCL cl = new combosCL();
            cl.strTabla = "SerieNCR";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Clave";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Des"].Visible = false;
            cboSerie.ItemIndex = 0;



            cl.strTabla = "Clientes";
            cboCliente.Properties.ValueMember = "Clave";
            cboCliente.Properties.DisplayMember = "Des";
            cboCliente.Properties.DataSource = cl.CargaCombos();
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCliente.Properties.PopulateColumns();
            cboCliente.Properties.Columns["Clave"].Visible = false;
            cboCliente.Properties.Columns["Clave"].Visible = false; //con esta propiedad puedo ocultar campos en el cbo
            cboCliente.Properties.Columns["AgentesID"].Visible = false;
            cboCliente.Properties.Columns["Plazo"].Visible = false;
            cboCliente.Properties.Columns["Listadeprecios"].Visible = false;
            cboCliente.Properties.Columns["Exportar"].Visible = false;
            cboCliente.Properties.Columns["cFormapago"].Visible = false;
            cboCliente.Properties.Columns["cMetodopago"].Visible = false;
            cboCliente.Properties.Columns["BancoordenanteID"].Visible = false;
            cboCliente.Properties.Columns["Cuentaordenante"].Visible = false;
            cboCliente.Properties.Columns["PIva"].Visible = false;
            cboCliente.Properties.Columns["PIeps"].Visible = false;
            cboCliente.Properties.Columns["PRetiva"].Visible = false;
            cboCliente.Properties.Columns["EMail"].Visible = false;
            cboCliente.Properties.Columns["UsoCfdi"].Visible = false;
            cboCliente.Properties.Columns["PRetIsr"].Visible = false;
            cboCliente.Properties.Columns["cFormapagoDepositos"].Visible = false;
            cboCliente.Properties.Columns["Moneda"].Visible = false;
            cboCliente.Properties.Columns["DescuentoBase"].Visible = false;
            cboCliente.Properties.Columns["DesctoPP"].Visible = false;
            cboCliente.Properties.Columns["Desglosardescuentoalfacturar"].Visible = false;
            cboCliente.Properties.Columns["TransportesID"].Visible = false;
            cboCliente.Properties.Columns["Addenda"].Visible = false;
            cboCliente.Properties.Columns["SerieEle"].Visible = false;
            cboCliente.ItemIndex = 0;


            cl.strTabla = "Agentes";
            cboAgentesID.Properties.ValueMember = "Clave";
            cboAgentesID.Properties.DisplayMember = "Des";
            cboAgentesID.Properties.DataSource = cl.CargaCombos();
            cboAgentesID.Properties.ForceInitialize();
            cboAgentesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgentesID.Properties.PopulateColumns();
            cboAgentesID.Properties.Columns["Clave"].Visible = false;

            cl.strTabla = "Monedas";
            cboMonedasID.Properties.ValueMember = "Clave";
            cboMonedasID.Properties.DisplayMember = "Des";
            cboMonedasID.Properties.DataSource = cl.CargaCombos();
            cboMonedasID.Properties.ForceInitialize();
            cboMonedasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedasID.Properties.PopulateColumns();
            cboMonedasID.Properties.Columns["Clave"].Visible = false;
            cboMonedasID.ItemIndex = 0;

            cl.strTabla = "ObservacionesNCR";
            cboMotivo.Properties.ValueMember = "Clave";
            cboMotivo.Properties.DisplayMember = "Des";
            cboMotivo.Properties.DataSource = cl.CargaCombos();
            cboMotivo.Properties.ForceInitialize();
            cboMotivo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMotivo.Properties.PopulateColumns();
            cboMotivo.Properties.Columns["Clave"].Visible = false;
            cboMotivo.ItemIndex = 0;

            cl.strTabla = "TiposdemovimientoinvE";
            cboTipodemov.Properties.ValueMember = "Clave";
            cboTipodemov.Properties.DisplayMember = "Des";
            cboTipodemov.Properties.DataSource = cl.CargaCombos();
            cboTipodemov.Properties.ForceInitialize();
            cboTipodemov.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTipodemov.Properties.PopulateColumns();
            cboTipodemov.Properties.Columns["Clave"].Visible = false;

            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();
            cl.strTabla = "FormadePago";
            cboFormadePago.Properties.ValueMember = "Clave";
            cboFormadePago.Properties.DisplayMember = "Des";
            src.DataSource = cl.CargaCombos();
            cboFormadePago.Properties.DataSource = clg.AgregarOpcionc_FormaPago(src);
            cboFormadePago.Properties.ForceInitialize();
            cboFormadePago.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFormadePago.Properties.PopulateColumns();
            cboFormadePago.Properties.Columns["Clave"].Visible = false;
            cboFormadePago.EditValue = cboFormadePago.Properties.GetDataSourceValue(cboFormadePago.Properties.ValueMember, 0);

           
        }

        private void LlenarGrid()
        {
            NotasdecreditoCL cl = new NotasdecreditoCL();
            gridControlPrincipal.DataSource = cl.NotasdecreditoGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridNotasdecredito";
            clg.restoreLayout(gridViewPrincipal);
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
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

            gridColumnRetIva.OptionsColumn.ReadOnly = true;
            gridColumnRetIva.OptionsColumn.AllowFocus = false;

            gridColumnRetIsr.OptionsColumn.ReadOnly = true;
            gridColumnRetIsr.OptionsColumn.AllowFocus = false;

            gridColumnNeto.OptionsColumn.ReadOnly = true;
            gridColumnNeto.OptionsColumn.AllowFocus = false;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridNotasdecreditoDetalle";
            clg.restoreLayout(gridViewDetalle);
        }

        public class detalleCL
        {
            public decimal Cantidad { get; set; }
            public string Descripcion { get; set; }
            public string SerieCfdi { get; set; }
            public int FolioCfdi { get; set; }
            public decimal Precio { get; set; }
            public decimal Importe { get; set; }
            public decimal Iva { get; set; }
            public decimal RetIva { get; set; }
            public decimal RetIsr { get; set; }
            public decimal Neto { get; set; }
        }

        private void Nuevo()
        {
            ribbonPageGroup1.Visible = false;
            blNuevo = true;
            //BuscarSerie();
            SiguienteID();
            Inicialisalista();
            ribbonPageGroup2.Visible = true;
            ribbonStatusBar.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void LimpiaCajas()
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            cboCliente.EditValue = null;
            cboAgentesID.EditValue = null;
            cboMotivo.EditValue = null;
            cboMonedasID.EditValue = null;
            swDevolucion.IsOn = false;
            cboTipodemov.EditValue = null;
            txtSerideEntrada.Text = string.Empty;
            chkNoTimbrar.Checked = false;

  
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
                string sCondicion = String.Empty;
                System.Data.DataTable dtNotasdecredito = new System.Data.DataTable("Notasdecredito");
                dtNotasdecredito.Columns.Add("Serie", Type.GetType("System.String"));
                dtNotasdecredito.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtNotasdecredito.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtNotasdecredito.Columns.Add("ClientesID", Type.GetType("System.Int32"));
                dtNotasdecredito.Columns.Add("ObservacionesID", Type.GetType("System.Int32"));
                dtNotasdecredito.Columns.Add("AgentesID", Type.GetType("System.Int32"));
                dtNotasdecredito.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dtNotasdecredito.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtNotasdecredito.Columns.Add("RetIva", Type.GetType("System.Decimal"));
                dtNotasdecredito.Columns.Add("RetIsr", Type.GetType("System.Decimal"));
                dtNotasdecredito.Columns.Add("PIva", Type.GetType("System.Decimal"));
                dtNotasdecredito.Columns.Add("PRetIva", Type.GetType("System.Decimal"));
                dtNotasdecredito.Columns.Add("PRetIsr", Type.GetType("System.Decimal"));
                dtNotasdecredito.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dtNotasdecredito.Columns.Add("Status", Type.GetType("System.Int32"));
                dtNotasdecredito.Columns.Add("Fechacancelacion", Type.GetType("System.DateTime"));
                dtNotasdecredito.Columns.Add("Razoncancelacion", Type.GetType("System.String"));
                dtNotasdecredito.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtNotasdecredito.Columns.Add("MonedasID", Type.GetType("System.String"));
                dtNotasdecredito.Columns.Add("Tipodecambio", Type.GetType("System.Decimal"));
                dtNotasdecredito.Columns.Add("Esdevolucion", Type.GetType("System.Int32"));
                dtNotasdecredito.Columns.Add("EntradaTipodemovimientoinvID", Type.GetType("System.Int32"));
                dtNotasdecredito.Columns.Add("Entradaserie", Type.GetType("System.String"));
                dtNotasdecredito.Columns.Add("Entradafolio", Type.GetType("System.Int32"));
                dtNotasdecredito.Columns.Add("PolizasID", Type.GetType("System.Int32"));
                dtNotasdecredito.Columns.Add("Timbrado", Type.GetType("System.Int32"));
                dtNotasdecredito.Columns.Add("Tesk", Type.GetType("System.Int32"));

                System.Data.DataTable dtNotasdecreditodetalle = new System.Data.DataTable("Notasdecreditodetalle");
                dtNotasdecreditodetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtNotasdecreditodetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtNotasdecreditodetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtNotasdecreditodetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtNotasdecreditodetalle.Columns.Add("Descripcion", Type.GetType("System.String"));
                dtNotasdecreditodetalle.Columns.Add("Seriecfdi", Type.GetType("System.String"));
                dtNotasdecreditodetalle.Columns.Add("Foliocfdi", Type.GetType("System.Int32"));
                dtNotasdecreditodetalle.Columns.Add("Precio", Type.GetType("System.Decimal"));
                dtNotasdecreditodetalle.Columns.Add("Importe", Type.GetType("System.Decimal"));
                dtNotasdecreditodetalle.Columns.Add("PIva", Type.GetType("System.Decimal"));
                dtNotasdecreditodetalle.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtNotasdecreditodetalle.Columns.Add("PRetiva", Type.GetType("System.Decimal"));
                dtNotasdecreditodetalle.Columns.Add("Retiva", Type.GetType("System.Decimal"));
                dtNotasdecreditodetalle.Columns.Add("PRetisr", Type.GetType("System.Decimal"));
                dtNotasdecreditodetalle.Columns.Add("Retisr", Type.GetType("System.Decimal"));
                dtNotasdecreditodetalle.Columns.Add("Neto", Type.GetType("System.Decimal"));


                string strSerie = cboSerie.EditValue.ToString();
                if (blNuevo && strSerie=="NCR" )
                {
                    SiguienteID();
                }
                timbrado = chkNoTimbrar.Checked ? 0:1;
                string dato = String.Empty;
                
                int intFolio = Convert.ToInt32(txtFolio.Text);
                DateTime fFecha = Convert.ToDateTime(txtFecha.Text);
                int intSeq = 0;
                decimal dCantidad = 0;
                string strDescripcion = String.Empty;
                string strSeriecfdi = String.Empty;
                int intFoliocfdi = 0;
                decimal dPrecio = 0;
                decimal dImporte = 0;
                decimal dPIva = 0;
                decimal dIva = 0;
                decimal dPRetiva = 0;
                decimal dRetiva = 0;
                decimal dPRetisr = 0;
                decimal dRetisr = 0;
                decimal dNeto = 0;
                decimal dSubtotal=0, dRetIvaTotal=0, dRetIsrTotal=0, dIvaTotal=0,dNetoTotal=0;
                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "Cantidad").ToString();
                    if (dato.Length > 0)
                    {
                        intSeq = i;                       
                        dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Cantidad"));
                        if (gridViewDetalle.GetRowCellValue(i, "Descripcion") != null)
                        {
                            strDescripcion = gridViewDetalle.GetRowCellValue(i, "Descripcion").ToString();
                        }
                        if (gridViewDetalle.GetRowCellValue(i, "SerieCfdi") != null)
                        {
                            strSeriecfdi = gridViewDetalle.GetRowCellValue(i, "SerieCfdi").ToString();
                        }
                        intFoliocfdi = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "FolioCfdi"));
                        dPrecio = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Precio"));
                        dImporte = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importe"));
                        dIva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Iva"));
                        if (dIva == 0) { dPIva = 0; }
                        else { dPIva = dPivaG; }
                        dRetiva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Retiva"));
                        if (dRetiva == 0) { dPRetiva = 0; }
                        else { dPRetiva = dPRetIvaG; }
                        dRetisr = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Retisr"));
                        if (dRetisr == 0) { dPRetisr = 0; }
                        else { dPRetisr = dPRetIsrG; }
                        dNeto = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Neto"));

                        dtNotasdecreditodetalle.Rows.Add(
                            strSerie,
                            intFolio,
                            intSeq, 
                            dCantidad, 
                            strDescripcion, 
                            strSeriecfdi, 
                            intFoliocfdi, 
                            dPrecio, 
                            dImporte, 
                            dPIva, 
                            dIva, 
                            dPRetiva, 
                            dRetiva, 
                            dPRetisr, 
                            dRetisr, 
                            dNeto);

                        dSubtotal += dImporte;
                        dIvaTotal += dIva;
                        dRetIvaTotal += dRetiva;
                        dRetIsrTotal += dRetisr;
                        dNetoTotal += dNeto;

                    }
                }

                int intStatus = 1;

                DateTime fFechacancelacion = Convert.ToDateTime(null);
                string strRazoncancelacion = "";
                decimal dTipodecambio = Convert.ToDecimal(txtTipodecambio.Text);
                strMonedasID = cboMonedasID.EditValue.ToString();

                int intEntradaTipodemovimientoinvID=0, intEntradafolio=0, intPolizasID=0;
                string strEntradaserie= "";
                int intEsdevolucion = Convert.ToInt32(swDevolucion.IsOn ? 1 : 0);

                if (swDevolucion.IsOn == true)
                {
                    intEntradaTipodemovimientoinvID = Convert.ToInt32(cboTipodemov.EditValue);
                    strEntradaserie = txtSerideEntrada.Text.ToString();
                    intEntradafolio = Convert.ToInt32(txtFolioEntrada.Text);
                }

                intClienteID = Convert.ToInt32(cboCliente.EditValue);
                int intAgentesID = Convert.ToInt32(cboAgentesID.EditValue);
                int intMotivoID = Convert.ToInt32(cboMotivo.EditValue);

                dtNotasdecredito.Rows.Add(
                strSerie, 
                intFolio, 
                fFecha, 
                intClienteID,
                intMotivoID, 
                intAgentesID, 
                dSubtotal, 
                dIvaTotal, 
                dRetIvaTotal, 
                dRetIsrTotal,
                dPIva, 
                dPRetiva,
                dPRetisr, 
                dNetoTotal, 
                intStatus, 
                fFechacancelacion, 
                strRazoncancelacion, 
                intUsuarioID, 
                strMonedasID, 
                dTipodecambio, 
                intEsdevolucion, 
                intEntradaTipodemovimientoinvID, 
                strEntradaserie, intEntradafolio, intPolizasID,
                timbrado,
                0);

                NotasdecreditoCL cl = new NotasdecreditoCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.dtm = dtNotasdecredito;
                cl.dtd = dtNotasdecreditodetalle;
                cl.intUsuarioID = intUsuarioID;
                cl.strMaquina = Environment.MachineName;

                // Timbrar //
                cl.intClientesID = Convert.ToInt32(cboCliente.EditValue);
                cl.strMonedasID = cboMonedasID.EditValue.ToString();
                cl.strc_FP = cboFormadePago.EditValue.ToString();

                cl.TipoRelacionAlCancelar = txtTipoRelacionAlCancelar.Text;
                cl.UUIDASustituirAlCancelar = txtUUIDRelacionAlCancelar.Text;

                if (chkNoTimbrar.Checked)
                    Result = "OK";
                else
                    Result = cl.TimbraCfdi33(dtNotasdecreditodetalle, dtNotasdecredito);

                if (Result.ToUpper()=="OK")
                {
                    Result = cl.NotasdecreditoCrud();
                    if (Result == "OK")
                    {
                        //MessageBox.Show("Guardado Correctamente");

                        //Impresion(strSerie, intFolio, fFecha, strMonedasID);

                        DatosdecontrolCL clg = new DatosdecontrolCL();
                        string result = clg.DatosdecontrolLeer();
                        if (result == "OK")
                        {

                            if (clg.iEnvioCfdiAuto == 1)
                            {
                                MessageBox.Show("Guardada correctamente, se enviará por correo");
                                Impresion(strSerie, intFolio, dFecha, strMoneda);
                                EnviaCorreo();
                                intFolio = 0;
                            }
                            else
                            {
                                MessageBox.Show("Guardada correctamente");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Guardada correctamente");
                        }                      
                        LimpiaCajas();
                        Inicialisalista();
                    }
                    else
                    {
                        MessageBox.Show("Al Guardar:" + Result);
                    }
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
        } //Guardar  

        private string Valida()
        {
            { }
            if (cboAgentesID.EditValue == null)
            {
                return "El campo AgentesID no puede ir vacio";
            }          
            if (cboCliente.EditValue == null)
            {
                return "El campo ClientesID no puede ir vacio";
            }
            if (cboMonedasID.EditValue == null)
            {
                return "El campo Moneda no puede ir vacio";
            }

            globalCL clg = new globalCL();
            if (!clg.esNumerico(txtTipodecambio.Text))
            {
                if (cboMonedasID.EditValue.ToString()=="MXN")
                {
                    txtTipodecambio.Text = "1";
                }
                else
                {
                    return "Teclee el tipo de cambio";
                }
            }

            if (cboMotivo.EditValue == null)
            {
                return "Seleccione un motivo";
            }

            if (!clg.esNumerico(txtFolio.Text))
            {
                txtFolio.Text = "0";
            }
            if (Convert.ToInt32(txtFolio.Text) == 0)
            {
                return "El folio no puede ser cero";
            }

            string result3 = clg.GM_CierredemodulosStatus(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month, "CXC");
            if (result3 == "C")
            {
                    return "Este mes está cerrado, no se puede actualizar";
            }

            return "OK";
        } //Valida


        #region Filtros de bbi inferior
        //private void bbiRegistradas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Registrada'";
        //}

        //private void bbiAceptadas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Aceptada'";
        //}

        //private void bbiRechazadas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Rechazada'";
        //}

        //private void bbiExpiradas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Expirada'";
        //}

        //private void bbiCanceladas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Cancelada'";
        //}

        //private void bbiTodas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilter.Clear();
        //}
        #endregion

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            //intStatus = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status"));
            dFecha = Convert.ToDateTime(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Fecha"));
            strMoneda = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "MonedasID"));
            strTo = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "EMail").ToString();
            string status = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Estado").ToString();
            timbrado = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Timbrado"));

            //if (intStatus == 1)
            //{
            //    bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //}
            //else if (intStatus == 5)
            //{
            //    bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //}

            if (status == "Registrada")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intFolio = 0;
            strNCliente = string.Empty;
            LimpiaCajas();
            LlenarGrid();
            ribbonPageGroup2.Visible = false;
            ribbonPageGroup1.Visible = true;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            ribbonStatusBar.Visible = true;
            Inicialisalista();
            LimpiaCajas();
            navigationFrame.SelectedPageIndex = 0;
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
                    intClienteID = Convert.ToInt32(((DataRowView)orow)["Clave"]);
                    dPivaG = Convert.ToDecimal(((DataRowView)orow)["PIva"]);
                    dPRetIvaG = Convert.ToDecimal(((DataRowView)orow)["PRetiva"]);
                    dPRetIsrG = Convert.ToDecimal(((DataRowView)orow)["PRetisr"]);
                    cboAgentesID.EditValue = Convert.ToInt32(((DataRowView)orow)["AgentesID"]);

                    cboMonedasID.EditValue = ((DataRowView)orow)["Moneda"].ToString();

                }
            }

            catch (Exception ex)

            {

               MessageBox.Show("cboClientes: " + ex.Message);

            }

     
        }

        private void cboMonedasID_EditValueChanged(object sender, EventArgs e)
        {
            if (cboMonedasID.ItemIndex == 0)
            {
                txtTipodecambio.Text = "1";
                txtTipodecambio.ReadOnly = true;
            }
            else
            {
                txtTipodecambio.Text = "";
                txtTipodecambio.ReadOnly = false;
            }
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();

        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridNotasdecredito" +
                "";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
   
            globalCL clgd = new globalCL();
            clgd.strGridLayout = "gridNotasdecreditoDetalle" +
                "";
             result = clgd.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void CierraPopUp()
        {
            popUpCancelar.Visible = false;
            txtLogin.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtRazondecancelacion.Text = string.Empty;
        }

        private void btnAut_Click(object sender, EventArgs e)
        {
            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLogin.Text;
            clU.strClave = txtPassword.Text;
            clU.strPermiso = "Cancelardepositos";
            string result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }

            CierraPopUp();

            if (strSerie == "NCR" && swInterno.IsOn)
                Cancelatimbrado();
            else
                Cancelarenlabasededatos();
           
        }

        private void Cancelatimbrado()
        {
            try
            {

                //FacturacionCL cl = new FacturacionCL();
                string año = dFecha.Year.ToString();
                int mes = dFecha.Month;

                globalCL clg = new globalCL();
                string nombremes = clg.NombreDeMes(mes,3);

                string serie = strSerie;

                vsFK.vsFinkok vs = new vsFK.vsFinkok();
                string pRutaXML = System.Configuration.ConfigurationManager.AppSettings["pathxml"];
                string pRutaCer = System.Configuration.ConfigurationManager.AppSettings["pathcer"];
                string pRutaKey = System.Configuration.ConfigurationManager.AppSettings["pathkey"];
                string pRutaOpenSSL = System.Configuration.ConfigurationManager.AppSettings["pathopenssl"];

                cfdiCL cl2 = new cfdiCL();
                cl2.pSerie = System.Configuration.ConfigurationManager.AppSettings["serie"];
                string strresult = cl2.DatosCfdiEmisor();
                if (strresult != "OK")
                {
                    MessageBox.Show("No se pudieron leer los datos del emisor");
                    return;
                }

                string strarchivo = pRutaXML + año + "\\" + nombremes + "\\" + strSerie + intFolio.ToString() + ".xml";
                string strUUID = vs.ExtraeValor(strarchivo, "tfd:TimbreFiscalDigital", "UUID");
                string strnombrearchivo = strSerie + intFolio.ToString() + ".xml";
                string strrutaxml = pRutaXML + año + "\\" + nombremes + "\\";

                if (strUUID.Length == 0)
                {
                    MessageBox.Show("No se pudo leer el UUID en: " + pRutaXML + año + "\\" + nombremes + "\\" + strSerie + intFolio.ToString() + ".xml");
                    return;
                }

                if (!swInterno.IsOn)
                    strresult = clg.CancelaTimbrado(strSerie, pRutaXML + año + "\\" + nombremes + "\\" + strSerie + intFolio.ToString() + ".xml", strnombrearchivo, txtMotivo22.Text, txtUUIDNuevo22.Text);
                else
                    strresult = "Cancelacion exitosa";

                //if (cl2.pEmisorRegFed == "EKU9003173C9")
                //{
                //    strresult = vs.CancelaTimbradoDemostracion(pRutaCer, pRutaKey, pRutaOpenSSL, cl2.pLlaveCFD, strrutaxml, cl2.pEmisorRegFed, strUUID, strnombrearchivo,txtMotivo22.Text,txtUUIDNuevo22.Text);
                //}
                //else
                //{
                //    strresult = vs.CancelaTimbrado(pRutaCer, pRutaKey, pRutaOpenSSL, cl2.pLlaveCFD, strrutaxml, cl2.pEmisorRegFed, strUUID, strnombrearchivo, txtMotivo22.Text, txtUUIDNuevo22.Text);
                //}


                if (strresult == "Cancelacion exitosa" || strresult == "CFDI previamente cancelado")
                {
                    Cancelarenlabasededatos();
                    //DepositosCL cld = new DepositosCL();
                    //string sserieCP = System.Configuration.ConfigurationManager.AppSettings["serieCP"].ToString();
                    //cld.strSerie = strSerie;
                    //cld.intFolio = intFolio;

                    //string strresultado = cld.DepositosCancelar();
                    //if (strresultado != "OK")
                    //{
                    //    MessageBox.Show("Al cancelar en la BD: " + strresultado);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Cancelado correctamente");
                    //    bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //    LlenarGrid();
                    //}
                }
                else
                {
                    MessageBox.Show("vsfk:" + strresult);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cancelar:" + ex.Message);
            }
        }


        private void Cancelarenlabasededatos()
        {
            //aqui cancelar
            try
            {

                globalCL clg = new globalCL();
                string result3 = clg.GM_CierredemodulosStatus(dFecha.Year, dFecha.Month, "CXC");
                if (result3 == "C")
                {
                    MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                    return;
                }

                NotasdecreditoCL cl = new NotasdecreditoCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                cl.strMaquina = Environment.MachineName;
                cl.strRazon = txtRazondecancelacion.Text;
                string result = cl.NotasdecreditoCancelar();
                if (result == "OK")
                {
                    MessageBox.Show("Se ha cancelado correctamente");
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
        }

        private void Editar()
        {
            LlenaCajas();
            DetalleLlenaCajas();

            BotonesEdicion();
            blNuevo = false;
        }//ver

        private void BotonesEdicion()
        {
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup2.Visible = true;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 1;

        }//BotonesEdicion 

        private void LlenaCajas()
        {
            NotasdecreditoCL cl = new NotasdecreditoCL();
            cl.intFolio = intFolio;
            cl.strSerie = strSerie;
            String Result = cl.NotasdecreditoLlenaCajas();
            if (Result == "OK")
            {
                txtFolio.Text = cl.intFolio.ToString();
                //cboSerie.EditValue = cl.strSerie.ToString();
                cboCliente.EditValue = cl.intClientesID;
                txtFecha.Text = cl.fFecha.ToShortDateString();
                cboMotivo.EditValue = cl.intObservacionesID;
                cboMonedasID.EditValue = cl.strMonedasID;

                swDevolucion.IsOn = cl.intEsdevolucion == 1 ? true : false;
                if (swDevolucion.IsOn == true)
                {
                    cboTipodemov.EditValue = cl.intEntradaTipodemovimientoinvID;
                    txtSerideEntrada.Text = cl.strEntradaserie;
                    txtFolio.Text = cl.intEntradafolio.ToString();
                }
                else
                {
                    cboTipodemov.EditValue = null;
                    txtSerideEntrada.Text = string.Empty;
                    txtFolio.Text = string.Empty;
                }

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
                NotasdecreditoCL cl = new NotasdecreditoCL();
                cl.intFolio = intFolio;
                cl.strSerie = strSerie;
                gridControlDetalle.DataSource = cl.NotasdecreditodetalleLlenaCajas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }


        }//detalleLlenacajas


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            CierraPopUp();
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {        
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                swInterno.IsOn = false;
                popUpCancelar.Visible = true;
                groupControl3.Text = "Cancelar el folio:" + strSerie + intFolio.ToString();
                txtLogin.Focus();
            }
        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                //int intstatus = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Status"));
                string _mark = (string)view.GetRowCellValue(e.RowHandle, "Estado");

                if (_mark == "Cancelada")
                {
                    e.Appearance.ForeColor = Color.Red;
                }

            }
            catch (Exception ex)
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void bbiActualizar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void swDevolucion_EditValueChanged(object sender, EventArgs e)
        {
            if (swDevolucion.IsOn == true)
            {
                cboTipodemov.ReadOnly = false;
                txtSerideEntrada.ReadOnly = false;
                txtFolioEntrada.ReadOnly = false;
            }
            else
            {
                cboTipodemov.EditValue = null;
                cboTipodemov.ReadOnly = true;
                txtSerideEntrada.ReadOnly = true;
                txtFolioEntrada.ReadOnly = true;
            }
        }

        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Impresion(strSerie,intFolio,dFecha,strMoneda);
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string strSeriecfdi;
            int intFoliocfdi;
            decimal pIvaL = dPivaG, dRetIvaL = dPRetIvaG, dPRetIsrL = dPRetIsrG;
            
            try
            {
                if (e.Column.Name == "gridColumnFolioCfdi")
                {
                    decimal dTotalimpuestosret = 0, dTotalimpuestostras=0;
                    strSeriecfdi = Convert.ToString(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "SerieCfdi"));
                    intFoliocfdi = Convert.ToInt32(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "FolioCfdi"));
                    NotasdecreditoCL cl = new NotasdecreditoCL();
                    cl.strSerieCfdi = strSeriecfdi;
                    cl.intFolioCfdi = intFoliocfdi;
                    string Result = cl.BuscarImpuestosporSerieyFolioCfdi();
                    if (Result == "OK")
                    {
                        //cl.dTotalimpuestostras = dTotalimpuestostras;
                        //cl.dTotalimpuestosret = dTotalimpuestosret;
                        //mandar llamar el SP
                        if (cl.dTotalimpuestostras == 0)
                        {
                            pIvaL = 0;
                        }
                        if (cl.dTotalimpuestosret == 0)
                        {
                            dRetIvaL = 0;
                            dPRetIsrL = 0;
                        }

                        cboMonedasID.EditValue = cl.strMonedasID;

                    }
                    else
                    {
                        MessageBox.Show(Result);
                    }
                    
                }
                if (e.Column.Name == "gridColumnCantidad" || e.Column.Name == "gridColumnPrecio")
                {
                    decimal cant = 0;
                    decimal precio = 0;
                    decimal imp = 0;
                    decimal iva = 0;
                    decimal RetIva = 0;
                    decimal RetIsr = 0;
                    decimal NETO = 0;

                    cant = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Cantidad"));
                    precio = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Precio"));


                    imp = Math.Round(cant * precio, 2); //Calculamos el importe y lo redondeamos a dos decimales
                    iva = Math.Round(imp * (pIvaL / 100), 2);
                    RetIva = Math.Round(imp * (dRetIvaL / 100), 2);
                    RetIsr = Math.Round(imp * (dPRetIsrL / 100), 2);
                    NETO = imp + iva + RetIva + RetIsr;

                    gridViewDetalle.SetFocusedRowCellValue("Importe", imp);
                    gridViewDetalle.SetFocusedRowCellValue("Iva", iva);
                    gridViewDetalle.SetFocusedRowCellValue("RetIva", RetIva);
                    gridViewDetalle.SetFocusedRowCellValue("RetIsr", RetIsr);
                    gridViewDetalle.SetFocusedRowCellValue("Neto", NETO);
                }                
              
            }                     

             catch (Exception ex)
            {
                MessageBox.Show("griddetallecalculatotales: " + ex);
            }
        }

        private void bbiEnviar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EnviaCorreo();
        }

        private void cboSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (cboSerie.EditValue.ToString() != "NCR")
            {
                txtFolio.Enabled = true;
                chkNoTimbrar.Checked = true;
                txtFolio.Text = "0";
            }
            else
            {
                txtFolio.Enabled = false;
                chkNoTimbrar.Checked = false;
            }
                
        }

        private void txtLogin_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscaNuevoUUID_Click(object sender, EventArgs e)
        {
            globalCL clg = new globalCL();
            //txtUUIDNuevo22.Text = clg.leeUUIDNuevo("N", txtSerieNueva22.Text, txtFolioNuevo22.Text);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            globalCL clg = new globalCL();
            txtUUIDRelacionAlCancelar.Text = clg.leeUUIDNuevo(txtSerieRelacionAlCancelar.Text, txtSerieRelacionAlCancelar.Text, txtFolioRelacionAlCancelar.Text,intClienteID);
        }

        private void Impresion(string serie, int Folio, DateTime Fecha, string Moneda)
        {
            try
            {
                if (timbrado==0)
                    return;

                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Newsa.net","Generando PDF...");

                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                globalCL clG = new globalCL();

                string rutaXML = string.Empty;
                string rutaPDF = string.Empty;
                string sYear = string.Empty;
                string sMes = string.Empty;

                sYear = Fecha.Year.ToString();
                sMes =  clG.NombreDeMes(Fecha.Month,3);


                rutaXML = ConfigurationManager.AppSettings["pathxml"].ToString() + "\\" + sYear + "\\" + sMes + "\\" + serie + Folio + ".xml";
                if (!File.Exists(rutaXML))
                {
                    rutaXML = ConfigurationManager.AppSettings["pathxml"].ToString() + "\\" + sYear + "\\" + sMes + "\\" +  Folio + "timbrado.xml";
                    if (!File.Exists(rutaXML))
                    {
                        MessageBox.Show("No existe el archivo: " + rutaXML);
                        DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        return;
                    }
                        
                }

                rutaPDF = ConfigurationManager.AppSettings["pathpdf"].ToString() + "\\" + sYear + "\\" + sMes + "\\";
                if (!Directory.Exists(rutaPDF))
                {
                    Directory.CreateDirectory(rutaPDF);
                }
                rutaPDF = rutaPDF + serie + Folio + ".pdf";

                vs.RutaXmlTimbrado = rutaXML;
                vs.RutaPdfTimbrado = rutaPDF;


                vs.Moneda33 = cboMonedasID.EditValue.ToString();
                vs.ArchivoTcr = "NCR.vx25";
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
                vs.Moneda33 = cboMonedasID.EditValue.ToString();
                vs.Cliente = Convert.ToString(intClienteID);
                vs.Leyenda1 = ".";
                
                vs.CampoExtra2 = ""; //txtObservaciones.Text;



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
               
                vs.CampoExtra9 = "Tipo de comprobante";

                vs.CampoExtra10 = EmpDatos;
                //result = vs.ImprimeFormatoTagCode();

                vs.OC = "";
                vs.Pedido = "";
                vs.EmpresaOtros = EmpDatos;
                vs.EmpresaLogo = ConfigurationManager.AppSettings["logoEmpresa"].ToString();
                vs.VistaPrevia = "SI";
                vs.SCEObservaciones = "";
                result = vs.generaPDF();
                if (result != "OK")
                {
                    MessageBox.Show("Al generar pdf:" + result);
                }

                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

            }
            catch (Exception ex)
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                MessageBox.Show("Impresión: " + ex.Message);
            }
        }

        private void EnviaCorreo()
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Correos", "Enviando...");
                globalCL cl = new globalCL();
                //cl.AbreOutlook(strSerie, intFolio, dFecha, strTo);
                string result = cl.EnviaCorreo(strTo, strSerie, intFolio, dFecha, "N");
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
    }
}