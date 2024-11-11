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
using DevExpress.XtraEditors.Popup;
using DevExpress.Pdf.Native;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace VisualSoftErp.Catalogos.Herramientas
{
    public partial class Accesosporusuario : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intUsuarioID;
        int intProgramasID;
        bool permisosEscritura;

        public Accesosporusuario()
        {
            InitializeComponent();
            PermisosEscritura();
            CargaCombos();
            InitGridPrincipal();
            InitGridProgramas();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }


        #region INTERACCIONES DE USUARIO
        private void cboUsuariosID_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit cboUsuariosID = (LookUpEdit)sender;
            if (cboUsuariosID != null)
            {
                if (cboUsuariosID.IsPopupOpen) return;
                if (cboUsuariosID.EditValue != null)
                {
                    intUsuarioID = Convert.ToInt32(cboUsuariosID.EditValue);
                    LlenarGrid();
                }

            }
        }

        #endregion INTERACCIONES DE USUARIO

        private void PermisosEscritura()
        {
            globalCL clg = new globalCL();
            clg.strPrograma = "0449";
            if (clg.accesoSoloLectura())
            {
                bbiGuardar.Enabled = false;
                permisosEscritura = false;
            }
            else
            {
                bbiGuardar.Enabled = true;
                permisosEscritura = true;
            }
        }

        private void InitGridPrincipal()
        {
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
            gridViewPrincipal.ActiveFilter.Clear();
            gridViewPrincipal.ViewCaption = "Programas";
            gridViewPrincipal.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewPrincipal.OptionsSelection.MultiSelect = true;
        }

        private void InitGridProgramas()
        {
            gridViewProgramas.OptionsBehavior.ReadOnly = true;
            gridViewProgramas.OptionsBehavior.Editable = false;
            gridViewProgramas.OptionsView.ShowViewCaption = true;
            gridViewProgramas.OptionsView.ShowAutoFilterRow = true;
            gridViewProgramas.ActiveFilter.Clear();
            gridViewProgramas.ViewCaption = "Usuarios";
            gridViewProgramas.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewProgramas.OptionsSelection.MultiSelect = true;
        }

        private void LlenarGrid()
        {
            AccesosCL cl = new AccesosCL();
            globalCL clg = new globalCL();
            if (navigationFrame.SelectedPage == NavigationPageUsuarios)
            {
                
                cl.intUsuariosID = intUsuarioID;
                gridControlPrincipal.DataSource = cl.AccesosGridPorusuario();
                gridViewPrincipal.ViewCaption = "Programas a los que accede " + cboUsuariosID.Text;
                clg.strGridLayout = "AccesosgridPrincipal";
                clg.restoreLayout(gridViewPrincipal);

                for (int i = 0; i <= gridViewPrincipal.RowCount - 1; i++)
                {
                    gridViewPrincipal.FocusedRowHandle = i;
                    if (gridViewPrincipal.GetRowCellValue(i, "Favorito").ToString() != "")
                        gridViewPrincipal.SelectRow(i);
                }

                gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
                gridViewPrincipal.Focus();
                gridViewPrincipal.FocusedRowHandle = 0;
            }
            else if (navigationFrame.SelectedPage == NavigationPageProgramas)
            {
                string programa = cboProgramas.Text;
                cl.intProgramaID = intProgramasID;
                gridControlProgramas.DataSource = cl.AccesosGridPorPrograma();
                gridViewProgramas.ViewCaption = "Usuarios con Acceso a " + programa;

                for (int i = 0; i <= gridViewProgramas.RowCount -1; i++)
                {
                    programa = gridViewProgramas.GetRowCellValue(i, "Programa").ToString();
                    gridViewProgramas.FocusedRowHandle = i;

                    if (programa != null && programa != string.Empty)
                        gridViewProgramas.SelectRow(i);
                }

                gridViewProgramas.Columns["Programa"].Visible = false;
                gridViewProgramas.Columns["UsuariosID"].Visible = false;
                gridViewProgramas.OptionsView.ShowAutoFilterRow = true;
                gridViewProgramas.Focus();
                gridViewProgramas.FocusedRowHandle = 0;
            }

            

        }

        private void CargaCombos()
        {
            combosCL cl = new combosCL();

            #region COMBO USUARIOS
            cl.strTabla = "Usuarios";
            cboUsuariosID.Properties.ValueMember = "Clave";
            cboUsuariosID.Properties.DisplayMember = "Des";
            cboUsuariosID.Properties.DataSource = cl.CargaCombos();
            cboUsuariosID.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboUsuariosID.Properties.ForceInitialize();
            cboUsuariosID.Properties.PopulateColumns();
            cboUsuariosID.Properties.Columns["Clave"].Visible = false;
            cboUsuariosID.Properties.NullText = "Seleccione un usuario";
            cboUsuariosID.EditValue = null;
            #endregion COMBO USUARIOS

            #region COMBO PROGRAMAS
            cl.strTabla = "Programas";
            cboProgramas.Properties.ValueMember = "Clave";
            cboProgramas.Properties.DisplayMember = "Des";
            cboProgramas.Properties.DataSource = cl.CargaCombos();
            cboProgramas.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboProgramas.Properties.ForceInitialize();
            cboProgramas.Properties.PopulateColumns();
            cboProgramas.Properties.Columns["Clave"].Visible = false;
            cboProgramas.Properties.NullText = "Seleccione un programa";
            cboProgramas.EditValue = null;
            #endregion COMBO PROGRAMAS
        }

        private void Guardar()
        {
            if (!permisosEscritura) return;
            try
            {
                AccesosCL cl = new AccesosCL();
                string sCondicion = String.Empty;
                System.Data.DataTable dtAccesos = new System.Data.DataTable("Accesos");
                dtAccesos.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtAccesos.Columns.Add("ProgramaID", Type.GetType("System.String"));
                dtAccesos.Columns.Add("Favorito", Type.GetType("System.Int32"));
                dtAccesos.Columns.Add("Sololectura", Type.GetType("System.Int32"));
                int intUsuario = 0;
                string strProgramaID = string.Empty;
                string strFavorito = string.Empty;
                int intFavorito = 0;
                string strSoloLectura = string.Empty;
                int intSoloLectura = 0;
                
                if (navigationFrame.SelectedPage == NavigationPageUsuarios)
                {
                    intUsuario = intUsuarioID;
                    strProgramaID = string.Empty;
                    strFavorito = string.Empty;
                    intFavorito = 0;
                    strSoloLectura = string.Empty;
                    intSoloLectura = 0;
                

                    foreach (int i in gridViewPrincipal.GetSelectedRows())
                    {
                        strProgramaID = gridViewPrincipal.GetRowCellValue(i, "ProgramaID").ToString();
                        strFavorito = gridViewPrincipal.GetRowCellValue(i, "Favorito").ToString();
                        strSoloLectura = gridViewPrincipal.GetRowCellValue(i, "Sololectura").ToString();
                        
                        if (strFavorito == "NO") 
                            intFavorito = 0;
                        else
                            intFavorito = 1;
                        
                        if (strSoloLectura == "NO")
                            intSoloLectura = 0;
                        else 
                            intSoloLectura = 1;

                        dtAccesos.Rows.Add(intUsuario, strProgramaID, intFavorito, intSoloLectura);
                    }

                    
                    cl.dtm = dtAccesos;
                    cl.intUsuariosID = intUsuario;
                    cl.strMaquina = Environment.MachineName;
                    string Result = cl.AccesosaProgramasCRUD();
                    if (Result == "OK")
                    {
                        MessageBox.Show("Guardado Correctamente");
                    }
                    else
                    {
                        MessageBox.Show(Result);
                    }
                }
                else if (navigationFrame.SelectedPage == NavigationPageProgramas)
                {
                    strProgramaID = cboProgramas.EditValue.ToString();
                    foreach (int i in gridViewProgramas.GetSelectedRows())
                    {
                        strFavorito = gridViewProgramas.GetRowCellValue(i, "Favorito").ToString();
                        strSoloLectura = gridViewProgramas.GetRowCellValue(i, "Solo Lectura").ToString();

                        intUsuario = Convert.ToInt32(gridViewProgramas.GetRowCellValue(i, "UsuariosID"));
                        intFavorito = strFavorito == "SI" ? 1 : 0;
                        intSoloLectura = strSoloLectura == "SI" ? 1 : 0;

                        dtAccesos.Rows.Add(intUsuario, strProgramaID, intFavorito, intSoloLectura);
                    }

                    cl.dtm = dtAccesos;
                    cl.intProgramaID = Convert.ToInt32(strProgramaID);
                    cl.strMaquina = Environment.MachineName;
                    string result = cl.AccesosCrudPorPrograma();
                    if (result == "OK")
                        MessageBox.Show("Guardado Correctamente");
                    else
                        MessageBox.Show(result);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Guardar: " + ex.Message);
            }
        }

        private void cboUsuariosID_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            LookUpEdit cboUsuariosID = (LookUpEdit)sender;
            if(cboUsuariosID != null)
            {
                if(cboUsuariosID.EditValue != null)
                {
                    intUsuarioID = Convert.ToInt32(cboUsuariosID.EditValue);
                    LlenarGrid();
                }
            }
        }


        private void cboProgramas_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            LookUpEdit cboProgramas = (LookUpEdit)sender;
            if(cboProgramas != null)
            {
                if (cboProgramas.EditValue != null)
                {
                    intProgramasID = Convert.ToInt32(cboProgramas.EditValue);
                    LlenarGrid();
                }
            }
        }

        private void cboProgramas_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit cboProgramas = (LookUpEdit)sender;
            if(cboProgramas != null)
            {
                if (cboProgramas.IsPopupOpen) return;
                if (cboProgramas.EditValue != null)
                {
                    intProgramasID = Convert.ToInt32(cboProgramas.EditValue);
                    LlenarGrid();
                }
            }
        }

        private void bbiSoloLectura_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (int i in gridViewPrincipal.GetSelectedRows())
            {
                gridViewPrincipal.FocusedRowHandle = i;
                gridViewPrincipal.SetFocusedRowCellValue("Sololectura", "SI");
            }
        }

        private void bbiEscritura_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (int i in gridViewPrincipal.GetSelectedRows())
            {
                gridViewPrincipal.FocusedRowHandle = i;
                gridViewPrincipal.SetFocusedRowCellValue("Sololectura", "NO");
           
            }
        }

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            if (navigationFrame.SelectedPage == NavigationPageUsuarios)
                gridViewPrincipal.SetFocusedRowCellValue("Favorito", "SI");
            else if (navigationFrame.SelectedPage == NavigationPageProgramas)
                gridViewProgramas.SetFocusedRowCellValue("Favorito", "SI");
        }

        private void bbiSololecturaRenglon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (navigationFrame.SelectedPage == NavigationPageUsuarios)
                gridViewPrincipal.SetFocusedRowCellValue("Sololectura", "SI");
            else if (navigationFrame.SelectedPage == NavigationPageProgramas)
                gridViewProgramas.SetFocusedRowCellValue("Solo Lectura", "SI");
        }

        private void bbiEscrituraRenglonactual_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (navigationFrame.SelectedPage == NavigationPageUsuarios)
                gridViewPrincipal.SetFocusedRowCellValue("Sololectura", "NO");
            else if (navigationFrame.SelectedPage == NavigationPageProgramas)
                gridViewProgramas.SetFocusedRowCellValue("Solo Lectura", "NO");
        }

        private void bbiNofavorito_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (navigationFrame.SelectedPage == NavigationPageUsuarios)
                gridViewPrincipal.SetFocusedRowCellValue("Favorito", "NO");
            else if (navigationFrame.SelectedPage == NavigationPageProgramas)
                gridViewProgramas.SetFocusedRowCellValue("Favorito", "NO");
        }

        private void bbiCargarAccesos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LlenarGrid();
        }

        private void navBarControl1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NavBarItemLink item = e.Link;
            if(item != null)
            {
                switch(item.ItemName)
                {
                    case "navBarItemAccesosUsuario":
                        intProgramasID = 0;
                        intUsuarioID = 0;
                        cboProgramas.EditValue = null;
                        cboUsuariosID.EditValue = null;
                        gridControlPrincipal.DataSource = null;
                        gridControlProgramas.DataSource = null;
                        gridViewProgramas.ViewCaption = "Programas";
                        gridViewProgramas.ViewCaption = "Usuarios";
                        bbiEscritura.Enabled = true;
                        bbiSoloLectura.Enabled = true;
                        navigationFrame.SelectedPage = NavigationPageUsuarios;
                        break;
                    case "navBarItemAccesosPrograma":
                        intProgramasID = 0;
                        intUsuarioID = 0;
                        cboProgramas.EditValue = null;
                        cboUsuariosID.EditValue = null;
                        gridControlPrincipal.DataSource = null;
                        gridControlProgramas.DataSource = null;
                        gridViewProgramas.ViewCaption = "Usuarios";
                        gridViewProgramas.ViewCaption = "Programas";
                        bbiEscritura.Enabled = false;
                        bbiSoloLectura.Enabled = false;
                        navigationFrame.SelectedPage = NavigationPageProgramas;
                        break;
                }
            }
        }

    }
}