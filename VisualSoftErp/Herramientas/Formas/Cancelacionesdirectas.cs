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
using System.IO;
using VisualSoftErp.Operacion.Compras.Clases;

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class Cancelacionesdirectas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string origen;
        public Cancelacionesdirectas()
        {
            InitializeComponent();
            CargaCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            List<ClaseGenricaCL> ClaseMeses = new List<ClaseGenricaCL>();
            ClaseMeses.Add(new ClaseGenricaCL() { Clave = "1", Des = "Enero" });
            ClaseMeses.Add(new ClaseGenricaCL() { Clave = "2", Des = "Febrero" });
            ClaseMeses.Add(new ClaseGenricaCL() { Clave = "3", Des = "Marzo" });
            ClaseMeses.Add(new ClaseGenricaCL() { Clave = "4", Des = "Abril" });
            ClaseMeses.Add(new ClaseGenricaCL() { Clave = "5", Des = "Mayo" });
            ClaseMeses.Add(new ClaseGenricaCL() { Clave = "6", Des = "Junio" });
            ClaseMeses.Add(new ClaseGenricaCL() { Clave = "7", Des = "Julio" });
            ClaseMeses.Add(new ClaseGenricaCL() { Clave = "8", Des = "Agosto" });
            ClaseMeses.Add(new ClaseGenricaCL() { Clave = "9", Des = "Septiembre" });
            ClaseMeses.Add(new ClaseGenricaCL() { Clave = "10", Des = "Octubre" });
            ClaseMeses.Add(new ClaseGenricaCL() { Clave = "11", Des = "Noviembre" });
            ClaseMeses.Add(new ClaseGenricaCL() { Clave = "12", Des = "Diciembre" });


            cl.strTabla = "";
            cboMes.Properties.ValueMember = "Clave";
            cboMes.Properties.DisplayMember = "Des";
            cboMes.Properties.DataSource = ClaseMeses;
            cboMes.Properties.ForceInitialize();
            cboMes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMes.Properties.PopulateColumns();
            cboMes.Properties.Columns["Clave"].Visible = false;
            cboMes.ItemIndex = 0;

            cl.strTabla = "Añosventas";
            cboAño.Properties.ValueMember = "Clave";
            cboAño.Properties.DisplayMember = "Des";
            cboAño.Properties.DataSource = cl.CargaCombos();
            cboAño.Properties.ForceInitialize();
            cboAño.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAño.Properties.PopulateColumns();
            cboAño.Properties.Columns["Clave"].Visible = false;
            cboAño.ItemIndex = 0;
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            origen = "Nuevo";
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCargardatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVerificarCfdi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void Regresprincipal()
        {
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCargardatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVerificarCfdi.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (origen=="Nuevo")
            {
                Regresprincipal();
            }      
            else
            {
                globalCL clg = new globalCL();
                clg.strGridLayout = "gridCancelacionDirectaDetalle";
                String Result = clg.SaveGridLayout(gridViewDetalle);
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                }
                origen = "Nuevo";
                bbiCargardatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                navigationFrame.SelectedPageIndex = 1;
            }
        }    

        private void CargaDatos()
        {
            globalCL clg = new globalCL();
            if (!clg.esNumerico(txtFolio.Text))
            {
                MessageBox.Show("Teclee el folio");
            }

            //RutaXML
            string pRutaXML = System.Configuration.ConfigurationManager.AppSettings["pathxml"];

            string sYear = cboAño.EditValue.ToString();
            int Mes = Convert.ToInt32(cboMes.EditValue);
            string pRutaXMLPorCancelar = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString() + sYear + "\\" + clg.NombreDeMes(Mes,3) + "\\" + txtSerie.Text + txtFolio.Text + "_PORCANCELAR.XML";

            if (!File.Exists(pRutaXMLPorCancelar))
            {
                MessageBox.Show("No se encontró el XML por cancelar");
                return;
            }


            gridControlDetalle.DataSource = clg.LeeXml(pRutaXMLPorCancelar);
            if (gridControlDetalle != null)
            {
                txtCliente.Text = gridViewDetalle.GetRowCellValue(0, "Cliente").ToString();
                txtMoneda.Text = gridViewDetalle.GetRowCellValue(0, "Moneda").ToString();
                txtFecha.Text = gridViewDetalle.GetRowCellValue(0, "Fecha").ToString();
                txtSerieDet.Text = txtSerie.Text;
                txtFolioDet.Text = txtFolio.Text;

               
                clg.strGridLayout = "gridCancelacionDirectaDetalle";
                clg.restoreLayout(gridViewDetalle);

                origen = "XML";
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiCargardatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                navigationFrame.SelectedPageIndex = 2;

            }
            else
            {
                MessageBox.Show("No se pudo leer la información del XML");
            }
        }

        private void bbiCargardatos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CargaDatos();
        }

        private void Cancelar()
        {
            bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            globalCL cl = new globalCL();

            string result = cl.GM_CierredemodulosStatus(DateTime.Now.Year, DateTime.Now.Month, "VTA");
            if (result == "C")
            {
                MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                return;
            }

            string sYear = cboAño.EditValue.ToString();
            int Mes = Convert.ToInt32(cboMes.EditValue);
            string pRutaXML = System.Configuration.ConfigurationManager.AppSettings["pathxml"].ToString() + sYear + "\\" + cl.NombreDeMes(Mes,3) + "\\";
            string strNomArch = txtSerie.Text + txtFolio.Text + "_PORCANCELAR.XML";
            string strNomArchCancelado = txtSerie.Text + txtFolio.Text + "_PORCANCELAR_CANCELADO.XML";

            result = cl.CancelaTimbrado(txtSerie.Text, pRutaXML, strNomArch,"","");
            if (result=="OK")
            {
                Guardar();
                File.Move(pRutaXML + strNomArch, pRutaXML + strNomArchCancelado);
                MessageBox.Show("Cancelado correctamente, sugerimos verifique el CFDI en el sat");
                Regresprincipal();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cancelar();
        }

        private void Guardar()
        {
            try
            {
                
                CancelacionesdirectasCL cl = new CancelacionesdirectasCL();
                cl.fFecha = Convert.ToDateTime(txtFecha.Text);
                cl.intUsuariosID = globalCL.gv_UsuarioID;
                cl.strSerie = txtSerie.Text;
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.strReceptornombre = txtCliente.Text;
                string Result = cl.CancelacionesdirectasCrud();
                if (Result != "OK")
                {                   
                    MessageBox.Show("Al guardar:" + Result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        } //Guardar
    }
}