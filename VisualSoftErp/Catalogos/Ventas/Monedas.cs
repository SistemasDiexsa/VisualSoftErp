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
    public partial class Monedas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string strMoneda;
        public Monedas()
        {
            InitializeComponent();

            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Catálogo de Monedas";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            monedasCL cl = new monedasCL();
            grdMonedas.DataSource = cl.monedasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridMonedas";
            clg.restoreLayout(gridView1);

        }//LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
            strMoneda = string.Empty;

        }//Nuevo()

        private void LimpiaCajas()
        {
            txtDes.Text = string.Empty;
            txtMonedaID.Text = string.Empty;
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

                monedasCL cl = new monedasCL();
                cl.strMonedaID = txtMonedaID.Text;
                cl.strDes = txtDes.Text;

                result = cl.MonedasCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (strMoneda == "")
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
                MessageBox.Show(ex.Message);
            }
        }//Guardar

        private string Valida()
        {
            if (txtMonedaID.Text == "")
            {
                return "Moneda no puede ir vacio";
            }
            if (txtDes.Text == "")
            {
                return "La descripción no puede ir vacio";
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
            monedasCL cl = new monedasCL();
            cl.strMonedaID = strMoneda;
            string result = cl.monedasLlenaCajas();
            if (result == "OK")
            {
                txtDes.Text = cl.strDes;
                txtMonedaID.Text = cl.strMonedaID;
            }

            else
            {
                MessageBox.Show(result);
            }
        }//llenaCajas()

        private void Eliminar()
        {
            monedasCL cl = new monedasCL();
            cl.strMonedaID = strMoneda;
            string result = cl.monedasEliminar();

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

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            strMoneda = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MonedasID"));
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (strMoneda == "")
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (strMoneda == "")
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult result = MessageBox.Show("Desea eliminar la línea " + strMoneda.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridMonedas" +
                "";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiVista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grdMonedas.ShowRibbonPrintPreview();
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
    }
}