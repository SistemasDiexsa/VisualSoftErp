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
    public partial class TiposdeMovimientoInv : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intTiposmovimientoinv;
        int intReservado;
        int intMov;
        public TiposdeMovimientoInv()
        {
            InitializeComponent();
            gdrTipoMovInv.OptionsBehavior.ReadOnly = true;
            gdrTipoMovInv.OptionsBehavior.Editable = false;
            gdrTipoMovInv.OptionsView.ShowViewCaption = true;
            gdrTipoMovInv.ViewCaption = "Catálogo de Tipos de Movimiento Inventario";
            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void LlenarGrid()
        {
            tiposdemovimientoinvCL cl = new tiposdemovimientoinvCL();
            gridControl1.DataSource = cl.tiposdemovimientoinvGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridTiposdemovimientoinv";
            clg.restoreLayout(gdrTipoMovInv);

        }//LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
            intTiposmovimientoinv = 0;
            cboTipo.SelectedIndex = 0;

        }//Nuevo()

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            cboTipo.SelectedIndex = 0;
        }//LimpiaCajas()

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

        }//BotonesEdicion()

        private void Guardar()
        {
            try
            {
                string result = Valida();

                if (result != "OK")
                {
                    MessageBox.Show(result);
                    return;
                }

                tiposdemovimientoinvCL cl = new tiposdemovimientoinvCL();
                cl.intTiposdemovimientoinvID = intTiposmovimientoinv;
                cl.strNombre = txtNombre.Text;
                if (cboTipo.SelectedIndex == 1)
                {
                    cl.strTipo = "E";
                }
                else { cl.strTipo = "S"; }

                result = cl.TiposdemovimientoinvCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intTiposmovimientoinv == 0)
                    {
                        LimpiaCajas();
                    }

                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        }//Guardar

        private string Valida()
        {
            if (txtNombre.Text == "")
            {
                return "El nombre no puede ir vacio";
            }
            if (cboTipo.SelectedIndex == 0)
            {
                return "Seleccione el tipo de movimiento";
            }
            return "OK";
        }//Valida()

        private void Editar()
        {
            if (intReservado == 1)
            {
                MessageBox.Show("Movimiento Reservado");
                return;
            }
            else
            {
                BotonesEdicion();
                llenaCajas();
            }
            

        }//Editar()

        private void llenaCajas()
        {
            tiposdemovimientoinvCL cl = new tiposdemovimientoinvCL();
            cl.intTiposdemovimientoinvID = intTiposmovimientoinv;
            string result = cl.tiposdemovimientoinvLlenaCajas();

            if (result == "OK")
            {

                txtNombre.Text = cl.strNombre;

                if (cl.strTipo == "E")
                { cboTipo.SelectedIndex = 1; }
                else
                { cboTipo.SelectedIndex = 2; }

            }

            else
            {
                MessageBox.Show(result);
            }
        }//llenaCajas()

        private void Eliminar()
        {
            tiposdemovimientoinvCL cl = new tiposdemovimientoinvCL();
            cl.intTiposdemovimientoinvID = intTiposmovimientoinv;
            string result = cl.tiposdemovimientoinvEliminar();


            if (result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(result);
            }


        }//Eliminar

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

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intTiposmovimientoinv == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }

            else
            {
                if (intReservado == 1)
                {
                    MessageBox.Show("No se puede editar este movimiento");
                }
                else
                {
                    if (intMov != 0)
                    {
                        MessageBox.Show("Este ID ya tiene movimientos de inventarios, No se puede editar");

                    }
                    else
                    {
                        Editar();
                    }
                }

            }
        }

        private void gdrTipoMovInv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intReservado = Convert.ToInt32(gdrTipoMovInv.GetRowCellValue(gdrTipoMovInv.FocusedRowHandle, "Reservado"));
            intTiposmovimientoinv = Convert.ToInt32(gdrTipoMovInv.GetRowCellValue(gdrTipoMovInv.FocusedRowHandle, "TiposdemovimientoinvID"));
            intMov = Convert.ToInt32(gdrTipoMovInv.GetRowCellValue(gdrTipoMovInv.FocusedRowHandle, "Mov"));

        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intTiposmovimientoinv == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                if (intReservado == 1)
                {
                    MessageBox.Show("No se puede Eliminar");
                }
                if (intMov != 0)
                {
                    MessageBox.Show("Este ID ya tiene movimientos de inventarios, No se puede eliminar");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Desea eliminar la línea " + intTiposmovimientoinv.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                    if (result.ToString() == "Yes")
                    {
                        Eliminar();
                    }
                }
            }

        }

        private void bbiVista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gdrTipoMovInv.ShowRibbonPrintPreview();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridTipoMovInv" +
                "";
            string result = clg.SaveGridLayout(gdrTipoMovInv);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }
    }


}