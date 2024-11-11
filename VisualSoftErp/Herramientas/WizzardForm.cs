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
using System.IO;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Herramientas
{
    public partial class WizzardForm : DevExpress.XtraEditors.XtraForm
    {
        bool PrimerMaquina = true;

        public WizzardForm()
        {
            InitializeComponent();
        }

      
        private void wizardControl1_SelectedPageChanged(object sender, DevExpress.XtraWizard.WizardPageChangedEventArgs e)
        {
            switch (e.Page.ToString())
            {
                case "Cancel":
                    System.Windows.Forms.Application.Exit();
                    break;
            }
        }


        private void wizardControl1_SelectedPageChanging(object sender, DevExpress.XtraWizard.WizardPageChangingEventArgs e)
        {
            string pagina = e.Page.Text;
            switch (pagina)
            {
                case "Crear directorios del sistema":
                    Unidaddedatos();
                    break;
                case "Datos de la empresa":
                    string result = ValidaDirectorio();
                    if (result == "Regresar")
                    {
                        e.Cancel = true;
                    }

                    leeDatosEmpresa();

                    break;
                case "Finalizando el asistente":
                    string resulte = validaEmpresa();
                    if (resulte != "OK")
                    {
                        MessageBox.Show(resulte);
                        e.Cancel = true;
                    }
                    break;
                
            }
        }

      
        private void leeDatosEmpresa()
        {
            try
            {
                LoginCL cl = new LoginCL();
                string result = cl.LeeDatosEmpresa();
                if (result=="OK")
                {
                    txtNombre.Text = cl.sNombre;
                    txtRfc.Text = cl.sRfc;
                    txtDir.Text = cl.sDir;
                    txtTel.Text = cl.sTel;
                    txtCorreo.Text = cl.sCorreo;
                    txtPagina.Text = cl.sPagina;
                    txtNombre.Enabled = false;
                    txtRfc.Enabled = false;
                    txtDir.Enabled = false;
                    txtCorreo.Enabled = false;
                    txtPagina.Enabled = false;
                    txtTel.Enabled = false;

                    PrimerMaquina = false;

                }
                else
                {
                    PrimerMaquina = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string validaEmpresa()
        {
            if (txtNombre.Text.Length==0)
            {
                return "Teclee el nombre de la empresa";
            }
            if (txtRfc.Text.Length==0)
            {
                return "Teclee el rfc de la empresa";
            }
            if (txtDir.Text.Length==0)
            {
                return "Teclee la dirección de la empresa";
            }
            if (txtTel.Text.Length==0)
            {
                return "Teclee el teléfono de la empresa";
            }
            if (txtCorreo.Text.Length==0)
            {
                return "Teclee el correo de la empresa";
            }
            if (txtPagina.Text.Length==0)
            {
                return "Teclee la página web de la empresa";
            }
            if (txtLogo.Text.Length==0)
            {
              
                    return "Seleccione un archivo .png para usar como logotipo";
                
               
            }
            if (!File.Exists(txtLogo.Text))
            {
                return "El archivo " + txtLogo.Text + " no existe";
            }

            return "OK";
        }

        private string ValidaDirectorio()
        {
            string drive = lBCDrive.Text;
            string path = drive + "VisualSoftErp\\";
            if (Directory.Exists(path))
            {
                MessageBox.Show("El directorio " + path + " ya existe, por favor renombre o eliminelo antes de seguir");
                return "Regresar";
            }

            return "OK";
        }

        private string CrearDirectorios()
        {
            try
            {
                string drive = lBCDrive.Text;
                string path = drive + "VisualSoftErp\\";
                if (Directory.Exists(path))
                {
                    MessageBox.Show("El directorio " + path + " ya existe, por favor renombre o eliminelo antes de seguir");
                    return "Regresar";
                }

                string subDir = path + "barcode";
                Directory.CreateDirectory(subDir);

                subDir = path + "Cotizaciones";
                Directory.CreateDirectory(subDir);

                subDir = path + "Images";
                Directory.CreateDirectory(subDir);

                subDir = path + "Layouts";
                Directory.CreateDirectory(subDir);

                subDir = path + "Pdf33";
                Directory.CreateDirectory(subDir);

                subDir = path + "Pref";
                Directory.CreateDirectory(subDir);

                subDir = path + "Sat";
                Directory.CreateDirectory(subDir);

                subDir = path + "Tcr33";
                Directory.CreateDirectory(subDir);

                subDir = path + "Xml33";
                Directory.CreateDirectory(subDir);

                subDir = path + "Xslt";
                Directory.CreateDirectory(subDir);

                string filedestino = path + "Images\\logoEmpresa.png"; 
                File.Copy(txtLogo.Text, filedestino);

                filedestino = path + "Images\\SplashEmpresa.png";
                File.Copy(txtLogo.Text, filedestino);

                return "OK";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "Regresar";
            }
        }

        private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Está seguro que desa salir del asistente?", "Salir", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void Unidaddedatos()
        {
            try
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();

                foreach (DriveInfo d in allDrives)
                {
                    if (d.DriveType.ToString()=="Fixed" ||  d.DriveType.ToString()=="Network")
                    {
                        lBCDrive.Items.Add(d.Name);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            string result = HabilitarSistema();
            if (result != "OK")
            {
                MessageBox.Show(result);
                e.Cancel=true;
                return;
            }

            Login frm = new Login();
            frm.Show();
            this.Hide();

        }

        private string HabilitarSistema()
        {
            try
            {
                string result = CrearDirectorios();
                if (result!="OK")
                {
                    return "Error al crear directorios: " + result;                    
                }

                if (PrimerMaquina)
                {
                
                    LoginCL cl = new LoginCL();
                    cl.sNombre = txtNombre.Text;
                    cl.sRfc = txtRfc.Text;
                    cl.sDir = txtDir.Text;
                    cl.sTel=txtTel.Text;
                    cl.sCorreo=txtCorreo.Text;
                    cl.sPagina=txtPagina.Text;
                    result = cl.EmpresasCrud();

                    if (result!="OK")
                    {
                        return "Error al guardar los datos de la empresa: " + result;
                    }

                    result = cl.InsertaUsuarioyAccesos();
                    if (result != "OK")
                    {
                        return "Error al insertar usuarios y accesos: " + result;
                    }

                }

                return "OK";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }


        private void btnAddFile_Click(object sender, EventArgs e)
        {
            try
            {
                string drive = lBCDrive.Text;
                xtraOpenFileDialog1.Filter = "Archivos (*.png)|*.png";
                xtraOpenFileDialog1.InitialDirectory = drive;
                xtraOpenFileDialog1.CheckFileExists = false;
                DialogResult result = xtraOpenFileDialog1.ShowDialog();
                if (result==DialogResult.OK)
                {
                    txtLogo.Text = xtraOpenFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}