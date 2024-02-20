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
using VisualSoftErp.Operacion.CxC.Clases;
using DevExpress.XtraEditors.Controls;
using VisualSoftErp.Clases;
using VisualSoftErp.Operacion.Compras.Clases;

namespace VisualSoftErp.Operacion.CxC.Informes
{
    public partial class clientesRetenidos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public clientesRetenidos()
        {
            InitializeComponent();
            txtAñoAnt.Text = (DateTime.Now.Year - 1).ToString();
            txtAñoAct.Text = DateTime.Now.Year.ToString();
            //CargaCombos();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void CargaCombos()
        {
            globalCL clg = new globalCL();
            combosCL cl = new combosCL();
            BindingSource src = new BindingSource();
            cl.strTabla = "ClasificacionPorTipo";
            chkTipo.ValueMember = "Clave";
            chkTipo.DisplayMember = "Des";
            src.DataSource = cl.CargaCombos();
            chkTipo.DataSource= clg.AgregarOpcionTodos(src);            
            chkTipo.ForceInitialize();

            cl.strTabla = "Canalesdeventa";
            chkTipo.ValueMember = "Clave";
            chkTipo.DisplayMember = "Des";
            src.DataSource = cl.CargaCombos();
            chkCanal.DataSource = clg.AgregarOpcionTodos(src);
            chkCanal.ForceInitialize();
        }

        private void Proceso()
        {
            try
            {
                globalCL clG = new globalCL();
                if (!clG.esNumerico(txtAñoAnt.Text))
                {
                    MessageBox.Show("Teclee el año base");
                    botonesAlValidar();
                    return;
                }
                if (!clG.esNumerico(txtAñoAct.Text))
                {
                    MessageBox.Show("Teclee el año actual");
                    botonesAlValidar();
                    return;
                }

                clientesRetenidosCL cl = new clientesRetenidosCL();
                

                string strMesesAnt = string.Empty;
                string strMesesAct = string.Empty;

                //Meses anteriores
                foreach (CheckedListBoxItem item in chkMesesBase.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        if (item.Value.ToString() != "-1")
                            strMesesAnt = strMesesAnt + item.Value + ",";
                    }
                }

                if (strMesesAnt.Length==0)
                {
                    MessageBox.Show("Seleccione al menos un mes base");
                    botonesAlValidar();
                    return;
                }

                strMesesAnt = strMesesAnt.Substring(0, strMesesAnt.Length - 1);

                //Meses actuales
                foreach (CheckedListBoxItem item in chkMesesAct.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        if (item.Value.ToString() != "-1")
                            strMesesAct = strMesesAct + item.Value + ",";
                    }
                }

                if (strMesesAct.Length == 0)
                {
                    MessageBox.Show("Seleccione al menos un mes actual");
                    botonesAlValidar();
                    return;
                }

                strMesesAct = strMesesAct.Substring(0, strMesesAct.Length - 1);

                //Canales
                String strCanales = string.Empty;
                foreach (CheckedListBoxItem item in chkCanal.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        if (item.Value.ToString() != "-1")
                            strCanales = strCanales + item.Value + ",";
                    }
                }

                if (strCanales.Length == 0)
                {
                    MessageBox.Show("Seleccione al menos un canal");
                    botonesAlValidar();
                    return;
                }

                strCanales = strCanales.Substring(0, strCanales.Length - 1);

                //Tipos
                String strTipos = string.Empty;
                foreach (CheckedListBoxItem item in chkTipo.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        if (item.Value.ToString() != "-1")
                            strTipos = strTipos + item.Value + ",";
                    }
                }

                if (strTipos.Length == 0)
                {
                    MessageBox.Show("Seleccione al menos un tipo");
                    botonesAlValidar();
                    return;
                }
                strTipos = strTipos.Substring(0, strTipos.Length - 1);


                //Se llama SP
                cl.intAñoAnt = Convert.ToInt32(txtAñoAnt.Text);
                cl.intAñoAct = Convert.ToInt32(txtAñoAct.Text);
                cl.strMesesAnt = strMesesAnt;
                cl.strMesesNvo = strMesesAct;
                cl.strCanales = strCanales;
                cl.strTipos = strTipos;

                gridControl1.DataSource = cl.clientesRetenidos();             
                decimal intBase = 0;
                decimal intRet = 0;
                decimal Ptje = 0;

                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    intBase = Convert.ToInt32(gridView1.GetRowCellValue(i, "Base"));
                    intRet = Convert.ToInt32(gridView1.GetRowCellValue(i, "Retenidos"));

                    if (intBase > 0)
                        Ptje = Math.Round((intRet / intBase) * 100, 2);
                    else
                        Ptje = 0;

                    gridView1.FocusedRowHandle = i;
                    gridView1.SetFocusedRowCellValue("Ptje", Ptje);
                }

                gridView1.OptionsView.ShowViewCaption = true;
                gridView1.ViewCaption = "Base :" + txtAñoAnt.Text + " " + strMesesAnt + " VS " + txtAñoAct.Text + " " + strMesesAct;

                bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiExportar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbiPrevio.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                navigationFrame.SelectedPageIndex = 1;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bbiPrevio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiPrevio.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;            
            Proceso();

        }

        private void chkMesesBase_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 12)
            {
                if (e.State == CheckState.Checked)
                    chkMesesBase.CheckAll();
                else
                    chkMesesBase.UnCheckAll();
                    
            }
        }

        private void chkMesesAct_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 12)
            {
                if (e.State == CheckState.Checked)
                    chkMesesAct.CheckAll();
                else
                    chkMesesAct.UnCheckAll();

            }
        }

        private void botonesAlValidar()
        {
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiExportar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiPrevio.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void bbiRegresar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bbiRegresar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiExportar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiPrevio.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            navigationFrame.SelectedPageIndex = 0;
        }

        private void chkTipo_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 5)
            {
                if (e.State == CheckState.Checked)
                    chkTipo.CheckAll();
                else
                    chkTipo.UnCheckAll();
            }
        }

        private void bbiExportar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void chkCanal_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 7)
            {
                if (e.State == CheckState.Checked)
                    chkCanal.CheckAll();
                else
                    chkCanal.UnCheckAll();
            }
        }
    }
}