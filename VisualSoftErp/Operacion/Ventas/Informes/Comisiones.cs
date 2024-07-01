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

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class Comisiones : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private int intAgentesID;
        private int intConceptosComisionesID;
        private bool isEditing;
        private bool permisosEscritura;
        private DataTable Conceptos { get; set; } = new DataTable
        {
            Columns =
            {
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Porcentaje", typeof(int))
            }
        };

        public Comisiones()
        {
            InitializeComponent();
        }

        private void Comisiones_Load(object sender, EventArgs e)
        {
            soloLectura();
            CargarCombos();
            ribbonPageHome.Visible = true;
            if (ribbonControl.MergeOwner != null)
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            InitGridControlAgentes();
            InitGridControlConceptos();
            InitGridControlConceptosForm();
            CargarCombos();
            MostrarOcultarFormConceptos(false);
            navigationFrame.SelectedPage = NavigationPageAgentes;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void soloLectura()
        {
            globalCL clg = new globalCL();
            clg.strPrograma = "0537";
            if (clg.accesoSoloLectura())
            {
                bbiGuardarConceptosAgente.Enabled = false;

                bbiGuardarConcepto.Enabled = false;
                bbiEditarConcepto.Enabled = false;
                bbiEliminarConcepto.Enabled = false;
                permisosEscritura = false;
            }
        }
        private void CargarCombos()
        {
            combosCL combos = new combosCL();
            BindingSource src = new BindingSource();
            globalCL global = new globalCL();

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
            #endregion

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
            cboFamilias.Properties.NullText = "Seleccione una Familia de Artículos";
            cboFamilias.EditValue = null;
            #endregion COMBO FAMILIAS

            #region COMBO SUBFAMILIAS
            combos.strTabla = "SubFamilias";
            cboSubFamilias.Properties.ValueMember = "Clave";
            cboSubFamilias.Properties.DisplayMember = "Des";
            cboSubFamilias.Properties.DataSource = combos.CargaCombos();
            cboSubFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSubFamilias.Properties.ForceInitialize();
            cboSubFamilias.Properties.PopulateColumns();
            cboSubFamilias.Properties.Columns["Clave"].Visible = false;
            cboSubFamilias.Properties.NullText = "Seleccione una SubFamilias de Artículos";
            cboSubFamilias.EditValue = null;
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

        private void navBarControlMenu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NavBarItemLink item = e.Link;
            if (item != null)
            {
                switch (item.ItemName)
                {
                    case "navBarItemAgentes":
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
                        navigationFrame.SelectedPage = NavigationPageReporte;
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
                        navigationFrame.SelectedPage = NavigationPageReporte;
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
            gridControlConceptos.DataSource = Conceptos;

            gridViewConceptos.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            gridViewConceptos.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            gridViewConceptos.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewConceptos.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewConceptos.OptionsNavigation.AutoMoveRowFocus = true;
            gridViewConceptos.Columns["Porcentaje"].Width = 150;
        }

        private void gridViewAgentes_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            Conceptos.Clear();
            intAgentesID = Convert.ToInt32(gridViewAgentes.GetRowCellValue(e.RowHandle, "AgentesID"));

            // SE LLENA GRID DE CONCEPTOS CON LOS CONCEPTOS DEL AGENTE Y SUS RESPECTIVOS PORCENTAJES
            // InitGridControlComponentes();  o aquí mero, como sea. Después vemos.
        }

        private void bbiGuardarConceptosAgente_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Console.WriteLine(intAgentesID);
            Console.WriteLine(Conceptos);
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
            swClientesNuevos.IsOn = false;
            cboVariable.EditValue = null;
            cboLineas.EditValue = null;
            cboFamilias.EditValue = null;
            cboSubFamilias.EditValue = null;
            cboArticulos.EditValue = null;
        }

        private void CargarComboVariable(int FormaCalculo)
        {
            combosCL combos = new combosCL();
            BindingSource src = new BindingSource();
            globalCL global = new globalCL();

            if (FormaCalculo == 1 | FormaCalculo == 5)
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
            else if (FormaCalculo == 7)
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

        private void InitGridControlConceptosForm()
        {
            ComisionesCL cl = new ComisionesCL();
            gridControlConceptosForm.DataSource = cl.ConceptosComisionesGRID();
            gridViewConceptosForm.OptionsBehavior.ReadOnly = true;
            gridViewConceptosForm.OptionsBehavior.Editable = false;
        }

        private void bbiNuevoConcepto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            isEditing = true;
            intConceptosComisionesID = 0;
            MostrarOcultarFormConceptos(true);
        }

        private void bbiEditarConcepto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            isEditing = true;
            MostrarOcultarFormConceptos(true);
            ConceptosComisionesLlenarCajas();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            isEditing = false;
            LimpiarCajas(false);
            MostrarOcultarFormConceptos(false);
        }

        private void MostrarOcultarFormConceptos(bool isVisible)
        {
            int sizeUp;
            int sizeDown;
            if(isVisible)
            {
                sizeUp = 25;
                sizeDown = 75;
                bbiNuevoConcepto.Enabled = false; 
                bbiRegresar.Enabled = true;
                bbiGuardarConcepto.Enabled = true;
                bbiEliminarConcepto.Enabled = false; 
                bbiEditarConcepto.Enabled = false;
            }
            else
            {
                sizeUp = 0;
                sizeDown = 100;
                bbiNuevoConcepto.Enabled = true;
                bbiRegresar.Enabled = false;
                bbiGuardarConcepto.Enabled = false;
                bbiEliminarConcepto.Enabled = true;
                bbiEditarConcepto.Enabled = true;
            }
            tablePanel2.Rows[0].Height = sizeUp;
            tablePanel2.Rows[0].Style = DevExpress.Utils.Layout.TablePanelEntityStyle.Relative;
            tablePanel2.Rows[1].Height = sizeDown;
            tablePanel2.Rows[1].Style = DevExpress.Utils.Layout.TablePanelEntityStyle.Relative;
        }

        private void cboFormasCalculo_EditValueChanged(object sender, EventArgs e)
        {
            LimpiarCajas(true);
            LookUpEdit formasCalculo = (LookUpEdit)sender;
            if(formasCalculo != null)
            {
                switch(formasCalculo.EditValue)
                {
                    case 1:
                        tablePanelVariable.Visible = true;
                        lblVariable.Text = "Canal de Venta";
                        CargarComboVariable(Convert.ToInt32(formasCalculo.EditValue));
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        swClientesNuevos.Enabled = true; 
                        break;
                    case 2:
                        tablePanelVariable.Visible = false;
                        cboLineas.Enabled = true;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        swClientesNuevos.Enabled = true; 
                        break;
                    case 3:
                        tablePanelVariable.Visible = false;
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = true;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        swClientesNuevos.Enabled = true; 
                        break;
                    case 4:
                        tablePanelVariable.Visible = false;
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = true;
                        cboArticulos.Enabled = false;
                        swClientesNuevos.Enabled = true; 
                        break;
                    case 5:
                        tablePanelVariable.Visible = true;
                        lblVariable.Text = "Canal de Venta"; 
                        CargarComboVariable(Convert.ToInt32(formasCalculo.EditValue));
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = true;
                        swClientesNuevos.Enabled = true; 
                        break;
                    case 6:
                        tablePanelVariable.Visible = false;
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = true;
                        swClientesNuevos.Enabled = true; 
                        break;
                    case 7:
                        tablePanelVariable.Visible = true;
                        lblVariable.Text = "Agente";
                        CargarComboVariable(Convert.ToInt32(formasCalculo.EditValue));
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        swClientesNuevos.Enabled = true;
                        break;
                    case 8:
                        tablePanelVariable.Visible = false;
                        cboLineas.Enabled = false;
                        cboFamilias.Enabled = false;
                        cboSubFamilias.Enabled = false;
                        cboArticulos.Enabled = false;
                        swClientesNuevos.EditValue = true;
                        swClientesNuevos.Enabled = false;
                        break;
                }
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
                if(cboVariable.EditValue == null)
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
        
        private void bbiGuardarConcepto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string result = ValidarConceptosComisiones();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }
            ComisionesCL cl = new ComisionesCL();
            cl.intConceptosComisionesID = intConceptosComisionesID;
            cl.strNombre = txtNombre.Text;
            cl.intFormasCalculoComisionesID = Convert.ToInt32(cboFormasCalculo.EditValue);
            cl.intClientesNuevos = swClientesNuevos.IsOn == true ? 1 : 0;
            cl.intCanalVentasID = (Convert.ToInt32(cboFormasCalculo.EditValue) == 1 || Convert.ToInt32(cboFormasCalculo.EditValue) == 5) ? Convert.ToInt32(cboVariable.EditValue) : 0;
            cl.intLineasID = Convert.ToInt32(cboLineas.EditValue);
            cl.intFamiliasID = Convert.ToInt32(cboFamilias.EditValue);
            cl.intSubFamiliasID = Convert.ToInt32(cboSubFamilias.EditValue);
            cl.intArticulosID = Convert.ToInt32(cboArticulos.EditValue);
            cl.intAgentesID = Convert.ToInt32(cboFormasCalculo.EditValue) == 7 ? Convert.ToInt32(cboVariable.EditValue) : 0;

            result = cl.ConceptosComisionesCRUD();
            if (result != "OK")
                MessageBox.Show(result);
            else
            {
                InitGridControlConceptosForm();
                MessageBox.Show("Guardado Correctamente");
            }
        }

        private void gridControlConceptosForm_Click(object sender, EventArgs e)
        {
            if (!permisosEscritura) return;

            GridControl grid = (GridControl)sender;
            if (grid != null)
            {
                GridView gridView = grid.FocusedView as GridView;
                if (gridView != null)
                {
                    object conceptoComisionesID = gridView.GetFocusedRowCellValue("ConceptosComisionesID");

                    if (conceptoComisionesID != null)
                    {
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

        private void ConceptosComisionesLlenarCajas()
        {
            if(intConceptosComisionesID == 0)
            {
                MessageBox.Show("Seleccione un Concepto");
                return;
            }
            ComisionesCL cl = new ComisionesCL();
            cl.intConceptosComisionesID = intConceptosComisionesID;
            string result = cl.ConceptosComisionesLlenarCajas();
            if(result != "OK")
            {
                MessageBox.Show(result);
                return;
            }
            else
            {
                txtNombre.Text = cl.strNombre;
                cboFormasCalculo.EditValue = cl.intFormasCalculoComisionesID;
                swClientesNuevos.IsOn = cl.intClientesNuevos == 1 ? true : false;
                if (cl.intFormasCalculoComisionesID == 1 || cl.intFormasCalculoComisionesID == 5)
                    cboVariable.EditValue = cl.intCanalVentasID;
                if (cl.intFormasCalculoComisionesID == 7)
                    cboVariable.EditValue = cl.intAgentesID;
                cboLineas.EditValue = cl.intLineasID;
                cboFamilias.EditValue = cl.intFamiliasID;
                cboSubFamilias.EditValue = cl.intSubFamiliasID;
                cboArticulos.EditValue = cl.intArticulosID;
                MostrarOcultarFormConceptos(true);
            }

        }

        private void bbiEliminarConcepto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ComisionesCL cl = new ComisionesCL();
            cl.intConceptosComisionesID = intConceptosComisionesID;
            string result = cl.ConceptosComisionesEliminar();
            if(result == "OK")
            {
                MessageBox.Show(result);
                return;
            }
            else
            {
                InitGridControlConceptosForm();
                LimpiarCajas(false);
                MessageBox.Show("Eliminado Correctamente");
            }
        }

        #endregion CONCEPTOS DE COMISIONES NAVPAGE


    }
}