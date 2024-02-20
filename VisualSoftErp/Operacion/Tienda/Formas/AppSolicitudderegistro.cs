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
using VisualSoftErp.Clases.CCP_CLs;

namespace VisualSoftErp.CCP.Catalogos
{
    public partial class AppSolicitudderegistro : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intAppSolicitudderegistroID;

        public AppSolicitudderegistro()
        {
            InitializeComponent();

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Solicitudes de registro";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            AppSolicitudderegistroCL cl = new AppSolicitudderegistroCL();
            gridControlPrincipal.DataSource = cl.AppSolicitudderegistroGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppSolicitudderegistro";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()


        private void Ver()
        {
            BotonesEdicion();//limpiaCajas 
            llenaCajas();
        }

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtCiudad.Text = string.Empty;
            txtComoseenterodenosotros.Text = string.Empty;
            txtComentarios.Text = string.Empty;
        }

        private void bbiProceder_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult Result = MessageBox.Show("Verificar ID " + intAppSolicitudderegistroID.ToString(), "Proceder", MessageBoxButtons.YesNo);
            if (Result.ToString() == "Yes")
            {
                Guardar();
            }
            
        }

        private void Guardar()
        {
            try
            {
                string Result = string.Empty;
                AppSolicitudderegistroCL cl = new AppSolicitudderegistroCL();
                cl.intAppSolicitudderegistroID = intAppSolicitudderegistroID;
                cl.intVerificado = 1;
                cl.fFechaVerificado = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                cl.intUsuarioVerifico = globalCL.gv_UsuarioID;
                Result = cl.AppSolicitudderegistroVERIF();
                if (Result == "OK")
                {
                    MessageBox.Show("Verificado Correctamente");
                    if (intAppSolicitudderegistroID == 0)
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

            return "OK";
        } //Valida

        private void llenaCajas()
        {
            AppSolicitudderegistroCL cl = new AppSolicitudderegistroCL();
            cl.intAppSolicitudderegistroID = intAppSolicitudderegistroID;
            String Result = cl.AppSolicitudderegistroLlenaCajas();
            if (Result == "OK")
            {
                txtNombre.Text = cl.strNombre;
                txtCorreo.Text = cl.strCorreo;
                txtTelefono.Text = cl.strTelefono;
                txtCiudad.Text = cl.strCiudad;
                txtComoseenterodenosotros.Text = cl.strComoseenterodenosotros;
                txtComentarios.Text = cl.strComentarios;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas



        private void BotonesEdicion()
        {
            LimpiaCajas();
            bbiProceder.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVer.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            navigationFrame.SelectedPageIndex = 1;
        }


        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intAppSolicitudderegistroID = 0;
            bbiProceder.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVer.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppSolicitudderegistro";
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
            intAppSolicitudderegistroID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "AppSolicitudderegistroID"));
        }

        private void bbiVer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intAppSolicitudderegistroID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Ver();
            }
           
        }
    }
}