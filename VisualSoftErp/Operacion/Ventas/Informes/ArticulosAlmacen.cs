using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.Pdf.Native.BouncyCastle.Ocsp;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSpreadsheet.Mouse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Clases;
using VisualSoftErp.Interface.Request;
using VisualSoftErp.Operacion.Compras.Formas;
using VisualSoftErp.Operacion.Ventas.Clases;
using VisualSoftErp.Operacion.Ventas.Designers;

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class ArticulosAlmacen : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private int intPagina;
        public ArticulosAlmacen()
        {
            InitializeComponent();
        }

        private void ArticulosAlmacen_Load(object sender, EventArgs e)
        {
            intPagina = 1;
            if (ribbonControl.MergeOwner != null)
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            CargarCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargarCombos()
        {
            BindingSource src = new BindingSource();
            combosCL combos = new combosCL();
            globalCL global = new globalCL();

            #region LINEAS
            combos.strTabla = "Lineas";
            cboLineas.Properties.ValueMember = "Clave";
            cboLineas.Properties.DisplayMember = "Des";
            src.DataSource = combos.CargaCombos();
            cboLineas.Properties.DataSource = global.AgregarOpcionTodos(src);
            cboLineas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboLineas.Properties.ForceInitialize();
            cboLineas.Properties.PopulateColumns();
            cboLineas.Properties.Columns["Clave"].Visible = false;
            cboLineas.Properties.NullText = "Seleccione una Línea de Artículos";
            cboLineas.EditValue = null;
            #endregion LINEAS

            #region FAMILIAS
            CargarCombosFamilias();
            #endregion FAMILIAS

            #region SUBFAMILIAS
            CargarCombosSubFamilias();
            #endregion SUBFAMILIAS

            #region ALMACENES
            combos.strTabla = "Almacenes";
            cboAlmacenes.Properties.ValueMember = "Clave";
            cboAlmacenes.Properties.DisplayMember = "Des";
            src.DataSource = combos.CargaCombos();
            cboAlmacenes.Properties.DataSource = global.AgregarOpcionTodos(src);
            cboAlmacenes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacenes.Properties.ForceInitialize();
            cboAlmacenes.Properties.PopulateColumns();
            cboAlmacenes.Properties.Columns["Clave"].Visible = false;
            cboAlmacenes.Properties.NullText = "Seleccione un almacen";
            cboAlmacenes.EditValue = null;
            #endregion ALMACENES
        }

        private void CargarCombosFamilias()
        {
            if(cboLineas.EditValue != null)
            {
                combosCL combos = new combosCL();
                globalCL global = new globalCL();
                BindingSource src = new BindingSource();
                combos.strTabla = "FamiliasLineas";
                combos.iCondicion = Convert.ToInt32(cboLineas.EditValue);
                cboFamilias.Properties.ValueMember = "Clave";
                cboFamilias.Properties.DisplayMember = "Des";
                src.DataSource = combos.CargaCombos();
                cboFamilias.Properties.DataSource = global.AgregarOpcionTodos(src);
                cboFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboFamilias.Properties.ForceInitialize();
                cboFamilias.Properties.PopulateColumns();
                cboFamilias.Properties.Columns["Clave"].Visible = false;
                cboFamilias.Properties.NullText = "Seleccione un familia";
                cboFamilias.EditValue = null;
            }
        }

        private void CargarCombosSubFamilias()
        {
            if (cboFamilias.EditValue != null)
            {
                combosCL combos = new combosCL();
                globalCL global = new globalCL();
                BindingSource src = new BindingSource();
                combos.strTabla = "SubFamiliasXFamilia";
                combos.iCondicion = Convert.ToInt32(cboFamilias.EditValue);
                cboSubFamilias.Properties.ValueMember = "Clave";
                cboSubFamilias.Properties.DisplayMember = "Des";
                src.DataSource = combos.CargaCombos();
                cboSubFamilias.Properties.DataSource = global.AgregarOpcionTodos(src);
                cboSubFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboSubFamilias.Properties.ForceInitialize();
                cboSubFamilias.Properties.PopulateColumns();
                cboSubFamilias.Properties.Columns["Clave"].Visible = false;
                cboSubFamilias.Properties.NullText = "Seleccione una SubFamilias de Artículos";
                cboSubFamilias.EditValue = null;
            }
        }

        private string Validar()
        {
            string result = "OK";
            if (cboLineas.EditValue == null)
                result = "Favor de seleccionar una línea de artículos";

            if (cboFamilias.EditValue == null)
                result = "Favor de seleccionar una familia de artículos";

            if (cboSubFamilias.EditValue == null)
                result = "Favor de seleccionar una subfamilia de artículos";

            if (cboAlmacenes.EditValue == null)
                result = "Favor de seleccionar un almacen";

            if (radioGroupActivo.EditValue == null)
                result = "Favor de seleccionar si serán los artículos activos, inactivos o todos";

            return result;
        }

        private void Reporte()
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            try
            {
                ArticulosAlmacenDesigner rep = new ArticulosAlmacenDesigner();
                rep.Parameters["parameter1"].Value = Convert.ToInt32(radioGroupActivo.EditValue);
                rep.Parameters["parameter2"].Value = Convert.ToInt32(cboLineas.EditValue);
                rep.Parameters["parameter3"].Value = Convert.ToInt32(cboFamilias.EditValue);
                rep.Parameters["parameter4"].Value = Convert.ToInt32(cboSubFamilias.EditValue);

                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                int impDirecto = result == "OK" ? cl.iImpresiondirecta : 0;

                if (impDirecto == 1)
                {
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                }
                else
                {
                    rep.CreateDocument();
                    documentViewer1.DocumentSource = rep;
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImprimir.Text);
                    navigationFrameArticulosAlmacen.SelectedPage = navigationPageReporte;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Línea: " + ex.LineNumber() + "\n" + ex.Message, "Error al Cargar Reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            try
            {
                CrearIndicesPaginas();
                ArticulosAlmacenCL cl = new ArticulosAlmacenCL();
                cl.intActivos = Convert.ToInt32(radioGroupActivo.EditValue);
                cl.intLineasID = Convert.ToInt32(cboLineas.EditValue);
                cl.intFamiliasID = Convert.ToInt32(cboFamilias.EditValue);
                cl.intSubFamiliasID = Convert.ToInt32(cboSubFamilias.EditValue);
                cl.intAlmacenesID = Convert.ToInt32(cboAlmacenes.EditValue);
                cl.intPagina = intPagina;

                gridControlArticulosAlmacen.DataSource = cl.ArticulosAlmacenGRID();
                gridViewArticulosAlmacen.Columns["NomEmp"].Visible = false;

                if (navigationFrameArticulosAlmacen.SelectedPage != navigationPageGrid)
                    navigationFrameArticulosAlmacen.SelectedPage = navigationPageGrid;
            }
            catch ( Exception ex ) 
            {
                MessageBox.Show(ex.Message, "Error en Grid()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region CLICK_LINK
        private void navBarControlMenu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            intPagina = 1;
            NavBarItemLink item = e.Link;
            if (item != null)
            {
                switch (item.ItemName)
                {
                    case "navBarItemDatos":
                        if (ribbonControl.MergeOwner != null)
                            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
                        navigationFrameArticulosAlmacen.SelectedPage = navigationPageDatos;
                        break;
                    case "navBarItemReporte":
                        DialogResult msg = MessageBox.Show("¿Desea visualizar reporte para impresión?", "Precaución", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                        string result = Validar();
                        if (result != "OK")
                        {
                            MessageBox.Show(result, "Error al Cargar Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (msg == DialogResult.Cancel)
                            return;
                        if (msg == DialogResult.Yes)
                            Reporte();
                        if (msg == DialogResult.No)
                        {
                            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
                            LlenarGrid();
                            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        }
                        break;
                }
            }
        }

        private void officeNavigationBarMenu_ItemClick(object sender, NavigationBarItemEventArgs e)
        {
            intPagina = 1;
            OfficeNavigationBar officeNavigationBar = (OfficeNavigationBar)sender;
            if (officeNavigationBar != null)
            {
                NavigationBarItem item = officeNavigationBar.SelectedItem;
                switch (item.Name)
                {
                    case "navigationBarItemDatos":
                        if (ribbonControl.MergeOwner != null)
                            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
                        navigationFrameArticulosAlmacen.SelectedPage = navigationPageDatos;
                        break;
                    case "navigationBarItemReporte":

                        DialogResult msg = MessageBox.Show("¿Desea visualizar reporte para impresión?", "Precaución", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                        string result = Validar();
                        if (result != "OK")
                        {
                            MessageBox.Show(result, "Error al Cargar Reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (msg == DialogResult.Cancel)
                            return;
                        if (msg == DialogResult.Yes)
                            Reporte();
                        if (msg == DialogResult.No)
                        {
                            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
                            LlenarGrid();
                            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                        }

                        break;
                }
            }
        }

        private void bbiCerrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiVistaPrevia_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult msg = MessageBox.Show("¿Desea visualizar reporte para impresión?", "Precaución", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            string result = Validar();
            if (result != "OK")
            {
                MessageBox.Show(result, "Error al Cargar Reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (msg == DialogResult.Cancel)
                return;
            if (msg == DialogResult.Yes)
                Reporte();
            if (msg == DialogResult.No)
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
                LlenarGrid();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        private void bbiRegresar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ribbonControl.MergeOwner != null)
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrameArticulosAlmacen.SelectedPage = navigationPageDatos;
        }

        #endregion CLICK_LINK


        #region EDIT_VALUE_CHANGED
        private void cboLineas_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit combo = (LookUpEdit)sender;
            if (combo != null)
                CargarCombosFamilias();
        }

        private void cboFamilias_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit combo = (LookUpEdit)sender;
            if(combo != null)
                CargarCombosSubFamilias();
        }

        #endregion EDIT_VALUE_CHANGED


        #region PAGINADO

        private void bbiAntPagina_Click(object sender, EventArgs e)
        {
            if (intPagina > 1)
            {
                intPagina--;
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
                LlenarGrid();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        private void bbiSigPagina_Click(object sender, EventArgs e)
        {
            if(gridViewArticulosAlmacen.RowCount == 500)
            {
                intPagina++;
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
                LlenarGrid();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        private void CrearIndicesPaginas()
        {
            globalCL cl = new globalCL();
            cl.strTabla = "ArticulosAlmacenGRID";
            cl.intLineasID = Convert.ToInt32(cboLineas.EditValue);
            cl.intFamiliasID = Convert.ToInt32(cboFamilias.EditValue);
            cl.intSubFamiliasID = Convert.ToInt32(cboSubFamilias.EditValue);
            cl.intAlmacenesID = Convert.ToInt32(cboAlmacenes.EditValue);
            cl.intActivo = Convert.ToInt32(radioGroupActivo.EditValue);
            int intNumeroFilas = cl.NumeroFilasGrid();
            int itemsPerPage = 500;
            int totalPages = (int)Math.Ceiling(intNumeroFilas / (double)itemsPerPage);

            flowLayoutPanelPaginas.Controls.Clear();

            for (int i = 1; i <= totalPages; i++)
            {
                LinkLabel link = new LinkLabel();
                link.Text = i.ToString();
                link.Font = new Font(link.Font.FontFamily, 12);
                link.Tag = i;
                link.Click += new EventHandler(Pagina_Click);
                if (i == intPagina)
                    link.LinkColor = Color.Red;
                flowLayoutPanelPaginas.Controls.Add(link);
            }
        }

        private void Pagina_Click(object sender, EventArgs e )
        {
            LinkLabel link = (LinkLabel)sender;
            if (intPagina != (int)link.Tag)
            {
                intPagina = (int)link.Tag;
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
                LlenarGrid();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        #endregion PAGINADO







        // ESTA FUNCION SE UTILIZA EN MUCHAS PARTES, ES MEJOR METERLO A UNA CLASE Y MANDARLA A LLAMAR

        //private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        //{
        //    try
        //    {
        //        string VisualSoftErpConnectionString = globalCL.gv_strcnn;
        //        CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(VisualSoftErpConnectionString);

        //        if (e.ConnectionName == "VisualSoftErpConnectionString")
        //            e.ConnectionParameters = connectionParameters;
        //    }
        //    catch (Exception ex)
        //    {
        //        string x = ex.Message;
        //    }
        //}
    }
}