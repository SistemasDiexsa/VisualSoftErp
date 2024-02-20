using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using VisualSoftErp.Clases;
using VisualSoftErp.BI;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraGrid.Views.Grid;

namespace VisualSoftErp.Herramientas.Formas
{
    public partial class infoCliente : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int cliente;
        string strGrid = string.Empty;
        string strNom = string.Empty;

        public infoCliente()
        {
            InitializeComponent();

            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;

            gridViewVentas.OptionsBehavior.ReadOnly = true;
            gridViewVentas.OptionsBehavior.Editable = false;
            gridViewVentas.OptionsView.ShowAutoFilterRow = true;
            gridViewVentas.OptionsView.ShowViewCaption = true;


            navigationFrame1.SelectedPageIndex = 1;
            

            txtCte.Select();

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiVentas_ItemClick(object sender, ItemClickEventArgs e)
        {
            strGrid = "Ventas";
            Ventas();
        }

        private void CargaClientes()
        {
            clientesCL cl = new clientesCL();
            cl.strNombre = txtCte.Text;
            gridControl1.DataSource = cl.infoCliente();
            gridView1.FocusedRowHandle = 0;
            cliente = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ClientesID"));

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridInfoCliente";
            clg.restoreLayout(gridView1);
        }

        private void txtCte_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    CargaClientes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void botones()
        {
            bbiVentas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDashBoard.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCxC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void Ventas()
        {
            try
            {
                if (cliente == 0)
                {
                    MessageBox.Show("De click en el renglón para seleccionar el cliente");
                    return;
                }
                clientesCL cl = new clientesCL();
                cl.intClientesID = cliente;
                gridControlVentas.DataSource = cl.infoClienteVentas();
                gridViewVentas.ViewCaption = "VENTAS DEL CLIENTE " + strNom;

                globalCL clg = new globalCL();
                clg.strGridLayout = "gridInfoCteVentas";
                clg.restoreLayout(gridViewVentas);

                

                botones();

                navigationFrame1.SelectedPageIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            cliente = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ClientesID"));
            if (cliente>0)
                strNom = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nombre").ToString();
        }

        private void bbiRegresar_ItemClick(object sender, ItemClickEventArgs e)
        {
            Regresar();
        }

        private void Regresar()
        {
            globalCL clg = new globalCL();
            string result=string.Empty;

            bbiVentas.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDashBoard.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCxC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navigationFrame1.SelectedPageIndex = 1;

            switch (strGrid)
            {
                case "Ventas":
                    
                    clg.strGridLayout = "gridInfoCteVentas";
                    result = clg.SaveGridLayout(gridViewVentas);
                    if (result != "OK")
                    {
                        MessageBox.Show(result);
                    }
                    break;
                   
            }
            
        }

        private void bbiDashBoard_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Newsa.net","Generando dashboard...");
            strGrid = "Dashboard";
            botones();

            //Datos
            DashboardClientes ds = new DashboardClientes();
            ds.Parameters["Cliente"].Value = cliente;
            ds.Parameters["Año"].Value = 2020;

            dashboardViewer1.Dashboard = ds;
            dashboardViewer1.ReloadData();

            navigationFrame1.SelectedPageIndex = 2;

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void bbiCerrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridInfoCliente";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiCxC_ItemClick(object sender, ItemClickEventArgs e)
        {
            InformacionCxC();
            botones();
            navigationFrame1.SelectedPageIndex = 3;
        }

        private void InformacionCxC()
        {
            clientesCL cl = new clientesCL();
            cl.intClientesID = cliente;
            string result = cl.clientesVerificaCreditoDisponible();
            if (result != "OK")
            {
                MessageBox.Show("No se pudo verificar el crédito del cliente");
                return;
            }



            globalCL clg = new globalCL();
            

            txtCxCPlazo.Text = cl.intPlazo.ToString();
            txtCxCCreAut.Text = cl.decCreditoAutorizado.ToString("c2");
            txtCxCPSSinfac.Text = cl.decPedidosSurtidosSinFacturar.ToString("c2");           
            txtCxCCxC.Text = cl.decCxC.ToString("c2");


            decimal Disponible = cl.decDisponible;
            txtCxCDisponible.Text = Disponible.ToString("c2");

            decimal vencido = 0;
            gridControlCxC.DataSource = cl.ClientesAntiguedaddesaldos();

            decimal saldo;
            int dias;
            for (int i = 0; i <= gridViewCxC.RowCount - 1; i++)
            {
                dias = Convert.ToInt32(gridViewCxC.GetRowCellValue(i, "DiasVenc"));
                saldo = Convert.ToInt32(gridViewCxC.GetRowCellValue(i, "Importe"));
                if (dias > 0)
                    vencido += saldo;
            }

            txtVencido.Text = vencido.ToString("c2");

            


        }

        private void gridViewCxC_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {


                GridView view = (GridView)sender;
                int dias = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "DiasVenc"));


                if (dias > 0)
                {
                    e.Appearance.ForeColor = Color.Red;

                }

            }
            catch (Exception)
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void dashboardViewer1_ConfigureDataConnection(object sender, DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs e)
        {
            try
            {
                string VisualSoftErpConnectionString = globalCL.gv_strcnn;
                CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(VisualSoftErpConnectionString);

                if (e.ConnectionName == "VisualSoftErpConnectionString")
                {
                    e.ConnectionParameters = connectionParameters;
                }

            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }
    }
}