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
    public partial class Ventasporclientearticulo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string strNiveldeinformacion;
        int impDirecto = 0;
        DateTime FI, FF;
        public Ventasporclientearticulo()
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

            cl.strTabla = "Clientesrep";
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

            cl.strTabla = "Familias";
            src.DataSource = cl.CargaCombos();
            cboFamilia.Properties.ValueMember = "Clave";
            cboFamilia.Properties.DisplayMember = "Des";
            cboFamilia.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboFamilia.Properties.ForceInitialize();
            cboFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilia.Properties.PopulateColumns();
            cboFamilia.Properties.Columns["Clave"].Visible = false;
            cboFamilia.ItemIndex = 0;

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

            //cboArticulo.Properties.NullText = "Seleccione un articulo";

        }

        private string Valida()
        {


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

            if (cboFamilia.EditValue == null)
            {
                return "El campo familia no puede ir vacio";
            }

            if (cboLinea.EditValue == null)
            {
                return "El campo linea de venta no puede ir vacio";
            }


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

            return "OK";
        }

        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Generando informe...");

            FI = DateTime.Now;
            FF = DateTime.Now;
            cboCliente.Enabled = false;
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

                VentasporclientearticulosDesigner rep = new VentasporclientearticulosDesigner();
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
                    rep.Parameters["parameter8"].Value = Convert.ToInt32(cboLinea.EditValue);
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = Convert.ToInt32(cboFamilia.EditValue);
                    rep.Parameters["parameter9"].Visible = false;
                    rep.Parameters["parameter10"].Value = Convert.ToInt32(cboArticulo.EditValue);
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
                    rep.Parameters["parameter8"].Value = Convert.ToInt32(cboLinea.EditValue);
                    rep.Parameters["parameter8"].Visible = false;
                    rep.Parameters["parameter9"].Value = Convert.ToInt32(cboFamilia.EditValue);
                    rep.Parameters["parameter9"].Visible = false;
                    rep.Parameters["parameter10"].Value = Convert.ToInt32(cboArticulo.EditValue);
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
            cboCliente.Enabled = true;
        }

        private void cboLinea_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //if (Convert.ToInt32(cboLinea.EditValue) == 0) { }
                //else
                //{
                    BindingSource src = new BindingSource();
                    combosCL cl = new combosCL();
                    globalCL clg = new globalCL();
                    cl.strTabla = "FamiliaxLinea";
                    cl.intClave = Convert.ToInt32(cboLinea.EditValue);
                    src.DataSource = cl.CargaCombosCondicion();
                    cboFamilia.Properties.ValueMember = "Clave";
                    cboFamilia.Properties.DisplayMember = "Des";
                    cboFamilia.Properties.DataSource = clg.AgregarOpcionTodos(src);
                    cboFamilia.Properties.ForceInitialize();
                    cboFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                    cboFamilia.Properties.PopulateColumns();
                    cboFamilia.Properties.Columns["Clave"].Visible = false;
                    cboFamilia.ItemIndex = 0;

                    //cl.strTabla = "ArticulosxFamilia";
                    //cl.intClave = Convert.ToInt32(cboLinea.EditValue);
                    //cboArticulo.Properties.ValueMember = "Clave";
                    //cboArticulo.Properties.DisplayMember = "Des";
                    //cboArticulo.Properties.DataSource = cl.CargaCombosCondicion();
                    //cboArticulo.Properties.ForceInitialize();
                    //cboArticulo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                    //cboArticulo.Properties.PopulateColumns();
                    //cboArticulo.Properties.Columns["Clave"].Visible = false;
                    //cboArticulo.ItemIndex = 0;
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("cboLinea: " + ex);
            }
        }

        private void cboFamilia_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                
                
                    BindingSource src = new BindingSource();
                    combosCL cl = new combosCL();
                    globalCL clg = new globalCL();
                    cl.strTabla = "ArticulosxFamilia";
                    cl.intClave = Convert.ToInt32(cboFamilia.EditValue);
                    src.DataSource = cl.CargaCombosCondicion();
                    cboArticulo.Properties.ValueMember = "Clave";
                    cboArticulo.Properties.DisplayMember = "Des";
                    cboArticulo.Properties.DataSource = cl.CargaCombosCondicion();
                    cboArticulo.Properties.ForceInitialize();
                    cboArticulo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                    cboArticulo.Properties.PopulateColumns();
                    cboArticulo.Properties.Columns["Clave"].Visible = false;
                    cboArticulo.ItemIndex = 0;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("cboFamilia: " + ex);
            }
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

       
    }
}