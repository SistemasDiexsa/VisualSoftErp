namespace VisualSoftErp
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.pELogo = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtLogin = new DevExpress.XtraEditors.TextEdit();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiCerrar = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEntrar = new DevExpress.XtraBars.BarButtonItem();
            this.barHeaderItem1 = new DevExpress.XtraBars.BarHeaderItem();
            this.barHeaderVersion = new DevExpress.XtraBars.BarHeaderItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.swPassword = new DevExpress.XtraEditors.ToggleSwitch();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.hyperLinkEdit1 = new DevExpress.XtraEditors.HyperLinkEdit();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pELogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.swPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pELogo
            // 
            this.pELogo.EditValue = global::VisualSoftErp.Properties.Resources.VS_grande;
            this.pELogo.Location = new System.Drawing.Point(538, 398);
            this.pELogo.Name = "pELogo";
            this.pELogo.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.pELogo.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.pELogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pELogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pELogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pELogo.Size = new System.Drawing.Size(118, 24);
            this.pELogo.TabIndex = 0;
            this.pELogo.EditValueChanged += new System.EventHandler(this.pELogo_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(142, 206);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 24);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Login";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(142, 263);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 24);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Password";
            // 
            // txtLogin
            // 
            this.txtLogin.EnterMoveNextControl = true;
            this.txtLogin.Location = new System.Drawing.Point(274, 206);
            this.txtLogin.MenuManager = this.ribbonControl1;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtLogin.Properties.Appearance.Options.UseFont = true;
            this.txtLogin.Size = new System.Drawing.Size(272, 30);
            this.txtLogin.TabIndex = 5;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.bbiCerrar,
            this.bbiEntrar,
            this.barHeaderItem1,
            this.barHeaderVersion});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 5;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(668, 174);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // bbiCerrar
            // 
            this.bbiCerrar.Caption = "Cerrar";
            this.bbiCerrar.Id = 1;
            this.bbiCerrar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiCerrar.ImageOptions.SvgImage")));
            this.bbiCerrar.Name = "bbiCerrar";
            this.bbiCerrar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCerrar_ItemClick);
            // 
            // bbiEntrar
            // 
            this.bbiEntrar.Caption = "Entrar";
            this.bbiEntrar.Id = 2;
            this.bbiEntrar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiEntrar.ImageOptions.SvgImage")));
            this.bbiEntrar.Name = "bbiEntrar";
            this.bbiEntrar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEntrar_ItemClick);
            // 
            // barHeaderItem1
            // 
            this.barHeaderItem1.Caption = "VisualSoft Erp .Net";
            this.barHeaderItem1.Id = 3;
            this.barHeaderItem1.Name = "barHeaderItem1";
            // 
            // barHeaderVersion
            // 
            this.barHeaderVersion.Caption = "barHeaderItem2";
            this.barHeaderVersion.Id = 4;
            this.barHeaderVersion.Name = "barHeaderVersion";
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
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiCerrar);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiEntrar);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Acciones";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.barHeaderItem1);
            this.ribbonStatusBar1.ItemLinks.Add(this.barHeaderVersion);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 428);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(668, 36);
            // 
            // txtPassword
            // 
            this.txtPassword.EnterMoveNextControl = true;
            this.txtPassword.Location = new System.Drawing.Point(274, 260);
            this.txtPassword.MenuManager = this.ribbonControl1;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(272, 30);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // swPassword
            // 
            this.swPassword.Location = new System.Drawing.Point(394, 398);
            this.swPassword.MenuManager = this.ribbonControl1;
            this.swPassword.Name = "swPassword";
            this.swPassword.Properties.OffText = "No";
            this.swPassword.Properties.OnText = "Sí";
            this.swPassword.Size = new System.Drawing.Size(102, 28);
            this.swPassword.TabIndex = 8;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(274, 404);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(111, 16);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "Recordar password";
            // 
            // hyperLinkEdit1
            // 
            this.hyperLinkEdit1.EditValue = "Cambiar password";
            this.hyperLinkEdit1.Location = new System.Drawing.Point(4, 402);
            this.hyperLinkEdit1.MenuManager = this.ribbonControl1;
            this.hyperLinkEdit1.Name = "hyperLinkEdit1";
            this.hyperLinkEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hyperLinkEdit1.Size = new System.Drawing.Size(125, 20);
            this.hyperLinkEdit1.TabIndex = 12;
            this.hyperLinkEdit1.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyperLinkEdit1_OpenLink);
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.groupControl1);
            this.popupContainerControl1.Location = new System.Drawing.Point(212, 206);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(303, 151);
            this.popupContainerControl1.TabIndex = 15;
            // 
            // groupControl1
            // 
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(303, 151);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Aviso!";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(20, 57);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(260, 16);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Está en modo de pruebas, avise a sistemas!!!";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(204, 117);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(94, 29);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Cerrar";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click_1);
            // 
            // Login
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 464);
            this.Controls.Add(this.popupContainerControl1);
            this.Controls.Add(this.hyperLinkEdit1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.swPassword);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.pELogo);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pELogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.swPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pELogo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtLogin;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem bbiCerrar;
        private DevExpress.XtraBars.BarButtonItem bbiEntrar;
        private DevExpress.XtraBars.BarHeaderItem barHeaderItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraEditors.ToggleSwitch swPassword;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraBars.BarHeaderItem barHeaderVersion;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit1;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}