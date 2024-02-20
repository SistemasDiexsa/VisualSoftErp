using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class CasaleyAddenda : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string strSerie;
        int intFolio;
        public CasaleyAddenda()
        {
            InitializeComponent();
            ConseguirDatos();
        }

        private void ConseguirDatos()
        {
            intFolio = globalCL.gv_Folio;
            strSerie = globalCL.gv_Serie;
            lblPedido.Text = "Pedido: "+strSerie + intFolio.ToString();
            ValidarSerieyFolio();
        }

        private void ValidarSerieyFolio()
        {
            CasaLeyAddendaCL cl = new CasaLeyAddendaCL();
            cl.intPedido = intFolio;
            cl.strSerie = strSerie;
            String Result = cl.CasaLeyAddendaLlenaCajas();
            if (Result == "OK")
            {
                txtRemision.Text = cl.strRemision;
                txtFechadeEntrada.Text = cl.fFechaEntrada.ToShortDateString();
                txtNumeroEntrada.Text = cl.strNumeroEntrada;
                txtCentro.Text = cl.strCentro;
            }
            else
            {
                txtRemision.Text = string.Empty;
                txtFechadeEntrada.Text = DateTime.Now.ToShortDateString();
                txtNumeroEntrada.Text = string.Empty;
                txtCentro.Text = string.Empty;
            }
        }

        private string Valida()
        {
       
            if (txtRemision.Text.Length == 0)
            {
                return "El campo Remision no puede ir vacio";
            }
            if (txtFechadeEntrada.Text.Length == 0)
            {
                return "El campo FechaEntrada no puede ir vacio";
            }
            if (txtNumeroEntrada.Text.Length == 0)
            {
                return "El campo NumeroEntrada no puede ir vacio";
            }
            if (txtCentro.Text.Length == 0)
            {
                return "El campo Centro no puede ir vacio";
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
                CasaLeyAddendaCL cl = new CasaLeyAddendaCL();
                cl.strSerie = strSerie;
                cl.intPedido = intFolio;
                cl.strRemision = txtRemision.Text;
                cl.fFechaEntrada = Convert.ToDateTime(txtFechadeEntrada.Text);
                cl.strNumeroEntrada = txtNumeroEntrada.Text;
                cl.strCentro = txtCentro.Text;
                Result = cl.CasaLeyAddendaCrud();
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

        private void bbiGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiRegresar_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}