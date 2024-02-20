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
using DevExpress.XtraReports.UI;
using VisualSoftErp.Operacion.Compras.Designers;
using DevExpress.Xpo;
using DevExpress.DataAccess.Sql;

namespace VisualSoftErp.Operacion.Compras.Informes
{
    public partial class Comprasrelacion : DevExpress.XtraBars.Ribbon.RibbonForm
    {


        IDataLayer DataLayerInstance = null;

        int Proveedores = 0;
        int Articulos = 0;
        //string Articulo = null;
        int Pais = 0;
        //int Familia = 0;

        public Comprasrelacion()
        {
            InitializeComponent();
            Cargacombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Cargacombos()
        {
            combosCL cl = new combosCL();
            globalCL clg = new globalCL();

            BindingSource src = new BindingSource();
         

            cboPaises.Properties.ValueMember = "Clave";
            cboPaises.Properties.DisplayMember = "Des";
            cl.strTabla = "Paises";
            src.DataSource = cl.CargaCombos();
            cboPaises.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboPaises.Properties.ForceInitialize();
            cboPaises.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboPaises.Properties.ForceInitialize();
            cboPaises.Properties.PopulateColumns();
            cboPaises.Properties.Columns["Clave"].Visible = false;
            cboPaises.ItemIndex = 0;

            cboProveedores.Properties.ValueMember = "Clave";
            cboProveedores.Properties.DisplayMember = "Des";
            cl.strTabla = "Proveedoresrep";
            src.DataSource = cl.CargaCombos();
            cboProveedores.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboProveedores.Properties.ForceInitialize();
            cboProveedores.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProveedores.Properties.ForceInitialize();
            cboProveedores.Properties.PopulateColumns();
            cboProveedores.Properties.Columns["Clave"].Visible = false;
            cboProveedores.ItemIndex = 0;

            cboArticulos.Properties.ValueMember = "Clave";
            cboArticulos.Properties.DisplayMember = "Des";
            cl.strTabla = "Articulos";
            src.DataSource = cl.CargaCombos();
            cboArticulos.Properties.DataSource = clg.AgregarOpcionTodos(src);
            cboArticulos.Properties.ForceInitialize();
            cboArticulos.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboArticulos.Properties.ForceInitialize();
            cboArticulos.Properties.PopulateColumns();
            cboArticulos.Properties.Columns["Clave"].Visible = false;
            cboArticulos.ItemIndex = 0;

            rdoNivelinfo.SelectedIndex = 0;
            cboArticulos.ItemIndex = 0;
            cboArticulos.Enabled = false;
        }

        private void Reporte()
        {
            try
            {
                Pais = Convert.ToInt32(cboPaises.EditValue);
                Proveedores = Convert.ToInt32(cboProveedores.EditValue);
                Articulos = Convert.ToInt32(cboArticulos.EditValue); ;
                //Linea = Convert.ToInt32(cboLinea.EditValue);
                //Familia = Convert.ToInt32(cboLinea.EditValue);


                int impDirecto = 0;
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

                switch (Convert.ToInt16(rdoNivelinfo.EditValue))
                {
                    case 1:
                        ImpresionComprasRelacionResumen(impDirecto);
                        break;
                    case 2:
                        ImpresionComprasRelacionDetalle(impDirecto);
                        break;

                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void ImpresionComprasRelacionResumen(int ImpDirecto)
        {
            string strDummy = swFF.IsOn ? "FF" : "x";

            ComprasCL cl = new ComprasCL();

            Comprasrelaciondesigner rep = new Comprasrelaciondesigner();
            cl.fFecha= Convert.ToDateTime(vsFiltroFechas1.FechaInicial); 
            cl.fFechaReal= Convert.ToDateTime(vsFiltroFechas1.FechaFinal);

            //SqlDataSource dataSource = (SqlDataSource)rep.DataSource;
            
            //XPObjectSource dataSource = (XPObjectSource)rep.DataSource;
            //dataSource.ResolveSession += new EventHandler<ResolveSessionEventArgs>(OnResolveSession);
            //dataSource.DismissSession += new EventHandler<ResolveSessionEventArgs>(OnDismissSession);

            if (ImpDirecto == 1)
            {
                rep.Parameters["parameter1"].Value = 0;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = 0;
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Proveedores;
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Pais; 
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = strDummy;
                rep.Parameters["parameter7"].Visible = false;
              
                ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                rpt.Print();
                return;
            }
            else
            {
                rep.Parameters["parameter1"].Value = 0;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = 0;
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Proveedores;
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Pais;
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = strDummy;
                rep.Parameters["parameter7"].Visible = false;
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                navigationFrame.SelectedPageIndex = 1;
            }
        }

        private void ImpresionComprasRelacionDetalle(int ImpDirecto)
        {
            ComprasrelaciondetalladaDesigner rep = new ComprasrelaciondetalladaDesigner();
            if (ImpDirecto == 1)
            {

                rep.Parameters["parameter1"].Value = 0;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = 0;
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Proveedores;
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Pais;
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = Articulos;
                rep.Parameters["parameter7"].Visible = false;
                rep.Parameters["parameter8"].Value = "x";
                rep.Parameters["parameter8"].Visible = false;

                ReportPrintTool rpt = new DevExpress.XtraReports.UI.ReportPrintTool(rep);
                rpt.Print();
                return;
            }
            else
            {
                rep.Parameters["parameter1"].Value = 0;
                rep.Parameters["parameter1"].Visible = false;
                rep.Parameters["parameter2"].Value = 0;
                rep.Parameters["parameter2"].Visible = false;
                rep.Parameters["parameter3"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaInicial);
                rep.Parameters["parameter3"].Visible = false;
                rep.Parameters["parameter4"].Value = Convert.ToDateTime(vsFiltroFechas1.FechaFinal);
                rep.Parameters["parameter4"].Visible = false;
                rep.Parameters["parameter5"].Value = Proveedores;
                rep.Parameters["parameter5"].Visible = false;
                rep.Parameters["parameter6"].Value = Pais;
                rep.Parameters["parameter6"].Visible = false;
                rep.Parameters["parameter7"].Value = Articulos;
                rep.Parameters["parameter7"].Visible = false;
                rep.Parameters["parameter8"].Value = "x";
                rep.Parameters["parameter8"].Visible = false;
                documentViewer1.DocumentSource = rep;
                rep.CreateDocument();
                ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageImpresion.Text);
                navigationFrame.SelectedPageIndex = 1;
            }
        }


        private void bbiVistaprevia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Generando informe","Espere por favor...");
            Reporte();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiRegresarImpresion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonPageHome.Text);
            navigationFrame.SelectedPageIndex = 0;
        }

        private void rdoNivelinfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(rdoNivelinfo.EditValue) == 1)
            {
                cboArticulos.Enabled = false;
            }
            else
            {
                cboArticulos.Enabled = true;
            }
        }

        private void OnResolveSession(object sender, ResolveSessionEventArgs e)
        {
            //Create a single IDataLayer instance if it does not exist
            if (DataLayerInstance == null)
            {
                string connectionString = globalCL.gv_strcnn; //ConfigurationManager.ConnectionStrings["nwind"].ConnectionString;
                DataLayerInstance = XpoDefault.GetDataLayer(connectionString, DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists);
            }

            //Create new session based on the DataLayer instance
            e.Session = new UnitOfWork(DataLayerInstance);
        }

        private void OnDismissSession(object sender, ResolveSessionEventArgs e)
        {
            e.Session.Session.Dispose();
        }
    }
}