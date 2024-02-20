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
using VisualSoftErp.Operacion.Inventarios.Clases;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace VisualSoftErp.Operacion.Inventarios.Formas
{
    public partial class Inventariofisicoregistrarconteo : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        DateTime Fecha;
        int intNumero;
        int intMarbete;
        int intArticulosID;

        public Inventariofisicoregistrarconteo()
        {
            InitializeComponent();

            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Registro de inventario fisico";

            gridViewDetalle.OptionsView.ShowAutoFilterRow = true;

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }


        private void LlenarGrid()
        {
            InventariofisicoregistrarconteoCL cl = new InventariofisicoregistrarconteoCL();
            gridControlPrincipal.DataSource = cl.InventariofisicoconteoGrid();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridRegistraConteo";
            clg.restoreLayout(gridViewPrincipal);
        } //LlenarGrid()

        private void Editar()
        {
            ribbonPageGroup2.Visible = true;
            ribbonPageGroup1.Visible = false;
            llenaCajas();
            txtFecha.Text = Fecha.ToShortDateString();
            txtNumero.Text = intNumero.ToString();
            txtMarbete.Text = intMarbete.ToString();
            navigationFrame.SelectedPageIndex = 1;            
        }

        private void llenaCajas()
        {
            InventariofisicoregistrarconteoCL cl = new InventariofisicoregistrarconteoCL();
            cl.fFecha = Convert.ToDateTime(Fecha.ToShortDateString());
            cl.intNumero = intNumero;
            cl.intMarbete = intMarbete;
            cl.intArticulosID = intArticulosID;
            gridControlDetalle.DataSource = cl.InventarioFisicoListarpararegistrarconteo();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridRegistraConteoDetalle";
            clg.restoreLayout(gridViewDetalle);
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intNumero == 0)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                Editar();
            }
        }

        private void Guardar()
        {

            try
            {
                string result; 
                

                System.Data.DataTable dtInventariofisico = new System.Data.DataTable("Inventariofisico");
                dtInventariofisico.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtInventariofisico.Columns.Add("Numero", Type.GetType("System.Int32"));
                dtInventariofisico.Columns.Add("Marbete", Type.GetType("System.Int32"));
                dtInventariofisico.Columns.Add("ArticulosID", Type.GetType("System.Int32"));
                dtInventariofisico.Columns.Add("Conteofisico", Type.GetType("System.Decimal"));

               
                int intArticulosID;
                decimal ConteoFisico;
                Fecha = Convert.ToDateTime(Fecha.ToShortDateString());
                intMarbete = Convert.ToInt32(txtMarbete.Text);
                string art=string.Empty;



                for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++) // SE AGREGO EL <=
                {
                    art = gridViewDetalle.GetRowCellValue(i, "Articulo").ToString();
                    if (art.Length > 0)
                    {

                        
                   
                        intArticulosID = Convert.ToInt32(gridViewDetalle.GetRowCellValue(i, "ArticulosID"));
                        ConteoFisico = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(i, "Conteofisico"));


                        dtInventariofisico.Rows.Add(
                            Fecha,
                            intNumero,
                            intMarbete,
                            intArticulosID,
                            ConteoFisico);
                        
                    }

                }


                InventariofisicoregistrarconteoCL cl = new InventariofisicoregistrarconteoCL();             
                cl.dtd = dtInventariofisico;             
                result = cl.InventariofisicoconteoCrud();
                if (result == "OK")
                {
                   
                    MessageBox.Show("Guardado correctamente");
                    Regresar();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }

        }//Guardar

        private void Regresar()
        {
            ribbonPageGroup2.Visible = false;
            ribbonPageGroup1.Visible = true;
            navigationFrame.SelectedPageIndex = 0;
            Fecha = DateTime.Now;
            intNumero = 0;
            intMarbete = 0;
            intArticulosID = 0;

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridRegistraConteoDetalle";
            string result = clg.SaveGridLayout(gridViewDetalle);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
        }


        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Regresar();
        }



        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            globalCL clg = new globalCL();
            clg.strGridLayout = "gridRegistraConteo";
            string result = clg.SaveGridLayout(gridViewPrincipal);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }

            this.Close();
        }

        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            Fecha = Convert.ToDateTime(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Fecha"));
            intNumero = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Numero"));
            intMarbete = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Marbete"));
            intArticulosID = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ArticulosID"));
        }

        private void bbiCargarinv_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            llenaCajas();
        }

        private void bbiActualizar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //decimal dCantidad;
            //for (int i = 0; i <= gridViewDetalle.RowCount - 1; i++) // SE AGREGO EL <=
            //{
            //    gridViewDetalle.FocusedRowHandle = i; //se coloca al iniciar el for

            //    dCantidad = 0;
            //    dCantidad = Convert.ToDecimal(gridViewDetalle.GetRowCellValue(gridViewDetalle.FocusedRowHandle, "Conteo"));

            //    gridViewDetalle.SetFocusedRowCellValue("Conteofisico", dCantidad);

            //}

            //10Jul23 --  creo no estaba hecho este procedimiento
            DialogResult Result = MessageBox.Show("Actualizar existencia del sistema en este inventario generado?", "Actualizar", MessageBoxButtons.YesNo);
            if (Result.ToString() != "Yes")
            {
                return;
            }

            try
            {
                InventariofisicorepCL cl = new InventariofisicorepCL();
                cl.fFecha = Convert.ToDateTime(txtFecha.Text);
                cl.intNumero = Convert.ToInt32(txtNumero.Text);
                cl.intMarbete = Convert.ToInt32(txtMarbete.Text);
                cl.intUsuario = globalCL.gv_UsuarioID;
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
                String result = cl.InventarioFisicoActualizaExistenciadelSistema();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }
    }
}