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
using DevExpress.XtraDiagram.Bars;
using DevExpress.XtraRichEdit.Import.Doc;

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class ComercioExterior : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string strSerie;
        int intFolio;

        public ComercioExterior()
        {
            InitializeComponent();
            CargarCombos();
            lblPedidos.Text = "Pedido: " + globalCL.gv_Serie + globalCL.gv_Folio.ToString();
            ValidarSerieyFolio();
           
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargarCombos()
        {
            // COMBO TIPO EXPORTACION
            DataTable dtSrcTipoExportacion = new DataTable();
            dtSrcTipoExportacion.Columns.Add("ID", typeof(string));
            dtSrcTipoExportacion.Columns.Add("Des", typeof(string));
            dtSrcTipoExportacion.Rows.Add("02", "Definitiva con clave A1");
            dtSrcTipoExportacion.Rows.Add("04", "Definitiva con clave distinta a A1 o cuando no existe enajenación en términos del CFF");
            cboTipoExportacion.Properties.DataSource = dtSrcTipoExportacion;
            cboTipoExportacion.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTipoExportacion.Properties.ForceInitialize();
            cboTipoExportacion.Properties.PopulateColumns();
            cboTipoExportacion.Properties.ValueMember = "ID";
            cboTipoExportacion.Properties.DisplayMember = "Des";
            cboTipoExportacion.Properties.Columns["ID"].Visible = false;
            cboTipoExportacion.ItemIndex = 0;
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridComercioExterior";
            string result = clg.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        public class detalleCL
        {
            public int Seq { get; set; }
            public string Cantidad { get; set; }
            public string Articulo { get; set; }
            public string Des { get; set; }
            public decimal CantidadAduana { get; set; }
            public string Valor { get; set; }
            public decimal Unidad { get; set; }
        }

        public void Guardar()
        {
            try
            {
                System.Data.DataTable dtPedidosComercioExterior = new System.Data.DataTable("PedidosComercioExterior");
                dtPedidosComercioExterior.Columns.Add("Serie", Type.GetType("System.String"));
                dtPedidosComercioExterior.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtPedidosComercioExterior.Columns.Add("Tipooperacion", Type.GetType("System.String"));
                dtPedidosComercioExterior.Columns.Add("Clavedepedimento", Type.GetType("System.String"));
                dtPedidosComercioExterior.Columns.Add("Certificadororigen", Type.GetType("System.String"));
                dtPedidosComercioExterior.Columns.Add("Numcertificadoorigen", Type.GetType("System.String"));
                dtPedidosComercioExterior.Columns.Add("Numexportadorconfiable", Type.GetType("System.String"));
                dtPedidosComercioExterior.Columns.Add("Incoterm", Type.GetType("System.String"));
                dtPedidosComercioExterior.Columns.Add("Subdivision", Type.GetType("System.String"));
                dtPedidosComercioExterior.Columns.Add("ceObs", Type.GetType("System.String"));
                dtPedidosComercioExterior.Columns.Add("TipoExportacion", Type.GetType("System.String"));

                System.Data.DataTable dtPedidosdetalleComercioExterior = new System.Data.DataTable("PedidosdetalleComercioExterior");
                dtPedidosdetalleComercioExterior.Columns.Add("Serie", Type.GetType("System.String"));
                dtPedidosdetalleComercioExterior.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtPedidosdetalleComercioExterior.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtPedidosdetalleComercioExterior.Columns.Add("Cantidadaduana", Type.GetType("System.Decimal"));
                dtPedidosdetalleComercioExterior.Columns.Add("Valorunitarioaduana", Type.GetType("System.Decimal"));
                dtPedidosdetalleComercioExterior.Columns.Add("Unidadaduana", Type.GetType("System.String"));


                dtPedidosComercioExterior.Rows.Add(globalCL.gv_Serie, globalCL.gv_Folio, txtTipooperacion.Text, txtClavedepedimento.Text, txtCertificadoOrigen.Text, txtNumCertificado.Text, txtNumExpConfiable.Text, txtIncomterm.Text, txtSubDivision.Text, txtObs.Text, cboTipoExportacion.EditValue.ToString());

                decimal cantidad=0;
                string unidad = string.Empty;
                decimal precio = 0;
                int seq = 0;


                for (
                    int i = 0; i <= gridViewDetalle.RowCount - 1; i++)
                {
                    seq = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Seq"));
                    cantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "CantidadAduana"));
                    unidad = gridViewDetalle.GetRowCellValue(i, "Unidad").ToString();
                    precio = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Valor"));

                    dtPedidosdetalleComercioExterior.Rows.Add(globalCL.gv_Serie, globalCL.gv_Folio, seq, cantidad, precio, unidad);
                }

                PedidosComercioExteriorCL cl = new PedidosComercioExteriorCL();
                cl.strSerie = globalCL.gv_Serie;
                cl.intFolio = globalCL.gv_Folio;
                cl.dtCE = dtPedidosComercioExterior;
                cl.dtCEDet = dtPedidosdetalleComercioExterior;

                string result = cl.PedidosComercioExteriorCrud();
                if (result != "OK")
                    MessageBox.Show(result);
                else
                    MessageBox.Show("Guardado correctmente");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ValidarSerieyFolio()
        {
            PedidosComercioExteriorCL cl = new PedidosComercioExteriorCL();
            cl.strSerie = globalCL.gv_Serie;
            cl.intFolio = globalCL.gv_Folio;
            String Result = cl.PedidosComercioExteriorLlenaCajas();
            if (Result == "OK")
            {
                txtTipooperacion.Text = cl.strTipooperacion;
                txtClavedepedimento.Text = cl.strClavedepedimento;
                txtCertificadoOrigen.Text = cl.strCertificadororigen;
                txtNumCertificado.Text = cl.strNumcertificadoorigen;
                txtNumExpConfiable.Text = cl.strNumexportadorconfiable;
                txtIncomterm.Text = cl.strIncoterm;
                txtSubDivision.Text = cl.strSubdivision;
                txtObs.Text = cl.strceObs;
                cboTipoExportacion.EditValue = cl.strTipoExportacion;

                LlenarGrid();
            }
            else
            {
                txtTipooperacion.Text = string.Empty;
                txtClavedepedimento.Text = string.Empty;
                txtCertificadoOrigen.Text = string.Empty;
                txtCertificadoOrigen.Text = string.Empty;
                txtNumExpConfiable.Text = string.Empty;
                txtIncomterm.Text = string.Empty;
                txtSubDivision.Text = string.Empty;
                txtObs.Text = string.Empty;
                cboTipoExportacion.ItemIndex = 0;

                LlenarGrid();
            }
        }

        public void LlenarGrid()
        {
            PedidosComercioExteriorCL cl = new PedidosComercioExteriorCL();
            cl.strSerie = globalCL.gv_Serie;
            cl.intFolio = globalCL.gv_Folio;
            gridControlDetalle.DataSource = cl.PedidosComercioExteriorGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridComercioExterior";
            clg.restoreLayout(gridViewDetalle);
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}