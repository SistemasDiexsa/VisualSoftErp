namespace VisualSoftErp.Operacion.Inventario.Formas
{
    partial class Ciclicos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ciclicos));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageHome = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDiferencia = new DevExpress.XtraEditors.LabelControl();
            this.labelDiferencia = new DevExpress.XtraEditors.LabelControl();
            this.lblExistencia = new DevExpress.XtraEditors.LabelControl();
            this.labelExistencia = new DevExpress.XtraEditors.LabelControl();
            this.txtConteo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboArticulos = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConteo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboArticulos.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.bbiGuardar});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 2;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageHome});
            this.ribbon.Size = new System.Drawing.Size(973, 147);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // bbiGuardar
            // 
            this.bbiGuardar.Caption = "Guardar";
            this.bbiGuardar.Id = 1;
            this.bbiGuardar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiGuardar.ImageOptions.SvgImage")));
            this.bbiGuardar.Name = "bbiGuardar";
            this.bbiGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiGuardar_ItemClick);
            // 
            // ribbonPageHome
            // 
            this.ribbonPageHome.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPageHome.Name = "ribbonPageHome";
            this.ribbonPageHome.Text = "Home";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiGuardar);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Acciones";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 512);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(973, 23);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 147);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(973, 365);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDiferencia);
            this.panel1.Controls.Add(this.labelDiferencia);
            this.panel1.Controls.Add(this.lblExistencia);
            this.panel1.Controls.Add(this.labelExistencia);
            this.panel1.Controls.Add(this.txtConteo);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.cboArticulos);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(482, 150);
            this.panel1.TabIndex = 0;
            // 
            // lblDiferencia
            // 
            this.lblDiferencia.Location = new System.Drawing.Point(406, 82);
            this.lblDiferencia.Name = "lblDiferencia";
            this.lblDiferencia.Size = new System.Drawing.Size(0, 13);
            this.lblDiferencia.TabIndex = 7;
            // 
            // labelDiferencia
            // 
            this.labelDiferencia.Location = new System.Drawing.Point(406, 59);
            this.labelDiferencia.Name = "labelDiferencia";
            this.labelDiferencia.Size = new System.Drawing.Size(48, 13);
            this.labelDiferencia.TabIndex = 6;
            this.labelDiferencia.Text = "Diferencia";
            // 
            // lblExistencia
            // 
            this.lblExistencia.Location = new System.Drawing.Point(406, 36);
            this.lblExistencia.Name = "lblExistencia";
            this.lblExistencia.Size = new System.Drawing.Size(0, 13);
            this.lblExistencia.TabIndex = 5;
            // 
            // labelExistencia
            // 
            this.labelExistencia.Location = new System.Drawing.Point(406, 13);
            this.labelExistencia.Name = "labelExistencia";
            this.labelExistencia.Size = new System.Drawing.Size(48, 13);
            this.labelExistencia.TabIndex = 4;
            this.labelExistencia.Text = "Existencia";
            // 
            // txtConteo
            // 
            this.txtConteo.Location = new System.Drawing.Point(14, 79);
            this.txtConteo.MenuManager = this.ribbon;
            this.txtConteo.Name = "txtConteo";
            this.txtConteo.Size = new System.Drawing.Size(307, 20);
            this.txtConteo.TabIndex = 3;
            this.txtConteo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConteo_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 59);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(35, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Conteo";
            // 
            // cboArticulos
            // 
            this.cboArticulos.Location = new System.Drawing.Point(14, 33);
            this.cboArticulos.MenuManager = this.ribbon;
            this.cboArticulos.Name = "cboArticulos";
            this.cboArticulos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboArticulos.Size = new System.Drawing.Size(307, 20);
            this.cboArticulos.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Artículo";
            // 
            // Ciclicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 535);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "Ciclicos";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Ciclicos";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConteo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboArticulos.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageHome;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem bbiGuardar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtConteo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit cboArticulos;
        private DevExpress.XtraEditors.LabelControl lblDiferencia;
        private DevExpress.XtraEditors.LabelControl labelDiferencia;
        private DevExpress.XtraEditors.LabelControl lblExistencia;
        private DevExpress.XtraEditors.LabelControl labelExistencia;
    }
}