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
using VisualSoftErp.Operacion.Inventarios.Designers;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.Inventarios.Formas
{
    public partial class Movimientosahojadeproduccion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intFolio;
        string strTipo;
        int intSeq;
        int intRen;
        int intUsuarioID = globalCL.gv_UsuarioID;
        public BindingList<detalleCL> detalle;
        public class tipoCL
        {
            public string Clave { get; set; }
            public string Des { get; set; }
        }
        public class detalleCL
        {
            public string Articulo { get; set; }
            public string Descripcion { get; set; }
            public double Cantidad { get; set; }
            public double Surtir { get; set; }
            public string Observacion { get; set; }
        }

        public Movimientosahojadeproduccion()
        {
            InitializeComponent();

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Movimientos a hoja de producción";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            txtFecha.Text = DateTime.Now.ToShortDateString();
            LlenarGrid();
            CargaCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            BFMovsCL cl = new BFMovsCL();
            cl.intAño = DateTime.Now.Year;
            cl.intMes = 0; // DateTime.Now.Month;
            gridControlPrincipal.DataSource = cl.BFMovsGrid();
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridBFMovs";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Almacenes";
            cboAlmacen.Properties.ValueMember = "Clave";
            cboAlmacen.Properties.DisplayMember = "Des";
            cboAlmacen.Properties.DataSource = cl.CargaCombos();
            cboAlmacen.Properties.ForceInitialize();
            cboAlmacen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacen.Properties.PopulateColumns();
            cboAlmacen.Properties.Columns["Clave"].Visible = false;
            cboAlmacen.ItemIndex = 0;

            List<tipoCL> tipoL = new List<tipoCL>();
            tipoL.Add(new tipoCL() { Clave = "S", Des = "Salida de MP" });
            tipoL.Add(new tipoCL() { Clave = "E", Des = "Entrada de PT" });
            tipoL.Add(new tipoCL() { Clave = "N", Des = "Entrada a sobrante de MP" });
            cboMovmiento.Properties.ValueMember = "Clave";
            cboMovmiento.Properties.DisplayMember = "Des";
            cboMovmiento.Properties.DataSource = tipoL;
            cboMovmiento.Properties.ForceInitialize();
            cboMovmiento.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMovmiento.Properties.PopulateColumns();
            cboMovmiento.Properties.Columns["Clave"].Visible = false;
            cboMovmiento.EditValue = "S";
        }

        private void Inicialisalista()
        {

            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
            if (txtSeq.Text.Length == 0)
            {
                gridColumnArticulo.OptionsColumn.ReadOnly = true;
                gridColumnCantidad.OptionsColumn.ReadOnly = true;
                gridColumnDescripcion.OptionsColumn.ReadOnly = true;
                gridColumnObservacion.OptionsColumn.ReadOnly = true;

            }
            else
            {
                intSeq = Convert.ToInt32(txtSeq.Text);
            }
            if (intSeq == 1)
            {
                gridColumnArticulo.OptionsColumn.ReadOnly = true;
                gridColumnCantidad.OptionsColumn.ReadOnly = true;
                gridColumnDescripcion.OptionsColumn.ReadOnly = true;
                gridColumnObservacion.OptionsColumn.ReadOnly = false;

            }
            if (intSeq > 1)
            {
                gridColumnArticulo.OptionsColumn.ReadOnly = true;
                gridColumnCantidad.OptionsColumn.ReadOnly = false;
                gridColumnDescripcion.OptionsColumn.ReadOnly = true;
                gridColumnObservacion.OptionsColumn.ReadOnly = false;
            }

        }

        private void BotonesEdicion()
        {
            //LimpiaCajas();
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup2.Visible = true;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void LimpiaCajas()
        {
            //txtFolioHP.Text = string.Empty;
            cboMovmiento.ItemIndex = 0;
            cboAlmacen.ItemIndex = 0;
            txtSeq.Text = string.Empty;
            txtFecha.Text = DateTime.Now.ToShortDateString();
            lblCantidadPT.Text = string.Empty;
            lblFactorUm2PT.Text = string.Empty;
            lblNom.Text = string.Empty;
            gridControlDetalle.DataSource = null;
        }

        private void BuscarFolio()

        {
            String Result = "";
            try
            {
                BFCL cl = new BFCL();
                cl.intFolio = Convert.ToInt32(txtFolioHP.Text);
                Result = cl.BFLlenaCajas();
                if (Result == "OK")
                {
                    lblCantidadPT.Text = cl.decCantidad.ToString();
                    lblFactorUm2PT.Text = cl.intFactorUm2.ToString();
                    lblNom.Text = cl.strNombre + " Factor:" + cl.intFactorUm2.ToString();

                    // STATUS == 5 ES STATUS CANCELADO
                    if (cl.strStatus == "5")
                    {
                        bbiGuardar.Enabled = false;
                        MessageBox.Show("Hoja de Producción con status cancelado");
                    }
                    else
                        bbiGuardar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("El Folio:" + intFolio.ToString() + " no existe en la tabla BF, ingrese un folio existente");
                    txtFolioHP.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Result);
            }
        }

        private void BuscarSeqMovs()
        {
            string Result = "";
            try
            {


                if (cboMovmiento.EditValue == null) { }

                else
                {
                    BFMovsCL cl = new BFMovsCL();
                    cl.intFolio = Convert.ToInt32(txtFolioHP.Text);
                    cl.strTipo = cboMovmiento.EditValue.ToString();
                    cl.intSeq = Convert.ToInt32(txtSeq.Text);
                    cl.intCantidad = Convert.ToInt32(lblFactorUm2PT.Text);
                    Result = cl.BFMovsLlenaCajas();
                    if (Result == "OK")
                    {
                        if (txtSeq.Text.Length == 0)
                        {
                            txtSeq.Text = "1";
                            GridDetalleBFMP();
                        }
                        else
                        {
                            GridDetalleBFMP();
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Result);
            }
        }

        //private void GridDetalleMovs()
        //{
        //    //validaciones si txtseq es vacio, o con 0
        //    int intSeq = 0;
        //    if (txtSeq.Text == "") { }
        //    if (txtSeq.Text.Length != 0) { intSeq = Convert.ToInt32(txtSeq.Text); }
        //    if (intSeq != 1)
        //    {
        //        Inicialisalista();
        //        BFMovsCL cl = new BFMovsCL();
        //        cl.intFolio = Convert.ToInt32(txtFolioHP.Text);
        //        cl.strTipo = cboMovmiento.EditValue.ToString();
        //        gridControlDetalle.DataSource = cl.BFMovsGridDETALLE();
        //    }
        //    else if (intSeq == 1)
        //    {
        //        GridDetalleBFMP();
        //    }

        //}

        private void GridDetalleBFMP()
        {
            Inicialisalista();
            BFMovsCL cl = new BFMovsCL();
            cl.intFolio = Convert.ToInt32(txtFolioHP.Text);
            cl.intSeq = Convert.ToInt32(txtSeq.Text);
            cl.strTipo = cboMovmiento.EditValue.ToString();
            gridControlDetalle.DataSource = cl.BFMPGridDETALLE();

            //if (Convert.ToInt32(txtSeq.Text) !=1)
            //{
            //    for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++)
            //    {
            //        gridViewDetalle.FocusedRowHandle = i;
            //        gridViewDetalle.SetFocusedRowCellValue("Cantidad", 0);
            //    }
            //}
        }

        private void FolioySeq()
        {
            string strStatus = "";
           
                BuscarFolio();            
        }

        private string Valida()
        {
            if (txtFolioHP.Text.Length == 0)
            {
                return "El campo FolioHP no puede ir vacio";
            }
            if (cboMovmiento.Text.Length == 0)
            {
                return "El campo Tipo de movimiento no puede ir vacio";
            }
            if (txtSeq.Text=="0")
            {
                return "Seq no puede ser cero";
            }
            if (cboAlmacen.EditValue == null)
            {
                return "El campo almacen no puede ir vacio";
            }
            if (txtFecha.Text.Length == 0)
            {
                return "El campo fecha no puede ir vacio";
            }

            globalCL clg = new globalCL();
           

            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month, "INV");
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
                String Result = Valida();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }
                System.Data.DataTable dtBFMovs = new System.Data.DataTable("BFMovs");
                dtBFMovs.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtBFMovs.Columns.Add("Tipo", Type.GetType("System.String"));
                dtBFMovs.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtBFMovs.Columns.Add("Ren", Type.GetType("System.Int32"));
                dtBFMovs.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtBFMovs.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtBFMovs.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtBFMovs.Columns.Add("Hora", Type.GetType("System.String"));
                dtBFMovs.Columns.Add("Obs", Type.GetType("System.String"));
                dtBFMovs.Columns.Add("Status", Type.GetType("System.String"));
                dtBFMovs.Columns.Add("AlmacenesID", Type.GetType("System.Int32"));
                dtBFMovs.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtBFMovs.Columns.Add("UsuarioCancelaID", Type.GetType("System.Int32"));
                dtBFMovs.Columns.Add("FechaCancelacion", Type.GetType("System.DateTime"));
                dtBFMovs.Columns.Add("RazonCancelacion", Type.GetType("System.String"));
                dtBFMovs.Columns.Add("FechaReal", Type.GetType("System.DateTime"));

                intFolio = Convert.ToInt32(txtFolioHP.Text);
                strTipo = cboMovmiento.EditValue.ToString();
                intSeq = Convert.ToInt32(txtSeq.Text);

                string dato = String.Empty;

                int intArticulosID = 0;
                decimal dCantidad = 0;
                DateTime fFecha = Convert.ToDateTime(txtFecha.Text);
                string strHora = DateTime.Now.ToShortTimeString();
                string strObs = String.Empty;
                string strStatus = String.Empty;
                int intAlmacenesID = Convert.ToInt32(cboAlmacen.EditValue);
                int intUsuarioCancelaID = 0;
                DateTime fFechaCancelacion = Convert.ToDateTime(DateTime.Now);
                string strRazonCancelacion = "";

                decimal TotalSurtido = 0;

                for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "Articulo").ToString();
                    if (dato.Length > 0)
                    {
                        if (gridViewDetalle.GetRowCellValue(i, "Surtir") != null)
                        {

                            if (Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Surtir")) != 0)
                            {
                                strObs = gridViewDetalle.GetRowCellValue(i, "Observacion").ToString();
                                intRen = i + 1;
                                intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "ArticulosID"));
                                dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Surtir"));
                                if (gridViewDetalle.GetRowCellValue(i, "Observacion") != null)
                                {
                                    strObs = gridViewDetalle.GetRowCellValue(i, "Observacion").ToString();
                                }
                                else
                                {
                                    MessageBox.Show("el campo observaciones del renglón:" + intRen.ToString() + " no puede ir vacio");
                                }
                                strStatus = "R";

                                TotalSurtido += dCantidad;

                                dtBFMovs.Rows.Add(intFolio, strTipo, intSeq, intRen,
                                    intArticulosID, dCantidad,
                                    fFecha, strHora, strObs, strStatus,
                                    intAlmacenesID, intUsuarioID,
                                    intUsuarioCancelaID, fFechaCancelacion,
                                    strRazonCancelacion,
                                    DateTime.Now);
                            }
                        }
                        else
                        {
                            MessageBox.Show("");
                        }
                    }
                }

                if (TotalSurtido==0)
                {
                    MessageBox.Show("Debe surtir al menos un renglón");
                    return;
                }

                BFMovsCL cl = new BFMovsCL();
                cl.dtm = dtBFMovs;
                cl.intFolio = intFolio;
                cl.strTipo = strTipo;
                cl.intSeq = intSeq;
                cl.intUsuarioID = intUsuarioID;
                cl.strMaquina = Environment.MachineName;
                Result = cl.BFMovsCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    Imprime();
                    LimpiaCajas();

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

        private void Imprime()
        {
            int impDirecto = 0;
            try
            {

                string sTipo = string.Empty;
                switch (strTipo)
                {
                    case "Salida de MP":
                        sTipo = "S";
                        break;
                    case "Entrada de PT":
                        sTipo = "E";
                        break;
                    case "Devolución de MP":
                        sTipo = "N";
                        break;
                }

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

                MovimientosahojadeproduccionDesigner rep = new MovimientosahojadeproduccionDesigner();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = intFolio;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = sTipo;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = intSeq;
                    rep.Parameters["parameter3"].Visible = false;
                    
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = intFolio;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = strTipo;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = intSeq;
                    rep.Parameters["parameter3"].Visible = false;
                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    RibbomBar.MergeOwner.SelectedPage = RibbomBar.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                    navigationFrame.SelectedPageIndex = 2;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void Editar()
        {
            LlenaCajas();
   

            BotonesEdicion();
        }

        private void LlenaCajas()
        {
            txtSeq.Text = intSeq.ToString();
            cboMovmiento.EditValue = strTipo;
            txtFolioHP.Text = intFolio.ToString();
            cboAlmacen.EditValue = intAlmacen;
            txtFecha.Text = strFecha;
            GridDetalleBFMP();

        }

        private void CierraPopUp()
        {
            popUpCancelar.Visible = false;
            txtLogin.Text = string.Empty;
            txtPassword.Text = string.Empty;
           // txtRazondecancelacion.Text = string.Empty;
        }

        private void Cancelarenlabasededatos()
        {
            //aqui cancelar
            try
            {
                BFMovsCL cl = new BFMovsCL();
                cl.intFolio = intFolio;
                cl.strTipo = strTipo;
                cl.intSeq = intSeq;
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                cl.strMaquina = Environment.MachineName;
                cl.strRazon = Convert.ToString(txtRazondecancelacion.Text);
                string result = cl.BFMovsCancelar();
                if (result == "OK")
                {
                    MessageBox.Show("Cancelado Correctamente");
                    LlenarGrid();
                    txtRazondecancelacion.Text = string.Empty;
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

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtFolioHP.Text = intFolio.ToString();
            BotonesEdicion();
            Inicialisalista();
            intFolio = 0;
            strTipo = "";
            intSeq = 0;
            intRen = 0;

            gridColumnArticulo.OptionsColumn.ReadOnly = true;
            gridColumnCantidad.OptionsColumn.ReadOnly = true;
            gridColumnSurtir.OptionsColumn.ReadOnly = false;
            gridColumnDescripcion.OptionsColumn.ReadOnly = true;
            gridColumnObservacion.OptionsColumn.ReadOnly = false;

            txtFolioHP.Select();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageGroup1.Visible = true;
            ribbonPageGroup2.Visible = false;
            LlenarGrid();
            intFolio = 0;
            navigationFrame.SelectedPageIndex = 0;

        }

      

        private void cboMovmiento_EditValueChanged(object sender, EventArgs e)
        {
            BuscaSeq();
        }


        private void BuscaSeq()
        {
            try
            {
                BFMovsCL cl = new BFMovsCL();

                globalCL clG = new globalCL();
                if (!clG.esNumerico(txtFolioHP.Text))
                    return;

                cl.intFolio = Convert.ToInt32(txtFolioHP.Text);
                cl.strTipo = cboMovmiento.EditValue.ToString();
                string result = cl.BFMovsSigID();
                if (result=="OK")
                {
                    txtSeq.Text = cl.intSeq.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Buscasig: " + ex.Message);
            }

        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void txtSeq_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }

            Imprime();
        }

        int intAlmacen;
        string strFecha;
        private void gridViewPrincipal_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //string strStatus = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Estado"));
            strTipo = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Tipo"));
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            intSeq = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Seq"));
            intAlmacen = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "AlmacenesID"));
            strFecha = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Fecha"));

        }

        private void bbiRegresarImp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RibbomBar.MergeOwner.SelectedPage = RibbomBar.MergeOwner.TotalPageCategory.GetPageByText(ribbonPage1.Text);
   
            LlenarGrid();
            intFolio = 0;
            ribbonPageGroup1.Visible = true;
            ribbonPageGroup2.Visible = false;
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }

            Editar();
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                popUpCancelar.Visible = true;
                groupControl3.Text = "Cancelar el folio:" + intFolio.ToString();
                txtLogin.Focus();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            CierraPopUp();
        }

        private void btnAut_Click(object sender, EventArgs e)
        {
            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLogin.Text;
            clU.strClave = txtPassword.Text;
            clU.strPermiso = "Cancelardepositos";
            string result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }

            CierraPopUp();
            //if (timbrado == 1)
            //{
            //    Cancelar();
            //}
            //else
            //{
            Cancelarenlabasededatos();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridBFMovs" +
                "";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            globalCL clgd = new globalCL();
            clgd.strGridLayout = "gridBFMovsDet" +
                "";
            result = clgd.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void txtSeq_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                //FolioySeq();
                LlenaCajasSeq();
            }
        }

        private void LlenaCajasSeq()
        {
            try
            {
                globalCL clG = new globalCL();
                if (!clG.esNumerico(txtSeq.Text))
                {
                    MessageBox.Show("Teclee el seq");
                    return;
                }

                BFMovsCL cl = new BFMovsCL();
                cl.intFolio = Convert.ToInt32(txtFolioHP.Text);
                cl.strTipo = cboMovmiento.EditValue.ToString();
                cl.intSeq = Convert.ToInt32(txtSeq.Text);
                cl.intCantidad = Convert.ToInt32(lblCantidadPT.Text);
                cl.intFactorUm2 = Convert.ToInt32(lblFactorUm2PT.Text);

                gridControlDetalle.DataSource = cl.LlenaCajasBfMovs();

                if (gridViewDetalle.RowCount > 0)
                {
                    int ren = Convert.ToInt32(gridViewDetalle.GetRowCellValue(0, "Ren"));
                    if (ren>0)                    
                        bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    else
                        bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFolioHP_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Return)
            {
                globalCL clG = new globalCL();
                if (!clG.esNumerico(txtFolioHP.Text))
                {
                    return;
                }
                BuscarFolio();
            }
               
        }

        private void txtFolioHP_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}