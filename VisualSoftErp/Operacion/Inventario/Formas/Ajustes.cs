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
using VisualSoftErp.Operacion.Ventas.Clases;
using System.Configuration;

namespace VisualSoftErp.Operacion.Inventarios.Formas
{
    public partial class Ajustes : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intUsuarioID = globalCL.gv_UsuarioID;
        public BindingList<detalleCL> detalle;
        public bool blNuevo;

        string strSerie;
        int intFolio;


        public Ajustes()
        {
            InitializeComponent();

            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;

            gridColumnDes.OptionsColumn.ReadOnly = true;
            gridColumnDes.OptionsColumn.AllowEdit = false;

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Ajustes";

            txtObservaciones.Properties.MaxLength = 250;
            txtObservaciones.EnterMoveNextControl = true;


            LlenarGrid();

            txtFecha.Text = DateTime.Now.ToShortDateString();

            CargaCombos();

            cboSerie.Enabled = true;

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private string BuscarSerie()
        {

            cboSerie.ItemIndex = 0;
            return "OK";

            SerieCL cl = new SerieCL();
            cl.intUsuarioID = intUsuarioID;
            String Result = cl.BuscarSerieporUsuario();
            if (Result == "OK")
            {
                if (cl.strSerie == "")
                { cboSerie.ReadOnly = false; }
                else
                {
                    cboSerie.EditValue = cl.strSerie;
                    cboSerie.ReadOnly = true;
                }

            }
            else
            {
                MessageBox.Show(Result);
            }
            return "";
        } //BuscaSerie

        private void LlenarGrid()
        {
            AjustesCL cl = new AjustesCL();
            gridControlPrincipal.DataSource = cl.AjustesGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAjustes";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        public class detalleCL
        {
            public decimal Cantidad { get; set; }
            public string Articulo { get; set; }
            public string Des { get; set; }
            public string Operacion { get; set; }
            public string Observaciones { get; set; }
        }

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;



        }

        public class operacionCL
        {
            public string Clave { get; set; }
            public string Des { get; set; }
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();

     
            cl.strTabla = "Serie";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Des";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Clave"].Visible = false;
            cboSerie.ItemIndex = 0;

            cl.strTabla = "Almacenes";
            cboAlmacenesID.Properties.ValueMember = "Clave";
            cboAlmacenesID.Properties.DisplayMember = "Des";
            cboAlmacenesID.Properties.DataSource = cl.CargaCombos();
            cboAlmacenesID.Properties.ForceInitialize();
            cboAlmacenesID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacenesID.Properties.PopulateColumns();
            cboAlmacenesID.Properties.Columns["Clave"].Visible = false;
            cboAlmacenesID.ItemIndex = 0;

            cl.strTabla = "Articulos";
            repositoryItemLookUpEditArticulos.ValueMember = "Clave";
            repositoryItemLookUpEditArticulos.DisplayMember = "Des";
            repositoryItemLookUpEditArticulos.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulos.ForceInitialize();
            repositoryItemLookUpEditArticulos.PopulateColumns();
            repositoryItemLookUpEditArticulos.Columns["Clave"].Visible = false;
            repositoryItemLookUpEditArticulos.Columns["FactorUM2"].Visible = false;
            repositoryItemLookUpEditArticulos.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulos.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            

            List<operacionCL> opeList = new List<operacionCL>();
            opeList.Add(new operacionCL() { Clave = "A", Des = "Aumentar" });
            opeList.Add(new operacionCL() { Clave = "D", Des = "Disminuir" });

            repositoryItemLookUpEditOpe.ValueMember = "Clave";
            repositoryItemLookUpEditOpe.DisplayMember = "Des";
            repositoryItemLookUpEditOpe.DataSource = opeList;
            repositoryItemLookUpEditOpe.ForceInitialize();
            repositoryItemLookUpEditOpe.PopulateColumns();
            repositoryItemLookUpEditOpe.Columns["Clave"].Visible = false;
            repositoryItemLookUpEditOpe.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditOpe.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

        }//CargaCombo

        private void SiguienteID()
        {
            string serie = ConfigurationManager.AppSettings["serie"].ToString();

            AjustesCL cl = new AjustesCL();
            cl.strSerie = serie;
            cl.strDoc = "Ajustes";

            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {
                txtFolio.Text = cl.intID.ToString();
            }
            else
            {
                MessageBox.Show("SiguienteID :" + result);
            }

        }//SguienteID

        private void Nuevo()
        {
            CargaCombos();
            BuscarSerie();
            blNuevo = true;
            SiguienteID();
            txtFechadeconteo.EditValue = DateTime.Now.ToShortDateString();
            txtFecha.Text = DateTime.Now.ToShortDateString();
            Inicialisalista();
            navigationFrame.SelectedPageIndex = 1;
            ribbonPageGroupHome.Visible = false;
            ribbonPageGroup2.Visible = true;
            navBarControl.Visible = false;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAjustesDetalle";
            clg.restoreLayout(gridViewDetalle);
        }

        private void LimpiaCajas()
        {

            cboSerie.ItemIndex = 0;
            txtObservaciones.Text = string.Empty;
            cboAlmacenesID.ItemIndex = 0;
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtFechadeconteo.Text = DateTime.Now.ToShortDateString();
            txtNumero.Text = "0";
            txtMarbete.Text = "0";

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
                string sCondicion = String.Empty;
                System.Data.DataTable dtAjustes = new System.Data.DataTable("Ajustes");
                dtAjustes.Columns.Add("Serie", Type.GetType("System.String"));
                dtAjustes.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtAjustes.Columns.Add("Observaciones", Type.GetType("System.String"));
                dtAjustes.Columns.Add("AlmacenesID", Type.GetType("System.Int32"));
                dtAjustes.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtAjustes.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtAjustes.Columns.Add("Fechadeconteo", Type.GetType("System.DateTime"));
                dtAjustes.Columns.Add("Numero", Type.GetType("System.Int32"));
                dtAjustes.Columns.Add("Marbete", Type.GetType("System.Int32"));
                dtAjustes.Columns.Add("FechaReal", Type.GetType("System.DateTime"));

                System.Data.DataTable dtAjustesdetalle = new System.Data.DataTable("Ajustesdetalle");
                dtAjustesdetalle.Columns.Add("Serie", Type.GetType("System.String"));
                dtAjustesdetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtAjustesdetalle.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtAjustesdetalle.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtAjustesdetalle.Columns.Add("Cantidad", Type.GetType("System.Decimal"));
                dtAjustesdetalle.Columns.Add("Operacion", Type.GetType("System.String"));
                dtAjustesdetalle.Columns.Add("Observaciones", Type.GetType("System.String"));


                if (blNuevo)
                {
                    SiguienteID();
                }

                globalCL glC = new globalCL();
                if (!glC.esNumerico(txtFolio.Text))
                {
                    MessageBox.Show("El folio debe ser númerico");
                    return;
                }
                if (!glC.esNumerico(txtNumero.Text))
                {
                    txtNumero.Text = "0";
                }
                if (!glC.esNumerico(txtMarbete.Text))
                {
                    txtMarbete.Text = "0";
                }

                strSerie = cboSerie.EditValue.ToString();
                intFolio = Convert.ToInt32(txtFolio.Text);
                int intSeq = 0;
                int intArticulosID = 0;
                decimal dCantidad = 0;
                string strOperacion = "";
                string strObservaciones = "";
                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {

                    intSeq = i;
                    intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Articulo"));
                    dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Cantidad"));
                    strOperacion = gridViewDetalle.GetRowCellValue(i, "Operacion").ToString();

                    if (gridViewDetalle.GetRowCellValue(i, "Observaciones") != null)
                        strObservaciones = gridViewDetalle.GetRowCellValue(i, "Observaciones").ToString();
                    else
                        strObservaciones = string.Empty;

                    dtAjustesdetalle.Rows.Add(strSerie, intFolio, intSeq,
                        intArticulosID, dCantidad, strOperacion, strObservaciones);

                }
                string strObservacionesM = txtObservaciones.Text;
                int intAlmacenesID = Convert.ToInt32(cboAlmacenesID.EditValue);
                DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
                DateTime Fechadeconteo = Convert.ToDateTime(txtFechadeconteo.Text);
                int intNumero = Convert.ToInt32(txtNumero.Text);
                int intMarbete = Convert.ToInt32(txtMarbete.Text);
                DateTime fFechaReal = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                dtAjustes.Rows.Add(strSerie, intFolio, strObservaciones, intAlmacenesID, intUsuarioID, Fecha, Fechadeconteo,
                    intNumero, intMarbete, fFechaReal);

                AjustesCL cl = new AjustesCL();
                cl.dtm = dtAjustes;
                cl.dtd = dtAjustesdetalle;
                cl.strSerie = cboSerie.EditValue.ToString();
                cl.intFolio = Convert.ToInt32(txtFolio.Text);
                cl.intUsuarioID = intUsuarioID;
                cl.strMaquina = Environment.MachineName;
                Result = cl.AjustesCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    LimpiaCajas();
                    Inicialisalista();
                    SiguienteID();
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
            
            
            if (cboAlmacenesID.EditValue == null)
            {
                return "El campo AlmacenesID no puede ir vacio";
            }

            return "OK";
        } //Valida

        private void Editar()
        {
            ribbonPageGroupHome.Visible = false;
            ribbonPageGroup2.Visible = true;
            navBarControl.Visible = false;
            llenaCajas();
            DetalleLlenaCajas();
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAjustesDetalle";
            clg.restoreLayout(gridViewDetalle);
            navigationFrame.SelectedPageIndex = 1;
        }

        private void llenaCajas()
        {
            AjustesCL cl = new AjustesCL();
            cl.strSerie = strSerie;
            cl.intFolio = intFolio;
            String Result = cl.AjustesLlenaCajas();
            if (Result == "OK")
            {
                txtFolio.Text = intFolio.ToString();
                cboAlmacenesID.EditValue = cl.intAlmacenesID;
                txtFechadeconteo.Text = cl.fFechadeconteo.ToShortDateString();
                txtNumero.Text = cl.intNumero.ToString();
                txtMarbete.Text = cl.intMarbete.ToString();
                txtObservaciones.Text = cl.strObservaciones;
            }
            else
            {
                MessageBox.Show(Result);
            }
        } // llenaCajas

        private void DetalleLlenaCajas()
        {
            try
            {
                AjustesCL cl = new AjustesCL();
                cl.intFolio = intFolio;
                cl.strSerie = strSerie;
                gridControlDetalle.DataSource = cl.Detallellenacajas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }

        }//DetalleLlenaCajas



        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }

        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
        }



        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }

        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                //Sacamos datos del artículo
                if (e.Column.Name == "gridColumnArticulo")
                {

                    articulosCL cl = new articulosCL();
                    string art = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulo").ToString();
                    if (art.Length > 0) //validamos que haya algo en la celda
                    {
                        cl.intArticulosID = Convert.ToInt32(art);
                        string result = cl.articulosLlenaCajas();
                        if (result == "OK")
                        {
                            gridViewDetalle.SetFocusedRowCellValue("Des", cl.strNombre);

                        }

                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void bbiGuardar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageGroupHome.Visible = true;
            ribbonPageGroup2.Visible = false;
            navBarControl.Visible = true;
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
            blNuevo = false;
            intFolio = 0;
            strSerie = "";

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAjustesDetalle";
            string result = clg.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
        }

        private void bbieditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }
        private void gridViewPrincipal_RowClick(Object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
        }

        private void bbiVistaPrevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridViewPrincipal.ShowRibbonPrintPreview();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridAjustes";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

           
            this.Close();
        }
        #region  filtros meses

       
        private void navBarItemEne_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 1";
        }

        private void navBarItemFeb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 2";
        }

        private void navBarItemMar_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 3";
        }

        private void navBarItemAbr_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 4";
        }

        private void navBarItemMay_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 5";
        }

        private void navBarItemJun_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 6";
        }

        private void navBarItemJul_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 7";
        }

        private void navBarItemAgo_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 8";
        }

        private void navBarItemSep_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 9";
        }

        private void navBarItemOct_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 10";
        }

        private void navBarItemNov_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 11";
        }

        private void navBarItemDic_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilterString = "[Año]=" + DateTime.Now.Year.ToString() + " and [Mes]= 12";
        }

  

        private void navBarItemTodos_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridViewPrincipal.ActiveFilter.Clear();
        }





        #endregion

        private void txtFechadeconteo_EditValueChanged(object sender, EventArgs e)
        {
            txtFecha.Text = txtFechadeconteo.Text;
        }
    }
}