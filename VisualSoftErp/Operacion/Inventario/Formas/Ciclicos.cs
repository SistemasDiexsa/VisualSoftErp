using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Inventario.Formas
{
    public partial class Ciclicos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Ciclicos()
        {
            InitializeComponent();
            cargarCombos();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void cargarCombos()
        {
            combosCL combos = new combosCL();
            combos.strTabla = "Articulos";

            cboArticulos.Properties.ValueMember = "Articulo";
            cboArticulos.Properties.DisplayMember = "Des";
            cboArticulos.Properties.DataSource = combos.CargaCombos();
            cboArticulos.Properties.ForceInitialize();
            cboArticulos.Properties.PopulateColumns();
            cboArticulos.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulos.Properties.Columns["Clave"].Visible = false;
            cboArticulos.Properties.Columns["FactorUM2"].Visible = false;
            cboArticulos.Properties.Columns["Articulo"].Visible = false;
            cboArticulos.EditValue = null;
            cboArticulos.Properties.NullText = "Favor de seleccionar un artículo";
        }

        private string Validar()
        {
            string result = "OK";

            if (cboArticulos.EditValue == null)
                result = "Favor de seleccionar un artículo";

            if (txtConteo.Text == string.Empty)
                result = "Favor de introducir el conteo";

            return result;
        }

        private void bbiGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            string result = Validar();
            if(result == "OK")
            {
                CiclicosCL cl = new CiclicosCL();
                cl.strArticulo = cboArticulos.EditValue.ToString();
                cl.intConteo = Convert.ToInt32(txtConteo.Text);
                cl.intUsuario = globalCL.gv_UsuarioID;
                result = cl.InventarioCiclicoCrud();
                if(result != "OK")
                {
                    MessageBox.Show(result);
                }
                else
                {
                    lblDiferencia.Text = cl.intDiferencia.ToString();
                    labelDiferencia.Visible = true;
                    lblExistencia.Text = cl.intExistencia.ToString();
                    labelExistencia.Visible = true;
                    MessageBox.Show("Guardado Exitosamente");
                }
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void txtConteo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}