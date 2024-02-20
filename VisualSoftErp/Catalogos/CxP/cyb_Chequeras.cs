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

    public partial class cyb_Chequeras : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intChequerasID;

        public cyb_Chequeras()
        {
            InitializeComponent();

            txtCuentabancaria.Properties.MaxLength = 20;
            txtSucursal.Properties.MaxLength = 50;
            txtTelefono.Properties.MaxLength = 50;
            txtTitular.Properties.MaxLength = 50;
            cboMonedasID.Properties.MaxLength = 3;
            txtLogotipo.Properties.MaxLength = 50;

            //Esconde los botones eliminar y guardar en el GridP
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Chequeras";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            cyb_ChequerasCL cl = new cyb_ChequerasCL();
            gridControlPrincipal.DataSource = cl.cyb_ChequerasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridcyb_Chequeras";
            clg.restoreLayout(gridViewPrincipal);

            //con esta lina de codigo ponemos los autofiltros
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "cyb_Bancos";
            cboBancosID.Properties.ValueMember = "Clave";
            cboBancosID.Properties.DisplayMember = "Des";
            cboBancosID.Properties.DataSource = cl.CargaCombos();
            cboBancosID.Properties.ForceInitialize();
            cboBancosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboBancosID.Properties.PopulateColumns();
            cboBancosID.Properties.Columns["Clave"].Visible = false;
            cboBancosID.Properties.Columns["Rfc"].Visible = false;
            cboBancosID.Properties.NullText = "Seleccione un banco";
            //cboBancosID.ItemIndex = 0;

            cl.strTabla = "Monedas";
            cboMonedasID.Properties.ValueMember = "Clave";
            cboMonedasID.Properties.DisplayMember = "Des";
            cboMonedasID.Properties.DataSource = cl.CargaCombos();
            cboMonedasID.Properties.ForceInitialize();
            cboMonedasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedasID.Properties.PopulateColumns();
            cboMonedasID.Properties.Columns["Clave"].Visible = false;
            cboMonedasID.ItemIndex = 0;
           
            cl.strTabla = "cyb_Cuentas";
            cboCuentasID.Properties.ValueMember = "Clave";
            cboCuentasID.Properties.DisplayMember = "Des";
            cboCuentasID.Properties.DataSource = cl.CargaCombos();
            cboCuentasID.Properties.ForceInitialize();
            cboCuentasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCuentasID.Properties.PopulateColumns();
            cboCuentasID.Properties.Columns["Clave"].Visible = false;
            cboCuentasID.Properties.NullText = "Seleccione una cuenta";

            cl.strTabla = "cyb_Cuentas";
            cboCuentacontablecomplementaria.Properties.ValueMember = "Clave";
            cboCuentacontablecomplementaria.Properties.DisplayMember = "Des";
            cboCuentacontablecomplementaria.Properties.DataSource = cl.CargaCombos();
            cboCuentacontablecomplementaria.Properties.ForceInitialize();
            cboCuentacontablecomplementaria.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCuentacontablecomplementaria.Properties.PopulateColumns();
            cboCuentacontablecomplementaria.Properties.Columns["Clave"].Visible = false;
            cboCuentacontablecomplementaria.Properties.NullText = "Seleccione una cuenta complementaria";

        }
        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intChequerasID = 0;
        }

        private void LimpiaCajas()
        {
            cboBancosID.EditValue = null;
            txtCuentabancaria.Text = string.Empty;
            txtSucursal.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtTitular.Text = string.Empty;
            cboMonedasID.Text = string.Empty;
            txtChequeinicial.Text = null;
            txtChequefinal.Text = null;
            cboCuentasID.EditValue = null;
            cboCuentacontablecomplementaria.EditValue = null;
            //No usarStatus.EditValue = null;
            txtLogotipo.Text = string.Empty;
            txtComisiontarjeta.Text = null;
            swMasutilizada.IsOn = false;
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
                cyb_ChequerasCL cl = new cyb_ChequerasCL();
                cl.intChequerasID = intChequerasID;
                cl.intBancosID = Convert.ToInt32(cboBancosID.EditValue);
                cl.strCuentabancaria = txtCuentabancaria.Text;
                cl.strSucursal = txtSucursal.Text;
                cl.strTelefono = txtTelefono.Text;
                cl.strTitular = txtTitular.Text;
                cl.strMonedasID=cboMonedasID.EditValue.ToString();
                cl.intChequeinicial = Convert.ToInt32(txtChequeinicial.Text);
                cl.intChequefinal = Convert.ToInt32(txtChequefinal.Text);
                cl.intCuentasID = Convert.ToInt32(cboCuentasID.EditValue);
                cl.intCuentacontablecomplementaria = Convert.ToInt32(cboCuentacontablecomplementaria.EditValue);
                cl.intStatus = 1;
                cl.strLogotipo = txtLogotipo.Text;
                cl.decComisiontarjeta = Convert.ToDecimal(txtComisiontarjeta.Text);
                cl.intMasutilizada = swMasutilizada.IsOn ? 1 : 0;
                Result = cl.cyb_ChequerasCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intChequerasID == 0)
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
            if (cboBancosID.EditValue == null)
            {
                return "El campo BancosID no puede ir vacio";
            }
            if (txtCuentabancaria.Text.Length == 0)
            {
                return "El campo Cuentabancaria no puede ir vacio";
            }
            if (txtSucursal.Text.Length == 0)
            {
                txtSucursal.Text = string.Empty;
            }
            if (txtTelefono.Text.Length == 0)
            {
                txtTelefono.Text = string.Empty;
            }
            if (txtTitular.Text.Length == 0)
            {
                txtTitular.Text = string.Empty;
            }
            if (cboMonedasID.Text.Length == 0)
            {
                return "El campo MonedasID no puede ir vacio";
            }
            if (txtChequeinicial.Text.Length == 0)
            {
                txtChequeinicial.Text = 0.ToString();
            }
            if (txtChequefinal.Text.Length == 0)
            {
                txtChequefinal.Text = 0.ToString();
            }
            //if (txtChequefinal.Text = txtChequeinicial.Text)
            //{
            //    return "El campo MonedasID no puede ir vacio";
            //}

            //if (cboCuentasID.EditValue == null)
            //{
            //    return "El campo CuentasID no puede ir vacio";
            //}
            if (cboCuentacontablecomplementaria.EditValue == null)
            {
                cboCuentacontablecomplementaria.EditValue = 0;
            }
            
            if (txtLogotipo.Text.Length == 0)
            {
                txtLogotipo.Text = string.Empty;
            }
            if (txtComisiontarjeta.Text.Length == 0)
            {
                txtComisiontarjeta.Text = 0.ToString();
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            cyb_ChequerasCL cl = new cyb_ChequerasCL();
            cl.intChequerasID = intChequerasID;
            String Result = cl.cyb_ChequerasLlenaCajas();
            if (Result == "OK")
            {
                cboBancosID.EditValue = cl.intBancosID;
                txtCuentabancaria.Text = cl.strCuentabancaria;
                txtSucursal.Text = cl.strSucursal;
                txtTelefono.Text = cl.strTelefono;
                txtTitular.Text = cl.strTitular;
                cboMonedasID.EditValue = cl.strMonedasID;
                txtChequeinicial.Text = cl.intChequeinicial.ToString();
                txtChequefinal.Text = cl.intChequefinal.ToString();
                cboCuentasID.EditValue = cl.intCuentasID;
                cboCuentacontablecomplementaria.EditValue = cl.intCuentacontablecomplementaria;
                //cboStatus.EditValue = cl.intCuentacontablecomplementaria;
                txtLogotipo.Text = cl.strLogotipo;
                txtComisiontarjeta.Text = cl.decComisiontarjeta.ToString();
                swMasutilizada.IsOn = cl.intMasutilizada == 1 ? true : false;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intChequerasID == 0)
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
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            navigationFrame.SelectedPageIndex = 1;
        }

        private void Eliminar()
        {
            cyb_ChequerasCL cl = new cyb_ChequerasCL();
            cl.intChequerasID = intChequerasID;
            String Result = cl.cyb_ChequerasEliminar();
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

        private void Cancelar()
        {
            cyb_ChequerasCL cl = new cyb_ChequerasCL();
            cl.intChequerasID = intChequerasID;
            String Result = cl.cyb_ChequerasCancelar();
            if (Result == "OK")
            {
                MessageBox.Show("Cancelado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intChequerasID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intChequerasID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
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
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridcyb_Chequeras";
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
            intChequerasID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ChequerasID"));
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intChequerasID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea cancelar el ID " + intChequerasID.ToString(), "Cancelar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "Yes")
                {
                    Cancelar();
                }
            }
        }        
    }
}
