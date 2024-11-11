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
using VisualSoftErp.Operacion.Ventas.Designers;
using DevExpress.Pdf.Native.BouncyCastle.Ocsp;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class AutorizarCambiosdePrecio : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private bool permisosEscritura;
        public AutorizarCambiosdePrecio()
        {
            InitializeComponent();
            PermisosEscritura();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }


        #region BOTONES
        private void btnCargarPedido_Click(object sender, EventArgs e)
        {
            string folio = txtPedido.Text;
            if (folio.Length > 0)
                CargaGrid();
        }
        private void bbiVistaPrevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            if (txtPedido.Text != string.Empty) Reporte();
            else MessageBox.Show("Favor de escribir el folio del pedido");
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = employeesNavigationPage;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPage1.Text);
            documentViewer1.DocumentSource = null;
        }
        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
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
        private void bbiHistorialAutorizaciones_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            ReporteHistorialAutorizaciones();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        #endregion BOTONES


        #region INTERACCIONES USUARIO
        private void txtPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargaGrid();
            }
        }
        #endregion INTERACCIONES USUARIO

        private void PermisosEscritura()
        {
            globalCL clg = new globalCL();
            UsuariosCL usuarios = new UsuariosCL();

            clg.strPrograma = "0683";
            if (clg.accesoSoloLectura()) permisosEscritura = false;
            else permisosEscritura = true;

            if (permisosEscritura) bbiAutorizar.Enabled = true;
            else bbiAutorizar.Enabled = false;
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

                lblFecha.Text ="Fecha: " + gridView1.GetRowCellValue(0, "Fecha").ToString().Substring(0,10);
                lblAgente.Text = "Agente: " + gridView1.GetRowCellValue(0, "Agente").ToString();
                lblcte.Text = "Cliente: " + gridView1.GetRowCellValue(0, "Cliente").ToString();
                lblRazon.Text = "Razón: " + gridView1.GetRowCellValue(0, "Razon").ToString();
            }
        }

        private void Reporte()
        {
            try
            {
                int impDirecto = 0;
                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                if (result == "OK") impDirecto = cl.iImpresiondirecta;
                else impDirecto = 0;

                AutorizarCambiosdePrecioDesigner rep = new AutorizarCambiosdePrecioDesigner();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = string.Empty;
                    rep.Parameters["parameter2"].Value = Convert.ToInt32(txtPedido.Text);
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Visible = false;
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                }
                else
                {
                    rep.Parameters["parameter1"].Value = string.Empty;
                    rep.Parameters["parameter2"].Value = Convert.ToInt32(txtPedido.Text);
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.CreateDocument();
                    documentViewer1.DocumentSource = rep;
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImprimir.Text);
                    navigationFrame.SelectedPage = customersNavigationPage;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Generar Reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ReporteHistorialAutorizaciones()
        {
            try
            {
                globalCL cl = new globalCL();
                int impDirecto = 0;
                string result = cl.Datosdecontrol();

                if (result == "OK") impDirecto = cl.iImpresiondirecta;
                else impDirecto = 0;

                MessageBox.Show("**Se Imprime**");
                //if (impDirecto == 1)
                //{

                //    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                //    rpt.Print();
                //}
                //else
                //{

                //    rep.CreateDocument();
                //    documentViewer1.DocumentSource = rep;
                //    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImprimir.Text);
                //    navigationFrame.SelectedPage = customersNavigationPage;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Generar Reporte de Historial", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}