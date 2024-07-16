namespace VisualSoftErp.Operacion.Compras.Informes
{
    partial class CosteosArticulos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CosteosArticulos));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiGuardarMargenes = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageMargenes = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupAcciones = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.navBarControlMenu = new DevExpress.XtraNavBar.NavBarControl();
            this.NavBarGroupOpciones = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemMargenes = new DevExpress.XtraNavBar.NavBarItem();
            this.navigationFrameCosteoArticulos = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.navigationPageMargenes = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.gridControlMargenes = new DevExpress.XtraGrid.GridControl();
            this.gridViewMargenes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.officeNavigationBarMenu = new DevExpress.XtraBars.Navigation.OfficeNavigationBar();
            this.navigationBarItemMargenes = new DevExpress.XtraBars.Navigation.NavigationBarItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControlMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrameCosteoArticulos)).BeginInit();
            this.navigationFrameCosteoArticulos.SuspendLayout();
            this.navigationPageMargenes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMargenes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMargenes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.officeNavigationBarMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.ribbonControl.SearchEditItem,
            this.bbiGuardarMargenes});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 49;
            this.ribbonControl.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageMargenes});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.Size = new System.Drawing.Size(1169, 147);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // bbiGuardarMargenes
            // 
            this.bbiGuardarMargenes.Caption = "Guardar Margenes";
            this.bbiGuardarMargenes.Id = 48;
            this.bbiGuardarMargenes.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiGuardarMargenes.ImageOptions.SvgImage")));
            this.bbiGuardarMargenes.Name = "bbiGuardarMargenes";
            this.bbiGuardarMargenes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiGuardarMargenes_ItemClick);
            // 
            // ribbonPageMargenes
            // 
            this.ribbonPageMargenes.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupAcciones});
            this.ribbonPageMargenes.Name = "ribbonPageMargenes";
            this.ribbonPageMargenes.Text = "Margenes";
            // 
            // ribbonPageGroupAcciones
            // 
            this.ribbonPageGroupAcciones.ItemLinks.Add(this.bbiGuardarMargenes);
            this.ribbonPageGroupAcciones.Name = "ribbonPageGroupAcciones";
            this.ribbonPageGroupAcciones.Text = "Acciones";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 602);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1169, 23);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.IsSplitterFixed = true;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 147);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.navBarControlMenu);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.navigationFrameCosteoArticulos);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1169, 455);
            this.splitContainerControl1.SplitterPosition = 185;
            this.splitContainerControl1.TabIndex = 4;
            // 
            // navBarControlMenu
            // 
            this.navBarControlMenu.ActiveGroup = this.NavBarGroupOpciones;
            this.navBarControlMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControlMenu.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.NavBarGroupOpciones});
            this.navBarControlMenu.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItemMargenes});
            this.navBarControlMenu.Location = new System.Drawing.Point(0, 0);
            this.navBarControlMenu.Name = "navBarControlMenu";
            this.navBarControlMenu.OptionsNavPane.ExpandedWidth = 185;
            this.navBarControlMenu.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControlMenu.Size = new System.Drawing.Size(185, 455);
            this.navBarControlMenu.TabIndex = 0;
            this.navBarControlMenu.Text = "Menu";
            this.navBarControlMenu.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarControlMenu_LinkClicked);
            // 
            // NavBarGroupOpciones
            // 
            this.NavBarGroupOpciones.Caption = "Opciones";
            this.NavBarGroupOpciones.Expanded = true;
            this.NavBarGroupOpciones.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemMargenes)});
            this.NavBarGroupOpciones.Name = "NavBarGroupOpciones";
            // 
            // navBarItemMargenes
            // 
            this.navBarItemMargenes.Caption = "Tabla de Margenes";
            this.navBarItemMargenes.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItemMargenes.ImageOptions.SmallImage")));
            this.navBarItemMargenes.Name = "navBarItemMargenes";
            // 
            // navigationFrameCosteoArticulos
            // 
            this.navigationFrameCosteoArticulos.Controls.Add(this.navigationPageMargenes);
            this.navigationFrameCosteoArticulos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationFrameCosteoArticulos.Location = new System.Drawing.Point(0, 0);
            this.navigationFrameCosteoArticulos.Name = "navigationFrameCosteoArticulos";
            this.navigationFrameCosteoArticulos.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navigationPageMargenes});
            this.navigationFrameCosteoArticulos.SelectedPage = this.navigationPageMargenes;
            this.navigationFrameCosteoArticulos.Size = new System.Drawing.Size(972, 455);
            this.navigationFrameCosteoArticulos.TabIndex = 0;
            this.navigationFrameCosteoArticulos.Text = "CosteoArticulos";
            // 
            // navigationPageMargenes
            // 
            this.navigationPageMargenes.Controls.Add(this.gridControlMargenes);
            this.navigationPageMargenes.Name = "navigationPageMargenes";
            this.navigationPageMargenes.Size = new System.Drawing.Size(972, 455);
            // 
            // gridControlMargenes
            // 
            this.gridControlMargenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlMargenes.Location = new System.Drawing.Point(0, 0);
            this.gridControlMargenes.MainView = this.gridViewMargenes;
            this.gridControlMargenes.MenuManager = this.ribbonControl;
            this.gridControlMargenes.Name = "gridControlMargenes";
            this.gridControlMargenes.Size = new System.Drawing.Size(972, 455);
            this.gridControlMargenes.TabIndex = 0;
            this.gridControlMargenes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMargenes});
            // 
            // gridViewMargenes
            // 
            this.gridViewMargenes.GridControl = this.gridControlMargenes;
            this.gridViewMargenes.Name = "gridViewMargenes";
            // 
            // officeNavigationBarMenu
            // 
            this.officeNavigationBarMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.officeNavigationBarMenu.Items.AddRange(new DevExpress.XtraBars.Navigation.NavigationBarItem[] {
            this.navigationBarItemMargenes});
            this.officeNavigationBarMenu.Location = new System.Drawing.Point(0, 556);
            this.officeNavigationBarMenu.Name = "officeNavigationBarMenu";
            this.officeNavigationBarMenu.Size = new System.Drawing.Size(1169, 46);
            this.officeNavigationBarMenu.TabIndex = 7;
            this.officeNavigationBarMenu.Text = "Menu";
            this.officeNavigationBarMenu.ItemClick += new DevExpress.XtraBars.Navigation.NavigationBarItemClickEventHandler(this.officeNavigationBarMenu_ItemClick);
            // 
            // navigationBarItemMargenes
            // 
            this.navigationBarItemMargenes.Name = "navigationBarItemMargenes";
            this.navigationBarItemMargenes.Text = "Tabla de Margenes";
            // 
            // CosteosArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 625);
            this.Controls.Add(this.officeNavigationBarMenu);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonControl);
            this.Name = "CosteosArticulos";
            this.Ribbon = this.ribbonControl;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Costeo Articulos";
            this.Load += new System.EventHandler(this.CosteosArticulos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControlMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrameCosteoArticulos)).EndInit();
            this.navigationFrameCosteoArticulos.ResumeLayout(false);
            this.navigationPageMargenes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMargenes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMargenes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.officeNavigationBarMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageMargenes;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupAcciones;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraNavBar.NavBarControl navBarControlMenu;
        private DevExpress.XtraNavBar.NavBarGroup NavBarGroupOpciones;
        private DevExpress.XtraNavBar.NavBarItem navBarItemMargenes;
        private DevExpress.XtraBars.Navigation.NavigationFrame navigationFrameCosteoArticulos;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPageMargenes;
        private DevExpress.XtraGrid.GridControl gridControlMargenes;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMargenes;
        private DevExpress.XtraBars.BarButtonItem bbiGuardarMargenes;
        private DevExpress.XtraBars.Navigation.OfficeNavigationBar officeNavigationBarMenu;
        private DevExpress.XtraBars.Navigation.NavigationBarItem navigationBarItemMargenes;
    }
}