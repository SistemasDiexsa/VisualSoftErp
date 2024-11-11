using DevExpress.Data.Filtering.Helpers;
using DevExpress.Xpo.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraNavBar;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using VisualSoftErp.Catalogos.Inv;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Inventario.Clases;
using VisualSoftErp.Operacion.Inventario.Designers;
using static VisualSoftErp.Operacion.Inventario.Formas.Produccion2Form;

namespace VisualSoftErp.Operacion.Inventario.Formas
{
    public partial class RequisicionMaterial : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region VARIABLES
        private bool permisosEscritura;
        int AñoFiltro;
        int MesFiltro;
        int intFolio;        
        private BindingList<RequisicionMaterialDetalleCL> requisicionMaterialDetalle;
        private DataTable dtRequisicionMaterial { get; set; } = new DataTable()
        {
            Columns =
            {
                new DataColumn("intFolio", typeof(int)),
                new DataColumn("dateFecha", typeof(DateTime)),
                new DataColumn("intStatus", typeof(int)),
                new DataColumn("strObservaciones", typeof(string)),
                new DataColumn("strOrdenCompraSerie", typeof(string)),
                new DataColumn("intOrdenCompraFolio", typeof(int)),
                new DataColumn("intUsuariosID", typeof(int)),
                new DataColumn("intUsuarioAutorizacionID", typeof(int)),
                new DataColumn("strMaquinaAutorizado", typeof(string)),
                new DataColumn("intUsuarioCancelacionID", typeof(int)),
                new DataColumn("strMaquinaCancelacion", typeof(string)),
                new DataColumn("dateFechaCancelacion", typeof(DateTime)),
                new DataColumn("strRazonCancelacion", typeof(string))
            }
        };
        
        public DataTable dtRequisicionMaterialDetalle { get; set; } = new DataTable()
        {
            Columns =
            {
                new DataColumn("intFolio", typeof(int)),
                new DataColumn("intArticulosID", typeof(int)),
                new DataColumn("decCantidadRequerida", typeof(decimal)),
                new DataColumn("dateFechaRequerida", typeof(DateTime)),
            }
        };
        private class RequisicionCL
        {
            public int intFolio { get; set; }
            public DateTime dateFecha { get; set; }
            public int intStatus { get; set; }
            public string strObservaciones { get; set; }
            public string strOrdenCompraSerie { get; set; }
            public int intOrdenCompraFolio { get; set; }
            public int intUsuariosID { get; set; }
            public int intUsuarioAutorizacionID { get; set; }
            public string strMaquinaAutorizado { get; set; }
            public DateTime dateFechaCancelación { get; set; }
            public string strRazonCancelacion { get; set; }
        }
        private class RequisicionMaterialDetalleCL
        {
            public int intFolio { get; set; }
            public int intArticulosID { get; set; }
            public string strSKU { get; set; }
            public string strUnidadMedida { get; set; }
            public decimal decCantidadRequerida { get; set; }
            public DateTime dateFechaRequerida { get; set; }
        }
        #endregion VARIABLES


        public RequisicionMaterial()
        {
            InitializeComponent();
        }


        #region LOAD
        private void RequisicionMaterial_Load(object sender, EventArgs e)
        {
            PermisosEscritura();
            InitGridPrincipal();
            InitGridDetalle();
            BotonesEdicion(false);
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
        private void InitGridDetalle()
        {

            requisicionMaterialDetalle = new BindingList<RequisicionMaterialDetalleCL>();
            requisicionMaterialDetalle.AllowNew = true;
            gridControlDetalle.DataSource = requisicionMaterialDetalle;
            
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            gridColumnSKU.OptionsColumn.ReadOnly = true;
            gridColumnSKU.OptionsColumn.AllowEdit = false;
            gridColumnSKU.OptionsColumn.AllowEdit = false;

            gridColumnUnidadMedida.OptionsColumn.ReadOnly = true;
            gridColumnUnidadMedida.OptionsColumn.AllowEdit = false;
            gridColumnUnidadMedida.OptionsColumn.AllowEdit = false;
        }
        #endregion INIT GRID


        #region NAVIGATION
        private void navBarControlRequisicionMaterial_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
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
        private void bbiNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            Nuevo();
        }
        private void bbiVer_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
                MessageBox.Show("Selecciona un renglón");
            else
                Ver();
        }
        private void bbiGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            Guardar();
        }
        private void bbiCancelar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
                MessageBox.Show("Seleccione un renglón");
            else
                Cancelar();
        }
        private void bbiAutorizar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
                MessageBox.Show("Seleccione un renglón");
            else
                Autorizar();
        }
        private void bbiImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
                MessageBox.Show("Selecciona un renglón");
            else
                Imprimir();
        }
        private void bbiRegresar_ItemClick(object sender, ItemClickEventArgs e)
        {
            Regresar();
        }
        private void btnProceder_Click(object sender, EventArgs e)
        {
            ProcederCancelacion();
        }
        private void btnCerrarPopUp_Click(object sender, EventArgs e)
        {
            CerrarPopUp();
        }
        #endregion BOTONES
        

        #region INTERACCIONES USUARIO
        private void txtObservaciones_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEdit = (TextEdit)sender;
            if (textEdit != null)
                if (dtRequisicionMaterial.Rows.Count > 0)
                    dtRequisicionMaterial.Rows[0]["strObservaciones"] = textEdit.Text;
        }
        private void gridViewPrincipal_Click(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            if (view != null)
                intFolio = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Folio"));
        }
        private void gridViewPrincipal_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            if (view != null)
            {
                intFolio = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Folio"));
                Ver();
            }
        }
        private void gridViewPrincipal_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                GridView view = (GridView)sender;
                string status = view.GetRowCellValue(e.RowHandle, "Status").ToString();
                if (status == "Cancelada")
                    e.Appearance.ForeColor = Color.Red;
            }
        }
        private void gridControlDetalle_ProcessGridKey(object sender, KeyEventArgs e)
        {
            GridControl control = sender as GridControl;
            GridView view = control.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                view.DeleteRow(view.FocusedRowHandle);
                e.Handled = true;
            }
        }
        private void gridViewDetalle_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = (GridView)sender;
            if (view != null)
                view.SetRowCellValue(e.RowHandle, "dateFechaRequerida", DateTime.Now);
        }
        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = (GridView)sender;
            if (view != null)
            {
                globalCL clg = new globalCL();
                if (e.Column == gridColumnArticulosID)
                {
                    int articulosID = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "intArticulosID"));
                    DataRowView dataRowRepositoryItem = repositoryItemLookUpEditArticulosID.GetDataSourceRowByKeyValue(articulosID) as DataRowView;

                    view.SetRowCellValue(view.FocusedRowHandle, gridColumnSKU, dataRowRepositoryItem["Articulo"].ToString());
                    view.SetRowCellValue(view.FocusedRowHandle, gridColumnUnidadMedida, dataRowRepositoryItem["NombreUM"].ToString());
                }
            }
        }
        private void popUpConfirmarLogin_Leave(object sender, EventArgs e)
        {
            CerrarPopUp();
        }
        private void popUpConfirmarLogin_Paint(object sender, PaintEventArgs e)
        {
            if (sender is PopupControlContainer popup)
            {
                int x = (this.Width - popup.Width) / 2;
                int y = (this.Height - popup.Height) / 2;
                popup.Location = new Point(x, y);
            }
        }
        #endregion INTERACCIONES USUARIO


        private void CerrarPopUp()
        {
            txtRazonCancelacion.Text = string.Empty;
            popUpConfirmarLogin.Hide();
        }

        private void PermisosEscritura()
        {
            globalCL clg = new globalCL();
            UsuariosCL usuarios = new UsuariosCL();

            clg.strPrograma = "0345";
            if (clg.accesoSoloLectura()) permisosEscritura = false;
            else permisosEscritura = true;
        }

        private void LlenarGridPrincipal()
        {
            RequisicionMaterialCL cl = new RequisicionMaterialCL();
            cl.intAño = AñoFiltro;
            cl.intMes = MesFiltro;
            gridControlPrincipal.DataSource = cl.RequisicionMaterialGrid();

            globalCL clg = new globalCL();
            string strGridCaption = "Requisiciones de Material de ";
            
            if (MesFiltro == 0) strGridCaption += "todo " + AñoFiltro.ToString();
            else strGridCaption += clg.NombreDeMesCompleto(MesFiltro).ToLower() + " del " + AñoFiltro.ToString();

            gridViewPrincipal.ViewCaption = strGridCaption;
        }

        private void BotonesEdicion(bool isEditing)
        {
            if (isEditing)
            {
                bbiNuevo.Enabled = false;
                bbiVer.Enabled = false;
                bbiGuardar.Enabled = (intFolio == 0 && permisosEscritura) ? true : false;
                bbiCancelar.Enabled = (intFolio == 0 && permisosEscritura) ? false : true;
                bbiAutorizar.Enabled = (intFolio == 0 && permisosEscritura) ? false : true;
                bbiImprimir.Enabled = (intFolio == 0) ? false : true;
                bbiRegresar.Enabled = true;
            }
            else
            {
                bbiNuevo.Enabled = true;
                bbiVer.Enabled = true;
                bbiGuardar.Enabled = false;
                bbiCancelar.Enabled = true;
                bbiAutorizar.Enabled = true;
                bbiImprimir.Enabled = true;
                bbiRegresar.Enabled = false;
            }
        }

        private void AgregaAñosNavBar()
        {
            try
            {
                globalCL cl = new globalCL();
                DataTable dt = new DataTable("Años");
                dt.Columns.Add("Año", Type.GetType("System.Int32"));
                cl.strTabla = "RequisicionMaterial";
                dt = cl.NavbarAños();

                NavBarItem item1 = new NavBarItem();
                foreach (DataRow dr in dt.Rows)
                {
                    item1.Caption = dr["Año"].ToString();
                    item1.Name = dr["Año"].ToString();
                    navBarGroupAños.ItemLinks.Add(item1);
                    item1 = new NavBarItem();
                }
                navBarControlRequisicionMaterial.ActiveGroup = navBarGroupMeses;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AgregaAñosNavBar:" + ex.Message);
            }
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Articulos";
            repositoryItemLookUpEditArticulosID.ValueMember = "Clave";
            repositoryItemLookUpEditArticulosID.DisplayMember = "Des";
            repositoryItemLookUpEditArticulosID.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulosID.ForceInitialize();
            repositoryItemLookUpEditArticulosID.PopulateColumns();
            repositoryItemLookUpEditArticulosID.Columns["Clave"].Visible = false;
            repositoryItemLookUpEditArticulosID.Columns["FactorUM2"].Visible = false;
            repositoryItemLookUpEditArticulosID.Columns["Articulo"].Visible = false;
            repositoryItemLookUpEditArticulosID.Columns["ArticulobaseparacosteoID"].Visible = false;
            repositoryItemLookUpEditArticulosID.Columns["Largo"].Visible = false;
            repositoryItemLookUpEditArticulosID.Columns["NombreUM"].Visible = false;
            repositoryItemLookUpEditArticulosID.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulosID.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }

        private void LimpiaCajas()
        {
            dateFecha.Text = DateTime.Now.ToShortDateString();
            txtFolio.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            requisicionMaterialDetalle.Clear();
            dtRequisicionMaterial.Rows.Clear();
            dtRequisicionMaterial.Rows.Add(
                0,
                DateTime.Now,
                1,
                "",
                "",
                0,
                globalCL.gv_UsuarioID,
                0,
                "",
                0,
                "",
                null,
                ""
            );
            dtRequisicionMaterialDetalle.Rows.Clear();
        }
            
        private string Validar()
        {
            string result = "OK";
            if (gridViewDetalle.IsEditing)
            {
                gridViewDetalle.PostEditor();
                gridViewDetalle.UpdateCurrentRow();
            }

            if (txtFolio.Text == string.Empty)
                return "El campo Folio no puede ir vacio";

            if (dateFecha.DateTime.Date != DateTime.Now.Date)
            {
                DialogResult dialog = MessageBox.Show("La fecha que se ingresó no es la fecha actual.\n¿ Desea Continuar ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dialog != DialogResult.Yes)
                    return "";
            }

            
            bool isEmpty = true;
            int cantidadRequerida = 0;
            int articulosID = 0;
            DateTime fechaRequerida;

            for (int i = 0; i <= gridViewDetalle.RowCount; i++)
            {
                object row = gridViewDetalle.GetRow(i);
                if (row != null)
                {
                    cantidadRequerida = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "decCantidadRequerida"));
                    if (cantidadRequerida == 0) result = "Los artículos requeridos no pueden tener cero en cantidad requerida";

                    articulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "intArticulosID"));
                    if (articulosID == 0) result = "Todas las filas de la tabla deben de tener el artículo requerido seleccionado";

                    fechaRequerida = Convert.ToDateTime(gridViewDetalle.GetRowCellValue(i, "dateFechaRequerida"));
                    if (fechaRequerida == null) result = "Los artículos reueridos deben de tener seleccionada la fecha en que se requiere";
                    
                    isEmpty = false;
                }
            }

            if (isEmpty)
                return "Favor de llenar la tabla con los artículos requeridos";

            return result;
        }

        private void LlenarDataTables()
        {
            dtRequisicionMaterialDetalle.Rows.Clear();
            foreach (var item in requisicionMaterialDetalle)
            {
                DataRow row = dtRequisicionMaterialDetalle.NewRow();
                row["intFolio"] = intFolio;
                row["intArticulosID"] = item.intArticulosID;
                row["decCantidadRequerida"] = item.decCantidadRequerida;
                row["dateFechaRequerida"] = item.dateFechaRequerida;
                dtRequisicionMaterialDetalle.Rows.Add(row);
            }
        }

        private void LlenarBindingListDetalle()
        {
            foreach (DataRow row in dtRequisicionMaterialDetalle.Rows)
            {

                RequisicionMaterialDetalleCL detalle = new RequisicionMaterialDetalleCL
                {
                    intFolio = Convert.ToInt32(row["intFolio"]),
                    intArticulosID = Convert.ToInt32(row["intArticulosID"]),
                    decCantidadRequerida = Convert.ToInt32(row["decCantidadRequerida"]),
                    dateFechaRequerida = Convert.ToDateTime(row["dateFechaRequerida"])
                };
                requisicionMaterialDetalle.Add(detalle);

                DataRowView dataRowRepositoryItem = repositoryItemLookUpEditArticulosID.GetDataSourceRowByKeyValue(Convert.ToInt32(row["intArticulosID"])) as DataRowView;
                gridViewDetalle.SetRowCellValue(gridViewDetalle.FocusedRowHandle, gridColumnSKU, dataRowRepositoryItem["Articulo"].ToString());
                gridViewDetalle.SetRowCellValue(gridViewDetalle.FocusedRowHandle, gridColumnUnidadMedida, dataRowRepositoryItem["NombreUM"].ToString());
            }
        }
       
        private void LlenaCajas()
        {
            try
            {
                RequisicionMaterialCL cl = new RequisicionMaterialCL();
                cl.intFolio = intFolio;

                string result = cl.RequisicionMaterialLlenaCajas();
                if (result != "OK")
                    throw new Exception(result);

                dtRequisicionMaterial = cl.dtRequisicionMaterial;
                dtRequisicionMaterialDetalle = cl.dtRequisicionMaterialDetalle;
                LlenarBindingListDetalle();
                txtFolio.Text = dtRequisicionMaterial.Rows[0]["intFolio"].ToString();
                dateFecha.Text = dtRequisicionMaterial.Rows[0]["dateFecha"].ToString();
                txtObservaciones.Text = dtRequisicionMaterial.Rows[0]["strObservaciones"].ToString();
                
                switch(Convert.ToInt32(dtRequisicionMaterial.Rows[0]["intStatus"]))
                {
                    case 1:
                        lblStatus.Text = "Registrada";
                        break;
                    case 2:
                        lblStatus.Text = "Autorizada";
                        break;
                    case 3:
                        lblStatus.Text = "Relacionada a Orden de Compra";
                        break;
                    case 5:
                        lblStatus.Text = "Cancelada";
                        break;
                    default:
                        lblStatus.Text = "Status desconocido";
                        break;
                }

                switch (Convert.ToInt32(dtRequisicionMaterial.Rows[0]["intOrdenCompraFolio"]))
                {
                    case 0:
                        lblOrdenCompra.Text = "Sin Orden de Compra";
                        break;
                    default:
                        string serieOC = dtRequisicionMaterial.Rows[0]["strOrdenCompraSerie"].ToString();
                        string folioOC = dtRequisicionMaterial.Rows[0]["intOrdenCompraFolio"].ToString();
                        lblOrdenCompra.Text = serieOC + folioOC;
                        break;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en LlenaCajas: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ProcederCancelacion()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRazonCancelacion.Text.Trim()))
                {
                    MessageBox.Show("Escriba la razón por la cual cancela la requisición");
                    return;
                }

                RequisicionMaterialCL cl = new RequisicionMaterialCL();
                cl.intFolio = intFolio;
                cl.strRazonCancelacion = txtRazonCancelacion.Text;

                string result = cl.RequisicionMaterialCancelar();
                if (result != "OK")
                    throw new Exception(result);
                else
                {
                    lblStatus.Text = "Cancelada";
                    LlenarGridPrincipal();
                    MessageBox.Show("Cancelado Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Cancelar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Nuevo()
        {
            intFolio = 0;
            lblStatus.Text = "Sin Registrar";
            lblOrdenCompra.Text = "Sin Orden de Compra";
            LimpiaCajas();
            BotonesEdicion(true);
            globalCL global = new globalCL();
            global.strDoc = "RequisicionMaterial";
            global.strSerie = string.Empty;
            string Result = global.DocumentosSiguienteID();

            if (Result != "OK")
            {
                MessageBox.Show("No se pudo leer el siguiente folio");
                return;
            }
            else
            {
                txtFolio.Text = global.iFolio.ToString();
                navigationFrameRequisicionMaterial.SelectedPage = navigationPageDatos;
            }
        }

        private void Ver()
        {
            LlenaCajas();
            BotonesEdicion(true);
            navigationFrameRequisicionMaterial.SelectedPage = navigationPageDatos;
        }

        private void Guardar()
        {
            try
            {
                string result = Validar();
                if (result != "OK")
                {
                    if (result != string.Empty)
                        MessageBox.Show(result, "Error al validar información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LlenarDataTables();
                RequisicionMaterialCL cl = new RequisicionMaterialCL();
                cl.dtRequisicionMaterial = dtRequisicionMaterial;
                cl.dtRequisicionMaterialDetalle = dtRequisicionMaterialDetalle;
                result = cl.RequisicionMaterialCrud();

                if (result == "OK")
                {
                    intFolio = Convert.ToInt32(txtFolio.Text);
                    lblStatus.Text = "Registrada";
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
                if (status == "Cancelada" || lblStatus.Text == "Cancelada")
                {
                    MessageBox.Show("Este folio se encuentra cancelado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult dialog = MessageBox.Show("¿Está seguro de cancelar el folio " + intFolio.ToString() + "?", "Cancelar Folio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog != DialogResult.Yes) return;

                popUpConfirmarLogin.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Cancelar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Autorizar()
        {
            try
            {
                string status = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status").ToString();
                if (status == "Cancelada" || lblStatus.Text == "Cancelada")
                {
                    MessageBox.Show("Este folio se encuentra cancelado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RequisicionMaterialCL cl = new RequisicionMaterialCL();
                cl.intFolio = intFolio;

                string result = cl.RequisicionMaterialAutorizar();
                if (result != "OK")
                    throw new Exception(result);
                else
                {
                    lblStatus.Text = "Autorizada";
                    LlenarGridPrincipal();
                    MessageBox.Show("Autorizada Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Autorizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                RequisicionMaterialDesigner rep = new RequisicionMaterialDesigner();
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter1"].Value = intFolio;
                
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
                    navigationFrameRequisicionMaterial.SelectedPage = navigationPageReporte;
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
            navigationFrameRequisicionMaterial.SelectedPage = navigationPageGrid;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            LimpiaCajas();
            BotonesEdicion(false);
            documentViewer1.DocumentSource = null;
        }   
    }
}