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

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class ComercioExterior : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public ComercioExterior()
        {
            InitializeComponent();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        public class detalleCL
        {
            public int Seq { get; set; }
            public string Articulo { get; set; }
            public string Des { get; set; }
            public decimal Cantidad { get; set; }
            public string Unidad { get; set; }
            public decimal Valorunitario { get; set; }
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

                System.Data.DataTable dtPedidosdetalleComercioExterior = new System.Data.DataTable("PedidosdetalleComercioExterior");
                dtPedidosdetalleComercioExterior.Columns.Add("Serie", Type.GetType("System.String"));
                dtPedidosdetalleComercioExterior.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtPedidosdetalleComercioExterior.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtPedidosdetalleComercioExterior.Columns.Add("Cantidadaduana", Type.GetType("System.Decimal"));
                dtPedidosdetalleComercioExterior.Columns.Add("Valorunitarioaduana", Type.GetType("System.Decimal"));
                dtPedidosdetalleComercioExterior.Columns.Add("Unidadaduana", Type.GetType("System.String"));

                



                dtPedidosComercioExterior.Rows.Add(globalCL.gv_Serie, globalCL.gv_Folio, txtTipooperacion.Text, txtClavedepedimento.Text, txtCertificadoOrigen.Text, txtNumCertificado.Text, txtNumExpConfiable.Text, txtIncomterm.Text, txtSubDivision.Text, txtObs.Text);

                decimal cantidad=0;
                string unidad = string.Empty;
                decimal precio = 0;
                int seq = 0;


                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    seq = Convert.ToInt32(gridView1.GetRowCellValue(i, "Seq"));
                    cantidad = Convert.ToDecimal(gridView1.GetRowCellValue(i, "Cantidad"));
                    unidad = gridView1.GetRowCellValue(i, "Unidad").ToString();
                    precio = Convert.ToDecimal(gridView1.GetRowCellValue(i, "ValorUnitario"));

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

    }
}