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

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class Fletes1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public BindingList<detalleCL> detalle;
        string strSerie;
        int intFolio;
        string strTo;
        DateTime dFecha;
        int intUsuarioID = globalCL.gv_UsuarioID, intStatus;
        Boolean blNuevo;
        private object popUpCancelar;

        public Fletes1()
        {
            InitializeComponent();
            txtFecha.Text = DateTime.Now.ToShortDateString();
            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

     

            CargaCombos();
            BuscarSerie();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Fletes";

            LlenarGrid();
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

        private void LlenarGrid()
        {
            FletesCL cl = new FletesCL();
            cl.strSerie = cboSerie.EditValue.ToString();
            gridControlPrincipal.DataSource = cl.FletesGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridgenerarFletes";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        public class detalleCL
        {
            public string Tipodedocumento { get; set; }
            public string Serie { get; set; }
            public int Folio { get; set; }
            public string Notas { get; set; }
        }

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
        }

        private void CargaCombos()
        {
            BindingSource src = new BindingSource();
            globalCL clg = new globalCL();
            combosCL cl = new combosCL();
            cl.strTabla = "Serie";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Clave";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Des"].Visible = false;
            cl.strTabla = "Transportes";
            cboTransportesID.Properties.ValueMember = "Clave";
            cboTransportesID.Properties.DisplayMember = "Des";
            cboTransportesID.Properties.DataSource = cl.CargaCombos();
            cboTransportesID.Properties.ForceInitialize();
            cboTransportesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTransportesID.Properties.PopulateColumns();
            cboTransportesID.Properties.Columns["Clave"].Visible = false;
            cboTransportesID.ItemIndex = 0;

            cl.strTabla = "Ciudades";
            cboOrigen.Properties.ValueMember = "Clave";
            cboOrigen.Properties.DisplayMember = "Des";
            cboOrigen.Properties.DataSource = cl.CargaCombos();
            cboOrigen.Properties.ForceInitialize();
            cboOrigen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboOrigen.Properties.PopulateColumns();
            cboOrigen.Properties.Columns["Clave"].Visible = false;
            cboOrigen.ItemIndex = 0;

            cl.strTabla = "Ciudades";
            cboDestino.Properties.ValueMember = "Clave";
            cboDestino.Properties.DisplayMember = "Des";
            cboDestino.Properties.DataSource = cl.CargaCombos();
            cboDestino.Properties.ForceInitialize();
            cboDestino.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboDestino.Properties.PopulateColumns();
            cboDestino.Properties.Columns["Clave"].Visible = false;
            cboDestino.Properties.NullText = "Seleccione un destino";// con esta liena podemos poner una desceripcion al cbo 

            List<tipoCL> tipoLL = new List<tipoCL>();
            tipoLL.Add(new tipoCL() { Clave = "D", Des = "A domicilio" });
            tipoLL.Add(new tipoCL() { Clave = "O", Des = "Ocurre" });
            cboTipodeentrega.Properties.ValueMember = "Clave";
            cboTipodeentrega.Properties.DisplayMember = "Des";
            cboTipodeentrega.Properties.DataSource = tipoLL;
            cboTipodeentrega.Properties.ForceInitialize();
            cboTipodeentrega.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTipodeentrega.Properties.PopulateColumns();
            cboTipodeentrega.Properties.Columns["Clave"].Visible = false;
            cboTipodeentrega.ItemIndex = 0;

            List<tipoCL> tipoL = new List<tipoCL>();
            tipoL.Add(new tipoCL() { Clave = "F", Des = "Factura" });
            tipoL.Add(new tipoCL() { Clave = "P", Des = "Pedido" });
            tipoL.Add(new tipoCL() { Clave = "R", Des = "Remision" });
            tipoL.Add(new tipoCL() { Clave = "S", Des = "Salida" });
            repositoryItemGridLookUpEditTipodedoc.ValueMember = "Clave";
            repositoryItemGridLookUpEditTipodedoc.DisplayMember = "Des";
            repositoryItemGridLookUpEditTipodedoc.DataSource = tipoL;
            //repositoryItemGridLookUpEditTipodedoc.ForceInitialize();
            repositoryItemGridLookUpEditTipodedoc.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemGridLookUpEditTipodedoc.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemGridLookUpEditTipodedoc.NullText = "Seleccione un tipo de documento";

        }

        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        public class tipoCL
        {
            public string Clave { get; set; }
            public string Des { get; set; }
        }

        private void Nuevo()
        {
            Inicialisalista();
            BotonesEdicion();
            strSerie = string.Empty;
            intFolio = 0;
            blNuevo = true;
            SiguienteID();
        }

        private void BotonesEdicion()
        {
            ribbonPageGroupHome.Visible = false;
            ribbonPageGroup1.Visible = true;
            navBarControl1.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void LimpiaCajas()
        {
            cboTransportesID.EditValue = null;
            txtNumerodeguia.Text = "";
            txtNumerodecontrol.Text = "";
            cboOrigen.EditValue = "";
            cboDestino.EditValue = "";
            cboTipodeentrega.EditValue = "";
            swPorCobrar.IsOn = false;
            cboSerie.EditValue = "";
            txtFolio.Text = "";

            txtSubtotal.Text = "";
            txtManeaje.Text = "";
            txtSeguro.Text = "";
            txtIva.Text = "";
            txtRetIva.Text = "";

            txtNotasGenerales.Text = "";
        }

        private void SiguienteID()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            FacturasCL cl = new FacturasCL();
            cl.strSerie = serie;
            cl.strDoc = "Fletes";

            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {
                txtFolio.Text = cl.intID.ToString();
            }
            else
            {
                MessageBox.Show("SiguienteID :" + result);
            }

        }//SguienteID

        private string Valida()
        {
            if (cboTransportesID.EditValue == null)
            {
                return "El campo Transportes no puede ir vacio";
            }
            if (txtFecha.Text.Length == 0)
            {
                return "El campo Fecha no puede ir vacio";
            }
            if (txtNumerodeguia.Text.Length == 0)
            {
                return "El campo numero de guia no puede ir vacio";
            }
            if (txtNumerodecontrol.Text.Length == 0)
            {
                return "El campo numero de control no puede ir vacio";
            }
            if (cboOrigen.EditValue == null)
            {
                return "El campo Origen no puede ir vacio";
            }
            if (cboDestino.EditValue == null)
            {
                return "El campo Destino no puede ir vacio";
            }
            if (cboTipodeentrega.EditValue == null)
            {
                return "El campo Tipode entrega no puede ir vacio";
            }
            if (txtSubtotal.Text.Length == 0)
            {
                return "El campo subtotal no puede ir vacio";
            }
            if (txtNeto.Text.Length == 0)
            {
                return "El campo neto no puede ir vacio";
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

                string sCondicion = String.Empty;
                System.Data.DataTable dtFletes = new System.Data.DataTable("Fletes");
                dtFletes.Columns.Add("Serie", Type.GetType("System.String"));
                dtFletes.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtFletes.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtFletes.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dtFletes.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtFletes.Columns.Add("Retiva", Type.GetType("System.Decimal"));
                dtFletes.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dtFletes.Columns.Add("Guia", Type.GetType("System.String"));
                dtFletes.Columns.Add("Numerodecontrol", Type.GetType("System.String"));
                dtFletes.Columns.Add("Seguro", Type.GetType("System.Decimal"));
                dtFletes.Columns.Add("Meneaje", Type.GetType("System.Decimal"));
                dtFletes.Columns.Add("Porcobrar", Type.GetType("System.Int32"));
                dtFletes.Columns.Add("OcurreoAdomicilio", Type.GetType("System.String"));
                dtFletes.Columns.Add("TransportesID", Type.GetType("System.Int32"));
                dtFletes.Columns.Add("CiudadorigenID", Type.GetType("System.Int32"));
                dtFletes.Columns.Add("CiudaddestinoID", Type.GetType("System.Int32"));
                dtFletes.Columns.Add("Notas", Type.GetType("System.String"));
                dtFletes.Columns.Add("Status", Type.GetType("System.int32"));



                System.Data.DataTable dtFletesdetalle = new System.Data.DataTable("Fletesdetalle");
                dtFletesdetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtFletesdetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtFletesdetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtFletesdetalle.Columns.Add("TipoDoc", Type.GetType("System.String"));
                dtFletesdetalle.Columns.Add("SerieDoc", Type.GetType("System.String"));
                dtFletesdetalle.Columns.Add("FolioDoc", Type.GetType("System.Int32"));
                dtFletesdetalle.Columns.Add("Notas", Type.GetType("System.String"));


                if (blNuevo)
                {
                    SiguienteID();
                }


                string dato = String.Empty;
                string strSerie = cboSerie.EditValue.ToString();
                int intFolio = Convert.ToInt32(txtFolio.Text);
                int intSeq = 0;
                string strTipoDoc = String.Empty; string strDoc = String.Empty;
                string strSerieDoc = String.Empty;
                int intFolioDoc = 0;
                string strNotas = String.Empty;
                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    strDoc = "";
                    dato = gridViewDetalle.GetRowCellValue(i, "Tipodedocumento").ToString();
                    if (dato.Length > 0)
                    {
                        intSeq = i;
                        strTipoDoc = gridViewDetalle.GetRowCellValue(i, "Tipodedocumento").ToString();
                        strSerieDoc = gridViewDetalle.GetRowCellValue(i, "Serie").ToString();
                        intFolioDoc = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Folio"));
                        strNotas = gridViewDetalle.GetRowCellValue(i, "Notas").ToString();

                        FletesCL clf = new FletesCL();
                        clf.strSerie = strSerieDoc;
                        clf.intFolio = intFolioDoc;
                        switch (strTipoDoc)
                        {
                            case "R":
                                strDoc = "Remisiones";
                                break;
                            case "F":
                                strDoc = "Facturas";
                                break;
                            case "P":
                                strDoc = "Pedidos";
                                break;
                            case "S":
                                strDoc = "Salida";
                                break;
                        }
                        clf.strDoc = strDoc;
                        String Result1 = clf.FletesValidarDoc();
                        if (Result1 == "OK")
                        {
                            if (clf.strExiste == "")
                            {
                                MessageBox.Show("La serie y folio del renglón: " + intSeq + " no existen en la tabla: " + strDoc);
                                return;
                            }

                        }

                        dtFletesdetalle.Rows.Add(strSerie, intFolio, intSeq, strTipoDoc, strSerieDoc, intFolioDoc, strNotas);
                    }
                    else
                    {
                        MessageBox.Show("la tabla de detalle no puede ir vacio");
                    }
                }

                DateTime strFecha = Convert.ToDateTime(txtFecha.Text);

                decimal dSeguro = 0;
                decimal dMeneaje = 0, dIva = 0, dRetiva = 0;
                decimal dSubtotal = Convert.ToDecimal(txtSubtotal.Text);
                if (txtManeaje.Text == string.Empty) { }
                else
                    dMeneaje = Convert.ToDecimal(txtManeaje.Text);
                if (txtSeguro.Text == string.Empty) { }
                else
                    dSeguro = Convert.ToDecimal(txtSeguro.Text);
                if (txtIva.Text == string.Empty) { }
                else
                    dIva = Convert.ToDecimal(txtIva.Text);
                if (txtRetIva.Text == string.Empty) { }
                else
                    dRetiva = Convert.ToDecimal(txtRetIva.Text);
                decimal dNeto = Convert.ToDecimal(txtNeto.Text);
                string strGuia = txtNumerodeguia.Text;
                string strNumerodecontrol = txtNumerodecontrol.Text;
                int intPorcobrar = swPorCobrar.IsOn ? 1 : 0; ;
                string strOcurreoAdomicilio = cboTipodeentrega.EditValue.ToString();
                int intTransportesID = Convert.ToInt32(cboTransportesID.EditValue);
                int intCiudadorigenID = Convert.ToInt32(cboOrigen.EditValue);
                int intCiudaddestinoID = Convert.ToInt32(cboDestino.EditValue);
                string strNotasGenerales = txtNotasGenerales.Text;
                intStatus = 1;
                dato = String.Empty;

                dtFletes.Rows.Add(strSerie, intFolio, strFecha.ToShortDateString(), dSubtotal, dIva, dRetiva, dNeto, strGuia, strNumerodecontrol,
              dSeguro, dMeneaje, intPorcobrar, strOcurreoAdomicilio, intTransportesID, intCiudaddestinoID, intCiudaddestinoID, strNotasGenerales, intStatus);

                FletesCL cl = new FletesCL();
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.dtm = dtFletes;
                cl.dtd = dtFletesdetalle;
                cl.intUsuarioID = intUsuarioID;
                cl.strMaquina = Environment.MachineName;
                Result = cl.FletesCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    intStatus = 0;
                    EnviaCorreo();

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

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();
            LlenarGridDetalle();


        }

        private void llenaCajas()
        {
            FletesCL cl = new FletesCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            String Result = cl.FletesLlenaCajas();
            if (Result == "OK")
            {
                txtFecha.Text = cl.strFecha.ToShortDateString();
                cboTransportesID.EditValue = cl.intTransportesID;
                txtNumerodeguia.Text = cl.strGuia;
                txtNumerodecontrol.Text = cl.strNumerodecontrol;
                cboOrigen.EditValue = cl.intCiudadorigenID;
                cboDestino.EditValue = cl.intCiudaddestinoID;
                cboTipodeentrega.EditValue = cl.strOcurreoAdomicilio;
                swPorCobrar.IsOn = cl.intPorcobrar == 1 ? true : false;
                cboSerie.EditValue = strSerie;
                txtFolio.Text = intFolio.ToString();

                txtSubtotal.Text = cl.dSubtotal.ToString();
                txtManeaje.Text = cl.dManeaje.ToString();
                txtSeguro.Text = cl.dManeaje.ToString();
                txtIva.Text = cl.dManeaje.ToString();
                txtRetIva.Text = cl.dRetiva.ToString();

                txtNotasGenerales.Text = cl.strNotas;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void LlenarGridDetalle()
        {
            FletesCL cl = new FletesCL();
            cl.intFolio = intFolio;
            cl.strSerie = cboSerie.EditValue.ToString();
            gridControlDetalle.DataSource = cl.FletesDetalleGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridgenerarFletesDetalle";
            clg.restoreLayout(gridViewDetalle);
        } //LlenarGrid()

        private void CierraPopUp()
        {
            groupControlCancelar.Visible = false;
            txtLogin.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtRazondecancelacion.Text = string.Empty;
        }

        private void Cancelarenlabasededatos()
        {
            //aqui cancelar
            try
            {
                FletesCL cl = new FletesCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.intUsuarioID = intUsuarioID;  // ------------------------- No dejarlo fijo -------------------
                cl.strMaquina = Environment.MachineName;
                cl.strRazon = txtRazondecancelacion.Text;
                string result = cl.FletesCancelar();
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

        private void EnviaCorreo()
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                dFecha = DateTime.Now;
                string strEmailTo = "";
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Correos", "Enviando...");
                globalCL cl = new globalCL();
                //cl.AbreOutlook(strSerie, intFolio, dFecha, strTo);

                string result = cl.EnviaCorreo(strEmailTo, strSerie, intFolio, dFecha, "P");

                if (!blNuevo)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                }

                if (cl.iAbrirOutlook == 0)
                {
                    //MessageBox.Show(result);
                }
            }
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void txtSubtotal_EditValueChanged(object sender, EventArgs e)
        {
            decimal dNeto = 0, dMan = 0, dSeg = 0, dIva = 0, dRetiva = 0, dsubtotal = 0;

            if (txtSubtotal.Text == string.Empty) { }
            else
                dsubtotal = Convert.ToDecimal(txtSubtotal.Text);
            if (txtManeaje.Text == string.Empty) { }
            else
                dMan = Convert.ToDecimal(txtManeaje.Text);
            if (txtSeguro.Text == string.Empty) { }
            else
                 dSeg = Convert.ToDecimal(txtSeguro.Text);
            if (txtIva.Text == string.Empty) { }
            else
                dIva = Convert.ToDecimal(txtIva.Text);
            if (txtRetIva.Text == string.Empty) { }
            else
                dRetiva = Convert.ToDecimal(txtRetIva.Text);

            dNeto = dsubtotal + dMan + dSeg + (dIva - dRetiva);
            txtNeto.Text = dNeto.ToString();
        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            intStatus = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Status"));
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridgenerarFletes";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageGroupHome.Visible = true;
            ribbonPageGroup1.Visible = false;
            navBarControl1.Visible = true;
            navigationFrame.SelectedPageIndex = 0;
            intFolio = 0;
            strSerie = "";
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
                EnviaCorreo();
            }
        }

        private void txtManeaje_EditValueChanged(object sender, EventArgs e)
        {
            decimal dNeto = 0, dMan = 0, dSeg = 0, dIva = 0, dRetiva = 0, dsubtotal = 0;

            if (txtSubtotal.Text == string.Empty) { }
            else
                dsubtotal = Convert.ToDecimal(txtSubtotal.Text);
            if (txtManeaje.Text == string.Empty) { }
            else
                dMan = Convert.ToDecimal(txtManeaje.Text);
            if (txtSeguro.Text == string.Empty) { }
            else
                dSeg = Convert.ToDecimal(txtSeguro.Text);
            if (txtIva.Text == string.Empty) { }
            else
                dIva = Convert.ToDecimal(txtIva.Text);
            if (txtRetIva.Text == string.Empty) { }
            else
                dRetiva = Convert.ToDecimal(txtRetIva.Text);

            dNeto = dsubtotal + dMan + dSeg + (dIva - dRetiva);
            txtNeto.Text = dNeto.ToString();
        }

        private void txtSeguro_EditValueChanged(object sender, EventArgs e)
        {
            decimal dNeto = 0, dMan = 0, dSeg = 0, dIva = 0, dRetiva = 0, dsubtotal = 0;

            if (txtSubtotal.Text == string.Empty) { }
            else
                dsubtotal = Convert.ToDecimal(txtSubtotal.Text);
            if (txtManeaje.Text == string.Empty) { }
            else
                dMan = Convert.ToDecimal(txtManeaje.Text);
            if (txtSeguro.Text == string.Empty) { }
            else
                dSeg = Convert.ToDecimal(txtSeguro.Text);
            if (txtIva.Text == string.Empty) { }
            else
                dIva = Convert.ToDecimal(txtIva.Text);
            if (txtRetIva.Text == string.Empty) { }
            else
                dRetiva = Convert.ToDecimal(txtRetIva.Text);

            dNeto = dsubtotal + dMan + dSeg + (dIva - dRetiva);
            txtNeto.Text = dNeto.ToString();
        }

        private void txtIva_EditValueChanged(object sender, EventArgs e)
        {
            decimal dNeto = 0, dMan = 0, dSeg = 0, dIva = 0, dRetiva = 0, dsubtotal = 0;

            if (txtSubtotal.Text == string.Empty) { }
            else
                dsubtotal = Convert.ToDecimal(txtSubtotal.Text);
            if (txtManeaje.Text == string.Empty) { }
            else
                dMan = Convert.ToDecimal(txtManeaje.Text);
            if (txtSeguro.Text == string.Empty) { }
            else
                dSeg = Convert.ToDecimal(txtSeguro.Text);
            if (txtIva.Text == string.Empty) { }
            else
                dIva = Convert.ToDecimal(txtIva.Text);
            if (txtRetIva.Text == string.Empty) { }
            else
                dRetiva = Convert.ToDecimal(txtRetIva.Text);

            dNeto = dsubtotal + dMan + dSeg + (dIva - dRetiva);
            txtNeto.Text = dNeto.ToString();
        }

        private void txtRetIva_EditValueChanged(object sender, EventArgs e)
        {
            decimal dNeto = 0, dMan = 0, dSeg = 0, dIva = 0, dRetiva = 0, dsubtotal = 0;

            if (txtSubtotal.Text == string.Empty) { }
            else
                dsubtotal = Convert.ToDecimal(txtSubtotal.Text);
            if (txtManeaje.Text == string.Empty) { }
            else
                dMan = Convert.ToDecimal(txtManeaje.Text);
            if (txtSeguro.Text == string.Empty) { }
            else
                dSeg = Convert.ToDecimal(txtSeguro.Text);
            if (txtIva.Text == string.Empty) { }
            else
                dIva = Convert.ToDecimal(txtIva.Text);
            if (txtRetIva.Text == string.Empty) { }
            else
                dRetiva = Convert.ToDecimal(txtRetIva.Text);

            dNeto = dsubtotal + dMan + dSeg + (dIva - dRetiva);
            txtNeto.Text = dNeto.ToString();
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                groupControlCancelar.Visible = true;
                groupControlCancelar.Text = "Cancelar el folio:" + strSerie + intFolio.ToString();
                txtLogin.Focus();
            }
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
            //}
        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                string status = view.GetRowCellValue(e.RowHandle, "Estado").ToString();

                if (status == "Cancelada")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        #region Filtros de bbi Meses
        private void navBarItemEne_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 1";
        }

        private void navBarItemFeb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 2";
        }

        private void navBarItemMar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 3";
        }

        private void navBarItemAbr_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 4";
        }

        private void navBarItemMay_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 5";
        }

        private void navBarItemJun_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 6";
        }

        private void navBarItemJul_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 7";
        }

        private void navBarItemAgo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 8";
        }

        private void navBarItemsep_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 9";
        }

        private void navBarItemOct_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 10";
        }

        private void navBarItemNov_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 11";
        }

        private void navBarItemDic_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 12";
        }

        private void navBarItemTodos_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilter.Clear();
        }



        #endregion

        private void btnCerrarPopup_Click(object sender, EventArgs e)
        {
            CierraPopUp();
        }
    }
}