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
using DevExpress.XtraReports.UI;
using VisualSoftErp.Operacion.Compras.Designers;
using DevExpress.Pdf.Native.BouncyCastle.Ocsp;

namespace VisualSoftErp.Operacion.Compras.Informes
{
    public partial class Relaciondecomprasporproveedor : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Proveedores = 0;
        int Paises = 0;
        public Relaciondecomprasporproveedor()
        {
            InitializeComponent();
            Cargacombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            cboProveedores.Properties.ValueMember = "Clave";
            cboProveedores.Properties.DisplayMember = "Des";
            cl.strTabla = "Proveedores";
            src.DataSource = cl.CargaCombos();
            cboProveedores.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboProveedores.Properties.ForceInitialize();
            cboProveedores.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedores.Properties.ForceInitialize();
            cboProveedores.Properties.PopulateColumns();
            cboProveedores.Properties.Columns["Clave"].Visible = false;
            cboProveedores.Properties.Columns["Piva"].Visible = false;
            cboProveedores.Properties.Columns["Plazo"].Visible = false;
            cboProveedores.Properties.Columns["Tiempodeentrega"].Visible = false;
            cboProveedores.Properties.Columns["Diastraslado"].Visible = false;
            cboProveedores.Properties.Columns["Lab"].Visible = false;
            cboProveedores.Properties.Columns["Via"].Visible = false;
            cboProveedores.Properties.Columns["BancosID"].Visible = false;
            cboProveedores.Properties.Columns["Cuentabancaria"].Visible = false;
            cboProveedores.Properties.Columns["C_Formapago"].Visible = false;
            cboProveedores.Properties.Columns["MonedasID"].Visible = false;
            cboProveedores.Properties.Columns["Retiva"].Visible = false;
            cboProveedores.Properties.Columns["Retisr"].Visible = false;
            cboProveedores.Properties.NullText = "Seleccione un Proveedor";

            cboPaises.Properties.ValueMember = "Clave";
            cboPaises.Properties.DisplayMember = "Des";
            cl.strTabla = "Paises";
            src.DataSource = cl.CargaCombos();
            cboPaises.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboPaises.Properties.ForceInitialize();
            cboPaises.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboPaises.Properties.ForceInitialize();
            cboPaises.Properties.PopulateColumns();
            cboPaises.Properties.Columns["Clave"].Visible = false;
            cboPaises.Properties.NullText = "Seleccione un Pais";

        }

        private void ReporteDetallado()
        {
            try
            {
                Proveedores = Convert.ToInt32(cboProveedores.EditValue);
                Paises = Convert.ToInt32(cboPaises.EditValue);

                int impDirecto = 0;
                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                if (result == "OK")
                    impDirecto = cl.iImpresiondirecta;
                else
                    impDirecto = 0;

                RelaciondecomprasporproveedorDetalladoDesigner rep = new RelaciondecomprasporproveedorDetalladoDesigner();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Proveedores;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Paises;
                    rep.Parameters["parameter6"].Visible = false;
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Proveedores;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Paises;
                    rep.Parameters["parameter6"].Visible = false;
                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                    navigationFrame.SelectedPageIndex = 1;
                }
            }
            catch (Exception ex)
            {
                string line = ex.LineNumber().ToString();
                MessageBox.Show("Error en linea " + line + ":\n" + ex.Message);
            }
        }

        private void ReporteResumen()
        {
            try
            {
                Proveedores = Convert.ToInt32(cboProveedores.EditValue);
                Paises = Convert.ToInt32(cboPaises.EditValue);

                int impDirecto = 0;
                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                if (result == "OK")
                    impDirecto = cl.iImpresiondirecta;
                else
                    impDirecto = 0;

                RelaciondecomprasporproveedorResumenDesigner rep = new RelaciondecomprasporproveedorResumenDesigner();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Proveedores;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Paises;
                    rep.Parameters["parameter6"].Visible = false;
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = 0;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Proveedores;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Paises;
                    rep.Parameters["parameter6"].Visible = false;
                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                    navigationFrame.SelectedPageIndex = 1;
                }
            }
            catch (Exception ex)
            {
                string line = ex.LineNumber().ToString();
                MessageBox.Show("Error en linea " + line + ":\n" + ex.Message);
            }
        }


        private void bbiRegresarImpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.Close();
        }

        private void bbiPrevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Generando informe", "Espere por favor...");
            if (swResumenDetallado.IsOn)
                ReporteDetallado();
            else
                ReporteResumen();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
    }
}