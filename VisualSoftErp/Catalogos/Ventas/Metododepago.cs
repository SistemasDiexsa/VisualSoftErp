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

namespace VisualSoftErp.Catalogos
{
    public partial class Metododepago : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string strMetododepagoID;
        int intdo;
        public Metododepago()
        {
            InitializeComponent();

            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowViewCaption = true;
            gridView1.ViewCaption = "Catálogo de Metodo de pago";

            LlenarGrid();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        }

        private void LlenarGrid()
        {
            c_metodopagoCL cl = new c_metodopagoCL();
            grdMetodoP.DataSource = cl.c_metodopagoGrid();
        
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridmetododepago";
            clg.restoreLayout(gridView1);

        }//LlenarGrid()

        private void Nuevo()
        {
            BotonesEdicion();
            strMetododepagoID = "";

        }//Nuevo()

        private void LimpiaCajas()
        {
            txtClave.Text = string.Empty;
            txtDesc.Text = string.Empty;
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

            navigationFrame.SelectedPageIndex = 1;

        }//BotonesEdicion()

        private void Guardar()
        {
            try
            {
                string result = Valida();

                if (result != "OK")
                {
                    MessageBox.Show(result);
                    return;
                }

                c_metodopagoCL cl = new c_metodopagoCL();
                cl.strClave = txtClave.Text;
                cl.strDes = txtDesc.Text;
                cl.intdo = intdo;
                result = cl.c_MetodoPagoCrud();

                if (result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                    if (strMetododepagoID == "")
                    {
                        LimpiaCajas();
                    }
                }
                else if (result == "EXISTS")
                {
                    MessageBox.Show("la clave existe en otro metodo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        }//Guardar

        private string Valida()
        {
            if (txtClave.Text == "")
            {
                return "la Clave no puede ir vacio";
            }
            if (txtDesc.Text == "")
            {
                return "la Descripción no puede ir vacio";
            }

            return "OK";
        }//Valida()

        private void Editar()
        {
            BotonesEdicion();
            llenaCajas();

        }//Editar()

        private void llenaCajas()
        {
            c_metodopagoCL cl = new c_metodopagoCL();
            cl.strClave = strMetododepagoID;
            string result = cl.c_metodopagoLlenaCajas();
            if (result == "OK")
            {
                txtClave.Text = cl.strClave;
                txtDesc.Text = cl.strDes;
            }

            else
            {
                MessageBox.Show(result);
            }
        }//llenaCajas()

        private void Eliminar()
        {
            c_metodopagoCL cl = new c_metodopagoCL();
            cl.strClave = strMetododepagoID;
            string result = cl.c_metodopagoEliminar();

            if (result == "OK")
            {
                MessageBox.Show("Eliminado correctamente");
                LlenarGrid();
            }
            else
            {
                MessageBox.Show(result);
            }
        }//Eliminar

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intdo = 1;
            Nuevo();
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            Guardar();
        }

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intdo = 2;
            if (strMetododepagoID == null)
             {
                 MessageBox.Show("Selecciona un renglón");
             }
             else
             {
                 Editar();
             }
        }

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (strMetododepagoID == null)
            {
                MessageBox.Show("Selecciona un renglón");
            }
            else
            {
                DialogResult result = MessageBox.Show("Desea eliminar la línea " + strMetododepagoID.ToString(), "Elimnar", MessageBoxButtons.YesNo);
                if (result.ToString() == "Yes")
                {
                    Eliminar();
                }
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            strMetododepagoID = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Clave"));
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            LlenarGrid();
            navigationFrame.SelectedPageIndex = 0;
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridMetododepago" +
                "";
            string result = clg.SaveGridLayout(gridView1);
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close();
        }

        private void bbiVista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grdMetodoP.ShowRibbonPrintPreview();
        }
    }
}