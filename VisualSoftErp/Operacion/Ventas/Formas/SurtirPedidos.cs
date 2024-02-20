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
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using VisualSoftErp.Clases.VentasCLs;
using VisualSoftErp.Operacion.Ventas.Designers;

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class SurtirPedidos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        decimal dCantidad;
        string strSerie = "A"; // NO dejar fijo , editar tambien en bbiregresarClick
        int intFolio;
        int intUsuarioAutorizaCambioPrecio;
        string strNCliente;
        public BindingList<detalleCL> detalle;
        public SurtirPedidos()
        {
            InitializeComponent();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Surtir pedidos";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            SurtirPedidosCL cl = new SurtirPedidosCL();
            cl.strSerie = strSerie;
            gridControlPrincipal.DataSource = cl.PedidosSurtidosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridSurtirPedidos";
            clg.restoreLayout(gridViewPrincipal);
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
        } //LlenarGrid()

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
            gridColumnCantidad.OptionsColumn.ReadOnly = true;
            gridColumnCantidad.OptionsColumn.AllowFocus = false;

            gridColumnArticulo.OptionsColumn.ReadOnly = true;
            gridColumnArticulo.OptionsColumn.AllowFocus = false;

            gridColumnDescripcion.OptionsColumn.ReadOnly = true;
            gridColumnDescripcion.OptionsColumn.AllowFocus = false;

            gridColumnSurtido.OptionsColumn.ReadOnly = true;
            gridColumnSurtido.OptionsColumn.AllowFocus = false;

            gridColumnPorSurtir.OptionsColumn.ReadOnly = true;
            gridColumnPorSurtir.OptionsColumn.AllowFocus = false;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCotizacionesDetalle";
            clg.restoreLayout(gridViewDetalle);
        }

        public class detalleCL
        {
            public string Articulo { get; set; }
            public decimal Cantidad { get; set; }
            public int ArticulosID { get; set; }
            public string Descripcion { get; set; }
            public string Surtido { get; set; }
            public string PorSurtir { get; set; }
            public decimal Surtir { get; set; }
            public string Lote { get; set; }
        }

        private void Nuevo()
        {
            ribbonPageGroup1.Visible = false;
            txtCliente.Text = strNCliente;
            lblFolio.Text = "Surtiendo el pedido:" + strSerie + intFolio.ToString();
            LlenaLista();
            ribbonPageGroup2.Visible = true;

            ribbonStatusBar.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void LimpiaCajas()
        {
            txtCliente.Text = string.Empty;
            lblFolio.Text = string.Empty;
            Inicialisalista();
        }

        private void LlenaLista()
        {
            try
            {
                SurtirPedidosCL cl = new SurtirPedidosCL();
                cl.intFolio = intFolio;
                cl.strSerie = strSerie;
                gridControlDetalle.DataSource = cl.PedidosSurtidosLlenaLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show("LlenaLista: " + ex);
            }

        }


        private string Valida()
        {
            globalCL cl = new globalCL();
            decimal CantSur = 0;
            string dato=string.Empty;

            for (int i=0;i<=gridViewDetalle.RowCount;i++)
            {
                gridViewDetalle.FocusedRowHandle = i;
                gridViewDetalle.UpdateCurrentRow();  //Forza el update del edit aunque no cambien de celda
                

                if (gridViewDetalle.GetRowCellValue(i, "Surtir") != null)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "Surtir").ToString();
                    if (cl.esNumerico(dato))
                    {
                        CantSur += Convert.ToDecimal(dato);
                    }
                }

                

            }

            if (CantSur == 0)
            {
                return "Debe surtir al menos un renglón";
            }


            globalCL clg = new globalCL();


            string result = clg.GM_CierredemodulosStatus(DateTime.Now.Year, DateTime.Now.Month, "VTA");
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";
            }

            return "OK";
        }

        private void Guardar()
        {

            try
            {
                string result=Valida();
                if (result != "OK")
                {
                    MessageBox.Show(result);
                    return;
                }

                System.Data.DataTable dtPedidosSurtidos = new System.Data.DataTable("PedidosSurtidos");
                dtPedidosSurtidos.Columns.Add("Serie", Type.GetType("System.String"));
                dtPedidosSurtidos.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtPedidosSurtidos.Columns.Add("SurtidoSeq", Type.GetType("System.Int32"));
                dtPedidosSurtidos.Columns.Add("Pedidosdetalleseq", Type.GetType("System.Int32"));
                dtPedidosSurtidos.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtPedidosSurtidos.Columns.Add("ArticulofisicoID", Type.GetType("System.Int32"));
                dtPedidosSurtidos.Columns.Add("SerieFactura", Type.GetType("System.String"));
                dtPedidosSurtidos.Columns.Add("Factura", Type.GetType("System.Int32"));
                dtPedidosSurtidos.Columns.Add("Lote", Type.GetType("System.String"));
                dtPedidosSurtidos.Columns.Add("Pallet", Type.GetType("System.String"));
                dtPedidosSurtidos.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtPedidosSurtidos.Columns.Add("Plantaentrega", Type.GetType("System.String"));
                dtPedidosSurtidos.Columns.Add("ObservacionesID", Type.GetType("System.Int32"));
                dtPedidosSurtidos.Columns.Add("UsuariosID", Type.GetType("System.Int32"));


                SurtirPedidosCL cl = new SurtirPedidosCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                result = cl.PedidosSurtidosSiguienteSeq();
                if (result != "OK")
                {
                    MessageBox.Show("No se pudo leer el siguiente ID de pedidos surtidos");
                    return;
                }

                int SeqPedidoSurtido = cl.intSurtidoSeq;
                int intSeq;
                string art;
                
                decimal dCantidad;
                int intArticulosID;
                string strDescripcion, strLote;
                string Fecha = DateTime.Now.ToShortDateString();
                decimal CantSur = 0;
                string dato = string.Empty;

                globalCL clg = new globalCL();

                for (int i = 0; i <= gridViewDetalle.RowCount; i++) // SE AGREGO EL <=
                {
                    CantSur = 0;
                    if (gridViewDetalle.GetRowCellValue(i, "Surtir") != null)
                    {
                        dato = gridViewDetalle.GetRowCellValue(i, "Surtir").ToString();
                        if (clg.esNumerico(dato))
                        {
                            CantSur = Convert.ToDecimal(dato);
                        }
                    }

                    
                    if (CantSur > 0)
                    {

                        intSeq = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Seq"));
                        art = gridViewDetalle.GetRowCellValue(i, "Articulo").ToString();
                        dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Surtir"));
                        intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "ArticulosID"));
                        strLote = Convert.ToString(gridViewDetalle.GetRowCellValue(i, "Lote"));


                        dtPedidosSurtidos.Rows.Add(
                            strSerie,
                            intFolio,
                            SeqPedidoSurtido,
                            intSeq,
                            dCantidad,
                            intArticulosID,
                            "",
                            0,
                            strLote,          // NO GUARDA EL LOTE DE SEQ 1
                            "",                  /*pPallet*/
                            Fecha,                           
                            1,
                            1,
                            globalCL.gv_UsuarioID);
                    }

                }


                

               
                cl.dtd = dtPedidosSurtidos;
                cl.strHora = DateTime.Now.ToShortTimeString();
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                cl.strMaquina = Environment.MachineName;
                result = cl.PedidosSurtidosCrud();
                if (result == "OK")
                {
                    strSerie = cl.strSerie;
                    intFolio = cl.intFolio;
                    MessageBox.Show("Guardado correctamente");
                    Regresar();

                } 
                else
                {
                    MessageBox.Show("Al guardar: " + result);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }

        }//Guardar

        private void Imprime()
        {

            try
            {
                RibbonPagePrint.Visible = true;
                ribbonPage1.Visible = false;
                navigationFrame.SelectedPageIndex = 2;
                ribbonStatusBar.Visible = false;

                reporte();

                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Imprime: " + ex.Message);
            }

        }

        private void reporte()
        {
            try
            {
                Pedidosformatoimpresion report = new Pedidosformatoimpresion();
                report.Parameters["parameter1"].Value = strSerie;
                report.Parameters["parameter2"].Value = intFolio;
                report.Parameters["parameter1"].Visible = false;
                report.Parameters["parameter2"].Visible = false;

                this.documentViewer1.DocumentSource = report;
                report.CreateDocument(false);
                documentViewer1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void Regresar()
        {
            intFolio = 0;
            strSerie = "A";
            strNCliente = string.Empty;
            LimpiaCajas();
            LlenarGrid();
            ribbonPageGroup2.Visible = false;
            ribbonPageGroup1.Visible = true;

            ribbonStatusBar.Visible = true;
            navigationFrame.SelectedPageIndex = 0;
        }

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

        private void bbiSurtir_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                if (intUsuarioAutorizaCambioPrecio != 0)
                    Nuevo();
                else
                    MessageBox.Show("Falta autorización cambio de precio");
            }
        }

        private void gridViewPrincipal_RowClick(object sender, RowClickEventArgs e)
        {
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            strNCliente = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Nombre"));
            intUsuarioAutorizaCambioPrecio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "UsuarioAutorizaCambioPrecio"));
        }

        private void bbiRegresar_ItemClick(object sender, ItemClickEventArgs e)
        {
            Regresar();
        }



        private void bbiActualizar_ItemClick(object sender, ItemClickEventArgs e)
        {
            LlenarGrid();
        }

        private void bbiGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiSurtirTodo_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++) // SE AGREGO EL <=
            {
                gridViewDetalle.FocusedRowHandle = i; //se coloca al iniciar el for

                dCantidad = 0;
                dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "PorSurtir"));

                gridViewDetalle.SetFocusedRowCellValue("Surtir", dCantidad);
               
            }
        }

        private void bbiNoSurtir_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++) // SE AGREGO EL <=
            {
                gridViewDetalle.FocusedRowHandle = i; //se coloca al iniciar el for

                dCantidad = 0;

                gridViewDetalle.SetFocusedRowCellValue("Surtir", dCantidad);

            }
        }

        private void bbiIImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }

            Imprime();
        }

        private void bbiRegresarImp_ItemClick(object sender, ItemClickEventArgs e)
        {

            navigationFrame.SelectedPageIndex = 0;


            ribbonStatusBar.Visible = true;
            ribbonPage1.Visible = true;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText("Home");
            RibbonPagePrint.Visible = false;
        }

        private void bbiImprimirKit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }

            Imprimekit();
        }

        private void reportekit()
        {
            try
            {
                PedidosFormatoImpresionKits report = new PedidosFormatoImpresionKits();
                report.Parameters["parameter1"].Value = strSerie;
                report.Parameters["parameter2"].Value = intFolio;
                report.Parameters["parameter1"].Visible = false;
                report.Parameters["parameter2"].Visible = false;

                this.documentViewer1.DocumentSource = report;
                report.CreateDocument(false);
                documentViewer1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void bbiCerrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridSurtirPedidos";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();            
        }

        private void Imprimekit()
        {
            try
            {
                RibbonPagePrint.Visible = true;
                ribbonPage1.Visible = false;
                navigationFrame.SelectedPageIndex = 2;
                ribbonStatusBar.Visible = false;
                reportekit();
               
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Imprime: " + ex.Message);
            }
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gridViewDetalle.UpdateCurrentRow();  //Forza el update del edit aunque no cambien de celda
        }
    }
}