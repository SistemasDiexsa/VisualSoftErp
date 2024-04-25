using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VisualSoftErp.Clases;
using System.IO;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using VisualSoftErp.Herramientas.Formas;
using DevExpress.Utils.UI.Localization;
using DevExpress.Mvvm;
using DevExpress.XtraGrid.Views.Grid;
using VisualSoftErp.Catalogos;

namespace VisualSoftErp.Operacion.Compras.Formas
{
    public partial class Validarcompras : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        decimal pIvaProv;
        string serieRM;
        int folioRM;
        string RFCE = string.Empty;
        string strRFCemisor = string.Empty;

        public Validarcompras()
        {
            InitializeComponent();
            
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridView1.OptionsSelection.MultiSelect = false;
            gridView1.OptionsBehavior.Editable = false;

            cargaCombos();
            cboProveedores.ItemIndex = 0;
            //cboMoneda.ItemIndex = 0;            
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void cargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Proveedores";
            cboProveedores.Properties.ValueMember = "Clave";
            cboProveedores.Properties.DisplayMember = "Des";
            cboProveedores.Properties.DataSource = cl.CargaCombos();
            cboProveedores.Properties.ForceInitialize();
            cboProveedores.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedores.Properties.PopulateColumns();
            cboProveedores.Properties.Columns["Clave"].Visible = false;
            cboProveedores.Properties.Columns["Piva"].Visible = false;
            cboProveedores.Properties.Columns["Plazo"].Visible = false;
            cboProveedores.Properties.Columns["Tiempodeentrega"].Visible = false;
            cboProveedores.Properties.Columns["Diastraslado"].Visible = false;
            cboProveedores.Properties.Columns["Lab"].Visible = false;
            cboProveedores.Properties.Columns["Via"].Visible = false;
            cboProveedores.Properties.Columns["BancosID"].Visible = false;
            cboProveedores.Properties.Columns["Cuentabancaria"].Visible = false;
            cboProveedores.Properties.Columns["C_Formapago"].Visible = false;
            cboProveedores.Properties.Columns["Retisr"].Visible = false;
            cboProveedores.Properties.Columns["Retiva"].Visible = false;
            cboProveedores.Properties.Columns["MonedasID"].Visible = false;
            cboProveedores.Properties.Columns["Contacto"].Visible = false;
            cboProveedores.Properties.Columns["Diasdesurtido"].Visible = false;
            cboProveedores.Properties.Columns["Diasdetraslado"].Visible = false;
            cboProveedores.Properties.Columns["Rfc"].Visible = false;
           
           


            cl.strTabla = "Monedas";
            cboMoneda.Properties.ValueMember = "Clave";
            cboMoneda.Properties.DisplayMember = "Des";
            cboMoneda.Properties.DataSource = cl.CargaCombos();
            cboMoneda.Properties.ForceInitialize();
            cboMoneda.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMoneda.Properties.PopulateColumns();
            cboMoneda.Properties.Columns["Clave"].Visible = false;
        }
       
        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void cargaRM()
        {
            try
            {
                ComprasCL cl = new ComprasCL();
                cl.intProveedoresID = Convert.ToInt32(cboProveedores.EditValue);
                gridControl1.DataSource = cl.CargaRM();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiCargarRM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
            cargaRM();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string result = Validar();

            if (result == "OK")
                Guardar();
            else 
                MessageBox.Show(result);  
              
        }

        private void Guardar()
        {
            try
            {
                System.Data.DataTable dtRM = new System.Data.DataTable("RM");
                dtRM.Columns.Add("Serie", Type.GetType("System.String"));
                dtRM.Columns.Add("Folio", Type.GetType("System.Int32"));

                string serie = string.Empty;
                int folio = 0;

                foreach (int i in gridView1.GetSelectedRows())
                {
                    serie = gridView1.GetRowCellValue(i, "Folio").ToString();
                    if (serie.Length > 0)
                    {
                        serie = gridView1.GetRowCellValue(i, "Serie").ToString();
                        folio = Convert.ToInt32(gridView1.GetRowCellValue(i, "Folio"));

                        dtRM.Rows.Add(serie, folio);
                    }
                }

                ComprasCL cl = new ComprasCL();
                cl.dtm = dtRM;
                cl.intProveedoresID = Convert.ToInt32(cboProveedores.EditValue);
                cl.strFactura = txtFac.Text;
                cl.fFecha = Convert.ToDateTime(dFechaFac.Text);
                cl.strMonedasID = cboMoneda.EditValue.ToString();
                cl.dTC = Convert.ToDecimal(txtTC.Text);
                cl.intPlazo = Convert.ToInt32(txtPlazo.Text);
                cl.decNeto = Convert.ToDecimal(txtNeto.Text);
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                cl.strUUID = txtUUID.Text;
                cl.decCargoVario = Convert.ToDecimal(TxtCargoVario.Text);

                string result = cl.ComprasValidar();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente!");
                    limpiacajas();
                }
                else
                    MessageBox.Show(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void limpiacajas()
        {
            gridControl1.DataSource = null;
            txtFac.Text = string.Empty;
            txtTC.Text = string.Empty;
            dFechaFac.Text = string.Empty;  
            txtNeto.Text = string.Empty;           
            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            txtUUID.Text = string.Empty;    
        }

        private string Validar()
        {
            globalCL clg = new globalCL();
            if (!clg.esNumerico(txtNeto.Text))
            {
                return "Falta capturar el neto";
            }

            if (!clg.esNumerico(TxtCargoVario.Text))
            {
                TxtCargoVario.Text = "0";
            }

            if (cboProveedores.ItemIndex == -1)
            {
                return "Seleccione un proveedor";
            }
            if (txtFac.Text.Length == 0)
            {
                return "Teclee la factura";
            }
            if (!clg.esFecha(dFechaFac.Text))
            {
                return "Teclee la fecha de la factura";
            }
            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(dFechaFac.Text).Year, Convert.ToDateTime(dFechaFac.Text).Month, "COM");
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";
            }

            if (cboMoneda.ItemIndex == -1)
            {
                return "Seleccione una moneda";
            }

            if (cboMoneda.EditValue.ToString() == "MXN")
                txtTC.Text = "1";
            else
            {
                if (!clg.esNumerico(txtTC.Text))
                {
                    return "Teclee el tipo de cambio";
                }
            }

            if (!clg.esNumerico(txtPlazo.Text))
            {
                return "Teclee el plazo";
            }

            if (!clg.esNumerico(txtNeto.Text))
            {
                return "Teclee el neto de la factura";
            }

            if (strRFCemisor != RFCE )
            {
                if (RFCE.Length > 0)
                {
                    return "El proveedor seleccionado es diferente al del XML";
                }
              
            }

            decimal dNeto = 0;
            decimal tNeto = 0;
            string dato = string.Empty;

            foreach (int i in gridView1.GetSelectedRows())
            {
                dato = gridView1.GetRowCellValue(i, "Folio").ToString();
                if (dato.Length > 0)
                {
                    dNeto = Convert.ToDecimal(gridView1.GetRowCellValue(i, "Neto"));
                    tNeto += dNeto;
                }
            }
            if (tNeto == 0)
            {
                return "Seleccione una recepción";
            }
            if (tNeto != Convert.ToDecimal(txtNeto.Text))
            {
                Decimal totalFactura = Convert.ToDecimal(txtNeto.Text);
                Decimal limSup = totalFactura + 0.10M;
                Decimal limInf = totalFactura - 0.10M;
                Decimal cargoV = Convert.ToDecimal(TxtCargoVario.Text) * (1 + (pIvaProv/100));
                Decimal totalRM = tNeto + cargoV;

                if (!(totalRM >= limInf && totalRM <= limSup)) 
                {
                    return "No concuerda el neto de la factura: " + txtNeto.Text + " Contra el de la recepción de mercancía:" + tNeto.ToString() + " mas el cargo vario: " + TxtCargoVario.Text;
                }
            }
            return "OK";
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            limpiacajas();
        }

        private void cboProveedores_EditValueChanged(object sender, EventArgs e)
        {
           // limpiacajas();
            object orow = cboProveedores.Properties.GetDataSourceRowByKeyValue(cboProveedores.EditValue);
            if (orow != null)
            {
                txtPlazo.Text = ((DataRowView)orow)["Plazo"].ToString();
                pIvaProv = Convert.ToDecimal(((DataRowView)orow)["Piva"]);
                cboMoneda.EditValue = ((DataRowView)orow)["MonedasID"].ToString();
                strRFCemisor = ((DataRowView)orow)["RFC"].ToString();
                gridControl1.DataSource = null;
                gridControl2.DataSource = null;
                cargaRM();
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            serieRM = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Serie").ToString();
            folioRM = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Folio"));

            LlenaOC();
        }

        private void LlenaOC()
        {            
            try
            {
                RecepciondemercanciaCL cl = new RecepciondemercanciaCL();
                cl.strSerie = serieRM;
                cl.intFolio = folioRM;
                gridControl2.DataSource = cl.RecepciondemercanciaOCGrid();
                gridView2.Columns["Serie"].OptionsColumn.ReadOnly = true;
                gridView2.Columns["Folio"].OptionsColumn.ReadOnly = true;
                gridView2.Columns["Seq"].OptionsColumn.ReadOnly = true;
                gridView2.Columns["Articulos"].OptionsColumn.ReadOnly = true;
                gridView2.Columns["UM"].OptionsColumn.ReadOnly = true;
                gridView2.Columns["Nombre"].OptionsColumn.ReadOnly = true;
                gridView2.Columns["Cantidad"].OptionsColumn.ReadOnly = true;
                gridView2.Columns["Importe"].OptionsColumn.ReadOnly = true;
                gridView2.Columns["Iva"].OptionsColumn.ReadOnly = true;
                gridView2.Columns["Neto"].OptionsColumn.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiCambiarFecha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (folioRM == 0)
                {
                    MessageBox.Show("Seleccione una recepción de mercancía");
                    return;
                }

                DialogResult Result = MessageBox.Show("Desea modificar la fecha a la RM: " + serieRM + folioRM.ToString(), "Modificar", MessageBoxButtons.YesNo);
                if (Result.ToString() != "Yes")
                {
                    return;
                }

                RecepciondemercanciaCL cl = new RecepciondemercanciaCL();
                cl.strSerie = serieRM;
                cl.intFolio = folioRM;
                cl.fFecha = Convert.ToDateTime(dFechaFac.Text);
                string result = cl.RecepciondemercanciasCambiaFecha();
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiCargaXML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            vsFK.vsFinkok vs = new vsFK.vsFinkok();
            String dato = string.Empty;
            string sfile = xtraOpenFileDialog1.FileName;
            string sDir = Path.GetDirectoryName(sfile);

            string strLayoutPath = System.Configuration.ConfigurationManager.AppSettings["gridlayout"].ToString() + "xmlcompras.txt";
            string data = string.Empty;
            if (File.Exists(strLayoutPath))
            {
                data = System.IO.File.ReadAllText(strLayoutPath);
                data = data.Substring(0, data.Length - 2) + "\\";
            }

            if (data.Length == 0)
                xtraOpenFileDialog1.InitialDirectory = "C:\\";
            else
                xtraOpenFileDialog1.InitialDirectory = data;

            xtraOpenFileDialog1.ShowDragDropConfirmation = true;
            xtraOpenFileDialog1.AutoUpdateFilterDescription = false;
            xtraOpenFileDialog1.Filter = "XML files (*.xml)|*.xml";
            xtraOpenFileDialog1.Multiselect = false;

            DialogResult dr = xtraOpenFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {

                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Newsa.net", "Analizando xml");


                sfile = xtraOpenFileDialog1.FileName;

                string TipoDeComprobante = string.Empty;
                dato = vs.ExtraeValor(sfile, "cfdi:Comprobante", "TipoDeComprobante");
                if (dato != null)
                {
                    TipoDeComprobante = dato;
                }

                if (TipoDeComprobante != "I")
                {
                    MessageBox.Show("Comprobante no es de ingreso");
                    return;
                }


                dato = vs.ExtraeValor(sfile, "cfdi:Receptor", "Rfc");
                if (dato != "DEX9201028BA")
                {
                    MessageBox.Show("XML no es para DIEXSA ");
                    return;
                }

                string Moneda = string.Empty;
                dato = vs.ExtraeValor(sfile, "cfdi:Comprobante", "Moneda");
                if (dato != null)
                {
                    Moneda = dato;
                }

                
                dato = vs.ExtraeValor(sfile, "cfdi:Emisor", "Rfc");
                if (dato != null)
                {
                    RFCE = dato;
                }
                else
                {
                    MessageBox.Show("RFC emisor no leido ");
                    return;
                }


                string strUUID = string.Empty;
                dato = vs.ExtraeValor(sfile, "tfd:TimbreFiscalDigital", "UUID");
                if (dato != null)
                {
                    strUUID = dato;
                }
                txtUUID.Text = strUUID;

                string strserie = string.Empty;
                dato = vs.ExtraeValor(sfile, "cfdi:Comprobante", "Serie");
                if (dato != null)
                {
                    strserie = dato;
                }

                string folio = string.Empty;
                dato = vs.ExtraeValor(sfile, "cfdi:Comprobante", "Folio");
                if (dato != null)
                {
                    folio = dato;
                }
                txtFac.Text = strserie + folio;

                DateTime Fecha = default;
                dato = vs.ExtraeValor(sfile, "cfdi:Comprobante", "Fecha");
                if (dato != null)
                {
                    Fecha = DateTime.Parse(dato);
                }
                dFechaFac.DateTime = Fecha;

                string TipoCambio = string.Empty;
                dato = vs.ExtraeValor(sfile, "cfdi:Comprobante", "TipoCambio");
                if (dato != null)
                {
                    TipoCambio = dato;
                }
                else
                {
                    if (Moneda != "MXN")
                    {
                        MessageBox.Show("Documento sin tipo de cambio");

                    };
                    TipoCambio = "1";
                }
                txtTC.Text = TipoCambio;

                string Neto = string.Empty;
                dato = vs.ExtraeValor(sfile, "cfdi:Comprobante", "Total");
                if (dato != null)
                {
                    Neto = dato;
                }
                txtNeto.Text = Neto;

                ComprasCL clc = new ComprasCL();

                clc.strRfc = RFCE;
                string result = clc.Leeproveedorporrfc();
                if (result == "OK")
                {
                    int intProv = clc.intProveedoresID;
                    cboProveedores.EditValue = intProv;
                }
                else
                {
                    MessageBox.Show("No se pudo leer el proveedor para el RFC: " +RFCE);
                };

                cboMoneda.EditValue = Moneda;


                globalCL clg = new globalCL();
                clg.strGridLayout = "xmlcompras.txt";
                clg.restoreLayout(gridView1);

                using (StreamWriter writer = new StreamWriter(strLayoutPath))
                {
                    writer.WriteLine(sDir);
                }
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            };
        }

        private void bbiCorregirCostos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Está seguro de corregir los costos de la RM: " + serieRM.ToString() + folioRM.ToString(), "Confirmación", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                DateTime date = DateTime.Now;
                // DT ENCABEZADO
                DataTable dtRM = new DataTable();
                dtRM.Columns.Add("Serie", Type.GetType("System.String"));
                dtRM.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtRM.Columns.Add("Comprasserie", Type.GetType("System.String"));
                dtRM.Columns.Add("ComprasFolio", Type.GetType("System.Int32"));
                dtRM.Columns.Add("ContrarecibosFolio", Type.GetType("System.Int32"));
                dtRM.Columns.Add("ContrarecibosSeq", Type.GetType("System.Int32"));
                dtRM.Columns.Add("OrdendecompraSerie", Type.GetType("System.String"));
                dtRM.Columns.Add("Ordedecomprafolio", Type.GetType("System.Int32"));
                dtRM.Columns.Add("Status", Type.GetType("System.Int32"));
                dtRM.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtRM.Columns.Add("FechaCancelacion", Type.GetType("System.DateTime"));
                dtRM.Columns.Add("Motivocancelacion", Type.GetType("System.String"));
                dtRM.Columns.Add("Observaciones", Type.GetType("System.String"));
                dtRM.Columns.Add("Validado", Type.GetType("System.Int32"));
                dtRM.Columns.Add("Fechavalidacion", Type.GetType("System.DateTime"));
                dtRM.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtRM.Columns.Add("FechaReal", Type.GetType("System.DateTime"));
                dtRM.Columns.Add("ProveedoresID", Type.GetType("System.Int32"));
                dtRM.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dtRM.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtRM.Columns.Add("Neto", Type.GetType("System.Decimal"));
                dtRM.Columns.Add("AlmacenesID", Type.GetType("System.Int32"));
                dtRM.Columns.Add("CargoVario", Type.GetType("System.Decimal"));
                decimal gSubtotal = 0.00M;
                decimal gIva = 0.00M;
                decimal gNeto = 0.00M;

                // DT DETALLE
                DataTable dtRMdetalle = new DataTable();
                dtRMdetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtRMdetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtRMdetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtRMdetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtRMdetalle.Columns.Add("Precio", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("importe", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("Costoum2", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("Factorum2", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("PIva", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("PIeps", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("Descuento", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("PDescuento", Type.GetType("System.Decimal"));
                dtRMdetalle.Columns.Add("OCSerie", Type.GetType("System.String"));
                dtRMdetalle.Columns.Add("OCNumero", Type.GetType("System.Int32"));
                dtRMdetalle.Columns.Add("OCSeq", Type.GetType("System.Int32"));
                dtRMdetalle.Columns.Add("Neto", Type.GetType("System.Decimal"));
                
                int seq = 0;
                int articulosID = 0;
                int cantidad = 0;
                decimal precio = 0.00M;
                decimal importe = 0.00M;
                decimal iva = 0.00M;
                decimal neto = 0.00M;

                // LLENAMOS DT DETALLE
                for(int i = 0; i < gridView2.RowCount; i++)
                {
                    // DETALLE
                    serieRM = gridView2.GetRowCellValue(i, "Serie").ToString();
                    folioRM = Convert.ToInt32(gridView2.GetRowCellValue(i, "Folio"));
                    seq = Convert.ToInt32(gridView2.GetRowCellValue(i, "Seq"));
                    articulosID = Convert.ToInt32(gridView2.GetRowCellValue(i, "Articulos"));
                    cantidad = Convert.ToInt32(gridView2.GetRowCellValue(i, "Cantidad"));
                    precio = Convert.ToDecimal(gridView2.GetRowCellValue(i, "Precio"));
                    importe = Convert.ToDecimal(gridView2.GetRowCellValue(i, "Importe"));
                    iva = Convert.ToDecimal(gridView2.GetRowCellValue(i, "Iva"));
                    neto = Convert.ToDecimal(gridView2.GetRowCellValue(i, "Neto"));
                    // ENCABEZADO
                    gSubtotal += importe;
                    gIva += iva;
                    gNeto += neto;
                    // SE LLENA DATATABLE DETALLE
                    dtRMdetalle.Rows.Add(serieRM, folioRM, seq,
                                        cantidad, articulosID, precio,
                                        importe, iva, 0,
                                        0, 0, 0,
                                        0, 0, 0,
                                        "", 0, 0,
                                        neto);
                }
                
                // LLENAMOS DT ENCABEZADO
                dtRM.Rows.Add(serieRM, folioRM,
                            "", 0, 0,
                            0, "", 0,
                            0, date, date,
                            "", "", 0,
                            date, 0, date,
                            0, gSubtotal, gIva,
                            gNeto, 0, 0);

                // SE MANDA A GUARDAR
                RecepciondemercanciaCL cl = new RecepciondemercanciaCL();
                cl.strSerie = serieRM;
                cl.intFolio = folioRM;
                cl.dtRM = dtRM;
                cl.dtRMDet = dtRMdetalle;
                cl.strMaquina = Environment.MachineName;
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                cl.strPrograma = "0123";

                string result = cl.RecepcionMercanciaCorreccionCostos();
                if(result == "OK")
                {
                    cargaRM();
                    LlenaOC();
                    MessageBox.Show("Guardado Correctamente!");
                }
                else
                    MessageBox.Show(result);                
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gridView = sender as GridView;
            if(gridView != null)
            {
                globalCL global = new globalCL();
                string columnName = e.Column.FieldName;
                if (columnName == "Precio" && global.esNumerico(e.Value.ToString())) 
                {
                    int cantidad = Convert.ToInt32(gridView.GetRowCellValue(e.RowHandle, "Cantidad"));
                    decimal precio = Convert.ToDecimal(e.Value);
                    decimal importe = Convert.ToDecimal(precio * cantidad);
                    decimal iva = importe * 0.16M;
                    decimal neto = importe + iva;

                    gridView2.SetRowCellValue(e.RowHandle, "Importe", importe);
                    gridView2.SetRowCellValue(e.RowHandle, "Iva", iva);
                    gridView2.SetRowCellValue(e.RowHandle, "Neto", neto);
                }
            }
        }
    }
}