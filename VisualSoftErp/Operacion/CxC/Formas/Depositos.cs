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
using DevExpress.XtraBars;
using System.IO;
using VisualSoftErp.Clases.HerrramientasCLs;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using ViajesNet.Clases;
using DevExpress.XtraNavBar;

namespace VisualSoftErp.Catalogos
{

    public partial class Depositos : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        public BindingList<detalleCL> detalle;
        string strTo;
        int intFolio;
        string strSerie;
        string strMonedaDelPago;
        int intCliente;
        public bool blNuevo;
        int intCuentasbancariasID;
        int intUsuarioID = 1; //NO DEJAR FIJO 
        string Rfcbanco;
        string Cuentabancariabeneficiario;
        string Rfcbancoordenante;
        string Nombrebancoordenante;
        DateTime dFecha;
        string sMoneda;
        int timbrado;
        int AñoFiltro;
        int MesFiltro;

        public Depositos()
        {
            InitializeComponent();

            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            txtCuentaordenante.Properties.MaxLength = 50;
            txtCuentaordenante.EnterMoveNextControl = true;            
            txtHora.Properties.MaxLength = 8;
            txtHora.EnterMoveNextControl = true;

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;
            gridViewDetalle.OptionsView.ShowAutoFilterRow = true;

            //------------- Inicializar aquí opciones de columnas del grid ----------------
            //gridColumnIva.OptionsColumn.ReadOnly = true;
            //gridColumnIva.OptionsColumn.AllowFocus = false;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Depósitos";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            dateEditFecha.Text = DateTime.Now.ToShortDateString();

            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;
            LlenarGrid(AñoFiltro,MesFiltro);
            //las dos lineas siguientes hace que nos aparesca en el grid una columna y un check para seleccionar el renglon
            gridViewDetalle.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewDetalle.OptionsSelection.MultiSelect = true;

            AgregaAñosNavBar();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid(int Año,int Mes)
        {
            DepositosCL cl = new DepositosCL();
            cl.intAño = Año;
            cl.intMes = Mes;
            gridControlPrincipal.DataSource = cl.DepositosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridDepositos";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        public class detalleCL
        {
            public string Seriecdfi { get; set; }
            public int Foliocfdi { get; set; }
            public DateTime Fechafac { get; set; }
            public DateTime Fechavence { get; set; }
            public decimal Importe { get; set; }
            public decimal Supago { get; set; }
            public decimal Interes { get; set; }
            public int Diasvence { get; set; }
            public int ClientesId { get; set; }
            public decimal Supagoconvertido { get; set; }
        }

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridDepositosDetalle";
            clg.restoreLayout(gridViewDetalle);
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "serieCP";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Clave";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Des"].Visible = false;            
            cboSerie.ItemIndex = 0;

            cl.strTabla = "Clientes";
            cboClientesID.Properties.ValueMember = "Clave";
            cboClientesID.Properties.DisplayMember = "Des";
            cboClientesID.Properties.DataSource = cl.CargaCombos();
            cboClientesID.Properties.ForceInitialize();
            cboClientesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboClientesID.Properties.PopulateColumns();
            //cboClientesID.Properties.Columns["Clave"].Visible = false; //con esta propiedad puedo ocultar campos en el cbo
            cboClientesID.Properties.Columns["AgentesID"].Visible = false;
            cboClientesID.Properties.Columns["Plazo"].Visible = false;
            cboClientesID.Properties.Columns["Listadeprecios"].Visible = false;
            cboClientesID.Properties.Columns["Exportar"].Visible = false;
            cboClientesID.Properties.Columns["cFormapago"].Visible = false;
            cboClientesID.Properties.Columns["cMetodopago"].Visible = false;
            cboClientesID.Properties.Columns["BancoordenanteID"].Visible = false;
            cboClientesID.Properties.Columns["Cuentaordenante"].Visible = false;
            cboClientesID.Properties.Columns["PIva"].Visible = false;
            cboClientesID.Properties.Columns["PIeps"].Visible = false;
            cboClientesID.Properties.Columns["PRetiva"].Visible = false;
            cboClientesID.Properties.Columns["EMail"].Visible = false;
            cboClientesID.Properties.Columns["UsoCfdi"].Visible = false;
            cboClientesID.Properties.Columns["PRetIsr"].Visible = false;
            cboClientesID.Properties.Columns["cFormapagoDepositos"].Visible = false;
            cboClientesID.Properties.Columns["Moneda"].Visible = false;
            cboClientesID.Properties.Columns["DescuentoBase"].Visible = false;
            cboClientesID.Properties.Columns["DesctoPP"].Visible = false;
            cboClientesID.Properties.Columns["Desglosardescuentoalfacturar"].Visible = false;
            cboClientesID.Properties.Columns["TransportesID"].Visible = false;
            cboClientesID.Properties.Columns["Addenda"].Visible = false;
            cboClientesID.Properties.Columns["SerieEle"].Visible = false;
            cboClientesID.Properties.NullText = "Seleccione un cliente";

            cl.strTabla = "cyb_Chequeras";
            cboCuentasbancariaID.Properties.ValueMember = "Clave";
            cboCuentasbancariaID.Properties.DisplayMember = "Des";
            cboCuentasbancariaID.Properties.DataSource = cl.CargaCombos();
            cboCuentasbancariaID.Properties.ForceInitialize();
            cboCuentasbancariaID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCuentasbancariaID.Properties.PopulateColumns();
            cboCuentasbancariaID.Properties.Columns["Clave"].Visible = false;
            cboCuentasbancariaID.Properties.Columns["MonedasID"].Visible = false;
            cboCuentasbancariaID.Properties.Columns["Cuentabancaria"].Visible = false;
            cboCuentasbancariaID.Properties.NullText = "Seleccione una chequera";

            cl.strTabla = "cyb_bancos";
            cboBancosId.Properties.ValueMember = "Clave";
            cboBancosId.Properties.DisplayMember = "Des";
            cboBancosId.Properties.DataSource = cl.CargaCombos();
            cboBancosId.Properties.ForceInitialize();
            cboBancosId.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboBancosId.Properties.PopulateColumns();
            cboBancosId.Properties.Columns["Clave"].Visible = false;
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
            cboClientesID.Enabled = true;
            dateEditFecha.Enabled = true;
            //  CargaCombos();
            //BuscarSerie();
            SiguienteID();

            //Oculta los litros en el segundo frame
            navBarControl.Visible = false;
            ribbonStatusBar.Visible = false;

          //  DatosdecontrolCL cl = new DatosdecontrolCL();
           // string result = cl.DatosdecontrolLeer();

           // if (result=="OK")
            //{
               // cboCuentasbancariaID.EditValue = cl.intDepositoschequerabeneficiarioID;
           // }

            chkTimbrar.Checked = true;
            

            navigationFrame.SelectedPageIndex = 1;
        }

        private void LimpiaCajas()
        {
            Inicialisalista();
            cboSerie.ItemIndex = 0;
            txtFolio.Text = string.Empty;
            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            txtImporte.Text = "";
            cboClientesID.EditValue = null;
            cboCuentasbancariaID.ItemIndex = 0;
            //txtTipodecambio.Text = "";
            cboBancosId.EditValue = null;
            txtCuentaordenante.Text = string.Empty;            
            txtNumerooperacion.Text = "1";
            txtHora.Text = "12:00:00";
            txtFactorajeInteres.Text = string.Empty;
            txtFactorajeNombre.Text = string.Empty;
            txtFactorajePtjeInteres.Text = string.Empty;
            txtFactorajePtjePago.Text = string.Empty;
            txtFactorajeRfc.Text = string.Empty;
        }

        private string BuscarSerie()
        {
            DepositosCL cl = new DepositosCL();
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
            string serie = ConfigurationManager.AppSettings["serieCP"].ToString();

            DepositosCL cl = new DepositosCL();
            cl.strSerie = serie;
            cl.strDoc = "Depositos";

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
                    {
                        MessageBox.Show(Result);
                    }
               
                    return;
                }
                if (chkTimbrar.Checked)
                {
                    Result = Timbra();
                    if (Result != "OK")
                    {
                        MessageBox.Show(Result);
                        return;
                    }
                }

                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtFactorajeInteres.Text))
                    txtFactorajeInteres.Text = "0";
             
                string sCondicion = String.Empty;
                //DT para Depositos
                System.Data.DataTable dtDepositos = new System.Data.DataTable("Depositos");
                dtDepositos.Columns.Add("Serie", Type.GetType("System.String"));
                dtDepositos.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtDepositos.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtDepositos.Columns.Add("Importe", Type.GetType("System.Decimal"));
                dtDepositos.Columns.Add("ClientesID", Type.GetType("System.Int32"));
                dtDepositos.Columns.Add("CuentasBancariaID", Type.GetType("System.Int32"));
                dtDepositos.Columns.Add("Tipodecambio", Type.GetType("System.Decimal"));
                dtDepositos.Columns.Add("BancosId", Type.GetType("System.Int32"));
                dtDepositos.Columns.Add("CuentaOrdenante", Type.GetType("System.String"));
                dtDepositos.Columns.Add("C_Formapago", Type.GetType("System.String"));
                dtDepositos.Columns.Add("NumeroOperacion", Type.GetType("System.Int32"));
                dtDepositos.Columns.Add("C_TipoCadenaPago", Type.GetType("System.Int32"));
                dtDepositos.Columns.Add("MonedaDelPago", Type.GetType("System.String"));
                dtDepositos.Columns.Add("Status", Type.GetType("System.String"));
                dtDepositos.Columns.Add("Hora", Type.GetType("System.String"));
                dtDepositos.Columns.Add("FechaReal", Type.GetType("System.DateTime"));
                dtDepositos.Columns.Add("Timbrado", Type.GetType("System.Int32"));
                dtDepositos.Columns.Add("FactorInteres", Type.GetType("System.Decimal"));
                dtDepositos.Columns.Add("Tesk", Type.GetType("System.Int32"));
                dtDepositos.Columns.Add("TeskPoliza", Type.GetType("System.String"));
                dtDepositos.Columns.Add("RazonCancelacion", Type.GetType("System.String"));
                dtDepositos.Columns.Add("FechaCancelacion", Type.GetType("System.DateTime"));
                dtDepositos.Columns.Add("DepositoRecepcion", Type.GetType("System.Int32"));

                //DT para Depositos
                System.Data.DataTable dtDepositosdetalle = new System.Data.DataTable("Depositosdetalle");
                dtDepositosdetalle.Columns.Add("SERIE", Type.GetType("System.String"));
                dtDepositosdetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtDepositosdetalle.Columns.Add("SEQ", Type.GetType("System.Int32"));
                dtDepositosdetalle.Columns.Add("SerieCFDI", Type.GetType("System.String"));
                dtDepositosdetalle.Columns.Add("FolioCfdi", Type.GetType("System.Int32"));
                dtDepositosdetalle.Columns.Add("SUPAGO", Type.GetType("System.Decimal"));
                dtDepositosdetalle.Columns.Add("SUPAGOCONVERTIDO", Type.GetType("System.Decimal"));
                dtDepositosdetalle.Columns.Add("TipoMov", Type.GetType("System.Int32"));
                dtDepositosdetalle.Columns.Add("CxCID", Type.GetType("System.Int32"));
                dtDepositosdetalle.Columns.Add("ClientesID", Type.GetType("System.Int32"));
                dtDepositosdetalle.Columns.Add("Interes", Type.GetType("System.Decimal"));

                if (Result != "OK")
                {
                    MessageBox.Show("No se pudo leer el siguiente folio");
                    return;
                }
                //iFolio = clg.intSigFolio;
                int iFolio = 0;
                int intRen = 0;
                string dato = String.Empty;
                string strSerieCFDI = String.Empty;
                int intFolioCfdi = 0;
                decimal dSUPAGO = 0;
                int intSeq = 0;
                decimal dsupagoconvertido = 0;
                int iTipomov = 0;
                decimal dTipocambio = Convert.ToDecimal(txtTipodecambio.Text);
                int Timbrado = 0;
                int intcxcID = 0;
                decimal decInteres = 0;
                int intClientesID = Convert.ToInt32(cboClientesID.EditValue);
                int clienteDet = 0;

                foreach (int i in gridViewDetalle.GetSelectedRows())
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "Factura").ToString();
                    if (dato.Length > 0)
                    {
                        
                        strSerieCFDI = gridViewDetalle.GetRowCellValue(i, "SerieFactura").ToString();
                        intFolioCfdi = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Factura"));
                        dSUPAGO = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "APagar"));

                        if (gridViewDetalle.GetRowCellValue(i, "Interes") == null)
                            decInteres = 0;
                        else                            
                            decInteres = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Interes"));
                        dsupagoconvertido = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Supagoconvertido"));
                        intcxcID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "CxCID"));
                        iTipomov = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "TipoMov"));
                        clienteDet= Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "ClientesId"));

                        
                        if (clienteDet == 0)
                            clienteDet = intClientesID;

                        if (dSUPAGO > 0)
                        {
                            intSeq++;
                            dtDepositosdetalle.Rows.Add(
                            cboSerie.EditValue,
                            txtFolio.Text,
                            intSeq,
                            strSerieCFDI,
                            intFolioCfdi,
                            dSUPAGO,
                            dsupagoconvertido,
                            iTipomov,
                            intcxcID,
                            clienteDet,
                            decInteres
                            );
                        }


                        
                        if (decInteres > 0)
                        {
                            intSeq++;
                            dtDepositosdetalle.Rows.Add(
                            cboSerie.EditValue,
                            txtFolio.Text,
                            intSeq,
                            strSerieCFDI,
                            intFolioCfdi,
                            decInteres,
                            decInteres,
                            iTipomov,
                            intcxcID,
                            clienteDet,
                            0
                            );
                        }


                    }
                }
                strSerie = cboSerie.EditValue.ToString();
                int intfolio = Convert.ToInt32(txtFolio.Text);
                DateTime fFecha = Convert.ToDateTime(dateEditFecha.Text);
                decimal dImporte = Convert.ToDecimal(txtImporte.Text);
                
                int intCuentasBancariaID = Convert.ToInt32(cboCuentasbancariaID.EditValue);
                decimal dTipodecambio = Convert.ToDecimal(txtTipodecambio.Text);
                int intBancosId = Convert.ToInt32(cboBancosId.EditValue);
                string strCuentaOrdenante = txtCuentaordenante.Text;
                string strC_Formapago = cboC_Formapago.EditValue.ToString();
                int intNumeroOperacion = Convert.ToInt32(txtNumerooperacion.Text);
                int intC_TipoCadenaPago = 0;                
                string strStatus = "1";
                string strHora = txtHora.Text;
                DateTime fFechaReal = Convert.ToDateTime(DateTime.Now);
                int intDepositoRecepcion = swDepositoRecepcion.IsOn ? 1 : 0;
                dato = String.Empty;
                if (chkTimbrar.Checked)
                {
                    Timbrado = 1;
                }
                else
                {
                    Timbrado = 0;
                }

                if (!clg.esNumerico(txtFactorajePtjeInteres.Text))
                    txtFactorajePtjeInteres.Text = "0";

                    dtDepositos.Rows.Add(
                    strSerie,
                    intfolio, 
                    fFecha, 
                    dImporte, 
                    intClientesID, 
                    intCuentasBancariaID, 
                    dTipodecambio, 
                    intBancosId, 
                    strCuentaOrdenante, 
                    strC_Formapago, 
                    intNumeroOperacion, 
                    intC_TipoCadenaPago, 
                    strMonedaDelPago, 
                    strStatus, 
                    strHora, 
                    fFechaReal,
                    Timbrado,
                    Convert.ToDecimal(txtFactorajePtjeInteres.Text),
                    0,
                    "",
                    "",
                    fFecha,
                    intDepositoRecepcion
                    );

                DepositosCL cl = new DepositosCL();
                cl.strSerie = cboSerie.EditValue.ToString();
                strSerie = cl.strSerie;
                cl.intFolio = Convert.ToInt32(txtFolio.Text);               
                cl.dtm = dtDepositos;
                cl.dtd = dtDepositosdetalle;
                cl.intUsuarioID = 1;
                cl.strMaquina = Environment.MachineName;
                cl.strPrograma = "0520";
                string result = cl.DepositosCrud();
                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    
                        if (blNuevo)
                        {
                            if (chkTimbrar.Checked)
                            {
                                Impresion(cl.strSerie, cl.intFolio, fFecha, strMonedaDelPago);
                            }

                            DatosdecontrolCL cld = new DatosdecontrolCL();
                            result = cld.DatosdecontrolLeer();
                            if (result == "OK")
                            {
                                if (cld.iEnvioCfdiAuto == 1)
                                {
                                    intFolio = Convert.ToInt32(txtFolio.Text);                                    
                                    dFecha = fFecha;
                                    EnviaCorreo();
                                    intFolio = 0;
                                }
                            }

                            LimpiaCajas();
                            SiguienteID();
                            cboClientesID.Enabled = true;
                            dateEditFecha.Enabled = true;
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

        private string Timbra()
        {
            try
            {

                complementodepagoCL cl = new complementodepagoCL();
                SiguienteID();
                cl.pImporte = txtImporte.Text;
                cl.pMoneda = strMonedaDelPago;
                cl.pFP = cboC_Formapago.EditValue.ToString();
                cl.pParidad = txtTipodecambio.Text;
                cl.pFecha = Convert.ToDateTime(dateEditFecha.Text);
                cl.pHora = txtHora.Text;
                cl.pNumOpe = txtNumerooperacion.Text;
                cl.pBancoidBen = Convert.ToInt32(cboCuentasbancariaID.EditValue.ToString());
                cl.pRfcbancobeneficiario = Rfcbanco;
                cl.pCuentaBeneficiario = Cuentabancariabeneficiario;
                cl.pRfcbancoordentante = Rfcbancoordenante;
                cl.pNombrebancoordenante = Nombrebancoordenante;
                cl.pCuentaOrdenante = txtCuentaordenante.Text;
                cl.intCliente = Convert.ToInt32(cboClientesID.EditValue);
                cl.pFolio = Convert.ToInt32(txtFolio.Text);
                cl.pFactorajeNombre = txtFactorajeNombre.Text;
                cl.pFactorajeRfc = txtFactorajeRfc.Text;
                cl.pFactorInteres = Convert.ToDecimal(txtFactorajeInteres.Text);
                cl.TipoRelacion = txtTipoRelacion.Text;
                cl.UUIDRelacion = txtUUIDRelacion.Text;
                //cl.pInteres = Convert.ToDecimal(txtFactorajeInteres.Text);

                if (txtTipoRelacion.Text.Length > 0)
                {
                    if (txtTipoRelacion.Text != "04")
                    {
                        return "El tipo de relación debe ser 04 y usarlo solo cuando vaya a cancelar un cfdi previo por motivo 01";
                    }

                    if (txtUUIDRelacion.Text.Length == 0)
                    {
                        return "Si hay tipo relación, debe leer el UUID del cfdi anterior";
                    }
                }



                //con la clse que tenemos mandamos llamar el grid para timbrar
                string result = cl.TimbraCfdi33Complemento(gridViewDetalle);


                return result;
            }
            catch (Exception ex)
            {
                return "Timbra:" + ex.Message;
            }
        }//Timbra

        private void Impresion(string serie, int Folio, DateTime Fecha, string Moneda)
        {
            try
            {
                vsFK.vsFinkok vs = new vsFK.vsFinkok();

                string rutaXML = string.Empty;
                string rutaPDF = string.Empty;
                string sYear = string.Empty;
                string sMes = string.Empty;

                globalCL clg = new globalCL();
                sYear = Fecha.Year.ToString();
                sMes = clg.NombreDeMes(Fecha.Month,3);

                rutaXML = ConfigurationManager.AppSettings["pathxml"].ToString() + "\\" + sYear + "\\" + sMes + "\\" + serie + Folio + ".xml";//luego actualizar a solo sera seriefolio.xml
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
                rutaPDF = rutaPDF + serie + Folio + ".pdf";

                vs.RutaXmlTimbrado = rutaXML;
                vs.RutaPdfTimbrado = rutaPDF;

                vs.Moneda33 = Moneda;
                vs.ArchivoTcr = "PAGOS.vx25";
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

                DepositosCL cl = new DepositosCL();
                cl.strSerie = serie;
                cl.intFolio = Folio;
                result = cl.DepositosLlenaCajas();
                if (result == "OK")
                {
                    vs.CampoExtra3 = cl.strC_Formapago;
                    vs.CampoExtraP5 = cl.dImporte.ToString("c2");
                }

                else
                {
                    MessageBox.Show("No se pudo leer el depósito: " + serie + Folio.ToString() + "   " + result);
                    return;
                }

                vs.CampoExtra7 = vs.ExtraeValor(rutaXML, "pago10:Pago", "FormaDePagoP");
                vs.CampoExtra8 = vs.ExtraeValor(rutaXML, "pago10:Pago", "Monto");

                vs.CampoExtra10 = vs.ExtraeValor(rutaXML, "pago10:Pago", "RfcEmisorCtaBen");
                vs.CampoExtra2 = vs.ExtraeValor(rutaXML, "pago10:Pago", "RfcEmisorCtaOrd");

                //vs.CampoExtraP5 = vs.ExtraeValor(rutaXML, "pago10:Pago", "RfcEmisorCtaBen");

                vs.ImpresoraNombre = "";
                vs.Copias = "1";
                vs.Moneda33 = Moneda;
                vs.Cliente = ".";
                vs.Leyenda1 = ".";

                if (Folio< 13062)
                {
                  //  result = vs.ImprimeFormatoTagCode();
                    //if (result != "OK")
                    //{
                    //    MessageBox.Show("Al imprimir: " + result);
                    //}
                    //else
                    //{
                    //    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    //    proc.StartInfo.FileName = rutaPDF;
                    //    proc.Start();
                    //    proc.Close();
                    //}
                }
                else
                {
                    vs.EmpresaLogo = ConfigurationManager.AppSettings["logoEmpresa"].ToString();
                    result = vs.generaCPPdf();
                    if (result != "OK")
                    {
                        MessageBox.Show("Al imprimir: " + result);
                    }
                    else
                    {
                        System.Diagnostics.Process proc = new System.Diagnostics.Process();
                        proc.StartInfo.FileName = rutaPDF;
                        proc.Start();
                        proc.Close();
                    }
                }


               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Impresión: " + ex.Message);
            }
        }//Impresion

        private string Valida()
        {
            globalCL clg = new globalCL();
            if (!clg.esNumerico(txtFactorajeInteres.Text))
                txtFactorajeInteres.Text = "0";
            if (!clg.esNumerico(txtFactorajePtjeInteres.Text))
                txtFactorajePtjeInteres.Text = "0";
            if (!clg.esNumerico(txtFactorajePtjePago.Text))
                txtFactorajePtjePago.Text = "0";

            
            if (cboSerie.Text=="[Vacío]")
            {
                return "Seleccione la serie";
            }

            if (cboSerie.Text.Length == 0)
            {
                return "El campo Serie no puede ir vacio";
            }
            if (txtFolio.Text.Length == 0)
            {
                return "El campo Folio no puede ir vacio";
            }
            if (txtImporte.Text.Length == 0)
            {
                return "El campo Importe no puede ir vacio";
            }
            if (cboClientesID.EditValue == null)
            {
                return "El campo ClientesID no puede ir vacio";
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
            if (txtNumerooperacion.Text.Length == 0)
            {
                txtNumerooperacion.Text = "0";
            }            
            if (txtHora.Text.Length == 0)
            {
                return "El campo Hora no puede ir vacio";
            }
            globalCL clG = new globalCL();
            if (strMonedaDelPago != "MXN")
            {
                if (!clG.esNumerico(txtTipodecambio.Text)) {
                    return "Teclee el tipo de cambio";
                }
                if (Convert.ToDecimal(txtTipodecambio.Text)<=1)
                {
                    return "Teclee el tipo de cambio";
                }
            }

            decimal importe;
            decimal suPago = 0;
            decimal totalPago = 0;
            string strMP = string.Empty;
            bool blnLlevaSerie = false;
            string sfac = string.Empty;

            foreach (int i in gridViewDetalle.GetSelectedRows())
            {
                if (gridViewDetalle.GetRowCellValue(i, "APagar") != null)
                {

                    sfac = gridViewDetalle.GetRowCellValue(i, "SerieFactura").ToString();
                    if (sfac.Length > 0)
                        blnLlevaSerie = true;

                    strMP = gridViewDetalle.GetRowCellValue(i, "MP").ToString();

                    if (strMP=="PUE" && chkTimbrar.Checked)
                    {
                        DialogResult result = MessageBox.Show("Si la factura tiene método de pago PUE, no se puede timbrar el complemento, continua?", "Advertencia", MessageBoxButtons.YesNo);
                        if (result.ToString() == "No")
                        {
                            return "";
                        }
                        //return "Si la factura tiene método de pago PUE, no se puede timbrar el complemento";
                    }
                    if (strMP=="PPD" && !chkTimbrar.Checked)
                    {
                        if (!blnLlevaSerie)
                        {
                            DialogResult result = MessageBox.Show("Si el método de pago de la factura es PPD, debe timbrar el complento, continua?", "Advertencia", MessageBoxButtons.YesNo);
                            if (result.ToString() == "No")
                            {
                                return "";
                            }
                        }
                       
                    }

                    suPago = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Supagoconvertido"));
                    totalPago += suPago;
                }
            }

            if (blnLlevaSerie && chkTimbrar.Checked)
            {
                DialogResult result2 = MessageBox.Show("Hay facturas con serie que normalmente no se timbran y tiene habilitado timbrar, continua?", "Advertencia", MessageBoxButtons.YesNo);
                if (result2.ToString() == "No")
                {
                    return "";
                }
            }

            importe = Convert.ToDecimal(clG.Dejasolonumero(txtImporte.Text));
            decimal interes = Math.Round(Convert.ToDecimal(clG.Dejasolonumero(txtFactorajeInteres.Text)),2);

            if (importe != totalPago)
            {
                DialogResult result = MessageBox.Show("El importe pagado y la suma de los pagos no concuerda, continua?","Advertencia", MessageBoxButtons.YesNo);
                if (result.ToString() == "No")
                {
                    return "";
                }
            }


            string result3 = clg.GM_CierredemodulosStatus(Convert.ToDateTime(dateEditFecha.Text).Year, Convert.ToDateTime(dateEditFecha.Text).Month, "CXC");
            if (result3 == "C")
            {
                if (DateTime.Now.Day>5)  //Se dan 5 días de gracia por que a veces los 1os dias del mes aun estan capturando cobranza del mes anterior
                    return "Este mes está cerrado, no se puede actualizar";
            }


            return "OK";
        } //Valida

        private void llenaCajas()
        {
            DepositosCL cl = new DepositosCL();
            cl.strSerie = cboSerie.EditValue.ToString();
            cl.intFolio = Convert.ToInt32(txtFolio.Text);
            String Result = cl.DepositosLlenaCajas();
            if (Result == "OK")
            {
                cboSerie.EditValue = cl.strSerie.ToString();
                txtFolio.Text = cl.intFolio.ToString();
                dateEditFecha.Text = cl.fFecha.ToShortDateString();
                txtImporte.Text = cl.dImporte.ToString();
                cboClientesID.EditValue = cl.intClientesID;
                cboCuentasbancariaID.EditValue = cl.intCuentasBancariaID;
                txtTipodecambio.Text = cl.dTipodecambio.ToString();
                cboBancosId.EditValue = cl.intBancosId;
                txtCuentaordenante.Text = cl.strCuentaOrdenante;
                cboC_Formapago.EditValue = cl.strC_Formapago;
                txtNumerooperacion.Text = cl.strC_Formapago;                
                txtHora.Text = cl.strHora;
                swDepositoRecepcion.IsOn = cl.intDepositoRecepcion == 1 ? true : false;
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
            llenaCajas();
        }

        //borrar si no se usa editar
        private void BotonesEdicion()
        {
            LimpiaCajas();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCargarfacturas.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVerificar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEnviar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCargarCxC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            navigationFrame.SelectedPageIndex = 1;
        }       

        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Cargando grid principal...");

            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCargarfacturas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVerificar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEnviar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCargarCxC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LlenarGrid(AñoFiltro,MesFiltro);

            intFolio = 0;
            strSerie = null;
            navigationFrame.SelectedPageIndex = 0;

            navBarControl.Visible = true;
            navBarControl.Visible = true;
            ribbonStatusBar.Visible = true;

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridDepositos";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            clg.strGridLayout = "gridDepositosDetalle";
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

        private void gridViewPrincipal_RowClick(Object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            strSerie = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie").ToString();
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            dFecha = Convert.ToDateTime(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Fecha"));
            sMoneda = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "MonedaDelPago").ToString();
            timbrado = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Timbrado"));
            string status = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status").ToString();
            strTo = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "EMail").ToString();

            if (status=="Registrado")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void txtFolio_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void chkTimbrar_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cboClientesID_EditValueChanged(object sender, EventArgs e)
        {

            object orow = cboClientesID.Properties.GetDataSourceRowByKeyValue(cboClientesID.EditValue);
            if (orow != null)
            {
                int bancoOrd = Convert.ToInt32(((DataRowView)orow)["BancoordenanteId"]);
                string cta = ((DataRowView)orow)["Cuentaordenante"].ToString();
                string fp = ((DataRowView)orow)["cFormapagoDepositos"].ToString();
                strTo = ((DataRowView)orow)["EMail"].ToString();

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

        private void bbiCargarfacturas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cargarfacturas();
        }

        //Carga las facturas pendientes de pago para el ciente selecionado
        private void Cargarfacturas()
        {
            try
            {
                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtCte2.Text))
                    txtCte2.Text = "0";

                DepositosCL cl = new DepositosCL();
                cl.intClientesID = intCliente;
                cl.intCliente2 = Convert.ToInt32(txtCte2.Text);
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                string result = cl.DepositosGeneraAntiguedaddesaldos();
                if (result == "OK")
                {
                    gridControlDetalle.DataSource = cl.DepositosCargaFacturas();
                    if (gridViewDetalle.RowCount > 1)
                        cboClientesID.Enabled = false;
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch(Exception ex)
            {
                string error = "Error en CargarFacturas línea" + ex.LineNumber().ToString() + "\n" + ex.Message;
                MessageBox.Show(error);
            }
                                
        }

        private void cboCuentasbancariaID_EditValueChanged(object sender, EventArgs e)
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
                    Cuentabancariabeneficiario = ((DataRowView)orow)["Cuentabancaria"].ToString();

                }

                //Limpiara tipo de cambio paa poder ingresar el valor
                txtTipodecambio.Text = "";
                txtTipodecambio.ReadOnly = false;

                //if (strMonedaDelPago == "MXN")
                //{
                //    txtTipodecambio.Text = "1";
                //    txtTipodecambio.ReadOnly = true;
                //}
            }

            catch (Exception)
            {                

            }
           
        }
        
        //Estas lienas le dan vida a los litros de la izq en el grid principal
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
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 1";
            MesFiltro = 1;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemFeb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 2";
            MesFiltro = 2;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemMar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 3";
            MesFiltro = 3;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemAbr_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 4";
            MesFiltro = 4;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemMay_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 5";
            MesFiltro = 5;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemJun_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 6";
            MesFiltro = 6;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemJul_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 7";
            MesFiltro = 7;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemAgo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 8";
            MesFiltro = 8;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemsep_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 9";
            MesFiltro = 9;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemOct_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 10";
            MesFiltro = 10;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemNov_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 11";
            MesFiltro = 11;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemDic_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 12";
            MesFiltro = 12;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemTodos_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilter.Clear();
            MesFiltro = 0;
            LlenarGrid(AñoFiltro, MesFiltro);
        }
        #endregion

        #region Filtros Inferiores       

        private void bbiRegistrados_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Registrado'";
        }      

        private void bbiCancelados_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Cancelada'";
        }

        private void bbiTodos_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilter.Clear();
        }
        #endregion

        private void gridViewDetalle_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            decimal saldo = 0;
            decimal interes = 0;
            decimal suPagoConvertido = 0;
            string monedaFac = string.Empty;
          
            int controllerRow = e.ControllerRow;
            if (controllerRow == GridControl.InvalidRowHandle)
            {

                Int32[] selectedRowHandles = gridViewDetalle.GetSelectedRows();

                if (selectedRowHandles.Length==0)
                {
                    for (int i = 0; i <= gridViewDetalle.RowCount; i++)
                    {
                        gridViewDetalle.FocusedRowHandle = i;                        
                        gridViewDetalle.SetFocusedRowCellValue("APagar", 0);
                        gridViewDetalle.SetFocusedRowCellValue("Supagoconvertido", 0);
                    }
                }
                else
                {
                    for (int i = 0; i <= gridViewDetalle.RowCount; i++)
                    {
                        gridViewDetalle.FocusedRowHandle = i;

                        monedaFac = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Moneda").ToString();


                        if (txtFactorajePtjeInteres.Text == "")
                            txtFactorajePtjeInteres.Text = "0";
                        //Factoraje
                        if (Convert.ToDecimal(txtFactorajePtjeInteres.Text) > 0)
                        {
                            saldo = Math.Round(Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Importe")) * Convert.ToDecimal(txtFactorajePtjePago.Text),2);
                            interes = Math.Round(Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Importe")) * (Convert.ToDecimal(txtFactorajePtjeInteres.Text)/100), 2);
                        }
                        else
                        {
                            saldo = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Importe"));
                        }

                        if (monedaFac != strMonedaDelPago)
                        {
                            if (strMonedaDelPago == "MXN")
                                suPagoConvertido = Math.Round(saldo * Convert.ToDecimal(txtTipodecambio.Text),2);
                            else
                                suPagoConvertido = Math.Round(saldo / Convert.ToDecimal(txtTipodecambio.Text),2);
                        }
                        else
                            suPagoConvertido = saldo;
                            

                        gridViewDetalle.SetFocusedRowCellValue("APagar", saldo);
                        gridViewDetalle.SetFocusedRowCellValue("Interes", interes);
                        gridViewDetalle.SetFocusedRowCellValue("Supagoconvertido", suPagoConvertido);
                    }
                }                
            }

            else
            {
                if (e.Action.ToString() == "Add")
                {
                    globalCL clg = new globalCL();
                    if (!clg.esNumerico(txtFactorajePtjeInteres.Text))
                        txtFactorajePtjeInteres.Text = "0";

                    monedaFac = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Moneda").ToString();

                    //Factoraje
                    if (Convert.ToDecimal(txtFactorajePtjeInteres.Text) > 0)
                    {
                        saldo = Math.Round(Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Importe")) * Convert.ToDecimal(txtFactorajePtjePago.Text), 2);
                        interes = Math.Round(Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Importe")) * (Convert.ToDecimal(txtFactorajePtjeInteres.Text)/100), 2);
                    }
                    else
                    {
                        saldo = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Importe"));
                    }

                    if (monedaFac != strMonedaDelPago)
                    {
                        if (!clg.esNumerico(txtTipodecambio.Text))
                        {
                            MessageBox.Show("Teclee el tipo de cambio");
                            return;
                        }
                        if (strMonedaDelPago == "MXN")
                            suPagoConvertido = Math.Round(saldo * Convert.ToDecimal(txtTipodecambio.Text),2);
                        else
                            suPagoConvertido = Math.Round(saldo / Convert.ToDecimal(txtTipodecambio.Text),2);
                    }
                    else
                        suPagoConvertido = saldo;

                    //saldo = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Importe"));
                    gridViewDetalle.SetFocusedRowCellValue("APagar", saldo);
                    gridViewDetalle.SetFocusedRowCellValue("Interes", interes);
                    gridViewDetalle.SetFocusedRowCellValue("Supagoconvertido", suPagoConvertido);
                }
                else
                {
                    gridViewDetalle.SetFocusedRowCellValue("APagar", 0);
                    gridViewDetalle.SetFocusedRowCellValue("Supagoconvertido", 0);
                }
            }            
            gridViewDetalle.UpdateTotalSummary();
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
            Impresion(strSerie, intFolio, dFecha, sMoneda);
        }

        private void bbiCancelar_ItemClick(object sender, ItemClickEventArgs e)
        {
            popUpCancelar.Visible = true;
            groupControl3.Text = "Cancelar el folio:" + strSerie + intFolio.ToString();
            txtLogin.Focus();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            CierraPopUp();
        }

        private void CierraPopUp()
        {
            popUpCancelar.Visible = false;
            txtLogin.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }
        private void CierraPopUpTesk()
        {
            popupTesk.Visible = false;
            txtTeskSerie.Text = string.Empty;
            txtTeskFac.Text = string.Empty;
        }

        private void Cancelar()
        {
            try
            {
                globalCL clg = new globalCL();
                string result = clg.GM_CierredemodulosStatus(dFecha.Year, dFecha.Month, "CXC");
                if (result == "C")
                {
                    MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                    return;
                }


                //FacturacionCL cl = new FacturacionCL();
                string año = dFecha.Year.ToString();
                int mes = dFecha.Month;

                string nombremes = clg.NombreDeMes(mes,3);

                string serie = ConfigurationManager.AppSettings["serieCP"].ToString();

                vsFK.vsFinkok vs = new vsFK.vsFinkok();
                string pRutaXML = System.Configuration.ConfigurationManager.AppSettings["pathxml"];
                string pRutaCer = System.Configuration.ConfigurationManager.AppSettings["pathcer"];
                string pRutaKey = System.Configuration.ConfigurationManager.AppSettings["pathkey"];
                string pRutaOpenSSL = System.Configuration.ConfigurationManager.AppSettings["pathopenssl"];

                cfdiCL cl2 = new cfdiCL();
                cl2.pSerie = System.Configuration.ConfigurationManager.AppSettings["serieCP"];
                string strresult = cl2.DatosCfdiEmisor();
                if (strresult != "OK")
                {
                    MessageBox.Show("No se pudieron leer los datos del emisor");
                    return;
                }

                string strarchivo = pRutaXML + año + "\\" + nombremes + "\\" + strSerie + intFolio.ToString() + ".xml";
                string strUUID = vs.ExtraeValor(strarchivo, "tfd:TimbreFiscalDigital", "UUID");
                string strnombrearchivo = strSerie + intFolio.ToString() + "timbrado.xml";
                string strrutaxml = pRutaXML + año + "\\" + nombremes + "\\";

                if (strUUID.Length==0)
                {
                    MessageBox.Show("No se pudo leer el UUID en: " + pRutaXML + año + "\\" + nombremes + "\\" + strSerie + intFolio.ToString() + ".xml");
                    return;
                }

            
                strresult = clg.CancelaTimbrado(strSerie, strrutaxml + strSerie + intFolio.ToString() + ".xml", strnombrearchivo, txtMotivo22.Text, txtUUIDNuevo22.Text);

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
                    //Cancerlarenlabasededatos();
                    DepositosCL cld = new DepositosCL();
                    string sserieCP = System.Configuration.ConfigurationManager.AppSettings["serieCP"].ToString();
                    cld.strSerie = strSerie;
                    cld.intFolio = intFolio;
                    cld.strRazon = txtRazon.Text;
                    
                    string strresultado = cld.DepositosCancelar();
                    if (strresultado != "OK")
                    {
                        MessageBox.Show("Al cancelar en la BD: " + strresultado);                       
                    }
                    else
                    {
                        MessageBox.Show("Cancelado correctamente");
                        CierraPopUp();
                        bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        LlenarGrid(AñoFiltro,MesFiltro);
                    }                   
                }
                else
                {
                    MessageBox.Show("vsfk: \n" + strresult);
                }
            }
            catch(Exception ex)
            {
                int linenum = ex.LineNumber();
                MessageBox.Show("Cancelar: " + ex.Message + " LN:" + linenum.ToString());
            }
        }

        private void Cancerlarenlabasededatos()
        {
            DepositosCL cl = new DepositosCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            cl.strRazon = txtRazon.Text;
            MessageBox.Show("DepositosCancelar");
            string result = cl.DepositosCancelar();
            if (result == "OK")
            {
                MessageBox.Show("Cancelado correctamente");
                LlenarGrid(AñoFiltro,MesFiltro);
            }
               
            else
                MessageBox.Show(result);
        }

        private void btnAut_Click(object sender, EventArgs e)
        {

            if (txtRazon.Text.Length == 0)
            {
                MessageBox.Show("Teclee la razón");
                return;
            }

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
            if (timbrado == 1 && !swInterno.IsOn)
            {
                Cancelar();
            }
            else
            {
                Cancerlarenlabasededatos();
            }
            
        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string _mark = (string)view.GetRowCellValue(e.RowHandle, "Status");

            if (_mark == "Cancelado")
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void cboClientesID_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            intCliente = Convert.ToInt32(e.NewValue);
            
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

        private void bbiVerificar_ItemClick(object sender, ItemClickEventArgs e)
        {
            globalCL cl = new globalCL();
            cl.VerificaComprobante(strSerie, intFolio, dFecha);
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

                string result=cl.EnviaCorreo(strTo, strSerie, intFolio, dFecha,"P");

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

        #region FitrosMeses
        private void navBarItemEne_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
//            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " And [Mes]=1";
            MesFiltro = 1;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemFeb_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " And [Mes]=2";
            MesFiltro = 2;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemMar_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " And [Mes]=3";
            MesFiltro = 3;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemAbr_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " And [Mes]=4";
            MesFiltro = 4;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemMay_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " And [Mes]=5";
            MesFiltro = 5;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemJun_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " And [Mes]=6";
            MesFiltro = 6;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemJul_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " And [Mes]=7";
            MesFiltro = 7;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemAgo_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " And [Mes]=8";
            MesFiltro = 8;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemsep_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " And [Mes]=9";
            MesFiltro = 9;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemOct_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " And [Mes]=10";
            MesFiltro = 10;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemNov_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " And [Mes]=11";
            MesFiltro = 11;
            LlenarGrid(AñoFiltro, MesFiltro);
        }

        private void navBarItemDic_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " And [Mes]=12";
            MesFiltro = 12;
            LlenarGrid(AñoFiltro, MesFiltro);
        }
        
        private void navBarItemTodos_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //gridViewPrincipal.ActiveFilter.Clear();
            MesFiltro = 0;
            LlenarGrid(AñoFiltro, MesFiltro);
        }
        #endregion FitrosMeses
        
        private void bbiCargarCxC_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cargarfacturas();
        }

        private void btnTomaImporte_Click(object sender, EventArgs e)
        {
            var summaryValue = gridViewDetalle.Columns["APagar"].SummaryItem.SummaryValue;
            txtImporte.Text = summaryValue.ToString();
        }

        private void AgregaAñosNavBar()
        {
            try
            {
                globalCL cl = new globalCL();
                System.Data.DataTable dt = new System.Data.DataTable("Años");
                dt.Columns.Add("Año", Type.GetType("System.Int32"));
                cl.strTabla = "Depositos";
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

        private void txtFactorajeNombre_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnFactorajeCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtFactorajeInteres.Text))
                {
                    MessageBox.Show("Teclee el interes");
                    return;
                }
                if (!clg.esNumerico(txtImporte.Text))
                {
                    MessageBox.Show("Teclee el importe");
                    return;
                }

                decimal curTotal;
                curTotal = Convert.ToDecimal(txtImporte.Text);
                    //+ Convert.ToDecimal(txtFactorajeInteres.Text);
                txtFactorajePtjePago.Text = Math.Round((Convert.ToDecimal(txtImporte.Text) / curTotal),8).ToString();
                txtFactorajePtjeInteres.Text = Math.Round((Convert.ToDecimal(txtFactorajeInteres.Text) / curTotal)*100,8).ToString();

                RecalculaInteresRenglones();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                decimal supago = 0;
                decimal interes = 0;
                string monedaFac = string.Empty;
                decimal suPagoConvertido = 0;
                globalCL clg = new globalCL();

                if (e.Column.Name== "gridColumnSupago")
                {
                    supago = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "APagar"));
                    if (Convert.ToDecimal(txtFactorajePtjeInteres.Text) > 0)
                    {
                        
                        interes = Math.Round(supago * (Convert.ToDecimal(txtFactorajePtjeInteres.Text) / 100), 2);
                        gridViewDetalle.SetFocusedRowCellValue("Interes", interes);
                    }

                    if (monedaFac != strMonedaDelPago)
                    {
                        if (strMonedaDelPago == "MXN")
                        {
                            txtTipodecambio.Text = "1";
                            suPagoConvertido = Math.Round(supago * Convert.ToDecimal(txtTipodecambio.Text), 2);
                        }
                            
                        else
                        {
                            if (!clg.esNumerico(txtTipodecambio.Text))
                            {
                                MessageBox.Show("Teclee el tipo de cambio");
                            }
                            else                                
                                suPagoConvertido = Math.Round(supago / Convert.ToDecimal(txtTipodecambio.Text), 2);
                        }
                            
                    }
                    else
                        suPagoConvertido = supago;

                    gridViewDetalle.SetFocusedRowCellValue("Supagoconvertido", suPagoConvertido);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("CellValueChanged:" + ex.Message);
            }
        }

        private void RecalculaInteresRenglones()
        {
            try
            {
                foreach (int i in gridViewDetalle.GetSelectedRows())
                {
                    decimal supago = 0;
                    decimal interes = 0;
                    if (Convert.ToDecimal(txtFactorajePtjeInteres.Text) > 0)
                    {
                        supago = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "APagar"));
                        interes = Math.Round(supago * (Convert.ToDecimal(txtFactorajePtjeInteres.Text) / 100), 2);

                        gridViewDetalle.FocusedRowHandle = i;
                        gridViewDetalle.SetFocusedRowCellValue("Interes", interes);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RecalculaInteresRenglones: " + ex.Message);
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
            txtTeskSerie.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CierraPopUpTesk();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                DepositosCL cl = new DepositosCL();
                globalCL clg = new globalCL();


                if (!clg.esNumerico(txtTeskFac.Text))
                {
                    MessageBox.Show("La factura debe ser númerica");
                    return;
                }
                if (!clg.esNumerico(txtTeskSaldo.Text))
                {
                    MessageBox.Show("El saldo debe ser númerico");
                    return;
                }

                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.strSerieFac = txtTeskSerie.Text;
                cl.intFac = Convert.ToInt32(txtTeskFac.Text);
                cl.dImporte = Convert.ToDecimal(txtTeskSaldo.Text);

                string result = cl.SaldoTesk();

                MessageBox.Show(result);

                if (result == "OK")
                    CierraPopUpTesk();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEnabledCte_Click(object sender, EventArgs e)
        {
            cboClientesID.Enabled = true;
            gridControlDetalle.DataSource = null;
        }

        private void dateEditFecha_Leave(object sender, EventArgs e)
        {
            try
            {
              
                if (Convert.ToDateTime(dateEditFecha.Text).ToShortDateString() != DateTime.Now.ToShortDateString())
                {
                    dateEditFecha.Enabled = false;
                }
            }
            catch (Exception)
            {

            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            dateEditFecha.Enabled = true;
        }

        private void btnBuscaNuevoUUID_Click(object sender, EventArgs e)
        {
            globalCL clg = new globalCL();
            txtUUIDNuevo22.Text = clg.leeUUIDNuevo("P", txtSerieNueva22.Text, txtFolioNuevo22.Text,intCliente);
        }

        private void labelControl27_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            globalCL clg = new globalCL();
            txtUUIDRelacion.Text = clg.leeUUIDNuevo(txtSerieRelacion.Text, txtSerieRelacion.Text, txtFolioRelacion.Text,intCliente);
        }

        private void cboC_Formapago_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;
            DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);
            string value = row["Clave"].ToString();
            if (value == "01")
                swDepositoRecepcion.Visible = true;
            else
            {
                swDepositoRecepcion.Visible = false;
                swDepositoRecepcion.IsOn = false;
            }
        }
    }
}
