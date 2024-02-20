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
using DevExpress.XtraGrid.Views.Grid;
using VisualSoftErp.Clases;
using DevExpress.XtraGrid;
using VisualSoftErp.Operacion.Inventarios.Clases;
using System.Threading;
using System.Globalization;

namespace VisualSoftErp.Operacion.Inventarios.Formas
{
    public partial class Existencias : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Existencias()
        {

           

            InitializeComponent();
            //txtFechadecorte.Text = DateTime.Now.ToShortDateString();
            CargaCombos();
            gridViewDetalle.OptionsView.ShowViewCaption = true;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

            //gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            //gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            //gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;
            gridViewDetalle.OptionsView.ShowAutoFilterRow = true;
            gridViewDetalle.OptionsBehavior.ReadOnly = true;
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();
            cboLineas.Properties.ValueMember = "Clave";
            cboLineas.Properties.DisplayMember = "Des";
            cl.strTabla = "Lineas";
            src.DataSource = cl.CargaCombos();
            cboLineas.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboLineas.Properties.ForceInitialize();
            cboLineas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboLineas.Properties.ForceInitialize();
            cboLineas.Properties.PopulateColumns();
            cboLineas.Properties.Columns["Clave"].Visible = false;
            cboLineas.ItemIndex = 0;
            CargaFamilias();

            
        }

        private void CargaFamilias()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();
            cboFamilias.Properties.ValueMember = "Clave";
            cboFamilias.Properties.DisplayMember = "Des";
            cl.strTabla = "FamiliasLineas";
            cl.iCondicion = Convert.ToInt32(cboLineas.EditValue);
            src.DataSource = cl.CargaCombos();
            cboFamilias.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboFamilias.Properties.ForceInitialize();
            cboFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilias.Properties.ForceInitialize();
            cboFamilias.Properties.PopulateColumns();
            cboFamilias.Properties.Columns["Clave"].Visible = false;
            cboFamilias.ItemIndex = 0;
        }

        private void Ver()
        {
            String Result = Valida();
            if (Result != "OK")
            {
                MessageBox.Show(Result);
                return;
            }
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 1;
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup2.Visible = true;
        }

        private void LlenarGrid()
        {
            

            gridViewDetalle.ViewCaption = "Existencias al " + txtFechadecorte.Text.ToString();
            ExistenciasCL cl = new ExistenciasCL();
            cl.intLineasID = Convert.ToInt32(cboLineas.EditValue);
            cl.intFamiliasID = Convert.ToInt32(cboFamilias.EditValue);
            try
            {
                cl.Fechadecorte = Convert.ToDateTime(txtFechadecorte.Text);
            }
            catch(Exception ex)
            {
                cl.Fechadecorte = DateTime.Now;
            }

            cl.intDesactivados = swDesactivados.IsOn == true ? 1 : 0;
            gridControlDetalle.DataSource = cl.ExistenciasGrid();
            Encabezados();
            CalculaTotal();
            gridViewDetalle.FocusedRowHandle = 0;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridExistencias";
            clg.restoreLayout(gridViewDetalle);
        } //LlenarGrid()

        private string Valida()
        {

            if (cboLineas.EditValue == null)
            {
                return "El campo Lineas no puede ir vacio";
            }
            if (cboFamilias.EditValue == null)
            {
                return "El campo Familias no puede ir vacio";
            }
            if (txtFechadecorte.Text == null)
            {
                return "El campo fecha de corte no puede ir vacio";
            }

            globalCL clg = new globalCL();
            if (!clg.esFecha(txtFechadecorte.Text))
            {
                return "Seleccione la fecha de corte";
             
            }
            return "OK";
        }
        //private void LimpiaCajas()
        //{
        //    cboFamilias.EditValue = null;
        //    cboLineas.EditValue = null;
        //    txtFechadecorte.Text = DateTime.Now.ToShortDateString();
        //}


        #region gridPrincipal
        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //    string strStatus = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Estado"));
            //    intStatus = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status"));
            //    intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            //    strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            //    intTiposdemovimientoinvID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "TiposdemovimientoinvID"));

        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                string status = view.GetRowCellValue(e.RowHandle, "Estado").ToString();

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
            //if (intFolio == 0)
            //{
            //    MessageBox.Show("Selecciona un renglón");
            //}
            //else
            //{
            //    Editar();
            //}
        }
        #endregion

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                //Sacamos datos del artículo
                if (e.Column.Name == "gridColumnArticulo")
                {
                    articulosCL cl = new articulosCL();
                    string art = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulo").ToString();
                    if (art.Length > 0) //validamos que haya algo en la celda
                    {
                        cl.intArticulosID = Convert.ToInt32(art);
                        string result = cl.articulosLlenaCajas();
                        if (result == "OK")
                        {
                            gridViewDetalle.SetFocusedRowCellValue("Descripcion", cl.strNombre);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("GridviewDetalle Changed: " + ex);
            }
        }

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

        private void bbiVer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Generando informe...");
            cboLineas.Enabled = false;
            cboFamilias.Enabled = false;
            Ver();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridExistencia";
            string result = clg.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {        
            navigationFrame.SelectedPageIndex = 0;
            ribbonPageGroup1.Visible = true;
            ribbonPageGroup2.Visible = false;
            cboLineas.Enabled = true;
            cboFamilias.Enabled = true;
        }

        private void bbiTodo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridViewDetalle.ActiveFilter.Clear();
        }

        private void Encabezados()
        {
            almacenesCL cl = new almacenesCL();
            int alm;
            string result = string.Empty;

            for (int i = 8; i <= gridViewDetalle.VisibleColumns.Count - 1; i++)
            {
                alm = Convert.ToInt32(gridViewDetalle.Columns[i].FieldName);
                cl.intAlmacenesID = alm;
                result = cl.almacenesLlenaCajas();
                if (result == "OK")
                {
                    gridViewDetalle.Columns[i].Caption = cl.strNombre;
                }
            }



        }

        private void CalculaTotal()
        {
            decimal total = 0;
            for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++)   //renglones
            {
                total = 0;
                for (int j = 8; j <= gridViewDetalle.Columns.Count - 1; j++)  //Columnas
                {

                    if (!DBNull.Value.Equals(gridViewDetalle.GetRowCellValue(i, gridViewDetalle.Columns[j])))
                    {

                        total += Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, gridViewDetalle.Columns[j]));

                    }

                }
                gridViewDetalle.FocusedRowHandle = i;
                gridViewDetalle.SetFocusedRowCellValue("Total", total);
                total = 0;
            }
        }

        private void bbiExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlDetalle.ShowRibbonPrintPreview();
        }

        private void bbiSoloconexistencia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridViewDetalle.ActiveFilterString = "[Total] <> 0";
        }

        private void bbiNegativos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridViewDetalle.ActiveFilterString = "[Total] < 0";
        }

        private void cboLineas_EditValueChanged(object sender, EventArgs e)
        {
            CargaFamilias();
        }

        private void employeesNavigationPage_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}