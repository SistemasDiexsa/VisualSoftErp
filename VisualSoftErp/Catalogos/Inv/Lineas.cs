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
    public partial class Lineas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intLinea;
        public Lineas()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;

            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Catálogo de Lineas";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            lineasCL cl = new lineasCL();
            gridControl1.DataSource = cl.lineasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridLineas";
            clg.restoreLayout(gridView1);

        }//LlenarGrid()

        

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intLinea = 0;

        }

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            txtMargenarribadelcosto.Text = string.Empty;
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
            clg.strGridLayout = "gridLineas";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

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

                lineasCL cl = new lineasCL();
                cl.intLineasID = intLinea;
                cl.strNombre = txtNombre.Text;
                cl.decMargenarribadelcosto = Convert.ToDecimal(txtMargenarribadelcosto.Text);
                cl.intCodigoArticulo = swCodigoArticulo.IsOn == true ? 1 : 0;
                result = cl.LineasCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intLinea == 0)
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
        }

        private string Valida()
        {
            if (txtNombre.Text == "")
            {
                return "El nombre no puede ir vacio";
            }

            globalCL cl = new globalCL();
            if (!cl.esNumerico(txtMargenarribadelcosto.Text))
            {
                txtMargenarribadelcosto.Text = "0";
            }

            return "OK";
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intLinea = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LineasID"));

        }

        private void llenaCajas()
        {
            lineasCL cl = new lineasCL();
            cl.intLineasID = intLinea;
            string result = cl.LineasllenaCajas();
            if (result == "OK")
            {
                txtNombre.Text = cl.strNombre;
                txtMargenarribadelcosto.Text = cl.decMargenarribadelcosto.ToString();
                swCodigoArticulo.IsOn = cl.intCodigoArticulo == 1 ? true : false;
            }

            else
            {
                MessageBox.Show(result);
            }
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //validacion de editar  



            if (intLinea == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }

        }

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
            lineasCL cl = new lineasCL();
            cl.intLineasID = intLinea;
            string result = cl.LineasEliminar();

            if (result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intLinea == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult result = MessageBox.Show("Desea eliminar la línea " + intLinea.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void bbiVista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }
    }

}