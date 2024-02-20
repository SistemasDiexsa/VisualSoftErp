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

    public partial class Tipodemovimientocxp : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intTiposdemovimientocxpID;

        public Tipodemovimientocxp()
        {
            InitializeComponent();

            txtNombre.Properties.MaxLength = 50;
            txtNombre.EnterMoveNextControl = true;


            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Tipodemovimientocxp";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            TipodemovimientocxpCL cl = new TipodemovimientocxpCL();
            gridControlPrincipal.DataSource = cl.TipodemovimientocxpGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridTipodemovimientocxp";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargaCombos()
        {

            List<tipoCL> tipoL = new List<tipoCL>();

            tipoL.Add(new tipoCL() { Clave = "null", Des = "Seleccione tipo de movimiento" });
            tipoL.Add(new tipoCL() { Clave = "C", Des = "Cargo" });
            tipoL.Add(new tipoCL() { Clave = "A", Des = "Abono" });


            cboTipo.Properties.ValueMember = "Clave";
            cboTipo.Properties.DisplayMember = "Des";
            cboTipo.Properties.DataSource = tipoL;
            cboTipo.Properties.ForceInitialize();
            cboTipo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTipo.Properties.PopulateColumns();
            cboTipo.Properties.Columns["Clave"].Visible = false;
           
        }
        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intTiposdemovimientocxpID = 0;
        }

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            cboTipo.ItemIndex = 0;
            swReservado.IsOn = false;
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
                TipodemovimientocxpCL cl = new TipodemovimientocxpCL();
                cl.intTiposdemovimientocxpID = intTiposdemovimientocxpID;
                cl.strNombre = txtNombre.Text;
                cl.strTipo = cboTipo.EditValue.ToString();
                cl.intReservado = swReservado.IsOn ? 1 : 0;
                Result = cl.TipodemovimientocxpCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intTiposdemovimientocxpID == 0)
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
            if (cboTipo.EditValue == null)
            {
                return "El campo Tipo no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            TipodemovimientocxpCL cl = new TipodemovimientocxpCL();
            cl.intTiposdemovimientocxpID = intTiposdemovimientocxpID;
            String Result = cl.TipodemovimientocxpLlenaCajas();
            if (Result == "OK")
            {
                txtNombre.Text = cl.strNombre;
                cboTipo.EditValue = cl.strTipo;
                swReservado.IsOn = cl.intReservado == 1 ? true : false;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intTiposdemovimientocxpID == 0)
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

        public class tipoCL
        {
            public string Clave { get; set; }
            public string Des { get; set; }
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
            TipodemovimientocxpCL cl = new TipodemovimientocxpCL();
            cl.intTiposdemovimientocxpID = intTiposdemovimientocxpID;
            String Result = cl.TipodemovimientocxpEliminar();
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
            if (intTiposdemovimientocxpID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intTiposdemovimientocxpID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
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
            clg.strGridLayout = "gridTipodemovimientocxp";
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
            intTiposdemovimientocxpID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "TiposdemovimientocxpID"));
        }

    }
}
