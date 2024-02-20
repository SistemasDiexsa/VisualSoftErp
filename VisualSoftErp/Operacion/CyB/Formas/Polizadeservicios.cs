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
using VisualSoftErp.Operacion.Compras.Clases;
using VisualSoftErp.Operacion.CyB.Clases;

namespace VisualSoftErp.Operacion.CyB.Formas
{
    public partial class Polizadeservicios : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Polizadeservicios()
        {
            InitializeComponent();
            txtEjercicio.Text = DateTime.Now.Year.ToString();

            globalCL clg = new globalCL();

            Cargacombos();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            List<ClaseGenricaCL> mesescl = new List<ClaseGenricaCL>();
            mesescl.Add(new ClaseGenricaCL() { Clave = "1", Des = "Enero" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "2", Des = "Febrero" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "3", Des = "Marzo" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "4", Des = "Abril" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "5", Des = "Mayo" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "6", Des = "Junio" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "7", Des = "Julio" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "8", Des = "Agosto" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "9", Des = "Septiembre" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "10", Des = "Octubre" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "11", Des = "Noviembre" });
            mesescl.Add(new ClaseGenricaCL() { Clave = "12", Des = "Diciembre" });


            cl.strTabla = "";
            cboMeses.Properties.ValueMember = "Clave";
            cboMeses.Properties.DisplayMember = "Des";
            cboMeses.Properties.DataSource = mesescl;
            cboMeses.Properties.ForceInitialize();
            cboMeses.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMeses.Properties.PopulateColumns();
            cboMeses.Properties.Columns["Clave"].Visible = false;

            if (DateTime.Now.Month == 1)
            {
                txtEjercicio.Text = (DateTime.Now.Year - 1).ToString();
                cboMeses.ItemIndex = 11;
            }
            else
            {
                cboMeses.ItemIndex = DateTime.Now.Month - 2;
            }
        } // Carga combos
        private void CargaGrid()
        {

            bool blnSiSePuedeGenerar = false;

            cybCL cl = new cybCL();
            cl.intEje = Convert.ToInt32(txtEjercicio.Text);
            cl.intMes = Convert.ToInt32(cboMeses.EditValue);
            gridControl1.DataSource = cl.Servicios();


            string strRfc = string.Empty;
            string result = string.Empty;
            string strCta = string.Empty;
            string strCtaGastos = string.Empty;
            int Pol = 0;




            for (int i = 0; i <= gridView1.RowCount - 1; i++)
            {

                Pol = Convert.ToInt32(gridView1.GetRowCellValue(i, "Poliza"));
                if (Pol == 0)
                {
                    blnSiSePuedeGenerar = true;
                }

                strRfc = gridView1.GetRowCellValue(i, "Rfc").ToString();
                cl.strRfc = strRfc;

                strCtaGastos = string.Empty;
                strCta = string.Empty;

                result = cl.Leecuentacontable();
                if (result == "OK")
                {
                    strCta = cl.strCuenta;
                    strCtaGastos = cl.strCuentaGastos;
                }
                else
                {
                    strCta = result;
                }
                gridView1.FocusedRowHandle = i;
                gridView1.SetFocusedRowCellValue("Cuenta", strCta);
                gridView1.SetFocusedRowCellValue("CuentaGastoCompra", strCtaGastos);
            }

            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.OptionsBehavior.ReadOnly = true;
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPolizadeservicios";
            clg.restoreLayout(gridView1);


            if (blnSiSePuedeGenerar == false)
            {
                bbiGenerar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

        } //CargaGrid

        private void bbiSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPolizadeservicios";
            String Result = clg.SaveGridLayout(gridView1);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }

        private void bbiGenerar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtEjercicio.Text))
                {
                    MessageBox.Show("Teclee un ejercicio");
                    return;
                }

                DialogResult Result = MessageBox.Show("Iniciar generación de póliza", "Generar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "No")
                {
                    return;
                }


                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Generando póliza", "espere por favor...");



                cybCL cl = new cybCL();
                cl.intEje = Convert.ToInt32(txtEjercicio.Text);
                cl.intMes = Convert.ToInt32(cboMeses.EditValue);
                string result = cl.Polizadepagoscxp(gridView1);
                if (result == "OK")
                {
                    MessageBox.Show("Generado correctamente, Primer póliza: D" + cl.strPrimerPoliza);
                    CargaGrid();
                }
                else
                {
                    MessageBox.Show(result);
                }

                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiCargar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CargaGrid();
        }
    }
}