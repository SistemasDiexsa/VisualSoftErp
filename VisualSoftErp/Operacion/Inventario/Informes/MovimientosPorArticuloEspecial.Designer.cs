namespace VisualSoftErp.Operacion.Inventario.Informes
{
    partial class MovimientosPorArticuloEspecial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MovimientosPorArticuloEspecial));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiPrintPreview = new DevExpress.XtraBars.BarButtonItem();
            this.bsiRecordsCount = new DevExpress.XtraBars.BarStaticItem();
            this.bbiNew = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEdit = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.vsFiltroFechas1 = new VSFiltroFechas.vsFiltroFechas();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cboArticulos = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.cboSubFamilias = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cboFamilia = new DevExpress.XtraEditors.LookUpEdit();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboAlm = new DevExpress.XtraEditors.LookUpEdit();
            this.bbiCerrar = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboArticulos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubFamilias.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFamilia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAlm.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.ribbonControl.SearchEditItem,
            this.bbiPrintPreview,
            this.bsiRecordsCount,
            this.bbiNew,
            this.bbiEdit,
            this.bbiDelete,
            this.bbiRefresh,
            this.bbiCerrar});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 21;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.Size = new System.Drawing.Size(1364, 147);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // bbiPrintPreview
            // 
            this.bbiPrintPreview.Caption = "Print Preview";
            this.bbiPrintPreview.Id = 14;
            this.bbiPrintPreview.ImageOptions.ImageUri.Uri = "Preview";
            this.bbiPrintPreview.Name = "bbiPrintPreview";
            this.bbiPrintPreview.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPrintPreview_ItemClick);
            // 
            // bsiRecordsCount
            // 
            this.bsiRecordsCount.Caption = "RECORDS : 0";
            this.bsiRecordsCount.Id = 15;
            this.bsiRecordsCount.Name = "bsiRecordsCount";
            // 
            // bbiNew
            // 
            this.bbiNew.Caption = "Procesar";
            this.bbiNew.Id = 16;
            this.bbiNew.ImageOptions.ImageUri.Uri = "New";
            this.bbiNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiNew.ImageOptions.SvgImage")));
            this.bbiNew.Name = "bbiNew";
            this.bbiNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNew_ItemClick);
            // 
            // bbiEdit
            // 
            this.bbiEdit.Caption = "Edit";
            this.bbiEdit.Id = 17;
            this.bbiEdit.ImageOptions.ImageUri.Uri = "Edit";
            this.bbiEdit.Name = "bbiEdit";
            // 
            // bbiDelete
            // 
            this.bbiDelete.Caption = "Delete";
            this.bbiDelete.Id = 18;
            this.bbiDelete.ImageOptions.ImageUri.Uri = "Delete";
            this.bbiDelete.Name = "bbiDelete";
            // 
            // bbiRefresh
            // 
            this.bbiRefresh.Caption = "Datos";
            this.bbiRefresh.Id = 19;
            this.bbiRefresh.ImageOptions.ImageUri.Uri = "Refresh";
            this.bbiRefresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiRefresh.ImageOptions.SvgImage")));
            this.bbiRefresh.Name = "bbiRefresh";
            this.bbiRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRefresh_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.MergeOrder = 0;
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Home";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiNew);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiRefresh);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiCerrar);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Tasks";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.AllowTextClipping = false;
            this.ribbonPageGroup2.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiPrintPreview);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Print and Export";
            this.ribbonPageGroup2.Visible = false;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.bsiRecordsCount);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 576);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1364, 23);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 147);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.cboAlm);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl2);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl1);
            this.splitContainerControl1.Panel1.Controls.Add(this.vsFiltroFechas1);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl5);
            this.splitContainerControl1.Panel1.Controls.Add(this.cboArticulos);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl6);
            this.splitContainerControl1.Panel1.Controls.Add(this.cboSubFamilias);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl4);
            this.splitContainerControl1.Panel1.Controls.Add(this.cboFamilia);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1364, 429);
            this.splitContainerControl1.SplitterPosition = 206;
            this.splitContainerControl1.TabIndex = 4;
            // 
            // vsFiltroFechas1
            // 
            this.vsFiltroFechas1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.vsFiltroFechas1.Appearance.Options.UseBackColor = true;
            this.vsFiltroFechas1.Location = new System.Drawing.Point(467, 7);
            this.vsFiltroFechas1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.vsFiltroFechas1.Name = "vsFiltroFechas1";
            this.vsFiltroFechas1.Size = new System.Drawing.Size(255, 196);
            this.vsFiltroFechas1.TabIndex = 21;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 99);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 13);
            this.labelControl5.TabIndex = 20;
            this.labelControl5.Text = "Articulo";
            // 
            // cboArticulos
            // 
            this.cboArticulos.Location = new System.Drawing.Point(12, 118);
            this.cboArticulos.MenuManager = this.ribbonControl;
            this.cboArticulos.Name = "cboArticulos";
            this.cboArticulos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboArticulos.Size = new System.Drawing.Size(440, 20);
            this.cboArticulos.TabIndex = 19;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 54);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(51, 13);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "Sub familia";
            // 
            // cboSubFamilias
            // 
            this.cboSubFamilias.Location = new System.Drawing.Point(12, 73);
            this.cboSubFamilias.MenuManager = this.ribbonControl;
            this.cboSubFamilias.Name = "cboSubFamilias";
            this.cboSubFamilias.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubFamilias.Size = new System.Drawing.Size(440, 20);
            this.cboSubFamilias.TabIndex = 17;
            this.cboSubFamilias.EditValueChanged += new System.EventHandler(this.cboSubFamilias_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(32, 13);
            this.labelControl4.TabIndex = 14;
            this.labelControl4.Text = "Familia";
            // 
            // cboFamilia
            // 
            this.cboFamilia.Location = new System.Drawing.Point(12, 25);
            this.cboFamilia.MenuManager = this.ribbonControl;
            this.cboFamilia.Name = "cboFamilia";
            this.cboFamilia.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFamilia.Size = new System.Drawing.Size(440, 20);
            this.cboFamilia.TabIndex = 13;
            this.cboFamilia.EditValueChanged += new System.EventHandler(this.cboFamilia_EditValueChanged);
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.MenuManager = this.ribbonControl;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(1364, 211);
            this.gridControl.TabIndex = 3;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsBehavior.ReadOnly = true;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(664, 97);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 13);
            this.labelControl1.TabIndex = 22;
            this.labelControl1.Text = "Articulo";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 145);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 13);
            this.labelControl2.TabIndex = 23;
            this.labelControl2.Text = "Almacén";
            // 
            // cboAlm
            // 
            this.cboAlm.Location = new System.Drawing.Point(13, 164);
            this.cboAlm.MenuManager = this.ribbonControl;
            this.cboAlm.Name = "cboAlm";
            this.cboAlm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboAlm.Size = new System.Drawing.Size(440, 20);
            this.cboAlm.TabIndex = 24;
            // 
            // bbiCerrar
            // 
            this.bbiCerrar.Caption = "Cerrar";
            this.bbiCerrar.Id = 20;
            this.bbiCerrar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiCerrar.ImageOptions.SvgImage")));
            this.bbiCerrar.Name = "bbiCerrar";
            this.bbiCerrar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCerrar_ItemClick);
            // 
            // MovimientosPorArticuloEspecial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 599);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonControl);
            this.Name = "MovimientosPorArticuloEspecial";
            this.Ribbon = this.ribbonControl;
            this.StatusBar = this.ribbonStatusBar;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            this.splitContainerControl1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboArticulos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubFamilias.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFamilia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAlm.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem bbiPrintPreview;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarStaticItem bsiRecordsCount;
        private DevExpress.XtraBars.BarButtonItem bbiNew;
        private DevExpress.XtraBars.BarButtonItem bbiEdit;
        private DevExpress.XtraBars.BarButtonItem bbiDelete;
        private DevExpress.XtraBars.BarButtonItem bbiRefresh;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit cboFamilia;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LookUpEdit cboSubFamilias;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit cboArticulos;
        private VSFiltroFechas.vsFiltroFechas vsFiltroFechas1;
        private DevExpress.XtraEditors.LookUpEdit cboAlm;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.BarButtonItem bbiCerrar;
    }
}