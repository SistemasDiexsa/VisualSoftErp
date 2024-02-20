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
using VisualSoftErp.Operacion.CxC.Clases;
using DevExpress.XtraGrid.Views.Grid;

namespace VisualSoftErp.Operacion.CxC.Formas
{
    public partial class ValesCadenas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        decimal dPivaG, dPRetIvaG, dPRetIsrG;
        string strSerie;
        int intFolio;
        DateTime dfecha;
        string strNCliente;
        int intClienteID;
        string strMonedasID;
        int intUsuarioID = globalCL.gv_UsuarioID;
        public BindingList<detalleCL> detalle;
        bool blNuevo;
        int intStatus;

        public ValesCadenas()
        {
            InitializeComponent();

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Vales Cadenas";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;


            gridViewDetalle.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewDetalle.OptionsSelection.MultiSelect = true;

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            LlenarGrid();
            CargaCombos();
            txtFecha.EditValue = DateTime.Now;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private string BuscarSerie()
        {
            SerieCL cl = new SerieCL();
            cl.intUsuarioID = intUsuarioID;
            String Result = cl.BuscarSerieporUsuario();
            if (Result == "OK")
            {
                if (cl.strSerie == "")
                { cboSerie.ReadOnly = false; }
                else
                {
                    cboSerie.EditValue = cl.strSerie;
                    cboSerie.ReadOnly = true;
                }

            }
            else
            {
                MessageBox.Show(Result);
            }
            return "";
        } //BuscaSerie

        private void SiguienteID()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            globalCL cl = new globalCL();
            cl.strSerie = "A";
            cl.strDoc = "ValesCadenas";
            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {
                txtFolio.Text = cl.iFolio.ToString();
            }
            else
            {
                MessageBox.Show("SiguienteID :" + result);
            }

        }//SguienteID

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Serie";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Clave";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Des"].Visible = false;
            cboSerie.Properties.NullText = "";

            cl.strTabla = "Clientesrep";
            cboCliente.Properties.ValueMember = "Clave";
            cboCliente.Properties.DisplayMember = "Des";
            cboCliente.Properties.DataSource = cl.CargaCombos();
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCliente.Properties.PopulateColumns();
            cboCliente.Properties.Columns["Clave"].Visible = false;
            cboCliente.Properties.NullText = "Seleccione un cliente";

            cl.strTabla = "ValesConceptos";
            repositoryItemLookUpEditConcepto.ValueMember = "Clave";
            repositoryItemLookUpEditConcepto.DisplayMember = "Des";
            repositoryItemLookUpEditConcepto.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditConcepto.ForceInitialize();
            repositoryItemLookUpEditConcepto.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditConcepto.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }//CargarCombos

        private void LlenarGrid()
        {
            ValesCadenasCL cl = new ValesCadenasCL();
            gridControlPrincipal.DataSource = cl.ValesCadenasGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridValesCadenas";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid() 

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;

            gridColumnSerie.OptionsColumn.ReadOnly = true;
            gridColumnSerie.OptionsColumn.AllowFocus = false;

            gridColumnFactura.OptionsColumn.ReadOnly = true;
            gridColumnFactura.OptionsColumn.AllowFocus = false;

            gridColumnSaldo.OptionsColumn.ReadOnly = true;
            gridColumnSaldo.OptionsColumn.AllowFocus = false;

            gridColumnFormadePago.OptionsColumn.ReadOnly = true;
            gridColumnFormadePago.OptionsColumn.AllowFocus = false;

            gridColumnMoneda.OptionsColumn.ReadOnly = true;
            gridColumnMoneda.OptionsColumn.AllowFocus = false;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridValesCadenas";
            clg.restoreLayout(gridViewDetalle);
        }

        private void Nuevo()
        {
            ribbonPageGroup1.Visible = false;
            blNuevo = true;
            BuscarSerie();
            SiguienteID();
            Inicialisalista();
            cboCliente.EditValue = null;
            ribbonPageGroup2.Visible = true;
            ribbonStatusBar.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
        }

        public class detalleCL
        {
            public string Serie { get; set; }
            public int Factura { get; set; }
            public decimal Saldo { get; set; }
            public string Moneda { get; set; }
            public decimal SuPago { get; set; }
            public string FormadePago { get; set; }
            public int Concepto { get; set; }
            public string Referencia { get; set; }

        }

        private void LimpiaCajas()
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            cboCliente.EditValue = null;
            txtFolio.Text = "";
            txtFecha.Text = DateTime.Now.ToShortTimeString();
        }

        private void DetalleLlenaCajas()
        {
            try
            {
                ValesCadenasCL clv = new ValesCadenasCL();
                clv.intClientesID = Convert.ToInt32(cboCliente.EditValue);

                DepositosCL cl = new DepositosCL();
                cl.intClientesID = Convert.ToInt32(cboCliente.EditValue);
                cl.intUsuarioID = globalCL.gv_UsuarioID;

                string result = cl.DepositosGeneraAntiguedaddesaldos();
                if (result == "OK")
                {
                    if (blNuevo)
                    {
                        gridControlDetalle.DataSource = clv.ValesCadenasDetalleLlenaCajas();
                      
                    }
                    else
                    {
             
                        clv.strSerie = "A";
                        clv.intFolio = intFolio;
                        gridControlDetalle.DataSource = clv.ValesCadenasLlenaCajas();
                        cboCliente.EditValue = Convert.ToInt32(clv.dtd.Rows[0]["ClientesID"]);
                        txtFecha.Text = clv.dtd.Rows[0]["Fecha"].ToString();
                        cboSerie.EditValue = strSerie;
                        txtFolio.Text = intFolio.ToString();
                    }
                    
                    
                }
                else
                {
                    MessageBox.Show(result);
                }


               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }


        }//detalleLlenacajas

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

                System.Data.DataTable dtValesCadenas = new System.Data.DataTable("ValesCadenas");
                dtValesCadenas.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtValesCadenas.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtValesCadenas.Columns.Add("Serie", Type.GetType("System.String"));
                dtValesCadenas.Columns.Add("Factura", Type.GetType("System.Int32"));
                dtValesCadenas.Columns.Add("SuPago", Type.GetType("System.Decimal"));
                dtValesCadenas.Columns.Add("FolioPago", Type.GetType("System.Int32"));
                dtValesCadenas.Columns.Add("RefSuPago", Type.GetType("System.String"));
                dtValesCadenas.Columns.Add("ClientesID", Type.GetType("System.Int32"));
                dtValesCadenas.Columns.Add("FPago", Type.GetType("System.String"));
                dtValesCadenas.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtValesCadenas.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dtValesCadenas.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtValesCadenas.Columns.Add("FechaSistema", Type.GetType("System.DateTime"));
                dtValesCadenas.Columns.Add("ValesConceptosID", Type.GetType("System.Int32"));
                dtValesCadenas.Columns.Add("Status", Type.GetType("System.Int32"));
                dtValesCadenas.Columns.Add("MonedasID", Type.GetType("System.String"));


                //Siguiente folio
                int iFolio = 0;
                globalCL clg = new globalCL();
                clg.strSerie = "";
                clg.strDoc = "ValesCadenas";

                //Manual: --- Definir la condicion necesaria para traer el siguiente ID ---------------------         clg.sCondicion = sCondicion;
                Result = clg.DocumentosSiguienteID();
                if (Result != "OK")
                {
                    MessageBox.Show("No se pudo leer el siguiente folio");
                    return;
                }
                iFolio = clg.iFolio;
                int intRen = 0;
                string dato = String.Empty;

                int intFolio = 0;
                int intSeq = 0;
                int intFactura = 0;
                decimal dSuPago = 0;
                string strMoneda = String.Empty;
                decimal dSaldo = 0;
                int intFolioPago = 0, intValesConceptosID = 0 ;
                string strSerie = String.Empty;
                string strRefSuPago = String.Empty;
                int intClientesID = Convert.ToInt32(cboCliente.EditValue);
                string strFPago = String.Empty;
                DateTime fFecha = Convert.ToDateTime(txtFecha.Text);
                decimal dSubtotal = 0, dIvaNeto=0;
                decimal dIva = 0;
                DateTime fFechaSistema = DateTime.Now.Date;
                int intCpto = 0;

                
                    foreach (int i in gridViewDetalle.GetSelectedRows())
                    {
                        intFolio = iFolio;
                        intSeq = i+1;
                        if (gridViewDetalle.GetRowCellValue(i, "Serie") != null)
                        {
                            strSerie = gridViewDetalle.GetRowCellValue(i, "Serie").ToString();
                        }
                        else { strSerie = ""; }
                        intFactura = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Factura"));
                        dSuPago = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "SuPago"));
                        strMoneda = gridViewDetalle.GetRowCellValue(i, "Moneda").ToString();
                        dSaldo = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Saldo"));
                        dSuPago = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "SuPago"));
                        intFolioPago = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Factura"));
                        strRefSuPago = gridViewDetalle.GetRowCellValue(i, "Referencia").ToString();
                       // intClientesID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "ClientesID"));
                        strFPago = gridViewDetalle.GetRowCellValue(i, "FormadePago").ToString();
                       // fFecha = Convert.ToDateTime(gridViewDetalle.GetRowCellValue(i, "Fecha"));
                        ////dSubtotal = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Subtotal"));
                        dIva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Iva"));
                        //fFechaSistema = Convert.ToDateTime(gridViewDetalle.GetRowCellValue(i, "FechaSistema"));
                        intValesConceptosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Concepto"));
                        intStatus = 1;

                        if (dSuPago <= 0)
                        {
                            MessageBox.Show("Su Pago en el renglòn: " + intSeq.ToString() + " No debe ser menor de cero");
                            return;
                        }
                        else if (dIva > 0)
                        {
                            
                            
                            dSubtotal = Math.Round(dSuPago / (1 + (dIva / 100)),2 );
                            dIvaNeto = Math.Round( dSubtotal * (dIva/100),2);

                        if (dSuPago - dSubtotal - dIvaNeto > 0)
                            dSubtotal = dSuPago - dIvaNeto;
                            
                        }
                        dtValesCadenas.Rows.Add(intFolio, intSeq, strSerie,intFactura, dSuPago, intFolioPago,strRefSuPago, intClientesID, strFPago,
                            fFecha, dSubtotal, dIvaNeto,fFechaSistema, intValesConceptosID, intStatus,strMoneda);

                    }

                ValesCadenasCL cl = new ValesCadenasCL();
                cl.dtm = dtValesCadenas;
                cl.intFolio = iFolio;
                cl.intUsuarioID = intUsuarioID;
                cl.strMaquina = Environment.MachineName;
                Result = cl.ValesCadenasCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado correctamente");
                    Nuevo();

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
            if (cboCliente.EditValue == null)
            {
                return "El campo Cliente no puede ir vacio";
            }
            if (txtFolio.Text.Length == 0)
            {
                return "El campo Folio no puede ir vacio";
            }

            globalCL clg = new globalCL();


            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month, "CXC");
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

            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            intStatus = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status"));
            dfecha = Convert.ToDateTime(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Fecha"));

        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intFolio = 0;
            strNCliente = string.Empty;
            LimpiaCajas();
            LlenarGrid();
            ribbonPageGroup2.Visible = false;
            ribbonPageGroup1.Visible = true;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCargarCxC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            ribbonStatusBar.Visible = true;
            Inicialisalista();
            LimpiaCajas();
            navigationFrame.SelectedPageIndex = 0;

          
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void cboCliente_EditValueChanged(object sender, EventArgs e)
        {
            //    if (cboCliente.EditValue == null)
            //    {
            //    }
            //    else if(blNuevo=false)
            //    {
            //        DetalleLlenaCajas();
            //    }
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridValesCadenas" +
                "";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            globalCL clgd = new globalCL();
            clgd.strGridLayout = "gridValesCadenasDetalle" +
                "";
            result = clgd.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        #region Cancelar,
        private void CierraPopUp()
        {
            popUpCancelar.Visible = false;
            txtLogin.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtRazondecancelacion.Text = string.Empty;
        }

        private void btnAut_Click(object sender, EventArgs e)
        {
            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLogin.Text;
            clU.strClave = txtPassword.Text;
            clU.strPermiso = "Cancelarvalescadenas";
            string result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }

            CierraPopUp();
            
            Cancelarenlabasededatos();

        }

        private void Cancelarenlabasededatos()
        {
            //aqui cancelar
            try
            {

                globalCL clg = new globalCL();


                string result = clg.GM_CierredemodulosStatus(dfecha.Year, dfecha.Month, "CXC");
                if (result == "C")
                {
                    MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                    return;
                }

                ValesCadenasCL cl = new ValesCadenasCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intUsuarioID = intUsuarioID;
                cl.strMaquina = Environment.MachineName;
                cl.strRazon = txtRazondecancelacion.Text;
                result = cl.ValesCadenasCancelar();
                if (result == "OK")
                {
                    MessageBox.Show("Se ah cancelado correctamente");
                    LlenarGrid();
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CambiarStatus: " + ex.Message);
            }
        }

        private void Editar()
        {        
            DetalleLlenaCajas();
            BotonesEdicion();
            blNuevo = false;
        }//ver

        private void BotonesEdicion()
        {
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup2.Visible = true;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCargarCxC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 1;

        }//BotonesEdicion 

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            CierraPopUp();
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            if(intStatus==5)
            {
                MessageBox.Show("Este Folio ya se a cancelado");
            }
            else
            {
                popUpCancelar.Visible = true;
                groupControl3.Text = "Cancelar el folio:" + strSerie + intFolio.ToString();
                txtLogin.Focus();
            }
        }

        private void bbiActualizar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }


        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //string strSeriecfdi;
            //int intFoliocfdi;
            //decimal pIvaL = dPivaG, dRetIvaL = dPRetIvaG, dPRetIsrL = dPRetIsrG;

            //try
            //{
            //    if (e.Column.Name == "gridColumnFolioCfdi")
            //    {
            //        decimal dTotalimpuestosret = 0, dTotalimpuestostras = 0;
            //        strSeriecfdi = Convert.ToString(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "SerieCfdi"));
            //        intFoliocfdi = Convert.ToInt32(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "FolioCfdi"));
            //        NotasdecreditoCL cl = new NotasdecreditoCL();
            //        cl.strSerieCfdi = strSeriecfdi;
            //        cl.intFolioCfdi = intFoliocfdi;
            //        string Result = cl.BuscarImpuestosporSerieyFolioCfdi();
            //        if (Result == "OK")
            //        {
            //            cl.dTotalimpuestostras = dTotalimpuestostras;
            //            cl.dTotalimpuestosret = dTotalimpuestosret;
            //            //mandar llamar el SP
            //            if (cl.dTotalimpuestostras == 0)
            //            {
            //                pIvaL = 0;
            //            }
            //            if (cl.dTotalimpuestosret == 0)
            //            {
            //                dRetIvaL = 0;
            //                dPRetIsrL = 0;
            //            }

            //        }
            //        else
            //        {
            //            MessageBox.Show(Result);
            //        }

            //    }
            //    if (e.Column.Name == "gridColumnCantidad" || e.Column.Name == "gridColumnPrecio")
            //    {
            //        decimal cant = 0;
            //        decimal precio = 0;
            //        decimal imp = 0;
            //        decimal iva = 0;
            //        decimal RetIva = 0;
            //        decimal RetIsr = 0;
            //        decimal NETO = 0;

            //        cant = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Cantidad"));
            //        precio = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Precio"));


            //        imp = Math.Round(cant * precio, 2); //Calculamos el importe y lo redondeamos a dos decimales
            //        iva = Math.Round(imp * (precio * pIvaL / 100), 2);
            //        RetIva = Math.Round(imp * (precio * dRetIvaL / 100), 2);
            //        RetIsr = Math.Round(imp * (precio * dPRetIsrL / 100), 2);
            //        NETO = imp + iva + RetIva + RetIsr;

            //        gridViewDetalle.SetFocusedRowCellValue("Importe", imp);
            //        gridViewDetalle.SetFocusedRowCellValue("Iva", iva);
            //        gridViewDetalle.SetFocusedRowCellValue("RetIva", RetIva);
            //        gridViewDetalle.SetFocusedRowCellValue("RetIsr", RetIsr);
            //        gridViewDetalle.SetFocusedRowCellValue("Neto", NETO);
            //    }



            //}


            //catch (Exception ex)
            //{
            //    MessageBox.Show("griddetallecalculatotales: " + ex);
            //}


        }


        #endregion


        private void bbiCargarCxC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cboCliente.EditValue == null)
            {
                MessageBox.Show("Seleccione un cliente");
            }
            else
            {
                Inicialisalista();
                DetalleLlenaCajas();
                gridControlDetalle.Select();
            }
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


        
    }
}