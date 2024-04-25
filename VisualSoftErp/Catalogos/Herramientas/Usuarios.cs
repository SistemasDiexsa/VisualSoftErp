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

namespace VisualSoftErp.Catalogos
{

    public partial class Usuarios : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intUsuariosID;
        int intActivos;

        public Usuarios()
        {
            InitializeComponent();

            txtNombre.Properties.MaxLength = 50;
            txtLogin.Properties.MaxLength = 50;
            txtClave.Properties.MaxLength = 50;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Usuarios";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            intActivos = 1;
            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            CargaCombos();
        }

        private void LlenarGrid()
        {
            UsuariosCL cl = new UsuariosCL();
            cl.intActivo=intActivos;
            gridControlPrincipal.DataSource = cl.UsuariosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridUsuarios";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

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
            cboSerie.Properties.NullText = "Seleccione una serie";
        }

        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intUsuariosID = 0;
            CargaCombos();
        }

        private void LimpiaCajas()
        {
            txtNombre.Text = string.Empty;
            txtLogin.Text = string.Empty;
            txtClave.Text = string.Empty;
            swCambiarprecios.IsOn = false;
            swAutorizarrequisiciones.IsOn = false;
            swAutorizarpedidoscxc.IsOn = false;
            swAlertapedidoscxc.IsOn = false;
            swAutorizapedidosxexistencia.IsOn = false;
            swAlertapedidosxexistencia.IsOn = false;
            swAutorizaoc.IsOn = false;
            swAlertaoc.IsOn = false;
            swAutorizabajocosto.IsOn = false;
            swCancelarpedidossurtidos.IsOn = false;
            swVercostos.IsOn = false;
            swAlertapedidosporfacturar.IsOn = false;
            swModificarcantidadalsurtir.IsOn = false;
            swActualizarcostosmanualmente.IsOn = false;
            swAutorizacancelacionesanteriores.IsOn = false;
            swAlmacendefault.IsOn = false;
            swDepuraoc.IsOn = false;
            swUltimosprecios.IsOn = false;
            cboSerie.EditValue = null;

            swEditarCondicionesPago.IsOn = false;
            txtCapturista.Text = string.Empty;
            swCancelarDepositos.IsOn = false;
            swCancelarPedidos.IsOn = false;
            swCancelarPagosCXP.IsOn = false;
            swCancelarContraRecibos.IsOn = false;
            swCancelarCompras.IsOn = false;
            swCancelarAnticipos.IsOn = false;

            swFlexArtArribosProveedor.IsOn = false;
            swFlexArtCompras.IsOn = false;
            swFlexArtMovimientos.IsOn = false;
            swFlexArtVentas.IsOn = false;  
            swActivo.IsOn = false;  
            swTienda.IsOn = false;

            swCancelarHP.IsOn = false;
            swCancelarValesCadenas.IsOn = false;
            swCancelarAplicacionAnticipos.IsOn = false;
            swAsignarPolizaTesk.IsOn = false;
            swCancelarRM.IsOn = false;
            swCambiarFechaRM.IsOn = false;
            swCancelarFacturas.IsOn = false;
            swActualizarExistenciaInventarioFisico.IsOn = false;

        }

        private void bbiGuardar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
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
                UsuariosCL cl = new UsuariosCL();
                cl.intUsuariosID = intUsuariosID;
                cl.strNombre = txtNombre.Text;
                cl.strLogin = txtLogin.Text;
                cl.strClave = txtClave.Text;
                cl.intCambiarprecios = swCambiarprecios.IsOn ? 1 : 0;
                cl.intAutorizarrequisiciones = swAutorizarrequisiciones.IsOn ? 1 : 0;
                cl.intAutorizarpedidoscxc = swAutorizarpedidoscxc.IsOn ? 1 : 0;
                cl.intAlertapedidoscxc = swAlertapedidoscxc.IsOn ? 1 : 0;
                cl.intAutorizapedidosxexistencia = swAutorizapedidosxexistencia.IsOn ? 1 : 0;
                cl.intAlertapedidosxexistencia = swAlertapedidosxexistencia.IsOn ? 1 : 0;
                cl.intAutorizaoc = swAutorizaoc.IsOn ? 1 : 0;
                cl.intAlertaoc = swAlertaoc.IsOn ? 1 : 0;
                cl.intAutorizabajocosto = swAutorizabajocosto.IsOn ? 1 : 0;
                cl.intCancelarpedidossurtidos = swCancelarpedidossurtidos.IsOn ? 1 : 0;
                cl.intVercostos = swVercostos.IsOn ? 1 : 0;
                cl.intAlertapedidosporfacturar = swAlertapedidosporfacturar.IsOn ? 1 : 0;
                cl.intModificarcantidadalsurtir = swModificarcantidadalsurtir.IsOn ? 1 : 0;
                cl.intActualizarcostosmanualmente = swActualizarcostosmanualmente.IsOn ? 1 : 0;
                cl.intAutorizacancelacionesanteriores = swAutorizacancelacionesanteriores.IsOn ? 1 : 0;
                cl.intAlmacendefault = swAlmacendefault.IsOn ? 1 : 0;
                cl.intDepuraoc = swDepuraoc.IsOn ? 1 : 0;
                cl.intUltimosprecios = swUltimosprecios.IsOn ? 1 : 0;
                cl.strSerie = cboSerie.EditValue.ToString();
                
                cl.intEditarcondicionesdepago = swEditarCondicionesPago.IsOn ? 1 : 0;
                cl.strCapturista = txtCapturista.Text;
                cl.intCancelardepositos = swCancelarDepositos.IsOn ? 1 : 0;
                cl.intCancelarpedidos = swCancelarPedidos.IsOn ? 1 : 0;
                cl.intCancelarpagoscxp = swCancelarPagosCXP.IsOn ? 1 : 0;
                cl.intCancelarcontrarecibos = swCancelarContraRecibos.IsOn ? 1 : 0;
                cl.intCancelarcompras = swCancelarCompras.IsOn ? 1 : 0;
                cl.intCancelaranticipos = swCancelarAnticipos.IsOn ? 1 : 0;
                
                cl.intFlexArtArribosProveedor = swFlexArtArribosProveedor.IsOn ? 1 : 0;
                cl.intFlexArtMovimientos = swFlexArtMovimientos.IsOn ? 1 : 0;
                cl.intFlexArtVentas = swFlexArtVentas.IsOn ? 1 : 0;
                cl.intFlexArtCompras = swFlexArtCompras.IsOn ? 1 : 0;
                cl.intActivo = swActivo.IsOn ? 1 : 0;
                cl.intTienda = swTienda.IsOn ? 1 : 0;
                
                cl.intCancelarHP = swCancelarHP.IsOn ? 1 : 0;
                cl.intCancelarValesCadenas = swCancelarValesCadenas.IsOn ? 1 : 0;
                cl.intCancelarAplicacionAnticipos = swCancelarAplicacionAnticipos.IsOn ? 1 : 0;
                cl.intAsignarPolizaTesk = swAsignarPolizaTesk.IsOn ? 1 : 0;
                cl.intCancelarRM = swCancelarRM.IsOn ? 1 : 0;
                cl.intCambiarFechaRM = swCambiarFechaRM.IsOn ? 1 : 0;
                cl.intCancelarFacturas = swCancelarFacturas.IsOn ? 1 : 0;
                cl.intActualizarExistenciaInventarioFisico = swActualizarExistenciaInventarioFisico.IsOn ? 1 : 0;
                
                cl.iFirmaElectronicaSalidas = swIFirmaElectronicaSalidas.IsOn ? 1 : 0;
                cl.intModificaFamiliasArticulos = swModificaFamiliasArticulos.IsOn ? 1 : 0;
                cl.intModificaSubFamiliasArticulos = swModificaSubFamiliasArticulos.IsOn ? 1 : 0;
                Result = cl.UsuariosCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intUsuariosID == 0)
                    {
                        LimpiaCajas();
                        CargaCombos();
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
            if (txtNombre.Text.Length == 0)
            {
                return "El campo Nombre no puede ir vacio";
            }
            if (txtLogin.Text.Length == 0)
            {
                return "El campo Login no puede ir vacio";
            }
            if (txtClave.Text.Length == 0)
            {
                return "El campo Clave no puede ir vacio";
            }
            if (cboSerie.EditValue== null)
            {
                return "El campo Clave no puede ir vacio";
            }

            return "OK";
        } //Valida

        private void llenaCajas()
        {
            UsuariosCL cl = new UsuariosCL();
            cl.intUsuariosID = intUsuariosID;
            String Result = cl.UsuariosLlenaCajas();
            if (Result == "OK")
            {
                txtNombre.Text = cl.strNombre;
                txtLogin.Text = cl.strLogin;
                txtClave.Text = cl.strClave;
                swCambiarprecios.IsOn = cl.intCambiarprecios == 1 ? true : false;
                swAutorizarrequisiciones.IsOn = cl.intAutorizarrequisiciones == 1 ? true : false;
                swAutorizarpedidoscxc.IsOn = cl.intAutorizarpedidoscxc == 1 ? true : false;
                swAlertapedidoscxc.IsOn = cl.intAlertapedidoscxc == 1 ? true : false;
                swAutorizapedidosxexistencia.IsOn = cl.intAutorizapedidosxexistencia == 1 ? true : false;
                swAlertapedidosxexistencia.IsOn = cl.intAlertapedidosxexistencia == 1 ? true : false;
                swAutorizaoc.IsOn = cl.intAutorizaoc == 1 ? true : false;
                swAlertaoc.IsOn = cl.intAlertaoc == 1 ? true : false;
                swAutorizabajocosto.IsOn = cl.intAutorizabajocosto == 1 ? true : false;
                swCancelarpedidossurtidos.IsOn = cl.intCancelarpedidossurtidos == 1 ? true : false;
                swVercostos.IsOn = cl.intVercostos == 1 ? true : false;
                swAlertapedidosporfacturar.IsOn = cl.intAlertapedidosporfacturar == 1 ? true : false;
                swModificarcantidadalsurtir.IsOn = cl.intModificarcantidadalsurtir == 1 ? true : false;
                swActualizarcostosmanualmente.IsOn = cl.intActualizarcostosmanualmente == 1 ? true : false;
                swAutorizacancelacionesanteriores.IsOn = cl.intAutorizacancelacionesanteriores == 1 ? true : false;
                swAlmacendefault.IsOn = cl.intAlmacendefault == 1 ? true : false;
                swDepuraoc.IsOn = cl.intDepuraoc == 1 ? true : false;
                swUltimosprecios.IsOn = cl.intUltimosprecios == 1 ? true : false;
                cboSerie.EditValue = cl.strSerie;

                swEditarCondicionesPago.IsOn = cl.intEditarcondicionesdepago == 1 ? true : false;
                txtCapturista.Text = cl.strCapturista;
                swCancelarDepositos.IsOn = cl.intCancelardepositos == 1 ? true : false;
                swCancelarPedidos.IsOn = cl.intCancelarpedidos == 1 ? true : false;
                swCancelarPagosCXP.IsOn = cl.intCancelarpagoscxp == 1 ? true : false;
                swCancelarContraRecibos.IsOn = cl.intCancelarcontrarecibos == 1 ? true : false;
                swCancelarCompras.IsOn = cl.intCancelarcompras == 1 ? true : false;
                swCancelarAnticipos.IsOn = cl.intCancelaranticipos == 1 ? true : false;

                swFlexArtArribosProveedor.IsOn = cl.intFlexArtArribosProveedor == 1 ? true : false;
                swFlexArtCompras.IsOn = cl.intFlexArtCompras == 1 ? true : false;
                swFlexArtMovimientos.IsOn = cl.intFlexArtMovimientos == 1 ? true : false;
                swFlexArtVentas.IsOn = cl.intFlexArtVentas == 1 ? true : false;
                swActivo.IsOn = cl.intActivo == 1 ? true : false;
                swTienda.IsOn = cl.intTienda == 1 ? true : false;

                swCancelarHP.IsOn = cl.intCancelarHP == 1 ? true : false;
                swCancelarValesCadenas.IsOn = cl.intCancelarValesCadenas == 1 ? true : false;
                swCancelarAplicacionAnticipos.IsOn = cl.intCancelarAplicacionAnticipos == 1 ? true : false;
                swAsignarPolizaTesk.IsOn = cl.intAsignarPolizaTesk == 1 ? true : false;
                swCancelarRM.IsOn = cl.intCancelarRM == 1 ? true : false;
                swCambiarFechaRM.IsOn = cl.intCambiarFechaRM == 1 ? true : false;
                swCancelarFacturas.IsOn = cl.intCancelarFacturas == 1 ? true : false;
                swActualizarExistenciaInventarioFisico.IsOn = cl.intActualizarExistenciaInventarioFisico == 1 ? true : false;                

                swIFirmaElectronicaSalidas.IsOn = cl.iFirmaElectronicaSalidas == 1 ? true : false;
                swModificaFamiliasArticulos.IsOn = cl.intModificaFamiliasArticulos == 1 ? true : false;
                swModificaSubFamiliasArticulos.IsOn = cl.intModificaSubFamiliasArticulos == 1 ? true : false;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intUsuariosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }  //Editar

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();
        }

        private void BotonesEdicion()
        {
            LimpiaCajas();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            navigationFrame.SelectedPageIndex = 1;
        }

        private void Eliminar()
        {
            UsuariosCL cl = new UsuariosCL();
            cl.intUsuariosID = intUsuariosID;
            cl.intActivo = 0;
            String Result = cl.UsuariosEliminar();
            if (Result == "OK")
            {
                MessageBox.Show("Desactivado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intUsuariosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea desactivar el ID " + intUsuariosID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridUsuarios";
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
            intUsuariosID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "UsuariosID"));
        }

        private void bbiTodos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intActivos = 0;
            bbiTodos.Visibility= DevExpress.XtraBars.BarItemVisibility.Never;
            bbiActivos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiActivar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LlenarGrid();
        }

        private void bbiActivos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intActivos = 1;
            bbiTodos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiActivos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiActivar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            LlenarGrid();
        }

        private void bbiActivar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UsuariosCL cl = new UsuariosCL();
            cl.intUsuariosID = intUsuariosID;
            cl.intActivo = 1;
            String Result = cl.UsuariosEliminar();
            if (Result == "OK")
            {
                MessageBox.Show("Activado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(Result);
            }
        }

    }
}
