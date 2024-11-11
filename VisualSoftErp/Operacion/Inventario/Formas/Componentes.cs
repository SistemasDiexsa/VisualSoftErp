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
using VisualSoftErp.Operacion.Inventarios.Designers;
using DevExpress.XtraReports.UI;
using System.Configuration;

namespace VisualSoftErp.Operacion.Inventarios.Informes
{
    public partial class Componentes : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        double dblFactorUm2;
        int intComponente, intProductoTerminadoID;
        int intUsuarioID = globalCL.gv_UsuarioID;
        public BindingList<detalleCL> detalle;
        public bool blNuevo;
        int intTiposdemovimientoinvID;

        public Componentes(int articulosID)
        {
            if (articulosID == 0) 
            {
                InitializeComponent();
                gridViewPrincipal.OptionsView.ShowViewCaption = true;
                cboArticulos.EnterMoveNextControl = true;
                gridViewPrincipal.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
                gridViewPrincipal.OptionsView.ShowFooter = true;
                gridViewPrincipal.OptionsNavigation.EnterMoveNextColumn = true;
                gridViewPrincipal.OptionsNavigation.AutoMoveRowFocus = true;
                CargaCombos();
                CargaComboGrid();
                Inicialisalista();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
            else
            {
                InitializeComponent();
                gridViewPrincipal.OptionsView.ShowViewCaption = true;
                cboArticulos.EnterMoveNextControl = true;
                gridViewPrincipal.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
                gridViewPrincipal.OptionsView.ShowFooter = true;
                gridViewPrincipal.OptionsNavigation.EnterMoveNextColumn = true;
                gridViewPrincipal.OptionsNavigation.AutoMoveRowFocus = true;
                CargaCombos();
                CargaComboGrid();
                Inicialisalista();
                cboArticulos.EditValue = articulosID;
                CargaComponentes();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        private void Componentes_Load(object sender, EventArgs e)
        {
            
        }

        private void LlenarGrid()
        {
            ComponentesCL cl = new ComponentesCL();
            cl.intArticulosID = Convert.ToInt32(cboArticulos.EditValue);
            gridControlPrincipal.DataSource = cl.ComponentesGrid();
           
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridComponentes";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        public class detalleCL
        {
            public int Componente { get; set; }
            public decimal Cantidad { get; set; }
            public string Presentacion { get; set; }
            public decimal Arebajar { get; set; }
            public decimal Costo { get; set; }
            public decimal Importe { get; set; }
        }

        private void Inicialisalista()
        {

            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlPrincipal.DataSource = detalle;
            gridColumnImporte.OptionsColumn.ReadOnly = true;
            gridColumnImporte.OptionsColumn.AllowFocus = false;

            gridColumnArebajar.OptionsColumn.ReadOnly = true;
            gridColumnArebajar.OptionsColumn.AllowFocus = false;

            gridColumnCosto.OptionsColumn.ReadOnly = true;
            gridColumnCosto.OptionsColumn.AllowFocus = false;

        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();

            cl.strTabla = "Articulos";
            cboArticulos.Properties.ValueMember = "Clave";
            cboArticulos.Properties.DisplayMember = "Des";
            cboArticulos.Properties.DataSource = cl.CargaCombos();
            cboArticulos.Properties.ForceInitialize();
            cboArticulos.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulos.Properties.PopulateColumns();
            cboArticulos.Properties.Columns["Clave"].Visible = false;
            cboArticulos.Properties.Columns["FactorUM2"].Visible = false;
            cboArticulos.Properties.NullText = "Seleccione un producto terminado";
            //repositoryItemLookUpEditArticulo.Properties.NullText = "Seleccione un producto terminado";



        }

        public void CargaComboGrid()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Componentes";
            cl.intClave = intComponente;
            repositoryItemLookUpEditArticulo.ValueMember = "Clave";
            repositoryItemLookUpEditArticulo.DisplayMember = "Des";
            repositoryItemLookUpEditArticulo.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulo.ForceInitialize();
            repositoryItemLookUpEditArticulo.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditArticulo.Properties.NullText = "Seleccione un componente";
        }

        public class tipoCL
        {
            public string Clave { get; set; }
            public string Des { get; set; }
        }

        private void LimpiaCajas()
        {
            Inicialisalista();
            cboArticulos.EditValue = null;
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
                string sCondicion = String.Empty;
                System.Data.DataTable dtComponentes = new System.Data.DataTable("Componentes");
                dtComponentes.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtComponentes.Columns.Add("Componente", Type.GetType("System.Int32"));
                dtComponentes.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtComponentes.Columns.Add("Presentacion", Type.GetType("System.String"));

                int IntArticulosID = 0;
                int intComponente = 0;
                decimal dCantidad= 0;
                string dato
                      , strPresentacion;

                intProductoTerminadoID = Convert.ToInt32(cboArticulos.EditValue);

                for (int i = 0; i < gridViewPrincipal.RowCount - 1; i++)
                {
                    dato = gridViewPrincipal.GetRowCellValue(i, "Componente").ToString();
                    if (dato.Length > 0)
                    {

                        intComponente = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(i, "Componente"));
                        dCantidad = Convert.ToDecimal(gridViewPrincipal.GetRowCellValue(i, "Cantidad"));
                        strPresentacion = gridViewPrincipal.GetRowCellValue(i, "Presentacion").ToString();
                        dtComponentes.Rows.Add(intProductoTerminadoID, intComponente, dCantidad, strPresentacion);

                    }
                }

                ComponentesCL cl = new ComponentesCL();
                cl.dtm = dtComponentes;
                cl.intArticulosID = intProductoTerminadoID;
                cl.intUsuarioID = intUsuarioID;
                cl.strMaquina = Environment.MachineName;

                Result = cl.ComponentesCrud();

                if (Result == "OK")
                {
                    MessageBox.Show("Guardado correctamente");
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

        private string Valida()
        {
            if (cboArticulos.EditValue == null)
            {
                return "Seleccione un articulo";
            }
            //if (gridViewPrincipal.Container == 0)
            //{
            //    return "No hay componentes capturados";
            //}

            return "OK";
        } //Valida


        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }


        //private void BuscarFactorUM2()
        //{
         
        //    articulosCL cl = new articulosCL();
        //    cl.intArticulosID = intComponente;
        //    string result = cl.articulosLlenaCajas();
        //    if (result == "OK")
        //    {
        //        intFactorUm2 = cl.intFactorUM2;

        //    }
        //}

        private void repositoryItemLookUpEditArticulo_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbiCargar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CargaComponentes();
        }

        private void CargaComponentes()
        {
            
            Inicialisalista();
            LlenarGrid();
            cboArticulos.Enabled = false;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            cboArticulos.Enabled = true;
            Inicialisalista();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void cboArticulos_EditValueChanged(object sender, EventArgs e)
        {
            object orow = cboArticulos.Properties.GetDataSourceRowByKeyValue(cboArticulos.EditValue);
            if (orow != null)
            {
                dblFactorUm2 = Convert.ToDouble(((DataRowView)orow)["FactorUm2"]);
                if (dblFactorUm2 == 0)
                    dblFactorUm2 = 1;
                lblPresentacion.Text = "Presentación: " + dblFactorUm2.ToString();
            }
        }

        private void bbiPrevio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = cboArticulos.Text;
            gridControlPrincipal.ShowRibbonPrintPreview();
        }


        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
         
            decimal dArebajar=0, dCosto=0, dImporte=0;
            DateTime fFecha = DateTime.Now;
            try
            {
                //Sacamos datos del artículo
                if (e.Column.Name == "gridColumnComponente")
                {
                    articulosCL cl = new articulosCL();
                    string art = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Componente").ToString();
                    decimal dCantidad = Convert.ToDecimal(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Cantidad"));
                    if (art.Length > 0) //validamos que haya algo en la celda
                    {
                        cl.intArticulosID = Convert.ToInt32(art);
                        string result = cl.articulosLlenaCajas();
                        if (result == "OK")
                        {
                            //intFactorUm2 = cl.intFactorUM2;

                            dArebajar = Convert.ToDecimal(Convert.ToDouble(dCantidad) * dblFactorUm2);
                            dCosto = 0;

                            ComponentesCL clc = new ComponentesCL();
                            clc.intArticulosID = Convert.ToInt32(art);
                            clc.Fecha = Convert.ToDateTime(fFecha.ToShortDateString());
                            String Result = clc.fnObtenerUltimoCosto();
                            if (Result == "OK")
                            {
                                dCosto = clc.dUltimoCosto;
                            }
                            else { dCosto = 0; }

                            dImporte = dArebajar * dCosto;

                            if (dArebajar == 0) { }
                            else
                            {
                                gridViewPrincipal.SetFocusedRowCellValue("Arebajar", dArebajar);
                            }
                            if (dCosto == 0) { }
                            else
                            {
                                gridViewPrincipal.SetFocusedRowCellValue("Costo", dCosto);
                            }
                            if (dImporte == 0) { }
                            else
                            {
                                gridViewPrincipal.SetFocusedRowCellValue("Importe", dImporte);
                            }                       

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("GridviewDetalle Changed: " + ex);
            }
        }







    }
}