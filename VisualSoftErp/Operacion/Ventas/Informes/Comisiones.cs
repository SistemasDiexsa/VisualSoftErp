using DevExpress.Mvvm.POCO;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Design;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Layout;
using DevExpress.XtraNavBar;
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
using VisualSoftErp.Operacion.Ventas.Clases;
using DevExpress.XtraGrid;
using DevExpress.XtraRichEdit.Model;
using DevExpress.Utils;
using DevExpress.Pdf.Native;
using System.Web.UI;
using VisualSoftErp.Operacion.Ventas.Designers;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class Comisiones : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private int intAgentesID;
        private int intConceptosComisionesID;
        private bool isEditing;
        private bool permisosEscritura;
        private bool nuevoConceptoAgregado = false;
        private DataTable ComisionesAgentes { get; set; } = new DataTable
        {
            Columns =
            {
                new DataColumn("ComisionesAgentesID", typeof(int)),
                new DataColumn("AgentesID", typeof(int)),
                new DataColumn("ConceptosComisionesID", typeof(int)),
                new DataColumn("Porcentaje", typeof(decimal)),
                new DataColumn("Meta", typeof(int))
            }
        };

        public Comisiones()
        {
            InitializeComponent();
        }

        private void Comisiones_Load(object sender, EventArgs e)
        {
            ribbonPageHome.Visible = true;
            if (ribbonControl.MergeOwner != null)
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            SoloLectura();
            CargarCombos();
            InitGridControlAgentes();
            InitGridControlConceptos();
            InitGridControlConceptosForm();
            MostrarOcultarFormConceptos(false);
            navigationFrame.SelectedPage = NavigationPageAgentes;
            AdjustColumnWidths();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void SoloLectura()
        {
            globalCL clg = new globalCL();
            clg.strPrograma = "0449";
            if (clg.accesoSoloLectura())
            {
                bbiGuardarConceptosAgente.Enabled = false;

                bbiNuevoConcepto.Enabled = false;
                bbiGuardarConcepto.Enabled = false;
                bbiEditarConcepto.Enabled = false;
                bbiEliminarConcepto.Enabled = false;

                permisosEscritura = false;
            }
            else
                permisosEscritura = true;
        }
        
        private void CargarCombos()
        {
            combosCL combos = new combosCL();
            BindingSource src = new BindingSource();
            globalCL global = new globalCL();

            #region COMBO CONCEPTOS COMISIONES POR AGENTE
            CargarCombosConceptosGrid();
            #endregion COMBO CONCEPTOS COMISIONES POR AGENTE

            #region COMBO FORMAS DE CALCULO PARA COMISIONES
            combos.strTabla = "FormasCalculoComisiones";
            cboFormasCalculo.Properties.ValueMember = "Clave";
            cboFormasCalculo.Properties.DisplayMember = "Des";
            cboFormasCalculo.Properties.DataSource = combos.CargaCombos();
            cboFormasCalculo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFormasCalculo.Properties.ForceInitialize();
            cboFormasCalculo.Properties.PopulateColumns();
            cboFormasCalculo.Properties.Columns["Clave"].Visible = false;
            cboFormasCalculo.Properties.NullText = "Seleccione una Forma de Calculo";
            cboFormasCalculo.EditValue = null;
            #endregion COMBO FORMAS DE CALCULO PARA COMISIONES

            #region COMBO LINEAS
            combos.strTabla = "Lineas";
            cboLineas.Properties.ValueMember = "Clave";
            cboLineas.Properties.DisplayMember = "Des";
            cboLineas.Properties.DataSource = combos.CargaCombos();
            cboLineas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboLineas.Properties.ForceInitialize();
            cboLineas.Properties.PopulateColumns();
            cboLineas.Properties.Columns["Clave"].Visible = false;
            cboLineas.Properties.NullText = "Seleccione una Línea de Artículos";
            cboLineas.EditValue = null;
            #endregion COMBO LINEAS

            #region COMBO FAMILIAS
            combos.strTabla = "Familias";
            cboFamilias.Properties.ValueMember = "Clave";
            cboFamilias.Properties.DisplayMember = "Des";
            cboFamilias.Properties.DataSource = combos.CargaCombos();
            cboFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilias.Properties.ForceInitialize();
            cboFamilias.Properties.PopulateColumns();
            cboFamilias.Properties.Columns["Clave"].Visible = false;
            cboFamilias.Properties.Columns["CodigoArticulo"].Visible = false;
            cboFamilias.Properties.Columns["LineasID"].Visible = false;
            cboFamilias.Properties.NullText = "Seleccione una Familia de Artículos";
            cboFamilias.EditValue = null;
            #endregion COMBO FAMILIAS

            #region COMBO SUBFAMILIAS
            CargarCombosSubFamilias();
            #endregion COMBO SUBFAMILIAS

            #region COMBO ARTICULOS
            combos.strTabla = "Articulos";
            cboArticulos.Properties.ValueMember = "Clave";
            cboArticulos.Properties.DisplayMember = "Des";
            cboArticulos.Properties.DataSource = combos.CargaCombos();
            cboArticulos.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulos.Properties.ForceInitialize();
            cboArticulos.Properties.PopulateColumns();
            cboArticulos.Properties.Columns["Clave"].Visible = false;
            cboArticulos.Properties.NullText = "Seleccione un Artículo";
            cboArticulos.EditValue = null;
            #endregion COMBO ARTICULOS

        }

        private void CargarCombosSubFamilias()
        {
            if (cboFamilias.EditValue != null)
            {
                combosCL combos = new combosCL();
                BindingSource src = new BindingSource();
                globalCL global = new globalCL();

                combos.strTabla = "SubFamiliasXFamilia";
                combos.iCondicion = Convert.ToInt32(cboFamilias.EditValue);
                cboSubFamilias.Properties.ValueMember = "Clave";
                cboSubFamilias.Properties.DisplayMember = "Des";
                cboSubFamilias.Properties.DataSource = combos.CargaCombos();
                cboSubFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboSubFamilias.Properties.ForceInitialize();
                cboSubFamilias.Properties.PopulateColumns();
                cboSubFamilias.Properties.NullText = "Seleccione una SubFamilias de Artículos";
                cboSubFamilias.EditValue = null;
            }
        }

        private void CargarCombosConceptosGrid()
        {
            combosCL combos = new combosCL();
            BindingSource src = new BindingSource();
            globalCL global = new globalCL();

            combos.strTabla = "ConceptosComisiones";
            repositoryItemLookUpEditConceptosComisiones.ValueMember = "Clave";
            repositoryItemLookUpEditConceptosComisiones.DisplayMember = "Des";
            repositoryItemLookUpEditConceptosComisiones.DataSource = combos.CargaCombos();
            repositoryItemLookUpEditConceptosComisiones.ForceInitialize();
            repositoryItemLookUpEditConceptosComisiones.PopulateColumns();
            repositoryItemLookUpEditConceptosComisiones.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditConceptosComisiones.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditConceptosComisiones.NullText = "Seleccione un concepto de comision";

            if (nuevoConceptoAgregado) nuevoConceptoAgregado = false;
        }

        private void CargarComboVariable(int FormaCalculo)
        {
            combosCL combos = new combosCL();
            BindingSource src = new BindingSource();
            globalCL global = new globalCL();

            if (FormaCalculo == 1 || FormaCalculo == 5 || FormaCalculo == 6 || FormaCalculo == 7 || FormaCalculo == 8)
            {
                #region COMBO CANAL DE VENTA
                combos.strTabla = "Canalesdeventa";
                cboVariable.Properties.ValueMember = "Clave";
                cboVariable.Properties.DisplayMember = "Des";
                cboVariable.Properties.DataSource = combos.CargaCombos();
                cboVariable.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboVariable.Properties.ForceInitialize();
                cboVariable.Properties.PopulateColumns();
                cboVariable.Properties.Columns["Clave"].Visible = false;
                cboVariable.Properties.NullText = "Seleccione un Canal de Ventas";
                cboVariable.EditValue = null;
                #endregion COMBO CANAL DE VENTA
            }
            else if (FormaCalculo == 10)
            {
                #region COMBO AGENTES
                combos.strTabla = "Agentes";
                cboVariable.Properties.ValueMember = "Clave";
                cboVariable.Properties.DisplayMember = "Des";
                cboVariable.Properties.DataSource = combos.CargaCombos();
                cboVariable.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboVariable.Properties.ForceInitialize();
                cboVariable.Properties.PopulateColumns();
                cboVariable.Properties.Columns["Clave"].Visible = false;
                cboVariable.Properties.Columns["Encabezado"].Visible = false;
                cboVariable.Properties.Columns["Piedepagina"].Visible = false;
                cboVariable.Properties.Columns["Email"].Visible = false;
                cboVariable.Properties.NullText = "Seleccione un Agente de Ventas";
                cboVariable.EditValue = null;
                #endregion COMBO AGENTES
            }
        }

        private void navBarControlMenu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NavBarItemLink item = e.Link;
            if (item != null)
            {
                switch (item.ItemName)
                {
                    case "navBarItemAgentes":
                        if (nuevoConceptoAgregado) CargarCombosConceptosGrid();
                        ribbonPagePrint.Visible = false;
                        ribbonPageConceptos.Visible = false;
                        ribbonPageHome.Visible = true;
                        ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
                        navigationFrame.SelectedPage = NavigationPageAgentes;
                        break;
                    case "navBarItemConceptos":
                        ribbonPageHome.Visible = false;
                        ribbonPagePrint.Visible = false;
                        ribbonPageConceptos.Visible = true;
                        ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageConceptos.Text);
                        navigationFrame.SelectedPage = NavigationPageConceptos;
                        break;
                    case "navBarItemReporte":
                        ribbonPageHome.Visible = false;
                        ribbonPageConceptos.Visible = false;
                        ribbonPagePrint.Visible = true;
                        ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                        MostrarOcultarPopUpReporte(true);
                        break;
                }
            }
        }

        private void officeNavigationBar_ItemClick(object sender, NavigationBarItemEventArgs e)
        {
            OfficeNavigationBar officeNavigationBar = (OfficeNavigationBar)sender;

            if (officeNavigationBar != null)
            {
                NavigationBarItem item = officeNavigationBar.SelectedItem;
                switch (item.Name)
                {
                    case "navigationBarItemAgentes":
                        if (nuevoConceptoAgregado) CargarCombosConceptosGrid();
                        ribbonPagePrint.Visible = false;
                        ribbonPageConceptos.Visible = false;
                        ribbonPageHome.Visible = true;
                        ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
                        navigationFrame.SelectedPage = NavigationPageAgentes;
                        break;
                    case "navigationBarItemConceptos":
                        ribbonPageHome.Visible = false;
                        ribbonPagePrint.Visible = false;
                        ribbonPageConceptos.Visible = true;
                        ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageConceptos.Text);
                        navigationFrame.SelectedPage = NavigationPageConceptos;
                        break;
                    case "navigationBarItemReporte":
                        ribbonPageHome.Visible = false;
                        ribbonPageConceptos.Visible = false;
                        ribbonPagePrint.Visible = true;
                        ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                        MostrarOcultarPopUpReporte(true);
                        break;
                }
            }
        }


        #region AGENTES NAVPAGE

        private void InitGridControlAgentes()
        {
            agentesCL agentes = new agentesCL();
            gridControlAgentes.DataSource = agentes.AgentesGrid();

            gridViewAgentes.OptionsBehavior.ReadOnly = true;
            gridViewAgentes.OptionsBehavior.Editable = false;
            gridViewAgentes.Columns["Telefono"].Visible = false;
            gridViewAgentes.Columns["Encabezado"].Visible = false;
            gridViewAgentes.Columns["Piedepagina"].Visible = false;
        }

        private void InitGridControlConceptos()
        {
            gridControlConceptos.DataSource = ComisionesAgentes;

            gridViewConceptos.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gridViewConceptos.OptionsBehavior.AllowDeleteRows = DefaultBoolean.True;
            gridViewConceptos.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewConceptos.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewConceptos.OptionsNavigation.AutoMoveRowFocus = true;
            gridViewConceptos.Columns["ConceptosComisionesID"].ColumnEdit = repositoryItemLookUpEditConceptosComisiones;
            gridViewConceptos.Columns["ComisionesAgentesID"].Visible = false;
            gridViewConceptos.Columns["AgentesID"].Visible = false;
        }

        private void AdjustColumnWidths()
        {
            int totalWidth = gridControlConceptos.Width;
            int otraColumnaWidth = (int)(totalWidth * 0.70);
            int porcentajeColumnWidth = (int)(totalWidth * 0.15);

            gridViewConceptos.Columns["Porcentaje"].Width = porcentajeColumnWidth;
            gridViewConceptos.Columns["Meta"].Width = porcentajeColumnWidth;
            gridViewConceptos.Columns["ConceptosComisionesID"].Width = otraColumnaWidth;
        }

        private string ValidarConceptosAgentes()
        {
            string result = "OK";
            try
            {
                for (int i = 0; i < ComisionesAgentes.Rows.Count; i++)
                {
                    int X = i + 1;

                    if (Convert.ToInt32(ComisionesAgentes.Rows[i]["Porcentaje"]) < 0 || Convert.ToInt32(ComisionesAgentes.Rows[i]["Porcentaje"]) > 100)
                    {
                        result = "El porcentaje del renglón " + X.ToString() + " no puede ser menor que 0 o mayor que 100";
                        break;
                    }
                    if (ComisionesAgentes.Rows[i]["ConceptosComisionesID"].ToString() == string.Empty)
                    {
                        result = "El concepto del renglón " + X.ToString() + " no puede estar vacío";
                        break;
                    }
                    if (Convert.ToInt32(ComisionesAgentes.Rows[i]["Meta"]) < 0 || ComisionesAgentes.Rows[i]["Meta"].ToString() == string.Empty)
                    {
                        result = "La meta del renglón " + X.ToString() + " no puede estar vacío o ser menor a cero. \nEn caso de que no se vaya a considerar una meta, favor de introducir 0";
                        break;
                    }
                }

                if (ComisionesAgentes.Rows.Count == 0)
                {
                    DialogResult dialog = MessageBox.Show("¿Desea borrar todos los conceptos del agente?", "Sin Conceptos de Comisión", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.No)
                        result = "No se tienen conceptos. Seleccione un Agente y llene los conceptos de comisión";
                }

                if (intAgentesID == 0)
                    result = "Seleccione un Agente";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        private void gridViewAgentes_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            ComisionesAgentes.Clear();
            intAgentesID = Convert.ToInt32(gridViewAgentes.GetRowCellValue(e.RowHandle, "AgentesID"));

            ComisionesCL cl = new ComisionesCL();
            cl.intAgentesID = intAgentesID;
            ComisionesAgentes = cl.ComisionesAgentesGRID();
            gridControlConceptos.DataSource = ComisionesAgentes;
        }

        private void gridViewConceptos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("¿Estás seguro de que quieres eliminar este concepto?", "Confirmar Eliminación", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DataRow rowGridView = gridViewConceptos.GetFocusedDataRow();
                    DataRow rowDataTable;

                    for (int i = 0; i < ComisionesAgentes.Rows.Count; i++)
                    {
                        rowDataTable = ComisionesAgentes.Rows[i];
                        if (rowGridView != null && rowDataTable != null)
                        {
                            if (rowGridView.Equals(rowDataTable))
                            {
                                ComisionesAgentes.Rows[i].Delete();
                                ComisionesAgentes.AcceptChanges();
                                gridViewConceptos.DeleteRow(gridViewConceptos.FocusedRowHandle);
                                break;
                            }
                        }
                    }
                    Console.WriteLine(rowGridView);
                }
            }
        }

        private void bbiGuardarConceptosAgente_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string result = ValidarConceptosAgentes();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }

            ComisionesCL cl = new ComisionesCL();
            cl.intAgentesID = intAgentesID;
            cl.ComisionesAgentes = ComisionesAgentes;

            result = cl.ComisionesAgentesCRUD();

            if (result != "OK")
                MessageBox.Show(result);
            else
            {
                ComisionesAgentes = cl.ComisionesAgentesGRID();
                MessageBox.Show("Guardado Correctamente");
            }
        }

        #endregion AGENTES NAVPAGE


        #region CONCEPTOS DE COMISIONES NAVPAGE
        private void LimpiarCajas(bool guardarFormaCalculo)
        {
            if (!guardarFormaCalculo)
            {
                txtNombre.Text = string.Empty;
                cboFormasCalculo.EditValue = null;
            }
            radioGroupClientes.SelectedIndex = -1;
            cboVariable.EditValue = null;
            cboLineas.EditValue = null;
            cboFamilias.EditValue = null;
            cboSubFamilias.EditValue = null;
            cboArticulos.EditValue = null;
        }

        private void InitGridControlConceptosForm()
        {
            ComisionesCL cl = new ComisionesCL();
            gridControlConceptosForm.DataSource = cl.ConceptosComisionesGRID();
            gridViewConceptosForm.OptionsBehavior.ReadOnly = true;
            gridViewConceptosForm.OptionsBehavior.Editable = false;
        }

        private void MostrarOcultarFormConceptos(bool isVisible)
        {
            int sizeUp;
            int sizeDown;
            if (isVisible)
            {

                sizeUp = 25;
                sizeDown = 75;
                bbiEditarConcepto.Enabled = false;
                bbiGuardarConcepto.Enabled = true;
                bbiRegresar.Enabled = true;
                isEditing = true;
            }
            else
            {
                sizeUp = 0;
                sizeDown = 100;
                bbiEditarConcepto.Enabled = true;
                bbiGuardarConcepto.Enabled = false;
                bbiRegresar.Enabled = false;
                isEditing = false;
            }
            SoloLectura();
            tablePanel2.Rows[0].Height = sizeUp;
            tablePanel2.Rows[0].Style = DevExpress.Utils.Layout.TablePanelEntityStyle.Relative;
            tablePanel2.Rows[1].Height = sizeDown;
            tablePanel2.Rows[1].Style = DevExpress.Utils.Layout.TablePanelEntityStyle.Relative;
        }

        private void MostrarOcultarPopUpConjunto(bool visible)
        {
            if (visible)
            {
                combosCL combos = new combosCL();
                BindingSource src = new BindingSource();
                globalCL global = new globalCL();

                combos.strTabla = "ConceptosComisionesPorAgente";
                combos.iCondicion = Convert.ToInt32(cboVariable.EditValue);
                cboConceptoConjunto.Properties.ValueMember = "Clave";
                cboConceptoConjunto.Properties.DisplayMember = "Des";
                cboConceptoConjunto.Properties.DataSource = combos.CargaCombos();
                cboConceptoConjunto.Properties.ForceInitialize();
                cboConceptoConjunto.Properties.PopulateColumns();
                cboConceptoConjunto.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboConceptoConjunto.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                cboConceptoConjunto.Properties.NullText = "Seleccione el concepto que se compartirá";

                int locX = (this.ClientSize.Width - popupConceptoConjunto.Width) / 2;
                int locY = (this.ClientSize.Height - 200 - popupFechasReporte.Height) / 2;
                popupConceptoConjunto.Location = new Point(locX, locY);
                popupConceptoConjunto.Show();
            }
            else
            {
                popupConceptoConjunto.Hide();
            }
        }

        private string ValidarConceptosComisiones()
        {
            string result = "OK";
            int formaCalculo = Convert.ToInt32(cboFormasCalculo.EditValue);
            if (txtNombre.Text == string.Empty)
                result = "Escriba el nombre del concepto";

            if (cboFormasCalculo.EditValue == null)
                result = "Seleccione la forma de cálcular el concepto";

            if (formaCalculo == 1 || formaCalculo == 5 || formaCalculo == 7)
            {
                if (cboVariable.EditValue == null)
                {
                    switch (formaCalculo)
                    {
                        case 1:
                            result = "Seleccione el Canal de Venta";
                            break;
                        case 5:
                            result = "Seleccione el Canal de Venta";
                            break;
                        case 7:
                            result = "Seleccione el agente";
                            break;
                    }
                }
            }

            if (formaCalculo == 2 && cboLineas.EditValue == null)
                result = "Seleccione la línea de artículos";

            if (formaCalculo == 3 && cboFamilias.EditValue == null)
                result = "Seleccione la familia de artículos";

            if (formaCalculo == 4 && cboSubFamilias.EditValue == null)
                result = "Seleccione la subfamilia de artículos";

            if ((formaCalculo == 5 || formaCalculo == 6) && cboArticulos.EditValue == null)
                result = "Seleccione el artículo";

            return result;
        }

        private void ConceptosComisionesLlenarCajas()
        {
            ComisionesCL cl = new ComisionesCL();
            cl.intConceptosComisionesID = intConceptosComisionesID;
            string result = cl.ConceptosComisionesLlenarCajas();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }
            else
            {
                txtNombre.Text = cl.strNombre;
                cboFormasCalculo.EditValue = cl.intFormasCalculoComisionesID;
                radioGroupClientes.SelectedIndex = cl.intClientesNuevos;

                if (cl.intFormasCalculoComisionesID == 1 || cl.intFormasCalculoComisionesID == 5 ||
                    cl.intFormasCalculoComisionesID == 6 || cl.intFormasCalculoComisionesID == 7 ||
                    cl.intFormasCalculoComisionesID == 8)
                    cboVariable.EditValue = cl.intCanalVentasID;

                if (cl.intFormasCalculoComisionesID == 10)
                    cboVariable.EditValue = cl.intAgentesID;

                cboLineas.EditValue = cl.intLineasID;
                cboFamilias.EditValue = cl.intFamiliasID;
                cboSubFamilias.EditValue = cl.intSubFamiliasID;
                cboArticulos.EditValue = cl.intArticulosID;
                cboConceptoConjunto.EditValueChanged -= cboConceptoConjunto_EditValueChanged;
                cboConceptoConjunto.EditValue = cl.intConceptoConjunto;
                cboConceptoConjunto.EditValueChanged += cboConceptoConjunto_EditValueChanged;
                MostrarOcultarFormConceptos(true);
            }

        }

        private void bbiNuevoConcepto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intConceptosComisionesID = 0;
            LimpiarCajas(false);
            MostrarOcultarFormConceptos(true);
        }

        private void bbiEditarConcepto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intConceptosComisionesID == 0)
            {
                MessageBox.Show("Seleccione un Concepto");
                return;
            }
            MostrarOcultarFormConceptos(true);
            ConceptosComisionesLlenarCajas();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LimpiarCajas(false);
            MostrarOcultarFormConceptos(false);
        }

        private void btnCerrarPopUpConceptoConjunto_Click(object sender, EventArgs e)
        {
            cboConceptoConjunto.EditValue = null;
            popupConceptoConjunto.Hide();
        }
        
        private void bbiGuardarConcepto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string result = ValidarConceptosComisiones();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }

            int FormaCalculo = Convert.ToInt32(cboFormasCalculo.EditValue);
            ComisionesCL cl = new ComisionesCL();
            cl.intConceptosComisionesID = intConceptosComisionesID;
            cl.strNombre = txtNombre.Text;
            cl.intFormasCalculoComisionesID = Convert.ToInt32(cboFormasCalculo.EditValue);
            cl.intClientesNuevos = radioGroupClientes.SelectedIndex;
            cl.intCanalVentasID = (FormaCalculo == 1 || FormaCalculo == 5 || FormaCalculo == 6 || FormaCalculo == 7 || FormaCalculo == 8) ? Convert.ToInt32(cboVariable.EditValue) : 0;
            cl.intLineasID = Convert.ToInt32(cboLineas.EditValue);
            cl.intFamiliasID = Convert.ToInt32(cboFamilias.EditValue);
            cl.intSubFamiliasID = Convert.ToInt32(cboSubFamilias.EditValue);
            cl.intArticulosID = Convert.ToInt32(cboArticulos.EditValue);
            cl.intAgentesID = (FormaCalculo == 10 ) ? Convert.ToInt32(cboVariable.EditValue) : 0;
            cl.intConceptoConjunto = (FormaCalculo == 10) ? Convert.ToInt32(cboConceptoConjunto.EditValue) : 0;
            result = cl.ConceptosComisionesCRUD();
            if (result != "OK")
                MessageBox.Show(result);
            else
            {
                nuevoConceptoAgregado = true;
                InitGridControlConceptosForm();
                MessageBox.Show("Guardado Correctamente");
            }
        }

        private void gridControlConceptosForm_Click(object sender, EventArgs e)
        {
            if(!isEditing)
            {
                GridControl grid = (GridControl)sender;
                if (grid != null)
                {
                    GridView gridView = grid.FocusedView as GridView;
                    if (gridView != null)
                    {
                        object conceptoComisionesID = gridView.GetFocusedRowCellValue("ConceptosComisionesID");

                        if (conceptoComisionesID != null)
                            intConceptosComisionesID = Convert.ToInt32(conceptoComisionesID);
                    }
                }
            }
        }

        private void gridControlConceptosForm_DoubleClick(object sender, EventArgs e)
        {
            if (!permisosEscritura) return;

            GridControl grid = (GridControl)sender;
            if(grid != null)
            {
                GridView gridView = grid.FocusedView as GridView;
                if (gridView != null)
                {
                    object conceptoComisionesID = gridView.GetFocusedRowCellValue("ConceptosComisionesID");

                    if (conceptoComisionesID != null)
                    {
                        intConceptosComisionesID = Convert.ToInt32(conceptoComisionesID);
                        ConceptosComisionesLlenarCajas();
                    }
                }
            }
        }

        private void bbiEliminarConcepto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intConceptosComisionesID == 0)
            {
                MessageBox.Show("Seleccione un Concepto");
                return;
            }

            DialogResult dialog = MessageBox.Show("¿Desea eliminar éste concepto?", "Eliminar Concepto de Comisión", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                ComisionesCL cl = new ComisionesCL();
                cl.intConceptosComisionesID = intConceptosComisionesID;
                string result = cl.ConceptosComisionesEliminar();
                if(result != "OK")
                {
                    MessageBox.Show(result);
                    return;
                }
                else
                {
                    nuevoConceptoAgregado = true;
                    InitGridControlConceptosForm();
                    LimpiarCajas(false);
                    MessageBox.Show("Eliminado Correctamente");
                }
            }
        }

        private void cboFormasCalculo_EditValueChanged(object sender, EventArgs e)
        {
            LimpiarCajas(true);
            LookUpEdit formasCalculo = (LookUpEdit)sender;
            if (formasCalculo != null)
            {
                switch (formasCalculo.EditValue)
                {
                    // LIMPIAR CAJAS
                    case null:
                        tablePanelVariable.Visible = false;
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        radioGroupClientes.Enabled = true;
                        break;
                    // POR CANAL DE VENTAS
                    case 1:
                        tablePanelVariable.Visible = true;
                        lblVariable.Text = "Canal de Venta";
                        CargarComboVariable(Convert.ToInt32(formasCalculo.EditValue));
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        radioGroupClientes.Enabled = true;
                        break;
                    // POR LINEA DE ARTICULOS
                    case 2:
                        tablePanelVariable.Visible = false;
                        cboLineas.Enabled = true;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        radioGroupClientes.Enabled = true;
                        break;
                    // POR FAMILIA DE ARTICULOS
                    case 3:
                        tablePanelVariable.Visible = false;
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = true;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        radioGroupClientes.Enabled = true;
                        break;
                    // POR SUBFAMILIA DE ARTICULOS
                    case 4:
                        tablePanelVariable.Visible = false;
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = true;
                        cboSubFamilias.Enabled = true;
                        cboArticulos.Enabled = false;
                        radioGroupClientes.Enabled = true;
                        break;
                    // POR CANAL DE VENTA Y LÍNEA
                    case 5:
                        tablePanelVariable.Visible = true;
                        lblVariable.Text = "Canal de Venta";
                        CargarComboVariable(Convert.ToInt32(formasCalculo.EditValue));
                        cboLineas.Enabled = true;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        radioGroupClientes.Enabled = true;
                        break;
                    // POR CANAL DE VENTA Y FAMILIA
                    case 6:
                        tablePanelVariable.Visible = true;
                        lblVariable.Text = "Canal de Venta";
                        CargarComboVariable(Convert.ToInt32(formasCalculo.EditValue));
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = true;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        radioGroupClientes.Enabled = true;
                        break;
                    // POR CANAL DE VENTA Y SUBFAMILIA
                    case 7:
                        tablePanelVariable.Visible = true;
                        lblVariable.Text = "Canal de Venta";
                        CargarComboVariable(Convert.ToInt32(formasCalculo.EditValue));
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = true;
                        cboSubFamilias.Enabled = true;
                        cboArticulos.Enabled = false;
                        radioGroupClientes.Enabled = true;
                        break;
                    // POR CANAL DE VENTA Y ARTICULOS
                    case 8:
                        tablePanelVariable.Visible = true;
                        lblVariable.Text = "Canal de Venta";
                        CargarComboVariable(Convert.ToInt32(formasCalculo.EditValue));
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = true;
                        radioGroupClientes.Enabled = true;
                        break;
                    // POR ARTICULO
                    case 9:
                        tablePanelVariable.Visible = false;
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = true;
                        radioGroupClientes.Enabled = true;
                        break;
                    // EN CONJUNTO
                    case 10:
                        tablePanelVariable.Visible = true;
                        lblVariable.Text = "Agente";
                        CargarComboVariable(Convert.ToInt32(formasCalculo.EditValue));
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        radioGroupClientes.Enabled = true;
                        break;
                    // POR CLIENTES NUEVOS
                    case 11:
                        tablePanelVariable.Visible = false;
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        radioGroupClientes.SelectedIndex = 1;
                        radioGroupClientes.Enabled = false;
                        break;
                }
            }
        }

        private void cboVariable_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cboFormasCalculo.EditValue) == 10)
            {
                MostrarOcultarPopUpConjunto(true);
            }
        }

        private void cboConceptoConjunto_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit cboConceptoConjunto = (LookUpEdit)sender;
            if (cboConceptoConjunto != null)
            {
                if (cboConceptoConjunto.EditValue != null)
                {
                    string strConcepto = cboConceptoConjunto.Text;
                    DialogResult dialog = MessageBox.Show("¿Desea agregar como compartido el concepto " + strConcepto + "?", "Confirmar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        popupConceptoConjunto.Hide();
                    }
                    else if (dialog == DialogResult.Cancel)
                    {
                        cboConceptoConjunto.EditValue = null;
                        popupConceptoConjunto.Hide();
                    }
                }
            }
        }

        private void cboFamilias_EditValueChanged(object sender, EventArgs e)
        {
            int formasCalculo = Convert.ToInt32(cboFormasCalculo.EditValue);
            if (formasCalculo == 4 || formasCalculo == 7)
            {
                LookUpEdit familias = sender as LookUpEdit;
                DataRowView row = (DataRowView)familias.Properties.GetDataSourceRowByKeyValue(familias.EditValue);

                if (row != null)
                    CargarCombosSubFamilias();
            }
        }

        #endregion CONCEPTOS DE COMISIONES NAVPAGE


        #region REPORTE NAVPAGE

        private void MostrarOcultarPopUpReporte(bool visible)
        {
            if (visible)
            {
                int locX = (this.ClientSize.Width - popupFechasReporte.Width) / 2;
                int locY = (this.ClientSize.Height - 200 - popupFechasReporte.Height) / 2;
                popupFechasReporte.Location = new Point(locX, locY);
                popupFechasReporte.Show();
            }
            else
            {
                popupFechasReporte.Hide();
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            try
            {
                ComisionesDesigner rep = new ComisionesDesigner();
                rep.Parameters["parameter1"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
            
                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                int impDirecto = result == "OK" ? cl.iImpresiondirecta : 0;
            
                if (impDirecto == 1)
                {
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                }
                else
                {
                    rep.CreateDocument();
                    documentViewer1.DocumentSource = rep;
                    navigationFrame.SelectedPage = NavigationPageReporte;
                }
                MostrarOcultarPopUpReporte(false);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error al crear reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }


        #endregion REPORTE NAVPAGE

        
    }
}