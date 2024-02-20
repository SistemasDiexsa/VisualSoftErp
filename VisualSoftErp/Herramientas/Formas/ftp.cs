using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class ftp : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public ftp()
        {
            InitializeComponent();

          
        }
      
   
  

        private void bbiCerrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiBajar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (txtFolio.Text.Length == 0)
            {
                MessageBox.Show("Teclee el folio de la factura");
            }

            globalCL clg = new globalCL();
            string result = clg.descargaFtp("","");
        }
    }
}