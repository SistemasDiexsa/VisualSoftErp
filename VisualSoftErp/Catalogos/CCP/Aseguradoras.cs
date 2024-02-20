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
    public partial class Aseguradoras : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intAseguradorasID;
        public Aseguradoras()
        {
            InitializeComponent();

            txtNombre.Properties.MaxLength = 100;
            txtNombre.EnterMoveNextControl = true;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Aseguradoras";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            MalocClient cliente = new MalocClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
            List<aseguradorasGRIDResponse> resp = cliente.PeticionesWSPost<List<aseguradorasGRIDResponse>>(url, "Service1.svc/AseguradorasGrid", null);

            gridControlPrincipal.DataSource = resp;
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAseguradoras";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            intAseguradorasID = 0;
            BotonesEdicion();
        }

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
        }

        private void bbiGuardar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void Guardar()
        {
            try
            {
                var Request = new aseguradorasCRUDRequest();
                String Result = Valida();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }
                Request.AseguradorasID = intAseguradorasID;
                Request.Nombre = txtNombre.Text;

                MalocClient cliente = new MalocClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["wsurl"];
                crudResponse resp = cliente.PeticionesWSPost<crudResponse>(url, "Service1.svc/AseguradorasCrud", Request);
                if (resp.Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intAseguradorasID == 0)
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
            if (txtNombre.Text.Length == 0)
            {
                return "El campo Nombre no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            MalocClient cliente = new MalocClient();
            string url = "http://visualsoft.com.mx:180/";

            llenaCajasRequest request = new llenaCajasRequest();
            request.ID = intAseguradorasID;
            aseguradorasLlenaCajasResponse resp = cliente.PeticionesWSPost<aseguradorasLlenaCajasResponse>(url, "Service1.svc/aseguradorasLlenaCajas", request);

            if (resp.AseguradorasID > 0)
            {
                txtNombre.Text = resp.Nombre;
            }
            else
            {
                MessageBox.Show("LlenaCajas: Error");
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(intAseguradorasID==0)
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
            string url = "http://visualsoft.com.mx:180/";

            llenaCajasRequest request = new llenaCajasRequest();
            request.ID = intAseguradorasID;
            crudResponse resp = cliente.PeticionesWSPost<crudResponse>(url, "Service1.svc/AseguradorasEliminar", request);
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
            if(intAseguradorasID==0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intAseguradorasID.ToString(), "Elimnar", MessageBoxButtons.YesNo);

                if (Result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intAseguradorasID = 0;
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
            clg.strGridLayout = "gridAseguradoras";
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
            intAseguradorasID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "AseguradorasID"));
        }

        private void txtNombre_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl11_Click(object sender, EventArgs e)
        {

        }
    }
}