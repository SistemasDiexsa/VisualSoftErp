using System;
using System.Windows.Forms;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Catalogos.CXC
{
    public partial class Tiposdemovimientocxc : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intTipoMovID, intReservado;
        public Tiposdemovimientocxc()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Catálogo de Tipos de Movimiento CxC";
            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void LlenarGrid()
        {
            tiposdemovimientocxcCL cl = new tiposdemovimientocxcCL();
            grdTipoMov.DataSource = cl.tiposdemovimientocxcGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridTiposdemovimientocxc";
            clg.restoreLayout(gridView1);

        }//LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
            intTipoMovID = 0;
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

                tiposdemovimientocxcCL cl = new tiposdemovimientocxcCL();
                cl.intTiposdemovimientocxcID = intTipoMovID;
                cl.strNombre = txtNombre.Text;
                if (cboTipo.SelectedIndex == 1)
                {
                    cl.strTipo = "C";
                }
                else { cl.strTipo = "A"; }

                result = cl.TiposdemovimientocxcCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intTipoMovID == 0)
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
                return "Seleccione el tipo de movimiento cxc";
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
            tiposdemovimientocxcCL cl = new tiposdemovimientocxcCL();
            cl.intTiposdemovimientocxcID = intTipoMovID;
            string result = cl.tiposdemovimientocxcLlenaCajas();

            if (result == "OK")
            {

                txtNombre.Text = cl.strNombre;

                if (cl.strTipo == "C")
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
            tiposdemovimientocxcCL cl = new tiposdemovimientocxcCL();
            cl.intTiposdemovimientocxcID = intTipoMovID;
            string result = cl.tiposdemovimientocxcEliminar();


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
           if (intTipoMovID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }

            else
            {
                if (intReservado == 1)
                {
                    MessageBox.Show("No se puede editar este movimiento");
                }

                else { Editar();  }
                /*else
                {
                    if (intMov != 0)
                    {
                        MessageBox.Show("Este ID ya tiene movimientos de inventarios, No se puede editar");

                    }
                    else
                    {
                        Editar();
                    }
                }*/

            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intTipoMovID = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TiposdemovimientocxcID"));
            intReservado = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Reservado"));         

        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intTipoMovID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                if (intReservado == 1)
                {
                    MessageBox.Show("No se puede Eliminar");
                }
                else { Eliminar(); }
                //if (intMov != 0)
                //{
                //    MessageBox.Show("Este ID ya tiene movimientos de inventarios, No se puede eliminar");
                //}
                //else
                //{
                //    DialogResult result = MessageBox.Show("Desea eliminar la línea " + intTiposmovimientoinv.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                //    if (result.ToString() == "Yes")
                //    {
                //        Eliminar();
                //    }
                //}
            }

        }

        private void bbiVista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grdTipoMov.ShowRibbonPrintPreview();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridTiposdemovimientocxc" +
                "";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

    }
}