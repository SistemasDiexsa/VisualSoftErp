﻿using System;
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
            txtM.Text = DateTime.Now.Month.ToString();
            Cargacombos();
            gridViewDetalle.OptionsView.ShowAutoFilterRow = true;

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

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

            cboFamilia.Properties.NullText = "";

        }

        private void CargaComboFamilias()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();
            BindingSource src = new BindingSource();

            cboFamilia.Properties.ValueMember = "Clave";
            cboFamilia.Properties.DisplayMember = "Des";
            cl.strTabla = "FamiliasLineas";
            cl.intClave = Convert.ToInt32(cboLinea.EditValue);
            src.DataSource = cl.CargaCombosCondicion();
            cboFamilia.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboFamilia.Properties.ForceInitialize();
            cboFamilia.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilia.Properties.ForceInitialize();
            cboFamilia.Properties.PopulateColumns();
            cboFamilia.Properties.Columns["Clave"].Visible = false;
            cboFamilia.ItemIndex = 0;

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
            Inicialisalista();
            try
            {
                MetaporarticuloCL cl = new MetaporarticuloCL();

                int Mes = 0;
                int Año = 0;

                Año= Convert.ToInt32(txtA.Text);

                Mes = Convert.ToInt32(txtM.Text);
                if (Mes > 1)
                {
                    Mes = Mes - 1;
                }
                else
                {
                    Mes = 12;
                    Año = Año - 1;
                }

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

                gridViewDetalle.ViewCaption = "PRONOSTICO PARA " + clg.NombreDeMes(Convert.ToInt32(txtM.Text), 0) + " DEL " + txtA.Text;
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

                int intM = Convert.ToInt32(txtM.Text);
                int intA = Convert.ToInt32(txtA.Text);
                int intArticulosID = 0;
                string dato = "";
                decimal dENEP = 0;
                decimal dFEBP = 0;
                decimal dMARP = 0;
                decimal dABRP = 0;
                decimal dMAYP = 0;
                decimal dJUNP = 0;
                decimal dJULP = 0;
                decimal dAGOP = 0;
                decimal dSEPP = 0;
                decimal dOCTP = 0;
                decimal dNOVP = 0;
                decimal dDICP = 0;
                for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "ArticulosID").ToString();
                    if (dato.Length > 0)
                    {
                        intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "ArticulosID"));

                        switch (intM)
                        {
                            case 1:
                                dENEP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
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
                                break;
                            case 2:
                                dENEP = 0;
                                dFEBP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
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
                                break;
                            case 3:
                                dENEP = 0;
                                dFEBP = 0;
                                dMARP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                dABRP = 0;
                                dMAYP = 0;
                                dJUNP = 0;
                                dJULP = 0;
                                dAGOP = 0;
                                dSEPP = 0;
                                dOCTP = 0;
                                dNOVP = 0;
                                dDICP = 0;
                                break;
                            case 4:
                                dENEP = 0;
                                dFEBP = 0;
                                dMARP = 0;
                                dABRP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                dMAYP = 0;
                                dJUNP = 0;
                                dJULP = 0;
                                dAGOP = 0;
                                dSEPP = 0;
                                dOCTP = 0;
                                dNOVP = 0;
                                dDICP = 0;
                                break;
                            case 5:
                                dENEP = 0;
                                dFEBP = 0;
                                dMARP = 0;
                                dABRP = 0;
                                dMAYP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                dJUNP = 0;
                                dJULP = 0;
                                dAGOP = 0;
                                dSEPP = 0;
                                dOCTP = 0;
                                dNOVP = 0;
                                dDICP = 0;
                                break;
                            case 6:
                                dENEP = 0;
                                dFEBP = 0;
                                dMARP = 0;
                                dABRP = 0;
                                dMAYP = 0;
                                dJUNP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                dJULP = 0;
                                dAGOP = 0;
                                dSEPP = 0;
                                dOCTP = 0;
                                dNOVP = 0;
                                dDICP = 0;
                                break;
                            case 7:
                                dENEP = 0;
                                dFEBP = 0;
                                dMARP = 0;
                                dABRP = 0;
                                dMAYP = 0;
                                dJUNP = 0;
                                dJULP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                dAGOP = 0;
                                dSEPP = 0;
                                dOCTP = 0;
                                dNOVP = 0;
                                dDICP = 0;
                                break;
                            case 8:
                                dENEP = 0;
                                dFEBP = 0;
                                dMARP = 0;
                                dABRP = 0;
                                dMAYP = 0;
                                dJUNP = 0;
                                dJULP = 0;
                                dAGOP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                dSEPP = 0;
                                dOCTP = 0;
                                dNOVP = 0;
                                dDICP = 0;
                                break;
                            case 9:
                                dENEP = 0;
                                dFEBP = 0;
                                dMARP = 0;
                                dABRP = 0;
                                dMAYP = 0;
                                dJUNP = 0;
                                dJULP = 0;
                                dAGOP = 0;
                                dSEPP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                dOCTP = 0;
                                dNOVP = 0;
                                dDICP = 0;
                                break;
                            case 10:
                                dENEP = 0;
                                dFEBP = 0;
                                dMARP = 0;
                                dABRP = 0;
                                dMAYP = 0;
                                dJUNP = 0;
                                dJULP = 0;
                                dAGOP = 0;
                                dSEPP = 0;
                                dOCTP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                dNOVP = 0;
                                dDICP = 0;
                                break;
                            case 11:
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
                                dNOVP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
                                dDICP = 0;
                                break;
                            case 12:
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
                                dDICP = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Proyectado"));
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
    }
}