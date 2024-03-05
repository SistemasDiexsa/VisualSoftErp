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
using System.IO;

namespace VisualSoftErp.Catalogos.Inv
{
    public partial class Articulos2 : DevExpress.XtraBars.Ribbon.RibbonForm
    {


        #region Variables
        int intArticulosID;
        int intFamiliaID, intUnidadID, intTAID;
        string strimagen;
        int intEsunkit;
        int iActivo = 1;
        bool blnSinBaseParacosteo;
        string strArticuloBase = string.Empty;
        bool blCodigoArticulo = false;
        string strArticuloDB = string.Empty;
        string strLineasID = string.Empty;
        string strFamiliasID = string.Empty;
        string strSubFamiliasID = string.Empty;
        bool boolEditando = false;
        #endregion

        public Articulos2()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Catálogo de Articulos";

            LlenarGrid(1);
            llenarcombos();
            soloLectura();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();



        }

        #region Metodos
        private void soloLectura()
        {
            globalCL clg = new globalCL();
            clg.strPrograma = "0307";
            if (clg.accesoSoloLectura())
            {
                bbiGuardarr.Enabled = false;
                bbiEliminarr.Enabled = false;
            }
        }
        private void LlenarGrid(int Op)
        {
            articulosCL cl = new articulosCL();
            cl.intActivo = Op;
            gridControl1.DataSource = cl.articulosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridArticulos";
            clg.restoreLayout(gridView1);
            gridView1.OptionsView.ShowAutoFilterRow = true;

        }//LlenarGrid()

        private void CargaSubFamilias()
        {
            combosCL cl = new combosCL();

            cl.strTabla = "SubfamiliasXfamilias";
            cl.iCondicion = intFamiliaID;
            cboSubFamiliasIDClasificaciones.Properties.ValueMember = "Clave";
            cboSubFamiliasIDClasificaciones.Properties.DisplayMember = "Des";
            cboSubFamiliasIDClasificaciones.Properties.DataSource = cl.CargaCombos();
            cboSubFamiliasIDClasificaciones.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSubFamiliasIDClasificaciones.Properties.NullText = "Seleccione una familia";
            cboSubFamiliasIDClasificaciones.Properties.ForceInitialize();
            cboSubFamiliasIDClasificaciones.Properties.PopulateColumns();
            cboSubFamiliasIDClasificaciones.Properties.Columns["Clave"].Visible = false;

            cboSubFamiliasID.Properties.ValueMember = "Clave";
            cboSubFamiliasID.Properties.DisplayMember = "Des";
            cboSubFamiliasID.Properties.DataSource = cl.CargaCombos();
            cboSubFamiliasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSubFamiliasID.Properties.NullText = "Seleccione una familia";
            cboSubFamiliasID.Properties.PopulateColumns();
            cboSubFamiliasID.Properties.ForceInitialize();
            // cboSubFamiliasID.Properties.Columns["Clave"].Visible = false;
        }
        
        public void llenarcombos()
        {
            combosCL cl = new combosCL();

            cboFamiliasClasificaciones.Properties.ValueMember = "Clave";
            cboFamiliasClasificaciones.Properties.DisplayMember = "Des";
            cl.strTabla = "Familias";
            cboFamiliasClasificaciones.Properties.DataSource = cl.CargaCombos();
            cboFamiliasClasificaciones.Properties.ForceInitialize();
            cboFamiliasClasificaciones.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamiliasClasificaciones.Properties.ForceInitialize();
            cboFamiliasClasificaciones.Properties.PopulateColumns();
            cboFamiliasClasificaciones.Properties.Columns["Clave"].Visible = false;
            cboFamiliasClasificaciones.Properties.Columns["LineasID"].Visible = false;
            cboFamiliasClasificaciones.Properties.Columns["CodigoArticulo"].Visible = false;
            cboProveedoresID.Properties.NullText = "Seleccione un familia";

            // COMBO FAMILIAS SUPERIOR
            cboFamilias.Properties.ValueMember = "Clave";
            cboFamilias.Properties.DisplayMember = "Des";
            cboFamilias.Properties.DataSource = cl.CargaCombos();
            cboFamilias.Properties.ForceInitialize();
            cboFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilias.Properties.PopulateColumns();
            cboFamilias.Properties.Columns["Clave"].Visible = false;
            cboFamilias.Properties.Columns["LineasID"].Visible = false;
            cboFamilias.Properties.Columns["CodigoArticulo"].Visible = false;
            cboProveedoresID.Properties.NullText = "Seleccione un familia";

            // COMBO UNIDAD
            cboUnidad.Properties.ValueMember = "Clave";
            cboUnidad.Properties.DisplayMember = "Des";
            cl.strTabla = "Unidadesdemedida";
            cboUnidad.Properties.DataSource = cl.CargaCombos();
            cboUnidad.Properties.ForceInitialize();
            cboUnidad.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboUnidad.Properties.ForceInitialize();
            cboUnidad.Properties.PopulateColumns();
            cboUnidad.Properties.Columns["Clave"].Visible = false;

            cboTA.Properties.ValueMember = "Clave";
            cboTA.Properties.DisplayMember = "Des";
            cl.strTabla = "Tiposdearticulo";
            cboTA.Properties.DataSource = cl.CargaCombos();
            cboTA.Properties.ForceInitialize();
            cboTA.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTA.Properties.ForceInitialize();
            cboTA.Properties.PopulateColumns();
            cboTA.Properties.Columns["Clave"].Visible = false;

            // COMBO PROVEEDORES
            cl.strTabla = "Proveedoresrep";
            cboProveedoresID.Properties.ValueMember = "Clave";
            cboProveedoresID.Properties.DisplayMember = "Des";
            cboProveedoresID.Properties.DataSource = cl.CargaCombos();
            cboProveedoresID.Properties.ForceInitialize();
            cboProveedoresID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedoresID.Properties.PopulateColumns();
            cboProveedoresID.Properties.Columns["Clave"].Visible = false;
            cboProveedoresID.Properties.NullText = "Seleccione un proveedor";

            // COMBO MONEDAS
            cl.strTabla = "Monedas";
            cboMonedasID.Properties.ValueMember = "Clave";
            cboMonedasID.Properties.DisplayMember = "Des";
            cboMonedasID.Properties.DataSource = cl.CargaCombos();
            cboMonedasID.Properties.ForceInitialize();
            cboMonedasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedasID.Properties.PopulateColumns();
            cboMonedasID.Properties.Columns["Clave"].Visible = false;
            cboMonedasID.Properties.NullText = "Seleccione una moneda";

            // COMBO MONEDAS
            cl.strTabla = "Monedas";
            cboMonedaCosto.Properties.ValueMember = "Clave";
            cboMonedaCosto.Properties.DisplayMember = "Des";
            cboMonedaCosto.Properties.DataSource = cl.CargaCombos();
            cboMonedaCosto.Properties.ForceInitialize();
            cboMonedaCosto.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedaCosto.Properties.PopulateColumns();
            cboMonedaCosto.Properties.Columns["Clave"].Visible = false;
            cboMonedaCosto.Properties.NullText = "Seleccione una moneda";

            // COMBO UNIDADES DE MEDIDA
            cl.strTabla = "Unidadesdemedida";
            cboUnidaddemedidasecundaria.Properties.ValueMember = "Clave";
            cboUnidaddemedidasecundaria.Properties.DisplayMember = "Des";
            cboUnidaddemedidasecundaria.Properties.DataSource = cl.CargaCombos();
            cboUnidaddemedidasecundaria.Properties.ForceInitialize();
            cboUnidaddemedidasecundaria.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboUnidaddemedidasecundaria.Properties.PopulateColumns();
            cboUnidaddemedidasecundaria.Properties.Columns["Clave"].Visible = false;
            cboUnidaddemedidasecundaria.Properties.NullText = "Seleccione una unidad de medida secundaria";

            // COMBO MARCAS
            cl.strTabla = "Marcas";
            cboMarcasID.Properties.ValueMember = "Clave";
            cboMarcasID.Properties.DisplayMember = "Des";
            cboMarcasID.Properties.DataSource = cl.CargaCombos();
            cboMarcasID.Properties.ForceInitialize();
            cboMarcasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMarcasID.Properties.PopulateColumns();
            cboMarcasID.Properties.Columns["Clave"].Visible = false;
            cboMarcasID.Properties.NullText = "Seleccione una marca";

            // COMBO ARTICULOS
            cl.strTabla = "Articulos";
            cboArticulobaseparacosteoID.Properties.ValueMember = "Clave";
            cboArticulobaseparacosteoID.Properties.DisplayMember = "Des";
            cboArticulobaseparacosteoID.Properties.DataSource = cl.CargaCombos();
            cboArticulobaseparacosteoID.Properties.ForceInitialize();
            cboArticulobaseparacosteoID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulobaseparacosteoID.Properties.PopulateColumns();
            cboArticulobaseparacosteoID.Properties.Columns["Clave"].Visible = false;
            cboArticulobaseparacosteoID.Properties.NullText = "Seleccione una Articulo base para costeo";

            // COMBO CLASIFICACION
            List<tipoCL> tipoL = new List<tipoCL>();
            tipoL.Add(new tipoCL() { Clave = 0, Des = "No aplica" });
            tipoL.Add(new tipoCL() { Clave = 1, Des = "Componente" });
            tipoL.Add(new tipoCL() { Clave = 2, Des = "Producto terminado" });
            cboClasificacionproduccion.Properties.ValueMember = "Clave";
            cboClasificacionproduccion.Properties.DisplayMember = "Des";
            cboClasificacionproduccion.Properties.DataSource = tipoL;
            cboClasificacionproduccion.Properties.ForceInitialize();
            cboClasificacionproduccion.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboClasificacionproduccion.Properties.PopulateColumns();
            cboClasificacionproduccion.Properties.Columns["Clave"].Visible = false;
            cboClasificacionproduccion.ItemIndex = 0;
            cboClasificacionproduccion.Properties.NullText = "Seleccione una clasificacion de produccion";

            // COMBO TIPO DE CORTE
            List<tipoCL> tipoLL = new List<tipoCL>();
            tipoLL.Add(new tipoCL() { Clave = 0, Des = "No aplica" });
            tipoLL.Add(new tipoCL() { Clave = 1, Des = "Se corta" });
            tipoLL.Add(new tipoCL() { Clave = 2, Des = "Se produce" });
            tipoLL.Add(new tipoCL() { Clave = 3, Des = "Se corta y se produce" });
            cboTipodecorte.Properties.ValueMember = "Clave";
            cboTipodecorte.Properties.DisplayMember = "Des";
            cboTipodecorte.Properties.DataSource = tipoLL;
            cboTipodecorte.Properties.ForceInitialize();
            cboTipodecorte.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTipodecorte.Properties.PopulateColumns();
            cboTipodecorte.Properties.Columns["Clave"].Visible = false;
            cboTipodecorte.Properties.NullText = "Seleccione un tipo de corte";

        }//llenarcombos

        public class tipoCL
        {
            public int Clave { get; set; }
            public string Des { get; set; }
        }

        private void Nuevo()
        {

            BotonesEdicion();
            boolEditando = false;
            intArticulosID = 0;
            cboFamilias.Enabled = true;
            cboFamiliasClasificaciones.Enabled = true;
            cboSubFamiliasID.Enabled = true;
            cboSubFamiliasIDClasificaciones.Enabled = true;
            blnSinBaseParacosteo = false;
            cboUnidad.EditValue = 0;
            cboTA.EditValue = 0;
            txtArticulo.Focus();
            LimpiaCajas();

        }//Nuevo()

        private void LimpiaCajas()
        {
            blnSinBaseParacosteo = false;
            txtArticulo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtNombreOC.Text = string.Empty;
            // cboFamilias.ItemIndex = 0;
            cboFamilias.EditValue = null;
            //cboFamiliasClasificaciones.ItemIndex = 0;
            cboFamiliasClasificaciones.EditValue = null;
            cboUnidad.ItemIndex = 0;
            cboTA.ItemIndex = 0;
            txtPieps.Text = string.Empty;
            txtPiva.Text = string.Empty;
            swExistencia.IsOn = false;
            txtClaveSat.Text = string.Empty;
            //txtImagen.Text = "X";
            swActivo.IsOn = true;
            swEsunkit.IsOn = false;
            txtDiasdeentrega.Text = string.Empty;
            txtMaximo.Text = string.Empty;
            txtMinimo.Text = string.Empty;
            txtReorden.Text = string.Empty;


            swExistencia.IsOn = false;
            txtClaveSat.Text = string.Empty;
            //txtImagen.Text = string.Empty;

            txtDiasdeentrega.Text = string.Empty;
            cboArticulobaseparacosteoID.ItemIndex = 0;

            swEsunkit.IsOn = false;
            swObsoleto.IsOn = false;
            txtMargenarribadelcosto.Text = string.Empty;
            cboProveedoresID.EditValue = null;
            cboMonedasID.ItemIndex = 0;
            cboUnidaddemedidasecundaria.EditValue = null;
            txtFactorUM2.Text = string.Empty;
            txtGrupodearticulos.Text = string.Empty;
            txtDiasstock.Text = string.Empty;
            txtEanAmece.Text = string.Empty;
            txtUmAmece.Text = string.Empty;
            swExcluirdereportes.IsOn = false;
            cboMarcasID.EditValue = null;
            swManejapedimentos.IsOn = false;
            txtClasificacion.Text = string.Empty;
            txtCodigodebarras.Text = string.Empty;
            txtUbicacion.Text = string.Empty;
            txtFactorauxiliar.Text = string.Empty;
            txtMetroslinealesunidadesporpieza.Text = string.Empty;
            cboArticulobaseparacosteoID.EditValue = null;
            swCosteodirectoenfactura.IsOn = false;
            swRedondear.IsOn = false;
            swSeCompraEnRollos.IsOn = false;
            cboClasificacionproduccion.EditValue = null;
            cboTipodecorte.EditValue = null;
            txtAncho.Text = string.Empty;
            txtLargo.Text = string.Empty;

            txtfraccion.Text = string.Empty;
            txtunidad.Text = string.Empty;
            cboSubFamiliasIDClasificaciones.EditValue = null;
            cboProveedoresID.EditValue = null;
            //txtPathImagen.Text = string.Empty;
            //txtBreveDescripcion.Text = string.Empty;
            swTiendaOnline.IsOn = false;
            cboMonedasID.EditValue = null;
            txtPrecioventa.Text = string.Empty;
            txtPesoKg.Text = string.Empty;
            txtDimenLargo.Text = string.Empty;
            txtDimenAlto.Text = string.Empty;
            txtDimenAncho.Text = string.Empty;

            //txtArtBaseCosteoDX.Text = string.Empty;

        }//LimpiaCajas()

        private void BotonesEdicion()
        {
            boolEditando = true;
            LimpiaCajas();
            ribbonPageGroupAcciones.Visible = false;
            ribbonPageGroup2.Visible = true;

            navigationFrame.SelectedPageIndex = 1;

        }//BotonesEdicion()

        private void Guardar()
        {
            try
            {
                string result = Valida();

                if (result != "OK")
                {
                    if (result.Length > 0)
                    {
                        MessageBox.Show(result);

                    }
                    return;

                }

                articulosCL cl = new articulosCL();

                cl.intArticulosID = intArticulosID;
                cl.strArticulo = txtArticulo.Text;
                cl.strNombre = txtNombre.Text;
                cl.strNombreOC = txtNombreOC.Text;
                cl.intProveedoresID = Convert.ToInt32(cboProveedoresID.EditValue);
                cl.strClaveSat = txtClaveSat.Text;
                cl.fFechaalta = Convert.ToDateTime(dateEditFechaalta.Text);
                cl.intActivo = swActivo.IsOn ? 1 : 0;

                cl.dPrecioventa = Convert.ToDecimal(txtPrecioventa.Text);
                cl.strMonedasID = cboMonedasID.EditValue.ToString();
                cl.dPtjeIva = Convert.ToDecimal(txtPiva.Text);
                cl.dPtjeIeps = Convert.ToDecimal(txtPieps.Text);

                cl.intFamiliasID = Convert.ToInt32(cboFamiliasClasificaciones.EditValue);
                cl.intSubFamiliaID = Convert.ToInt32(cboSubFamiliasIDClasificaciones.EditValue);
                cl.intTiposdearticuloID = Convert.ToInt32(cboTA.EditValue);

                cl.intUnidadesdemedidaID = Convert.ToInt32(cboUnidad.EditValue);
                cl.intUnidaddemedidasecundaria = Convert.ToInt32(cboUnidaddemedidasecundaria.EditValue);
                cl.decFactorUM2 = Convert.ToDecimal(txtFactorUM2.Text);
                cl.intEsunkit = swEsunkit.IsOn ? 1 : 0;

                cl.dGrupodearticulos = Convert.ToDecimal(txtGrupodearticulos.Text);
                cl.intDiasstock = Convert.ToInt32(txtDiasstock.Text);
                cl.strUnidadAduana = txtunidad.Text;
                cl.dKilosAduana = Convert.ToDecimal(txtkilos.Text);

                cl.strEanAmece = txtEanAmece.Text;
                cl.strUmAmece = txtUmAmece.Text;
                cl.intObsoleto = swObsoleto.IsOn ? 1 : 0;

                cl.intExcluirdereportes = swExcluirdereportes.IsOn ? 1 : 0;
                cl.intMarcasID = Convert.ToInt32(cboMarcasID.EditValue);

                cl.intManejaExistencia = swExistencia.IsOn ? 1 : 0;
                cl.intManejapedimentos = swManejapedimentos.IsOn ? 1 : 0;
                cl.strClasificacion = txtClasificacion.Text;
                cl.intTiempodeentrega = Convert.ToInt32(txtDiasdeentrega.Text);
                cl.intMaximo = Convert.ToInt32(txtMaximo.Text);
                cl.intMinimo = Convert.ToInt32(txtMinimo.Text);
                cl.intReorden = Convert.ToInt32(txtReorden.Text);
                cl.strCodigodebarras = txtCodigodebarras.Text;
                cl.strUbicación = Convert.ToString(txtUbicacion.Text);
                cl.dFactorauxiliar = Convert.ToDecimal(txtFactorauxiliar.Text);

                cl.strFraccionArancelaria = txtfraccion.Text;

                cl.dMetroslinealesunidadesporpieza = Convert.ToDecimal(txtMetroslinealesunidadesporpieza.Text);
                if (blnSinBaseParacosteo)
                    cl.intArticulobaseparacosteoID = 0;
                else
                    cl.intArticulobaseparacosteoID = Convert.ToInt32(cboArticulobaseparacosteoID.EditValue);
                cl.intCosteodirectoenfactura = swCosteodirectoenfactura.IsOn ? 1 : 0;
                cl.intRedondear = swRedondear.IsOn ? 1 : 0;
                cl.intSeCompraEnRollos = swSeCompraEnRollos.IsOn ? 1 : 0;
                cl.intClasificacionproduccion = Convert.ToInt32(cboClasificacionproduccion.EditValue);

                cl.intTipodecorte = Convert.ToInt32(cboTipodecorte.EditValue);
                cl.dAncho = Convert.ToDecimal(txtAncho.Text);
                cl.dLargo = Convert.ToDecimal(txtLargo.Text);

                cl.intTiendaonline = swTiendaOnline.IsOn ? 1 : 0;

                cl.dMargenarribadelcosto = Convert.ToDecimal(txtMargenarribadelcosto.Text);
                cl.strBreveDescripcion = ""; //txtBreveDescripcion.Text;
                cl.strImagen = ""; // txtImagen.Text;
                cl.strPathImagen = ""; //txtPathImagen.Text;

                cl.strArtBaseCosteoDX = ""; //txtArtBaseCosteoDX.Text;

                cl.dPesoKg = Convert.ToDecimal(txtPesoKg.Text);
                cl.dDimenLargo = Convert.ToDecimal(txtDimenLargo.Text);
                cl.dDimenAlto = Convert.ToDecimal(txtDimenAlto.Text);
                cl.dDimenAncho = Convert.ToDecimal(txtDimenAncho.Text);

                cl.intDisponibleTiendaonline = swDisponibleentienda.IsOn ? 1 : 0;

                result = cl.ArticulosCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intArticulosID == 0)
                    {
                        LimpiaCajas();
                    }

                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        }//Guardar

        private string Valida()
        {

            globalCL clg = new globalCL();

            if (intArticulosID == Convert.ToInt32(cboArticulobaseparacosteoID.EditValue))
            {
                cboArticulobaseparacosteoID.EditValue = null;
                return "No se puede poner el mismo artículo como base para costeo";
            }

            if (!clg.esNumerico(txtPesoKg.Text))
                txtPesoKg.Text = "0";

            if (txtArticulo.Text == "")
            {
                return "El articulo no puede ir vacio";
            }

            if (!clg.esNumerico(txtMargenarribadelcosto.Text))
                txtMargenarribadelcosto.Text = "0";

            if (txtNombreOC.Text == "")
            {
                txtReorden.Text = "0";
            }
            if (txtPiva.Text == "")
            {
                DialogResult dialogResult = MessageBox.Show("El iva debe ser cero?", "Advertencia", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    txtPiva.Text = "0";
                }
                else
                {
                    return "";
                }
            }
            if (txtPieps.Text == "")
            {
                txtPieps.Text = "0";
            }
            if (txtClaveSat.Text == "")
            {
                return "La clave SAT no puede ir vacio";
            }
            //if (txtImagen.Text == "")
            //{
            //    txtImagen.Text = "X";
            //}
            if (txtDiasdeentrega.Text == "")
            {
                txtDiasdeentrega.Text = "0";
            }
            if (txtMaximo.Text == "")
            {
                txtMaximo.Text = "0";
            }
            if (txtMinimo.Text == "")
            {
                txtMinimo.Text = "0";
            }
            if (txtReorden.Text == "")
            {
                txtReorden.Text = "0";
            }
            if (txtfraccion.Text == "")
            {
                txtfraccion.Text = "";
            }
            if (txtunidad.Text == "")
            {
                txtunidad.Text = "";
            }

            if (!clg.esNumerico(txtGrupodearticulos.Text))
                txtGrupodearticulos.Text = "0";


            if (!clg.esNumerico(txtkilos.Text))
                txtkilos.Text = "0";
            if (!clg.esNumerico(txtDimenLargo.Text))
                txtDimenLargo.Text = "0";
            if (!clg.esNumerico(txtDimenAlto.Text))
                txtDimenAlto.Text = "0";
            if (!clg.esNumerico(txtDimenAncho.Text))
                txtDimenAncho.Text = "0";

            if (!clg.esNumerico(txtPrecioventa.Text))
                txtPrecioventa.Text = "0";

            if (!clg.esNumerico(txtDiasstock.Text))
                txtDiasstock.Text = "0";

            if (!clg.esNumerico(txtLargo.Text))
                txtLargo.Text = "0";

            if (Convert.ToInt32(txtLargo.Text.Replace(".00", "")) > 500)
            {
                if (swTiendaOnline.IsOn)
                {
                    DialogResult Result = MessageBox.Show("Si se trata de un Master, lo vá subir a la tienda?", "Advertencia", MessageBoxButtons.YesNo);
                    if (Result.ToString() == "Yes")
                    {
                        MessageBox.Show("No olvide capturar los tonos y medidas de este artículo");
                    }
                    else
                    {
                        return "Los master normalmente no van en la tienda";
                    }
                }

            }

            return "OK";
        }//Valida()

        private void Editar()
        {
            // Se tiene que ver el permiso para editar Familias y SubFamilias en ésta función
            BotonesEdicion();
            llenaCajas();
            ModificaFamilias();
            blnSinBaseParacosteo = false;
            xtraTabControl1.SelectedTabPage = xtraTabPage1;

        }//Editar()

        private void ModificaFamilias()
        {
            UsuariosCL cl = new UsuariosCL();
            cl.intUsuariosID = globalCL.gv_UsuarioID;
            String result = cl.UsuariosLlenaCajas();
            if(result == "OK")
            {
                bool modificaFamilias = cl.intModificaFamiliasArticulos == 1 ? true : false;
                bool modificaSubFamilias = cl.intModificaSubFamiliasArticulos == 1 ? true : false;
                if(modificaFamilias)
                {
                    cboFamilias.Enabled = true;
                    cboFamiliasClasificaciones.Enabled = true;
                } else
                {
                    cboFamilias.Enabled = false;
                    cboFamiliasClasificaciones.Enabled = false;
                }

                if (modificaSubFamilias)
                {
                    cboSubFamiliasID.Enabled = true;
                    cboSubFamiliasIDClasificaciones.Enabled = true;
                }
                else
                {
                    cboSubFamiliasID.Enabled = false;
                    cboSubFamiliasIDClasificaciones.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void llenaCajas()
        {
            articulosCL cl = new articulosCL();
            cl.intArticulosID = intArticulosID;
            string result = cl.articulosLlenaCajas();

            if (result == "OK")
            {
                strArticuloDB = cl.strArticulo;
                txtNombre.Text = cl.strNombre;
                txtNombreOC.Text = cl.strNombreOC;
                cboProveedoresID.EditValue = cl.intProveedoresID;
                txtClaveSat.Text = cl.strClaveSat;
                dateEditFechaalta.Text = cl.fFechaalta.ToShortDateString();
                swActivo.IsOn = cl.intActivo == 1 ? true : false;

                txtPrecioventa.Text = cl.dPrecioventa.ToString();
                cboMonedasID.EditValue = cl.strMonedasID;
                txtPiva.Text = cl.dPtjeIva.ToString();
                txtPieps.Text = cl.dPtjeIeps.ToString();

                cboFamiliasClasificaciones.EditValue = cl.intFamiliasID;
                cboSubFamiliasIDClasificaciones.EditValue = cl.intSubFamiliaID;
                txtArticulo.Text = cl.strArticulo;
                cboTA.EditValue = cl.intTiposdearticuloID;

                cboUnidad.EditValue = cl.intUnidadesdemedidaID;
                cboUnidaddemedidasecundaria.EditValue = cl.intUnidaddemedidasecundaria;
                txtFactorUM2.Text = cl.decFactorUM2.ToString();
                swEsunkit.IsOn = cl.intEsunkit == 1 ? true : false;

                txtGrupodearticulos.Text = cl.dGrupodearticulos.ToString();
                txtDiasstock.Text = cl.intDiasstock.ToString();
                txtunidad.Text = cl.strUnidadAduana;
                txtkilos.Text = cl.dKilosAduana.ToString();

                txtEanAmece.Text = cl.strEanAmece;
                txtUmAmece.Text = cl.strUmAmece;
                swObsoleto.IsOn = cl.intObsoleto == 1 ? true : false;

                swExcluirdereportes.IsOn = cl.intExcluirdereportes == 1 ? true : false;
                cboMarcasID.EditValue = cl.intMarcasID;



                swExistencia.IsOn = cl.intManejaExistencia == 1 ? true : false;
                swManejapedimentos.IsOn = cl.intManejapedimentos == 1 ? true : false;
                txtClasificacion.Text = cl.strClasificacion;
                txtDiasdeentrega.Text = cl.intTiempodeentrega.ToString();
                txtMaximo.Text = cl.intMaximo.ToString();
                txtMinimo.Text = cl.intMinimo.ToString();
                txtReorden.Text = cl.intReorden.ToString();
                txtCodigodebarras.Text = cl.strCodigodebarras;
                txtUbicacion.Text = cl.strUbicación;
                txtFactorauxiliar.Text = cl.dFactorauxiliar.ToString();

                txtfraccion.Text = cl.strFraccionArancelaria;

                txtMetroslinealesunidadesporpieza.Text = cl.dMetroslinealesunidadesporpieza.ToString();
                cboArticulobaseparacosteoID.EditValue = cl.intArticulobaseparacosteoID;
                lblArtBaseParaCosteo.Text = cl.strArtBase + " / " + cl.intArticulobaseparacosteoID.ToString();
                cl.intCosteodirectoenfactura = swCosteodirectoenfactura.IsOn ? 1 : 0;
                swRedondear.IsOn = cl.intRedondear == 1 ? true : false;
                swSeCompraEnRollos.IsOn = cl.intSeCompraEnRollos == 1 ? true : false;
                cboClasificacionproduccion.EditValue = cl.intClasificacionproduccion;

                cboTipodecorte.EditValue = cl.intTipodecorte;
                txtAncho.Text = cl.dAncho.ToString();
                txtLargo.Text = cl.dLargo.ToString();

                swTiendaOnline.IsOn = cl.intTiendaonline == 1 ? true : false;

                swDisponibleentienda.IsOn = cl.intDisponibleTiendaonline == 1 ? true : false;
                txtMargenarribadelcosto.Text = cl.dMargenarribadelcosto.ToString();

                txtPesoKg.Text = cl.dPesoKg.ToString();
                txtDimenLargo.Text = cl.dDimenLargo.ToString();
                txtDimenAlto.Text = cl.dDimenAlto.ToString();
                txtDimenAncho.Text = cl.dDimenAncho.ToString();

                strArticuloBase = cl.strArtBase;

                gridControlCostos.DataSource = cl.ArticulosCostosGrid();
                //CargaSubFamilias();

                //txtBreveDescripcion.Text=cl.strBreveDescripcion;
                //txtImagen.Text=cl.strImagen;
                //txtPathImagen.Text=cl.strPathImagen;

                //txtArtBaseCosteoDX.Text = cl.strArtBaseCosteoDX;

            }

            else
            {
                MessageBox.Show(result);
            }
        }//llenaCajas()

        private void Eliminar()
        {

            KitsCL clk = new KitsCL();
            clk.intArticulosID = intArticulosID;
            string result = clk.KitsExiste();
            if (result == "OK")
            {
                MessageBox.Show("No se puede eliminar por que tiene formado un kit, elimine primero el kit");
                return;
            }

            articulosCL cl = new articulosCL();
            cl.intArticulosID = intArticulosID;
            result = cl.articulosEliminar();


            if (result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid(iActivo);
            }
            else
            {
                MessageBox.Show(result);
            }


        }//Eliminar()
        #endregion

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
            if (intArticulosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }
            else
            {
                Editar();

                try
                {
                    string path = ConfigurationManager.AppSettings["imagenesdearticulos"].ToString();
                    //string filename = path + txtImagen.Text;
                    //if (File.Exists(filename))
                    //{

                    //    navigationFrame.SelectedPageIndex = 2;
                    //    pictureEditArticulo.Image = Image.FromFile(filename);
                    //}
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intArticulosID = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ArticulosID"));
            strimagen = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Imagen"));
        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intArticulosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Desea eliminar la línea " + intArticulosID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Newsa.net", "Cargando información...");
            navigationFrame.SelectedPageIndex = 0;
            guardaLayout();
            ribbonPageGroupAcciones.Visible = true;
            ribbonPageGroup2.Visible = false;
            boolEditando = false;
            LimpiaCajas();
            LlenarGrid(iActivo);
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void guardaLayout()
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridArticulos" +
                "";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            guardaLayout();
            this.Close();
        }

        private void bbiVista_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.ShowRibbonPrintPreview();
        }

        private void bbiImagen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


        }

        private void cboTA_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //proceso de DE para obtener valor del combo
                DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;

                DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

                if(row != null )
                {
                    Object value = row["Clave"];
                    intTAID = Convert.ToInt32(value);
                }

            }

            catch (Exception ex)

            {

                //MessageBox.Show("cboTA: " + ex.Message);

            }
        }

        private void btnActCosto_Click(object sender, EventArgs e)
        {
            ActualizaCosto();
        }
        private void ActualizaCosto()
        {
            try
            {
                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtCosto.Text))
                {
                    MessageBox.Show("Teclee el costo");
                    return;
                }


                articulosCL cl = new articulosCL();
                cl.intArticulosID = intArticulosID;
                cl.fFechaalta = Convert.ToDateTime(dateEditFechaCosto.Text);
                cl.strMonedasID = cboMonedaCosto.EditValue.ToString();
                cl.dcosto = Convert.ToDecimal(txtCosto.Text);

                if (!clg.esNumerico(txtParidad.Text))
                {
                    if (cl.strMonedasID == "MXN")
                    {
                        txtParidad.Text = "1";
                    }
                    else
                    {
                        MessageBox.Show("Teclee la paridad");
                        return;
                    }
                }

                cl.dtipodecambio = Convert.ToDecimal(txtParidad.Text);
                cl.strObs = txtObsCosto.Text;

                string result = cl.ActualizaCostos();

                MessageBox.Show(result);

                if (result == "OK")
                {
                    gridControlCostos.DataSource = cl.ArticulosCostosGrid();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {

        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //if (e.Page.Name == "xtraTabPage4")
            //    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //else
            //{
            //    try
            //    {
            //        bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //    }
            //    catch (Exception)
            //    {

            //    }


            //}

        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bbiDesactivos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Cargando artículos");
            bbiDesactivos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiActivos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LlenarGrid(0);
            iActivo = 0;
            gridView1.ActiveFilter.Clear();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiActivos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Cargando artículos");
            bbiDesactivos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiActivos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            LlenarGrid(1);
            iActivo = 1;
            gridView1.ActiveFilter.Clear();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            blnSinBaseParacosteo = true;
        }

        private void lblArtBaseParaCosteo_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(strArticuloBase);
        }

        private void CargarTxtArticulo()
        {
            utilCL utilCL = new utilCL();
            string subFam = strSubFamiliasID != "" ? strSubFamiliasID : "0";
            string linea = utilCL.fillNumberString(strLineasID, 2);
            string fam = utilCL.fillNumberString(strFamiliasID, 2);
            string famSub = utilCL.fillNumberString(subFam, 2);
            string codArticulo = linea + fam + famSub;

            
            if (linea != "00" && fam != "00" && famSub != "00")
            { 
                articulosCL articulosCL = new articulosCL();
                articulosCL.intArticulosID = intArticulosID;
                articulosCL.intFamiliasID = Convert.ToInt32(strFamiliasID);
                articulosCL.intSubFamiliaID = Convert.ToInt32(subFam);
                articulosCL.strArticuloDB = strArticuloDB;
                articulosCL.strConsecutivo = codArticulo;
                string consecutivo = utilCL.fillNumberString(articulosCL.GetConsecutivo(), 2);
                codArticulo += consecutivo;
                txtArticulo.Text = codArticulo;
            }
            else
                txtArticulo.Text = "";
        }
        private void cboFamilias_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;
            DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

            if (row != null)
            {
                // CARGAMOS COMBO DE SUBFAMILIAS 
                intFamiliaID = Convert.ToInt32(row["Clave"]);
                cboSubFamiliasID.EditValue = null;
                cboSubFamiliasIDClasificaciones.EditValue = null;
                CargaSubFamilias();

                // CARGAMOS TEXTO ARTICULO
                blCodigoArticulo = Convert.ToBoolean(row["CodigoArticulo"]);
                if (blCodigoArticulo)
                {
                    txtArticulo.Enabled = false;
                    strFamiliasID = row["Clave"].ToString();
                    strLineasID = row["lineasID"].ToString();
                    strSubFamiliasID = "";
                    CargarTxtArticulo();
                }
                else
                {
                    txtArticulo.Enabled = true;
                }

            }
            cboFamilias.EditValue = editor.EditValue;
            cboFamiliasClasificaciones.EditValue = editor.EditValue;
        }

        private void  cboSubFamiliasID_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;
            DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

            if(row != null)
            {
                strSubFamiliasID = row["Clave"].ToString();
                if (blCodigoArticulo) CargarTxtArticulo();
            }
            cboSubFamiliasID.EditValue = editor.EditValue;
            cboSubFamiliasIDClasificaciones.EditValue = editor.EditValue;
        }

        private void cboUnidad_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //proceso de DE para obtener valor del combo
                DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;

                DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

                Object value = row["Clave"];

                intUnidadID = Convert.ToInt32(value);

            }

            catch (Exception ex)

            {

                //MessageBox.Show("cboUnidadMed: " + ex.Message);

            }
        }

    }
} 