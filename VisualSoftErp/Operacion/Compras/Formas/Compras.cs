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
using VisualSoftErp.Operacion.Compras.Designers;
using VisualSoftErp.Clases.HerrramientasCLs;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Xml;
using System.IO;
using DevExpress.XtraNavBar;

namespace VisualSoftErp.Catalogos
{

    public partial class Compras : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        public BindingList<detalleCL> detalle;
        string strSerie;
        int intFolio;
        decimal dIvaPorcentaje;
        bool blnNuevo;
        int intManejaIva;
        int intManejaIeps;
        decimal pIvaProv;  //Iva del proveedor manda sobre iva del artículo
        int intRM;
        string strRfcEmpresa;
        int intUsuariocancelo;
        string DatosFactura = string.Empty;
        int AñoFiltro;
        int MesFiltro;
        int Ordendecompra;

        public Compras()
        {
            InitializeComponent();

            dateFecha.Text = DateTime.Now.ToShortDateString();
            txtOCfolio.Properties.MaxLength = 50;
            txtOCfolio.EnterMoveNextControl = true;
           

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            gridViewOC.OptionsBehavior.ReadOnly = true;
            gridViewOC.OptionsBehavior.Editable = false;


            //------------- Inicializar aquí opciones de columnas del grid ----------------          
            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Registro de Compras";


            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;
            LlenarGrid(AñoFiltro,MesFiltro);
            gridViewPrincipal.ActiveFilter.Clear();

            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            MedioAmbiente();

            AgregaAñosNavBar();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void LlenarGrid(int Año,int Mes)
        {
            ComprasCL cl = new ComprasCL();
            cl.intAño = Año;
            cl.intMes = Mes;
            gridControlPrincipal.DataSource = cl.ComprasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCompras";
            clg.restoreLayout(gridViewPrincipal);

            //con esta lina de codigo ponemos los autofiltros
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            gridViewPrincipal.ViewCaption = "COMPRAS DE " + clg.NombreDeMes(MesFiltro,0) + " DEL " + AñoFiltro.ToString();

        } //LlenarGrid()

        public class detalleCL
        {
            public decimal Cantidad { get; set; }
            public string Articulo { get; set; }
            public decimal Precio { get; set; }
            public decimal Importe { get; set; }
            public decimal Iva { get; set; }
            public decimal Neto { get; set; }
            public decimal Ieps { get; set; }
            public decimal Costoum2 { get; set; }
            public decimal Factorum2 { get; set; }
            public decimal Piva { get; set; }
            public decimal Pieps { get; set; }
            public decimal Descuento { get; set; }
            public decimal Pdescuento { get; set; }
            public string OCSerie { get; set; }
            public string OCNumero { get; set; }
            public string OCSeq { get; set; }
            public string Descripcion { get; set; }
            public string NoIdentificacion { get; set; }
        }

        private void MedioAmbiente()
        {
            DatosdecontrolCL cl = new DatosdecontrolCL();
            cl.DatosdecontrolLeer();

            //if (cl.iManejariva == 0)
            //{
            //    gridColumnPiva.Visible = false;
            //    intManejaIva = 0;
            //}
            //else
            //{
            //    intManejaIva = 1;
            //}
            //if (cl.iManejarieps == 0)
            //{
            //    gridColumnPieps.Visible = false;
            //    intManejaIeps = 0;
            //}
            //else
            //{
            //    intManejaIeps = 1;
            //}

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

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
            globalCL clg = new globalCL();
                                 
            clg.strGridLayout = "gridComprasDetalle";
            clg.restoreLayout(gridViewDetalle);
            if (intManejaIeps == 0)
            {
                gridColumnIEPS.Visible = false;
                gridColumnPieps.Visible = false;
            }
            if (intManejaIva == 0)
            {
                gridColumnPiva.Visible = false;
                gridColumnIVA.Visible = false;
            }

            gridColumnImporte.OptionsColumn.ReadOnly = true;
            gridColumnImporte.OptionsColumn.AllowEdit = false;
            gridColumnImporte.OptionsColumn.AllowFocus = false;
            gridViewDetalle.OptionsView.RowAutoHeight = true;



        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Serie";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Des";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Clave"].Visible = false;
            cboSerie.ItemIndex = 0;

            cl.strTabla = "Proveedores";
            cboProveedores.Properties.ValueMember = "Clave";
            cboProveedores.Properties.DisplayMember = "Des";
            cboProveedores.Properties.DataSource = cl.CargaCombos();
            cboProveedores.Properties.ForceInitialize();
            cboProveedores.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedores.Properties.PopulateColumns();
            cboProveedores.Properties.Columns["Clave"].Visible = false;
            cboProveedores.Properties.Columns["Piva"].Visible = false;
            cboProveedores.Properties.Columns["Plazo"].Visible = false;
            cboProveedores.Properties.Columns["Tiempodeentrega"].Visible = false;
            cboProveedores.Properties.Columns["Diastraslado"].Visible = false;
            cboProveedores.Properties.Columns["Lab"].Visible = false;
            cboProveedores.Properties.Columns["Via"].Visible = false;
            cboProveedores.Properties.Columns["BancosID"].Visible = false;
            cboProveedores.Properties.Columns["Cuentabancaria"].Visible = false;
            cboProveedores.Properties.Columns["C_Formapago"].Visible = false;
            cboProveedores.Properties.Columns["Retisr"].Visible = false;
            cboProveedores.Properties.Columns["Retiva"].Visible = false;
            cboProveedores.Properties.Columns["MonedasID"].Visible = false;
            cboProveedores.Properties.Columns["Contacto"].Visible = false;
            cboProveedores.ItemIndex = 0;


            cl.strTabla = "Monedas";
            cboMoneda.Properties.ValueMember = "Clave";
            cboMoneda.Properties.DisplayMember = "Des";
            cboMoneda.Properties.DataSource = cl.CargaCombos();
            cboMoneda.Properties.ForceInitialize();
            cboMoneda.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMoneda.Properties.PopulateColumns();
            cboMoneda.Properties.Columns["Clave"].Visible = false;

            cl.strTabla = "Monedas";
            cboMonedasIDIncremental.Properties.ValueMember = "Clave";
            cboMonedasIDIncremental.Properties.DisplayMember = "Des";
            cboMonedasIDIncremental.Properties.DataSource = cl.CargaCombos();
            cboMonedasIDIncremental.Properties.ForceInitialize();
            cboMonedasIDIncremental.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedasIDIncremental.Properties.PopulateColumns();
            cboMonedasIDIncremental.Properties.Columns["Clave"].Visible = false;

            cl.strTabla = "Almacenes";
            cboAlmacen.Properties.ValueMember = "Clave";
            cboAlmacen.Properties.DisplayMember = "Des";
            cboAlmacen.Properties.DataSource = cl.CargaCombos();
            cboAlmacen.Properties.ForceInitialize();
            cboAlmacen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacen.Properties.PopulateColumns();
            cboAlmacen.Properties.Columns["Clave"].Visible = false;


            cl.strTabla = "NoKits";
            repositoryItemLookUpEditArticulo.ValueMember = "Clave";
            repositoryItemLookUpEditArticulo.DisplayMember = "Des";
            repositoryItemLookUpEditArticulo.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulo.ForceInitialize();
            repositoryItemLookUpEditArticulo.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }

        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            intRM = 0;
            blnNuevo = true;
            Inicialisalista();
            BotonesEdicion();
            strSerie = string.Empty;
            SiguienteID();
            cboProveedores.Focus();
            DatosFactura = "N";
            lblRM.Text = "";
            lblPagada.Text = "";
        }

        private void SiguienteID()
        {
            string serie = cboSerie.EditValue.ToString();

            ComprasCL cl = new ComprasCL();
            cl.strSerie = serie;
            cl.strDoc = "Compras";

            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {

                cboSerie.EditValue = serie;
                txtFolio.Text = cl.intFolio.ToString();
                intFolio = cl.intFolio;
            }
            else
            {
                MessageBox.Show("SiguienteID :" + result);
            }

        }//SguienteID

        private void LimpiaCajas()
        {
            cboSerie.ItemIndex = 0;
            dateFecha.Text = DateTime.Now.ToShortDateString();
            txtOCfolio.Text = string.Empty;

            cboMoneda.ItemIndex = 0;
            cboAlmacen.ItemIndex = 0;
            txtFactura.Text = string.Empty;
            dateFechafactura.Text = DateTime.Now.ToShortDateString();
            if (cboMoneda.EditValue.ToString() == "MXN")
            {
                txtTipocambio.Text = "1";
                txtTipocambio.Enabled = false;
            }
            else
            {
                txtTipocambio.Text = "";
                txtTipocambio.Enabled = true;
            }
            txtPlazo.Text = "0";
            txtOcSerie.Text = string.Empty;
            cboProveedores.Enabled = true;

        }

        private void bbiGuardar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DatosFactura == "S")
                GuardaDatosFactura();
            else
                Guardar();
        }

        private void GuardaDatosFactura()
        {
            try
            {
                //validar
                globalCL clg = new globalCL();
                if (txtFac2.Text.Length==0)
                {
                    MessageBox.Show("Teclee la factura");
                    return;
                }
                if (dFechaFac2.Text.Length==0)
                {
                    MessageBox.Show("Teclee la fecha de la factura");
                    return;
                } 
                if (txtMoneda2.Text!="MXN")
                {
                    if (!clg.esNumerico(txtTC2.Text))
                    {
                        MessageBox.Show("Teclee el tipo de cambio");
                        return;
                    }
                }
                if (!clg.esNumerico(txtPlazo2.Text))
                {
                    txtPlazo2.Text = "0";
                }


                string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(dateFecha.Text).Year, Convert.ToDateTime(dateFecha.Text).Month, "COM");
                if (result=="C")
                {
                    MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                    return;
                }


                ComprasCL cl = new ComprasCL();
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = intFolio;
                cl.strFactura = txtFac2.Text;
                cl.fFecha = Convert.ToDateTime(dFechaFac2.Text);
                cl.dTC = Convert.ToDecimal(txtTC2.Text);
                cl.intPlazo = Convert.ToInt32(txtPlazo2.Text);

                result = cl.GuardaDatosFactura();
                MessageBox.Show(result);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                System.Data.DataTable dtCompras = new System.Data.DataTable("Compras");


                dtCompras.Columns.Add("Serie", Type.GetType("System.String"));
                dtCompras.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtCompras.Columns.Add("OCSerie", Type.GetType("System.String"));
                dtCompras.Columns.Add("OCFolio", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("ProveedoresID", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("MonedasID", Type.GetType("System.String"));
                dtCompras.Columns.Add("Factura", Type.GetType("System.String"));
                dtCompras.Columns.Add("fechafactura", Type.GetType("System.DateTime"));
                dtCompras.Columns.Add("tipodecambio", Type.GetType("System.Decimal"));
                dtCompras.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dtCompras.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtCompras.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtCompras.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dtCompras.Columns.Add("Status", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("Plazo", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("Fechacancelacion", Type.GetType("System.DateTime"));
                dtCompras.Columns.Add("Razoncancelacion", Type.GetType("System.String"));
                dtCompras.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dtCompras.Columns.Add("Poliza", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("Nodeducible", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("ContrarecibosSerie", Type.GetType("System.String"));
                dtCompras.Columns.Add("ContrarecibosFolio", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("RecepcionSerie", Type.GetType("System.String"));
                dtCompras.Columns.Add("RecepcionFolio", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("ValidadoPor", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("FechaValidado", Type.GetType("System.DateTime"));
                dtCompras.Columns.Add("FechaReal", Type.GetType("System.DateTime"));
                dtCompras.Columns.Add("Observaciones", Type.GetType("System.String"));
                dtCompras.Columns.Add("AlmacenesID", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("UsuarioCancelo", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("UUID", Type.GetType("System.String"));
                dtCompras.Columns.Add("Costear", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("Tesk", Type.GetType("System.Int32"));
                dtCompras.Columns.Add("TeskPoliza", Type.GetType("System.String"));

                System.Data.DataTable dtComprasdetalle = new System.Data.DataTable("Comprasdetalle");
                dtComprasdetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtComprasdetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtComprasdetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtComprasdetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtComprasdetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtComprasdetalle.Columns.Add("Precio", Type.GetType("System.Decimal"));
                dtComprasdetalle.Columns.Add("importe", Type.GetType("System.Decimal"));
                dtComprasdetalle.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtComprasdetalle.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtComprasdetalle.Columns.Add("Costoum2", Type.GetType("System.Decimal"));
                dtComprasdetalle.Columns.Add("Factorum2", Type.GetType("System.Decimal"));
                dtComprasdetalle.Columns.Add("PIva", Type.GetType("System.Decimal"));
                dtComprasdetalle.Columns.Add("PIeps", Type.GetType("System.Decimal"));
                dtComprasdetalle.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dtComprasdetalle.Columns.Add("PDescuento", Type.GetType("System.Decimal"));
                dtComprasdetalle.Columns.Add("OCSerie", Type.GetType("System.String"));
                dtComprasdetalle.Columns.Add("OCNumero", Type.GetType("System.Int32"));
                dtComprasdetalle.Columns.Add("OCSeq", Type.GetType("System.Int32"));
                dtComprasdetalle.Columns.Add("Neto", Type.GetType("System.Decimal"));

                System.Data.DataTable dtPxA = new System.Data.DataTable("PxA");
                dtPxA.Columns.Add("ProveedoresID", Type.GetType("System.Int32"));
                dtPxA.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtPxA.Columns.Add("Clave", Type.GetType("System.String"));

                //se vuelve a llamar
                if (blnNuevo)
                    SiguienteID();

                int intRen = 0;
                string dato = String.Empty;
                decimal dCantidad = 0;
                int intArticulosID = 0;
                decimal dPrecio = 0;
                decimal dimporte = 0;
                decimal dIva = 0;
                decimal dIeps = 0;
                decimal dCostoum2 = 0;
                decimal dFactorum2 = 0;
                decimal dPIva = 0;
                decimal dPIeps = 0;
                decimal dDescuento = 0;
                decimal dPDescuento = 0;
                string strOCSerie = String.Empty;

                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtOCfolio.Text))
                    txtOCfolio.Text = "0";

                int intOCNumero = Convert.ToInt32(txtOCfolio.Text);
                int intOCSeq = 0;
                int intSeq = 0;

                decimal decTotSubtotal = 0;
                decimal decTotIva = 0;
                decimal decTotIeps = 0;
                decimal decTotNeto = 0;
                decimal decTotDescuento = 0;
                decimal decNetogrid = 0;
                string NoID = string.Empty;
                int intProveedoresID = Convert.ToInt32(cboProveedores.EditValue);
                string valor = string.Empty;
                bool success = false;

                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "Cantidad").ToString();
                    if (dato.Length > 0)
                    {
                        intSeq = i;
                        dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Cantidad"));
                        intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Articulo"));
                        dPrecio = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Precio"));
                        dimporte = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importe"));
                        dIva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Iva"));
                        dIeps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Ieps"));
                        dPIva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Piva"));
                        dPIeps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Pieps"));
                        dPDescuento = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Pdescuento"));
                        decNetogrid = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Neto"));
                        dDescuento = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Descuento"));

                        intOCSeq = 0;
                        try
                        {
                            valor = gridViewDetalle.GetRowCellValue(i, "OCSeq").ToString();
                            success = int.TryParse(valor, out intOCSeq);
                        }
                        catch
                        {

                        }
                    
                        


                        if (gridViewDetalle.GetRowCellValue(i, "NoIdentificacion") == null)
                        {
                            NoID = "";
                        }
                        else
                        {
                            NoID = gridViewDetalle.GetRowCellValue(i, "NoIdentificacion").ToString();
                        }


                        if (NoID.Length > 0)
                        {
                            dtPxA.Rows.Add(intProveedoresID, intArticulosID, NoID);
                        }

                        dtComprasdetalle.Rows.Add(cboSerie.EditValue.ToString(), Convert.ToInt32(txtFolio.Text), intSeq, dCantidad, intArticulosID, dPrecio, dimporte, dIva, dIeps, 0, 0, dPIva, dPIeps, dDescuento, dPDescuento, txtOcSerie.Text, intOCNumero, intOCSeq, decNetogrid);


                        decTotSubtotal += dimporte;
                        decTotIva += dIva;
                        decTotIeps += dIeps;
                        decTotDescuento += dDescuento;

                        decTotNeto += decNetogrid;

                    }


                }
                string strSerie = cboSerie.EditValue.ToString();
                int intFolio = Convert.ToInt32(txtFolio.Text);
                DateTime fFecha = Convert.ToDateTime(dateFecha.Text);
                strOCSerie = txtOcSerie.Text;
                int intOCFolio = Convert.ToInt32(txtOCfolio.Text);

                string strMonedas = cboMoneda.EditValue.ToString();
                int iAlm = Convert.ToInt32(cboAlmacen.EditValue);
                string strFactura = txtFactura.Text;
                DateTime ffechafactura = Convert.ToDateTime(dateFechafactura.Text);
                decimal dtipodecambio = Convert.ToDecimal(txtTipocambio.Text);
                decimal decSubtotal = decTotSubtotal;
                decimal decIva = decTotIva;
                decimal decIeps = decTotIeps;
                decimal decNeto = decTotNeto;
                int intStatus = 1;
                int intPlazo = Convert.ToInt32(txtPlazo.Text);
                DateTime fFechacancelacion = DateTime.Now;
                string strRazoncancelacion = string.Empty;
                decimal decDescuento = decTotDescuento;
                int intPoliza = 0;
                int intNodeducible = 0;
                string strContrarecibosSerie = string.Empty;
                int intContrarecibosFolio = 0;
                string strRecepcionSerie = string.Empty;
                int intRecepcionFolio = 0;
                int intValidadoPor = 0;
                DateTime fFechaValidado = DateTime.Now;
                DateTime fFechaReal = DateTime.Now;
                dato = String.Empty;
                string strObservaciones = memoObservaciones.Text;
                dtCompras.Rows.Add(strSerie, intFolio, fFecha, strOCSerie, intOCFolio, intProveedoresID, strMonedas, strFactura, ffechafactura, dtipodecambio, decSubtotal, decIva, decIeps, decNeto, intStatus, intPlazo, fFechacancelacion, strRazoncancelacion, decDescuento, intPoliza, intNodeducible, strContrarecibosSerie, intContrarecibosFolio, strRecepcionSerie, intRecepcionFolio, intValidadoPor, fFechaValidado, fFechaReal, strObservaciones, iAlm, globalCL.gv_UsuarioID, 0, txtUUID.Text,1,0,"");

                ComprasCL cl = new ComprasCL();
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.strMaquina = Environment.MachineName;
                cl.intUsuarioID = 1;
                cl.strPrograma = "0121";
                cl.dtm = dtCompras;
                cl.dtd = dtComprasdetalle;
                cl.dpxa = dtPxA;
                Result = cl.ComprasCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                

                    if (blnNuevo)
                    {
                        LimpiaCajas();
                        Inicialisalista();
                        SiguienteID();
                    }

                    cboProveedores.Focus();
                }
                else
                {
                    MessageBox.Show(Result);
                }
            }
            catch (Exception ex)
            {

                int linenum = ex.LineNumber();
                MessageBox.Show("Guardar: " + ex.Message + " LN:" + linenum.ToString());
            }
        } //Guardar

        private string Valida()
        {
            //if (cboSerie.EditValue == null)
            //{
            //    return "El campo Serie no puede ir vacio";
            //}
            if (txtFolio.Text.Length == 0)
            {
                return "El campo Folio no puede ir vacio";
            }
            if (txtOcSerie.Text.Length == 0)
            {
                txtOcSerie.Text = string.Empty;
            }

            if (txtOCfolio.Text.Length == 0)
            {
                txtOCfolio.Text = 0.ToString();
            }
            if (cboProveedores.EditValue == null)
            {
                return "El campo Proveedores no puede ir vacio";
            }
            if (cboMoneda.EditValue == null)
            {
                return "El campo MonedasID no puede ir vacio";
            }
            if (txtFactura.Text.Length == 0)
            {
                return "El campo Factura no puede ir vacio";
            }
            if (txtTipocambio.Text.Length == 0)
            {
                return "El campo tipodecambio no puede ir vacio";
            }
            if (memoObservaciones.Text.Length == 0)
            {
                memoObservaciones.Text = string.Empty;
            }
            if (txtDescuento.Text.Length == 0)
            {
                txtDescuento.Text = 0.ToString();
            }

            string ss = ConfigurationManager.AppSettings["serie"].ToString();
            string ssc = cboSerie.EditValue.ToString();

            if (ss != ssc)
            {
                return "La serie del combo no concuerda con la del config";
            }


            globalCL clg = new globalCL();
            int ren = 0;
            string dato = string.Empty;
            for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++)
            {
                if (gridViewDetalle.GetRowCellValue(i, "Cantidad") != null)
                {
                    if (clg.esNumerico(gridViewDetalle.GetRowCellValue(i, "Cantidad").ToString()))
                    {
                        ren = ren + 1;
                    }

                }
            }

            if (ren == 0)
            {
                return "Debe capturar al menos un renglón";
            }

            if (Convert.ToDateTime(dateFecha.Text) > DateTime.Now)
            {
                return "No se permiten fechas futuras";
            }

            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(dateFecha.Text).Year, Convert.ToDateTime(dateFecha.Text).Month, "COM");
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";
                
            }

            if (cboMoneda.EditValue.ToString() != "MXN")
            {
                if (!clg.esNumerico(txtTipocambio.Text))
                    txtTipocambio.Text = "1";
                else
                {
                    if (txtTipocambio.Text == "0")
                        txtTipocambio.Text = "1";
                }
                if (Convert.ToDecimal(txtTipocambio.Text) == 1)
                {
                    return "Si la moneda no es MXN, debe capturar el tipo de cambio";
                }
            }
            else
                txtTipocambio.Text = "1";

            return "OK";
        } //Valida

        private void llenaCajas()
        {
            ComprasCL cl = new ComprasCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            String Result = cl.ComprasLlenaCajas();
            if (Result == "OK")
            {
                dateFecha.Text = cl.fFecha.ToShortDateString();
                txtOcSerie.Text = cl.strOCSerie;
                txtOCfolio.Text = cl.intOCFolio.ToString();
                cboProveedores.EditValue = cl.intProveedoresID;
                cboMoneda.EditValue = cl.strMonedasID;
                cboAlmacen.EditValue = cl.intAlmacen;
                txtFactura.Text = cl.strFactura;
                dateFechafactura.Text = cl.ffechafactura.ToShortDateString();
                txtTipocambio.Text = cl.dectipodecambio.ToString();
                cboSerie.EditValue = strSerie;
                txtFolio.Text = intFolio.ToString();
                txtUUID.Text = cl.strUUID;
                lblRM.Text = "RM: " + cl.intRecepcionFolio.ToString();
                lblPolizaTesk.Text = "Póliza: " + cl.teskPoliza;

                if (txtUUID.Text.Length > 0)
                {
                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                txtPlazo.Text = cl.intPlazo.ToString();

                if (cl.intPago > 0)
                {
                    lblPagada.Text = "Pagada";
                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiOC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else
                    lblPagada.Text = "";
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void DetalleLlenaCajas()
        {
            try
            {
                ComprasCL cl = new ComprasCL();
                cl.intFolio = intFolio;
                cl.strSerie = strSerie;
                gridControlDetalle.DataSource = cl.ComprasDetalleGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }

        }//DetalleLlenaCajas

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

        private void Editar()
        {
            blnNuevo = false;
            DatosFactura = "N";
            BotonesEdicion();
            llenaCajas();
            DetalleLlenaCajas();
        }

        private void BotonesEdicion()
        {
            LimpiaCajas();
            if (intRM == 0)
            {
                bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDatosFac.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiIncrementales.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiPolizaTesk.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            navBarControl.Visible = false;
            StatusBar.Visible = false;

            if (blnNuevo)
            {
                bbiXML.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }

            navigationFrame.SelectedPageIndex = 1;
        }

        private void Cancelar()
        {
            ComprasCL cl = new ComprasCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            cl.intUsuarioID = intUsuariocancelo;
            String Result = cl.ComprasCancelar();
            if (Result == "OK")
            {
                MessageBox.Show("Cancelado correctamente");
                LlenarGrid(AñoFiltro,MesFiltro);
            }
            else
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (strSerie.Length == 0)
            //    if (intFolio == 0)
            //    {
            //        MessageBox.Show("Selecciona un renglón");
            //    }
            //    else
            //    {
            //        DialogResult Result = MessageBox.Show("Desea eliminar el ID " + strSerie.ToString(), "Elimnar", MessageBoxButtons.YesNo);
            //        if (Result.ToString() == "Yes")
            //        {
            //            Cancelar();
            //        }
            //    }
        }

        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiXML.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiIncrementales.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiPolizaTesk.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (intRM>0)
                bbiDatosFac.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navBarControl.Visible = true;
            StatusBar.Visible = true;

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Cargando grid","Espere por favor...");
            LlenarGrid(AñoFiltro,MesFiltro);
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            navigationFrame.SelectedPageIndex = 0;

            globalCL clg = new globalCL();

            clg.strGridLayout = "gridComprasDetalle";
            String Result = clg.SaveGridLayout(gridViewDetalle);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }

        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCompras";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
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

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                strSerie = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie").ToString();
                intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
                intRM = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "RecepcionFolio"));
                dSubtotaldeCompra = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Subtotal"));
                strTipodeMoneda = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "MonedasID").ToString();
            }
            catch(Exception)
            {
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
                    //4Ago21 JZG se quita validación por que se resuelve en SP ComprasCancelar
                    //if (intRM > 0)
                    //{
                    //    MessageBox.Show("Esta compra no se puede cancelar por que se generó de una recepción de mercancía, debe cancelar la recepción");
                    //    return;
                    //}

                    popUpCancelar.Visible = true;
                    groupControl3.Text = "Cancelar el folio:" + strSerie + intFolio.ToString();
                    popUpCancelar.Show();
                    txtLogin.Focus();
                }
            

        }

        private void cboProveedores_EditValueChanged(object sender, EventArgs e)
        {
            //ComprasCL cl = new ComprasCL();
            //cl.intProveedoresID = Convert.ToInt32(cboProveedores.EditValue);
            //string strResult = cl.ObtenerPlazoProveedor();
            //if (strResult == "OK") txtPlazo.Text = cl.intPlazo.ToString();

            object orow = cboProveedores.Properties.GetDataSourceRowByKeyValue(cboProveedores.EditValue);
            if (orow != null)
            {
                txtPlazo.Text = ((DataRowView)orow)["Plazo"].ToString();
                pIvaProv = Convert.ToDecimal(((DataRowView)orow)["Piva"]);
                cboMoneda.EditValue = ((DataRowView)orow)["MonedasID"].ToString();
            }

        }

        private void reporte()
        {
            try
            {
                ComprasFormatoImpresion report = new ComprasFormatoImpresion();
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

        private void btoAplicarporcentaje_Click(object sender, EventArgs e)
        {
            if (txtDescuento.Text.Length == 0) MessageBox.Show("Capture un descuento");

            decimal PDescuento = 0;
            decimal dimporte = 0;
            decimal descuento = 0;
            decimal iva = 0;
            decimal piva = 0;
            string valor = string.Empty;
            bool success;
            decimal imp = 0;

            PDescuento = Convert.ToDecimal(txtDescuento.Text);

            for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
            {
                gridViewDetalle.FocusedRowHandle = i;
                gridViewDetalle.SetRowCellValue(i, "Pdescuento", PDescuento);
                dimporte = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importe"));
                descuento = dimporte * (PDescuento / 100);
                gridViewDetalle.SetRowCellValue(i, "Descuento", descuento);

                //Extraemos el valor del grid, celda % iva
                if (pIvaProv > 0)  //Si el proveedor no maneja iva, se ignora el del artículo
                {
                    if (gridViewDetalle.GetRowCellValue(i, "Piva") == null)
                    {
                        valor = "0";
                    }
                    else
                    {
                        valor = gridViewDetalle.GetRowCellValue(i, "Piva").ToString();
                    }
                }
                else
                {
                    valor = "0";
                }

                dIvaPorcentaje = Convert.ToDecimal(valor);
                success = Decimal.TryParse(valor, out piva);

                imp = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importe")); //Calculamos el importe y lo redondeamos a dos decimales
                iva = Math.Round((imp - descuento) * (dIvaPorcentaje / 100), 2);

                gridViewDetalle.SetFocusedRowCellValue("Iva", iva); //Le ponemos el valor a la celda Iva del grid
                gridViewDetalle.SetFocusedRowCellValue("Neto", iva + imp - descuento); //Le ponemos el valor a la celda neto del grid


            }
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
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
                        gridViewDetalle.SetFocusedRowCellValue("Piva", cl.dPtjeIva);
                        gridViewDetalle.SetFocusedRowCellValue("Pieps", cl.dPtjeIeps);
                        gridViewDetalle.SetFocusedRowCellValue("Ieps", 0);

                        gridViewDetalle.SetFocusedRowCellValue("Pdescuento", 0);
                        gridViewDetalle.SetFocusedRowCellValue("Descuento", 0);

                    }
                }

                if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "NoIdentificacion") == null)
                {
                    gridViewDetalle.SetFocusedRowCellValue("NoIdentificacion", "X");
                }
                else
                {
                    if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "NoIdentificacion").ToString().Length == 0)
                    {
                        gridViewDetalle.SetFocusedRowCellValue("NoIdentificacion", "X");
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
                if (pIvaProv > 0)
                {
                    if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Piva") == null)
                    {
                        valor = "0";
                    }
                    else
                    {
                        valor = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Piva").ToString();
                    }
                }
                else
                {
                    valor = "0";
                }

                dIvaPorcentaje = 0;
                //dIvaPorcentaje = Convert.ToDecimal(valor);
                success = Decimal.TryParse(valor, out dIvaPorcentaje);

                imp = Math.Round(cant * pu, 2); //Calculamos el importe y lo redondeamos a dos decimales
                iva = Math.Round(imp * (dIvaPorcentaje / 100), 2);

                gridViewDetalle.SetFocusedRowCellValue("Importe", imp); //Le ponemos el valor a la celda Importe del grid
                gridViewDetalle.SetFocusedRowCellValue("Iva", iva); //Le ponemos el valor a la celda Iva del grid
                gridViewDetalle.SetFocusedRowCellValue("Neto", iva + imp); //Le ponemos el valor a la celda neto del grid

            }

            //Calculamos el descuento
            if (e.Column.Name == "gridColumnPdescuento")
            {
                decimal descuento = 0;
                decimal pdescuento = 0;
                decimal importe = 0;



                //Extraemos el valor del pdescuento
                if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Pdescuento") == null)
                {
                    gridViewDetalle.SetFocusedRowCellValue("Pdescuento", pdescuento);
                    gridViewDetalle.SetFocusedRowCellValue("Descuento", descuento);
                }
                else
                {
                    pdescuento = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Pdescuento"));
                    importe = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Importe"));
                    descuento = importe * (pdescuento / 100);
                    gridViewDetalle.SetFocusedRowCellValue("Descuento", descuento);
                }
            }
        }

        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void bbiRegresarImp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void cboMoneda_EditValueChanged(object sender, EventArgs e)
        {
            if (cboMoneda.EditValue.ToString() == "MXN")
            {
                txtTipocambio.Text = "1";
                txtTipocambio.Enabled = false;
            }
            else
            {
                txtTipocambio.Enabled = true;
            }
        }

        private void gridViewPrincipal_DoubleClick(object sender, EventArgs e)
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

        #region navBarItems

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

        private void navBarItemSep_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
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

        private void navBarItemTodo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilter.Clear();
        }

        #endregion

        //Esto elimina el renglon seleccionado en una partida
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

        private void CargaXml()
        {
            try
            {
                //string strLayoutPath = System.Configuration.ConfigurationManager.AppSettings["gridlayout"].ToString() + "xmlcompras.txt";
                //string data = string.Empty;
                //if (File.Exists(strLayoutPath))
                //{
                //    data = System.IO.File.ReadAllText(strLayoutPath);
                //    data = data.Substring(0, data.Length - 2) + "\\";
                //}

                //if (data.Length == 0)
                //    xtraOpenFileDialog1.InitialDirectory = "C:\\";
                //else
                //    xtraOpenFileDialog1.InitialDirectory = data;

                //xtraOpenFileDialog1.ShowDragDropConfirmation = true;
                //xtraOpenFileDialog1.AutoUpdateFilterDescription = false;
                //xtraOpenFileDialog1.Filter = "XML files (*.xml)|*.xml";
                //xtraOpenFileDialog1.Multiselect = false;

                //DialogResult dr = xtraOpenFileDialog1.ShowDialog();
                //if (dr == System.Windows.Forms.DialogResult.OK)
                //{
                //    string file = xtraOpenFileDialog1.FileName;
                //    string sDir = Path.GetDirectoryName(file);
                //    LeeXml(file);


                //    using (StreamWriter writer = new StreamWriter(strLayoutPath))
                //    {
                //        writer.WriteLine(sDir);
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LeeXml(string filename)
        {

            DataTable dt = new DataTable();

            string attrName = string.Empty;
            string strSerie = string.Empty;
            string strFolio = string.Empty;
            string strFecha = string.Empty;
            string strProv = string.Empty;
            string strRfcEmisor = string.Empty;
            string strRfcReceptor = string.Empty;
            string strTC = string.Empty;
            string strSubtotal = string.Empty;
            string strIva = string.Empty;
            string strRetIva = string.Empty;
            string strNeto = string.Empty;
            string strTipoProv = string.Empty;
            string strEmisorNombre = string.Empty;
            string strReceptorNombre = string.Empty;
            string strFP = string.Empty;
            string strMoneda = string.Empty;
            string strTipo = string.Empty;
            string strMP = string.Empty;
            string strUso = string.Empty;


            cfdiCL cl = new cfdiCL();
            cl.pSerie = System.Configuration.ConfigurationManager.AppSettings["Serie"].ToString();
            string result = cl.DatosCfdiEmisor();

            if (result == "OK")
            {
                strRfcEmpresa = cl.pEmisorRegFed;
            }
            else
            {
                MessageBox.Show("No se pudo leer los datos de la empresa");
                return;
            }



            dt.Columns.Add("Cantidad", System.Type.GetType("System.String"));
            dt.Columns.Add("Articulo", System.Type.GetType("System.String"));
            dt.Columns.Add("Precio", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Importe", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Iva", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Neto", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Ieps", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Costoum2", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Factorum2", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Piva", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Pieps", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Descuento", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Pdescuento", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("Descripcion", System.Type.GetType("System.String"));
            dt.Columns.Add("OCSerie", System.Type.GetType("System.String"));
            dt.Columns.Add("OCNumero", System.Type.GetType("System.String"));
            dt.Columns.Add("OCSeq", System.Type.GetType("System.String"));
            dt.Columns.Add("NoIdentificacion", System.Type.GetType("System.String"));

            string sclaveProdServ = string.Empty;
            string sNoIdentificacion = string.Empty;
            string sCantidad = string.Empty;
            string sClaveUnidad = string.Empty;
            string sUnidad = string.Empty;
            string sDescripcion = string.Empty;
            string sValorUnitario = string.Empty;
            string sImporte = string.Empty;
            int intProv = 0;
            decimal iva = 0;
            decimal neto = 0;
            string UUID = string.Empty;
            string strArt = string.Empty;


            string attrImptoName = string.Empty;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            //Datos generales
            foreach (XmlNode nodeG in xmlDoc.ChildNodes)
            {
                //MessageBox.Show(nodeG.Name.ToString())

                if (nodeG.Name.Equals("cfdi:Comprobante"))
                {
                    foreach (XmlAttribute attr in nodeG.Attributes)
                    {
                        attrName = attr.Name.ToString();
                        switch (attrName)
                        {
                            case "Serie":
                                strSerie = attr.Value.ToString();
                                break;
                            case "Folio":
                                strFolio = attr.Value.ToString();
                                break;
                            case "Fecha":
                                strFecha = attr.Value.ToString();
                                break;
                            case "FormaPago":
                                strFP = attr.Value.ToString();
                                break;
                            case "SubTotal":
                                strSubtotal = attr.Value.ToString();
                                break;
                            case "Moneda":
                                strMoneda = attr.Value.ToString();
                                break;
                            case "Total":
                                strNeto = attr.Value.ToString();
                                break;
                            case "TipoDeComprobante":
                                strTipo = attr.Value.ToString();
                                break;
                            case "MetodoPago":
                                strMP = attr.Value.ToString();
                                break;
                        }
                    }
                    foreach (XmlNode nodeD in nodeG.ChildNodes)
                    {
                        if (nodeD.Name.Equals("cfdi:Emisor"))
                        {
                            foreach (XmlAttribute attr in nodeD.Attributes)
                            {
                                attrName = attr.Name.ToString();
                                switch (attrName)
                                {
                                    case "Rfc":
                                        strRfcEmisor = attr.Value.ToString();
                                        break;
                                    case "Nombre":
                                        strEmisorNombre = attr.Value.ToString();
                                        break;
                                }
                            }
                        }

                        if (nodeD.Name.Equals("cfdi:Complemento"))
                        {
                            foreach (XmlNode nodeU in nodeD.ChildNodes)
                            {
                                if (nodeU.Name.Equals("tfd:TimbreFiscalDigital"))
                                {
                                    foreach (XmlAttribute attr in nodeU.Attributes)
                                    {
                                        attrName = attr.Name.ToString();
                                        if (attrName == "UUID")
                                        {
                                            txtUUID.Text = attr.Value.ToString();
                                        }
                                    }
                                }
                            }


                        }

                        if (nodeD.Name.Equals("cfdi:Receptor"))
                        {
                            foreach (XmlAttribute attr in nodeD.Attributes)
                            {
                                attrName = attr.Name.ToString();
                                switch (attrName)
                                {
                                    case "Rfc":
                                        strRfcReceptor = attr.Value.ToString();
                                        break;
                                    case "Nombre":
                                        strReceptorNombre = attr.Value.ToString();
                                        break;
                                    case "UsoCFDI":
                                        strUso = attr.Value.ToString();
                                        break;
                                }
                            }
                        }  // Receptor
                        if (nodeD.Name.Equals("cfdi:Impuestos"))
                        {
                            foreach (XmlAttribute attr in nodeD.Attributes)
                            {
                                attrName = attr.Name.ToString();
                                switch (attrName)
                                {
                                    case "TotalImpuestosTrasladados":
                                        strIva = attr.Value.ToString();
                                        break;
                                    case "TotalImpuestosRetenidos":
                                        strRetIva = attr.Value.ToString();
                                        break;
                                }
                            }
                        }  // Receptor
                    }
                } //Comprobante
            } //foreach (XmlNode nodeG in xmlDoc.ChildNodes)

            ComprasCL clc = new ComprasCL();
            if (strTipo == "I" && strRfcEmpresa == strRfcReceptor)
            {

                clc.strRfc = strRfcEmisor;
                result = clc.Leeproveedorporrfc();
                if (result == "OK")
                {
                    intProv = clc.intProveedoresID;
                    if (clc.strTipoProv == "1")
                    {
                        MessageBox.Show("Este XML es de un proveedor de servicios, registrelo en contrarecibos");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Este proveedor no está registrado o el rfc está incorrecto: " + strRfcEmisor + " " + strEmisorNombre);
                    return;
                }
            }
            else
            {
                MessageBox.Show("XML no es de ingreso o no es para el rfc: " + strRfcEmpresa);
                return;
            }

            cboProveedores.EditValue = intProv;
            dateFecha.Text = strFecha;
            dateFechafactura.Text = strFecha;
            cboMoneda.EditValue = strMoneda;
            txtFactura.Text = strSerie + strFolio;
            cboProveedores.Enabled = false;

            //Eof: Datos generales

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {

                if (element.Name.Equals("cfdi:Conceptos"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        //MessageBox.Show(node.Name.ToString());
                        if (node.Name.Equals("cfdi:Concepto"))
                        {

                            //MessageBox.Show("Dentro de cpto:" + node.Name.ToString());



                            foreach (XmlAttribute attr in node.Attributes)
                            {
                                attrName = attr.Name;

                                switch (attrName)
                                {
                                    case "ClaveProdServ":
                                        sclaveProdServ = attr.Value.ToString();
                                        break;
                                    case "NoIdentificacion":
                                        sNoIdentificacion = attr.Value.ToString();
                                        break;
                                    case "Cantidad":
                                        sCantidad = attr.Value.ToString();
                                        break;
                                    case "ClaveUnidad":
                                        sClaveUnidad = attr.Value.ToString();
                                        break;
                                    case "Unidad":
                                        sUnidad = attr.Value.ToString();
                                        break;
                                    case "Descripcion":
                                        sDescripcion = attr.Value.ToString();
                                        break;
                                    case "ValorUnitario":
                                        sValorUnitario = attr.Value.ToString();
                                        break;
                                    case "Importe":
                                        sImporte = attr.Value.ToString();
                                        break;
                                }
                            }

                            //dt.Columns.Add("Cantidad", System.Type.GetType("System.String"));
                            //dt.Columns.Add("Articulo", System.Type.GetType("System.String"));
                            //dt.Columns.Add("Precio", System.Type.GetType("System.Decimal"));
                            //dt.Columns.Add("Importe", System.Type.GetType("System.Decimal"));
                            //dt.Columns.Add("Iva", System.Type.GetType("System.Decimal"));
                            //dt.Columns.Add("Neto", System.Type.GetType("System.Decimal"));
                            //dt.Columns.Add("Ieps", System.Type.GetType("System.Decimal"));
                            //dt.Columns.Add("Costoum2", System.Type.GetType("System.Decimal"));
                            //dt.Columns.Add("Factorum2", System.Type.GetType("System.Decimal"));
                            //dt.Columns.Add("Piva", System.Type.GetType("System.Decimal"));
                            //dt.Columns.Add("Pieps", System.Type.GetType("System.Decimal"));
                            //dt.Columns.Add("Descuento", System.Type.GetType("System.Decimal"));
                            //dt.Columns.Add("Pdescuento", System.Type.GetType("System.Decimal"));
                            //dt.Columns.Add("Descripcion", System.Type.GetType("System.String"));
                            //dt.Columns.Add("OCSerie", System.Type.GetType("System.String"));
                            //dt.Columns.Add("OCNumero", System.Type.GetType("System.String"));
                            //dt.Columns.Add("OCSeq", System.Type.GetType("System.String"));
                            //dt.Columns.Add("NoIdentificacion", System.Type.GetType("System.String"));

                            //aqui

                            clc.intProveedoresID = intProv;
                            clc.strClave = sNoIdentificacion;
                            result = clc.ObtenArticuloIDPxA();
                            if (result == "OK")
                            {
                                strArt = clc.intArt.ToString();
                            }


                            iva = Convert.ToDecimal(sImporte) * (pIvaProv / 100);
                            neto = Convert.ToDecimal(sImporte) + iva;
                            dt.Rows.Add(Convert.ToDecimal(sCantidad), strArt, Convert.ToDecimal(sValorUnitario),
                                Convert.ToDecimal(sImporte), iva, neto, 0, 0, 0, pIvaProv, 0, 0, 0, sDescripcion, "", "", "", sNoIdentificacion);
                            //dt.Rows.Add(sclaveProdServ, sNoIdentificacion, sCantidad, sClaveUnidad, sUnidad, sDescripcion, sValorUnitario, sImporte);

                            //Impuestos
                            foreach (XmlNode nl in node.ChildNodes)
                            {
                                //MessageBox.Show(nl.Name.ToString());
                                foreach (XmlNode nl2 in nl.ChildNodes)
                                {
                                    //MessageBox.Show(nl2.Name.ToString());
                                    foreach (XmlNode nl3 in nl2.ChildNodes)
                                    {
                                        if (nl3.Name.Equals("cfdi:Traslado"))
                                        {
                                            foreach (XmlAttribute attrI in nl3.Attributes)
                                            {
                                                attrImptoName = attrI.Name;
                                                //MessageBox.Show(attrImptoName + " " + attrI.Value.ToString());
                                            }

                                        }

                                    }
                                }
                            }


                        }


                    }
                }
            } // foreach (XmlElement element in xmlDoc.DocumentElement) 

            gridControlDetalle.DataSource = dt;

        } //LeeXML

        private void bbiXML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CargaXml();
        }

        private void btnHabilitaProveedor_Click(object sender, EventArgs e)
        {
            cboProveedores.Enabled = true;
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
            txtRazoncancelacion.Text = string.Empty;
        }
        private void CierraPopUpTesk()
        {
            popupTesk.Visible = false;
            txtLoginTesk.Text = string.Empty;
            txtPasswordTesk.Text = string.Empty;
            txtPolizaTesk.Text = string.Empty;
        }
        private void btnAut_Click(object sender, EventArgs e)
        {

            globalCL clg = new globalCL();
            string result = clg.GM_CierredemodulosStatus(DateTime.Now.Year, DateTime.Now.Month, "COM");
            if (result == "C")
            {
                MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                return;
            }

            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLogin.Text;
            clU.strClave = txtPassword.Text;
            clU.strPermiso = "Cancelarcompras";
            result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }
            intUsuariocancelo = clU.intUsuariosID;
            CierraPopUp();
            Cancelar();
        }

        private void gridViewPrincipal_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string _mark = (string)view.GetRowCellValue(e.RowHandle, "Status");

            if (_mark == "Cancelado")
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Red;
            }
        }

        #region INCREMENTALES

        string strSerieFolio;
        decimal dSubtotaldeCompra;
        string strTipodeMoneda;

        private void LimpiaCajasIncrementales()
        {

            txtNumeroPedimento.Text = string.Empty;
            txtAduana.Text = string.Empty;
            txtFechaInc.Text = DateTime.Now.ToShortDateString();

            txtTipodecambioInc.Text = "0";
            txtFactordeintegracion.Text = "0";
            txtGastosaduanalesUSA.Text = "0";
            txtFleteextranjero.Text = "0";
            txtGastosaduanalesMX.Text = "0";
            txtFletenacional.Text = "0";
            txtDTA.Text = "0";
            txtIvadelpedimento.Text = "0";
            txtIGI.Text = "0";
            txtPRV.Text = "0";

            txtTotalincremental.Text = "0";
            txtBaseparacalcularPtje.Text = string.Empty;
            txtPtjeincremental.Text = "0";
            txtNumeroPedimento.Text = string.Empty;
        }

        private void Incremental()
        {
            strSerieFolio = strSerie + intFolio.ToString();
            BuscarIncremental();
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup3.Visible = true;
            navBarControl.Visible = false;

            cboMonedasIDIncremental.EditValue = strTipodeMoneda;
            txtBaseparacalcularPtje.Text = dSubtotaldeCompra.ToString();
            lblSerieyFolio.Text = strSerieFolio;

            navigationFrame.SelectedPageIndex = 3;
        }

        private void BuscarIncremental()
        {
            IncrementalesCL cl = new IncrementalesCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            String Result = cl.BuscarIncrementalporSerieyFolio();
            if (Result == "OK")
            {
                txtNumeroPedimento.Text = cl.strPedimento;
                txtAduana.Text = cl.strAduana;
                txtFechaInc.Text = cl.fFecha.ToString();
                txtTipodecambioInc.Text = cl.dTipodecambio.ToString();
                txtFactordeintegracion.Text = cl.dFactordeintegracion.ToString();
                txtGastosaduanalesUSA.Text = cl.dGastosaduanalesUSA.ToString();
                txtFleteextranjero.Text = cl.dFleteextranjero.ToString();
                txtGastosaduanalesMX.Text = cl.dGastosaduanalesMX.ToString();
                txtFletenacional.Text = cl.dFletenacional.ToString();
                txtDTA.Text = cl.dDTA.ToString();
                txtIvadelpedimento.Text = cl.dIvadelpedimento.ToString();
                txtIGI.Text = cl.dIGI.ToString();
                txtPRV.Text = cl.dPRV.ToString();
                txtTotalincremental.Text = cl.dTotalincremental.ToString();
                txtBaseparacalcularPtje.Text = cl.dBaseparacalcularPtje.ToString();
                txtPtjeincremental.Text = cl.dPtjeincremental.ToString();
               

                CalcularTotalIncremental();
            }
            if (Result == "Empty")
            {
                LimpiaCajasIncrementales();
            }
            else
            {
                //MessageBox.Show(Result);
            }
        } // BuscarIncremental

        private string ValidaIncrementales()
        {

            if (txtNumeroPedimento.Text.Length == 0)
            {
                return "El campo Numero no puede ir vacio";
            }
            if (txtAduana.Text.Length == 0)
            {
                return "El campo Aduana no puede ir vacio";
            }
            if (txtFechaInc.Text.Length == 0)
            {
                return "El campo Fecha no puede ir vacio";
            }
            if (txtTipodecambioInc.Text.Length == 0)
            {
                return "El campo Tipodecambio no puede ir vacio";
            }
            if (txtFactordeintegracion.Text.Length == 0)
            {
                return "El campo Factordeintegracion no puede ir vacio";
            }
            if (txtGastosaduanalesUSA.Text.Length == 0)
            {
                return "El campo GastosaduanalesUSA no puede ir vacio";
            }
            if (txtFleteextranjero.Text.Length == 0)
            {
                return "El campo Fleteextranjero no puede ir vacio";
            }
            if (txtGastosaduanalesMX.Text.Length == 0)
            {
                return "El campo GastosaduanalesMX no puede ir vacio";
            }
            if (txtFletenacional.Text.Length == 0)
            {
                return "El campo Fletenacional no puede ir vacio";
            }
            if (txtDTA.Text.Length == 0)
            {
                return "El campo DTA no puede ir vacio";
            }
            if (txtIvadelpedimento.Text.Length == 0)
            {
                return "El campo Ivadelpedimento no puede ir vacio";
            }
            if (txtIGI.Text.Length == 0)
            {
                return "El campo IGI no puede ir vacio";
            }
            if (txtPRV.Text.Length == 0)
            {
                return "El campo PRV no puede ir vacio";
            }
            if (txtTotalincremental.Text.Length == 0)
            {
                return "El campo Totalincremental no puede ir vacio";
            }
            if (txtBaseparacalcularPtje.Text.Length == 0)
            {
                return "El campo BaseparacalcularPtje no puede ir vacio";
            }
            if (txtPtjeincremental.Text.Length == 0)
            {
                return "El campo Ptjeincremental no puede ir vacio";
            }

            globalCL clg = new globalCL();
            if (!clg.esNumerico(txtNumeroPedimento.Text.Substring(0, 2)))
            {
                return "Pedimento mal capturado";
            }
            if (txtNumeroPedimento.Text.Substring(2, 2) != "  ")
            {
                return "Pedimento mal capturado";
            }
            if (!clg.esNumerico(txtNumeroPedimento.Text.Substring(4, 2)))
            {
                return "Pedimento mal capturado";
            }
            if (txtNumeroPedimento.Text.Substring(6, 2) != "  ")
            {
                return "Pedimento mal capturado";
            }
            if (!clg.esNumerico(txtNumeroPedimento.Text.Substring(8, 4)))
            {
                return "Pedimento mal capturado";
            }
            if (txtNumeroPedimento.Text.Substring(12, 2) != "  ")
            {
                return "Pedimento mal capturado";
            }
            if (!clg.esNumerico(txtNumeroPedimento.Text.Substring(14)))
            {
                return "Pedimento mal capturado";
            }

            return "OK";
        } //Valida

        private void CalcularTotalIncremental()
        {
            decimal
            dTipodecambio = 0
            , dGastosaduanalesUSA = 0
            , dFleteextranjero = 0
            , dGastosaduanalesMX = 0
            , dFletenacional = 0
            , dDTA = 0
            , dIvadelpedimento = 0
            , dIGI = 0
            , dPRV = 0
            , dTotalincremental = 0
            , dBaseparacalcularPtje = 0
            , dPtjeincremental = 0
            ,dTotalMX=0
            ,dTotalUSD=0;

            if (txtTipodecambioInc.Text.Length == 0) { }
            else { dTipodecambio = Convert.ToDecimal(txtTipodecambioInc.Text); }

            if (txtGastosaduanalesUSA.Text.Length == 0) { }
            else { dGastosaduanalesUSA = Convert.ToDecimal(txtGastosaduanalesUSA.Text); }

            if (txtFleteextranjero.Text.Length == 0) { }
            else { dFleteextranjero = Convert.ToDecimal(txtFleteextranjero.Text); }

            if (txtGastosaduanalesMX.Text.Length == 0) { }
            else { dGastosaduanalesMX = Convert.ToDecimal(txtGastosaduanalesMX.Text); }

            if (txtFletenacional.Text.Length == 0) { }
            else { dFletenacional = Convert.ToDecimal(txtFletenacional.Text); }

            if (txtDTA.Text.Length == 0) { }
            else { dDTA = Convert.ToDecimal(txtDTA.Text); }

            if (txtIvadelpedimento.Text.Length == 0) { }
            else { dIvadelpedimento = Convert.ToDecimal(txtIvadelpedimento.Text); }

            if (txtIGI.Text.Length == 0) { }
            else { dIGI = Convert.ToDecimal(txtIGI.Text); }

            if (txtPRV.Text.Length == 0) { }
            else { dPRV = Convert.ToDecimal(txtPRV.Text); }


            if (strTipodeMoneda == "MXN")
            {
                dTotalUSD = (dGastosaduanalesUSA + dFleteextranjero);
                dTotalMX = ((dGastosaduanalesMX + dFletenacional + dIGI + dDTA + dPRV) / dTipodecambio);

                dTotalincremental = dTotalUSD + dTotalMX; 
            }
            else
            {
                dTotalUSD = ((dGastosaduanalesUSA + dFleteextranjero));
                dTotalMX = Math.Round(((dGastosaduanalesMX + dFletenacional + dIGI + dDTA + dPRV) / dTipodecambio),2);

                dTotalincremental = dTotalUSD + dTotalMX;
            }

            txtTotalincremental.Text = dTotalincremental.ToString();
            txtBaseparacalcularPtje.Text = dSubtotaldeCompra.ToString();
            txtPtjeincremental.Text = Math.Round(((dTotalincremental / dSubtotaldeCompra)*100), 4).ToString();
        }

        private void GuardarIncremental()
        {
            try
            {
                String Result = ValidaIncrementales();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }
                IncrementalesCL cl = new IncrementalesCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.strPedimento = txtNumeroPedimento.Text;
                cl.strAduana = txtAduana.Text;
                cl.fFecha = Convert.ToDateTime(txtFechaInc.Text);
                cl.dTipodecambio = Convert.ToDecimal(txtTipodecambioInc.Text);
                cl.dFactordeintegracion = Convert.ToDecimal(txtFactordeintegracion.Text);
                cl.dGastosaduanalesUSA = Convert.ToDecimal(txtGastosaduanalesUSA.Text);
                cl.dFleteextranjero = Convert.ToDecimal(txtFleteextranjero.Text);
                cl.dGastosaduanalesMX = Convert.ToDecimal(txtGastosaduanalesMX.Text);
                cl.dFletenacional = Convert.ToDecimal(txtFletenacional.Text);
                cl.dDTA = Convert.ToDecimal(txtDTA.Text);
                cl.dIvadelpedimento = Convert.ToDecimal(txtIvadelpedimento.Text);
                cl.dIGI = Convert.ToDecimal(txtIGI.Text);
                cl.dPRV = Convert.ToDecimal(txtPRV.Text);
                cl.dTotalincremental = Convert.ToDecimal(txtTotalincremental.Text);
                cl.dBaseparacalcularPtje = Convert.ToDecimal(txtBaseparacalcularPtje.Text);
                cl.dPtjeincremental = Convert.ToDecimal(txtPtjeincremental.Text);
                Result = cl.IncrementalesCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");

                }
                else
                {
                    MessageBox.Show(Result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GuardarIncremental: " + ex.Message);
            }
        } //Guardar

        private void bbiIncrementales_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Incremental();
            }
        }

        private void bbiRegresarIncre_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageGroup1.Visible = true;
            ribbonPageGroup3.Visible = false;
            navBarControl.Visible = true;
            strSerieFolio = "";
            intFolio = 0;
            LimpiaCajasIncrementales();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiGuardarIncre_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GuardarIncremental();
        }

        private void txtTipodecambioInc_EditValueChanged(object sender, EventArgs e)
        {

            //if (txtTipodecambioInc.Text.Length == 0) { }
            //if (txtGastosaduanalesUSA.Text.Length == 0) { }
            //if (txtFleteextranjero.Text.Length == 0) { }
            //if (txtGastosaduanalesMX.Text.Length == 0) { }
            //if (txtFletenacional.Text.Length == 0) { }
            //if (txtDTA.Text.Length == 0) { }
            //if (txtIvadelpedimento.Text.Length == 0) { }
            //if (txtIGI.Text.Length == 0) { }
            //if (txtPRV.Text.Length == 0) { }

            //else { CalcularTotalIncremental(); }

        }

        private void cboMonedasIDIncremental_EditValueChanged(object sender, EventArgs e)
        {
            strTipodeMoneda = cboMonedasIDIncremental.EditValue.ToString();
           
        }

        private void bbiCalcularIncremental_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CalcularTotalIncremental();
        }

    

        private void bbiDatosFac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio==0)
            {
                MessageBox.Show("Seleccione un folio");
                return;
            }



            DatosFactura = "S";
            lblCompraFac.Text = "Datos de la factura para la compra: " + intFolio.ToString() + "/n" + cboProveedores.Text;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiIncrementales.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDatosFac.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navBarControl.Visible = false;


            txtFac2.Text = txtFactura.Text;
            dFechaFac2.Text = dateFechafactura.Text;
            txtMoneda2.Text = strTipodeMoneda;
            txtTC2.Text = txtTipocambio.Text;
            txtPlazo2.Text = txtPlazo.Text;

            if (txtMoneda2.Text == "MXN")
            {
                txtTC2.Text = "1";
                txtTC2.Enabled = false;
            }
            else
                txtTC2.Enabled = true;
            

            navigationFrame.SelectedPageIndex = 5;
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
                    case "navBarItemSep":
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

        private void gridControlPrincipal_Click(object sender, EventArgs e)
        {

        }

        private void bbiOC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                RecepciondemercanciaCL cl = new RecepciondemercanciaCL();
                cl.IdProveedor = Convert.ToInt32(cboProveedores.EditValue);
                cl.strSerie = "";
                gridControlOC.DataSource = cl.OrdenComprasPorProveedor();
                popupContainerControlOC.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            popupContainerControlOC.Hide();
        }

        private void gridControlOC_Click(object sender, EventArgs e)
        {
            Ordendecompra = Convert.ToInt32(gridViewOC.GetRowCellValue(gridViewOC.FocusedRowHandle, "FolioOC"));
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            cargaOC();
        }
        private void cargaOC()
        {
            try
            {
                ComprasCL cl = new ComprasCL();
                cl.strSerie = "";
                cl.intFolio = Ordendecompra;
                gridControlDetalle.DataSource = cl.CargaOrdendeCompra();
                globalCL clg = new globalCL();
                                     
                clg.strGridLayout = "gridComprasDetalle";
                clg.restoreLayout(gridViewDetalle);
                popupContainerControlOC.Hide();

                if (gridViewDetalle.RowCount == 1)
                {
                    MessageBox.Show("La OC " + txtOCfolio.Text + " no existe o no se ha autorizado");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewOC_RowClick(object sender, RowClickEventArgs e)
        {
            Ordendecompra = Convert.ToInt32(gridViewOC.GetRowCellValue(gridViewOC.FocusedRowHandle, "Folio"));
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            globalCL clg = new globalCL();
            if (!clg.esNumerico(txtOCfolio.Text))
            {
                MessageBox.Show("Teclee el folio de la OC");
                return;
            }
            Ordendecompra = Convert.ToInt32(txtOCfolio.Text);
            cargaOC();
        }

        private void bbiPolizaTesk_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
               

                popupTesk.Visible = true;
                groupControl7.Text = "Asignar póliza a el folio:" + strSerie + intFolio.ToString();
                txtLoginTesk.Focus();
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (txtPolizaTesk.Text.Length == 0)
            {
                MessageBox.Show("Debe capturar la póliza");
                return;
            }
            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLoginTesk.Text;
            clU.strClave = txtPasswordTesk.Text;
            clU.strPermiso = "asignarPolizaTesk";
            string result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }
            intUsuariocancelo = clU.intUsuariosID;
            AsignaPoliza();
            CierraPopUpTesk();
            
        }

        private void AsignaPoliza()
        {

            

            ComprasCL cl = new ComprasCL();
            cl.strTabla = "Compras";
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            cl.teskPoliza = txtPolizaTesk.Text;

            string result = cl.AsignaPolizaTesk();
            MessageBox.Show(result);
        }
    }




    #endregion




}

