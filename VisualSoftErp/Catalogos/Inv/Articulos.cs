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
using System.Configuration;
using System.IO;
using DevExpress.XtraPdfViewer;

namespace VisualSoftErp.Catalogos
{
    public partial class Articulos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region Variables
        int intArticulosID;
        int intFamiliaID;
        int intUnidadID;
        int intTAID;
        string strimagen;
        int intEsunkit;
        string OrigenRegresar;
        bool blnNuevo;
        bool blnViendoImagen;
        #endregion

        public Articulos()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Catálogo de Artículos";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

            llenarcombos();

        }

        #region Metodos
        private void LlenarGrid()
        {
            articulosCL cl = new articulosCL();
            grdArticulos.DataSource = cl.articulosGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridArticulos";
            clg.restoreLayout(gridView1);
            gridView1.OptionsView.ShowAutoFilterRow = true;

        }//LlenarGrid()

        public void llenarcombos()
        {
            combosCL cl = new combosCL();

            cboFamilias.Properties.ValueMember = "Clave";
            cboFamilias.Properties.DisplayMember = "Des";
            cl.strTabla = "Familias";
            cboFamilias.Properties.DataSource = cl.CargaCombos();
            cboFamilias.Properties.ForceInitialize();
            cboFamilias.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboFamilias.Properties.ForceInitialize();
            cboFamilias.Properties.PopulateColumns();
            cboFamilias.Properties.Columns["Clave"].Visible = false;
           

            cboUnidad.Properties.ValueMember = "Clave";
            cboUnidad.Properties.DisplayMember = "Des";
            cl.strTabla = "Unidadesdemedida";
            cboUnidad.Properties.DataSource = cl.CargaCombos();
            cboUnidad.Properties.ForceInitialize();
            cboUnidad.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboUnidad.Properties.ForceInitialize();
            cboUnidad.Properties.PopulateColumns();
            cboUnidad.Properties.Columns["Clave"].Visible = false;
           

            cboTA.Properties.ValueMember = "Clave";
            cboTA.Properties.DisplayMember = "Des";
            cl.strTabla = "Tiposdearticulo";
            cboTA.Properties.DataSource = cl.CargaCombos();
            cboTA.Properties.ForceInitialize();
            cboTA.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTA.Properties.ForceInitialize();
            cboTA.Properties.PopulateColumns();
            cboTA.Properties.Columns["Clave"].Visible = false;

            cl.ActualizaCombo(cboMonedaCosto, "Monedas", "");
            
          
        }//llenarcombos

        private void Nuevo()
        {
            blnNuevo = true;
            BotonesEdicion();
            intArticulosID = 0;
            cboFamilias.EditValue = 0;
            cboUnidad.EditValue = 0;
            cboTA.EditValue = 0;
            txtArticulo.Focus();
            LimpiaCajas();

        }//Nuevo()

        private void LimpiaCajas()
        {
            txtArticulo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtOrden.Text = string.Empty;
            cboFamilias.ItemIndex = 0;
            cboUnidad.ItemIndex = 0;
            cboTA.ItemIndex = 0;
            txtPieps.Text = string.Empty;
            txtPiva.Text = string.Empty;
            swExistencia.IsOn = false;
            txtClaveSat.Text = string.Empty;
            txtImagen.Text = "X";
            swActivo.IsOn = true;
            swEsunkit.IsOn = false;
            swObsoleto.IsOn = false;
            txtTiempo.Text = string.Empty;
            txtMax.Text = string.Empty;
            txtMin.Text = string.Empty;
            txtReorden.Text = string.Empty;
            txtMargenarribadelcosto.Text = string.Empty;
        }//LimpiaCajas()

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

            if (!blnNuevo)
            {
                bbiImagen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            
            OrigenRegresar = "Principal";
            navigationFrame.SelectedPageIndex = 1;

        }//BotonesEdicion()

        private void Guardar()
        {
            try
            {
                string result = Valida();

                if (result != "OK")
                {
                    if (result.Length>0)
                    {
                        MessageBox.Show(result);
                       
                    }
                    return;

                }

                articulosCL cl = new articulosCL();
                cl.intArticulosID = intArticulosID;
                cl.strArticulo = txtArticulo.Text;
                cl.strNombre = txtNombre.Text;
                cl.strNombreOC = txtOrden.Text;
                cl.intFamiliasID = intFamiliaID;
                cl.intUnidadesdemedidaID = intUnidadID;
                cl.intPtjeIva = Convert.ToDecimal(txtPiva.Text);
                cl.intPtjeIeps = Convert.ToDecimal(txtPieps.Text);
                cl.intManejaexistencia = swExistencia.IsOn ? 1 : 0;                
                cl.strClaveSat = txtClaveSat.Text;
                cl.strImagen = txtImagen.Text;
                if (swActivo.IsOn) { cl.intActivo = 1; }
                else { { cl.intActivo = 0; } }
                if (swEsunkit.IsOn) { cl.intEsunkit = 1; }
                else { { cl.intEsunkit = 0; } }
                cl.intObsoleto = swObsoleto.IsOn == true ? 1 : 0; 
                cl.intTiempodeentrega = Convert.ToInt32(txtTiempo.Text);
                cl.intTiposdearticuloID = intTAID;
                cl.intMaximo = Convert.ToInt32(txtMax.Text);
                cl.intMinimo = Convert.ToInt32(txtMin.Text);
                cl.intReorden = Convert.ToInt32(txtReorden.Text);
                cl.strFraccionArancelaria = txtfraccion.Text;
                cl.strUnidadAduana = txtunidad.Text;
                cl.strKilosAduana = Convert.ToDecimal(txtkilos.Text);
                cl.decMargenarribadelcosto = Convert.ToDecimal(txtMargenarribadelcosto.Text);
                result = cl.ArticulosCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (intArticulosID == 0)
                    {
                        LimpiaCajas();
                    }

                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: "+ex.Message);
            }
        }//Guardar

        private string Valida()
        {

            if (txtArticulo.Text == "")
            {
                return "El articulo no puede ir vacio";
            }
            if (intFamiliaID == 0)
            {
                return "Seleccione la familia del articulo";
            }
            if (intUnidadID == 0)
            {
                return "Seleccione la Unidad de medida del articulo";
            }
            if (intTAID == 0)
            {
                return "Seleccione el Tipo de articulo";
            }
            if (intFamiliaID == 0)
            {
                return "Seleccione la familia del articulo";
            }
            if (txtNombre.Text == "")
            {
                return "El nombre no puede ir vacio";
            }
            if (txtOrden.Text == "")
            {
                txtReorden.Text = "0";
            }
            if (txtPiva.Text == "")
            {
                DialogResult dialogResult = MessageBox.Show("El iva debe ser cero?", "Advertencia", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    txtPiva.Text = "0";
                }
                else
                {
                    return "";
                }
            }
            if (txtPieps.Text == "")
            {
                txtPieps.Text = "0";
            }
            if (txtClaveSat.Text == "")
            {
                return "La clave SAT no puede ir vacio";
            }
            if (txtImagen.Text == "")
            {
                txtImagen.Text = "X";
            }
            if (txtTiempo.Text == "")
            {
                txtTiempo.Text = "0";
            }
            if (txtMax.Text == "")
            {
                txtMax.Text = "0";
            }
            if (txtMin.Text == "")
            {
                txtMin.Text = "0";
            }
            if (txtReorden.Text == "")
            {
                txtReorden.Text = "0";
            }
            if (txtfraccion.Text == "")
            {
                txtfraccion.Text = "";
            }
            if (txtunidad.Text == "")
            {
                txtunidad.Text = "";
            }
            if (txtkilos.Text == "")
            {
                txtkilos.Text = "0";
            }

            globalCL clg = new globalCL();
            if (!clg.esNumerico(txtMargenarribadelcosto.Text))
            {
                txtMargenarribadelcosto.Text = "0";
            }
            return "OK";
        }//Valida()

        private void Editar()
        {
            blnNuevo = false;
            blnViendoImagen = false;
            BotonesEdicion();
            bbiCostos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            llenaCajas();

        }//Editar()

        private void llenaCajas()
        {
            articulosCL cl = new articulosCL();
            cl.intArticulosID = intArticulosID;
            string result = cl.articulosLlenaCajas();

            if (result == "OK")
            {
                txtArticulo.Text = cl.strArticulo;
                txtNombre.Text = cl.strNombre;
                txtOrden.Text = cl.strNombreOC;
                cboFamilias.EditValue = cl.intFamiliasID;
                cboUnidad.EditValue = cl.intUnidadesdemedidaID;
                txtPiva.Text = Convert.ToString(cl.intPtjeIva);
                txtPieps.Text = Convert.ToString(cl.intPtjeIeps);
                swExistencia.IsOn = cl.intManejaexistencia == 1 ? true : false;
                txtClaveSat.Text = cl.strClaveSat;
                txtImagen.Text = cl.strImagen;
                if (cl.intActivo == 1) { swActivo.IsOn = true; }
                else { swActivo.IsOn = false; }
                if (cl.intEsunkit == 1) { swEsunkit.IsOn = true; }
                else { swEsunkit.IsOn = false; }

                swObsoleto.IsOn = cl.intObsoleto == 1 ? true : false;

                txtTiempo.Text = Convert.ToString(cl.intTiempodeentrega);
                cboTA.EditValue = cl.intTiposdearticuloID;
                txtMax.Text = Convert.ToString(cl.intMaximo);
                txtMin.Text = Convert.ToString(cl.intMinimo);
                txtReorden.Text = Convert.ToString(cl.intReorden);
                txtfraccion.Text = cl.strFraccionArancelaria;
                txtunidad.Text = cl.strUnidadAduana;
                txtkilos.Text = Convert.ToString(cl.strKilosAduana);
                txtMargenarribadelcosto.Text = cl.decMargenarribadelcosto.ToString();

                intEsunkit = cl.intEsunkit;
            }

            else
            {
                MessageBox.Show(result);
            }
        }//llenaCajas()

        private void Eliminar()
        {

            KitsCL clk = new KitsCL();
            clk.intArticulosID = intArticulosID;
            string result = clk.KitsExiste();
            if (result=="OK")
            {
                MessageBox.Show("No se puede eliminar por que tiene formado un kit, elimine primero el kit");
                return;
            }

            articulosCL cl = new articulosCL();
            cl.intArticulosID = intArticulosID;
            result = cl.articulosEliminar();


            if (result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(result);
            }


        }//Eliminar()
        #endregion

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (OrigenRegresar == "Principal")
                Guardar();
            else
                Guardarcostos();
        }

        private void Guardarcostos()
        {
            try
            {
                //Validaciones
                globalCL clg = new globalCL();
                if (dateEditFechaCosto.Text=="")
                {
                    MessageBox.Show("Seleccione una fecha");
                    return;
                }
                if (!clg.esNumerico(txtCosto.Text))
                {
                    MessageBox.Show("Teclee el costo");
                    return;
                }
                if (Convert.ToDecimal(txtCosto.Text)<=0)
                {
                    MessageBox.Show("Teclee un costo mayor de cero");
                    return;
                }

                if (cboMonedaCosto.EditValue.ToString()=="MXN")
                {
                    txtParidad.Text = "1";
                }
                else
                {
                    if (!clg.esNumerico(txtParidad.Text))
                    {
                        MessageBox.Show("Teclee el tipo de cambio");
                        return;
                    }
                    if (Convert.ToDecimal(txtParidad.Text) <= 1)
                    {
                        MessageBox.Show("Teclee un tipo de cambio mayor de 1");
                        return;
                    }
                }

                //Guardar
                articulosCL cl = new articulosCL();
                cl.intArticuloscostosID = 0;
                cl.intArticulosID = intArticulosID;
                cl.fFecha = Convert.ToDateTime(dateEditFechaCosto.Text);
                cl.strMonedasID = cboMonedaCosto.EditValue.ToString();
                cl.decCosto = Convert.ToDecimal(txtCosto.Text);
                cl.decTipodecambio = Convert.ToDecimal(txtParidad.Text);
                cl.strObservacion = txtObsCosto.Text;
                cl.decMargenarribadelcosto = Convert.ToDecimal(txtMargenarribadelcosto.Text);

                

                string Result = cl.ArticuloscostosCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Costo guardado correctamente");
                    txtCosto.Text = string.Empty;
                    txtParidad.Text = string.Empty;
                    txtObsCosto.Text = string.Empty;
                    LlenaGridCosto();
                }
                else
                {
                    MessageBox.Show(Result);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Guardarcostos: " + ex.Message);
            }
        }

        private void LlenaGridCosto()
        {
            articulosCL cl = new articulosCL();
            cl.intArticulosID = intArticulosID;
            gridControlCostos.DataSource = cl.articulosCostosGrid();
            gridViewCostos.OptionsBehavior.ReadOnly = true;
            gridViewCostos.OptionsBehavior.Editable = false;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridArticulosCostos";
            clg.restoreLayout(gridViewCostos);
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intArticulosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }
            else
            {
                Editar();
                try
                {
                    string path = ConfigurationManager.AppSettings["imagenesdearticulos"].ToString();
                    string filename = path + txtImagen.Text;
                    if (File.Exists(filename))
                    {
                        
                        navigationFrame.SelectedPageIndex = 2;
                       // pictureEditArticulo.Image = Image.FromFile(filename);
                       //Aquí
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intArticulosID = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ArticulosID"));
            strimagen = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Imagen"));
        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intArticulosID == 0)
            {
                MessageBox.Show("Selecciona un renglón");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Desea eliminar la línea " + intArticulosID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
                if (OrigenRegresar=="Principal")
                {
                    bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                    bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiCostos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiImagen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    LlenarGrid();
                    navigationFrame.SelectedPageIndex = 0;
                }
                else
                {
                    globalCL clg = new globalCL();
                    clg.strGridLayout = "gridArticulosCostos";
                    string result = clg.SaveGridLayout(gridViewCostos);
                    if (result != "OK")
                    {
                        MessageBox.Show(result);
                    }

                    bbiCostos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    navigationFrame.SelectedPageIndex = 1;
                    OrigenRegresar = "Principal";
                }

        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridArticulos" +
                "";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiVista_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.ShowRibbonPrintPreview();
        }

        private void verImagen()
        {
            try
            {
                blnViendoImagen = true; 

                string fileName = System.Configuration.ConfigurationManager.AppSettings["imagenesdearticulos"].ToString() + txtArticulo.Text + ".Pdf";
                if (File.Exists(fileName))
                {
                    this.pdfViewer1.LoadDocument(fileName);
                    SizeF currentPageSize = pdfViewer1.GetPageSize(pdfViewer1.CurrentPageNumber);
                    float dpi = 110f;
                    float pageHeightPixel = currentPageSize.Height * dpi;
                    float topBottomOffset = 40f;
                    pdfViewer1.ZoomMode = PdfZoomMode.Custom;
                    pdfViewer1.ZoomFactor = ((float)pdfViewer1.ClientSize.Height - topBottomOffset) / pageHeightPixel * 100f;
                    pdfRibbonPage1.Visible = true;
                }
                bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbiCostos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                navigationFrame.SelectedPageIndex = 4;
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(pdfRibbonPage1.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiImagen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            verImagen();
        }

        private void cboTA_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //proceso de DE para obtener valor del combo
                DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;

                DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

                Object value = row["Clave"];

                intTAID = Convert.ToInt32(value);

            }

            catch (Exception ex)

            {

                //MessageBox.Show("cboTA: " + ex.Message);

            }
        }

        private void bbiCostos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Costos();
        }

        private void Costos()
        {
            lblArtcosto.Text = txtNombre.Text;
            OrigenRegresar = "Costos";
            bbiCostos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            LlenaGridCosto();
            navigationFrame.SelectedPageIndex = 2;
        }

        private void cboFamilias_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //proceso de DE para obtener valor del combo
                DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;

                DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

                Object value = row["Clave"];

                intFamiliaID = Convert.ToInt32(value);

            }

            catch (Exception ex)

            {

                //MessageBox.Show("cboFamilias: " + ex.Message);

            }
        }

        private void cboUnidad_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //proceso de DE para obtener valor del combo
                DevExpress.XtraEditors.LookUpEdit editor = sender as LookUpEdit;

                DataRowView row = (DataRowView)editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue);

                Object value = row["Clave"];

                intUnidadID = Convert.ToInt32(value);

            }

            catch (Exception ex)

            {

                //MessageBox.Show("cboUnidadMed: " + ex.Message);

            }
        }

        private void bbiregresardeimagen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCostos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            pdfRibbonPage1.Visible = false;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPage1.Text);
            navigationFrame.SelectedPageIndex = 1;
        }
    }
}