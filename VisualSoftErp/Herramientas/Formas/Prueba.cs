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
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class Prueba : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Prueba()
        {
            InitializeComponent();
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("Serie", Type.GetType("System.String"));
                table.Columns.Add("Folio", Type.GetType("System.Int32"));
                table.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                table.Columns.Add("Tipocop", Type.GetType("System.String"));

                table.Rows.Add('A', 1, DateTime.Now, 'C');
                table.Rows.Add('A', 2, DateTime.Now, 'P');
                table.Rows.Add('A', 3, DateTime.Now, 'C');

                DataSet ds = new DataSet();
                ds.Tables.Add(table);


                ds.WriteXmlSchema(@"c:\visualsofterp\tabla.xsd"); //save an XML schema  
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Serie", Type.GetType("System.String"));
            table.Columns.Add("Folio", Type.GetType("System.Int32"));
            table.Columns.Add("Fecha", Type.GetType("System.DateTime"));
            table.Columns.Add("Tipocop", Type.GetType("System.String"));

            table.Rows.Add('A', 1, DateTime.Now, 'C');
            table.Rows.Add('A', 2, DateTime.Now, 'P');
            table.Rows.Add('A', 3, DateTime.Now, 'C');

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

           
            // Create a report and bind it to a dataset.
            PruebaDesigner report = new PruebaDesigner();
            report.DataSource = ds;

            // Show the print preview.
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreview();
        }
    }
}