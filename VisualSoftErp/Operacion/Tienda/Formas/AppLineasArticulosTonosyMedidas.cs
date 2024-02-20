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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace VisualSoftErp.Operacion.Inventarios.Formas
{
    public partial class AppLineasArticulosTonosyMedidas : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        public BindingList<detalleCL> detalle;
        int intAppLineasID;
        int intArticulosID;
        int intAppLineasArticulosTonosyMedidasID;
        bool blNuevo;
        int intUsuarioID = globalCL.gv_UsuarioID;
        public AppLineasArticulosTonosyMedidas()
        {
            InitializeComponent();

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Catálogo de Artículos Tonos y Medidas";

            //gridViewDetalle.OptionsView.ShowAutoFilterRow = true;

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void LlenarGrid()
        {
            AppLineasArticulosTonosyMedidasCL cl = new AppLineasArticulosTonosyMedidasCL();
            gridControlPrincipal.DataSource = cl.AppLineasArticulosTonosyMedidasGRID();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppLineasArticulosTonosyMedidas";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        public class detalleCL
        {
            public string AppTonos { get; set; }
            public string AppMedidas { get; set; }
        }
        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Articulos";
            cboArticulosID.Properties.ValueMember = "Clave";
            cboArticulosID.Properties.DisplayMember = "Des";
            cboArticulosID.Properties.DataSource = cl.CargaCombos();
            cboArticulosID.Properties.ForceInitialize();
            cboArticulosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulosID.Properties.PopulateColumns();
            cboArticulosID.Properties.Columns["Clave"].Visible = false;
            cboArticulosID.Properties.NullText = "Seleccione un Artículo";

            cl.strTabla = "AppTonos";
            repositoryItemLookUpEditTono.ValueMember = "Clave";
            repositoryItemLookUpEditTono.DisplayMember = "Des";
            repositoryItemLookUpEditTono.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditTono.ForceInitialize();
            repositoryItemLookUpEditTono.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditTono.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditTono.PopulateColumns();
            repositoryItemLookUpEditTono.Columns["Clave"].Visible = false;            
            repositoryItemLookUpEditTono.NullText = "Seleccione un tono";

            cl.strTabla = "AppMedidas";
            repositoryItemLookUpEditMedida.ValueMember = "Clave";
            repositoryItemLookUpEditMedida.DisplayMember = "Des";
            repositoryItemLookUpEditMedida.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditMedida.ForceInitialize();
            repositoryItemLookUpEditMedida.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditMedida.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditMedida.PopulateColumns();
            repositoryItemLookUpEditMedida.Columns["Clave"].Visible = false;
            repositoryItemLookUpEditMedida.NullText = "Seleccione una medida";

        }

        private void bbiNuevo_ItemClick(Object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            Inicialisalista();
            BotonesEdicion();
            intAppLineasID = 0;
            intArticulosID = 0;
            blNuevo = true;
        }

        private void BotonesEdicion()
        {
            
            guardaLayout();
            ribbonPageGroup1.Visible = false;
            ribbonPageGroup2.Visible = true;
            navigationFrame.SelectedPageIndex = 1;
        }

        private void LimpiaCajas()
        {
            //cboArticulosID.EditValue = null;
        }

        private string Valida()
        {
            if (cboArticulosID.EditValue == null)
            {
                return "El campo ArticulosID no puede ir vacio";
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
                string sCondicion = String.Empty;

                System.Data.DataTable dtAppLineasArticulosTonosyMedidas = new System.Data.DataTable("AppLineasArticulosTonosyMedidas");
                dtAppLineasArticulosTonosyMedidas.Columns.Add("AppLineasID", Type.GetType("System.Int32"));
                dtAppLineasArticulosTonosyMedidas.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtAppLineasArticulosTonosyMedidas.Columns.Add("AppTonosID", Type.GetType("System.Int32"));
                dtAppLineasArticulosTonosyMedidas.Columns.Add("AppMedidasID", Type.GetType("System.Int32"));

                //Siguiente folio
                int iFolio = 0;
                globalCL clg = new globalCL();
                clg.strDoc = "AppLineasArticulos";
                clg.strSerie = "";
                if (blNuevo)
                {
                    Result = clg.DocumentosSiguienteID();
                    if (Result != "OK")
                    {
                        MessageBox.Show("No se pudo leer el siguiente folio");
                        return;
                    }
                    intAppLineasArticulosTonosyMedidasID = clg.iFolio;
                }


                //Manual: --- Definir la condicion necesaria para traer el siguiente ID ---------------------         clg.sCondicion = sCondicion;

                int intSeq = 0;
                string dato = String.Empty;
      
                int intAppTonosID = 0;
                int intAppMedidasID = 0;
                intArticulosID = Convert.ToInt32(cboArticulosID.EditValue);

                int [,] valores1;

                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "AppTonos").ToString();
                    if (dato.Length > 0)
                    {
                        intSeq = i;
                        intAppTonosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "AppTonos"));
                        intAppMedidasID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "AppMedidas"));
                        valores1 = new int[intAppTonosID, intAppMedidasID];

                        //var repetido = false;
                        //for (var j = 0; j < valores1.length; j++)
                        //{
                        //    for (var k = 0; k < valores1.length; k++)
                        //    {
                        //        if (valores1[j] == valores1[k] && j != k)
                        //        { //revisamos que i sea diferente de j, para que no compare el mismo elemento exacto.
                        //            repetido = true;
                        //        }
                        //    }
                        //}



                        dtAppLineasArticulosTonosyMedidas.Rows.Add(intAppLineasID,intArticulosID, intAppTonosID, intAppMedidasID);

                        //--- Aquí puedn ir los campos que ocupen sumarse para los totales y otro fin
                        //---Por ejemplo:
                        //--- dSubTotal += dImporte;

                    }
                }


                AppLineasArticulosTonosyMedidasCL cl = new AppLineasArticulosTonosyMedidasCL();
                cl.intArticulosID = intArticulosID;
                cl.dtm = dtAppLineasArticulosTonosyMedidas;
                cl.intUsuarioID = intUsuarioID;
                cl.strMaquina = Environment.MachineName;

                Result = cl.AppLineasArticulosTonosyMedidasCRUD();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");                   
                    LimpiaCajas();
                    Inicialisalista();
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

        private void BuscarLinea()
        {
            try
            {

                articulosCL cl = new articulosCL();
                intArticulosID = Convert.ToInt32(cboArticulosID.EditValue);
                cl.intArticulosID = intArticulosID;
                string result = cl.articulosLlenaCajas();
                if (result == "OK")
                {
                    intAppLineasID = cl.intFamiliasID;
                    llenaCajas();
                }

            }

            catch (Exception ex)

            {

                MessageBox.Show("BuscarLinea: " + ex.Message);

            }
        }

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();
            blNuevo = false;
        }

        private void llenaCajas()
        {
            AppLineasArticulosTonosyMedidasCL cl = new AppLineasArticulosTonosyMedidasCL();
            cl.intArticulosID = intArticulosID;
            gridControlDetalle.DataSource = cl.AppLineasArticulosTonosyMedidasLlenaCajas();
            cboArticulosID.EditValue = intArticulosID;
 
        } // llenaCajas

        private void bbbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clgd = new globalCL();
            clgd.strGridLayout = "gridAppLineasArticulosTonosyMedidasDet" +
                "";
            string result = clgd.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }


            LimpiaCajas();
            LlenarGrid();
            ribbonPageGroup2.Visible = false;
            ribbonPageGroup1.Visible = true;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            ribbonStatusBar.Visible = true;
            Inicialisalista();
            LimpiaCajas();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void cboArticulosID_EditValueChanged(object sender, EventArgs e)
        {
            BuscarLinea();     
        }

        private void guardaLayout()
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAppLineasArticulosTonosyMedidas" +
                "";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
        }



        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            guardaLayout();

            
            this.Close();
        }

        private void bbi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intArticulosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void gridViewDetalle_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void gridViewPrincipal_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            intArticulosID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ArticulosID"));
        }

        private void gridControlDetalle_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                view.DeleteSelectedRows();
                e.Handled = true;
            }
        }
    }
}