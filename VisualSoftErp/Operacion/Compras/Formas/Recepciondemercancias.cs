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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraNavBar;

namespace VisualSoftErp.Catalogos
{

    public partial class Recepciondemercancia : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public BindingList<OrdenesComprasCL> OrdenesCompras;
        public BindingList<DetalleCL> Detalle;
        string strSerie;
        int intFolio;
        int intFolioOC;
        string strSerieOC;
        string ComprasSerie;
        int ComprasFolio;
        int ProveedoresId;
        string MonedasId;
        string origenimpresion;
        string status;
        int AñoFiltro;
        int MesFiltro;

        public Recepciondemercancia()
        {
            InitializeComponent();



            CargaCombos();

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Recepción de mercancía";

            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;

            LlenarGrid(DateTime.Now.Year,DateTime.Now.Month);
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            bbiRecibirmercancias.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCargarordenes.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ribbonPageGroup2.Visible = false;

            gridViewOrdenesCompras.OptionsBehavior.ReadOnly = true;
            gridViewOrdenesCompras.OptionsBehavior.Editable = false;

            gridColumnCantidadPorRecibir.OptionsColumn.ReadOnly = true;
            gridColumnArticulo.OptionsColumn.ReadOnly = true;
            gridColumnDescripcion.OptionsColumn.ReadOnly = true;
            gridColumnCantidadPorRecibir.OptionsColumn.AllowEdit = false;
            gridColumnArticulo.OptionsColumn.AllowEdit = false;
            gridColumnDescripcion.OptionsColumn.AllowEdit = false;

            AgregaAñosNavBar();

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


        private void CargaCombos()
        {
            combosCL cl = new combosCL();

            cl.strTabla = "Proveedores";
            cboProveedor.Properties.ValueMember = "Clave";
            cboProveedor.Properties.DisplayMember = "Des";
            cboProveedor.Properties.DataSource = cl.CargaCombos();
            cboProveedor.Properties.ForceInitialize();
            cboProveedor.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedor.Properties.PopulateColumns();
            cboProveedor.Properties.Columns["Clave"].Visible = false; //con esta propiedad puedo ocultar campos en el cbo     
            cboProveedor.Properties.Columns["Piva"].Visible = false;
            cboProveedor.Properties.Columns["Plazo"].Visible = false;
            cboProveedor.Properties.Columns["Tiempodeentrega"].Visible = false;
            cboProveedor.Properties.Columns["Diastraslado"].Visible = false;
            cboProveedor.Properties.Columns["Lab"].Visible = false;
            cboProveedor.Properties.Columns["Via"].Visible = false;
            cboProveedor.Properties.Columns["BancosID"].Visible = false;
            cboProveedor.Properties.Columns["Cuentabancaria"].Visible = false;
            cboProveedor.Properties.Columns["C_Formapago"].Visible = false;
            cboProveedor.Properties.Columns["MonedasID"].Visible = false;
            cboProveedor.Properties.Columns["Retiva"].Visible = false;
            cboProveedor.Properties.Columns["Retisr"].Visible = false;
            cboProveedor.Properties.Columns["Contacto"].Visible = false;
            cboProveedor.Properties.Columns["Diasdesurtido"].Visible = false;
            cboProveedor.Properties.Columns["Diastraslado"].Visible = false;
            cboProveedor.Properties.Columns["Diasdetraslado"].Visible = false;


            cboProveedor.ItemIndex = 0;

        }

        private void LlenarGrid(int Año,int Mes)
        {
            RecepciondemercanciaCL cl = new RecepciondemercanciaCL();
            cl.intAño = Año;
            cl.intMes = Mes;            
            gridControlPrincipal.DataSource = cl.RecepciondemercanciaGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridRecepciondemercancia";
            clg.restoreLayout(gridViewPrincipal);

            //con esta lina de codigo ponemos los autofiltros
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
        } //LlenarGrid()



        private void Nuevo()
        {
            //BotonesEdicion();
            BotonesNuevo();
            CargaCombos();
            strSerie = string.Empty;
            intFolio = 0;
        }

        public class OrdenesComprasCL
        {
            public string Serie { get; set; }
            public string Folio { get; set; }
            public string Fecha { get; set; }
            public string TiempoEntrega { get; set; }
            public string NomProv { get; set; }
            public int ProveedoresID { get; set; }
            public string MonedasID { get; set; }
        }
        private void Inicialisalista()
        {
            OrdenesCompras = new BindingList<OrdenesComprasCL>();
            OrdenesCompras.AllowNew = true;
            gridControlOrdenesCompras.DataSource = OrdenesCompras;
        }


        public class DetalleCL
        {
            public string CantidadPorRecibir { get; set; }
            public string Articulo { get; set; }
            public string Descripcion { get; set; }
            public string Recibido { get; set; }
            public string Precio { get; set; }
            public string Iva { get; set; }
            public string Piva { get; set; }
            public string Pieps { get; set; }
            public string Seq { get; set; }
            public string Importe { get; set; }
            public string Neto { get; set; }
            public string Nombre { get; set; }
        }

        private void InicialisalistaOC()
        {
            OrdenesCompras = new BindingList<OrdenesComprasCL>();
            OrdenesCompras.AllowNew = true;
            gridControlOrdenesCompras.DataSource = OrdenesCompras;
        }

        private void SiguienteIDCompras()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            ComprasCL cl = new ComprasCL();
            cl.strSerie = serie;
            cl.strDoc = "Compras";

            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {

                ComprasSerie = serie;
                //ComprasFolio = cl.intFolio.ToString();
                ComprasFolio = cl.intFolio;
            }
            else
            {
                MessageBox.Show("SiguienteID :" + result);
            }

        }//SguienteID

        private void SiguienteIDOC()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            ComprasCL cl = new ComprasCL();
            cl.strSerie = serie;
            cl.strDoc = "Ordenesdecompras";

            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {

                strSerieOC = serie;
                //ComprasFolio = cl.intFolio.ToString();
                intFolioOC = cl.intFolio;
            }
            else
            {
                MessageBox.Show("SiguienteID :" + result);
            }

        }//SguienteID

        private void SiguienteIdRM()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            ComprasCL cl = new ComprasCL();
            cl.strSerie = serie;
            cl.strDoc = "Recepciondemercancia";

            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {

                strSerie = serie;
                //ComprasFolio = cl.intFolio.ToString();
                intFolio = cl.intFolio;
            }
            else
            {
                MessageBox.Show("SiguienteID :" + result);
            }

        }//SguienteID

        private void InicialisalistaDetalle()
        {
            Detalle = new BindingList<DetalleCL>();
            Detalle.AllowNew = true;
            gridControlOrdenesCompras.DataSource = Detalle;
        }

        private void BotonesNuevo()
        {
            bbiRecibirmercancias.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCargarordenes.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 1;
            navBarControl.Visible = false;
        }

        private void LimpiaCajas()
        {

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

                //SiguienteIDCompras();
                //SiguienteIDOC();
                SiguienteIdRM();


                string sCondicion = String.Empty;
                System.Data.DataTable dtCompras = new System.Data.DataTable("Compras");               

                System.Data.DataTable dtRMdetalle = new System.Data.DataTable("Comprasdetalle");
                dtRMdetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtRMdetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtRMdetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtRMdetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtRMdetalle.Columns.Add("Precio", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("importe", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("Costoum2", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("Factorum2", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("PIva", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("PIeps", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("PDescuento", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("OCSerie", Type.GetType("System.String"));
                dtRMdetalle.Columns.Add("OCNumero", Type.GetType("System.Int32"));
                dtRMdetalle.Columns.Add("OCSeq", Type.GetType("System.Int32"));
                dtRMdetalle.Columns.Add("Neto", Type.GetType("System.Decimal"));
               

                System.Data.DataTable dtRecepciondemercancia = new System.Data.DataTable("Recepciondemercancia");
                dtRecepciondemercancia.Columns.Add("Serie", Type.GetType("System.String"));
                dtRecepciondemercancia.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtRecepciondemercancia.Columns.Add("Comprasserie", Type.GetType("System.String"));
                dtRecepciondemercancia.Columns.Add("ComprasFolio", Type.GetType("System.Int32"));
                dtRecepciondemercancia.Columns.Add("ContrarecibosFolio", Type.GetType("System.Int32"));
                dtRecepciondemercancia.Columns.Add("ContrarecibosSeq", Type.GetType("System.Int32"));
                dtRecepciondemercancia.Columns.Add("OrdendecompraSerie", Type.GetType("System.String"));
                dtRecepciondemercancia.Columns.Add("Ordedecomprafolio", Type.GetType("System.Int32"));
                dtRecepciondemercancia.Columns.Add("Status", Type.GetType("System.Int32"));
                dtRecepciondemercancia.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtRecepciondemercancia.Columns.Add("FechaCancelacion", Type.GetType("System.DateTime"));
                dtRecepciondemercancia.Columns.Add("Motivocancelacion", Type.GetType("System.String"));
                dtRecepciondemercancia.Columns.Add("Observaciones", Type.GetType("System.String"));
                dtRecepciondemercancia.Columns.Add("Validado", Type.GetType("System.Int32"));
                dtRecepciondemercancia.Columns.Add("Fechavalidacion", Type.GetType("System.DateTime"));
                dtRecepciondemercancia.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtRecepciondemercancia.Columns.Add("FechaReal", Type.GetType("System.DateTime"));
                dtRecepciondemercancia.Columns.Add("ProveedoresID", Type.GetType("System.Int32"));
                dtRecepciondemercancia.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dtRecepciondemercancia.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtRecepciondemercancia.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dtRecepciondemercancia.Columns.Add("AlmacenesID", Type.GetType("System.Int32"));
                dtRecepciondemercancia.Columns.Add("CargoVario", Type.GetType("System.Decimal"));


                int intRen = 0;
                string dato = String.Empty;
                decimal dCantidad = 0;
                int intArticulosID = 0;
                decimal dPrecio = 0;
                decimal dimporte = 0;
                decimal dIva = 0;
                decimal dIeps = 0;
                //decimal dCostoum2 = 0;
                //decimal dFactorum2 = 0;
                decimal dPIva = 0;
                decimal dPIeps = 0;
                decimal dDescuento = 0;
                decimal dPDescuento = 0;               
                //int intOCSeq = 0;
                int intSeq = 0;

                decimal decTotSubtotal = 0;
                decimal decTotIva = 0;
                decimal decTotIeps = 0;
                decimal decTotNeto = 0;
                decimal decTotDescuento = 0;
                decimal decNetogrid = 0;
                decimal decCantidadRecibir;

                globalCL clg = new globalCL();

                for (int i = 0; i < gridViewOCdetalle.RowCount; i++)
                {
                    dato = gridViewOCdetalle.GetRowCellValue(i, "Recibido").ToString();
                    if (clg.esNumerico(dato))
                    {
                        if (Convert.ToDecimal(dato)>0)
                        {                        
                            intSeq = i;
                            dCantidad = Convert.ToDecimal(gridViewOCdetalle.GetRowCellValue(i, "Recibido"));
                            decCantidadRecibir = Convert.ToDecimal(gridViewOCdetalle.GetRowCellValue(i, "CantidadPorRecibir"));
                            intArticulosID = Convert.ToInt32(gridViewOCdetalle.GetRowCellValue(i, "Articulo"));
                            dPrecio = Convert.ToDecimal(gridViewOCdetalle.GetRowCellValue(i, "Precio"));
                            dimporte = Math.Round(dCantidad * dPrecio,2);

                            dPIva = Convert.ToDecimal(gridViewOCdetalle.GetRowCellValue(i, "Piva"));
                            dPIeps = Convert.ToDecimal(gridViewOCdetalle.GetRowCellValue(i, "Pieps"));
                            dIva = Math.Round(dimporte * (dPIva / 100),2);
                            dIeps = Math.Round(dimporte * (dPIeps / 100),2);
                            decNetogrid = dimporte + dIva + dIeps;

                            dtRMdetalle.Rows.Add(strSerie, intFolio, intSeq, dCantidad, intArticulosID, dPrecio, dimporte, dIva, dIeps, 0, 0, dPIva, dPIeps, dDescuento, dPDescuento, strSerieOC, intFolioOC, intSeq, decNetogrid);

                            decTotSubtotal += dimporte;
                            decTotIva += dIva;
                            decTotIeps += dIeps;
                            decTotDescuento += dDescuento;
                        }
                    }
                }
                decTotNeto += decTotSubtotal + decTotIva + decTotIeps;

                DateTime fFecha = DateTime.Now;

                
                string result = clg.GM_CierredemodulosStatus(fFecha.Year, fFecha.Month, "COM");
                if (result == "C")
                {
                    MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                    return;
                }

                int intOCFolio = intFolioOC;
                int intProveedoresID = ProveedoresId;
                string strMonedas = MonedasId;
                string strFactura = string.Empty;
                DateTime ffechafactura = DateTime.Now;
                decimal dtipodecambio = 1;
                decimal decSubtotal = decTotSubtotal;
                decimal decIva = decTotIva;
                decimal decIeps = decTotIeps;
                decimal decNeto = decTotNeto;
                int intStatus = 1;
                int intPlazo = 0;
                DateTime fFechacancelacion = DateTime.Now;
                string strRazoncancelacion = string.Empty;
                decimal decDescuento = decTotDescuento;
                int intPoliza = 0;
                int intNodeducible = 0;
                string strContrarecibosSerie = string.Empty;
                int intContrarecibosFolio = 0;
                string strRecepcionSerie = strSerie;
                int intRecepcionFolio = intFolio;
                int intValidadoPor = 0;
                DateTime fFechaValidado = DateTime.Now;
                DateTime fFechaReal = DateTime.Now;
                //dato = String.Empty;
                string strObservaciones = string.Empty;
                int intAlamcen = Convert.ToInt32(cboAlmacen.EditValue);
                int intUsuariosID = globalCL.gv_UsuarioID;
                int intUsuarioCancelo = 0;
                string sUUID = string.Empty;

              


                string strSerieRM = strSerie;
                int intFolioRM = intFolio;

                int intCotrarecibo = 0;
                int intContrareciboseq = 0;
                int Status = 1;
                DateTime Fecha = DateTime.Now;
                DateTime Fcancelacion = DateTime.Now;
                string strMotivosCancelacion = string.Empty;
                string observaciones = string.Empty;
                int Validado = 0;
                DateTime Fvalidacion = DateTime.Now;

                dtRecepciondemercancia.Rows.Add(strSerie, intFolio, "", 0, intCotrarecibo, intContrareciboseq, 
                    strSerieOC, intFolioOC, Status, Fecha, fFechacancelacion, strMotivosCancelacion, 
                    strObservaciones, Validado, fFechaValidado,intUsuariosID,Fecha, intProveedoresID, decSubtotal, decIva, decTotNeto, intAlamcen,0);



                RecepciondemercanciaCL cl = new RecepciondemercanciaCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.strComprasserie = ComprasSerie;
                cl.intComprasFolio = ComprasFolio;
                cl.strMaquina = Environment.MachineName;
                cl.intUsuarioID = 1;
                cl.strPrograma = "0122";               
                cl.dtRMDet = dtRMdetalle;
                cl.dtRM = dtRecepciondemercancia;
                Result = cl.RecepciondemercanciasCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    origenimpresion = "Guardar";
                    Imprime();


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

            decimal recibio = 0;
            decimal cantRec = 0;
            decimal cantxRec = 0;
            string valor = string.Empty;

            globalCL cl = new globalCL();

            for (int i = 0; i <= gridViewOCdetalle.RowCount - 1; i++)
            {
                gridViewOCdetalle.FocusedRowHandle = i;
                gridViewOCdetalle.UpdateCurrentRow();  //Forza el update del edit aunque no cambien de celda


                valor = gridViewOCdetalle.GetRowCellValue(i, "Recibido").ToString();
                if (cl.esNumerico(valor))
                {
                    cantRec = Convert.ToDecimal(valor);
                }
                else
                {
                    cantRec = 0;
                }

                valor = gridViewOCdetalle.GetRowCellValue(i, "CantidadPorRecibir").ToString();
                if (cl.esNumerico(valor))
                {
                    cantxRec = Convert.ToDecimal(valor);
                }
                else
                {
                    cantxRec = 0;
                }

                if (cantRec > cantxRec)
                {

                    Decimal CantMas = cantRec * Convert.ToDecimal(0.10);

                    if (cantRec > CantMas + cantxRec)
                    {
                        return "No se puede recibir más del 10% de lo solicitado";
                    }
                        
                    
                }

                recibio += cantRec;
            }

            if (recibio <= 0)
            {
                return "Debe surtir al menos un renglón";
            }

            globalCL clg = new globalCL();
            string result = clg.GM_CierredemodulosStatus(DateTime.Now.Year, DateTime.Now.Month, "COM");
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";                
            }



            return "OK";
        } //Valida



        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (intSerie == 0)
            //    if (intFolio == 0)
            //    {
            //        MessageBox.Show("Selecciona un renglón");
            //    }
            //    else
            //    {
            //        Editar();
            //    }
        }  //Editar

        private void Editar()
        {
            BotonesEdicion();
            //llenaCajas();
        }

        private void BotonesEdicion()
        {
            LimpiaCajas();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 1;
        }

        

        

        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (navigationFrame.SelectedPageIndex == 2)
            {
                navigationFrame.SelectedPageIndex = 1;

                BotonesNuevo();
                bbiRecibirmercancias.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                ribbonPageGroup2.Visible = false;
            }
            else
            {
                bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiCargarordenes.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiRecibirmercancias.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                ribbonPageGroup2.Visible = false;
                LlenarGrid(AñoFiltro,MesFiltro);
                navigationFrame.SelectedPageIndex = 0;
                navBarControl.Visible = true;
                InicialisalistaOC();
                cboProveedor.EditValue = null;
            }

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridRecepciondemercanciaocdetalle";
            String Result = clg.SaveGridLayout(gridViewOCdetalle);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridRecepciondemercancia";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            clg.strGridLayout = "gridRecepciondemercanciaoc";
            Result = clg.SaveGridLayout(gridViewOrdenesCompras);
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



        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void CargaOC()
        {
            try
            {
                if (cboProveedor.EditValue == null)
                {
                    MessageBox.Show("Selecciones un proveedor");
                }
                else
                {
                    RecepciondemercanciaCL cl = new RecepciondemercanciaCL();
                    cl.strSerie = "x";
                    cl.IdProveedor = Convert.ToInt32(cboProveedor.EditValue);
                    gridControlOrdenesCompras.DataSource = cl.OrdenComprasPorProveedor();
                    if (gridControlOrdenesCompras.DataSource != null)
                    {
                        globalCL clg = new globalCL();
                        clg.strGridLayout = "gridRecepciondemercanciaoc";
                        clg.restoreLayout(gridViewPrincipal);
                        bbiRecibirmercancias.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                        gridControlOrdenesCompras.Focus();
                    }
                    else
                    {
                        bbiRecibirmercancias.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        MessageBox.Show("No hay órdenes de compra por recibir");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }
        }

        private void bbiCargarordenes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CargaOC();

        }

        private void cboProveedor_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridViewOrdenesCompras_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            strSerieOC = gridViewOrdenesCompras.GetRowCellValue(gridViewOrdenesCompras.FocusedRowHandle, "Serie").ToString();
            intFolioOC = Convert.ToInt32(gridViewOrdenesCompras.GetRowCellValue(gridViewOrdenesCompras.FocusedRowHandle, "Folio"));
            ProveedoresId = Convert.ToInt32(gridViewOrdenesCompras.GetRowCellValue(gridViewOrdenesCompras.FocusedRowHandle, "ProveedoresID"));
            MonedasId = gridViewOrdenesCompras.GetRowCellValue(gridViewOrdenesCompras.FocusedRowHandle, "MonedasID").ToString();
        }

        private void CargoComboAlmacen()
        {
            combosCL cl = new combosCL();

            cl.strTabla = "Almacenes";
            cboAlmacen.Properties.ValueMember = "Clave";
            cboAlmacen.Properties.DisplayMember = "Des";
            cboAlmacen.Properties.DataSource = cl.CargaCombos();
            cboAlmacen.Properties.ForceInitialize();
            cboAlmacen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacen.Properties.PopulateColumns();
            cboAlmacen.Properties.Columns["Clave"].Visible = false;
            cboAlmacen.ItemIndex = 0;
        }

        private void bbiRecibirmercancias_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (strSerieOC == null)
                {
                    MessageBox.Show("Selecciona una Orden de compra");
                    return;
                }

                    if (intFolioOC == 0)
                    {
                        MessageBox.Show("Selecciona una Orden de compra");
                    }
                    else
                    {
                        RecepciondemercanciaCL cl = new RecepciondemercanciaCL();
                        cl.strSerie = strSerieOC;
                        cl.intFolio = intFolioOC;
                        gridControlOCdetalle.DataSource = cl.OrdenComprasDetalle();

                        globalCL clg = new globalCL();
                        clg.strGridLayout = "gridRecepciondemercanciaocdetalle";
                        clg.restoreLayout(gridViewPrincipal);


                        BotonesRecepcionMercancia();
                        CargoComboAlmacen();

                        lblRecibiendo.Text = "Recibiendo la OC: " + strSerieOC + intFolioOC.ToString() + " de " + cboProveedor.Text;
                    }                

            }
            catch (Exception ex)
            {
                MessageBox.Show("RecibirMercancias: " + ex);
            }
        }

        private void BotonesRecepcionMercancia()
        {
            bbiRecibirmercancias.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCargarordenes.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            ribbonPageGroup2.Visible = true;
            navigationFrame.SelectedPageIndex = 2;
            navBarControl.Visible = false;
        }

        private void bbiTodo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int Cantidad = 0;

                for (int i = 0; i < gridViewOCdetalle.RowCount; i++)
                {
                    //gridViewOCdetalle.SetRowCellValue(i, "Pdescuento", PDescuento);
                    Cantidad = Convert.ToInt32(gridViewOCdetalle.GetRowCellValue(i, "CantidadPorRecibir"));
                    gridViewOCdetalle.SetRowCellValue(i, "Recibido", Cantidad);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Todo: " + ex);
            }
        }

        private void bbiNada_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int Cantidad = 0;

                for (int i = 0; i < gridViewOCdetalle.RowCount; i++)
                {
                    //gridViewOCdetalle.SetRowCellValue(i, "Pdescuento", PDescuento);
                    //Cantidad = Convert.ToInt32(gridViewOCdetalle.GetRowCellValue(i, "CantidadPorRecibir"));
                    gridViewOCdetalle.SetRowCellValue(i, "Recibido", Cantidad);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Todo: " + ex);
            }
        }

        private void navBarItemEne_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void navBarItemFeb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
           
        }

        private void navBarItemMar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void navBarItemAbr_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void navBarItemMay_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void navBarItemJun_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void navBarItemJul_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void navBarItemAgo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void navBarItemSep_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void navBarItemOct_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void navBarItemNov_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void navBarItemDic_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void navBarItemTodos_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void reporte()
        {
            try
            {
                RecepciondecompraFormatoImpresion report = new RecepciondecompraFormatoImpresion();
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
                    origenimpresion = "Principal";
                    Imprime();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Imprime: " + ex.Message);
                }
            }
        }

        private void Imprime()
        {
            RibbonPagePrint.Visible = true;
            ribbonPage1.Visible = false;
            navigationFrame.SelectedPageIndex = 3;
            ribbonStatusBar.Visible = false;

            reporte();
            navBarControl.Visible = false;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
        }

        private void bbiRegresarImp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (origenimpresion == "Principal")
            {
                navigationFrame.SelectedPageIndex = 0;
                navBarControl.Visible = true;
                ribbonStatusBar.Visible = true;
                ribbonPage1.Visible = true;                
                
                intFolio = 0;
                strSerie = string.Empty;
            }
            else
            {
                navigationFrame.SelectedPageIndex = 1;
                CargaOC();
                //BotonesNuevo();
                bbiRecibirmercancias.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                ribbonPage1.Visible = true;
                ribbonPageGroup2.Visible = false;
                bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            RibbonPagePrint.Visible = false;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText("Home");


        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            strSerie = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie").ToString();
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            status = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status").ToString();

            if (status == "Registrada")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            popUpCancelar.Visible = true;
            groupControl1.Text = "Cancelar la recepción " + strSerie + intFolio.ToString();
            txtLogin.Focus();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            popUpCancelar.Visible = false;
        }

        private void btnAut_Click(object sender, EventArgs e)
        {
            popUpCancelar.Visible = false;
            Cancelar();
        }

        private void Cancelar()
        {

            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLogin.Text;
            clU.strClave = txtPassword.Text;
            clU.strPermiso = "CancelarRM";
            string result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }
            int intUsuariocancelo = clU.intUsuariosID;

            globalCL clg = new globalCL();
            result = clg.GM_CierredemodulosStatus(DateTime.Now.Year, DateTime.Now.Month, "COM");
            if (result == "C")
            {
                MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                return;
            }


            RecepciondemercanciaCL cl = new RecepciondemercanciaCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            cl.intUsuarioID = intUsuariocancelo;
            result = cl.RecepciondemercanciaCancelar();
            if (result == "OK")
            {
                MessageBox.Show("Cancelada correctamente");
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                txtLogin.Text = string.Empty;
                txtPassword.Text = string.Empty;
                LlenarGrid(AñoFiltro, MesFiltro);
            }
            else
            {
                MessageBox.Show("Cancelar: " + result);
            }



        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string _mark = (string)view.GetRowCellValue(e.RowHandle, "Status");
            string _validado = (string)view.GetRowCellValue(e.RowHandle, "Validado");
            if (_mark == "Cancelada")
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Red;
            }
            else
            {
                if (_validado == "SI")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.ForeColor = Color.Green;
                }
            }

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
    }
}
