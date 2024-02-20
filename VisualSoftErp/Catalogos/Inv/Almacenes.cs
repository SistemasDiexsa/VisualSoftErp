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
    public partial class Almacenes : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intAlmacenesID;
        public Almacenes()
        {
            InitializeComponent();
            grdAlmacen.OptionsBehavior.ReadOnly = true;
            grdAlmacen.OptionsBehavior.Editable = false;
            grdAlmacen.OptionsView.ShowViewCaption = true;
            grdAlmacen.ViewCaption = "Catálogo de Almacenes";
            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            almacenesCL cl = new almacenesCL();
            gridControl1.DataSource = cl.almacenesGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAlmacenes";
            clg.restoreLayout(grdAlmacen);

        }//LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
            intAlmacenesID = 0;
            swActivo.IsOn = true;

        }//Nuevo()

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            swActivo.IsOn = false;
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

                almacenesCL cl = new almacenesCL();
                cl.intAlmacenesID = intAlmacenesID;
                cl.strNombre = txtNombre.Text;
                if (swActivo.IsOn)
                {
                    cl.intActivo = 1;
                }
                else { cl.intActivo = 0; }
                result = cl.AlmacenesCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intAlmacenesID == 0)
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
                MessageBox.Show("Guardar: "+ex.Message);
            }
        }//Guardar

        private string Valida()
        {
            if (txtNombre.Text == "")
            {
                return "El nombre no puede ir vacio";
            }            
            return "OK";
        }//Valida()

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();

        }//Editar()

        private void llenaCajas()
        {
            almacenesCL cl = new almacenesCL();
            cl.intAlmacenesID = intAlmacenesID;
            
            string result = cl.almacenesLlenaCajas();

            if (result == "OK")
            {
                  txtNombre.Text = cl.strNombre;
                if (cl.intActivo == 1)
                    { swActivo.IsOn = true; }
                else
                    { swActivo.IsOn = false; }
            }

            else
            {
                MessageBox.Show(result);
            }
        }//llenaCajas()

        private void Eliminar()
        {
            almacenesCL cl = new almacenesCL();
            cl.intAlmacenesID = intAlmacenesID;
            string result = cl.almacenesEliminar();

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
            if (intAlmacenesID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void grdAlmacen_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intAlmacenesID = Convert.ToInt32(grdAlmacen.GetRowCellValue(grdAlmacen.FocusedRowHandle, "AlmacenesID"));
        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intAlmacenesID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult result = MessageBox.Show("Desea eliminar el almacén " + intAlmacenesID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void bbiVista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grdAlmacen.ShowRibbonPrintPreview();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAlmacenes";
            string result = clg.SaveGridLayout(grdAlmacen);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }
    }
}