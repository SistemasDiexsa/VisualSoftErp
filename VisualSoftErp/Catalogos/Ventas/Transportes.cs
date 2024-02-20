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
    public partial class Transportes : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intTransporte;
        public Transportes()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Catálogo de Transportes";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            transportesCL cl = new transportesCL();
            grdTransporte.DataSource = cl.transportesGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridTransportes";
            clg.restoreLayout(gridView1);

        }//LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
            intTransporte = 0;

        }//Nuevo()

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            txtCobertura.Text = string.Empty;
            txtHorario.Text = string.Empty;
            swPorCobrar.IsOn = false;
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

                transportesCL cl = new transportesCL();
                cl.intTransportesID = intTransporte;
                cl.strNombre = txtNombre.Text;
                cl.strCobertura = txtCobertura.Text;
                cl.strHorario = txtHorario.Text;
                if (swPorCobrar.IsOn)
                {
                    cl.strPorcobrar = "1";
                }
                else { cl.strPorcobrar = "0"; }
                switch (cboTipo.SelectedIndex)
                {
                    case 1:
                        cboTipo.SelectedIndex = 1;
                        cl.strTipo = "1";
                        break;

                    case 2:
                        cboTipo.SelectedIndex = 2;
                        cl.strTipo = "2";
                        break;

                    case 3:
                        cboTipo.SelectedIndex = 3;
                        cl.strTipo = "3";
                        break;
                }
                result = cl.TransportesCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intTransporte == 0)
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
            if (txtHorario.Text == "")
            {
                return "Ingrese horario";
            }
            if (txtCobertura.Text == "")
            {
                return "ingrese cobertura";
            }
            if (cboTipo.SelectedIndex == 0)
            {
                return "Seleccione tipo de transporte";
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
            transportesCL cl = new transportesCL();
            cl.intTransportesID = intTransporte;
            string result = cl.transportesLlenaCajas();
            if (result == "OK")
            {
                txtNombre.Text = cl.strNombre;
                txtCobertura.Text = cl.strCobertura;
                txtHorario.Text = cl.strHorario;
                if (cl.strPorcobrar == "1")
                {
                    swPorCobrar.IsOn = true;

                }
                else { swPorCobrar.IsOn = false; }
                switch (cl.strTipo)
                {
                    case "1":
                        cboTipo.SelectedIndex = 1;
                        break;

                    case "2":
                        cboTipo.SelectedIndex = 2;
                        break;

                    case "3":
                        cboTipo.SelectedIndex = 3;
                        break;
                }

            }

            else
            {
                MessageBox.Show(result);
            }
        }//llenaCajas()

        private void Eliminar()
        {
            transportesCL cl = new transportesCL();
            cl.intTransportesID = intTransporte;
            string result = cl.transportesEliminar();

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
            if (intTransporte == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intTransporte = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TransportesID"));
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
            clg.strGridLayout = "gridTransportes" +
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
            grdTransporte.ShowRibbonPrintPreview();
        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intTransporte == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult result = MessageBox.Show("Desea eliminar la línea " + intTransporte.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }
    }
}