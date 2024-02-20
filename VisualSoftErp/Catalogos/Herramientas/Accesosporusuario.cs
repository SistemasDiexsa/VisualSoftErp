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

namespace VisualSoftErp.Catalogos.Herramientas
{
    public partial class Accesosporusuario : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intUsuarioID;
        public Accesosporusuario()
        {
            InitializeComponent();


            CargaCombos();
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Accesos";
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
            gridViewPrincipal.ActiveFilter.Clear();

            //las dos lineas siguientes hace que nos aparesca en el grid una columna y un check para seleccionar el renglon
            gridViewPrincipal.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewPrincipal.OptionsSelection.MultiSelect = true;

            
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void LlenarGrid()
        {
            AccesosCL cl = new AccesosCL();
            cl.intUsuariosID = intUsuarioID;
            gridControlPrincipal.DataSource = cl.AccesosGridPrincipal();
            //Global, manda el nombre del grid para la clase Global
            globalCL clg = new globalCL();
            clg.strGridLayout = "AccesosgridPrincipal";
            clg.restoreLayout(gridViewPrincipal);

            for (int i=0;i<=gridViewPrincipal.RowCount-1;i++)
            {
                gridViewPrincipal.FocusedRowHandle = i;
                if (gridViewPrincipal.GetRowCellValue(i, "Favorito").ToString() != "")
                {
                    gridViewPrincipal.SelectRow(i);
                }
            }

            gridViewPrincipal.OptionsView.ShowAutoFilterRow=true;
            gridViewPrincipal.Focus();
            gridViewPrincipal.FocusedRowHandle = 0;

        } //LlenarGrid()

        private void CargaCombos()
        {
            combosCL cl = new combosCL();
            cl.strTabla = "Usuarios";
            cboUsuariosID.Properties.ValueMember = "Clave";
            cboUsuariosID.Properties.DisplayMember = "Des";
            cboUsuariosID.Properties.DataSource = cl.CargaCombos();
            cboUsuariosID.Properties.ForceInitialize();
            cboUsuariosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboUsuariosID.Properties.PopulateColumns();
            cboUsuariosID.Properties.Columns["Clave"].Visible = false;
            cboUsuariosID.Properties.NullText = "Seleccione un usuario";
        }

        private void Guardar()
        {
            try
            {
              
                string sCondicion = String.Empty;
                System.Data.DataTable dtAccesos = new System.Data.DataTable("Accesos");
                dtAccesos.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtAccesos.Columns.Add("ProgramaID", Type.GetType("System.String"));
                dtAccesos.Columns.Add("Favorito", Type.GetType("System.Int32"));
                dtAccesos.Columns.Add("Sololectura", Type.GetType("System.Int32"));


                int intUsuario = intUsuarioID;
                string strProgramaID = string.Empty;
                string strFavorito = string.Empty;
                int intFavorito = 0;
                string strSoloLectura = string.Empty;
                int intSoloLectura = 0;
                string dato = String.Empty;
                int intSeq;
                foreach (int i in gridViewPrincipal.GetSelectedRows())
                {
                  
                        intSeq = i;
                        strProgramaID = gridViewPrincipal.GetRowCellValue(i, "ProgramaID").ToString();
                        strFavorito = gridViewPrincipal.GetRowCellValue(i, "Favorito").ToString();
                        if (strFavorito == "NO") { intFavorito = 0; }
                        else { intFavorito = 1; }
                        strSoloLectura = gridViewPrincipal.GetRowCellValue(i, "Sololectura").ToString();
                        if (strSoloLectura == "NO") { intSoloLectura = 0; }
                        else { intSoloLectura = 1; }

                        dtAccesos.Rows.Add(intUsuario, strProgramaID, intFavorito, intSoloLectura);

                    
                }
                
                AccesosCL cl = new AccesosCL();
                cl.dtm = dtAccesos;
                cl.intUsuariosID = intUsuario;
                cl.strMaquina = Environment.MachineName;
                string  Result = cl.AccesosaProgramasCRUD();
                if (Result == "OK")
                {
                    MessageBox.Show("Guardado Correctamente");
                   
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

        private void cboUsuariosID_EditValueChanged(object sender, EventArgs e)
        {
            if (cboUsuariosID.EditValue == null) { }
            else
            {
                intUsuarioID = Convert.ToInt32(cboUsuariosID.EditValue);
                //LlenarGrid();
            }
        }

        private void bbiSoloLectura_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (int i in gridViewPrincipal.GetSelectedRows())
            {
                gridViewPrincipal.FocusedRowHandle = i;
                gridViewPrincipal.SetFocusedRowCellValue("Sololectura", "SI");
            }
            //for (int i = 0; i <= gridViewPrincipal.RowCount - 1; i++) // SE AGREGO EL <=
            //{
            //    gridViewPrincipal.FocusedRowHandle = i; //se coloca al iniciar el for;

            //    gridViewPrincipal.SetFocusedRowCellValue("Sololectura", "SI");
            //}
        }

        private void bbiEscritura_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (int i in gridViewPrincipal.GetSelectedRows())
            {
                gridViewPrincipal.FocusedRowHandle = i;
                gridViewPrincipal.SetFocusedRowCellValue("Sololectura", "NO");
           
            }
            //for (int i = 0; i <= gridViewPrincipal.RowCount - 1; i++) // SE AGREGO EL <=
            //{
            //    gridViewPrincipal.FocusedRowHandle = i; //se coloca al iniciar el for;

            //    gridViewPrincipal.SetFocusedRowCellValue("Sololectura", "NO");

            //}
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            globalCL clg = new globalCL();
            clg.strGridLayout = "AccesosgridPrincipal";
            String Result = clg.SaveGridLayout(gridViewPrincipal);
            if (Result != "OK")
            {
                MessageBox.Show(Result);
            }
            this.Close();
        }

        private void bbiFavorito_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Favorito").ToString() == "" || gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Favorito").ToString() == "NO")
                gridViewPrincipal.SetFocusedRowCellValue("Favorito", "SI");
            else
                gridViewPrincipal.SetFocusedRowCellValue("Favorito", "NO");
            {

            }
        }

        private void bbiSololecturaRenglon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridViewPrincipal.SetFocusedRowCellValue("Sololectura", "SI");
        }

        private void bbiEscrituraRenglonactual_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridViewPrincipal.SetFocusedRowCellValue("Sololectura", "NO");
        }

        private void bbiNofavorito_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridViewPrincipal.SetFocusedRowCellValue("Favorito", "NO");
        }

        private void bbiCargarAccesos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LlenarGrid();
        }
    }
}