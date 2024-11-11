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
using System.IO;

namespace VisualSoftErp.Operacion.Tienda.Formas
{
    public partial class AppFamiliasSubfamiliasImagenes : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intAppFamiliasSubfamiliasImagenesID;

        public AppFamiliasSubfamiliasImagenes()
        {
            InitializeComponent();
            txtImagen.Properties.MaxLength = 100;
            txtImagen.EnterMoveNextControl = true;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "AppFamiliasSubfamiliasImagenes";

            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            LlenarGrid();
            CargaCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void LlenarGrid()
        {
            AppFamiliasSubfamiliasImagenesCL cl = new AppFamiliasSubfamiliasImagenesCL();
            gridControlPrincipal.DataSource = cl.AppFamiliasSubfamiliasImagenesGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppFamiliasSubfamiliasImagenes";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Familiasrep";
            cboFamiliasID.Properties.ValueMember = "Clave";
            cboFamiliasID.Properties.DisplayMember = "Des";
            cboFamiliasID.Properties.DataSource = cl.CargaCombos();
            cboFamiliasID.Properties.ForceInitialize();
            cboFamiliasID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamiliasID.Properties.PopulateColumns();
            cboFamiliasID.Properties.Columns["Clave"].Visible = false;
            cboFamiliasID.Properties.NullText = "Seleccione una familia";

            cboSubFamilias.Properties.NullText = "Seleccione una familia";
        }

        private void CargaComboSubFamilias()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "SubfamiliasXfamilias";
            cl.iCondicion = Convert.ToInt32(cboFamiliasID.EditValue);
            cboSubFamilias.Properties.ValueMember = "Clave";
            cboSubFamilias.Properties.DisplayMember = "Des";
            cboSubFamilias.Properties.DataSource = cl.CargaCombos();
            cboSubFamilias.Properties.ForceInitialize();
            cboSubFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSubFamilias.Properties.PopulateColumns();
            cboSubFamilias.Properties.Columns["Clave"].Visible = false;
            cboSubFamilias.Properties.NullText = "Seleccione una SubFamilia";
        }

        private void Nuevo()
        {
            ribbonPageGroup1.Visible = false;
            LimpiaCajas();
            intAppFamiliasSubfamiliasImagenesID = 0;
            BotonesEdicion();
        }

        private void LimpiaCajas()
        {
           
            txtImagen.Text = string.Empty;
        }

        private void BotonesEdicion()
        {
            ribbonPageGroup2.Visible = true;
            ribbonPageGroup1.Visible = false;
            ribbonStatusBar.Visible = false;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();
        }

        private void llenaCajas()
        {
            AppFamiliasSubfamiliasImagenesCL cl = new AppFamiliasSubfamiliasImagenesCL();
            cl.intAppFamiliasSubfamiliasImagenesID = intAppFamiliasSubfamiliasImagenesID;
            String Result = cl.AppFamiliasSubfamiliasImagenesLlenaCajas();
            if (Result == "OK")
            {
                cboFamiliasID.EditValue = cl.intFamiliasID;
                cboSubFamilias.EditValue = cl.intSubFamiliasID;
                txtImagen.Text = cl.strImagen;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

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
                AppFamiliasSubfamiliasImagenesCL cl = new AppFamiliasSubfamiliasImagenesCL();
                cl.intAppFamiliasSubfamiliasImagenesID = intAppFamiliasSubfamiliasImagenesID;
                cl.intFamiliasID = Convert.ToInt32(cboFamiliasID.EditValue);
                cl.intSubFamiliasID = Convert.ToInt32(cboSubFamilias.EditValue);
                cl.strImagen = txtImagen.Text;
                Result = cl.AppFamiliasSubfamiliasImagenesCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intAppFamiliasSubfamiliasImagenesID == 0)
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
            if (cboFamiliasID.EditValue == null)
            {
                return "El campo FamiliasID no puede ir vacio";
            }
            if (cboSubFamilias.EditValue == null)
            {
                return "El campo SubFamiliasID no puede ir vacio";
            }
            if (txtImagen.Text.Length == 0)
            {
                return "El campo Imagen no puede ir vacio";
            }
            return "OK";
        } //Valida

        private void gridViewPrincipal_RowClick(Object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intAppFamiliasSubfamiliasImagenesID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "AppFamiliasSubfamiliasImagenesID"));
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intAppFamiliasSubfamiliasImagenesID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppFamiliasSubfamiliasImagenes";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LimpiaCajas();            
            ribbonPageGroup2.Visible = false;
            ribbonPageGroup1.Visible = true;
            ribbonStatusBar.Visible = true;
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiAbrirImagen_Click(object sender, EventArgs e)
        {
            string strLayoutPath = System.Configuration.ConfigurationManager.AppSettings["gridlayout"].ToString() + "xmlcompras.txt";
            string data = string.Empty;
            if (File.Exists(strLayoutPath))
            {
                data = System.IO.File.ReadAllText(strLayoutPath);
                data = data.Substring(0, data.Length - 2) + "\\";
            }

            if (data.Length == 0)
                xtraOpenFileDialog1.InitialDirectory = "C:\\";
            else
                xtraOpenFileDialog1.InitialDirectory = data;

            xtraOpenFileDialog1.ShowDragDropConfirmation = true;
            xtraOpenFileDialog1.AutoUpdateFilterDescription = false;
            xtraOpenFileDialog1.Filter = "XML files (*.xml)|*.xml";
            xtraOpenFileDialog1.Multiselect = false;

            DialogResult dr = xtraOpenFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string file = xtraOpenFileDialog1.FileName;
                string sDir = Path.GetDirectoryName(file);
                txtImagen.Text = file.ToString();
                using (StreamWriter writer = new StreamWriter(strLayoutPath))
                {
                    writer.WriteLine(sDir);
                }
            }

        }

        private void cboFamiliasID_EditValueChanged(object sender, EventArgs e)
        {
            if (cboFamiliasID.EditValue == null) { }
            else
            {
                CargaComboSubFamilias();
            }
        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intAppFamiliasSubfamiliasImagenesID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult Result = MessageBox.Show("Desea eliminar esta imagen?", "Eliminar", MessageBoxButtons.YesNo);
                if (Result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void Eliminar()
        {
            try
            {
                AppFamiliasSubfamiliasImagenesCL cl = new AppFamiliasSubfamiliasImagenesCL();
                cl.intAppFamiliasSubfamiliasImagenesID = intAppFamiliasSubfamiliasImagenesID;
                string result = cl.AppFamiliasSubfamiliasImagenesEliminar();
                if (result == "OK")
                {
                    MessageBox.Show("Eliminado correctamente");
                    LlenarGrid();
                }
                else
                    MessageBox.Show(result);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}