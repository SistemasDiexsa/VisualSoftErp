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
using VisualSoftErp.Operacion.CxP.Clases;
using VisualSoftErp.Clases.HerrramientasCLs;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraNavBar;

namespace VisualSoftErp.Operacion.CxP.Formas
{
    
    public partial class ContraRecibos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public BindingList<detalleCL> detalle;
        string strTo;
        int intFolio;
        string strSerie;
        string strMonedaDelPago;
        int intProveedor;
        public bool blNuevo;
        int intCuentasbancariasID;
        int intUsuarioID = 1; //NO DEJAR FIJO 
        int intUsuariocancelo = 0;
        DateTime dFecha;
        string sMoneda;
        decimal ivaproveedor;
        decimal retivaproveedor;
        decimal retisrproveedor;
        int plazoproveedor;
        string monedaproveedor;
        int AñoFiltro;
        int MesFiltro;

        public ContraRecibos()
        {
            InitializeComponent();

            txtFecha.Text = DateTime.Now.ToShortDateString();

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            //con las siguientes 3 lineas podemos poner redyonly a la columna que desemos del gridviwedetalle
            gridColumnIva.OptionsColumn.ReadOnly = true;
            gridColumnIva.OptionsColumn.AllowEdit = false;
            gridColumnIva.OptionsColumn.AllowFocus = false;

            gridColumnRetIva.OptionsColumn.ReadOnly = true;
            gridColumnRetIva.OptionsColumn.AllowEdit = false;
            gridColumnRetIva.OptionsColumn.AllowEdit = false;

            gridColumnNeto.OptionsColumn.ReadOnly = true;
            gridColumnNeto.OptionsColumn.AllowEdit = false;
            gridColumnNeto.OptionsColumn.AllowFocus = false;

            gridColumnRetIsr.OptionsColumn.ReadOnly = true;
            gridColumnRetIsr.OptionsColumn.AllowEdit = false;
            gridColumnRetIsr.OptionsColumn.AllowFocus = false;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Contra recibos";

            //Preguntar a javier si lo puedo poner en otro lado, aqui los oculta cuando damos click en pagos 'hacer invisibles desde el diseño mejor'
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;
            LlenarGrid(AñoFiltro,MesFiltro);
            gridViewPrincipal.ActiveFilter.Clear();

            AgregaAñosNavBar();
           
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
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


        private void LlenarGrid(int año,int mes)
        {
            ContraRecibosCL cl = new ContraRecibosCL();
            cl.intAño = año;
            cl.intMes = mes;
            gridControlPrincipal.DataSource = cl.ContrarecibosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridContrarecibos";            
            clg.restoreLayout(gridViewPrincipal);
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
            gridViewPrincipal.ViewCaption = "CONTRARECIBOS DE " + clg.NombreDeMes(MesFiltro, 0) + " DEL " + AñoFiltro.ToString();
        } //LlenarGrid()

        public class detalleCL
        {
            //deben de llamarse como esta en la tabla
            public string SerieFactura { get; set; }
            public string Factura { get; set; }
            public DateTime Fecha { get; set; }            
            public decimal Importegravado { get; set; }
            public decimal Importeexcento { get; set; }
            public decimal Iva { get; set; }
            public decimal RetIva { get; set; }
            public decimal RetIsr { get; set; }
            public decimal Ipes { get; set; }
            public decimal Neto { get; set; } 
           
        }

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridContrarecibosDetalle";
            clg.restoreLayout(gridViewDetalle);
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Serie";
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
            cboProveedoresID.Properties.Columns["Diasdetraslado"].Visible = false;
            cboProveedoresID.Properties.Columns["Lab"].Visible = false;
            cboProveedoresID.Properties.Columns["Via"].Visible = false;
            cboProveedoresID.Properties.Columns["BancosID"].Visible = false;
            cboProveedoresID.Properties.Columns["Cuentabancaria"].Visible = false;
            cboProveedoresID.Properties.Columns["C_Formapago"].Visible = false;
            cboProveedoresID.Properties.Columns["Retisr"].Visible = false;
            cboProveedoresID.Properties.Columns["Retiva"].Visible = false;
            cboProveedoresID.Properties.Columns["MonedasID"].Visible = false;
            cboProveedoresID.Properties.Columns["Diasdesurtido"].Visible = false;
            cboProveedoresID.Properties.Columns["Diastraslado"].Visible = false;
            cboProveedoresID.Properties.Columns["Contacto"].Visible = false;
            cboProveedoresID.Properties.NullText = "Seleccione un proveedor";

            cl.strTabla = "Monedas";
            cboMonedas.Properties.ValueMember = "Clave";
            cboMonedas.Properties.DisplayMember = "Des";
            cboMonedas.Properties.DataSource = cl.CargaCombos();
            cboMonedas.Properties.ForceInitialize();
            cboMonedas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedas.Properties.PopulateColumns();
            cboMonedas.Properties.Columns["Clave"].Visible = false;            
            cboMonedas.ItemIndex = 0;

        }

        private void Nuevo()
        {
            blNuevo = true;
            Inicialisalista();
            LimpiaCajas();
            BotonesEdicion();           
            cboProveedoresID.ReadOnly = false;
            SiguienteID();
            lblPoliza.Visible = false;
            bbiPolizaTesk.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            //Oculta los litros en el segundo frame
            navBarControl.Visible = false;
            ribbonStatusBar.Visible = false;            

            navigationFrame.SelectedPageIndex = 1;
        }

        private void LimpiaCajas()
        {
            Inicialisalista();

            cboSerie.ItemIndex = 0;
            txtFolio.Text = string.Empty;
            txtFecha.Text = DateTime.Now.ToShortDateString();           
            cboProveedoresID.EditValue = null;
            txtDes.Text = "";
            txtPlazo.Text = string.Empty;
            

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

            ContraRecibosCL cl = new ContraRecibosCL();
            cl.strSerie = serie;
            cl.strDoc = "ContraRecibos";

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
                //DT para Contrarecibos
                System.Data.DataTable dtContrarecibos = new System.Data.DataTable("Contrarecibos");
                dtContrarecibos.Columns.Add("Serie", Type.GetType("System.String"));
                dtContrarecibos.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtContrarecibos.Columns.Add("ProveedoresID", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("Plazo", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("MonedasID", Type.GetType("System.String"));
                dtContrarecibos.Columns.Add("Tipodecambio", Type.GetType("System.Decimal"));
                dtContrarecibos.Columns.Add("Status", Type.GetType("System.String"));
                dtContrarecibos.Columns.Add("Fechacancelacion", Type.GetType("System.DateTime"));
                dtContrarecibos.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("UsuarioCanceló", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("Fechareal", Type.GetType("System.DateTime"));
                dtContrarecibos.Columns.Add("Poliza", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("RazonCancelacion", Type.GetType("System.String"));
                dtContrarecibos.Columns.Add("Descripcion", Type.GetType("System.String"));
                dtContrarecibos.Columns.Add("Tesk", Type.GetType("System.Int32"));
                dtContrarecibos.Columns.Add("TeskPoliza", Type.GetType("System.String"));

                //DT para ContrarecibosDetalle
                System.Data.DataTable dtContrarecibosdetalle = new System.Data.DataTable("Pagoscxpdetalle");
                dtContrarecibosdetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtContrarecibosdetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtContrarecibosdetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtContrarecibosdetalle.Columns.Add("SerieFactura", Type.GetType("System.String"));
                dtContrarecibosdetalle.Columns.Add("Factura", Type.GetType("System.String"));
                dtContrarecibosdetalle.Columns.Add("Fecha", Type.GetType("System.DateTime"));               
                dtContrarecibosdetalle.Columns.Add("Importegravado", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("Importeexcento", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("PIva", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("PRetIva", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("PRetIsr", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("PIeps", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("RetIva", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("RetIsr", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dtContrarecibosdetalle.Columns.Add("UUID", Type.GetType("System.String"));
                dtContrarecibosdetalle.Columns.Add("Tesk", Type.GetType("System.Int32"));
                dtContrarecibosdetalle.Columns.Add("TeskPoliza", Type.GetType("System.String"));
                dtContrarecibosdetalle.Columns.Add("ArchivoXml", Type.GetType("System.String"));

                SiguienteID();
                if (Result != "OK")
                {
                    MessageBox.Show("No se pudo leer el siguiente folio");
                    return;
                }
                //iFolio = clg.intSigFolio;
                int iFolio = Convert.ToInt32(txtFolio.Text);
                int intRen = 0;
                string dato = String.Empty;
                string strSerie = cboSerie.EditValue.ToString();
                string strSerieFactura = string.Empty;
                string strFactura = String.Empty;                
                int intSeq = 0;                
                decimal dTipocambio = Convert.ToDecimal(txtTipodecambio.Text);               
                DateTime fdfecha = Convert.ToDateTime(null);
                decimal dImporteG = 0;
                decimal dImporteE = 0;
                decimal dPIva = ivaproveedor;
                decimal dPRetIva = retivaproveedor;
                decimal dPRetIsr = retisrproveedor;
                decimal dPIpes = 0; //se le dara un trato diferente
                decimal dIva = 0;
                decimal dRetIva = 0;
                decimal dRetIsr = 0;
                decimal dIeps = 0; //se ke dara un trato diferente 
                decimal dNeto = 0;
                string strUUID = string.Empty;
                bool error = false;

                //Recorrer el griddetalle para extraer los datos y pasarlos al dt (con el gridViewDetalle.GetSelectedRows solo tomara el renglon o renglones seleccionados)                
                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "Factura").ToString();
                    if (dato.Length > 0)
                    {
                        if (gridViewDetalle.GetRowCellValue(i, "SerieFactura") == null)
                            strSerieFactura = string.Empty;
                        else
                        strSerieFactura = gridViewDetalle.GetRowCellValue(i, "SerieFactura").ToString();
                        strFactura = gridViewDetalle.GetRowCellValue(i, "Factura").ToString();
                        fdfecha = Convert.ToDateTime(gridViewDetalle.GetRowCellValue(i, "Fecha"));
                        dImporteG = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importegravado"));
                        dImporteE = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importeexcento"));
                        dIva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Iva"));
                        dRetIva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "RetIva"));
                        dRetIsr = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "RetIsr"));
                        dIeps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Ieps"));
                        dNeto = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Neto"));                       
                        

                        if (dNeto > 0)
                        {
                            dtContrarecibosdetalle.Rows.Add(
                            strSerie,
                            iFolio,
                            intSeq,
                            strSerieFactura,  
                            strFactura,
                            fdfecha,
                            dImporteG,
                            dImporteE,
                            dPIva,
                            dPRetIva,
                            dPRetIsr,
                            dPIpes,
                            dIva,
                            dRetIva,
                            dRetIsr,
                            dIeps,
                            dNeto,
                            strUUID,
                            0,
                            "",
                            ""
                            );

                            intSeq = intSeq + 1;
                        }
                        else
                        {
                            error = true;
                            MessageBox.Show("El renglón de la factura #" + dato + " no ha podido agregarse debido a que el neto es igual a cero.", "Error al consultar información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                if ( error ) { return; }

                
                DateTime fFecha = Convert.ToDateTime(txtFecha.Text);                
                int intProveedoresID = Convert.ToInt32(cboProveedoresID.EditValue);
                string strPlazo = txtPlazo.Text;
                decimal dTipodecambio = Convert.ToDecimal(txtTipodecambio.Text);
                string strStatus = "1";
                DateTime fFechaReal = Convert.ToDateTime(DateTime.Now);
                DateTime fFechaCancelacion = Convert.ToDateTime(DateTime.Now); //cambiar cuando haga lo de cancelar
                int intUsuarioCancelo = 0;
                int intPoliza = 0;
                string strRazoncancelacion = txtRazoncancelacion.Text;
                dato = String.Empty;

                dtContrarecibos.Rows.Add(
                    strSerie,
                    iFolio,
                    fFecha,                   
                    intProveedoresID,
                    strPlazo,                    
                    monedaproveedor,
                    dTipodecambio,
                    strStatus,
                    fFechaCancelacion,
                    intUsuarioID,
                    intUsuarioCancelo,
                    fFechaReal,
                    intPoliza,
                    strRazoncancelacion,
                    txtDes.Text,
                    0,
                    "");

                ContraRecibosCL cl = new ContraRecibosCL();
                cl.strSerie = strSerie;
                cl.intFolio = iFolio;
                cl.dtm = dtContrarecibos;
                cl.dtd = dtContrarecibosdetalle;
                cl.intUsuarioID = 1;
                cl.strMaquina = Environment.MachineName;
                cl.strPrograma = "0221";
                string result = cl.ContrarecibosCrud();
                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");

                    if (blNuevo)
                    {
                        LimpiaCajas();
                        SiguienteID();
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
            if (txtFecha.Text.Length == 0)
            {
                return "El campo fecha no puede ir vacio";
            }
            if (cboProveedoresID.EditValue == null)
            {
                return "El campo ProveedoresID no puede ir vacio";
            }
            if (cboMonedas.EditValue == null)
            {
                return "El campo monedas no puede ir vacio";
            }
            if (txtTipodecambio.Text.Length == 0)
            {
                if (cboMonedas.EditValue.ToString() == "MXN")
                    txtTipodecambio.Text = "1";
                else
                    return "El campo Tipodecambio no puede ir vacio";
            }           
            if (txtPlazo.Text.Length == 0)
            {
                return "El campo plazo no puede ir vacio";
            }
            if (monedaproveedor != "MXN")
            {
                globalCL clG = new globalCL();

                if (!clG.esNumerico(txtTipodecambio.Text))
                {
                    return "Teclee el tipo de cambio";
                }
                if (Convert.ToDecimal(txtTipodecambio.Text) <= 1)
                {
                    return "Teclee el tipo de cambio";
                }
            }

            if (gridViewDetalle.RowCount - 1 == 0)
            {
                return "Debe capturar al menos un renglón";
            }

            for (int i = 0; i < gridViewDetalle.RowCount - 1 ; i++)
            {
                string factura = gridViewDetalle.GetRowCellValue(i, "Factura").ToString();

                if (factura == null || factura.Length == 0 || factura == string.Empty)
                {
                    return "Teclee la Factura";
                }
            }


            globalCL clg = new globalCL();
            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month, "CXP");
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";
                
            }


            return "OK";

        } //Valida

        private void llenaCajas()
        {
            ContraRecibosCL cl = new ContraRecibosCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;

            if (intFolio == 0)
            {
                MessageBox.Show("Seleccione un renglón");
                return;
            }

            gridControlDetalle.DataSource =  cl.Contrarecibosllenacajas();
            if (gridControlDetalle !=null)
            {
                cboSerie.EditValue = strSerie;
                txtFolio.Text = intFolio.ToString();
                txtFecha.Text = gridViewDetalle.GetRowCellValue(0, "FechaCR").ToString();
                cboProveedoresID.EditValue = Convert.ToInt32(gridViewDetalle.GetRowCellValue(0, "ProveedoresID"));
                txtPlazo.Text = gridViewDetalle.GetRowCellValue(0, "Plazo").ToString();
                txtTipodecambio.Text = gridViewDetalle.GetRowCellValue(0, "Tipodecambio").ToString();
                cboMonedas.EditValue = gridViewDetalle.GetRowCellValue(0, "MonedasID").ToString();
                txtDes.Text = gridViewDetalle.GetRowCellValue(0, "Descripcion").ToString();
                lblPoliza.Text = "POLIZA: " + gridViewDetalle.GetRowCellValue(0, "TeskPoliza").ToString();
            }
            else
            {
                MessageBox.Show("No se pudo leer");
            }
        } // llenaCajas

        private void BotonesEdicion()
        {
            if (blNuevo)
            {
                LimpiaCajas();
                bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                navBarControl.Visible = false;
            }
           
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiMovs.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            navigationFrame.SelectedPageIndex = 1;
            
        }       

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Configurando detalle", "espere por favor...");
            Nuevo();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnAut.Text = "Cancelar";
            labelControl1.Text = "Razón";
            popUpCancelar.Visible = true;
            groupControl3.Text = "Cancelar el folio:" + strSerie + intFolio.ToString();
            txtLogin.Focus();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Newsa.net","Cargando información...");

            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;            
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiMovs.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiPolizaTesk.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LlenarGrid(AñoFiltro, MesFiltro);

            intFolio = 0;
            strSerie = null;
            navigationFrame.SelectedPageIndex = 0;

            navBarControl.Visible = true;
            navBarControl.Visible = true;
            ribbonStatusBar.Visible = true;

            globalCL clg = new globalCL();            
            clg.strGridLayout = "gridContrarecibosDetalle";
            string Result = clg.SaveGridLayout(gridViewDetalle);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridContrarecibos";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            
            this.Close();
        }

        //con esto llenamos plazo y monedas dependiento
        private void cboProveedoresID_EditValueChanged(object sender, EventArgs e)
        {
            object orow = cboProveedoresID.Properties.GetDataSourceRowByKeyValue(cboProveedoresID.EditValue);
            if (orow != null)
            {
                plazoproveedor = Convert.ToInt32(((DataRowView)orow)["Plazo"]);
                monedaproveedor = ((DataRowView)orow)["MonedasID"].ToString();
                ivaproveedor = Convert.ToDecimal(((DataRowView)orow)["Piva"]);
                retivaproveedor = Convert.ToDecimal(((DataRowView)orow)["Retiva"]);
                retisrproveedor = Convert.ToDecimal(((DataRowView)orow)["Retisr"]);

                //if (plazoproveedor > 0)
                //{
                    cboMonedas.EditValue = monedaproveedor;
                //}

                txtPlazo.Text = plazoproveedor.ToString();


                txtTipodecambio.Text = "";
                txtTipodecambio.ReadOnly = false;

                if (monedaproveedor == "MXN")
                {
                    txtTipodecambio.Text = "1";
                    txtTipodecambio.ReadOnly = true;
                }

                gridColumnIeps.Visible = false;  //Para diexsa no usan IEPS

                //con las siguientes podemos oculatar las columnas cuando se cumpla la condicion
                if (ivaproveedor == 0)
                {
                    gridColumnIva.Visible = false;
                }
                else
                {
                    gridColumnIva.Visible = true;
                }

                if (retivaproveedor == 0)
                {
                    gridColumnRetIva.Visible = false;
                }
                else
                {
                    gridColumnRetIva.Visible = true;
                }

                if (retisrproveedor == 0)
                {
                    gridColumnRetIsr.Visible = false;
                }
                else
                {
                    gridColumnRetIsr.Visible = true;
                }

            }
        }

        // desde este evento del gridviewdetalle realizaremos el calculo de los impuestos
        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.Name == "gridColumnImportegravado")
                {
                    decimal importegravado = 0;
                    decimal iva = 0;                    
                    decimal neto = 0;
                    decimal importeexcento = 0;
                    decimal retiva = 0;
                    decimal retisr = 0;

                    importegravado = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Importegravado"));
                    iva = Math.Round(importegravado * (ivaproveedor / 100), 2);
                    retiva = Math.Round(importegravado * (retivaproveedor / 100), 2);
                    retisr = Math.Round(importegravado * (retisrproveedor / 100), 2);

                    importeexcento = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Importeexcento"));


                    neto = importegravado + importeexcento + iva - retiva -retisr;

                    //con esto pasamos valor al griddetalle
                    gridViewDetalle.SetFocusedRowCellValue("Iva", iva);
                    gridViewDetalle.SetFocusedRowCellValue("RetIva", retiva);
                    gridViewDetalle.SetFocusedRowCellValue("RetIsr", retisr);
                    gridViewDetalle.SetFocusedRowCellValue("Neto", neto);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("GridviewDetalle Changed: " + ex);
            }
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

                ContraRecibosCL cl = new ContraRecibosCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intUsuariocancelo = intUsuariocancelo;
                cl.strRazoncancelacion = txtRazoncancelacion.Text;
                result = cl.ContrarecibosCancelar();
                if (result == "OK")
                {
                    MessageBox.Show("Cancelado correctamente");
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Newsa.net", "Cargando información...");
                    LlenarGrid(AñoFiltro, MesFiltro);
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

                }
                else
                    MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cancelar:" + ex.Message);
            }
        }

        private void CierraPopUp()
        {
            popUpCancelar.Visible = false;
            txtLogin.Text = string.Empty;
            txtPassword.Text = string.Empty;           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            CierraPopUp();
        }

        private void CapturaPolizaTesk()
        {
            try
            {
                if (txtRazoncancelacion.Text.Length == 0)
                {
                    MessageBox.Show("Capture la póliza");
                    return;
                }

                ContraRecibosCL cl = new ContraRecibosCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.StrPoliza = txtRazoncancelacion.Text;
                string result = cl.ContrarecibosCapturaPoliza();
                MessageBox.Show(result);
                CierraPopUp();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAut_Click(object sender, EventArgs e)
        {

            if (groupControl3.Text.Substring(0,8) == "Capturar")
            {
                CapturaPolizaTesk();
                return;
            }

            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLogin.Text;
            clU.strClave = txtPassword.Text;
            clU.strPermiso = "Cancelarcontrarecibos";            
            string result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }
            intUsuariocancelo = clU.intUsuariosID;
            CierraPopUp();

            Cancelar();
            
        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            strSerie = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie").ToString();
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            dFecha = Convert.ToDateTime(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Fecha"));            
            string status = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status").ToString();
            

            if (status == "Registrado")
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
            GridView view = sender as GridView;
            string _mark = (string)view.GetRowCellValue(e.RowHandle, "Status");

            if (_mark == "Cancelado")
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Red;
            }
        }
        
        // son los filtros laterales de los meses 
        private void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            //navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }

        #region Filtros Meses
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

        private void Editar()
        {
            llenaCajas();          
            BotonesEdicion();
            blNuevo = false;
            lblPoliza.Visible = true;
            navigationFrame.SelectedPageIndex = 1;
        }//ver

        
        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            Editar();
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
                    case "navBarItemTodo":
                        MesFiltro = 0;
                        break;
                }

                LlenarGrid(AñoFiltro, MesFiltro);
            }
        }

        private void bbiMovs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Movimientos();
        }

        private void Movimientos()
        {
            try
            {
                ContraRecibosCL cl = new ContraRecibosCL();
                cl.intFolio = intFolio;
                gridControlMovs.DataSource = cl.ContrarecibosMovimientos();
                navigationFrame.SelectedPageIndex = 2;

                bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiMovs.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiPolizaTesk_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (intFolio == 0)
                {
                    MessageBox.Show("Seleccione un folio");
                    return;
                }

                popUpCancelar.Visible = true;
                groupControl3.Text = "Capturar póliza a el folio:" + strSerie + intFolio.ToString();
                btnAut.Text = "Proceder";
                labelControl1.Text = "Póliza";
                txtLogin.Focus();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}