using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VisualSoftErp
{
    public partial class byeForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public byeForm()
        {
            InitializeComponent();
            
        }

        private void cerrar()
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }

        }
    }
}
