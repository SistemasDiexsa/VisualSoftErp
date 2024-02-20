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

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class infoProv : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intProveedoresID;
        string strGrid = string.Empty;
        string strNom = string.Empty;

        public infoProv()
        {
            InitializeComponent();

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            gridViewCompras.OptionsBehavior.ReadOnly = true;
            gridViewCompras.OptionsBehavior.Editable = false;
            gridViewCompras.OptionsView.ShowAutoFilterRow = true;
            gridViewCompras.OptionsView.ShowViewCaption = true;

            gridViewCxP.OptionsBehavior.ReadOnly = true;
            gridViewCxP.OptionsBehavior.Editable = false;
            gridViewCxP.OptionsView.ShowAutoFilterRow = true;
            gridViewCxP.OptionsView.ShowViewCaption = true;

            gridViewMovimientosCxP.OptionsBehavior.ReadOnly = true;
            gridViewMovimientosCxP.OptionsBehavior.Editable = false;
            gridViewMovimientosCxP.OptionsView.ShowAutoFilterRow = true;
            gridViewMovimientosCxP.OptionsView.ShowViewCaption = true;

            txtProv.Select();

            ribbonControl.SelectedPage = ribbonPage1;

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        

        private void CargaProveedores()
        {
            ProveedoresCL cl = new ProveedoresCL();
            cl.strNombre = txtProv.Text;
            gridControlPrincipal.DataSource = cl.infoProveedores();
            gridViewPrincipal.FocusedRowHandle = 0;
            intProveedoresID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ProveedoresID"));

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridInfoProveedores";
            clg.restoreLayout(gridViewPrincipal);
        }

        private void Compras()
        {
            try
            {
              
                ProveedoresCL cl = new ProveedoresCL();
                cl.intProveedoresID = intProveedoresID;
                gridControlCompras.DataSource = cl.infoProveedoresCompras();
                gridViewCompras.ViewCaption = "COMPRAS DEL PROVEEDOR " + strNom;

                globalCL clg = new globalCL();
                clg.strGridLayout = "gridInfoProvCompras";
                clg.restoreLayout(gridViewCompras);



                BotonesEdicion();

                navigationFrame.SelectedPageIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BotonesEdicion()
        {
            bbiCompras.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiMovimientosCxP.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCxP.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridInfoProvCompras";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
        }

        private void CxP()
        {
            try
            {
                ProveedoresCL cl = new ProveedoresCL();
                cl.intProveedoresID = intProveedoresID;
                String Result = cl.ProveedoresInfoCxP();
                if (Result == "OK")
                {
                    txtNombre.Text = strNom;
                    txtTelefono.Text = cl.strTelefono;
                    txtCreditoAutorizado.Text = cl.intCreditoautorizado.ToString();
                    txtPlazo.Text = cl.intPLazo.ToString();
                    txtCorreo.Text = cl.strEmail;                
                }
                else
                {
                    MessageBox.Show(Result);
                }

                cl.intProveedoresID = intProveedoresID;
                cl.fFechaInfo = DateTime.Now;
                gridControlCxP.DataSource = cl.infoProveedoresCxpAntiguedaddeSaldosRep();

                globalCL clg = new globalCL();
                clg.strGridLayout = "gridInfoProvCxP";
                clg.restoreLayout(gridViewCompras);



                BotonesEdicion();

                navigationFrame.SelectedPageIndex = 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void MovimientosCxP()
        {
            try
            {

                ProveedoresCL cl = new ProveedoresCL();
                cl.intProveedoresID = intProveedoresID;
                cl.fFechaInfoI = Convert.ToDateTime("01/01/" + DateTime.Now.Year.ToString());
                cl.fFechaInfo = DateTime.Now;
                gridControlMovimientosCxP.DataSource = cl.infoProveedoresMovimientosCxP();
                gridViewMovimientosCxP.ViewCaption = "MOVIMIENTOS CXP DE PROVEEDOR " + strNom;

                globalCL clg = new globalCL();
                clg.strGridLayout = "gridInfoProvMovCxP";
                clg.restoreLayout(gridViewMovimientosCxP);



                BotonesEdicion();

                navigationFrame.SelectedPageIndex = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtProv_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    CargaProveedores();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intProveedoresID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ProveedoresID"));
            if (intProveedoresID > 0)
                strNom = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Nombre").ToString();
        }

        private void bbiCompras_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intProveedoresID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Compras();
            }
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            string result = string.Empty;

            bbiCompras.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiMovimientosCxP.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCxP.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 0;
            intProveedoresID = 0;

            clg.strGridLayout = "gridInfoProvCompras";
            result = clg.SaveGridLayout(gridViewCompras);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            clg.strGridLayout = "gridInfoProvCxP";
            result = clg.SaveGridLayout(gridViewCxP);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            clg.strGridLayout = "gridInfoProvMovCxP";
            result = clg.SaveGridLayout(gridViewMovimientosCxP);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridInfoProvCompras";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

           
            this.Close();
        }

        private void bbiCxP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intProveedoresID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                CxP();
            }
        }

        private void bbiMovimientosCxP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intProveedoresID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                MovimientosCxP();
            }
        }
    }
}