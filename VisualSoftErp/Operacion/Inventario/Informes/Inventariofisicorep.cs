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
using DevExpress.XtraReports.UI;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Inventarios.Designers;
using VisualSoftErp.Operacion.Inventarios.Clases;

namespace VisualSoftErp.Operacion.Inventarios.Informes
{
    public partial class Inventariofisicorep : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Inventariofisicorep()
        {
            InitializeComponent();

            txtFechadeconteo.Text = DateTime.Now.ToShortDateString();
            swMostrar.IsOn = true;

            CargaCombos();
            txtFechadeconteo.Text = DateTime.Now.ToShortDateString();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();

            List<tipoCL> tipoL = new List<tipoCL>();

            tipoL.Add(new tipoCL() { Clave = "null", Des = "Seleccione Agrupar reporte por" });
            tipoL.Add(new tipoCL() { Clave = "F", Des = "Familia" });
            tipoL.Add(new tipoCL() { Clave = "U", Des = "Ubicacion" });
            cboAgrupar.Properties.ValueMember = "Clave";
            cboAgrupar.Properties.DisplayMember = "Des";
            cboAgrupar.Properties.DataSource = tipoL;
            cboAgrupar.Properties.ForceInitialize();
            cboAgrupar.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgrupar.Properties.PopulateColumns();
            cboAgrupar.Properties.Columns["Clave"].Visible = false;
            cboAgrupar.ItemIndex = 1;

            cl.strTabla = "Almacenes";
            cboAlmacen.Properties.ValueMember = "Clave";
            cboAlmacen.Properties.DisplayMember = "Des";
            cboAlmacen.Properties.DataSource = cl.CargaCombos();
            cboAlmacen.Properties.ForceInitialize();
            cboAlmacen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacen.Properties.PopulateColumns();
            cboAlmacen.Properties.Columns["Clave"].Visible = false;
            cboAlmacen.ItemIndex = 0;

            cl.strTabla = "Familias";
            src.DataSource = cl.CargaCombos();
            cboFamilias.Properties.ValueMember = "Clave";
            cboFamilias.Properties.DisplayMember = "Des";
            cboFamilias.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboFamilias.Properties.ForceInitialize();
            cboFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilias.Properties.PopulateColumns();
            cboFamilias.Properties.Columns["Clave"].Visible = false;
            cboFamilias.ItemIndex = 0;

        }

        private string Valida()
        {         
            if (cboAlmacen.EditValue == null)
            {
                return "El campo almacén no puede ir vacio";
            }

            if (cboAgrupar.EditValue == null)
            {
                return "El campo Ubicacion no puede ir vacio";
            }

            if (txtFechadeconteo.Text == null)
            {
                return "El campo fecha de corte no puede ir vacio";
            }

            if (SwGenerar.IsOn == true)
            {

                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtNumero.Text))
                {
                    return "Teclee el número de la toma de inventario";
                }
                if (!clg.esNumerico(txtMarbete.Text))
                {
                    txtMarbete.Text = "0";
                }


                InventariofisicorepCL cl = new InventariofisicorepCL();
                cl.intNumero = Convert.ToInt32(txtNumero.Text);
                cl.intMarbete = Convert.ToInt32(txtMarbete.Text);
                cl.fFecha = Convert.ToDateTime(txtFechadeconteo.Text);
                String Result = cl.InventariofisicoValidar();
                if (Result == "OK")
                {


                    
                        DialogResult result1 = MessageBox.Show("Ya existe un registro en inventariofisico con el numero: " + txtNumero.Text, "Desea continuar ", MessageBoxButtons.YesNo);
                        if (result1.ToString() == "Yes")
                        {
                            return "OK";
                        }
                        else if (result1.ToString() == "No")
                        {
                            return "Ingrese un nuevo número o marbete";
                        }
                   
                }

                string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(txtFechadeconteo.Text).Year, Convert.ToDateTime(txtFechadeconteo.Text).Month, "INV");
                if (result == "C")
                {
                    return "Este mes está cerrado, no se puede actualizar";
                }

            }

            return "OK";
        }

        public class tipoCL
        {
            public string Clave { get; set; }
            public string Des { get; set; }
        }

        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Informes", "Generando, espere por favor...");
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Reporte()
        {
            String Result = Valida();
            if (Result != "OK")
            {
                MessageBox.Show(Result);
                return;
            }

            try
            {
                int Familia = Convert.ToInt32(cboFamilias.EditValue);
                int Almacen = Convert.ToInt32(cboAlmacen.EditValue);
                string GenerarArchivo = string.Empty;

                GenerarArchivo = SwGenerar.IsOn == true ? "S" : "N";


                int FamI = Familia;
                int FamF = Familia;

                if (Familia==0)  //Tdoos
                {
                    FamI = 0;
                    FamF = 99999;
                }

                if (SwGenerar.IsOn == false)
                {
                    txtNumero.Text = "0";
                    txtMarbete.Text = "0";
                }

                int impDirecto = 0;
                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                if (result == "OK")
                {
                    impDirecto = cl.iImpresiondirecta;
                }
                else
                {
                    impDirecto = 0;
                }

                InventariofisicoDesigner rep = new InventariofisicoDesigner();

               
                //Nota: 3Oct20 En FamF se manda el parametro para ver si se muestra o no la existencia

                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = FamI;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = swMostrar.IsOn ? 1:0;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Almacen;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = cboAgrupar.EditValue;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = Convert.ToDateTime(txtFechadeconteo.Text);
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = Convert.ToInt32(txtNumero.Text);
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = Convert.ToInt32(txtMarbete.Text);
                    rep.Parameters["parameter9"].Visible = false;
                    rep.Parameters["parameter10"].Value = GenerarArchivo;   //Duumy
                    rep.Parameters["parameter10"].Visible = false;
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = FamI;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = swMostrar.IsOn ? 1 : 0;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Almacen;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = 0;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = Convert.ToDateTime(txtFechadeconteo.Text);
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = Convert.ToInt32(txtNumero.Text);
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = Convert.ToInt32(txtMarbete.Text);
                    rep.Parameters["parameter9"].Visible = false;
                    rep.Parameters["parameter10"].Value = GenerarArchivo;   //Duumy
                    rep.Parameters["parameter10"].Visible = false;
                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                    navigationFrame.SelectedPageIndex = 1;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void bbiRegresarImp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

        private void SwGenerar_EditValueChanged(object sender, EventArgs e)
        {
            if (SwGenerar.IsOn == true)
            {
                txtNumero.ReadOnly = false;
                txtMarbete.ReadOnly = false;
                txtNumero.Text = "";
                txtMarbete.Text = "";

            }
            else
            {
                txtNumero.ReadOnly = true;
                txtMarbete.ReadOnly = true;
                txtNumero.Text = "0";
                txtMarbete.Text = "0";

            }
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}