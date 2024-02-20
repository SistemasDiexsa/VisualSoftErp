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
    public partial class AnticiposCxP : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string Monedadelachequera;
        int AntID;

        public AnticiposCxP()
        {
            InitializeComponent();

            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtReferencia.Properties.MaxLength = 20;
            txtReferencia.EnterMoveNextControl = true;

            txtDescripcion.Properties.MaxLength = 200;
            txtDescripcion.EnterMoveNextControl = true;

            gridView1.OptionsView.ShowAutoFilterRow = true;
            
            
            CargaCombos();
            LlenarGrid();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();

            if (globalCL.gv_AnticiposOrigen == "CXP")
            {
                lblProvoCliente.Text = "Proveedor";
                groupControl2.Text = "Se le dió al proveedor";
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
                lblProvoCliente.Text = "Cliente";
                groupControl2.Text = "Nos dió el cliente";
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



            cl.strTabla = "cyb_Chequeras";
            cboChequerasID.Properties.ValueMember = "Clave";
            cboChequerasID.Properties.DisplayMember = "Des";
            cboChequerasID.Properties.DataSource = cl.CargaCombos();
            cboChequerasID.Properties.ForceInitialize();
            cboChequerasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboChequerasID.Properties.PopulateColumns();
            cboChequerasID.Properties.Columns["Clave"].Visible = false;
            cboChequerasID.Properties.Columns["MonedasID"].Visible = false;
            cboChequerasID.Properties.Columns["Rfcbanco"].Visible = false;
            cboChequerasID.ItemIndex = 0;

            cl.strTabla = "Monedas";
            cboMonedaProveedor.Properties.ValueMember = "Clave";
            cboMonedaProveedor.Properties.DisplayMember = "Des";
            cboMonedaProveedor.Properties.DataSource = cl.CargaCombos();
            cboMonedaProveedor.Properties.ForceInitialize();
            cboMonedaProveedor.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedaProveedor.Properties.PopulateColumns();
            cboMonedaProveedor.Properties.Columns["Clave"].Visible = false;
            cboMonedaProveedor.Properties.NullText = "Seleccione una moneda";

            cl.strTabla = "Formadepago";
            cboFormadePago.Properties.ValueMember = "Clave";
            cboFormadePago.Properties.DisplayMember = "Des";
            cboFormadePago.Properties.DataSource = cl.CargaCombos();
            cboFormadePago.Properties.ForceInitialize();
            cboFormadePago.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFormadePago.Properties.PopulateColumns();
            cboFormadePago.Properties.Columns["Clave"].Visible = false;
            cboFormadePago.Properties.NullText = "Seleccione una forma de pago";
        }

        private void LimpiaCajas()
        {
            cboProveedoresID.EditValue = null;
            txtFecha.Text = DateTime.Now.ToShortDateString();
            cboChequerasID.ItemIndex = 0;
            txtImporte.Text = string.Empty; txtImporteProvee.Text = string.Empty;
            txtReferencia.Text = string.Empty;
            cboMonedaProveedor.EditValue = null;
            txtTipodecambio.Text = string.Empty;
            txtDescripcion.Text = string.Empty;

            cboFormadePago.EditValue = "03";

        }

        private string Valida()
        {
            string origen;
            globalCL clg = new globalCL();
            if (globalCL.gv_AnticiposOrigen == "CXP")
            {
                if (cboProveedoresID.EditValue == null)
                {
                    return "El campo Proveedores no puede ir vacio";
                }
                origen = "CXP";
            }
            else
            {
                if (cboProveedoresID.EditValue == null)
                {
                    return "El campo Clientes no puede ir vacio";
                }
                origen = "CXC";
            }
            if (txtFecha.Text.Length == 0)
            {
                return "El campo Fecha no puede ir vacio";
            }
            if (cboChequerasID.EditValue == null)
            {
                return "El campo ChequerasID no puede ir vacio";
            }
            if (txtImporte.Text.Length == 0)
            {
                return "El campo Importechequera no puede ir vacio";
            }
            if (txtReferencia.Text.Length == 0)
            {
                return "El campo Referencia no puede ir vacio";
            }
            if (cboMonedaProveedor.EditValue == null)
            {
                return "El campo Moneda del proveedor no puede ir vacio";
            }
            if (!clg.esNumerico(txtTipodecambio.Text))
            {
                txtTipodecambio.Text = "1";
            }
            if (cboMonedaProveedor.Text.Length == 0)
            {
                return "El campo MonedaProveedor no puede ir vacio";
            }
            if (txtDescripcion.Text.Length == 0)
            {
                return "El campo Descripcion no puede ir vacio";
            }

            if (cboFormadePago.EditValue == null)
            {
                return "El campo cFormaPago no puede ir vacio";
            }

            
            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month,origen);
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";                
            }

            return "OK";
        } //Valida

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
                AnticiposCxPCL cl = new AnticiposCxPCL();
                cl.intAnticiposCxPID = 0;
                cl.intProveedoresID = Convert.ToInt32(cboProveedoresID.EditValue);
                cl.fFecha = Convert.ToDateTime(txtFecha.Text);
                cl.intChequerasID = Convert.ToInt32(cboChequerasID.EditValue);
                cl.dImportechequera = Convert.ToDecimal(txtImporte.Text);
                cl.strReferencia = txtReferencia.Text;
                cl.strMonedaCehquera = Monedadelachequera;
                cl.dTipodecambio = Convert.ToDecimal(txtTipodecambio.Text);
                cl.strMonedaProveedor = cboMonedaProveedor.EditValue.ToString();
                cl.dImporteProveedor = Convert.ToDecimal(txtImporteProvee.Text);
                cl.strDescripcion = txtDescripcion.Text;
                cl.strStatus = "1";
                cl.strcFormaPago = cboFormadePago.EditValue.ToString();
                Result = cl.AnticiposCxPCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");

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

        private void LlenarGrid()
        {
            AnticiposCxPCL cl = new AnticiposCxPCL();
            if (globalCL.gv_AnticiposOrigen == "CXP")
            {
                gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
                gridViewPrincipal.OptionsBehavior.ReadOnly = true;
                gridViewPrincipal.OptionsBehavior.Editable = false;
                this.Text = "AnticiposCxP";

                gridControlPrincipal.DataSource = cl.AnticiposCxPGrid();
                //Global, manda el nombre del grid para la clase Global
                globalCL clg = new globalCL();
                clg.strGridLayout = "gridAnticiposCxP";
                clg.restoreLayout(gridViewPrincipal);
            }
            else
            {
                gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
                gridViewPrincipal.OptionsBehavior.ReadOnly = true;
                gridViewPrincipal.OptionsBehavior.Editable = false;
                this.Text = "AnticiposCxC";

                gridControlPrincipal.DataSource = cl.AnticiposCxCGrid();
                //Global, manda el nombre del grid para la clase Global
                globalCL clg = new globalCL();
                clg.strGridLayout = "gridAnticiposCxC";
                clg.restoreLayout(gridViewPrincipal);
            }


        } //LlenarGrid()

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiLimpiar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LimpiaCajas();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            if (globalCL.gv_AnticiposOrigen == "CXP")
            {
                clg.strGridLayout = "gridAnticiposCxP";
            }
            else
            {
                clg.strGridLayout = "gridAnticiposCxC";

            }
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiListado.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            LimpiaCajas();
            cboFormadePago.EditValue = "03";
            navigationFrame.SelectedPageIndex = 1;
            
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridListadoAnticiposPorAplicar";
            String Result = clg.SaveGridLayout(gridView1);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiPrevio.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiListado.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LimpiaCajas();
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiGuardar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void LeeTipodecambio()
        {
            tiposdecambioCL cl = new tiposdecambioCL();
            cl.fFecha = Convert.ToDateTime(txtFecha.Text);
            cl.strMoneda = Monedadelachequera;
            string result = cl.tiposdecambioLlenaCajas();
            if (result == "OK")
            {
                txtTipodecambio.Text = cl.dParidad.ToString();
            }

        }

        private void cboChequerasID_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                object orow = cboChequerasID.Properties.GetDataSourceRowByKeyValue(cboChequerasID.EditValue);
                if (orow != null)
                {
                    Monedadelachequera = ((DataRowView)orow)["MonedasID"].ToString();
                    lblMoneda.Text = Monedadelachequera;

                    cboMonedaProveedor.EditValue = Monedadelachequera;


                    if (Monedadelachequera == "MXN")
                    {
                        txtTipodecambio.Text = "1";
                    }
                    else
                    {
                        LeeTipodecambio();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            popUpCancelar.Visible = true;
            groupControl3.Text = "Cancelar el folio:" + AntID.ToString();
            txtLogin.Focus();
        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            AntID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "AnticiposCxPID"));
            if (globalCL.gv_AnticiposOrigen == "CXC")
            {
                AntID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "AnticiposCxCID"));
            }
        }

        private void CierraPopUp()
        {
            popUpCancelar.Visible = false;
            txtLogin.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private void Cancelar(int UsuCan)
        {
            try
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
                cl.intAnticiposCxPID = AntID;
                cl.intUsuCan = UsuCan;
                cl.strRazon = txtRazoncancelacion.Text;
                result = cl.AnticiposCxPCancelar();
                if (result == "OK")
                {
                    MessageBox.Show("Anticipo cancelado correctamente");
                    LlenarGrid();
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cancelar:" + ex.Message);
            }
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            CierraPopUp();
        }

        private void btnAut_Click_1(object sender, EventArgs e)
        {

        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string _mark;
            try
            {
                _mark = (string)view.GetRowCellValue(e.RowHandle, "Status");
            }
            catch (Exception ex)
            {
                _mark = "Registrado";
            }

            if (_mark == "Cancelado")
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Red;
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Editar();
        }

        private void Editar()
        {
            try
            {

                bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiListado.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                LlenaCajas();
                navigationFrame.SelectedPageIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Editar:" + ex.Message);
            }
        }

        private void LlenaCajas()
        {
            AnticiposCxPCL cl = new AnticiposCxPCL();
            cl.intAnticiposCxPID = AntID;
            if (globalCL.gv_AnticiposOrigen == "CXP")
            {
                string result = cl.AnticiposCxPLlenaCajas();
                if (result == "OK")
                {
                    cboProveedoresID.EditValue = cl.intProveedoresID;
                    txtFecha.Text = cl.fFecha.ToShortDateString();
                    cboChequerasID.EditValue = cl.intChequerasID;
                    txtImporte.Text = cl.dImportechequera.ToString("C2");
                    txtReferencia.Text = cl.strReferencia;
                    txtTipodecambio.Text = cl.dTipodecambio.ToString("C4");
                    cboMonedaProveedor.EditValue = cl.strMonedaProveedor;
                    txtImporteProvee.Text = cl.dImporteProveedor.ToString("C2");
                    cboFormadePago.EditValue = cl.strcFormaPago;
                    txtDescripcion.Text = cl.strDescripcion;

                    //cl.intProveedoresID
                    //cl.intFolioAnt = AntID;

                    gridControlAplicaciones.DataSource = cl.Aplicaciones();
                }
                else
                {
                    MessageBox.Show("LllenacajasCxP: " + result);
                }
            }
            else
            {
                string result = cl.AnticiposCxCLlenaCajas();
                if (result == "OK")
                {
                    cboProveedoresID.EditValue = cl.intProveedoresID;
                    txtFecha.Text = cl.fFecha.ToShortDateString();
                    cboChequerasID.EditValue = cl.intChequerasID;
                    txtImporte.Text = cl.dImportechequera.ToString("C2");
                    txtReferencia.Text = cl.strReferencia;
                    txtTipodecambio.Text = cl.dTipodecambio.ToString("C4");
                    cboMonedaProveedor.EditValue = cl.strMonedaProveedor;
                    txtImporteProvee.Text = cl.dImporteProveedor.ToString("C2");
                    cboFormadePago.EditValue = cl.strcFormaPago;
                    txtDescripcion.Text = cl.strDescripcion;

                    //cl.intProveedoresID
                    //cl.intFolioAnt = AntID;

                    gridControlAplicaciones.DataSource = cl.Aplicaciones();
                }
                else
                {
                    MessageBox.Show("LllenacajasCxC: " + result);
                }
            }

        }

        private void cboMonedaProveedor_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                object orow = cboChequerasID.Properties.GetDataSourceRowByKeyValue(cboChequerasID.EditValue);
                if (orow != null)
                {
                    string moneda = ((DataRowView)orow)["Clave"].ToString();
                    if (moneda == "1")
                        moneda = "MXN";
                    else
                        moneda = "USD";

                    if (Monedadelachequera != moneda)
                    {
                        globalCL clg = new globalCL();
                        if (!clg.esNumerico(txtImporte.Text))
                            return;
                        if (Monedadelachequera == "MXN")
                            txtImporteProvee.Text = Math.Round(Convert.ToDecimal(txtImporte.Text) / Convert.ToDecimal(txtTipodecambio.Text), 2).ToString();
                        else
                            txtImporteProvee.Text = Math.Round(Convert.ToDecimal(txtImporte.Text) * Convert.ToDecimal(txtTipodecambio.Text), 2).ToString();
                    }
                    else
                    {
                        txtImporteProvee.Text = txtImporte.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            txtImporteProvee.Text = txtImporte.Text;
        }

        private void btnAut_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            CierraPopUp();
        }

        private void btnAut_Click_2(object sender, EventArgs e)
        {
            procedeCancelar();
        }

        private void procedeCancelar()
        {
            try
            {
                UsuariosCL clU = new UsuariosCL();
                clU.strLogin = txtLogin.Text;
                clU.strClave = txtPassword.Text;
                clU.strPermiso = "Cancelaranticipos";
                string result = clU.UsuariosPermisos();
                if (result != "OK")
                {
                    MessageBox.Show(result);
                    return;
                }
                //intUsuariocancelo = clU.intUsuariosID;
                CierraPopUp();

                Cancelar(clU.intUsuariosID);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiPrevio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(navigationFrame.SelectedPageIndex == 0)
            {
                gridControlPrincipal.ShowRibbonPrintPreview();
            }
            else if(navigationFrame.SelectedPageIndex == 2)
            {
                gridControlListado.ShowRibbonPrintPreview();
            }
        }

        private void bbiListado_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiListado.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navigationFrame.SelectedPageIndex = 2;

            if(globalCL.gv_AnticiposOrigen == "CXC")
            {
                AnticiposCxCCL cl = new AnticiposCxCCL();
                gridControlListado.DataSource = cl.AnticiposCxCPendientes();
            }
            else if(globalCL.gv_AnticiposOrigen == "CXP")
            {
                AnticiposCxPCL cl = new AnticiposCxPCL();
                gridControlListado.DataSource = cl.AnticiposCxPPendientes();
            }
        }
    }
}