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

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class Clientesquenohabiancomprado : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Clientesquenohabiancomprado()
        {
            InitializeComponent();
            txtEje.Text = DateTime.Now.Year.ToString();
            txtMes.Text = DateTime.Now.Month.ToString();
            txtDias.Text = "364";
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }


        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiProcesar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Newsa.net","Procesando...");
            procesar();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void procesar()
        {
            try
            {
                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtEje.Text)) {
                    MessageBox.Show("Teclee el ejercicio");
                    return;
                }
                if (!clg.esNumerico(txtMes.Text)) {
                    MessageBox.Show("Teclee el mes");
                    return;
                }
                if (!clg.esNumerico(txtDias.Text)) {
                    MessageBox.Show("Teclee los días");
                    return;
                }

                FacturasCL cl = new FacturasCL();
                cl.intEje = Convert.ToInt32(txtEje.Text);
                cl.intMes = Convert.ToInt32(txtMes.Text);
                cl.intDias = Convert.ToInt32(txtDias.Text);

                gridControl1.DataSource = cl.Clientesquenohancomprado();

                string result = string.Empty;
                int Dias = 0;
                DateTime fecha;
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    cl.intClientesID = Convert.ToInt32(gridView1.GetRowCellValue(i, "ClientesID"));
                    fecha = Convert.ToDateTime(gridView1.GetRowCellValue(i, "Fecha"));
                    cl.intEje = Convert.ToInt32(txtEje.Text);
                    cl.intMes = Convert.ToInt32(txtMes.Text);



                    result = cl.ClientesquenohancompradoFacturaAnterior();
                    if (result == "OK")
                    {
                        gridView1.FocusedRowHandle = i;
                        gridView1.SetFocusedRowCellValue("UltimaSerie", cl.strSerie);
                        gridView1.SetFocusedRowCellValue("UltimoFolio", cl.intClientesID);
                        gridView1.SetFocusedRowCellValue("UltimaFecha", cl.fFecha);

                        Dias = (fecha - cl.fFecha).Days;

                        gridView1.SetFocusedRowCellValue("Dias", Dias);
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiPrevio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }
    }
}