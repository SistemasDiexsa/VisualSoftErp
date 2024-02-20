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
using VisualSoftErp.Catalogos.Inv.Designers;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace VisualSoftErp.Operacion.Inventarios.Formas
{
    public partial class Kits : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Kits()
        {
            InitializeComponent();
            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Kits";

            LlenarGrid();


            CargaCombos();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        
        public BindingList<detalleCL> detalle;
        int intKitsID;
        int intArticulosID;
        string strEspecificacionesMaster;
        public bool blNuevo;

        int intUsuarioID = 1; //NO DEJAR FIJO 

        private void LlenarGrid()///gridprincipal
        {
            KitsCL cl = new KitsCL();
            gridControlPrincipal.DataSource = cl.KitsGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridKits";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();

            cl.strTabla = "Kits";
            cboArticulosID.Properties.ValueMember = "Clave";
            cboArticulosID.Properties.DisplayMember = "Des";
            cboArticulosID.Properties.DataSource = cl.CargaCombos();
            cboArticulosID.Properties.ForceInitialize();
            cboArticulosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulosID.Properties.PopulateColumns();
            cboArticulosID.Properties.Columns["Clave"].Visible = false;          
            cboArticulosID.Properties.NullText = "Seleccione un articulo";

            cl.strTabla = "NoKits";
            repositoryItemLookUpEditArticulo.ValueMember = "Clave";
            repositoryItemLookUpEditArticulo.DisplayMember = "Des";
            repositoryItemLookUpEditArticulo.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulo.ForceInitialize();
            repositoryItemLookUpEditArticulo.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
           // repositoryItemLookUpEditArticulo.Properties.NullText = "Seleccione  un articulo";

        }

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
            //gridColumnImporte.OptionsColumn.ReadOnly = true;
            //gridColumnImporte.OptionsColumn.AllowFocus = false;

        }

        public class detalleCL
        {
            public string KitsID { get; set; }
        
            public string Articulo { get; set; }
            public string Cantidad { get; set; }
            public string Especificaciones { get; set; }

        }

        private void Nuevo()
        {
            Inicialisalista();
            LimpiaCajas();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiHistorial.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVer.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
           
            ribbonStatusBar.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
            CargaCombos();
            blNuevo = true;
            
        }//Nuevo
      
        private void LimpiaCajas()
        {
            cboArticulosID.EditValue = null;          
            txtSubtotal.Text = string.Empty;
            txtIva.Text = string.Empty;
            txtIeps.Text = string.Empty;
            txtReiva.Text = string.Empty;
            txtRetIsr.Text = string.Empty;
            txtNeto.Text = string.Empty;
        }//LimpiaCajas

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
                string sCondicion = String.Empty;
                System.Data.DataTable dtKits = new System.Data.DataTable("Kits");
                dtKits.Columns.Add("KitsID", Type.GetType("System.Int32"));
                dtKits.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtKits.Columns.Add("Especificaciones", Type.GetType("System.String"));

                System.Data.DataTable dtKitsdetalle = new System.Data.DataTable("Kitsdetalle");
                dtKitsdetalle.Columns.Add("KitsID", Type.GetType("System.Int32"));
             
                dtKitsdetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtKitsdetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtKitsdetalle.Columns.Add("Especificaciones", Type.GetType("System.String"));
                KitsCL cl = new KitsCL();

                if (blNuevo)
                {
                    
                    Result = cl.KitsSigId();
                    if (Result=="OK")
                    {
                        intKitsID = cl.intKitsID;
                    }
                    else
                    {
                        MessageBox.Show("Al buscar siguiente ID:" + Result);
                    }
                   
                }
         


                string dato = String.Empty;
                int intSeq;
                int intArticulosID = 0;
               
                decimal dCantidad = 0;
                string strEspecificaciones = String.Empty;
                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    
                        intSeq = i;
                        intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Articulo"));
                        dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Cantidad"));
                        strEspecificaciones = gridViewDetalle.GetRowCellValue(i, "Especificaciones").ToString();

                        dtKitsdetalle.Rows.Add(intKitsID, intArticulosID, dCantidad, strEspecificaciones);     

                }

                intArticulosID = Convert.ToInt32(cboArticulosID.EditValue);
                strEspecificacionesMaster = txtEspecificaciones.Text;
                dato = String.Empty;
                dtKits.Rows.Add(intKitsID, intArticulosID, strEspecificacionesMaster);

                
                cl.intKitsID = intKitsID;
                cl.dtm = dtKits;
                cl.dtd = dtKitsdetalle;
                cl.intUsuarioID = 1;
                cl.strMaquina = Environment.MachineName;
                Result = cl.KitsCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intKitsID == 0)
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
            if (cboArticulosID.Text.Length == 0)
            {
                return "El campo Artículo no puede ir vacio";
            }

            if (gridViewDetalle.RowCount-1 == 0)
            {
                return "Debe capturar al menos un componente";
            }

            for (int i=0;i<=gridViewDetalle.RowCount-1;i++)
            {
                gridViewDetalle.FocusedRowHandle = i;
                gridViewDetalle.UpdateCurrentRow();  //Forza el update del edit aunque no cambien de celda
            }
        
            return "OK";
        } //Valida


        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }


        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }
     
        private void gridViewDetalle_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }    
     
        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          Guardar();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiFacturar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiPagarsinfactura.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVer.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
   
            cboArticulosID.ReadOnly = false;
    
            ribbonStatusBar.Visible = true;

            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;

            intKitsID = 0;
     


        }

        private void BotonesEdicion()
        {
            LimpiaCajas();

            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVer.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiIImprimir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            navigationFrame.SelectedPageIndex = 1;

        }//BotonesEdicion()
        private void btnVer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intKitsID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void Editar()
        {
            blNuevo = false;
            BotonesEdicion();
            LlenaCajas();
            
        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
           
            intKitsID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "KitsID"));
           
        }

        private void LlenaCajas()
        {
            KitsCL cl = new KitsCL();
            cl.intKitsID = intKitsID;
            gridControlDetalle.DataSource = cl.KitsLlenaCajas();
            int Art = Convert.ToInt32(gridViewDetalle.GetRowCellValue(0, "KitArt"));
            string Esp = gridViewDetalle.GetRowCellValue(0, "EspGrales").ToString();
            cboArticulosID.EditValue = Art;
            txtEspecificaciones.Text = Esp;

        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //    try
            //    {
            //        GridView view = (GridView)sender;
            //        string status = view.GetRowCellValue(e.RowHandle, "Estado").ToString();

            //        if (status == "Cancelada")
            //        {
            //            e.Appearance.ForeColor = Color.Red;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        e.Appearance.ForeColor = Color.Black;
            //    }
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (intFolio == 0)
            //{
            //    MessageBox.Show("Selecciona un renglón");
            //}
            //else
            //{
            //    strStatus = "Cancelar";
            //    ribbonPageGroup2.Visible = true;
            //    txtRazon.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //}
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridKits";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

       
        #region Filtros de bbi Meses

        private void navBarItemEne_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 1";
        }

        private void navBarItemFeb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 2";
        }

        private void navBarItemMar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 3";
        }

        private void navBarItemAbr_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 4";
        }

        private void navBarItemMay_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 5";
        }

        private void navBarItemJun_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 6";
        }

        private void navBarItemJul_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 7";
        }

        private void navBarItemAgo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 8";
        }

        private void navBarItemsep_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 9";
        }

        private void navBarItemOct_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 10";
        }

        private void navBarItemNov_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 11";
        }

        private void navBarItemDic_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 12";
        }

        private void navBarItemTodos_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilter.Clear();
        }

        private void cboArticulosID_EditValueChanged(object sender, EventArgs e)
        {
            ExisteKit();
        }

        private void ExisteKit()
        {

            if (blNuevo)
            {
                KitsCL cl = new KitsCL();
                cl.intArticulosID = Convert.ToInt32(cboArticulosID.EditValue);
                string result = cl.KitsExiste();
                if (result=="OK")
                {
                    if (cl.intArticulosID>0)
                    {
                        MessageBox.Show("Este artículo ya se capturó como kit");
                        bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else
                    {
                        bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                }
                else
                {
                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
            }
        }

        private void bbiIImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intKitsID==0)
            {
                MessageBox.Show("Seleccione un kit");
            }
            else
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Informes","Generando informe...");
                Impresion();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        private void Impresion()
        {
            try
            {
                
                int impDirecto = 0;
                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                if (result == "OK")
                {
                    impDirecto = cl.iImpresiondirecta;
                }
                else
                {
                    impDirecto = 0;
                }

                Kitsdesigner rep = new Kitsdesigner();
                if (impDirecto == 1)
                {

                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = intKitsID;
                    rep.Parameters["parameter3"].Visible = false;
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = intKitsID;
                    rep.Parameters["parameter3"].Visible = false;
                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    ribbonPageImpresion.Visible = true;
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                    navigationFrame.SelectedPageIndex = 2;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void bbiRegresarImp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPage1.Text);
            ribbonPageImpresion.Visible = false;
            navigationFrame.SelectedPageIndex = 0;
        }

        private void gridControlDetalle_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                view.DeleteSelectedRows();
                e.Handled = true;
            }
        }




        #endregion

        #region Filtros Inferiores
        //private void bbiFacturadas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Facturada'";
        //}

        //private void bbiRegistradas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Registrada'";
        //}

        //private void bbiPagadasinfactura_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Pagadasinfactura'";
        //}

        //private void bbiCanceladas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Cancelada'";
        //}

        //private void bbiTodas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilter.Clear();
        //}
        #endregion

        //private void bbiVista_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ShowRibbonPrintPreview();
        //}
    }
}