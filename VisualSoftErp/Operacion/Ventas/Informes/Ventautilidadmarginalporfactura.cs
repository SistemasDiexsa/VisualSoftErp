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
using VisualSoftErp.Operacion.Ventas.Designers;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class Ventautilidadmarginalporfactura : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string strNiveldeinformacion;
        int impDirecto = 0;
        DateTime FI, FF;
        public Ventautilidadmarginalporfactura()
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
            tipoL.Add(new tipoCL() { Clave = "F", Des = "Por factura" });
            tipoL.Add(new tipoCL() { Clave = "FM-ART", Des = "Por familia-artículo" });
            tipoL.Add(new tipoCL() { Clave = "C", Des = "Por cliente" });
            cboNiveldeinfor.Properties.ValueMember = "Clave";
            cboNiveldeinfor.Properties.DisplayMember = "Des";
            cboNiveldeinfor.Properties.DataSource = tipoL;
            cboNiveldeinfor.Properties.ForceInitialize();
            cboNiveldeinfor.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboNiveldeinfor.Properties.PopulateColumns();
            cboNiveldeinfor.Properties.Columns["Clave"].Visible = false;
            cboNiveldeinfor.ItemIndex = 0;

            cl.strTabla = "ClientesRep";
            src.DataSource = cl.CargaCombos();
            cboCliente.Properties.ValueMember = "Clave";
            cboCliente.Properties.DisplayMember = "Des";
            cboCliente.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCliente.Properties.PopulateColumns();
            cboCliente.Properties.Columns["Clave"].Visible = false; //con esta propiedad puedo ocultar campos en el cbo
            //cboCliente.Properties.Columns["AgentesID"].Visible = false;
            //cboCliente.Properties.Columns["Plazo"].Visible = false;
            //cboCliente.Properties.Columns["Listadeprecios"].Visible = false;
            //cboCliente.Properties.Columns["Exportar"].Visible = false;
            //cboCliente.Properties.Columns["cFormapago"].Visible = false;
            //cboCliente.Properties.Columns["cMetodopago"].Visible = false;
            //cboCliente.Properties.Columns["BancoordenanteID"].Visible = false;
            //cboCliente.Properties.Columns["Cuentaordenante"].Visible = false;
            ////cboClientesID.Properties.Columns["Usocfdi"].Visible = false;
            //cboCliente.Properties.Columns["PIva"].Visible = false;
            //cboCliente.Properties.Columns["PIeps"].Visible = false;
            //cboCliente.Properties.Columns["PRetiva"].Visible = false;
            ////cboClientesID.Properties.Columns["PRetisr"].Visible = false;
            //cboCliente.Properties.Columns["EMail"].Visible = false;
            cboCliente.ItemIndex = 0;
           
            cl.strTabla = "Agentes";
            src.DataSource = cl.CargaCombos();
            cboAgentes.Properties.ValueMember = "Clave";
            cboAgentes.Properties.DisplayMember = "Des";
            cboAgentes.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAgentes.Properties.ForceInitialize();
            cboAgentes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgentes.Properties.PopulateColumns();
            cboAgentes.Properties.Columns["Clave"].Visible = false;
            cboAgentes.ItemIndex = 0;

            cl.strTabla = "Canalesdeventa";
            src.DataSource = cl.CargaCombos();
            cboCanaldeventa.Properties.ValueMember = "Clave";
            cboCanaldeventa.Properties.DisplayMember = "Des";
            cboCanaldeventa.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboCanaldeventa.Properties.ForceInitialize();
            cboCanaldeventa.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCanaldeventa.Properties.PopulateColumns();
            cboCanaldeventa.Properties.Columns["Clave"].Visible = false;
            cboCanaldeventa.ItemIndex = 0;

            cl.strTabla = "Giros";
            src.DataSource = cl.CargaCombos();
            cboGiro.Properties.ValueMember = "Clave";
            cboGiro.Properties.DisplayMember = "Des";
            cboGiro.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboGiro.Properties.ForceInitialize();
            cboGiro.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboGiro.Properties.PopulateColumns();
            cboGiro.Properties.Columns["Clave"].Visible = false;
            cboGiro.ItemIndex = 0;

            cl.strTabla = "TiposUEN";
            src.DataSource = cl.CargaCombos();
            cboUen.Properties.ValueMember = "Clave";
            cboUen.Properties.DisplayMember = "Des";
            cboUen.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboUen.Properties.ForceInitialize();
            cboUen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboUen.Properties.PopulateColumns();
            cboUen.Properties.Columns["Clave"].Visible = false;
            cboUen.ItemIndex = 0;

            cl.strTabla = "ClasificacionPorTipo";
            src.DataSource = cl.CargaCombos();
            cboTipo.Properties.ValueMember = "Clave";
            cboTipo.Properties.DisplayMember = "Des";
            cboTipo.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboTipo.Properties.ForceInitialize();
            cboTipo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTipo.Properties.PopulateColumns();
            cboTipo.Properties.Columns["Clave"].Visible = false;
            cboTipo.ItemIndex = 0;

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
            CargaFamilias();
            CargaSubFamilias();

        }

        private void CargaFamilias()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();

            if (cboLinea.ItemIndex == 0)
            {
                cl.strTabla = "Familias";
            }
            else
            {
                cl.strTabla = "FamiliasLineas";
                cl.iCondicion = Convert.ToInt32(cboLinea.EditValue);
            }
            src.DataSource = cl.CargaCombos();
            cboFamilia.Properties.ValueMember = "Clave";
            cboFamilia.Properties.DisplayMember = "Des";
            cboFamilia.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboFamilia.Properties.ForceInitialize();
            cboFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilia.Properties.PopulateColumns();
            cboFamilia.Properties.Columns["Clave"].Visible = false;
            cboFamilia.ItemIndex = 0;

            
        }

        private void CargaSubFamilias()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();

            if (cboLinea.ItemIndex == 0)
            {
                return;
            }
            else
            {
                cl.strTabla = "SubFamiliasXFamilia";
                cl.iCondicion = Convert.ToInt32(cboFamilia.EditValue);
            }
            src.DataSource = cl.CargaCombos();
            cboSubFamilia.Properties.ValueMember = "Clave";
            cboSubFamilia.Properties.DisplayMember = "Des";
            cboSubFamilia.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboSubFamilia.Properties.ForceInitialize();
            cboSubFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSubFamilia.Properties.PopulateColumns();
            cboSubFamilia.Properties.Columns["Clave"].Visible = false;
            cboSubFamilia.ItemIndex = 0;
        }

        private string Valida()
        {

            switch (strNiveldeinformacion)
            {
                case "F":
                    if (cboCliente.EditValue == null)
                    {
                        return "El campo Cliente no puede ir vacio";
                    }

                    if (cboAgentes.EditValue == null)
                    {
                        return "El campo Agente no puede ir vacio";
                    }

                    if (cboCanaldeventa.EditValue == null)
                    {
                        return "El campo Canales de venta no puede ir vacio";
                    }
                    break;
                case "FM-ART":
                    if (cboFamilia.EditValue == null)
                    {
                        return "El campo familia no puede ir vacio";
                    }

                    if (cboLinea.EditValue == null)
                    {
                        return "El campo linea de venta no puede ir vacio";
                    }
                    break;

                case "C":
                    if (cboCliente.EditValue == null)
                    {
                        return "El campo Cliente no puede ir vacio";
                    }

                    if (cboAgentes.EditValue == null)
                    {
                        return "El campo Agente no puede ir vacio";
                    }

                    if (cboCanaldeventa.EditValue == null)
                    {
                        return "El campo Canales de venta no puede ir vacio";
                    }
                    break;
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
            FI = DateTime.Now;
            FF = DateTime.Now;
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Generando informe...");
            Reporte();

            cboCliente.Enabled = false;

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

                FI = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                FF = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);

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

                if (strNiveldeinformacion == "F") { ImprimirxFactura(); }
                if (strNiveldeinformacion == "FM-ART") { ImprimirxFamArt(); }
                if (strNiveldeinformacion == "C") { ImprimirxCliente(); }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void ImprimirxFactura()
        {
            


            VentasutilidadmarginalporfacturaDesigner rep = new VentasutilidadmarginalporfacturaDesigner();

            if (cboUen.Text != "Todos")
                rep.FilterString = "[uen]=" + cboUen.EditValue.ToString();
            if (cboGiro.Text != "Todos")
                rep.FilterString = "[GirosID]=" + cboGiro.EditValue.ToString();
            if (cboTipo.Text != "Todos")
                rep.FilterString = "[ClasificacionPorTipoid]=" + cboTipo.EditValue.ToString();

            if (impDirecto == 1)
            {
                rep.Parameters["parameter1"].Value = 0;         //Emp
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = 0;         //Suc
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = FI;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = FF;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Convert.ToInt32(cboCliente.EditValue);
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Convert.ToInt32(cboAgentes.EditValue);
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = Convert.ToInt32(cboCanaldeventa.EditValue);
                rep.Parameters["parameter7"].Visible = false;
                rep.Parameters["parameter8"].Value = "";
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
                rep.Parameters["parameter5"].Value = Convert.ToInt32(cboCliente.EditValue);
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Convert.ToInt32(cboAgentes.EditValue);
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = Convert.ToInt32(cboCanaldeventa.EditValue);
                rep.Parameters["parameter7"].Visible = false;
                rep.Parameters["parameter8"].Value = "";
                rep.Parameters["parameter8"].Visible = false;
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                navigationFrame.SelectedPageIndex = 1;
            }
        }

        private void ImprimirxFamArt()
        {
            int subFam = 0;
            if (cboSubFamilia.ItemIndex == -1)
                subFam = 0;
            else
                subFam = Convert.ToInt32(cboSubFamilia.EditValue);

            UtilidadmarginalporlineafamiliaarticuloDesigner rep = new UtilidadmarginalporlineafamiliaarticuloDesigner();
            if (impDirecto == 1)
            {
                rep.Parameters["parameter1"].Value = 0;         //Emp
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = 0;         //Suc
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = FI;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = FF;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Convert.ToInt32(cboLinea.EditValue);
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Convert.ToInt32(cboFamilia.EditValue);
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = "";
                rep.Parameters["parameter7"].Visible = false;
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
                rep.Parameters["parameter5"].Value = Convert.ToInt32(cboLinea.EditValue);
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Convert.ToInt32(cboFamilia.EditValue);
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = "";
                rep.Parameters["parameter7"].Visible = false;
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                navigationFrame.SelectedPageIndex = 1;
            }
        }

        private void ImprimirxCliente()
        {
            
            UtilidadmarginalporclienteDesigner rep = new UtilidadmarginalporclienteDesigner();

            if (cboUen.Text != "Todos")
                rep.FilterString = "[uen]=" + cboUen.EditValue.ToString();
            if (cboGiro.Text != "Todos")
                rep.FilterString = "[GirosID]=" + cboGiro.EditValue.ToString();
            if (cboTipo.Text != "Todos")
                rep.FilterString = "[ClasificacionPorTipoid]=" + cboTipo.EditValue.ToString();

            if (impDirecto == 1)
            {
                rep.Parameters["parameter1"].Value = 0;         //Emp
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = 0;         //Suc
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = FI;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = FF;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Convert.ToInt32(cboCliente.EditValue);
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Convert.ToInt32(cboAgentes.EditValue);
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = Convert.ToInt32(cboCanaldeventa.EditValue);
                rep.Parameters["parameter7"].Visible = false;
                rep.Parameters["parameter8"].Value = "";
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
                rep.Parameters["parameter5"].Value = Convert.ToInt32(cboCliente.EditValue);
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Convert.ToInt32(cboAgentes.EditValue);
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = Convert.ToInt32(cboCanaldeventa.EditValue);
                rep.Parameters["parameter7"].Visible = false;
                rep.Parameters["parameter8"].Value = "";
                rep.Parameters["parameter8"].Visible = false;
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                navigationFrame.SelectedPageIndex = 1;
            }
        }

        private void bbiRegresarImp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            cboCliente.Enabled = true;
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void cboLinea_EditValueChanged(object sender, EventArgs e)
        {
            CargaFamilias();
        }

        private void cboFamilia_EditValueChanged(object sender, EventArgs e)
        {
            CargaSubFamilias();
        }

        private void cboNiveldeinfor_EditValueChanged(object sender, EventArgs e)
        {
            strNiveldeinformacion = cboNiveldeinfor.EditValue.ToString();
            cboCliente.Visible = false; lblcliente.Visible = false;
            cboAgentes.Visible = false; lblAgente.Visible = false;
            cboCanaldeventa.Visible = false; lblCanaldeventa.Visible = false;
            cboFamilia.Visible = false; lblfam.Visible = false;
            cboSubFamilia.Visible = false; lblSubFam.Visible = false;
            cboLinea.Visible = false; lblLinea.Visible = false;
            switch (strNiveldeinformacion)
            {
                case "F":
                    cboCliente.Visible = true; lblcliente.Visible = true;
                    cboAgentes.Visible = true; lblAgente.Visible = true;
                    cboCanaldeventa.Visible = true; lblCanaldeventa.Visible = true;
                    cboGiro.Visible = true;
                    lblGiro.Visible = true;
                    cboTipo.Visible = true;
                    lblTipo.Visible = true;
                    cboUen.Visible = true;
                    lblUen.Visible = true;
                    
                    break;
                case "FM-ART":
                    cboFamilia.Visible = true; lblfam.Visible = true;
                    cboLinea.Visible = true; lblLinea.Visible = true;
                    cboSubFamilia.Visible = true; lblSubFam.Visible = true;
                    cboGiro.Visible = false;
                    lblGiro.Visible = false;
                    cboTipo.Visible = false;
                    lblTipo.Visible = false;
                    cboUen.Visible = false;
                    lblUen.Visible = false;
                    break;
                case "C":
                    cboCliente.Visible = true; lblcliente.Visible = true;
                    cboAgentes.Visible = true; lblAgente.Visible = true;
                    cboCanaldeventa.Visible = true; lblCanaldeventa.Visible = true;
                    cboGiro.Visible = true;
                    lblGiro.Visible = true;
                    cboTipo.Visible = true;
                    lblTipo.Visible = true;
                    cboUen.Visible = true;
                    lblUen.Visible = true;
                    break;
            }
        }
    }
}