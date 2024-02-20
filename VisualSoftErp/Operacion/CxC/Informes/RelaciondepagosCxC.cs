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
using VisualSoftErp.Operacion.CxC.Designers;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Operacion.Compras.Clases;

namespace VisualSoftErp.Operacion.CxC.Informes
{
    public partial class RelaciondepagosCxC : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Proveedores = 0;
        int Chequera= 0;

        public RelaciondepagosCxC()
        {
            InitializeComponent();
            Cargacombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
     

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            List<ClaseGenricaCL> ClaseGenercia = new List<ClaseGenricaCL>();
            ClaseGenercia.Add(new ClaseGenricaCL() { Clave = "0", Des = "Todo" });
            ClaseGenercia.Add(new ClaseGenricaCL() { Clave = "1", Des = "Solo lo timbrado" });
            ClaseGenercia.Add(new ClaseGenricaCL() { Clave = "2", Des = "Sin timbrar" });

            BindingSource src = new BindingSource();
          
            cboCliente.Properties.ValueMember = "Clave";
            cboCliente.Properties.DisplayMember = "Des";
            cl.strTabla = "Clientes";
            src.DataSource = cl.CargaCombos();
            cboCliente.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopulateColumns();
            cboCliente.Properties.Columns["Clave"].Visible = false; //con esta propiedad puedo ocultar campos en el cbo
            cboCliente.Properties.Columns["AgentesID"].Visible = false;
            cboCliente.Properties.Columns["Plazo"].Visible = false;
            cboCliente.Properties.Columns["Listadeprecios"].Visible = false;
            cboCliente.Properties.Columns["Exportar"].Visible = false;
            cboCliente.Properties.Columns["cFormapago"].Visible = false;
            cboCliente.Properties.Columns["cMetodopago"].Visible = false;
            cboCliente.Properties.Columns["BancoordenanteID"].Visible = false;
            cboCliente.Properties.Columns["Cuentaordenante"].Visible = false;
            cboCliente.Properties.Columns["PIva"].Visible = false;
            cboCliente.Properties.Columns["PIeps"].Visible = false;
            cboCliente.Properties.Columns["PRetiva"].Visible = false;
            cboCliente.Properties.Columns["EMail"].Visible = false;
            cboCliente.Properties.Columns["UsoCfdi"].Visible = false;
            cboCliente.Properties.Columns["PRetIsr"].Visible = false;
            cboCliente.Properties.Columns["cFormapagoDepositos"].Visible = false;
            cboCliente.Properties.Columns["Moneda"].Visible = false;
            cboCliente.ItemIndex = 0;

            cboChequera.Properties.ValueMember = "Clave";
            cboChequera.Properties.DisplayMember = "Des";
            cl.strTabla = "cyb_Chequeras";
            src.DataSource = cl.CargaCombos();
            cboChequera.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboChequera.Properties.ForceInitialize();
            cboChequera.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboChequera.Properties.ForceInitialize();
            cboChequera.Properties.PopulateColumns();
            cboChequera.Properties.Columns["Clave"].Visible = false;
            cboChequera.ItemIndex = 0;

            cboTimbrado.Properties.ValueMember = "Clave";
            cboTimbrado.Properties.DisplayMember = "Des";
            cl.strTabla = "";
            cboTimbrado.Properties.DataSource = ClaseGenercia;
            cboTimbrado.Properties.ForceInitialize();
            cboTimbrado.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTimbrado.Properties.PopulateColumns();
            cboTimbrado.ItemIndex = 0;

        }

        private void Reporte()
        {
            try
            {

                int Timbrado = Convert.ToInt32(cboTimbrado.EditValue);

                Proveedores = Convert.ToInt32(cboCliente.EditValue);
                Chequera = Convert.ToInt32(cboChequera.EditValue); ;
                //Linea = Convert.ToInt32(cboLinea.EditValue);
                //Familia = Convert.ToInt32(cboLinea.EditValue);


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

                RelaciondepagosDesignerCxC rep = new RelaciondepagosDesignerCxC();
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
                    rep.Parameters["parameter6"].Value = Chequera;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = "x";
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = Timbrado;
                    rep.Parameters["parameter8"].Visible = false;

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
                    rep.Parameters["parameter6"].Value = Chequera;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = "x";
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = Timbrado;
                    rep.Parameters["parameter8"].Visible = false;
                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                    navigationFrame.SelectedPageIndex = 1;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void bbiRegresarImpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiPrevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Reporte();
        }
    }
}