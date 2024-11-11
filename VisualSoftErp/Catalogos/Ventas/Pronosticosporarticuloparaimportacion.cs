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

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class Pronosticosporarticuloparaimportacion : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        public BindingList<detalleCL> detalle;
        bool blNuevo;
        public Pronosticosporarticuloparaimportacion()
        {
            InitializeComponent();
            txtA.Text = DateTime.Now.Year.ToString();
            Cargacombos();
            gridViewDetalle.OptionsView.ShowAutoFilterRow = true;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            // COMBO LINEAS
            cboLinea.Properties.ValueMember = "Clave";
            cboLinea.Properties.DisplayMember = "Des";
            cl.strTabla = "Lineas";
            src.DataSource = cl.CargaCombos();
            cboLinea.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboLinea.Properties.ForceInitialize();
            cboLinea.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboLinea.Properties.ForceInitialize();
            cboLinea.Properties.PopulateColumns();
            cboLinea.Properties.Columns["Clave"].Visible = false;
            cboLinea.ItemIndex = 1;

            // COMBO TIPO DE ARTICULO
            cboTipo.Properties.ValueMember = "Clave";
            cboTipo.Properties.DisplayMember = "Des";
            cl.strTabla = "Tiposdearticulo";
            src.DataSource = cl.CargaCombos();
            cboTipo.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboTipo.Properties.ForceInitialize();
            cboTipo.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTipo.Properties.ForceInitialize();
            cboTipo.Properties.PopulateColumns();
            cboTipo.Properties.Columns["Clave"].Visible = false;
            cboTipo.ItemIndex = 0;

            // COMBO FAMILIAS
            cboFamilia.Properties.NullText = "Seleccione una Familia";

            // COMBO MESES
            List<ClaseGenricaCL> ListadoMeses = new List<ClaseGenricaCL>();
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
            cboMeses.Properties.ValueMember = "Clave";
            cboMeses.Properties.DisplayMember = "Des";
            cboMeses.Properties.DataSource = ListadoMeses;
            cboMeses.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMeses.Properties.ForceInitialize();
            cboMeses.Properties.PopulateColumns();
            cboMeses.Properties.Columns["Clave"].Visible = false;
            cboMeses.Properties.Columns["Value"].Visible = false;
            cboMeses.Properties.Columns["Description"].Visible = false;
            cboMeses.ItemIndex = DateTime.Now.Month;
        }

        private void CargaComboFamilias()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            cboFamilia.Properties.ValueMember = "Clave";
            cboFamilia.Properties.DisplayMember = "Des";
            cl.strTabla = "FamiliasLineas";
            cl.iCondicion = Convert.ToInt32(cboLinea.EditValue);
            src.DataSource = cl.CargaCombos();
            cboFamilia.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboFamilia.Properties.ForceInitialize();
            cboFamilia.Properties.PopulateColumns();
            cboFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilia.Properties.Columns["Clave"].Visible = false;
            cboFamilia.EditValue = null;
        }

        private void Nuevo()
        {
            ribbonPageGroup1.Visible = false;
            blNuevo = true;
            Inicialisalista();
            llenarGridDetalle();
            BotonesEdicion();

        }

        private void BotonesEdicion()
        {
            ribbonPageGroup2.Visible = true;
            ribbonPageGroup1.Visible = false;
            ribbonStatusBar.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void LimpiaCajas()
        {
            //cboFamiliasID.EditValue = null;
            //cboSubFamiliasID.EditValue = null;
        }

        public class detalleCL
        {
            public string NomLin { get; set; }
            public string NomFam { get; set; }
            public int ArticulosID { get; set; }
            public string Articulo { get; set; }
            public string NomArt { get; set; }
            public int Ene { get; set; }
            public int Feb { get; set; }
            public int Mar { get; set; }
            public int Abr { get; set; }
            public int May { get; set; }
            public int Jun { get; set; }
            public int Jul { get; set; }
            public int Ago { get; set; }
            public int Sep { get; set; }
            public int Oct { get; set; }
            public int Nov { get; set; }
            public int Dic { get; set; }
            public int Total { get; set; }
            public int Promedio { get; set; }
            public int Proyectado { get; set; }
        }

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;

            gridColumnNomLin.OptionsColumn.ReadOnly = true;
            gridColumnNomLin.OptionsColumn.AllowFocus = false;

            gridColumnNomFam.OptionsColumn.ReadOnly = true;
            gridColumnNomFam.OptionsColumn.AllowFocus = false;

            gridColumnArticulosID.OptionsColumn.ReadOnly = true;
            gridColumnArticulosID.OptionsColumn.AllowFocus = false;

            gridColumnArticulo.OptionsColumn.ReadOnly = true;
            gridColumnArticulo.OptionsColumn.AllowFocus = false;

            gridColumnEne.OptionsColumn.ReadOnly = true;
            gridColumnEne.OptionsColumn.AllowFocus = false;

            gridColumnFeb.OptionsColumn.ReadOnly = true;
            gridColumnFeb.OptionsColumn.AllowFocus = false;

            gridColumnMar.OptionsColumn.ReadOnly = true;
            gridColumnMar.OptionsColumn.AllowFocus = false;

            gridColumnAbr.OptionsColumn.ReadOnly = true;
            gridColumnAbr.OptionsColumn.AllowFocus = false;

            gridColumnMay.OptionsColumn.ReadOnly = true;
            gridColumnMay.OptionsColumn.AllowFocus = false;

            gridColumnJun.OptionsColumn.ReadOnly = true;
            gridColumnJun.OptionsColumn.AllowFocus = false;

            gridColumnJul.OptionsColumn.ReadOnly = true;
            gridColumnJul.OptionsColumn.AllowFocus = false;

            gridColumnAgo.OptionsColumn.ReadOnly = true;
            gridColumnAgo.OptionsColumn.AllowFocus = false;

            gridColumnSep.OptionsColumn.ReadOnly = true;
            gridColumnSep.OptionsColumn.AllowFocus = false;

            gridColumnOct.OptionsColumn.ReadOnly = true;
            gridColumnOct.OptionsColumn.AllowFocus = false;

            gridColumnNov.OptionsColumn.ReadOnly = true;
            gridColumnNov.OptionsColumn.AllowFocus = false;

            gridColumnDic.OptionsColumn.ReadOnly = true;
            gridColumnDic.OptionsColumn.AllowFocus = false;

            gridColumnPromedio.OptionsColumn.ReadOnly = true;
            gridColumnPromedio.OptionsColumn.AllowFocus = false;

            gridColumnProyectado.OptionsColumn.ReadOnly = false;
            gridColumnProyectado.OptionsColumn.AllowFocus = true;

        }

        private void llenarGridDetalle()
        {
            String Result = Valida();
            if (Result != "OK")
            {
                MessageBox.Show(Result);
                return;
            }
            
            try
            {
                MetaporarticuloCL cl = new MetaporarticuloCL();
                int Mes = 0;
                int Año = 0;
                Año= Convert.ToInt32(txtA.Text);
                Mes = Convert.ToInt32(cboMeses.EditValue);


                // PETICION DE RAQUENEL: 
                // SE QUITA ESTA PARTE PARA QUE CARGUE EL PRONOSTICO DE CADA MES Y NO EL DEL MES PASADO
                //if (Mes > 1)
                //    Mes = Mes - 1;
                //else
                //{
                //    Mes = 12;
                //    Año = Año - 1;
                //}

                cl.intEmp = 1;
                cl.intSuc = 0;
                cl.intEje = Año;
                cl.intMes = Mes;
                cl.intLin = Convert.ToInt32(cboLinea.EditValue);
                cl.intFam = Convert.ToInt32(cboFamilia.EditValue);
                cl.intTipo = Convert.ToInt32(cboTipo.EditValue);
                gridControlDetalle.DataSource = cl.MetaporarticuloGridDetalle();

                //Se calcula el promedio
                int promedio = 0;
                int total = 0;
                
                for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++)
                {
                    promedio = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Ene"));
                    promedio = promedio + Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Feb"));
                    promedio = promedio + Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Mar"));
                    promedio = promedio + Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Abr"));
                    promedio = promedio + Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "May"));
                    promedio = promedio + Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Jun"));
                    promedio = promedio + Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Jul"));
                    promedio = promedio + Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Ago"));
                    promedio = promedio + Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Sep"));
                    promedio = promedio + Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Oct"));
                    promedio = promedio + Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Nov"));
                    promedio = promedio + Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Dic"));

                    total = promedio;
                    gridViewDetalle.FocusedRowHandle = i;
                    gridViewDetalle.SetFocusedRowCellValue("Total", total);
                    if (promedio > 0)
                    {
                        promedio = total / Mes;
                        gridViewDetalle.SetFocusedRowCellValue("Promedio", promedio);
                    }                                            
                }

                globalCL clg = new globalCL();
                clg.strGridLayout = "gridMetaporarticuloDetalle";
                clg.restoreLayout(gridViewDetalle);

                gridViewDetalle.OptionsView.ShowViewCaption = true;

                gridViewDetalle.ViewCaption = "PRONOSTICO PARA " + clg.NombreDeMes(Convert.ToInt32(cboMeses.EditValue), 0) + " DEL " + txtA.Text;
                gridViewDetalle.FocusedRowHandle = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }
        }

        private string Valida()
        {
            if (cboLinea.EditValue == null)
            {
                return "El campo Linea no puede ir vacio";
            }
            if (cboFamilia.EditValue == null)
            {
                return "El campo Familia no puede ir vacio";
            }
            if (cboTipo.EditValue == null)
            {
                return "El campo Tipo no puede ir vacio";
            }
            return "OK";
        }

        private void Guardar()
        {
            try
            {
                String Result = Valida();
                if (Result != "OK")
                {
                    MessageBox.Show(Result);
                    return;
                }

                int intM = Convert.ToInt32(cboMeses.EditValue);
                int intA = Convert.ToInt32(txtA.Text);
                int intArticulosID = 0;
                string dato = "";
                decimal dENEP;
                decimal dFEBP;
                decimal dMARP;
                decimal dABRP;
                decimal dMAYP;
                decimal dJUNP;
                decimal dJULP;
                decimal dAGOP;
                decimal dSEPP;
                decimal dOCTP;
                decimal dNOVP;
                decimal dDICP;
                for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "ArticulosID").ToString();
                    if (dato.Length > 0)
                    {
                        dENEP = 0;
                        dFEBP = 0;
                        dMARP = 0;
                        dABRP = 0;
                        dMAYP = 0;
                        dJUNP = 0;
                        dJULP = 0;
                        dAGOP = 0;
                        dSEPP = 0;
                        dOCTP = 0;
                        dNOVP = 0;
                        dDICP = 0;
                        intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "ArticulosID"));

                        switch (intM)
                        {
                            case 1:
                                dENEP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                break;
                            case 2:
                                dFEBP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                break;
                            case 3:
                                dMARP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                break;
                            case 4:
                                dABRP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                break;
                            case 5:
                                dMAYP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                break;
                            case 6:
                                dJUNP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                break;
                            case 7:
                                dJULP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                               break;
                            case 8:
                                dAGOP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                break;
                            case 9:
                                dSEPP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                break;
                            case 10:
                                dOCTP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                break;
                            case 11:
                                dNOVP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                break;
                            case 12:
                                dDICP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                break;
                            default:
                                break;
                        }

                        MetaporarticuloCL cl = new MetaporarticuloCL();
                        cl.intAño = intA;
                        cl.intArticulosID = intArticulosID;
                        cl.dENEP = dENEP;
                        cl.dFEBP = dFEBP;
                        cl.dMARP = dMARP;
                        cl.dABRP = dABRP;
                        cl.dMAYP = dMAYP;
                        cl.dJUNP = dJUNP;
                        cl.dJULP = dJULP;
                        cl.dAGOP = dAGOP;
                        cl.dSEPP = dSEPP;
                        cl.dOCTP = dOCTP;
                        cl.dNOVP = dNOVP;
                        cl.dDICP = dDICP;
                        Result = cl.MetaporarticuloCrud();
                        if (Result == "OK")
                        {
                            
   
                        }
                        else
                        {
                            MessageBox.Show(Result);
                        }

                    }
                }

                MessageBox.Show("Guardado Correctamente");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        } //Guardar  

        private void cboLinea_EditValueChanged(object sender, EventArgs e)
        {
            if (cboLinea.EditValue == null) { }
            else { CargaComboFamilias(); }
        }

        private void bbiProcesar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Visualsoft", "Cargando información...");
            Nuevo();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageGroup2.Visible = false;
            ribbonPageGroup1.Visible = true;
            navigationFrame.SelectedPageIndex = 0;
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //try
            //{
            //    decimal dNeto=0,dTotal=0, dMes=0;
            //    if (e.Column.Name == "gridColumnLin")
            //    {
            //        dMes = Convert.ToDecimal(txtM.Text);
            //        dTotal = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Total"));
            //        dNeto = Math.Round(dTotal / (dMes-1), 2);

            //        gridViewDetalle.SetFocusedRowCellValue("Promedio", dNeto);                 
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("griddetallecalcularPromedio: " + ex);
            //}
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Visualsoft","Guardando...");
            Guardar();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridMetaporarticuloDetalle";
            string result = clg.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiPrevio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlDetalle.ShowRibbonPrintPreview();
        }

        private void txtA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}