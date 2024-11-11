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
using VisualSoftErp.Clases.HerrramientasCLs;

namespace VisualSoftErp.Catalogos.Ventas
{
    public partial class Listasdeprecios : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private bool permisosEscritura;
        private bool blnNuevo;
        private int intFolioSeleccionado;
        string strNom = string.Empty;
        string strMoneda = string.Empty;
        string strGridActual = string.Empty;
        public Listasdeprecios()
        {
            InitializeComponent();
            PermisosEscritura();
            strGridActual = "principal";
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;

            gridViewDetalle.OptionsView.ShowAutoFilterRow = true;
            gridViewDetalle.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewDetalle.OptionsSelection.MultiSelect = true;
            gridViewDetalle.OptionsView.ShowAutoFilterRow = true;

            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;

            CargaCombos();
            LLenaGridMaster();

            DatosdecontrolCL clg = new DatosdecontrolCL();
            string result = clg.DatosdecontrolLeer();
            if (result == "OK")
            {
                if (clg.iManejarieps == 0)
                {
                    gridColumnPtjeIeps.Visible = false;
                }
                if (clg.iManejariva == 0)
                {
                    gridColumnPtjeIva.Visible = false;
                }
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            //LlenaGrid();
        }

        public class operacionbase
        {
            public string Clave { get; set; }
            public string Des { get; set; }
        }

        private void PermisosEscritura()
        {
            globalCL clg = new globalCL();
            UsuariosCL usuarios = new UsuariosCL();

            clg.strPrograma = "0410";
            if (clg.accesoSoloLectura()) permisosEscritura = false;
            else permisosEscritura = true;
        }

        private void LLenaGridMaster()
        {
            ListasdeprecioCL cl = new ListasdeprecioCL();
            gridControlPrincipal.DataSource = cl.ListasdeprecioGrid();
        }

        private void LlenaGridDetalle()
        {
            try
            {               
                ListasdeprecioCL cl = new ListasdeprecioCL();

                if (blnNuevo) 
                    gridControlDetalle.DataSource = cl.ListasdeprecioLineasFamiliasGrid();
                else
                {
                    cl.intListasdeprecioID = intFolioSeleccionado;
                    gridControlDetalle.DataSource = cl.ListasdeprecioLineasFamiliasEditarGrid();
                    txtNombre.Text = strNom; //gridViewDetalle.GetRowCellValue(0, "NombreLista").ToString();
                    cboMonedas.EditValue = strMoneda; //gridViewDetalle.GetRowCellValue(0, "MonedasID").ToString();
                }

                globalCL clg = new globalCL();
                clg.strGridLayout = "gridLpDetalle";
                clg.restoreLayout(gridViewDetalle);

                if (!blnNuevo)
                    gridViewDetalle.ActiveFilterString = "[Precio]<>0.00";
                else
                    gridViewDetalle.ActiveFilter.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Llenagrid: " + ex.Message);
            }
        }

        private void Nuevo()
        {
            blnNuevo = true;
            LlenaGridDetalle();
            BotonesEdicion();
                     
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            
            clg.strGridLayout = "gridLpprincipal";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void llenaListasBase()
        {
            combosCL cl = new combosCL();

            cl.strTabla = "Listasbase";
            cboListaBase.Properties.ValueMember = "Clave";
            cboListaBase.Properties.DisplayMember = "Des";
            cboListaBase.Properties.DataSource = cl.CargaCombos();
            cboListaBase.Properties.ForceInitialize();
            cboListaBase.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboListaBase.Properties.PopulateColumns();
            cboListaBase.Properties.Columns["Clave"].Visible = false;
            cboListaBase.EditValue = cboListaBase.Properties.GetDataSourceValue(cboListaBase.Properties.ValueMember, 0);
            cboListaBase.ItemIndex = 0;

            bbiPreciobase.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void BotonesEdicion()
        {
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiGuardar.Enabled = permisosEscritura;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;           
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            if (blnNuevo)
            {
                gridColumnArticulosID.Visible = false;
                globalCL cl = new globalCL();
                cl.sTabla = "Listasdeprecios";
                cl.sCondicion = "";
                string result = cl.BuscaSiguienteID();
                if (result=="OK")
                {
                    if (cl.iSigID==1)
                    {
                        splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
                        gridColumnPrecioBase.Visible = false;                       
                    }
                    else
                    {

                        llenaListasBase();
                    }
                }
                else
                {
                    llenaListasBase();
                    splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
                }

            }
            else
            {
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
            }

            navigationFrame.SelectedPageIndex = 1;

        }//BotonesEdicion()

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;           
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiAgregarPrecios.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiPreciobase.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiAplicarVariacion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            globalCL clg = new globalCL();
            clg.strGridLayout = "gridLpDetalle";
            string result = clg.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            LLenaGridMaster();
           
            navigationFrame.SelectedPageIndex = 0;
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "gridColumnPrecio")
            {
                decimal precio = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Precio"));
                decimal pIva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "PtjeIva"));
                decimal pIeps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "PtjeIeps"));
                decimal Iva = (precio * (pIva / 100));
                decimal Ieps = (precio * (pIeps / 100));
                decimal Neto = precio + Iva + Ieps;
                gridViewDetalle.SetRowCellValue(gridViewDetalle.FocusedRowHandle, "PrecioNeto",Neto);
            }
        }
        
        private void CargaCombos()
        {

            List<operacionbase> tipoope = new List<operacionbase>();
                   
            tipoope.Add(new operacionbase() { Clave = "A", Des = "Aumentar" });
            tipoope.Add(new operacionbase() { Clave = "D", Des = "Disminuir" });

            cboOperacion.Properties.ValueMember = "Clave";
            cboOperacion.Properties.DisplayMember = "Des";
            cboOperacion.Properties.DataSource = tipoope;
            cboOperacion.Properties.ForceInitialize();
            cboOperacion.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboOperacion.Properties.PopulateColumns();
            cboOperacion.Properties.Columns["Clave"].Visible = false;
            cboOperacion.ItemIndex = 0;

            combosCL cl = new combosCL();
            
            cl.strTabla = "Monedas";
            cboMonedas.Properties.ValueMember = "Clave";
            cboMonedas.Properties.DisplayMember = "Des";
            cboMonedas.Properties.DataSource = cl.CargaCombos();
            cboMonedas.Properties.ForceInitialize();
            cboMonedas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboMonedas.Properties.PopulateColumns();
            cboMonedas.Properties.Columns["Clave"].Visible = false;
            cboMonedas.EditValue = cboMonedas.Properties.GetDataSourceValue(cboMonedas.Properties.ValueMember, 0);
            cboMonedas.ItemIndex = 0;
        
          

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
                System.Data.DataTable dtListasdeprecio = new System.Data.DataTable("Listasdeprecio");
                dtListasdeprecio.Columns.Add("ListasdeprecioID", Type.GetType("System.Int32"));
                dtListasdeprecio.Columns.Add("Nombre", Type.GetType("System.String"));
                dtListasdeprecio.Columns.Add("MonedasID", Type.GetType("System.String"));
                dtListasdeprecio.Columns.Add("Fechaultimaactualizacion", Type.GetType("System.DateTime"));

                System.Data.DataTable dtListasdepreciodetalle = new System.Data.DataTable("Listasdepreciodetalle");
                dtListasdepreciodetalle.Columns.Add("ListasdeprecioID", Type.GetType("System.Int32"));
                dtListasdepreciodetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtListasdepreciodetalle.Columns.Add("Precio", Type.GetType("System.Decimal"));
                dtListasdepreciodetalle.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtListasdepreciodetalle.Columns.Add("Ieps", Type.GetType("System.Decimal"));
                dtListasdepreciodetalle.Columns.Add("Neto", Type.GetType("System.Decimal"));

                int iFolio = 0;
                globalCL clg = new globalCL();
                if (blnNuevo)
                {
                    //Siguiente folio
                    
                    
                    clg.sTabla = "Listasdeprecios";
                    clg.sCondicion = "";

                    Result = clg.BuscaSiguienteID();
                    if (Result != "OK")
                    {
                        MessageBox.Show("No se pudo leer el siguiente folio");
                        return;
                    }
                    iFolio = clg.iSigID;
                }else
                {
                    iFolio = intFolioSeleccionado;
                }
               
                string dato = String.Empty;
                int intArticulosID = 0;
                decimal dPrecio = 0;
                decimal dIva = 0;
                decimal dIeps = 0;
                int iRens = 0;
                decimal dNeto = 0;

                gridViewDetalle.ActiveFilter.Clear();

                for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "Precio").ToString();
                    if (dato.Length > 0)
                    {
                        if (clg.esNumerico(dato))
                        {
                            if (Convert.ToDecimal(dato)>0)
                            {
                                intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "ArticulosID"));
                                dPrecio = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Precio"));
                                dIva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "PtjeIva"));
                                dIeps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "PtjeIeps"));
                                dNeto = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "PrecioNeto"));

                                dtListasdepreciodetalle.Rows.Add(iFolio, intArticulosID, dPrecio, dIva, dIeps, dNeto);
                                iRens = iRens + 1;
                            }                            
                        }
                    }
                }
                
                if (iRens==0)
                {
                    MessageBox.Show("Debe capturar al menos un artículo con precio mayor de cero");
                    return;
                }
                
                string strNombre = txtNombre.Text;
                string strMoneda = cboMonedas.EditValue.ToString();
                DateTime fFechaultimaactualizacion = Convert.ToDateTime(DateTime.Now);
                dato = String.Empty;

                dtListasdeprecio.Rows.Add(iFolio, strNombre, strMoneda, fFechaultimaactualizacion);

                ListasdeprecioCL cl = new ListasdeprecioCL();
                cl.intListasdeprecioID = iFolio;
                cl.dtm = dtListasdeprecio;
                cl.dtd = dtListasdepreciodetalle;
                Result = cl.ListasdeprecioCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    
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
            if (txtNombre.Text.Length == 0)
            {
                return "El campo Nombre no puede ir vacio";
            }
            
            return "OK";
        } //Valida

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            Guardar();
        }

        private void gridViewDetalle_DoubleClick(object sender, EventArgs e)
        {
            if (intFolioSeleccionado == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intFolioSeleccionado = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ListasdeprecioID"));
            strNom = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Nombre").ToString();
            strMoneda = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "MonedasID").ToString();
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolioSeleccionado == 0 && strNom != "PUBLICO")
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void Editar()
        {
            strGridActual = "Detalle";
            blnNuevo = false;
            LlenaGridDetalle();
            BotonesEdicion();
            bbiAgregarPrecios.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void bbiAgregarPrecios_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridViewDetalle.ActiveFilter.Clear();
            bbiAgregarPrecios.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void bbiPreciobase_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtVariacion.Text.Length==0)
            {
                MessageBox.Show("Teclee el % de variación (5, 10, 20 etc)");
                return;
            }

            globalCL cl = new globalCL();

            if (!cl.esNumerico(txtVariacion.Text)) {
                MessageBox.Show("Teclee el % de variación (5, 10, 20 etc)");
                return;
            }

            if (Convert.ToDecimal(txtVariacion.Text)<=0)
            {
                MessageBox.Show("Teclee el % de variación (5, 10, 20 etc)");
                return;
            }

            PrecioBase();
        }

        private void PrecioBase()
        {
            try
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Precios","Revisando precio base, espere por favor...");
                ListasdeprecioCL cl = new ListasdeprecioCL();
                decimal precio = 0;
                decimal preciobase = 0;
                decimal pIva = 0;
                decimal pIeps = 0;
                decimal Iva = 0;
                decimal Ieps = 0;
                decimal Neto = 0;
                string ope = cboOperacion.EditValue.ToString();
                decimal variacion = Convert.ToDecimal(txtVariacion.Text);


                int art = 0;
                string result = string.Empty;



                int lista = Convert.ToInt32(cboListaBase.EditValue);
                cl.intListasdeprecioID = lista;

                gridColumnPrecioBase.Visible = true;
                foreach (int handle in gridViewDetalle.GetSelectedRows()) {
                    gridViewDetalle.FocusedRowHandle = handle;

                    art = Convert.ToInt32(gridViewDetalle.GetRowCellValue(handle, "ArticulosID"));
                    cl.intArt = art;
                    result = cl.ListasdeprecioLeeunPrecio();
                    if (result=="OK")
                    {
                        preciobase = cl.dPrecio;
                        

                        if (ope=="A")
                        {
                            precio = Math.Round(preciobase * (1 + (variacion/100)),2);
                        }
                        else
                        {
                            precio = Math.Round(preciobase * (1 - (variacion / 100)),2);
                        }

                        pIva = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "PtjeIva"));
                        pIeps = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "PtjeIeps"));
                        Iva = (precio * (pIva / 100));
                        Ieps = (precio * (pIeps / 100));
                        Neto = precio + Iva + Ieps;

                        gridViewDetalle.SetFocusedRowCellValue("PrecioBase", preciobase);
                        gridViewDetalle.SetFocusedRowCellValue("Precio", precio);
                        gridViewDetalle.SetRowCellValue(gridViewDetalle.FocusedRowHandle, "PrecioNeto", Neto);

                    }                   
                }
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiPrevio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (strGridActual == "principal")
            {
                gridControlPrincipal.ShowRibbonPrintPreview();

            }
            else
            {
                gridControlDetalle.ShowRibbonPrintPreview();
            }
        }
    }
}