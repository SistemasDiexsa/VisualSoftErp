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
using VisualSoftErp.Operacion.Ventas.Designers;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Ventas.Informes
{

    public partial class Ventasporcliente : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Cliente;
        int Agentes;
        int CanalVents;
        int Incluirclientespresu;
        int Nivelinfo;

        public Ventasporcliente()
        {
            InitializeComponent();
            Cargacombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
       

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void Reporte()
        {
            try
            {
                 Cliente = Convert.ToInt32(cboCliente.EditValue);
                 Agentes = Convert.ToInt32(cboAgente.EditValue);
                 CanalVents = Convert.ToInt32(cboCanalventa.EditValue);
                 Incluirclientespresu = Convert.ToInt32(swtIncluirclientes.EditValue);
                 Nivelinfo = Convert.ToInt32(rdoNivelinformacion.EditValue);

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

                switch (Convert.ToInt16(rdoNivelinformacion.EditValue))
                {
                    case 1:
                        ImpresionVentasDetalleClienteResumen(impDirecto);
                        break;
                    case 2:
                        ImpresionVentasClienteDetalle(impDirecto);
                        break;
                    case 3:
                        ImpresionVentasDetalleClienteConArticulos(impDirecto);
                        break;
                    default:
                        break;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void ImpresionVentasDetalleClienteResumen(int ImpDirecto)
        {
            VentasDesignerVentasporclienteResumen rep = new VentasDesignerVentasporclienteResumen();
            if (ImpDirecto == 1)
            {

                rep.Parameters["parameter1"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Cliente;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Agentes;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Incluirclientespresu;
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = CanalVents;
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = "x";     //Duumy
                rep.Parameters["parameter7"].Visible = false;
                ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                rpt.Print();
                return;
            }
            else
            {
                rep.Parameters["parameter1"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Cliente;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Agentes;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Incluirclientespresu;
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = CanalVents;
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = "x";     //Duumy
                rep.Parameters["parameter7"].Visible = false;
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                navigationFrame.SelectedPageIndex = 1;
            }
        }

        private void ImpresionVentasClienteDetalle(int ImpDirecto)
        {
            VentasDesignerVentasporclienteDetalle rep = new VentasDesignerVentasporclienteDetalle();
            if (ImpDirecto == 1)
            {

                rep.Parameters["parameter1"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Cliente;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Agentes;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = CanalVents;
                rep.Parameters["parameter5"].Visible = false;
                //rep.Parameters["parameter6"].Value = CanalVents;
                //rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter6"].Value = "x";     //Duumy
                rep.Parameters["parameter6"].Visible = false;
                ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                rpt.Print();
                return;
            }
            else
            {
                rep.Parameters["parameter1"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Cliente;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Agentes;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = CanalVents;
                rep.Parameters["parameter5"].Visible = false;
                //rep.Parameters["parameter6"].Value = CanalVents;
                //rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter6"].Value = "x";     //Duumy
                rep.Parameters["parameter6"].Visible = false;
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                navigationFrame.SelectedPageIndex = 1;
            }
        }

        private void ImpresionVentasDetalleClienteConArticulos(int ImpDirecto)
        {
            VentasDesignerVentasporclienteDetalleConArticulos rep = new VentasDesignerVentasporclienteDetalleConArticulos();
            if (ImpDirecto == 1)
            {

                rep.Parameters["parameter1"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Cliente;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Agentes;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = CanalVents;
                rep.Parameters["parameter5"].Visible = false;
                //rep.Parameters["parameter6"].Value = CanalVents;
                //rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter6"].Value = "x";     //Duumy
                rep.Parameters["parameter6"].Visible = false;
                ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                rpt.Print();
                return;
            }
            else
            {
                rep.Parameters["parameter1"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Cliente;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Agentes;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = CanalVents;
                rep.Parameters["parameter5"].Visible = false;
                //rep.Parameters["parameter6"].Value = CanalVents;
                //rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter6"].Value = "x";     //Duumy
                rep.Parameters["parameter6"].Visible = false;
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                navigationFrame.SelectedPageIndex = 1;
            }
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

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
            cboCliente.Properties.Columns["Clave"].Visible = false;
            cboCliente.Properties.Columns["AgentesID"].Visible = false;
            cboCliente.Properties.Columns["Plazo"].Visible = false;
            cboCliente.Properties.Columns["Listadeprecios"].Visible = false;
            cboCliente.Properties.Columns["Exportar"].Visible = false;
            cboCliente.Properties.Columns["cFormapago"].Visible = false;
            cboCliente.Properties.Columns["cMetodopago"].Visible = false;
            cboCliente.Properties.Columns["UsoCfdi"].Visible = false;
            cboCliente.Properties.Columns["PIva"].Visible = false;
            cboCliente.Properties.Columns["PIeps"].Visible = false;
            cboCliente.Properties.Columns["PRetiva"].Visible = false;
            cboCliente.Properties.Columns["PRetIsr"].Visible = false;
            cboCliente.Properties.Columns["EMail"].Visible = false;
            cboCliente.Properties.Columns["BancoordenanteID"].Visible = false;
            cboCliente.Properties.Columns["Cuentaordenante"].Visible = false;
            cboCliente.Properties.Columns["cFormapagoDepositos"].Visible = false;
            cboCliente.Properties.Columns["Moneda"].Visible = false;
            cboCliente.Properties.Columns["DescuentoBase"].Visible = false;
            cboCliente.Properties.Columns["DesctoPP"].Visible = false;
            cboCliente.Properties.Columns["Desglosardescuentoalfacturar"].Visible = false;
            cboCliente.Properties.NullText = "Seleccione un Clientes";

            cboAgente.Properties.ValueMember = "Clave";
            cboAgente.Properties.DisplayMember = "Des";
            cl.strTabla = "Agentes";
            src.DataSource = cl.CargaCombos();
            cboAgente.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAgente.Properties.ForceInitialize();
            cboAgente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgente.Properties.ForceInitialize();
            cboAgente.Properties.PopulateColumns();
            cboAgente.Properties.Columns["Clave"].Visible = false;
            cboAgente.Properties.Columns["Encabezado"].Visible = false;
            cboAgente.Properties.Columns["Piedepagina"].Visible = false;
            cboAgente.Properties.Columns["Email"].Visible = false;
            cboAgente.Properties.NullText = "Seleccione un Agente";

            //Canalesdeventa
            cboCanalventa.Properties.ValueMember = "Clave";
            cboCanalventa.Properties.DisplayMember = "Des";
            cl.strTabla = "Canalesdeventa";
            src.DataSource = cl.CargaCombos();
            cboCanalventa.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboCanalventa.Properties.ForceInitialize();
            cboCanalventa.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCanalventa.Properties.ForceInitialize();
            cboCanalventa.Properties.PopulateColumns();
            cboCanalventa.Properties.Columns["Clave"].Visible = false;
            cboCanalventa.Properties.NullText = "Seleccione un Canal de venta";

            rdoNivelinformacion.SelectedIndex = 0;
        }

        private void bbiVistaprevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Reporte();
        }

        private void bbiRegresarImpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

        private void rdoNivelinformacion_EditValueChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt16(rdoNivelinformacion.EditValue)==1)
            {
              swtIncluirclientes.Enabled = true;
            }else
            {
                swtIncluirclientes.Enabled = false;
            }
        }

        private void rdoNivelinformacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(rdoNivelinformacion.EditValue) == 1)
            {
                swtIncluirclientes.Enabled = true;
            }
            else
            {
                swtIncluirclientes.Enabled = false;
            }
        }
    }
}