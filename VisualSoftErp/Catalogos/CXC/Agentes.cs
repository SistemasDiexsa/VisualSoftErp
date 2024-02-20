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

    public partial class Agentes : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intAgentesID;

        public Agentes()
        {
            InitializeComponent();

            txtNombre.Properties.MaxLength = 50;
            txtNombre.EnterMoveNextControl = true;
            txtTelefono.Properties.MaxLength = 50;
            txtTelefono.EnterMoveNextControl = true;
            txtPuesto.Properties.MaxLength = 70;
            txtPuesto.EnterMoveNextControl = true;
            txtEncabezado.Properties.MaxLength = 200;
            txtEncabezado.EnterMoveNextControl = true;
            txtPiedepagina.Properties.MaxLength = 200;
            txtPiedepagina.EnterMoveNextControl = true;
            txtEmail.Properties.MaxLength = 100;
            txtEmail.EnterMoveNextControl = true;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Agentes";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            agentesCL cl = new agentesCL();
            gridControlPrincipal.DataSource = cl.AgentesGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAgentes";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intAgentesID = 0;
        }

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPuesto.Text = string.Empty;
            txtEncabezado.Text = string.Empty;
            txtPiedepagina.Text = string.Empty;
        }

        private void bbiGuardar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
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
                agentesCL cl = new agentesCL();
                cl.intAgentesID = intAgentesID;
                cl.strNombre = txtNombre.Text;
                cl.strTelefono = txtTelefono.Text;
                cl.strEmail = txtEmail.Text;
                cl.strPuesto = txtPuesto.Text;
                cl.strEncabezado = txtEncabezado.Text;
                cl.strPiedepagina = txtPiedepagina.Text;
                Result = cl.AgentesCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intAgentesID == 0)
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
            if (txtNombre.Text.Length == 0)
            {
                return "El campo Nombre no puede ir vacio";
            }
            if (txtTelefono.Text.Length == 0)
            {
                return "El campo Telefono no puede ir vacio";
            }
            if (txtEmail.Text.Length==0)
            {
                return "El correo no puede ir vacío";
            }
            if (txtPuesto.Text.Length == 0)
            {
                return "El campo Puesto no puede ir vacio";
            }
            if (txtEncabezado.Text.Length == 0)
            {
                return "El campo Encabezado no puede ir vacio";
            }
            if (txtPiedepagina.Text.Length == 0)
            {
                return "El campo Piedepagina no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            agentesCL cl = new agentesCL();
            cl.intAgentesID = intAgentesID;
            String Result = cl.AgentesLlenaCajas();
            if (Result == "OK")
            {
                txtNombre.Text = cl.strNombre;
                txtTelefono.Text = cl.strTelefono;
                txtEmail.Text = cl.strEmail;
                txtPuesto.Text = cl.strPuesto;
                txtEncabezado.Text = cl.strEncabezado;
                txtPiedepagina.Text = cl.strPiedepagina;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intAgentesID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }  //Editar

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
            agentesCL cl = new agentesCL();
            cl.intAgentesID = intAgentesID;
            String Result = cl.AgentesEliminar();
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

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intAgentesID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intAgentesID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAgentes";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }

        private void bbiVista_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlPrincipal.ShowRibbonPrintPreview();
        }

        private void gridViewPrincipal_RowClick(Object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intAgentesID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "AgentesID"));
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL cl = new globalCL();
            cl.Bitacora("Excelente2");
        }

        
    }
}
