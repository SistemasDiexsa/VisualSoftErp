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
using System.IO;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using VisualSoftErp.Herramientas.Formas;
using DevExpress.Utils.UI.Localization;

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
            else MessageBox.Show(result);  
              
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
                MessageBox.Show(result);

                if (result == "OK")
                {
                    limpiacajas();
                }

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
                Decimal cargoV = Convert.ToDecimal(TxtCargoVario.Text) * (1 + (pIvaProv/100)) ;
                if (tNeto + cargoV != Convert.ToDecimal(txtNeto.Text)) 
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

        private void cboProveedores_EditValueChanged_1(object sender, EventArgs e)
        {
           // limpiacajas();
            object orow = cboProveedores.Properties.GetDataSourceRowByKeyValue(cboProveedores.EditValue);
            if (orow != null)
            {
                txtPlazo.Text = ((DataRowView)orow)["Plazo"].ToString();
                pIvaProv = Convert.ToDecimal(((DataRowView)orow)["Piva"]);
                cboMoneda.EditValue = ((DataRowView)orow)["MonedasID"].ToString();
                strRFCemisor = ((DataRowView)orow)["RFC"].ToString();
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

            }
            catch(Exception ex)
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
    }
}