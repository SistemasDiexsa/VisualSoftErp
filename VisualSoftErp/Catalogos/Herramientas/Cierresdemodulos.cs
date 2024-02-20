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
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;

namespace VisualSoftErp.Catalogos
{

    public partial class Cierresdemodulos : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        int intEjercicio;
        int intMes;
        string strModulo;
        string sMes = string.Empty;

        public Cierresdemodulos()
        {
            InitializeComponent();
            
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Cierresdemodulos";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            CierresdemodulosCL cl = new CierresdemodulosCL();
            gridControlPrincipal.DataSource = cl.CierresdemodulosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCierresdemodulos";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

       
        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            BotonesEdicion();
           
        }

        private void LimpiaCajas()
        {
            cboMes.EditValue =0;
            txtEjercicio.Text = DateTime.Now.Year.ToString();
            swCom.IsOn = false;
            swCxc.IsOn = false;
            swCxp.IsOn = false;
            swVtas.IsOn = false;
            swInv.IsOn = false;



        }

        private void bbiGuardar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void VerificaStatus()
        {

            txtEjercicio.Text = intEjercicio.ToString();

            switch (sMes)
            {
                case "ENERO":
                    intMes = 1;
                    break;
                case "FEBRERO":
                    intMes = 2;
                    break;
                case "MARZO":
                    intMes = 3;
                    break;
                case "ABRIL":
                    intMes = 4;
                        break;
                case "MAYO":
                    intMes = 5;
                        break;
                case "JUNIO":
                    intMes = 6;
                        break;
                case "JULIO":
                    intMes = 7;
                        break;
                case "AGOSTO":
                    intMes = 8;
                        break;
                case "SEPTIEMBRE":
                    intMes = 9;
                        break;
                case "OCTUBRE":
                    intMes = 10;
                        break;
                case "NOVIEMBRE":
                    intMes = 11;
                        break;
                case "DICIEMBRE":
                    intMes = 12;
                        break;
            }

            cboMes.SelectedIndex = intMes;


            globalCL cl = new globalCL();

            String result = string.Empty;
           
            bool status;


            result = cl.GM_CierredemodulosStatus(intEjercicio, intMes, "COM");
            status = result is "A" ? true : false;
            swCom.IsOn = status;

            result = cl.GM_CierredemodulosStatus(intEjercicio, intMes, "CXP");
            status = result is "A" ? true : false;
            swCxp.IsOn = status;

            result = cl.GM_CierredemodulosStatus(intEjercicio, intMes, "INV");
            status = result is "A" ? true : false;
            swInv.IsOn = status;

            result = cl.GM_CierredemodulosStatus(intEjercicio, intMes, "VTA");
            status = result is "A" ? true : false;
            swVtas.IsOn = status;

            result = cl.GM_CierredemodulosStatus(intEjercicio, intMes, "CXC");
            status = result is "A" ? true : false;
            swCxc.IsOn = status;



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
                CierresdemodulosCL cl = new CierresdemodulosCL();
                cl.intEjercicio = Convert.ToInt32(txtEjercicio.Text);
                cl.intUsuario = 1;   // Hay que cambiar esto cuando haya login
                cl.intMes = cboMes.SelectedIndex;
                cl.strCOM = swCom.IsOn ? "1" : "0";
                cl.strCXP = swCxp.IsOn ? "1" : "0";
                cl.strINV = swInv.IsOn ? "1" : "0";
                cl.strVTA = swVtas.IsOn ? "1" : "0";
                cl.strCXC = swCxc.IsOn ? "1" : "0";
                Result = cl.CierresdemodulosCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intEjercicio == 0)
                    {
                        LimpiaCajas();
                    }
                }
                else
                {
                    MessageBox.Show(Result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        } //Guardar

        private string Valida()
        {
            if (cboMes.SelectedIndex == 0)
            {
                return "El campo Mes no puede ir vacio";
            }
            if (txtEjercicio.Text.Length == 0)
            {
                return "El ejercicio no puede ir vacio";
            }

            globalCL clg = new globalCL();
            if (!clg.esNumerico(txtEjercicio.Text))
            {
                return "Teclee el ejercicio";
            }
           
            return "OK";
        } //Valida     

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intEjercicio == 0 && intMes == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }  //Editar

        private void Editar()
        {
            BotonesEdicion();
            VerificaStatus();
        }

        private void BotonesEdicion()
        {
            LimpiaCajas();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            navigationFrame.SelectedPageIndex = 1;
        }     

        private void bbiRegresar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCierresdemodulos";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }

        private void bbiVista_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlPrincipal.ShowRibbonPrintPreview();
        }

        private void gridViewPrincipal_RowClick(Object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            
            intEjercicio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Ejercicio"));
            sMes = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "MES").ToString();
           
            strModulo = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Modulo"));
        }

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            /*try
            {
                GridView view = sender as GridView;

                string strMes = string.Empty;
                string com = string.Empty;
                string cxp = string.Empty;
                string cxc = string.Empty;
                string inv = string.Empty;
                string vta = string.Empty;

                string sMes = view.GetRowCellValue(e.RowHandle, "Mes").ToString();
                com = view.GetRowCellValue(e.RowHandle, "COM").ToString();

                switch (sMes)
                {
                    case "1":
                        strMes = "ENERO";
                        break;
                    case "2":
                        strMes = "FEBRERO";
                        break;
                    case "3":
                        strMes = "MARZO";
                        break;
                    case "4":
                        strMes = "ABRIL";
                        break;
                    case "5":
                        strMes = "MAYO";
                        break;
                    case "6":
                        strMes = "JUNIO";
                        break;
                    case "7":
                        strMes = "JULIO";
                        break;
                    case "8":
                        strMes = "AGOSTO";
                        break;
                    case "9":
                        strMes = "SEPTIEMBRE";
                        break;
                    case "10":
                        strMes = "OCTUBRE";
                        break;
                    case "11":
                        strMes = "NOVIEMBRE";
                        break;
                    case "12":
                        strMes = "DICIEMBRE";
                        break;                    
                }

                com = com is "1" ? "ABIERTO" : "CERRADO";
                cxp = cxp is "1" ? "ABIERTO" : "CERRADO";
                cxc = cxc is "1" ? "ABIERTO" : "CERRADO";
                inv = inv is "1" ? "ABIERTO" : "CERRADO";
                vta = vta is "1" ? "ABIERTO" : "CERRADO";


                view.SetRowCellValue(e.RowHandle, "Nombredemes", strMes);
                view.SetRowCellValue(e.RowHandle, "Compras", com);
                view.SetRowCellValue(e.RowHandle, "CtasPorPagar", cxp);
                view.SetRowCellValue(e.RowHandle, "CuentasPorCobrar", cxc);
                view.SetRowCellValue(e.RowHandle, "Inventarios", inv);
                view.SetRowCellValue(e.RowHandle, "Ventas", vta);
                //if (_mark == "Cancelado")
                //{
                //    e.Appearance.ForeColor = Color.Red;
                //}
            }
            catch (Exception ex)
            {
                string strerr = ex.Message;
            }*/
        }
    }
}
