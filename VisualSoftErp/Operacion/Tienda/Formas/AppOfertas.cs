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

namespace VisualSoftErp.Operacion.Inventarios.Formas
{
    public partial class AppOfertas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intAppOfertasID;
        public AppOfertas()
        {
            InitializeComponent();
            txtDescripcion.Properties.MaxLength = 100;
            txtDescripcion.EnterMoveNextControl = true;
            txtVigenciaFinal.Text = DateTime.Now.ToShortDateString();
            txtVigenciaInicio.Text = DateTime.Now.ToShortDateString();

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de AppOfertas";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            AppOfertasCL cl = new AppOfertasCL();
            gridControlPrincipal.DataSource = cl.AppOfertasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppOfertas";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Articulos";
            cboArticulosID.Properties.ValueMember = "Clave";
            cboArticulosID.Properties.DisplayMember = "Des";
            cboArticulosID.Properties.DataSource = cl.CargaCombos();
            cboArticulosID.Properties.ForceInitialize();
            cboArticulosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulosID.Properties.PopulateColumns();
            cboArticulosID.Properties.Columns["Clave"].Visible = false;
            cboArticulosID.Properties.Columns["FactorUM2"].Visible = false;
            cboArticulosID.Properties.NullText = "Seleccione un articulo";
            cl.strTabla = "Monedas";
            cboMonedasID.Properties.ValueMember = "Clave";
            cboMonedasID.Properties.DisplayMember = "Des";
            cboMonedasID.Properties.DataSource = cl.CargaCombos();
            cboMonedasID.Properties.ForceInitialize();
            cboMonedasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedasID.Properties.PopulateColumns();
            cboMonedasID.Properties.Columns["Clave"].Visible = false;
            cboMonedasID.ItemIndex = 0;
        }
        private void BotonesEdicion()
        {
            //LimpiaCajas();
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup2.Visible = true;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intAppOfertasID = 0;
        }

        private void LimpiaCajas()
        {
            cboArticulosID.EditValue = null;
            txtPrecio.Text = "0";
            cboMonedasID.ItemIndex = 0;
            txtDescripcion.Text = string.Empty;
            txtVigenciaInicio.Text = DateTime.Now.ToShortDateString();
            txtVigenciaFinal.Text = DateTime.Now.ToShortDateString();
        }

        private void Guardar()
        {
            try
            {
                String Result = Valida();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }
                AppOfertasCL cl = new AppOfertasCL();
                cl.intAppOfertasID = intAppOfertasID;
                cl.intArticulosID = Convert.ToInt32(cboArticulosID.EditValue);
                cl.dPrecio = Convert.ToInt32(txtPrecio.Text);
                cl.strMoendasID = cboMonedasID.EditValue.ToString();
                cl.strDescripción = txtDescripcion.Text;
                cl.fVigenciaInicio = Convert.ToDateTime(txtVigenciaInicio.Text);
                cl.fVigenciaFin = Convert.ToDateTime(txtVigenciaFinal.Text);
                Result = cl.AppOfertasCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intAppOfertasID == 0)
                    {
                        LimpiaCajas();
                    }
                }
                else
                {
                    MessageBox.Show(Result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        } //Guardar

        private string Valida()
        {
            if (cboArticulosID.EditValue == null)
            {
                return "El campo ArticulosID no puede ir vacio";
            }
            if (txtPrecio.Text.Length == 0)
            {
                return "El campo Precio no puede ir vacio";
            }
            if (cboMonedasID.Text.Length == 0)
            {
                return "El campo MoendasID no puede ir vacio";
            }
            if (txtDescripcion.Text.Length == 0)
            {
                return "El campo Descripción no puede ir vacio";
            }
            if (txtVigenciaInicio.Text.Length == 0)
            {
                return "El campo VigenciaInicio no puede ir vacio";
            }
            if (txtVigenciaFinal.Text.Length == 0)
            {
                return "El campo VigenciaFin no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void Editar()
        {
            BotonesEdicion();
            
            llenaCajas();
        }

        private void llenaCajas()
        {
            AppOfertasCL cl = new AppOfertasCL();
            cl.intAppOfertasID = intAppOfertasID;
            String Result = cl.AppOfertasLlenaCajas();
            if (Result == "OK")
            {
                cboArticulosID.EditValue = cl.intArticulosID;
                txtPrecio.Text = cl.dPrecio.ToString(); ;
                cboMonedasID.EditValue = cl.strMoendasID;
                txtDescripcion.Text = cl.strDescripción;
                txtVigenciaInicio.Text = cl.fVigenciaInicio.ToShortDateString();
                txtVigenciaFinal.Text = cl.fVigenciaFin.ToShortDateString();
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void Eliminar()
        {
            AppOfertasCL cl = new AppOfertasCL();
            cl.intAppOfertasID = intAppOfertasID;
            String Result = cl.AppOfertasEliminar();
            if (Result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(Result);
            }
        }

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
            ribbonPageGroup1.Visible = true;
            ribbonPageGroup2.Visible = false;
            LimpiaCajas();
            LlenarGrid();
            intAppOfertasID = 0;
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intAppOfertasID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void gridViewPrincipal_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intAppOfertasID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "AppOfertasID"));

        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppOfertas";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }

        private void bbiElimnar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intAppOfertasID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intAppOfertasID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }
    }
}