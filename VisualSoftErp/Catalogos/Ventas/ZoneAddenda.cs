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

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class ZoneAddenda : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string strSerie;
        int intFolio;
        public ZoneAddenda()
        {
            InitializeComponent();
            txtBuyer.Properties.MaxLength = 100;
            txtBuyer.EnterMoveNextControl = true;
            txtDeptID.Properties.MaxLength = 20;
            txtDeptID.EnterMoveNextControl = true;
            txtEmail.Properties.MaxLength = 100;
            txtEmail.EnterMoveNextControl = true;
            txtPoDate.Properties.MaxLength = 10;
            txtPoDate.EnterMoveNextControl = true;
            txtPoID.Properties.MaxLength = 20;
            txtPoID.EnterMoveNextControl = true;
            txtVendorID.Properties.MaxLength = 20;
            txtVendorID.EnterMoveNextControl = true;
            txtVersion.Properties.MaxLength = 5;
            txtVersion.EnterMoveNextControl = true;
            ConseguirDatos();

        }

        private void ConseguirDatos()
        {
            intFolio = globalCL.gv_Folio;
            strSerie = globalCL.gv_Serie;
            lblPedido.Text = "Pedido: " + strSerie + intFolio.ToString();
            ValidarSerieyFolio();
        }

        private void ValidarSerieyFolio()
        {
            ZoneAddendaCL cl = new ZoneAddendaCL();
            cl.intFolio = intFolio;
            cl.strSerie = strSerie;
            String Result = cl.ZoneAddendaLlenaCajas();
            if (Result == "OK")
            {
                txtBuyer.Text = cl.strBuyer;
                txtDeptID.Text = cl.strDeptID;
                txtEmail.Text = cl.strEmail;
                txtPoDate.Text = cl.strPoDate;
                txtPoID.Text = cl.strPoID;
                txtVendorID.Text = cl.strVendorID;
                txtVersion.Text = cl.strVersion;
            }
            else
            {
                txtBuyer.Text = string.Empty;
                txtDeptID.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPoDate.Text = string.Empty;
                txtPoID.Text = string.Empty;
                txtVendorID.Text = string.Empty;
                txtVersion.Text = string.Empty;
            }
        }

        private string Valida()
        {
           
            if (txtBuyer.Text.Length == 0)
            {
                return "El campo Buyer no puede ir vacio";
            }
            if (txtDeptID.Text.Length == 0)
            {
                return "El campo DeptID no puede ir vacio";
            }
            if (txtEmail.Text.Length == 0)
            {
                return "El campo Email no puede ir vacio";
            }
            if (txtPoDate.Text.Length == 0)
            {
                return "El campo PoDate no puede ir vacio";
            }
            if (txtPoID.Text.Length == 0)
            {
                return "El campo PoID no puede ir vacio";
            }
            if (txtVendorID.Text.Length == 0)
            {
                return "El campo VendorID no puede ir vacio";
            }
            if (txtVersion.Text.Length == 0)
            {
                return "El campo Version no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void Guardar()
        {
            try
            {
                String Result = Valida();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }
                ZoneAddendaCL cl = new ZoneAddendaCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.strBuyer = txtBuyer.Text;
                cl.strDeptID = txtDeptID.Text;
                cl.strEmail = txtEmail.Text;
                cl.strPoDate = txtPoDate.Text;
                cl.strPoID = txtPoID.Text;
                cl.strVendorID = txtVendorID.Text;
                cl.strVersion = txtVersion.Text;
                Result = cl.ZoneAddendaCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");              
                }
                else
                {
                    MessageBox.Show(Result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        } //Guardar


        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}