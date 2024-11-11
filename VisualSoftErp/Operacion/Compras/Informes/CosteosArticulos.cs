using DevExpress.DataAccess.Native.ObjectBinding;
using DevExpress.Pdf.Native.BouncyCastle.Ocsp;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraNavBar;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSpreadsheet.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Compras.Clases;
using VisualSoftErp.Operacion.Compras.Designers;
using VisualSoftErp.Operacion.Compras.Formas;
using VisualSoftErp.Operacion.Inventarios.Informes;

namespace VisualSoftErp.Operacion.Compras.Informes
{
    public partial class CosteosArticulos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private int intListasCosteoID;
        private bool permisosEscritura;
        private bool permisosModificarListasPrecios;
        private DataTable ListasCosteo { get; set; } = new DataTable
        {
            Columns =
            {
                new DataColumn("ListasCosteoID", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("CanalVentasID", typeof(int)),
                new DataColumn("Independiente", typeof(int)),
                new DataColumn("ListasCosteoIDIndependiente", typeof(int)),
                new DataColumn("Categoria", typeof(string))
            }
        };
        private DataTable ListasCosteoDetalle { get; set; } = new DataTable
        {
            Columns =
            {
                new DataColumn("ListasCosteoDetalleID", typeof(int)),
                new DataColumn("ListasCosteoID", typeof(int)),
                new DataColumn("ArticulosID", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("MonedasID", typeof(string)),
                new DataColumn("TipoCambio", typeof(decimal)),
                new DataColumn("Incremental", typeof(decimal)),
                new DataColumn("Costo", typeof(decimal)),
                new DataColumn("Merma", typeof(int)),
                new DataColumn("Margen", typeof(int)),
                new DataColumn("CostoTotal", typeof(decimal)),
                new DataColumn("PrecioActual", typeof(decimal))
            }
        };

        public CosteosArticulos()
        {
            InitializeComponent();
        }

        private void PermisosEscritura()
        {
            globalCL clg = new globalCL();
            UsuariosCL usuarios = new UsuariosCL();

            clg.strPrograma = "0140";
            if (clg.accesoSoloLectura()) permisosEscritura = false;
            else permisosEscritura = true;

            usuarios.intUsuariosID = globalCL.gv_UsuarioID;
            usuarios.UsuariosLlenaCajas();
            if (usuarios.intCambiarprecios == 0) permisosModificarListasPrecios = false;
            else permisosModificarListasPrecios = true;
        }

        private void CargarCombos()
        {
            BindingSource src = new BindingSource();
            combosCL combos = new combosCL();
            globalCL globalCL = new globalCL();

            #region CANAL DE VENTAS
            cboCanalVentas_PCA.Properties.ValueMember = "Clave";
            cboCanalVentas_PCA.Properties.DisplayMember = "Des";
            combos.strTabla = "Canalesdeventa";
            src.DataSource = combos.CargaCombos();
            cboCanalVentas_PCA.Properties.DataSource = globalCL.AgregarOpcionTodos(src);
            cboCanalVentas_PCA.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCanalVentas_PCA.Properties.ForceInitialize();
            cboCanalVentas_PCA.Properties.PopulateColumns();
            cboCanalVentas_PCA.Properties.Columns["Clave"].Visible = false;
            cboCanalVentas_PCA.Properties.NullText = "Seleccione un canal de ventas";
            cboCanalVentas_PCA.EditValue = null;

            cboCanalVentas_Herramientas.Properties.ValueMember = "Clave";
            cboCanalVentas_Herramientas.Properties.DisplayMember = "Des";
            combos.strTabla = "Canalesdeventa";
            src.DataSource = combos.CargaCombos();
            cboCanalVentas_Herramientas.Properties.DataSource = globalCL.AgregarOpcionTodos(src);
            cboCanalVentas_Herramientas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCanalVentas_Herramientas.Properties.ForceInitialize();
            cboCanalVentas_Herramientas.Properties.PopulateColumns();
            cboCanalVentas_Herramientas.Properties.Columns["Clave"].Visible = false;
            cboCanalVentas_Herramientas.Properties.NullText = "Seleccione un canal de ventas";
            cboCanalVentas_Herramientas.EditValue = null;

            cboCanalVentas_Peliculas.Properties.ValueMember = "Clave";
            cboCanalVentas_Peliculas.Properties.DisplayMember = "Des";
            combos.strTabla = "Canalesdeventa";
            src.DataSource = combos.CargaCombos();
            cboCanalVentas_Peliculas.Properties.DataSource = globalCL.AgregarOpcionTodos(src);
            cboCanalVentas_Peliculas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCanalVentas_Peliculas.Properties.ForceInitialize();
            cboCanalVentas_Peliculas.Properties.PopulateColumns();
            cboCanalVentas_Peliculas.Properties.Columns["Clave"].Visible = false;
            cboCanalVentas_Peliculas.Properties.NullText = "Seleccione un canal de ventas";
            cboCanalVentas_Peliculas.EditValue = null;
            #endregion CANAL DE VENTAS

            #region LISTAS DE COSTEO
            CargarCombosListasCosteoIndependientes();
            #endregion

            #region LISTAS DE PRECIOS
            cboListasPrecio_PCA.Properties.ValueMember = "Clave";
            cboListasPrecio_PCA.Properties.DisplayMember = "Des";
            combos.strTabla = "ListasPrecio";
            cboListasPrecio_PCA.Properties.DataSource = combos.CargaCombos();
            cboListasPrecio_PCA.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboListasPrecio_PCA.Properties.ForceInitialize();
            cboListasPrecio_PCA.Properties.PopulateColumns();
            cboListasPrecio_PCA.Properties.Columns["Clave"].Visible = false;
            cboListasPrecio_PCA.Properties.NullText = "Seleccione una lista de precios";
            cboListasPrecio_PCA.EditValue = null;

            cboListasPrecio_Herramientas.Properties.ValueMember = "Clave";
            cboListasPrecio_Herramientas.Properties.DisplayMember = "Des";
            combos.strTabla = "ListasPrecio";
            cboListasPrecio_Herramientas.Properties.DataSource = combos.CargaCombos();
            cboListasPrecio_Herramientas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboListasPrecio_Herramientas.Properties.ForceInitialize();
            cboListasPrecio_Herramientas.Properties.PopulateColumns();
            cboListasPrecio_Herramientas.Properties.Columns["Clave"].Visible = false;
            cboListasPrecio_Herramientas.Properties.NullText = "Seleccione una lista de precios";
            cboListasPrecio_Herramientas.EditValue = null;

            cboListasPrecio_Peliculas.Properties.ValueMember = "Clave";
            cboListasPrecio_Peliculas.Properties.DisplayMember = "Des";
            combos.strTabla = "ListasPrecio";
            cboListasPrecio_Peliculas.Properties.DataSource = combos.CargaCombos();
            cboListasPrecio_Peliculas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboListasPrecio_Peliculas.Properties.ForceInitialize();
            cboListasPrecio_Peliculas.Properties.PopulateColumns();
            cboListasPrecio_Peliculas.Properties.Columns["Clave"].Visible = false;
            cboListasPrecio_Peliculas.Properties.NullText = "Seleccione una lista de precios";
            cboListasPrecio_Peliculas.EditValue = null;
            #endregion
        }

        private void CargarCombosListasCosteoIndependientes()
        {
            combosCL combos = new combosCL();
            combos.strTabla = "ListasCosteo";
            cboListasCosteoIndependiente_PCA.Properties.ValueMember = "Clave";
            cboListasCosteoIndependiente_PCA.Properties.DisplayMember = "Des";
            cboListasCosteoIndependiente_PCA.Properties.DataSource = combos.CargaCombos();
            cboListasCosteoIndependiente_PCA.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboListasCosteoIndependiente_PCA.Properties.ForceInitialize();
            cboListasCosteoIndependiente_PCA.Properties.PopulateColumns();
            cboListasCosteoIndependiente_PCA.Properties.Columns["Clave"].Visible = false;
            cboListasCosteoIndependiente_PCA.Properties.NullText = "Seleccione una lista de costeo";
            cboListasCosteoIndependiente_PCA.EditValue = null;

            combos.strTabla = "ListasCosteo";
            cboListasCosteoIndependiente_Herramientas.Properties.ValueMember = "Clave";
            cboListasCosteoIndependiente_Herramientas.Properties.DisplayMember = "Des";
            cboListasCosteoIndependiente_Herramientas.Properties.DataSource = combos.CargaCombos();
            cboListasCosteoIndependiente_Herramientas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboListasCosteoIndependiente_Herramientas.Properties.ForceInitialize();
            cboListasCosteoIndependiente_Herramientas.Properties.PopulateColumns();
            cboListasCosteoIndependiente_Herramientas.Properties.Columns["Clave"].Visible = false;
            cboListasCosteoIndependiente_Herramientas.Properties.NullText = "Seleccione una lista de costeo";
            cboListasCosteoIndependiente_Herramientas.EditValue = null;

            combos.strTabla = "ListasCosteo";
            cboListasCosteoIndependiente_Peliculas.Properties.ValueMember = "Clave";
            cboListasCosteoIndependiente_Peliculas.Properties.DisplayMember = "Des";
            cboListasCosteoIndependiente_Peliculas.Properties.DataSource = combos.CargaCombos();
            cboListasCosteoIndependiente_Peliculas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboListasCosteoIndependiente_Peliculas.Properties.ForceInitialize();
            cboListasCosteoIndependiente_Peliculas.Properties.PopulateColumns();
            cboListasCosteoIndependiente_Peliculas.Properties.Columns["Clave"].Visible = false;
            cboListasCosteoIndependiente_Peliculas.Properties.NullText = "Seleccione una lista de costeo";
            cboListasCosteoIndependiente_Peliculas.EditValue = null;
        }

        private string Validar()
        {
            string result = "OK";
            if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
            {
                if (txtNombreListasCosteo_PCA.Text == string.Empty)
                    result = "Escriba el nombre de la lista de costeo.";

                if (cboCanalVentas_PCA.EditValue == null)
                    result = "Seleccione el canal de ventas a la que pertenece la lista de costeo.";

                if (swIndependiente_PCA.IsOn == false && cboListasCosteoIndependiente_PCA.EditValue == null)
                    result = "Si esta lista depende de otra, seleccione la lista independiente.\nDe lo contrario, marque la lista de costeo como 'Independiente'.";

                string nombreArticulo;
                decimal merma;
                decimal margen;
                decimal costoTotal;

                for (int i = 0; i < gridViewPCA_ListasCosteoDetalle.RowCount; i++)
                {
                    nombreArticulo = gridViewPCA_ListasCosteoDetalle.GetRowCellValue(i, "Nombre").ToString();
                    merma = Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetRowCellValue(i, "Merma"));
                    margen = Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetRowCellValue(i, "Margen"));
                    costoTotal = Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetRowCellValue(i, "CostoTotal"));

                    if (merma == 0)
                    {
                        result = "El artículo " + nombreArticulo + " no puede tener 0 como porcentaje para merma.";
                        break;
                    }

                    if (margen == 0)
                    {
                        result = "El artículo " + nombreArticulo + " no puede tener 0 como porcentaje para margen.";
                        break;
                    }

                    if (costoTotal == 0)
                    {
                        result = "El artículo " + nombreArticulo + " tiene 0 como costo total.\nEs necesario que modifique el margen del artículo.";
                        break;
                    }
                }
            }
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
            {
                if (txtNombreListasCosteo_Herramientas.Text == string.Empty)
                    result = "Escriba el nombre de la lista de costeo.";

                if (cboCanalVentas_Herramientas.EditValue == null)
                    result = "Seleccione el canal de ventas a la que pertenece la lista de costeo.";

                if (swIndependiente_Herramientas.IsOn == false && cboListasCosteoIndependiente_Herramientas.EditValue == null)
                    result = "Si esta lista depende de otra, seleccione la lista independiente.\nDe lo contrario, marque la lista de costeo como 'Independiente'.";

                string nombreArticulo;
                decimal merma;
                decimal margen;
                decimal costoTotal;

                for (int i = 0; i < gridViewHerramientas_ListasCosteoDetalle.RowCount; i++)
                {
                    nombreArticulo = gridViewHerramientas_ListasCosteoDetalle.GetRowCellValue(i, "Nombre").ToString();
                    merma = Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetRowCellValue(i, "Merma"));
                    margen = Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetRowCellValue(i, "Margen"));
                    costoTotal = Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetRowCellValue(i, "CostoTotal"));

                    if (merma == 0)
                    {
                        result = "El artículo " + nombreArticulo + " no puede tener 0 como porcentaje para merma.";
                        break;
                    }

                    if (margen == 0)
                    {
                        result = "El artículo " + nombreArticulo + " no puede tener 0 como porcentaje para margen.";
                        break;
                    }

                    if (costoTotal == 0)
                    {
                        result = "El artículo " + nombreArticulo + " tiene 0 como costo total.\nEs necesario que modifique el margen del artículo.";
                        break;
                    }
                }
            }
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
            {
                if (txtNombreListasCosteo_Peliculas.Text == string.Empty)
                    result = "Escriba el nombre de la lista de costeo.";

                if (cboCanalVentas_Peliculas.EditValue == null)
                    result = "Seleccione el canal de ventas a la que pertenece la lista de costeo.";

                if (swIndependiente_Peliculas.IsOn == false && cboListasCosteoIndependiente_Peliculas.EditValue == null)
                    result = "Si esta lista depende de otra, seleccione la lista independiente.\nDe lo contrario, marque la lista de costeo como 'Independiente'.";

                string nombreArticulo;
                decimal merma;
                decimal margen;
                decimal costoTotal;

                for (int i = 0; i < gridViewPeliculas_ListasCosteoDetalle.RowCount; i++)
                {
                    nombreArticulo = gridViewPeliculas_ListasCosteoDetalle.GetRowCellValue(i, "Nombre").ToString();
                    merma = Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetRowCellValue(i, "Merma"));
                    margen = Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetRowCellValue(i, "Margen"));
                    costoTotal = Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetRowCellValue(i, "CostoTotal"));

                    if (margen == 0)
                    {
                        result = "El artículo " + nombreArticulo + " no puede tener 0 como porcentaje para margen.";
                        break;
                    }

                    if (costoTotal == 0)
                    {
                        result = "El artículo " + nombreArticulo + " tiene 0 como costo total.\nEs necesario que modifique el margen del artículo.";
                        break;
                    }
                }
            }
            return result;
        }

        private string ValidarModificarListaPrecios()
        {
            string result = "OK";
            if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
            {
                if (cboListasPrecio_PCA.EditValue == null)
                    result = "Favor de seleccionar la lista de precios a modificar";

                if (gridViewPCA_ListasCosteoDetalle.SelectedRowsCount == 0)
                    result = "Favor de seleccionar los artículos a los que desea modificar el precio";

                int[] selectedRows = gridViewPCA_ListasCosteoDetalle.GetSelectedRows();
                string nombreArticulo;
                decimal precioActual;
                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int rowHandle = selectedRows[i];
                    precioActual = Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetRowCellValue(rowHandle, "PrecioActual"));
                    nombreArticulo = gridViewPCA_ListasCosteoDetalle.GetRowCellValue(rowHandle, "Nombre").ToString();
                    if (precioActual == 0)
                    {
                        result = "El artículo " + nombreArticulo + " no se encuentra en ésta lista de precios. \nFavor de quitar de selección.";
                        break;
                    }
                }

                if (Convert.ToInt32(ListasCosteo.Rows[0]["ListasCosteoID"]) == 0)
                    result = "Favor de primero guardar los márgenes y posteriormente \nmodificar la lista de precios.";
            }
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
            {
                if (cboListasPrecio_Herramientas.EditValue == null)
                    result = "Favor de seleccionar la lista de precios a modificar";

                if (gridViewHerramientas_ListasCosteoDetalle.SelectedRowsCount == 0)
                    result = "Favor de seleccionar los artículos a los que desea modificar el precio";

                int[] selectedRows = gridViewHerramientas_ListasCosteoDetalle.GetSelectedRows();
                string nombreArticulo;
                decimal precioActual;
                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int rowHandle = selectedRows[i];
                    precioActual = Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetRowCellValue(rowHandle, "PrecioActual"));
                    nombreArticulo = gridViewHerramientas_ListasCosteoDetalle.GetRowCellValue(rowHandle, "Nombre").ToString();
                    if (precioActual == 0)
                    {
                        result = "El artículo " + nombreArticulo + " no se encuentra en ésta lista de precios. \nFavor de quitar de selección.";
                        break;
                    }
                }

                if (Convert.ToInt32(ListasCosteo.Rows[0]["ListasCosteoID"]) == 0)
                    result = "Favor de primero guardar los márgenes y posteriormente \nmodificar la lista de precios.";
            }
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
            {
                if (cboListasPrecio_Peliculas.EditValue == null)
                    result = "Favor de seleccionar la lista de precios a modificar";

                if (gridViewPeliculas_ListasCosteoDetalle.SelectedRowsCount == 0)
                    result = "Favor de seleccionar los artículos a los que desea modificar el precio";

                int[] selectedRows = gridViewPeliculas_ListasCosteoDetalle.GetSelectedRows();
                string nombreArticulo;
                decimal precioActual;
                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int rowHandle = selectedRows[i];
                    precioActual = Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetRowCellValue(rowHandle, "PrecioActual"));
                    nombreArticulo = gridViewPeliculas_ListasCosteoDetalle.GetRowCellValue(rowHandle, "Nombre").ToString();
                    if (precioActual == 0)
                    {
                        result = "El artículo " + nombreArticulo + " no se encuentra en ésta lista de precios. \nFavor de quitar de selección.";
                        break;
                    }
                }

                if (Convert.ToInt32(ListasCosteo.Rows[0]["ListasCosteoID"]) == 0)
                    result = "Favor de primero guardar los márgenes y posteriormente \nmodificar la lista de precios.";
            }
            return result;
        }

        private void LimpiarCajas()
        {
            intListasCosteoID = 0;
            ListasCosteo.Rows.Clear();
            ListasCosteoDetalle.Rows.Clear();
            if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
            {
                ListasCosteo.Rows.Add(0, "", 0, 1, 0, "PCA");
                InitGridControlPCA_ListasCosteoDetalle();
                txtNombreListasCosteo_PCA.Text = string.Empty;
                cboCanalVentas_PCA.EditValue = null;
                swIndependiente_PCA.IsOn = true;
                cboListasCosteoIndependiente_PCA.EditValue = null;
                cboListasPrecio_PCA.EditValue = null;
            }
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
            {
                ListasCosteo.Rows.Add(0, "", 0, 1, 0, "HERRAMIENTAS");
                InitGridControlHerramientas_ListasCosteoDetalle();
                txtNombreListasCosteo_Herramientas.Text = string.Empty;
                cboCanalVentas_Herramientas.EditValue = null;
                swIndependiente_Herramientas.IsOn = true;
                cboListasCosteoIndependiente_Herramientas.EditValue = null;
                cboListasPrecio_Herramientas.EditValue = null;
            }
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
            {
                ListasCosteo.Rows.Add(0, "", 0, 1, 0, "PELICULAS");
                InitGridControlPeliculas_ListasCosteoDetalle();
                txtNombreListasCosteo_Peliculas.Text = string.Empty;
                cboCanalVentas_Peliculas.EditValue = null;
                swIndependiente_Peliculas.IsOn = true;
                cboListasCosteoIndependiente_Peliculas.EditValue = null;
                cboListasPrecio_Peliculas.EditValue = null;
            }
        }

        private void MostrarOcultarBotones(bool isEditing)
        {
            if (isEditing)
            {
                bbiNuevo.Enabled = false;
                bbiEditar.Enabled = false;
                if (permisosEscritura) bbiGuardar.Enabled = true;
                bbiEliminar.Enabled = false;
                if (permisosModificarListasPrecios) bbiModificarListasPrecio.Enabled = true;
                bbiVistaPrevia.Enabled = true;
                bbiRegresar.Enabled = true;
            }
            else
            {
                bbiNuevo.Enabled = true;
                bbiEditar.Enabled = true;
                bbiGuardar.Enabled = false;
                bbiEliminar.Enabled = true;
                bbiModificarListasPrecio.Enabled = false;
                bbiVistaPrevia.Enabled = false;
                bbiRegresar.Enabled = false;
            }
        }


        #region AL CARGAR
        private void CosteosArticulos_Load(object sender, EventArgs e)
        {
            CargarCombos();
            PermisosEscritura();
            InitGridControlPCA_ListasCosteo();
            InitGridControlHerramientas_ListasCosteo();
            InitGridControlPeliculas_ListasCosteo();
            if (ribbonControl.MergeOwner != null) ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageAcciones.Text);
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        #endregion AL CARGAR


        #region INIT GRIDS
        private void InitGridControlPCA_ListasCosteo()
        {
            CosteosArticulosCL cl = new CosteosArticulosCL();
            cl.strTabla = "PCA";
            gridControlPCA_ListasCosteo.DataSource = cl.CosteoArticulosListasCosteo();
            gridViewPCA_ListasCosteo.ViewCaption = "Listas de Costeo PCA";
            gridViewPCA_ListasCosteo.OptionsView.ShowViewCaption = true;
            gridViewPCA_ListasCosteo.OptionsBehavior.ReadOnly = true;
            gridViewPCA_ListasCosteo.OptionsBehavior.Editable = false;
            gridViewPCA_ListasCosteo.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False;
        }
        private void InitGridControlPCA_ListasCosteoDetalle()
        {
            CosteosArticulosCL cl = new CosteosArticulosCL();
            cl.strTabla = "PCA";
            cl.ListasCosteo = ListasCosteo;
            cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_PCA.EditValue);
            ListasCosteoDetalle = cl.CosteoArticulosGrid();
            gridControlPCA_ListasCosteoDetalle.DataSource = ListasCosteoDetalle;
            gridViewPCA_ListasCosteoDetalle.ViewCaption = "Articulos de PCA";
            gridViewPCA_ListasCosteoDetalle.OptionsView.ShowViewCaption = true;
            gridViewPCA_ListasCosteoDetalle.OptionsBehavior.ReadOnly = false;
            gridViewPCA_ListasCosteoDetalle.OptionsBehavior.Editable = true;
            gridViewPCA_ListasCosteoDetalle.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False;
            gridViewPCA_ListasCosteoDetalle.OptionsSelection.MultiSelect = true;
            gridViewPCA_ListasCosteoDetalle.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewPCA_ListasCosteoDetalle.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gridViewPCA_ListasCosteoDetalle.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gridViewPCA_ListasCosteoDetalle.Columns["ArticulosID"].OptionsColumn.AllowEdit = false;
            gridViewPCA_ListasCosteoDetalle.Columns["Nombre"].OptionsColumn.AllowEdit = false;
            gridViewPCA_ListasCosteoDetalle.Columns["Costo"].OptionsColumn.AllowEdit = false;
            gridViewPCA_ListasCosteoDetalle.Columns["CostoTotal"].OptionsColumn.AllowEdit = false;
            gridViewPCA_ListasCosteoDetalle.Columns["PrecioActual"].OptionsColumn.AllowEdit = false;
            gridViewPCA_ListasCosteoDetalle.Columns["PorcentajeDiferencia"].OptionsColumn.AllowEdit = false;
            gridViewPCA_ListasCosteoDetalle.Columns["Merma"].Caption = "Porcentaje para Merma";
            gridViewPCA_ListasCosteoDetalle.Columns["Margen"].Caption = "Porcentaje para Margen";
            gridViewPCA_ListasCosteoDetalle.Columns["CostoTotal"].Caption = "Precio Sugerido";
        }
        private void InitGridControlHerramientas_ListasCosteo()
        {
            CosteosArticulosCL cl = new CosteosArticulosCL();
            cl.strTabla = "HERRAMIENTAS";
            gridControlHerramientas_ListasCosteo.DataSource = cl.CosteoArticulosListasCosteo();
            gridViewHerramientas_ListasCosteo.ViewCaption = "Listas de Costeo HERRAMIENTAS";
            gridViewHerramientas_ListasCosteo.OptionsView.ShowViewCaption = true;
            gridViewHerramientas_ListasCosteo.OptionsBehavior.ReadOnly = true;
            gridViewHerramientas_ListasCosteo.OptionsBehavior.Editable = false;
            gridViewHerramientas_ListasCosteo.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False;
        }
        private void InitGridControlHerramientas_ListasCosteoDetalle()
        {
            CosteosArticulosCL cl = new CosteosArticulosCL();
            cl.strTabla = "HERRAMIENTAS";
            cl.ListasCosteo = ListasCosteo;
            cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_Herramientas.EditValue);
            ListasCosteoDetalle = cl.CosteoArticulosGrid();
            gridControlHerramientas_ListasCosteoDetalle.DataSource = ListasCosteoDetalle;
            gridViewHerramientas_ListasCosteoDetalle.ViewCaption = "Articulos de HERRAMIENTAS";
            gridViewHerramientas_ListasCosteoDetalle.OptionsView.ShowViewCaption = true;
            gridViewHerramientas_ListasCosteoDetalle.OptionsBehavior.ReadOnly = false;
            gridViewHerramientas_ListasCosteoDetalle.OptionsBehavior.Editable = true;
            gridViewHerramientas_ListasCosteoDetalle.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False;
            gridViewHerramientas_ListasCosteoDetalle.OptionsSelection.MultiSelect = true;
            gridViewHerramientas_ListasCosteoDetalle.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewHerramientas_ListasCosteoDetalle.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gridViewHerramientas_ListasCosteoDetalle.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gridViewHerramientas_ListasCosteoDetalle.Columns["ArticulosID"].OptionsColumn.AllowEdit = false;
            gridViewHerramientas_ListasCosteoDetalle.Columns["Nombre"].OptionsColumn.AllowEdit = false;
            gridViewHerramientas_ListasCosteoDetalle.Columns["Costo"].OptionsColumn.AllowEdit = false;
            gridViewHerramientas_ListasCosteoDetalle.Columns["CostoTotal"].OptionsColumn.AllowEdit = false;
            gridViewHerramientas_ListasCosteoDetalle.Columns["PrecioActual"].OptionsColumn.AllowEdit = false;
            gridViewHerramientas_ListasCosteoDetalle.Columns["PorcentajeDiferencia"].OptionsColumn.AllowEdit = false;
            gridViewHerramientas_ListasCosteoDetalle.Columns["Merma"].Caption = "Porcentaje para Merma";
            gridViewHerramientas_ListasCosteoDetalle.Columns["Margen"].Caption = "Porcentaje para Margen";
            gridViewHerramientas_ListasCosteoDetalle.Columns["CostoTotal"].Caption = "Precio Sugerido";
        }
        private void InitGridControlPeliculas_ListasCosteo()
        {
            CosteosArticulosCL cl = new CosteosArticulosCL();
            cl.strTabla = "PELICULAS";
            gridControlPeliculas_ListasCosteo.DataSource = cl.CosteoArticulosListasCosteo();
            gridViewPeliculas_ListasCosteo.ViewCaption = "Listas de Costeo PELICULAS";
            gridViewPeliculas_ListasCosteo.OptionsView.ShowViewCaption = true;
            gridViewPeliculas_ListasCosteo.OptionsBehavior.ReadOnly = true;
            gridViewPeliculas_ListasCosteo.OptionsBehavior.Editable = false;
            gridViewPeliculas_ListasCosteo.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False;
        }
        private void InitGridControlPeliculas_ListasCosteoDetalle()
        {
            CosteosArticulosCL cl = new CosteosArticulosCL();
            cl.strTabla = "PELICULAS";
            cl.ListasCosteo = ListasCosteo;
            cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_Peliculas.EditValue);
            ListasCosteoDetalle = cl.CosteoArticulosGrid();
            gridControlPeliculas_ListasCosteoDetalle.DataSource = ListasCosteoDetalle;
            gridViewPeliculas_ListasCosteoDetalle.ViewCaption = "Articulos de PELICULAS";
            gridViewPeliculas_ListasCosteoDetalle.OptionsView.ShowViewCaption = true;
            gridViewPeliculas_ListasCosteoDetalle.OptionsBehavior.ReadOnly = false;
            gridViewPeliculas_ListasCosteoDetalle.OptionsBehavior.Editable = true;
            gridViewPeliculas_ListasCosteoDetalle.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False;
            gridViewPeliculas_ListasCosteoDetalle.OptionsSelection.MultiSelect = true;
            gridViewPeliculas_ListasCosteoDetalle.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewPeliculas_ListasCosteoDetalle.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gridViewPeliculas_ListasCosteoDetalle.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gridViewPeliculas_ListasCosteoDetalle.Columns["ArticulosID"].OptionsColumn.AllowEdit = false;
            gridViewPeliculas_ListasCosteoDetalle.Columns["Nombre"].OptionsColumn.AllowEdit = false;
            gridViewPeliculas_ListasCosteoDetalle.Columns["Costo"].OptionsColumn.AllowEdit = false;
            gridViewPeliculas_ListasCosteoDetalle.Columns["CostoTotal"].OptionsColumn.AllowEdit = false;
            gridViewPeliculas_ListasCosteoDetalle.Columns["PrecioActual"].OptionsColumn.AllowEdit = false;
            gridViewPeliculas_ListasCosteoDetalle.Columns["PorcentajeDiferencia"].OptionsColumn.AllowEdit = false;
            gridViewPeliculas_ListasCosteoDetalle.Columns["Merma"].Caption = "Porcentaje para Merma";
            gridViewPeliculas_ListasCosteoDetalle.Columns["Margen"].Caption = "Porcentaje para Margen";
            gridViewPeliculas_ListasCosteoDetalle.Columns["CostoTotal"].Caption = "Precio Sugerido";
        }
        #endregion INIT GRIDS


        #region NAVEGACION
        private void navBarControlMenu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            LimpiarCajas();
            NavBarItemLink item = e.Link;
            if (item != null)
            {
                switch (item.ItemName)
                {
                    case "navBarItemPCA":
                        MostrarOcultarBotones(false);
                        navigationFramePCA.SelectedPage = navigationPagePCA_ListasCosteo;
                        navigationFrameCosteoArticulos.SelectedPage = navigationPagePCA;
                        break;
                    case "navBarItemHerramientas":
                        MostrarOcultarBotones(false);
                        navigationFrameHerramientas.SelectedPage = navigationPageHerramientas_ListasCosteo;
                        navigationFrameCosteoArticulos.SelectedPage = navigationPageHerramientas;
                        break;
                    case "navBarItemPeliculas":
                        MostrarOcultarBotones(false);
                        navigationFramePeliculas.SelectedPage = navigationPagePeliculas_ListasCosteo;
                        navigationFrameCosteoArticulos.SelectedPage = navigationPagePeliculas;
                        break;
                }
            }
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void officeNavigationBarMenu_ItemClick(object sender, NavigationBarItemEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            LimpiarCajas();
            OfficeNavigationBar menu = (OfficeNavigationBar)sender;
            if (menu != null)
            {
                NavigationBarItem item = menu.SelectedItem;
                switch (item.Name)
                {
                    case "navigationBarItemPCA":
                        MostrarOcultarBotones(false);
                        navigationFramePCA.SelectedPage = navigationPagePCA_ListasCosteo;
                        navigationFrameCosteoArticulos.SelectedPage = navigationPagePCA;
                        break;
                    case "navigationBarItemHerramientas":
                        MostrarOcultarBotones(false);
                        navigationFrameHerramientas.SelectedPage = navigationPageHerramientas_ListasCosteo;
                        navigationFrameCosteoArticulos.SelectedPage = navigationPageHerramientas;
                        break;
                    case "navigationBarItemPeliculas":
                        MostrarOcultarBotones(false);
                        navigationFramePeliculas.SelectedPage = navigationPagePeliculas_ListasCosteo;
                        navigationFrameCosteoArticulos.SelectedPage = navigationPagePeliculas;
                        break;
                }
            }
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        #endregion NAVEGACION


        #region BOTONES
        private void bbiNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            LimpiarCajas();
            // PAGINA PCA
            if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
            {
                navigationFramePCA.SelectedPage = navigationPagePCA_ListasCosteoDetalle;
            }
            // PAGINA HERRAMIENTAS
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
            {
                navigationFrameHerramientas.SelectedPage = navigationPageHerramientas_ListasCosteoDetalle;
            }
            // PAGINA PELICULAS
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
            {
                navigationFramePeliculas.SelectedPage = navigationPagePeliculas_ListasCosteoDetalle;
            }
            MostrarOcultarBotones(true);
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void bbiEditar_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();

            if (intListasCosteoID == 0)
            {
                MessageBox.Show("Seleccione la lista de costeo que desea editar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // PAGINA PCA
            if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
            {
                CosteosArticulosCL cl = new CosteosArticulosCL();
                cl.intListasCosteoID = intListasCosteoID;
                ListasCosteo = cl.LlenarCajasListasCosteo();
                InitGridControlPCA_ListasCosteoDetalle();
                txtNombreListasCosteo_PCA.Text = ListasCosteo.Rows[0]["Nombre"].ToString();
                cboCanalVentas_PCA.EditValue = Convert.ToInt32(ListasCosteo.Rows[0]["CanalVentasID"]);
                swIndependiente_PCA.IsOn = Convert.ToInt32(ListasCosteo.Rows[0]["Independiente"]) == 1 ? true : false;
                cboListasCosteoIndependiente_PCA.EditValueChanged -= cboListasCosteoIndependiente_EditValueChanged;
                cboListasCosteoIndependiente_PCA.EditValue = Convert.ToInt32(ListasCosteo.Rows[0]["ListasCosteoIDIndependiente"]);
                cboListasCosteoIndependiente_PCA.EditValueChanged += cboListasCosteoIndependiente_EditValueChanged;
                navigationFramePCA.SelectedPage = navigationPagePCA_ListasCosteoDetalle;
            }
            // PAGINA HERRAMIENTAS
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
            {
                CosteosArticulosCL cl = new CosteosArticulosCL();
                cl.intListasCosteoID = intListasCosteoID;
                ListasCosteo = cl.LlenarCajasListasCosteo();
                InitGridControlHerramientas_ListasCosteoDetalle();
                txtNombreListasCosteo_Herramientas.Text = ListasCosteo.Rows[0]["Nombre"].ToString();
                cboCanalVentas_Herramientas.EditValue = Convert.ToInt32(ListasCosteo.Rows[0]["CanalVentasID"]);
                swIndependiente_Herramientas.IsOn = Convert.ToInt32(ListasCosteo.Rows[0]["Independiente"]) == 1 ? true : false;
                cboListasCosteoIndependiente_Herramientas.EditValueChanged -= cboListasCosteoIndependiente_EditValueChanged;
                cboListasCosteoIndependiente_Herramientas.EditValue = Convert.ToInt32(ListasCosteo.Rows[0]["ListasCosteoIDIndependiente"]);
                cboListasCosteoIndependiente_Herramientas.EditValueChanged += cboListasCosteoIndependiente_EditValueChanged;
                navigationFrameHerramientas.SelectedPage = navigationPageHerramientas_ListasCosteoDetalle;
            }
            // PAGINA PELICULAS
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
            {
                CosteosArticulosCL cl = new CosteosArticulosCL();
                cl.intListasCosteoID = intListasCosteoID;
                ListasCosteo = cl.LlenarCajasListasCosteo();
                InitGridControlPeliculas_ListasCosteoDetalle();
                txtNombreListasCosteo_Peliculas.Text = ListasCosteo.Rows[0]["Nombre"].ToString();
                cboCanalVentas_Peliculas.EditValue = Convert.ToInt32(ListasCosteo.Rows[0]["CanalVentasID"]);
                swIndependiente_Peliculas.IsOn = Convert.ToInt32(ListasCosteo.Rows[0]["Independiente"]) == 1 ? true : false;
                cboListasCosteoIndependiente_Peliculas.EditValueChanged -= cboListasCosteoIndependiente_EditValueChanged;
                cboListasCosteoIndependiente_Peliculas.EditValue = Convert.ToInt32(ListasCosteo.Rows[0]["ListasCosteoIDIndependiente"]);
                cboListasCosteoIndependiente_Peliculas.EditValueChanged += cboListasCosteoIndependiente_EditValueChanged;
                navigationFramePeliculas.SelectedPage = navigationPagePeliculas_ListasCosteoDetalle;
            }
            MostrarOcultarBotones(true);
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void bbiGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            string result = string.Empty;
            result = this.Validar();
            if (result != "OK")
            {
                MessageBox.Show(result, "Error al Validar Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
            {
                try
                {
                    ListasCosteo.Rows[0]["Nombre"] = txtNombreListasCosteo_PCA.Text;
                    ListasCosteo.Rows[0]["CanalVentasID"] = cboCanalVentas_PCA.EditValue;
                    ListasCosteo.Rows[0]["Independiente"] = swIndependiente_PCA.IsOn ? 1 : 0;
                    ListasCosteo.Rows[0]["ListasCosteoIDIndependiente"] = Convert.ToInt32(cboListasCosteoIndependiente_PCA.EditValue);
                    ListasCosteo.Rows[0]["Categoria"] = "PCA";

                    for(int i = 0; i < ListasCosteoDetalle.Rows.Count; i++)
                    {
                        string moneda = ListasCosteoDetalle.Rows[i]["MonedasID"].ToString();
                        decimal tipoCambio = Convert.ToDecimal(ListasCosteoDetalle.Rows[i]["TipoCambio"]);

                        if (moneda == "USD" && tipoCambio > 1)
                            ListasCosteoDetalle.Rows[i]["MonedasID"] = "MXN";
                    }

                    CosteosArticulosCL cl = new CosteosArticulosCL();
                    cl.ListasCosteo = ListasCosteo;
                    cl.ListasCosteoDetalle = ListasCosteoDetalle;

                    result = cl.CosteoArticulosCrud();

                    if (result != "OK")
                        MessageBox.Show(result, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        InitGridControlPCA_ListasCosteo();
                        CargarCombosListasCosteoIndependientes();
                        if (intListasCosteoID == 0)
                        {
                            LimpiarCajas();
                            MostrarOcultarBotones(false);
                            navigationFramePCA.SelectedPage = navigationPagePCA_ListasCosteo;
                        }
                        MessageBox.Show("Guardado Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    result = "Error en línea " + ex.LineNumber() + "\n\n" + ex.Message;
                    MessageBox.Show(result, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
            {
                try
                {
                    ListasCosteo.Rows[0]["Nombre"] = txtNombreListasCosteo_Herramientas.Text;
                    ListasCosteo.Rows[0]["CanalVentasID"] = cboCanalVentas_Herramientas.EditValue;
                    ListasCosteo.Rows[0]["Independiente"] = swIndependiente_Herramientas.IsOn ? 1 : 0;
                    ListasCosteo.Rows[0]["ListasCosteoIDIndependiente"] = Convert.ToInt32(cboListasCosteoIndependiente_Herramientas.EditValue);
                    ListasCosteo.Rows[0]["Categoria"] = "HERRAMIENTAS";

                    CosteosArticulosCL cl = new CosteosArticulosCL();
                    cl.ListasCosteo = ListasCosteo;
                    cl.ListasCosteoDetalle = ListasCosteoDetalle;

                    result = cl.CosteoArticulosCrud();

                    if (result != "OK")
                        MessageBox.Show(result, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        InitGridControlHerramientas_ListasCosteo();
                        CargarCombosListasCosteoIndependientes();
                        if (intListasCosteoID == 0)
                        {
                            LimpiarCajas();
                            MostrarOcultarBotones(false);
                            navigationFrameHerramientas.SelectedPage = navigationPageHerramientas_ListasCosteo;
                        }
                        MessageBox.Show("Guardado Correctamente", "Exito al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    result = "Error en línea " + ex.LineNumber() + "\n\n" + ex.Message;
                    MessageBox.Show(result, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
            {
                try
                {
                    ListasCosteo.Rows[0]["Nombre"] = txtNombreListasCosteo_Peliculas.Text;
                    ListasCosteo.Rows[0]["CanalVentasID"] = cboCanalVentas_Peliculas.EditValue;
                    ListasCosteo.Rows[0]["Independiente"] = swIndependiente_Peliculas.IsOn ? 1 : 0;
                    ListasCosteo.Rows[0]["ListasCosteoIDIndependiente"] = Convert.ToInt32(cboListasCosteoIndependiente_Peliculas.EditValue);
                    ListasCosteo.Rows[0]["Categoria"] = "PELICULAS";

                    CosteosArticulosCL cl = new CosteosArticulosCL();
                    cl.ListasCosteo = ListasCosteo;
                    cl.ListasCosteoDetalle = ListasCosteoDetalle;

                    result = cl.CosteoArticulosCrud();

                    if (result != "OK")
                        MessageBox.Show(result, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        InitGridControlPeliculas_ListasCosteo();
                        CargarCombosListasCosteoIndependientes();
                        if (intListasCosteoID == 0)
                        {
                            LimpiarCajas();
                            MostrarOcultarBotones(false);
                            navigationFramePeliculas.SelectedPage = navigationPagePeliculas_ListasCosteo;
                        }
                        MessageBox.Show("Guardado Correctamente", "Exito al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    result = "Error en línea " + ex.LineNumber() + "\n\n" + ex.Message;
                    MessageBox.Show(result, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void bbiEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            if (intListasCosteoID == 0)
            {
                MessageBox.Show("Seleccione la lista de costeo que desea eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CosteosArticulosCL cl = new CosteosArticulosCL();
            cl.intListasCosteoID = intListasCosteoID;
            string result = cl.CosteoArticulosEliminar();

            if (result != "OK")
                MessageBox.Show(result, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                LimpiarCajas();
                if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                    InitGridControlPCA_ListasCosteo();
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                    InitGridControlHerramientas_ListasCosteo();
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                    InitGridControlPeliculas_ListasCosteo();

                MessageBox.Show("Eliminado Correctamente", "Exito al Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void bbiModificarListasPrecio_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();

            string result = string.Empty;
            result = ValidarModificarListaPrecios();
            if (result != "OK")
            {
                MessageBox.Show(result, "Error al validar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
            {
                DataTable dt = new DataTable
                {
                    Columns =
                    {
                        new DataColumn("ListasCosteoDetalleID", typeof(int)),
                        new DataColumn("ListasCosteoID", typeof(int)),
                        new DataColumn("ArticulosID", typeof(int)),
                        new DataColumn("Nombre", typeof(string)),
                        new DataColumn("TipoCambio", typeof(decimal)),
                        new DataColumn("Costo", typeof(decimal)),
                        new DataColumn("Merma", typeof(int)),
                        new DataColumn("Margen", typeof(int)),
                        new DataColumn("CostoTotal", typeof(decimal)),
                        new DataColumn("PrecioActual", typeof(decimal))
                    }
                };
                int[] selectedRows = gridViewPCA_ListasCosteoDetalle.GetSelectedRows();
                int articulosID;
                decimal precioSugerido;
                decimal precioNeto;
                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int rowHandle = selectedRows[i];
                    articulosID = Convert.ToInt32(gridViewPCA_ListasCosteoDetalle.GetRowCellValue(rowHandle, "ArticulosID"));
                    precioSugerido = Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetRowCellValue(rowHandle, "CostoTotal"));

                    precioNeto = (precioSugerido * 1.16M);
                    dt.Rows.Add(
                        0,
                        Convert.ToInt32(ListasCosteo.Rows[0]["ListasCosteoID"]),
                        articulosID,
                        "",
                        0,
                        0,
                        0,
                        0,
                        precioSugerido,
                        precioNeto
                    );
                }

                CosteosArticulosCL cl = new CosteosArticulosCL();
                cl.ListasCosteoDetalle = dt;
                cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_PCA.EditValue);
                result = cl.CosteoArticulosModificarListasPrecios();
                if (result != "OK")
                {
                    MessageBox.Show(result, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    EventArgs ev = new EventArgs();
                    cboListasPrecio_EditValueChanged(cboListasPrecio_PCA, ev);
                    MessageBox.Show("Guardado Correctamente", "Guardado exitosamente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
            {
                DataTable dt = new DataTable
                {
                    Columns =
                    {
                        new DataColumn("ListasCosteoDetalleID", typeof(int)),
                        new DataColumn("ListasCosteoID", typeof(int)),
                        new DataColumn("ArticulosID", typeof(int)),
                        new DataColumn("Nombre", typeof(string)),
                        new DataColumn("TipoCambio", typeof(decimal)),
                        new DataColumn("Costo", typeof(decimal)),
                        new DataColumn("Merma", typeof(int)),
                        new DataColumn("Margen", typeof(int)),
                        new DataColumn("CostoTotal", typeof(decimal)),
                        new DataColumn("PrecioActual", typeof(decimal))
                    }
                };
                int[] selectedRows = gridViewHerramientas_ListasCosteoDetalle.GetSelectedRows();
                int articulosID;
                decimal precioSugerido;
                decimal precioNeto;
                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int rowHandle = selectedRows[i];
                    articulosID = Convert.ToInt32(gridViewHerramientas_ListasCosteoDetalle.GetRowCellValue(rowHandle, "ArticulosID"));
                    precioSugerido = Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetRowCellValue(rowHandle, "CostoTotal"));

                    precioNeto = (precioSugerido * 1.16M);
                    dt.Rows.Add(
                        0,
                        Convert.ToInt32(ListasCosteo.Rows[0]["ListasCosteoID"]),
                        articulosID,
                        "",
                        0,
                        0,
                        0,
                        0,
                        precioSugerido,
                        precioNeto
                    );
                }

                CosteosArticulosCL cl = new CosteosArticulosCL();
                cl.ListasCosteoDetalle = dt;
                cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_Herramientas.EditValue);
                result = cl.CosteoArticulosModificarListasPrecios();
                if (result != "OK")
                {
                    MessageBox.Show(result, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    EventArgs ev = new EventArgs();
                    cboListasPrecio_EditValueChanged(cboListasPrecio_Herramientas, ev);
                    MessageBox.Show("Guardado Correctamente", "Guardado exitosamente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
            {
                DataTable dt = new DataTable
                {
                    Columns =
                    {
                        new DataColumn("ListasCosteoDetalleID", typeof(int)),
                        new DataColumn("ListasCosteoID", typeof(int)),
                        new DataColumn("ArticulosID", typeof(int)),
                        new DataColumn("Nombre", typeof(string)),
                        new DataColumn("TipoCambio", typeof(decimal)),
                        new DataColumn("Costo", typeof(decimal)),
                        new DataColumn("Merma", typeof(int)),
                        new DataColumn("Margen", typeof(int)),
                        new DataColumn("CostoTotal", typeof(decimal)),
                        new DataColumn("PrecioActual", typeof(decimal))
                    }
                };
                int[] selectedRows = gridViewPCA_ListasCosteoDetalle.GetSelectedRows();
                int articulosID;
                decimal precioSugerido;
                decimal precioNeto;
                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int rowHandle = selectedRows[i];
                    articulosID = Convert.ToInt32(gridViewPCA_ListasCosteoDetalle.GetRowCellValue(rowHandle, "ArticulosID"));
                    precioSugerido = Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetRowCellValue(rowHandle, "CostoTotal"));

                    precioNeto = (precioSugerido * 1.16M);
                    dt.Rows.Add(
                        0,
                        Convert.ToInt32(ListasCosteo.Rows[0]["ListasCosteoID"]),
                        articulosID,
                        "",
                        0,
                        0,
                        0,
                        0,
                        precioSugerido,
                        precioNeto
                    );
                }

                CosteosArticulosCL cl = new CosteosArticulosCL();
                cl.ListasCosteoDetalle = dt;
                cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_PCA.EditValue);
                result = cl.CosteoArticulosModificarListasPrecios();
                if (result != "OK")
                {
                    MessageBox.Show(result, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    EventArgs ev = new EventArgs();
                    cboListasPrecio_EditValueChanged(cboListasPrecio_Peliculas, ev);
                    MessageBox.Show("Guardado Correctamente", "Guardado exitosamente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void bbiVistaPrevia_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();

            LookUpEdit cbo = new LookUpEdit();
            if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                cbo = cboListasPrecio_PCA;
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                cbo = cboListasPrecio_Herramientas;
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                cbo = cboListasPrecio_Peliculas;

            if (cbo.EditValue == null)
            {
                MessageBox.Show("Escoja la lista de precios con la que desea comparar el costeo.", "Error al generar reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            globalCL globalCL = new globalCL();
            string results = globalCL.Datosdecontrol();
            int impDirecto = results == "OK" ? globalCL.iImpresiondirecta : 0;

            CosteosArticulosDesigner rep = new CosteosArticulosDesigner();
            rep.Parameters["parameter1"].Value = intListasCosteoID;
            rep.Parameters["parameter2"].Value = Convert.ToInt32(cbo.EditValue);
            rep.Parameters["parameter1"].Visible = false;
            rep.Parameters["parameter2"].Visible = false;

            if (impDirecto == 1)
            {
                ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                rpt.Print();
            }
            else
            {
                documentViewer.DocumentSource = rep;
                rep.CreateDocument();
                Ribbon.MergeOwner.SelectedPage = Ribbon.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                navigationFrameCosteoArticulos.SelectedPage = navigationPageReporte;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void bbiRegresar_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            LimpiarCajas();
            if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                navigationFramePCA.SelectedPage = navigationPagePCA_ListasCosteo;

            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                navigationFrameHerramientas.SelectedPage = navigationPageHerramientas_ListasCosteo;

            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                navigationFramePeliculas.SelectedPage = navigationPagePeliculas_ListasCosteo;

            MostrarOcultarBotones(false);
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void bbiRegresarAListas_ItemClick(object sender, ItemClickEventArgs e)
        {
            Ribbon.MergeOwner.SelectedPage = Ribbon.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageAcciones.Text);
            navigationFrameCosteoArticulos.SelectedPage = navigationPagePCA;
            documentViewer.DocumentSource = null;
        }
        #endregion BOTONES


        #region INTERACCIONES USUARIO
        private void swIndependiente_Toggled(object sender, EventArgs e)
        {
            ToggleSwitch swIndependiente = (ToggleSwitch)sender;
            if(swIndependiente.IsOn)
            {
                if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                {
                    cboListasCosteoIndependiente_PCA.EditValue = null;
                    tablePanel6.Visible = false;
                }
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                {
                    cboListasCosteoIndependiente_Herramientas.EditValue = null;
                    tablePanel13.Visible = false;
                }
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                {
                    cboListasCosteoIndependiente_Peliculas.EditValue = null;
                    tablePanel20.Visible = false;
                }
            }
            else
            {
                if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                    tablePanel6.Visible = true;
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                    tablePanel13.Visible = true;
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                    tablePanel20.Visible = true;
            }
        }
        private void gridControl_ListasCosteo_Click(object sender, EventArgs e)
        {
            if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
            {
                GridView view = gridControlPCA_ListasCosteo.FocusedView as GridView;
                if (view != null && view.FocusedRowHandle >= 0)
                {
                    DataRow row = view.GetDataRow(view.FocusedRowHandle);
                    if (row != null)
                    {
                        int ListasCosteoID = Convert.ToInt32(row["ListasCosteoID"]);
                        intListasCosteoID = ListasCosteoID;
                    }
                }
            }
            else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
            {
                GridView view = gridControlHerramientas_ListasCosteo.FocusedView as GridView;
                if (view != null && view.FocusedRowHandle >= 0)
                {
                    DataRow row = view.GetDataRow(view.FocusedRowHandle);
                    if (row != null)
                    {
                        int ListasCosteoID = Convert.ToInt32(row["ListasCosteoID"]);
                        intListasCosteoID = ListasCosteoID;
                    }
                }
            }
        }
        private void gridControl_ListasCosteo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                {
                    GridView view = gridControlPCA_ListasCosteo.FocusedView as GridView;
                    if (view != null && view.FocusedRowHandle >= 0)
                    {
                        DataRow row = view.GetDataRow(view.FocusedRowHandle);
                        if (row != null)
                        {
                            intListasCosteoID = Convert.ToInt32(row["ListasCosteoID"]);
                            CosteosArticulosCL cl = new CosteosArticulosCL();
                            cl.intListasCosteoID = intListasCosteoID;
                            ListasCosteo = cl.LlenarCajasListasCosteo();
                            InitGridControlPCA_ListasCosteoDetalle();
                            txtNombreListasCosteo_PCA.Text = ListasCosteo.Rows[0]["Nombre"].ToString();
                            cboCanalVentas_PCA.EditValue = Convert.ToInt32(ListasCosteo.Rows[0]["CanalVentasID"]);
                            swIndependiente_PCA.IsOn = Convert.ToInt32(ListasCosteo.Rows[0]["Independiente"]) == 1 ? true : false;
                            cboListasCosteoIndependiente_PCA.EditValueChanged -= cboListasCosteoIndependiente_EditValueChanged;
                            cboListasCosteoIndependiente_PCA.EditValue = Convert.ToInt32(ListasCosteo.Rows[0]["ListasCosteoIDIndependiente"]);
                            cboListasCosteoIndependiente_PCA.EditValueChanged += cboListasCosteoIndependiente_EditValueChanged;
                            if (Convert.ToInt32(cboListasCosteoIndependiente_PCA.EditValue) == 0) cboListasCosteoIndependiente_PCA.EditValue = null;
                            MostrarOcultarBotones(true);
                            navigationFramePCA.SelectedPage = navigationPagePCA_ListasCosteoDetalle;
                        }
                    }
                }
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                {
                    GridView view = gridControlHerramientas_ListasCosteo.FocusedView as GridView;
                    if (view != null && view.FocusedRowHandle >= 0)
                    {
                        DataRow row = view.GetDataRow(view.FocusedRowHandle);
                        if (row != null)
                        {
                            intListasCosteoID = Convert.ToInt32(row["ListasCosteoID"]);
                            CosteosArticulosCL cl = new CosteosArticulosCL();
                            cl.intListasCosteoID = intListasCosteoID;
                            ListasCosteo = cl.LlenarCajasListasCosteo();
                            InitGridControlHerramientas_ListasCosteoDetalle();
                            txtNombreListasCosteo_Herramientas.Text = ListasCosteo.Rows[0]["Nombre"].ToString();
                            cboCanalVentas_Herramientas.EditValue = Convert.ToInt32(ListasCosteo.Rows[0]["CanalVentasID"]);
                            swIndependiente_Herramientas.IsOn = Convert.ToInt32(ListasCosteo.Rows[0]["Independiente"]) == 1 ? true : false;
                            cboListasCosteoIndependiente_Herramientas.EditValueChanged -= cboListasCosteoIndependiente_EditValueChanged;
                            cboListasCosteoIndependiente_Herramientas.EditValue = Convert.ToInt32(ListasCosteo.Rows[0]["ListasCosteoIDIndependiente"]);
                            cboListasCosteoIndependiente_Herramientas.EditValueChanged += cboListasCosteoIndependiente_EditValueChanged;
                            if (Convert.ToInt32(cboListasCosteoIndependiente_Herramientas.EditValue) == 0) cboListasCosteoIndependiente_Herramientas.EditValue = null;
                            MostrarOcultarBotones(true);
                            navigationFrameHerramientas.SelectedPage = navigationPageHerramientas_ListasCosteoDetalle;
                        }
                    }
                }
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                {
                    GridView view = gridControlPeliculas_ListasCosteo.FocusedView as GridView;
                    if (view != null && view.FocusedRowHandle >= 0)
                    {
                        DataRow row = view.GetDataRow(view.FocusedRowHandle);
                        if (row != null)
                        {
                            intListasCosteoID = Convert.ToInt32(row["ListasCosteoID"]);
                            CosteosArticulosCL cl = new CosteosArticulosCL();
                            cl.intListasCosteoID = intListasCosteoID;
                            ListasCosteo = cl.LlenarCajasListasCosteo();
                            InitGridControlPeliculas_ListasCosteoDetalle();
                            txtNombreListasCosteo_Peliculas.Text = ListasCosteo.Rows[0]["Nombre"].ToString();
                            cboCanalVentas_Peliculas.EditValue = Convert.ToInt32(ListasCosteo.Rows[0]["CanalVentasID"]);
                            swIndependiente_Peliculas.IsOn = Convert.ToInt32(ListasCosteo.Rows[0]["Independiente"]) == 1 ? true : false;
                            cboListasCosteoIndependiente_Peliculas.EditValueChanged -= cboListasCosteoIndependiente_EditValueChanged;
                            cboListasCosteoIndependiente_Peliculas.EditValue = Convert.ToInt32(ListasCosteo.Rows[0]["ListasCosteoIDIndependiente"]);
                            cboListasCosteoIndependiente_Peliculas.EditValueChanged += cboListasCosteoIndependiente_EditValueChanged;
                            if (Convert.ToInt32(cboListasCosteoIndependiente_Peliculas.EditValue) == 0) cboListasCosteoIndependiente_Peliculas.EditValue = null;
                            MostrarOcultarBotones(true);
                            navigationFramePeliculas.SelectedPage = navigationPagePeliculas_ListasCosteoDetalle;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gridControl_ListasCosteoDetalle_DoubleClick(object sender, EventArgs e)
        {
            GridControl gridControl = (GridControl)sender;
            if (gridControl != null)
            {
                GridView gridView = gridControl.FocusedView as GridView;
                if (gridView != null && gridView.FocusedRowHandle >= 0)
                {
                    DataRow row = gridView.GetDataRow(gridView.FocusedRowHandle);
                    if (row != null)
                    {
                        int articulosID = Convert.ToInt32(row["ArticulosID"]);
                        DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
                        Componentes form = new Componentes(articulosID);
                        form.Show();
                    }
                }
            }
        }
        private void gridView_ListasCosteoDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view != null)
                {
                    if (e.Value.ToString() == string.Empty || (e.Column.FieldName == "Margen" && Convert.ToInt32(e.Value) >= 100))
                    {
                        if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                        {
                            gridViewPCA_ListasCosteoDetalle.CellValueChanged -= gridView_ListasCosteoDetalle_CellValueChanged;
                            gridViewPCA_ListasCosteoDetalle.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0);
                            gridViewPCA_ListasCosteoDetalle.CellValueChanged += gridView_ListasCosteoDetalle_CellValueChanged;
                        }
                        else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                        {
                            gridViewHerramientas_ListasCosteoDetalle.CellValueChanged -= gridView_ListasCosteoDetalle_CellValueChanged;
                            gridViewHerramientas_ListasCosteoDetalle.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0);
                            gridViewHerramientas_ListasCosteoDetalle.CellValueChanged += gridView_ListasCosteoDetalle_CellValueChanged;
                        }
                        else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                        {
                            gridViewPeliculas_ListasCosteoDetalle.CellValueChanged -= gridView_ListasCosteoDetalle_CellValueChanged;
                            gridViewPeliculas_ListasCosteoDetalle.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0);
                            gridViewPeliculas_ListasCosteoDetalle.CellValueChanged += gridView_ListasCosteoDetalle_CellValueChanged;
                        }
                    }

                    if (e.Column.FieldName == "TipoCambio")
                    {
                        if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                        {

                        }
                        else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                        {

                        }
                        else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                        {

                        }
                    }

                    if (e.Column.FieldName == "Merma")
                    {
                        if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                        {
                            gridViewPCA_ListasCosteoDetalle.CellValueChanged -= gridView_ListasCosteoDetalle_CellValueChanged;
                            globalCL clg = new globalCL();
                            if (clg.esNumerico(e.Value.ToString()))
                            {
                                for (int i = 0; i < gridViewPCA_ListasCosteoDetalle.RowCount; i++)
                                {
                                    gridViewPCA_ListasCosteoDetalle.SetRowCellValue(i, e.Column.FieldName, e.Value);
                                }
                            }
                            gridViewPCA_ListasCosteoDetalle.CellValueChanged += gridView_ListasCosteoDetalle_CellValueChanged;
                        }
                        else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                        {
                            gridViewHerramientas_ListasCosteoDetalle.CellValueChanged -= gridView_ListasCosteoDetalle_CellValueChanged;
                            globalCL clg = new globalCL();
                            if (clg.esNumerico(e.Value.ToString()))
                            {
                                for (int i = 0; i < gridViewHerramientas_ListasCosteoDetalle.RowCount; i++)
                                {
                                    gridViewHerramientas_ListasCosteoDetalle.SetRowCellValue(i, e.Column.FieldName, e.Value);
                                }
                            }
                            gridViewHerramientas_ListasCosteoDetalle.CellValueChanged += gridView_ListasCosteoDetalle_CellValueChanged;
                        }
                        else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                        {
                            gridViewPeliculas_ListasCosteoDetalle.CellValueChanged -= gridView_ListasCosteoDetalle_CellValueChanged;
                            globalCL clg = new globalCL();
                            if (clg.esNumerico(e.Value.ToString()))
                            {
                                for (int i = 0; i < gridViewPeliculas_ListasCosteoDetalle.RowCount; i++)
                                {
                                    gridViewPeliculas_ListasCosteoDetalle.SetRowCellValue(i, e.Column.FieldName, e.Value);
                                }
                            }
                            gridViewPeliculas_ListasCosteoDetalle.CellValueChanged += gridView_ListasCosteoDetalle_CellValueChanged;
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gridView_ListasCosteoDetalle_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "CalculatedColumn" && e.IsGetData)
                {
                    if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                    {
                        decimal ultimoCosto = gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Costo") == null ? 0 : Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Costo"));
                        decimal tipoCambio = gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "TipoCambio") == null ? 0 : Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "TipoCambio"));
                        decimal incremental = gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Incremental") == null ? 0 : Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Incremental"));
                        decimal PMerma  = gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Merma") == null ? 0 : Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Merma"));
                        decimal PMargen = gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Margen") == null ? 0 : Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Margen"));
                        
                        if (incremental > 1)
                            ultimoCosto = (ultimoCosto * tipoCambio) * (1 + (incremental / 100));
                        else
                            ultimoCosto = (ultimoCosto * tipoCambio);
                        decimal costoTotal = Math.Round((ultimoCosto * (1 + (PMerma / 100))) / (1 - (PMargen / 100)), 4);
                        e.Value = costoTotal;
                    }
                    else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                    {
                        decimal ultimoCosto = gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Costo") == null ? 0 : Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Costo"));
                        decimal tipoCambio = gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "TipoCambio") == null ? 0 : Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "TipoCambio"));
                        decimal incremental = gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Incremental") == null ? 0 : Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Costo"));
                        decimal PMerma = gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Merma") == null ? 0 : Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Merma"));
                        decimal PMargen = gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Margen") == null ? 0 : Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Margen"));

                        if (incremental > 1)
                            ultimoCosto = (ultimoCosto * tipoCambio) * (1 + (incremental / 100));
                        else
                            ultimoCosto = (ultimoCosto * tipoCambio);
                        decimal costoTotal = Math.Round((ultimoCosto * (1 + (PMerma / 100))) / (1 - (PMargen / 100)), 4);
                        e.Value = costoTotal;
                    }
                    else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                    {
                        decimal ultimoCosto = gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Costo") == null ? 0 : Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Costo"));
                        decimal tipoCambio = gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "TipoCambio") == null ? 0 : Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "TipoCambio"));
                        decimal incremental = gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Incremental") == null ? 0 : Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Incremental"));
                        decimal PMerma = gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Merma") == null ? 0 : Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Merma"));
                        decimal PMargen = gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Margen") == null ? 0 : Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Margen"));

                        if (incremental > 1)
                            ultimoCosto = (ultimoCosto * tipoCambio) * (1 + (incremental / 100));
                        else
                            ultimoCosto = (ultimoCosto * tipoCambio);
                        decimal costoTotal = Math.Round((ultimoCosto * (1 + (PMerma / 100))) / (1 - (PMargen / 100)), 4);
                        e.Value = costoTotal;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gridView_ListasCosteoDetalle_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "CostoTotal")
                {
                    if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                    {
                        decimal ultimoCosto = Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Costo"));
                        decimal tipoCambio = Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "TipoCambio"));
                        decimal incremental = Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Incremental"));
                        decimal PMerma = Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Merma"));
                        decimal PMargen = Convert.ToDecimal(gridViewPCA_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Margen"));

                        if (incremental > 1)
                            ultimoCosto = (ultimoCosto * tipoCambio) * (1 + (incremental / 100));
                        else
                            ultimoCosto = (ultimoCosto * tipoCambio);
                        decimal costoTotal = Math.Round((ultimoCosto * (1 + (PMerma / 100))) / (1 - (PMargen / 100)), 4);
                        e.DisplayText = costoTotal.ToString();
                    }
                    else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                    {
                        decimal ultimoCosto = Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Costo"));
                        decimal tipoCambio = Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "TipoCambio"));
                        decimal incremental = Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Incremental"));
                        decimal PMerma = Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Merma"));
                        decimal PMargen = Convert.ToDecimal(gridViewHerramientas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Margen"));

                        if (incremental > 1)
                            ultimoCosto = (ultimoCosto * tipoCambio) * (1 + (incremental / 100));
                        else
                            ultimoCosto = (ultimoCosto * tipoCambio);
                        decimal costoTotal = Math.Round((ultimoCosto * (1 + (PMerma / 100))) / (1 - (PMargen / 100)), 4);
                        e.DisplayText = costoTotal.ToString();
                    }
                    else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                    {
                        decimal ultimoCosto = Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Costo"));
                        decimal tipoCambio = Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "TipoCambio"));
                        decimal incremental = Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Incremental"));
                        decimal PMerma = Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Merma"));
                        decimal PMargen = Convert.ToDecimal(gridViewPeliculas_ListasCosteoDetalle.GetListSourceRowCellValue(e.ListSourceRowIndex, "Margen"));

                        if (incremental > 1)
                            ultimoCosto = (ultimoCosto * tipoCambio) * (1 + (incremental / 100));
                        else
                            ultimoCosto = (ultimoCosto * tipoCambio);
                        decimal costoTotal = Math.Round((ultimoCosto * (1 + (PMerma / 100))) / (1 - (PMargen / 100)), 4);
                        e.DisplayText = costoTotal.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gridView_ListasCosteoDetalle_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = (e.Row as DataRowView).Row;
            if (row != null)
            {
                decimal ultimoCosto = Convert.ToDecimal(row["Costo"]);
                decimal tipoCambio = Convert.ToDecimal(row["TipoCambio"]);
                decimal incremental = Convert.ToDecimal(row["Incremental"]);
                decimal PMerma = Convert.ToDecimal(row["Merma"]);
                decimal PMargen = Convert.ToDecimal(row["Margen"]);
                
                ultimoCosto = (ultimoCosto * tipoCambio) * (1 + (incremental / 100));
                decimal costoTotal = Math.Round((ultimoCosto * (1 + (PMerma / 100))) / (1 - (PMargen / 100)), 4);
                row["CostoTotal"] = costoTotal;
            }
        }
        private void cboListasCosteoIndependiente_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit cbo = (LookUpEdit)sender;
            if (cbo != null)
            {
                DataRowView dataRow = cbo.GetSelectedDataRow() as DataRowView;

                if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                {
                    if (dataRow != null && dataRow.Row["Categoria"].ToString() != "PCA")
                    {
                        MessageBox.Show("Debe de escoger una lista de costeo que pertenezca a la misma categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cbo.EditValue = null;
                        return;
                    }

                    if (cboListasCosteoIndependiente_PCA.EditValue != null)
                    {
                        CosteosArticulosCL cl = new CosteosArticulosCL();
                        cl.strTabla = "PCA";
                        cl.ListasCosteo = ListasCosteo;
                        cl.intListasCosteoIDIndependiente = Convert.ToInt32(cboListasCosteoIndependiente_PCA.EditValue);
                        cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_PCA.EditValue);
                        ListasCosteoDetalle = cl.CosteoArticulosGridIndependiente();
                        gridControlPCA_ListasCosteoDetalle.DataSource = ListasCosteoDetalle;
                    }
                    else
                        InitGridControlPCA_ListasCosteoDetalle();
                }
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                {
                    if (dataRow != null && dataRow.Row["Categoria"].ToString() != "HERRAMIENTAS")
                    {
                        MessageBox.Show("Debe de escoger una lista de costeo que pertenezca a la misma categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cbo.EditValue = null;
                        return;
                    }

                    if (cboListasCosteoIndependiente_Herramientas.EditValue != null)
                    {
                        CosteosArticulosCL cl = new CosteosArticulosCL();
                        cl.strTabla = "HERRAMIENTAS";
                        cl.ListasCosteo = ListasCosteo;
                        cl.intListasCosteoIDIndependiente = Convert.ToInt32(cboListasCosteoIndependiente_Herramientas.EditValue);
                        cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_Herramientas.EditValue);
                        ListasCosteoDetalle = cl.CosteoArticulosGridIndependiente();
                        gridControlHerramientas_ListasCosteoDetalle.DataSource = ListasCosteoDetalle;
                    }
                    else
                        InitGridControlHerramientas_ListasCosteoDetalle();
                }
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                {
                    if (dataRow != null && dataRow.Row["Categoria"].ToString() != "PELICULAS")
                    {
                        MessageBox.Show("Debe de escoger una lista de costeo que pertenezca a la misma categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cbo.EditValue = null;
                        return;
                    }

                    if (cboListasCosteoIndependiente_Peliculas.EditValue != null)
                    {
                        CosteosArticulosCL cl = new CosteosArticulosCL();
                        cl.strTabla = "PELICULAS";
                        cl.ListasCosteo = ListasCosteo;
                        cl.intListasCosteoIDIndependiente = Convert.ToInt32(cboListasCosteoIndependiente_Peliculas.EditValue);
                        cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_Peliculas.EditValue);
                        ListasCosteoDetalle = cl.CosteoArticulosGridIndependiente();
                        gridControlPeliculas_ListasCosteoDetalle.DataSource = ListasCosteoDetalle;
                    }
                    else
                        InitGridControlPeliculas_ListasCosteoDetalle();
                }
            }
        }
        private void cboListasPrecio_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit cbo = (LookUpEdit)sender;
            if(cbo != null)
            {
                if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                {
                    CosteosArticulosCL cl = new CosteosArticulosCL();
                    cl.strTabla = "PCA";
                    cl.ListasCosteo = ListasCosteo;
                    cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_PCA.EditValue);
                    ListasCosteoDetalle = cl.CosteoArticulosGrid();
                    gridControlPCA_ListasCosteoDetalle.DataSource = ListasCosteoDetalle;
                }
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                {
                    CosteosArticulosCL cl = new CosteosArticulosCL();
                    cl.strTabla = "HERRAMIENTAS";
                    cl.ListasCosteo = ListasCosteo;
                    cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_Herramientas.EditValue);
                    ListasCosteoDetalle = cl.CosteoArticulosGrid();
                    gridControlHerramientas_ListasCosteoDetalle.DataSource = ListasCosteoDetalle;
                }
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                {
                    CosteosArticulosCL cl = new CosteosArticulosCL();
                    cl.strTabla = "PELICULAS";
                    cl.ListasCosteo = ListasCosteo;
                    cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_Peliculas.EditValue);
                    ListasCosteoDetalle = cl.CosteoArticulosGrid();
                    gridControlPeliculas_ListasCosteoDetalle.DataSource = ListasCosteoDetalle;
                }
            }
        }
        private void cboCanalVentas_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit cbo = (LookUpEdit)sender;
            if (cbo != null)
            {
                CosteosArticulosCL cl = new CosteosArticulosCL();
                ListasCosteo.Rows[0]["CanalVentasID"] = Convert.ToInt32(cbo.EditValue);
                if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePCA)
                {
                    cl.strTabla = "PCA";
                    cl.ListasCosteo = ListasCosteo;
                    cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_PCA.EditValue);
                    ListasCosteoDetalle = cl.CosteoArticulosGrid();
                    gridControlPCA_ListasCosteoDetalle.DataSource = ListasCosteoDetalle;
                }
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPageHerramientas)
                {
                    cl.strTabla = "HERRAMIENTAS";
                    cl.ListasCosteo = ListasCosteo;
                    cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_Herramientas.EditValue);
                    ListasCosteoDetalle = cl.CosteoArticulosGrid();
                    gridControlHerramientas_ListasCosteoDetalle.DataSource = ListasCosteoDetalle;
                }
                else if (navigationFrameCosteoArticulos.SelectedPage == navigationPagePeliculas)
                {
                    cl.strTabla = "PELICULAS";
                    cl.ListasCosteo = ListasCosteo;
                    cl.intListasPrecioID = Convert.ToInt32(cboListasPrecio_Peliculas.EditValue);
                    ListasCosteoDetalle = cl.CosteoArticulosGrid();
                    gridControlPeliculas_ListasCosteoDetalle.DataSource = ListasCosteoDetalle;
                }
            }
        }
        #endregion INTERACCIONES USUARIO

        
    }
}