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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace VisualSoftErp.Catalogos.Inv
{
    public partial class SubFamilias : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intSubFamiliasID, intFamiliasID;
        bool blNuevo;
        public BindingList<descripcionesCL> detalle;

        public SubFamilias()
        {
            InitializeComponent();

            txtNombre.Properties.MaxLength = 50;
            txtNombre.EnterMoveNextControl = true;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de SubFamilias";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            gridViewDes.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDes.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDes.OptionsNavigation.AutoMoveRowFocus = true;

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            SubFamiliasCL cl = new SubFamiliasCL();
            gridControlPrincipal.DataSource = cl.SubFamiliasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridSubFamilias";
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
            cboFamiliasID.Properties.NullText = "Seleccione una Familia";
        }

        private void Nuevo()
        {
            BotonesEdicion();
            InicializaLista();
            blNuevo = true;
        }


        private void LimpiaCajas()
        {
            cboFamiliasID.EditValue = null;
            txtNombre.Text = string.Empty;
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
                if (blNuevo)
                {
                    Result = BuscarSubFamiliasID();
                    if (Result != "OK")
                    {
                        MessageBox.Show(Result);
                        return;
                    }
                }

                //Descripciones
                System.Data.DataTable dtAppSubfamiliasDescripciones = new System.Data.DataTable("AppSubfamiliasDescripciones");
                dtAppSubfamiliasDescripciones.Columns.Add("FamiliasID", Type.GetType("System.Int32"));
                dtAppSubfamiliasDescripciones.Columns.Add("SubfamiliasID", Type.GetType("System.Int32"));
                dtAppSubfamiliasDescripciones.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtAppSubfamiliasDescripciones.Columns.Add("Descripcion", Type.GetType("System.String"));

                string sDes = string.Empty;
                for (int i=0; i <= gridViewDes.RowCount - 1; i++)
                {
                    if (gridViewDes.GetRowCellValue(i, "Des") != null)
                    {
                        sDes = gridViewDes.GetRowCellValue(i, "Des").ToString();
                        if (sDes.Length > 0)
                            dtAppSubfamiliasDescripciones.Rows.Add(Convert.ToInt32(cboFamiliasID.EditValue), intSubFamiliasID, i, sDes);
                    }
                }

                SubFamiliasCL cl = new SubFamiliasCL();
                cl.intFamiliasID = Convert.ToInt32(cboFamiliasID.EditValue);
                cl.intSubFamiliasID = intSubFamiliasID;
                cl.strNombre = txtNombre.Text;
                cl.intTienda = swIncluirentienda.IsOn ? 1 : 0;
                cl.dtDes = dtAppSubfamiliasDescripciones;
                Result = cl.SubFamiliasCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");

                    if (blNuevo)
                    {
                        LimpiaCajas();
                        intFamiliasID = 0;
                        intSubFamiliasID = 0;
                    }
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
                return "El campo FamiliasID no puede ir vacio";
            }
            if (txtNombre.Text.Length == 0)
            {
                return "El campo Nombre no puede ir vacio";
            }
            return "OK";
        } //Valida

        private string BuscarSubFamiliasID()
        {
            if (cboFamiliasID.EditValue == null)
            {
                return "El campo FamiliasID no puede ir vacio";
            }
            else
            {
                SubFamiliasCL cl = new SubFamiliasCL();
                cl.intFamiliasID = Convert.ToInt32(cboFamiliasID.EditValue);
                String Result = cl.BuscarSubFamiliasID();
                if (Result == "OK")
                {
                    intSubFamiliasID = cl.intSubFamiliasID;

                }
                else
                {
                    MessageBox.Show(Result);
                }
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            SubFamiliasCL cl = new SubFamiliasCL();
            cl.intFamiliasID = intFamiliasID;
            cl.intSubFamiliasID = intSubFamiliasID;
            String Result = cl.SubFamiliasLlenaCajas();
            if (Result == "OK")
            {
                cboFamiliasID.EditValue = cl.intFamiliasID;
                txtNombre.Text = cl.strNombre;
                swIncluirentienda.IsOn = cl.intTienda == 1 ? true : false;

                gridControlDes.DataSource = cl.SubFamiliasDesGrid();

            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas



        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
       
                if (intSubFamiliasID == 0)
                {
                    MessageBox.Show("Selecciona un renglón");
                }
                else
                {
                    Editar();
                }
        }  //Editar

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();
            blNuevo = false;
        }

        private void BotonesEdicion()
        {
  
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup2.Visible = true;

            navigationFrame.SelectedPageIndex = 1;
        }

        private void Eliminar()
        {
            SubFamiliasCL cl = new SubFamiliasCL();
            cl.intFamiliasID = intFamiliasID;
            cl.intSubFamiliasID = intSubFamiliasID;
            String Result = cl.SubFamiliasEliminar();
            if (Result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        
                if (intSubFamiliasID == 0)
                {
                    MessageBox.Show("Selecciona un renglón");
                }
                else
                {
                    DialogResult Result = MessageBox.Show("Desea eliminar el ID: " + intFamiliasID.ToString()+ " con SubFamiliaID: " + intSubFamiliasID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                    if (Result.ToString() == "Yes")
                    {
                        Eliminar();
                    }
                }
        }

        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageGroup1.Visible = true;
            ribbonPageGroup2.Visible = false;
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridSubFamilias";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
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

        private void gridViewPrincipal_RowClick(Object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intFamiliasID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "FamiliasID"));
            intSubFamiliasID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "SubFamiliasID"));
        }

        public class descripcionesCL
        {
            public string Des { get; set; }
        }

        private void gridControlDes_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                view.DeleteSelectedRows();
                e.Handled = true;
            }
        }

        private void InicializaLista()
        {
            detalle = new BindingList<descripcionesCL>();
            detalle.AllowNew = true;
            gridControlDes.DataSource = detalle;
        }

    }
}