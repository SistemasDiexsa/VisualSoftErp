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
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class PedidosCambiosdeprecio : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public PedidosCambiosdeprecio()
        {
            InitializeComponent();
            Proceso();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
       
        private void Proceso()
        {
            PedidosCL cl = new PedidosCL();
            gridControl1.DataSource = cl.PedidosCambiodeprecio();

            globalCL clg = new globalCL();
            clg.strGridLayout = "pedidoscambiosdeprecio";
            clg.restoreLayout(gridView1);

            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.OptionsBehavior.ReadOnly = true;

        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "pedidoscambiosdeprecio";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiPrev_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.ShowRibbonPrintPreview();
        }
    }
}