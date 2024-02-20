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
using VisualSoftErp.Clases.HerrramientasCLs;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraNavBar;

namespace VisualSoftErp.Catalogos
{

    public partial class Ordenesdecompras : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        public BindingList<detalleCL> detalle;
        string strSerie=string.Empty;
        int intFolio=0;
        decimal dIvaPorcentaje;
        bool blnNuevo;
        int tiempodeSurtido;
        int tiempoTraslado;
        int diasTraslado;
        int EmbarcarA;
        int FacturarA;
        int intManejaIva;
        int intManejaIeps;
        decimal pIvaProv;
        string Atenciona;
        string Correspondenciaa;
        int UltimosPreciosOC;
        bool Autorizando;
        bool Desautorizando;
        decimal OCmontominimo;
        string Lab;
        int Via;
        string strCancelando;
        string sOrigenImpresion = string.Empty;
        int AutorizadoPor;        
        int Pais;
        string status = string.Empty;
        string strPais = string.Empty;
        int AñoFiltro;
        int MesFiltro;
        bool Editando;
        int intNuevaFechaseq;


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

            MedioAmbiente();

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Ordenes de compras";

            AgregaAñosNavBar();
            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;

            LlenarGrid(AñoFiltro,MesFiltro);
            gridViewPrincipal.ActiveFilter.Clear();

            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

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
            catch (Exception ex)
            {
                MessageBox.Show("AgregaAñosNavBar:" + ex.Message);
            }
        }

        private void MedioAmbiente()
        {
            DatosdecontrolCL cl = new DatosdecontrolCL();
            cl.DatosdecontrolLeer();

            if (cl.iManejariva == 0)
            {
                gridColumnPiva.Visible = false;
                intManejaIva = 0;
            }
            else
            {
                intManejaIva = 1;
            }
            if (cl.iManejarieps == 0)
            {
                gridColumnPieps.Visible = false;
                intManejaIeps = 0;
            }
            else
            {
                intManejaIeps = 1;
            }

            EmbarcarA = cl.iEmbarcara;
            FacturarA = cl.iFacturara;
            Atenciona = cl.sAtenciona;
            Correspondenciaa = cl.sCorrespondenciaa;
            OCmontominimo = cl.decOCmontominio;
        }

        private void LlenarGrid(int año,int mes)
        {
            OrdenesdecomprasCL cl = new OrdenesdecomprasCL();
            cl.intAño = AñoFiltro;
            cl.intMes = MesFiltro;
            gridControlPrincipal.DataSource = cl.OrdenesdecomprasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridOrdenesdecompras";
            clg.restoreLayout(gridViewPrincipal);

            gridViewPrincipal.ViewCaption = "Ordenes de compra de " + clg.NombreDeMes(MesFiltro, 0) + " del " + AñoFiltro.ToString();
            //con esta lina de codigo ponemos los autofiltros
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
        } //LlenarGrid()

        public class detalleCL
        {
            public string Serie { get; set; }
            public string Folio { get; set; }
            public string Seq { get; set; }
            public string Articulos { get; set; }
            public string UM { get; set; }
            public string Descripcion { get; set; }
            public decimal Cantidad { get; set; }
            public decimal Precio { get; set; }
            public decimal Importe { get; set; }
            public decimal Iva { get; set; }
            public decimal Ieps { get; set; }
            public decimal Cantidadrecibida { get; set; }
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
            public decimal Neto { get; set; }
        }
        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;

            gridColumnUM.OptionsColumn.ReadOnly = true;
            gridColumnUM.OptionsColumn.AllowFocus = false;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridOrdenesdecomprasdetalle";
            clg.restoreLayout(gridViewDetalle);

            if (intManejaIeps==0)
            {
                gridColumnIeps.Visible = false;
                gridColumnPieps.Visible = false;
            }
            if (intManejaIva == 0)
            {
                gridColumnIva.Visible = false;
                gridColumnPiva.Visible = false;
            }

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
            cboSerie.ItemIndex = 0;

            cl.strTabla = "Proveedores";
            cboProveedor.Properties.ValueMember = "Clave";
            cboProveedor.Properties.DisplayMember = "Des";
            cboProveedor.Properties.DataSource = cl.CargaCombos();
            cboProveedor.Properties.ForceInitialize();
            cboProveedor.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedor.Properties.PopulateColumns();
            cboProveedor.Properties.Columns["Clave"].Visible = false;
            cboProveedor.Properties.Columns["Piva"].Visible = false;
            cboProveedor.Properties.Columns["Tiempodeentrega"].Visible = false;
            cboProveedor.Properties.Columns["Diasdetraslado"].Visible = false;
            cboProveedor.Properties.Columns["Lab"].Visible = false;
            cboProveedor.Properties.Columns["Via"].Visible = false;
            cboProveedor.Properties.Columns["Plazo"].Visible = false;
            cboProveedor.Properties.Columns["BancosID"].Visible = false;
            cboProveedor.Properties.Columns["Cuentabancaria"].Visible = false;
            cboProveedor.Properties.Columns["C_Formapago"].Visible = false;
            cboProveedor.Properties.Columns["MonedasID"].Visible = false;
            cboProveedor.Properties.Columns["Retiva"].Visible = false;
            cboProveedor.Properties.Columns["Retisr"].Visible = false;
            cboProveedor.Properties.Columns["Contacto"].Visible = false;
            cboProveedor.Properties.Columns["MonedasID"].Visible = false;                       
            cboProveedor.ItemIndex = 0;

            cl.strTabla = "Proveedores";
            cboEmbarcarA.Properties.ValueMember = "Clave";
            cboEmbarcarA.Properties.DisplayMember = "Des";
            cboEmbarcarA.Properties.DataSource = cl.CargaCombos();
            cboEmbarcarA.Properties.ForceInitialize();
            cboEmbarcarA.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboEmbarcarA.Properties.PopulateColumns();
            cboEmbarcarA.Properties.Columns["Clave"].Visible = false;
            cboEmbarcarA.Properties.Columns["Piva"].Visible = false;
            cboEmbarcarA.Properties.Columns["Tiempodeentrega"].Visible = false;
            cboEmbarcarA.Properties.Columns["Diasdetraslado"].Visible = false;
            cboEmbarcarA.Properties.Columns["Plazo"].Visible = false;
            cboEmbarcarA.Properties.Columns["Lab"].Visible = false;
            cboEmbarcarA.Properties.Columns["Via"].Visible = false;
            cboEmbarcarA.Properties.Columns["BancosID"].Visible = false;
            cboEmbarcarA.Properties.Columns["Cuentabancaria"].Visible = false;
            cboEmbarcarA.Properties.Columns["C_Formapago"].Visible = false;
            cboEmbarcarA.Properties.Columns["MonedasID"].Visible = false;
            cboEmbarcarA.Properties.Columns["Retiva"].Visible = false;
            cboEmbarcarA.Properties.Columns["Retisr"].Visible = false;
            cboEmbarcarA.Properties.Columns["Contacto"].Visible = false;
            cboEmbarcarA.ItemIndex = 0;

            cl.strTabla = "Proveedores";
            cboFacturarA.Properties.ValueMember = "Clave";
            cboFacturarA.Properties.DisplayMember = "Des";
            cboFacturarA.Properties.DataSource = cl.CargaCombos();
            cboFacturarA.Properties.ForceInitialize();
            cboFacturarA.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFacturarA.Properties.PopulateColumns();
            cboFacturarA.Properties.Columns["Clave"].Visible = false;
            cboFacturarA.Properties.Columns["Piva"].Visible = false;
            cboFacturarA.Properties.Columns["Tiempodeentrega"].Visible = false;
            cboFacturarA.Properties.Columns["Diasdetraslado"].Visible = false;
            cboFacturarA.Properties.Columns["Plazo"].Visible = false;
            cboFacturarA.Properties.Columns["Lab"].Visible = false;
            cboFacturarA.Properties.Columns["Via"].Visible = false;
            cboFacturarA.Properties.Columns["BancosID"].Visible = false;
            cboFacturarA.Properties.Columns["Cuentabancaria"].Visible = false;
            cboFacturarA.Properties.Columns["C_Formapago"].Visible = false;
            cboFacturarA.Properties.Columns["MonedasID"].Visible = false;
            cboFacturarA.Properties.Columns["Retiva"].Visible = false;
            cboFacturarA.Properties.Columns["Retisr"].Visible = false;
            cboFacturarA.Properties.Columns["Contacto"].Visible = false;
            cboFacturarA.ItemIndex = 0;

            cl.strTabla = "";
            cboEmbarcarVia.Properties.ValueMember = "Clave";
            cboEmbarcarVia.Properties.DisplayMember = "Des";
            cboEmbarcarVia.Properties.DataSource =ClaseVia;
            cboEmbarcarVia.Properties.ForceInitialize();
            cboEmbarcarVia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboEmbarcarVia.Properties.PopulateColumns();
            cboEmbarcarVia.Properties.Columns["Clave"].Visible = false;
            cboEmbarcarVia.ItemIndex = 0;

            cl.strTabla = "Monedas";
            cboMoneda.Properties.ValueMember = "Clave";
            cboMoneda.Properties.DisplayMember = "Des";
            cboMoneda.Properties.DataSource = cl.CargaCombos();
            cboMoneda.Properties.ForceInitialize();
            cboMoneda.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMoneda.Properties.PopulateColumns();
            cboMoneda.Properties.Columns["Clave"].Visible = false;
            cboMoneda.ItemIndex = 0;
            
            cl.strTabla = "Articulos";
            repositoryItemLookUpEditArticulos.ValueMember = "Clave";
            repositoryItemLookUpEditArticulos.DisplayMember = "Des";
            repositoryItemLookUpEditArticulos.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulos.ForceInitialize();
            repositoryItemLookUpEditArticulos.PopulateColumns();           
            repositoryItemLookUpEditArticulos.Columns["Clave"].Visible = false;           
            repositoryItemLookUpEditArticulos.Columns["FactorUM2"].Visible = false;
            
            repositoryItemLookUpEditArticulos.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulos.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;            
            //repositoryItemLookUpEditArticulos.Columns["NombreOC"].Visible = false;
        }
       

        private void Nuevo()
        {
            Editando = false;
            Inicialisalista();
            BotonesEdicion();
            strSerie = string.Empty;
            intFolio = 0;
            bbiDepurar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            SiguienteID();

            cboEmbarcarA.EditValue = EmbarcarA;
            cboFacturarA.EditValue = FacturarA;

            lblStatus.Text = "Registrando";
            lblAutorizada.Text = string.Empty;

            cboProveedor.Focus();
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
            cboProveedor.ItemIndex = 0;
            memoObservaciones.Text = string.Empty;
            cboEmbarcarA.EditValue = EmbarcarA;
            cboFacturarA.EditValue = FacturarA;
            txtMailito.Text = string.Empty;
            txtAtencionA.Text = Atenciona;            
            txtLab.Text = string.Empty;
            cboEmbarcarVia.ItemIndex=0;
            cboMoneda.ItemIndex=0;
            txtTiempoentrega.Text = "0";
            txtCorrespondencia.Text = Correspondenciaa;
            txtDiastraslado.Text = 0.ToString();
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

                //se vuelve a llamar
                if (blnNuevo)
                    SiguienteID();

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

                for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++)
                {
                    if (gridViewDetalle.GetRowCellValue(i, "Cantidad") != null)
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

                            dtOrdenesdecomprasDetalle.Rows.Add(strSerie, intFolio, intSeq, intArticulosID, strDescripcion, dCantidad, dPrecio, dImporte, dIva, dIeps, dCantidadrecibida, strCompraSerie, intCompraFolio, intCompraSeq, fFechaembarque, fFecharecepcion, dPiva, dPieps, fFechadellegada, strReqSerie, intReqFolio, intReqSeq, dCantidaddepurada, dNeto, UM);

                            dSubtotalTot += dImporte;
                            dIvaTot += dIva;
                            dIepsTot += dIeps;

                        }
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

                DatosdecontrolCL cld = new DatosdecontrolCL();
                Result=cld.DatosdecontrolLeer();

                if (Result == "OK")
                {
                    if (cboMoneda.EditValue.ToString() == "MXN")
                    {
                        if (dNetoTot <= cld.decOCmontominio)
                            intAutorizadopor = globalCL.gv_UsuarioID;
                    }
                    else
                    {
                        //USD
                        decimal decTC = 0;

                        tiposdecambioCL clt = new tiposdecambioCL();
                        clt.strMoneda = cboMoneda.EditValue.ToString();
                        clt.fFecha = Convert.ToDateTime(dateEditFecha.Text);
                        string result = clt.tiposdecambioLlenaCajas();
                        if (result == "OK")
                        {

                            decTC = Convert.ToDecimal(clt.dParidad);

                            decimal totmn = Math.Round(decTC * dNetoTot, 2);
                            if (totmn <= cld.decOCmontominio)
                                intAutorizadopor = globalCL.gv_UsuarioID;    //Si no hay tipo de cambio capturado no se autoriza
                        }

                        

                    }
                    
                }

                AutorizadoPor = intAutorizadopor;

                dtOrdenesdecompras.Rows.Add(strSerie, intFolio, fFecha, intProveedoresID, dSubtotalTot, dIvaTot, dIepsTot, dNetoTot, intStatus, 
                    fFechacancelacion, strRazoncancelacion, intUsuariosID, strObservaciones, intEmbarcara, intFacturara, txtMailito.Text, 
                    strAtenciona, strCondiciones, strLab, intVia, strMonedasID, intTiempodeentrega, intAutorizadopor, Environment.MachineName, 0, 0, 
                    DateTime.Now, 0,strCorrespondencia,intDiasTraslado);

                OrdenesdecomprasCL cl = new OrdenesdecomprasCL();
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.strMaquina = Environment.MachineName;
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                cl.strPrograma = "0120";
                cl.dtm = dtOrdenesdecompras;
                cl.dtd = dtOrdenesdecomprasDetalle;
                Result = cl.OrdenesdecomprasCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    sOrigenImpresion = "Guardar";
                    Imprimir();
                    LimpiaCajas();
                    Inicialisalista();

                    if (blnNuevo)
                    {
                        SiguienteID();
                    }
                   
                    cboProveedor.Focus();
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

            try
            {
                //Quitar ya no se usara por que no validamos serie 
                //if (cboSerie.EditValue == null)
                //{
                //    return "El campo Serie no puede ir vacio";
                //}
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
                //Evaluamos que cantidad e importes no se vallan vacios
                string dato = string.Empty;
                int intArticulosID = 0;
                decimal dCantidad = 0;
                decimal dPrecio = 0;
                decimal dImporte = 0;

                if (gridViewDetalle.RowCount-1==0)
                {
                    return "Debe capturar al menos un renglón";
                }

                for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++)
                {
                    if (gridViewDetalle.GetRowCellValue(i, "Cantidad") != null)
                    {
                        dato = gridViewDetalle.GetRowCellValue(i, "Cantidad").ToString();
                        if (dato.Length > 0)
                        {

                            intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Articulos"));
                            dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Cantidad"));
                            dPrecio = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Precio"));
                            dImporte = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Importe"));

                            if (intArticulosID == 0)
                            {
                                return "El artículo no puede estar vacio";
                            }
                            if (dCantidad == 0)
                            {
                                return "La cantidad no puede estar vacio";

                            }
                            if (dPrecio == 0)
                            {
                                return "El precio no puede estar vacio";
                            }
                            if (dImporte == 0)
                            {
                                return "El importe no puede estar vacio";
                            }

                        }
                    }
                }

                globalCL clg = new globalCL();
                string result = clg.GM_CierredemodulosStatus(DateTime.Now.Year, DateTime.Now.Month, "COM");
                if (result == "C")
                {
                    return "Este mes está cerrado, no se puede actualizar";
                    
                }


                return "OK";
            }
           
            catch ( Exception ex)
            {
                return ex.Message;
            }      
            

            
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
                tiempodeSurtido = cl.intTiempodeentrega;
                txtCondiciones.Text = cl.strCondiciones;
                txtCorrespondencia.Text = cl.CorrespondenciaA;
                txtDiastraslado.Text = cl.DiasTraslado.ToString();

                switch (cl.intStatus) {
                    case 1:
                        lblStatus.Text = "Por surtir";
                        break;
                    case 2:
                        lblStatus.Text = "Parcialmente surtida";
                        break;
                    case 3:
                        lblStatus.Text = "Surtida";
                        break;
                }

                if (cl.intDepurada > 0)
                    lblDepurada.Text = "Depurada";
                else
                    lblDepurada.Text = string.Empty;

                if (cl.intStatus>1 || cl.intAutorizadopor>0)
                {
                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (AutorizadoPor == 0)
                {
                    lblAutorizada.Text = "Por autorizar";
                    bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiParcialidades.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else
                {
                    lblAutorizada.Text = "Autorizada";
                    bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    gridViewDetalle.OptionsBehavior.ReadOnly = true;
                    gridViewDetalle.OptionsBehavior.Editable = false;

                    if (lblStatus.Text == "Por surtir")
                    {
                        bbiDesautorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        bbiParcialidades.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                }
                if (lblStatus.Text == "Parcialmente surtida")
                {
                    bbiDepurar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else
                {
                    bbiDepurar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
           

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

       

        private void Editar()
        {
            Editando = true;
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
            bbiNuevaFecha.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            gridViewDetalle.OptionsBehavior.ReadOnly = false;
            gridViewDetalle.OptionsBehavior.Editable = true;

            if (!blnNuevo)
            {
                bbiParcialidades.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }

            
            navBarControl.Visible = false;
            StatusBar.Visible = false;

            navigationFrame.SelectedPageIndex = 1;
        }

        private void Cancelar()
        {
            OrdenesdecomprasCL cl = new OrdenesdecomprasCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            String Result = cl.OrdenesdecomprasEliminar();
            if (Result == "OK")
            {
                MessageBox.Show("Cancelado correctamente");
                cierraPopAutorizar();
                LlenarGrid(AñoFiltro,MesFiltro);
            }
            else
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (strSerie.Length > 0)
            //    if (intFolio == 0)
            //    {
            //        MessageBox.Show("Selecciona un renglón");
            //    }
            //    else
            //    {
            //        DialogResult Result = MessageBox.Show("Desea eliminar el ID " + strSerie.ToString(), "Elimnar", MessageBoxButtons.YesNo);
            //        if (Result.ToString() == "Yes")
            //        {
            //            Eliminar();
            //        }
            //    }
        }

        private void gridViewPrincipal_RowClick(Object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                strSerie = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie").ToString();
                intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
                status = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status").ToString();
                AutorizadoPor = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Autorizadopor"));
                strPais = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Pais").ToString();
                //Pais = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Pais"));

                if (status == "Cancelada")
                {
                    bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else
                {
                    //bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    if (AutorizadoPor == 0)
                    {
                        
                        //if (status != "Cancelada")
                        //{
                        //    bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        //}
                        //else
                        //{
                        //    bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        //}
                    }
                    else
                    {
                        bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    //if (status == "Parcialmente surtida")
                    //{
                    //    bbiDepurar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    //}
                    //else
                    //{
                    //    bbiDepurar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //}
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }


            
            
         
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //Sacamos datos del artículo
            globalCL clg = new globalCL();
            if (e.Column.Name == "gridColumnArticulos")
            {
                DateTime fEmb=DateTime.Now;
                DateTime fRec;

                OrdenesdecomprasCL cl = new OrdenesdecomprasCL();
                string art = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulos").ToString();
                if (art.Length > 0) //validamos que haya algo en la celda
                {
                    cl.intArticulosID = Convert.ToInt32(art);
                    cl.intProveedoresID = Convert.ToInt32(cboProveedor.EditValue);
                    string result = cl.OrdenesdecomprasDatosArticulo();
                    if (result == "OK")
                    {
                        if (cl.sNombreOC.Length>0)
                        {
                            gridViewDetalle.SetFocusedRowCellValue("Descripcion", cl.sNombreOC);
                        }
                        else
                        {
                            gridViewDetalle.SetFocusedRowCellValue("Descripcion", cl.sNombreArt);
                        }
                        
                        gridViewDetalle.SetFocusedRowCellValue("UM", cl.sNombreUM);

                        if (pIvaProv == 0 || intManejaIva ==0)
                            cl.dPtjeIva = 0;
                    
                        if (intManejaIeps == 0)
                        {
                            cl.dPtjeIeps = 0;
                        }
                        

                        gridViewDetalle.SetFocusedRowCellValue("Pieps",cl.dPtjeIeps);
                        gridViewDetalle.SetFocusedRowCellValue("Piva", cl.dPtjeIva);
                        gridViewDetalle.SetFocusedRowCellValue("Precio", cl.dUltimoPrecio);

                    }
                    else
                    {
                        MessageBox.Show("CellValueChanged Art:" + result);
                    }

                    //Fecha de embarque
                    if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "FechaEmbarque") == null)
                    {
                        tiempodeSurtido = Convert.ToInt32(txtTiempoentrega.Text);
                        fEmb = Convert.ToDateTime(dateEditFecha.Text).AddDays(tiempodeSurtido);
                        gridViewDetalle.SetFocusedRowCellValue("Fechaembarque", fEmb.ToShortDateString());

                        tiempoTraslado = Convert.ToInt32(txtDiastraslado.Text);
                        fRec = fEmb.AddDays(tiempoTraslado);
                        gridViewDetalle.SetFocusedRowCellValue("Fecharecepcion", fRec);
                    }
                    //Fecha de recepcion
                    if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Fecharecepcion") == null)
                    {
                        fRec = fEmb.AddDays(tiempoTraslado);
                        gridViewDetalle.SetFocusedRowCellValue("Fecharecepcion", fRec);
                    }

                }
            }

            if (e.Column.Name== "gridColumnFechaEmbarque")
            {
                //Fecha de recepcion
                DateTime fEmb = DateTime.Now;
                DateTime fRec;
                if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Fecharecepcion") == null)
                {
                    
                    fRec = fEmb.AddDays(tiempodeSurtido);
                    gridViewDetalle.SetFocusedRowCellValue("Fecharecepcion", fRec);
                }
                else
                {
                    string valor = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Fecharecepcion").ToString();
                    if (!clg.esFecha(valor))
                    {
                        fRec = fEmb.AddDays(tiempoTraslado);
                        gridViewDetalle.SetFocusedRowCellValue("Fecharecepcion", fRec);
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
                    dIvaPorcentaje = 0;
                }
                else
                {
                    valor = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Piva").ToString();
                    if (!clg.esNumerico(valor))
                        dIvaPorcentaje = 0;
                    else
                        dIvaPorcentaje = Convert.ToDecimal(valor);
                }

                success = Decimal.TryParse(valor, out piva);

                imp = Math.Round(cant * pu, 2); //Calculamos el importe y lo redondeamos a dos decimales
                iva = Math.Round(imp * (dIvaPorcentaje / 100), 2);

                if (gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Pieps") == null)
                    valor = "0";
                else
                    valor = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Pieps").ToString();

                if (!clg.esNumerico(valor))
                    pieps = 0;
                else
                    pieps = Convert.ToDecimal(valor);
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
            object orow = cboProveedor.Properties.GetDataSourceRowByKeyValue(cboProveedor.EditValue);
            if (orow != null)
            {
                int Plazo = Convert.ToInt32(((DataRowView)orow)["Plazo"]);
                txtTiempoentrega.Text = ((DataRowView)orow)["Diasdesurtido"].ToString();
                txtDiastraslado.Text = ((DataRowView)orow)["Diasdetraslado"].ToString();
                pIvaProv = Convert.ToDecimal(((DataRowView)orow)["Piva"]);
                txtCondiciones.Text = Plazo == 0 ? "CONTADO" : "CREDITO " + Plazo.ToString() + " DÍAS";
                txtLab.Text=((DataRowView)orow)["Lab"].ToString();
                cboEmbarcarVia.EditValue=Convert.ToInt32(((DataRowView)orow)["Via"]);
                if (((DataRowView)orow)["Contacto"].ToString().Length > 0)
                {
                    txtAtencionA.Text = ((DataRowView)orow)["Contacto"].ToString();
                }
                cboMoneda.EditValue= ((DataRowView)orow)["MonedasID"].ToString();
            }

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

                if (AutorizadoPor==0)
                {
                    Watermark textWatermark = new Watermark();
                    if (status == "Cancelada")
                    {
                        textWatermark.Text = "Cancelada";
                        textWatermark.ForeColor = Color.Red;
                    }                        
                    else
                    {
                        if (strPais == "Mexico")
                            textWatermark.Text = "No autorizado";
                        else
                            textWatermark.Text = "Not authorized";
                        textWatermark.ForeColor = Color.DodgerBlue;
                    }
                        
                    textWatermark.TextDirection = DirectionMode.ForwardDiagonal;
                    textWatermark.Font = new Font(textWatermark.Font.FontFamily, 40);
                   
                    textWatermark.TextTransparency = 150;
                    textWatermark.ShowBehind = false;
                    textWatermark.PageRange = "1,3-5";
                    report.Watermark.CopyFrom(textWatermark);
                }
                


                this.documentViewer1.DocumentSource = report;
                report.CreateDocument(false);
                documentViewer1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    
        private void bbiRegresarimpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (sOrigenImpresion=="Guardar")
            {
                SiguienteID();
                navigationFrame.SelectedPageIndex = 1;
                navBarControl.Visible = false;
                StatusBar.Visible = false;
                ribbonStatusBar.Visible = true;
                ribbonPage.Visible = true;
                btnAutorizar.MergeOwner.SelectedPage = btnAutorizar.MergeOwner.TotalPageCategory.GetPageByText("Home");
                RibbonPagePrint.Visible = false;
                intFolio = 0;
                strSerie = string.Empty;

                StatusBar.Visible = false;
            }
            else
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Grid principal", "Cargando información...");
                RibbonPagePrint.Visible = false;
                printPreviewBarItem25.Enabled = false;
                printPreviewBarItem26.Enabled = false;
                printPreviewBarItem27.Enabled = false;
                ribbonPage.Visible = true;
                ribbonStatusBar.Visible = true;
                navBarControl.Visible = true;
                btnAutorizar.MergeOwner.SelectedPage = btnAutorizar.MergeOwner.TotalPageCategory.GetPageByText("Home");
                navigationFrame.SelectedPageIndex = 0;
                bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
            
        }

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

        private void navBarItemAbril_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
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

        private void navBarItemSep_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
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

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Quitar ya no se usara por que no validamos serie 
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
            else
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
        }



        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Quitar
            if (strSerie.Length > 0)
            {
            if (intFolio == 0)
                {
                    MessageBox.Show("Selecciona un renglón");
                }
                else
                {
                    strCancelando = "SI";
                    Autorizando = false;               
                    groupControl1.Text = "Cancelar la órden de compra " + strSerie + intFolio.ToString();
                    popUpAutorizar.Visible = true;
                    btnAut.Text = "Cancelar";
                    txtLogin.Focus();

                    //DialogResult Result = MessageBox.Show("Desea cancelar la Orden de compra " + strSerie.ToString() + intFolio.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                    //if (Result.ToString() == "Yes")
                    //{
                    //    Eliminar();
                    //}
                }
            }
            else
            {
                if (intFolio == 0)
                {
                    MessageBox.Show("Selecciona un renglón");
                }
                else
                {
                    strCancelando = "SI";
                    Autorizando = false;
                    groupControl1.Text = "Cancelar la órden de compra " + strSerie + intFolio.ToString();
                    popUpAutorizar.Visible = true;
                    btnAut.Text = "Cancelar";
                    txtLogin.Focus();

                    //DialogResult Result = MessageBox.Show("Desea cancelar la Orden de compra " + strSerie.ToString() + intFolio.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                    //if (Result.ToString() == "Yes")
                    //{
                    //    Eliminar();
                    //}
                }

            }
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiVista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlPrincipal.ShowRibbonPrintPreview();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (sOrigenImpresion == "Parcialidades")
            {
               
                if (lblStatus.Text=="Por surtir")
                {
                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiDepurar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                    
                
                bbiParcialidades.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                sOrigenImpresion = string.Empty;
                navigationFrame.SelectedPageIndex = 1;
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Grid principal", "Cargando información...");
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDesautorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDepurar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiParcialidades.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevaFecha.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navBarControl.Visible = true;
            StatusBar.Visible = true;
            LlenarGrid(AñoFiltro,MesFiltro);
            navigationFrame.SelectedPageIndex = 0;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridOrdenesdecomprasdetalle";
            String Result = clg.SaveGridLayout(gridViewDetalle);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Imprimir()
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
                    printPreviewBarItem25.Enabled = true;
                    printPreviewBarItem26.Enabled = true;
                    printPreviewBarItem27.Enabled = true;
                    ribbonPage.Visible = false;
                    navigationFrame.SelectedPageIndex = 2;
                    ribbonStatusBar.Visible = false;




                    reporte();
                    navBarControl.Visible = false;
                    btnAutorizar.MergeOwner.SelectedPage = btnAutorizar.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Imprime: " + ex.Message);
                }
            }
        }
        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!Editando)
                sOrigenImpresion = "Grid";
            else
                sOrigenImpresion = "Guardar";
            Imprimir();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void bbiDepurar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Autorizando = false;
            strCancelando = "";
            groupControl1.Text = "Depurar la órden de compra " + strSerie + intFolio.ToString();
            popUpAutorizar.Visible = true;
            btnAut.Text = "Depurar";
            txtLogin.Focus();
        }

        private void txtTiempoentrega_Leave(object sender, EventArgs e)
        {
            globalCL cl = new globalCL();
            if (cl.esNumerico(txtTiempoentrega.Text))
            {

                tiempodeSurtido = Convert.ToInt32(txtTiempoentrega.Text);
            }
        }

        private void txtDiastraslado_Leave(object sender, EventArgs e)
        {
            globalCL cl = new globalCL();
            if (cl.esNumerico(txtDiastraslado.Text))
            {
                diasTraslado = Convert.ToInt32(txtDiastraslado.Text);
            }
        }

        private void bbiAutorizar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            strCancelando = "";
            Autorizando = true;
            Desautorizando = false;
            groupControl1.Text = "Autorizar la órden de compra " + strSerie + intFolio.ToString();
            popUpAutorizar.Visible = true;
            txtLogin.Focus();
        }

        private void cierraPopAutorizar()
        {
            popUpAutorizar.Visible = false;
            txtLogin.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            cierraPopAutorizar();
        }
        private void Autorizar()
        {
            if (txtLogin.Text.Length==0)
            {
                MessageBox.Show("Teclee el login");
                return;
            }
            if (txtPassword.Text.Length==0)
            {
                MessageBox.Show("Teclee el password");
                return;
            }
        }

        private void btnAut_Click(object sender, EventArgs e)
        {
            
        }

        private void bbiEnviaCorreo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EnviaCorreo();
        }
        private void EnviaCorreo()
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                //DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Correos", "Enviando...");
                //globalCL cl = new globalCL();
                //cl.AbreOutlook(strSerie, intFolio, dFecha, strTo);
                //if (!blNuevo)
                //{
                //    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                //}

                //if (cl.iAbrirOutlook == 0)
                //{
                //    MessageBox.Show("Correo enviado");
                //}
            }
        }

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

        private void gridViewPrincipal_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string _mark = (string)view.GetRowCellValue(e.RowHandle, "Status");

            if (_mark == "Cancelada")
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            popUpAutorizar.Hide();
        }

        private void btnAut_Click_1(object sender, EventArgs e)
        {
            try
            {

                globalCL clg = new globalCL();
                string result = clg.GM_CierredemodulosStatus(DateTime.Now.Year, DateTime.Now.Month, "COM");
                if (result == "C")
                {
                    MessageBox.Show("Este mes está cerrado, no se puede hacer movimientos");
                    return;

                }

                if (strCancelando == "SI")
                {
                    Cancelar();
                    return;
                }

                OrdenesdecomprasCL cl = new OrdenesdecomprasCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.sLogin = txtLogin.Text;
                cl.sPassword = txtPassword.Text;
                cl.blnAutorizando = Autorizando;
                cl.blnDesautorizando = Desautorizando;

                result = cl.OrdenesdecomprasAutorizar();
                if (result == "OK")
                {
                    if (Autorizando)
                    {
                        MessageBox.Show("Autorizada correctamente");
                        bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        bbiDesautorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        lblAutorizada.Text = "Autorizada";
                        AutorizadoPor = globalCL.gv_UsuarioID;
                    }
                    else
                    if (Desautorizando)
                    {
                        MessageBox.Show("Desautorizada correctamente");
                        bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        bbiDesautorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        gridViewDetalle.OptionsBehavior.ReadOnly = false;
                        gridViewDetalle.OptionsBehavior.Editable = true;
                        lblAutorizada.Text = "Por autorizar";
                        AutorizadoPor = 0;
                    }
                    else
                    {
                        MessageBox.Show("Depurado correctamente");
                        bbiDepurar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                        
                    cierraPopAutorizar();
                    //LlenarGrid();
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnAut:" + ex.Message);
            }
        }

        private void bbiDesautorizar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strCancelando = "";
            Autorizando = false;
            Desautorizando = true;
            groupControl1.Text = "Desautorizar la órden de compra " + strSerie + intFolio.ToString();
            popUpAutorizar.Visible = true;
            txtLogin.Focus();
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
                    case "navBarItemAbril":
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
                    case "navBarItemTodos":
                        MesFiltro = 0;
                        break;
                }

                LlenarGrid(AñoFiltro, MesFiltro);

            }
        }

        private void bbiParcialidades_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            sOrigenImpresion = "Parcialidades";
            OrdenesdecomprasCL cl = new OrdenesdecomprasCL();
            cl.strSerie = cboSerie.EditValue.ToString();
            cl.intFolio = Convert.ToInt32(txtFolio.Text);
            gridControlParcialidades.DataSource = cl.Parcialidades();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDepurar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiParcialidades.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 3;
        }

        private void bbiNuevaFecha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            popupControlContainerNuevaFecha.Visible = true;
            swNuevaFechatodalacompra.IsOn = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            popupControlContainerNuevaFecha.Visible = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            CambiarFechaRecepcion();
        }

        private void CambiarFechaRecepcion()
        {
            try
            {
                OrdenesdecomprasCL cl = new OrdenesdecomprasCL();
                try
                {
                    cl.fFecha = Convert.ToDateTime(dFechaNueva.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Seleccione una fecha");
                    return;
                }

                
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);

                if (swNuevaFechatodalacompra.IsOn)
                    cl.iSeq = 0;
                else                   
                    cl.iSeq = intNuevaFechaseq;
                

                string result = cl.OrdenesdecomprasNuevaFecha();
                if (result == "OK")
                {
                    MessageBox.Show("Fecha cambiada correctamente");
                    popupControlContainerNuevaFecha.Visible = false;
                }                    
                else
                    MessageBox.Show(result);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewDetalle_RowClick(object sender, RowClickEventArgs e)
        {
            lblArtNuevaFecha.Text = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Descripcion").ToString();
            intNuevaFechaseq = Convert.ToInt32(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Seq"));
        }
    }
}
