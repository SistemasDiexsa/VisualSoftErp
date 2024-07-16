using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
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
using VisualSoftErp.Operacion.Compras.Clases;

namespace VisualSoftErp.Operacion.Compras.Informes
{
    public partial class CosteosArticulos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private bool permisosEscritura;
        private DataTable ArticulosMargenes { get; set; } = new DataTable
        {
            Columns =
            {
                new DataColumn("ArticulosID", typeof(int)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Costo", typeof(decimal)),
                new DataColumn("Porcentaje", typeof(decimal)),
                new DataColumn("Margen", typeof(int))
            }
        };
        public CosteosArticulos()
        {
            InitializeComponent();
        }

        private void CosteosArticulos_Load(object sender, EventArgs e)
        {
            PermisosEscritura();
            InitGridControlMargenes();
            if (ribbonControl.MergeOwner != null)
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageMargenes.Text);
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void PermisosEscritura()
        {
            globalCL clg = new globalCL();
            clg.strPrograma = "0140";
            if (clg.accesoSoloLectura())
            {
                bbiGuardarMargenes.Enabled = false;
                permisosEscritura = false;
            }
            else
            {
                bbiGuardarMargenes.Enabled = true;
                permisosEscritura = true;
            }
        }

        private void InitGridControlMargenes()
        {
            CosteosArticulosCL cl = new CosteosArticulosCL();
            ArticulosMargenes = cl.CosteoArticulosGrid();
            gridControlMargenes.DataSource = ArticulosMargenes;

            gridViewMargenes.ViewCaption = "Margenes de Artículos";
            gridViewMargenes.OptionsBehavior.ReadOnly = false;
            gridViewMargenes.OptionsBehavior.Editable = true;
            gridViewMargenes.Columns["ArticulosID"].OptionsColumn.AllowEdit = false;
            gridViewMargenes.Columns["Nombre"].OptionsColumn.AllowEdit = false;
            gridViewMargenes.Columns["Costo"].OptionsColumn.AllowEdit = false;
        }

        private void navBarControlMenu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NavBarItemLink item = e.Link;
            if (item != null)
            {
                switch (item.ItemName)
                {
                    case "navBarItemMargenes":
                        ribbonPageMargenes.Visible = true;
                        navigationFrameCosteoArticulos.SelectedPage = navigationPageMargenes;
                        break;
                }
            }
        }

        private void officeNavigationBarMenu_ItemClick(object sender, NavigationBarItemEventArgs e)
        {
            OfficeNavigationBar menu = (OfficeNavigationBar)sender;
            if(menu != null)
            {
                NavigationBarItem item = menu.SelectedItem;
                switch(item.Name)
                {
                    case "navigationBarItemMargenes":
                        ribbonPageMargenes.Visible = true;
                        navigationFrameCosteoArticulos.SelectedPage = navigationPageMargenes;
                        break;
                }
            }
        }

        private void bbiGuardarMargenes_ItemClick(object sender, ItemClickEventArgs e)
        {
            string result = string.Empty;
            try
            {
                CosteosArticulosCL cl = new CosteosArticulosCL();
                cl.ArticulosMargenes = ArticulosMargenes;

                result = cl.CosteoArticulosCrud();
                if (result != "OK")
                {
                    MessageBox.Show(result, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Guardado Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                result = "Error en línea " + ex.LineNumber() + "\n\n" + ex.Message;
                MessageBox.Show(result, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}