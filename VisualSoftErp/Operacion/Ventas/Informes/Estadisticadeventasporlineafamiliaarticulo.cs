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
using VisualSoftErp.Operacion.Compras.Clases;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Ventas.Designers;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;


namespace VisualSoftErp.Operacion.Ventas.Informes
{
    public partial class Estadisticadeventasporlineafamiliaarticulo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Linea = 0;
        int Año = 0;
        int Mes = 0;
        int Familia = 0;
        int Almacen = 0;
        public Estadisticadeventasporlineafamiliaarticulo()
        {
            InitializeComponent();
            Cargacombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            List<ClaseGenricaCL> ListadoMeses = new List<ClaseGenricaCL>();
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "0", Des = "Todos" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "1", Des = "Enero" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "2", Des = "Febrero" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "3", Des = "Marzo" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "4", Des = "Abril" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "5", Des = "Mayo" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "6", Des = "Junio" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "7", Des = "Julio" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "8", Des = "Agosto" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "9", Des = "Septiembre" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "10", Des = "Octubre" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "11", Des = "Noviembre" });
            ListadoMeses.Add(new ClaseGenricaCL() { Clave = "12", Des = "Diciembre" });

            BindingSource src = new BindingSource();
            cboLineas.Properties.ValueMember = "Clave";
            cboLineas.Properties.DisplayMember = "Des";
            cl.strTabla = "Lineas";
            src.DataSource = cl.CargaCombos();
            cboLineas.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboLineas.Properties.ForceInitialize();
            cboLineas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboLineas.Properties.ForceInitialize();
            cboLineas.Properties.PopulateColumns();
            cboLineas.Properties.Columns["Clave"].Visible = false;
            cboLineas.ItemIndex = 0;

            cboFamilias.Properties.ValueMember = "Clave";
            cboFamilias.Properties.DisplayMember = "Des";
            cl.strTabla = "Familias";
            src.DataSource = cl.CargaCombos();
            cboFamilias.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboFamilias.Properties.ForceInitialize();
            cboFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilias.Properties.ForceInitialize();
            cboFamilias.Properties.PopulateColumns();
            cboFamilias.Properties.Columns["Clave"].Visible = false;
            cboFamilias.ItemIndex = 0;

            cboAños.Properties.ValueMember = "Clave";
            cboAños.Properties.DisplayMember = "Des";
            cl.strTabla = "Añosventas";
            src.DataSource = cl.CargaCombos();
            cboAños.Properties.DataSource = src.DataSource;
            cboAños.Properties.ForceInitialize();
            cboAños.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAños.Properties.ForceInitialize();
            cboAños.Properties.PopulateColumns();
            cboAños.Properties.Columns["Clave"].Visible = false;
            cboAños.Properties.NullText = "Seleccione un Año";
            cboAños.ItemIndex = 0;

            cboMeses.Properties.ValueMember = "Clave";
            cboMeses.Properties.DisplayMember = "Des";
            cboMeses.Properties.DataSource = ListadoMeses;
            cboMeses.Properties.ForceInitialize();
            cboMeses.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMeses.Properties.ForceInitialize();
            cboMeses.Properties.PopulateColumns();
            cboMeses.Properties.Columns["Clave"].Visible = false;
            cboMeses.ItemIndex = DateTime.Now.Month;

            cl.strTabla = "Estados";
            cl.intClave = 0;
            cboEstado.Properties.ValueMember = "Clave";
            cboEstado.Properties.DisplayMember = "Des";
            src.DataSource = cl.CargaCombos();
            cboEstado.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboEstado.Properties.ForceInitialize();
            cboEstado.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboEstado.Properties.PopulateColumns();
            cboEstado.Properties.Columns["Clave"].Visible = false;
            cboEstado.ItemIndex = 0;

            cl.strTabla = "Almacenes";
            cl.intClave = 0;
            cboAlmacen.Properties.ValueMember = "Clave";
            cboAlmacen.Properties.DisplayMember = "Des";
            src.DataSource = cl.CargaCombos();
            cboAlmacen.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboAlmacen.Properties.ForceInitialize();
            cboAlmacen.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacen.Properties.PopulateColumns();
            cboAlmacen.Properties.Columns["Clave"].Visible = false;
            cboAlmacen.ItemIndex = 0;


        }

        private void Reporte()
        {
            try
            {

                Linea = Convert.ToInt32(cboLineas.EditValue);
                Familia = Convert.ToInt32(cboFamilias.EditValue);
                Año = Convert.ToInt32(cboAños.EditValue);
                Mes = DateTime.Now.Month;
                Almacen = Convert.ToInt32(cboAlmacen.EditValue);

                int Estado = Convert.ToInt32(cboEstado.EditValue);

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

                EstadisticadeventasporlineafamiliaarticuloDesigner rep = new EstadisticadeventasporlineafamiliaarticuloDesigner();
                rep.Parameters["parameter1"].Value = swUnidades.IsOn ? 1 : 0;
                rep.Parameters["parameter2"].Value = Estado;
                rep.Parameters["parameter3"].Value = Año;
                rep.Parameters["parameter4"].Value = Mes;
                rep.Parameters["parameter5"].Value = Linea;
                rep.Parameters["parameter6"].Value = Familia;
                rep.Parameters["parameter7"].Value = Almacen;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Visible = false;

                if (impDirecto == 1)
                {
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
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
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Generando informe...");
            this.Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiRegresarImpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

        private void cboLineas_EditValueChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(cboLineas.EditValue)==0)
            {
                cboFamilias.ItemIndex = 0;
            }else
            {
                this.LlenaComboFamilias(Convert.ToInt32(cboLineas.EditValue));
            }
        }

        private void LlenaComboFamilias(int IdLinea)
        {
            ClaseGenricaCL cl = new ClaseGenricaCL();
            globalCL gl = new globalCL();
            BindingSource src = new BindingSource();
            src.DataSource=cl.CargaCombosFamiliasByIdLinea(IdLinea);
            cboFamilias.Properties.DataSource = gl.AgregarOpcionTodos(src);
        }
    }
}