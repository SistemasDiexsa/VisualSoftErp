﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using VisualSoftErp.Clases.HerrramientasCLs;
using DevExpress.DataAccess.ConnectionParameters;
using VisualSoftErp.Clases;

namespace VisualSoftErp.Operacion.Ventas.Designers
{
    public partial class VentasporarticuloclienteDesigner : DevExpress.XtraReports.UI.XtraReport
    {

        int dbr = 0;
        int dbg = 0;
        int dbb = 0;
        int dfr = 0;
        int dfg = 0;
        int dfb = 0;
        string stipoLogo;
        decimal decPrecio = 0, decCantidad, decTotal, decNetoTotal, decPreciopromedio;

        private void calculatedFieldTotal_GetValue(object sender, GetValueEventArgs e)
        {
            decCantidad = Convert.ToDecimal(GetCurrentColumnValue("Cantidad"));
            decPrecio = Convert.ToDecimal(GetCurrentColumnValue("Importe"));

            decTotal = decPrecio; // Math.Round(decCantidad * decPrecio, 2);
            decNetoTotal += decTotal;
            e.Value = decTotal;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void calculatedFieldNetoTotal_GetValue(object sender, GetValueEventArgs e)
        {

            if (decNetoTotal == 0) { }
            else
            {
                e.Value = decNetoTotal;
                decNetoTotal = 0;
            }
        }

        private void tableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tableCell5.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell5.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell6.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell6.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell9.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell9.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            //tableCell10.BackColor = Color.FromArgb(dbr, dbg, dbb);
            //tableCell10.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            tableCell11.BackColor = Color.FromArgb(dbr, dbg, dbb);
            tableCell11.ForeColor = Color.FromArgb(dfr, dfg, dfb);

            xrTableCell5.BackColor = Color.FromArgb(dbr, dbg, dbb);
            xrTableCell5.ForeColor = Color.FromArgb(dfr, dfg, dfb);
        }

        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
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

        private void calculatedFieldPreciopromedio_GetValue(object sender, GetValueEventArgs e)
        {
            decCantidad = Convert.ToDecimal(GetCurrentColumnValue("Cantidad"));
            decPrecio = Convert.ToDecimal(GetCurrentColumnValue("Importe"));

            decTotal = decPrecio; // Math.Round(decCantidad * decPrecio, 2);
            decPreciopromedio = Math.Round(decTotal / decCantidad, 2);
            e.Value = decPreciopromedio;
        }

        public VentasporarticuloclienteDesigner()
        {
            InitializeComponent();
            stipoLogo = "C";
            DatosdecontrolCL cl = new DatosdecontrolCL();
            string result = cl.DatosdecontrolLeer();
            if (result == "OK")
            {
                dbr = cl.dbr;
                dbg = cl.dbg;
                dbb = cl.dbb;
                dfr = cl.dfr;
                dfg = cl.dfg;
                dfb = cl.dfb;
                stipoLogo = cl.sTipologo;
            }

        }

        private void xrPictureBoxLogo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string pathLogo = System.Configuration.ConfigurationManager.AppSettings["logoEmpresa"].ToString();
            xrPictureBoxLogo.ImageUrl = pathLogo;

            if (stipoLogo == "L")
            {
                xrPictureBoxLogo.WidthF = 237;
                xrPictureBoxLogo.HeightF = 47;
            }

        }

        private void xrLblEmp_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (stipoLogo == "L")
            {
                xrLblEmp.LocationF = new PointF(10, xrPictureBoxLogo.LocationF.Y + xrPictureBoxLogo.HeightF + 1);
            }
            else
            {
                xrLblEmp.LocationF = new PointF(xrPictureBoxLogo.LocationF.X + xrPictureBoxLogo.WidthF + 80, 5);
            }

        }


    }
}