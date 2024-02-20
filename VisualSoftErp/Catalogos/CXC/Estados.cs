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

namespace VisualSoftErp.Clases.CXC_CLs
{
    public partial class Estados : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intEstadosID;
        public Estados()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Catálogo de Estados";


            gridView1.OptionsView.ShowAutoFilterRow = true;
            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();


            combosCL cl = new combosCL();
            cl.strTabla = "Zonas";

            cboZonasID.Properties.ValueMember = "Clave";
            cboZonasID.Properties.DisplayMember = "Des";            
            cboZonasID.Properties.DataSource = cl.CargaCombos();
            cboZonasID.Properties.ForceInitialize();
            cboZonasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;         
            cboZonasID.Properties.PopulateColumns();
            cboZonasID.Properties.Columns["Clave"].Visible = false;
            cboZonasID.Properties.NullText = "Seleccione una zonza";

        }

        private void LlenarGrid()
        {
            estadosCL cl = new estadosCL();
            grdEstados.DataSource = cl.estadosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridEstados";
            clg.restoreLayout(gridView1);

        }//LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
            intEstadosID = 0;
            cboZonasID.ItemIndex = 0;

        }//Nuevo()

        private void LimpiaCajas()
        {
         
            txtNombre.Text = string.Empty;
            txtClaveSepo.Text = string.Empty;
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

                estadosCL cl = new estadosCL();
                cl.intEstadosID = intEstadosID;
                cl.intZonasID = Convert.ToInt32(cboZonasID.EditValue);
                cl.strNombre = txtNombre.Text;
                cl.intClaveSepomex = Convert.ToInt32(txtClaveSepo.Text);
                result = cl.EstadosCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intEstadosID == 0)
                    {
                        LimpiaCajas();
                        txtNombre.Select();
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
            if (txtClaveSepo.Text == "")
            {
                txtClaveSepo.Text = "0";
            }
            if (cboZonasID.EditValue.ToString() == "")
            {
                return "Selecciona la zona";
            }

            globalCL clg = new globalCL();
            if (!clg.esNumerico(txtClaveSepo.Text))
            {
                txtClaveSepo.Text = "0";
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
            estadosCL cl = new estadosCL();
            cl.intEstadosID = intEstadosID;
            string result = cl.estadosLlenaCajas();
            if (result == "OK")
            {
                cboZonasID.EditValue = cl.intZonasID;
                txtNombre.Text = cl.strNombre;
                txtClaveSepo.Text = Convert.ToString(cl.intClaveSepomex);
            }

            else
            {
                MessageBox.Show(result);
            }
        }//llenaCajas()

        private void Eliminar()
        {
            estadosCL cl = new estadosCL();
            cl.intEstadosID = intEstadosID;
            string result = cl.estadosEliminar();

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
            if (intEstadosID == 0)
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
           if (intEstadosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult result = MessageBox.Show("Desea eliminar la línea " + intEstadosID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intEstadosID = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EstadosID"));
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
            clg.strGridLayout = "gridEstados" +
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
            grdEstados.ShowRibbonPrintPreview();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cboZonasID.EditValue.ToString());
        }
    }
}