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

    public partial class Accesos : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intUsuariosID;
        string strprogramaID;

        public Accesos()
        {
            InitializeComponent();

     

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Accesos";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            AccesosCL cl = new AccesosCL();
            gridControlPrincipal.DataSource = cl.AccesosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAccesos";
            clg.restoreLayout(gridViewPrincipal);
            
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Usuarios";
            cboUsuariosID.Properties.ValueMember = "Clave";
            cboUsuariosID.Properties.DisplayMember = "Des";
            cboUsuariosID.Properties.DataSource = cl.CargaCombos();
            cboUsuariosID.Properties.ForceInitialize();
            cboUsuariosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboUsuariosID.Properties.PopulateColumns();
            cboUsuariosID.Properties.Columns["Clave"].Visible = false;
            cboUsuariosID.Properties.NullText = "Seleccione un usuario";

            cl.strTabla = "Programas";
            cboProgramaID.Properties.ValueMember = "Clave";
            cboProgramaID.Properties.DisplayMember = "Des";
            cboProgramaID.Properties.DataSource = cl.CargaCombos();
            cboProgramaID.Properties.ForceInitialize();
            cboProgramaID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProgramaID.Properties.PopulateColumns();
            cboProgramaID.Properties.Columns["Clave"].Visible = false;
            cboProgramaID.Properties.NullText = "Seleccione un programa";
        }
        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intUsuariosID = 0;
            strprogramaID = string.Empty;

            cboUsuariosID.Enabled = true;
            cboProgramaID.Enabled = true;
            CargaCombos();
        }

        private void LimpiaCajas()
        {
            cboUsuariosID.EditValue = null;
            cboProgramaID.EditValue = null;
            swFavorito.IsOn = false;
            swSololectura.IsOn = false;
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
                AccesosCL cl = new AccesosCL();
                cl.intUsuariosID = Convert.ToInt32(cboUsuariosID.EditValue);
                cl.strProgramaID = cboProgramaID.EditValue.ToString();
                cl.intFavorito = swFavorito.IsOn ? 1 : 0;
                cl.intSololectura = swSololectura.IsOn ? 1 : 0;
                Result = cl.AccesosCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intUsuariosID == 0)
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
            if (cboUsuariosID.EditValue == null)
            {
                return "El campo UsuariosID no puede ir vacio";
            }
            if (cboUsuariosID.EditValue == null)
            {
                return "El campo ProgramaID no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            AccesosCL cl = new AccesosCL();
            cl.intUsuariosID = intUsuariosID;
            cl.strProgramaID = strprogramaID;
            String Result = cl.AccesosLlenaCajas();
            if (Result == "OK")
            {
                cboUsuariosID.EditValue = cl.intUsuariosID;
                cboProgramaID.EditValue = cl.strProgramaID;
                swFavorito.IsOn = cl.intFavorito == 1 ? true : false;
                swSololectura.IsOn = cl.intSololectura == 1 ? true : false;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intUsuariosID == 0 & strprogramaID == null)
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

            cboUsuariosID.Enabled = false;
            cboProgramaID.Enabled = false;
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
            AccesosCL cl = new AccesosCL();
            cl.intUsuariosID = intUsuariosID;
            cl.strProgramaID = strprogramaID;
            String Result = cl.AccesosEliminar();
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
            if (intUsuariosID == 0 & strprogramaID == null)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el Acceso de:  " + intUsuariosID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
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
            clg.strGridLayout = "gridAccesos";
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
            intUsuariosID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "UsuariosID"));
            strprogramaID = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ProgramaID"));
        }

    }
}
