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
using VisualSoftErp.Clases.VentasCLs;

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class AutorizarCambiosdePrecio : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public AutorizarCambiosdePrecio()
        {
            InitializeComponent();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        
       

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargaGrid();
            }
        }

        private void txtPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void CargaGrid()
        {
            PedidosCL cl = new PedidosCL();
            cl.strSerie = string.Empty;
            cl.intFolio = Convert.ToInt32(txtPedido.Text);
            gridControl1.DataSource = cl.PedidosGridAutorizaPrecios();
            if (gridView1.RowCount > 0)
            {
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
                gridView1.Columns[2].Visible = false;
                gridView1.Columns[3].Visible = false;
                gridView1.Columns[9].Visible = false;

                gridView1.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView1.Columns[6].DisplayFormat.FormatString = "c2";
                gridView1.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView1.Columns[7].DisplayFormat.FormatString = "c2";
                gridView1.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView1.Columns[8].DisplayFormat.FormatString = "c2";

                lblFecha.Text = gridView1.GetRowCellValue(0, "Fecha").ToString().Substring(0,10);
                lblAgente.Text = "AGENTE: " + gridView1.GetRowCellValue(0, "Agente").ToString();
                lblcte.Text = "CLIENTE: " + gridView1.GetRowCellValue(0, "Cliente").ToString();
                lblRazon.Text = "RAZÓN: " + gridView1.GetRowCellValue(0, "Razon").ToString();


            }
        }

        private void txtPedido_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bbiAutorizar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PedidosCL cl = new PedidosCL();
            cl.strSerie = string.Empty;
            cl.intFolio = Convert.ToInt32(txtPedido.Text);
            string result = cl.PedidosAutorizaCambiodePrecio();
            if (result == "OK")
                MessageBox.Show("OK");
            else
                MessageBox.Show(result);
        }
    }
}