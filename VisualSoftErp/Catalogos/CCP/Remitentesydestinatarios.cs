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
    public partial class Remitentesydestinatarios : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intRemitentesydestinatariosID;
        public Remitentesydestinatarios()
        {
            InitializeComponent();

            txtRfc.Properties.MaxLength = 14;
            txtRfc.EnterMoveNextControl = true;
            txtNombre.Properties.MaxLength = 150;
            txtNombre.EnterMoveNextControl = true;
            txtNumRegIdTrib.Properties.MaxLength = 40;
            txtNumRegIdTrib.EnterMoveNextControl = true;
            txtResidenciaFiscal.Properties.MaxLength = 3;
            txtResidenciaFiscal.EnterMoveNextControl = true;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
            gridViewPrincipal.ViewCaption = "Catálogo de RemitentesyDestinatarios";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            try
            {
                MalocClient cliente = new MalocClient();
                string url =System.Configuration.ConfigurationManager.AppSettings["wsurl"];
                List<remitentesyDestinatariosGRIDResponse> resp = cliente.PeticionesWSPost<List<remitentesyDestinatariosGRIDResponse>>(url, "Service1.svc/remitentesyDestinatariosGrid", null);

                gridControlPrincipal.DataSource = resp;
                //Global, manda el nombre del grid para la clase Global
                globalCL clg = new globalCL();
                clg.strGridLayout = "gridRemitentesyDestinatarios";
                clg.restoreLayout(gridViewPrincipal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("LlenaGrid: " + ex.Message);
                gridControlPrincipal.DataSource = null;
            }
        } //LlenarGrid()

        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intRemitentesydestinatariosID = 0;
        }

        private void LimpiaCajas()
        {
            txtRfc.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtNumRegIdTrib.Text = string.Empty;
            txtResidenciaFiscal.Text = string.Empty;
            swActivo.IsOn = true;
        }

        private void bbiGuardar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void Guardar()
        {
            var Request = new remitentesyDestinatariosCRUDRequest();
            try
            {
                String Result = Valida();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }
                Request.RemitentesydestinatariosID = intRemitentesydestinatariosID;
                Request.Rfc = txtRfc.Text;
                Request.Nombre = txtNombre.Text;
                Request.NumRegIdTrib = txtNumRegIdTrib.Text;
                Request.ResidenciaFiscal = txtResidenciaFiscal.Text;
                Request.Activo = swActivo.IsOn ? 1 : 0;
                Request.Recoger = "";
                MalocClient cliente = new MalocClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
                crudResponse resp = cliente.PeticionesWSPost<crudResponse>(url, "Service1.svc/remitentesyDestinatariosCrud", Request);
                if (resp.Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intRemitentesydestinatariosID == 0)
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
            if (txtRfc.Text.Length == 0)
            {
                return "El campo Rfc no puede ir vacio";
            }
            if (txtNombre.Text.Length == 0)
            {
                return "El campo Nombre no puede ir vacio";
            }
        
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];

            llenaCajasRequest request = new llenaCajasRequest();
            request.ID = intRemitentesydestinatariosID;
            remitentesyDestinatariosLlenaCajasResponse resp = cliente.PeticionesWSPost<remitentesyDestinatariosLlenaCajasResponse>(url, "Service1.svc/remitentesyDestinatariosLlenaCajas", request);

            if (resp.RemitentesydestinatariosID > 0)
            {
                txtRfc.Text = resp.Rfc;
                txtNombre.Text = resp.Nombre;
                txtNumRegIdTrib.Text = resp.NumRegIdTrib;
                txtResidenciaFiscal.Text = resp.ResidenciaFiscal;
                swActivo.IsOn = resp.Activo == 1 ? true : false;
            }
            else
            {
                MessageBox.Show("LlenaCajas: Error");
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intRemitentesydestinatariosID == 0)
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
            request.ID = intRemitentesydestinatariosID;
            crudResponse resp = cliente.PeticionesWSPost<crudResponse>(url, "Service1.svc/remitentesyDestinatariosEliminar", request);
            if (resp.Result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show("Eliminar: "+resp.Result);
            }
        }

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intRemitentesydestinatariosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intRemitentesydestinatariosID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
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
            clg.strGridLayout = "gridRemitentesyDestinatarios";
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
            intRemitentesydestinatariosID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "RemitentesydestinatariosID"));
        }
    }
}