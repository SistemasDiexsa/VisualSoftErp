namespace VisualSoftErp.Operacion.Ventas.Designers
{
    partial class PedidosFormatoImpresionKits
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
            this.components = new System.ComponentModel.Container();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PedidosFormatoImpresionKits));
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GroupCaption1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GroupData1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailCaption1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailData1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GroupFooterBackground3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailData3_Odd = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.pageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.pageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelAge = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.table3 = new DevExpress.XtraReports.UI.XRTable();
            this.tableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.parameter1 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter2 = new DevExpress.XtraReports.Parameters.Parameter();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrPictureBoxLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabelEmpPH = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.table1 = new DevExpress.XtraReports.UI.XRTable();
            this.tableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.table3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "VisualSoftErpConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "PedidosImpresionFormatokits";
            queryParameter1.Name = "@prmSerie";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("?parameter1", typeof(string));
            queryParameter2.Name = "@prmFolio";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("?parameter2", typeof(int));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.StoredProcName = "PedidosImpresionFormatokits";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            this.sqlDataSource1.ConfigureDataConnection += new DevExpress.DataAccess.Sql.ConfigureDataConnectionEventHandler(this.sqlDataSource1_ConfigureDataConnection);
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.BorderColor = System.Drawing.Color.Black;
            this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Title.BorderWidth = 1F;
            this.Title.Font = new System.Drawing.Font("Arial", 14.25F);
            this.Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Title.Name = "Title";
            // 
            // GroupCaption1
            // 
            this.GroupCaption1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(159)))), ((int)(((byte)(228)))));
            this.GroupCaption1.BorderColor = System.Drawing.Color.White;
            this.GroupCaption1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.GroupCaption1.BorderWidth = 2F;
            this.GroupCaption1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.GroupCaption1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.GroupCaption1.Name = "GroupCaption1";
            this.GroupCaption1.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
            this.GroupCaption1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GroupData1
            // 
            this.GroupData1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(159)))), ((int)(((byte)(228)))));
            this.GroupData1.BorderColor = System.Drawing.Color.White;
            this.GroupData1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.GroupData1.BorderWidth = 2F;
            this.GroupData1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.GroupData1.ForeColor = System.Drawing.Color.White;
            this.GroupData1.Name = "GroupData1";
            this.GroupData1.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
            this.GroupData1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DetailCaption1
            // 
            this.DetailCaption1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(159)))), ((int)(((byte)(228)))));
            this.DetailCaption1.BorderColor = System.Drawing.Color.White;
            this.DetailCaption1.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.DetailCaption1.BorderWidth = 2F;
            this.DetailCaption1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.DetailCaption1.ForeColor = System.Drawing.Color.White;
            this.DetailCaption1.Name = "DetailCaption1";
            this.DetailCaption1.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
            this.DetailCaption1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DetailData1
            // 
            this.DetailData1.BorderColor = System.Drawing.Color.Transparent;
            this.DetailData1.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.DetailData1.BorderWidth = 2F;
            this.DetailData1.Font = new System.Drawing.Font("Arial", 8.25F);
            this.DetailData1.ForeColor = System.Drawing.Color.Black;
            this.DetailData1.Name = "DetailData1";
            this.DetailData1.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
            this.DetailData1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GroupFooterBackground3
            // 
            this.GroupFooterBackground3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(117)))), ((int)(((byte)(129)))));
            this.GroupFooterBackground3.BorderColor = System.Drawing.Color.White;
            this.GroupFooterBackground3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.GroupFooterBackground3.BorderWidth = 2F;
            this.GroupFooterBackground3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.GroupFooterBackground3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.GroupFooterBackground3.Name = "GroupFooterBackground3";
            this.GroupFooterBackground3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
            this.GroupFooterBackground3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DetailData3_Odd
            // 
            this.DetailData3_Odd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.DetailData3_Odd.BorderColor = System.Drawing.Color.Transparent;
            this.DetailData3_Odd.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.DetailData3_Odd.BorderWidth = 1F;
            this.DetailData3_Odd.Font = new System.Drawing.Font("Arial", 8.25F);
            this.DetailData3_Odd.ForeColor = System.Drawing.Color.Black;
            this.DetailData3_Odd.Name = "DetailData3_Odd";
            this.DetailData3_Odd.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
            this.DetailData3_Odd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PageInfo
            // 
            this.PageInfo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.PageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.PageInfo.Name = "PageInfo";
            this.PageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 50F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pageInfo1,
            this.pageInfo2});
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // pageInfo1
            // 
            this.pageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(5F, 5F);
            this.pageInfo1.Name = "pageInfo1";
            this.pageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.pageInfo1.SizeF = new System.Drawing.SizeF(365F, 23F);
            this.pageInfo1.StyleName = "PageInfo";
            // 
            // pageInfo2
            // 
            this.pageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(380F, 5F);
            this.pageInfo2.Name = "pageInfo2";
            this.pageInfo2.SizeF = new System.Drawing.SizeF(365F, 23F);
            this.pageInfo2.StyleName = "PageInfo";
            this.pageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.pageInfo2.TextFormatString = "Page {0} of {1}";
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7,
            this.xrLabelAge,
            this.xrLabel41,
            this.xrLabel5,
            this.xrLabel24,
            this.xrLabel4,
            this.xrLabel26,
            this.xrLabel6,
            this.xrLabel3});
            this.GroupHeader2.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
            this.GroupHeader2.HeightF = 131.0419F;
            this.GroupHeader2.Level = 1;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 8.75F);
            this.xrLabel7.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(7.36618F, 77.41674F);
            this.xrLabel7.Multiline = true;
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(85.85323F, 18F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseForeColor = false;
            this.xrLabel7.Text = "Observaciones:";
            // 
            // xrLabelAge
            // 
            this.xrLabelAge.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AgentesID]")});
            this.xrLabelAge.LocationFloat = new DevExpress.Utils.PointFloat(545.5219F, 0F);
            this.xrLabelAge.Multiline = true;
            this.xrLabelAge.Name = "xrLabelAge";
            this.xrLabelAge.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelAge.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabelAge.Text = "xrLabelAge";
            this.xrLabelAge.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabelAge.Visible = false;
            // 
            // xrLabel41
            // 
            this.xrLabel41.Font = new System.Drawing.Font("Arial", 8.75F);
            this.xrLabel41.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(8.000056F, 29.81924F);
            this.xrLabel41.Multiline = true;
            this.xrLabel41.Name = "xrLabel41";
            this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel41.SizeF = new System.Drawing.SizeF(85.85323F, 18F);
            this.xrLabel41.StylePriority.UseFont = false;
            this.xrLabel41.StylePriority.UseForeColor = false;
            this.xrLabel41.Text = "Empresa:";
            // 
            // xrLabel5
            // 
            this.xrLabel5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Cliente]")});
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 8.75F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(97.52172F, 29.81924F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(594.9008F, 18F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "xrLabel4";
            // 
            // xrLabel24
            // 
            this.xrLabel24.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ClientesID]")});
            this.xrLabel24.Font = new System.Drawing.Font("Arial", 8.75F);
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(692.4225F, 29.81924F);
            this.xrLabel24.Multiline = true;
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(42.70832F, 23F);
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.Text = "xrLabel6";
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(7.36618F, 0F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(127.7632F, 23F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Vendido a:";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel26
            // 
            this.xrLabel26.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Observaciones]")});
            this.xrLabel26.Font = new System.Drawing.Font("Arial", 8F);
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(6.00001F, 95.41672F);
            this.xrLabel26.Multiline = true;
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(732.0303F, 30.50003F);
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.Text = "Notas: ";
            // 
            // xrLabel6
            // 
            this.xrLabel6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EmpTel]")});
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 8.75F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(97.52172F, 52.81925F);
            this.xrLabel6.Multiline = true;
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(594.9008F, 18F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "xrLabel4";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 8.75F);
            this.xrLabel3.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(8.000056F, 52.81925F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(85.85323F, 18F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseForeColor = false;
            this.xrLabel3.Text = "Telefono:";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.table3});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            // 
            // table3
            // 
            this.table3.LocationFloat = new DevExpress.Utils.PointFloat(45.00005F, 0F);
            this.table3.Name = "table3";
            this.table3.OddStyleName = "DetailData3_Odd";
            this.table3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.tableRow3});
            this.table3.SizeF = new System.Drawing.SizeF(704.9999F, 25F);
            // 
            // tableRow3
            // 
            this.tableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tableCell24,
            this.tableCell25,
            this.tableCell26});
            this.tableRow3.Name = "tableRow3";
            this.tableRow3.Weight = 11.5D;
            // 
            // tableCell24
            // 
            this.tableCell24.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CantidadComponente]")});
            this.tableCell24.Name = "tableCell24";
            this.tableCell24.StyleName = "DetailData1";
            this.tableCell24.TextFormatString = "{0:#,#0.00;;\"\"}";
            this.tableCell24.Weight = 0.096912052808905427D;
            // 
            // tableCell25
            // 
            this.tableCell25.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Componente]")});
            this.tableCell25.Name = "tableCell25";
            this.tableCell25.StyleName = "DetailData1";
            this.tableCell25.StylePriority.UseTextAlignment = false;
            this.tableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tableCell25.TextFormatString = "{0:C2}";
            this.tableCell25.Weight = 0.46563598380009913D;
            // 
            // tableCell26
            // 
            this.tableCell26.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EspecificacionComppnente]")});
            this.tableCell26.Name = "tableCell26";
            this.tableCell26.StyleName = "DetailData1";
            this.tableCell26.Weight = 0.37745193645069625D;
            // 
            // parameter1
            // 
            this.parameter1.Name = "parameter1";
            // 
            // parameter2
            // 
            this.parameter2.Name = "parameter2";
            this.parameter2.Type = typeof(int);
            this.parameter2.ValueInfo = "0";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.PageHeader.HeightF = 179.7916F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrPanel1
            // 
            this.xrPanel1.BackColor = System.Drawing.Color.LightGray;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBoxLogo,
            this.xrLabelEmpPH,
            this.xrLabel36,
            this.xrLabel37,
            this.xrLabel38,
            this.xrLabel39,
            this.xrLabel2});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(750F, 169.7916F);
            this.xrPanel1.StylePriority.UseBackColor = false;
            this.xrPanel1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPanel1_BeforePrint);
            // 
            // xrPictureBoxLogo
            // 
            this.xrPictureBoxLogo.ImageUrl = "C:\\Users\\Programador1\\Desktop\\VS chico.png";
            this.xrPictureBoxLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 27.6944F);
            this.xrPictureBoxLogo.Name = "xrPictureBoxLogo";
            this.xrPictureBoxLogo.SizeF = new System.Drawing.SizeF(137.1293F, 109.9999F);
            this.xrPictureBoxLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBoxLogo.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBoxLogo_BeforePrint);
            // 
            // xrLabelEmpPH
            // 
            this.xrLabelEmpPH.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EmpNom]")});
            this.xrLabelEmpPH.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelEmpPH.LocationFloat = new DevExpress.Utils.PointFloat(151.605F, 54.16663F);
            this.xrLabelEmpPH.Multiline = true;
            this.xrLabelEmpPH.Name = "xrLabelEmpPH";
            this.xrLabelEmpPH.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelEmpPH.SizeF = new System.Drawing.SizeF(390.5889F, 23F);
            this.xrLabelEmpPH.StylePriority.UseFont = false;
            this.xrLabelEmpPH.StylePriority.UseTextAlignment = false;
            this.xrLabelEmpPH.Text = "xrLabelEmp";
            this.xrLabelEmpPH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLabelEmpPH.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabelEmpPH_BeforePrint);
            // 
            // xrLabel36
            // 
            this.xrLabel36.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel36.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(584.6687F, 114.6943F);
            this.xrLabel36.Multiline = true;
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel36.SizeF = new System.Drawing.SizeF(60.85324F, 23F);
            this.xrLabel36.StylePriority.UseFont = false;
            this.xrLabel36.StylePriority.UseForeColor = false;
            this.xrLabel36.Text = "Folio";
            // 
            // xrLabel37
            // 
            this.xrLabel37.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SerieyFolio]")});
            this.xrLabel37.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel37.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(645.5219F, 114.6943F);
            this.xrLabel37.Multiline = true;
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel37.SizeF = new System.Drawing.SizeF(104.4781F, 23F);
            this.xrLabel37.StylePriority.UseFont = false;
            this.xrLabel37.StylePriority.UseForeColor = false;
            this.xrLabel37.StylePriority.UseTextAlignment = false;
            this.xrLabel37.Text = "xrLabel37";
            this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel38
            // 
            this.xrLabel38.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel38.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(584.6688F, 137.6943F);
            this.xrLabel38.Multiline = true;
            this.xrLabel38.Name = "xrLabel38";
            this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel38.SizeF = new System.Drawing.SizeF(60.85324F, 23F);
            this.xrLabel38.StylePriority.UseFont = false;
            this.xrLabel38.StylePriority.UseForeColor = false;
            this.xrLabel38.Text = "Fecha";
            // 
            // xrLabel39
            // 
            this.xrLabel39.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Fecha]")});
            this.xrLabel39.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel39.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(648.0001F, 137.6943F);
            this.xrLabel39.Multiline = true;
            this.xrLabel39.Name = "xrLabel39";
            this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel39.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel39.StylePriority.UseFont = false;
            this.xrLabel39.StylePriority.UseForeColor = false;
            this.xrLabel39.StylePriority.UseTextAlignment = false;
            this.xrLabel39.Text = "xrLabel39";
            this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel39.TextFormatString = "{0:d}";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(412.8334F, 77.16662F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.SizeF = new System.Drawing.SizeF(337.1666F, 37.52767F);
            this.xrLabel2.StyleName = "Title";
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "P E D I D O";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.table1});
            this.GroupHeader3.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Seq", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("Kit", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader3.HeightF = 52.7917F;
            this.GroupHeader3.Name = "GroupHeader3";
            // 
            // xrLabel1
            // 
            this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Especificaciones]")});
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 26.99999F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(750F, 23F);
            this.xrLabel1.Text = "xrLabel1";
            // 
            // table1
            // 
            this.table1.LocationFloat = new DevExpress.Utils.PointFloat(2.288818E-05F, 1.999995F);
            this.table1.Name = "table1";
            this.table1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.tableRow1});
            this.table1.SizeF = new System.Drawing.SizeF(750F, 25F);
            // 
            // tableRow1
            // 
            this.tableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2});
            this.tableRow1.Name = "tableRow1";
            this.tableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Cantidad]")});
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StyleName = "GroupData1";
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.TextFormatString = "{0:#,#}";
            this.xrTableCell1.Weight = 0.43674931019430607D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Kit]")});
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StyleName = "GroupData1";
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.Weight = 2.7553940916808788D;
            // 
            // PedidosFormatoImpresionKits
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader2,
            this.Detail,
            this.PageHeader,
            this.GroupHeader3});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "PedidosImpresionFormatokits";
            this.DataSource = this.sqlDataSource1;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.parameter1,
            this.parameter2});
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.GroupCaption1,
            this.GroupData1,
            this.DetailCaption1,
            this.DetailData1,
            this.GroupFooterBackground3,
            this.DetailData3_Odd,
            this.PageInfo});
            this.Version = "19.1";
            ((System.ComponentModel.ISupportInitialize)(this.table3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRControlStyle Title;
        private DevExpress.XtraReports.UI.XRControlStyle GroupCaption1;
        private DevExpress.XtraReports.UI.XRControlStyle GroupData1;
        private DevExpress.XtraReports.UI.XRControlStyle DetailCaption1;
        private DevExpress.XtraReports.UI.XRControlStyle DetailData1;
        private DevExpress.XtraReports.UI.XRControlStyle GroupFooterBackground3;
        private DevExpress.XtraReports.UI.XRControlStyle DetailData3_Odd;
        private DevExpress.XtraReports.UI.XRControlStyle PageInfo;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRPageInfo pageInfo1;
        private DevExpress.XtraReports.UI.XRPageInfo pageInfo2;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRTable table3;
        private DevExpress.XtraReports.UI.XRTableRow tableRow3;
        private DevExpress.XtraReports.UI.XRTableCell tableCell24;
        private DevExpress.XtraReports.UI.XRTableCell tableCell25;
        private DevExpress.XtraReports.UI.XRTableCell tableCell26;
        private DevExpress.XtraReports.Parameters.Parameter parameter1;
        private DevExpress.XtraReports.Parameters.Parameter parameter2;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBoxLogo;
        private DevExpress.XtraReports.UI.XRLabel xrLabelEmpPH;
        private DevExpress.XtraReports.UI.XRLabel xrLabel36;
        private DevExpress.XtraReports.UI.XRLabel xrLabel37;
        private DevExpress.XtraReports.UI.XRLabel xrLabel38;
        private DevExpress.XtraReports.UI.XRLabel xrLabel39;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRTable table1;
        private DevExpress.XtraReports.UI.XRTableRow tableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabelAge;
        private DevExpress.XtraReports.UI.XRLabel xrLabel41;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel24;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel26;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
    }
}
