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

namespace VisualSoftErp.Catalogos.CXC
{
    public partial class Clientes : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intClientesID;
        public Clientes()
        {
            InitializeComponent();

            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Catálogo de Clientes";
            llenaCombos();
            LlenarGrid();
            soloLectura();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void soloLectura()
        {
            globalCL clG = new globalCL();
            clG.strPrograma = "0508";
            if (clG.accesoSoloLectura())
            {
                bbiGuardar.Enabled = false;
                bbiEliminar.Enabled = false;
            }
                
        }

        private void LlenarGrid()
        {
            clientesCL cl = new clientesCL();
            grdClientes.DataSource = cl.ClientesGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridClientes";
            clg.restoreLayout(gridView1);
            gridView1.OptionsView.ShowAutoFilterRow = true;

        }//LlenarGrid()

        private void llenaCombos()
        {
            combosCL cl = new combosCL();

            cboZona.Properties.ValueMember = "Clave";
            cboZona.Properties.DisplayMember = "Des";
            cl.strTabla = "Zonas";
            cboZona.Properties.DataSource = cl.CargaCombos();
            cboZona.Properties.ForceInitialize();
            cboZona.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboZona.Properties.ForceInitialize();
            cboZona.Properties.PopulateColumns();
            cboZona.Properties.Columns["Clave"].Visible = false;
            cboZona.Properties.NullText = "Seleccione una Zona";

            cl.strTabla = "Estados";
            cl.intClave = Convert.ToInt32(cboZona.EditValue);
            cboEstado.Properties.ValueMember = "Clave";
            cboEstado.Properties.DisplayMember = "Des";
            cboEstado.Properties.DataSource = cl.CargaCombosCondicionSepomex();
            cboEstado.Properties.ForceInitialize();
            cboEstado.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboEstado.Properties.PopulateColumns();
            cboEstado.Properties.Columns["Clave"].Visible = false;
            cboEstado.ItemIndex = 0;

            cboCfdi.Properties.ValueMember = "Clave";
            cboCfdi.Properties.DisplayMember = "Des";
            cl.strTabla = "Usodecfdi";
            cboCfdi.Properties.DataSource = cl.CargaCombos();
            cboCfdi.Properties.ForceInitialize();
            cboCfdi.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCfdi.Properties.NullText = "Seleccione un CFDI";

            cboMetododeP.Properties.ValueMember = "Clave";
            cboMetododeP.Properties.DisplayMember = "Des";
            cl.strTabla = "Metododepago";
            cboMetododeP.Properties.DataSource = cl.CargaCombos();
            cboMetododeP.Properties.ForceInitialize();
            cboMetododeP.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMetododeP.Properties.NullText = "Seleccione un metodo de pago";

            cboFormadeP.Properties.ValueMember = "Clave";
            cboFormadeP.Properties.DisplayMember = "Des";
            cl.strTabla = "Formadepago";
            cboFormadeP.Properties.DataSource = cl.CargaCombos();
            cboFormadeP.Properties.ForceInitialize();
            cboFormadeP.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFormadeP.Properties.NullText = "Seleccione una forma de pago";

            cboAgente.Properties.ValueMember = "Clave";
            cboAgente.Properties.DisplayMember = "Des";
            cl.strTabla = "Agentes";
            cboAgente.Properties.DataSource = cl.CargaCombos();
            cboAgente.Properties.ForceInitialize();
            cboAgente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgente.Properties.ForceInitialize();
            cboAgente.Properties.PopulateColumns();
            cboAgente.Properties.Columns["Clave"].Visible = false;
            cboAgente.Properties.NullText = "Seleccione un Agente";

            cboMoneda.Properties.ValueMember = "Clave";
            cboMoneda.Properties.DisplayMember = "Des";
            cl.strTabla = "Monedas";
            cboMoneda.Properties.DataSource = cl.CargaCombos();
            cboMoneda.Properties.ForceInitialize();
            cboMoneda.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMoneda.Properties.NullText = "Seleccione una moneda";

            cboCanalesdeVenta.Properties.ValueMember = "Clave";
            cboCanalesdeVenta.Properties.DisplayMember = "Des";
            cl.strTabla = "Canalesdeventa";
            cboCanalesdeVenta.Properties.DataSource = cl.CargaCombos();
            cboCanalesdeVenta.Properties.ForceInitialize();
            cboCanalesdeVenta.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCanalesdeVenta.Properties.NullText = "Seleccione un canal de venta";

            cboUEN.Properties.ValueMember = "Clave";
            cboUEN.Properties.DisplayMember = "Des";
            cl.strTabla = "TiposUEN";
            cboUEN.Properties.DataSource = cl.CargaCombos();
            cboUEN.Properties.ForceInitialize();
            cboUEN.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboUEN.Properties.NullText = "Seleccione un tipo UEN";            

            List<tipoCL> tipoL = new List<tipoCL>();
            tipoL.Add(new tipoCL() { Clave = 0, Des = "Polarizado" });
            tipoL.Add(new tipoCL() { Clave = 1, Des = "Cadenas" });
            cboCadenas.Properties.ValueMember = "Clave";
            cboCadenas.Properties.DisplayMember = "Des";
            cboCadenas.Properties.DataSource = tipoL;
            cboCadenas.Properties.ForceInitialize();
            cboCadenas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCadenas.Properties.PopulateColumns();
            cboCadenas.Properties.Columns["Clave"].Visible = false;
            cboCadenas.Properties.NullText = "Seleccione Cadena";

            List<tipoCL> tipoLL = new List<tipoCL>();
            tipoLL.Add(new tipoCL() { Clave = 0, Des = "Ninguna" });
            tipoLL.Add(new tipoCL() { Clave = 1, Des = "HEB" });
            tipoLL.Add(new tipoCL() { Clave = 2, Des = "Soriana" });
            tipoLL.Add(new tipoCL() { Clave = 3, Des = "WalMart" });
            tipoLL.Add(new tipoCL() { Clave = 4, Des = "Sin conector" });
            tipoLL.Add(new tipoCL() { Clave = 5, Des = "Amece" });
            tipoLL.Add(new tipoCL() { Clave = 6, Des = "Casa ley" });
            tipoLL.Add(new tipoCL() { Clave = 7, Des = "Auto Zone" });
            cboAddenda.Properties.ValueMember = "Clave";
            cboAddenda.Properties.DisplayMember = "Des";
            cboAddenda.Properties.DataSource = tipoLL;
            cboAddenda.Properties.ForceInitialize();
            cboAddenda.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAddenda.Properties.PopulateColumns();
            cboAddenda.Properties.Columns["Clave"].Visible = false;
            cboAddenda.Properties.NullText = "Seleccione un addenda";

            cboTransportesID.Properties.ValueMember = "Clave";
            cboTransportesID.Properties.DisplayMember = "Des";
            cl.strTabla = "Transportes";
            cboTransportesID.Properties.DataSource = cl.CargaCombos();
            cboTransportesID.Properties.ForceInitialize();
            cboTransportesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTransportesID.Properties.NullText = "Seleccione un transporte";

            cboBancosID.Properties.ValueMember = "Clave";
            cboBancosID.Properties.DisplayMember = "Des";
            cl.strTabla = "cyb_Bancos";
            cboBancosID.Properties.DataSource = cl.CargaCombos();
            cboBancosID.Properties.ForceInitialize();
            cboBancosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboBancosID.Properties.NullText = "Seleccione un banco";

            cboFormadepagodeDep.Properties.ValueMember = "Clave";
            cboFormadepagodeDep.Properties.DisplayMember = "Des";
            cl.strTabla = "Formadepago";
            cboFormadepagodeDep.Properties.DataSource = cl.CargaCombos();
            cboFormadepagodeDep.Properties.ForceInitialize();
            cboFormadepagodeDep.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFormadepagodeDep.Properties.NullText = "Seleccione un banco";

            List<tipoCL> tipoLLL = new List<tipoCL>();
            tipoLLL.Add(new tipoCL() { Clave = 1, Des = "Local" });
            tipoLLL.Add(new tipoCL() { Clave = 2, Des = "Ocurre" });
            tipoLLL.Add(new tipoCL() { Clave = 3, Des = "A domicilio" });
            cboTipodeenvio.Properties.ValueMember = "Clave";
            cboTipodeenvio.Properties.DisplayMember = "Des";
            cboTipodeenvio.Properties.DataSource = tipoLLL;
            cboTipodeenvio.Properties.ForceInitialize();
            cboTipodeenvio.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTipodeenvio.Properties.PopulateColumns();
            cboTipodeenvio.Properties.Columns["Clave"].Visible = false;
            cboTipodeenvio.ItemIndex = 0;
            cboTipodeenvio.Properties.NullText = "Seleccione un tipo de envio";

            List<tipoCL> tipoLLLL = new List<tipoCL>();
            tipoLLLL.Add(new tipoCL() { Clave = 0, Des = "Por cobrar" });
            tipoLLLL.Add(new tipoCL() { Clave = 1, Des = "Credito" });
            tipoLLLL.Add(new tipoCL() { Clave = 2, Des = "Pre pagado" });
            cboEnviocargo.Properties.ValueMember = "Clave";
            cboEnviocargo.Properties.DisplayMember = "Des";
            cboEnviocargo.Properties.DataSource = tipoLLLL;
            cboEnviocargo.Properties.ForceInitialize();
            cboEnviocargo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboEnviocargo.Properties.PopulateColumns();
            cboEnviocargo.Properties.Columns["Clave"].Visible = false;
            cboEnviocargo.ItemIndex = 0;
            cboEnviocargo.Properties.NullText = "Seleccione un envio de cargo";

            List<tipoCL> tipoLLLLL = new List<tipoCL>();
            tipoLLLLL.Add(new tipoCL() { Clave = 0, Des = "Domingo" });
            tipoLLLLL.Add(new tipoCL() { Clave = 1, Des = "Lunes" });
            tipoLLLLL.Add(new tipoCL() { Clave = 2, Des = "Martes" });
            tipoLLLLL.Add(new tipoCL() { Clave = 3, Des = "Miercoles" });
            tipoLLLLL.Add(new tipoCL() { Clave = 4, Des = "Jueves" });
            tipoLLLLL.Add(new tipoCL() { Clave = 5, Des = "Viernes" });
            tipoLLLLL.Add(new tipoCL() { Clave = 6, Des = "Sabado" });
            cboDiadepago.Properties.ValueMember = "Clave";
            cboDiadepago.Properties.DisplayMember = "Des";
            cboDiadepago.Properties.DataSource = tipoLLLLL;
            cboDiadepago.Properties.ForceInitialize();
            cboDiadepago.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboDiadepago.Properties.PopulateColumns();
            cboDiadepago.Properties.Columns["Clave"].Visible = false;
 
            cboDiadepago.Properties.NullText = "Seleccione un dia de pago";

            cboDiaderevision.Properties.ValueMember = "Clave";
            cboDiaderevision.Properties.DisplayMember = "Des";
            cboDiaderevision.Properties.DataSource = tipoLLLLL;
            cboDiaderevision.Properties.ForceInitialize();
            cboDiaderevision.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboDiaderevision.Properties.PopulateColumns();
            cboDiaderevision.Properties.Columns["Clave"].Visible = false;          
            cboDiaderevision.Properties.NullText = "Seleccione un dia de revision";


            List<mercadoCL> mercado = new List<mercadoCL>();
            mercado.Add(new mercadoCL() { Clave = 1, Des = "Profesional" });
            mercado.Add(new mercadoCL() { Clave = 2, Des = "Consumo" });
            cboMercado.Properties.ValueMember = "Clave";
            cboMercado.Properties.DisplayMember = "Des";
            cboMercado.Properties.DataSource = mercado;
            cboMercado.Properties.ForceInitialize();
            cboMercado.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMercado.Properties.PopulateColumns();
            cboMercado.Properties.Columns["Clave"].Visible = false;
            cboMercado.ItemIndex = 0;

            cboClasificacionxtipo.Properties.ValueMember = "Clave";
            cboClasificacionxtipo.Properties.DisplayMember = "Des";
            cl.strTabla = "ClasificacionPorTipo";
            cboClasificacionxtipo.Properties.DataSource = cl.CargaCombos();
            cboClasificacionxtipo.Properties.ForceInitialize();
            cboClasificacionxtipo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboClasificacionxtipo.Properties.NullText = "Seleccione una clasificacion";

            cboGiro.Properties.ValueMember = "Clave";
            cboGiro.Properties.DisplayMember = "Des";
            cl.strTabla = "Giros";
            cboGiro.Properties.DataSource = cl.CargaCombos();
            cboGiro.Properties.ForceInitialize();
            cboGiro.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboGiro.Properties.NullText = "Seleccione una giro";

            cboRegimen.Properties.ValueMember = "Clave";
            cboRegimen.Properties.DisplayMember = "Des";
            cl.strTabla = "RegimenFiscal";
            cboRegimen.Properties.DataSource = cl.CargaCombos();
            cboRegimen.Properties.ForceInitialize();
            cboRegimen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboRegimen.Properties.NullText = "Seleccione un régimen fiscal";

        }

        private void Nuevo()
        {
            BotonesEdicion();
            intClientesID = 0;
            lblCienteID.Text = string.Empty;

        }//Nuevo()

        public class tipoCL
        {
            public int Clave { get; set; }
            public string Des { get; set; }
        }

        public class mercadoCL
        {
            public int Clave { get; set; }
            public string Des { get; set; }
        }

        private void LimpiaCajas()
        {
            txtRFC.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            cboCiudad.EditValue = null;
            txtCP.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtPlazo.Text = string.Empty;
            FechaIngreso.Text = string.Empty;
            txtcreditoauto.Text = string.Empty;
            cboCfdi.EditValue = null;
            cboMetododeP.EditValue = null;
            cboFormadeP.EditValue = null;
            swActivo.IsOn = true;
            cboAgente.EditValue = null;
            txtLista.Text = string.Empty;
            swExportar.IsOn = false; 
            cboMoneda.ItemIndex = 0;
            cboTipo.SelectedIndex = 1;
            cboCanalesdeVenta.EditValue = null;
            cboCanalesdeVenta.Properties.NullText = "Seleccione un canal de venta";
            txtPIva.Text = string.Empty;
            txtPRetisr.Text = string.Empty;
            txtPRetiva.Text = string.Empty;

            swPedirMonedaenventas.IsOn = false;
            txtResponsabledepagos.Text = string.Empty;
            txtResponsabledecompras.Text = string.Empty;
            cboUEN.EditValue = null;
            cboCadenas.EditValue = null;
            txtCuentapesos.Text = string.Empty;
            txtDolares.Text = string.Empty;
            cboTransportesID.EditValue = null;
            txtAtencionA.Text = string.Empty;
            cboTipodeenvio.EditValue = null;
            cboEnviocargo.EditValue = null;
            txtProveedorSAP.Text = string.Empty;
            txtRI.Text = string.Empty;
            txtNoProveedor.Text = string.Empty;
            cboAddenda.ItemIndex = 0;
            txtSerieEle.Text = string.Empty;
            txtBuyerGLN.Text = string.Empty;
            txtTradingPartner.Text = string.Empty;
            txtCalidadProveedor.Text = string.Empty;
            txtCalificadorEDI.Text = string.Empty;            
            txtSellerGLN.Text = string.Empty;
            txtDescuentoPP.Text = string.Empty;
            txtDescuentoBase.Text = string.Empty;
            txtDiasplazobloquear.Text = string.Empty;
            cboDiadepago.EditValue = null;
            cboDiaderevision.EditValue = null;
            txtHoraPago.Text = string.Empty;
            txtIncremental.Text = string.Empty;
            swBloqueadoporcartera.IsOn = false;
            swDesglosardescuentoalfacturar.IsOn = false;
            txtNumeroRegDtrib.Text = string.Empty;
            txtResidenciafiscal.Text = string.Empty;
        }//LimpiaCajas()

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

        }//BotonesEdicion()

        private void Guardar()
        {
            try
            {
                string result = Valida();

                if (result != "OK")
                {
                    MessageBox.Show(result);
                    return;
                }

                clientesCL cl = new clientesCL();
                cl.intClientesID = intClientesID;
                cl.strRfc = txtRFC.Text;
                cl.strNombre = txtNombre.Text;
                cl.strDireccion = txtDireccion.Text;
                cl.intCiudadesId = 0;           
                cl.strCP = txtCP.Text;
                cl.strTelefono = txtTel.Text.Replace("-", "");
                cl.strEMail = txtCorreo.Text;
                cl.fFechaingreso = Convert.ToDateTime(FechaIngreso.Text);
                cl.intPlazo = Convert.ToInt32(txtPlazo.Text);
                cl.intCreditoautorizado = Convert.ToDecimal(txtcreditoauto.Text);
                cl.strUsocfdi = Convert.ToString(cboCfdi.EditValue);
                cl.strcMetodopago = Convert.ToString(cboMetododeP.EditValue);
                cl.strcFormapago = Convert.ToString(cboFormadeP.EditValue);

                switch (cboTipo.SelectedIndex)
                {
                    case 1:
                        cboTipo.SelectedIndex = 1;
                        cl.strTipocop = "C";
                        break;

                    case 2:
                        cboTipo.SelectedIndex = 2;
                        cl.strTipocop = "P";
                        break;
                }
                cl.strActivo = swActivo.IsOn ? "1" : "0";
                cl.intAgentesID = Convert.ToInt32(cboAgente.EditValue);
                cl.intListadeprecios = Convert.ToInt32(txtLista.Text);
                cl.intExportar = swExportar.IsOn ? 1 : 0;
                cl.strMoneda = Convert.ToString(cboMoneda.EditValue);
                cl.intPIva = Convert.ToDecimal(txtPIva.Text.Replace("%", ""));
                cl.intPIeps = 0;
                cl.intPRetiva = 0;
                cl.intPRetisr = 0;
                cl.intCanalesdeventa = Convert.ToInt32(cboCanalesdeVenta.EditValue);
                cl.intBancoordenanteID = Convert.ToInt32(cboBancosID.EditValue);
                cl.strCuentaordenante = txtCuentabancaria.Text.ToString();
                cl.strCFormapagoDepositos = cboFormadepagodeDep.EditValue.ToString();

                cl.intPedirmonedaenventas = swPedirMonedaenventas.IsOn ? 1 : 0;
                cl.strResponsabledepagos = txtResponsabledepagos.Text;
                cl.strResponsabledecompras = txtResponsabledecompras.Text;
                cl.intUEN = Convert.ToInt32(cboUEN.EditValue);
                cl.intCadena = Convert.ToInt32(cboCadenas.EditValue);
                cl.strCuentapesos = txtCuentapesos.Text;
                cl.strCuentadolares = txtDolares.Text;
                cl.intTransportesID = Convert.ToInt32(cboTransportesID.EditValue);
                cl.strAtencionA = txtAtencionA.Text;
                cl.intTipodeenvio = Convert.ToInt32(cboTipodeenvio.EditValue);
                cl.intEnvioCargo = Convert.ToInt32(cboEnviocargo.EditValue);
                cl.strProveedorSAP = txtProveedorSAP.Text;
                cl.strRI = txtRI.Text;
                cl.strNumeroproveedor = txtNoProveedor.Text;
                cl.strAddenda = cboAddenda.EditValue.ToString();
                cl.strSerieEle = txtSerieEle.Text;
                cl.strBuyerGLN = txtBuyerGLN.Text;
                cl.strTradingPartner = txtTradingPartner.Text;
                cl.strCalidadProveedor = txtCalidadProveedor.Text;
                cl.strCalificadorEDI = txtCalificadorEDI.Text;
                cl.intMercado = Convert.ToInt32(cboMercado.EditValue);
                cl.strSellerGLN = txtSellerGLN.Text;
                cl.dDesctoPP = Convert.ToDecimal(txtDescuentoPP.Text);
                cl.dDescuentoBase = Convert.ToDecimal(txtDescuentoBase.Text);
                cl.intDiasPlazoBloquear = Convert.ToInt32(txtDiasplazobloquear.Text);

                cl.intDiadepago = Convert.ToInt32(cboDiadepago.EditValue);
                cl.intDiaderevision = Convert.ToInt32(cboDiaderevision.EditValue);
                cl.strHoraPago = txtHoraPago.Text;
                cl.strHoraRev = txtHoraRev.Text;
                cl.dIncremental = Convert.ToDecimal(txtIncremental.Text);
                cl.intBloqueadoporcartera = swBloqueadoporcartera.IsOn ? 1 : 0;
                cl.intDesglosardescuentoalfacturar = swDesglosardescuentoalfacturar.IsOn ? 1 : 0;
                cl.strNumReglDTrib = txtNumeroRegDtrib.Text;
                cl.strResidenciafiscal = txtResidenciafiscal.Text;

                if (intClientesID==0) //Nuevo
                    cl.intTesk = 0;
                else
                    cl.intTesk = swEnTesk.IsOn ? 1 : 0;
                cl.strceCalle = txtceCalle.Text;
                cl.strceNumext = txtceNumext.Text;
                cl.strceNumint = txtceNumint.Text;
                cl.strceColonia = txtceColonia.Text;
                cl.strceLocalidad = txtceLocalidad.Text;
                cl.strceReferencia = txtceReferencia.Text;
                cl.strceMunicipio = txtceMunicipio.Text;
                cl.strceEstado = txtceEstado.Text;
                cl.strcePais = txtcePais.Text;
                cl.strceCP = txtceCP.Text;
                cl.intZonasID = Convert.ToInt32(cboZona.EditValue);
                cl.intSepomexEstadosID = Convert.ToInt32(cboEstado.EditValue);
                cl.intSepomexCiudadesID = Convert.ToInt32(cboCiudad.EditValue);
                cl.IntSepomexColoniasID = Convert.ToInt32(cboColonia.EditValue);
                cl.intClasificacionesportipoID = Convert.ToInt32(cboClasificacionxtipo.EditValue);
                cl.intGirosID = Convert.ToInt32(cboGiro.EditValue);
                cl.strEmailTienda = txtEmailTienda.Text;
                cl.strNom40 = txtNom40.Text;
                cl.strRegimenFiscal = cboRegimen.EditValue.ToString();
                cl.strCfdiVer = txtCfdiVer.Text;


                result = cl.ClientesCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intClientesID == 0)
                    {
                        LimpiaCajas();
                    }
                }
                else
                {
                    MessageBox.Show("Error al Intentar Guardar: \n" + result);
                }
            }
            catch (Exception ex)
            {
                int linenum = ex.LineNumber();
                MessageBox.Show("Function Guardar: " + ex.Message + " LN:" + linenum.ToString());
            }
        }//Guardar

        private string Valida()
        {

            DateTime.Now.Year.ToString();

            globalCL clg = new globalCL();

            if (txtRFC.Text.Length == 0)
            {
                return "El campo Rfc no puede ir vacio";
            }
            if (txtNombre.Text.Length == 0)
            {
                return "El campo Nombre no puede ir vacio";
            }
            if (txtDireccion.Text.Length == 0)
            {
                return "El campo Direccion no puede ir vacio";
            }
            
            if (cboEstado.Text.Length == 0)
            {
                return "El campo estado no puede ir vacio";
            }
            if (cboCiudad.Text.Length == 0)
            {
                return "El campo ciudad no puede ir vacio";
            }
            if (cboColonia.Text.Length == 0)
            {
                return "El campo colonia no puede ir vacio";
            }
            if (cboClasificacionxtipo.Text.Length == 0)
            {
                return "El campo clasificacion no puede ir vacio";
            }
            if (cboGiro.Text.Length == 0)
            {
                return "El campo giro no puede ir vacio";
            }
            if (txtCP.Text.Length == 0)
            {
                return "El campo CP no puede ir vacio";
            }
            if (txtTel.Text.Length == 0)
            {
                return "El campo Telefono no puede ir vacio";
            }
            if (txtCorreo.Text.Length == 0)
            {
                return "El campo EMail no puede ir vacio";
            }
            if (!clg.esEmail(txtEmailTienda.Text))
            {
                return "Teclee el correo para la tienda (solo debe registrar uno)";
            }
            if (FechaIngreso.Text.Length == 0)
            {
                FechaIngreso.Text = DateTime.Now.ToShortDateString();
            }
            if (txtPlazo.Text.Length == 0)
            {
                txtPlazo.Text = "0";
            }
            if (txtcreditoauto.Text.Length == 0)
            {
                txtcreditoauto.Text = "0";
            }
            if (cboCfdi.Text.Length == 0)
            {
                return "El campo UsoCfdi no puede ir vacio";
            }
            if (cboMetododeP.Text.Length == 0)
            {
                return "El campo cMetodoPago no puede ir vacio";
            }
            if (cboFormadeP.Text.Length == 0)
            {
                return "El campo cFormaPago no puede ir vacio";
            }
            //if (swac.Text.Length == 0)
            //{
            //    return "El campo Activo no puede ir vacio";
            //}
            if (cboAgente.Text.Length == 0)
            {
                return "Seleccione un agente";
            }
            if (cboAgente.EditValue == null)
            {
                return "Seleccione un agente";
            }
            if (txtLista.Text.Length == 0)
            {
                return "El campo Listadeprecios no puede ir vacio";
            }
            
            if (cboMoneda.Text.Length == 0)
            {
                return "El campo Moneda no puede ir vacio";
            }
            if (cboTipo.SelectedIndex == 0)
            {
                return "El campo Tipo no puede ir vacio";
            }
            
            if (txtPIva.Text.Length == 0)
            {
                return "El campo porcentaje de iva no puede ir vacio";
            }
            if (txtPRetisr.Text.Length == 0)
            {
                txtPRetiva.Text = "0";
            }
            if (txtPRetiva.Text.Length == 0)
            {
                txtPRetiva.Text = "0";
            }

            if (cboBancosID.EditValue == null)
            {
                cboBancosID.ItemIndex = 0;
            }
            if (txtCuentabancaria.Text.Length == 0)
            {
                txtCuentabancaria.Text = "0";
            }
            if (cboFormadepagodeDep.EditValue == null)
            {
                cboFormadepagodeDep.ItemIndex = 0;
            }
            if (txtResponsabledepagos.Text.Length == 0)
            {
                return "El campo Responsabledepagos no puede ir vacio";
            }
            if (txtResponsabledecompras.Text.Length == 0)
            {
                return "El campo Responsabledecompras no puede ir vacio";
            }
            if (cboUEN.EditValue == null)
            {
                return "El campo UEN no puede ir vacio";
            }
            if (cboCadenas.EditValue == null)
            {
                return "El campo Cadena no puede ir vacio";
            }
            if (txtCuentapesos.Text.Length == 0)
            {
                txtCuentapesos.Text = "0";
            }
            if (txtDolares.Text.Length == 0)
            {
                txtDolares.Text = "0";
            }
            if (cboTransportesID.EditValue == null)
            {
                return "El campo TransportesID no puede ir vacio";
            }
            if (txtAtencionA.Text.Length == 0)
            {
                txtAtencionA.Text = "";
            }
            if (cboTipodeenvio.EditValue == null)
            {
                return "El campo Tipodeenvio no puede ir vacio";
            }
            if (cboEnviocargo.EditValue == null)
            {
                return "El campo EnvioCargo no puede ir vacio";
            }
            if (txtProveedorSAP.Text.Length == 0)
            {
                txtProveedorSAP.Text = "0";
            }
            if (txtRI.Text.Length == 0)
            {
                txtRI.Text = "0";
            }
            if (txtNoProveedor.Text.Length == 0)
            {
                txtNoProveedor.Text = "0";
            }
            if (cboAddenda.Text.Length == 0)
            {
                return "El campo Addenda no puede ir vacio";
            }
            if (txtSerieEle.Text.Length == 0)
            {
                txtSerieEle.Text = "";
            }
            if (txtBuyerGLN.Text.Length == 0)
            {
                txtBuyerGLN.Text = "0";
            }
            if (txtTradingPartner.Text.Length == 0)
            {
                txtTradingPartner.Text = "0";
            }
            if (txtCalidadProveedor.Text.Length == 0)
            {
                txtCalidadProveedor.Text = "";
            }
            if (txtCalificadorEDI.Text.Length == 0)
            {
                txtCalificadorEDI.Text = "";
            }
            
            if (txtSellerGLN.Text.Length == 0)
            {
                txtSellerGLN.Text = "0";
            }
            if (txtDescuentoPP.Text.Length == 0)
            {
                txtDescuentoPP.Text = "0";
            }
            if (txtDescuentoBase.Text.Length == 0)
            {
                txtDescuentoBase.Text = "0";
            }
            if (txtDiasplazobloquear.Text.Length == 0)
            {
                txtDiasplazobloquear.Text = "0";
            }
            if (cboDiadepago.EditValue == null)
            {
                return "El campo Diadepago no puede ir vacio";
            }
            if (cboDiaderevision.EditValue == null)
            {
                return "El campo Diaderevision no puede ir vacio";
            }
            if (txtHoraPago.Text.Length == 0)
            {
                txtHoraPago.Text = "";
            }
            if (txtIncremental.Text.Length == 0)
            {
                txtIncremental.Text = "0";
            }
    
            if (txtNumeroRegDtrib.Text.Length == 0)
            {
                txtNumeroRegDtrib.Text = "";
            }
            if (txtResidenciafiscal.Text.Length == 0)
            {
                txtResidenciafiscal.Text = "";
            }
            if (cboCanalesdeVenta.ItemIndex==-1)
            {
                return "Seleccione el canal de venta";
            }

            if (cboRegimen == null)
            {
                return "Seleccione un régimen fiscal";
            }

            if (txtCfdiVer.Text!="3.3" && txtCfdiVer.Text != "4.0")
            {
                return "Teclee Cfdi 3.3 o 4.0";
            }

            string strRegimen = cboRegimen.EditValue.ToString();
            if (strRegimen.Length == 0)
            {
                return "Seleccione un régimen fiscal";
            }

            txtNom40.Text = txtNom40.Text.ToUpper();
            if (txtNom40.Text.Length == 0 || (txtNom40.Text.Length > txtNombre.Text.Length && strRegimen=="601"))
            {
                return "El nombre 4.0 está incorrecto";
            }

            
            return "OK";

        }//Valida()

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();

        }//Editar()

        private void llenaCajas()
        {
            clientesCL cl = new clientesCL();
            cl.intClientesID = intClientesID;
            string result = cl.ClientesLlenaCajas();
            if (result == "OK")
            {
                lblCienteID.Text = intClientesID.ToString();
                intClientesID = cl.intClientesID;
                txtRFC.Text = cl.strRfc;
                txtNombre.Text = cl.strNombre;
                txtDireccion.Text = cl.strDireccion;
                //cboCiudad.EditValue = cl.intCiudadesId;
                txtCP.Text = cl.strCP;
                txtTel.Text = cl.strTelefono;
                txtCorreo.Text = cl.strEMail;
                FechaIngreso.Text = cl.fFechaingreso.ToShortDateString();
                txtPlazo.Text = Convert.ToString(cl.intPlazo);
                txtcreditoauto.Text = Convert.ToString(cl.intCreditoautorizado);
                cboCfdi.EditValue = cl.strUsocfdi;
                cboMetododeP.EditValue = cl.strcMetodopago;
                cboFormadeP.EditValue = cl.strcFormapago;
                switch (cl.strTipocop)
                {
                    case "C":
                        cboTipo.SelectedIndex = 1;

                        break;

                    case "P":
                        cboTipo.SelectedIndex = 2;

                        break;
                }
                if (cl.strActivo == "1") { swActivo.IsOn = true; }
                else { swActivo.IsOn = false; }
                cboAgente.EditValue = cl.intAgentesID;
                txtLista.Text = Convert.ToString(cl.intListadeprecios);
                if (cl.intExportar == 1) { swExportar.IsOn = true; }
                else { swExportar.IsOn = false; }
                cboMoneda.EditValue = cl.strMoneda;
                txtPIva.Text = cl.intPIva.ToString();
           
                txtPRetiva.Text = cl.intPRetiva.ToString();
                txtPRetisr.Text = cl.intPRetisr.ToString();
                cboCanalesdeVenta.EditValue = cl.intCanalesdeventa;

                cboBancosID.EditValue = cl.intBancoordenanteID;
                txtCuentabancaria.Text = cl.strCuentaordenante;
                cboFormadepagodeDep.EditValue = cl.strCFormapagoDepositos;
                swPedirMonedaenventas.IsOn = cl.intPedirmonedaenventas == 1 ? true : false;
                swEnTesk.IsOn = cl.intTesk == 1 ? true : false;
                txtResponsabledepagos.Text = cl.strResponsabledepagos;
                txtResponsabledecompras.Text = cl.strResponsabledecompras;
                cboUEN.EditValue = cl.intUEN;
                cboCadenas.EditValue = cl.intCadena;
                txtCuentapesos.Text = cl.strCuentapesos;                
                txtDolares.Text = cl.strCuentadolares;
                cboTransportesID.EditValue = cl.intTransportesID;
                txtAtencionA.Text = cl.strAtencionA;
                cboTipodeenvio.EditValue = cl.intTipodeenvio;
                cboEnviocargo.EditValue = cl.intEnvioCargo;
                txtProveedorSAP.Text = cl.strProveedorSAP;
                txtRI.Text = cl.strRI;
                txtNoProveedor.Text = cl.strNumeroproveedor;
                cboAddenda.EditValue = Convert.ToInt32(cl.strAddenda);
                txtSerieEle.Text = cl.strSerieEle;
                txtBuyerGLN.Text = cl.strBuyerGLN;
                txtTradingPartner.Text = cl.strTradingPartner;
                txtCalidadProveedor.Text = cl.strCalidadProveedor;
                txtCalificadorEDI.Text = cl.strCalificadorEDI;
                cboMercado.EditValue = cl.intMercado;
                txtSellerGLN.Text = cl.strSellerGLN;
                txtDescuentoPP.Text = cl.dDesctoPP.ToString();
                txtDescuentoBase.EditValue = cl.dDescuentoBase;
                txtDiasplazobloquear.Text = cl.intDiasPlazoBloquear.ToString();
                cboDiadepago.EditValue = cl.intDiadepago;
                cboDiaderevision.EditValue = cl.intDiaderevision;
                txtHoraPago.Text = cl.strHoraPago;
                txtHoraRev.Text = cl.strHoraRev;
                txtIncremental.Text = cl.dIncremental.ToString();
                swBloqueadoporcartera.IsOn = cl.intBloqueadoporcartera == 1 ? true : false;
                swDesglosardescuentoalfacturar.IsOn = cl.intDesglosardescuentoalfacturar == 1 ? true : false;
                txtNumeroRegDtrib.Text = cl.strNumReglDTrib;
                txtResidenciafiscal.Text = cl.strResidenciafiscal;
                txtceCalle.Text = cl.strceCalle;
                txtceNumext.Text = cl.strceNumext;
                txtceNumint.Text = cl.strceNumint;
                txtceColonia.Text = cl.strceColonia;
                txtceLocalidad.Text = cl.strceLocalidad;
                txtceReferencia.Text = cl.strceReferencia;
                txtceMunicipio.Text = cl.strceMunicipio;
                txtceEstado.Text = cl.strceEstado;
                txtcePais.Text = cl.strcePais;
                txtceCP.Text = cl.strceCP;
                //cboZona.EditValue = cl.intZonasID;
                cboEstado.EditValue = cl.intSepomexEstadosID;
                cboCiudad.EditValue = cl.intSepomexCiudadesID;
                cboColonia.EditValue = cl.IntSepomexColoniasID;
                cboClasificacionxtipo.EditValue = cl.intClasificacionesportipoID;
                cboGiro.EditValue = cl.intGirosID;
                txtEmailTienda.Text = cl.strEmailTienda;
                txtNom40.Text = cl.strNom40;
                cboRegimen.EditValue = cl.strRegimenFiscal;
                txtCfdiVer.Text = cl.strCfdiVer;
                
            }

            else
            {
                MessageBox.Show(result);
            }
        }//llenaCajas()

        private void Eliminar()
        {
            clientesCL cl = new clientesCL();
            cl.intClientesID = intClientesID;
            string result = cl.ClientesEliminar();

            if (result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(result);
            }
        }//Eliminar

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intClientesID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intClientesID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult result = MessageBox.Show("Desea eliminar la línea " + intClientesID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intClientesID = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ClientesID"));
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Menú principal","Cargando información");
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LlenarGrid();
            LimpiaCajas();
            navigationFrame.SelectedPageIndex = 0;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridClientes" +
                "";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiVista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grdClientes.ShowRibbonPrintPreview();
        }

        private void labelControl66_Click(object sender, EventArgs e)
        {

        }

        private void labelControl73_Click(object sender, EventArgs e)
        {

        }

        private void cboZona_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cboZona.EditValue) == 0) { }
                else
                {
                    combosCL cl = new combosCL();
                                      
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("cboEstado: " + ex);
            }
        }    

        private void cboEstado_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cboEstado.EditValue) == 0) { }
                else
                {
                    combosCL cl = new combosCL();
                    cl.strTabla = "Ciudades";
                    cl.intClave = Convert.ToInt32(cboEstado.EditValue);
                    cboCiudad.Properties.ValueMember = "Clave";
                    cboCiudad.Properties.DisplayMember = "Des";
                    cboCiudad.Properties.DataSource = cl.CargaCombosCondicionSepomex();
                    cboCiudad.Properties.ForceInitialize();
                    cboCiudad.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                    cboCiudad.Properties.PopulateColumns();
                    cboCiudad.Properties.Columns["Clave"].Visible = false;
                    cboCiudad.ItemIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("cboCiudad: " + ex);
            }
        }

        private void cboCiudad_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cboCiudad.EditValue) == 0) { }
                else
                {
                    combosCL cl = new combosCL();
                    cl.strTabla = "Colonias";
                    cl.intClave = Convert.ToInt32(cboCiudad.EditValue);
                    cboColonia.Properties.ValueMember = "Clave";
                    cboColonia.Properties.DisplayMember = "Des";
                    cboColonia.Properties.DataSource = cl.CargaCombosCondicionSepomex();
                    cboColonia.Properties.ForceInitialize();
                    cboColonia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                    cboColonia.Properties.PopulateColumns();
                    cboColonia.Properties.Columns["Clave"].Visible = false;
                    cboColonia.ItemIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("cboCiudad: " + ex);
            }

        }

        private void labelControl83_Click(object sender, EventArgs e)
        {

        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCP_Click(object sender, EventArgs e)
        {
            buscaSepomexCP();
        }
        private void buscaSepomexCP()
        {
            try
            {
                globalCL clG = new globalCL();
                if (!clG.esNumerico(txtCP.Text))
                {
                    MessageBox.Show("Teclee un código postal");
                    return;
                }

                DataTable dt = new DataTable();

                combosCL cl = new combosCL();
                cl.strTabla = "CP";
                cl.intClave = Convert.ToInt32(txtCP.Text);                
                dt = cl.CargaCombosCondicionSepomex();

                if (dt.Rows.Count > 0)
                {
                    int cd;
                    int edo;

                    DataRow dr;

                    dr = dt.Rows[0];

                    edo = Convert.ToInt32(dr["c_estado"]);
                    cd = Convert.ToInt32(dr["Ciudad"]);

                    cboEstado.EditValue = edo;
                    cboCiudad.EditValue = cd;

                    cboColonia.Properties.ValueMember = "Clave";
                    cboColonia.Properties.DisplayMember = "Des";
                    cboColonia.Properties.DataSource = dt;
                    cboColonia.Properties.ForceInitialize();
                    cboColonia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                    cboColonia.Properties.PopulateColumns();
                    cboColonia.Properties.Columns["Clave"].Visible = false;
                    cboColonia.ItemIndex = 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCorreo_Leave(object sender, EventArgs e)
        {
            if (Ribboncontrol.Text.Length == 0)
                Ribboncontrol.Text = txtCorreo.Text;
        }

        private void labelControl90_Click(object sender, EventArgs e)
        {

        }

        private void labelControl91_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (txtNom40.Text.Length == 0)
                txtNom40.Text = txtNombre.Text.ToUpper();
        }
    }
}