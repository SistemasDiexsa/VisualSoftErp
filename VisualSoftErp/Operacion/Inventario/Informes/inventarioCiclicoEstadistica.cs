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
using VisualSoftErp.Operacion.Inventarios.Clases;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Inventario.Informes
{
    public partial class inventarioCiclicoEstadistica : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public inventarioCiclicoEstadistica()
        {
            InitializeComponent();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
       

        private void bbiCerrar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiProcesar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            proceso();
        }

        private void proceso()
        {
            try
            {
                globalCL clg = new globalCL();

                InventariofisicorepCL cl = new InventariofisicorepCL();
                DataSet ds = new DataSet();
                ds = cl.InventarioCilicoEstadistica();

                DataTable dtYM = new DataTable();
                DataTable dtDia = new DataTable();
                DataTable dtDet = new DataTable();

                dtDet = ds.Tables[0];
                dtDia= ds.Tables[1];
                dtYM= ds.Tables[2];

                Decimal Ptje;

                foreach (DataRow drYM in dtYM.Rows)
                {

                    Ptje = Convert.ToDecimal(drYM["Diferencia"]) / Convert.ToDecimal(drYM["Contados"]);
                    drYM["Porcentaje"] = 100 - Math.Round(Ptje * 100,2);
                    drYM["Mes"] = clg.NombreDeMes(Convert.ToInt32(drYM["Mes"]));

                }

                string dia;
                string mes;
                string año;


                foreach (DataRow drDia in dtDia.Rows)
                {
                    Ptje = (Convert.ToDecimal(drDia["Diferencia"]) / Convert.ToDecimal(drDia["Contados"]));
                    drDia["Porcentaje"] = 100 - Math.Round(Ptje * 100,2);

                    dia = drDia["Fecha"].ToString().Substring(6, 2);
                    mes = drDia["Fecha"].ToString().Substring(4, 2);
                    año = drDia["Fecha"].ToString().Substring(0, 4);

                    drDia["Fecha"] = dia + "/" + mes + "/" + año;
                }


                gridControlDetalle.DataSource = dtDet;
                gridControlDia.DataSource = dtDia;
                gridControlYearMes.DataSource = dtYM;

                gridViewDia.Columns["Porcentaje"].Caption = "Confiabilidad";
                gridViewyearMes.Columns["Porcentaje"].Caption = "Confiabilidad";

                gridViewDetalle.OptionsView.ShowAutoFilterRow = true;
                gridViewDetalle.OptionsView.ShowViewCaption = true;

                gridViewyearMes.OptionsView.ShowViewCaption = true;

                gridViewDia.OptionsView.ShowViewCaption = true;

                gridViewDetalle.ViewCaption = "CAPTURA DIARIA POR ARTICULO";
                gridViewDia.ViewCaption = "POR FECHA";
                gridViewyearMes.ViewCaption = "POR AÑO Y MES";

                gridViewDia.Columns["Fecha"].DisplayFormat.FormatString = "d";
                gridViewDia.Columns["Porcentaje"].DisplayFormat.FormatString = "n2";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}