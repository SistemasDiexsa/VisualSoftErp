using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
using VisualSoftErp.Response;
//using VisualSoftErp.Models.Cfdi;


namespace VisualSoftErp.CCP.Catalogos
{
    public partial class Domicilios : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intDomicilioID, intColID;
        string strnumext, strnumint;
        public Domicilios()
        {
            InitializeComponent();

            txtCalle.Properties.MaxLength = 100;
            txtCalle.EnterMoveNextControl = true;
            txtNumeroExterior.Properties.MaxLength = 10;
            txtNumeroExterior.EnterMoveNextControl = true;
            txtNumeroExterior.Properties.MaxLength = 10;
            txtNumeroExterior.EnterMoveNextControl = true;
            txtReferencia.Properties.MaxLength = 100;
            txtReferencia.EnterMoveNextControl = true;
            cboEstados.Properties.MaxLength = 3;
            cboEstados.EnterMoveNextControl = true;
            cboPais.Properties.MaxLength = 3;
            cboPais.EnterMoveNextControl = true;
            txtCodigoPostal.Properties.MaxLength = 12;
            txtCodigoPostal.EnterMoveNextControl = true;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Domicilios";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            List<domiciliosGridResponse> resp = cliente.PeticionesWSPost<List<domiciliosGridResponse>>(url, "Service1.svc/DomiciliosGrid", null);

            gridControlPrincipal.DataSource = resp;
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridDomicilios";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargaCombos()
        {

            comboResponse model = new comboResponse();

            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            domiciliosCombosResponse resp = cliente.PeticionesWSPost<domiciliosCombosResponse>(url, "Service1.svc/domiciliosCombos", null);

            cboPais.Properties.ValueMember = "ClaveStr";
            cboPais.Properties.DisplayMember = "Des";
            cboPais.Properties.DataSource = resp.ListadePaises.Paises;
            cboPais.Properties.ForceInitialize();
            cboPais.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboPais.Properties.PopulateColumns();
            cboPais.Properties.Columns["ClaveStr"].Visible = false;

            cboLocalidadesID.Properties.ValueMember = "Clave";
            cboLocalidadesID.Properties.DisplayMember = "Des";
            cboLocalidadesID.Properties.DataSource = resp.ListadeLocalidades.Localidades;
            cboLocalidadesID.Properties.ForceInitialize();
            cboLocalidadesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboLocalidadesID.Properties.PopulateColumns();
            cboLocalidadesID.Properties.Columns["ClaveStr"].Visible = false;


            cboMunicipiosID.Properties.ValueMember = "Clave";
            cboMunicipiosID.Properties.DisplayMember = "Des";
            cboMunicipiosID.Properties.DataSource = resp.ListadeMunicipios.Municipios;
            cboMunicipiosID.Properties.ForceInitialize();
            cboMunicipiosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMunicipiosID.Properties.PopulateColumns();
            cboMunicipiosID.Properties.Columns["Clave"].Visible = false;



            cboEstados.Properties.ValueMember = "ClaveStr";
            cboEstados.Properties.DisplayMember = "Des";
            cboEstados.Properties.DataSource = resp.ListadeEstados.Estados;
            cboEstados.Properties.ForceInitialize();
            cboEstados.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboEstados.Properties.PopulateColumns();
            cboEstados.Properties.Columns["ClaveStr"].Visible = false;



            cboClientesID.Properties.ValueMember = "Clave";
            cboClientesID.Properties.DisplayMember = "Des";
            cboClientesID.Properties.DataSource = resp.Listadeclientes.Clientes;
            cboClientesID.Properties.ForceInitialize();
            cboClientesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboClientesID.Properties.PopulateColumns();
            cboClientesID.Properties.Columns["Clave"].Visible = false;


        }
        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intDomicilioID = 0;
        }

        private void LimpiaCajas()
        {
            txtCalle.Text = string.Empty;
            txtNumeroExterior.Text = string.Empty;
            txtNumeroExterior.Text = string.Empty;
            cboColoniasID.EditValue = null;
            cboLocalidadesID.EditValue = null;
            txtReferencia.Text = string.Empty;
            cboMunicipiosID.EditValue = null;
            cboEstados.EditValue = null;
            cboPais.EditValue = "MEX";
            
            cboClientesID.EditValue = null;
        }

        private void bbiGuardar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void Guardar()
        {
            var Request = new domiciliosCrudRequest();
            try
            {
                String Result = Valida();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }
                Request.DomicilioID = intDomicilioID;
                Request.Calle = txtCalle.Text;
                Request.NumeroExterior = strnumext;
                Request.NumeroInterior = strnumint;
                Request.Colonia = intColID;
                Request.Localidad = Convert.ToInt32(cboLocalidadesID.EditValue);
                Request.Referencia = txtReferencia.Text;
                Request.Municipio = Convert.ToInt32(cboMunicipiosID.EditValue);
                Request.Estado = cboEstados.EditValue.ToString();
                Request.Pais = cboPais.EditValue.ToString();
                Request.CodigoPostal = txtCodigoPostal.Text;
                Request.ClientesID = Convert.ToInt32(cboClientesID.EditValue);
                MalocClient cliente = new MalocClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
                crudResponse resp = cliente.PeticionesWSPost<crudResponse>(url, "Service1.svc/domiciliosCrud", Request);
                if (resp.Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intDomicilioID == 0)
                    {
                        LimpiaCajas();
                    }
                }
                else { MessageBox.Show(Result); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        } //Guardar

        private string Valida()
        {
            if (txtCalle.Text.Length == 0)
            {
                return "El campo Calle no puede ir vacio";
            }

            if (txtNumeroExterior.Text.Length == 0)
            {
                strnumext = "S/N";
            }
            else 
            { strnumext = txtNumeroExterior.Text; }

            if (txtNumeroExterior.Text.Length == 0)
            {
                strnumint = "";
            }
            else 
            { strnumint = txtNumeroInterior.Text; }

            if (cboColoniasID.EditValue == null)
            {
                intColID = 0;
            }
            else 
            { intColID = Convert.ToInt32(cboColoniasID.EditValue); }

            
            
            if (cboMunicipiosID.EditValue == null)
            {
                return "El campo MunicipiosID no puede ir vacio";
            }
            if (cboEstados.Text.Length == 0)
            {
                return "El campo Estado no puede ir vacio";
            }
            if (cboPais.Text.Length == 0)
            {
                return "El campo Pais no puede ir vacio";
            }
            if (txtCodigoPostal.Text.Length == 0)
            {
                return "El campo CodigoPostal no puede ir vacio";
            }
            if (cboClientesID.EditValue == null)
            {
                return "El campo ClientesID no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            MalocClient cliente = new MalocClient();
            string url =  System.Configuration.ConfigurationManager.AppSettings["wsurl"]; 
            llenaCajasRequest request = new llenaCajasRequest();
            request.ID = intDomicilioID;
            domiciliosLlenaCajasResponse resp = cliente.PeticionesWSPost<domiciliosLlenaCajasResponse>(url, "Service1.svc/DomiciliosLlenaCajas", request);

            if (resp.DomicilioID > 0)
            {
                txtCodigoPostal.Text = resp.CodigoPostal;
                CargarDatos(resp.CodigoPostal);
                txtCalle.Text = resp.Calle;
                txtNumeroInterior.Text = resp.NumeroInterior;
                txtNumeroExterior.Text = resp.NumeroExterior;
                cboColoniasID.EditValue = resp.Colonia;
                cboLocalidadesID.EditValue = resp.Localidad;
                txtReferencia.Text = resp.Referencia;
                cboMunicipiosID.EditValue = resp.Municipio;
                cboEstados.EditValue = resp.Estado;
                cboPais.Text = resp.Calle;
                cboClientesID.EditValue = resp.ClientesID;
            }
            else
            {
                MessageBox.Show("LlenaCajas: Error");
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intDomicilioID == 0)
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
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];

            llenaCajasRequest request = new llenaCajasRequest();
            request.ID = intDomicilioID;
            crudResponse resp = cliente.PeticionesWSPost<crudResponse>(url, "Service1.svc/domiciliosEliminar", request);
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
            if (intDomicilioID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intDomicilioID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
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
            clg.strGridLayout = "gridDomicilios";
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
            intDomicilioID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "DomicilioID"));
        }

        private void bbiDatos_Click(object sender, EventArgs e)
        {
            string cp = txtCodigoPostal.Text;

            if (cp.Length > 0)
            {
                CargarDatos(cp);
            }
            else
            {
                MessageBox.Show("Ingrese CP");
            }
        }

        private void CargarDatos(string CP) 
        {
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            coloniasComboRequest request = new coloniasComboRequest();
            request.CP = CP;
            coloniasComboResponse resp = cliente.PeticionesWSPost<coloniasComboResponse>(url, "Service1.svc/coloniasCombo", request);

            cboColoniasID.Properties.DataSource = null;
            cboColoniasID.Properties.ValueMember = "ClaveStr";
            cboColoniasID.Properties.DisplayMember = "Des";
            cboColoniasID.Properties.DataSource = resp.Colonias;
            cboColoniasID.Properties.ForceInitialize();
            cboColoniasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboColoniasID.Properties.PopulateColumns();
            cboColoniasID.Properties.Columns["Clave"].Visible = false;

            cboEstados.EditValue = resp.Estado;
            cboMunicipiosID.EditValue = resp.MunicipioID;
            cboLocalidadesID.EditValue = resp.LocalidadesID;
        }
    }
}