using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using VisualSoftErp.Herramientas;
using VisualSoftErp.Clases;
using System.IO;

namespace VisualSoftErp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            
            LoginCL cl = new LoginCL();

            //AWS_DEX9201028BA
            cl.sRfc = "DEX9201028BA";
            string result = cl.LeeConexion();
            
            result = cl.Verificasihayempresas();

            if (result=="OK")
            {
                if (cl.iNumerodeEmpresas == 0)
                {
                    Application.Run(new WizzardForm());
                }
                else
                {
                    string path = "c:\\VisualSoftErp\\";
                    if (Directory.Exists(path))
                        Application.Run(new Login());
                    else
                        Application.Run(new WizzardForm());
                }
            }
            else
                MessageBox.Show("No se pudo leer la tabla de empresas, verifique la base de datos, si está en nube debe tener internet");                
        }
    }
}
