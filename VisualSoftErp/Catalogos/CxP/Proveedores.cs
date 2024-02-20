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
using VisualSoftErp.Clases.Tesk.Clases;

namespace VisualSoftErp.Catalogos
{

    public partial class Proveedores : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intProveedoresID;
        public BindingList<detalleCL> detalle;
        public Proveedores()
        {
            InitializeComponent();

            txtNombre.Properties.MaxLength = 100;
            txtNombre.EnterMoveNextControl = true;
            txtDireccion.Properties.MaxLength = 100;
            txtDireccion.EnterMoveNextControl = true;
            txtRfc.Properties.MaxLength = 14;
            txtRfc.EnterMoveNextControl = true;
            txtContacto.Properties.MaxLength = 100;
            txtContacto.EnterMoveNextControl = true;
            txtTelefono.Properties.MaxLength = 50;
            txtTelefono.EnterMoveNextControl = true;
            txtEmail.Properties.MaxLength = 100;
            txtEmail.EnterMoveNextControl = true;
            cboMonedasID.Properties.MaxLength = 3;
            cboMonedasID.EnterMoveNextControl = true;
            dateEditFechaderegistro.Text = DateTime.Now.ToShortDateString();
            txtObservaciones.Properties.MaxLength = 200;
            txtObservaciones.EnterMoveNextControl = true;
            txtClasificacion.Properties.MaxLength = 10;
            txtClasificacion.EnterMoveNextControl = true;
            txtCorrespondenciaA.Properties.MaxLength = 100;
            txtCorrespondenciaA.EnterMoveNextControl = true;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Proveedores";

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            LlenarGrid();
            soloLectura();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void soloLectura()
        {
            globalCL clG = new globalCL();
            clG.strPrograma = "0206";
            if (clG.accesoSoloLectura())
            {
                bbiGuardar.Enabled = false;
                bbiEliminar.Enabled = false;
            }
                
        }

        private void LlenarGrid()
        {
            ProveedoresCL cl = new ProveedoresCL();
            gridControlPrincipal.DataSource = cl.ProveedoresGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridProveedores";
            clg.restoreLayout(gridViewPrincipal);
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Ciudades";
            cboCiudadesID.Properties.ValueMember = "Clave";
            cboCiudadesID.Properties.DisplayMember = "Des";
            cboCiudadesID.Properties.DataSource = cl.CargaCombos();
            cboCiudadesID.Properties.ForceInitialize();
            cboCiudadesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCiudadesID.Properties.PopulateColumns();
            cboCiudadesID.Properties.Columns["Clave"].Visible = false;
            cboCiudadesID.Properties.NullText = "Seleccione una ciudad";

            cl.strTabla = "Paises";
            cboPais.Properties.ValueMember = "Clave";
            cboPais.Properties.DisplayMember = "Des";
            cboPais.Properties.DataSource = cl.CargaCombos();
            cboPais.Properties.ForceInitialize();
            cboPais.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboPais.Properties.PopulateColumns();
            cboPais.Properties.Columns["Clave"].Visible = false;
            cboPais.Properties.NullText = "Seleccione un país";

            cl.strTabla = "Monedas";
            cboMonedasID.Properties.ValueMember = "Clave";
            cboMonedasID.Properties.DisplayMember = "Des";
            cboMonedasID.Properties.DataSource = cl.CargaCombos();
            cboMonedasID.Properties.ForceInitialize();
            cboMonedasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedasID.Properties.PopulateColumns();
            cboMonedasID.Properties.Columns["Clave"].Visible = false;
            cboMonedasID.ItemIndex = 0;

            List<tipoCL> tipoL = new List<tipoCL>();
            tipoL.Add(new tipoCL() { Clave = 1, Des = "Terrestre" });
            tipoL.Add(new tipoCL() { Clave = 2, Des = "Aereo" });
            tipoL.Add(new tipoCL() { Clave = 3, Des = "Maritima" });
            cl.strTabla = "";
            cboEmbarcarvia.Properties.ValueMember = "Clave";
            cboEmbarcarvia.Properties.DisplayMember = "Des";
            cboEmbarcarvia.Properties.DataSource = tipoL;
            cboEmbarcarvia.Properties.ForceInitialize();
            cboEmbarcarvia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboEmbarcarvia.Properties.PopulateColumns();
            cboEmbarcarvia.Properties.Columns["Clave"].Visible = false;
            cboEmbarcarvia.ItemIndex = 0;

            cl.strTabla = "Formadepago";
            cboFormadepago.Properties.ValueMember = "Clave";
            cboFormadepago.Properties.DisplayMember = "Des";
            cboFormadepago.Properties.DataSource = cl.CargaCombos();
            cboFormadepago.Properties.ForceInitialize();
            cboFormadepago.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFormadepago.Properties.PopulateColumns();
            cboFormadepago.Properties.Columns["Clave"].Visible = false;
            cboFormadepago.ItemIndex = 0;
            cl.strTabla = "MetododePago";
            cboMetododepago.Properties.ValueMember = "Clave";
            cboMetododepago.Properties.DisplayMember = "Des";
            cboMetododepago.Properties.DataSource = cl.CargaCombos();
            cboMetododepago.Properties.ForceInitialize();
            cboMetododepago.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMetododepago.Properties.PopulateColumns();
            cboMetododepago.Properties.Columns["Clave"].Visible = false;
            cboMetododepago.ItemIndex = 0;
            cl.strTabla = "Usodecfdi";
            cboUsodeCfdi.Properties.ValueMember = "Clave";
            cboUsodeCfdi.Properties.DisplayMember = "Des";
            cboUsodeCfdi.Properties.DataSource = cl.CargaCombos();
            cboUsodeCfdi.Properties.ForceInitialize();
            cboUsodeCfdi.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboUsodeCfdi.Properties.PopulateColumns();
            cboUsodeCfdi.Properties.Columns["Clave"].Visible = false;
            cboUsodeCfdi.ItemIndex = 0;
            cl.strTabla = "cyb_Bancos";
            cboBancosID.Properties.ValueMember = "Clave";
            cboBancosID.Properties.DisplayMember = "Des";
            cboBancosID.Properties.DataSource = cl.CargaCombos();
            cboBancosID.Properties.ForceInitialize();
            cboBancosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboBancosID.Properties.PopulateColumns();
            cboBancosID.Properties.Columns["Clave"].Visible = false;
            cboBancosID.ItemIndex = 0;
            cl.strTabla = "Transportes";
            cboTransportesID.Properties.ValueMember = "Clave";
            cboTransportesID.Properties.DisplayMember = "Des";
            cboTransportesID.Properties.DataSource = cl.CargaCombos();
            cboTransportesID.Properties.ForceInitialize();
            cboTransportesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTransportesID.Properties.PopulateColumns();
            cboTransportesID.Properties.Columns["Clave"].Visible = false;
            cboTransportesID.ItemIndex = 0;

            List<tipoCL> tipoLL = new List<tipoCL>();
            tipoLL.Add(new tipoCL() { Clave = 1, Des = "Local" });
            tipoLL.Add(new tipoCL() { Clave = 2, Des = "Ocurre" });
            tipoLL.Add(new tipoCL() { Clave = 3, Des = "A domicilio" });
            cl.strTabla = "";
            cboFormadeentrega.Properties.ValueMember = "Clave";
            cboFormadeentrega.Properties.DisplayMember = "Des";
            cboFormadeentrega.Properties.DataSource = tipoLL;
            cboFormadeentrega.Properties.ForceInitialize();
            cboFormadeentrega.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFormadeentrega.Properties.PopulateColumns();
            cboFormadeentrega.Properties.Columns["Clave"].Visible = false;
            cboFormadeentrega.ItemIndex = 0;

            List<vencimientoenBaseACL> venBase = new List<vencimientoenBaseACL>();
            venBase.Add(new vencimientoenBaseACL() { Clave = 1, Des = "Fecha recibido" });
            venBase.Add(new vencimientoenBaseACL() { Clave = 2, Des = "Fecha factura" });
            cl.strTabla = "";
            cboVenceEnBaseA.Properties.ValueMember = "Clave";
            cboVenceEnBaseA.Properties.DisplayMember = "Des";
            cboVenceEnBaseA.Properties.DataSource = venBase;
            cboVenceEnBaseA.Properties.ForceInitialize();
            cboVenceEnBaseA.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboVenceEnBaseA.Properties.PopulateColumns();
            cboVenceEnBaseA.Properties.Columns["Clave"].Visible = false;
            cboVenceEnBaseA.ItemIndex = 0;

        }

        public class tipoCL
        {
            public int Clave { get; set; }
            public string Des { get; set; }
        }
        public class vencimientoenBaseACL
        {
            public int Clave { get; set; }
            public string Des { get; set; }
        }


        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
            intProveedoresID = 0;
            lblNumero.Text = string.Empty;
        }

        private void LimpiaCajas()
        {
            txtDiaSurtido.Text = string.Empty;
            txtDiasdetraslado.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            cboCiudadesID.EditValue = null;
            cboPais.EditValue = null;
            txtRfc.Text = string.Empty;
            txtContacto.Text = string.Empty;
            cboTipo.SelectedIndex = 0;
            txtPLazo.Text = string.Empty;
            txtCreditoautorizado.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
            cboMonedasID.ItemIndex = 0;
            dateEditFechaderegistro.Text = DateTime.Now.ToShortDateString();
            //txtDiasdeentrega.Text = string.Empty;
            //txtDiasTraslado.Text = string.Empty;
            txtPiva.Text = string.Empty;
            swIvaEstricto.IsOn = true;
            swRetIvaEstricto.IsOn = true;
            txtObservaciones.Text = string.Empty;
            txtClasificacion.Text = string.Empty;
            txtRetiva.Text = string.Empty;
            txtRetisr.Text = string.Empty;
            swEsdeservicio.IsOn = false;
            swActivo.IsOn = false;
            txtCorrespondenciaA.Text = string.Empty;
            txtLab.Text = string.Empty;
            cboEmbarcarvia.ItemIndex = 0;
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
                ProveedoresCL cl = new ProveedoresCL();
                cl.intProveedoresID = intProveedoresID;
                cl.strNombre = txtNombre.Text;
                cl.strDireccion = txtDireccion.Text;
                cl.intCiudadesID = Convert.ToInt32(cboCiudadesID.EditValue);
                cl.intPais = Convert.ToInt32(cboPais.EditValue);
                cl.strRfc = txtRfc.Text;
                cl.strContacto = txtContacto.Text;
                cl.intTipo = Convert.ToInt32(cboTipo.SelectedIndex);
                cl.intPLazo = Convert.ToInt32(txtPLazo.Text);
                cl.intCreditoautorizado = Convert.ToDecimal(txtCreditoautorizado.Text);
                cl.strTelefono = txtTelefono.Text;
                cl.strEmail = txtEmail.Text;
                cl.strMonedasID = cboMonedasID.EditValue.ToString();
                cl.fFechaderegistro = Convert.ToDateTime(dateEditFechaderegistro.Text);
                //cl.intTiempodeentrega = Convert.ToInt32(txtDiasdeentrega.Text);
                cl.strCorrespondenciaA = txtCorrespondenciaA.Text;
                //cl.intDiasTraslado = Convert.ToInt32(txtDiasTraslado.Text);
                cl.intPiva = Convert.ToInt32(txtPiva.Text);
                cl.intIvaEstricto = swIvaEstricto.IsOn ? 1 : 0;
                cl.intRetIvaEstricto = swRetIvaEstricto.IsOn ? 1 : 0;
                cl.strObservaciones = txtObservaciones.Text;
                cl.strClasificacion = txtClasificacion.Text;
                cl.decRetiva = Convert.ToDecimal(txtRetiva.Text);
                cl.decRetisr = Convert.ToDecimal(txtRetisr.Text);
                cl.intEsdeservicio = swEsdeservicio.IsOn ? 1 : 0;
                cl.intActivo = swActivo.IsOn ? 1 : 0;
                cl.strLab = txtLab.Text;
                cl.intVia = Convert.ToInt32(cboEmbarcarvia.EditValue);
                cl.intBancosID = Convert.ToInt32(cboBancosID.EditValue);
                cl.strCuentabancaria = txtCuentabancaria.Text.ToString();
                cl.strc_Formapago = cboFormadepago.EditValue.ToString();
                cl.strc_Metodopago = cboMetododepago.EditValue.ToString();
                cl.strc_Usocfdi= cboUsodeCfdi.EditValue.ToString();
                cl.intVenceEnBaseA = Convert.ToInt32(cboVenceEnBaseA.EditValue);

                cl.intTransportesID = Convert.ToInt32(cboTransportesID.EditValue);
                //cl.intDiasentrega = Convert.ToInt32(txtDiasdeentrega.Text);
                cl.intFormaentrega = Convert.ToInt32(cboFormadeentrega.EditValue);
                cl.intAuxCompras = swAuxCompras.IsOn ? 1 : 0;
                cl.strCuentaMN = txtCuentaMN.Text;
                cl.strCuentaExt = txtCuentaExt.Text;
                cl.strCuentaGastos = txtCuentaGastos.Text;
                cl.strCuentaComp = txtCuentaComp.Text;
                cl.dMesesdeconsumo = Convert.ToDecimal(txtMesesdeconsumo.Text);
                cl.dIncrementales = Convert.ToDecimal(txtPIncrementales.Text);
                cl.dDiasdesurtido = Convert.ToDecimal(txtDiaSurtido.Text);
                cl.dDiasdetraslado = Convert.ToDecimal(txtDiasdetraslado.Text);
                //cl.dDiasStock = Convert.ToDecimal(txtDiasStock.Text);
                Result = cl.ProveedoresCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intProveedoresID == 0) //Nuevo
                    {
                        intProveedoresID = 699;
                        subeTesk();
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
            if (txtNombre.Text.Length == 0)
            {
                return "El campo Nombre no puede ir vacio";
            }
            if (txtDireccion.Text.Length == 0)
            {
                return "El campo Direccion no puede ir vacio";
            }
            if (cboCiudadesID.EditValue == null)
            {
                return "El campo CiudadesID no puede ir vacio";
            }
            if (cboPais.EditValue == null)
            {
                return "El campo País no puede ir vacio";
            }
            if (txtRfc.Text.Length == 0)
            {
                return "El campo Rfc no puede ir vacio";
            }
            if (txtContacto.Text.Length == 0)
            {
                return "El campo Contacto no puede ir vacio";
            }
            if (cboTipo.EditValue == null)
            {
                return "El campo Tipo no puede ir vacio";
            }
            if (txtPLazo.Text.Length == 0)
            {
                return "El campo PLazo no puede ir vacio";
            }
            if (txtCreditoautorizado.Text.Length == 0)
            {
                return "El campo Creditoautorizado no puede ir vacio";
            }
            if (txtTelefono.Text.Length == 0)
            {
                return "El campo Telefono no puede ir vacio";
            }
            if (txtEmail.Text.Length == 0)
            {
                return "El campo Correo no puede ir vacio";
            }
            if (cboMonedasID.EditValue == null)
            {
                return "El campo MonedasID no puede ir vacio";
            }
            if (dateEditFechaderegistro.Text.Length == 0)
            {
                return "El campo Fechaderegistro no puede ir vacio";
            }
            //if (txtDiasdeentrega.Text.Length == 0)
            //{
            //    txtDiasdeentrega.Text = "0";
            //}
            //if (txtDiasTraslado.Text.Length == 0)
            //{
            //    txtDiasTraslado.Text = "0";
            //}
            if (txtPiva.Text.Length == 0)
            {
                return "El campo Porcentaje de iva no puede ir vacio";
            }
            if (txtObservaciones.Text.Length == 0)
            {
                txtObservaciones.Text = "0";
            }
            if (txtClasificacion.Text.Length == 0)
            {
                txtClasificacion.Text = "";
            }
            if (txtRetiva.Text.Length == 0)
            {
                txtRetiva.Text = "0";
            }
            if (txtRetisr.Text.Length == 0)
            {
                txtRetisr.Text = "0";
            }
            if (txtCorrespondenciaA.Text.Length == 0)
            {
                txtCorrespondenciaA.Text = "";
            }



            if (cboTransportesID.EditValue == null)
            {
                return "El campo TransportesID no puede ir vacio";
            }
            //if (txtDiasdeentrega.Text.Length == 0)
            //{
            //    return "El campo Diasentrega no puede ir vacio";
            //}
            if (cboFormadeentrega.EditValue == null)
            {
                return "El campo Formaentrega no puede ir vacio";
            }
            if (cboVenceEnBaseA.EditValue == null)
            {
                return "El campo fecha de vencimiento en base a no puede ir vaco";
            }
           
            if (txtCuentaMN.Text.Length == 0)
            {
                return "El campo Cuenta tesk no puede ir vacio, para continuar puede poner un 0, pero debe poner el que tesk asigne para poder subir sus compras o cargos";
            }
            if (txtCuentaExt.Text.Length == 0)
            {
                txtCuentaExt.Text = "0";
            }
            if (txtCuentaGastos.Text.Length == 0)
            {
                return "El campo Cuenta gastos no puede ir vacio, para avanzar puede poner un 0, pero debe poner la cuenta correcta según tesk para subir compras y/o cargos posteriormente.";
            }
            if (txtCuentaComp.Text.Length == 0)
            {
                return "El campo CuentaComp no puede ir vacio";
            }
            if (txtMesesdeconsumo.Text.Length == 0)
            {
                return "El campo Mesesdeconsumo no puede ir vacio";
            }
            if (txtPIncrementales.Text.Length == 0)
            {
                return "El campo incrementales no puede ir vacio";
            }
            if (txtDiaSurtido.Text.Length == 0)
            {
                return "El campo Diasdesurtido no puede ir vacio";
            }
            //if (txtDiasTraslado.Text.Length == 0)
            //{
            //    return "El campo Diasdetraslado no puede ir vacio";
            //}
            //if (txtDiasStock.Text.Length == 0)
            //{
            //    return "El campo Diasstock no puede ir vacio";
            //}
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            ProveedoresCL cl = new ProveedoresCL();
            cl.intProveedoresID = intProveedoresID;
            String Result = cl.ProveedoresLlenaCajas();
            if (Result == "OK")
            {
                txtNombre.Text = cl.strNombre;
                txtDireccion.Text = cl.strDireccion;
                cboCiudadesID.EditValue = cl.intCiudadesID;
                cboPais.EditValue = cl.intPais;
                txtRfc.Text = cl.strRfc;
                txtContacto.Text = cl.strContacto;
                cboTipo.SelectedIndex = cl.intTipo;
                txtPLazo.EditValue = cl.intPLazo;
                txtCreditoautorizado.Text = cl.intCreditoautorizado.ToString();
                txtTelefono.Text = cl.strTelefono;
                txtEmail.Text = cl.strEmail;
                cboMonedasID.EditValue = cl.strMonedasID;
                dateEditFechaderegistro.Text = cl.fFechaderegistro.ToShortDateString();
                //txtDiasdeentrega.Text = cl.intTiempodeentrega.ToString();
                //txtDiasTraslado.Text = cl.intDiasTraslado.ToString();
                txtPiva.Text = cl.intPiva.ToString();
                swIvaEstricto.IsOn = cl.intIvaEstricto == 1 ? true : false;
                swRetIvaEstricto.IsOn = cl.intRetIvaEstricto == 1 ? true : false;
                txtObservaciones.Text = cl.strObservaciones;
                txtClasificacion.Text = cl.strClasificacion;
                txtRetiva.Text = cl.decRetiva.ToString();
                txtRetisr.Text = cl.decRetisr.ToString();
                swEsdeservicio.IsOn = cl.intEsdeservicio == 1 ? true : false;
                swActivo.IsOn = cl.intActivo == 1 ? true : false;
                txtCorrespondenciaA.Text = cl.strCorrespondenciaA;
                cboEmbarcarvia.EditValue = cl.intVia;
                txtLab.Text = cl.strLab;
                cboBancosID.EditValue = cl.intBancosID;
                txtCuentabancaria.Text = cl.strCuentabancaria;
                cboFormadepago.EditValue = cl.strc_Formapago;
                cboMetododepago.EditValue = cl.strc_Metodopago;
                cboUsodeCfdi.EditValue = cl.strc_Usocfdi;

                cboTransportesID.EditValue = cl.intTransportesID;
                //txtDiasdeentrega.EditValue = cl.intDiasentrega;
                cboFormadeentrega.EditValue = cl.intFormaentrega;
                cl.intAuxCompras = swAuxCompras.IsOn ? 1 : 0;
                txtCuentaMN.Text = cl.strCuentaMN;
                txtCuentaExt.Text = cl.strCuentaExt;
                txtCuentaGastos.Text = cl.strCuentaGastos;
                txtCuentaComp.Text = cl.strCuentaComp;
                txtMesesdeconsumo.Text = cl.dMesesdeconsumo.ToString();
                txtPIncrementales.Text = cl.dIncrementales.ToString();
                txtDiaSurtido.Text = cl.dDiasdesurtido.ToString();
                txtDiasdetraslado.Text = cl.dDiasdetraslado.ToString();
                //txtDiasStock.Text = cl.dDiasStock.ToString();
                cboPais.EditValue = cl.intPais;
                cboVenceEnBaseA.EditValue = cl.intVenceEnBaseA;

                lblNumero.Text = intProveedoresID.ToString();
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intProveedoresID == 0)
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
            bbiArticulos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            navigationFrame.SelectedPageIndex = 1;
        }

        private void Eliminar()
        {
            ProveedoresCL cl = new ProveedoresCL();
            cl.intProveedoresID = intProveedoresID;
            String Result = cl.ProveedoresEliminar();
            if (Result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intProveedoresID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intProveedoresID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
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
            bbiArticulos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
            intProveedoresID = 0;
        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridProveedores";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            globalCL clgd = new globalCL();
            clgd.strGridLayout = "gridProveedoresArticulo";
            Result = clgd.SaveGridLayout(gridViewDetalle);
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

        private void gridViewPrincipal_RowClick_1(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intProveedoresID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ProveedoresID"));
        }

        private void bbiArticulos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intProveedoresID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                ribbonPageGroup1.Visible = false;
                ribbonPageGroup2.Visible = true;
                try
                {
                    ProveedoresCL cl = new ProveedoresCL();
                    cl.intProveedoresID = intProveedoresID;
                    String Result = cl.ProveedoresLlenaCajas();
                    if (Result == "OK")
                    {
                        lblProveedor.Text = cl.strNombre;
                    }
                }
                catch (Exception ex) { }
                CargaCombosArticulos();

                //Inicialisalista();
               BuscarArticulosxProveedor();
               navigationFrame.SelectedPageIndex = 2;

            }
        }

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridProveedoresArticulo";
            clg.restoreLayout(gridViewDetalle);
        }

        public class detalleCL
        {
 
            public int Articulo { get; set; }
            public string Clave { get; set; }

        }

        private void CargaCombosArticulos()
        {
            combosCL cl = new combosCL();

            cl.strTabla = "Articulos";
            repositoryItemLookUpEditArticulos.ValueMember = "Clave";
            repositoryItemLookUpEditArticulos.DisplayMember = "Des";
            repositoryItemLookUpEditArticulos.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulos.ForceInitialize();
            repositoryItemLookUpEditArticulos.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulos.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }

       private void BuscarArticulosxProveedor()
        {
            try
            {
                ProveedoresCL cl = new ProveedoresCL();
                cl.intProveedoresID = intProveedoresID;

                String Result = cl.ProveedoresBuscarArticulos().ToString();
                if (cl.dtd.Rows.Count == 0)
                { Inicialisalista(); }
                else
                { gridControlDetalle.DataSource = cl.ProveedoresBuscarArticulos(); }

                //gridControlDetalle.ForceInitialize();
                
                //if (gridViewDetalle.RowCount==1)
                //{
                //    Inicialisalista();
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                //if (e.Column.Name == "gridColumnArticulo")
                //{
                //    articulosCL cl = new articulosCL();
                //    int intArtID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulo"));
                //    if (intArtID > 0) //validamos que haya algo en la celda
                //    {
                //        cl.intArticulosID = intArtID;
                //        string result = cl.articulosLlenaCajas();
                //        if (result == "OK")
                //        {
                //            gridViewDetalle.SetFocusedRowCellValue("Descripcion", cl.strNombre);
                //            gridViewDetalle.SetFocusedRowCellValue("Clave", cl.strClaveSat);

                //        }
                //    }

                //}
            }


            catch (Exception ex)
            {
                MessageBox.Show("griddetalle: " + ex);
            }


        }

        private void bbiGuardarArticulos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GuardarArticulos();
        }

        private void GuardarArticulos()
        {

            try
            {
                System.Data.DataTable dtProveedoresArticulos = new System.Data.DataTable("ProveedoresArticulos");
                dtProveedoresArticulos.Columns.Add("ProveedoresID", Type.GetType("System.Int32"));
                dtProveedoresArticulos.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtProveedoresArticulos.Columns.Add("Clave", Type.GetType("System.String"));

                int intSeq, intArticulosID;
                string dato;
                string strClave = String.Empty;
                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "Articulo").ToString();
                    if (dato.Length > 0)
                    {
                        intSeq = i;
                        intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Articulo"));
                        strClave = gridViewDetalle.GetRowCellValue(i, "Clave").ToString();

                        dtProveedoresArticulos.Rows.Add(intProveedoresID, intArticulosID, strClave);

                    }
                }

                ProveedoresCL cl = new ProveedoresCL();
                cl.dtd = dtProveedoresArticulos;
                cl.intProveedoresID = intProveedoresID;
                string Result = cl.ProveedoresArticulosCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intProveedoresID == 0)
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
        }

        private void bbiRegresarArticulo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intProveedoresID = 0;
            ribbonPageGroup1.Visible = true;
            ribbonPageGroup2.Visible = false;
            navigationFrame.SelectedPageIndex = 0;
        }

        private void btnUpdCd_Click(object sender, EventArgs e)
        {
            combosCL cl = new combosCL();
            cl.ActualizaCombo(cboCiudadesID, "Ciudades", "Seleccione una ciudad");
        }

        private void btnUpdMoneda_Click(object sender, EventArgs e)
        {
            combosCL cl = new combosCL();
            cl.ActualizaCombo(cboMonedasID, "Monedas", "");
        }

        private void btnUpdFormadep_Click(object sender, EventArgs e)
        {
            combosCL cl = new combosCL();
            cl.ActualizaCombo(cboFormadepago, "Formadepago", "");
        }

        private void btnUpdMetododep_Click(object sender, EventArgs e)
        {
            combosCL cl = new combosCL();
            cl.ActualizaCombo(cboMetododepago, "Metododepago", "");
        }

        private void btnUpUsodecfdi_Click(object sender, EventArgs e)
        {
            combosCL cl = new combosCL();
            cl.ActualizaCombo(cboUsodeCfdi, "Usodecfdi", "");

        }

        private void btnUpBancos_Click(object sender, EventArgs e)
        {
            combosCL cl = new combosCL();
            cl.ActualizaCombo(cboBancosID, "cyb_Bancos", "");
        }

        private void btnUpTransportesID_Click(object sender, EventArgs e)
        {
            combosCL cl = new combosCL();
            cl.ActualizaCombo(cboBancosID, "Transportes", "");
        }

        private void subeTesk()
        {
            try
            {
                teskCL cl = new teskCL();
                cl.intDato = intProveedoresID;

                string result = cl.PostProveedoresDiexsa();

                if (result == "OK")
                {
                    //ProveedoresCL clp = new ProveedoresCL();
                    //clp.intProveedoresID = intProveedoresID;
                    //clp.strCuentaMN = cl.strCta;
                    //result = clp.ProveedoresActualizaTesk();
                    //if (result == "OK")
                        MessageBox.Show("Actualizado correctamente cuenta tesk");
                    //else
                    //    MessageBox.Show("Al actulizar cuenta tesk en proveedores: " + clp.strCuentaMN + " " + result);
                }
                else
                    MessageBox.Show("Tesk: " + result);
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiTesk_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                subeTesk();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

