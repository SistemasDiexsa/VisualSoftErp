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

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class ZoneArticulos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public ZoneArticulos()
        {
            InitializeComponent();
            txtSKU.Properties.MaxLength = 20;
            txtSKU.EnterMoveNextControl = true;
            txtPart_Number.Properties.MaxLength = 20;
            txtPart_Number.EnterMoveNextControl = true;
            txtAlt_Part_Number.Properties.MaxLength = 20;
            txtAlt_Part_Number.EnterMoveNextControl = true;
            txtDescription.Properties.MaxLength = 100;
            txtDescription.EnterMoveNextControl = true;

            CargaCombos();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Articulosrep";
            cboArticulosID.Properties.ValueMember = "Clave";
            cboArticulosID.Properties.DisplayMember = "Des";
            cboArticulosID.Properties.DataSource = cl.CargaCombos();
            cboArticulosID.Properties.ForceInitialize();
            cboArticulosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulosID.Properties.PopulateColumns();
            cboArticulosID.Properties.Columns["Clave"].Visible = false;
            cboArticulosID.Properties.NullText = "Seleccione un articulo";
        }

        private void llenaCajas()
        {
            ZoneArticulosCL cl = new ZoneArticulosCL();
            cl.intArticulosID = Convert.ToInt32(cboArticulosID.EditValue);
            String Result = cl.ZoneArticulosLlenaCajas();
            if (Result == "OK")
            {
                txtSKU.Text = cl.strSKU;
                txtPart_Number.Text = cl.strPart_Number;
                txtAlt_Part_Number.Text = cl.strAlt_Part_Number;
                txtDescription.Text = cl.strDescription;
            }
            else
            {
                LimpiaCajas();
            }
        } // llenaCajas

        private void LimpiaCajas()
        {
            txtSKU.Text = string.Empty;
            txtPart_Number.Text = string.Empty;
            txtAlt_Part_Number.Text = string.Empty;
            txtDescription.Text = string.Empty;
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
                ZoneArticulosCL cl = new ZoneArticulosCL();
                cl.intArticulosID = Convert.ToInt32(cboArticulosID.EditValue);
                cl.strSKU = txtSKU.Text;
                cl.strPart_Number = txtPart_Number.Text;
                cl.strAlt_Part_Number = txtAlt_Part_Number.Text;
                cl.strDescription = txtDescription.Text;
                Result = cl.ZoneArticulosCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");                
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
            if (cboArticulosID.EditValue == null)
            {
                return "El campo ArticulosID no puede ir vacio";
            }
            if (txtSKU.Text.Length == 0)
            {
                return "El campo SKU no puede ir vacio";
            }           
            return "OK";
        } //Valida

        private void cboArticulosID_EditValueChanged(object sender, EventArgs e)
        {
            if (cboArticulosID.EditValue == null)
            {
             
            }
            else
            {
                llenaCajas();
            }
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}