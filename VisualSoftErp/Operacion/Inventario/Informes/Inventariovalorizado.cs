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
using VisualSoftErp.Operacion.Inventarios.Designers;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.Inventarios.Informes
{
    public partial class Inventariovalorizado : DevExpress.XtraBars.Ribbon.RibbonForm
    {   int intLineasID;
        public Inventariovalorizado()
        {
            InitializeComponent();
            txtFechadecorte.Text = DateTime.Now.ToShortDateString();
            CargaCombos();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();

            cl.strTabla = "Lineas";
            src.DataSource = cl.CargaCombos();
            cboLinea.Properties.ValueMember = "Clave";
            cboLinea.Properties.DisplayMember = "Des";
            cboLinea.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboLinea.Properties.ForceInitialize();
            cboLinea.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboLinea.Properties.PopulateColumns();
            cboLinea.Properties.Columns["Clave"].Visible = false;
            cboLinea.ItemIndex = 0;

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

            if (cboLinea.EditValue == null)
            {
                return "El campo Linea no puede ir vacio";
            }

            if (cboFamilia.EditValue == null)
            {
                return "El campo Familia no puede ir vacio";
            }

            return "OK";
        }

        public class tipoCL
        {
            public string Clave { get; set; }
            public string Des { get; set; }
        }

        private void CargaComboFamilias()
        {
            intLineasID = Convert.ToInt32(cboLinea.ItemIndex);
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();

            if (intLineasID == 0)
            {
                cl.strTabla = "Familias";
                src.DataSource = cl.CargaCombos();
                cboFamilia.Properties.ValueMember = "Clave";
                cboFamilia.Properties.DisplayMember = "Des";
                cboFamilia.Properties.DataSource = clg.AgregarOpcionTodos(src);
                cboFamilia.Properties.ForceInitialize();
                cboFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboFamilia.Properties.PopulateColumns();
                cboFamilia.Properties.Columns["Clave"].Visible = false;
                cboFamilia.Properties.NullText = "Seleccione una familia";
                cboFamilia.ItemIndex = 0;
            }
            else
            {
                cl.strTabla = "FamiliasLineas";
                cl.iCondicion = Convert.ToInt32(cboLinea.EditValue);
                src.DataSource = cl.CargaCombos();
                cboFamilia.Properties.ValueMember = "Clave";
                cboFamilia.Properties.DisplayMember = "Des";
                cboFamilia.Properties.DataSource = clg.AgregarOpcionTodos(src);
                cboFamilia.Properties.ForceInitialize();
                cboFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboFamilia.Properties.PopulateColumns();
                cboFamilia.Properties.Columns["Clave"].Visible = false;
                cboFamilia.Properties.NullText = "Seleccione una familia";
                cboFamilia.ItemIndex = 0;
            }
            
        }

        private void cboLinea_EditValueChanged(object sender, EventArgs e)
        {
            intLineasID = Convert.ToInt32(cboLinea.EditValue);

            CargaComboFamilias();
        }

        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Generando informe...");
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
                DateTime Fecha;
                Fecha = Convert.ToDateTime(txtFechadecorte.Text);
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

                InventariovalorizadoDesigner rep = new InventariovalorizadoDesigner();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(txtFechadecorte.Text);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboLinea.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToInt32(cboFamilia.EditValue);
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToInt32(cboAlmacen.EditValue);
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = swSolo.IsOn ? 1 : 0;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = 0;
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = "";
                    rep.Parameters["parameter9"].Visible = false;
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    if (swSolo.IsOn)
                        rep.FilterString = "[Costo] = 0";
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(txtFechadecorte.Text);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboLinea.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToInt32(cboFamilia.EditValue);
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToInt32(cboAlmacen.EditValue);
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = swSolo.IsOn ? 1 : 0;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = 0;
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = "";
                    rep.Parameters["parameter9"].Visible = false;
                    documentViewer1.DocumentSource = rep;
                    if (swSolo.IsOn)
                        rep.FilterString = "[Costo] = 0";
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