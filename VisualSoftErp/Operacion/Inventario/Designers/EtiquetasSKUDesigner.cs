using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace VisualSoftErp.Operacion.Inventario.Designers
{
    public partial class EtiquetasSKUDesigner : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable multiplicadosArticulos = new DataTable();
        private DataTable ArticulosSeleccionados = new DataTable
        {
            Columns = {
                { "ArticulosID", typeof(int) },
                { "Código Artículo", typeof(string) },
                { "Nombre Artículo", typeof(string) },
                { "Nombre Línea", typeof(string) },
                { "Nombre Familia", typeof(string) },
                { "Nombre SubFamilia", typeof(string) }
            }
        };

        public EtiquetasSKUDesigner(DataTable articulos, int cantidadEtiquetas)
        {
            ArticulosSeleccionados.Clear();
            ArticulosSeleccionados = articulos;
            multiplicarEtiquetas(cantidadEtiquetas);
            this.DataSource = multiplicadosArticulos;
            InitializeComponent();
        }

        private void multiplicarEtiquetas(int cantidad)
        {
            foreach (DataColumn column in ArticulosSeleccionados.Columns)
            {
                multiplicadosArticulos.Columns.Add(column.ColumnName, column.DataType);
            }

            foreach (DataRow row in ArticulosSeleccionados.Rows)
            {
                for (int i = 0; i < cantidad; i++)
                {
                    multiplicadosArticulos.Rows.Add(row.ItemArray);
                }
            }
        }

        private void lblNombreArticulo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblNombreArticulo.Text = GetCurrentColumnValue("Nombre Artículo").ToString();
        }

        private void lblNombreLineas_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblNombreLineas.Text = GetCurrentColumnValue("Nombre Línea").ToString();
        }

        private void lblNombreFamilias_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblNombreFamilias.Text = GetCurrentColumnValue("Nombre Familia").ToString();
        }

        private void lblNombreSubfamilias_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblNombreSubfamilias.Text = GetCurrentColumnValue("Nombre SubFamilia").ToString();
        }
        
        private void BarCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BarCode.Text = GetCurrentColumnValue("Código Artículo").ToString();
        }
    }
}
