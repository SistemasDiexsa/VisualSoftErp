using DevExpress.Charts.Native;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraNavBar;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Inventario.Designers;

namespace VisualSoftErp.Operacion.Inventario.Formas
{
    public partial class Produccion2Form : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region VARIABLES
        private bool permisosEscritura;
        List<int> intArticulosIDUtilizados = new List<int>();
        string strSerie;
        int intFolio;
        int intSeq;
        int AñoFiltro;
        int MesFiltro;
        private BindingList<detalleUtilizadoCL> detalleUtilizado;
        private BindingList<detalleProducidoCL> detalleProducido;
        private DataTable dtDetalleUtilizado;
        private DataTable dtDetalleProducido;
        private class detalleUtilizadoCL
        {
            /// <summary>
            /// </summary>
            public int intProduccionID { get; set; }
            public int intDetalleID { get; set; }
            public string strSerie { get; set; }
            public int intFolio { get; set; }
            public int intArticulosID { get; set; }
            public int intAlmacenesID { get; set; }
            public decimal decCantidad { get; set; }
            public string strCodigoArticulo { get; set; }
            public string strLetra { get; set; }
        }
        private class detalleProducidoCL
        {
            /// <summary>
            /// </summary>
            public int intProduccionID { get; set; }
            public int intDetalleID { get; set; }
            public string strSerie { get; set; }
            public int intFolio { get; set; }
            public int intArticulosID { get; set; }
            public int intAlmacenesID { get; set; }
            public decimal decCantidad { get; set; }
            public string strCodigoArticulo { get; set; }
            public string strLetra { get; set; }
            public int intMaquina { get; set; }
        }
        #endregion VARIABLES

        public Produccion2Form()
        {
            InitializeComponent();
        }

        #region LOAD
        private void Produccion2_Load(object sender, EventArgs e)
        {
            PermisosEscritura();
            InitGridPrincipal();
            InitGridDetalleUtilizado();
            InitGridDetalleProducido();
            BuscarSerie();
            BotonesEdicion(false);
            dateFecha.Text = DateTime.Now.ToShortDateString();
            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;
            LlenarGridPrincipal();
            CargaCombos();
            AgregaAñosNavBar();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        #endregion LOAD

        #region INIT GRID
        private void InitGridPrincipal()
        {
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ActiveFilter.Clear();
        }
        private void InitGridDetalleUtilizado()
        {
            detalleUtilizado = new BindingList<detalleUtilizadoCL>();
            detalleUtilizado.AllowNew = true;
            detalleUtilizado.AllowEdit = true;
            detalleUtilizado.AllowRemove = true;

            gridControlDetalleUtilizado.DataSource = detalleUtilizado;

            gridColumnAlmacenesUtilizado.ColumnEdit = repositoryItemLookUpEditAlmacenesUtilizado;
            gridColumnArticuloUtilizado.ColumnEdit = repositoryItemLookUpEditArticuloUtilizado;
            gridColumnArticuloUtilizadoDes.OptionsColumn.ReadOnly = true;
            gridColumnArticuloUtilizadoDes.OptionsColumn.AllowFocus = false;

            gridViewDetalleUtilizado.ViewCaption = "Articulos Utilizados";
            gridViewDetalleUtilizado.OptionsView.ShowViewCaption = true;
            gridViewDetalleUtilizado.OptionsView.ShowGroupPanel = false;
            gridViewDetalleUtilizado.OptionsView.ShowFooter = true;
            gridViewDetalleUtilizado.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalleUtilizado.OptionsCustomization.AllowColumnMoving = false;
            gridViewDetalleUtilizado.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalleUtilizado.OptionsNavigation.AutoMoveRowFocus = true;
        }
        private void InitGridDetalleProducido()
        {
            detalleProducido = new BindingList<detalleProducidoCL>();
            detalleProducido.AllowNew = true;
            detalleProducido.AllowEdit = true;
            detalleProducido.AllowRemove = true;

            gridControlDetalleProducido.DataSource = detalleProducido;

            gridColumnAlmacenesProducido.ColumnEdit = repositoryItemLookUpEditAlmacenesProducido;
            gridColumnArticuloProducido.ColumnEdit = repositoryItemLookUpEditArticuloProducido;
            gridColumnArticuloProducidoDes.OptionsColumn.ReadOnly = true;
            gridColumnArticuloProducidoDes.OptionsColumn.AllowFocus = false;

            gridViewDetalleProducido.ViewCaption = "Articulos Producidos";
            gridViewDetalleProducido.OptionsView.ShowViewCaption = true;
            gridViewDetalleProducido.OptionsView.ShowGroupPanel = false;
            gridViewDetalleProducido.OptionsView.ShowFooter = true;
            gridViewDetalleProducido.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalleProducido.OptionsCustomization.AllowColumnMoving = false;
            gridViewDetalleProducido.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalleProducido.OptionsNavigation.AutoMoveRowFocus = true;
        }
        #endregion INIT GRID

        #region NAVIGATION
        private void navBarControl_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            NavBarItemLink item = e.Link;

            if (item != null)
            {
                globalCL clg = new globalCL();
                string Name = item.ItemName;
                if (clg.esNumerico(Name))
                    AñoFiltro = Convert.ToInt32(Name);
                else
                {
                    switch (Name)
                    {
                        case "navBarItemEnero":
                            MesFiltro = 1;
                            break;
                        case "navBarItemFebrero":
                            MesFiltro = 2;
                            break;
                        case "navBarItemMarzo":
                            MesFiltro = 3;
                            break;
                        case "navBarItemAbril":
                            MesFiltro = 4;
                            break;
                        case "navBarItemMayo":
                            MesFiltro = 5;
                            break;
                        case "navBarItemJunio":
                            MesFiltro = 6;
                            break;
                        case "navBarItemJulio":
                            MesFiltro = 7;
                            break;
                        case "navBarItemAgosto":
                            MesFiltro = 8;
                            break;
                        case "navBarItemSeptiembre":
                            MesFiltro = 9;
                            break;
                        case "navBarItemOctubre":
                            MesFiltro = 10;
                            break;
                        case "navBarItemNoviembre":
                            MesFiltro = 11;
                            break;
                        case "navBarItemDiciembre":
                            MesFiltro = 12;
                            break;
                        case "navBarItemTodos":
                            MesFiltro = 0;
                            break;
                    }
                }
                LlenarGridPrincipal();
            }
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        #endregion NAVIGATION

        #region BOTONES
        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }
        private void bbiVer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
                MessageBox.Show("Selecciona un renglón");
            else
                Ver();
        }
        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }
        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
                MessageBox.Show("Seleccione un renglón");
            else
                Cancelar();
        }
        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
                MessageBox.Show("Selecciona un renglón");
            else
                Imprimir();
        }
        private void bbiRegresarGrid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Regresar();
        }
        #endregion BOTONES

        #region INTERACCIONES USUARIO
        private void gridViewPrincipal_Click(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            if (view != null)
            {
                strSerie = view.GetRowCellValue(view.FocusedRowHandle, "Serie").ToString();
                intFolio = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Folio"));
            }
        }
        private void gridViewPrincipal_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            if (view != null)
            {
                strSerie = view.GetRowCellValue(view.FocusedRowHandle, "Serie").ToString();
                intFolio = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Folio"));
                Ver();
            }
        }
        private void gridViewPrincipal_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                GridView view = (GridView)sender;
                string status = view.GetRowCellValue(e.RowHandle, "Status").ToString();

                if (status == "Cancelado")
                    e.Appearance.ForeColor = Color.Red;
            }
        }
        private void gridControlDetalle_ProcessGridKey(object sender, KeyEventArgs e)
        {
            GridControl control = sender as GridControl;
            GridView view = control.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                if (view == gridViewDetalleUtilizado)
                {
                    int intArticulosID = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "intArticulosID"));
                    intArticulosIDUtilizados.Remove(intArticulosID);
                    CargarCombosArticuloProducido();
                }
                view.DeleteRow(view.FocusedRowHandle);
                e.Handled = true;
            }
        }
        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = (GridView)sender;
            if (view != null)
            {
                try
                {
                    int articulosIDFor = 0;
                    int intArticulosID = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "intArticulosID"));

                    if (e.Column == gridColumnArticuloUtilizado)
                    {
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            object row = view.GetRow(i);
                            if (row != null)
                            {
                                articulosIDFor = Convert.ToInt32(view.GetRowCellValue(i, "intArticulosID"));
                                if (i != view.FocusedRowHandle)
                                {
                                    if (articulosIDFor == intArticulosID)
                                    {
                                        view.SetRowCellValue(view.FocusedRowHandle, "intArticulosID", 0);
                                        MessageBox.Show("Este artículo ya fué seleccionado.\nFavor de modificar la cantidad utilizada", "Articulo Repetido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                        }

                        DataRowView dataRowRepositoryItem = repositoryItemLookUpEditArticuloUtilizado.GetDataSourceRowByKeyValue(intArticulosID) as DataRowView;
                        if (intArticulosID == 0)
                            view.SetRowCellValue(view.FocusedRowHandle, gridColumnArticuloUtilizadoDes, "");
                        else
                            view.SetRowCellValue(view.FocusedRowHandle, gridColumnArticuloUtilizadoDes, dataRowRepositoryItem["Articulo"].ToString());
                    }

                    else if (e.Column == gridColumnArticuloProducido)
                    {
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            object row = view.GetRow(i);
                            if (row != null)
                            {
                                articulosIDFor = Convert.ToInt32(view.GetRowCellValue(i, "intArticulosID"));
                                if (i != view.FocusedRowHandle)
                                {
                                    if (articulosIDFor == intArticulosID)
                                    {
                                        view.SetRowCellValue(view.FocusedRowHandle, "intArticulosID", 0);
                                        MessageBox.Show("Este artículo ya fué seleccionado.\nFavor de modificar la cantidad producida", "Articulo Repetido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                        }

                        DataRowView dataRowRepositoryItem = repositoryItemLookUpEditArticuloProducido.GetDataSourceRowByKeyValue(intArticulosID) as DataRowView;
                        if (intArticulosID == 0)
                            view.SetRowCellValue(view.FocusedRowHandle, gridColumnArticuloProducidoDes, "");
                        else
                            view.SetRowCellValue(view.FocusedRowHandle, gridColumnArticuloProducidoDes, dataRowRepositoryItem["Articulo"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Message: \n" + ex.Message + "\n\nTrace: \n" + ex.StackTrace, "Error en línea " + ex.LineNumber(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void gridViewDetalle_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            GridView view = (GridView)sender;
            if (view != null)
            {
                if (view == gridViewDetalleUtilizado)
                {
                    GuardarArticulosIDUtilizados();
                    CargarCombosArticuloProducido();
                }
            }
        }
        #endregion INTERACCIONES USUARIO

        private void PermisosEscritura()
        {
            globalCL clg = new globalCL();
            UsuariosCL usuarios = new UsuariosCL();

            clg.strPrograma = "0344";
            if (clg.accesoSoloLectura()) permisosEscritura = false;
            else permisosEscritura = true;
        }

        private void BuscarSerie()
        {
            SerieCL cl = new SerieCL();
            cl.intUsuarioID = globalCL.gv_UsuarioID;
            string result = cl.BuscarSerieporUsuario();
            if (result == "OK")
            {
                if (cl.strSerie == "")
                    cboSerie.ReadOnly = false;
                else
                {
                    cboSerie.EditValue = cl.strSerie;
                    cboSerie.ReadOnly = true;
                }
            }
            else
                MessageBox.Show(result);
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
                navBarControlGridPrincipal.ActiveGroup = navBarGroupMeses;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AgregaAñosNavBar:" + ex.Message);
            }
        }

        private void GuardarArticulosIDUtilizados()
        {
            GridView view = gridViewDetalleUtilizado;
            if (view != null)
            {
                intArticulosIDUtilizados.Clear();
                for (int i = 0; i <= view.RowCount; i++)
                {
                    object row = view.GetRow(i);
                    if (row != null)
                    {
                        int intArticulosID = Convert.ToInt32(view.GetRowCellValue(i, "intArticulosID"));
                        intArticulosIDUtilizados.Add(intArticulosID);
                    }
                }
            }
        }

        private void LlenarGridPrincipal()
        {
            CortesCL cl = new CortesCL();
            cl.intAño = AñoFiltro;
            cl.intMes = MesFiltro;
            gridControlPrincipal.DataSource = cl.ProduccionGrid();

            globalCL clg = new globalCL();
            string strGridCaption = "Producción de ";
            if (MesFiltro == 0)
                strGridCaption += "todo " + AñoFiltro.ToString();
            else
                strGridCaption += clg.NombreDeMesCompleto(MesFiltro).ToLower() + " del " + AñoFiltro.ToString();

            gridViewPrincipal.ViewCaption = strGridCaption;
            clg.strGridLayout = "gridCortes";
            clg.restoreLayout(gridViewPrincipal);
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();

            #region SERIE
            cl.strTabla = "Serie";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Clave";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Des"].Visible = false;
            cboSerie.EditValue = cboSerie.Properties.GetDataSourceValue(cboSerie.Properties.ValueMember, 0);
            #endregion SERIE

            #region TURNO
            DataTable dt = new DataTable();
            dt.Columns.Add("Clave", typeof(int));
            dt.Columns.Add("Des", typeof(string));
            dt.Rows.Add(1, "Matutino");
            dt.Rows.Add(2, "Vespertino");
            cboTurno.Properties.ValueMember = "Clave";
            cboTurno.Properties.DisplayMember = "Des";
            cboTurno.Properties.DataSource = dt;
            cboTurno.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTurno.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            cboTurno.Properties.ForceInitialize();
            cboTurno.Properties.PopulateColumns();
            cboTurno.Properties.Columns["Clave"].Visible = false;
            cboTurno.EditValue = null;
            cboTurno.Properties.NullText = "Seleccione el turno laboral";
            #endregion TURNO

            #region ARTICULO UTILIZADO
            cl.strTabla = "ArticulosProduccionPeliculas";
            repositoryItemLookUpEditArticuloUtilizado.ValueMember = "Clave";
            repositoryItemLookUpEditArticuloUtilizado.DisplayMember = "Des";
            repositoryItemLookUpEditArticuloUtilizado.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticuloUtilizado.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticuloUtilizado.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditArticuloUtilizado.NullText = "Seleccione el articulo a cortar";
            repositoryItemLookUpEditArticuloUtilizado.ForceInitialize();
            repositoryItemLookUpEditArticuloUtilizado.Columns["Clave"].Visible = false;
            repositoryItemLookUpEditArticuloUtilizado.Columns["Articulo"].Visible = false;
            repositoryItemLookUpEditArticuloUtilizado.Columns["FactorUM2"].Visible = false;
            repositoryItemLookUpEditArticuloUtilizado.Columns["ArticulobaseparacosteoID"].Visible = false;
            repositoryItemLookUpEditArticuloUtilizado.Columns["Largo"].Visible = false;
            #endregion ARTCULO UTILIZADO

            #region ARTICULO PRODUCIDO
            CargarCombosArticuloProducido();
            #endregion ARTICULO PRODUCIDO

            #region ALMACEN UTILIZADO
            cl.strTabla = "Almacenes";
            repositoryItemLookUpEditAlmacenesUtilizado.ValueMember = "Clave";
            repositoryItemLookUpEditAlmacenesUtilizado.DisplayMember = "Des";
            repositoryItemLookUpEditAlmacenesUtilizado.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditAlmacenesUtilizado.ForceInitialize();
            repositoryItemLookUpEditAlmacenesUtilizado.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditAlmacenesUtilizado.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditAlmacenesUtilizado.Columns["Clave"].Visible = false;
            repositoryItemLookUpEditAlmacenesUtilizado.NullText = "Seleccione el almacen";
            #endregion ALMACEN UTILIZADO

            #region ALMACEN PRODUCIDO
            cl.strTabla = "Almacenes";
            repositoryItemLookUpEditAlmacenesProducido.ValueMember = "Clave";
            repositoryItemLookUpEditAlmacenesProducido.DisplayMember = "Des";
            repositoryItemLookUpEditAlmacenesProducido.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditAlmacenesProducido.ForceInitialize();
            repositoryItemLookUpEditAlmacenesProducido.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditAlmacenesProducido.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditAlmacenesProducido.Columns["Clave"].Visible = false;
            repositoryItemLookUpEditAlmacenesProducido.NullText = "Seleccione el almacen";
            #endregion ALMACEN PRODUCIDO

            #region MAQUINA
            dt = new DataTable();
            dt.Columns.Add("Clave", typeof(int));
            dt.Columns.Add("Des", typeof(string));
            dt.Rows.Add(1, "Máquina 1");
            dt.Rows.Add(2, "Máquina 2");
            repositoryItemLookUpEditMaquina.ValueMember = "Clave";
            repositoryItemLookUpEditMaquina.DisplayMember = "Des";
            repositoryItemLookUpEditMaquina.DataSource = dt;
            repositoryItemLookUpEditMaquina.ForceInitialize();
            repositoryItemLookUpEditMaquina.PopulateColumns();
            repositoryItemLookUpEditMaquina.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditMaquina.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditMaquina.Columns["Clave"].Visible = false;
            repositoryItemLookUpEditMaquina.NullText = "Seleccione la máquina que utilizó";
            #endregion MAQUINA
        }

        private void CargarCombosArticuloProducido()
        {
            if (intArticulosIDUtilizados.Count > 0)
            {
                combosCL cl = new combosCL();
                cl.strTabla = "ArticulosHijosYComponentes";
                cl.strCondicion = String.Join(",", intArticulosIDUtilizados);
                repositoryItemLookUpEditArticuloProducido.ValueMember = "Clave";
                repositoryItemLookUpEditArticuloProducido.DisplayMember = "Des";
                repositoryItemLookUpEditArticuloProducido.DataSource = cl.CargaCombos();
                repositoryItemLookUpEditArticuloProducido.ForceInitialize();
                repositoryItemLookUpEditArticuloProducido.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                repositoryItemLookUpEditArticuloProducido.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                repositoryItemLookUpEditArticuloProducido.NullText = "Seleccione el articulo a producir";
                repositoryItemLookUpEditArticuloProducido.Columns["Largo"].Visible = false;
                repositoryItemLookUpEditArticuloProducido.Columns["Clave"].Visible = false;
                repositoryItemLookUpEditArticuloProducido.Columns["FactorUM2"].Visible = false;
                repositoryItemLookUpEditArticuloProducido.Columns["Articulo"].Visible = false;
                repositoryItemLookUpEditArticuloProducido.Columns["ArticulobaseparacosteoID"].Visible = false;
            }
            else
                repositoryItemLookUpEditArticuloProducido.NullText = "Seleccione primero los arículos que utilizará";
        }

        private void BotonesEdicion(bool isEditing)
        {
            if (isEditing)
            {
                bbiNuevo.Enabled = false;
                bbiVer.Enabled = false;
                bbiGuardar.Enabled = (intFolio == 0 && permisosEscritura) ? true : false;
                bbiCancelar.Enabled = (intFolio == 0 && permisosEscritura) ? false : true;
                bbiImprimir.Enabled = (intFolio == 0 && permisosEscritura) ? false : true;
                bbiRegresar.Enabled = true;
            }
            else
            {
                bbiNuevo.Enabled = true;
                bbiVer.Enabled = true;
                bbiGuardar.Enabled = false;
                bbiCancelar.Enabled = true;
                bbiImprimir.Enabled = true;
                bbiRegresar.Enabled = false;
            }
        }

        private void LimpiaCajas()
        {
            dateFecha.Text = DateTime.Now.ToShortDateString();
            cboTurno.EditValue = null;
            txtElaboro.Text = string.Empty;
            txtObservaciones.EditValue = null;
            detalleUtilizado.Clear();
            detalleProducido.Clear();
        }

        private void LlenaCajas()
        {
            try
            {
                CortesCL cl = new CortesCL();
                cl.intFolio = intFolio;
                cl.strSerie = strSerie;

                string result = cl.ProduccionLlenaCajas();
                if (result != "OK")
                    throw new Exception(result);

                dtDetalleUtilizado = cl.detalleUtilizado;
                dtDetalleProducido = cl.detalleProducido;
                LlenarBindingListDetalle();
                txtElaboro.Text = cl.strElaboro;
                cboTurno.EditValue = cl.intTurno;
                txtObservaciones.Text = cl.strObservaciones;
                cboSerie.EditValue = cl.strSerie;
                txtFolio.Text = cl.intFolio.ToString();
                dateFecha.Text = cl.fFecha.ToShortDateString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }
        }

        private string Valida()
        {
            if (gridViewDetalleUtilizado.IsEditing)
            {
                gridViewDetalleUtilizado.PostEditor();
                gridViewDetalleUtilizado.UpdateCurrentRow();
            }

            if (gridViewDetalleProducido.IsEditing)
            {
                gridViewDetalleProducido.PostEditor();
                gridViewDetalleProducido.UpdateCurrentRow();
            }

            if (cboSerie.EditValue == null)
                return "Seleccione la serie de la producción";
            if (txtFolio.Text == string.Empty)
                return "El campo Folio no puede ir vacio";
            if (txtElaboro.Text.Length == 0)
                return "Escriba quién elaboró esta producción";
            if (cboTurno.EditValue == null || Convert.ToInt32(cboTurno.EditValue) == 0)
                return "Seleccione el turno en el que se hizo la producción";

            if (dateFecha.DateTime.Date != DateTime.Now.Date)
            {
                DialogResult dialog = MessageBox.Show("La fecha que se ingresó no es la fecha actual.\n¿ Desea Continuar ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dialog != DialogResult.Yes)
                    return "";
            }

            return CuadrarUtilizadoConProducido();
        }

        private string CuadrarUtilizadoConProducido()
        {
            try
            {
                // VALIDA QUE LOS LARGOS CORTADOS CUADREN CON LOS LARGOS PRODUCIDOS
                // QUE NINGÚN CAMPO DE LOS DETALLES ESTÉ VACÍO O CON CERO
                // Y QUE LOS GRID NO ESTÉN VACIOS
                int articulosIDUtilizado;
                int cantidadUtilizada;
                int largoArticuloUtilizado;
                int anchoArticuloUtilizado;

                int articulosIDProducido;
                int cantidadProducida = 0;
                int largoArticuloProducido;
                int anchoArticuloProducido;
                int ArticulobaseparacosteoIDProducido;

                if (gridViewDetalleUtilizado.RowCount == 0 || gridViewDetalleProducido.RowCount == 0)
                    return "Favor de llenar las tablas de los artículos utilizados y producidos";

                for (int i = 0; i < gridViewDetalleUtilizado.RowCount; i++)
                {
                    object row = gridViewDetalleUtilizado.GetRow(i);
                    if (row != null)
                    {
                        int almacenUtilizado = Convert.ToInt32(gridViewDetalleUtilizado.GetRowCellValue(i, "intAlmacenesID"));
                        if (almacenUtilizado == 0)
                            return "Seleccione el almacen de los artículos utilizados.";

                        articulosIDUtilizado = Convert.ToInt32(gridViewDetalleUtilizado.GetRowCellValue(i, "intArticulosID"));
                        if (articulosIDUtilizado == 0)
                            return "Seleccione el artículo utilizado de todas las filas o borre aquellas filas que no utilizará. \nPara borrar seleccione la fila y presione Supr";

                        string letraUtilizada = gridViewDetalleUtilizado.GetRowCellValue(i, "strLetra").ToString();
                        if (letraUtilizada.Length == 0)
                            return "Escriba la letra para los artículos utilizados";

                        cantidadProducida = 0;
                        cantidadUtilizada = Convert.ToInt32(gridViewDetalleUtilizado.GetRowCellValue(i, "decCantidad"));
                        if (cantidadUtilizada > 0)
                        {
                            DataRowView dataRowArticuloCortado = repositoryItemLookUpEditArticuloUtilizado.GetDataSourceRowByKeyValue(articulosIDUtilizado) as DataRowView;
                            string nombreArticulo = dataRowArticuloCortado["Des"].ToString();
                            largoArticuloUtilizado = Convert.ToInt32(dataRowArticuloCortado["Largo"]);
                            anchoArticuloUtilizado = Convert.ToInt32(dataRowArticuloCortado["Ancho"]);
                            cantidadUtilizada *= (largoArticuloUtilizado * anchoArticuloUtilizado);

                            for (int x = 0; x < gridViewDetalleProducido.RowCount; x++)
                            {
                                object row2 = gridViewDetalleProducido.GetRow(x);
                                if (row2 != null)
                                {
                                    int almacenProducido = Convert.ToInt32(gridViewDetalleProducido.GetRowCellValue(x, "intAlmacenesID"));
                                    if (almacenUtilizado == 0)
                                        return "Seleccione el almacen de los artículos producidos.";

                                    articulosIDProducido = Convert.ToInt32(gridViewDetalleProducido.GetRowCellValue(x, "intArticulosID"));
                                    if (articulosIDProducido == 0)
                                        return "Seleccione el artículo producido de todas las filas o borre aquellas filas que no utilizará. \nPara borrar seleccione la fila y presione Supr";

                                    string letraProducida = gridViewDetalleProducido.GetRowCellValue(x, "strLetra").ToString();
                                    if (letraProducida.Length == 0)
                                        return "Escriba la letra para los artículos producidos";

                                    int maquina = Convert.ToInt32(gridViewDetalleProducido.GetRowCellValue(x, "intMaquina"));
                                    if (maquina == 0)
                                        return "Seleccione la máquina que se utilizó para la producción";

                                    int cantidadProducidaArticulo = Convert.ToInt32(gridViewDetalleProducido.GetRowCellValue(x, "decCantidad"));
                                    if (cantidadProducidaArticulo > 0)
                                    {
                                        DataRowView dataRowArticuloProducido = repositoryItemLookUpEditArticuloProducido.GetDataSourceRowByKeyValue(articulosIDProducido) as DataRowView;
                                        ArticulobaseparacosteoIDProducido = Convert.ToInt32(dataRowArticuloProducido["ArticulobaseparacosteoID"]);

                                        if (ArticulobaseparacosteoIDProducido == articulosIDUtilizado)
                                        {
                                            largoArticuloProducido = Convert.ToInt32(dataRowArticuloProducido["Largo"]);
                                            anchoArticuloProducido = Convert.ToInt32(dataRowArticuloProducido["Ancho"]);
                                            cantidadProducidaArticulo *= (largoArticuloProducido * anchoArticuloProducido);
                                            cantidadProducida += cantidadProducidaArticulo;
                                        }
                                    }
                                    else
                                    {
                                        nombreArticulo = gridViewDetalleProducido.GetRowCellValue(x, "strCodigoArticulo").ToString();
                                        return "El artículo " + nombreArticulo + " no puede tener cero en cantidad producida";
                                    }
                                }
                            }
                            if (cantidadUtilizada < cantidadProducida)
                                return "La cantidad utilizada expresada en metros^2 del artículo " + nombreArticulo + "\nno puede ser mayor a la cantidad producida expresada en metros^2." +
                                    "\nCantidad utilizada = " + cantidadUtilizada.ToString() + "m^2" +
                                    "\nCantidad producida = " + cantidadProducida.ToString() + "m^2";

                            if (cantidadUtilizada > cantidadProducida)
                            {
                                int diferencia = cantidadUtilizada - cantidadProducida;
                                DialogResult dialog = MessageBox.Show("Existe un sobrante de " + diferencia.ToString() + "m^2. en la producción del artículo utilizado " + nombreArticulo +
                                    "\nCantidad utilizada = " + cantidadUtilizada.ToString() + "m^2" +
                                    "\nCantidad producida = " + cantidadProducida.ToString() + "m^2" +
                                    "\n¿ Desea Continuar ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                                if (dialog != DialogResult.Yes)
                                    return "";
                            }
                        }
                        else
                        {
                            DataRowView dataRowArticuloCortado = repositoryItemLookUpEditArticuloUtilizado.GetDataSourceRowByKeyValue(articulosIDUtilizado) as DataRowView;
                            string nombreArticulo = dataRowArticuloCortado["Des"].ToString();
                            return "El artículo " + nombreArticulo + " no puede tener cero en cantidad utilizada";
                        }
                    }
                }

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void LlenarDataTablesDetalle()
        {
            dtDetalleUtilizado = new DataTable();
            dtDetalleUtilizado.Columns.Add("intProduccionID", typeof(int));
            dtDetalleUtilizado.Columns.Add("intDetalleID", typeof(int));
            dtDetalleUtilizado.Columns.Add("strSerie", typeof(string));
            dtDetalleUtilizado.Columns.Add("intFolio", typeof(int));
            dtDetalleUtilizado.Columns.Add("intAlmacenesID", typeof(int));
            dtDetalleUtilizado.Columns.Add("intArticulosID", typeof(int));
            dtDetalleUtilizado.Columns.Add("decCantidad", typeof(decimal));
            dtDetalleUtilizado.Columns.Add("strLetra", typeof(string));

            dtDetalleProducido = new DataTable();
            dtDetalleProducido.Columns.Add("intProduccionID", typeof(int));
            dtDetalleProducido.Columns.Add("intDetalleID", typeof(int));
            dtDetalleProducido.Columns.Add("strSerie", typeof(string));
            dtDetalleProducido.Columns.Add("intFolio", typeof(int));
            dtDetalleProducido.Columns.Add("intAlmacenesID", typeof(int));
            dtDetalleProducido.Columns.Add("intArticulosID", typeof(int));
            dtDetalleProducido.Columns.Add("decCantidad", typeof(decimal));
            dtDetalleProducido.Columns.Add("strLetra", typeof(string));
            dtDetalleProducido.Columns.Add("intMaquina", typeof(int));

            foreach (var item in detalleUtilizado)
            {
                DataRow row = dtDetalleUtilizado.NewRow();
                row["intProduccionID"] = item.intProduccionID;
                row["intDetalleID"] = item.intDetalleID;
                row["strSerie"] = cboSerie.EditValue.ToString();
                row["intFolio"] = Convert.ToInt32(txtFolio.Text);
                row["intAlmacenesID"] = item.intAlmacenesID;
                row["intArticulosID"] = item.intArticulosID;
                row["decCantidad"] = item.decCantidad;
                row["strLetra"] = item.strLetra.ToUpper();
                dtDetalleUtilizado.Rows.Add(row);
            }

            foreach (var item in detalleProducido)
            {
                DataRow row = dtDetalleProducido.NewRow();
                row["intProduccionID"] = item.intProduccionID;
                row["intDetalleID"] = item.intDetalleID;
                row["strSerie"] = cboSerie.EditValue.ToString();
                row["intFolio"] = Convert.ToInt32(txtFolio.Text);
                row["intAlmacenesID"] = item.intAlmacenesID;
                row["intArticulosID"] = item.intArticulosID;
                row["decCantidad"] = item.decCantidad;
                row["strLetra"] = item.strLetra.ToUpper();
                row["intMaquina"] = item.intMaquina;
                dtDetalleProducido.Rows.Add(row);
            }
        }

        private void LlenarBindingListDetalle()
        {
            // DETALLE UTILIZADO
            foreach (DataRow row in dtDetalleUtilizado.Rows)
            {
                detalleUtilizadoCL detalle = new detalleUtilizadoCL
                {
                    intProduccionID = Convert.ToInt32(row["intProduccionID"]),
                    intDetalleID = Convert.ToInt32(row["intDetalleID"]),
                    strSerie = row["strSerie"].ToString(),
                    intFolio = Convert.ToInt32(row["intFolio"]),
                    intArticulosID = Convert.ToInt32(row["intArticulosID"]),
                    intAlmacenesID = Convert.ToInt32(row["intAlmacenesID"]),
                    decCantidad = Convert.ToDecimal(row["decCantidad"]),
                    strCodigoArticulo = string.Empty,
                    strLetra = row["strLetra"].ToString()
                };
                detalleUtilizado.Add(detalle);
            }
            GuardarArticulosIDUtilizados();
            CargarCombosArticuloProducido();
            // DETALLE PRODUCIDO
            foreach (DataRow row in dtDetalleProducido.Rows)
            {
                detalleProducidoCL detalle = new detalleProducidoCL
                {
                    intProduccionID = Convert.ToInt32(row["intProduccionID"]),
                    intDetalleID = Convert.ToInt32(row["intDetalleID"]),
                    strSerie = row["strSerie"].ToString(),
                    intFolio = Convert.ToInt32(row["intFolio"]),
                    intArticulosID = Convert.ToInt32(row["intArticulosID"]),
                    intAlmacenesID = Convert.ToInt32(row["intAlmacenesID"]),
                    decCantidad = Convert.ToDecimal(row["decCantidad"]),
                    strCodigoArticulo = string.Empty,
                    strLetra = row["strLetra"].ToString(),
                    intMaquina = Convert.ToInt32(row["intMaquina"])
                };
                detalleProducido.Add(detalle);
            }
        }

        private void Nuevo()
        {
            intFolio = 0;
            LimpiaCajas();
            BotonesEdicion(true);
            globalCL global = new globalCL();
            global.strDoc = "Cortes";
            global.strSerie = cboSerie.EditValue.ToString();
            string Result = global.DocumentosSiguienteID();
            if (Result != "OK")
            {
                MessageBox.Show("No se pudo leer el siguiente folio");
                return;
            }
            else
            {
                txtFolio.Text = global.iFolio.ToString();
                navigationFrameProduccion2.SelectedPage = navigationPageDatosProduccion2;
            }
        }

        private void Ver()
        {
            LlenaCajas();
            BotonesEdicion(true);
            navigationFrameProduccion2.SelectedPage = navigationPageDatosProduccion2;
        }

        private void Guardar()
        {
            try
            {
                string result = Valida();
                if (result != "OK")
                {
                    if (result != string.Empty)
                        MessageBox.Show(result, "Error al validar información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LlenarDataTablesDetalle();
                CortesCL cortes = new CortesCL();
                cortes.strSerie = cboSerie.Text;
                cortes.intFolio = Convert.ToInt32(txtFolio.Text);
                cortes.strElaboro = txtElaboro.Text;
                cortes.intTurno = Convert.ToInt32(cboTurno.EditValue);
                cortes.fFecha = dateFecha.DateTime;
                cortes.strObservaciones = txtObservaciones.Text;
                cortes.detalleUtilizado = dtDetalleUtilizado;
                cortes.detalleProducido = dtDetalleProducido;
                result = cortes.ProduccionCrud();

                if (result == "OK")
                {
                    strSerie = cboSerie.Text;
                    intFolio = Convert.ToInt32(txtFolio.Text);

                    DialogResult dialog = MessageBox.Show("¿Desea generar nuevo registro?", "Folio " + intFolio.ToString() + " Guardado Correctamente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialog == DialogResult.Yes)
                        Nuevo();
                    else
                        BotonesEdicion(true);
                }
                else
                    MessageBox.Show(result, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        }

        private void Cancelar()
        {
            try
            {
                string status = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status").ToString();
                if (status == "Cancelado")
                {
                    MessageBox.Show("Este folio se encuentra cancelado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult dialog = MessageBox.Show("¿Está seguro de eliminar el folio " + intFolio.ToString() + "?", "Eliminar Folio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog != DialogResult.Yes) return;

                CortesCL cl = new CortesCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;

                string result = cl.ProduccionCancelar();
                if (result != "OK")
                    throw new Exception(result);
                else
                {
                    LlenarGridPrincipal();
                    MessageBox.Show("Cancelado Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Cancelar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Imprimir()
        {
            try
            {
                globalCL global = new globalCL();
                string result = global.Datosdecontrol();
                if (result != "OK")
                {
                    MessageBox.Show(result);
                    return;
                }
                HojaProduccionCortesDesigner rep = new HojaProduccionCortesDesigner();
                rep.Parameters["parameter1"].Value = strSerie;
                rep.Parameters["parameter2"].Value = intFolio;

                if (global.iImpresiondirecta == 1)
                {
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.CreateDocument();
                    documentViewer1.DocumentSource = rep;
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImprimir.Text);
                    navigationFrameProduccion2.SelectedPage = navigationPageReporte;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al generar reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Regresar()
        {
            LlenarGridPrincipal();
            navigationFrameProduccion2.SelectedPage = navigationPageGridPrincipal;
            BotonesEdicion(false);
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            LimpiaCajas();
        }
    }
}