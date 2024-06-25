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
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using System.Configuration;
using DevExpress.XtraGrid;
using VisualSoftErp.Operacion.Inventarios.Designers;
using VisualSoftErp.Operacion.Inventario.Designers;
using DevExpress.XtraNavBar;

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class Entradasysalidas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intFolio, intStatus;
        string strSerie;
        int intUsuarioID = globalCL.gv_UsuarioID;
        public BindingList<detalleCL> detalle;
        public bool blNuevo;
        int intTiposdemovimientoinvID;
        bool blnfirmaElectronica;
        int AñoFiltro;
        int MesFiltro;
        int intTM;


        public Entradasysalidas()
        {
            InitializeComponent();
            gridViewDetalle.OptionsView.ShowViewCaption = false;
            dateEditFech.Text = DateTime.Now.ToShortDateString();
            txtObservaciones.Properties.MaxLength = 500;
            txtObservaciones.EnterMoveNextControl = true;

            cboOperacion.EnterMoveNextControl = true;

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;
            gridViewDetalle.Columns[3].OptionsColumn.AllowEdit = false;

            gridViewPorAutorizar.OptionsBehavior.ReadOnly = true;
            gridViewPorAutDetalle.OptionsBehavior.ReadOnly = true;
            gridViewPorAutorizar.OptionsBehavior.Editable = false;
            gridViewPorAutDetalle.OptionsBehavior.Editable = false;


            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Entradasysalidas";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            BuscarSerie();

            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;
            LlenarGrid(AñoFiltro, MesFiltro);
            gridViewPrincipal.ActiveFilter.Clear();

            AgregaAñosNavBar();

            UsuariosCL clU = new UsuariosCL();
            clU.intUsuariosID = globalCL.gv_UsuarioID;

            string result = clU.UsuariosLlenaCajas();
            if (result == "OK")
                blnfirmaElectronica = clU.iFirmaElectronicaSalidas == 1 ? true : false;

            VerificaPorAutorizar();
            //DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void VerificaPorAutorizar()
        {
            globalCL cl = new globalCL();
            string result = cl.Salidasporautorizar();
            if (result == "OK")
            {
                if (cl.intSalidasporautorizar>0)
                {
                    UsuariosCL clu = new UsuariosCL();
                    clu.intUsuariosID = globalCL.gv_UsuarioID;
                    result = clu.UsuariosLlenaCajas();
                    if (result=="OK")
                    {
                        if (clu.iFirmaElectronicaSalidas==1)
                            bbiListaPorAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        else
                            bbiListaPorAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo llenarCajasUsuarios: " + globalCL.gv_UsuarioID.ToString());
                    }   
                }
                else
                    bbiListaPorAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }


           
        }

        private void AgregaAñosNavBar()
        {
            try
            {
                globalCL cl = new globalCL();
                System.Data.DataTable dt = new System.Data.DataTable("Años");
                dt.Columns.Add("Año", Type.GetType("System.Int32"));
                cl.strTabla = "EyS";
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

        private void LlenarGrid(int Año, int Mes)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Cargando información...");
            EntradasysalidasCL cl = new EntradasysalidasCL();
            cl.intAño = Año;
            cl.intMes = Mes;
            gridControlPrincipal.DataSource = cl.EntradasysalidasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridEntradasysalidas";
            clg.restoreLayout(gridViewPrincipal);
            gridViewPrincipal.ViewCaption = "ENTRADAS Y SALIDAS DE " + clg.NombreDeMes(MesFiltro, 0) + " DEL " + AñoFiltro.ToString();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        } //LlenarGrid()

        public class detalleCL
        {
            public decimal Cantidad { get; set; }
            public string Articulo { get; set; }
            public string Descripcion { get; set; }
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
            

            List<tipoCL> tipoL = new List<tipoCL>();

            tipoL.Add(new tipoCL() { Clave = "E", Des = "Entrada" });
            tipoL.Add(new tipoCL() { Clave = "S", Des = "Salida" });
            cboOperacion.Properties.ValueMember = "Clave";
            cboOperacion.Properties.DisplayMember = "Des";
            cboOperacion.Properties.DataSource = tipoL;
            cboOperacion.Properties.ForceInitialize();
            cboOperacion.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboOperacion.Properties.PopulateColumns();
            cboOperacion.Properties.Columns["Clave"].Visible = false;
            cboOperacion.ItemIndex = 0;

            cl.strTabla = "Serie";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Des";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Des"].Visible = false;
            cboSerie.ItemIndex = 0;
            
            cl.strTabla = "Almacenes";
            cboAlmacenesID.Properties.ValueMember = "Clave";
            cboAlmacenesID.Properties.DisplayMember = "Des";
            cboAlmacenesID.Properties.DataSource = cl.CargaCombos();
            cboAlmacenesID.Properties.ForceInitialize();
            cboAlmacenesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacenesID.Properties.PopulateColumns();
            cboAlmacenesID.Properties.Columns["Clave"].Visible = false;
            cboAlmacenesID.ItemIndex = 0;



            cl.strTabla = "NoKits";
            repositoryItemLookUpEditArticulo.ValueMember = "Clave";
            repositoryItemLookUpEditArticulo.DisplayMember = "Des";
            repositoryItemLookUpEditArticulo.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulo.ForceInitialize();
            repositoryItemLookUpEditArticulo.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditArticulo.NullText = "Seleccione un articulo";
        }

        public class tipoCL
        {
            public string Clave { get; set; }
            public string Des { get; set; }
        }

        private string BuscarSerie()
        {

            cboSerie.ItemIndex = 0;
            return "";

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

        private void Nuevo()
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Entradas y salidas","Configurando detalle...");
            Inicialisalista();
            LimpiaCajas();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCanceladas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navBarControl.Visible = false;
           
            navigationFrame.SelectedPageIndex = 1;


            //Se obtiene el Id de la cotizacion
            blNuevo = true;
            
            SiguienteID();
           
            intTiposdemovimientoinvID = 0;
            intFolio = 0;
            cboOperacion.ItemIndex = 0;
            cboAlmacenesID.ItemIndex = 0;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridEntradasysalidasdetalle";
            clg.restoreLayout(gridViewDetalle);

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LimpiaCajas()
        {
            cboTiposdemovimientoinvID.EditValue = null;
            dateEditFech.Text = DateTime.Now.ToShortDateString();
            cboAlmacenesID.ItemIndex = 0;
            txtObservaciones.Text = string.Empty;
            lblTotal.Text = string.Empty;
        }

        private void SiguienteID()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            EntradasysalidasCL cl = new EntradasysalidasCL();
            cl.strSerie = cboSerie.EditValue.ToString();
            cl.strDoc = "Entradasysalidas";
            cl.intTiposdemovimientoinvID = Convert.ToInt32(cboTiposdemovimientoinvID.EditValue);
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
                System.Data.DataTable dtEntradasysalidas = new System.Data.DataTable("Entradasysalidas");
                dtEntradasysalidas.Columns.Add("Serie", Type.GetType("System.String"));
                dtEntradasysalidas.Columns.Add("TiposdemovimientoinvID", Type.GetType("System.Int32"));
                dtEntradasysalidas.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtEntradasysalidas.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtEntradasysalidas.Columns.Add("AlmacenesID", Type.GetType("System.Int32"));
                dtEntradasysalidas.Columns.Add("Observaciones", Type.GetType("System.String"));
                dtEntradasysalidas.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtEntradasysalidas.Columns.Add("Operacion", Type.GetType("System.String"));
                dtEntradasysalidas.Columns.Add("Autorizo", Type.GetType("System.Int32"));
                dtEntradasysalidas.Columns.Add("UsuarioAutorizo", Type.GetType("System.Int32"));
                dtEntradasysalidas.Columns.Add("FechaAutorizacion", Type.GetType("System.DateTime"));


                System.Data.DataTable dtEntradasysalidasdetalle = new System.Data.DataTable("Entradasysalidasdetalle");
                dtEntradasysalidasdetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtEntradasysalidasdetalle.Columns.Add("TiposdemovimientoinvID", Type.GetType("System.Int32"));
                dtEntradasysalidasdetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtEntradasysalidasdetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtEntradasysalidasdetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtEntradasysalidasdetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));

                if (blNuevo)
                {
                    SiguienteID();   //Se vuelve a llamar por que se usará multiusuario
                }

                bool enviarcorreo = false;

                int autorizo = 0;
                int usuAut = 0;
                
                if (blnfirmaElectronica)
                {
                    autorizo = 1;
                    usuAut = globalCL.gv_UsuarioID;
                    enviarcorreo = false;
                }
                else
                {
                    autorizo = 0;
                    usuAut = 0;
                    enviarcorreo = true;
                }


                string dato = String.Empty;
                strSerie = cboSerie.EditValue.ToString();
                intTiposdemovimientoinvID = Convert.ToInt32(cboTiposdemovimientoinvID.EditValue);
                intFolio = Convert.ToInt32(txtFolio.Text);
                int intSeq = 0;
                decimal dCantidad = 0;
                int intArticulosID = 0;
                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "Articulo").ToString();
                    if (dato.Length > 0)
                    {
                        intSeq = i;
                        dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Cantidad"));
                        intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Articulo"));

                        dtEntradasysalidasdetalle.Rows.Add(strSerie, intTiposdemovimientoinvID, intFolio, intSeq, dCantidad, intArticulosID);

             
                    }
                }

                DateTime fFecha = Convert.ToDateTime(dateEditFech.Text);
                int intAlmacenesID = Convert.ToInt32(cboAlmacenesID.EditValue);
                string strObservaciones = txtObservaciones.Text;
                string strOperacion = cboOperacion.EditValue.ToString();
                dato = String.Empty;
                dtEntradasysalidas.Rows.Add(strSerie,
                    intTiposdemovimientoinvID,
                    intFolio,
                    fFecha,
                    intAlmacenesID,
                    strObservaciones,
                    intUsuarioID,
                    strOperacion,
                    autorizo,
                    usuAut,
                    fFecha                    
                    );

                EntradasysalidasCL cl = new EntradasysalidasCL();
                cl.dtm = dtEntradasysalidas;
                cl.dtd = dtEntradasysalidasdetalle;
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.intUsuarioID = intUsuarioID;
                cl.strMaquina = Environment.MachineName;
                cl.intAutorizo = autorizo;

                Result = cl.EntradasysalidasCrud();

                if (Result == "OK")
                {
                    MessageBox.Show("Guardado correctamente");
                    Imprime();

                    if (cl.strOperacion == "S" && enviarcorreo)
                        EnviaSolicitudPorCorreo(cboSerie.EditValue.ToString(), Convert.ToInt32(txtFolio.Text));

                    LimpiaCajas();
                    Inicialisalista();
                }
                else
                {
                    MessageBox.Show("Crud: " + Result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        } //Guardar

        private void EnviaSolicitudPorCorreo(string ser,int fol)
        {
            try
            {
                globalCL clg = new globalCL();
                string result = clg.EnviaCorreoSalida("rgonzalez@diexsa.com", ser, fol);
                if (result != "OK")
                    MessageBox.Show(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Al enviar correo:" + ex.Message);
            }
        }

        private string Valida()
        {
         
            if (cboTiposdemovimientoinvID.EditValue == null)
            {
                return "El campo TiposdemovimientoinvID no puede ir vacio";
            }
            if (cboAlmacenesID.EditValue == null)
            {
                return "El campo AlmacenesID no puede ir vacio";
            }
            if (txtObservaciones.Text.Length == 0)
            {
                return "El campo Observaciones no puede ir vacio";
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

            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(dateEditFech.Text).Year, Convert.ToDateTime(dateEditFech.Text).Month, "INV");
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";                
            }

            return "OK";
        } //Valida

        private void llenaCajas()
        {
            EntradasysalidasCL cl = new EntradasysalidasCL();
            cl.strSerie = strSerie;
            cl.intTiposdemovimientoinvID = intTiposdemovimientoinvID;
            cl.intFolio = intFolio;
            String Result = cl.EntradasysalidasLlenaCajas();
            if (Result == "OK")
            {
                dateEditFech.Text = cl.fFecha.ToShortDateString();
                cboSerie.EditValue = cl.strSerie;
                txtFolio.Text = cl.intFolio.ToString();
                txtObservaciones.Text = cl.strObservaciones;
                cboTiposdemovimientoinvID.EditValue = cl.intTiposdemovimientoinvID;
                cboOperacion.EditValue = cl.strOperacion;
                cboAlmacenesID.EditValue = cl.intAlmacenesID;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();
            DetalleLlenaCajas();
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridEntradasysalidasdetalle";
            clg.restoreLayout(gridViewDetalle);
        }

        private void BotonesEdicion()
        {
            LimpiaCajas();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiListaPorAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            navigationFrame.SelectedPageIndex = 1;
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

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            navBarControl.Visible = true;

            navBarControl.Visible = true;

            VerificaPorAutorizar();

            //LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
            blNuevo = false;
            intFolio = 0;
            strSerie = null;
            intTiposdemovimientoinvID = 0;


        }

        private void DetalleLlenaCajas()
        {
            try
            {
                EntradasysalidasCL cl = new EntradasysalidasCL();
                cl.intFolio = intFolio;
                cl.strSerie = strSerie;
                cl.intTiposdemovimientoinvID = intTiposdemovimientoinvID;
                gridControlDetalle.DataSource = cl.EntradasysalidasDetalleGrid();

                int total = 0;
                for (int i = 0; i <= gridViewDetalle.RowCount; i++)
                {
                    int importe = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Importe"));
                    total += importe;
                }
                lblTotal.Text = "Total: $" + total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }

        }//DetalleLlenaCajas

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridEntradasysalidas";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

           
            this.Close();
        }

        private void bbiVistaP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridViewPrincipal.ShowRibbonPrintPreview();
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                this.gridViewDetalle.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewDetalle_CellValueChanged);
                //Sacamos datos del artículo
                if (e.Column.Name == "gridColumnArticulo")
                {
                    articulosCL cl = new articulosCL();
                    string art = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulo").ToString();
                    //validamos que haya algo en la celda
                    if (art.Length > 0)
                    {
                        cl.intArticulosID = Convert.ToInt32(art);
                        string result = cl.articulosLlenaCajas();
                        if (result == "OK")
                        {
                            gridViewDetalle.SetFocusedRowCellValue("Descripcion", cl.intArticulosID);
                            string desc = gridViewDetalle.GetRowCellDisplayText(gridViewDetalle.FocusedRowHandle, "Descripcion");
                            if(desc.Length <= 0 )
                            {
                                gridViewDetalle.SetFocusedRowCellValue("Articulo", "");
                                MessageBox.Show("Artículo no encontrado");
                            }
                        }

                    }
                }
                if(e.Column.Name == "gridColumnDescripcion")
                {
                    articulosCL cl = new articulosCL();
                    int articulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Descripcion"));
                    if(articulosID > 0)
                    {
                        cl.intArticulosID = articulosID;
                        string result = cl.articulosLlenaCajas();
                        if(result == "OK")
                            gridViewDetalle.SetFocusedRowCellValue("Articulo", cl.intArticulosID);
                    }
                }
                this.gridViewDetalle.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewDetalle_CellValueChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show("GridviewDetalle Changed: " + ex);
            }
        }
       

        #region gridPrincipal
        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            string strStatus = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Estado"));
            intStatus = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status"));
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            intTiposdemovimientoinvID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "TiposdemovimientoinvID"));
            if (strStatus == "Cancelada" || strStatus == "Rechazada")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else if (strStatus == "Registrada")
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                ////   bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            

        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                string status = string.Empty;
                    //status = view.GetRowCellValue(e.RowHandle, "Estado").ToString();

                if (status == "Cancelada")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                e.Appearance.ForeColor = Color.Black;
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
        #endregion

        #region Filtros de bbi inferior
        private void bbiRegistradas_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Registrada'";
        }

        private void bbiAceptadas_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Aceptada'";
        }

        private void bbiRechazadas_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Rechazada'";
        }

        private void bbiExpiradas_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Estado]='Expirada'";
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

        #region Filtros de bbi Meses
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

        #region Imprimir
        private void Imprime()
        {

            try
            {
                RibbonPagePrint.Visible = true;
                ribbonPage1.Visible = false;
                navigationFrame.SelectedPageIndex = 2;
               

                reporte();
                navBarControl.Visible = false;
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Imprime: " + ex.Message);
            }

        }
        private void bbiIImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }

            Imprime();


        }

        private void reporte()
        {
            try
            {
                EntradasysalidasformatoImpresion report = new EntradasysalidasformatoImpresion();
                report.Parameters["parameter1"].Value = strSerie;
                report.Parameters["parameter2"].Value = intTiposdemovimientoinvID;
                report.Parameters["parameter3"].Value = intFolio;
                report.Parameters["parameter1"].Visible = false;
                report.Parameters["parameter2"].Visible = false;
                report.Parameters["parameter3"].Visible = false;

                this.documentViewer1.DocumentSource = report;
                report.CreateDocument(false);
                documentViewer1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        #endregion

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

        private void cboOperacion_EditValueChanged(object sender, EventArgs e)
        {
            CargaComboTipodemovimientoinv();
        }

        private void CargaComboTipodemovimientoinv()
        {
            combosCL cl = new combosCL();
            if (cboOperacion.EditValue.ToString()=="E")
                cl.strTabla = "TiposdemovimientoinvE";
            else
                cl.strTabla = "TiposdemovimientoinvS";

            cboTiposdemovimientoinvID.Properties.ValueMember = "Clave";
            cboTiposdemovimientoinvID.Properties.DisplayMember = "Des";
            cboTiposdemovimientoinvID.Properties.DataSource = cl.CargaCombos();
            cboTiposdemovimientoinvID.Properties.ForceInitialize();
            cboTiposdemovimientoinvID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTiposdemovimientoinvID.Properties.PopulateColumns();
            cboTiposdemovimientoinvID.Properties.Columns["Clave"].Visible = false;
            cboTiposdemovimientoinvID.ItemIndex = 0;
        }

        private void cboTiposdemovimientoinvID_EditValueChanged(object sender, EventArgs e)
        {
            if (blNuevo)
            {
                SiguienteID();
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

        private void bbiAutorizar_ItemClick(object sender, ItemClickEventArgs e)
        {
            groupControl3.Text = "Autorizar salida: " + intFolio.ToString();
            popUpCancelar.Show();
        }

        private void Muestraporautorizar()
        {
            EntradasysalidasCL cl = new EntradasysalidasCL();
            gridControlPorAutorizar.DataSource = cl.EntradasysalidasPorAutorizarLista();
            navigationFrame.SelectedPageIndex = 3;

            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiListaPorAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiAutorizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navBarControl.Visible = false;
        }

        private void gridViewPorAutorizar_RowClick(object sender, RowClickEventArgs e)
        {
            intFolio = Convert.ToInt32(gridViewPorAutorizar.GetRowCellValue(gridViewPorAutorizar.FocusedRowHandle, "Folio"));
            intTM = Convert.ToInt32(gridViewPorAutorizar.GetRowCellValue(gridViewPorAutorizar.FocusedRowHandle, "TiposdemovimientoinvID"));
            
            EntradasysalidasCL cl = new EntradasysalidasCL();
            cl.strSerie = "";
            cl.intFolio = intFolio;
            cl.intTiposdemovimientoinvID = intTM;
            gridControlPorAutDetalle.DataSource = cl.EntradasysalidasDetalleGrid();
        }

        private void bbiListaPorAutorizar_ItemClick(object sender, ItemClickEventArgs e)
        {
            Muestraporautorizar();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            popUpCancelar.Hide();
        }

        private void btnAut_Click(object sender, EventArgs e)
        {
            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLogin.Text;
            clU.strClave = txtPassword.Text;
            clU.strPermiso = "FirmaElectronicaSalidas";
            string result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }
            //intUsuarioID = clU.intUsuariosID;
            popUpCancelar.Hide();
            AutorizaSalida();
        }

        private void AutorizaSalida()
        {
            EntradasysalidasCL cl = new EntradasysalidasCL();
            cl.strSerie = "";
            cl.intFolio = intFolio;
            cl.intTiposdemovimientoinvID = intTM;
            cl.intUsuarioID = globalCL.gv_UsuarioID;
            string result = cl.EntradasysalidasAutoriza();
            if (result == "OK")
            {
                MessageBox.Show("Salida autorizada correctamente");
                gridControlPorAutorizar.DataSource = cl.EntradasysalidasPorAutorizarLista();
                gridControlPorAutDetalle.DataSource = null;
            }
            else
                MessageBox.Show(result);
        }

        private void bbiRegresarImp_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            navigationFrame.SelectedPageIndex = 0;
            navBarControl.Visible = true;
           
            ribbonPage1.Visible = true;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText("Home");
            RibbonPagePrint.Visible = false;

            intFolio = 0;
            strSerie = string.Empty;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridEntradasysalidasdetalle";
            string result = clg.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
        }





    }
}