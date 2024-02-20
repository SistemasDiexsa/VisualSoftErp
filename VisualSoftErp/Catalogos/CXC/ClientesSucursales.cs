using DevExpress.DataProcessing;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Services;
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
using VisualSoftErp.Clases.CXC_CLs;
using VisualSoftErp.Interface.Request;

namespace VisualSoftErp.Catalogos.CXC
{
    public partial class ClientesSucursales : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int intCliente; //valor ID cliente
        bool Editando; //indica si está editando o no el registro
        string strNombre;// valor Nombre Del Cliente
        public ClientesSucursales()
        {
            InitializeComponent();
            cargaCombos();
            LlenaGrid();
            gridViewPrincipal.OptionsView.ShowAutoFilterRow = true;
            gridViewPrincipal.OptionsBehavior.ReadOnly = true;
            gridViewPrincipal.OptionsBehavior.Editable = false;
            gridViewPrincipal.OptionsView.ShowViewCaption = true;
            gridViewPrincipal.ViewCaption = "Clientes Sucursales";

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();

        } // carga campos iniciales de la pantalla

        private void cargaCombos() //Metodo para llenar los Combo box 
        {
            combosCL cl = new combosCL();

            cl.strTabla = "Clientes";
            cboCliente.Properties.ValueMember = "Clave";
            cboCliente.Properties.DisplayMember = "Des";
            cboCliente.Properties.DataSource = cl.CargaCombos();
            cboCliente.Properties.ForceInitialize();
            cboCliente.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cboCliente.Properties.PopulateColumns();
            //cboCliente.Properties.Columns["Clave"].Visible = false;
            cboCliente.Properties.Columns["AgentesID"].Visible = false;
            cboCliente.Properties.Columns["Plazo"].Visible = false;
            cboCliente.Properties.Columns["Listadeprecios"].Visible = false;
            cboCliente.Properties.Columns["Exportar"].Visible = false;
            cboCliente.Properties.Columns["cFormapago"].Visible = false;
            cboCliente.Properties.Columns["cMetodopago"].Visible = false;
            cboCliente.Properties.Columns["UsoCfdi"].Visible = false;
            cboCliente.Properties.Columns["PIva"].Visible = false;
            cboCliente.Properties.Columns["PIeps"].Visible = false;
            cboCliente.Properties.Columns["PRetiva"].Visible = false;
            cboCliente.Properties.Columns["PRetIsr"].Visible = false;
            cboCliente.Properties.Columns["EMail"].Visible = false;
            cboCliente.Properties.Columns["BancoordenanteID"].Visible = false;
            cboCliente.Properties.Columns["Cuentaordenante"].Visible = false;
            cboCliente.Properties.Columns["cFormapagoDepositos"].Visible = false;
            cboCliente.Properties.Columns["Moneda"].Visible = false;
            cboCliente.Properties.Columns["DescuentoBase"].Visible = false;
            cboCliente.Properties.Columns["DesctoPP"].Visible = false;
            cboCliente.Properties.Columns["TransportesID"].Visible = false;
            cboCliente.Properties.Columns["Addenda"].Visible = false;
            cboCliente.Properties.Columns["Desglosardescuentoalfacturar"].Visible = false;
            cboCliente.Properties.NullText = "Seleccione un cliente";

        }
     
        private void cboCliente_EditValueChanged(object sender, EventArgs e)
        {
            object orow = cboCliente.Properties.GetDataSourceRowByKeyValue(cboCliente.EditValue);
            if (orow != null)
            {
                intCliente = Convert.ToInt32(((DataRowView)orow)["Clave"]);
            }
        } // se utiliza para tomar el ID del elemento seleccionado en el combo (LOOKUP EDIT )

        public void LlenaGrid()  // Metodo para llenar data Grid
        {
            ClientesSucursalesCL cl = new ClientesSucursalesCL();
            gridControlPrincipal.DataSource = cl.ClientesSucursalesGrid();
            globalCL clg = new globalCL();
            clg.strGridLayout = "gridclientessucursales";
            clg.restoreLayout(gridViewPrincipal);
        }

        private void bbiNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Nuevo();

        } // Evento Click Boton Nuevo 

        private void Nuevo()
        {
            BotonesEdicion();
            LimpiaCajas();
            LblSucursal.Text = "0";
            Editando = false;
        } // Oculta botones innesesarios para edición, Limpia los TXT e inicialisa LBL sucursal en 0, inicialisa Editando false 

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
        } // cambia de pagina e solo deja visible los botones Guardar y Regresar a la hora de EDITAR

        private void LimpiaCajas ()
        {
            TxtNombre.Text = string.Empty;
            TxtDireccion.Text = string.Empty;
            TxtCiudad.Text = string.Empty;  
            TxtTelefono.Text = string.Empty;
            TxtResponsable.Text = string.Empty;
            TxtCorreo.Text = string.Empty;
            TxtTransporte.Text = string.Empty;
        } // limpia el contenido de los TXT

        private void LlenaCajas ()
        {
            ClientesSucursalesCL cl = new ClientesSucursalesCL ();
            cl.IntClientesID = intCliente;
            cl.IntSucursal = Convert.ToInt32(LblSucursal.Text);
            String result = cl.ClientesSucursalesLlenaCajas ();
            if (result== "OK")
            {
                cboCliente.EditValue = intCliente;
                TxtNombre.Text = cl.strNombre;
                TxtDireccion.Text = cl.strDir;
                TxtCiudad.Text = cl.strCd;
                TxtTelefono.Text = cl.strTel;
                TxtResponsable.Text = cl.strResponsable;
                TxtCorreo.Text = cl.strCorreo;  
                TxtTransporte.Text = cl.strTransporte;         
                    
            }
            else
            {
                MessageBox.Show(result);
            }
        } // intancia una clase y se llena los TXT con los campos de la consulta en el Store prosedure Llena cajas

        private void Regresar()
        {
            bbiGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiCerrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            bbiVista.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            navigationFrame.SelectedPageIndex = 0;
            LlenaGrid  ();

        } // Oculta los botones innesesarios para la pagina inicial y llena el data Grid 

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) // Evento click Regresar
        {
            Regresar();
        }

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            globalCL clg = new globalCL(); // instanciamos la clase globalCL
            clg.strGridLayout = "gridclientessucursales";// asignamos el nombre de la forma al lyout
            String result = clg.SaveGridLayout(gridViewPrincipal); //guardamos el Lyout del Grid
            if (result != "OK")
            {
                MessageBox.Show(result);
            }
            this.Close (); // cierra la forma
        }// se cierra el programa y se guarda la ultima configuracion en el data grid 

        private void bbiVista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlPrincipal.ShowRibbonPrintPreview();
        } // envia el contenido del datagrid a un preview para Reporteo 

        private void bbiEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Editar();  
        } // Evento click editar 
        
        private void Editar()
        {
            if (intCliente == 0)
            {
                MessageBox.Show("Selecciona Un Cliente");
                return;
            }
            BotonesEdicion();
            LlenaCajas();
            Editando = true;
        } //  valida si seleccionó un cliente, desactiva y activa los botones necesarios para editar y pasa de pagina y llena los TXT con los registros a Editar

        private void bbiGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Guardar();
        } // Evento Click Guardar

        private bool validar ()
        {

            if (intCliente == 0)
            {
                MessageBox.Show("Selecciona Un Cliente");
                return false;
            }
            if (TxtNombre.Text.Length == 0)
            {
                MessageBox.Show("Capture el nombre ");
                return false;
            }
            if (TxtDireccion.Text.Length == 0)
            {
                MessageBox.Show("Capture La Dirección ");
                return false;
            }

            if (TxtCiudad.Text.Length == 0)
            {
                MessageBox.Show("Capture La Ciudad ");
                return false;
            }
            if (TxtTelefono.Text.Length == 0)
            {
                MessageBox.Show("Capture El Teléfono");
                return false;
            }
            if (TxtResponsable.Text.Length == 0)
            {
                MessageBox.Show("Capture el responsable");
                return false;
            }
            if (TxtCorreo.Text.Length == 0)
            {
                MessageBox.Show("Capture el Correo");
                return false;
            }
            if (TxtTransporte.Text.Length == 0)
            {
                MessageBox.Show("Capture el Transporte");
                return false;
            }
            return true;   
        }

        private void Guardar()
        {
            if (!validar())
            {
               
                return;
            }

            ClientesSucursalesCL cl  = new ClientesSucursalesCL();

            cl.IntClientesID = intCliente;
            cl.IntSucursal = Convert.ToInt32(LblSucursal.Text); 
            cl.strNombre= TxtNombre.Text;
            cl.strDir = TxtDireccion.Text; 
            cl.strCd  = TxtCiudad.Text;
            cl.strTel = TxtTelefono.Text;
            cl.strResponsable = TxtResponsable.Text;
            cl.strCorreo = TxtCorreo.Text;
            cl.strTransporte = TxtTransporte.Text;

            string result = cl.ClientesSucursalesCRUD();
            if (result == "OK")
            {
                MessageBox.Show("Guardado Correctamente");
                if (!Editando)
                {
                    LimpiaCajas();
                }
               
            }
            else
            {
                MessageBox.Show("Error al guardar:" + result);
            }
        } // valida que se tenga ID del cliente, instancia una clase y se le pasa los valores Store Prosedure CRUD esperando un OK Como Respuesta
               
        private void gridViewPrincipal_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            intCliente = Convert.ToInt32(gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "ClientesID"));
            LblSucursal.Text  = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Sucursal").ToString() ;
            strNombre = gridViewPrincipal.GetRowCellValue(gridViewPrincipal.FocusedRowHandle, "Nombre").ToString();
        } // Toma los valores de los campos con el evento Click del renglon en el data grid para usarlos  

        private void bbiEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult Result = MessageBox.Show("Desea eliminar la sucursal " + strNombre, "Eliminar", MessageBoxButtons.YesNo);
            if (Result.ToString() == "Yes")
            {
                Eliminar();
                
            }
        } // Manda Mensaje Yes NO para preguntar si desea eliminar el registro y luego corre el proceso o se sale del evento
       
        private void Eliminar()
        {
           
            ClientesSucursalesCL CL = new ClientesSucursalesCL();
            CL.IntClientesID = intCliente;
            CL.IntSucursal = Convert.ToInt32(LblSucursal.Text);

            string result = CL.ClientesSucursalesEliminar();
            if (result == "OK") 
            {
                MessageBox.Show("Se elíminó el registro correctamente ");
                LlenaGrid();
            }
            else 
            {
                MessageBox.Show("Error:" + result);
            }
        } // instancia la clase se corre el Store prosedure Elimina y se espera un OK de respuesta si se corrió correctamente.
    }
}