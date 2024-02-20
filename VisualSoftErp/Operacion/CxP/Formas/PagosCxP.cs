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
using VisualSoftErp.Clases.HerrramientasCLs;
using System.Configuration;
using DevExpress.XtraGrid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraNavBar;

namespace VisualSoftErp.Operacion.CxP.Formas
{
    public partial class PagosCxP : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public BindingList<detalleCL> detalle;
        string strTo;
        int intFolio;
        string strSerie;
        string strMonedaDelPago;
        int intProveedor;
        public bool blNuevo;
        int intCuentasbancariasID;
        int intUsuarioID = globalCL.gv_UsuarioID;
        string Rfcbanco;
        string Cuentabancariabeneficiario;
        string Rfcbancoordenante;
        string Nombrebancoordenante;
        DateTime dFecha;
        string sMoneda;
        int AñoFiltro;
        int MesFiltro;

        public PagosCxP()
        {
            InitializeComponent();

            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            txtCuentaordenante.Properties.MaxLength = 50;
            txtCuentaordenante.EnterMoveNextControl = true;

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Pagos a proveedores";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            //Preguntar a javier si lo puedo poner en otro lado, aqui los oculta cuando damos click en pagos 'hacer invisibles desde el diseño mejor'
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;
            LlenarGrid(AñoFiltro, MesFiltro);
            gridViewPrincipal.ActiveFilter.Clear();

            //las dos lineas siguientes hace que nos aparesca en el grid una columna y un check para seleccionar el renglon
            gridViewDetalle.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewDetalle.OptionsSelection.MultiSelect = true;

            gridViewDetalle.OptionsView.ShowAutoFilterRow = true;

            AgregaAñosNavBar();

            try
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

            }
            catch
            {

            }
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

        private void LlenarGrid(int año,int mes)
        {

            globalCL clg = new globalCL();
            clg.strPrograma = "0220";
            if (clg.CargaInicialLeer() == 0)
            {
                bbiCargaGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                return;
            }
                

            LlenaGridDesdeBoton();
            
        } //LlenarGrid()

        private void LlenaGridDesdeBoton()
        {

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Newsa.net", "Cargando información...");

            PagosCxPCL cl = new PagosCxPCL();
            cl.intAño = AñoFiltro;
            cl.intMes = MesFiltro;
            gridControlPrincipal.DataSource = cl.PagoscxpGrid();
            //Global, manda el nombre del grid para la clase Global

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPagoscxp";
            clg.restoreLayout(gridViewPrincipal);
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        public class detalleCL
        {
            public string Seriecdfi { get; set; }
            public int Foliocfdi { get; set; }
            public DateTime Fechafac { get; set; }
            public DateTime Fechavence { get; set; }
            public decimal Importe { get; set; }
            public decimal Supago { get; set; }
            public decimal APagarConvertido { get; set; }
            public int Diasvence { get; set; }
            public int ID { get; set; } //debe de llamarse como esta en la tabla
        }

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPagoscxpDetalle";
            clg.restoreLayout(gridViewDetalle);
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "serie";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Clave";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Des"].Visible = false;
            cboSerie.ItemIndex = 0;

            cl.strTabla = "Proveedores";
            cboProveedoresID.Properties.ValueMember = "Clave";
            cboProveedoresID.Properties.DisplayMember = "Des";
            cboProveedoresID.Properties.DataSource = cl.CargaCombos();
            cboProveedoresID.Properties.ForceInitialize();
            cboProveedoresID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedoresID.Properties.PopulateColumns();
            cboProveedoresID.Properties.Columns["Clave"].Visible = false; //con esta propiedad puedo ocultar campos en el cbo     
            cboProveedoresID.Properties.Columns["Piva"].Visible = false;
            cboProveedoresID.Properties.Columns["Plazo"].Visible = false;
            cboProveedoresID.Properties.Columns["Tiempodeentrega"].Visible = false;
            cboProveedoresID.Properties.Columns["Diastraslado"].Visible = false;
            cboProveedoresID.Properties.Columns["Lab"].Visible = false;
            cboProveedoresID.Properties.Columns["Via"].Visible = false;
            cboProveedoresID.Properties.Columns["BancosID"].Visible = false;
            cboProveedoresID.Properties.Columns["Cuentabancaria"].Visible = false;
            cboProveedoresID.Properties.Columns["C_Formapago"].Visible = false;
            cboProveedoresID.Properties.Columns["MonedasID"].Visible = false;
            cboProveedoresID.Properties.Columns["Retiva"].Visible = false;
            cboProveedoresID.Properties.Columns["Contacto"].Visible = false;
            cboProveedoresID.Properties.Columns["Retisr"].Visible = false;
            cboProveedoresID.Properties.Columns["Diasdesurtido"].Visible = false;
            cboProveedoresID.Properties.Columns["Diastraslado"].Visible = false;
            cboProveedoresID.Properties.NullText = "Seleccione un proveedor";

            cl.strTabla = "cyb_Chequeras";
            cboCuentasbancariaID.Properties.ValueMember = "Clave";
            cboCuentasbancariaID.Properties.DisplayMember = "Des";
            cboCuentasbancariaID.Properties.DataSource = cl.CargaCombos();
            cboCuentasbancariaID.Properties.ForceInitialize();
            cboCuentasbancariaID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCuentasbancariaID.Properties.PopulateColumns();
            cboCuentasbancariaID.Properties.Columns["Clave"].Visible = false;
            cboCuentasbancariaID.Properties.Columns["MonedasID"].Visible = false;
            cboCuentasbancariaID.Properties.Columns["Rfcbanco"].Visible = false;
            cboCuentasbancariaID.ItemIndex = 0;

            cl.strTabla = "cyb_bancos";
            cboBancosId.Properties.ValueMember = "Clave";
            cboBancosId.Properties.DisplayMember = "Des";
            cboBancosId.Properties.DataSource = cl.CargaCombos();
            cboBancosId.Properties.ForceInitialize();
            cboBancosId.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboBancosId.Properties.PopulateColumns();
            cboBancosId.Properties.Columns["Clave"].Visible = false;
            cboBancosId.Properties.Columns["Rfc"].Visible = false;
            cboBancosId.Properties.NullText = "Seleccione un banco";

            cl.strTabla = "FormadePago";
            cboC_Formapago.Properties.ValueMember = "Clave";
            cboC_Formapago.Properties.DisplayMember = "Des";
            cboC_Formapago.Properties.DataSource = cl.CargaCombos();
            cboC_Formapago.Properties.ForceInitialize();
            cboC_Formapago.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboC_Formapago.Properties.PopulateColumns();
            cboC_Formapago.Properties.Columns["Clave"].Visible = false;
            cboC_Formapago.Properties.NullText = "Seleccione la forma de pago";
        }
        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Configurando detalle", "espere por favor...");
            Nuevo();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Nuevo()
        {
            Inicialisalista();
            LimpiaCajas();
            BotonesEdicion();
            blNuevo = true;
            cboProveedoresID.ReadOnly = false;
            SiguienteID();

            //Oculta los litros en el segundo frame
            navBarControl.Visible = false;
            ribbonStatusBar.Visible = false;

            //DatosdecontrolCL cl = new DatosdecontrolCL();
            //string result = cl.DatosdecontrolLeer();

            //if (result == "OK")
            //{
            //    cboCuentasbancariaID.EditValue = cl.intDepositoschequerabeneficiarioID;
            //}
            cboC_Formapago.EditValue = "03";  //default transfer
            cboProveedoresID.Enabled = true;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void LimpiaCajas()
        {
            Inicialisalista();

            cboSerie.ItemIndex = 0;
            txtFolio.Text = string.Empty;
            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            txtImporte.Text = "";
            cboProveedoresID.EditValue = null;
            cboCuentasbancariaID.ItemIndex = 0;
            
            cboBancosId.EditValue = null;
            txtCuentaordenante.Text = string.Empty;

        }

        private string BuscarSerie()
        {
            PagosCxPCL cl = new PagosCxPCL();
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
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            PagosCxPCL cl = new PagosCxPCL();
            cl.strSerie = serie;
            cl.strDoc = "PagosCxP";

            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {
                cboSerie.EditValue = serie;
                strSerie = serie;
                txtFolio.Text = cl.intID.ToString();
                intFolio = cl.intFolio;
            }
            else
            {
                MessageBox.Show("SiguienteID :" + result);
            }

        }//SguienteID

        private void bbiGuardar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();           
        }

        private void Guardar()
        {
            try
            {
                String Result = Valida();
                if (Result != "OK")
                {
                    if (Result.Length>0)
                        MessageBox.Show(Result);
                    return;
                }

                string sCondicion = String.Empty;
                //DT para PagosCxP
                System.Data.DataTable dtPagoscxp = new System.Data.DataTable("Pagoscxp");
                dtPagoscxp.Columns.Add("Serie", Type.GetType("System.String"));
                dtPagoscxp.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtPagoscxp.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtPagoscxp.Columns.Add("Importe", Type.GetType("System.Decimal"));
                dtPagoscxp.Columns.Add("ProveedoresID", Type.GetType("System.Int32"));
                dtPagoscxp.Columns.Add("CuentasbancariaID", Type.GetType("System.Int32"));
                dtPagoscxp.Columns.Add("Tipodecambio", Type.GetType("System.Decimal"));
                dtPagoscxp.Columns.Add("BancodelproveedorId", Type.GetType("System.Int32"));
                dtPagoscxp.Columns.Add("Cuentadelproveedor", Type.GetType("System.String"));
                dtPagoscxp.Columns.Add("C_Formapago", Type.GetType("System.String"));
                dtPagoscxp.Columns.Add("Foliochequeotransferencia", Type.GetType("System.Int32"));
                dtPagoscxp.Columns.Add("Monedadelpago", Type.GetType("System.String"));
                dtPagoscxp.Columns.Add("Status", Type.GetType("System.String"));
                dtPagoscxp.Columns.Add("FechaReal", Type.GetType("System.DateTime"));
                dtPagoscxp.Columns.Add("Origen", Type.GetType("System.Int32"));
                dtPagoscxp.Columns.Add("Poliza", Type.GetType("System.Int32"));
                dtPagoscxp.Columns.Add("Tesk", Type.GetType("System.Int32"));

                //DT para PagosCxPDetalle
                System.Data.DataTable dtPagoscxpdetalle = new System.Data.DataTable("Pagoscxpdetalle");
                dtPagoscxpdetalle.Columns.Add("SERIE", Type.GetType("System.String"));
                dtPagoscxpdetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtPagoscxpdetalle.Columns.Add("SEQ", Type.GetType("System.Int32"));
                dtPagoscxpdetalle.Columns.Add("TipodemovimientocxpID", Type.GetType("System.Int32"));
                dtPagoscxpdetalle.Columns.Add("SerieCxP", Type.GetType("System.String"));
                dtPagoscxpdetalle.Columns.Add("FolioCxP", Type.GetType("System.Int32"));
                dtPagoscxpdetalle.Columns.Add("SUPAGO", Type.GetType("System.Decimal"));
                dtPagoscxpdetalle.Columns.Add("SUPAGOCONVERTIDO", Type.GetType("System.Decimal"));
                dtPagoscxpdetalle.Columns.Add("CxPID", Type.GetType("System.Int32"));
                

                if (Result != "OK")
                {
                    MessageBox.Show("No se pudo leer el siguiente folio");
                    return;
                }
                //iFolio = clg.intSigFolio;
                int iFolio = 0;
                int intRen = 0;
                string dato = String.Empty;
                string strSerieCxP = String.Empty;
                int intFolioCxP = 0;
                decimal dSUPAGO = 0;
                int intSeq = 0;
                decimal dsupagoconvertido = 0;
                int intTipoMov = 0;
                int intCxPID = 0;
                decimal dTipocambio = Convert.ToDecimal(txtTipodecambio.Text);

                globalCL clg = new globalCL();

                //Recorrer el griddetalle para extraer los datos y pasarlos al dt (con el gridViewDetalle.GetSelectedRows solo tomara el renglon o renglones seleccionados)
                foreach (int i in gridViewDetalle.GetSelectedRows())
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "APagar").ToString();
                    if (clg.esNumerico(dato))
                    {
                        
                        strSerieCxP = gridViewDetalle.GetRowCellValue(i, "Serie").ToString();
                        intFolioCxP = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Folio"));
                        dSUPAGO = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "APagar"));
                        dsupagoconvertido = Math.Round(dSUPAGO * dTipocambio, 2);
                        intTipoMov = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "TipoMov"));
                        intCxPID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "ID"));

                        if (dSUPAGO > 0)
                        {
                            dtPagoscxpdetalle.Rows.Add(
                            cboSerie.EditValue,
                            txtFolio.Text,
                            intSeq,
                            intTipoMov,
                            strSerieCxP,
                            intFolioCxP,
                            dSUPAGO,
                            dsupagoconvertido,
                            intCxPID);

                            intSeq = intSeq + 1;
                        }                                             
                    }
                }

                if (dtPagoscxpdetalle.Rows.Count==0)
                {
                    MessageBox.Show("Debe seleccionar al menos un renglón para pagar");
                    return;
                }

                string strSerie = cboSerie.EditValue.ToString();
                int intfolio = Convert.ToInt32(txtFolio.Text);
                DateTime fFecha = Convert.ToDateTime(dateEditFecha.Text);
                decimal dImporte = Convert.ToDecimal(txtImporte.Text);
                int intProveedoresID = Convert.ToInt32(cboProveedoresID.EditValue);
                int intCuentasBancariaID = Convert.ToInt32(cboCuentasbancariaID.EditValue);
                decimal dTipodecambio = Convert.ToDecimal(txtTipodecambio.Text);
                int intBancosId = Convert.ToInt32(cboBancosId.EditValue);
                string strCuentaOrdenante = txtCuentaordenante.Text;
                string strC_Formapago = cboC_Formapago.EditValue.ToString();
                int intFoliochequetransfer = 1; 
                string strStatus = "1";
                DateTime fFechaReal = Convert.ToDateTime(DateTime.Now);
                int intOrigen = 1;
                dato = String.Empty;

                dtPagoscxp.Rows.Add(
                    strSerie,
                    intfolio,
                    fFecha,
                    dImporte,
                    intProveedoresID,
                    intCuentasBancariaID,
                    dTipodecambio,
                    intBancosId,
                    strCuentaOrdenante,
                    strC_Formapago,
                    intFoliochequetransfer,
                    strMonedaDelPago,
                    strStatus,
                    fFechaReal,
                    intOrigen,
                    0,
                    0);

                PagosCxPCL cl = new PagosCxPCL();
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.dtm = dtPagoscxp;
                cl.dtd = dtPagoscxpdetalle;
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                cl.strMaquina = Environment.MachineName;
                cl.strPrograma = "0220";
                string result = cl.PagoxcxpCrud();
                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");

                    if (intFolio == 0)
                    {
                        LimpiaCajas();
                        SiguienteID();
                        cboProveedoresID.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        } //Guardar



        private string Valida()
        {
        
            if (txtFolio.Text.Length == 0)
            {
                return "El campo Folio no puede ir vacio";
            }
            if (txtImporte.Text.Length == 0)
            {
                return "El campo Importe no puede ir vacio";
            }
            if (cboProveedoresID.EditValue == null)
            {
                return "El campo ProveedoresID no puede ir vacio";
            }
            if (cboCuentasbancariaID.EditValue == null)
            {
                return "El campo CuentasBancariaID no puede ir vacio";
            }
            if (txtTipodecambio.Text.Length == 0)
            {
                return "El campo Tipodecambio no puede ir vacio";
            }
            if (cboBancosId.EditValue == null)
            {
                return "El campo BancosId no puede ir vacio";
            }
            if (txtCuentaordenante.Text.Length == 0)
            {
                txtCuentaordenante.Text = "";
            }

            globalCL clG = new globalCL();
            //if (strMonedaDelPago != "MXN")
            //{
                

            //    if (!clG.esNumerico(txtTipodecambio.Text))
            //    {
            //        return "Teclee el tipo de cambio";
            //    }
            //    if (Convert.ToDecimal(txtTipodecambio.Text) <= 1)
            //    {
            //        return "Teclee el tipo de cambio";
            //    }
            //}
            decimal summaryValue = Convert.ToDecimal(gridViewDetalle.Columns["APagarConvertido"].SummaryItem.SummaryValue);
            if (summaryValue==0)
            {
                return "Debe pagar al menos una factura";
            }

            if (!clG.esNumerico(txtImporte.Text))
            {
                return "Teclee el importe del pago";
            }

            if (Convert.ToDecimal(txtImporte.Text) != summaryValue)
            {
                return "La suma de las facturas a pagar no concuerda con el importe del pago";
            }

            bool diferenteMoneda=false;
            string monedaCxP = string.Empty;

            foreach (int i in gridViewDetalle.GetSelectedRows())
            {
                monedaCxP = gridViewDetalle.GetRowCellValue(i, "Moneda").ToString();
                if (strMonedaDelPago!=monedaCxP)
                {
                    diferenteMoneda = true;
                }
            }

            if (diferenteMoneda)
            {
                
                    if (txtTipodecambio.Text == "1")
                    {
                        return "Teclee un tipo de cambio mayor de 1";
                    }
                
            }
            else
            {
                //if (strMonedaDelPago == "MXN")
                //{
                //    txtTipodecambio.Text = "1";
                //}
                //else
                //{
                //    if (txtTipodecambio.Text == "1")
                //    {
                //        return "Teclee un tipo de cambio mayor de 1";
                //    }
                //}
            }

            globalCL clg = new globalCL();
            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(dateEditFecha.Text).Year, Convert.ToDateTime(dateEditFecha.Text).Month, "CXP");
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";                
            }


            string FP = cboC_Formapago.EditValue.ToString();
            if (FP != "03")
            {
                DialogResult Result = MessageBox.Show("La forma de pago no es Transferencia, continua?", "Precaución", MessageBoxButtons.YesNo);
                if (Result.ToString() == "No")
                {
                    return "";
                }

            }


            return "OK";
        } //Valida

        private void llenaCajas()
        {
            PagosCxPCL cl = new PagosCxPCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            String Result = cl.PagoscxpLlenaCajas();
            if (Result == "OK")
            {
                cboSerie.EditValue = cl.strSerie.ToString();
                txtFolio.Text = cl.intFolio.ToString();
                dateEditFecha.Text = cl.fFecha.ToShortDateString();
                txtImporte.Text = cl.dImporte.ToString();
                cboProveedoresID.EditValue = cl.intProveedoresID;
                cboCuentasbancariaID.EditValue = cl.intCuentasBancariaID;
                txtTipodecambio.Text = cl.dTipodecambio.ToString();
                cboBancosId.EditValue = cl.intBancosId;
                txtCuentaordenante.Text = cl.strCuentaOrdenante;
                cboC_Formapago.EditValue = cl.strC_Formapago;
                strMonedaDelPago = cl.strMonedaDelPago;
                lblPoliza.Text = "POLIZA: " + cl.strPoliza;

                gridControlDetalle.DataSource = cl.PagoscxpLlenaCajasDetalle();
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }  //Editar

        //borrar si no se usa editar
        private void Editar()
        {
            BotonesEdicion();
            bbiCargarCxP.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            llenaCajas();
            navigationFrame.SelectedPageIndex = 1;
        }

        //borrar si no se usa editar
        private void BotonesEdicion()
        {
            LimpiaCajas();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEnviar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCargarCxP.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCorrigeFacturaTesk.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiTeskSaldos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            navigationFrame.SelectedPageIndex = 1;
        }

        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEnviar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCargarCxP.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCorrigeFacturaTesk.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiTeskSaldos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            LlenarGrid(AñoFiltro,MesFiltro);

            intFolio = 0;
            strSerie = null;
            navigationFrame.SelectedPageIndex = 0;

            navBarControl.Visible = true;
            navBarControl.Visible = true;
            ribbonStatusBar.Visible = true;
            

        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPagoscxp";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            clg.strGridLayout = "gridPagoscxpDetalle";
            Result = clg.SaveGridLayout(gridViewDetalle);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }

        private void bbiVista_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlPrincipal.ShowRibbonPrintPreview();
        }


        private void txtFolio_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void chkTimbrar_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void bbiCargarfacturas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Cargarfacturas();
        }

        //Carga las facturas pendientes de pago para el ciente selecionado
        private void Cargarfacturas()
        {
            PagosCxPCL cl = new PagosCxPCL();
            cl.intProveedoresID = intProveedor;
            cl.intUsuarioID = globalCL.gv_UsuarioID;
            string result = cl.PagosGeneraAntiguedaddesaldos();
            if (result == "OK")
            {
                gridControlDetalle.DataSource = cl.PagoscxpCargaFacturas();
                if (gridViewDetalle.RowCount>1)
                {
                    cboProveedoresID.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show(result);
            }


        }//Cargafacturas             

        //Estas lienas le dan vida a los litros de la izq en el grid principal
        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
        }

        private void cboBancosId_EditValueChanged(object sender, EventArgs e)
        {
            object orow = cboBancosId.Properties.GetDataSourceRowByKeyValue(cboBancosId.EditValue);
            if (orow != null)
            {

                Rfcbancoordenante = ((DataRowView)orow)["Rfc"].ToString();
                Nombrebancoordenante = ((DataRowView)orow)["Des"].ToString();
            }
        }

        private void bbiImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void Cancelar()
        {
            try
            {

                globalCL clg = new globalCL();
                string result = clg.GM_CierredemodulosStatus(DateTime.Now.Year, DateTime.Now.Month, "CXP");
                if (result == "C")
                {
                    MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                    return;

                }

                PagosCxPCL cl = new PagosCxPCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                cl.strMaquina = Environment.MachineName;
                result = cl.PagoscxpCancelar(); 
                if(result == "OK")
                {
                    MessageBox.Show("Cancelado correctamente");
                    LlenarGrid(AñoFiltro,MesFiltro);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cancelar:" + ex.Message);
            }
        }

        private void Cancerlarenlabasededatos()
        {

        }

        private void btnAut_Click(object sender, EventArgs e)
        {

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

                string result = cl.EnviaCorreo(strTo, strSerie, intFolio, dFecha,"P");

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

        private void bbiEnviar_ItemClick(object sender, ItemClickEventArgs e)
        {
            EnviaCorreo();
        }

        private void cboProveedoresID_EditValueChanged(object sender, EventArgs e)
        {
            object orow = cboProveedoresID.Properties.GetDataSourceRowByKeyValue(cboProveedoresID.EditValue);
            if (orow != null)
            {
                int bancoOrd = Convert.ToInt32(((DataRowView)orow)["BancosID"]);
                string cta = ((DataRowView)orow)["Cuentabancaria"].ToString();
                string fp = ((DataRowView)orow)["C_Formapago"].ToString();


                if (bancoOrd > 0)
                {
                    cboBancosId.EditValue = bancoOrd;
                }
                if (fp.Length > 0)
                {
                    cboC_Formapago.EditValue = fp;
                }

                txtCuentaordenante.Text = cta;
            }

        }

        private void cboProveedoresID_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            intProveedor = Convert.ToInt32(e.NewValue);

            //if (gridViewDetalle.RowCount - 1 > 0)
            //{
            //    DialogResult dialogResult = MessageBox.Show("Hay datos en el grid, desea continuar?", "Advertencia", MessageBoxButtons.YesNo);
            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        Cargarfacturas();
            //    }
            //    else
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //else
            //{
            //    Cargarfacturas();
            //}
        }

        private void cboCuentasbancariaID_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                //proceso de DE para obtener valor del combo
                DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;

                DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

                Object value = row["Clave"];

                intCuentasbancariasID = Convert.ToInt32(value);

                object orow = cboCuentasbancariaID.Properties.GetDataSourceRowByKeyValue(cboCuentasbancariaID.EditValue);
                if (orow != null)
                {
                    strMonedaDelPago = ((DataRowView)orow)["MonedasID"].ToString();
                    Rfcbanco = ((DataRowView)orow)["Rfcbanco"].ToString();
                    Cuentabancariabeneficiario = ((DataRowView)orow)["Des"].ToString();

                }

                //Limpiara tipo de cambio paa poder ingresar el valor
                txtTipodecambio.Text = "";
                txtTipodecambio.ReadOnly = false;

                if (strMonedaDelPago == "MXN")
                {
                    txtTipodecambio.Text = "1";
                    
                }
            }

            catch (Exception)
            {

            }
        }//EditValue cbocuentabancaria

        private void gridViewDetalle_SelectionChanged_1(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            decimal saldo = 0;
            decimal convertido = 0;
            //bool seleccionados = false;

            int controllerRow = e.ControllerRow;
            if (controllerRow == GridControl.InvalidRowHandle)
            {

                Int32[] selectedRowHandles = gridViewDetalle.GetSelectedRows();

                if (selectedRowHandles.Length == 0)
                {
                    for (int i = 0; i <= gridViewDetalle.RowCount; i++)
                    {
                        gridViewDetalle.FocusedRowHandle = i;
                        gridViewDetalle.SetFocusedRowCellValue("APagar", 0);
                        gridViewDetalle.SetFocusedRowCellValue("APagarConvertido", 0);
                    }
                }
                else
                {
                    for (int i = 0; i <= gridViewDetalle.RowCount; i++)
                    {
                        gridViewDetalle.FocusedRowHandle = i;

                       
                        saldo = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Importe"));

                        convertido = Math.Round(saldo * Convert.ToDecimal(txtTipodecambio.Text),2);


                       gridViewDetalle.SetFocusedRowCellValue("APagar", saldo);
                        gridViewDetalle.SetFocusedRowCellValue("APagarConvertido", convertido);
                    }
                }


            }

            else
            {
                if (e.Action.ToString() == "Add")
                {
                    globalCL clg = new globalCL();
                    if (!clg.esNumerico(txtTipodecambio.Text))
                    {
                        if (strMonedaDelPago == "MXN")
                            txtTipodecambio.Text = "1";
                        else
                        {
                            MessageBox.Show("Teclee el tipo de cambio");
                            return;
                        }
                        
                    }
                    saldo = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Importe"));
                    convertido = Math.Round(saldo * Convert.ToDecimal(txtTipodecambio.Text), 2);
                    gridViewDetalle.SetFocusedRowCellValue("APagar", saldo);
                    gridViewDetalle.SetFocusedRowCellValue("APagarConvertido", convertido);
                    
                }
                else
                {
                    gridViewDetalle.SetFocusedRowCellValue("APagar", 0);
                    gridViewDetalle.SetFocusedRowCellValue("APagarConvertido", 0);
                }
            }

            gridViewDetalle.UpdateTotalSummary();

        }

        // son los filtros laterales de los meses 
        private void navBarControl_ActiveGroupChanged_1(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            //navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }


        #region Filtros Meses
        private void navBarItemEne_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 1";
        }

        private void navBarItemFeb_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 2";
        }

        private void navBarItemMar_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 3";
        }

        private void navBarItemAbr_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 4";
        }

        private void navBarItemMay_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 5";
        }

        private void navBarItemJun_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 6";
        }

        private void navBarItemJul_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 7";
        }

        private void navBarItemAgo_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 8";
        }

        private void navBarItemsep_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 9";
        }

        private void navBarItemOct_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 10";
        }

        private void navBarItemNov_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 11";
        }

        private void navBarItemDic_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 12";
        }

        private void navBarItemTodos_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilter.Clear();
        }
        #endregion

        #region Status Inferior barra
        private void bbiRegistrados_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Status]='Registrada'";
        }

        private void bbiCancelados_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Status]='Cancelada'";
        }

        private void bbiTodos_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilter.Clear();
        }
        #endregion

        private void bbiCancelar_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            popUpCancelar.Visible = true;
            groupControl3.Text = "Cancelar el folio:" + strSerie + intFolio.ToString();
            txtLogin.Focus();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            CierraPopUp();
        }

        private void CierraPopUp()
        {
            popUpCancelar.Visible = false;
            txtLogin.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private void gridViewPrincipal_RowClick_1(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            strSerie = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie").ToString();
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            dFecha = Convert.ToDateTime(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Fecha"));
            sMoneda = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Monedadelpago").ToString();
            string status = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status").ToString();
            //strTo = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "EMail").ToString();

            if (status == "Registrado")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

        }

        private void gridViewPrincipal_RowCellStyle_1(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string _mark = (string)view.GetRowCellValue(e.RowHandle, "Status");

            if (_mark == "Cancelado")
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void btnAut_Click_1(object sender, EventArgs e)
        {
            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLogin.Text;
            clU.strClave = txtPassword.Text;
            clU.strPermiso = "Cancelarpagoscxp";
            string result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }
            CierraPopUp();
            Cancelar();
        }

        private void bbiCargarCxP_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cargarfacturas();
        }

        private void btnHabilitaProveedor_Click(object sender, EventArgs e)
        {
            cboProveedoresID.Enabled = true;
        }

        private void btnTomaImporte_Click(object sender, EventArgs e)
        {
            var summaryValue = gridViewDetalle.Columns["APagarConvertido"].SummaryItem.SummaryValue;
            if (summaryValue is null)
                txtImporte.Text = "0";
            else
                txtImporte.Text = summaryValue.ToString();
        }

        private void bbiCargaGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            LlenaGridDesdeBoton();
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

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.Name=="gridColumnSupago")
                {

                    globalCL clg = new globalCL();
                    if (!clg.esNumerico(txtTipodecambio.Text))
                    {
                        MessageBox.Show("Teclee el tipo de cambio");
                        return;
                    }

                    decimal pago = 0;
                    decimal convertido = 0;
                    pago = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "APagar"));

                    convertido = Math.Round(pago * Convert.ToDecimal(txtTipodecambio.Text), 2);

                                                            
                    gridViewDetalle.SetFocusedRowCellValue("APagarConvertido", convertido);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiEditar_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            Editar();
        }

        private void bbiCorrigeFacturaTesk_ItemClick(object sender, ItemClickEventArgs e)
        {
            CorrigeFactura();
        }

        private void CorrigeFactura()
        {
            try
            {
                if (intFolio == 0)
                {
                    MessageBox.Show("Seleccione un folio de pago");
                    return;
                }

                PagosCxPCL cl = new PagosCxPCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;

                gridControlCorrigeFac.DataSource = cl.PagosSacaFacturaParaTesk();
                gridViewCorrigeFac.OptionsBehavior.ReadOnly = true;
                gridViewCorrigeFac.OptionsBehavior.Editable = false;
                
                //if (result != "OK")
                //{
                //    MessageBox.Show(result);
                //    return;
                //}

                //txtNuevaFac.Text = cl.strRef;
                //lblFechaFac.Text = cl.fFecha.ToShortDateString();
                //txtFolioCR.Text = cl.intFolioCargo.ToString();

                bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiEnviar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiCargarCxP.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiCorrigeFacturaTesk.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                navigationFrame.SelectedPageIndex = 2;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ActualizaFacturaTesk();
        }

        private void ActualizaFacturaTesk()
        {
            try
            {
                PagosCxPCL cl = new PagosCxPCL();
                cl.strSerie = strSerie;
                cl.intFolioCargo = Convert.ToInt32(txtFolioCR.Text);
                cl.strRef = txtNuevaFac.Text;
                string result = cl.PagosActualizaFacturaParaTesk();
                MessageBox.Show(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            CierraPopUpTesk();
        }

        private void CierraPopUpTesk()
        {
            popupTesk.Visible = false;            
            txtTeskSeq.Text = string.Empty;
            txtTeskSaldo.Text = string.Empty;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                PagosCxPCL cl = new PagosCxPCL();
                globalCL clg = new globalCL();


                
                if (!clg.esNumerico(txtTeskSaldo.Text))
                {
                    MessageBox.Show("El saldo debe ser númerico");
                    return;
                }
                if (!clg.esNumerico(txtTeskSeq.Text))
                {
                    txtTeskSeq.Text = "0";
                }

                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intSeq = Convert.ToInt32(txtTeskSeq.Text);
                cl.dImporte = Convert.ToDecimal(txtTeskSaldo.Text);

                string result = cl.PagoscxpdetalleRedonondeosTesk();

                MessageBox.Show(result);

                if (result == "OK")
                    CierraPopUpTesk();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiTeskSaldos_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (intFolio == 0)
            {
                MessageBox.Show("Seleccione un folio");
                return;
            }

            popupTesk.Visible = true;
            groupControlTesk.Text = "Actualizar saldo  folio:" + strSerie + intFolio.ToString();
            txtTeskSeq.Focus();
        }

        private void labelControl14_Click(object sender, EventArgs e)
        {

        }

        private void gridViewCorrigeFac_RowClick(object sender, RowClickEventArgs e)
        {
           
                txtNuevaFac.Text = gridViewCorrigeFac.GetRowCellValue(gridViewCorrigeFac.FocusedRowHandle, "Referencia").ToString();
                txtFolioCR.Text = gridViewCorrigeFac.GetRowCellValue(gridViewCorrigeFac.FocusedRowHandle, "FolioContrarecibo").ToString();
                lblFechaFac.Text = gridViewCorrigeFac.GetRowCellValue(gridViewCorrigeFac.FocusedRowHandle, "Fecha").ToString();

        }

        private void lblPoliza_Click(object sender, EventArgs e)
        {

        }
    }
}