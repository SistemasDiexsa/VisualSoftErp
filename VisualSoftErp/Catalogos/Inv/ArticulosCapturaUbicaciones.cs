using DevExpress.CodeParser;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Catalogos.Inv
{
    public partial class ArticulosCapturaUbicaciones : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intArtID = 0;
        public ArticulosCapturaUbicaciones()
        {
            InitializeComponent();
            cargaCombos();

            gridView.OptionsBehavior.ReadOnly = false; 
            gridView.OptionsBehavior.Editable = true;
            gridView.OptionsView.ShowAutoFilterRow = true;

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        void cargaCombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();

            cl.strTabla = "Lineas";
            src.DataSource = cl.CargaCombos();
            cboLineas.Properties.ValueMember = "Clave";
            cboLineas.Properties.DisplayMember = "Des";
            cboLineas.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboLineas.Properties.ForceInitialize();
            cboLineas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboLineas.Properties.PopulateColumns();
            cboLineas.Properties.Columns["Clave"].Visible = false;
            cboLineas.ItemIndex = 0;

            cl.strTabla = "Familias";
            src.DataSource = cl.CargaCombos();
            cboFamilias.Properties.ValueMember = "Clave";
            cboFamilias.Properties.DisplayMember = "Des";
            cboFamilias.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboFamilias.Properties.ForceInitialize();
            cboFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilias.Properties.PopulateColumns();
            cboFamilias.Properties.Columns["Clave"].Visible = false;
            cboFamilias.ItemIndex = 0;
        }
        void bbiPrintPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl.ShowRibbonPrintPreview();
        }
    
        private void bbiCerrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "artubi" +
                "";
            string result = clg.SaveGridLayout(gridView);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            procesar();
        }

        void procesar()
        {

            articulosCL cl = new articulosCL();
            cl.intLinea= Convert.ToInt32(cboLineas.EditValue);
            cl.intFam= Convert.ToInt32(cboFamilias.EditValue);
            gridControl.DataSource = cl.articulosUbicaciones();
            globalCL clg = new globalCL();
            clg.strGridLayout = "artubi";
            clg.restoreLayout(gridView);

            gridView.Columns[0].OptionsColumn.ReadOnly = true;
            gridView.Columns[1].OptionsColumn.ReadOnly = true;
            gridView.Columns[2].OptionsColumn.ReadOnly = true;
            gridView.Columns[3].OptionsColumn.ReadOnly = true;
            gridView.Columns[4].OptionsColumn.ReadOnly = true;
        }

        private void bbiGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            guardar();
        }

        void guardar()
        {
            try
            {
                string result = string.Empty;
                articulosCL cl = new articulosCL();
                string art;
                for (int i = 0; i <= gridView.RowCount - 1; i++) // SE AGREGO EL <=
                {
                    intArtID = Convert.ToInt32(gridView.GetRowCellValue(i, "ArticulosID"));
                    if (intArtID>0)
                    {
                        cl.intArticulosID = intArtID;
                        cl.strUbicación = gridView.GetRowCellValue(i, "Ubicacion").ToString();

                        result = cl.articulosActualizaUbicacion();

                        if (result != "OK")
                        {
                            MessageBox.Show("Error al actualizar el art:" + gridView.GetRowCellValue(i, "Articulo").ToString());
                            break;
                        }

                    }
                }

                MessageBox.Show("Proceso terminado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}