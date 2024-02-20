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
    public partial class Tiposdecambio : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string strMoneda;
        DateTime dfecha;
        int ParidadG;
        public Tiposdecambio()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Catálogo de tipo de cambio";

            LlenarGrid();
            llenacombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            tiposdecambioCL cl = new tiposdecambioCL();
            grdMTiposdeC.DataSource = cl.tiposdecambioGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridtiposdecambio";
            clg.restoreLayout(gridView1);

        }//LlenarGrid()

        private void llenacombos()
        {
            combosCL cl = new combosCL();
            cbomoneda.Properties.ValueMember = "Clave";
            cbomoneda.Properties.DisplayMember = "Des";
            cl.strTabla = "Monedas";
            cbomoneda.Properties.DataSource = cl.CargaCombos();
            cbomoneda.Properties.ForceInitialize();
            cbomoneda.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cbomoneda.ItemIndex = 1;
        }//llenacombos

        private void Nuevo()
        {
            BotonesEdicion();
            strMoneda = String.Empty;

            cbomoneda.Enabled = true;
            dateEditF.Enabled = true;
        }//Nuevo()

        private void LimpiaCajas()
        {

            cbomoneda.EditValue = 1;
            txtParidad.Text = string.Empty;
            dateEditF.Text = DateTime.Now.ToShortDateString();
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

                tiposdecambioCL cl = new tiposdecambioCL();
                cl.strMoneda = Convert.ToString(cbomoneda.EditValue);
                cl.fFecha = Convert.ToDateTime(dateEditF.Text);
                cl.dParidad = Convert.ToDecimal(txtParidad.Text);
                result = cl.TiposdecambioCrud();

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
                MessageBox.Show("Guardar: " + ex.Message);
            }
        }//Guardar

        private string Valida()
        {
            if (cbomoneda.Text == string.Empty)
            {
                return "Moneda no puede ir vacio";
            }
            if (dateEditF.Text == "")
            {
                return "La fecha no puede ir vacio";
            }
            if (txtParidad.Text == "")
            {
                return "La paridad no puede ir vacio";
            }
            return "OK";
        }//Valida()

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();

            cbomoneda.Enabled = false;
            dateEditF.Enabled = false;

        }//Editar()

        private void llenaCajas()
        {
            tiposdecambioCL cl = new tiposdecambioCL();
            cl.strMoneda = strMoneda;
            cl.fFecha = dfecha;
            string result = cl.tiposdecambioLlenaCajas();
            if (result == "OK")
            {
                cbomoneda.EditValue = cl.strMoneda;
                dateEditF.Text = Convert.ToString(cl.fFecha);
                txtParidad.Text = Convert.ToString(cl.dParidad);
            }

            else
            {
                MessageBox.Show(result);
            }
        }//llenaCajas()

        private void Eliminar()
        {
            //SE USAN LOS 3 CAMPOS DE LA TABLA PARA ELIMINAR EL SELECCIONADO
            //LOS 3 CAMPOS SE OBTIENEN DEL GRID

            tiposdecambioCL cl = new tiposdecambioCL();
            cl.strMoneda = strMoneda;
            cl.fFecha = dfecha;
            cl.dParidad = ParidadG;
            string result = cl.tiposdecambioEliminar();

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
            if (strMoneda == null)
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
            if (strMoneda == null)
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
            clg.strGridLayout = "gridtiposdecambio" +
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
            grdMTiposdeC.ShowRibbonPrintPreview();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            strMoneda = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MonedasID"));
            
            //parametros para eliminar
            dfecha = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Fecha"));
            ParidadG = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Paridad"));
        }
    }

}
