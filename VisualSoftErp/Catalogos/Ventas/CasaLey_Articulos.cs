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
    public partial class CasaLey_Articulos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intArt;
        public CasaLey_Articulos()
        {
            InitializeComponent();
           
            txtCodigoBarras.Properties.MaxLength = 50;
            txtCodigoBarras.EnterMoveNextControl = true;
            txtCodigoCasaLey.Properties.MaxLength = 50;
            txtCodigoCasaLey.EnterMoveNextControl = true;
            txtUMC.Properties.MaxLength = 10;
            txtUMC.EnterMoveNextControl = true;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "CasaLey Articulos para addenda";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            cl.strTabla = "Articulos";
            src.DataSource = cl.CargaCombos();
            
            
            cboArt.Properties.ValueMember = "Clave";
            cboArt.Properties.DisplayMember = "Des";
            cboArt.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboArt.Properties.ForceInitialize();
            cboArt.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArt.Properties.PopulateColumns();
            cboArt.Properties.Columns["Clave"].Visible = false;
            cboArt.ItemIndex = 0;
        }

        private void LlenarGrid()
        {
            CasaLey_ArticulosCL cl = new CasaLey_ArticulosCL();
            gridControlPrincipal.DataSource = cl.CasaLey_ArticulosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCasaLey_Articulos";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
           
            Cargacombos();
        }

        private void LimpiaCajas()
        {
            
            txtCodigoBarras.Text = string.Empty;
            txtCodigoCasaLey.Text = string.Empty;
            txtUMC.Text = string.Empty;
        }

        private void BotonesEdicion()
        {
            //LimpiaCajas();
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup2.Visible = true;
            navigationFrame.SelectedPageIndex = 1;
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
                CasaLey_ArticulosCL cl = new CasaLey_ArticulosCL();
                cl.intArt = Convert.ToInt32(cboArt.EditValue);
                cl.strCodigoBarras = txtCodigoBarras.Text;
                cl.strCodigoCasaLey = txtCodigoCasaLey.Text;
                cl.strUMC = txtUMC.Text;
                Result = cl.CasaLey_ArticulosCrud();
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
            if (cboArt.EditValue == null)
            {
                return "El campo Articulo no puede ir vacio";
            }
            if (txtCodigoBarras.Text.Length == 0)
            {
                return "El campo CodigoBarras no puede ir vacio";
            }
            if (txtCodigoCasaLey.Text.Length == 0)
            {
                return "El campo CodigoCasaLey no puede ir vacio";
            }
            if (txtUMC.Text.Length == 0)
            {
                return "El campo UMC no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            CasaLey_ArticulosCL cl = new CasaLey_ArticulosCL();
            cl.intArt = intArt;
            String Result = cl.CasaLey_ArticulosLlenaCajas();
            if (Result == "OK")
            {
                cboArt.EditValue = cl.intArt;
                txtCodigoBarras.Text = cl.strCodigoBarras;
                txtCodigoCasaLey.Text = cl.strCodigoCasaLey;
                txtUMC.Text = cl.strUMC;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();
        }

        private void Eliminar()
        {
            CasaLey_ArticulosCL cl = new CasaLey_ArticulosCL();
            cl.intArt = Convert.ToInt32(cboArt.EditValue);
            String Result = cl.CasaLey_ArticulosEliminar();
            if (Result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
                intArt = 0;
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
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intArt == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void gridViewPrincipal_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intArt = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ArticulosID"));

        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCLArt";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }

        private void bbiElimnar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intArt == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el Artículo: " + intArt.ToString(), "Eliminar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }
    }
}