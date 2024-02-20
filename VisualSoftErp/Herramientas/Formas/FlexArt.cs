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
    public partial class FlexArt : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int iArticulo = 0;
        string sNomArt = string.Empty;
        string sGrid = string.Empty;
        bool verMasInfo;
        int iArribosProv = 0;
        int iFlexArtMovimientos = 0;
        int iFlexArtVentas = 0;
        int iFlexArtCompras = 0;

        public FlexArt()
        {
            InitializeComponent();
            gridViewArt.OptionsBehavior.ReadOnly = true;
            gridViewArt.OptionsBehavior.Editable = false;
            gridViewArt.OptionsView.ShowAutoFilterRow = true;

            splitContainerControl2.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;

            verMasInfo = false;

            articulosCL cl = new articulosCL();
            string result = cl.FlexArtUsuarios();
            if (result == "OK")
            {
                iArribosProv = cl.iFlexArtArribosProveedor;
                iFlexArtMovimientos = cl.iFlexArtMovimientos;
                iFlexArtVentas = cl.iFlexArtVentas;
                iFlexArtCompras = cl.iFlexArtCompras;

                if (cl.iFlexArtMovimientos == 1)
                    bbiMovs.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    bbiMovs.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                if (cl.iFlexArtVentas == 1)
                    bbiVentas.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    bbiVentas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                if (cl.iFlexArtCompras == 1)
                    bbiCompras.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    bbiCompras.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                bbiVentas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiCompras.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiMovs.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                iArribosProv = 0;

            }


            gridViewArt.OptionsBehavior.ReadOnly = true;
            gridViewArt.OptionsBehavior.Editable = false;
            gridViewExAlm.OptionsBehavior.ReadOnly = true;
            gridViewExAlm.OptionsBehavior.Editable = false;
            gridViewPedidos.OptionsBehavior.ReadOnly = true;
            gridViewPedidos.OptionsBehavior.Editable = false;
            gridViewVentas.OptionsBehavior.ReadOnly = true;
            gridViewVentas.OptionsBehavior.Editable = false;
            gridViewMovs.OptionsBehavior.ReadOnly = true;
            gridViewMovs.OptionsBehavior.Editable = false;
            gridViewCompras.OptionsBehavior.ReadOnly = true;
            gridViewCompras.OptionsBehavior.Editable = false;
            

            txtArt.Select();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
       
        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridFlexArt";
            String Result = clg.SaveGridLayout(gridViewArt);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }

            this.Close();
        }

        private void CargaArticulos()
        {
            articulosCL cl = new articulosCL();
            cl.strNombre = txtArt.Text;
            gridControlArt.DataSource = cl.flexArt();

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridFlexArt";
            clg.restoreLayout(gridViewArt);

            sGrid = "Flexart";


            gridViewArt.FocusedRowHandle = 0;
            
            if (gridViewArt.RowCount>1)
            {
                iArticulo = Convert.ToInt32(gridViewArt.GetRowCellValue(0, "ArticulosID"));
                sNomArt = gridViewArt.GetRowCellValue(0, "Nombre").ToString();
                Cargamasinfo();
            }

            

        }

        private void txtArt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    CargaArticulos();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiMasInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FlexArt.Size.Width = 1638;
            FlexArt.ActiveForm.Size = new System.Drawing.Size(1638, 738);

            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

            Cargamasinfo();

            bbiMasInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiMenosInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            verMasInfo = true;
        }

        private void Cargamasinfo()
        {
            articulosCL cl = new articulosCL();
            cl.intArticulosID = iArticulo;
            cl.fFechaalta = DateTime.Now;
            gridControlExAlm.DataSource = cl.ExistenciaPorAlmacen();

            gridControlPedidos.DataSource = cl.Pedidosporfacturar();

            gridControlOC.DataSource = cl.ocporRecibir();

            if (iArribosProv==0)
            {
                gridViewOC.Columns["Proveedor"].Visible = false;
                gridViewOC.Columns["Nombre"].Visible = false;
            }
            else
            {
                gridViewOC.Columns["Proveedor"].Visible = true;
                gridViewOC.Columns["Nombre"].Visible = true;
            }


            gridViewExAlm.OptionsView.ShowViewCaption = true;
            gridViewExAlm.ViewCaption = "Existencia por almacén";

            gridViewPedidos.OptionsView.ShowViewCaption = true;
            gridViewPedidos.ViewCaption = "Pedidos por facturar";

            gridViewOC.OptionsView.ShowViewCaption = true;
            gridViewOC.ViewCaption = "Arribos";
        }

        private void VerificaAccesos()
        {            
                if (iFlexArtMovimientos == 1)
                    bbiMovs.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    bbiMovs.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                if (iFlexArtVentas == 1)
                    bbiVentas.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    bbiVentas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                if (iFlexArtCompras == 1)
                    bbiCompras.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    bbiCompras.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
           
        }


        private void gridViewArt_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            iArticulo = Convert.ToInt32(gridViewArt.GetRowCellValue(gridViewArt.FocusedRowHandle, "ArticulosID"));
            sNomArt = gridViewArt.GetRowCellValue(gridViewArt.FocusedRowHandle, "Nombre").ToString();

            //if (verMasInfo)
           // {
                Cargamasinfo();
            //}
        }

        private void bbiMovs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            articulosCL cl = new articulosCL();
            cl.intArticulosID = iArticulo;
            gridControlMovs.DataSource = cl.Movimientos();
            gridViewMovs.OptionsView.ShowAutoFilterRow = true;
            gridViewMovs.OptionsView.ShowViewCaption = true;
            gridViewMovs.ViewCaption = "MOVIMIENTOS DEL ARTÍCULO: " + sNomArt;

            bbiMovs.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiMasInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVentas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCompras.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 1;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridFlexMovs";
            clg.restoreLayout(gridViewArt);

            sGrid = "Movs";
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiMovs.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiMasInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVentas.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCompras.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            globalCL clg = new globalCL();

            switch (sGrid)
            {
                case "Movs":
                    clg.strGridLayout = "gridFlexMovs";
                    break;
                case "Ventas":
                    clg.strGridLayout = "gridFlexVentas";
                    break;
                case "Compras":
                    clg.strGridLayout = "gridFlexCompras";
                    break;
            }
            

            String Result = clg.SaveGridLayout(gridViewArt);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }

            VerificaAccesos();

            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiVentas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            articulosCL cl = new articulosCL();
            cl.intArticulosID = iArticulo;
            gridControlVentas.DataSource = cl.Ventas();
            gridViewVentas.OptionsView.ShowAutoFilterRow = true;
            gridViewVentas.OptionsView.ShowViewCaption = true;
            gridViewVentas.ViewCaption = "VENTAS DEL ARTÍCULO: " + sNomArt;

            bbiMovs.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiMasInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVentas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCompras.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 2;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridFlexVentas";
            clg.restoreLayout(gridViewArt);

            sGrid = "Ventas";
        }

        private void bbiCompras_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            articulosCL cl = new articulosCL();
            cl.intArticulosID = iArticulo;
            gridControlCompras.DataSource = cl.Compras();
            gridViewCompras.OptionsView.ShowAutoFilterRow = true;
            gridViewCompras.OptionsView.ShowViewCaption = true;
            gridViewCompras.ViewCaption = "COMPRAS DEL ARTÍCULO: " + sNomArt;

            bbiMovs.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiMasInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVentas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCompras.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 3;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridFlexCompras";
            clg.restoreLayout(gridViewArt);

            sGrid = "Compras";
        }

        private void bbiPrev_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (sGrid)
            {
                case "Flexart":
                    gridControlArt.ShowRibbonPrintPreview();
                    break;
                case "Movs":
                    gridControlMovs.ShowRibbonPrintPreview();
                    break;
                case "Ventas":
                    gridControlVentas.ShowRibbonPrintPreview();
                    break;
                case "Compras":
                    gridControlCompras.ShowRibbonPrintPreview();
                    break;
            }
            
        }

        private void bbiMenosInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FlexArt.ActiveForm.Size = new System.Drawing.Size(936, 738);

            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

            bbiMasInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiMenosInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            verMasInfo = false;
        }
    }
}