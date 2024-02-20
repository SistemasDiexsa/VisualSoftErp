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
using VisualSoftErp.Operacion.Compras.Clases;
using VisualSoftErp.Clases;
using DevExpress.XtraGrid;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;

/// <summary>
/// 1. Poner columnas en consumo
/// 2. Guardar consumo usando type
/// </summary>

namespace VisualSoftErp.Operacion.Compras.Informes
{
    public partial class Auxiliardecomprasnacionales : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string origen = string.Empty;
        int componente = 0;
        int articulo = 0;
        bool bolPedidos = false;
       

        public Auxiliardecomprasnacionales()
        {
            InitializeComponent();
            CargaCombos();
            txtAño.Text = DateTime.Now.Year.ToString();
            cboMes.ItemIndex = DateTime.Now.Month-1;
            dFecha.Text = DateTime.Now.ToShortDateString();

            gridViewConsumo.OptionsView.ShowAutoFilterRow = true;
            gridViewAux.OptionsView.ShowAutoFilterRow = true;
            gridViewAux.OptionsBehavior.ReadOnly = true;
            gridViewAux.OptionsBehavior.Editable = false;
            gridViewImplosion.OptionsBehavior.ReadOnly = true;
            gridViewImplosion.OptionsBehavior.Editable = false;
            gridViewImplosion.OptionsView.ShowFooter = true;
            gridViewMinMax.OptionsBehavior.ReadOnly = true;
            gridViewMinMax.OptionsBehavior.Editable = false;
            gridViewPedidos.OptionsBehavior.ReadOnly = true;
            gridViewPedidos.OptionsBehavior.Editable = false;
            gridViewPedidos.OptionsView.ShowFooter = true;

            //Se hacen readonly columnas de gridconsumo
            gridColumn1.OptionsColumn.ReadOnly = true;
            gridColumn2.OptionsColumn.ReadOnly = true;
            gridColumn3.OptionsColumn.ReadOnly = true;
            gridColumn4.OptionsColumn.ReadOnly = true;
            gridColumn5.OptionsColumn.ReadOnly = true;
            gridColumn6.OptionsColumn.ReadOnly = true;
            gridColumn7.OptionsColumn.ReadOnly = true;
            gridColumn8.OptionsColumn.ReadOnly = true;
            gridColumn9.OptionsColumn.ReadOnly = true;

            gridColumn1.OptionsColumn.AllowEdit = false;
            gridColumn2.OptionsColumn.AllowEdit = false;
            gridColumn3.OptionsColumn.AllowEdit = false;
            gridColumn4.OptionsColumn.AllowEdit = false;
            gridColumn5.OptionsColumn.AllowEdit = false;
            gridColumn6.OptionsColumn.AllowEdit = false;
            gridColumn7.OptionsColumn.AllowEdit = false;
            gridColumn8.OptionsColumn.AllowEdit = false;
            gridColumn9.OptionsColumn.AllowEdit = false;

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
        
        private void CargaCombos()
        {
            try
            {
                combosCL cl = new combosCL();
                globalCL clg = new globalCL();

                BindingSource src = new BindingSource();
                cboFam.Properties.ValueMember = "Clave";
                cboFam.Properties.DisplayMember = "Des";
                cl.strTabla = "FamiliasLineasAuxNac";
                src.DataSource = cl.CargaCombos();
                cboFam.Properties.DataSource = clg.AgregarOpcionTodos(src);
                cboFam.Properties.ForceInitialize();
                cboFam.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboFam.Properties.ForceInitialize();
                cboFam.Properties.PopulateColumns();
                cboFam.Properties.Columns["Clave"].Visible = false;
                cboFam.ItemIndex = 0;

                cboImplosionArt.Properties.ValueMember = "Clave";
                cboImplosionArt.Properties.DisplayMember = "Des";
                cl.strTabla = "Articulos";
                src.DataSource = cl.CargaCombos();
                cboImplosionArt.Properties.DataSource = cl.CargaCombos();
                cboImplosionArt.Properties.ForceInitialize();
                cboImplosionArt.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboImplosionArt.Properties.ForceInitialize();
                cboImplosionArt.Properties.PopulateColumns();
                cboImplosionArt.Properties.Columns["Clave"].Visible = false;
                cboImplosionArt.ItemIndex = 0;

                List<ClaseGenricaCL> mesesCL = new List<ClaseGenricaCL>();
                mesesCL.Add(new ClaseGenricaCL() { Clave = "1", Des = "Enero" });
                mesesCL.Add(new ClaseGenricaCL() { Clave = "2", Des = "Febrero" });
                mesesCL.Add(new ClaseGenricaCL() { Clave = "3", Des = "Marzo" });
                mesesCL.Add(new ClaseGenricaCL() { Clave = "4", Des = "Abril" });
                mesesCL.Add(new ClaseGenricaCL() { Clave = "5", Des = "Mayo" });
                mesesCL.Add(new ClaseGenricaCL() { Clave = "6", Des = "Junio" });
                mesesCL.Add(new ClaseGenricaCL() { Clave = "7", Des = "Julio" });
                mesesCL.Add(new ClaseGenricaCL() { Clave = "8", Des = "Agosto" });
                mesesCL.Add(new ClaseGenricaCL() { Clave = "9", Des = "Septiembre" });
                mesesCL.Add(new ClaseGenricaCL() { Clave = "10", Des = "Octubre" });
                mesesCL.Add(new ClaseGenricaCL() { Clave = "11", Des = "Noviembre" });
                mesesCL.Add(new ClaseGenricaCL() { Clave = "12", Des = "Diciembre" });

                cboMes.Properties.ValueMember = "Clave";
                cboMes.Properties.DisplayMember = "Des";
                cboMes.Properties.DataSource = mesesCL;
                cboMes.Properties.ForceInitialize();
                cboMes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboMes.Properties.ForceInitialize();
                cboMes.Properties.PopulateColumns();
                cboMes.Properties.Columns["Clave"].Visible = false;
                cboMes.ItemIndex = 0;

                List<ClaseGenricaCL> sf = new List<ClaseGenricaCL>();
                sf.Add(new ClaseGenricaCL() { Clave = "0", Des = "Todas" });
                cboSubFam.Properties.ValueMember = "Clave";
                cboSubFam.Properties.DisplayMember = "Des";
                cboSubFam.Properties.DataSource = sf;
                cboSubFam.Properties.ForceInitialize();
                cboSubFam.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboSubFam.Properties.ForceInitialize();
                cboSubFam.Properties.PopulateColumns();
                cboSubFam.Properties.Columns["Clave"].Visible = false;
                cboSubFam.ItemIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Cargacombos: " + ex.Message);
            }
        }

        private void CargaSubfamilias()
        {
            try
            {
                combosCL cl = new combosCL();
                globalCL clg = new globalCL();

                BindingSource src = new BindingSource();
                cboSubFam.Properties.ValueMember = "Clave";
                cboSubFam.Properties.DisplayMember = "Des";
                cl.strTabla = "SubfamiliasXfamilias";
                cl.iCondicion = Convert.ToInt32(cboFam.EditValue);
                src.DataSource = cl.CargaCombos();
                cboSubFam.Properties.DataSource = clg.AgregarOpcionTodos(src);
                cboSubFam.Properties.ForceInitialize();
                cboSubFam.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboSubFam.Properties.ForceInitialize();
                cboSubFam.Properties.PopulateColumns();
                cboSubFam.Properties.Columns["Clave"].Visible = false;
                cboSubFam.ItemIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cargasubfamilias: " + ex.Message);
            }
        }

        private void ConsumoMensual()
        {
            try
            {

                /// <summary>
                /// El consumo de saca con las ventas (facturasdetalle) de los ultimos 12 meses en base a la fecha enviada
                /// El pronostico se saca de PresupuestoporArticulo, y es el que se puede modificar aquí para guardarse
                /// en base a un año,mes y artículo
                /// Si el resultado de SP la columna Pronostico=-1 quiere decir que no lo capturaron o modificaron, entonces se 
                /// toma como pronóstico el promedio
                /// Es decir, se maneja el pronostico solo por excepción
                /// </summary>

                ComprasCL cl = new ComprasCL();
                cl.fFecha = Convert.ToDateTime(dFecha.Text);
                cl.intLinea = 0;
                cl.intFam = Convert.ToInt32(cboFam.EditValue);
                cl.intSubFam = Convert.ToInt32(cboSubFam.EditValue);
                gridControlConsumo.DataSource = cl.AuxiliarNacionalConsumo();

                decimal Pronostico = 0;
                decimal Promedio = 0;
                for (int i = 0; i <= gridViewConsumo.RowCount - 1; i++)
                {
                    Promedio = Convert.ToDecimal(gridViewConsumo.GetRowCellValue(i, "Promedio"));
                    Pronostico = Convert.ToDecimal(gridViewConsumo.GetRowCellValue(i, "Pronostico"));
                    gridViewConsumo.FocusedRowHandle = i;
                    if (Pronostico != -1)                      
                        gridViewConsumo.SetFocusedRowCellValue("Pronostico", Pronostico);
                    else
                        gridViewConsumo.SetFocusedRowCellValue("Pronostico", Promedio);
                }

                globalCL clg = new globalCL();
                clg.strGridLayout = "gridACNConsumo";
                clg.restoreLayout(gridViewConsumo);


                bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                Botones();

                navigationFrame.SelectedPageIndex = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Consumo mensual: " + ex.Message);
            }
        }

        

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.Close();
        }

        private void bbiConsumo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            origen = "Consumo";
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Generando consumo...");
            ConsumoMensual();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void cboFam_EditValueChanged(object sender, EventArgs e)
        {
            CargaSubfamilias();
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            bbiAuxiliar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiConsumo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiOC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiPedidos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;


            string strGrid = string.Empty;
            globalCL clg = new globalCL();
            String Result = string.Empty;
            switch (origen)
            {
                case "Consumo":
                    clg.strGridLayout = "gridACNConsumo";
                    Result = clg.SaveGridLayout(gridViewConsumo);
                    navigationFrame.SelectedPageIndex = 0;
                    bbiPrevio.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    break;
                case "Auxiliar":
                    clg.strGridLayout = "gridACNAuxiliar";
                    Result = clg.SaveGridLayout(gridViewAux);
                    navigationFrame.SelectedPageIndex = 0;
                    bbiPrevio.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    break;
                case "Implosion":
                    clg.strGridLayout = "gridACNImplosion";
                    Result = clg.SaveGridLayout(gridViewImplosion);
                    bbiImplosion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    origen = "Auxilar";
                    navigationFrame.SelectedPageIndex = 2;
                    break;
                case "OC":
                    clg.strGridLayout = "gridACNOC";
                    Result = clg.SaveGridLayout(gridViewImplosion);
                    bbiImplosion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    origen = "Auxilar";
                    navigationFrame.SelectedPageIndex = 2;
                    break;
                case "MaxMin":
                    clg.strGridLayout = "gridACNAuxiliarMinMax";
                    Result = clg.SaveGridLayout(gridViewImplosion);
                    bbiImplosion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    origen = "MaxMin";
                    navigationFrame.SelectedPageIndex = 5;
                    break;


            }

            if (bolPedidos)
            {
                bolPedidos = false;
                clg.strGridLayout = "gridACNAuxiliarPedidos";
                Result = clg.SaveGridLayout(gridViewPedidos);
                clg.strGridLayout = "gridACNAuxiliarPedidosDetalle";
                Result = clg.SaveGridLayout(gridViewPedidosDetalle);
            }

            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }

                       
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Guardando consumo...");
            guardaConsumo();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            
        }

        private void guardaConsumo()
        {
            try
            {

                gridViewConsumo.ActiveFilter.Clear();

                int pAño = Convert.ToInt32(txtAño.Text);
                int pMes = Convert.ToInt32(cboMes.EditValue);
                int Art = 0;
                decimal Pronostico = 0;


                System.Data.DataTable dtPresupuestoporarticulo = new System.Data.DataTable("Presupuestoporarticulo");
                dtPresupuestoporarticulo.Columns.Add("Año", Type.GetType("System.Int32"));
                dtPresupuestoporarticulo.Columns.Add("Mes", Type.GetType("System.Int32"));
                dtPresupuestoporarticulo.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtPresupuestoporarticulo.Columns.Add("VERSION", Type.GetType("System.Int32"));
                dtPresupuestoporarticulo.Columns.Add("PRONOSTICO", Type.GetType("System.Int32"));

                for (int i = 0; i <= gridViewConsumo.RowCount - 1; i++)
                {
                    Art = Convert.ToInt32(gridViewConsumo.GetRowCellValue(i, "ArticulosID"));
                    Pronostico = Convert.ToDecimal(gridViewConsumo.GetRowCellValue(i, "Pronostico"));

                    dtPresupuestoporarticulo.Rows.Add(pAño, pMes, Art, 1, Pronostico);
                }


                ComprasCL cl = new ComprasCL();

                cl.intAño = pAño;
                cl.intMes = pMes;
                cl.dtConsumo = dtPresupuestoporarticulo;
                string result = cl.AuxiliarNacionalConsumoGuardar();

                MessageBox.Show(result);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("GuardaConsumo:" + ex.Message);
            }
        }

        private void bbiAuxiliar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult Result = MessageBox.Show("Desea generar el auxiliar?", "Informe", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Generando auxiliar...");
            origen = "Auxiliar";
            Auxiliar();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void AuxMinMax()
        {
            try
            {
                globalCL clg = new globalCL();
                if (!clg.esNumerico(txtDias.Text))
                    txtDias.Text = "90";
                ComprasCL cl = new ComprasCL();
                cl.intFam = Convert.ToInt32(cboFam.EditValue);
                cl.intDiasTrasladoProv = Convert.ToInt32(txtDias.Text);
                cl.intLinea = 0; //por lo pronto no se usa
                DataTable dt = new DataTable();
                dt = cl.AuxiliarNacionalMinMax();

                int totalExistencia = 0;
                int existencia;
                int oc;
                int pedidos = 0;
                int minimo = 0;
                int maximo = 0;
                int dias = 0;
                int sugerido = 0;
                int diario = 0;
                int diasTotales = 0;
                

                foreach (DataRow dr in dt.Rows)
                {
                    

                    existencia = Convert.ToInt32(dr["Existencia"]);
                    oc = Convert.ToInt32(dr["oc"]);
                    pedidos = Convert.ToInt32(dr["Pedidos"]);

                    totalExistencia = existencia + oc - pedidos;

                    diario=Convert.ToInt32(dr["Diario"]);
                    maximo = Convert.ToInt32(dr["FactorMaximo"]);

                    diasTotales = Convert.ToInt32(dr["DiasProveedor"]) + Convert.ToInt32(dr["DiasArticulo"]);

                    minimo = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(diario * diasTotales)));

                    if (maximo == 0)
                        maximo = 1;

                    maximo = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(minimo * maximo)));

                    if (minimo > 0)
                        dias = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((totalExistencia - minimo) / diario)));
                    else
                        dias = 0;

                    if (totalExistencia <= minimo)
                        sugerido = maximo - totalExistencia;
                    else
                        sugerido = 0;

                    dr["Totalexistencia"] = totalExistencia;
                    dr["Minimo"] = minimo;
                    dr["Maximo"] = maximo;
                    dr["Dias"] = dias;
                    dr["Sugerido"] = sugerido;
                }
                gridControlMinMax.DataSource = dt;

                clg.strGridLayout = "gridACNAuxiliarMinMax";
                clg.restoreLayout(gridViewAux);
                gridViewMinMax.OptionsView.ShowAutoFilterRow = true;

                navigationFrame.SelectedPageIndex = 5;
                Botones();
                bbiImplosion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiOC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Pedidos()
        {
            try
            {
                gridControlPedidosDetalle.DataSource = null;

                ComprasCL cl = new ComprasCL();
                cl.intArt = componente;
                gridControlPedidos.DataSource = cl.AuxiliarNacionalImplosionPedidos();

                globalCL clg = new globalCL();
                clg.strGridLayout = "gridACNAuxiliarPedidos";
                clg.restoreLayout(gridViewPedidos);

                //clg.strGridLayout = "gridACNAuxiliarPedidosDetalle";
                //clg.restoreLayout(gridViewPedidosDetalle);

                foreach (GridColumn column in gridViewPedidos.Columns)
                {
                    GridSummaryItem item = column.SummaryItem;
                    if (item != null)
                        column.Summary.Remove(item);
                }

                GridColumnSummaryItem Total = new GridColumnSummaryItem();
                Total.SummaryType = SummaryItemType.Sum;
                Total.FieldName = "AProducirMP";
                //gridViewPedidos.Columns["AProducirMP"].Summary.Remove(Total);
                gridViewPedidos.Columns["AProducirMP"].Summary.Add(Total);
                


                decimal Pedidossinfacturar;
                decimal Presentacion;
                double MPNecesaria;
                double AProducirMP;

                for (int i = 0; i <= gridViewPedidos.RowCount; i++)
                {
                    Pedidossinfacturar = Convert.ToDecimal(gridViewPedidos.GetRowCellValue(i, "Pedidossinfacturar"));
                    Presentacion = Convert.ToDecimal(gridViewPedidos.GetRowCellValue(i, "Presentacion"));
                    MPNecesaria = Convert.ToDouble(gridViewPedidos.GetRowCellValue(i, "MPNecesaria"));

                    AProducirMP = Convert.ToDouble(Convert.ToDouble((Pedidossinfacturar * Presentacion)) * MPNecesaria);

                    gridViewPedidos.SetRowCellValue(i,"AProducirMP",Math.Ceiling(AProducirMP));
                }

                gridViewPedidos.UpdateTotalSummary();

                Botones();
                navigationFrame.SelectedPageIndex = 6;
                bbiPedidos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Auxiliar()
        {
            try
            {
                globalCL clg = new globalCL();
                ComprasCL cl = new ComprasCL();
                cl.intAño = Convert.ToInt32(txtAño.Text);
                cl.intMes = Convert.ToInt32(cboMes.EditValue);
                cl.intFam = Convert.ToInt32(cboFam.EditValue);
                cl.intSubFam = Convert.ToInt32(cboSubFam.EditValue);
                cl.fFecha = Convert.ToDateTime(dFecha.Text);

                if (!clg.esNumerico(txtPedido.Text))
                    txtPedido.Text = "0";

                cl.intPedido = Convert.ToInt32(txtPedido.Text);

                //Se revisan productos intermedios (tipo E) -----------------------------------------------------------------------------
                ComponentesCL clc = new ComponentesCL();
                int tipoArt = 0;
                int ArtId = 0;
                int Componente = 0;
                DataTable dtComp = new DataTable();
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();

                DataRow[] drCExiste;
                dt= cl.AuxiliarNacionalAuxiliar();
                dt2 = cl.AuxiliarNacionalAuxiliar();

                bool existeArt = false;
                string dato = string.Empty;

                decimal OC = 0;
                decimal ExMP = 0;
                decimal Ped = 0;
                decimal TEx = 0;
                decimal Reorden = 0;
                decimal AProducir = 0;
                decimal Dif = 0;
                int DiasTotales = 0;
                decimal Sugerido = 0;
                int Adicional = 0;
                decimal Pronostico = 0;


                foreach (DataRow dr in dt2.Rows)
                {
                    tipoArt = Convert.ToInt32(dr["tipoArticulo"]);
                    if (tipoArt == 4)
                    {
                        ArtId = Convert.ToInt32(dr["ID"]);

                        //Se buscan los componentes de este componente tipo 4 (en proceso)
                        OC = Convert.ToDecimal(dr["OC"]);
                        ExMP = Convert.ToDecimal(dr["ExistenciaMT"]);
                        TEx = OC + ExMP;

                        AProducir = Convert.ToDecimal(dr["Pronostico"]);

                        DiasTotales = Convert.ToInt32(dr["DiasTotales"]) + Convert.ToInt32(dr["DiasStock"]);

                        Adicional = Convert.ToInt32(dr["Mesesdeconsumo"]);

                        if (DiasTotales > 0)
                            Reorden = (AProducir / 30) * DiasTotales;
                        else
                            Reorden = 1;

                        if (AProducir > 0)
                            Dif = Math.Ceiling((TEx - Reorden) / (AProducir) * 30);

                        if (Adicional == 0)
                            Adicional = 1;

                        Sugerido = Math.Round((Reorden + (Adicional * AProducir)) - TEx, 0);

                        if (Sugerido < 0)
                            Sugerido = 0;

                        clc.intArticulosID = ArtId;
                        clc.dFecha = Convert.ToDateTime(dFecha.Text);
                        clc.intSugerido = Sugerido;
                        dtComp = clc.ComponentesGridParaAuxNacional();

                        //Se recorre para sumarizar a las MP que vienen del SP original (AuxNac)
                        foreach (DataRow drC in dtComp.Rows)
                        {
                            Componente = Convert.ToInt32(drC["Componente"]);


                            drCExiste = dt.Select("ID=" + Componente.ToString());
                            // Display column 1 of the found row.

                            existeArt = false;
                            try
                            {
                                dato = drCExiste[0][0].ToString();
                                existeArt = true;
                            }
                            catch (Exception)
                            {

                            }
                            //Calculo de la MP 
                            OC = Convert.ToDecimal(drC["OC"]);
                            ExMP = Convert.ToDecimal(drC["ExistenciaMP"]);
                            TEx = OC + ExMP;

                            AProducir = Convert.ToDecimal(drC["Arebajar"]);

                            DiasTotales = Convert.ToInt32(drC["DiasTotales"]) + Convert.ToInt32(drC["DiasStock"]);

                            Adicional = Convert.ToInt32(drC["Mesesdeconsumo"]);

                            if (DiasTotales > 0)
                                Reorden = (AProducir / 30) * DiasTotales;
                            else
                                Reorden = 1;

                            if (AProducir > 0)
                                Dif = Math.Ceiling((TEx - Reorden) / (AProducir) * 30);

                            if (Adicional == 0)
                                Adicional = 1;

                            if (TEx > Reorden)
                                Sugerido = 0;
                            else
                                Sugerido = Math.Round((Reorden + (Adicional * AProducir)) - TEx, 0);

                            if (Sugerido < 0)
                                Sugerido = 0;


                            if (existeArt)
                            {
                                Pronostico = Convert.ToDecimal(drCExiste[0]["Pronostico"]);

                                Pronostico += Convert.ToDecimal(drC["Arebajar"]);
                                drCExiste[0]["Pronostico"] = Pronostico;
                            }
                            else
                            {                               
                                dt.Rows.Add(Componente, drC["Articulo"].ToString(), drC["Nombre"].ToString(), 
                                    drC["Arebajar"].ToString(), 
                                    drC["OC"].ToString(),
                                    0, 
                                    drC["ExistenciaMP"].ToString(), 
                                    TEx,
                                    Dif,
                                    Reorden,
                                    Sugerido,
                                    DiasTotales,
                                    Convert.ToInt32(drC["DiasStock"]),
                                    drC["Proveedor"], 1, 3);
                                //
                                
                            }                           
                        }                        
                    }
                }

                //Eof: revisar tipo E

                gridControlAux.DataSource = dt;
                //cl.AuxiliarNacionalAuxiliar();
               
                clg.strGridLayout = "gridACNAuxiliar";
                clg.restoreLayout(gridViewConsumo);

                if (Convert.ToInt32(txtPedido.Text)>0)
                {
                    gridViewAux.OptionsView.ShowViewCaption = true;
                    gridViewAux.ViewCaption = "PEDIDO :" + txtPedido.Text;
                }

                Botones();
                bbiImplosion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiOC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                //Se recorre el grid para los cálculos               
                for (int i = 0; i <= gridViewAux.RowCount - 1; i++)
                {
                    //Art = Convert.ToInt32(gridViewConsumo.GetRowCellValue(i, "ArticulosID"));
                    OC = Convert.ToDecimal(gridViewAux.GetRowCellValue(i, "OC"));
                    ExMP = Convert.ToDecimal(gridViewAux.GetRowCellValue(i, "ExistenciaMT"));
                    //Ped = Convert.ToDecimal(gridViewAux.GetRowCellValue(i, "Pedidos"));
                    TEx = OC + ExMP;

                    AProducir = Convert.ToDecimal(gridViewAux.GetRowCellValue(i, "Pronostico"));

                    DiasTotales = Convert.ToInt32(gridViewAux.GetRowCellValue(i, "DiasTotales")) + Convert.ToInt32(gridViewAux.GetRowCellValue(i, "DiasStock")); ;

                    Adicional = Convert.ToInt32(gridViewAux.GetRowCellValue(i, "Mesesdeconsumo"));

                    if (DiasTotales > 0)
                        Reorden = (AProducir / 30) * DiasTotales;
                    else
                        Reorden = 1;

                    //if (Consumo > 0)
                    //{
                    //    Diferencia = (totalExistencia - Reorden);
                    //    Diferencia = Diferencia / Consumo;
                    //    Diferencia = Diferencia * 30;
                    //}


                    if (AProducir>0)
                    {
                        Dif = TEx - Reorden;
                        Dif = Dif / AProducir;
                        Dif=Dif * 30;
                    }
                        

                    if (Adicional == 0)
                        Adicional = 1;

                    Sugerido = Math.Round((Reorden + (Adicional * AProducir)) - TEx, 0);
                    
                    if (Sugerido < 0)
                        Sugerido = 0;


                    gridViewAux.FocusedRowHandle = i;
                    gridViewAux.SetFocusedRowCellValue("TotalExistencia", TEx);
                    gridViewAux.SetFocusedRowCellValue("Diferencia", Dif);
                    gridViewAux.SetFocusedRowCellValue("Reorden", Reorden);
                    gridViewAux.SetFocusedRowCellValue("Sugerido", Sugerido);





                }

                navigationFrame.SelectedPageIndex = 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Auxiliar:" + ex.Message);
            }
        }

        private void Botones()
        {
            bbiConsumo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiAuxiliar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //bbiImplosion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiPrevio.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiPedidos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

        }

        private void PedidosDetalle()
        {
            try
            {
                ComprasCL cl = new ComprasCL();
                cl.intArt = articulo;
                gridControlPedidosDetalle.DataSource = cl.AuxiliarNacionalPedidosDetalleUnArticulo();
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

        private void Implosion()
        {
            try
            {
                componente = Convert.ToInt32(cboImplosionArt.EditValue);

                ComprasCL cl = new ComprasCL();
                cl.intAño = Convert.ToInt32(txtAño.Text);
                cl.intMes = Convert.ToInt32(cboMes.EditValue);
                cl.intArt = componente;
                cl.fFecha = Convert.ToDateTime(dFecha.Text);

                gridControlImplosion.DataSource = cl.AuxiliarNacionalImplosion();

                globalCL clg = new globalCL();
                clg.strGridLayout = "gridACNImplosion";
                clg.restoreLayout(gridViewImplosion);

                Botones();

                decimal PT = 0;
                decimal MP = 0;
                decimal Pedidos = 0;
                decimal Pronostico = 0;
                decimal Existencia = 0;
                decimal Reorden = 0;
                decimal Presentacion = 0;
                decimal Cantidad = 0;

                for (int i=0; i <= gridViewImplosion.RowCount - 1; i++){
                    //PT = Convert.ToInt32(gridViewImplosion.GetRowCellValue(i, "Pronostico")) + Convert.ToInt32(gridViewImplosion.GetRowCellValue(i, "Reorden")) - (Convert.ToInt32(gridViewImplosion.GetRowCellValue(i, "Existencia")- (Convert.ToInt32(gridViewImplosion.GetRowCellValue(i, "Pedidos"))));
                    Presentacion = Convert.ToDecimal(gridViewImplosion.GetRowCellValue(i, "Presentacion"));
                    
                    Cantidad = Convert.ToDecimal(gridViewImplosion.GetRowCellValue(i, "Cantidad"));
                    Pedidos = Convert.ToDecimal(gridViewImplosion.GetRowCellValue(i, "Pedidos"));
                    Existencia = Convert.ToDecimal(gridViewImplosion.GetRowCellValue(i, "Existencia"));
                    Pronostico = Convert.ToDecimal(gridViewImplosion.GetRowCellValue(i, "Pronostico"));
                    Reorden = Convert.ToDecimal(gridViewImplosion.GetRowCellValue(i, "Reorden"));

                    PT = Pronostico - (Existencia - Pedidos) + Reorden;
                    MP = Convert.ToDecimal(PT * Cantidad * Presentacion);

                    gridViewImplosion.FocusedRowHandle = i;

                    if (PT < 0)
                        PT = 0;

                    if (MP < 0)
                        MP = 0;

                    gridViewImplosion.SetFocusedRowCellValue("AProducirPT", PT);
                    gridViewImplosion.SetFocusedRowCellValue("AProducirMP", MP);

                    Botones();
                    bbiImplosion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                }

                //navigationFrame.SelectedPageIndex = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Implosion:" + ex.Message);
            }
        }

        private void bbiImplosion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cboImplosionArt.EditValue = componente;
            navigationFrame.SelectedPageIndex = 3;
                       
        }

        private void gridViewAux_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            componente = Convert.ToInt32(gridViewAux.GetRowCellValue(gridViewAux.FocusedRowHandle, "ID"));
        }

        private void dFecha_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bbiPrevio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (origen)
            {
                case "Consumo":
                    gridViewConsumo.OptionsView.ShowViewCaption = true;
                    gridViewConsumo.ViewCaption = "AUXILIAR DE COMPRAS NACIONALES";
                    gridControlConsumo.ShowRibbonPrintPreview();
                    break;
                case "Auxiliar":
                    gridViewAux.OptionsView.ShowViewCaption = true;
                    gridViewAux.ViewCaption = "AUXILIAR DE COMPRAS NACIONALES";
                    gridControlAux.ShowRibbonPrintPreview();
                    break;
                case "Implosion":
                    gridViewImplosion.OptionsView.ShowViewCaption = true;
                    gridViewImplosion.ViewCaption = "AUXILIAR DE COMPRAS NACIONALES";
                    gridControlImplosion.ShowRibbonPrintPreview();
                    break;
                case "MaxMin":
                    gridViewMinMax.OptionsView.ShowViewCaption = true;
                    gridViewMinMax.ViewCaption = "AUXILIAR DE COMPRAS NACIONALES (MAXMIN)";
                    gridViewMinMax.ShowRibbonPrintPreview();
                    break;
            }
        }

        private void bbiOC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Generando implosión...");
            //origen = "OC";
            CargaOC();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void CargaOC()
        {
            ComprasCL cl = new ComprasCL();
            cl.intArt = componente;
            gridControlOC.DataSource = cl.CargaOCaunComponente();
            Botones();
            bbiOC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            navigationFrame.SelectedPageIndex = 4;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Generando implosión...");
            //origen = "Implosion";
            Implosion();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiAuxNacMinMax_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult Result = MessageBox.Show("Desea generar el auxiliar basado en mínimo ?", "Informe", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Generando auxiliar...");
            origen = "MaxMin";
            AuxMinMax();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void gridViewMinMax_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            componente = Convert.ToInt32(gridViewMinMax.GetRowCellValue(gridViewMinMax.FocusedRowHandle, "ID"));
        }

        private void bbiPedidos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft", "Verificando pedidos...");
            bolPedidos = true;
            Pedidos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void gridViewPedidos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            articulo = Convert.ToInt32(gridViewPedidos.GetRowCellValue(gridViewPedidos.FocusedRowHandle, "ArticulosID"));
            PedidosDetalle();
        }
    }
}