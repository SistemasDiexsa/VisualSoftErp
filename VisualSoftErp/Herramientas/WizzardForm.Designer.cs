namespace VisualSoftErp.Herramientas
{
    partial class WizzardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizzardForm));
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.wizardPage1 = new DevExpress.XtraWizard.WizardPage();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lBCDrive = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit4 = new DevExpress.XtraEditors.PictureEdit();
            this.wizardPage2 = new DevExpress.XtraWizard.WizardPage();
            this.pictureEdit5 = new DevExpress.XtraEditors.PictureEdit();
            this.txtLogo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit3 = new DevExpress.XtraEditors.PictureEdit();
            this.txtPagina = new DevExpress.XtraEditors.TextEdit();
            this.txtCorreo = new DevExpress.XtraEditors.TextEdit();
            this.txtTel = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtRfc = new DevExpress.XtraEditors.TextEdit();
            this.txtDir = new DevExpress.XtraEditors.TextEdit();
            this.txtNombre = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.xtraOpenFileDialog1 = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.welcomeWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.wizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lBCDrive)).BeginInit();
            this.completionWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit4.Properties)).BeginInit();
            this.wizardPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPagina.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorreo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRfc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Controls.Add(this.welcomeWizardPage1);
            this.wizardControl1.Controls.Add(this.wizardPage1);
            this.wizardControl1.Controls.Add(this.completionWizardPage1);
            this.wizardControl1.Controls.Add(this.wizardPage2);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.ImageWidth = 216;
            this.wizardControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.wizardControl1.MinimumSize = new System.Drawing.Size(117, 123);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.wizardPage1,
            this.wizardPage2,
            this.completionWizardPage1});
            this.wizardControl1.Size = new System.Drawing.Size(784, 512);
            this.wizardControl1.Text = "VisualSoftErp.Net";
            this.wizardControl1.WizardStyle = DevExpress.XtraWizard.WizardStyle.WizardAero;
            this.wizardControl1.SelectedPageChanged += new DevExpress.XtraWizard.WizardPageChangedEventHandler(this.wizardControl1_SelectedPageChanged);
            this.wizardControl1.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.wizardControl1_SelectedPageChanging);
            this.wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_CancelClick);
            this.wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_FinishClick);
           
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.Controls.Add(this.labelControl1);
            this.welcomeWizardPage1.Controls.Add(this.pictureEdit1);
            this.welcomeWizardPage1.IntroductionText = "Este asistente";
            this.welcomeWizardPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(710, 301);
            this.welcomeWizardPage1.Text = "Bienvenido";
            
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(23, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(409, 17);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Este asistente lo guiará paso a paso para empezar a usar el sistema.";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::VisualSoftErp.Properties.Resources.VS_grande;
            this.pictureEdit1.Location = new System.Drawing.Point(511, 252);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(184, 35);
            this.pictureEdit1.TabIndex = 1;
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.pictureEdit2);
            this.wizardPage1.Controls.Add(this.labelControl3);
            this.wizardPage1.Controls.Add(this.lBCDrive);
            this.wizardPage1.Controls.Add(this.labelControl2);
            this.wizardPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(710, 301);
            this.wizardPage1.Text = "Crear directorios del sistema";
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.EditValue = global::VisualSoftErp.Properties.Resources.VS_grande;
            this.pictureEdit2.Location = new System.Drawing.Point(513, 253);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit2.Size = new System.Drawing.Size(184, 35);
            this.pictureEdit2.TabIndex = 6;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(16, 71);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(283, 16);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "si desea cambiar el drive, seleccionelo de la lista.";
            // 
            // lBCDrive
            // 
            this.lBCDrive.Location = new System.Drawing.Point(415, 44);
            this.lBCDrive.Name = "lBCDrive";
            this.lBCDrive.Size = new System.Drawing.Size(120, 21);
            this.lBCDrive.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(16, 49);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(379, 16);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "La estructura de directorios del sistema será instalada en el drive:";
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.Controls.Add(this.labelControl12);
            this.completionWizardPage1.Controls.Add(this.labelControl10);
            this.completionWizardPage1.Controls.Add(this.labelControl11);
            this.completionWizardPage1.Controls.Add(this.pictureEdit4);
            this.completionWizardPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.Size = new System.Drawing.Size(710, 301);
            this.completionWizardPage1.Text = "Finalizando el asistente";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Location = new System.Drawing.Point(18, 270);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(164, 16);
            this.labelControl12.TabIndex = 11;
            this.labelControl12.Text = "De click en el botón finalizar.";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 18F);
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Silver;
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Appearance.Options.UseForeColor = true;
            this.labelControl10.Location = new System.Drawing.Point(196, 175);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(500, 36);
            this.labelControl10.TabIndex = 10;
            this.labelControl10.Text = "Gracias por utilizar nuestros sistemas.";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 18F);
            this.labelControl11.Appearance.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Appearance.Options.UseForeColor = true;
            this.labelControl11.Location = new System.Drawing.Point(18, 23);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(593, 36);
            this.labelControl11.TabIndex = 9;
            this.labelControl11.Text = "Felicidades, has completado todos los pasos!";
            // 
            // pictureEdit4
            // 
            this.pictureEdit4.EditValue = global::VisualSoftErp.Properties.Resources.VS_grande;
            this.pictureEdit4.Location = new System.Drawing.Point(512, 251);
            this.pictureEdit4.Name = "pictureEdit4";
            this.pictureEdit4.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit4.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit4.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit4.Size = new System.Drawing.Size(184, 35);
            this.pictureEdit4.TabIndex = 7;
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.pictureEdit5);
            this.wizardPage2.Controls.Add(this.txtLogo);
            this.wizardPage2.Controls.Add(this.labelControl13);
            this.wizardPage2.Controls.Add(this.pictureEdit3);
            this.wizardPage2.Controls.Add(this.txtPagina);
            this.wizardPage2.Controls.Add(this.txtCorreo);
            this.wizardPage2.Controls.Add(this.txtTel);
            this.wizardPage2.Controls.Add(this.labelControl9);
            this.wizardPage2.Controls.Add(this.labelControl8);
            this.wizardPage2.Controls.Add(this.labelControl7);
            this.wizardPage2.Controls.Add(this.txtRfc);
            this.wizardPage2.Controls.Add(this.txtDir);
            this.wizardPage2.Controls.Add(this.txtNombre);
            this.wizardPage2.Controls.Add(this.labelControl4);
            this.wizardPage2.Controls.Add(this.labelControl5);
            this.wizardPage2.Controls.Add(this.labelControl6);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(710, 301);
            this.wizardPage2.Text = "Datos de la empresa";
            // 
            // pictureEdit5
            // 
            this.pictureEdit5.EditValue = ((object)(resources.GetObject("pictureEdit5.EditValue")));
            this.pictureEdit5.Location = new System.Drawing.Point(443, 235);
            this.pictureEdit5.Name = "pictureEdit5";
            this.pictureEdit5.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit5.Size = new System.Drawing.Size(47, 31);
            this.pictureEdit5.TabIndex = 23;
            this.pictureEdit5.EditValueChanged += new System.EventHandler(this.pictureEdit5_EditValueChanged);
            this.pictureEdit5.Click += new System.EventHandler(this.pictureEdit5_Click);
            // 
            // txtLogo
            // 
            this.txtLogo.Enabled = false;
            this.txtLogo.EnterMoveNextControl = true;
            this.txtLogo.Location = new System.Drawing.Point(101, 244);
            this.txtLogo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLogo.Name = "txtLogo";
            this.txtLogo.Properties.MaxLength = 14;
            this.txtLogo.Size = new System.Drawing.Size(336, 22);
            this.txtLogo.TabIndex = 22;
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(15, 247);
            this.labelControl13.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(48, 16);
            this.labelControl13.TabIndex = 21;
            this.labelControl13.Text = "Logotipo";
            // 
            // pictureEdit3
            // 
            this.pictureEdit3.EditValue = global::VisualSoftErp.Properties.Resources.VS_grande;
            this.pictureEdit3.Location = new System.Drawing.Point(511, 263);
            this.pictureEdit3.Name = "pictureEdit3";
            this.pictureEdit3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit3.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit3.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit3.Size = new System.Drawing.Size(184, 35);
            this.pictureEdit3.TabIndex = 20;
            // 
            // txtPagina
            // 
            this.txtPagina.EnterMoveNextControl = true;
            this.txtPagina.Location = new System.Drawing.Point(101, 205);
            this.txtPagina.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPagina.Name = "txtPagina";
            this.txtPagina.Properties.MaxLength = 100;
            this.txtPagina.Size = new System.Drawing.Size(594, 22);
            this.txtPagina.TabIndex = 19;
            // 
            // txtCorreo
            // 
            this.txtCorreo.EnterMoveNextControl = true;
            this.txtCorreo.Location = new System.Drawing.Point(101, 169);
            this.txtCorreo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Properties.MaxLength = 100;
            this.txtCorreo.Size = new System.Drawing.Size(594, 22);
            this.txtCorreo.TabIndex = 18;
            // 
            // txtTel
            // 
            this.txtTel.EnterMoveNextControl = true;
            this.txtTel.Location = new System.Drawing.Point(101, 133);
            this.txtTel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTel.Name = "txtTel";
            this.txtTel.Properties.MaxLength = 50;
            this.txtTel.Size = new System.Drawing.Size(594, 22);
            this.txtTel.TabIndex = 17;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(15, 208);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(66, 16);
            this.labelControl9.TabIndex = 16;
            this.labelControl9.Text = "Página web";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(15, 172);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(39, 16);
            this.labelControl8.TabIndex = 15;
            this.labelControl8.Text = "Correo";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(15, 136);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(50, 16);
            this.labelControl7.TabIndex = 14;
            this.labelControl7.Text = "Teléfono";
            // 
            // txtRfc
            // 
            this.txtRfc.EnterMoveNextControl = true;
            this.txtRfc.Location = new System.Drawing.Point(101, 54);
            this.txtRfc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRfc.Name = "txtRfc";
            this.txtRfc.Properties.MaxLength = 14;
            this.txtRfc.Size = new System.Drawing.Size(218, 22);
            this.txtRfc.TabIndex = 10;
            // 
            // txtDir
            // 
            this.txtDir.EnterMoveNextControl = true;
            this.txtDir.Location = new System.Drawing.Point(101, 93);
            this.txtDir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDir.Name = "txtDir";
            this.txtDir.Properties.MaxLength = 100;
            this.txtDir.Size = new System.Drawing.Size(594, 22);
            this.txtDir.TabIndex = 11;
            // 
            // txtNombre
            // 
            this.txtNombre.EnterMoveNextControl = true;
            this.txtNombre.Location = new System.Drawing.Point(101, 14);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Properties.MaxLength = 100;
            this.txtNombre.Size = new System.Drawing.Size(594, 22);
            this.txtNombre.TabIndex = 8;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(15, 96);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 16);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "Dirección";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 18);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(45, 16);
            this.labelControl5.TabIndex = 12;
            this.labelControl5.Text = "Nombre";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(15, 57);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(18, 16);
            this.labelControl6.TabIndex = 9;
            this.labelControl6.Text = "Rfc";
            // 
            // xtraOpenFileDialog1
            // 
            this.xtraOpenFileDialog1.FileName = "xtraOpenFileDialog1";
            // 
            // WizzardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 512);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WizzardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VisualSoftErp.Net";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.welcomeWizardPage1.ResumeLayout(false);
            this.welcomeWizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lBCDrive)).EndInit();
            this.completionWizardPage1.ResumeLayout(false);
            this.completionWizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit4.Properties)).EndInit();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPagina.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorreo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRfc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraWizard.WizardPage wizardPage1;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ListBoxControl lBCDrive;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
        private DevExpress.XtraWizard.WizardPage wizardPage2;
        private DevExpress.XtraEditors.TextEdit txtPagina;
        private DevExpress.XtraEditors.TextEdit txtCorreo;
        private DevExpress.XtraEditors.TextEdit txtTel;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtRfc;
        private DevExpress.XtraEditors.TextEdit txtDir;
        private DevExpress.XtraEditors.TextEdit txtNombre;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.PictureEdit pictureEdit4;
        private DevExpress.XtraEditors.PictureEdit pictureEdit3;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.PictureEdit pictureEdit5;
        private DevExpress.XtraEditors.TextEdit txtLogo;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.XtraOpenFileDialog xtraOpenFileDialog1;
    }
}