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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;

namespace VisualSoftErp.Operacion.Compras.Formas
{
    public partial class AuxiliardecomprasExp : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string Origen = string.Empty;
        int grupo = 0;
        string nombregrupo=string.Empty;
        public AuxiliardecomprasExp()
        {
            InitializeComponent();
            txtAño.Text = DateTime.Now.Year.ToString();
            txtMes.Text = DateTime.Now.Month.ToString();
            CargaCombos();
            gridView2.OptionsView.ShowAutoFilterRow = true;

            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView2.OptionsBehavior.ReadOnly = true;
            gridView2.OptionsBehavior.Editable = false;

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
              
        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAuxGpo";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            clg.strGridLayout = "gridAuxArt";
            result = clg.SaveGridLayout(gridView2);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            this.Close();
        }

        private void bbiProcesar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Origen = "1";
            Procesar(1);
            bbiArtsConsmo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navigationFrame.SelectedPageIndex = 0;
            gridView1.Focus();
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Proveedores";
            cboProveedores.Properties.ValueMember = "Clave";
            cboProveedores.Properties.DisplayMember = "Des";
            cboProveedores.Properties.DataSource = cl.CargaCombos();
            cboProveedores.Properties.ForceInitialize();
            cboProveedores.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
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
            cboProveedores.Properties.Columns["Retisr"].Visible = false;
            cboProveedores.Properties.Columns["Retiva"].Visible = false;
            cboProveedores.Properties.Columns["MonedasID"].Visible = false;
            cboProveedores.Properties.Columns["Contacto"].Visible = false;
            cboProveedores.ItemIndex = 0;
        }

        private void Procesar(int Op)
        {
            try
            {
                string result = string.Empty;

                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtAño.Text))
                {
                    MessageBox.Show("Teclee el año");
                    return;
                }
                if (!clg.esNumerico(txtMes.Text))
                {
                    MessageBox.Show("Teclee el mes");
                    return;
                }

                DateTime fecha;
                result = clg.UltimoDiaDelMes(Convert.ToInt32(txtAño.Text), Convert.ToInt32(txtMes.Text));
                if (result != "OK")
                {
                    MessageBox.Show(result);
                    return;
                }

                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Newsa.net","Generando informe...");

                fecha = clg.fechaUltimoDiadelMes;

                ComprasCL cl = new ComprasCL();
                cl.intProveedoresID = Convert.ToInt32(cboProveedores.EditValue);
                cl.intAño = Convert.ToInt32(txtAño.Text);
                cl.intMes = Convert.ToInt32(txtMes.Text);
                cl.fFecha = fecha;

                if (cl.intProveedoresID == 0)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    MessageBox.Show("Seleccione el proveedor y cambie de campo");
                    return;
                }

                gridControl1.DataSource = null;
                gridControl2.DataSource = null;

                if (Op==1)
                {
                    result= cl.AuxiliarImportaciones(Op);
                    if (result=="OK")
                    {
                        gridControl1.DataSource = cl.dtResult;
                        CalculaDatos(gridView1);
                        clg.strGridLayout = "gridAuxGpo";
                        clg.restoreLayout(gridView1);
                    }
                    else
                    {
                        MessageBox.Show(result);
                    }
                    
                }                    
                else
                {
                    result = cl.AuxiliarImportaciones(Op);
                    if (result == "OK")
                    {
                        gridControl2.DataSource = cl.AuxiliarImportaciones(Op);
                        CalculaDatos(gridView2);
                        clg.strGridLayout = "gridAuxArt";
                        clg.restoreLayout(gridView2);
                    }
                    else
                    {
                        MessageBox.Show(result);
                    }

                }

                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } //Procesar

        private void CalculaDatos(GridView gv)
        {
            try
            {
                ComprasCL cl = new ComprasCL();
                cl.intProveedoresID = Convert.ToInt32(cboProveedores.EditValue);
                string result = cl.ComprasAuxiliarDatosProveedor();
                if (result!="OK")
                {
                    MessageBox.Show("No se pudieron leer los días del proveedor");
                    return;
                }

                int DiasTotalesProv = cl.intDiasSurtidoProv + cl.intDiasTrasladoProv + cl.intDiasStockProv;
                int DiasTotalesGrupo = 0;
                Decimal MaterialaSolicitar = 0;
                int Mesesdeconsumo = cl.intMesesdeconsumoProv;
                Decimal Consumo = 0;
                Decimal Diferencia = 0;
                Decimal OC = 0;
                Decimal Existencia = 0;
                Decimal Pedidossinfacturar = 0;

                Decimal totalExistencia = 0;
                Decimal Reorden;
                for (int i = 0; i <= gv.RowCount - 1; i++)
                {
                    if (gv.GetRowCellValue(i, "OC") == null)
                        OC = 0;
                    else
                        OC = Convert.ToInt32(gv.GetRowCellValue(i, "OC"));

                    if (gv.GetRowCellValue(i, "Existencia") == null)
                        Existencia = 0;
                    else
                        Existencia = Convert.ToInt32(gv.GetRowCellValue(i, "Existencia"));

                    if (gv.GetRowCellValue(i, "Pedidossinfacturar") == null)
                        Pedidossinfacturar = 0;
                    else
                        Pedidossinfacturar = Convert.ToInt32(gv.GetRowCellValue(i, "Pedidossinfacturar"));

                    totalExistencia = OC + Existencia - Pedidossinfacturar;


                    if (gv.GetRowCellValue(i, "Diasstock") == null)
                        DiasTotalesGrupo = 0;
                    else
                        DiasTotalesGrupo = Convert.ToInt32(gv.GetRowCellValue(i, "Diasstock"));

                    if (gv.GetRowCellValue(i, "Consumo") == null)
                        Consumo = 0;
                    else
                        Consumo = Convert.ToInt32(gv.GetRowCellValue(i, "Consumo"));

                    Reorden = (Consumo/30) * (DiasTotalesProv + DiasTotalesGrupo);

                    if (totalExistencia > Reorden)
                        MaterialaSolicitar = 0;
                    else
                        MaterialaSolicitar = Reorden + (Mesesdeconsumo * Consumo) - totalExistencia;

                    if (Consumo > 0)
                    {
                        Diferencia = (totalExistencia - Reorden);
                        Diferencia = Diferencia / Consumo;
                        Diferencia = Diferencia * 30;
                    }
                        

                    gv.FocusedRowHandle = i;
                    gv.SetFocusedRowCellValue("TotalExistencia", totalExistencia);
                    gv.SetFocusedRowCellValue("Diferencia", Diferencia);
                    gv.SetFocusedRowCellValue("Reorden", Reorden);
                    gv.SetFocusedRowCellValue("Sugerido", MaterialaSolicitar);


                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Calcula datos:" + ex.Message);
            }
        }

        private void bbiPrevio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Origen=="1")
            {
                gridView1.OptionsView.ShowViewCaption = true;
                gridView1.ViewCaption = "AUXILIAR DE COMPRAS " + cboProveedores.Text;
                gridControl1.ShowRibbonPrintPreview();
            }
            else
            {
                gridView2.OptionsView.ShowViewCaption = true;
                gridView2.ViewCaption = "AUXILIAR DE COMPRAS POR ARTICULO " + cboProveedores.Text;
                gridControl2.ShowRibbonPrintPreview();
            }
           
        }

        private void bbiPorArticulo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Origen = "2";
            Procesar(2);
            bbiArtsConsmo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 1;

            bbiProcesar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiPorArticulo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiProcesar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiPorArticulo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex =0;
        }

        private void gridView1_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            PrintingSystemBase pb = e.PrintingSystem as PrintingSystemBase;
            pb.PageSettings.Landscape = true;
        }

        private void gridView2_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            PrintingSystemBase pb = e.PrintingSystem as PrintingSystemBase;
            pb.PageSettings.Landscape = true;
        }

        private void bbiArtsConsmo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string result = string.Empty;

                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtAño.Text))
                {
                    MessageBox.Show("Teclee el año");
                    return;
                }
                if (!clg.esNumerico(txtMes.Text))
                {
                    MessageBox.Show("Teclee el mes");
                    return;
                }

                gridView3.OptionsView.ShowViewCaption = true;
                gridView3.ViewCaption = "GRUPO " + grupo.ToString() + " " + nombregrupo;

                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Newsa.net", "Generando informe...");
                ComprasCL cl = new ComprasCL();
                cl.intAño = Convert.ToInt32(txtAño.Text);
                cl.intGrupo = grupo;
                gridControl3.DataSource = cl.Articulosdeungrupoconconsumo();
                navigationFrame.SelectedPageIndex = 2;
                gridView3.Focus();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                Origen = "3";


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            grupo = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "GpoID"));
            nombregrupo = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Material").ToString();
        }
    }
}