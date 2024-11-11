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
using DevExpress.XtraNavBar;
using VisualSoftErp.Operacion.CxP.Informes;
using VisualSoftErp.Operacion.Inventario.Designers;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Blending;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace VisualSoftErp.Operacion.Inventarios.Formas
{
    public partial class Produccion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intUsuarioID = globalCL.gv_UsuarioID;
        string strSerie;
        int intFolio;
        int intSeq;
        public BindingList<detalleCL> detalle;
        bool blNuevo;
        int AñoFiltro;
        int MesFiltro;
        public class detalleCL
        {
            public string CantidadCortada { get; set; }
            public string Articulo { get; set; }
            public string DesCortada { get; set; }
            public string LetraCortada { get; set; }
            public string CantidadProducida { get; set; }
            public string ArticuloProducido { get; set; }
            public string LetraProducida { get; set; }
            public string AlmacenProducido { get; set; }
            public string DesProducido { get; set; }
            public string letraproducida { get; set; }
            public string UltimoCostoCortado { get; set; }
            public string CostoProducido { get; set; }
            public string Maquina { get; set; }
        }

        public Produccion()
        {
            InitializeComponent();
        }

        private void AgregaAñosNavBar()
        {
            try
            {
                globalCL cl = new globalCL();
                System.Data.DataTable dt = new System.Data.DataTable("Años");
                dt.Columns.Add("Año", Type.GetType("System.Int32"));
                cl.strTabla = "Pedidos";
                dt = cl.NavbarAños();

                NavBarItem item1 = new NavBarItem();
                foreach (DataRow dr in dt.Rows)
                {
                    item1.Caption = dr["Año"].ToString();
                    item1.Name = dr["Año"].ToString();
                    navBarGroupAño.ItemLinks.Add(item1);
                    item1 = new NavBarItem();

                }

                navBarControl.ActiveGroup = navBarControl.Groups[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("AgregaAñosNavBar:" + ex.Message);
            }
        }

        private void SiguienteID()
        {
            int iFolio = 0;
            globalCL clg = new globalCL();
            clg.strDoc = "Cortes";
            clg.strSerie = Convert.ToString(cboSerie.EditValue);
            string Result = clg.DocumentosSiguienteID();

            if (Result != "OK")
            {
                MessageBox.Show("No se pudo leer el siguiente folio");
                return;
            }
            iFolio = clg.iFolio;
            txtFolio.Text = iFolio.ToString();
        }

        private string BuscarSerie()
        {
            SerieCL cl = new SerieCL();
            cl.intUsuarioID = intUsuarioID;
            String Result = cl.BuscarSerieporUsuario();
            if (Result == "OK")
            {
                if (cl.strSerie == "")
                    cboSerie.ReadOnly = false;
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
        }

        private void LlenarGrid(int intYear, int intMes)
        {

            CortesCL cl = new CortesCL();
            cl.intAño = intYear;
            cl.intMes = intMes;
            gridControlPrincipal.DataSource = cl.CortesGrid();

            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCortes";
            clg.restoreLayout(gridViewPrincipal);
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();

            #region ALMACENES CORTADO
            cl.strTabla = "Almacenes";
            cboAlmacenCortado.Properties.ValueMember = "Clave";
            cboAlmacenCortado.Properties.DisplayMember = "Des";
            cboAlmacenCortado.Properties.DataSource = cl.CargaCombos();
            cboAlmacenCortado.Properties.ForceInitialize();
            cboAlmacenCortado.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboAlmacenCortado.Properties.PopulateColumns();
            cboAlmacenCortado.Properties.Columns["Clave"].Visible = false;
            cboAlmacenCortado.Properties.NullText = "Seleccionar almacen cortado";
            #endregion ALMACENES CORTADO

            #region ARTICULO CORTADO
            cl.strTabla = "Articulos";
            repositoryItemLookUpEditArticulo.ValueMember = "Clave";
            repositoryItemLookUpEditArticulo.DisplayMember = "Des";
            repositoryItemLookUpEditArticulo.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticulo.ForceInitialize();
            repositoryItemLookUpEditArticulo.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticulo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditArticulo.NullText = "Seleccione el articulo a cortar";
            #endregion ARTCULO CORTADO

            #region ARTICULO PRODUCIDO
            cl.strTabla = "Articulos";
            repositoryItemLookUpEditArticuloProducido.ValueMember = "Clave";
            repositoryItemLookUpEditArticuloProducido.DisplayMember = "Des";
            repositoryItemLookUpEditArticuloProducido.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditArticuloProducido.ForceInitialize();
            repositoryItemLookUpEditArticuloProducido.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditArticuloProducido.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            repositoryItemLookUpEditArticuloProducido.NullText = "Seleccione el articulo a producir";
            #endregion ARTICULO PRODUCIDO

            #region ALMACEN PRODUCIDO
            cl.strTabla = "Almacenes";
            repositoryItemLookUpEditAlmacenProd.ValueMember = "Clave";
            repositoryItemLookUpEditAlmacenProd.DisplayMember = "Des";
            repositoryItemLookUpEditAlmacenProd.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditAlmacenProd.ForceInitialize();
            repositoryItemLookUpEditAlmacenProd.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditAlmacenProd.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            #endregion ALMACEN PRODUCIDO

            #region SERIE
            cl.strTabla = "Serie";
            cboSerie.Properties.ValueMember = "Clave";
            cboSerie.Properties.DisplayMember = "Clave";
            cboSerie.Properties.DataSource = cl.CargaCombos();
            cboSerie.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboSerie.Properties.ForceInitialize();
            cboSerie.Properties.PopulateColumns();
            cboSerie.Properties.Columns["Des"].Visible = false;
            cboSerie.EditValue = cboSerie.Properties.GetDataSourceValue(cboSerie.Properties.ValueMember, 0);
            #endregion SERIE
        }

        private void LimpiaCajas()
        {
    
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtTurno.Text = string.Empty;

            cboAlmacenCortado.ItemIndex = 0;
            txtElaboro.Text = string.Empty;
            txtObervaciones.EditValue = null;
            Inicialisalista();
        }

        private void Nuevo()
        {
            intFolio = 0;
            blNuevo = true;
            LimpiaCajas();
            BotonesEdicion();
           
            globalCL global = new globalCL();
            global.strDoc = "Cortes";
            global.strSerie = cboSerie.EditValue.ToString();
            // Manual:
            // -- Definir la condicion necesaria para traer el siguiente ID --
            // clg.sCondicion = sCondicion;

            string Result = global.DocumentosSiguienteID();
            if (Result != "OK")
            {
                MessageBox.Show("No se pudo leer el siguiente folio");
                return;
            }
            else
            {
                txtFolio.Text = global.iFolio.ToString();
            }
        }

        private void BotonesEdicion()
        {
            ribbonPageGroup1.Visible = false;
            navBarControl.Visible = false;

            ribbonPageGroup2.Visible = true;
            navigationFrame.SelectedPageIndex = 1;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCortesDetalle";
            clg.restoreLayout(gridViewDetalle);
        }   

        private string Valida()
        {
            if (cboSerie.EditValue == null)
            {
                return "El campo Serie no puede ir vacio";
            }
            if (cboAlmacenCortado.EditValue == null)
            {
                return "El campo Serie no puede ir vacio";
            }
            if (txtTurno.Text.Length == 0)
            {
                return "El campo Turno no puede ir vacio";
            }
            if (txtElaboro.Text.Length == 0)
            {
                return "El campo Elaboro no puede ir vacio";
            }

            return "OK";
        }

        private void Inicialisalista()
        {
            detalle = new BindingList<detalleCL>();
            detalle.AllowNew = true;
            gridControlDetalle.DataSource = detalle;
            gridColumnDesCortada.OptionsColumn.ReadOnly = true;
            gridColumnDesCortada.OptionsColumn.AllowFocus = false;
            gridColumnDesProducido.OptionsColumn.ReadOnly = true;
            gridColumnDesProducido.OptionsColumn.AllowFocus = false;
        }

        private void CargaComboAlmacenProd()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "AlmacenesProducido";
            cl.iCondicion = Convert.ToInt32(cboAlmacenCortado.EditValue);
            repositoryItemLookUpEditAlmacenProd.ValueMember = "Clave";
            repositoryItemLookUpEditAlmacenProd.DisplayMember = "Des";
            repositoryItemLookUpEditAlmacenProd.DataSource = cl.CargaCombos();
            repositoryItemLookUpEditAlmacenProd.ForceInitialize();
            repositoryItemLookUpEditAlmacenProd.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEditAlmacenProd.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
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

                System.Data.DataTable dtCortes = new System.Data.DataTable("Cortes");
                dtCortes.Columns.Add("Serie", Type.GetType("System.String"));
                dtCortes.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtCortes.Columns.Add("Seq", Type.GetType("System.Int32"));
                dtCortes.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtCortes.Columns.Add("Turno", Type.GetType("System.String"));
                dtCortes.Columns.Add("Elaboro", Type.GetType("System.String"));
                dtCortes.Columns.Add("ArticulocortadoID", Type.GetType("System.Int32"));
                dtCortes.Columns.Add("ArtCor", Type.GetType("System.String"));
                dtCortes.Columns.Add("Cantidadcortada", Type.GetType("System.Decimal"));
                dtCortes.Columns.Add("LetraCortada", Type.GetType("System.String"));
                dtCortes.Columns.Add("ArticuloproducidoID", Type.GetType("System.Int32"));
                dtCortes.Columns.Add("ArtPro", Type.GetType("System.String"));
                dtCortes.Columns.Add("Cantidadproducida", Type.GetType("System.Decimal"));
                dtCortes.Columns.Add("letraproducida", Type.GetType("System.String"));
                dtCortes.Columns.Add("Status", Type.GetType("System.String"));
                dtCortes.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtCortes.Columns.Add("Maquina", Type.GetType("System.Int32"));
                dtCortes.Columns.Add("Observaciones", Type.GetType("System.String"));
                dtCortes.Columns.Add("UltimoCostoCortado", Type.GetType("System.Decimal"));
                dtCortes.Columns.Add("CostoProducido", Type.GetType("System.Decimal"));
                dtCortes.Columns.Add("AlmacenCortado", Type.GetType("System.Int32"));
                dtCortes.Columns.Add("AlmacenProducido", Type.GetType("System.Int32"));
                dtCortes.Columns.Add("FechaReal", Type.GetType("System.DateTime"));

                //Siguiente folio
                int iFolio = 0;
                globalCL clg = new globalCL();
                clg.strDoc = "Cortes";
                clg.strSerie = cboSerie.EditValue.ToString();
                //Manual: --- Definir la condicion necesaria para traer el siguiente ID ---------------------         clg.sCondicion = sCondicion;
                Result = clg.DocumentosSiguienteID();
                if (Result != "OK")
                {
                    MessageBox.Show("No se pudo leer el siguiente folio");
                    return;
                }
                iFolio = clg.iFolio;
                int intRen = 0;
                string dato = String.Empty;
                string strSerie = cboSerie.EditValue.ToString();
                int intFolio = iFolio;
                int intSeq = 0, intMaquina=0;
                DateTime fFecha = Convert.ToDateTime(txtFecha.EditValue);
                string strTurno = Convert.ToString(txtTurno.EditValue);
                string strElaboro = Convert.ToString(txtElaboro.EditValue);
                int intArticulocortadoID = 0;
                string strArtCor = "prueba";
                decimal dCantidadcortada = 0;
                string strLetraCortada = String.Empty;
                int intArticuloproducidoID = 0;
                string strArtPro = String.Empty;
                decimal dCantidadproducida = 0;
                string strletraproducida = String.Empty;
                string strStatus = String.Empty;
               //int intUsuarioID = intuys;
               string strMaquina = Environment.MachineName;
                string strObservaciones = txtObervaciones.Text;
                decimal dUltimoCostoCortado = 0;
                decimal dCostoProducido = 0;
                int intAlmacenCortado = Convert.ToInt32(cboAlmacenCortado.EditValue);
                int intAlmacenProducido = 0;
                DateTime fFechaReal;
                for (int i = 0; i < gridViewDetalle.RowCount - 1; i++)
                {
                    dato = gridViewDetalle.GetRowCellValue(i, "CantidadCortada").ToString();
                    if (dato.Length > 0)
                    {
                        intSeq = i+1;
                        //strSerie = strSerie;
                        //intFolio = iFolio;

                        //fFecha = fFecha;
                        //strTurno = strTurno;
                        //strElaboro = strElaboro;

                      
                        intArticulocortadoID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Articulo"));
                        strArtCor = gridViewDetalle.GetRowCellValue(i, "DesCortada").ToString();
                        dCantidadcortada = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "CantidadCortada"));
                        strLetraCortada = gridViewDetalle.GetRowCellValue(i, "LetraCortada").ToString();
                        intArticuloproducidoID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "ArticuloProducido"));
                        strArtPro = gridViewDetalle.GetRowCellValue(i, "DesProducido").ToString();
                        dCantidadproducida = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "CantidadProducida"));
                        strletraproducida = gridViewDetalle.GetRowCellValue(i, "LetraProducida").ToString();
                        strStatus = "1";
                        //intUsuariosID 
                        intMaquina = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Maquina"));
                        //strObservaciones 
                        dUltimoCostoCortado = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "UltimoCostoCortado"));
                        dCostoProducido = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "CostoProducido"));
                        //intAlmacenCortado 
                        intAlmacenProducido = intAlmacenCortado; //.ToInt32(gridViewDetalle.GetRowCellValue(i, "AlmacenProducido"));
                        fFechaReal = Convert.ToDateTime(DateTime.Now);


                        if (intArticulocortadoID == intArticuloproducidoID)
                        {
                            MessageBox.Show("Renglon: " + intSeq.ToString() + " El articulo cortado y producido no puede ser iguales, Seleccione un articulo terminado diferente");
                            return;
                        }

                        dtCortes.Rows.Add(strSerie, intFolio, intSeq, 
                            fFecha, strTurno, strElaboro, intArticulocortadoID, 
                            strArtCor, dCantidadcortada, strLetraCortada, 
                            intArticuloproducidoID, strArtPro, dCantidadproducida, 
                            strletraproducida, strStatus, intUsuarioID, intMaquina,
                            strObservaciones, dUltimoCostoCortado, 
                            dCostoProducido, intAlmacenCortado, intAlmacenProducido, fFechaReal);

                    }
                }
               
                CortesCL cl = new CortesCL();
                cl.strSerie = strSerie;
                cl.intFolio = intFolio;
                cl.dtm = dtCortes;
             
                cl.intUsuarioID = intUsuarioID;
                cl.strMaquina = Environment.MachineName;
                Result = cl.CortesCRUD();
                if (Result == "OK")
                {
                    DialogResult dialog = MessageBox.Show("¿Desea generar nuevo registro?", "Guardado Correctamente: Folio: " + intFolio.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(dialog == DialogResult.Yes)
                    {
                        Nuevo();
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
        }

        private void Editar()
        {
            DetalleLlenaCajas();
            BotonesEdicion();
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            blNuevo = false;
        }

        private void DetalleLlenaCajas()
        {
            try
            {
                CortesCL cl = new CortesCL();
                cl.intFolio = intFolio;
                cl.strSerie = strSerie;
                gridControlDetalle.DataSource = cl.CortesLlenaCajas();

                cboAlmacenCortado.EditValue = Convert.ToInt32(cl.dtd.Rows[0]["AlmacenCortado"]);
                txtElaboro.Text = cl.dtd.Rows[0]["Elaboro"].ToString();
                txtTurno.Text = cl.dtd.Rows[0]["Turno"].ToString();
                txtObervaciones.Text = cl.dtd.Rows[0]["Observaciones"].ToString();
                cboSerie.EditValue = cl.dtd.Rows[0]["Serie"].ToString();
                txtFolio.Text = cl.dtd.Rows[0]["Folio"].ToString();
                txtFecha.Text = cl.dtd.Rows[0]["Fecha"].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("DetalleLlenaCajas: " + ex);
            }


        }

        private void GenerarReporte()
        {
            try
            {
                globalCL global = new globalCL();
                string result = global.Datosdecontrol();
                if (result != "OK")
                {
                    MessageBox.Show(result);
                    return;
                }
                HojaProduccionCortesDesigner rep = new HojaProduccionCortesDesigner();
                rep.Parameters["parameter1"].Value = strSerie;
                rep.Parameters["parameter2"].Value = intFolio;

                if (global.iImpresiondirecta == 1)
                {
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.CreateDocument();
                    documentViewer.DocumentSource = rep;
                    RibbonPagePrint.Visible = true;
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(RibbonPagePrint.Text);
                    navigationFrame.SelectedPage = navigationPageDocument;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al generar reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #region AL CARGAR
        private void Produccion_Load(object sender, EventArgs e)
        {
            InitGridPrincipal();
            InitGridDetalle();
            BuscarSerie();
            txtFecha.Text = DateTime.Now.ToShortDateString();
            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;
            LlenarGrid(AñoFiltro, MesFiltro);
            CargaCombos();
            AgregaAñosNavBar();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        #endregion AL CARGAR


        #region INIT GRID
        private void InitGridPrincipal()
        {
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Producción";
            gridViewPrincipal.ActiveFilter.Clear();
        }
        private void InitGridDetalle()
        {
            gridViewDetalle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewDetalle.OptionsView.ShowFooter = true;
            gridViewDetalle.OptionsNavigation.EnterMoveNextColumn = true;
            gridViewDetalle.OptionsNavigation.AutoMoveRowFocus = true;
        }
        #endregion INIT GRID


        #region NAVEGACION
        private void navBarControl_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            globalCL clg = new globalCL();
            string Name = e.Link.ItemName.ToString();
            if (clg.esNumerico(Name))
            {
                AñoFiltro = Convert.ToInt32(Name);
                LlenarGrid(AñoFiltro, MesFiltro);
            }
            else
            {
                switch (Name)
                {
                    case "navBarItemEne":
                        MesFiltro = 1;
                        break;
                    case "navBarItemFeb":
                        MesFiltro = 2;
                        break;
                    case "navBarItemMar":
                        MesFiltro = 3;
                        break;
                    case "navBarItemAbr":
                        MesFiltro = 4;
                        break;
                    case "navBarItemMay":
                        MesFiltro = 5;
                        break;
                    case "navBarItemJun":
                        MesFiltro = 6;
                        break;
                    case "navBarItemJul":
                        MesFiltro = 7;
                        break;
                    case "navBarItemAgo":
                        MesFiltro = 8;
                        break;
                    case "navBarItemSep":
                        MesFiltro = 9;
                        break;
                    case "navBarItemOct":
                        MesFiltro = 10;
                        break;
                    case "navBarItemNov":
                        MesFiltro = 11;
                        break;
                    case "navBarItemDic":
                        MesFiltro = 12;
                        break;
                    case "navBarItemTodos":
                        MesFiltro = 0;
                        break;
                }
                LlenarGrid(AñoFiltro, MesFiltro);
            }
        }
        #endregion NAVEGACION


        #region BOTONES
        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();
        }
        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageGroup1.Visible = true;
            ribbonPageGroup2.Visible = false;
            navBarControl.Visible = true;
            LlenarGrid(AñoFiltro, MesFiltro);
            LimpiaCajas();
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPage1.Text);
            navigationFrame.SelectedPageIndex = 0;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            strSerie = "";
            intFolio = 0;


            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCortesDetalle";
            String Result = clg.SaveGridLayout(gridViewDetalle);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
        }
        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }
        private void bbiVer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
                MessageBox.Show("Selecciona un renglón");
            else
                Editar();
        }
        private void bbiVista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlPrincipal.ShowRibbonPrintPreview();
        }
        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridCortes";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }
        // Desde grid Principal
        private void bbiImprimir2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            if (intFolio == 0)
                MessageBox.Show("Selecciona un renglón");
            else
                GenerarReporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        // Desde form
        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            if (intFolio == 0)
                MessageBox.Show("Primero guardar y después generar el reporte");
            else
                GenerarReporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        #endregion BOTONES


        #region INTERACCIONES DE USUARIO
        private void cboAlmacenCortado_EditValueChanged(object sender, EventArgs e)
        {
            CargaComboAlmacenProd();
        }
        private void gridControlPrincipal_DoubleClick(object sender, EventArgs e)
        {
            GridControl gridPrincipal = (GridControl)sender;
            if(gridPrincipal != null)
            {
                intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
                strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
                Editar();
            }
        }
        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            strSerie = Convert.ToString(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Serie"));
        }
        private void gridControlDetalle_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                view.DeleteRow(gridViewDetalle.FocusedRowHandle);
                e.Handled = true;
            }
        }
        private void gridViewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string artProducido = "", art = "";
            articulosCL cl = new articulosCL();

            //Sacamos datos del artículo
             if (e.Column.Name == "gridColumnArticulo")
            {
                art = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulo").ToString();
                if (art.Length > 0)
                {
                    //int articuloSeleccionado = 0;
                    //for (int i = 0; i < gridViewDetalle.RowCount; i++ )
                    //{
                    //    object row = gridViewDetalle.GetRow(i);
                    //    if (row != null)
                    //    {
                    //        articuloSeleccionado = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "Articulo"));
                    //        if (i != gridViewDetalle.FocusedRowHandle)
                    //        {
                    //            if (Convert.ToInt32(art) == articuloSeleccionado)
                    //            {   
                    //                gridViewDetalle.SetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulo", 0);
                    //                MessageBox.Show("Este artículo ya fué seleccionado.\nFavor de modificar la cantidad cortada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //                return;
                    //            }
                    //        }
                    //    }
                    //}

                    cl.intArticulosID = Convert.ToInt32(art);
                    string result = cl.articulosLlenaCajas();
                    if (result == "OK")
                    {
                        gridViewDetalle.SetFocusedRowCellValue("DesCortada", cl.strArticulo);
                    }
                }
                
            }
          
            if (e.Column.Name == "gridColumnArticuloProducido")
            {
                artProducido = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "ArticuloProducido").ToString();
                art = gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Articulo").ToString();
                if (artProducido.Length > 0)
                {
                    //var dataRowArticuloProducido = repositoryItemLookUpEditArticuloProducido.GetDataSourceRowByKeyValue(artProducido) as DataRowView;
                    //if (dataRowArticuloProducido != null)
                    //{
                    //    int articuloBaseID = Convert.ToInt32(dataRowArticuloProducido["ArticulobaseparacosteoID"]);
                    //    if (Convert.ToInt32(art) != articuloBaseID)
                    //    {
                    //        gridViewDetalle.SetRowCellValue(gridViewDetalle.FocusedRowHandle, "ArticuloProducido", 0);
                    //        MessageBox.Show("Este articulo no puede ser producido con el articulo que seleccíonó para cortar.\nSeleccione un artículo 'hijo' del artículo cortado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        return;
                    //    }
                    //}

                    cl.intArticulosID = Convert.ToInt32(artProducido);
                    string result = cl.articulosLlenaCajas();
                    if (result == "OK")
                        gridViewDetalle.SetFocusedRowCellValue("DesProducido", cl.strArticulo);
                    
                    if (art == artProducido)
                        MessageBox.Show("El articulo cortado y producido no puede ser iguales, Seleccione un articulo terminado diferente");
                }
            }
        }
        #endregion INTERACCIONES DE USUARIO

    }
}