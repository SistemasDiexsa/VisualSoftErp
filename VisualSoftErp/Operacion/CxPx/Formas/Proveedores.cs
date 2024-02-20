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

    public partial class Proveedores : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intProveedoresID;

        public Proveedores()
        {
            InitializeComponent();

            txtNombre.Properties.MaxLength = 100;
            txtNombre.EnterMoveNextControl = true;
            txtDireccion.Properties.MaxLength = 100;
            txtDireccion.EnterMoveNextControl = true;
            txtRfc.Properties.MaxLength = 14;
            txtRfc.EnterMoveNextControl = true;
            txtContacto.Properties.MaxLength = 100;
            txtContacto.EnterMoveNextControl = true;
            txtTelefono.Properties.MaxLength = 50;
            txtTelefono.EnterMoveNextControl = true;
            txtEmail.Properties.MaxLength = 100;
            txtEmail.EnterMoveNextControl = true;
            cboMonedasID.Properties.MaxLength = 3;
            cboMonedasID.EnterMoveNextControl = true;
            dateEditFechaderegistro.Text = DateTime.Now.ToShortDateString();
            txtObservaciones.Properties.MaxLength = 200;
            txtObservaciones.EnterMoveNextControl = true;
            txtClasificacion.Properties.MaxLength = 10;
            txtClasificacion.EnterMoveNextControl = true;
            txtCorrespondenciaA.Properties.MaxLength = 100;
            txtCorrespondenciaA.EnterMoveNextControl = true;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Proveedores";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            ProveedoresCL cl = new ProveedoresCL();
            gridControlPrincipal.DataSource = cl.ProveedoresGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridProveedores";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Ciudades";
            cboCiudadesID.Properties.ValueMember = "Clave";
            cboCiudadesID.Properties.DisplayMember = "Des";
            cboCiudadesID.Properties.DataSource = cl.CargaCombos();
            cboCiudadesID.Properties.ForceInitialize();
            cboCiudadesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCiudadesID.Properties.PopulateColumns();
            cboCiudadesID.Properties.Columns["Clave"].Visible = false;
            cboCiudadesID.Properties.NullText = "Seleccione una ciudad";
            cl.strTabla = "Monedas";
            cboMonedasID.Properties.ValueMember = "Clave";
            cboMonedasID.Properties.DisplayMember = "Des";
            cboMonedasID.Properties.DataSource = cl.CargaCombos();
            cboMonedasID.Properties.ForceInitialize();
            cboMonedasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedasID.Properties.PopulateColumns();
            cboMonedasID.Properties.Columns["Clave"].Visible = false;
            cboMonedasID.Properties.NullText = "Seleccione una moneda";
        }
        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intProveedoresID = 0;
        }

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            cboCiudadesID.EditValue = null;
            txtRfc.Text = string.Empty;
            txtContacto.Text = string.Empty;
            cboTipo.EditValue = null;
            txtPLazo.Text = string.Empty;
            txtCreditoautorizado.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
            cboMonedasID.Text = string.Empty;
            dateEditFechaderegistro.Text = DateTime.Now.ToShortDateString();
            txtTiempodeentrega.Text = string.Empty;
            txtDiasTraslado.Text = string.Empty;
            txtPiva.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            txtClasificacion.Text = string.Empty;
            txtRetiva.Text = string.Empty;
            txtRetisr.Text = string.Empty;
            swEsdeservicio.IsOn = false;
            txtCorrespondenciaA.Text = string.Empty;
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
                ProveedoresCL cl = new ProveedoresCL();
                cl.intProveedoresID = intProveedoresID;
                cl.strNombre = txtNombre.Text;
                cl.strDireccion = txtDireccion.Text;
                cl.intCiudadesID = Convert.ToInt32(cboCiudadesID.EditValue);
                cl.strRfc = txtRfc.Text;
                cl.strContacto = txtContacto.Text;
                cl.intTipo = Convert.ToInt32(cboTipo.SelectedIndex);
                cl.intPLazo = Convert.ToInt32(txtPLazo.Text);
                cl.intCreditoautorizado = Convert.ToDecimal(txtCreditoautorizado.Text);
                cl.strTelefono = txtTelefono.Text;
                cl.strEmail = txtEmail.Text;
                cl.strMonedasID = cboMonedasID.EditValue.ToString();
                cl.fFechaderegistro = Convert.ToDateTime(dateEditFechaderegistro.Text);
                cl.intTiempodeentrega = Convert.ToInt32(txtTiempodeentrega.Text);
                cl.intDiasTraslado = Convert.ToInt32(txtDiasTraslado.Text);
                cl.intPiva = Convert.ToInt32(txtPiva.Text);
                cl.strObservaciones = txtObservaciones.Text;
                cl.strClasificacion = txtClasificacion.Text;
                cl.decRetiva = Convert.ToDecimal(txtRetiva.Text);
                cl.decRetisr = Convert.ToDecimal(txtRetisr.Text);
                cl.intEsdeservicio = swEsdeservicio.IsOn ? 1 : 0;
                cl.strCorrespondenciaA = txtCorrespondenciaA.Text;
                Result = cl.ProveedoresCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intProveedoresID == 0)
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
            if (txtDireccion.Text.Length == 0)
            {
                return "El campo Direccion no puede ir vacio";
            }
            if (cboCiudadesID.EditValue == null)
            {
                return "El campo CiudadesID no puede ir vacio";
            }
            if (txtRfc.Text.Length == 0)
            {
                return "El campo Rfc no puede ir vacio";
            }
            if (txtContacto.Text.Length == 0)
            {
                return "El campo Contacto no puede ir vacio";
            }
            if (cboTipo.EditValue == null)
            {
                return "El campo Tipo no puede ir vacio";
            }
            if (txtPLazo.Text.Length == 0)
            {
                return "El campo PLazo no puede ir vacio";
            }
            if (txtCreditoautorizado.Text.Length == 0)
            {
                return "El campo Creditoautorizado no puede ir vacio";
            }
            if (txtTelefono.Text.Length == 0)
            {
                return "El campo Telefono no puede ir vacio";
            }
            if (txtEmail.Text.Length == 0)
            {
                return "El campo Email no puede ir vacio";
            }
            if (cboMonedasID.EditValue== null)
            {
                return "El campo MonedasID no puede ir vacio";
            }
            if (dateEditFechaderegistro.Text.Length == 0)
            {
                return "El campo Fechaderegistro no puede ir vacio";
            }
            if (txtTiempodeentrega.Text.Length == 0)
            {
                return "El campo Tiempodeentrega no puede ir vacio";
            }
            if (txtDiasTraslado.Text.Length == 0)
            {
                return "El campo DiasTraslado no puede ir vacio";
            }
            if (txtPiva.Text.Length == 0)
            {
                return "El campo Piva no puede ir vacio";
            }
            if (txtObservaciones.Text.Length == 0)
            {
                return "El campo Observaciones no puede ir vacio";
            }
            if (txtClasificacion.Text.Length == 0)
            {
                return "El campo Clasificacion no puede ir vacio";
            }
            if (txtRetiva.Text.Length == 0)
            {
                return "El campo Retiva no puede ir vacio";
            }
            if (txtRetisr.Text.Length == 0)
            {
                return "El campo Retisr no puede ir vacio";
            }
            if (txtCorrespondenciaA.Text.Length == 0)
            {
                return "El campo CorrespondenciaA no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            ProveedoresCL cl = new ProveedoresCL();
            cl.intProveedoresID = intProveedoresID;
            String Result = cl.ProveedoresLlenaCajas();
            if (Result == "OK")
            {
                txtNombre.Text = cl.strNombre;
                txtDireccion.Text = cl.strDireccion;
                cboCiudadesID.EditValue = cl.intCiudadesID;
                txtRfc.Text = cl.strRfc;
                txtContacto.Text = cl.strContacto;
                cboTipo.SelectedIndex = cl.intTipo;
                txtPLazo.EditValue = cl.intPLazo;
                txtCreditoautorizado.Text = cl.intCreditoautorizado.ToString();
                txtTelefono.Text = cl.strTelefono;
                txtEmail.Text = cl.strEmail;
                cboMonedasID.EditValue = cl.strMonedasID;
                dateEditFechaderegistro.Text = cl.fFechaderegistro.ToShortDateString();
                txtTiempodeentrega.Text = cl.intTiempodeentrega.ToString();
                txtDiasTraslado.Text = cl.intDiasTraslado.ToString();
                txtPiva.Text = cl.intPiva.ToString();
                txtObservaciones.Text = cl.strObservaciones;
                txtClasificacion.Text = cl.strClasificacion;
                txtRetiva.Text = cl.decRetiva.ToString();
                txtRetisr.Text = cl.decRetisr.ToString(); 
                swEsdeservicio.IsOn = cl.intEsdeservicio == 1 ? true : false;
                txtCorrespondenciaA.Text = cl.strCorrespondenciaA;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intProveedoresID == 0)
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
            ProveedoresCL cl = new ProveedoresCL();
            cl.intProveedoresID = intProveedoresID;
            String Result = cl.ProveedoresEliminar();
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
            if (intProveedoresID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intProveedoresID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
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
            clg.strGridLayout = "gridProveedores";
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

        private void gridViewPrincipal_RowClick_1(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intProveedoresID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ProveedoresID"));
        }
    }
}
