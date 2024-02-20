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

namespace VisualSoftErp.Operacion.Inventarios.Formas
{
    public partial class ArticulosGruposAuxiliar : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public ArticulosGruposAuxiliar()
        {
            InitializeComponent();
            txtNombre.Properties.MaxLength = 50;
            txtNombre.EnterMoveNextControl = true;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de ArticulosGruposAuxiliar";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            ArticulosGruposAuxiliarCL cl = new ArticulosGruposAuxiliarCL();
            gridControlPrincipal.DataSource = cl.ArticulosGruposAuxiliarGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridArticulosGruposAuxiliar";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
            intGruposID = 0;
            groupControlArticulos.Visible = false;
        }

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            txtAdicionalalmes.Text = "0";
            txtDiasStock.Text = "0";
        }

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();
            llenaGridDetalle();
        }

        private void BotonesEdicion()
        {
            LimpiaCajas();
            ribbonPageGroup2.Visible = true;
            ribbonPageGroup1.Visible = false;

            navigationFrame.SelectedPageIndex = 1;
        }

        int intGruposID;

        private void llenaGridDetalle()
        {
            try
            {
                groupControlArticulos.Visible = true;
                ArticulosGruposAuxiliarCL cl = new ArticulosGruposAuxiliarCL();
                cl.intGruposID = intGruposID;
                gridControlDetalle.DataSource = cl.ArticulosGruposAuxiliarGridDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("llenaGridDetalle: " + ex);
            }
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
                ArticulosGruposAuxiliarCL cl = new ArticulosGruposAuxiliarCL();
                cl.intGruposID = intGruposID;
                cl.strNombre = txtNombre.Text;
                cl.dAdicionalalmes = Convert.ToDecimal(txtAdicionalalmes.Text);
                cl.intDiasStock = Convert.ToInt32(txtDiasStock.Text);
                Result = cl.ArticulosGruposAuxiliarCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intGruposID == 0)
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
            if (txtAdicionalalmes.Text.Length == 0)
            {
                return "El campo Adicionalalmes no puede ir vacio";
            }
            if (txtDiasStock.Text.Length == 0)
            {
                return "El campo DiasStock no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            ArticulosGruposAuxiliarCL cl = new ArticulosGruposAuxiliarCL();
            cl.intGruposID = intGruposID;
            String Result = cl.ArticulosGruposAuxiliarLlenaCajas();
            if (Result == "OK")
            {
                txtNombre.Text = cl.strNombre;
                txtAdicionalalmes.Text = cl.dAdicionalalmes.ToString();
                txtDiasStock.Text = cl.intDiasStock.ToString();
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas


        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageGroup2.Visible = false;
            ribbonPageGroup1.Visible = true;
            groupControlArticulos.Visible = false;
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridArticulosGruposAuxiliar";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }
     
        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intGruposID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "GruposID"));

        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intGruposID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }
    }
}