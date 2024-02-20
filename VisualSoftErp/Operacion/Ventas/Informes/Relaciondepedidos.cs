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
    public partial class Relaciondepedidos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string strNiveldeinformacion;
        int impDirecto = 0;
        DateTime FI, FF;
        public Relaciondepedidos()
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
            tipoL.Add(new tipoCL() { Clave = 0, Des = "Todos" });
            tipoL.Add(new tipoCL() { Clave = 1, Des = "Detenido CxC" });
            tipoL.Add(new tipoCL() { Clave = 2, Des = "Sin existencia" });
            tipoL.Add(new tipoCL() { Clave = 3, Des = "Bajo costo" });
            tipoL.Add(new tipoCL() { Clave = 4, Des = "Listo para surtir" });
            tipoL.Add(new tipoCL() { Clave = 5, Des = "Existencia parcial" });
            tipoL.Add(new tipoCL() { Clave = 6, Des = "Surtido sin facturar" });
            tipoL.Add(new tipoCL() { Clave = 7, Des = "Facturado" });
            tipoL.Add(new tipoCL() { Clave = 8, Des = "Entregado" });
            tipoL.Add(new tipoCL() { Clave = 9, Des = "Depurado" });
            tipoL.Add(new tipoCL() { Clave = 10, Des = "Por cliente" });
            cboStatus.Properties.ValueMember = "Clave";
            cboStatus.Properties.DisplayMember = "Des";
            cboStatus.Properties.DataSource = tipoL;
            cboStatus.Properties.ForceInitialize();
            cboStatus.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboStatus.Properties.PopulateColumns();
            cboStatus.Properties.Columns["Clave"].Visible = false;
            cboStatus.ItemIndex = 0;

            cl.strTabla = "ClientesRep";
            src.DataSource = cl.CargaCombos();
            cboCliente.Properties.ValueMember = "Clave";
            cboCliente.Properties.DisplayMember = "Des";
            cboCliente.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCliente.Properties.PopulateColumns();
            cboCliente.Properties.Columns["Clave"].Visible = false; //con esta propiedad puedo ocultar campos en el cbo
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
            cboAgentes.Properties.Columns["Encabezado"].Visible = false;
            cboAgentes.Properties.Columns["Piedepagina"].Visible = false;
            cboAgentes.Properties.Columns["Email"].Visible = false;

            cboAgentes.ItemIndex = 0;

            //cl.strTabla = "Agentes";
            //src.DataSource = cl.CargaCombos();
            //cboAgentes.Properties.ValueMember = "Clave";
            //cboAgentes.Properties.DisplayMember = "Des";
            //cboAgentes.Properties.DataSource = clg.AgregarOpcionTodos(src);
            //cboAgentes.Properties.ForceInitialize();
            //cboAgentes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            //cboAgentes.Properties.PopulateColumns();
            //cboAgentes.Properties.Columns["Clave"].Visible = false;
            //cboAgentes.ItemIndex = 0;

            //cl.strTabla = "Agentes";
            //src.DataSource = cl.CargaCombos();
            //cboAgentes.Properties.ValueMember = "Clave";
            //cboAgentes.Properties.DisplayMember = "Des";
            //cboAgentes.Properties.DataSource = clg.AgregarOpcionTodos(src);
            //cboAgentes.Properties.ForceInitialize();
            //cboAgentes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            //cboAgentes.Properties.PopulateColumns();
            //cboAgentes.Properties.Columns["Clave"].Visible = false;
            //cboAgentes.ItemIndex = 0;

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

            if (cboStatus.EditValue == null)
            {
                return "El campo Canales de venta no puede ir vacio";
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
                
                int status = swPorSurtir.IsOn==true ? 20 : Convert.ToInt32(cboStatus.EditValue);



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

                if (swRoD.IsOn == false)
                {
                    RelaciondepedidosDesigner rep = new RelaciondepedidosDesigner();
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
                        rep.Parameters["parameter7"].Value = Convert.ToInt32(cboStatus.EditValue);
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
                        rep.Parameters["parameter5"].Value = Convert.ToInt32(cboCliente.EditValue);
                        rep.Parameters["parameter5"].Visible = false;
                        rep.Parameters["parameter6"].Value = Convert.ToInt32(cboAgentes.EditValue);
                        rep.Parameters["parameter6"].Visible = false;
                        rep.Parameters["parameter7"].Value = Convert.ToInt32(cboStatus.EditValue);
                        rep.Parameters["parameter7"].Visible = false;
                        documentViewer1.DocumentSource = rep;
                        rep.CreateDocument();
                        ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                        navigationFrame.SelectedPageIndex = 1;
                    }
                }
                else
                {
                    PedidosRelacionDetalladoDesigner rep = new PedidosRelacionDetalladoDesigner();
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
                        rep.Parameters["parameter7"].Value = Convert.ToInt32(cboStatus.EditValue);
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
                        rep.Parameters["parameter5"].Value = Convert.ToInt32(cboCliente.EditValue);
                        rep.Parameters["parameter5"].Visible = false;
                        rep.Parameters["parameter6"].Value = Convert.ToInt32(cboAgentes.EditValue);
                        rep.Parameters["parameter6"].Visible = false;
                        rep.Parameters["parameter7"].Value = Convert.ToInt32(cboStatus.EditValue);
                        rep.Parameters["parameter7"].Visible = false;
                        documentViewer1.DocumentSource = rep;
                        rep.CreateDocument();
                        ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                        navigationFrame.SelectedPageIndex = 1;
                    }

                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void swRoD_EditValueChanged(object sender, EventArgs e)
        {
            //if (swRoD.IsOn == true)
            //{
            //    labelControl2.Text = "Resumen";
            //}
            //else
            //{
            //    labelControl2.Text = "Detallado";
            //}
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

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiRegresarImp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            cboCliente.Enabled = true;
            navigationFrame.SelectedPageIndex = 0;
        }

        public class tipoCL
        {
            public int Clave { get; set; }
            public string Des { get; set; }
        }
    }
}