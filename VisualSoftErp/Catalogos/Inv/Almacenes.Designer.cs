namespace VisualSoftErp.Catalogos
{
    partial class Almacenes
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Almacenes));
            this.tabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.skinRibbonGalleryBarItem = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
            this.barSubItemNavigation = new DevExpress.XtraBars.BarSubItem();
            this.employeesBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.customersBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.bbiNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEditar = new DevExpress.XtraBars.BarButtonItem();
            this.bbiGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRegresar = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.bbiVista = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCerrar = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupNavigation = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.officeNavigationBar = new DevExpress.XtraBars.Navigation.OfficeNavigationBar();
            this.navigationFrame = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.employeesNavigationPage = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.grdAlmacen = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.customersNavigationPage = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.swActivo = new DevExpress.XtraEditors.ToggleSwitch();
            this.txtNombre = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.officeNavigationBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame)).BeginInit();
            this.navigationFrame.SuspendLayout();
            this.employeesNavigationPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAlmacen)).BeginInit();
            this.customersNavigationPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.swActivo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.ribbonControl.SearchEditItem,
            this.skinRibbonGalleryBarItem,
            this.barSubItemNavigation,
            this.employeesBarButtonItem,
            this.customersBarButtonItem,
            this.bbiNuevo,
            this.bbiEditar,
            this.bbiGuardar,
            this.bbiRegresar,
            this.bbiEliminar,
            this.bbiVista,
            this.bbiCerrar});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 53;
            this.ribbonControl.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage,
            this.ribbonPage1});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.Size = new System.Drawing.Size(790, 143);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // skinRibbonGalleryBarItem
            // 
            this.skinRibbonGalleryBarItem.Id = 14;
            this.skinRibbonGalleryBarItem.Name = "skinRibbonGalleryBarItem";
            // 
            // barSubItemNavigation
            // 
            this.barSubItemNavigation.Caption = "Navigation";
            this.barSubItemNavigation.Id = 15;
            this.barSubItemNavigation.ImageOptions.ImageUri.Uri = "NavigationBar";
            this.barSubItemNavigation.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.employeesBarButtonItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.customersBarButtonItem)});
            this.barSubItemNavigation.Name = "barSubItemNavigation";
            // 
            // employeesBarButtonItem
            // 
            this.employeesBarButtonItem.Caption = "Employees";
            this.employeesBarButtonItem.Id = 44;
            this.employeesBarButtonItem.Name = "employeesBarButtonItem";
            // 
            // customersBarButtonItem
            // 
            this.customersBarButtonItem.Caption = "Customers";
            this.customersBarButtonItem.Id = 45;
            this.customersBarButtonItem.Name = "customersBarButtonItem";
            // 
            // bbiNuevo
            // 
            this.bbiNuevo.Caption = "Nuevo";
            this.bbiNuevo.Id = 46;
            this.bbiNuevo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiNuevo.ImageOptions.Image")));
            this.bbiNuevo.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiNuevo.ImageOptions.LargeImage")));
            this.bbiNuevo.Name = "bbiNuevo";
            this.bbiNuevo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNuevo_ItemClick);
            // 
            // bbiEditar
            // 
            this.bbiEditar.Caption = "Editar";
            this.bbiEditar.Id = 47;
            this.bbiEditar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiEditar.ImageOptions.Image")));
            this.bbiEditar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiEditar.ImageOptions.LargeImage")));
            this.bbiEditar.Name = "bbiEditar";
            this.bbiEditar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEditar_ItemClick);
            // 
            // bbiGuardar
            // 
            this.bbiGuardar.Caption = "Guardar";
            this.bbiGuardar.Id = 48;
            this.bbiGuardar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiGuardar.ImageOptions.SvgImage")));
            this.bbiGuardar.Name = "bbiGuardar";
            this.bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiGuardar_ItemClick);
            // 
            // bbiRegresar
            // 
            this.bbiRegresar.Caption = "Regresar";
            this.bbiRegresar.Id = 49;
            this.bbiRegresar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiRegresar.ImageOptions.Image")));
            this.bbiRegresar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiRegresar.ImageOptions.LargeImage")));
            this.bbiRegresar.Name = "bbiRegresar";
            this.bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiRegresar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRegresar_ItemClick);
            // 
            // bbiEliminar
            // 
            this.bbiEliminar.Caption = "Eliminar";
            this.bbiEliminar.Id = 50;
            this.bbiEliminar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiEliminar.ImageOptions.Image")));
            this.bbiEliminar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiEliminar.ImageOptions.LargeImage")));
            this.bbiEliminar.Name = "bbiEliminar";
            this.bbiEliminar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEliminar_ItemClick);
            // 
            // bbiVista
            // 
            this.bbiVista.Caption = "Vista Previa";
            this.bbiVista.Id = 51;
            this.bbiVista.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiVista.ImageOptions.SvgImage")));
            this.bbiVista.Name = "bbiVista";
            this.bbiVista.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiVista_ItemClick);
            // 
            // bbiCerrar
            // 
            this.bbiCerrar.Caption = "Cerrar";
            this.bbiCerrar.Id = 52;
            this.bbiCerrar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiCerrar.ImageOptions.Image")));
            this.bbiCerrar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiCerrar.ImageOptions.LargeImage")));
            this.bbiCerrar.Name = "bbiCerrar";
            this.bbiCerrar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCerrar_ItemClick);
            // 
            // ribbonPage
            // 
            this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupNavigation,
            this.ribbonPageGroup});
            this.ribbonPage.Name = "ribbonPage";
            this.ribbonPage.Text = "View";
            // 
            // ribbonPageGroupNavigation
            // 
            this.ribbonPageGroupNavigation.ItemLinks.Add(this.barSubItemNavigation);
            this.ribbonPageGroupNavigation.Name = "ribbonPageGroupNavigation";
            this.ribbonPageGroupNavigation.Text = "Module";
            // 
            // ribbonPageGroup
            // 
            this.ribbonPageGroup.AllowTextClipping = false;
            this.ribbonPageGroup.ItemLinks.Add(this.skinRibbonGalleryBarItem);
            this.ribbonPageGroup.Name = "ribbonPageGroup";
            this.ribbonPageGroup.ShowCaptionButton = false;
            this.ribbonPageGroup.Text = "Appearance";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Home";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiNuevo);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiEditar);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiGuardar);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiRegresar);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiEliminar);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiVista);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiCerrar);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Acciones";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 568);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(790, 31);
            // 
            // officeNavigationBar
            // 
            this.officeNavigationBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.officeNavigationBar.Location = new System.Drawing.Point(0, 522);
            this.officeNavigationBar.Name = "officeNavigationBar";
            this.officeNavigationBar.Size = new System.Drawing.Size(790, 46);
            this.officeNavigationBar.TabIndex = 1;
            this.officeNavigationBar.Text = "officeNavigationBar";
            // 
            // navigationFrame
            // 
            this.navigationFrame.Appearance.BackColor = System.Drawing.Color.White;
            this.navigationFrame.Appearance.Options.UseBackColor = true;
            this.navigationFrame.Controls.Add(this.employeesNavigationPage);
            this.navigationFrame.Controls.Add(this.customersNavigationPage);
            this.navigationFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationFrame.Location = new System.Drawing.Point(0, 143);
            this.navigationFrame.Name = "navigationFrame";
            this.navigationFrame.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.employeesNavigationPage,
            this.customersNavigationPage});
            this.navigationFrame.RibbonAndBarsMergeStyle = DevExpress.XtraBars.Docking2010.Views.RibbonAndBarsMergeStyle.Always;
            this.navigationFrame.SelectedPage = this.employeesNavigationPage;
            this.navigationFrame.Size = new System.Drawing.Size(790, 379);
            this.navigationFrame.TabIndex = 0;
            this.navigationFrame.Text = "navigationFrame";
            // 
            // employeesNavigationPage
            // 
            this.employeesNavigationPage.Controls.Add(this.gridControl1);
            this.employeesNavigationPage.Name = "employeesNavigationPage";
            this.employeesNavigationPage.Size = new System.Drawing.Size(790, 379);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.grdAlmacen;
            this.gridControl1.MenuManager = this.ribbonControl;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(790, 379);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdAlmacen});
            // 
            // grdAlmacen
            // 
            this.grdAlmacen.GridControl = this.gridControl1;
            this.grdAlmacen.Name = "grdAlmacen";
            this.grdAlmacen.OptionsView.ShowGroupPanel = false;
            this.grdAlmacen.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.grdAlmacen_RowClick);
            // 
            // customersNavigationPage
            // 
            this.customersNavigationPage.Controls.Add(this.labelControl2);
            this.customersNavigationPage.Controls.Add(this.swActivo);
            this.customersNavigationPage.Controls.Add(this.txtNombre);
            this.customersNavigationPage.Controls.Add(this.labelControl1);
            this.customersNavigationPage.Name = "customersNavigationPage";
            this.customersNavigationPage.Size = new System.Drawing.Size(790, 379);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(39, 112);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(30, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Activo";
            // 
            // swActivo
            // 
            this.swActivo.EnterMoveNextControl = true;
            this.swActivo.Location = new System.Drawing.Point(110, 107);
            this.swActivo.MenuManager = this.ribbonControl;
            this.swActivo.Name = "swActivo";
            this.swActivo.Properties.OffText = "No";
            this.swActivo.Properties.OnText = "Sí";
            this.swActivo.Size = new System.Drawing.Size(139, 24);
            this.swActivo.TabIndex = 1;
            // 
            // txtNombre
            // 
            this.txtNombre.EnterMoveNextControl = true;
            this.txtNombre.Location = new System.Drawing.Point(110, 48);
            this.txtNombre.MenuManager = this.ribbonControl;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(139, 20);
            this.txtNombre.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(39, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Nombre";
            // 
            // Almacenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.navigationFrame);
            this.Controls.Add(this.officeNavigationBar);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonControl);
            this.Name = "Almacenes";
            this.Ribbon = this.ribbonControl;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Catálogo de Almacenes";
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.officeNavigationBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame)).EndInit();
            this.navigationFrame.ResumeLayout(false);
            this.employeesNavigationPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAlmacen)).EndInit();
            this.customersNavigationPage.ResumeLayout(false);
            this.customersNavigationPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.swActivo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupNavigation;
        private DevExpress.XtraBars.BarSubItem barSubItemNavigation;
        private DevExpress.XtraBars.SkinRibbonGalleryBarItem skinRibbonGalleryBarItem;
        private DevExpress.XtraBars.Navigation.OfficeNavigationBar officeNavigationBar;
        private DevExpress.XtraBars.Navigation.NavigationFrame navigationFrame;
        private DevExpress.XtraBars.Navigation.NavigationPage employeesNavigationPage;
        private DevExpress.XtraBars.Navigation.NavigationPage customersNavigationPage;
        private DevExpress.XtraBars.BarButtonItem employeesBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem customersBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem bbiNuevo;
        private DevExpress.XtraBars.BarButtonItem bbiEditar;
        private DevExpress.XtraBars.BarButtonItem bbiGuardar;
        private DevExpress.XtraBars.BarButtonItem bbiRegresar;
        private DevExpress.XtraBars.BarButtonItem bbiEliminar;
        private DevExpress.XtraBars.BarButtonItem bbiVista;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem bbiCerrar;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView grdAlmacen;
        private DevExpress.XtraEditors.TextEdit txtNombre;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ToggleSwitch swActivo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}