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
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using VisualSoftErp.Catalogos;
using VisualSoftErp.Catalogos.Inv;
using VisualSoftErp.Catalogos.Ventas;
using VisualSoftErp.Catalogos.CXC;
using VisualSoftErp.Clases.CXC_CLs;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Ventas.Informes;
using VisualSoftErp.Herramientas.Formas;
using System.Threading;
using VisualSoftErp.Operacion.Inventarios.Formas;
using VisualSoftErp.Properties;
using DevExpress.LookAndFeel;
using System.IO;
using VisualSoftErp.Operacion.Ventas.Formas;
using VisualSoftErp.Operacion.Inventarios.Informes;
using VisualSoftErp.Operacion.CxP.Informes;
using VisualSoftErp.Operacion.Compras.Informes;
using VisualSoftErp.Operacion.CxP.Formas;
using VisualSoftErp.Operacion.CxC.Informes;
using VisualSoftErp.BI;
using VisualSoftErp.Operacion.CxC.Formas;
using VisualSoftErp.Operacion.CyB;
using VisualSoftErp.Operacion.CyB.Formas;
using VisualSoftErp.Catalogos.Herramientas;
using VisualSoftErp.Operacion.Compras.Formas;
using System.Diagnostics;
using VisualSoftErp.Operacion.Tienda.Formas;
using VisualSoftErp.Operacion.Tienda;
using VisualSoftErp.Operacion.Inventario.Informes;
using VisualSoftErp.Operacion.Tesk;
using VisualSoftErp.CCP.Catalogos;

namespace VisualSoftErp
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int iAppOrdenesPorAtender;
        int iAppSolicitudes;
        int iSalidasporautorizar;
        bool AlertaTienda;
        bool AlertaSalidasPorAutorizar;

        public Form1()
        {
            InitializeComponent();
           // UserLookAndFeel.Default.SkinName = Settings.Default["ApplicationSkinName"].ToString();
            Accesos();

            //Version
            string fileExe = System.Configuration.ConfigurationManager.AppSettings["pathlocalexe"].ToString();

            bsiEmpresa.Caption = globalCL.gv_NombreEmpresa + " | Ver " + FileVersionInfo.GetVersionInfo(fileExe).FileVersion; ;
            bsiUsuario.Caption = globalCL.gv_UsuarioNombre;

            CargaAlertas();

            timer1.Interval = 600000;  //10 min
            timer1.Start();
        }

        private void CargaAlertas()
        {

            

            UsuariosCL cl = new UsuariosCL();
            cl.intUsuariosID = globalCL.gv_UsuarioID;
            string result = cl.UsuariosLlenaCajas();
            if (result == "OK")
            {
                if (cl.intTienda == 1)
                    AlertaTienda = true;
                else
                    AlertaTienda = false;

                if (cl.iFirmaElectronicaSalidas == 1)
                    AlertaSalidasPorAutorizar = true;
                else
                    AlertaSalidasPorAutorizar = false;
                    
            }
            else
            {
                AlertaTienda = false;
                AlertaSalidasPorAutorizar = false;
            }
                
        }
                        
        
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Lineas frm = new Lineas();
            this.tabbedView.AddDocument(frm);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ribbonControl_Merge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e)
        {
            e.MergeOwner.SelectedPage = e.MergeOwner.MergedPages["Home"];
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Familias frm = new Familias();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            UnidadesDeMedida frm = new UnidadesDeMedida();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Almacenes frm = new Almacenes();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            TiposdeMovimientoInv frm = new TiposdeMovimientoInv();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Articulos2 frm = new Articulos2();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Monedas frm = new Monedas();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Transportes frm = new Transportes();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Tiposdecambio frm = new Tiposdecambio();
            this.tabbedView.AddDocument(frm);

        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Motivosnoaprobacioncotizaciones frm = new Motivosnoaprobacioncotizaciones();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Motivosdesalidas frm = new Motivosdesalidas();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Agentes frm = new Agentes();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Metododepago frm = new Metododepago();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Tipoderelacion frm = new Tipoderelacion();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Formapago frm = new Formapago();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Usocfdi frm = new Usocfdi();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Tiposdemovimientocxc frm = new Tiposdemovimientocxc();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Paises frm = new Paises();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Estados frm = new Estados();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Clientes frm = new Clientes();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Canalesdeventa frm = new Canalesdeventa();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Ciudades frm = new Ciudades();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Presupuestosporagente frm = new Presupuestosporagente();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Zonas frm = new Zonas();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Usuarios frm = new Usuarios();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            //Accesosporusuario frm = new Accesosporusuario();
            //this.tabbedView.AddDocument(frm);

        }

        private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Tiposdearticulo frm = new Tiposdearticulo();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Empresa frm = new Empresa();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Observaciones frm = new Observaciones();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Mediosdecontacto frm = new Mediosdecontacto();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Listasdeprecios  frm = new Listasdeprecios();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem32_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Cotizaciones frm = new Cotizaciones();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem33_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Cierresdemodulos frm = new Cierresdemodulos();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Remisiones frm = new Remisiones();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem35_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Facturas frm = new Facturas();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem36_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            Pedidos frm = new Pedidos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0425_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Facturas frm = new Facturas();
            this.tabbedView.AddDocument(frm);
        }

        private void ribbonControl_UnMerge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e)
        {
            ribbonControl.SelectedPage = globalCL.gv_RibbonPage;
        }

        private void bbi0430_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            RemisionesRelacion frm = new RemisionesRelacion();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0720_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInicio;
            Opciones frm = new Opciones();            
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0301_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Lineas frm = new Lineas();
            this.tabbedView.AddDocument(frm);
        }

        private void bb0302_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Familias frm = new Familias();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0304_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Almacenes frm = new Almacenes();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0303_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            UnidadesDeMedida frm = new UnidadesDeMedida();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0305_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            TiposdeMovimientoInv frm = new TiposdeMovimientoInv();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0307_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Articulos2 frm = new Articulos2();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0306_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Ubicaciones frm = new Ubicaciones();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0201_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            Tipodemovimientocxp frm = new Tipodemovimientocxp();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0401_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Tiposdecambio frm = new Tiposdecambio();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0405_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Monedas frm = new Monedas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0406_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Motivosdesalidas frm = new Motivosdesalidas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0407_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Motivosnoaprobacioncotizaciones frm = new Motivosnoaprobacioncotizaciones();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0402_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Observaciones frm = new Observaciones();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0408_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Transportes frm = new Transportes();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0409_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Presupuestosporagente frm = new Presupuestosporagente();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0403_ItemClick(object sender, ItemClickEventArgs e)
        {
            globalCL.gv_RibbonPage = ribbonPageVentas;
        }

        private void bbi0420_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Cotizaciones frm = new Cotizaciones();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0421_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Remisiones frm = new Remisiones();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0422_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Pedidos frm = new Pedidos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0501_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Tiposdemovimientocxc frm = new Tiposdemovimientocxc();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0502_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Paises frm = new Paises();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0503_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Zonas frm = new Zonas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0504_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Estados frm = new Estados();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0505_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Ciudades frm = new Ciudades();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0506_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Canalesdeventa frm = new Canalesdeventa();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0507_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Agentes frm = new Agentes();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0508_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Clientes frm = new Clientes();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0603_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            Usuarios frm = new Usuarios();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0604_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            Accesosporusuario frm = new Accesosporusuario();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0608_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            Empresa frm = new Empresa();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem38_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Relaciondepedidos frm = new Relaciondepedidos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0510_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Formapago frm = new Formapago();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem39_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Metododepago frm = new Metododepago();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0512_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Usocfdi frm = new Usocfdi();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0309_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Tiposdearticulo frm = new Tiposdearticulo();
            this.tabbedView.AddDocument(frm);
        }

        //Controlaremos los accesos
        private void Accesos()
        {
            try
            {
                LoginCL cl = new LoginCL();
                cl.iUsuarioId = globalCL.gv_UsuarioID;
                DataTable dt = new DataTable();
                dt = cl.Accesos();

                foreach (DataRow row in dt.Rows)
                {
                    
                    HabilitaAccesos(row["ProgramaID"].ToString());
                    HabilitaRibbon(row["RibbonPageGroup"].ToString());
                }

                //Regresamos al page donde estaba al salir
                //Se lee la última RibbonPageSelected
                string Path = System.Configuration.ConfigurationManager.AppSettings["ribbonselectedpage"].ToString();

                if (File.Exists(Path))
                {
                    string data = System.IO.File.ReadAllText(@Path);
                    if (data.Length > 0)
                    {
                        switch (data)
                        {
                            case "Compras\r\n":
                                Ribbon.SelectedPage = ribbonPageCompras;
                                break;
                            case "Cuentas por pagar\r\n":
                                Ribbon.SelectedPage = ribbonPageCxP;
                                break;
                            case "Inventarios\r\n":
                                Ribbon.SelectedPage = ribbonPageInventarios;
                                break;
                            case "Ventas\r\n":
                                Ribbon.SelectedPage = ribbonPageVentas;
                                break;
                            case "Cuentas por cobrar\r\n":
                                Ribbon.SelectedPage = ribbonPageCxC;
                                break;
                            case "Herramientas\r\n":
                                Ribbon.SelectedPage = ribbonPageHerramientas;
                                break;
                            case "Contabilidad\r\n":
                                Ribbon.SelectedPage = ribbonPageCyB;
                                break;
                            case "Tienda\r\n":
                                Ribbon.SelectedPage = ribbonPageTienda;
                                break;
                        }

                    }
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HabilitaRibbon(string rpg)  //Con esto habilitaremos el grupo (ribbonpage) de cada modulo de acuerdo a su acceso 
        {
            switch (rpg)
            {
                case "ribbonPageGroupComprasOperacion":
                    ribbonPageCompras.Visible = true;
                    ribbonPageGroupComprasOperacion.Visible = true;
                    break;
                case "ribbonPageGroupComprasInformes":
                    ribbonPageCompras.Visible = true;
                    ribbonPageGroupComprasInformes.Visible = true;
                    break;
                case "ribbonPageGroupCxPCatalogos":
                    ribbonPageCxP.Visible = true;
                    ribbonPageGroupCxPCatalogos.Visible = true;
                    break;
                case "ribbonPageGroupCxPOperacion":
                    ribbonPageCxP.Visible = true;
                    ribbonPageGroupCxPOperacion.Visible = true;
                    break;
                case "ribbonPageGroupCxPInformes":
                    ribbonPageCxP.Visible = true;
                    ribbonPageGroupCxPInformes.Visible = true;
                    break;
                case "ribbonPageGroupInventariosCatalogos":
                    ribbonPageInventarios.Visible = true;
                    ribbonPageGroupInventariosCatalogos.Visible = true;
                    break;
                case "ribbonPageGroupInventariosOperacion":
                    ribbonPageInventarios.Visible = true;
                    ribbonPageGroupInventariosOperacion.Visible = true;
                    break;
                case "ribbonPageGroupInventariosInformes":
                    ribbonPageInventarios.Visible = true;
                    ribbonPageGroupInventariosInformes.Visible = true;
                    break;
                case "ribbonPageGroupVentasCatalogos":
                    ribbonPageVentas.Visible = true;
                    ribbonPageGroupVentasCatalogos.Visible = true;
                    break;
                case "ribbonPageGroupVentasOperacion":
                    ribbonPageVentas.Visible = true;
                    ribbonPageGroupVentasOperacion.Visible = true;
                    break;
                case "ribbonPageGroupVentasInformes":
                    ribbonPageVentas.Visible = true;
                    ribbonPageGroupVentasInformes.Visible = true;
                    break;
                case "ribbonPageGroupCxCCatalogos":
                    ribbonPageCxC.Visible = true;
                    ribbonPageGroupCxCCatalogos.Visible = true;
                    break;
                case "ribbonPageGroupCxCOperacion":
                    ribbonPageCxC.Visible = true;
                    ribbonPageGroupCxCOperacion.Visible = true;
                    break;
                case "ribbonPageGroupCxCInformes":
                    ribbonPageCxC.Visible = true;
                    ribbonPageGroupCxCInformes.Visible = true;
                    break;
                case "ribbonPageGroupHerramientasCatalogos":                    
                    ribbonPageHerramientas.Visible = true;
                    ribbonPageGroupHerramientasCatalogos.Visible = true;
                    break;
                case "ribbonPageGroupHerramientasVentas":
                    ribbonPageHerramientas.Visible = true;
                    ribbonPageGroupHerramientasVentas.Visible = true;
                    break;
                case "ribbonPageGroupOpciones":
                    ribbonPageInicio.Visible = true;
                    ribbonPageGroupOpciones.Visible = true;
                    break;
                case "ribbonPageGroupTiendaCat":
                    ribbonPageTienda.Visible = true;
                    ribbonPageGroupTiendaCat.Visible = true;
                    break;
                case "ribbonPageGroupTiendaOpe":
                    ribbonPageTienda.Visible = true;
                    ribbonPageGroupTiendaOpe.Visible = true;
                    break;
                case "ribbonPageGroupHerramientasCorreos":
                    ribbonPageTienda.Visible = true;
                    ribbonPageGroupHerramientasCorreos.Visible = true;
                    break;
                case "ribbonPageHerramientasControl":
                    ribbonPageTienda.Visible = true;
                    ribbonPageHerramientasControl.Visible = true;
                    break;
                case "ribbonPageGroupHerramientasSat":
                    ribbonPageTienda.Visible = true;
                    ribbonPageGroupHerramientasSat.Visible = true;
                    break;
                case "ribbonPageGroupHerramientasConsultas":
                    ribbonPageTienda.Visible = true;
                    ribbonPageGroupHerramientasConsultas.Visible = true;
                    break;
                


            }
        }
        
        private void HabilitaAccesos(string programa)  //Con esto habilitamos los controles de cada modulo de acuerdo a su acceso 
        {
            switch (programa)
            {
                case "0120"://COMPRAS
                    bbi0120.Visibility = BarItemVisibility.Always;
                    break;
                case "0121":
                    bbi0121.Visibility = BarItemVisibility.Always;
                    break;
                case "0122":
                    bbi0122.Visibility = BarItemVisibility.Always;
                    break;
                case "0123":
                    bbi0123.Visibility = BarItemVisibility.Always;
                    break;
                case "0130":
                    bbi0130.Visibility = BarItemVisibility.Always;
                    break;
                case "0131":
                    bbi0131.Visibility = BarItemVisibility.Always;
                    break;
                case "0132":
                    bbi0132.Visibility = BarItemVisibility.Always;
                    break;
                case "0133":
                    bbi0133.Visibility = BarItemVisibility.Always;
                    break;
                case "0134":
                    bbi0132.Visibility = BarItemVisibility.Always;
                    break;
                case "0135":
                    bbi0135.Visibility = BarItemVisibility.Always;
                    break;
                case "0136":
                    bbi0136.Visibility = BarItemVisibility.Always;
                    break;
                case "0137":
                    bbi0137.Visibility = BarItemVisibility.Always;
                    break;
                case "0138":
                    bbi0138.Visibility = BarItemVisibility.Always;
                    break;
                case "0201"://CXP
                    bbi0201.Visibility = BarItemVisibility.Always;
                    break;
                case "0202":
                    bbi0202.Visibility = BarItemVisibility.Always;
                    break;
                case "0203":
                    bbi0203Desabled.Visibility = BarItemVisibility.Always;
                    break;
                case "0204":
                    bbi0204.Visibility = BarItemVisibility.Always;
                    break;
                case "0205":
                    bbi0205Desabled.Visibility = BarItemVisibility.Always;
                    break;
                case "0206":
                    bbi0206.Visibility = BarItemVisibility.Always;
                    break;
                case "0207":
                    bbi0207Disabled.Visibility = BarItemVisibility.Always;
                    break;
                case "0208":
                    bbi0208.Visibility = BarItemVisibility.Always;
                    break;
                case "0220":
                    bbi0220.Visibility = BarItemVisibility.Always;
                    break;
                case "0221":
                    bbi0221.Visibility = BarItemVisibility.Always;
                    break;
                case "0222":
                    bbi0222.Visibility = BarItemVisibility.Always;
                    break;
                case "0223":
                    bbi0223.Visibility = BarItemVisibility.Always;
                    break;
                case "0230":
                    bbi0230.Visibility = BarItemVisibility.Always;
                     break;
                case "0231":
                    bbi0231.Visibility = BarItemVisibility.Always;
                    break;
                case "0232":
                    bbi0232.Visibility = BarItemVisibility.Always;
                    break;
                case "0233":
                    bbi0233.Visibility = BarItemVisibility.Always;
                    break;
                case "0234":
                    bbi0234.Visibility = BarItemVisibility.Always;
                    break;
                case "0235":
                    bbi0235.Visibility = BarItemVisibility.Always;
                    break;
                case "0301"://Inventarios
                    bbi0301.Visibility = BarItemVisibility.Always;
                    break;
                case "0302":
                    bbi0302.Visibility = BarItemVisibility.Always;
                    break;
                case "0303":
                    bbi0303.Visibility = BarItemVisibility.Always;
                    break;
                case "0304":
                    bbi0304.Visibility = BarItemVisibility.Always;
                    break;
                case "0305":
                    bbi0305.Visibility = BarItemVisibility.Always;
                    break;
                case "0306":
                    bbi0306.Visibility = BarItemVisibility.Always;
                    break;
                case "0307":
                    bbi0307.Visibility = BarItemVisibility.Always;
                    break;
                case "0308":
                    bbi0308.Visibility = BarItemVisibility.Always;
                    break;
                case "0309":
                    bbi0309.Visibility = BarItemVisibility.Always;
                    break;
                case "0311":
                    bbi0311.Visibility = BarItemVisibility.Always;
                    break;
                case "0312":
                    bbi0312.Visibility = BarItemVisibility.Always;
                    break;
                case "0337":
                    bbi0337.Visibility = BarItemVisibility.Always;
                    break;
                case "0320":
                    bbi0320.Visibility = BarItemVisibility.Always;
                    break;
                case "0321":
                    bbi0321.Visibility = BarItemVisibility.Always;
                    break;
                case "0322":
                    bbiInventarioFisico.Visibility = BarItemVisibility.Always;
                    bbi0322.Visibility = BarItemVisibility.Always;
                    break;
                case "0323":
                    bbiInventarioFisico.Visibility = BarItemVisibility.Always;
                    bbi0323.Visibility = BarItemVisibility.Always;
                    break;
                case "0324":
                    bbiInventarioFisico.Visibility = BarItemVisibility.Always;
                    bbi0324.Visibility = BarItemVisibility.Always;
                    break;
                case "0325":
                    bbiInventarioFisico.Visibility = BarItemVisibility.Always;
                    bbi0325.Visibility = BarItemVisibility.Always;
                    break;
                case "0326":
                    bbiSProduccion.Visibility = BarItemVisibility.Always;
                    bbi0326.Visibility = BarItemVisibility.Always;
                    break;
                case "0327":
                    bbiSProduccion.Visibility = BarItemVisibility.Always;
                    bbi0327.Visibility = BarItemVisibility.Always;
                    break;
                case "0328":
                    bbiSProduccion.Visibility = BarItemVisibility.Always;
                    bbi0328.Visibility = BarItemVisibility.Always;
                    break;
                case "0329":
                    bbiSProduccion.Visibility = BarItemVisibility.Always;
                    bbi0329.Visibility = BarItemVisibility.Always;
                    break;
                case "0342":                   
                    bbi0342.Visibility = BarItemVisibility.Always;
                    break;
                case "0330":
                    bbi0330.Visibility = BarItemVisibility.Always;
                    break;
                case "0331":
                    bbi0331.Visibility = BarItemVisibility.Always;
                    break;
                case "0332":
                    bbi0332.Visibility = BarItemVisibility.Always;
                    break;
                case "0333":
                    bbi0333.Visibility = BarItemVisibility.Always;
                    break;
                case "0334":
                    bbi0334.Visibility = BarItemVisibility.Always;
                    break;
                case "0335":
                    bbi0335.Visibility = BarItemVisibility.Always;
                    break;
                case "0336":
                    bbi0336.Visibility = BarItemVisibility.Always;
                    break;
                              
                case "0401"://Ventas
                    bbi0401.Visibility = BarItemVisibility.Always;
                    break;
                case "0402":
                    bbi0402.Visibility = BarItemVisibility.Always;
                    break;
                case "0403":
                    bbi0403.Visibility = BarItemVisibility.Always;
                    break;
                case "0404":
                    bbi0404.Visibility = BarItemVisibility.Always;
                    break;
                case "0405":
                    bbi0405.Visibility = BarItemVisibility.Always;
                    break;
                case "0406":
                    bbi0406.Visibility = BarItemVisibility.Always;
                    break;
                case "0407":
                    bbi0407.Visibility = BarItemVisibility.Always;
                    break;
                case "0408":
                    bbi0408.Visibility = BarItemVisibility.Always;
                    break;
                case "0409":
                    bbi0409.Visibility = BarItemVisibility.Always;
                    break;
                case "0410":
                    bbi0410.Visibility = BarItemVisibility.Always;
                    break;
                case "0411":
                    bbi0411.Visibility = BarItemVisibility.Always;
                    break;
                case "0420":
                    bbi0420.Visibility = BarItemVisibility.Always;
                    break;
                case "0421":
                    bbi0421.Visibility = BarItemVisibility.Always;
                    break;
                case "0422":
                    bbi0422.Visibility = BarItemVisibility.Always;
                    break;
                case "0423":
                    bbi0423.Visibility = BarItemVisibility.Always;
                    break;
                case "0424":
                    bbi0424.Visibility = BarItemVisibility.Always;
                    break;
                case "0425":
                    bbi0425.Visibility = BarItemVisibility.Always;
                    break;
                case "0427":
                    bbi0427.Visibility = BarItemVisibility.Always;
                    break;
                case "0430":
                    bbi0430.Visibility = BarItemVisibility.Always;
                    break;
                case "0431":
                    bbi0431.Visibility = BarItemVisibility.Always;
                    break;
                case "0432":
                    bbi0432.Visibility = BarItemVisibility.Always;
                    break;
                case "0433":
                    bbi0433.Visibility = BarItemVisibility.Always;
                    break;
                case "0434":
                    bbi0434.Visibility = BarItemVisibility.Always;
                    break;
                case "0435":
                    bbi0435.Visibility = BarItemVisibility.Always;
                    break;
                case "0436":
                    bbi0436.Visibility = BarItemVisibility.Always;
                    break;
                case "0437":
                    bbi0437.Visibility = BarItemVisibility.Always;
                    break;
                case "0438":
                    bbi0438.Visibility = BarItemVisibility.Always;
                    break;
                case "0439":
                    bbi0439.Visibility = BarItemVisibility.Always;
                    break;
                case "0440":
                    bbi0440.Visibility = BarItemVisibility.Always;
                    break;
                case "0443":
                    bbi0443.Visibility = BarItemVisibility.Always;
                    break;
                case "0446":
                    bbi0446.Visibility = BarItemVisibility.Always;
                    break;
                case "0501"://CXC
                    bbi0501.Visibility = BarItemVisibility.Always;
                    break;
                case "0502":
                    bbi0502.Visibility = BarItemVisibility.Always;
                    break;
                case "0503":
                    bbi0503.Visibility = BarItemVisibility.Always;
                    break;
                case "0504":
                    bbi0504.Visibility = BarItemVisibility.Always;
                    break;
                case "0505":
                    bbi0505.Visibility = BarItemVisibility.Always;
                    break;
                case "0506":
                    bbi0506.Visibility = BarItemVisibility.Always;
                    break;
                case "0507":
                    bbi0507.Visibility = BarItemVisibility.Always;
                    break;
                case "0508":
                    bbi0508.Visibility = BarItemVisibility.Always;
                    break;
                case "0509":
                    bbi0509.Visibility = BarItemVisibility.Always;
                    break;
                case "0510":
                    bbiSat.Visibility = BarItemVisibility.Always;
                    bbi0510.Visibility = BarItemVisibility.Always;
                    break;
                case "0511":
                    bbiSat.Visibility = BarItemVisibility.Always;
                    bbi0511.Visibility = BarItemVisibility.Always;
                    break;
                case "0512":
                    bbiSat.Visibility = BarItemVisibility.Always;
                    bbi0512.Visibility = BarItemVisibility.Always;
                    break;
                case "0513":
                    bbiSat.Visibility = BarItemVisibility.Always;
                    bbi0513.Visibility = BarItemVisibility.Always;
                    break;
                case "0514":
                    bbi0514.Visibility = BarItemVisibility.Always;
                    break;
                case "0515":
                    bbi0515.Visibility = BarItemVisibility.Always;
                    break;
                case "0520":
                    bbi0520.Visibility = BarItemVisibility.Always;
                    break;
                case "0521":
                    bbi0521.Visibility = BarItemVisibility.Always;
                    break;
                case "0522":
                    bbi0522.Visibility = BarItemVisibility.Always;
                    break;
                case "0523":
                    bbi0523.Visibility = BarItemVisibility.Always;
                    break;
                case "0524":
                    bbi0524.Visibility = BarItemVisibility.Always;
                    break;
                case "0530":
                    bbi0530.Visibility = BarItemVisibility.Always;
                    break;
                case "0531":
                    bbi0531.Visibility = BarItemVisibility.Always;
                    break;
                case "0532":
                    bbi0532.Visibility = BarItemVisibility.Always;
                    break;
                case "0533":
                    bbi0533.Visibility = BarItemVisibility.Always;
                    break;
                case "0534":
                    bbi0534.Visibility = BarItemVisibility.Always;
                    break;
                case "0535":
                    bbi0535.Visibility = BarItemVisibility.Always;
                    break;
                case "0536":
                    bbi0536.Visibility = BarItemVisibility.Always;
                    break;
                case "0537":
                    bbi0537.Visibility = BarItemVisibility.Always;
                    break;
                case "0538":
                    bbi0538.Visibility = BarItemVisibility.Always;
                    break;
                case "0539":
                    bbi0539.Visibility = BarItemVisibility.Always;
                    break;
                case "0601"://Herramientas
                    bbi0601.Visibility = BarItemVisibility.Always;
                    break;
                case "0602":
                    bbi0602.Visibility = BarItemVisibility.Always;
                    break;
                case "0603":
                    bbi0603.Visibility = BarItemVisibility.Always;
                    break;
                case "0604":
                    bbi0604.Visibility = BarItemVisibility.Always;
                    break;
                case "0605":
                    bbi0605.Visibility = BarItemVisibility.Always;
                    break;
                case "0606":
                    bbi0606.Visibility = BarItemVisibility.Always;
                    break;
                case "0607":
                    bbi0607.Visibility = BarItemVisibility.Always;
                    break;
                case "0608":
                    bbi0608.Visibility = BarItemVisibility.Always;
                    break;
                case "0609":
                    bbi0608.Visibility = BarItemVisibility.Always;
                    break;
                case "0610":
                    bbi0610.Visibility = BarItemVisibility.Always;
                    break;
                case "0612":
                    bbi0612.Visibility = BarItemVisibility.Always;
                    break;
                case "0670":
                    bbi0670.Visibility = BarItemVisibility.Always;
                    break;
                case "0680":
                    bbi0680.Visibility = BarItemVisibility.Always;
                    break;
                case "0681":
                    bbi0681.Visibility = BarItemVisibility.Always;
                    break;
                case "0682":
                    bbi0682.Visibility = BarItemVisibility.Always;
                    ribbonPageHerramientas.Visible = true;
                    ribbonPageGroupHerramientasVentas.Visible = true;
                    break;
                case "0683":
                    bbi0683.Visibility = BarItemVisibility.Always;
                    ribbonPageHerramientas.Visible = true;
                    ribbonPageGroupHerramientasTesk.Visible = true;
                    break;
                case "0684":
                    bbi0684.Visibility = BarItemVisibility.Always;
                    ribbonPageHerramientas.Visible = true;
                    break;
                case "0720":
                    bbi0720.Visibility = BarItemVisibility.Always;
                    break;
                case "0801":
                    bbi0801.Visibility = BarItemVisibility.Always;
                    break;
                case "090":
                    bbi0901.Visibility = BarItemVisibility.Always;
                    break;
                case "0902":
                    bbi0902.Visibility = BarItemVisibility.Always;
                    break;
                case "0903":
                    bbi0903.Visibility = BarItemVisibility.Always;
                    break;
                case "1001":
                    bbi1001.Visibility = BarItemVisibility.Always;
                    break;
                case "1003":
                    bbi1004.Visibility = BarItemVisibility.Always;
                    break;
                case "1004":
                    bbi1004.Visibility = BarItemVisibility.Always;
                    break;
                case "1005":
                    bbi1005.Visibility = BarItemVisibility.Always;
                    break;
                case "1006":
                    bbi1006.Visibility = BarItemVisibility.Always;
                    break;
                case "1007":
                    bbi1007.Visibility = BarItemVisibility.Always;
                    break;
                case "1008":
                    bbi1008.Visibility = BarItemVisibility.Always;
                    break;
                case "1009":
                    bbi1009.Visibility = BarItemVisibility.Always;
                    break;
                case "1021":
                    bbi1021.Visibility = BarItemVisibility.Always;
                    break;
                case "1022":
                    bbi1022.Visibility = BarItemVisibility.Always;
                    break;
                case "1101":
                    bbi1101.Visibility = BarItemVisibility.Always;
                    break;
                case "1102":
                    bbi1102.Visibility = BarItemVisibility.Always;
                    break;
                case "1103":
                    bbi1103.Visibility = BarItemVisibility.Always;
                    break;
                case "1104":
                    bbi1104.Visibility = BarItemVisibility.Always;
                    break;
                case "1105":
                    bbi1105.Visibility = BarItemVisibility.Always;
                    break;
                case "1106":
                    bbi1106.Visibility = BarItemVisibility.Always;
                    break;
                case "1107":
                    bbi1107.Visibility = BarItemVisibility.Always;
                    break;
                case "1108":
                    bbi1108.Visibility = BarItemVisibility.Always;
                    break;
            }
        }

        private void bbi0513_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Tipoderelacion frm = new Tipoderelacion();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0514_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Mediosdecontacto frm = new Mediosdecontacto();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0410_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Listasdeprecios frm = new Listasdeprecios();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0433_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Ventautilidadmarginalporfactura frm = new Ventautilidadmarginalporfactura();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0820_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cerrar();
        }

        private void bbi0308_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
                Kits frm = new Kits();
            this.tabbedView.AddDocument(frm);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult Result = MessageBox.Show("Desea salir del sistema ", "Cerrar", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                e.Cancel = true;
                return;
            }


            Settings.Default["ApplicationSkinName"] = UserLookAndFeel.Default.SkinName;
            Settings.Default.Save();

            //Se guarda la pagina actual del ribbon
            string Path = System.Configuration.ConfigurationManager.AppSettings["ribbonselectedpage"].ToString();

            try
            {
                if (globalCL.gv_RibbonPage != null)
                {
                    String data = globalCL.gv_RibbonPage.ToString();

                    using (StreamWriter writer = new StreamWriter(Path))
                    {
                        writer.WriteLine(data);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Menu.FormClosing: " + ex.Message);
            }

            Dispose();
            System.Windows.Forms.Application.Exit();

        }
        private void Cerrar()
        {
            DialogResult Result = MessageBox.Show("Desea salir del sistema ", "Cerrar", MessageBoxButtons.YesNo);
            if (Result.ToString() == "No")
            {
                return;
            }


            Settings.Default["ApplicationSkinName"] = UserLookAndFeel.Default.SkinName;
            Settings.Default.Save();

            //Se guarda la pagina actual del ribbon
            string Path = System.Configuration.ConfigurationManager.AppSettings["ribbonselectedpage"].ToString();

            try
            {
                if (globalCL.gv_RibbonPage != null)
                {
                    String data = globalCL.gv_RibbonPage.ToString();

                    using (StreamWriter writer = new StreamWriter(Path))
                    {
                        writer.WriteLine(data);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Menu.FormClosing: " + ex.Message);
            }

            Dispose();
            System.Windows.Forms.Application.Exit();
            //this.Close();
        }

        private void bbi0435_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Ventasporagente frm = new Ventasporagente();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0432_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            FacturasRelacion frm = new FacturasRelacion();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0434_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Ventasporcliente frm = new Ventasporcliente();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0436_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Ventasporfamiliaarticulo frm = new Ventasporfamiliaarticulo();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0206_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            Proveedores frm = new Proveedores();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0121_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCompras;
            Compras frm = new Compras();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0404_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Choferes frm = new Choferes();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0120_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCompras;
            Ordenesdecompras frm = new Ordenesdecompras();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0423_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            SurtirPedidos frm = new SurtirPedidos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0520_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Depositos frm = new Depositos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0320_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Entradasysalidas frm = new Entradasysalidas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0333_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Existencias frm = new Existencias();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0532_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Antiguedaddesaldoscxc frm = new Antiguedaddesaldoscxc();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0122_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCompras;
            Recepciondemercancia frm = new Recepciondemercancia();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0202_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            Bancos frm = new Bancos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbiManualdeusuario_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("https://566328.wixsite.com/erpnet");
            
        }

        private void bbi0424_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            GenerarFacturas frm = new GenerarFacturas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0322_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Inventariofisicorep frm = new Inventariofisicorep();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0330_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Movimientosporarticulo frm = new Movimientosporarticulo();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0323_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Inventariofisicoregistrarconteo frm = new Inventariofisicoregistrarconteo();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0324_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Inventariocomparativo frm = new Inventariocomparativo();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem38_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            ComprasXML frm = new ComprasXML();
            this.tabbedView.AddDocument(frm);



        }

        private void bbi0325_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Ajustes frm = new Ajustes();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0232_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            CxPAntiguedaddesaldos frm = new CxPAntiguedaddesaldos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0331_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            InventariosRelaciondeentradasysalidas frm = new InventariosRelaciondeentradasysalidas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0131_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCompras;
            Comprasrelacion frm = new Comprasrelacion();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0130_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCompras;
            Relacionordenesdecompras frm = new Relacionordenesdecompras();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0220_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            PagosCxP frm = new PagosCxP();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0609_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            Cancelacionesdirectas frm = new Cancelacionesdirectas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0204_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            cyb_Chequeras frm = new cyb_Chequeras();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0332_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Inventariovalorizado frm = new Inventariovalorizado();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0230_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void bbi0530_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void bbi0234_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            RelaciondepagosCxP frm = new RelaciondepagosCxP();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0233_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            Cuentasporpagar frm = new Cuentasporpagar();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0610_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            Administracorreos frm = new Administracorreos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0534_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            RelaciondepagosCxC frm = new RelaciondepagosCxC();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0533_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            CuentasporcobrarCxC frm = new CuentasporcobrarCxC();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0531_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            EstadosdecuentaCxC frm = new EstadosdecuentaCxC();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem38_ItemClick_2(object sender, ItemClickEventArgs e)
        {
           
        }

        private void bbi0132_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCompras;
            Relaciondecomprasporproveedor frm = new Relaciondecomprasporproveedor();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0133_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCompras;
            Relaciondecomprasporarticulo frm = new Relaciondecomprasporarticulo();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0535_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Operacion.CxC.Informes.Carteravencida frm = new Operacion.CxC.Informes.Carteravencida();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0438_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Estadisticaventasporcliente frm = new Estadisticaventasporcliente();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0801_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientasBI;
            DashView frm = new DashView();
            this.tabbedView.AddDocument(frm);

        }

        private void bbi0439_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Estadisticadeventasporlineafamiliaarticulo frm = new Estadisticadeventasporlineafamiliaarticulo();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0221_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            ContraRecibos frm = new ContraRecibos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0521_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Notasdecredito frm = new Notasdecredito();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0536_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Relaciondenotasdecredito frm = new Relaciondenotasdecredito();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem39_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            //Prueba frm = new Prueba();
            MailingCxC frm = new MailingCxC();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0437_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Ventasporclientearticulo frm = new Ventasporclientearticulo();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0440_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Ventasporarticulocliente frm = new Ventasporarticulocliente();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0231_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            EstadosdecuentaCxP frm = new EstadosdecuentaCxP();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0901_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCyB;
            Polizadecompras frm = new Polizadecompras();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0902_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCyB;
            Polizadeservicios frm = new Polizadeservicios();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0903_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCyB;
            Polizadepagoscxp frm = new Polizadepagoscxp();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0611_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            ComprasXML frm = new ComprasXML();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0612_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            MailingCxC frm = new MailingCxC();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0602_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

         
        
        
        private void bbi0222_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            globalCL.gv_AnticiposOrigen = "CXP";
            AnticiposCxP frm = new AnticiposCxP();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0223_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            globalCL.gv_AnticiposOrigen = "CXP";
            AplicarAnticiposCxP frm = new AplicarAnticiposCxP();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0615_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            DevExpress.XtraEditors.XtraForm FlexArt = new FlexArt();
            DialogResult dialogResult;
            dialogResult = FlexArt.ShowDialog(this);
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Perform required actions here if the dialog result is Ok.
            }
            else
            {
                // Perform default actions here.
            }
            FlexArt.Dispose();
        }

        private void barButtonItem38_ItemClick_3(object sender, ItemClickEventArgs e)
        {
            
        }

        private void bbi0310_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            SubFamilias frm = new SubFamilias();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem38_ItemClick_4(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
          
            DevExpress.XtraEditors.XtraForm FlexArt = new FlexArt();
            DialogResult dialogResult;
            dialogResult = FlexArt.ShowDialog(this);
           
            FlexArt.Dispose();
        }

        private void bbi0326_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Componentes frm = new Componentes();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0327_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Hojadeproduccion frm = new Hojadeproduccion();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0328_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Movimientosahojadeproduccion frm = new Movimientosahojadeproduccion();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0329_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Produccion frm = new Produccion();
            this.tabbedView.AddDocument(frm);
        }

        private void toastNotificationsManager1_Activated(object sender, DevExpress.XtraBars.ToastNotifications.ToastNotificationEventArgs e)
        {
            switch (e.NotificationID.ToString())
            {
                case "57cb0baa-7c0e-470a-9a15-7827954bb2e2":   //Ordenes por atender de la tienda en línea
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
                    globalCL.gv_RibbonPage = ribbonPageTienda;
                    TiendaOrdenes frm = new TiendaOrdenes();
                    this.tabbedView.AddDocument(frm);
                    break;
                case "523f5cbf-c64a-4d5a-8ef9-1b79db788a6b":
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
                    globalCL.gv_RibbonPage = ribbonPageInventarios;
                    Entradasysalidas frm2 = new Entradasysalidas();
                    this.tabbedView.AddDocument(frm2);
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           
            if (!AlertaTienda && !AlertaSalidasPorAutorizar)
            {
                iAppOrdenesPorAtender = 0;
                iSalidasporautorizar = 0;
                return;
            }    

            globalCL cl = new globalCL();

            int i = 0;

            if (AlertaTienda)
            {
                string result = cl.AppOrdenesporatender();
                if (result == "OK")
                {
                    iAppOrdenesPorAtender = cl.intOrdenesPorAtender;
                    iAppSolicitudes = cl.intSolicitudesDeRegistro;
                }
                else
                {
                    iAppOrdenesPorAtender = 0;
                }

                i++;
            }

            if (AlertaSalidasPorAutorizar)
            {
                string result = cl.Salidasporautorizar();
                if (result == "OK")
                {
                    iSalidasporautorizar = cl.intSalidasporautorizar;
                }
                else
                {
                    iSalidasporautorizar = 0;
                }

                i++;
            }



        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Cancelled)
            {
                MessageBox.Show("The task has been cancelled");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error. Details: " + (e.Error as Exception).ToString());
            }
            else

            {
                if (iAppOrdenesPorAtender>0)
                {
                    toastNotificationsManager1.Notifications[0].Body = "Tiene " + iAppOrdenesPorAtender.ToString() + " órdenes por atender";
                    toastNotificationsManager1.ShowNotification("57cb0baa-7c0e-470a-9a15-7827954bb2e2");                                                                
                    bhCarrito.Caption = iAppOrdenesPorAtender.ToString();                                                          
                }
                if (iSalidasporautorizar > 0)
                {
                    toastNotificationsManager1.Notifications[1].Body = "Tiene " + iSalidasporautorizar.ToString() + " salidas por autorizar";
                    toastNotificationsManager1.ShowNotification("523f5cbf-c64a-4d5a-8ef9-1b79db788a6b");                         
                }               
            }            
        }

        private void bhCarrito_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Movimientosahojadeproduccion frm = new Movimientosahojadeproduccion();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0342_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            Traspasos frm = new Traspasos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbiAddendas_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            Addendas frm = new Addendas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1001_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageTienda;
            AppBanners frm = new AppBanners();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1003_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageTienda;
            AppFichasTecnicas frm = new AppFichasTecnicas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1004_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageTienda;
            AppTonos frm = new AppTonos();
            this.tabbedView.AddDocument(frm);
        }

        

     

        private void bbi0670_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            CasaLey_Articulos frm = new CasaLey_Articulos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbiInfoCte_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();

            DevExpress.XtraEditors.XtraForm frm = new infoCliente();
            DialogResult dialogResult;
            dialogResult = frm.ShowDialog(this);

            frm.Dispose();
        }

        

        private void bbi0680_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            FlexArt frm = new FlexArt();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0681_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            infoCliente frm = new infoCliente();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0135_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCompras;
            AuxiliardecomprasExp frm = new AuxiliardecomprasExp();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0443_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            Clientesquenohabiancomprado frm = new Clientesquenohabiancomprado();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1005_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageTienda;
            AppMedidas frm = new AppMedidas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1006_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageTienda;
            AppLineasArticulosTonosyMedidas frm = new AppLineasArticulosTonosyMedidas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0136_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCompras;
            Auxiliardecomprasnacionales frm = new Auxiliardecomprasnacionales();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1007_ItemClick(object sender, ItemClickEventArgs e)
        {

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageTienda;
            AppOfertas frm = new AppOfertas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1008_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageTienda;
            AppSubfamiliasDescripciones frm = new AppSubfamiliasDescripciones();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0235_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            CarteravencidaCxP frm = new CarteravencidaCxP();
            this.tabbedView.AddDocument(frm);
        }

        private void bbiImplementacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            Controldeimplementacion frm = new Controldeimplementacion();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0515_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            ZoneArticulos frm = new ZoneArticulos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0538_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            CxCSaldosResumenPorUen frm = new CxCSaldosResumenPorUen();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1021_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageTienda;
            TiendaOrdenes frm = new TiendaOrdenes();
            this.tabbedView.AddDocument(frm);
        }

        private void tileBarItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageTienda;
            TiendaOrdenes frm = new TiendaOrdenes();
            this.tabbedView.AddDocument(frm);
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void bbi0311_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            ArticulosGruposAuxiliar frm = new ArticulosGruposAuxiliar();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0411_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Pronosticosporarticuloparaimportacion frm = new Pronosticosporarticuloparaimportacion();
            this.tabbedView.AddDocument(frm);
        }

        private void bbiInfoProv_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();

            DevExpress.XtraEditors.XtraForm frm = new infoProv();
            DialogResult dialogResult;
            dialogResult = frm.ShowDialog(this);

            frm.Dispose();
        }

        private void bbi0601_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            Cierresdemodulos frm = new Cierresdemodulos();
            this.tabbedView.AddDocument(frm);
        }
        
        private void bbi0522_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            globalCL.gv_AnticiposOrigen = "CXC";
            AnticiposCxP frm = new AnticiposCxP();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0524_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            ValesCadenas frm = new ValesCadenas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0137_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCompras;
            IncrementalesRep frm = new IncrementalesRep();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0138_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCompras;
            EstadisticaPorProveedor frm = new EstadisticaPorProveedor();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1009_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageTienda;
            AppFamiliasSubfamiliasImagenes frm = new AppFamiliasSubfamiliasImagenes();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0523_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            globalCL.gv_AnticiposOrigen = "CXC";
            AplicarAnticiposCxP frm = new AplicarAnticiposCxP();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0334_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            bfMovsEstadistica frm = new bfMovsEstadistica();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0335_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            InventariosControldeExistencia frm = new InventariosControldeExistencia();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0336_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            inventarioCiclicoEstadistica frm = new inventarioCiclicoEstadistica();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0444_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            PedidosCambiosdeprecio frm = new PedidosCambiosdeprecio();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0224_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxP;
            ContrarecibosFromXML frm = new ContrarecibosFromXML();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0682_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            Tesk frm = new Tesk();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0683_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            AutorizarCambiosdePrecio frm = new AutorizarCambiosdePrecio();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0684_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            CompulsaTesk frm = new CompulsaTesk();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0123_ItemClick(object sender, ItemClickEventArgs e)
        {
            //bbi0123
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCompras;
            Validarcompras frm = new Validarcompras();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0539_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            clientesRetenidos frm = new clientesRetenidos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1022_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageTienda;
            AppSolicitudderegistro frm = new AppSolicitudderegistro();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1101_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCCP;
            Aseguradoras frm = new Aseguradoras();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1102_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCCP;
            Marcas frm = new Marcas();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1103_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCCP;
            Remitentesydestinatarios frm = new Remitentesydestinatarios();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1104_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCCP;
            Domicilios frm = new Domicilios();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1105_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCCP;
            Carros frm = new Carros();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1106_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCCP;
            Remolques frm = new Remolques();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi1107_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCCP;
            Operadores frm = new Operadores();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi06111_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageHerramientas;
            ftp frm = new ftp();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0312_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            ArticulosCapturaUbicaciones frm = new ArticulosCapturaUbicaciones();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0337_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageInventarios;
            MovimientosPorArticuloEspecial frm = new MovimientosPorArticuloEspecial();
            this.tabbedView.AddDocument(frm);
        }
              

        private void bbiClientesSucursales_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageCxC;
            ClientesSucursales frm = new ClientesSucursales();
            this.tabbedView.AddDocument(frm);
        }

     

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            EstadisticaVentasPorClienteDetalle frm = new EstadisticaVentasPorClienteDetalle();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem41_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            VentaEstadoClienteCanal frm = new VentaEstadoClienteCanal();
            this.tabbedView.AddDocument(frm);
        }

        private void barButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {            
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            ReporteAnticipos frm = new ReporteAnticipos();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0446_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            VentasEstadisticaPorSucursal frm = new VentasEstadisticaPorSucursal();
            this.tabbedView.AddDocument(frm);
        }

        private void bbi0427_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            globalCL.gv_RibbonPage = ribbonPageVentas;
            Guías frm = new Guías();
            this.tabbedView.AddDocument(frm);
        }
    }
}
