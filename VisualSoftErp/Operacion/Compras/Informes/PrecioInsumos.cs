using DevExpress.Pdf.Native.BouncyCastle.Ocsp;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Compras.Designers;
using VisualSoftErp.Operacion.Compras.Formas;
using VisualSoftErp.Operacion.CxP.Informes;

namespace VisualSoftErp.Operacion.Compras.Informes
{
    public partial class PrecioInsumos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public PrecioInsumos()
        {
            InitializeComponent();
            CargarCombos();
            tpFechaCorte.EditValue = DateTime.Now;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        
        private void bbiCerrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiRegresar_ItemClick(object sender, ItemClickEventArgs e)
        {
            navigationFrame1.SelectedPageIndex = 0;
            ribbon.MergeOwner.SelectedPage = ribbon.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
        }

        private void CargarCombos()
        {
            globalCL global = new globalCL();
            combosCL cl = new combosCL();
            BindingSource src = new BindingSource();

            #region COMBO LINEAS
            cl.strTabla = "Lineas";
            src.DataSource = cl.CargaCombos();
            cboLinea.Properties.DataSource = global.AgregarOpcionTodos(src);
            cboLinea.Properties.ValueMember = "Clave";
            cboLinea.Properties.DisplayMember = "Des";
            cboLinea.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboLinea.Properties.ForceInitialize();
            cboLinea.Properties.PopulateColumns();
            cboLinea.Properties.Columns["Clave"].Visible = false;
            cboLinea.Properties.NullText = "Seleccione una línea de artículos";
            #endregion COMBO LINEAS

            #region COMBO FAMILIAS
            cl.strTabla = "FamiliasLineas";
            cl.iCondicion = Convert.ToInt32(cboLinea.EditValue);
            src.DataSource = cl.CargaCombos();
            cboFamilia.Properties.DataSource = global.AgregarOpcionTodos(src);
            cboFamilia.Properties.ValueMember = "Clave";
            cboFamilia.Properties.DisplayMember = "Des";
            cboFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilia.Properties.ForceInitialize();
            cboFamilia.Properties.PopulateColumns();
            cboFamilia.Properties.Columns["Clave"].Visible = false;
            cboFamilia.Properties.NullText = "Seleccione una familia de artículos";
            cboFamilia.EditValue = 0;
            cboFamilia.Enabled = false;
            #endregion COMBO FAMILIAS

            #region COMBO SUBFAMILIAS
            cl.strTabla = "SubfamiliasXfamilias";
            cl.iCondicion = Convert.ToInt32(cboFamilia.EditValue);
            src.DataSource = cl.CargaCombos();
            cboSubfamilia.Properties.DataSource = global.AgregarOpcionTodos(src);
            cboSubfamilia.Properties.ValueMember = "Clave";
            cboSubfamilia.Properties.DisplayMember = "Des";
            cboSubfamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSubfamilia.Properties.ForceInitialize();
            cboSubfamilia.Properties.PopulateColumns();
            cboSubfamilia.Properties.Columns["Clave"].Visible = false;
            cboSubfamilia.Properties.NullText = "Seleccione una subfamilia de artículos";
            cboSubfamilia.EditValue = 0;
            cboSubfamilia.Enabled = false;
            #endregion COMBO SUBFAMILIAS
        }

        private void cboLinea_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;
            DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

            if(row != null)
            {
                if (Convert.ToInt32(row["Clave"]) != 0)
                {
                    globalCL global = new globalCL();
                    combosCL cl = new combosCL();
                    BindingSource src = new BindingSource();

                    cl.strTabla = "FamiliasLineas";
                    cl.iCondicion = Convert.ToInt32(row["Clave"]);
                    src.DataSource = cl.CargaCombos();
                    cboFamilia.Properties.DataSource = global.AgregarOpcionTodos(src);
                    cboFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                    cboFamilia.Properties.ForceInitialize();
                    cboFamilia.Properties.PopulateColumns();
                    cboFamilia.Properties.Columns["Clave"].Visible = false;
                    cboFamilia.Properties.NullText = "Seleccione una familia de artículos";
                    cboFamilia.EditValue = 0;
                    cboFamilia.Enabled = true;

                    cl.strTabla = "SubfamiliasXfamilias";
                    cl.iCondicion = Convert.ToInt32(cboFamilia.EditValue);
                    src.DataSource = cl.CargaCombos();
                    cboSubfamilia.Properties.DataSource = global.AgregarOpcionTodos(src);
                    cboSubfamilia.EditValue = 0;
                    cboSubfamilia.Enabled = false;
                }
                else
                {
                    cboFamilia.EditValue = 0;
                    cboFamilia.Enabled = false;
                    cboSubfamilia.EditValue = 0;
                    cboSubfamilia.Enabled = false;
                }
            }
        }

        private void cboFamilia_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;
            DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

            if(row != null)
            {
                if (Convert.ToInt32(row["Clave"]) != 0)
                {
                    globalCL global = new globalCL();
                    combosCL cl = new combosCL();
                    BindingSource src = new BindingSource();

                    cl.strTabla = "SubfamiliasXfamilias";
                    cl.iCondicion = Convert.ToInt32(cboFamilia.EditValue);
                    src.DataSource = cl.CargaCombos();
                    cboSubfamilia.Properties.DataSource = global.AgregarOpcionTodos(src);
                    cboSubfamilia.Enabled = true;
                }
                else
                {
                    cboSubfamilia.EditValue = 0;
                    cboSubfamilia.Enabled = false;
                }
            }
        }

        private string Validar()
        {
            string result = "OK";
            if (cboLinea.EditValue == null)
                result = "Por favor, escoja una línea de artículos";

            if (cboFamilia.EditValue == null)
                result = "Por favor, escoja una familia de artículos";

            if (cboSubfamilia.EditValue == null)
                result = "Por favor, escoja una subfamilia de artículos";

            return result;
        }

        private void bbiPrevio_ItemClick(object sender, ItemClickEventArgs e)
        {
            string result = Validar();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Reporte()
        {
            globalCL globalCL = new globalCL();
            string results = globalCL.Datosdecontrol();
            int impDirecto = results == "OK" ? globalCL.iImpresiondirecta : 0;

            if(impDirecto == 1)
            {
                PrecioInsumosRepDesigner rep = new PrecioInsumosRepDesigner();
                rep.Parameters["parameter1"].Value = cboLinea.EditValue;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = cboFamilia.EditValue;
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = cboSubfamilia.EditValue;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = tpFechaCorte.EditValue;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = swActivo.IsOn == true ? 1 : 0;
                rep.Parameters["parameter5"].Visible = false;

                ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                rpt.Print();
            }
            else
            {
                PrecioInsumosRepDesigner rep = new PrecioInsumosRepDesigner();
                rep.Parameters["parameter1"].Value = cboLinea.EditValue;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = cboFamilia.EditValue;
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = cboSubfamilia.EditValue;
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = tpFechaCorte.EditValue;
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = swActivo.IsOn == true ? 1 : 0;
                rep.Parameters["parameter5"].Visible = false;

                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbon.MergeOwner.SelectedPage = ribbon.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                navigationFrame1.SelectedPageIndex = 1;
            }
        }
    }
}