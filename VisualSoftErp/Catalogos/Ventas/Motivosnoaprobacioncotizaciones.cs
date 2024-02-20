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
    public partial class Motivosnoaprobacioncotizaciones : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intMotivoID;
        public Motivosnoaprobacioncotizaciones()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;

            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Catálogo de Motivos de no aprobaciones";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            motivosnoaprobacioncotizacionesCL cl = new motivosnoaprobacioncotizacionesCL();
            grdmotivos.DataSource = cl.motivosnoaprobacioncotizacionesGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridmotivo";
            clg.restoreLayout(gridView1);

        }//LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
            intMotivoID = 0;

        }//Nuevo()

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;

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

                motivosnoaprobacioncotizacionesCL cl = new motivosnoaprobacioncotizacionesCL();
                cl.intMotivosnoaprobacioncotizacionesID = intMotivoID;
                cl.strNombre = txtNombre.Text;

                result = cl.MotivosnoaprobacioncotizacionesCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intMotivoID == 0)
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

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();

        }//Editar()

        private string Valida()
        {
            if (txtNombre.Text == "")
            {
                return "El nombre no puede ir vacio";
            }
            return "OK";
        }//Valida()

        private void llenaCajas()
        {
            motivosnoaprobacioncotizacionesCL cl = new motivosnoaprobacioncotizacionesCL();
            cl.intMotivosnoaprobacioncotizacionesID = intMotivoID;
            string result = cl.motivosnoaprobacioncotizacionesLlenaCajas();

            if (result == "OK")
            {
                txtNombre.Text = cl.strNombre;

            }

            else
            {
                MessageBox.Show(result);
            }
        }//llenaCajas()

        private void Eliminar()
        {
            motivosnoaprobacioncotizacionesCL cl = new motivosnoaprobacioncotizacionesCL();
            cl.intMotivosnoaprobacioncotizacionesID = intMotivoID;
            string result = cl.motivosnoaprobacioncotizacionesEliminar();

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

        private void bbiNuevo_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void bbiEditar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intMotivoID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void bbiGuardar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiEliminar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intMotivoID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult result = MessageBox.Show("Desea eliminar la línea " + intMotivoID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void bbiRegresar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void bbiVista_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grdmotivos.ShowRibbonPrintPreview();
        }

        private void bbiCerrar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridmotivo" +
                "";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void gridView1_RowClick_1(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intMotivoID = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MotivosnoaprobacioncotizacionesID"));
        }
    }
}