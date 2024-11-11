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
using VisualSoftErp.Operacion.Tienda.Clases;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace VisualSoftErp.Operacion.Tienda.Formas
{
    public partial class AppSubfamiliasDescripciones : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        Boolean blNuevo = true;
        int intFamiliasID, intSubFamiliasID;
        int intUsuarioID = globalCL.gv_UsuarioID;
        public BindingList<detalleCL> detalle;

        public AppSubfamiliasDescripciones()
        {
            InitializeComponent();

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = false;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            //------------- Inicializar aquí opciones de columnas del grid ----------------
     

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de descripciones por subfamilia";

            LlenarGrid();
            CargaCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            AppSubfamiliasDescripcionesCL cl = new AppSubfamiliasDescripcionesCL();
            gridControlPrincipal.DataSource = cl.AppSubfamiliasDescripcionesGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppSubfamiliasDescripciones";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Familias";
            cboFamiliasID.Properties.ValueMember = "Clave";
            cboFamiliasID.Properties.DisplayMember = "Des";
            cboFamiliasID.Properties.DataSource = cl.CargaCombos();
            cboFamiliasID.Properties.ForceInitialize();
            cboFamiliasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamiliasID.Properties.PopulateColumns();
            cboFamiliasID.Properties.Columns["Clave"].Visible = false;
            cboFamiliasID.ItemIndex = 0;
            CargaSubFamilias();

        }

        private void CargaSubFamilias()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "SubfamiliasXfamilias";
            cl.iCondicion = Convert.ToInt32(cboFamiliasID.EditValue);
            cboSubFamiliasID.Properties.ValueMember = "Clave";
            cboSubFamiliasID.Properties.DisplayMember = "Des";
            cboSubFamiliasID.Properties.DataSource = cl.CargaCombos();
            cboSubFamiliasID.Properties.ForceInitialize();
            cboSubFamiliasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSubFamiliasID.Properties.PopulateColumns();
            cboSubFamiliasID.Properties.Columns["Clave"].Visible = false;
            cboSubFamiliasID.ItemIndex = 0;
        }

        public class detalleCL
        {
            public int Seq { get; set; }
            public string Descripcion { get; set; }
        }

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
        }

        private void Nuevo()
        {
            ribbonPageGroup1.Visible = false;
            LimpiaCajas();
            blNuevo = true;
            Inicialisalista();
            BotonesEdicion();

        }

        private void BotonesEdicion()
        {
            ribbonPageGroup2.Visible = true;
            ribbonPageGroup1.Visible = false;
            ribbonStatusBar.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
        }
        private void LimpiaCajas()
        {
            cboFamiliasID.ItemIndex = 0;
            
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

                System.Data.DataTable dtAppSubfamiliasDescripciones = new System.Data.DataTable("AppSubfamiliasDescripciones");
                dtAppSubfamiliasDescripciones.Columns.Add("FamiliasID", Type.GetType("System.Int32"));
                dtAppSubfamiliasDescripciones.Columns.Add("SubfamiliasID", Type.GetType("System.Int32"));
                dtAppSubfamiliasDescripciones.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtAppSubfamiliasDescripciones.Columns.Add("Descripcion", Type.GetType("System.String"));

                string dato = String.Empty;
                intFamiliasID = Convert.ToInt32(cboFamiliasID.EditValue);
                intSubFamiliasID = Convert.ToInt32(cboSubFamiliasID.EditValue);
                int intSeq = 0;
                string strDescripcion = String.Empty;
                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "Descripcion").ToString();
                    if (dato.Length > 0)
                    {
                        intSeq = i + 1;

                        if (gridViewDetalle.GetRowCellValue(i, "Descripcion") != null)
                        {
                            strDescripcion = gridViewDetalle.GetRowCellValue(i, "Descripcion").ToString();
                        }
                        else
                        {
                            MessageBox.Show("El campo Descripción renglón:" + intSeq + " no puede ir vacio");
                        }

                        dtAppSubfamiliasDescripciones.Rows.Add(intFamiliasID, intSubFamiliasID, intSeq, strDescripcion);


                    }
                }

                AppSubfamiliasDescripcionesCL cl = new AppSubfamiliasDescripcionesCL();
                cl.intFamiliasID = intFamiliasID;
                cl.intSubfamiliasID = intSubFamiliasID;
                cl.dtm = dtAppSubfamiliasDescripciones;
                cl.intUsuarioID = intUsuarioID;
                cl.strMaquina = Environment.MachineName;
                Result = cl.AppSubfamiliasDescripcionesCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
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
            if (cboFamiliasID.EditValue == null)
            {
                return "El campo Familia no puede ir vacio";
            }
            if (cboSubFamiliasID.EditValue == null)
            {
                return "El campo SubFamilias no puede ir vacio";
            }

            return "OK";
        } //Valida

        private void Editar()
        {
            blNuevo = false;
            LlenaCajas();
            DetalleLlenaCajas();

            BotonesEdicion();
            cboFamiliasID.ReadOnly = true;
            cboSubFamiliasID.ReadOnly = true;
            blNuevo = false;
        }//ver

        private void LlenaCajas()
        {
            cboFamiliasID.EditValue = intFamiliasID;
            cboSubFamiliasID.EditValue = intSubFamiliasID;
        }
        private void DetalleLlenaCajas()
        {
            Inicialisalista();
            if (blNuevo)
                return;
            try
            {
                AppSubfamiliasDescripcionesCL cl = new AppSubfamiliasDescripcionesCL();
                cl.intFamiliasID = intFamiliasID;
                cl.intSubfamiliasID = intSubFamiliasID;
                gridControlDetalle.DataSource = cl.AppSubfamiliasDescripcionesLlenaCajasDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }
        }



        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LimpiaCajas();
            Inicialisalista();
            ribbonPageGroup2.Visible = false;
            ribbonPageGroup1.Visible = true;
            ribbonStatusBar.Visible = true;
            LlenarGrid();
            cboFamiliasID.ReadOnly = false;
            cboSubFamiliasID.ReadOnly = false;
            navigationFrame.SelectedPageIndex = 0;

        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppSubfamiliasDescripciones" +
                "";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            globalCL clgd = new globalCL();
            clgd.strGridLayout = "gridAppSubfamiliasDescripcionesDetalle" +
                "";
            result = clgd.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intFamiliasID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "FamiliasID"));
            intSubFamiliasID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "SubfamiliasID"));
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFamiliasID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void cboFamiliasID_EditValueChanged(object sender, EventArgs e)
        {
            if (cboFamiliasID.EditValue != null)
            {
                CargaSubFamilias();
            }
        }

        private void cboSubFamiliasID_EditValueChanged(object sender, EventArgs e)
        {
            if (cboFamiliasID.EditValue != null)
            {
                if (cboSubFamiliasID.EditValue != null)
                {
                    intFamiliasID = Convert.ToInt32(cboFamiliasID.EditValue);
                    intSubFamiliasID = Convert.ToInt32(cboSubFamiliasID.EditValue);
                    DetalleLlenaCajas();
                }
            }
        }

        private void bbiEliminar2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFamiliasID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar todas las descripciones de la familia: " + intFamiliasID.ToString() + " subFamilia: " + intSubFamiliasID.ToString(), "Eliminar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void Eliminar()
        {
            try
            {
                AppSubfamiliasDescripcionesCL cl = new AppSubfamiliasDescripcionesCL();
                cl.intFamiliasID = intFamiliasID;
                cl.intSubfamiliasID = intSubFamiliasID;
                cl.intSeq = 0;
                string result = cl.AppSubfamiliasDescripcionesEliminar();
                if (result == "OK")
                {
                    MessageBox.Show("Eliminado correctamente");
                    LlenarGrid();
                }                                   
                else
                    MessageBox.Show(result);

            }
            catch(Exception ex)
            {
                MessageBox.Show("Eliminar:" + ex.Message);
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

        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }


    }
}