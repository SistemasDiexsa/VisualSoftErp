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

namespace VisualSoftErp.Catalogos
{

    public partial class Compras : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        public BindingList<detalleCL> detalle;
        string strSerie;
        int intFolio;
        decimal dIvaPorcentaje;

        public Compras()
        {
            InitializeComponent();

            //txtSerie.Properties.MaxLength = 10;
            //txtSerie.EnterMoveNextControl = true;
            dateFecha.Text = DateTime.Now.ToShortDateString();
            txtOCfolio.Properties.MaxLength = 50;
            txtOCfolio.EnterMoveNextControl = true;
            //txtMonedasID.Properties.MaxLength = 3;
            //txtMonedasID.EnterMoveNextControl = true;
            //txtFactura.Properties.MaxLength = 50;
            //txtFactura.EnterMoveNextControl = true;
            //dateEditfechafactura.Text = DateTime.Now.ToShortDateString();

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            //------------- Inicializar aquí opciones de columnas del grid ----------------
            //gridColumnIva.OptionsColumn.ReadOnly = true;
            //gridColumnIva.OptionsColumn.AllowFocus = false;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Compras";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void LlenarGrid()
        {
            ComprasCL cl = new ComprasCL();
            gridControlPrincipal.DataSource = cl.ComprasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCompras";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        public class detalleCL
        {
            public string Cantidad { get; set; }
            public string Articulo { get; set; }
            public string Precio { get; set; }
            public string Importe { get; set; }
            public string Iva { get; set; }
            public string Neto { get; set; }
            public string Ieps { get; set; }
            public string Costoum2 { get; set; }
            public string Factorum2 { get; set; }
            public string Piva { get; set; }
            public string Pieps { get; set; }
            public string Descuento { get; set; }
            public string Pdescuento { get; set; }
            public string OCSerie { get; set; }
            public string OCNumero { get; set; }
            public string OCSeq { get; set; }
        }
        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
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
            cboSerie.ItemIndex = 1;

            cl.strTabla = "Proveedores";
            cboProveedores.Properties.ValueMember = "Clave";
            cboProveedores.Properties.DisplayMember = "Des";
            cboProveedores.Properties.DataSource = cl.CargaCombos();
            cboProveedores.Properties.ForceInitialize();
            cboProveedores.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedores.Properties.PopulateColumns();
            cboProveedores.Properties.Columns["Clave"].Visible = false;
            cl.strTabla = "Monedas";

            cboMoneda.Properties.ValueMember = "Clave";
            cboMoneda.Properties.DisplayMember = "Des";
            cboMoneda.Properties.DataSource = cl.CargaCombos();
            cboMoneda.Properties.ForceInitialize();
            cboMoneda.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMoneda.Properties.PopulateColumns();
            cboMoneda.Properties.Columns["Clave"].Visible = false;


            cl.strTabla = "Articulos";
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
            Inicialisalista();
            BotonesEdicion();
            strSerie = string.Empty;
            SiguienteID();
        }

        private void SiguienteID()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

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
            cboSerie.EditValue = string.Empty;
            dateFecha.Text = DateTime.Now.ToShortDateString();
            txtOCfolio.Text = string.Empty;
            cboProveedores.EditValue = null;
            cboMoneda.EditValue=string.Empty;
            txtFactura.Text = string.Empty;
            dateFechafactura.Text = DateTime.Now.ToShortDateString();
            txtTipocambio.Text = null;
            txtPlazo.Text = "0";
            txtOcSerie.Text = string.Empty;
            //gridControlDetalle.DataSource= detalle;
        }

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
                int intOCNumero = 0;
                int intOCSeq = 0;
                int intSeq = 0;

                decimal decTotSubtotal = 0;
                decimal decTotIva = 0;
                decimal decTotIeps = 0;
                decimal decTotNeto = 0;
                decimal decTotDescuento = 0;
                decimal decNetogrid = 0;

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
                        decNetogrid= Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Neto"));
                        dDescuento= Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Descuento"));

                        dtComprasdetalle.Rows.Add(cboSerie.EditValue.ToString(),Convert.ToInt32(txtFolio.Text), intSeq,dCantidad, intArticulosID, dPrecio, dimporte, dIva, dIeps, 0, 0, dPIva, dPIeps, dDescuento, dPDescuento, string.Empty, 0, 0,decNetogrid);

                       
                        decTotSubtotal += dimporte;
                        decTotIva += dIva;
                        decTotIeps += dIeps;
                        decTotDescuento += dDescuento;
                       

                    }

                    decTotNeto += decTotSubtotal - decTotIva;
                }
                string strSerie=cboSerie.EditValue.ToString();
                int intFolio = Convert.ToInt32(txtFolio.Text);
                DateTime fFecha = Convert.ToDateTime(dateFecha.Text);
                strOCSerie = txtOcSerie.Text;
                int intOCFolio = Convert.ToInt32(txtOCfolio.Text);
                int intProveedoresID = Convert.ToInt32(cboProveedores.EditValue);
                string strMonedas=cboMoneda.EditValue.ToString();
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
                dtCompras.Rows.Add(strSerie, intFolio, fFecha, strOCSerie, intOCFolio, intProveedoresID, strMonedas, strFactura, ffechafactura, dtipodecambio, decSubtotal, decIva, decIeps, decNeto, intStatus, intPlazo, fFechacancelacion, strRazoncancelacion, decDescuento, intPoliza, intNodeducible, strContrarecibosSerie, intContrarecibosFolio, strRecepcionSerie, intRecepcionFolio, intValidadoPor, fFechaValidado, fFechaReal,strObservaciones);

                ComprasCL cl = new ComprasCL();
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.strMaquina = Environment.MachineName;
                cl.intUsuarioID = 1;
                cl.strPrograma = "0121";
                cl.dtm = dtCompras;
                cl.dtd = dtComprasdetalle;
                Result = cl.ComprasCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    LimpiaCajas();
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
            if (cboSerie.EditValue == null)
            {
                return "El campo Serie no puede ir vacio";
            }
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
                txtFactura.Text = cl.strFactura;
                dateFechafactura.Text = cl.ffechafactura.ToShortDateString();
                txtTipocambio.Text = cl.dectipodecambio.ToString();
                cboSerie.EditValue = strSerie;
                txtFolio.Text = intFolio.ToString();
                
                txtPlazo.Text = cl.intPlazo.ToString();
               
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
            if (strSerie.Length > 0)
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
        }  //Editar  
               

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();
            DetalleLlenaCajas();
        }

        private void BotonesEdicion()
        {
            LimpiaCajas();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            navigationFrame.SelectedPageIndex = 1;
        }

        private void Eliminar()
        {
            ComprasCL cl = new ComprasCL(); 
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            String Result = cl.ComprasEliminar();
            if (Result == "OK")
            {
                MessageBox.Show("Cancelado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (strSerie.Length == 0)
                if (intFolio == 0)
                {
                    MessageBox.Show("Selecciona un renglón");
                }
                else
                {
                    DialogResult Result = MessageBox.Show("Desea eliminar el ID " + strSerie.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                    if (Result.ToString() == "Yes")
                    {
                        Eliminar();
                    }
                }
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
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
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
            strSerie = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie").ToString();
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (strSerie.Length > 0)
            {
                if (intFolio == 0)
                {
                    MessageBox.Show("Selecciona un renglón");
                }
                else
                {
                    DialogResult Result = MessageBox.Show("Desea cancelar la Compra " + strSerie.ToString() + intFolio.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                    if (Result.ToString() == "Yes")
                    {
                        Eliminar();
                    }
                }
            }
            
        }

        private void cboProveedores_EditValueChanged(object sender, EventArgs e)
        {
            ComprasCL cl = new ComprasCL();
            cl.intProveedoresID = Convert.ToInt32(cboProveedores.EditValue);
            string strResult = cl.ObtenerPlazoProveedor();

            if (strResult == "OK") txtPlazo.Text = cl.intPlazo.ToString();
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
            if(txtDescuento.Text.Length==0) MessageBox.Show("Capture un descuento");

            decimal PDescuento = 0;
            decimal dimporte = 0;
            decimal descuento = 0;
            PDescuento = Convert.ToDecimal(txtDescuento.Text);

            for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
            {
                gridViewDetalle.SetRowCellValue(i,"Pdescuento", PDescuento);
                dimporte = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importe"));
                descuento = dimporte * (PDescuento / 100);
                gridViewDetalle.SetRowCellValue(i, "Descuento", descuento);
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
                        gridViewDetalle.SetFocusedRowCellValue("Piva", cl.intPtjeIva);
                        gridViewDetalle.SetFocusedRowCellValue("Pieps", cl.intPtjeIeps);
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
                iva = Math.Round(imp * (dIvaPorcentaje / 100), 2);

                gridViewDetalle.SetFocusedRowCellValue("Importe", imp); //Le ponemos el valor a la celda Importe del grid
                gridViewDetalle.SetFocusedRowCellValue("Iva", iva); //Le ponemos el valor a la celda Iva del grid
                gridViewDetalle.SetFocusedRowCellValue("Neto", iva + imp); //Le ponemos el valor a la celda neto del grid

                //Descuento = Importe * (%Descto/100)
                //Ieps = (Importe - Descuento) * (%Ieps / 100)

            }

            //Calculamos el descuento
            if (e.Column.Name == "gridColumnPdescuento")
            {
                decimal descuento = 0;
                decimal pdescuento=0;
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
                txtTipocambio.Text = 1.ToString();
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
    }
}
