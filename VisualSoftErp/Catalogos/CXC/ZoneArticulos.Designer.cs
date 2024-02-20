namespace VisualSoftErp.Operacion.Ventas.Formas
{
    partial class ZoneArticulos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZoneArticulos));
            this.tabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.skinRibbonGalleryBarItem = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
            this.barSubItemNavigation = new DevExpress.XtraBars.BarSubItem();
            this.employeesBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.customersBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.bbiGuardar = new DevExpress.XtraBars.BarButtonItem();
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.txtAlt_Part_Number = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboArticulosID = new DevExpress.XtraEditors.LookUpEdit();
            this.txtPart_Number = new DevExpress.XtraEditors.TextEdit();
            this.txtSKU = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.officeNavigationBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame)).BeginInit();
            this.navigationFrame.SuspendLayout();
            this.employeesNavigationPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAlt_Part_Number.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboArticulosID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPart_Number.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSKU.Properties)).BeginInit();
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
            this.bbiGuardar,
            this.bbiCerrar});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 48;
            this.ribbonControl.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage,
            this.ribbonPage1});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.Size = new System.Drawing.Size(1085, 178);
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
            // bbiGuardar
            // 
            this.bbiGuardar.Caption = "Guardar";
            this.bbiGuardar.Id = 46;
            this.bbiGuardar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiGuardar.ImageOptions.SvgImage")));
            this.bbiGuardar.Name = "bbiGuardar";
            this.bbiGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiGuardar_ItemClick);
            // 
            // bbiCerrar
            // 
            this.bbiCerrar.Caption = "Cerrar";
            this.bbiCerrar.Id = 47;
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
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiGuardar);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiCerrar, true);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Acciones";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 570);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1085, 29);
            // 
            // officeNavigationBar
            // 
            this.officeNavigationBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.officeNavigationBar.Location = new System.Drawing.Point(0, 524);
            this.officeNavigationBar.Name = "officeNavigationBar";
            this.officeNavigationBar.Size = new System.Drawing.Size(1085, 46);
            this.officeNavigationBar.TabIndex = 1;
            this.officeNavigationBar.Text = "officeNavigationBar";
            // 
            // navigationFrame
            // 
            this.navigationFrame.Appearance.BackColor = System.Drawing.Color.White;
            this.navigationFrame.Appearance.Options.UseBackColor = true;
            this.navigationFrame.Controls.Add(this.employeesNavigationPage);
            this.navigationFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationFrame.Location = new System.Drawing.Point(0, 178);
            this.navigationFrame.Name = "navigationFrame";
            this.navigationFrame.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.employeesNavigationPage});
            this.navigationFrame.RibbonAndBarsMergeStyle = DevExpress.XtraBars.Docking2010.Views.RibbonAndBarsMergeStyle.Always;
            this.navigationFrame.SelectedPage = this.employeesNavigationPage;
            this.navigationFrame.Size = new System.Drawing.Size(1085, 346);
            this.navigationFrame.TabIndex = 0;
            this.navigationFrame.Text = "navigationFrame";
            // 
            // employeesNavigationPage
            // 
            this.employeesNavigationPage.Controls.Add(this.labelControl4);
            this.employeesNavigationPage.Controls.Add(this.txtDescription);
            this.employeesNavigationPage.Controls.Add(this.txtAlt_Part_Number);
            this.employeesNavigationPage.Controls.Add(this.labelControl1);
            this.employeesNavigationPage.Controls.Add(this.labelControl2);
            this.employeesNavigationPage.Controls.Add(this.cboArticulosID);
            this.employeesNavigationPage.Controls.Add(this.txtPart_Number);
            this.employeesNavigationPage.Controls.Add(this.txtSKU);
            this.employeesNavigationPage.Controls.Add(this.labelControl17);
            this.employeesNavigationPage.Controls.Add(this.labelControl3);
            this.employeesNavigationPage.Name = "employeesNavigationPage";
            this.employeesNavigationPage.Size = new System.Drawing.Size(1085, 346);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(30, 56);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(41, 13);
            this.labelControl4.TabIndex = 90;
            this.labelControl4.Text = "Articulos";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(171, 184);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(796, 22);
            this.txtDescription.TabIndex = 89;
            // 
            // txtAlt_Part_Number
            // 
            this.txtAlt_Part_Number.Location = new System.Drawing.Point(171, 151);
            this.txtAlt_Part_Number.Name = "txtAlt_Part_Number";
            this.txtAlt_Part_Number.Size = new System.Drawing.Size(302, 22);
            this.txtAlt_Part_Number.TabIndex = 88;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 187);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 87;
            this.labelControl1.Text = "Description";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 154);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 13);
            this.labelControl2.TabIndex = 86;
            this.labelControl2.Text = "Alt_Part_Number";
            // 
            // cboArticulosID
            // 
            this.cboArticulosID.Location = new System.Drawing.Point(171, 53);
            this.cboArticulosID.MenuManager = this.ribbonControl;
            this.cboArticulosID.Name = "cboArticulosID";
            this.cboArticulosID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboArticulosID.Size = new System.Drawing.Size(796, 22);
            this.cboArticulosID.TabIndex = 85;
            this.cboArticulosID.EditValueChanged += new System.EventHandler(this.cboArticulosID_EditValueChanged);
            // 
            // txtPart_Number
            // 
            this.txtPart_Number.Location = new System.Drawing.Point(171, 118);
            this.txtPart_Number.Name = "txtPart_Number";
            this.txtPart_Number.Size = new System.Drawing.Size(302, 22);
            this.txtPart_Number.TabIndex = 84;
            // 
            // txtSKU
            // 
            this.txtSKU.Location = new System.Drawing.Point(171, 85);
            this.txtSKU.Name = "txtSKU";
            this.txtSKU.Size = new System.Drawing.Size(302, 22);
            this.txtSKU.TabIndex = 83;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(30, 121);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(63, 13);
            this.labelControl17.TabIndex = 82;
            this.labelControl17.Text = "Part_Number";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 88);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(19, 13);
            this.labelControl3.TabIndex = 81;
            this.labelControl3.Text = "SKU";
            // 
            // ZoneArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 599);
            this.Controls.Add(this.navigationFrame);
            this.Controls.Add(this.officeNavigationBar);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonControl);
            this.Name = "ZoneArticulos";
            this.Ribbon = this.ribbonControl;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "ZoneArticulos";
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.officeNavigationBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame)).EndInit();
            this.navigationFrame.ResumeLayout(false);
            this.employeesNavigationPage.ResumeLayout(false);
            this.employeesNavigationPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAlt_Part_Number.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboArticulosID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPart_Number.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSKU.Properties)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem employeesBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem customersBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem bbiGuardar;
        private DevExpress.XtraBars.BarButtonItem bbiCerrar;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.TextEdit txtAlt_Part_Number;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit cboArticulosID;
        private DevExpress.XtraEditors.TextEdit txtPart_Number;
        private DevExpress.XtraEditors.TextEdit txtSKU;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}