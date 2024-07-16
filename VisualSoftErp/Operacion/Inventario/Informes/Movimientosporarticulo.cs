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
using VisualSoftErp.Operacion.Inventarios.Clases;
using VisualSoftErp.Operacion.Inventarios.Informes;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.Inventarios.Formas
{
    public partial class Movimientosporarticulo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Movimientosporarticulo()
        {
            InitializeComponent();
            CargaCombos();          

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargaCombos()
        {
            BindingSource src = new BindingSource();
            globalCL clg = new globalCL();

            combosCL cl = new combosCL();
            cl.strTabla = "Almacenes";
            src.DataSource = cl.CargaCombos();
            cboAlmacenesID.Properties.ValueMember = "Clave";
            cboAlmacenesID.Properties.DisplayMember = "Des";
            cboAlmacenesID.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAlmacenesID.Properties.ForceInitialize();
            cboAlmacenesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacenesID.Properties.PopulateColumns();
            cboAlmacenesID.Properties.Columns["Clave"].Visible = false;
            cboAlmacenesID.ItemIndex = 0;

            cl.strTabla = "Articulos";
            cboArticulosID.Properties.ValueMember = "Clave";
            cboArticulosID.Properties.DisplayMember = "Des";
            src.DataSource = cl.CargaCombos();
            cboArticulosID.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboArticulosID.Properties.ForceInitialize();
            cboArticulosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulosID.Properties.PopulateColumns();
            cboArticulosID.Properties.Columns["Clave"].Visible = false;
            cboArticulosID.Properties.Columns["FactorUM2"].Visible = false;
            cboArticulosID.Properties.NullText = "Seleccione un artículo";

            //cl.strTabla = "Tiposdearticulo";
            //cboTipoArtículos.Properties.ValueMember = "Clave";
            //cboTipoArtículos.Properties.DisplayMember = "Des";
            //src.DataSource = cl.CargaCombos();
            //cboTipoArtículos.Properties.DataSource = clg.AgregarOpcionTodos(src);
            //cboTipoArtículos.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            //cboTipoArtículos.Properties.ForceInitialize(); 
            //cboTipoArtículos.Properties.PopulateColumns();
            //cboTipoArtículos.Properties.Columns["Clave"].Visible = false;
            //cboTipoArtículos.Properties.NullText = "Seleccione un tipo de artículo";
        }

        private void Aceptar()
        {
    
        }

        private string Valida()
        { 
            if (cboArticulosID.EditValue == null)
            {
                return "Seleccione un artículo";
            }
            if (cboAlmacenesID.EditValue == null)
            {
                return "Seleccione un almacén";
            }
            return "OK";
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

                MovimientosporarticuloDesigner rep = new MovimientosporarticuloDesigner();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = 0;         //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;         //Suc
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboArticulosID.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboAlmacenesID.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToDateTime(FI.ToShortDateString());
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToDateTime(FF.ToShortDateString());
                    rep.Parameters["parameter6"].Visible = false;
                    //rep.Parameters["parameter7"].Visible = false;
                    //rep.Parameters["parameter7"].Value = Convert.ToInt32(cboTipoArtículos.EditValue);
                  
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
                    rep.Parameters["parameter3"].Value = Convert.ToInt32(cboArticulosID.EditValue);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToInt32(cboAlmacenesID.EditValue);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Convert.ToDateTime(FI.ToShortDateString());
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Convert.ToDateTime(FF.ToShortDateString());
                    rep.Parameters["parameter6"].Visible = false;
                    //rep.Parameters["parameter7"].Visible = false;
                    //rep.Parameters["parameter7"].Value = Convert.ToInt32(cboTipoArtículos.EditValue);

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

        private void bbiAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Informes", "Generando, espere por favor...");
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        
            this.Close();
        }

        private void bbiRegresarImp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

    }
}