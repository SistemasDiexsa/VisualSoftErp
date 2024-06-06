namespace VisualSoftErp.Operacion.Inventario.Designers
{
    partial class EtiquetasSKUDesigner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.panel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.lblNombreSubfamilias = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNombreFamilias = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNombreLineas = new DevExpress.XtraReports.UI.XRLabel();
            this.BarCode = new DevExpress.XtraReports.UI.XRBarCode();
            this.lblNombreArticulo = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.timeSpanChartRangeControlClient1 = new DevExpress.XtraEditors.TimeSpanChartRangeControlClient();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanChartRangeControlClient1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.panel1});
            this.Detail.HeightF = 220F;
            this.Detail.MultiColumn.ColumnCount = 2;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
            this.Detail.Name = "Detail";
            // 
            // panel1
            // 
            this.panel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.panel1.CanGrow = false;
            this.panel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblNombreSubfamilias,
            this.lblNombreFamilias,
            this.lblNombreLineas,
            this.BarCode,
            this.lblNombreArticulo});
            this.panel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.panel1.Name = "panel1";
            this.panel1.SizeF = new System.Drawing.SizeF(380F, 210F);
            // 
            // lblNombreSubfamilias
            // 
            this.lblNombreSubfamilias.BorderColor = System.Drawing.Color.Transparent;
            this.lblNombreSubfamilias.Font = new System.Drawing.Font("Arial", 12F);
            this.lblNombreSubfamilias.LocationFloat = new DevExpress.Utils.PointFloat(189.9999F, 74.75F);
            this.lblNombreSubfamilias.Multiline = true;
            this.lblNombreSubfamilias.Name = "lblNombreSubfamilias";
            this.lblNombreSubfamilias.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNombreSubfamilias.SizeF = new System.Drawing.SizeF(180F, 32.375F);
            this.lblNombreSubfamilias.StylePriority.UseBorderColor = false;
            this.lblNombreSubfamilias.StylePriority.UseFont = false;
            this.lblNombreSubfamilias.StylePriority.UseTextAlignment = false;
            this.lblNombreSubfamilias.Text = "Nombre Subfamilias";
            this.lblNombreSubfamilias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblNombreSubfamilias.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblNombreSubfamilias_BeforePrint);
            // 
            // lblNombreFamilias
            // 
            this.lblNombreFamilias.BorderColor = System.Drawing.Color.Transparent;
            this.lblNombreFamilias.Font = new System.Drawing.Font("Arial", 12F);
            this.lblNombreFamilias.LocationFloat = new DevExpress.Utils.PointFloat(10.00004F, 74.75F);
            this.lblNombreFamilias.Multiline = true;
            this.lblNombreFamilias.Name = "lblNombreFamilias";
            this.lblNombreFamilias.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNombreFamilias.SizeF = new System.Drawing.SizeF(180F, 32.375F);
            this.lblNombreFamilias.StylePriority.UseBorderColor = false;
            this.lblNombreFamilias.StylePriority.UseFont = false;
            this.lblNombreFamilias.StylePriority.UseTextAlignment = false;
            this.lblNombreFamilias.Text = "Nombre Familias";
            this.lblNombreFamilias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblNombreFamilias.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblNombreFamilias_BeforePrint);
            // 
            // lblNombreLineas
            // 
            this.lblNombreLineas.BorderColor = System.Drawing.Color.Transparent;
            this.lblNombreLineas.Font = new System.Drawing.Font("Arial", 12F);
            this.lblNombreLineas.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 42.37498F);
            this.lblNombreLineas.Multiline = true;
            this.lblNombreLineas.Name = "lblNombreLineas";
            this.lblNombreLineas.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNombreLineas.SizeF = new System.Drawing.SizeF(360F, 32.375F);
            this.lblNombreLineas.StylePriority.UseBorderColor = false;
            this.lblNombreLineas.StylePriority.UseFont = false;
            this.lblNombreLineas.StylePriority.UseTextAlignment = false;
            this.lblNombreLineas.Text = "Nombre Lineas";
            this.lblNombreLineas.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblNombreLineas.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblNombreLineas_BeforePrint);
            // 
            // BarCode
            // 
            this.BarCode.BorderColor = System.Drawing.Color.Transparent;
            this.BarCode.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 115F);
            this.BarCode.Name = "BarCode";
            this.BarCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.BarCode.ShowText = false;
            this.BarCode.SizeF = new System.Drawing.SizeF(360F, 75.00001F);
            this.BarCode.StylePriority.UseBorderColor = false;
            this.BarCode.StylePriority.UseTextAlignment = false;
            this.BarCode.Symbology = code128Generator1;
            this.BarCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.BarCode.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BarCode_BeforePrint);
            // 
            // lblNombreArticulo
            // 
            this.lblNombreArticulo.BorderColor = System.Drawing.Color.Transparent;
            this.lblNombreArticulo.Font = new System.Drawing.Font("Arial", 12F);
            this.lblNombreArticulo.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 10F);
            this.lblNombreArticulo.Multiline = true;
            this.lblNombreArticulo.Name = "lblNombreArticulo";
            this.lblNombreArticulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNombreArticulo.SizeF = new System.Drawing.SizeF(360F, 32.375F);
            this.lblNombreArticulo.StylePriority.UseBorderColor = false;
            this.lblNombreArticulo.StylePriority.UseFont = false;
            this.lblNombreArticulo.StylePriority.UseTextAlignment = false;
            this.lblNombreArticulo.Text = "Nombre Articulo";
            this.lblNombreArticulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblNombreArticulo.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblNombreArticulo_BeforePrint);
            // 
            // TopMargin
            // 
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            // 
            // EtiquetasSKUDesigner
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(40, 40, 100, 100);
            this.ReportPrintOptions.DetailCountOnEmptyDataSource = 4;
            this.Version = "21.2";
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanChartRangeControlClient1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRPanel panel1;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraEditors.TimeSpanChartRangeControlClient timeSpanChartRangeControlClient1;
        private DevExpress.XtraReports.UI.XRLabel lblNombreArticulo;
        private DevExpress.XtraReports.UI.XRBarCode BarCode;
        private DevExpress.XtraReports.UI.XRLabel lblNombreLineas;
        private DevExpress.XtraReports.UI.XRLabel lblNombreSubfamilias;
        private DevExpress.XtraReports.UI.XRLabel lblNombreFamilias;
    }
}
