using DevExpress.CodeParser;
using DevExpress.Pdf.Native.BouncyCastle.Ocsp;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.CxP.Informes;
using VisualSoftErp.Operacion.Inventario.Designers;

namespace VisualSoftErp.Operacion.Inventario.Formas
{
    public partial class EtiquetasSKU : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private bool allSelected = false;
        private DataTable detalle;
        private DataTable ArticulosSeleccionados = new DataTable
        {
            Columns = {
            { "ArticulosID", typeof(int) },
            { "Código Artículo", typeof(string) },
            { "Nombre Artículo", typeof(string) },
            { "Nombre Línea", typeof(string) },
            { "Nombre Familia", typeof(string) },
            { "Nombre SubFamilia", typeof(string) }
    }
        };
        public EtiquetasSKU()
        {
            InitializeComponent();
            cargarCombos();
            InitGridView();
            llenarGrid();
            txtCantidad.Text = "1";
        }

        private void cargarCombos()
        {
            try
            {
                globalCL global = new globalCL();
                combosCL combos = new combosCL();
                BindingSource src = new BindingSource();

                #region COMBO LINEAS
                cboLineas.EditValueChanged -= cboLineas_EditValueChanged;
                combos.strTabla = "Lineas";
                cboLineas.Properties.ValueMember = "Clave";
                cboLineas.Properties.DisplayMember = "Des";
                src.DataSource = combos.CargaCombos();
                cboLineas.Properties.DataSource = global.AgregarOpcionTodos(src);
                cboLineas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboLineas.Properties.ForceInitialize();
                cboLineas.Properties.PopulateColumns();
                cboLineas.Properties.Columns["Clave"].Visible = false;
                cboLineas.Properties.NullText = "Seleccione una línea de artículos";
                cboLineas.EditValue = 0;
                cboLineas.EditValueChanged += cboLineas_EditValueChanged;
                #endregion COMBO LINEAS

                #region COMBO FAMILIAS
                cboFamilias.EditValueChanged -= cboFamilias_EditValueChanged;
                combos.strTabla = "FamiliasLineas";
                combos.iCondicion = Convert.ToInt32(cboLineas.EditValue);
                src.DataSource = combos.CargaCombos();
                cboFamilias.Properties.DataSource = global.AgregarOpcionTodos(src);
                cboFamilias.Properties.ValueMember = "Clave";
                cboFamilias.Properties.DisplayMember = "Des";
                cboFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboFamilias.Properties.ForceInitialize();
                cboFamilias.Properties.PopulateColumns();
                cboFamilias.Properties.Columns["Clave"].Visible = false;
                cboFamilias.Properties.NullText = "Seleccione una familia de artículos";
                cboFamilias.Enabled = false;
                cboFamilias.EditValue = 0;
                cboFamilias.EditValueChanged += cboFamilias_EditValueChanged;
                #endregion COMBO FAMILIAS

                #region COMBO SUBFAMILIAS
                cboSubFamilias.EditValueChanged -= cboSubFamilias_EditValueChanged;
                combos.strTabla = "SubfamiliasXfamilias";
                combos.iCondicion = Convert.ToInt32(cboFamilias.EditValue);
                src.DataSource = combos.CargaCombos();
                cboSubFamilias.Properties.DataSource = global.AgregarOpcionTodos(src);
                cboSubFamilias.Properties.ValueMember = "Clave";
                cboSubFamilias.Properties.DisplayMember = "Des";
                cboSubFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboSubFamilias.Properties.ForceInitialize();
                cboSubFamilias.Properties.PopulateColumns();
                cboSubFamilias.Properties.Columns["Clave"].Visible = false;
                cboSubFamilias.Properties.NullText = "Seleccione una subfamilia de artículos";
                cboSubFamilias.EditValue = 0;
                cboSubFamilias.Enabled = false;
                cboSubFamilias.EditValueChanged += cboSubFamilias_EditValueChanged;
                #endregion COMBO SUBFAMILIAS

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargarCombos() en línea " + ex.LineNumber().ToString() + ": \n" + ex.Message);
            }
        }

        private void InitGridView()
        {
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Artículos";
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.ActiveFilter.Clear();
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridView1.OptionsSelection.MultiSelect = true;
        }
        
        private void llenarGrid()
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            articulosCL cl = new articulosCL();
            cl.intLinea = Convert.ToInt32(cboLineas.EditValue);
            cl.intFamiliasID = Convert.ToInt32(cboFamilias.EditValue);
            cl.intSubFamiliaID = Convert.ToInt32(cboSubFamilias.EditValue);
            detalle = cl.ArticulosEtiquetasSKU();
            gridControl1.DataSource = detalle;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.Columns["ArticulosID"].Visible = false;
            recordarSeleccionados();
            allSelected = false;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void recordarSeleccionados()
        {
            if (ArticulosSeleccionados.Rows.Count <= 0)
                return;
            for (int rowSeleccionados = 0; rowSeleccionados < ArticulosSeleccionados.Rows.Count; rowSeleccionados++)
            {
                bool articuloEncontrado = false;
                DataRow rowArticulosSeleccionados = ArticulosSeleccionados.Rows[rowSeleccionados];
                for(int rowGrid = 0; rowGrid < gridView1.RowCount; rowGrid++)
                {
                    DataRow row = gridView1.GetDataRow(rowGrid);
                    int articuloSeleccionadoID = Convert.ToInt32(ArticulosSeleccionados.Rows[rowSeleccionados].ItemArray[0]);
                    int articuloGridID = Convert.ToInt32(row["ArticulosID"]);

                    if(articuloSeleccionadoID == articuloGridID)
                    {
                        gridView1.SelectionChanged -= gridView1_SelectionChanged;
                        gridView1.SelectRow(rowGrid);
                        gridView1.SelectionChanged += gridView1_SelectionChanged;
                        articuloEncontrado = true;
                        break;
                    }
                }

                if (!articuloEncontrado)
                {
                    gridView1.SelectionChanged -= gridView1_SelectionChanged;
                    DataRow rowTop = detalle.NewRow();
                    rowTop["ArticulosID"] = rowArticulosSeleccionados.ItemArray[0];
                    rowTop["Código Artículo"] = rowArticulosSeleccionados.ItemArray[1];
                    rowTop["Nombre Artículo"] = rowArticulosSeleccionados.ItemArray[2];
                    rowTop["Nombre Línea"] = rowArticulosSeleccionados.ItemArray[3];
                    rowTop["Nombre Familia"] = rowArticulosSeleccionados.ItemArray[4];
                    rowTop["Nombre SubFamilia"] = rowArticulosSeleccionados.ItemArray[5];
                    detalle.Rows.InsertAt(rowTop, 0);
                    gridControl1.DataSource = detalle;
                    int articulosID = Convert.ToInt32(rowArticulosSeleccionados.ItemArray[0]);
                    int newRow = gridView1.LocateByValue("ArticulosID", articulosID);
                    gridView1.SelectRow(newRow);
                    gridView1.SelectionChanged += gridView1_SelectionChanged;

                }
            }
        }
        
        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void cboLineas_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;
            DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

            if (row != null)
            {
                if (Convert.ToInt32(row["Clave"]) != 0)
                {
                    globalCL global = new globalCL();
                    combosCL cl = new combosCL();
                    BindingSource src = new BindingSource();

                    cl.strTabla = "FamiliasLineas";
                    cl.iCondicion = Convert.ToInt32(row["Clave"]);
                    src.DataSource = cl.CargaCombos();
                    cboFamilias.Properties.DataSource = global.AgregarOpcionTodos(src);
                    cboFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                    cboFamilias.Properties.ForceInitialize();
                    cboFamilias.Properties.PopulateColumns();
                    cboFamilias.Properties.Columns["Clave"].Visible = false;
                    cboFamilias.Properties.NullText = "Seleccione una familia de artículos";
                    cboFamilias.EditValue = 0;
                    cboFamilias.Enabled = true;

                    cl.strTabla = "SubfamiliasXfamilias";
                    cl.iCondicion = Convert.ToInt32(cboFamilias.EditValue);
                    src.DataSource = cl.CargaCombos();
                    cboSubFamilias.Properties.DataSource = global.AgregarOpcionTodos(src);
                    cboSubFamilias.EditValue = 0;
                    cboSubFamilias.Enabled = false;
                }
                else
                {
                    cboFamilias.EditValue = 0;
                    cboFamilias.Enabled = false;
                    cboSubFamilias.EditValue = 0;
                    cboSubFamilias.Enabled = false;
                }
                llenarGrid();
            }
        }

        private void cboFamilias_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;
            DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

            if (row != null)
            {
                if (Convert.ToInt32(row["Clave"]) != 0)
                {
                    globalCL global = new globalCL();
                    combosCL cl = new combosCL();
                    BindingSource src = new BindingSource();

                    cl.strTabla = "SubfamiliasXfamilias";
                    cl.iCondicion = Convert.ToInt32(cboFamilias.EditValue);
                    src.DataSource = cl.CargaCombos();
                    cboSubFamilias.Properties.DataSource = global.AgregarOpcionTodos(src);
                    cboSubFamilias.Enabled = true;
                }
                else
                {
                    cboSubFamilias.EditValue = 0;
                    cboSubFamilias.Enabled = false;
                }
                llenarGrid();
            }
        }

        private void cboSubFamilias_EditValueChanged(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private string Valida()
        {
            if (cboLineas.EditValue == null)
                return "Seleccione una línea de Artículos";
            if (cboFamilias.EditValue == null)
                return "Seleccione una familia de Artículos";
            if (cboSubFamilias.EditValue == null)
                return "Seleccione una SubFamilia de Artículos";
            if (Convert.ToInt32(txtCantidad.Text) < 0 || txtCantidad.Text == string.Empty)
                return "Cantidad de Etiquetas no válido";
            if (ArticulosSeleccionados.Rows.Count <= 0)
                return "Seleccione almenos un artículo";

            return "OK";
        }

        private void bbiVistaPrevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            string result = Valida();
            if (result != "OK")
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                MessageBox.Show(result);
                return;
            }

            reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void reporte()
        {
            globalCL cl = new globalCL();
            string result = cl.Datosdecontrol();
            if (result != "OK")
            {
                MessageBox.Show(result, "Error al obtener datos de control", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if(cl.iImpresiondirecta == 1)
            {
                EtiquetasSKUDesigner rep = new EtiquetasSKUDesigner(ArticulosSeleccionados, Convert.ToInt32(txtCantidad.Text));
                ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                rpt.Print();
                return;
            }
            else
            {
                EtiquetasSKUDesigner rep = new EtiquetasSKUDesigner(ArticulosSeleccionados, Convert.ToInt32(txtCantidad.Text));
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                navigationFrame.SelectedPageIndex = 1;
            }
            
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            GridView view = sender as GridView;
            DataRowView rowView = view.GetRow(e.ControllerRow) as DataRowView;
            if(rowView != null)
            {
                DataRow row = rowView.Row;
                if(e.Action == CollectionChangeAction.Add)
                {
                    DataRow newRow = ArticulosSeleccionados.NewRow();
                    newRow.ItemArray = row.ItemArray;
                    ArticulosSeleccionados.Rows.Add(newRow);
                }
                else if (e.Action == CollectionChangeAction.Remove)
                {
                    for(int i = 0; i < ArticulosSeleccionados.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(ArticulosSeleccionados.Rows[i].ItemArray[0]) == Convert.ToInt32(row.ItemArray[0]))
                        {
                            ArticulosSeleccionados.Rows.RemoveAt(i);
                            break;
                        }
                    }
                }
                else 
                {
                    MessageBox.Show(e.Action.ToString());
                }
            }
            else if(rowView == null && e.Action == CollectionChangeAction.Refresh)
            {
                if(gridView1.ActiveEditor != null)
                {
                    if (gridView1.ActiveEditor.EditValue.ToString() != string.Empty)
                        return;
                }

                if(!allSelected)
                {
                    ArticulosSeleccionados.Clear();
                    for (int i = 0; i < detalle.Rows.Count; i++)
                    {
                        DataRow newRow = ArticulosSeleccionados.NewRow();
                        newRow.ItemArray = detalle.Rows[i].ItemArray;
                        ArticulosSeleccionados.Rows.Add(newRow);
                    }
                    allSelected = true;
                }
                else
                {
                    ArticulosSeleccionados.Clear();
                    allSelected = false;
                }
            }
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void gridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            if(gridView.ActiveEditor.EditValue.ToString() == string.Empty)
                recordarSeleccionados();
        }
    }
}