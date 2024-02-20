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
using VisualSoftErp.Operacion.Compras.Clases;
using System.Configuration;
using VisualSoftErp.Operacion.Compras.Designers;

namespace VisualSoftErp.Catalogos
{

    public partial class Ordenesdecompras : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        public BindingList<detalleCL> detalle;
        string strSerie=string.Empty;
        int intFolio=0;
        decimal dIvaPorcentaje;

        public Ordenesdecompras()
        {
            InitializeComponent();

            //txtSerie.Properties.MaxLength = 10;
            //txtSerie.EnterMoveNextControl = true;
            dateEditFecha.Text = DateTime.Now.ToShortDateString();
           memoObservaciones.Properties.MaxLength = 250;
            memoObservaciones.EnterMoveNextControl = true;
            //txtMailto.Properties.MaxLength = 100;
            //txtMailto.EnterMoveNextControl = true;
            txtAtencionA.Properties.MaxLength = 100;
            txtAtencionA.EnterMoveNextControl = true;
            txtCondiciones.Properties.MaxLength = 50;
            txtCondiciones.EnterMoveNextControl = true;
            txtLab.Properties.MaxLength = 50;
            txtLab.EnterMoveNextControl = true;
            //txtMonedasID.Properties.MaxLength = 3;
            //txtMonedasID.EnterMoveNextControl = true;

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
            gridViewPrincipal.ViewCaption = "Catálogo de Ordenesdecompras";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void LlenarGrid()
        {
            OrdenesdecomprasCL cl = new OrdenesdecomprasCL();
            gridControlPrincipal.DataSource = cl.OrdenesdecomprasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridOrdenesdecompras";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        public class detalleCL
        {
            public string Serie { get; set; }
            public string Folio { get; set; }
            public string Seq { get; set; }
            public string Articulos { get; set; }
            public string UM { get; set; }
            public string Descripcion { get; set; }
            public string Cantidad { get; set; }
            public string Precio { get; set; }
            public string Importe { get; set; }
            public string Iva { get; set; }
            public string Ieps { get; set; }
            public string Cantidadrecibida { get; set; }
            public string CompraSerie { get; set; }
            public string CompraFolio { get; set; }
            public string CompraSeq { get; set; }
            public string Fechaembarque { get; set; }
            public string Fecharecepcion { get; set; }
            public string Piva { get; set; }
            public string Pieps { get; set; }
            public string Fechadellegada { get; set; }
            public string ReqSerie { get; set; }
            public string ReqFolio { get; set; }
            public string ReqSeq { get; set; }
            public string Cantidaddepurada { get; set; }
            public string Neto { get; set; }
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
            List<ClaseGenricaCL> ClaseVia = new List<ClaseGenricaCL>();
            ClaseVia.Add(new ClaseGenricaCL() { Clave = "1", Des = "Terrestre" });
            ClaseVia.Add(new ClaseGenricaCL() { Clave = "2", Des = "Area" });
            ClaseVia.Add(new ClaseGenricaCL() { Clave = "3", Des = "Maritima" });


            cl.strTabla = "Serie";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Des";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Clave"].Visible = false;

            cl.strTabla = "Proveedores";
            cboProveedor.Properties.ValueMember = "Clave";
            cboProveedor.Properties.DisplayMember = "Des";
            cboProveedor.Properties.DataSource = cl.CargaCombos();
            cboProveedor.Properties.ForceInitialize();
            cboProveedor.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedor.Properties.PopulateColumns();
            cboProveedor.Properties.Columns["Clave"].Visible = false;

            cl.strTabla = "Proveedores";
            cboEmbarcarA.Properties.ValueMember = "Clave";
            cboEmbarcarA.Properties.DisplayMember = "Des";
            cboEmbarcarA.Properties.DataSource = cl.CargaCombos();
            cboEmbarcarA.Properties.ForceInitialize();
            cboEmbarcarA.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboEmbarcarA.Properties.PopulateColumns();
            cboEmbarcarA.Properties.Columns["Clave"].Visible = false;

            cl.strTabla = "Proveedores";
            cboFacturarA.Properties.ValueMember = "Clave";
            cboFacturarA.Properties.DisplayMember = "Des";
            cboFacturarA.Properties.DataSource = cl.CargaCombos();
            cboFacturarA.Properties.ForceInitialize();
            cboFacturarA.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFacturarA.Properties.PopulateColumns();
            cboFacturarA.Properties.Columns["Clave"].Visible = false;
            cl.strTabla = "";

            cboEmbarcarVia.Properties.ValueMember = "Clave";
            cboEmbarcarVia.Properties.DisplayMember = "Des";
            cboEmbarcarVia.Properties.DataSource =ClaseVia;
            cboEmbarcarVia.Properties.ForceInitialize();
            cboEmbarcarVia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboEmbarcarVia.Properties.PopulateColumns();
            cboEmbarcarVia.Properties.Columns["Clave"].Visible = false;

            cl.strTabla = "Monedas";
            cboMoneda.Properties.ValueMember = "Clave";
            cboMoneda.Properties.DisplayMember = "Des";
            cboMoneda.Properties.DataSource = cl.CargaCombos();
            cboMoneda.Properties.ForceInitialize();
            cboMoneda.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMoneda.Properties.PopulateColumns();
            cboMoneda.Properties.Columns["Clave"].Visible = false;


            cl.strTabla = "Articulos";
            repositoryItemLookUpEditArticulos.ValueMember = "Clave";
            repositoryItemLookUpEditArticulos.DisplayMember = "Des";
            repositoryItemLookUpEditArticulos.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulos.ForceInitialize();
            repositoryItemLookUpEditArticulos.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulos.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
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
            intFolio = 0;
            SiguienteID();
        }

        private void SiguienteID()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            ComprasCL cl = new ComprasCL();
            cl.strSerie = serie;
            cl.strDoc = "Ordenesdecompras";

            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {
                cboSerie.EditValue = serie;
                strSerie = serie;
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
            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            cboProveedor.EditValue = null;
            memoObservaciones.Text = string.Empty;
            cboEmbarcarA.EditValue = null;
            cboFacturarA.EditValue = null;
            txtMailito.Text = string.Empty;
            txtAtencionA.Text = string.Empty;
            txtCondiciones.Text = string.Empty;
            txtLab.Text = string.Empty;
            cboEmbarcarVia.EditValue = null;
            cboMoneda.EditValue = string.Empty;
            txtTiempoentrega.Text = "0";
            txtCorrespondencia.Text = string.Empty;
            txtDiastraslado.Text = 0.ToString();
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
                System.Data.DataTable dtOrdenesdecompras = new System.Data.DataTable("Ordenesdecompras");
                dtOrdenesdecompras.Columns.Add("Serie", Type.GetType("System.String"));
                dtOrdenesdecompras.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtOrdenesdecompras.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtOrdenesdecompras.Columns.Add("ProveedoresID", Type.GetType("System.Int32"));
                dtOrdenesdecompras.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dtOrdenesdecompras.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtOrdenesdecompras.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtOrdenesdecompras.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dtOrdenesdecompras.Columns.Add("Status", Type.GetType("System.Int32"));
                dtOrdenesdecompras.Columns.Add("Fechacancelacion", Type.GetType("System.DateTime"));
                dtOrdenesdecompras.Columns.Add("Razoncancelacion", Type.GetType("System.String"));
                dtOrdenesdecompras.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtOrdenesdecompras.Columns.Add("Observaciones", Type.GetType("System.String"));
                dtOrdenesdecompras.Columns.Add("Embarcara", Type.GetType("System.Int32"));
                dtOrdenesdecompras.Columns.Add("Facturara", Type.GetType("System.Int32"));
                dtOrdenesdecompras.Columns.Add("Mailto", Type.GetType("System.String"));
                dtOrdenesdecompras.Columns.Add("Atenciona", Type.GetType("System.String"));
                dtOrdenesdecompras.Columns.Add("Condiciones", Type.GetType("System.String"));
                dtOrdenesdecompras.Columns.Add("Lab", Type.GetType("System.String"));
                dtOrdenesdecompras.Columns.Add("Via", Type.GetType("System.Int32"));
                dtOrdenesdecompras.Columns.Add("MonedasID", Type.GetType("System.String"));
                dtOrdenesdecompras.Columns.Add("Tiempodeentrega", Type.GetType("System.Int32"));
                dtOrdenesdecompras.Columns.Add("Autorizadopor", Type.GetType("System.Int32"));
                dtOrdenesdecompras.Columns.Add("Maquinaautorizado", Type.GetType("System.String"));
                dtOrdenesdecompras.Columns.Add("Requisicion", Type.GetType("System.Int32"));
                dtOrdenesdecompras.Columns.Add("Depurada", Type.GetType("System.Int32"));
                dtOrdenesdecompras.Columns.Add("Fechadepurada", Type.GetType("System.DateTime"));
                dtOrdenesdecompras.Columns.Add("Usuariodepuro", Type.GetType("System.Int32"));
                dtOrdenesdecompras.Columns.Add("Correspondeciaa", Type.GetType("System.String"));
                dtOrdenesdecompras.Columns.Add("DiasTraslado", Type.GetType("System.Int32"));

                System.Data.DataTable dtOrdenesdecomprasDetalle = new System.Data.DataTable("OrdenesdecomprasDetalle");
                dtOrdenesdecomprasDetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtOrdenesdecomprasDetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtOrdenesdecomprasDetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtOrdenesdecomprasDetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtOrdenesdecomprasDetalle.Columns.Add("Descripcion", Type.GetType("System.String"));
                dtOrdenesdecomprasDetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtOrdenesdecomprasDetalle.Columns.Add("Precio", Type.GetType("System.Decimal"));
                dtOrdenesdecomprasDetalle.Columns.Add("Importe", Type.GetType("System.Decimal"));
                dtOrdenesdecomprasDetalle.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtOrdenesdecomprasDetalle.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtOrdenesdecomprasDetalle.Columns.Add("Cantidadrecibida", Type.GetType("System.Decimal"));
                dtOrdenesdecomprasDetalle.Columns.Add("CompraSerie", Type.GetType("System.String"));
                dtOrdenesdecomprasDetalle.Columns.Add("CompraFolio", Type.GetType("System.Int32"));
                dtOrdenesdecomprasDetalle.Columns.Add("CompraSeq", Type.GetType("System.Int32"));
                dtOrdenesdecomprasDetalle.Columns.Add("Fechaembarque", Type.GetType("System.DateTime"));
                dtOrdenesdecomprasDetalle.Columns.Add("Fecharecepcion", Type.GetType("System.DateTime"));
                dtOrdenesdecomprasDetalle.Columns.Add("Piva", Type.GetType("System.Decimal"));
                dtOrdenesdecomprasDetalle.Columns.Add("Pieps", Type.GetType("System.Decimal"));
                dtOrdenesdecomprasDetalle.Columns.Add("Fechadellegada", Type.GetType("System.DateTime"));
                dtOrdenesdecomprasDetalle.Columns.Add("ReqSerie", Type.GetType("System.String"));
                dtOrdenesdecomprasDetalle.Columns.Add("ReqFolio", Type.GetType("System.Int32"));
                dtOrdenesdecomprasDetalle.Columns.Add("ReqSeq", Type.GetType("System.Int32"));
                dtOrdenesdecomprasDetalle.Columns.Add("Cantidaddepurada", Type.GetType("System.Decimal"));
                dtOrdenesdecomprasDetalle.Columns.Add("Decimal", Type.GetType("System.Decimal"));
                dtOrdenesdecomprasDetalle.Columns.Add("UM", Type.GetType("System.String"));

                int intRen = 0;
                string dato = String.Empty;
                //string strSerie = String.Empty;
                //int intFolio = intFolio;
                int intSeq = 0;
                int intArticulosID = 0;
                string strDescripcion = String.Empty;
                decimal dCantidad = 0;
                decimal dPrecio = 0;
                decimal dImporte = 0;
                decimal dIva = 0;
                decimal dIeps = 0;
                decimal dCantidadrecibida = 0;
                string strCompraSerie = String.Empty;
                int intCompraFolio = 0;
                int intCompraSeq = 0;
                DateTime fFechaembarque;
                DateTime fFecharecepcion;
                decimal dPiva = 0;
                decimal dPieps = 0;
                DateTime fFechadellegada=DateTime.Now;
                string strReqSerie = String.Empty;
                int intReqFolio = 0;
                int intReqSeq = 0;
                decimal dCantidaddepurada = 0;
                decimal dSubtotalTot = 0;
                decimal dIvaTot = 0;
                decimal dIepsTot = 0;
                decimal dNetoTot = 0;
                decimal dNeto = 0;
                string UM = string.Empty;

                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "Cantidad").ToString();
                    if (dato.Length > 0)
                    {
                        intSeq = i;
                        intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Articulos"));
                        strDescripcion = gridViewDetalle.GetRowCellValue(i, "Descripcion").ToString();
                        dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Cantidad"));
                        dPrecio = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Precio"));
                        dImporte = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importe"));
                        dIva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Iva"));
                        dIeps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Ieps"));
                        fFechaembarque = Convert.ToDateTime(gridViewDetalle.GetRowCellValue(i, "Fechaembarque"));
                        fFecharecepcion = Convert.ToDateTime(gridViewDetalle.GetRowCellValue(i, "Fecharecepcion"));
                        dPiva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Piva"));
                        dPieps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Pieps"));
                        dNeto = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Neto"));
                        UM = gridViewDetalle.GetRowCellValue(i, "UM").ToString();

                        dtOrdenesdecomprasDetalle.Rows.Add(strSerie,intFolio,intSeq, intArticulosID, strDescripcion, dCantidad, dPrecio, dImporte, dIva, dIeps, dCantidadrecibida, strCompraSerie, intCompraFolio, intCompraSeq, fFechaembarque, fFecharecepcion, dPiva, dPieps, fFechadellegada, strReqSerie, intReqFolio, intReqSeq, dCantidaddepurada, dNeto,UM);

                        dSubtotalTot += dImporte;
                        dIvaTot += dIva;
                        dIepsTot += dIeps;
                       
                    }
                    dNetoTot = dSubtotalTot + dIvaTot;
                }
                strSerie =cboSerie.EditValue.ToString();
                intFolio = Convert.ToInt32(txtFolio.Text);
                DateTime fFecha = Convert.ToDateTime(dateEditFecha.Text);
                int intProveedoresID = Convert.ToInt32(cboProveedor.EditValue);
           
                int intStatus = 1;
                DateTime fFechacancelacion = DateTime.Now;
                string strRazoncancelacion = string.Empty;
                int intUsuariosID = 1;
                string strObservaciones = memoObservaciones.Text;
                int intEmbarcara = Convert.ToInt32(cboEmbarcarA.EditValue);
                int intFacturara = Convert.ToInt32(cboFacturarA.EditValue);
                string strMailto = txtMailito.Text;
                string strAtenciona = txtAtencionA.Text;
                string strCondiciones = txtCondiciones.Text;
                string strLab = txtLab.Text;
                int intVia = Convert.ToInt32(cboEmbarcarVia.EditValue);
                string strMonedasID=cboMoneda.EditValue.ToString();
                int intTiempodeentrega = Convert.ToInt32(txtTiempoentrega.Text);
                int intAutorizadopor = 0;
                string strMaquinaautorizado = string.Empty;
                int intRequisicion = 0;
                int intDepurada = 0;
                DateTime fFechadepurada = DateTime.Now;
                int intUsuariodepuro = 0;
                dato = String.Empty;
                string strCorrespondencia = txtCorrespondencia.Text;
                int intDiasTraslado = Convert.ToInt32(txtDiastraslado.Text);
                dtOrdenesdecompras.Rows.Add(strSerie, intFolio, fFecha, intProveedoresID, dSubtotalTot, dIvaTot, dIepsTot, dNetoTot, intStatus, fFechacancelacion, strRazoncancelacion, intUsuariosID, strObservaciones, intEmbarcara, intFacturara, txtMailito.Text, strAtenciona, strCondiciones, strLab, intVia, strMonedasID, intTiempodeentrega, 0, Environment.MachineName, 0, 0, DateTime.Now, 0,strCorrespondencia,intDiasTraslado);

                OrdenesdecomprasCL cl = new OrdenesdecomprasCL();
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.strMaquina = Environment.MachineName;
                cl.intUsuarioID = 1;
                cl.strPrograma = "0120";
                cl.dtm = dtOrdenesdecompras;
                cl.dtd = dtOrdenesdecomprasDetalle;
                Result = cl.OrdenesdecomprasCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    LimpiaCajas();
                    SiguienteID();
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
            if (cboProveedor.EditValue == null)
            {
                return "El campo ProveedoresID no puede ir vacio";
            }
            if (dateEditFecha.EditValue == null)
            {
                return "El campo fecha no puede ir vacio";
            }
            if (txtMailito.Text.Length == 0)
            {
                txtMailito.Text = string.Empty;
            }
            if (memoObservaciones.Text.Length == 0)
            {
                memoObservaciones.Text = string.Empty;
            }
            if (cboEmbarcarA.EditValue == null)
            {
                return "El campo Embarcar a no puede ir vacio";
            }
            if (cboFacturarA.EditValue == null)
            {
                return "El campo Facturar a, no puede ir vacio";
            }
            if (txtAtencionA.Text.Length == 0)
            {
                txtAtencionA.Text = string.Empty;
            }
            if (txtCondiciones.Text.Length == 0)
            {
                txtCondiciones.Text = string.Empty;
            }
            if (txtLab.Text.Length == 0)
            {
                txtCondiciones.Text = string.Empty;
            }
            if (txtDiastraslado.Text.Length == 0)
            {
                txtDiastraslado.Text = 0.ToString();
            }
            if (txtCorrespondencia.Text.Length == 0)
            {
                txtCorrespondencia.Text = string.Empty;
            }

            return "OK";
        } //Valida

        private void llenaCajas()
        {
            OrdenesdecomprasCL cl = new OrdenesdecomprasCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            String Result = cl.OrdenesdecomprasLlenaCajas();
            if (Result == "OK")
            {
                dateEditFecha.Text = cl.fFecha.ToShortDateString();
                cboProveedor.EditValue = cl.intProveedoresID;
                cboSerie.EditValue = cl.strSerie;
                txtFolio.Text = cl.intFolio.ToString();
                memoObservaciones.Text = cl.strObservaciones;
                cboEmbarcarA.EditValue = cl.intEmbarcara;
                cboFacturarA.EditValue = cl.intFacturara;
                cboProveedor.EditValue = cl.intProveedoresID;
                txtMailito.Text = cl.strMailto;
                txtAtencionA.Text = cl.strAtenciona;
                txtCondiciones.Text = cl.strCondiciones;
                txtLab.Text = cl.strLab;
                cboEmbarcarVia.EditValue = cl.intVia.ToString();
                cboMoneda.EditValue = cl.strMonedasID;
                txtTiempoentrega.Text = cl.intTiempodeentrega.ToString();
                txtCondiciones.Text = cl.strCondiciones;
                txtCorrespondencia.Text = cl.CorrespondenciaA;
                txtDiastraslado.Text =cl.DiasTraslado.ToString();
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
                OrdenesdecomprasCL cl = new OrdenesdecomprasCL();
                cl.intFolio = intFolio;
                cl.strSerie = strSerie;
                gridControlDetalle.DataSource = cl.OrdebesdecomprasDetalleGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }

        }//DetalleLlenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (strSerie.Length > 0)
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
            OrdenesdecomprasCL cl = new OrdenesdecomprasCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            String Result = cl.OrdenesdecomprasEliminar();
            if (Result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (strSerie.Length > 0)
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
            clg.strGridLayout = "gridOrdenesdecompras";
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

        private void gridViewPrincipal_RowClick(Object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            strSerie = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie").ToString();
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //Sacamos datos del artículo
            if (e.Column.Name == "gridColumnArticulos")
            {
                articulosCL cl = new articulosCL();
                string art = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulos").ToString();
                if (art.Length > 0) //validamos que haya algo en la celda
                {
                    cl.intArticulosID = Convert.ToInt32(art);
                    string result = cl.articulosLlenaCajas();
                    if (result == "OK")
                    {
                        gridViewDetalle.SetFocusedRowCellValue("Descripcion", cl.strNombre);
                        gridViewDetalle.SetFocusedRowCellValue("UM", cl.strUM);
                        gridViewDetalle.SetFocusedRowCellValue("Pieps",cl.intPtjeIeps);
                        gridViewDetalle.SetFocusedRowCellValue("Piva", cl.intPtjeIva);

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
                decimal ieps = 0;
                decimal pieps = 0;

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

                pieps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Pieps"));
                ieps = imp * (pieps / 100);

                gridViewDetalle.SetFocusedRowCellValue("Importe", imp); //Le ponemos el valor a la celda Importe del grid
                gridViewDetalle.SetFocusedRowCellValue("Iva", iva); //Le ponemos el valor a la celda Iva del grid
                gridViewDetalle.SetFocusedRowCellValue("Neto", iva + imp); //Le ponemos el valor a la celda neto del grid
                gridViewDetalle.SetFocusedRowCellValue("Ieps", ieps);

                //Descuento = Importe * (%Descto/100)
                //Ieps = (Importe - Descuento) * (%Ieps / 100)

            }
        }

        private void gridViewPrincipal_DoubleClick(object sender, EventArgs e)
        {
            strSerie = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie").ToString();
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));

            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void cboProveedor_EditValueChanged(object sender, EventArgs e)
        {
            ComprasCL cl = new ComprasCL();
            cl.intProveedoresID = Convert.ToInt32(cboProveedor.EditValue);
            string strResult = cl.ObtenerPlazoProveedor();

            if (strResult == "OK") txtTiempoentrega.Text = cl.intPlazo.ToString();
        }

        private void reporte()
        {
            try
            {
                OrdenesdecompraFormatoImpresion report = new OrdenesdecompraFormatoImpresion();
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
                    ribbonPage.Visible = false;
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

        private void bbiRegresarimpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPageIndex = 0;
            navBarControl.Visible = true;
            ribbonStatusBar.Visible = true;
            ribbonPage.Visible = true;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText("Home");
            RibbonPagePrint.Visible = false;
            intFolio = 0;
            strSerie = string.Empty;
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

        private void navBarItemAbril_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
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

        private void navBarItemTodos_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilter.Clear();
        }
    }
}
