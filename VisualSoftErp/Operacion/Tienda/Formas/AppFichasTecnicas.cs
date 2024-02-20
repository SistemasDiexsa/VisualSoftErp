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

namespace VisualSoftErp.Operacion.Inventarios.Formas
{
    public partial class AppFichasTecnicas : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intFamiliasID;
        int intSubFamiliasID;
        int intSeq;

        public AppFichasTecnicas()
        {
            InitializeComponent();

            txtDescripcion.Properties.MaxLength = 100;
            txtDescripcion.EnterMoveNextControl = true;
            txtNombreimagen.Properties.MaxLength = 100;
            txtNombreimagen.EnterMoveNextControl = true;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Fichas técnicas";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            LlenarGrid();

            txtNombreimagen.ToolTip = "Capture nombre y extensión por ejemplo: (fichatecnica01.png)";

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            AppFichasTecnicasCL cl = new AppFichasTecnicasCL();
            gridControlPrincipal.DataSource = cl.AppFichasTecnicasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppFichasTecnicas";
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
            cargaSubFamilias();

            
        }

        private void cargaSubFamilias()
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
            cboSubFamiliasID.Properties.NullText = "Seleccione una Subfamilia";
        }

        private void BotonesEdicion()
        {
            //LimpiaCajas();
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup2.Visible = true;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intSeq = 0;
        }

        private void LimpiaCajas()
        {
            cboFamiliasID.EditValue = null;
            cboSubFamiliasID.EditValue = null;
            txtDescripcion.Text = string.Empty;
            txtNombreimagen.Text = string.Empty;
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
                AppFichasTecnicasCL cl = new AppFichasTecnicasCL();
                cl.intFamiliasID = Convert.ToInt32(cboFamiliasID.EditValue);
                cl.intSubFamiliasID = Convert.ToInt32(cboSubFamiliasID.EditValue);
  
                cl.strDescripcion = txtDescripcion.Text;
                cl.strNombreimagen = txtNombreimagen.Text;
                Result = cl.AppFichasTecnicasCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    LimpiaCajas();
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
            if (cboSubFamiliasID.EditValue == null)
            {
                return "El campo SubFamiliasID no puede ir vacio";
            }
            if (txtDescripcion.Text.Length == 0)
            {
                return "El campo Descripcion no puede ir vacio";
            }
            if (txtNombreimagen.Text.Length == 0)
            {
                return "El campo Nombreimagen no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void Eliminar()
        {
            AppFichasTecnicasCL cl = new AppFichasTecnicasCL();
            cl.intFamiliasID = intFamiliasID;
            cl.intSubFamiliasID = intSubFamiliasID;
            cl.intSeq = intSeq;
            String Result = cl.AppFichasTecnicasEliminar();
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


        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageGroup1.Visible = true;
            ribbonPageGroup2.Visible = false;
            LimpiaCajas();
            LlenarGrid();
            intSeq = 0;
            navigationFrame.SelectedPageIndex = 0;
        }


        private void gridViewPrincipal_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intFamiliasID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "FamiliasID"));
            intSubFamiliasID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "SubFamiliasID"));
            intSeq = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Seq"));

        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppFichasTecnicas";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }

        private void bbiElimnar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intSeq == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar la fam: " + intFamiliasID.ToString() + " SubFam: " + intSubFamiliasID.ToString() + " Seq: " + intSeq.ToString(), "Eliminar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void cboFamiliasID_EditValueChanged(object sender, EventArgs e)
        {
            object orow = cboFamiliasID.Properties.GetDataSourceRowByKeyValue(cboFamiliasID.EditValue);
            if (orow != null)
            {

                intFamiliasID = Convert.ToInt32(((DataRowView)orow)["Clave"].ToString());
                cargaSubFamilias();
            }
        }
    }
}