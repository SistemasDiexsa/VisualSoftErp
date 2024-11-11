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
using DevExpress.XtraGrid.Views.Grid;

namespace VisualSoftErp.Operacion.CxP.Formas
{
    public partial class AplicarAnticiposCxP : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int idAnt = 0;
        //int idCxP = 0;
        int intFolioCxP = 0;
        int tipocargo = 0;
        int intChequera = 0;
        int intUsuariocancelo = 0;
        decimal decImpProv = 0;

        public AplicarAnticiposCxP()
        {
            InitializeComponent();

            gridViewAnticipos.OptionsView.ShowAutoFilterRow = true;
            gridViewAnticipos.OptionsBehavior.ReadOnly = true;
            gridViewAnticipos.OptionsBehavior.Editable = false;

            gridViewCxP.OptionsView.ShowAutoFilterRow = true;
            gridViewCxP.OptionsBehavior.ReadOnly = true;
            gridViewCxP.OptionsBehavior.Editable = false;
            
            gridViewAnticipos.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewAnticipos.OptionsSelection.MultiSelect = false;

            gridViewCxP.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewCxP.OptionsSelection.MultiSelect = false;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            CargaCombos();
            LlenaGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenaGrid()
        {
            AnticiposCxPCL cl = new AnticiposCxPCL();
            gridControlPrincipal.DataSource = cl.AnticiposAplicadosGrid();
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();

            if (globalCL.gv_AnticiposOrigen == "CXP")
            {
                cl.strTabla = "Proveedoresrep";
                cboProveedoresID.Properties.ValueMember = "Clave";
                cboProveedoresID.Properties.DisplayMember = "Des";
                cboProveedoresID.Properties.DataSource = cl.CargaCombos();
                cboProveedoresID.Properties.ForceInitialize();
                cboProveedoresID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboProveedoresID.Properties.PopulateColumns();
                cboProveedoresID.Properties.Columns["Clave"].Visible = false;
                cboProveedoresID.Properties.NullText = "Seleccione un proveedor";
            }
            else
            {
                lblProv.Text = "Cliente";
                cl.strTabla = "Clientesrep";
                cboProveedoresID.Properties.ValueMember = "Clave";
                cboProveedoresID.Properties.DisplayMember = "Des";
                cboProveedoresID.Properties.DataSource = cl.CargaCombos();
                cboProveedoresID.Properties.ForceInitialize();
                cboProveedoresID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboProveedoresID.Properties.PopulateColumns();
                cboProveedoresID.Properties.Columns["Clave"].Visible = false;
                cboProveedoresID.Properties.NullText = "Seleccione un cliente";
            }
             
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void botonesEdicion()
        {
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCargarInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiPreview.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

        }

        private void guardar()
        {
            try
            {

                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtAplAplicar.Text))
                    txtAplAplicar.Text = "0";

                if (Convert.ToDecimal(txtAplAplicar.Text)<=0)
                {
                    MessageBox.Show("El importe a aplicar debe ser mayor a cero");
                    return;
                }

                string result;
                if (globalCL.gv_AnticiposOrigen == "CXP")
                    result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(dFecha.Text).Year, Convert.ToDateTime(dFecha.Text).Month, "CXP");
                else
                    result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(dFecha.Text).Year, Convert.ToDateTime(dFecha.Text).Month, "CXC");

                if (result == "C")
                {
                    MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                    return;

                }

                decimal ImpCheq = 0;

                AnticiposCxPCL cl = new AnticiposCxPCL();
                cl.strSerie = "";
                cl.intProveedoresID = Convert.ToInt32(cboProveedoresID.EditValue);
                cl.fFecha = Convert.ToDateTime(dFecha.Text);
                cl.intChequerasID = intChequera;

                if (txtAplMonedaAnt.Text == "MXN" && txtAplMonedaApl.Text == "USD")
                    ImpCheq = (Convert.ToDecimal(txtAplAplicar.Text) * Convert.ToDecimal(txtTC.Text));
                else
                    ImpCheq = Convert.ToDecimal(txtAplAplicar.Text);

                cl.dImportechequera = ImpCheq; // Convert.ToDecimal(txtAplAplicar.Text);
                cl.strReferencia = txtAplFac.Text;
                cl.strMonedaCehquera = txtAplMonedaAnt.Text;
                cl.dTipodecambio = Convert.ToDecimal(txtTC.Text);
                cl.strMonedaProveedor = txtAplMonedaApl.Text;
                cl.dImporteProveedor = Convert.ToDecimal(txtAplAplicar.Text);
                cl.strDescripcion = "APLICACION";
                cl.strStatus = "1";
                cl.strcFormaPago = "03";
                cl.intCxPAnticiposID = 0;
                cl.intFolioAnt = txtAplFolio.Text;
                cl.intTipoCargo = tipocargo;
                if (globalCL.gv_AnticiposOrigen == "CXP")
                    cl.intFolioFac = intFolioCxP;
                else
                    cl.intFolioFac = Convert.ToInt32(txtAplFac.Text);

                result = cl.AplicaAnticiposCrud();

                if (result == "OK")
                {
                    limpiaCajas();
                    MessageBox.Show("Guardado correctamente");
                   
                }
                else
                {
                    MessageBox.Show("Al guardar:" + result);
                }
                
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cargaAnticiposPorAplicar()
        {
            AnticiposCxPCL cl = new AnticiposCxPCL();
            cl.intProveedoresID = Convert.ToInt32(cboProveedoresID.EditValue);
            gridControlAnticipos.DataSource = cl.AnticiposCxPPorAplicar();
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            botonesEdicion();
            limpiaCajas();
            navigationFrame.SelectedPageIndex = 1;

        }

        private void limpiaCajas()
        {
            cboProveedoresID.ItemIndex = 0;
            dFecha.Text = DateTime.Now.ToShortDateString();
            txtTC.Text = string.Empty;
            gridControlAnticipos.DataSource = null;
            gridControlCxP.DataSource = null;   

        }

        private void bbiCargarInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cargaAnticiposPorAplicar();
            Cargarfacturas();
            gridControlAnticipos.Focus();
            bbiAplicar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void Cargarfacturas()
        {
            if (globalCL.gv_AnticiposOrigen == "CXP")
            {
                PagosCxPCL cl = new PagosCxPCL();
                cl.intProveedoresID = Convert.ToInt32(cboProveedoresID.EditValue);
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                string result = cl.PagosGeneraAntiguedaddesaldos();
                if (result == "OK")
                {
                    gridControlCxP.DataSource = cl.PagoscxpCargaFacturas();
                    //if (gridViewDetalle.RowCount > 1)
                    //{
                    //    cboProveedoresID.Enabled = false;
                    //}
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            else
            {
      

                DepositosCL cl = new DepositosCL();
                cl.intClientesID = Convert.ToInt32(cboProveedoresID.EditValue);
                cl.intCliente2 = 0;
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                string result = cl.DepositosGeneraAntiguedaddesaldos();
                if (result == "OK")
                {
                    gridControlCxP.DataSource = cl.DepositosCargaFacturas();
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            


        }//Cargafacturas     

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtAplProv.Text = string.Empty;
            txtAplFolio.Text = string.Empty;
            txtAplImp.Text = string.Empty;
            txtAplMonedaAnt.Text = string.Empty;
            txtAplFac.Text = string.Empty;
            txtAplSaldo.Text = string.Empty;
            txtAplMonedaApl.Text = string.Empty;
            txtTC.Text = string.Empty;
            txtAplAplicar.Text = string.Empty;
            bbiCargarInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            popupContainerControl1.Hide();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            AplicaAnticipo();
        }
        
        private void AplicaAnticipo()
        {
           
            popupContainerControl1.Hide();
            guardar();
        }

        private void bbiAplicar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CargaAplicacion();
        }

        private void CargaAplicacion()
        {
            try
            {
                if (txtAplFac.Text.Length == 0)
                {
                    MessageBox.Show("Seleccione una factura en la parte inferior");
                    return;
                }

                txtAplProv.Text = cboProveedoresID.Text;
                //txtTC.Text = "0";
                Convierte();
                popupContainerControl1.Show();
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewAnticipos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            txtAplFolio.Text = gridViewAnticipos.GetRowCellValue(gridViewAnticipos.FocusedRowHandle, "Folio").ToString();
            txtAplImp.Text = gridViewAnticipos.GetRowCellValue(gridViewAnticipos.FocusedRowHandle, "PorAplicar").ToString();
            txtAplMonedaAnt.Text = gridViewAnticipos.GetRowCellValue(gridViewAnticipos.FocusedRowHandle, "MonedaBanco").ToString();
            intChequera = Convert.ToInt32(gridViewAnticipos.GetRowCellValue(gridViewAnticipos.FocusedRowHandle, "Chequera"));
            decImpProv = Convert.ToDecimal(gridViewAnticipos.GetRowCellValue(gridViewAnticipos.FocusedRowHandle, "ImporteProveedor")) - Convert.ToDecimal(gridViewAnticipos.GetRowCellValue(gridViewAnticipos.FocusedRowHandle, "Aplicado")); ;
            //txtTC.Text = gridViewAnticipos.GetRowCellValue(gridViewAnticipos.FocusedRowHandle, "Tipodecambio").ToString();
        }

        private void gridViewCxP_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (globalCL.gv_AnticiposOrigen=="CXP")
            {
                txtAplFac.Text = gridViewCxP.GetRowCellValue(gridViewCxP.FocusedRowHandle, "Referencia").ToString();
                intFolioCxP = Convert.ToInt32(gridViewCxP.GetRowCellValue(gridViewCxP.FocusedRowHandle, "Folio").ToString());
            }
            else
            {
                txtAplFac.Text = gridViewCxP.GetRowCellValue(gridViewCxP.FocusedRowHandle, "Factura").ToString();
                intFolioCxP = Convert.ToInt32(gridViewCxP.GetRowCellValue(gridViewCxP.FocusedRowHandle, "ID").ToString());
            }
            
            txtAplSaldo.Text = gridViewCxP.GetRowCellValue(gridViewCxP.FocusedRowHandle, "Importe").ToString();
            txtAplMonedaApl.Text = gridViewCxP.GetRowCellValue(gridViewCxP.FocusedRowHandle, "Moneda").ToString();
            tipocargo = Convert.ToInt32(gridViewCxP.GetRowCellValue(gridViewCxP.FocusedRowHandle, "TipoMov").ToString());
            

            if (txtAplMonedaAnt.Text == txtAplMonedaApl.Text)
            {
                txtAplAplicar.Text = txtAplImp.Text;
                txtTC.Text = "1";
            }
        }

        private void Convierte()
        {
            globalCL clg = new globalCL();

            if (txtAplMonedaAnt.Text != txtAplMonedaApl.Text)
            {                
                if (!clg.esNumerico(txtTC.Text))
                {
                    txtTC.Text = "0";
                }

                if (txtAplMonedaAnt.Text == "MXN")
                {
                    if (txtTC.Text=="0")
                    {
                        if (!clg.esNumerico(txtAplSaldo.Text))
                            txtAplSaldo.Text = "0";

                        if (Convert.ToDecimal(txtAplSaldo.Text) <= decImpProv)
                            txtAplAplicar.Text = txtAplSaldo.Text;
                        else
                            txtAplAplicar.Text = decImpProv.ToString();
                        if (Convert.ToDecimal(txtAplSaldo.Text)>0)
                            txtTC.Text = Math.Round(Convert.ToDecimal(txtAplImp.Text) / decImpProv, 4).ToString();
                    }
                    else
                        txtAplAplicar.Text = Math.Round(Convert.ToDecimal(txtAplImp.Text) / Convert.ToDecimal(txtTC.Text), 2).ToString();
                }
            } 
            else
            {
                //misma moneda
                txtAplAplicar.Text = txtAplSaldo.Text;
                txtTC.Text = "1";

               
            }
        }

        private void btnConvertir_Click(object sender, EventArgs e)
        {
            Convierte();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            guardar();
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            popUpCancelar.Show();
        }

        private void CierraPopUp()
        {
            popUpCancelar.Hide();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            CierraPopUp();
        }

        private void Cancelar()
        {

            globalCL clg = new globalCL();
            string result;

            if (globalCL.gv_AnticiposOrigen == "CXP")
                result = clg.GM_CierredemodulosStatus(DateTime.Now.Year, DateTime.Now.Month, "CXP");
            else
                result = clg.GM_CierredemodulosStatus(DateTime.Now.Year, DateTime.Now.Month, "CXC");

            if (result == "C")
            {
                MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                return;

            }

            AnticiposCxPCL cl = new AnticiposCxPCL();
            cl.intAnticiposCxPID = idAnt;
            cl.intUsuCan = intUsuariocancelo;
            cl.strRazon = txtRazoncancelacion.Text;
            result = cl.AnticiposCxPAplicacionCancelar();

            if (result == "OK")
            {
                MessageBox.Show("Cancelado correctamente");
                LlenaGrid();
            }
                
            else
                MessageBox.Show(result);

        }

        private void btnAut_Click(object sender, EventArgs e)
        {
            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLogin.Text;
            clU.strClave = txtPassword.Text;
            clU.strPermiso = "cancelaraplicacionanticipos";
            string result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }
            intUsuariocancelo = clU.intUsuariosID;
            CierraPopUp();
            Cancelar();
        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            idAnt = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ID"));
            string sSt = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status").ToString();
            if (sSt == "Cancelado")
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            else
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string _mark = (string)view.GetRowCellValue(e.RowHandle, "Status");

            if (_mark == "Cancelado")
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Red;
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void txtAplFac_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtAplMonedaAnt_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCargarInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiPreview.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlPrincipal.ShowRibbonPrintPreview();
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}