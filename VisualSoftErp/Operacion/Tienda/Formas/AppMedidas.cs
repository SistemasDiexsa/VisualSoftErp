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
    public partial class AppMedidas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intAppMedidasID;

        public AppMedidas()
        {
            InitializeComponent();

            txtDescripcion.Properties.MaxLength = 15;
            txtDescripcion.EnterMoveNextControl = true;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de AppMedidas";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            AppMedidasCL cl = new AppMedidasCL();
            gridControlPrincipal.DataSource = cl.AppMedidasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppMedidas";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
            intAppMedidasID = 0;
        }

        private void LimpiaCajas()
        {
            txtDescripcion.Text = string.Empty;
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
                AppMedidasCL cl = new AppMedidasCL();
                cl.intAppMedidasID = intAppMedidasID;
                cl.strDescripcion = txtDescripcion.Text;
                Result = cl.AppMedidasCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intAppMedidasID == 0)
                    {
                        LimpiaCajas();
                        
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
            if (txtDescripcion.Text.Length == 0)
            {
                return "El campo Descripcion no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            AppMedidasCL cl = new AppMedidasCL();
            cl.intAppMedidasID = intAppMedidasID;
            String Result = cl.AppMedidasLlenaCajas();
            if (Result == "OK")
            {
                txtDescripcion.Text = cl.strDescripcion;
               
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
            AppMedidasCL cl = new AppMedidasCL();
            cl.intAppMedidasID = intAppMedidasID;
            String Result = cl.AppMedidasEliminar();
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
            intAppMedidasID = 0;
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intAppMedidasID == 0)
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
            intAppMedidasID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "AppMedidasID"));

        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppTonos";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }

        private void bbiElimnar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intAppMedidasID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intAppMedidasID.ToString(), " Elimnar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }
    }
}