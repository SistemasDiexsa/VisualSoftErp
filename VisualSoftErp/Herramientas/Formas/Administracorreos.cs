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
using VisualSoftErp.Clases.HerrramientasCLs;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class Administracorreos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intFolio;       
        string strSerie;
        string strSubject;

        public Administracorreos()
        {
            InitializeComponent();
            CargaGrid("F");
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }
        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
        }

        private void CargaGrid(string Doc)
        {
            try
            {
                correosCL cl = new correosCL();
                cl.strDoc = Doc;
                gridControl1.DataSource = cl.CorreosGrid();
                
                advBandedGridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
                advBandedGridView1.OptionsBehavior.ReadOnly = true;
                advBandedGridView1.OptionsBehavior.Editable = false;
                bandedGridColumnSubject.OptionsColumn.AllowFocus = false;
                bandedGridColumnPara.OptionsColumn.AllowFocus = false;
                bandedGridColumnFechaReal.OptionsColumn.AllowFocus = false;
                advBandedGridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;

                advBandedGridView1.OptionsView.ShowAutoFilterRow = true;

                try
                {
                    advBandedGridView1.FocusedRowHandle = 0;
                    GetValueRowClick();
                }
                catch (Exception)
                {
                    gridControl2.DataSource = null;
                    pdfViewer1.CloseDocument();
                }
             
                

            }
            catch(Exception ex)
            {
                MessageBox.Show("Cargagrid: " + ex.Message);
            }
        }

        private void CargaGridDetalle(string Doc)
        {
            try
            {
                correosCL cl = new correosCL();
                cl.strDoc = Doc;
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                gridControl2.DataSource = cl.CorreosDetalleGrid();

                advBandedGridView2.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
                advBandedGridView2.OptionsBehavior.ReadOnly = true;
                advBandedGridView2.OptionsBehavior.Editable = false;
                bandedGridColumnDetPara.OptionsColumn.AllowFocus = false;
                bandedGridColumnDetFechaReal.OptionsColumn.AllowFocus = false;
                advBandedGridView2.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
               

                gridBand1.Caption = strSubject;   
                
                AbrePdf();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cargagrid: " + ex.Message);
            }
        }

        private void AbrePdf()
        {
            try
            {
                string ruta = string.Empty;
                DateTime dFecha = Convert.ToDateTime(advBandedGridView1.GetRowCellValue(advBandedGridView1.FocusedRowHandle, "Fechadocumento"));
                string Año = dFecha.Year.ToString();
                int Mes = dFecha.Month;

                globalCL cl = new globalCL();

                string sMes = cl.NombreDeMes(Mes,3);

                ruta = System.Configuration.ConfigurationManager.AppSettings["pathpdf"].ToString();
                ruta = ruta + Año + "\\" + sMes + "\\" + strSerie + intFolio.ToString() + ".Pdf";

                this.pdfViewer1.LoadDocument(@ruta);
                this.pdfViewer1.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.FitToWidth;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nviFac_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            CargaGrid("F");
        }

        private void nviCP_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            CargaGrid("P");
        }

        private void nviNC_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            CargaGrid("N");
        }

        private void EnviaCorreo()
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Correos", "Enviando...");
                globalCL cl = new globalCL();

                correosCL clc = new correosCL();
                string doc= advBandedGridView1.GetRowCellValue(advBandedGridView1.FocusedRowHandle, "Documento").ToString();
                clc.strDoc = doc; 
                
                clc.strSerie = strSerie;
                clc.intFolio = intFolio;
                DateTime dFecha = Convert.ToDateTime(advBandedGridView1.GetRowCellValue(advBandedGridView1.FocusedRowHandle, "Fechadocumento"));


                string result = clc.Obtenercorreodecliente();

                if (result != "OK")
                {
                    MessageBox.Show("No se pudo obtener el correo del cliente");
                    return;
                }

                string strTo = clc.strEMail;

                result = cl.EnviaCorreo(strTo, strSerie, intFolio, dFecha, doc);
               
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                

                if (cl.iAbrirOutlook == 0)
                {
                    MessageBox.Show(result);
                }
            }
        }


        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiEnviar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EnviaCorreo();
        }


        private void GetValueRowClick()
        {
            strSerie = advBandedGridView1.GetRowCellValue(advBandedGridView1.FocusedRowHandle, "Serie").ToString();
            intFolio = Convert.ToInt32(advBandedGridView1.GetRowCellValue(advBandedGridView1.FocusedRowHandle, "Folio"));
            string doc = advBandedGridView1.GetRowCellValue(advBandedGridView1.FocusedRowHandle, "Documento").ToString();
            strSubject = advBandedGridView1.GetRowCellValue(advBandedGridView1.FocusedRowHandle, "Subject").ToString();

            CargaGridDetalle(doc);
        }

        private void advBandedGridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            GetValueRowClick();
        }
    }
}