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
using DevExpress.LookAndFeel;
using VisualSoftErp.Properties;
using DevExpress.CodeParser;
using System.IO;
using System.Diagnostics;
using VisualSoftErp.Herramientas.Formas;
using DevExpress.Map.Kml;
using System.Reflection;

namespace VisualSoftErp
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {
            AppDomain.CurrentDomain.FirstChanceException += (sender, e) =>
            {
                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                msg.AppendLine(e.Exception.GetType().FullName);
                msg.AppendLine(e.Exception.Message);
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                msg.AppendLine(st.ToString());
                msg.AppendLine();
                String desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string logFilePath = String.Format("{0}\\{1}", desktopPath, "logfile.txt");
                System.IO.File.AppendAllText(logFilePath, msg.ToString());
            };
            InitializeComponent();
            UserLookAndFeel.Default.SkinName = Settings.Default["ApplicationSkinName"].ToString();

            //Version            
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            barHeaderVersion.Caption = version.ToString();

            //Busca el login y password
            string data = string.Empty;
            string rutaPassword = string.Empty;
            string ruta = System.Configuration.ConfigurationManager.AppSettings["gridlayout"].ToString();
            ruta = ruta + "login.txt";
            rutaPassword = ruta + "ps.txt";
            if (File.Exists(ruta))
            {
                data = System.IO.File.ReadAllText(@ruta);
                txtLogin.Text = data.Substring(0, data.Length - 2);

                if (File.Exists(rutaPassword))
                {
                    data = System.IO.File.ReadAllText(@rutaPassword);
                    txtPassword.Text = data.Substring(0, data.Length - 2);
                    swPassword.IsOn = true;
                }
                else
                    swPassword.IsOn = false;

                txtPassword.Select();
            }
            else
                txtLogin.Select();

            //Revisa si está en modo de prueba
            if (globalCL.gv_strcnn.Substring(47, 16) == "PruebasDiexsaErp")
            {
                popupContainerControl1.Show();
            }
        }



        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Entrar();
            }
        }

        private void Entrar()
        {
            try
            {
                if (txtLogin.Text.Length == 0 || txtPassword.Text.Length == 0)
                {
                    MessageBox.Show("Teclee el login y password");
                    return;
                }

                LoginCL cl = new LoginCL();

                ////String de conexion
                cl.sLogin = txtLogin.Text;
                cl.sPassword = txtPassword.Text;



                string result = cl.Login();
                if (result == "OK")
                {
                    grabaLogin();

                    globalCL.gv_UsuarioID = cl.iUsuarioId;
                    globalCL.gv_UsuarioNombre = cl.sNombre;
                    globalCL.gv_NombreEmpresa = cl.sNomEmp;

                    Form1 frm = new Form1();
                    frm.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Login:" + txtLogin.Text + " o password:" + txtPassword.Text + " incorrectos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grabaLogin()
        {
            try
            {
                string rutapassword = string.Empty;
                string ruta = System.Configuration.ConfigurationManager.AppSettings["gridlayout"].ToString();
                ruta = ruta + "login.txt";
                rutapassword = ruta + "ps.txt";

                try
                {

                    using (StreamWriter writer = new StreamWriter(ruta))
                    {
                        writer.WriteLine(txtLogin.Text);
                    }

                    if (swPassword.IsOn)
                    {
                        using (StreamWriter writer = new StreamWriter(rutapassword))
                        {
                            writer.WriteLine(txtPassword.Text);
                        }
                    }
                    else
                    {

                        if (File.Exists(rutapassword))
                        {
                            File.Delete(rutapassword);
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Menu.FormClosing: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Al grabar login:" + ex.Message);
            }
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiEntrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Entrar();
        }

        private void hyperLinkEdit1_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            DevExpress.XtraEditors.XtraForm frm = new Cambiarpassword();
            DialogResult dialogResult;
            dialogResult = frm.ShowDialog(this);
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Perform required actions here if the dialog result is Ok.
            }
            else
            {
                // Perform default actions here.
            }
            frm.Dispose();
        }



        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}