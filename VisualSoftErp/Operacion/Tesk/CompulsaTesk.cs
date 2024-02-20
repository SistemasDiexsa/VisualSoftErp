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
using TeskApiTest.Clases;
using VisualSoftErp.Clases;
using System.IO;

namespace VisualSoftErp.Operacion.Tesk
{
    public partial class CompulsaTesk : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int iTipoDoc = 0;
        string cadenaNumero = string.Empty;
        string cadenaLetra = string.Empty;

        public CompulsaTesk()
        {
            InitializeComponent();
            txtAño.Text = DateTime.Now.Year.ToString();
            txtMes.Text = DateTime.Now.Month.ToString();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Concilia()
        {
            try
            {
                erpCL cl = new erpCL();
                cl.intEjer = Convert.ToInt32(txtAño.Text);
                cl.intMes = Convert.ToInt32(txtMes.Text);
                DataSet ds = cl.ConciliaTesk();
                DataTable dt = new DataTable();

                if (ds != null)
                {
                    // Ventas
                    dt = ds.Tables[0];
                    if (dt != null )
                    {
                        DataRow dr;
                        dr = dt.Rows[0];
                        txtVentasST.Text = Convert.ToDecimal(dr["Subtotal"]).ToString("c2");
                        txtVentasIva.Text = Convert.ToDecimal(dr["Iva"]).ToString("c2");
                        txtVentasNeto.Text = Convert.ToDecimal(dr["Neto"]).ToString("c2");
                    }
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiConciliar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            globalCL clg = new globalCL();
            if (!clg.esNumerico(txtAño.Text))
            {
                MessageBox.Show("Teclee el año");
                return;
            }
            if (!clg.esNumerico(txtMes.Text))
            {
                MessageBox.Show("Teclee el mes");
                return;
            }

            Concilia();
        }

        private void bbiCargar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CargaXls();
        }

        private string RestorePath()
        {
            try
            {
                string line;
                string ruta = System.Configuration.ConfigurationManager.AppSettings["gridlayout"].ToString();

                // Read the file and display it line by line.  
                System.IO.StreamReader file =
                    new System.IO.StreamReader(ruta + "conciliatesk.txt");
                line = file.ReadLine();

                file.Close();
                return line;
            }
            catch (Exception)
            {
                return "c:\\";
            }
        }

        private void savePath(string sPath)
        {
            string text = sPath;
            string ruta = System.Configuration.ConfigurationManager.AppSettings["gridlayout"].ToString();
            System.IO.File.WriteAllText(ruta + "conciliatesk.txt", text);
        } //savePath

        private void CargaXls()
        {
            try
            {


                xtraOpenFileDialog1.InitialDirectory = RestorePath();
                xtraOpenFileDialog1.Filter = "Excel 2003 o anteriores|*.xls|Excel 2010|*.xlsx";
                xtraOpenFileDialog1.FilterIndex = 1;
                xtraOpenFileDialog1.RestoreDirectory = true;

                if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("XLS", "Leyendo archivo de excel...");
                    FileInfo fileinfo = new FileInfo(xtraOpenFileDialog1.FileName);
                    savePath(fileinfo.DirectoryName);

                    spreadsheetControl1.LoadDocument(xtraOpenFileDialog1.FileName);
                   

                    //string sCC = spreadsheetControl1.ActiveWorksheet.Cells[2, 2].Value.ToString();
                    //sCC = sCC.Substring(sCC.Length - 4, 4);
                    //sCC = sCC.Substring(1, 2);

                   

                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CargaXLS:" + ex.Message);
            }
        } //Cargaxml

        private void bbiVentas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            iTipoDoc = 1;
            navigationFrame.SelectedPageIndex = 2;
            ribbonPageGroup2.Visible = true;
        }

        private void bbiSQL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string strTD = string.Empty;
                string strSerie = string.Empty;
                int iFolio;
                decimal dST;
                decimal dIva;
                decimal dNeto;
                string strDoc = string.Empty;
                string strFolio = string.Empty;
                string strST = string.Empty;
                string strIva = string.Empty;
                string strNeto = string.Empty;
                string strRfc = string.Empty;
                string strMoneda = string.Empty;

                switch (iTipoDoc)
                {
                    case 1:
                        strDoc = "Factura";
                        break;
                }


                erpCL cl = new erpCL();
                string result = string.Empty;

                for (int i = 0; i < 10000; i++)
                {
                    //Leemos los valores de cada celda
                    strTD = spreadsheetControl1.ActiveWorksheet.Cells[i, 2].Value.ToString();

                    if (strTD == strDoc)
                    {
                        strFolio = spreadsheetControl1.ActiveWorksheet.Cells[i, 3].Value.ToString();
                        strRfc = spreadsheetControl1.ActiveWorksheet.Cells[i, 5].Value.ToString();
                        strMoneda = spreadsheetControl1.ActiveWorksheet.Cells[i, 7].Value.ToString();
                        strST = spreadsheetControl1.ActiveWorksheet.Cells[i, 8].Value.ToString();
                        strMoneda = spreadsheetControl1.ActiveWorksheet.Cells[i, 7].Value.ToString();
                        strIva = spreadsheetControl1.ActiveWorksheet.Cells[i, 10].Value.ToString();
                        strNeto = spreadsheetControl1.ActiveWorksheet.Cells[i, 13].Value.ToString();

                        LetrayNumeros(strFolio);

                        cl.intTipoDoc = iTipoDoc;
                        cl.strSerie = cadenaLetra;
                        cl.intFolio = Convert.ToInt32(cadenaNumero);
                        cl.decSubtotal = Convert.ToDecimal(strST);
                        cl.decIva = Convert.ToDecimal(strIva);
                        cl.decNeto = Convert.ToDecimal(strNeto);
                        cl.intEjer = Convert.ToInt32(txtAño.Text);
                        cl.intMes = Convert.ToInt32(txtMes.Text);
                        if (strMoneda.Substring(0, 5) == "Pesos")
                            cl.strMoneda = "MXN";
                        else
                            cl.strMoneda = "USD";
                        cl.strRfc = strRfc;

                        result = cl.teskConciliacionCrud();
                        if (result != "OK")
                        {
                            MessageBox.Show("Error: " + result + " " + cadenaNumero);
                        }

                    }
                }

                MessageBox.Show("Proceso terminado");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LetrayNumeros(string cadena)
        {
            cadenaLetra = string.Empty;
            cadenaNumero = string.Empty;
            foreach (Char c in cadena.ToCharArray())
            {
                if (Char.IsNumber(c))
                    cadenaNumero += c;
                else
                    cadenaLetra += c;
            }

        
        }
    }
}