using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Clases;
using VisualSoftErp.Interface.Request;
using VisualSoftErp.Interface.Response;
using VisualSoftErp.Interfaces;
using static VisualSoftErp.Interface.Request.operadoresRequest;
using static VisualSoftErp.Interface.Response.operadoresResponse;

namespace VisualSoftErp.CCP.Catalogos
{
    public partial class Operadores : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intOperadoresID;

        public Operadores()
        {
            InitializeComponent();

            txtRfc.Properties.MaxLength = 14;
            txtRfc.EnterMoveNextControl = true;

            txtNombre.Properties.MaxLength = 150;
            txtNombre.EnterMoveNextControl = true;

            txtNumLicencia.Properties.MaxLength = 16;
            txtNumLicencia.EnterMoveNextControl = true;
            dateEditLicenciaVence.Text = DateTime.Now.ToShortDateString();

            txtTelefono.Properties.MaxLength = 150;
            txtTelefono.EnterMoveNextControl = true;
            CargaCombos();

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Operadores";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            List<operadoresGResponse> resp = cliente.PeticionesWSPost<List<operadoresGResponse>>(url, "Service1.svc/operadoresG", null);

            gridControlPrincipal.DataSource = resp;
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridOperadores";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            operadoresCombosListResponse resp = cliente.PeticionesWSPost<operadoresCombosListResponse>(url, "Service1.svc/operadoresCombos", null);

            cboCarrosID.Properties.ValueMember = "Clave";
            cboCarrosID.Properties.DisplayMember = "Des";
            cboCarrosID.Properties.DataSource = resp.Tractor;
            cboCarrosID.Properties.ForceInitialize();
            cboCarrosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCarrosID.Properties.PopulateColumns();
            cboCarrosID.Properties.Columns["Clave"].Visible = false; 
            cboCarrosID.Properties.Columns["ClaveStr"].Visible = false;
        }


        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intOperadoresID = 0;
        }

        private void LimpiaCajas()
        {
            txtRfc.Text = string.Empty;
            cboCarrosID.EditValue = null;
            txtNombre.Text = string.Empty;
            txtNumLicencia.Text = string.Empty;
            dateEditLicenciaVence.Text = DateTime.Now.ToShortDateString();
            txtTelefono.Text = string.Empty;

        }

        private void bbiGuardar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void Guardar()
        {
            var Request = new operadoresCRUDRequest();

            try
            {
                String Result = Valida();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }

                globalCL clg = new globalCL();

                Request.OperadoresID = intOperadoresID;
                Request.Rfc = txtRfc.Text;
                Request.Nombre = txtNombre.Text;
                Request.Calle = "";
                Request.NoExterior = "";
                Request.NoInterior = "";
                Request.EstadosID = "";
                Request.Pais = "";
                Request.CP = "";
                Request.MunicipiosID = 0;
                Request.LocalidadesID = 0;
                Request.ColoniasID = 0;
                Request.NumLicencia = txtNumLicencia.Text;
                Request.LicenciaVence =  clg.fechaSQL(Convert.ToDateTime(dateEditLicenciaVence.Text));
                Request.Imss = "";
                Request.FechaIngreso = clg.fechaSQL(DateTime.Now);
                Request.FechaBaja = clg.fechaSQL(DateTime.Now);
                Request.Telefono = txtTelefono.Text;
                Request.CuotaIspt = 0;
                Request.CuotaImss = 0;
                Request.Cuotainfonavit = 0;
                Request.Sueldo = 0;
                Request.PorcentajeSueldo = 0;
                Request.Foto = "";
                Request.Activo = 1;
                Request.CarrosID = Convert.ToInt32(cboCarrosID.EditValue);
                Request.Sexo = "";
                Request.EstadoCivilID = 1;
                Request.Curp = "";
                Request.FechaNacimiento = "";
                Request.NumCreditoInfonavit = "";
                Request.NumRegIdTribOperador = "";
                Request.ResidenciaFiscalOperador = "";
                Request.NoSeUsa_clave_operador = 0;
                Request.NoSeUsa_EstadoCivil = "";
                Request.Referencia = "";
                Request.EMail = "";

                MalocClient cliente = new MalocClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
                crudResponse resp = cliente.PeticionesWSPost<crudResponse>(url, "Service1.svc/operadoresCrud", Request);
                if (resp.Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intOperadoresID == 0)
                    {
                        LimpiaCajas();
                    }
                }
                else
                {
                    MessageBox.Show(resp.Result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        } //Guardar

        private string Valida()
        {
            if (txtRfc.Text.Length < 12)
            {
                return "El campo Rfc no puede ser menor a 12 caracteres";
            }
            if (cboCarrosID.EditValue == null)
            {
                return "Tractor no puede ir vacio";
            }
            if (txtNombre.Text.Length < 0)
            {
                return "El campo Nombre no puede ir vacio";
            }          
            if (txtNumLicencia.Text.Length < 6)
            {
                return "El campo NumLicencia no puede ser menor a 6 digítos";
            }     
            if (txtTelefono.Text.Length == 0)
            {
                return "El campo Telefono no puede ir vacio";
            }     
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            llenaCajasRequest request = new llenaCajasRequest();
            request.ID = intOperadoresID;
            operadoresLlenaCajasResponse resp = cliente.PeticionesWSPost<operadoresLlenaCajasResponse>(url, "Service1.svc/operadoresLlenaCajas", request);
            if (resp.OperadoresID > 0)
            { 
                txtNombre.Text = resp.Nombre;
                cboCarrosID.EditValue = resp.CarrosID;
                txtTelefono.Text = resp.Telefono;
                txtRfc.Text = resp.Rfc;
                txtNumLicencia.Text = resp.NumLicencia;
                dateEditLicenciaVence.EditValue = resp.LicenciaVence.Substring(0,10);
            

            }
            else
            {
                MessageBox.Show("LlenaCajas: Error");
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intOperadoresID == 0)
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
            MalocClient cliente = new MalocClient();
            string url = "http://visualsoft.com.mx:180/"; /*"http://localhost:57090/";*/

            llenaCajasRequest request = new llenaCajasRequest();
            request.ID = intOperadoresID;
            crudResponse resp = cliente.PeticionesWSPost<crudResponse>(url, "Service1.svc/operadoresEliminar", request);
            if (resp.Result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show("Eliminar: " + resp.Result);
            }
        }

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intOperadoresID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intOperadoresID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
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
            clg.strGridLayout = "gridOperadores";
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
            intOperadoresID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "OperadoresID"));
        }

    }
}