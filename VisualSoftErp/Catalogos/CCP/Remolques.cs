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
using static VisualSoftErp.Interface.Request.remolquesRequest;
using static VisualSoftErp.Interface.Response.remolquesResponse;

namespace VisualSoftErp.CCP.Catalogos
{
    public partial class Remolques : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intRemolquesID;
        public Remolques()
        {
            InitializeComponent();

            txtDescripcion.Properties.MaxLength = 50;
            txtDescripcion.EnterMoveNextControl = true;
            txtModelo.Properties.MaxLength = 10;
            txtModelo.EnterMoveNextControl = true;
            //txtSubTipoRem.Properties.MaxLength = 6;
            //txtSubTipoRem.EnterMoveNextControl = true;
            txtPlaca.Properties.MaxLength = 12;
            txtPlaca.EnterMoveNextControl = true;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Remolques";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            List<remolquesGRIDResponse> resp = cliente.PeticionesWSPost<List<remolquesGRIDResponse>>(url, "Service1.svc/remolquesGrid", null);

            gridControlPrincipal.DataSource = resp;
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridRemolques";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            remolquesCombosListResponse resp = cliente.PeticionesWSPost<remolquesCombosListResponse>(url, "Service1.svc/CombosRemolques", null);

            cboMarcas.Properties.ValueMember = "Clave";
            cboMarcas.Properties.DisplayMember = "Des";
            cboMarcas.Properties.DataSource = resp.Marcas;
            cboMarcas.Properties.ForceInitialize();
            cboMarcas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMarcas.Properties.PopulateColumns();
            cboMarcas.Properties.Columns["Clave"].Visible = false;

            cboSubtipoRem.Properties.ValueMember = "Clave";
            cboSubtipoRem.Properties.DisplayMember = "Des";
            cboSubtipoRem.Properties.DataSource = resp.SubtiposRemolques;
            cboSubtipoRem.Properties.ForceInitialize();
            cboSubtipoRem.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSubtipoRem.Properties.PopulateColumns();
            cboSubtipoRem.Properties.Columns["Clave"].Visible = false;
        }
        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intRemolquesID = 0;
        }

        private void LimpiaCajas()
        {
            txtDescripcion.Text = string.Empty;
            txtNumEco.Text = string.Empty;
            cboMarcas.EditValue = null;
            txtModelo.Text = string.Empty;
            cboSubtipoRem.EditValue = null;
            txtPlaca.Text = string.Empty;
        }

        private void bbiGuardar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void Guardar()
        {
            var Request = new remolquesCRUDRequest();
            try
            {
                String Result = Valida();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }
                Request.RemolquesID = intRemolquesID;
                Request.Descripcion = txtDescripcion.Text;
                Request.MarcasID = Convert.ToInt32(cboMarcas.EditValue);
                Request.Modelo = Convert.ToInt32(txtModelo.Text);
                Request.NumEco = txtNumEco.Text;
                Request.SubTipoRem=cboSubtipoRem.EditValue.ToString();
                Request.Placa = txtPlaca.Text;
                MalocClient cliente = new MalocClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
                crudResponse resp = cliente.PeticionesWSPost<crudResponse>(url, "Service1.svc/remolquesCrud", Request);
                if (resp.Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intRemolquesID == 0)
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
            if (txtDescripcion.Text.Length == 0)
            {
                return "El campo Descripcion no puede ir vacio";
            }
            if (cboMarcas.EditValue == null)
            {
                return "El campo MarcasID no puede ir vacio";
            }
            if (txtModelo.Text.Length == 0)
            {
                return "El campo Modelo no puede ir vacio";
            }
            if (txtModelo.Text.Length == 0)
            {
                return "El campo NumEco no puede ir vacio";
            }
            if (cboSubtipoRem.EditValue == null)
            {
                return "El campo SubTipoRem no puede ir vacio";
            }
            if (txtPlaca.Text.Length == 0)
            {
                return "El campo Placa no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            llenaCajasRequest request = new llenaCajasRequest();
            request.ID = intRemolquesID;
            remolquesLlenaCajasResponse resp = cliente.PeticionesWSPost<remolquesLlenaCajasResponse>(url, "Service1.svc/remolquesLlenaCajas", request);
            if (resp.RemolquesID > 0)
            {
                txtDescripcion.Text = resp.Descripcion;
                txtModelo.Text = resp.Modelo.ToString();
                txtNumEco.Text = resp.NumEco.ToString();
                cboSubtipoRem.EditValue = resp.SubTipoRem.ToString();
                cboMarcas.EditValue = resp.MarcasID.ToString();
                txtPlaca.Text = resp.Placa;
            }
            else
            {
                MessageBox.Show("LlenaCajas: Error");
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intRemolquesID == 0)
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
            request.ID = intRemolquesID;
            crudResponse resp = cliente.PeticionesWSPost<crudResponse>(url, "Service1.svc/remolquesEliminar", request);
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
            if (intRemolquesID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intRemolquesID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
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
            clg.strGridLayout = "gridRemolques";
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
            intRemolquesID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "RemolquesID"));
        }
    }
}