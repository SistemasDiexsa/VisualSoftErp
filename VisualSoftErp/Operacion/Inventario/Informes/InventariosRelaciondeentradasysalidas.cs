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
using System.Web.Hosting;
using DevExpress.LookAndFeel.Design;
using System.Configuration;
using System.Web;

namespace VisualSoftErp.Operacion.Inventarios.Informes
{
    public partial class InventariosRelaciondeentradasysalidas : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        public InventariosRelaciondeentradasysalidas()
        {
            InitializeComponent();

            CargaCombos();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();

            List<tipoCL> tipoL = new List<tipoCL>();
            tipoL.Add(new tipoCL() { Clave = "null", Des = "Seleccione operacion" });
            tipoL.Add(new tipoCL() { Clave = "E", Des = "Entrada" });
            tipoL.Add(new tipoCL() { Clave = "S", Des = "Salida" });
            cboOperacion.Properties.ValueMember = "Clave";
            cboOperacion.Properties.DisplayMember = "Des";
            cboOperacion.Properties.DataSource = tipoL;
            cboOperacion.Properties.ForceInitialize();
            cboOperacion.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboOperacion.Properties.PopulateColumns();
            cboOperacion.Properties.Columns["Clave"].Visible = false;
            cboOperacion.ItemIndex = 0;

            cl.strTabla = "Almacenes";
            src.DataSource = cl.CargaCombos();
            cboAlmacen.Properties.ValueMember = "Clave";
            cboAlmacen.Properties.DisplayMember = "Des";
            cboAlmacen.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAlmacen.Properties.ForceInitialize();
            cboAlmacen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacen.Properties.PopulateColumns();
            cboAlmacen.Properties.Columns["Clave"].Visible = false;
            cboAlmacen.ItemIndex = 0;



        }

        private string Valida()
        {
            if (cboAlmacen.EditValue == null)
            {
                return "El campo Almacen no puede ir vacio";
            }

            if (cboOperacion.EditValue == null)
            {
                return "El campo Ubicacion no puede ir vacio";
            }

            if (cboMovimiento.EditValue == null)
            {
                return "El campo Ubicacion no puede ir vacio";
            }

            if (cboOperacion.ItemIndex == 0)
            {
                return "Seleccione la operación";
            }
                
          
            return "OK";
        }

        public class tipoCL
        {
            public string Clave { get; set; }
            public string Des { get; set; }
        }

        private void CargaComboTipodemovimientoinv()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            if (cboOperacion.EditValue.ToString() == "E")
                cl.strTabla = "TiposdemovimientoinvE";
            else
                cl.strTabla = "TiposdemovimientoinvS";

            src.DataSource = cl.CargaCombos();
            cboMovimiento.Properties.ValueMember = "Clave";
            cboMovimiento.Properties.DisplayMember = "Des";
            cboMovimiento.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboMovimiento.Properties.ForceInitialize();
            cboMovimiento.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMovimiento.Properties.PopulateColumns();
            cboMovimiento.Properties.Columns["Clave"].Visible = false;
            cboMovimiento.ItemIndex = 0;
        }

        private void cboOperacion_EditValueChanged(object sender, EventArgs e)
        {
            CargaComboTipodemovimientoinv();
        }

      

        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Reporte();
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



                DateTime FI, FF;
                FI = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                FF = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
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

                InventariosRelaciondeentradasysalidasDesigner rep = new InventariosRelaciondeentradasysalidasDesigner();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = FI.ToShortDateString();
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = FF.ToShortDateString();
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = cboOperacion.EditValue.ToString();
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToInt32(cboMovimiento.EditValue);
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = cboAlmacen.EditValue;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = globalCL.gv_UsuarioID.ToString();
                    rep.Parameters["parameter8"].Visible = false;
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
                    rep.Parameters["parameter3"].Value = FI;
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = FF;
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = cboOperacion.EditValue.ToString();
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToInt32(cboMovimiento.EditValue);
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = cboAlmacen.EditValue;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = globalCL.gv_UsuarioID.ToString();
                    rep.Parameters["parameter8"].Visible = false;
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

        
        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
    }
}