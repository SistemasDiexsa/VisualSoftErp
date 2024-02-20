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

namespace VisualSoftErp.CCP.Catalogos
{
    public partial class Carros : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intCarrosID;
        public Carros()
        {
            InitializeComponent();
            txtNumeroEconomico.Properties.MaxLength = 50;
            txtNumeroEconomico.EnterMoveNextControl = true;

            txtPermSCT.Properties.MaxLength = 6;
            txtPermSCT.EnterMoveNextControl = true;

            txtNumPolizaSeguro.Properties.MaxLength = 30;
            txtNumPolizaSeguro.EnterMoveNextControl = true;           
            
            txtClaveTransporte.Properties.MaxLength = 10;
            txtClaveTransporte.EnterMoveNextControl = true; 

            txtNumPermisoSCT.Properties.MaxLength = 50;
            txtNumPermisoSCT.EnterMoveNextControl = true;

            txtPlacaVM.Properties.MaxLength = 12;
            txtPlacaVM.EnterMoveNextControl = true;

            txtNumPolizaSeguro.Properties.MaxLength = 30;
            txtNumPolizaSeguro.EnterMoveNextControl = true;
            txtConfigVehicularID.Properties.MaxLength = 10;
            txtConfigVehicularID.EnterMoveNextControl = true;
            txtPlacaVM.Properties.MaxLength = 12;
            txtPlacaVM.EnterMoveNextControl = true;
            txtClaveTransporte.Properties.MaxLength = 10;
            txtClaveTransporte.EnterMoveNextControl = true;
    
            txtSeguroInciso.Properties.MaxLength = 50;
            txtSeguroInciso.EnterMoveNextControl = true;
           
            txtTcirculacionVence.Text = DateTime.Now.ToShortDateString();
          
            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Carros";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            List<carrosGRIDResponse> resp = cliente.PeticionesWSPost<List<carrosGRIDResponse>>(url, "Service1.svc/carrosGrid", null);

            gridControlPrincipal.DataSource = resp;
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCarros";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            carrosCombosListResponse resp = cliente.PeticionesWSPost<carrosCombosListResponse>(url, "Service1.svc/CombosCarros", null);

            txtConfigVehicularID.Properties.ValueMember = "Clave";
             txtConfigVehicularID.Properties.DisplayMember = "Des";
            txtConfigVehicularID.Properties.DataSource = resp.c_ConfigAutotransporte;
            txtConfigVehicularID.Properties.ForceInitialize();
            txtConfigVehicularID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            txtConfigVehicularID.Properties.PopulateColumns();
            txtConfigVehicularID.Properties.Columns["Clave"].Visible = false;

            cboAseguradorasID.Properties.ValueMember = "Clave";
            cboAseguradorasID.Properties.DisplayMember = "Des";
            cboAseguradorasID.Properties.DataSource = resp.Aseguradoras;
            cboAseguradorasID.Properties.ForceInitialize();
            cboAseguradorasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAseguradorasID.Properties.PopulateColumns();
            cboAseguradorasID.Properties.Columns["Clave"].Visible = false;

            cbomARCA.Properties.ValueMember = "Clave";
            cbomARCA.Properties.DisplayMember = "Des";
            cbomARCA.Properties.DataSource = resp.Marcas;
            cbomARCA.Properties.ForceInitialize();
            cbomARCA.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cbomARCA.Properties.PopulateColumns();
            cbomARCA.Properties.Columns["Clave"].Visible = false;
        }
        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intCarrosID = 0;
        }

        private void LimpiaCajas()
        {
            txtNumeroEconomico.Text = string.Empty;
            //dateEditFechaAlta.Text = DateTime.Now.ToShortDateString();
            //dateEditFechaBaja.Text = DateTime.Now.ToShortDateString();
            txtActivo.IsOn = true;
            txtConfigVehicularID.EditValue = null;
            txtNumPermisoSCT.Text = string.Empty;
            cboAseguradorasID.EditValue = null;
            txtNumPolizaSeguro.Text = string.Empty;
            txtConfigVehicularID.Text = string.Empty;
            txtPlacaVM.Text = string.Empty;
            txtAnioModeloVM.Text = string.Empty;
            txtClaveTransporte.Text = string.Empty;
            txtPermSCT.Text = string.Empty;
            //txtPropietariosID = 0;
            //txtSerie.Text = string.Empty;
            //txtRfv.Text = string.Empty;
            //txtMotor.Text = string.Empty;
            //txtMotivoBaja.Text = string.Empty;
            txtSeguroVence.Text = DateTime.Now.ToShortDateString();
            txtTcirculacionVence.Text = DateTime.Now.ToShortDateString();
            txtSeguroInciso.Text = string.Empty;
            //dateEditSeguroVence.Text = DateTime.Now.ToShortDateString();
            //txtSeguro_imagen.Text = string.Empty;
            //dateEditVmecanicaVence.Text = DateTime.Now.ToShortDateString();
            //txtTcirculacionImagen.Text = string.Empty;
            //txtTcirculacionVence.Text = DateTime.Now.ToShortDateString();
            //dateEditVhumosVence.Text = DateTime.Now.ToShortDateString();
            //txtVhumos_imagen.Text = string.Empty;
            //txtNoSeUsarpropietario.Text = string.Empty;
            //txtNombreAseg.Text = string.Empty;
            cbomARCA.EditValue = null;
        }

        private void bbiGuardar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void Guardar()
        {
            var Request = new carrosCRUDRequest();
            try
            {
                String Result = Valida();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }

                globalCL cl = new globalCL();

                Request.CarrosID = intCarrosID;
                Request.NumeroEconomico = Convert.ToInt32(txtNumeroEconomico.Text);
                Request.FechaAlta = cl.fechaSQL(DateTime.Now);
                Request.FechaBaja = cl.fechaSQL(DateTime.Now);
                Request.Activo = txtActivo.IsOn ? 1 : 0;
                Request.MarcasID = Convert.ToInt32(cbomARCA.EditValue);
                Request.NumPermisoSCT = txtNumPermisoSCT.Text;
                Request.AseguradorasID = Convert.ToInt32(cboAseguradorasID.EditValue);
                Request.NumPolizaSeguro = txtNumPolizaSeguro.Text;
                Request.ConfigVehicularID = txtConfigVehicularID.EditValue.ToString();
                Request.PlacaVM = txtPlacaVM.Text;
                Request.AnioModeloVM = Convert.ToInt32(txtAnioModeloVM.Text);
                Request.ClaveTransporte = txtClaveTransporte.Text;
                Request.PermSCT = txtPermSCT.Text;
                Request.PropietariosID = 0;
                Request.Serie = "";
                Request.Rfv = "";
                Request.Motor = "";
                Request.MotivoBaja = "";
                Request.SeguroInciso = "";
                Request.SeguroVence = cl.fechaSQL(Convert.ToDateTime(txtSeguroVence.Text));
                Request.Seguro_imagen = "";
                Request.VmecanicaVence = cl.fechaSQL(DateTime.Now);
                Request.TcirculacionImagen = "";
                Request.TcirculacionVence = cl.fechaSQL(DateTime.Now);
                Request.VhumosVence = cl.fechaSQL(DateTime.Now);
                Request.Vhumos_imagen = "";
                Request.NoSeUsarpropietario = "";
                MalocClient cliente = new MalocClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
                crudResponse resp = cliente.PeticionesWSPost<crudResponse>(url, "Service1.svc/carrosCrud", Request);
                if (resp.Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intCarrosID == 0)
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
            if (txtNumeroEconomico.Text.Length == 0)
            {
                return "El campo NumeroEconomico no puede ir vacio";
            }           
            if (txtConfigVehicularID.EditValue == null)
            {
                return "El campo MarcasID no puede ir vacio";
            }
            if (txtNumPermisoSCT.Text.Length == 0)
            {
                return "El campo NumPermisoSCT no puede ir vacio";
            }
            if (cboAseguradorasID.EditValue == null)
            {
                return "El campo AseguradorasID no puede ir vacio";
            }
            if (txtNumPolizaSeguro.Text.Length == 0)
            {
                return "El campo NumPolizaSeguro no puede ir vacio";
            }
            if (txtConfigVehicularID.Text.Length == 0)
            {
                return "El campo ConfigVehicularID no puede ir vacio";
            }
            if (txtPlacaVM.Text.Length == 0)
            {
                return "El campo PlacaVM no puede ir vacio";
            }
            if (txtClaveTransporte.Text.Length == 0)
            {
                return "El campo ClaveTransporte no puede ir vacio";
            }
            if (txtPermSCT.Text.Length == 0)
            {
                return "El campo PermSCT no puede ir vacio";
            }
            if (txtTcirculacionVence.Text.Length == 0)
            {
                return "El campo TcirculacionVence no puede ir vacio";
            }
            if (txtAnioModeloVM.Text.Length < 4)
                return "El modelo no puede ser menor a dos dígitos";


            return "OK";
        } //Valida

        private void llenaCajas()
        {
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            llenaCajasRequest request = new llenaCajasRequest();
            request.ID = intCarrosID;
            carrosLlenaCajasResponse resp = cliente.PeticionesWSPost<carrosLlenaCajasResponse>(url, "Service1.svc/CarrosLlenaCajas", request);

            if (resp.CarrosID > 0)
            {
                txtNumeroEconomico.Text = resp.NumeroEconomico.ToString();

                txtConfigVehicularID.EditValue = resp.ConfigVehicularID;
                txtNumPermisoSCT.Text = resp.NumPermisoSCT;
                cboAseguradorasID.EditValue = resp.AseguradorasID;
                txtNumPolizaSeguro.Text = resp.NumPolizaSeguro;
                txtConfigVehicularID.Text = resp.ConfigVehicularID;
                txtPlacaVM.Text = resp.PlacaVM;
                txtAnioModeloVM.Text = resp.AnioModeloVM.ToString();
                txtClaveTransporte.Text = resp.ClaveTransporte;
                txtPermSCT.Text = resp.PermSCT;
              
                txtSeguroVence.Text = resp.SeguroVence;
               
                txtTcirculacionVence.Text = resp.TcirculacionVence;
               
                cbomARCA.EditValue = resp.MarcasID;
            }
            else
            {
                MessageBox.Show("LlenaCajas: Error");
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intCarrosID == 0)
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
            request.ID = intCarrosID;
            crudResponse resp = cliente.PeticionesWSPost<crudResponse>(url, "Service1.svc/carrosEliminar", request);

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
            if (intCarrosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intCarrosID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
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
            clg.strGridLayout = "gridCarros";
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
            intCarrosID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "CarrosID"));
        }

        private void labelControl14_Click(object sender, EventArgs e)
        {

        }

        private void txtSeguroVence_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}