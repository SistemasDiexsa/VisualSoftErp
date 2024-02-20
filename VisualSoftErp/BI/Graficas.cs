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

namespace VisualSoftErp.BI
{
    public partial class Graficas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Graficas()
        {
            InitializeComponent();
            cargaCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
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

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ventasPorClientes();
        }

        private void ventasPorClientes()
        {
            DSVentasporcliente.Queries[0].Parameters[0].Value = Convert.ToInt32(cboAños.EditValue);
            DSVentasporcliente.Queries[0].Parameters[0].Type = Type.GetType("System.Int32");
            DSVentasporcliente.Fill();
            navBarControl.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;
            chartControl1.Titles[0].Text = "VENTAS POR CLIENTE (" + cboAños.EditValue.ToString() + ")";
        }

        private void cargaCombos()
        {
            combosCL cl = new combosCL();
            BindingSource src = new BindingSource();
            cboAños.Properties.ValueMember = "Clave";
            cboAños.Properties.DisplayMember = "Des";
            cl.strTabla = "Añosventas";
            src.DataSource = cl.CargaCombos();
            cboAños.Properties.DataSource = src.DataSource;
            cboAños.Properties.ForceInitialize();
            cboAños.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAños.Properties.ForceInitialize();
            cboAños.Properties.PopulateColumns();
            cboAños.Properties.Columns["Clave"].Visible = false;
            cboAños.ItemIndex = 0;

            ventasPorClientes();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}