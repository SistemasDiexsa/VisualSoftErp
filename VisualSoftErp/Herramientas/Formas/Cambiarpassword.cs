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

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class Cambiarpassword : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int usuario;
        public Cambiarpassword()
        {
            InitializeComponent();
            ribbonControl.SelectedPage = ribbonPageHome;

            //DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Guardar()
        {
            try
            {
                String Result = Valida();
                if (Result == "OK")
                {
                    if (txtClavenueva.Text == txtConfirmar.Text)
                    {
                        LoginCL cl = new LoginCL();
                        cl.sLogin = txtLogin.Text;
                        cl.sPasswordNuevo = Convert.ToString(txtConfirmar.Text);
                        Result = cl.CambiarPassword();
                        if (Result == "OK")
                        {
                            MessageBox.Show("Guardado Correctamente");
                            LimpiaCajas();
                        }
                        else
                        {
                            MessageBox.Show(Result);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Clave nueva y confirmacion son diferentes");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(Result);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        } //Guardar

        private string Valida()
        {
          
            if (txtLogin.Text.Length == 0)
            {
                return "El campo Login no puede ir vacio";
            }
            if (txtClaveanterior.Text.Length == 0)
            {
                return "El campo Clave anterior no puede ir vacio";
            }
            if (txtClavenueva.Text.Length == 0)
            {
                return "El campo Clave nueva no puede ir vacio";
            }
            if (txtConfirmar.Text.Length == 0)
            {
                return "El campo Confirmar no puede ir vacio";
            }

            if (txtClavenueva.Text != txtConfirmar.Text)
            {
                return "No concuerda la clave nueva";
            }

            LoginCL cl = new LoginCL();
            cl.sLogin = txtLogin.Text;
            cl.sPassword = txtClaveanterior.Text;
            string Result = cl.Login();

            if (Result=="OK")
            {
                usuario = cl.iUsuarioId;
            }

            return Result;

        } //Valida   

        private void LimpiaCajas()
        {
            txtLogin.Text = string.Empty;
            txtClaveanterior.Text = string.Empty;
            txtClavenueva.Text = string.Empty;
            txtConfirmar.Text = string.Empty;
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}