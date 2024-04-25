using DevExpress.Charts.Native;
using DevExpress.Mvvm.Native;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraNavBar;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.Layout.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualSoftErp.Catalogos.CXC;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Compras.Clases;
using VisualSoftErp.Operacion.CxP.Informes;
using VisualSoftErp.Operacion.Ventas.Clases;
using VisualSoftErp.Operacion.Ventas.Designers;

namespace VisualSoftErp.Operacion.Ventas.Formas
{
    public partial class Guias : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int Mes;
        int Año;
        int FolioGuias;
        public DataTable detalle;
        public DataTable correos;
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
            gridViewFacturas.Columns["Email"].Visible = false;
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
            txtPorcentajeIva.Text = string.Empty;
            txtIva.Text = string.Empty;
            txtPorcentajeRetIva.Text = string.Empty;
            txtRetIva.Text = string.Empty;
            txtTotal.Text = string.Empty;
            detalle = new DataTable();
            gridViewFacturas.ClearSelection();
            //LlenarGridFacturas();
        }

        private void BotonesNuevo()
        {
            ribbonPageGroup2.Visible = false;
            bbiEditar.Visibility = BarItemVisibility.Never;
            bbiNuevo.Visibility = BarItemVisibility.Never;
            bbiEliminar.Visibility = BarItemVisibility.Never;
            bbiCerrar.Visibility = BarItemVisibility.Never;
            bbiGuardar.Visibility = BarItemVisibility.Always;
            bbiRegresar.Visibility = BarItemVisibility.Always;
        }

        private void BotonesEditar()
        {
            ribbonPageGroup2.Visible = false;
            bbiEditar.Visibility = BarItemVisibility.Never;
            bbiNuevo.Visibility = BarItemVisibility.Never;
            bbiEliminar.Visibility = BarItemVisibility.Never;
            bbiCerrar.Visibility = BarItemVisibility.Never;
            bbiGuardar.Visibility = BarItemVisibility.Always;
            bbiEnviarXCorreo.Visibility = BarItemVisibility.Always;
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
            bbiEnviarXCorreo.Visibility = BarItemVisibility.Never;
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
            BotonesNuevo();
            LimpiaCajas();
            LlenarGridFacturas();
            SiguienteFolioGuias();
            navigationFrame.SelectedPageIndex = 1;
        }

        private void bbiRegresar_ItemClick(object sender, ItemClickEventArgs e)
        {
            popUpEnviarXCorreo.Hide();
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
                    // AQUÍ DEBE DE CONSIDERARSE QUE SE ENVIARÁ POR CORREO.
                    DialogResult resultado = MessageBox.Show("Guardado Correctamente! \n¿Deseas enviar por correo?", "Confirmación", MessageBoxButtons.YesNo);
                    if (resultado == DialogResult.Yes)
                        EnviarDocXCorreo();

                    LimpiaCajas();
                    LlenarGridFacturas();
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
            if(FolioGuias == 0)
            {
                MessageBox.Show("Favor de escoger una guía para editar.");
                return;
            }
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

        private void EnviarDocXCorreo()
        {
            correos = new DataTable();
            correos.Columns.Add("Cliente", typeof(string));
            correos.Columns.Add("Correo", typeof(string));
            correos.Columns.Add("Factura", typeof(string));
            
            try
            {                
                // AGREGAMOS LA INFORMACION AL PANEL CREADO REFERENTE A LOS CORREOS
                int[] selectedRowHandles = gridViewFacturas.GetSelectedRows();
                foreach (int rowHandle in selectedRowHandles)
                {
                    DataRow row = gridViewFacturas.GetDataRow(rowHandle);
                    DataRow cliente = correos.NewRow();
                    if (row != null)
                    {
                        Label lb = new Label();
                        lb.Text = "Cliente: " + row["Cliente"].ToString() + " Factura: " + row["FolioFactura"].ToString();
                        lb.AutoSize = true;
                        flowLayoutPanelCorreos.Controls.Add(lb);

                        TextBox textBox = new TextBox();
                        textBox.Text = row["Email"].ToString();
                        textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                        textBox.Margin = new Padding(0, 0, 0, 5);
                        flowLayoutPanelCorreos.Controls.Add(textBox);

                        cliente["Cliente"] = row["Cliente"].ToString();
                        cliente["Correo"] = row["Email"].ToString();
                        cliente["Factura"] = row["FolioFactura"].ToString();
                        correos.Rows.Add(cliente);
                    }
                }

                int x = (globalCL.gv_intAnchoVentana - popUpEnviarXCorreo.Width) / 2;
                int y = (globalCL.gv_intAltoVentana - popUpEnviarXCorreo.Height - 347) / 2;
                popUpEnviarXCorreo.Location = new Point(x, y);
                popUpEnviarXCorreo.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reporte: " + ex.Message);
            }
        }

        private void bbiEnviarXCorreo_ItemClick(object sender, ItemClickEventArgs e)
        {
            EnviarDocXCorreo();
        }

        private void bbiRegresarRibbonImpresion_ItemClick(object sender, ItemClickEventArgs e)
        {
            popUpEnviarXCorreo.Hide();
            navigationFrame.SelectedPageIndex = 1;
            ribbonControl.MergeOwner.SelectedPage = ribbonControl.MergeOwner.TotalPageCategory.GetPageByText(ribbonHome.Text);
        }

        private void bbiCancelarEnvios_Click(object sender, EventArgs e)
        {
            flowLayoutPanelCorreos.Controls.Clear();
            popUpEnviarXCorreo.Hide();
        }

        private void bbiConfirmarEnvios_Click(object sender, EventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm();
            try
            {
                string result = string.Empty;
                string body = string.Empty;
                string subject = string.Empty;
                int rowIndex = 0;
                
                foreach (Control control in flowLayoutPanelCorreos.Controls)
                {
                    globalCL cl = new globalCL();
                    if (control is TextBox)
                    {
                        // Obtener el TextBox y su valor actualizado
                        TextBox textBox = (TextBox)control;
                        string nuevoCorreo = textBox.Text;
                        correos.Rows[rowIndex]["Correo"] = nuevoCorreo;
                        subject = "Pedido con Factura #" + correos.Rows[rowIndex]["Factura"].ToString();
                        body =  "Tu pedido con Factura #" + correos.Rows[rowIndex]["Factura"].ToString() + " se ha embarcado a la Guía #" + txtNumeroGuia.Text.ToString() + 
                                "<br><br>" + 
                                "Cualquier duda respecto a tu envío contacta directo a la paquetería.";
                        DateTime date = DateTime.Now;
                        if (cl.esEmail(nuevoCorreo))
                        {
                            result = cl.EnviaCorreoGeneral(correos.Rows[rowIndex]["Correo"].ToString(), subject, body, date, "", "G", "", 0);
                            if (result != "OK")
                                MessageBox.Show(result);
                        }
                        else if (nuevoCorreo != string.Empty)
                            MessageBox.Show("No es un correo válido: \n" + nuevoCorreo);
                        
                        rowIndex++; 
                    }
                }

                popUpEnviarXCorreo.Hide();
                flowLayoutPanelCorreos.Controls.Clear();
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
            catch (Exception ex)
            {
                string error = "Error en bbiConfirmarEnvios_Click línea " + ex.LineNumber().ToString() + ":\n" + ex.Message;
                MessageBox.Show(error);
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
                return;
            }
        }

        private void txtPorcentajeIva_KeyDown(object sender, KeyEventArgs e)
        {
            if(char.IsLetter((char)e.KeyCode))
                e.SuppressKeyPress = true;
        }

        private void txtPorcentajeRetIva_KeyDown(object sender, KeyEventArgs e)
        {
            if (char.IsLetter((char)e.KeyCode))
                e.SuppressKeyPress = true;
        }

        private void txtSubtotal_KeyDown(object sender, KeyEventArgs e)
        {
            if (char.IsLetter((char)e.KeyCode))
                e.SuppressKeyPress = true;
        }

        private void txtPorcentajeIva_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPorcentajeIva.Text != string.Empty)
            {
                decimal subTotal = Convert.ToDecimal(txtSubtotal.Text);
                decimal porcentajeIva = Convert.ToDecimal(txtPorcentajeIva.Text);
                decimal iva = Math.Round(subTotal * (porcentajeIva / 100), 2);
                txtIva.Text = iva.ToString();
            }
        }

        private void txtPorcentajeRetIva_EditValueChanged(object sender, EventArgs e)
        {
            if(txtPorcentajeRetIva.Text != string.Empty)
            {
                decimal subTotal = Convert.ToDecimal(txtSubtotal.Text);
                decimal porcentajeRetIva = Convert.ToDecimal(txtPorcentajeRetIva.Text);
                decimal retIva = Math.Round(subTotal * (porcentajeRetIva / 100), 2);
                txtRetIva.Text = retIva.ToString();
            }
        }

        private void txtIva_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPorcentajeIva.Text == string.Empty && txtIva.Text != string.Empty)
            {
                decimal subTotal = Convert.ToDecimal(txtSubtotal.Text);
                decimal iva = Convert.ToDecimal(txtIva.Text);
                decimal porcentajeIva = Math.Round(((iva / subTotal) * 100), 2);
                txtPorcentajeIva.Text = porcentajeIva.ToString();
            }
            if (txtIva.Text != string.Empty && txtRetIva.Text != string.Empty)
                txtTotal.Text = Math.Round(Convert.ToDecimal(txtSubtotal.Text) + (Convert.ToDecimal(txtIva.Text) + Convert.ToDecimal(txtRetIva.Text)), 2).ToString();
        }

        private void txtRetIva_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPorcentajeRetIva.Text == string.Empty && txtRetIva.Text != string.Empty)
            {
                decimal subTotal = Convert.ToDecimal(txtSubtotal.Text);
                decimal retIva = Convert.ToDecimal(txtRetIva.Text);
                decimal porcentajeRetIva = Math.Round(((retIva / subTotal) * 100), 2);
                txtPorcentajeRetIva.Text = porcentajeRetIva.ToString();
            }
            if (txtIva.Text != string.Empty && txtRetIva.Text != string.Empty)
                txtTotal.Text = Math.Round(Convert.ToDecimal(txtSubtotal.Text) + (Convert.ToDecimal(txtIva.Text) + Convert.ToDecimal(txtRetIva.Text)), 2).ToString();
        }

        private void txtSubtotal_EditValueChanged(object sender, EventArgs e)
        {
            if(txtSubtotal.Text != string.Empty)
            {
                decimal subTotal = Convert.ToDecimal(txtSubtotal.Text);
                
                if(txtPorcentajeIva.Text != string.Empty)
                {
                    decimal porcentajeIva = Convert.ToDecimal(txtPorcentajeIva.Text);
                    decimal iva = Math.Round(subTotal * (porcentajeIva / 100), 2);
                    txtIva.Text = iva.ToString();
                }
                if(txtPorcentajeRetIva.Text != string.Empty)
                {
                    decimal porcentajeRetIva = Convert.ToDecimal(txtPorcentajeRetIva.Text);
                    decimal retIva = Math.Round(subTotal * (porcentajeRetIva / 100), 2);
                    txtRetIva.Text = retIva.ToString();
                }

                if (txtIva.Text != string.Empty && txtRetIva.Text != string.Empty)
                    txtTotal.Text = Math.Round(Convert.ToDecimal(txtSubtotal.Text) + (Convert.ToDecimal(txtIva.Text) + Convert.ToDecimal(txtRetIva.Text)), 2).ToString();
            }
        }
    }
}