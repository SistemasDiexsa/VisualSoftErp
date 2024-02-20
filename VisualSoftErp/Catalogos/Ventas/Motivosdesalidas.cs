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
    public partial class Motivosdesalidas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intMotivosID;
        public Motivosdesalidas()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Catálogo de motivos de salida";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void LlenarGrid()
        {
            motivosdesalidasCL cl = new motivosdesalidasCL();
            grdMotivosSalida.DataSource = cl.motivosdesalidasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridMotivosdesalida";
            clg.restoreLayout(gridView1);

        }//LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
            intMotivosID = 0;

        }//Nuevo()

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            swFactura.IsOn = false;

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

                motivosdesalidasCL cl = new motivosdesalidasCL();
                cl.intMotivosdesalidasID = intMotivosID;
                cl.strNombre = txtNombre.Text;

                if (swFactura.IsOn)
                {
                    cl.intSeFactura = 1;
                }
                else { cl.intSeFactura = 0; }

                result = cl.MotivosdesalidasCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intMotivosID == 0)
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
            return "OK";
        }//Valida()

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();

        }//Editar()

        private void llenaCajas()
        {
            motivosdesalidasCL cl = new motivosdesalidasCL();
            cl.intMotivosdesalidasID = intMotivosID;
            string result = cl.motivosdesalidasLlenaCajas();
            if (result == "OK")
            {
                txtNombre.Text = cl.strNombre;
               
                if (cl.intSeFactura == 1)
                {
                    swFactura.IsOn = true;

                }
                else { swFactura.IsOn = false; }
            }

            else
            {
                MessageBox.Show(result);
            }
        }//llenaCajas()

        private void Eliminar()
        {
            motivosdesalidasCL cl = new motivosdesalidasCL();
            cl.intMotivosdesalidasID = intMotivosID;
            string result = cl.motivosdesalidasEliminar();

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

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intMotivosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intMotivosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult result = MessageBox.Show("Desea eliminar la línea " + intMotivosID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
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

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridMotivosdesalida" +"";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiVista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grdMotivosSalida.ShowRibbonPrintPreview();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            intMotivosID = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MotivosdesalidasID"));
        }


    }
}