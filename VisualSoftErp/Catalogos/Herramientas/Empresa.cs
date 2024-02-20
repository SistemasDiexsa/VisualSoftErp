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

namespace VisualSoftErp.Catalogos
{

    public partial class Empresa : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intEmpresasID;

        public Empresa()
        {
            InitializeComponent();

            txtNombre.Properties.MaxLength = 100;
            txtRegimenfiscal.Properties.MaxLength = 3;
            txtLlavecsd.Properties.MaxLength = 50;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Empresa";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            EmpresaCL cl = new EmpresaCL();
            gridControlPrincipal.DataSource = cl.EmpresaGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridEmpresa";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intEmpresasID = 0;
        }

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            txtRegimenfiscal.Text = string.Empty;
            txtLlavecsd.Text = string.Empty;
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
                EmpresaCL cl = new EmpresaCL();
                cl.intEmpresasID = intEmpresasID;
                cl.strNombre = txtNombre.Text;
                cl.strRegimenfiscal = txtRegimenfiscal.Text;
                cl.strLlavecsd = txtLlavecsd.Text;
                Result = cl.EmpresaCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intEmpresasID == 0)
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
            if (txtNombre.Text.Length == 0)
            {
                return "El campo Nombre no puede ir vacio";
            }
            if (txtRegimenfiscal.Text.Length == 0)
            {
                return "El campo Regimenfiscal no puede ir vacio";
            }
            if (txtLlavecsd.Text.Length == 0)
            {
                return "El campo Llavecsd no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            EmpresaCL cl = new EmpresaCL();
            cl.intEmpresasID = intEmpresasID;
            String Result = cl.EmpresaLlenaCajas();
            if (Result == "OK")
            {
                txtNombre.Text = cl.strNombre;
                txtRegimenfiscal.Text = cl.strRegimenfiscal;
                txtLlavecsd.Text = cl.strLlavecsd;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intEmpresasID == 0)
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
        }

        private void BotonesEdicion()
        {
            LimpiaCajas();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            navigationFrame.SelectedPageIndex = 1;
        }

        private void Eliminar()
        {
            EmpresaCL cl = new EmpresaCL();
            cl.intEmpresasID = intEmpresasID;
            String Result = cl.EmpresaEliminar();
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
            if (intEmpresasID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intEmpresasID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridEmpresa";
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

        private void gridViewPrincipal_RowClick(Object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intEmpresasID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "EmpresasID"));
        }

    }
}
