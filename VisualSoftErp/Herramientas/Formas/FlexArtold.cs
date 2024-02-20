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

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class FlexArtold : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FlexArtold()
        {
            InitializeComponent();
            CargaArticulos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
       

        private void CargaArticulos()
        {
            articulosCL cl = new articulosCL();
            gridControlArt.DataSource = cl.flexArt();
            gridViewArt.OptionsView.ShowAutoFilterRow = true;
        }
    }
}