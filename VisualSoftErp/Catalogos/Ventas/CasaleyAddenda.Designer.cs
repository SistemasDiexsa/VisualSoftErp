namespace VisualSoftErp.Operacion.Ventas.Formas
{
    partial class CasaleyAddenda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CasaleyAddenda));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRegresar = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtRemision = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblPedido = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroEntrada = new DevExpress.XtraEditors.TextEdit();
            this.txtFechadeEntrada = new DevExpress.XtraEditors.DateEdit();
            this.txtCentro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemision.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroEntrada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFechadeEntrada.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFechadeEntrada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCentro.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.bbiGuardar,
            this.bbiRegresar});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 3;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(649, 178);
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
            // bbiRegresar
            // 
            this.bbiRegresar.Caption = "Cerrar";
            this.bbiRegresar.Id = 2;
            this.bbiRegresar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiRegresar.ImageOptions.Image")));
            this.bbiRegresar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiRegresar.ImageOptions.LargeImage")));
            this.bbiRegresar.Name = "bbiRegresar";
            this.bbiRegresar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRegresar_ItemClick);
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
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiRegresar, true);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Acciones";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 420);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(649, 29);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(27, 304);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 13);
            this.labelControl1.TabIndex = 78;
            this.labelControl1.Text = "Fecha de entrada";
            // 
            // txtRemision
            // 
            this.txtRemision.Location = new System.Drawing.Point(168, 240);
            this.txtRemision.Name = "txtRemision";
            this.txtRemision.Size = new System.Drawing.Size(302, 22);
            this.txtRemision.TabIndex = 77;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(27, 273);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(93, 13);
            this.labelControl17.TabIndex = 74;
            this.labelControl17.Text = "Numero de entrada";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(27, 243);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(42, 13);
            this.labelControl3.TabIndex = 73;
            this.labelControl3.Text = "Remisión";
            // 
            // lblPedido
            // 
            this.lblPedido.Appearance.Font = new System.Drawing.Font("Tahoma", 16F);
            this.lblPedido.Appearance.Options.UseFont = true;
            this.lblPedido.Location = new System.Drawing.Point(27, 184);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(98, 33);
            this.lblPedido.TabIndex = 79;
            this.lblPedido.Text = "Pedido: ";
            // 
            // txtNumeroEntrada
            // 
            this.txtNumeroEntrada.Location = new System.Drawing.Point(168, 270);
            this.txtNumeroEntrada.Name = "txtNumeroEntrada";
            this.txtNumeroEntrada.Size = new System.Drawing.Size(302, 22);
            this.txtNumeroEntrada.TabIndex = 80;
            // 
            // txtFechadeEntrada
            // 
            this.txtFechadeEntrada.EditValue = null;
            this.txtFechadeEntrada.Location = new System.Drawing.Point(168, 301);
            this.txtFechadeEntrada.MenuManager = this.ribbon;
            this.txtFechadeEntrada.Name = "txtFechadeEntrada";
            this.txtFechadeEntrada.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFechadeEntrada.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFechadeEntrada.Size = new System.Drawing.Size(100, 22);
            this.txtFechadeEntrada.TabIndex = 81;
            // 
            // txtCentro
            // 
            this.txtCentro.Location = new System.Drawing.Point(168, 334);
            this.txtCentro.Name = "txtCentro";
            this.txtCentro.Size = new System.Drawing.Size(302, 22);
            this.txtCentro.TabIndex = 83;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(27, 337);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(33, 13);
            this.labelControl2.TabIndex = 82;
            this.labelControl2.Text = "Centro";
            // 
            // CasaleyAddenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 449);
            this.Controls.Add(this.txtCentro);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtFechadeEntrada);
            this.Controls.Add(this.txtNumeroEntrada);
            this.Controls.Add(this.lblPedido);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtRemision);
            this.Controls.Add(this.labelControl17);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "CasaleyAddenda";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "CasaleyAddenda";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemision.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroEntrada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFechadeEntrada.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFechadeEntrada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCentro.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem bbiGuardar;
        private DevExpress.XtraBars.BarButtonItem bbiRegresar;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtRemision;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lblPedido;
        private DevExpress.XtraEditors.TextEdit txtNumeroEntrada;
        private DevExpress.XtraEditors.DateEdit txtFechadeEntrada;
        private DevExpress.XtraEditors.TextEdit txtCentro;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}