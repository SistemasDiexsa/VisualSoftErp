using DevExpress.Mvvm.Native;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraNavBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Ventas.Clases;

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class Guias : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Mes;
        int Año;
        int FolioGuias;
        public DataTable detalle;
        public Guias()
        {
            InitializeComponent();
            AgregaAñosNavBar();
            CargarCombos();
            BotonesGrid();
            InitGridViewGuias();
            InitGridViewFacturas();
            navigationFrame.SelectedPageIndex = 0;
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void AgregaAñosNavBar()
        {
            try
            {
                globalCL cl = new globalCL();
                DataTable dt = new DataTable("Años");
                dt.Columns.Add("Año", Type.GetType("System.Int32"));
                cl.strTabla = "Guias";
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

        private void CargarCombos() 
        {
            // COMBO TRANSPORTES
            combosCL cl = new combosCL();
            cl.strTabla = "Transportes";
            cboTransportes.Properties.DataSource = cl.CargaCombos();
            cboTransportes.Properties.ValueMember = "Clave";
            cboTransportes.Properties.DisplayMember = "Des";
            cboTransportes.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboTransportes.Properties.ForceInitialize();
            cboTransportes.Properties.PopulateColumns();
            cboTransportes.Properties.Columns["Clave"].Visible = false;
            cboTransportes.Properties.NullText = "Seleccione un transportista";
        }

        private void InitGridViewGuias()
        {
            gridViewGuias.ViewCaption = "Guias";
            gridViewGuias.OptionsView.ShowViewCaption = true;
            gridViewGuias.OptionsBehavior.ReadOnly = true;
            gridViewGuias.OptionsBehavior.Editable = false;
            Mes = 0;
            Año = DateTime.Now.Year;
            LlenarGridGuias(Mes, Año);
        }

        public void LlenarGridGuias(int Mes, int Año)
        {
            GuiasCL cl = new GuiasCL();
            cl.intMes = Mes;
            cl.intAño = Año;
            gridControlGuias.DataSource = cl.GuiasGrid();
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridGuias";
            clg.restoreLayout(gridViewGuias);
            gridViewGuias.OptionsView.ShowAutoFilterRow = true;
            gridViewGuias.ViewCaption = Mes != 0 ? "GUIAS DE " + clg.NombreDeMes(Mes, 0) + " DEL " + Año.ToString() : "GUIAS DE TODO EL " + Año.ToString();
        }

        private void InitGridViewFacturas()
        {
            gridViewFacturas.ViewCaption = "Facturas";
            gridViewFacturas.OptionsView.ShowViewCaption = true;
            gridViewFacturas.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewFacturas.OptionsSelection.MultiSelect = true;
            gridViewFacturas.OptionsBehavior.Editable = true;
            gridViewFacturas.Columns["SerieFactura"].OptionsColumn.ReadOnly = true;
            gridViewFacturas.Columns["FolioFactura"].OptionsColumn.ReadOnly = true;
            gridViewFacturas.Columns["Cliente"].OptionsColumn.ReadOnly = true;
            LlenarGridFacturas();
        }

        public void LlenarGridFacturas()
        {
            GuiasCL cl = new GuiasCL();
            detalle = cl.FacturasRecientesGrid();
            detalle.Columns.Add("Descripcion", typeof(string));
            gridControlFacturas.DataSource = detalle;
            gridViewFacturas.OptionsView.ShowAutoFilterRow = true;
            gridViewFacturas.ViewCaption = "Facturas Recientes";
        }

        private void LimpiaCajas()
        {
            txtNumeroGuia.Text = string.Empty;
            dateFecha.DateTime = DateTime.Now;
            cboTransportes.EditValue = null;
            txtSubtotal.Text = string.Empty;
            txtIva.Text = string.Empty;
            txtRetIva.Text= string.Empty;
            txtTotal.Text = string.Empty;
            detalle = new DataTable();
            gridViewFacturas.ClearSelection();
            //LlenarGridFacturas();
        }

        private void BotonesEditar()
        {
            ribbonPageGroup2.Visible = false;
            bbiEditar.Visibility = BarItemVisibility.Never;
            bbiNuevo.Visibility = BarItemVisibility.Never;
            bbiEliminar.Visibility = BarItemVisibility.Never;
            bbiCerrar.Visibility = BarItemVisibility.Never;
            bbiGuardar.Visibility = BarItemVisibility.Always;
            bbiRegresar.Visibility = BarItemVisibility.Always;
        }

        private void BotonesGrid()
        {
            ribbonPageGroup2.Visible = true;
            bbiEditar.Visibility = BarItemVisibility.Always;
            bbiNuevo.Visibility = BarItemVisibility.Always;
            bbiEliminar.Visibility = BarItemVisibility.Always;
            bbiCerrar.Visibility = BarItemVisibility.Always;
            bbiGuardar.Visibility = BarItemVisibility.Never;
            bbiRegresar.Visibility = BarItemVisibility.Never;
        }

        private void SiguienteFolioGuias()
        {
            GuiasCL cl = new GuiasCL();
            cl.strSerieFactura = "";
            cl.strDoc = "Guias";
            string result = cl.DocumentosSiguienteID();
            if (result == "OK")
            {
                FolioGuias = cl.intFolio;
                lblFolio.Text = "Folio " + FolioGuias.ToString();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void bbiNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            BotonesEditar();
            LimpiaCajas();
            LlenarGridFacturas();
            SiguienteFolioGuias();
            navigationFrame.SelectedPageIndex = 1;
        }

        private void bbiRegresar_ItemClick(object sender, ItemClickEventArgs e)
        {
            navigationFrame.SelectedPageIndex = 0;
            LimpiaCajas();
            BotonesGrid();
            LlenarGridGuias(Mes, Año);
        }

        private void navBarControl_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            globalCL clg = new globalCL();
            string Name = e.Link.ItemName.ToString();
            if (clg.esNumerico(Name))
            {
                Año = Convert.ToInt32(Name);
                LlenarGridGuias(Mes, Año);
            }
            else
            {
                switch (Name)
                {
                    case "navBarItemEne":
                        Mes = 1;
                        break;
                    case "navBarItemFeb":
                        Mes = 2;
                        break;
                    case "navBarItemMar":
                        Mes = 3;
                        break;
                    case "navBarItemAbr":
                        Mes = 4;
                        break;
                    case "navBarItemMay":
                        Mes = 5;
                        break;
                    case "navBarItemJun":
                        Mes = 6;
                        break;
                    case "navBarItemJul":
                        Mes = 7;
                        break;
                    case "navBarItemAgo":
                        Mes = 8;
                        break;
                    case "navBarItemSep":
                        Mes = 9;
                        break;
                    case "navBarItemOct":
                        Mes = 10;
                        break;
                    case "navBarItemNov":
                        Mes = 11;
                        break;
                    case "navBarItemDic":
                        Mes = 12;
                        break;
                    case "navBarItemTodos":
                        Mes = 0;
                        break;
                }
                LlenarGridGuias(Mes, Año);
            }
        }

        private string Valida()
        {
            try
            {
                globalCL clg = new globalCL();
                string message = "OK";
                // NumeroGuia
                if (txtNumeroGuia.Text.Length == 0)
                    message = "Favor de introducir el Número de Guía";

                // Fecha;
                if (dateFecha.Text.Length == 0)
                    message = "Favor de introducir una Fecha válida";

                // Transportes
                if (Convert.ToInt32(cboTransportes.EditValue) == 0)
                    message = "Favor de seleccionar un Trasnporte";

                // Subtotal;
                if (!clg.esNumerico(txtSubtotal.Text))
                    message = "Favor de introducir un Subtotal válido";

                // Iva;
                if (!clg.esNumerico(txtIva.Text))
                    message = "Favor de introducir un Iva válido";

                // RetIva;
                if (!clg.esNumerico(txtRetIva.Text))
                    message = "Favor de introducir un RetIva válido";

                // Total;
                if (!clg.esNumerico(txtTotal.Text))
                    message = "Favor de introducir un Total válido";

                // Facturas;
                if (gridViewFacturas.SelectedRowsCount <= 0)
                    message = "Favor de seleccionar al menos una factura";

                // Descripcion;
                int[] selectedRowHandles = gridViewFacturas.GetSelectedRows();
                foreach (int rowHandle in selectedRowHandles)
                {
                    DataRow row = gridViewFacturas.GetDataRow(rowHandle);
                    if (row != null)
                    {
                        string factura = row["FolioFactura"].ToString();
                        string descripcion = row["Descripcion"].ToString();
                        if (descripcion.Length == 0)
                            message = "Favor de escribir la descripcíón de la factura " + factura;
                    }
                }
                
                return message;
            }
            catch (Exception ex)
            {
                string line = ex.LineNumber().ToString();
                return "Error en línea " + line + "\n" + ex.Message;
            }
        }
        private void bbiGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                // VALIDAMOS QUE LA INFORMACIÓN ESTÉ COMPLETA. REGRESA 'OK' O EL ERROR
                string valida = Valida();
                if(valida != "OK")
                {
                    MessageBox.Show(valida);
                    return;
                }

                // CREAMOS DT PARA SP (GUIAS)
                DataTable dtGuias = new DataTable("Guias");
                dtGuias.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtGuias.Columns.Add("Fecha", Type.GetType("System.DateTime"));
                dtGuias.Columns.Add("TransportesID", Type.GetType("System.Int32"));
                dtGuias.Columns.Add("Subtotal", Type.GetType("System.Decimal"));
                dtGuias.Columns.Add("Iva", Type.GetType("System.Decimal"));
                dtGuias.Columns.Add("RetIva", Type.GetType("System.Decimal"));
                dtGuias.Columns.Add("Total", Type.GetType("System.Decimal"));
                dtGuias.Columns.Add("UsuariosID", Type.GetType("System.Int32"));
                dtGuias.Columns.Add("FechaReal", Type.GetType("System.DateTime"));
                dtGuias.Columns.Add("Numerodeguia", Type.GetType("System.String"));

                // CREAMOS DT PARA SP (FACTURAS)
                DataTable dtGuiasDetalle = new DataTable("GuiasDetalle");
                dtGuiasDetalle.Columns.Add("Folio", Type.GetType("System.Int32"));
                dtGuiasDetalle.Columns.Add("SerieFactura", Type.GetType("System.String"));
                dtGuiasDetalle.Columns.Add("Factura", Type.GetType("System.Int32"));
                dtGuiasDetalle.Columns.Add("Descripcion", Type.GetType("System.String"));

                // AGREGAMOS LA INFORMACIÓN AL DT CREADO REFERENTE A LA GUÍA
                dtGuias.Rows.Add(
                    FolioGuias, 
                    Convert.ToDateTime(dateFecha.Text), 
                    Convert.ToInt32(cboTransportes.EditValue), 
                    Convert.ToDecimal(txtSubtotal.Text),
                    Convert.ToDecimal(txtIva.Text),
                    Convert.ToDecimal(txtRetIva.Text),
                    Convert.ToDecimal(txtTotal.Text),
                    globalCL.gv_UsuarioID,
                    Convert.ToDateTime(DateTime.Now),
                    txtNumeroGuia.Text
                );

                // AGREGAMOS LA INFORMACION AL DT CREADO REFERENTE A LAS FACTURAS
                int[] selectedRowHandles = gridViewFacturas.GetSelectedRows();
                foreach (int rowHandle in selectedRowHandles)
                {
                    DataRow row = gridViewFacturas.GetDataRow(rowHandle);
                    if (row != null)
                    {
                        dtGuiasDetalle.Rows.Add(
                            FolioGuias,
                            row["SerieFactura"].ToString(),
                            Convert.ToInt32(row["FolioFactura"]),
                            row["Descripcion"].ToString()
                        );
                    }
                }

                GuiasCL cl = new GuiasCL();
                cl.intFolio = FolioGuias;
                cl.dtGuias = dtGuias;
                cl.dtGuiasDetalle = dtGuiasDetalle;
                string result = cl.GuiasCrud();
                if (result == "OK")
                {
                    MessageBox.Show("Guardado Exitosamente!");
                    LimpiaCajas();
                    SiguienteFolioGuias();
                }
                else
                    MessageBox.Show(result);
             }
            catch (Exception ex)
            {
                int line = ex.LineNumber();
                MessageBox.Show("Error en linea: " + line.ToString() + "\n" + ex.Message);
            }
        }

        private void bbiCerrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gridViewGuias_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            FolioGuias = Convert.ToInt32(gridViewGuias.GetRowCellValue(gridViewGuias.FocusedRowHandle, "Folio Guía"));
        }

        private void bbiEditar_ItemClick(object sender, ItemClickEventArgs e)
        {
            BotonesEditar();
            LlenaCajas();
            LlenaCajasDetalle();
            navigationFrame.SelectedPageIndex = 1;
        }

        private void LlenaCajas()
        {
            try
            {
                GuiasCL cl = new GuiasCL();
                cl.intFolio = FolioGuias;
                string result = cl.LlenaCajas();
                if(result == "OK")
                {
                    txtNumeroGuia.Text = cl.intNumGuia.ToString();
                    dateFecha.Text = cl.dateFecha.ToShortDateString();
                    cboTransportes.EditValue = cl.intTransportesID;
                    txtSubtotal.Text = cl.decSubtotal.ToString();
                    txtIva.Text = cl.decIva.ToString();
                    txtRetIva.Text = cl.decRetIva.ToString();
                    txtTotal.Text = cl.decTotal.ToString();
                    lblFolio.Text = "Folio " + FolioGuias.ToString();
                }
            }
            catch (Exception ex)
            {
                string linea = ex.LineNumber().ToString();
                MessageBox.Show("Error en linea: " + linea + "\n" +  ex.Message);
            }
        }

        private void LlenaCajasDetalle()
        {
            try
            {
                GuiasCL cl = new GuiasCL();
                cl.intFolio = FolioGuias;
                DataTable dt = cl.LlenaCajasDetalle();
                bool facturaEncontrada = false;

                for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
                {
                    DataRow dr = dt.Rows[rowIndex];
                    int factura = Convert.ToInt32(dr["FolioFactura"]);
                    string descripcion = dr["Descripcion"].ToString();
                    facturaEncontrada = false;
                    for(int i = 0; i < gridViewFacturas.RowCount; i++)
                    {
                        DataRow facturaGrid = gridViewFacturas.GetDataRow(i);
                        if (factura == Convert.ToInt32(facturaGrid["FolioFactura"]))
                        {
                            gridViewFacturas.SelectRow(i);
                            int row = gridViewFacturas.LocateByValue("FolioFactura", factura);
                            gridViewFacturas.SetRowCellValue(row, "Descripcion", descripcion);
                            facturaEncontrada = true;
                            break;
                        }
                    }

                    if(!facturaEncontrada)
                    {
                        detalle.Rows.Add(dr.ItemArray);
                        int newRow = gridViewFacturas.LocateByValue("FolioFactura", factura);
                        gridViewFacturas.SelectRow(newRow);
                    }
                }
            }
            catch (Exception ex)
            {
                string linea = ex.LineNumber().ToString();
                MessageBox.Show("Error en linea: " + linea + "\n" + ex.Message);
            }
        }

        private void bbiEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                DialogResult resultado = MessageBox.Show("¿Estás seguro de eliminar?", "Confirmar Eliminación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (resultado == DialogResult.OK)
                {
                    GuiasCL cl = new GuiasCL();
                    cl.intFolio = FolioGuias;
                    string result = cl.Eliminar();
                    if (result == "OK")
                    {
                        LlenarGridGuias(Mes, Año);
                        MessageBox.Show("Eliminado Exitosamente!");
                    }
                    else
                        MessageBox.Show(result);
                }
            }
            catch(Exception ex)
            {
                string linea = ex.LineNumber().ToString();
                MessageBox.Show("Error en linea: " + linea + "\n" + ex.Message);
            }
        }
    }
}