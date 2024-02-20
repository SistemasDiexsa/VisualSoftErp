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

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class Controldeimplementacion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intImplementacionID;
        int intUsuarioID = globalCL.gv_UsuarioID;
        public Controldeimplementacion()
        {
            InitializeComponent();

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Control de implementación";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Programas";
            cboProgramasID.Properties.ValueMember = "Clave";
            cboProgramasID.Properties.DisplayMember = "Des";
            cboProgramasID.Properties.DataSource = cl.CargaCombos();
            cboProgramasID.Properties.ForceInitialize();
            cboProgramasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProgramasID.Properties.PopulateColumns();
            cboProgramasID.Properties.Columns["Clave"].Visible = false;
            cboProgramasID.Properties.NullText = "Selecciona un programa";
        }//CargaCombos

        private void LlenarGrid()
        {
            ControldeimplementacionCL cl = new ControldeimplementacionCL();
            gridControlPrincipal.DataSource = cl.ControldeimplementacionGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridControldeimplementacion";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
            intImplementacionID = 0;
        }

        private void BotonesEdicion()
        {
            LimpiaCajas();
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup2.Visible = true;

            navigationFrame.SelectedPageIndex = 1;
        }

        private void LimpiaCajas()
        {
            cboProgramasID.EditValue = null;
            dateEditFecha.Text = DateTime.Now.ToShortDateString();
            txtReporte.Text = string.Empty;
            swFueradealcance.IsOn = false;
            gridControlDetalle.DataSource = null;
        }

        private string Valida()
        {
            if (cboProgramasID.EditValue == null)
            {
                return "El campo ProgramasID no puede ir vacio";
            }
            if (dateEditFecha.Text.Length == 0)
            {
                return "El campo Fecha no puede ir vacio";
            }
            if (txtReporte.Text.Length == 0)
            {
                return "El campo Reporte no puede ir vacio";
            }

            return "OK";
        } //Valida

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
                ControldeimplementacionCL cl = new ControldeimplementacionCL();
                cl.intImplementacionID = intImplementacionID;
                cl.strProgramasID= cboProgramasID.EditValue.ToString();
                cl.fFecha = Convert.ToDateTime(dateEditFecha.Text);
                cl.intUsuariosID = intUsuarioID;
                cl.strReporte = txtReporte.Text;
                cl.intStatus = 1;
                cl.strSolucion = "";
                cl.fFechaProceso = Convert.ToDateTime(DateTime.Now);
                cl.fFechaTerminado = Convert.ToDateTime(DateTime.Now);
                cl.strAgente = "";
                cl.strVersion = "0";
                cl.intFueradealcance = swFueradealcance.IsOn ? 1 : 0;
                Result = cl.ControldeimplementacionCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                   
                        LimpiaCajas();
                    
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

        private void LlenarGridDetalle()
        {
            try
            {
                ControldeimplementacionCL cl = new ControldeimplementacionCL();
                cl.strProgramasID = cboProgramasID.EditValue.ToString();
                gridControlDetalle.DataSource = cl.ControldeimplementacionGridDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("LlenarGridDetalle: " + ex);
            }
        }

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();
        }

        private void llenaCajas()
        {
            ControldeimplementacionCL cl = new ControldeimplementacionCL();
            cl.intImplementacionID = intImplementacionID;
            String Result = cl.ControldeimplementacionLlenaCajas();
            if (Result == "OK")
            {
                cboProgramasID.EditValue = cl.strProgramasID;
                dateEditFecha.Text = cl.fFecha.ToShortDateString();
                txtReporte.Text = cl.strReporte;
                swFueradealcance.IsOn = cl.intFueradealcance == 1 ? true : false;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intImplementacionID==0)
            {
                MessageBox.Show("Seleccione un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridControldeimplementacion" +
                "";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            globalCL clgd = new globalCL();
            clgd.strGridLayout = "gridControldeimplementacionDetalle" +
                "";
            result = clgd.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intImplementacionID = 0;
            LimpiaCajas();
            LlenarGrid();
            ribbonPageGroup2.Visible = false;
            ribbonPageGroup1.Visible = true;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            ribbonStatusBar.Visible = true;
            gridControlDetalle.DataSource = null;
            LimpiaCajas();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void cboProgramas_EditValueChanged(object sender, EventArgs e)
        {
            if (cboProgramasID.EditValue == null)
            {

            }
            else
            {
                //LlenarGridDetalle();
            }

        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intImplementacionID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ImplementacionID"));
        }

        private void bbiCargarHistorial_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LlenarGridDetalle();
        }
    }
}