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
using VisualSoftErp.Operacion.Compras.Clases;
using VisualSoftErp.Operacion.Compras.Designers;
using DevExpress.XtraReports.UI;

namespace VisualSoftErp.Operacion.Compras.Informes
{
    public partial class Relacionordenesdecompras : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Proveedores = 0;
        int Articulos = 0;
        int Estatus = 0;
        int Fechaatrasada = 0;

        public Relacionordenesdecompras()
        {
            InitializeComponent();
            Cargacombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            List<ClaseGenricaCL> ListadoEstatus = new List<ClaseGenricaCL>();
            ListadoEstatus.Add(new ClaseGenricaCL() { Clave = "0", Des = "Todos" });
            ListadoEstatus.Add(new ClaseGenricaCL() { Clave = "1", Des = "Por surtir" });
            ListadoEstatus.Add(new ClaseGenricaCL() { Clave = "2", Des = "Parcialmente surtida" });
            ListadoEstatus.Add(new ClaseGenricaCL() { Clave = "3", Des = "Surtidas" });

            BindingSource src = new BindingSource();


            cboEstatus.Properties.ValueMember = "Clave";
            cboEstatus.Properties.DisplayMember = "Des";
            cboEstatus.Properties.DataSource = ListadoEstatus;
            cboEstatus.Properties.ForceInitialize();
            cboEstatus.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboEstatus.Properties.ForceInitialize();
            cboEstatus.Properties.PopulateColumns();
            cboEstatus.Properties.Columns["Clave"].Visible = false;
            cboEstatus.Properties.NullText = "Seleccione un estatus";

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
            cboProveedores.Properties.Columns["Via"].Visible = false;
            cboProveedores.Properties.Columns["Tiempodeentrega"].Visible = false;
            cboProveedores.Properties.Columns["Diastraslado"].Visible = false;
            cboProveedores.Properties.Columns["Lab"].Visible = false;
            cboProveedores.Properties.Columns["BancosID"].Visible = false;
            cboProveedores.Properties.Columns["Cuentabancaria"].Visible = false;
            cboProveedores.Properties.Columns["C_Formapago"].Visible = false;
            cboProveedores.Properties.Columns["MonedasID"].Visible = false;
            cboProveedores.Properties.Columns["Retiva"].Visible = false;
            cboProveedores.Properties.Columns["Retisr"].Visible = false;
            cboProveedores.Properties.NullText = "Seleccione un Proveedor";

            cboArticulos.Properties.ValueMember = "Clave";
            cboArticulos.Properties.DisplayMember = "Des";
            cl.strTabla = "Articulos";
            src.DataSource = cl.CargaCombos();
            cboArticulos.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboArticulos.Properties.ForceInitialize();
            cboArticulos.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulos.Properties.ForceInitialize();
            cboArticulos.Properties.PopulateColumns();
            cboArticulos.Properties.Columns["Clave"].Visible = false;
            cboArticulos.Properties.NullText = "Seleccione un Articulo";

           
        }

        private void Reporte()
        {
            try
            {
                Estatus = Convert.ToInt32(cboEstatus.EditValue);
                Proveedores = Convert.ToInt32(cboProveedores.EditValue);
                Articulos = Convert.ToInt32(cboArticulos.EditValue);
                Fechaatrasada = Convert.ToInt32(chkFechaatrasada.Checked);


                int porLlegar = chkPorLlegar.Checked ? 1 : 0;

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


                RelaciondeordenesdecompraDesigner rep = new RelaciondeordenesdecompraDesigner();
                if (impDirecto == 1)
                {

                    rep.Parameters["parameter1"].Value = porLlegar;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Proveedores;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Estatus;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = Articulos;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = Fechaatrasada;
                    rep.Parameters["parameter8"].Visible = false;

                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = porLlegar;
                    rep.Parameters["parameter1"].Visible = false;
                    rep.Parameters["parameter2"].Value = 0;
                    rep.Parameters["parameter2"].Visible = false;
                    rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                    rep.Parameters["parameter3"].Visible = false;
                    rep.Parameters["parameter4"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                    rep.Parameters["parameter4"].Visible = false;
                    rep.Parameters["parameter5"].Value = Proveedores;
                    rep.Parameters["parameter5"].Visible = false;
                    rep.Parameters["parameter6"].Value = Estatus;
                    rep.Parameters["parameter6"].Visible = false;
                    rep.Parameters["parameter7"].Value = Articulos;
                    rep.Parameters["parameter7"].Visible = false;
                    rep.Parameters["parameter8"].Value = Fechaatrasada;
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

        private void bbiPrevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Generando informe", "Espere por favor...");
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiRegresarImpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }
    }
}