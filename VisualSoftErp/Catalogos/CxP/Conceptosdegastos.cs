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

    public partial class Conceptosdegastos : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intConceptosdegastosID;

        public Conceptosdegastos()
        {
            InitializeComponent();

            txtNombre.Properties.MaxLength = 100;
            txtNombre.EnterMoveNextControl = true;
            txtCuenta.Properties.MaxLength = 16;
            txtCuenta.EnterMoveNextControl = true;
            txtCuentaRetIva.Properties.MaxLength = 16;
            txtCuentaRetIva.EnterMoveNextControl = true;
            txtCuentaRetIsr.Properties.MaxLength = 16;
            txtCuentaRetIsr.EnterMoveNextControl = true;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Conceptosdegastos";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            ConceptosdegastosCL cl = new ConceptosdegastosCL();
            gridControlPrincipal.DataSource = cl.ConceptosdegastosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridConceptosdegastos";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intConceptosdegastosID = 0;
        }

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            txtCuenta.Text = string.Empty;
            txtRetIsr.Text = string.Empty;
            txtRetIva.Text = string.Empty;          
            txtCuentaRetIva.Text = string.Empty;
            txtCuentaRetIsr.Text = string.Empty;
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
                ConceptosdegastosCL cl = new ConceptosdegastosCL();
                cl.intConceptosdegastosID = intConceptosdegastosID;
                cl.strNombre = txtNombre.Text;
                cl.strCuenta = txtCuenta.Text;
                cl.dRetIva = Convert.ToDecimal(txtRetIva.Text);
                cl.dRetIsr = Convert.ToDecimal(txtRetIsr.Text);
                cl.strCuentaRetIva = txtCuentaRetIva.Text;
                cl.strCuentaRetIsr = txtCuentaRetIsr.Text;
                Result = cl.ConceptosdegastosCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intConceptosdegastosID == 0)
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
            if (txtCuenta.Text.Length == 0)
            {
                return "El campo Cuenta no puede ir vacio";
            }
            if (txtRetIva.Text.Length == 0)
            {
                return "El campo RetIva no puede ir vacio";
            }
            if (txtRetIsr.Text.Length == 0)
            {
                return "El campo RetIsr no puede ir vacio";
            }
            if (txtCuentaRetIva.Text.Length == 0)
            {
                return "El campo CuentaRetIva no puede ir vacio";
            }
            if (txtCuentaRetIsr.Text.Length == 0)
            {
                return "El campo CuentaRetIsr no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            ConceptosdegastosCL cl = new ConceptosdegastosCL();
            cl.intConceptosdegastosID = intConceptosdegastosID;
            String Result = cl.ConceptosdegastosLlenaCajas();
            if (Result == "OK")
            {
                txtNombre.Text = cl.strNombre;
                txtCuenta.Text = cl.strCuenta;
                txtRetIva.Text = cl.dRetIva.ToString();
                txtRetIsr.Text = cl.dRetIsr.ToString();
                txtCuentaRetIva.Text = cl.strCuentaRetIva;
                txtCuentaRetIsr.Text = cl.strCuentaRetIsr;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intConceptosdegastosID == 0)
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
            ConceptosdegastosCL cl = new ConceptosdegastosCL();
            cl.intConceptosdegastosID = intConceptosdegastosID;
            String Result = cl.ConceptosdegastosEliminar();
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
            if (intConceptosdegastosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intConceptosdegastosID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
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
            clg.strGridLayout = "gridConceptosdegastos";
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
            intConceptosdegastosID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ConceptosdegastosID"));
        }

    }
}
