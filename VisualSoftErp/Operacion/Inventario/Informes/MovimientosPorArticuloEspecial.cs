using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.TextEditController.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Catalogos.Inv;
using VisualSoftErp.Catalogos;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Inventarios.Clases;

namespace VisualSoftErp.Operacion.Inventario.Informes
{
    public partial class MovimientosPorArticuloEspecial : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Articulo = 0;
        int Linea = 0;
        int Familia = 0;
        int Subfamilia = 0;
        int Emp = 0;

        public MovimientosPorArticuloEspecial()
        {
            InitializeComponent();

          

            splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
            CargaComboFamilias();
            CargaComboAlmacen();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        void bbiPrintPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl.ShowRibbonPrintPreview();
        }
       
    
        private void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
            bbiNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            ribbonPageGroup2.Visible=true;
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Reporte()
        {
            Articulo = Convert.ToInt32(cboArticulos.EditValue);
            Familia = Convert.ToInt32(cboFamilia.EditValue);
            Subfamilia = Convert.ToInt32(cboSubFamilias.EditValue);

            MovimientosporarticuloCL cl = new MovimientosporarticuloCL();
            cl.intFam = Familia;
            cl.intSubFam = Subfamilia;
            cl.intArtID = Articulo;
            cl.intAlmID= Convert.ToInt32(cboAlm.EditValue);
            cl.FechaIni = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
            cl.FechaFin = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
            gridControl.DataSource = cl.MovimientosporarticuloEspecialGrid();

            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "movsxartesp";
            clg.restoreLayout(gridView);

            gridView.OptionsView.ShowAutoFilterRow = true;
        }

        private void bbiRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
            bbiNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ribbonPageGroup2.Visible = false;
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

           
            cboFamilia.Properties.NullText = "";
            cboSubFamilias.Properties.NullText = "";
            cboArticulos.Properties.NullText = "";

        }

        private void CargaComboAlmacen()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            cl.strTabla = "Almacenes";
            cl.iCondicion = 0;
            src.DataSource = cl.CargaCombos();
            cboAlm.Properties.ValueMember = "Clave";
            cboAlm.Properties.DisplayMember = "Des";
            cboAlm.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAlm.Properties.ForceInitialize();
            cboAlm.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlm.Properties.PopulateColumns();
            cboAlm.Properties.Columns["Clave"].Visible = false;
            cboAlm.ItemIndex = 0;
        }
        private void CargaComboFamilias()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            cl.strTabla = "Familias";
            cl.iCondicion = 0;
            src.DataSource = cl.CargaCombos();
            cboFamilia.Properties.ValueMember = "Clave";
            cboFamilia.Properties.DisplayMember = "Des";
            cboFamilia.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboFamilia.Properties.ForceInitialize();
            cboFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilia.Properties.PopulateColumns();
            cboFamilia.Properties.Columns["Clave"].Visible = false;
            cboFamilia.ItemIndex = 0;



        }

        private void CargaComboSubfamilias()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            cl.strTabla = "SubFamiliasXFamilia";
            cl.iCondicion = Convert.ToInt32(cboFamilia.EditValue);
            src.DataSource = cl.CargaCombos();

            cboSubFamilias.Properties.ValueMember = "Clave";
            cboSubFamilias.Properties.DisplayMember = "Des";
            cboSubFamilias.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboSubFamilias.Properties.ForceInitialize();
            cboSubFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSubFamilias.Properties.PopulateColumns();
            cboSubFamilias.Properties.Columns["Clave"].Visible = false;
            cboSubFamilias.ItemIndex = 0;
        }

        private void CargaComboArticulos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            cl.strTabla = "ArticulosxSubFamilia";
            cl.intFam = Convert.ToInt32(cboFamilia.EditValue);
            cl.intSubFamilias = Convert.ToInt32(cboSubFamilias.EditValue);
            src.DataSource = cl.CargaCombos();

            cl.intSubFamilias = Convert.ToInt32(cboSubFamilias.EditValue);
            cboArticulos.Properties.ValueMember = "Clave";
            cboArticulos.Properties.DisplayMember = "Des";
            cboArticulos.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboArticulos.Properties.ForceInitialize();
            cboArticulos.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulos.Properties.PopulateColumns();
            try
            {
                cboArticulos.Properties.Columns["Clave"].Visible = false;
            }
            catch (Exception)
            {

            }

            cboArticulos.ItemIndex = 0;
        }

        private void cboFamilia_EditValueChanged(object sender, EventArgs e)
        {
            if (cboFamilia.EditValue == null) { }
            else { CargaComboSubfamilias(); }
        }

        private void cboSubFamilias_EditValueChanged(object sender, EventArgs e)
        {
            CargaComboArticulos();
        }

        private void bbiCerrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "movsxartesp" +
                "";
            string result = clg.SaveGridLayout(gridView);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }
    }
}