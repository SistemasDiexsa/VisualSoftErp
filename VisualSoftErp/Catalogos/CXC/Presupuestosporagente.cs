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

namespace VisualSoftErp.Catalogos
{

    public partial class Presupuestosporagente : DevExpress.XtraBars.Ribbon.RibbonForm
    {
      
        int intEjericio;
        int intAgentesID;

        public Presupuestosporagente()
        {
            InitializeComponent();


            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Presupuestosporagente";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            PresupuestosporagenteCL cl = new PresupuestosporagenteCL();
            gridControlPrincipal.DataSource = cl.PresupuestosporagenteGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridPresupuestosporagente";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Agentes";
            cboAgentesID.Properties.ValueMember = "Clave";
            cboAgentesID.Properties.DisplayMember = "Des";
            cboAgentesID.Properties.DataSource = cl.CargaCombos();
            cboAgentesID.Properties.ForceInitialize();
            cboAgentesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAgentesID.Properties.PopulateColumns();
            cboAgentesID.Properties.Columns["Clave"].Visible = false;
            cboAgentesID.Properties.NullText = "Seleccione un Agente";
        }

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
            txtEjericio.Text = string.Empty;
            cboAgentesID.EditValue = null;
            dEne.Text = string.Empty;
            dFeb.Text = string.Empty;
            dMar.Text = string.Empty;
            dAbr.Text = string.Empty;
            dMay.Text = string.Empty;
            dJun.Text = string.Empty;
            dJul.Text = string.Empty;
            dAgo.Text = string.Empty;
            dSep.Text = string.Empty;
            dOct.Text = string.Empty;
            dNov.Text = string.Empty;
            dDic.Text = string.Empty;

        }

        private void bbiGuardar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
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
                PresupuestosporagenteCL cl = new PresupuestosporagenteCL();
                cl.intEjericio = Convert.ToInt32(txtEjericio.Text);
                cl.intAgentesID = Convert.ToInt32(cboAgentesID.EditValue);
                cl.dEne = Convert.ToDecimal(dEne.Text);
                cl.dFeb = Convert.ToDecimal(dFeb.Text);
                cl.dMar = Convert.ToDecimal(dMar.Text);
                cl.dAbr = Convert.ToDecimal(dAbr.Text);
                cl.dMay = Convert.ToDecimal(dMay.Text);
                cl.dJun = Convert.ToDecimal(dJun.Text);
                cl.dJul = Convert.ToDecimal(dJul.Text);
                cl.dAgo = Convert.ToDecimal(dAgo.Text);
                cl.dSep = Convert.ToDecimal(dSep.Text);
                cl.dOct = Convert.ToDecimal(dOct.Text);
                cl.dNov = Convert.ToDecimal(dNov.Text);
                cl.dDic = Convert.ToDecimal(dDic.Text);
                Result = cl.PresupuestosporagenteCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intEjericio == 0)
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
            if (txtEjericio.Text.Length == 0)
            {
                return "El campo AgentesID no puede ir vacio";
            }
            if (cboAgentesID.EditValue == null)
            {
                return "El campo AgentesID no puede ir vacio";
            }
            if (dEne.Text.Length == 0)
            {
                return "El campo Ene no puede ir vacio";
            }
            if (dFeb.Text.Length == 0)
            {
                return "El campo Feb no puede ir vacio";
            }
            if (dMar.Text.Length == 0)
            {
                return "El campo Mar no puede ir vacio";
            }
            if (dAbr.Text.Length == 0)
            {
                return "El campo Abr no puede ir vacio";
            }
            if (dMay.Text.Length == 0)
            {
                return "El campo May no puede ir vacio";
            }
            if (dJun.Text.Length == 0)
            {
                return "El campo Jun no puede ir vacio";
            }
            if (dJul.Text.Length == 0)
            {
                return "El campo Jul no puede ir vacio";
            }
            if (dAgo.Text.Length == 0)
            {
                return "El campo Ago no puede ir vacio";
            }
            if (dSep.Text.Length == 0)
            {
                return "El campo Sep no puede ir vacio";
            }
            if (dOct.Text.Length == 0)
            {
                return "El campo Oct no puede ir vacio";
            }
            if (dNov.Text.Length == 0)
            {
                return "El campo Nov no puede ir vacio";
            }
            if (dDic.Text.Length == 0)
            {
                return "El campo Dic no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void llenaCajas()
        {
            PresupuestosporagenteCL cl = new PresupuestosporagenteCL();
            cl.intEjericio = intEjericio;
            cl.intAgentesID = intAgentesID;
            String Result = cl.PresupuestosporagenteLlenaCajas();
            if (Result == "OK")
            {
                cboAgentesID.EditValue = cl.intAgentesID;
                txtEjericio.Text = Convert.ToString(cl.intEjericio);
                dEne.Text = Convert.ToString(cl.dEne);
                dFeb.Text = Convert.ToString(cl.dFeb);
                dMar.Text = Convert.ToString(cl.dMar);
                dAbr.Text = Convert.ToString(cl.dAbr);
                dMay.Text = Convert.ToString(cl.dMay);
                dJun.Text = Convert.ToString(cl.dJun);
                dJul.Text = Convert.ToString(cl.dJul);
                dAgo.Text = Convert.ToString(cl.dAgo);
                dSep.Text = Convert.ToString(cl.dSep);
                dOct.Text = Convert.ToString(cl.dOct);
                dNov.Text = Convert.ToString(cl.dNov);
                dDic.Text = Convert.ToString(cl.dDic);
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void bbiEditar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intEjericio == 0)
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
            llenaCajas();
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

        private void Eliminar()
        {
            PresupuestosporagenteCL cl = new PresupuestosporagenteCL();
            cl.intEjericio = intEjericio;
            cl.intAgentesID = intAgentesID;
            String Result = cl.PresupuestosporagenteEliminar();
            if (Result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiEliminar_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intEjericio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar el ID " + intEjericio.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
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
            clg.strGridLayout = "gridPresupuestosporagente";
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
            intEjericio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Ejericio"));
            intAgentesID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "AgentesID"));
        }

        private void customersNavigationPage_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
