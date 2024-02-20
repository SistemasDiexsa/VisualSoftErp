using DevExpress.XtraSplashScreen;
using System;
using System.Drawing;

namespace VisualSoftErp
{
    public partial class Splash : SplashScreen
    {
        public Splash()
        {
            InitializeComponent();

            string Path = System.Configuration.ConfigurationManager.AppSettings["logoSplash"].ToString();

            peImage.Image = Image.FromFile(Path);


            this.labelCopyright.Text = "Copyright © 2020-" + (DateTime.Now.Year + 5).ToString() + " VisualSoft";
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }
    }
}