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
using DevExpress.XtraGrid.Views.Grid;
using VisualSoftErp.Clases;
using System.Configuration;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Operacion.CxC.Designers;

namespace VisualSoftErp.Operacion.CxC.Formas
{
    public partial class Traspasos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public BindingList<detalleCL> detalle;
        int intTraspasosID, intAlmacenOrigen, intAlmacenDestino;
        int intUsuarioID = globalCL.gv_UsuarioID;
        string origenImp = string.Empty;
        public Traspasos()
        {
            InitializeComponent();

            dateEditFecha.Text = Convert.ToString(DateTime.Now);
            txtObservaciones.Properties.MaxLength = 500;
            txtObservaciones.EnterMoveNextControl = true;


            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            //------------- Inicializar aquí opciones de columnas del grid ----------------
            //gridColumnIva.OptionsColumn.ReadOnly = true;
            //gridColumnIva.OptionsColumn.AllowFocus = false;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Traspasos entre almacenes";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            TraspasosCL cl = new TraspasosCL();
            gridControlPrincipal.DataSource = cl.TraspasosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridTraspasos";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        public class detalleCL
        {

            public string Articulo { get; set; }
            public string Cantidad { get; set; }
            public string Descripcion { get; set; }
        }
        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
            gridColumnDescripcion.OptionsColumn.ReadOnly = true;
            gridColumnDescripcion.OptionsColumn.AllowFocus = false;
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "AlmacenesTraslados";
            cboAlmacenOrigenID.Properties.ValueMember = "Clave";
            cboAlmacenOrigenID.Properties.DisplayMember = "Des";
            cboAlmacenOrigenID.Properties.DataSource = cl.CargaCombos();
            cboAlmacenOrigenID.Properties.ForceInitialize();
            cboAlmacenOrigenID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacenOrigenID.Properties.PopulateColumns();
            cboAlmacenOrigenID.Properties.Columns["Clave"].Visible = false;
            cboAlmacenOrigenID.Properties.NullText = "Seleccione Almancen de origen";

            cl.strTabla = "AlmacenesTraslados";
            cboAlmacenDestinoID.Properties.ValueMember = "Clave";
            cboAlmacenDestinoID.Properties.DisplayMember = "Des";
            cboAlmacenDestinoID.Properties.DataSource = cl.CargaCombos();
            cboAlmacenDestinoID.Properties.ForceInitialize();
            cboAlmacenDestinoID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacenDestinoID.Properties.PopulateColumns();
            cboAlmacenDestinoID.Properties.Columns["Clave"].Visible = false;
            cboAlmacenDestinoID.Properties.NullText = "Seleccione Almancen de destino";
            cl.strTabla = "Articulos";
            repositoryItemLookUpEditArticulo.ValueMember = "Clave";
            repositoryItemLookUpEditArticulo.DisplayMember = "Des";
            repositoryItemLookUpEditArticulo.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulo.ForceInitialize();
            repositoryItemLookUpEditArticulo.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

        }

        private void Nuevo()
        {
            ribbonPageGroup1.Visible = false;
            blNuevo = true;
   
            SiguienteID();
            lblTraspaso.Text = "Traspaso: " + intTraspasosID.ToString();
            LimpiaCajas();
            Inicialisalista();

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridTraspasosDetalle";
            clg.restoreLayout(gridViewDetalle);

            ribbonPageGroup2.Visible = true;
            ribbonStatusBar.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void LimpiaCajas()
        {
            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            cboAlmacenOrigenID.EditValue = null;
            cboAlmacenDestinoID.EditValue = null;
            txtObservaciones.Text = string.Empty;
            lblTraspaso.Text = "Traspaso: ";
          
        }

        private void SiguienteID()
        {
            int iFolio = 0;
            globalCL clg = new globalCL();
            clg.strDoc = "Traspasos";
            clg.strSerie = "A";
            //Manual: --- Definir la condicion necesaria para traer el siguiente ID ---------------------         clg.sCondicion = sCondicion;
            string Result = clg.DocumentosSiguienteID();
            if (Result == "OK")
            {
                intTraspasosID = clg.iFolio;
            }
            else
            {
                MessageBox.Show("SiguienteID :" + Result);
            }

        }//SguienteID

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
                System.Data.DataTable dtTraspasos = new System.Data.DataTable("Traspasos");
                dtTraspasos.Columns.Add("TraspasosID", Type.GetType("System.Int32"));
                dtTraspasos.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtTraspasos.Columns.Add("AlmacenOrigenID", Type.GetType("System.Int32"));
                dtTraspasos.Columns.Add("AlmacenDestinoID", Type.GetType("System.Int32"));
                dtTraspasos.Columns.Add("Observaciones", Type.GetType("System.String"));
                dtTraspasos.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtTraspasos.Columns.Add("FechaReal", Type.GetType("System.DateTime"));

                System.Data.DataTable dtTraspasosdetalle = new System.Data.DataTable("Traspasosdetalle");
                dtTraspasosdetalle.Columns.Add("TraspasosID", Type.GetType("System.Int32"));
                dtTraspasosdetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtTraspasosdetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtTraspasosdetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));


                if (blNuevo)
                {
                    SiguienteID();
                }
              
              
                string dato = String.Empty;
                int intSeq = 0;
                int intArticulosID = 0;
                decimal dCantidad = 0;

                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "Articulo").ToString();
                    if (dato.Length > 0)
                    {
                        intSeq = i+1;      
                        intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Articulo"));
                        dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Cantidad"));
                        if (dCantidad == 0) { MessageBox.Show("Renglon: " + intSeq + ", La cantidad no puede ir en 0"); }
                        dtTraspasosdetalle.Rows.Add(intTraspasosID, intSeq, intArticulosID, dCantidad) ;
  
                    }
                }

                DateTime fFecha = Convert.ToDateTime(dateEditFecha.Text);
                int intAlmacenOrigenID = Convert.ToInt32(cboAlmacenOrigenID.EditValue);
                int intAlmacenDestinoID = Convert.ToInt32(cboAlmacenDestinoID.EditValue);
                string strObservaciones = txtObservaciones.Text;
       
                DateTime fFechaReal = Convert.ToDateTime(DateTime.Now);
                dato = String.Empty;
                dtTraspasos.Rows.Add(intTraspasosID, fFecha, intAlmacenOrigenID, intAlmacenDestinoID, strObservaciones, intUsuarioID, fFechaReal);

                TraspasosCL cl = new TraspasosCL();
                cl.intTraspasosID = intTraspasosID;

                cl.dtm = dtTraspasos;
                cl.dtd = dtTraspasosdetalle;
                cl.intUsuarioID = intUsuarioID;
                cl.strMaquina = Environment.MachineName;
                Result = cl.TraspasosCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    origenImp = "G";
                    Imprime();
                    LimpiaCajas();
                    Inicialisalista();
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
            if (cboAlmacenOrigenID.EditValue == null)
            {
                return "El campo AlmacenOrigenID no puede ir vacio";
            }
            if (cboAlmacenDestinoID.EditValue == null)
            {
                return "El campo AlmacenDestinoID no puede ir vacio";
            }
            if (txtObservaciones.Text.Length == 0)
            {
                return "El campo Observaciones no puede ir vacio";
            }
            if (dateEditFecha.Text.Length == 0)
            {
                return "El campo Fecha no puede ir vacio";
            }

            globalCL clg = new globalCL();
            int ren = 0;
            string dato = string.Empty;
            for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++)
            {
                if (gridViewDetalle.GetRowCellValue(i, "Cantidad") != null)
                {
                    if (clg.esNumerico(gridViewDetalle.GetRowCellValue(i, "Cantidad").ToString()))
                    {
                        ren = ren + 1;
                    }

                }
            }

            if (ren == 0)
            {
                return "Debe capturar al menos un renglón";
            }

            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(dateEditFecha.Text).Year, Convert.ToDateTime(dateEditFecha.Text).Month, "INV");
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";
            }

            return "OK";
        } //Valida



        #region Filtros de bbi inferior
        //private void bbiRegistradas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Registrada'";
        //}

        //private void bbiAceptadas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Aceptada'";
        //}

        //private void bbiRechazadas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Rechazada'";
        //}

        //private void bbiExpiradas_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    gridViewPrincipal.ActiveFilterString = "[Estado]='Expirada'";
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


        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            intTraspasosID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "TraspasosID"));
          
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            globalCL clgd = new globalCL();
            clgd.strGridLayout = "gridTraspasosDetalle";
            string result = clgd.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            intTraspasosID = 0;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LimpiaCajas();
            LlenarGrid();
            ribbonPageGroup2.Visible = false;
            ribbonPageGroup1.Visible = true;
   
            ribbonStatusBar.Visible = true;
            Inicialisalista();
            LimpiaCajas();
            navigationFrame.SelectedPageIndex = 0;
        }

        int impDirecto = 0;
        private void Imprimir()
        {
            try
            {



              
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

                TraspasosDesigner rep = new TraspasosDesigner();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = intTraspasosID;        //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = intTraspasosID;        //Emp
                    rep.Parameters["parameter1"].Visible = false;
                   
                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                    navigationFrame.SelectedPageIndex = 2;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void Imprime()
        {

            try
            {
                ribbonPageHome.Visible = false;
                ribbonPageImpresion.Visible = true;
                navigationFrame.SelectedPageIndex = 2;
                ribbonStatusBar.Visible = false;

                Imprimir();

                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Imprime: " + ex.Message);
            }

        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           Guardar();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridTraspasos" +
                "";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

           
                this.Close();
        }

        
        bool blNuevo;

        private void Editar()
        {
            LlenaCajas();
            DetalleLlenaCajas();

            BotonesEdicion();
            blNuevo = false;
        }//ver

        private void BotonesEdicion()
        {
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup2.Visible = true;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 1;

        }//BotonesEdicion 

        private void LlenaCajas()
        {
            TraspasosCL cl = new TraspasosCL();
            cl.intTraspasosID = intTraspasosID;
            String Result = cl.TraspasosLlenaCajas();
            if (Result == "OK")
            {
                dateEditFecha.Text = cl.fFecha.ToShortDateString();
                cboAlmacenOrigenID.EditValue = cl.intAlmacenOrigenID;
                cboAlmacenDestinoID.EditValue = cl.intAlmacenDestinoID;
                txtObservaciones.Text = cl.strObservaciones;

                lblTraspaso.Text = "Traspaso: " + intTraspasosID.ToString();

            }
            else
            {
                MessageBox.Show(Result);
            }
        }//llenacajas

        private void DetalleLlenaCajas()
        {
            try
            {
                TraspasosCL cl = new TraspasosCL();
                cl.intTraspasosID = intTraspasosID;
                gridControlDetalle.DataSource = cl.TraspasosDetalleLlenaCajas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }


        }//detalleLlenacajas  

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (intFolio == 0)
            //{
            //    MessageBox.Show("Selecciona un renglón");
            //}
            //else
            //{
            //    popUpCancelar.Visible = true;
            //    groupControl3.Text = "Cancelar el folio:" + strSerie + intFolio.ToString();
            //    txtLogin.Focus();
            //}
        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                int intstatus = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Status"));

                if (intstatus == 5)
                {
                    e.Appearance.ForeColor = Color.Red;
                }

            }
            catch (Exception ex)
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
            int intArticulosID; 
            int intFoliocfdi;
            string strDes="";

            try
            {
                if (e.Column.Name == "gridColumnArticulo")
                {
                 
                    intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulo"));
                   
                    articulosCL cl = new articulosCL();
                    cl.intArticulosID = intArticulosID;
                    string Result = cl.articulosLlenaCajas();
                    if (Result == "OK")
                    {
                        strDes = cl.strNombre;
                        gridViewDetalle.SetFocusedRowCellValue("Descripcion", strDes);
                    }
                    else
                    {
                        MessageBox.Show(Result);
                    }

                }
               
            }


            catch (Exception ex)
            {
                MessageBox.Show("griddetallecalculatotales: " + ex);
            }


        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void bbiVer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intTraspasosID==0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intTraspasosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                origenImp = "P";
                Imprime();
                navigationFrame.SelectedPageIndex = 3;

            }
        }

        private void bbiRegresarImp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intTraspasosID = 0;

            LimpiaCajas();
            LlenarGrid();
            ribbonPageHome.Visible = true;
            ribbonPageImpresion.Visible = false;

            ribbonStatusBar.Visible = true;
            ribbonPageHome.Visible = true;

            if (origenImp == "P")
            {
                navigationFrame.SelectedPageIndex = 0;
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText("Home");
            }
            else
                navigationFrame.SelectedPageIndex = 1;

            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText("Home");
        }

        private void cboAlmacenOrigenID_EditValueChanged(object sender, EventArgs e)
        {
            intAlmacenOrigen = Convert.ToInt32(cboAlmacenOrigenID.EditValue);
            intAlmacenDestino = Convert.ToInt32(cboAlmacenDestinoID.EditValue);

            if (intAlmacenOrigen == intAlmacenDestino)
            {
                cboAlmacenDestinoID.EditValue = null;
            }
        }

        private void cboAlmacenDestinoID_EditValueChanged(object sender, EventArgs e)
        {
            intAlmacenOrigen = Convert.ToInt32(cboAlmacenOrigenID.EditValue);
            intAlmacenDestino = Convert.ToInt32(cboAlmacenDestinoID.EditValue);

            if (intAlmacenOrigen == intAlmacenDestino)
            {
                cboAlmacenOrigenID.EditValue = null;
            }

        }
    }
}