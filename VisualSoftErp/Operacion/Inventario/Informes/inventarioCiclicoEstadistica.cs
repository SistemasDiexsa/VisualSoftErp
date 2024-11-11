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
using VisualSoftErp.Operacion.Inventarios.Clases;
using VisualSoftErp.Clases;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraGrid;
using DevExpress.Spreadsheet;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraGrid.Views.Grid;
using System.Xml.Linq;
using System.IO;
using SpreadsheetLight;

namespace VisualSoftErp.Operacion.Inventario.Informes
{
    public partial class inventarioCiclicoEstadistica : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        DataTable dtYM;
        DataTable dtDia;
        DataTable dtDet;
        public inventarioCiclicoEstadistica()
        {
            InitializeComponent();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
       

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiProcesar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            proceso();
        }

        private void proceso()
        {
            try
            {
                globalCL clg = new globalCL();

                InventariofisicorepCL cl = new InventariofisicorepCL();
                DataSet ds = new DataSet();
                ds = cl.InventarioCilicoEstadistica();

                dtYM = new DataTable();
                dtDia = new DataTable();
                dtDet = new DataTable();

                dtDet = ds.Tables[0];
                dtDia= ds.Tables[1];
                dtYM= ds.Tables[2];

                Decimal Ptje;

                foreach (DataRow drYM in dtYM.Rows)
                {

                    Ptje = Convert.ToDecimal(drYM["Diferencia"]) / Convert.ToDecimal(drYM["Contados"]);
                    drYM["Porcentaje"] = 100 - Math.Round(Ptje * 100,2);
                    drYM["Mes"] = clg.NombreDeMes(Convert.ToInt32(drYM["Mes"]));

                }

                string dia;
                string mes;
                string año;


                foreach (DataRow drDia in dtDia.Rows)
                {
                    Ptje = (Convert.ToDecimal(drDia["Diferencia"]) / Convert.ToDecimal(drDia["Contados"]));
                    drDia["Porcentaje"] = 100 - Math.Round(Ptje * 100,2);

                    dia = drDia["Fecha"].ToString().Substring(6, 2);
                    mes = drDia["Fecha"].ToString().Substring(4, 2);
                    año = drDia["Fecha"].ToString().Substring(0, 4);

                    drDia["Fecha"] = dia + "/" + mes + "/" + año;
                }


                gridControlDetalle.DataSource = dtDet;
                gridControlDia.DataSource = dtDia;
                gridControlYearMes.DataSource = dtYM;


                gridViewDia.Columns["Porcentaje"].Caption = "Confiabilidad";
                gridViewyearMes.Columns["Porcentaje"].Caption = "Confiabilidad";

                gridViewDetalle.OptionsView.ShowAutoFilterRow = true;
                gridViewDetalle.OptionsView.ShowViewCaption = true;

                gridViewyearMes.OptionsView.ShowViewCaption = true;

                gridViewDia.OptionsView.ShowViewCaption = true;

                gridViewDetalle.ViewCaption = "CAPTURA DIARIA POR ARTICULO";
                gridViewDia.ViewCaption = "POR FECHA";
                gridViewyearMes.ViewCaption = "POR AÑO Y MES";

                gridViewDia.Columns["Fecha"].DisplayFormat.FormatString = "d";
                gridViewDia.Columns["Porcentaje"].DisplayFormat.FormatString = "n2";

                bbiVistaPrevia.Enabled = true;
            }
            catch (Exception ex)
            {
                bbiVistaPrevia.Enabled = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiVistaPrevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SLDocument excel = new SLDocument();
                excel.DeleteWorksheet("");

                SLStyle titulo = excel.CreateStyle();
                titulo.Font.Bold = true;
                titulo.SetPatternFill(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid, System.Drawing.Color.CadetBlue, System.Drawing.Color.CadetBlue);
                titulo.SetHorizontalAlignment(DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center);

                SLStyle tabla = excel.CreateStyle();
                tabla.Font.Bold = false;
                tabla.SetHorizontalAlignment(DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center);
                
                //dtYM
                excel.AddWorksheet("POR AÑO Y MES");
                excel.ImportDataTable(2, 2, dtYM, true);
                excel.SetColumnStyle(2, tabla);
                excel.SetColumnStyle(3, tabla);
                excel.SetColumnStyle(4, tabla);
                excel.SetColumnStyle(5, tabla); 
                excel.SetColumnStyle(6, tabla);
                excel.SetCellStyle(2, 2, 2, 6, titulo);
                excel.AutoFitColumn(2);
                excel.AutoFitColumn(3);
                excel.AutoFitColumn(4);
                excel.AutoFitColumn(5);
                excel.AutoFitColumn(6);

                //dtDia
                excel.AddWorksheet("POR FECHA");
                excel.ImportDataTable(2, 2, dtDia, true);
                excel.SetColumnStyle(2, tabla);
                excel.SetColumnStyle(3, tabla);
                excel.SetColumnStyle(4, tabla);
                excel.SetColumnStyle(5, tabla);
                excel.SetCellStyle(2, 2, 2, 5, titulo);
                excel.AutoFitColumn(2);
                excel.AutoFitColumn(3);
                excel.AutoFitColumn(4);
                excel.AutoFitColumn(5);

                //dtDet
                excel.AddWorksheet("CAPTURA DIARIA POR ARTICULO");
                excel.ImportDataTable(2, 2, dtDet, true);
                excel.SetColumnStyle(2, tabla);
                excel.SetColumnStyle(3, tabla);
                excel.SetColumnStyle(4, tabla);
                excel.SetColumnStyle(5, tabla);
                excel.SetColumnStyle(6, tabla);
                excel.SetColumnStyle(7, tabla);
                excel.SetColumnStyle(8, tabla);
                excel.SetColumnStyle(9, tabla);
                excel.SetColumnStyle(10, tabla);
                excel.SetColumnStyle(11, tabla);
                excel.SetCellStyle(2, 2, 2, 11, titulo);
                excel.AutoFitColumn(2);
                excel.AutoFitColumn(3);
                excel.AutoFitColumn(4);
                excel.AutoFitColumn(5);
                excel.AutoFitColumn(6);
                excel.AutoFitColumn(7);
                excel.AutoFitColumn(8);
                excel.AutoFitColumn(9);
                excel.AutoFitColumn(10);
                excel.AutoFitColumn(11);

                // Solicitar al usuario la ubicación y el nombre del archivo
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                    saveFileDialog.Title = "Guardar como";
                    saveFileDialog.FileName = "InventarioCiclicoEstadistica.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        excel.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Exportación completada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error durante la exportación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}