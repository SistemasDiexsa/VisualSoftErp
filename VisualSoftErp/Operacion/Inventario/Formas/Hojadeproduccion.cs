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
using DevExpress.XtraGrid.Views.Grid;
using VisualSoftErp.Operacion.Inventarios.Designers;
using DevExpress.XtraReports.UI;
using DevExpress.XtraNavBar;

namespace VisualSoftErp.Operacion.Inventarios.Formas
{
    public partial class Hojadeproduccion : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string origenImp = string.Empty;
        int intUsuarioID = globalCL.gv_UsuarioID
            , intFolio;
        int impDirecto = 0;
        int AñoFiltro;
        int MesFiltro;
        DateTime dfecha;

        public Hojadeproduccion()
        {
            InitializeComponent();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Hoja de producción";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            LimpiaCajas();
            txtFecha.EditValue = Convert.ToDateTime(DateTime.Now);
            AñoFiltro = DateTime.Now.Year;
            MesFiltro = DateTime.Now.Month;
            llenarGrid(AñoFiltro, MesFiltro);
            gridViewPrincipal.ActiveFilter.Clear();
            CargarCombos();
            AgregaAñosNavBar();
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

        private void llenarGrid(int intYear, int intMes)
        {
            BFCL cl = new BFCL();
            cl.intAño = intYear;
            cl.intMes = intMes;
            gridControlPrincipal.DataSource = cl.BFGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridBF";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void CargarCombos()
        {
            combosCL cl = new combosCL();

            cl.strTabla = "ProductoTerminadoPCA";
            cboProductoterminado.Properties.ValueMember = "Clave";
            cboProductoterminado.Properties.DisplayMember = "Des";
            cboProductoterminado.Properties.DataSource = cl.CargaCombos();
            cboProductoterminado.Properties.ForceInitialize();
            cboProductoterminado.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProductoterminado.Properties.PopulateColumns();
            cboProductoterminado.Properties.Columns["Clave"].Visible = false;
            //cboArticulos.ItemIndex = 0;
            cboProductoterminado.Properties.NullText = "Seleccione un producto terminado";
        }

        private void LimpiaCajas()
        {
            txtLitros.Text = "0";
            txtCantidad.Text = "0";
            cboProductoterminado.EditValue = null;
            txtObservaciones.Text = string.Empty;
            txtFolio.Text = string.Empty;
            txtFecha.EditValue = Convert.ToDateTime(DateTime.Now);
        }

        private string Valida()
        {
            if (txtLitros.Text == string.Empty) { txtLitros.Text = "0"; }

            if (txtCantidad.Text == string.Empty)
            {
                return "El campo Cantidad no puede ir vacio";
            }
            if (txtCantidad.Text == "0")
            {
                return "El campo Cantidad debe ser mayor a 0";
            }
            if (cboProductoterminado.EditValue == null)
            {
                return "El campo Producto terminado no puede ir vacio";
            }
            if (txtFolio.Text == string.Empty)
            {
                return "El campo Folio no puede ir vacio";
            }
            if (txtFecha.Text == string.Empty)
            {
                return "El campo Fecha no puede ir vacio";
            }
            else
            {
                BFCL cl = new BFCL();
                cl.intProductoTerminado = Convert.ToInt32(cboProductoterminado.EditValue);
                String Result = cl.ValidarProductoTerminado();
                if (Result != "OK")
                {
                    return("El producto terminado: " + cboProductoterminado.Text + " no tiene componentes registrados");
                }
            }

            globalCL clg = new globalCL();

            string result = clg.GM_CierredemodulosStatus(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month, "INV");
            if (result == "C")
            {
                return "Este mes está cerrado, no se puede actualizar";
            }

            return "OK";
          
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
                BFCL cl = new BFCL();
                intFolio = Convert.ToInt32(txtFolio.Text);
                cl.intFolio = intFolio;
                cl.intSeq = 1;
                cl.fFecha = Convert.ToDateTime(txtFecha.Text);
                cl.strPT = Convert.ToString(cboProductoterminado.EditValue);
                cl.decCantidad = Convert.ToDecimal(txtCantidad.Text);
                //cl.fFechaSistema = "";
                cl.dLitros = Convert.ToDecimal(txtLitros.Text);
                cl.strStatus = "";
                cl.fFechaCan = Convert.ToDateTime(null);
                cl.strRazonCan = "";
                cl.intUsuariosID = intUsuarioID;
                cl.intArticulosID = Convert.ToInt32(cboProductoterminado.EditValue);
                cl.strObservaciones = txtObservaciones.Text;
                cl.intEfectoCuprum = chkEfectoCuprum.Checked ? 1 : 0;
                Result = cl.BFCrud();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    origenImp = "Captura";
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("VisualSoft","Generando impresión");
                    Reporte();
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                    LimpiaCajas();
                    SiguienteFolio();
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

        private void Cancelarenlabasededatos()
        {
            //aqui cancelar
            try
            {

                globalCL clg = new globalCL();

                string result = clg.GM_CierredemodulosStatus(dfecha.Year, dfecha.Month, "INV");
                if (result == "C")
                {
                    MessageBox.Show("Este mes está cerrado, no se puede actualizar");
                    return;
                }

                BFCL cl = new BFCL();
                cl.intFolio = intFolio;
                cl.intUsuarioID = globalCL.gv_UsuarioID;
                cl.strMaquina = Environment.MachineName;
                cl.strRazon = txtRazondecancelacion.Text;
                result = cl.BFCancelar();
                if (result == "OK")
                {
                    MessageBox.Show("Se canceló correctamente");
                    llenarGrid(AñoFiltro,MesFiltro);
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CambiarStatus: " + ex.Message);
            }
        }

        private void Reporte()
        {
          
            try
            {
                globalCL cl = new globalCL();
                string result = cl.Datosdecontrol();
                if (result == "OK")
                {
                    impDirecto = cl.iImpresiondirecta;
                }
                else
                {
                    impDirecto = 0;
                }

                ProduccionHojaFormato rep = new ProduccionHojaFormato();
                if (impDirecto == 1)
                {
                    rep.Parameters["parameter1"].Value = intFolio;        //Emp
                    rep.Parameters["parameter1"].Visible = false;
                    
                    ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                    rpt.Print();
                    return;
                }
                else
                {
                    rep.Parameters["parameter1"].Value = intFolio;        //Emp
                    rep.Parameters["parameter1"].Visible = false;

                    documentViewer1.DocumentSource = rep;
                    rep.CreateDocument();
                    navBarControl.Visible = false;
                    ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPagePrint.Text);
                    navigationFrame.SelectedPageIndex = 2;
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void SiguienteFolio()
        {
            SerieCL cl = new SerieCL();
            String Result = cl.BuscarFoliodeProduccion();
            if (Result == "OK")
            {
                txtFolio.Text = cl.intFolio.ToString(); 

            }
            else
            {
                MessageBox.Show(Result);
            }
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridBF";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiLimpiar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LimpiaCajas();
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageGroupHome.Visible = false;
            ribbonPageGroupAcciones.Visible = true;
            navBarControl.Visible = false;
            SiguienteFolio();
            navigationFrame.SelectedPageIndex = 1;
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonPageGroupHome.Visible = true;
            LimpiaCajas();
            llenarGrid(AñoFiltro, MesFiltro);
            ribbonPageGroupAcciones.Visible = false;
            navigationFrame.SelectedPageIndex = 0;
            navBarControl.Visible = true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            CierraPopUp();
        }

        private void CierraPopUp()
        {
            popUpCancelar.Visible = false;
            txtLogin.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtRazondecancelacion.Text = string.Empty;
        }

        private void btnAut_Click(object sender, EventArgs e)
        {
            UsuariosCL clU = new UsuariosCL();
            clU.strLogin = txtLogin.Text;
            clU.strClave = txtPassword.Text;
            clU.strPermiso = "CancelarHP";
            string result = clU.UsuariosPermisos();
            if (result != "OK")
            {
                MessageBox.Show(result);
                return;
            }

            CierraPopUp();
            //if (timbrado == 1)
            //{
            //    Cancelar();
            //}
            //else
            //{
            Cancelarenlabasededatos();
            //}
        }
        

        private void gridViewPrincipal_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                string strStatus = Convert.ToString(view.GetRowCellValue(e.RowHandle, "Status"));

                if (strStatus == "5")
                {
                    e.Appearance.ForeColor = Color.Red;
                }

            }
            catch (Exception ex)
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gridViewPrincipal_RowClick(object sender, RowClickEventArgs e)
        {
            intFolio = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Folio"));
            dfecha = Convert.ToDateTime(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Fecha"));
        }

        private void bbiImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                origenImp = "Principal";
                Reporte();
            }
        }

        private void bbiRegresarImp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            if (origenImp == "Principal")
            {
                navigationFrame.SelectedPageIndex = 0;
                navBarControl.Visible = true;
            }
                
            else
                navigationFrame.SelectedPageIndex = 1;
        }

        private void navBarControl_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            globalCL clg = new globalCL();
            string Name = e.Link.ItemName.ToString();
            if (clg.esNumerico(Name))
            {
                AñoFiltro = Convert.ToInt32(Name);
                llenarGrid(AñoFiltro, MesFiltro);
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

                llenarGrid(AñoFiltro, MesFiltro);

            }
        }

        private void bbiCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intFolio == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                popUpCancelar.Visible = true;
                groupControl3.Text = "Cancelar el folio:" +intFolio.ToString();
                txtLogin.Focus();
            }
        }
    }
}