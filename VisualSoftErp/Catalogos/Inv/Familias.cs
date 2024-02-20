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
    public partial class Familias : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intFamilias;
        int intLineaID;
        public Familias()
        {
            InitializeComponent();
            navigationFrame.SelectedPageIndex = 0;
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Familias";

            LlenarGridFamilias();

            //inicializar un combo
            combosCL cl = new combosCL();
            //valor
            cboLineasID.Properties.ValueMember = "Clave";
            //lo que se muestra al usuario
            cboLineasID.Properties.DisplayMember = "Des";
            cl.strTabla = "Lineas";
            cboLineasID.Properties.DataSource = cl.CargaCombos();
            cboLineasID.Properties.ForceInitialize();
            //filtar combo
            cboLineasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            //Formato a combo 
            cboLineasID.Properties.ForceInitialize();
            cboLineasID.Properties.PopulateColumns();
            cboLineasID.Properties.Columns["Clave"].Visible = false;
            cboLineasID.Properties.NullText = "Seleccione una linea";

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void LlenarGridFamilias()
        {
            familiasCL cl = new familiasCL();
            gridControlPrincipal.DataSource = cl.FamiliasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridFamilias";
            clg.restoreLayout(gridViewPrincipal);

        }//LlenarGrid()

        private void BotonesEdicion()
        {

            ribbonPageGroup1.Visible = false;
            ribbonPageGroup2.Visible = true;

            navigationFrame.SelectedPageIndex = 1;
        }


        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            cboLineasID.EditValue = null;
            swIncluireninventarios.IsOn = false;
            swArribos.IsOn = false;
            txtPathimagen.Text = string.Empty;
            txtMargenarribadelcosto.Text = string.Empty;
            swIncluirentienda.IsOn = false;
            swTipodenavegacion.IsOn = false;
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
                familiasCL cl = new familiasCL();
                cl.intFamiliasID = intFamilias;
                cl.strNombre = txtNombre.Text;
                cl.intLineasID = Convert.ToInt32(cboLineasID.EditValue);
                cl.intIncluireninventarios = swIncluireninventarios.IsOn ? 1 : 0;
                cl.intArribos = swArribos.IsOn ? 1 : 0;
                cl.strPathimagen = txtPathimagen.Text;
                cl.dMargenarribadelcosto = Convert.ToDecimal(txtMargenarribadelcosto.Text);
                cl.intIncluirentienda = swIncluirentienda.IsOn ? 1 : 0;
                cl.intTipodenavegacion = swTipodenavegacion.IsOn ? 1 : 0;
                Result = cl.FamiliasCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intFamilias == 0)
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
            if (cboLineasID.EditValue == null)
            {
                return "El campo LineasID no puede ir vacio";
            }
            //if (txtPathimagen.Text.Length == 0)
            //{
            //    return "El campo Pathimagen no puede ir vacio";
            //}
            if (txtMargenarribadelcosto.Text.Length == 0)
            {
                return "El campo Margenarribadelcosto no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            familiasCL cl = new familiasCL();
            cl.intFamiliasID = intFamilias;
            String Result = cl.FamiliasLlenaCajas();
            if (Result == "OK")
            {
                txtNombre.Text = cl.strNombre;
                cboLineasID.EditValue = cl.intLineasID;
                swIncluireninventarios.IsOn = cl.intIncluireninventarios == 1 ? true : false;
                swArribos.IsOn = cl.intArribos == 1 ? true : false;
                txtPathimagen.Text = cl.strPathimagen;
                txtMargenarribadelcosto.Text = cl.dMargenarribadelcosto.ToString();
                swIncluirentienda.IsOn = cl.intIncluirentienda == 1 ? true : false;
                swTipodenavegacion.IsOn = cl.intTipodenavegacion == 1 ? true : false;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFamilias == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }  //Editar
        private void Nuevo()
        {
            BotonesEdicion();
            intFamilias = 0;
        }
        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();
        }



        private void Eliminar()
        {
            familiasCL cl = new familiasCL();
            cl.intFamiliasID = intFamilias;
            String Result = cl.FamiliasEliminar();
            if (Result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGridFamilias();
            }
            else
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFamilias == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intFamilias.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageGroup1.Visible = true;
            ribbonPageGroup2.Visible = false;
            LimpiaCajas();
            intFamilias = 0;
            LlenarGridFamilias();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridFamilias";
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
            intFamilias = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "FamiliasID"));
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }
    }
}