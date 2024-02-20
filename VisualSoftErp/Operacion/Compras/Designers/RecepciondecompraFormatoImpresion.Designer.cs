namespace VisualSoftErp.Operacion.Compras.Designers
{
    partial class RecepciondecompraFormatoImpresion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecepciondecompraFormatoImpresion));
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailCaption1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailData1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailData3_Odd = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.pageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.pageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.table1 = new DevExpress.XtraReports.UI.XRTable();
            this.tableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.table2 = new DevExpress.XtraReports.UI.XRTable();
            this.tableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.parameter1 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter2 = new DevExpress.XtraReports.Parameters.Parameter();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.LblEmail = new DevExpress.XtraReports.UI.XRLabel();
            this.LblFecha = new DevExpress.XtraReports.UI.XRLabel();
            this.LblNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.LblMoneda = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.label1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBoxLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabelPagina = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelDir = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelEmp = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelTel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelCorreo = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.table2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "VisualSoftErpConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "RecepciondemercanciaFormatoImpresion";
            queryParameter1.Name = "@prmSerie";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("?parameter1", typeof(string));
            queryParameter2.Name = "@prmFolio";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("?parameter2", typeof(int));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.StoredProcName = "RecepciondemercanciaFormatoImpresion";
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
            this.Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.Title.Name = "Title";
            // 
            // DetailCaption1
            // 
            this.DetailCaption1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
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
            // DetailData3_Odd
            // 
            this.DetailData3_Odd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
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
            this.PageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
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
            this.pageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(6F, 6F);
            this.pageInfo1.Name = "pageInfo1";
            this.pageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.pageInfo1.SizeF = new System.Drawing.SizeF(363F, 23F);
            this.pageInfo1.StyleName = "PageInfo";
            // 
            // pageInfo2
            // 
            this.pageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(381F, 6F);
            this.pageInfo2.Name = "pageInfo2";
            this.pageInfo2.SizeF = new System.Drawing.SizeF(363F, 23F);
            this.pageInfo2.StyleName = "PageInfo";
            this.pageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.pageInfo2.TextFormatString = "Page {0} of {1}";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.table1});
            this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
            this.GroupHeader1.HeightF = 28F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // table1
            // 
            this.table1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.table1.Name = "table1";
            this.table1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.tableRow1});
            this.table1.SizeF = new System.Drawing.SizeF(750F, 28F);
            // 
            // tableRow1
            // 
            this.tableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tableCell8,
            this.tableCell9,
            this.tableCell10});
            this.tableRow1.Name = "tableRow1";
            this.tableRow1.Weight = 1D;
            // 
            // tableCell8
            // 
            this.tableCell8.BackColor = System.Drawing.Color.Transparent;
            this.tableCell8.BorderColor = System.Drawing.Color.Black;
            this.tableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tableCell8.ForeColor = System.Drawing.Color.Black;
            this.tableCell8.Name = "tableCell8";
            this.tableCell8.StyleName = "DetailCaption1";
            this.tableCell8.StylePriority.UseBackColor = false;
            this.tableCell8.StylePriority.UseBorderColor = false;
            this.tableCell8.StylePriority.UseBorders = false;
            this.tableCell8.StylePriority.UseForeColor = false;
            this.tableCell8.Text = "Articulo";
            this.tableCell8.Weight = 0.29264720257653154D;
            // 
            // tableCell9
            // 
            this.tableCell9.BackColor = System.Drawing.Color.Transparent;
            this.tableCell9.BorderColor = System.Drawing.Color.Black;
            this.tableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tableCell9.ForeColor = System.Drawing.Color.Black;
            this.tableCell9.Name = "tableCell9";
            this.tableCell9.StyleName = "DetailCaption1";
            this.tableCell9.StylePriority.UseBackColor = false;
            this.tableCell9.StylePriority.UseBorderColor = false;
            this.tableCell9.StylePriority.UseBorders = false;
            this.tableCell9.StylePriority.UseForeColor = false;
            this.tableCell9.Text = "Descripcion";
            this.tableCell9.Weight = 0.4566121399137707D;
            // 
            // tableCell10
            // 
            this.tableCell10.BackColor = System.Drawing.Color.Transparent;
            this.tableCell10.BorderColor = System.Drawing.Color.Black;
            this.tableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tableCell10.ForeColor = System.Drawing.Color.Black;
            this.tableCell10.Name = "tableCell10";
            this.tableCell10.StyleName = "DetailCaption1";
            this.tableCell10.StylePriority.UseBackColor = false;
            this.tableCell10.StylePriority.UseBorderColor = false;
            this.tableCell10.StylePriority.UseBorders = false;
            this.tableCell10.StylePriority.UseForeColor = false;
            this.tableCell10.StylePriority.UseTextAlignment = false;
            this.tableCell10.Text = "Recibido";
            this.tableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tableCell10.Weight = 0.2507406524234348D;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.table2});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            // 
            // table2
            // 
            this.table2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.table2.Name = "table2";
            this.table2.OddStyleName = "DetailData3_Odd";
            this.table2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.tableRow2});
            this.table2.SizeF = new System.Drawing.SizeF(750F, 25F);
            // 
            // tableRow2
            // 
            this.tableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tableCell23,
            this.tableCell24,
            this.tableCell25});
            this.tableRow2.Name = "tableRow2";
            this.tableRow2.Weight = 11.5D;
            // 
            // tableCell23
            // 
            this.tableCell23.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Articulo]")});
            this.tableCell23.Name = "tableCell23";
            this.tableCell23.StyleName = "DetailData1";
            this.tableCell23.Weight = 0.292647221883138D;
            // 
            // tableCell24
            // 
            this.tableCell24.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomArt]")});
            this.tableCell24.Name = "tableCell24";
            this.tableCell24.StyleName = "DetailData1";
            this.tableCell24.Weight = 0.45661212158203124D;
            // 
            // tableCell25
            // 
            this.tableCell25.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Cantidad]")});
            this.tableCell25.Name = "tableCell25";
            this.tableCell25.StyleName = "DetailData1";
            this.tableCell25.StylePriority.UseTextAlignment = false;
            this.tableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tableCell25.TextFormatString = "{0:n}";
            this.tableCell25.Weight = 0.25074065653483069D;
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
            this.PageHeader.HeightF = 286.2501F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrPanel1
            // 
            this.xrPanel1.BackColor = System.Drawing.Color.LightGray;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.LblEmail,
            this.LblFecha,
            this.LblNombre,
            this.LblMoneda,
            this.xrLabel37,
            this.xrLabel39,
            this.xrLabel3,
            this.xrLabel7,
            this.label1,
            this.xrPictureBoxLogo,
            this.xrLabelPagina,
            this.xrLabelDir,
            this.xrLabelEmp,
            this.xrLabelTel,
            this.xrLabelCorreo});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(750F, 286.2501F);
            this.xrPanel1.StylePriority.UseBackColor = false;
            // 
            // xrLabel8
            // 
            this.xrLabel8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Status]")});
            this.xrLabel8.Font = new System.Drawing.Font("Arial", 14F);
            this.xrLabel8.ForeColor = System.Drawing.Color.Red;
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(474.1667F, 130.6249F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(275.8333F, 23F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseForeColor = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "xrLabel8";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel6
            // 
            this.xrLabel6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Proveedor]")});
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(84.60741F, 206.75F);
            this.xrLabel6.Multiline = true;
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(665.3926F, 23F);
            this.xrLabel6.Text = "xrLabel6";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.ForeColor = System.Drawing.Color.DimGray;
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(10F, 206.75F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(74.60742F, 23F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseForeColor = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Proveedor:";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(10F, 161.7918F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(422.1135F, 21.95822F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Datos de la órde de compra";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LblEmail
            // 
            this.LblEmail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.LblEmail.LocationFloat = new DevExpress.Utils.PointFloat(6.000048F, 254.2919F);
            this.LblEmail.Multiline = true;
            this.LblEmail.Name = "LblEmail";
            this.LblEmail.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LblEmail.SizeF = new System.Drawing.SizeF(108.3574F, 21.95822F);
            this.LblEmail.StylePriority.UseFont = false;
            this.LblEmail.StylePriority.UseTextAlignment = false;
            this.LblEmail.Text = "Observaciones:";
            this.LblEmail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LblFecha
            // 
            this.LblFecha.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.LblFecha.LocationFloat = new DevExpress.Utils.PointFloat(542.2435F, 69.50007F);
            this.LblFecha.Multiline = true;
            this.LblFecha.Name = "LblFecha";
            this.LblFecha.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LblFecha.SizeF = new System.Drawing.SizeF(78.60733F, 21.95822F);
            this.LblFecha.StylePriority.UseFont = false;
            this.LblFecha.StylePriority.UseTextAlignment = false;
            this.LblFecha.Text = "Fecha:";
            this.LblFecha.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LblNombre
            // 
            this.LblNombre.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.LblNombre.ForeColor = System.Drawing.Color.DimGray;
            this.LblNombre.LocationFloat = new DevExpress.Utils.PointFloat(10F, 183.75F);
            this.LblNombre.Multiline = true;
            this.LblNombre.Name = "LblNombre";
            this.LblNombre.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LblNombre.SizeF = new System.Drawing.SizeF(74.60742F, 23F);
            this.LblNombre.StylePriority.UseFont = false;
            this.LblNombre.StylePriority.UseForeColor = false;
            this.LblNombre.StylePriority.UseTextAlignment = false;
            this.LblNombre.Text = "Folio: ";
            this.LblNombre.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // LblMoneda
            // 
            this.LblMoneda.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.LblMoneda.LocationFloat = new DevExpress.Utils.PointFloat(542.2435F, 46.50006F);
            this.LblMoneda.Multiline = true;
            this.LblMoneda.Name = "LblMoneda";
            this.LblMoneda.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LblMoneda.SizeF = new System.Drawing.SizeF(78.6073F, 23.00001F);
            this.LblMoneda.StylePriority.UseFont = false;
            this.LblMoneda.StylePriority.UseTextAlignment = false;
            this.LblMoneda.Text = "Serie/Folio:";
            this.LblMoneda.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel37
            // 
            this.xrLabel37.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[OC]")});
            this.xrLabel37.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel37.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(84.60741F, 183.75F);
            this.xrLabel37.Multiline = true;
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel37.SizeF = new System.Drawing.SizeF(119.1492F, 23F);
            this.xrLabel37.StylePriority.UseFont = false;
            this.xrLabel37.StylePriority.UseForeColor = false;
            this.xrLabel37.StylePriority.UseTextAlignment = false;
            this.xrLabel37.Text = "xrLabel37";
            this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel39
            // 
            this.xrLabel39.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Fecha]")});
            this.xrLabel39.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel39.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(620.8508F, 69.50007F);
            this.xrLabel39.Multiline = true;
            this.xrLabel39.Name = "xrLabel39";
            this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel39.SizeF = new System.Drawing.SizeF(123.1493F, 23.00001F);
            this.xrLabel39.StylePriority.UseFont = false;
            this.xrLabel39.StylePriority.UseForeColor = false;
            this.xrLabel39.StylePriority.UseTextAlignment = false;
            this.xrLabel39.Text = "xrLabel39";
            this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrLabel39.TextFormatString = "{0:dd/MMM/yyyy}";
            // 
            // xrLabel3
            // 
            this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Observaciones]")});
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(114.3575F, 254.2918F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(625.6426F, 21.95828F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseForeColor = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "xrLabel39";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrLabel3.TextFormatString = "{0:d}";
            // 
            // xrLabel7
            // 
            this.xrLabel7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SerieFolio]")});
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(620.8508F, 46.50006F);
            this.xrLabel7.Multiline = true;
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(123.1492F, 23F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseForeColor = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "xrLabel37";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.label1.LocationFloat = new DevExpress.Utils.PointFloat(270.4712F, 0F);
            this.label1.Name = "label1";
            this.label1.SizeF = new System.Drawing.SizeF(469.5288F, 37.52767F);
            this.label1.StyleName = "Title";
            this.label1.StylePriority.UseFont = false;
            this.label1.StylePriority.UseTextAlignment = false;
            this.label1.Text = "RECEPCION DE MERCANCIA";
            this.label1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrPictureBoxLogo
            // 
            this.xrPictureBoxLogo.ImageUrl = "C:\\Users\\Programador1\\Desktop\\VS chico.png";
            this.xrPictureBoxLogo.LocationFloat = new DevExpress.Utils.PointFloat(8.95834F, 40.625F);
            this.xrPictureBoxLogo.Name = "xrPictureBoxLogo";
            this.xrPictureBoxLogo.SizeF = new System.Drawing.SizeF(164.2128F, 109.9999F);
            this.xrPictureBoxLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBoxLogo.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBoxLogo_BeforePrint);
            // 
            // xrLabelPagina
            // 
            this.xrLabelPagina.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[pagina]")});
            this.xrLabelPagina.Font = new System.Drawing.Font("Arial", 8.25F);
            this.xrLabelPagina.LocationFloat = new DevExpress.Utils.PointFloat(173.1712F, 130.6249F);
            this.xrLabelPagina.Multiline = true;
            this.xrLabelPagina.Name = "xrLabelPagina";
            this.xrLabelPagina.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelPagina.SizeF = new System.Drawing.SizeF(258.9423F, 20F);
            this.xrLabelPagina.StylePriority.UseFont = false;
            this.xrLabelPagina.StylePriority.UseTextAlignment = false;
            this.xrLabelPagina.Text = "xrLabel35";
            this.xrLabelPagina.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrLabelPagina.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabelPagina_BeforePrint);
            // 
            // xrLabelDir
            // 
            this.xrLabelDir.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[empDireccion]")});
            this.xrLabelDir.Font = new System.Drawing.Font("Arial", 8.25F);
            this.xrLabelDir.LocationFloat = new DevExpress.Utils.PointFloat(173.1712F, 63.62502F);
            this.xrLabelDir.Multiline = true;
            this.xrLabelDir.Name = "xrLabelDir";
            this.xrLabelDir.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelDir.SizeF = new System.Drawing.SizeF(258.9424F, 20F);
            this.xrLabelDir.StylePriority.UseFont = false;
            this.xrLabelDir.StylePriority.UseTextAlignment = false;
            this.xrLabelDir.Text = "xrLabelDir";
            this.xrLabelDir.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrLabelDir.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabelDir_BeforePrint);
            // 
            // xrLabelEmp
            // 
            this.xrLabelEmp.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[empNombre]")});
            this.xrLabelEmp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabelEmp.LocationFloat = new DevExpress.Utils.PointFloat(173.1712F, 40.625F);
            this.xrLabelEmp.Multiline = true;
            this.xrLabelEmp.Name = "xrLabelEmp";
            this.xrLabelEmp.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelEmp.SizeF = new System.Drawing.SizeF(258.9423F, 23F);
            this.xrLabelEmp.StylePriority.UseFont = false;
            this.xrLabelEmp.StylePriority.UseTextAlignment = false;
            this.xrLabelEmp.Text = "xrLabelEmp";
            this.xrLabelEmp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLabelEmp.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabelEmp_BeforePrint);
            // 
            // xrLabelTel
            // 
            this.xrLabelTel.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[empTelefono]")});
            this.xrLabelTel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.xrLabelTel.LocationFloat = new DevExpress.Utils.PointFloat(173.1712F, 87.81938F);
            this.xrLabelTel.Multiline = true;
            this.xrLabelTel.Name = "xrLabelTel";
            this.xrLabelTel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelTel.SizeF = new System.Drawing.SizeF(258.9424F, 20F);
            this.xrLabelTel.StylePriority.UseFont = false;
            this.xrLabelTel.StylePriority.UseTextAlignment = false;
            this.xrLabelTel.Text = "xrLabelTel";
            this.xrLabelTel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrLabelTel.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabelTel_BeforePrint);
            // 
            // xrLabelCorreo
            // 
            this.xrLabelCorreo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Correo]")});
            this.xrLabelCorreo.Font = new System.Drawing.Font("Arial", 8.25F);
            this.xrLabelCorreo.LocationFloat = new DevExpress.Utils.PointFloat(173.1712F, 110.625F);
            this.xrLabelCorreo.Multiline = true;
            this.xrLabelCorreo.Name = "xrLabelCorreo";
            this.xrLabelCorreo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelCorreo.SizeF = new System.Drawing.SizeF(258.9423F, 20F);
            this.xrLabelCorreo.StylePriority.UseFont = false;
            this.xrLabelCorreo.StylePriority.UseTextAlignment = false;
            this.xrLabelCorreo.Text = "xrLabel35";
            this.xrLabelCorreo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrLabelCorreo.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabelCorreo_BeforePrint);
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel2});
            this.PageFooter.Name = "PageFooter";
            // 
            // xrLabel1
            // 
            this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomUsuario]")});
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 47.91667F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(744.0001F, 21.95823F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Nombre del Usuario";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(310.9689F, 25.95838F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(125.3114F, 21.95828F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseForeColor = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Recibí Mercancía";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel2.TextFormatString = "{0:d}";
            // 
            // RecepciondecompraFormatoImpresion
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.Detail,
            this.PageHeader,
            this.PageFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "RecepciondemercanciaFormatoImpresion";
            this.DataSource = this.sqlDataSource1;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.parameter1,
            this.parameter2});
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.DetailCaption1,
            this.DetailData1,
            this.DetailData3_Odd,
            this.PageInfo});
            this.Version = "19.1";
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.table2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRControlStyle Title;
        private DevExpress.XtraReports.UI.XRControlStyle DetailCaption1;
        private DevExpress.XtraReports.UI.XRControlStyle DetailData1;
        private DevExpress.XtraReports.UI.XRControlStyle DetailData3_Odd;
        private DevExpress.XtraReports.UI.XRControlStyle PageInfo;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRPageInfo pageInfo1;
        private DevExpress.XtraReports.UI.XRPageInfo pageInfo2;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable table1;
        private DevExpress.XtraReports.UI.XRTableRow tableRow1;
        private DevExpress.XtraReports.UI.XRTableCell tableCell8;
        private DevExpress.XtraReports.UI.XRTableCell tableCell9;
        private DevExpress.XtraReports.UI.XRTableCell tableCell10;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRTable table2;
        private DevExpress.XtraReports.UI.XRTableRow tableRow2;
        private DevExpress.XtraReports.UI.XRTableCell tableCell23;
        private DevExpress.XtraReports.UI.XRTableCell tableCell24;
        private DevExpress.XtraReports.UI.XRTableCell tableCell25;
        private DevExpress.XtraReports.Parameters.Parameter parameter1;
        private DevExpress.XtraReports.Parameters.Parameter parameter2;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBoxLogo;
        private DevExpress.XtraReports.UI.XRLabel xrLabelPagina;
        private DevExpress.XtraReports.UI.XRLabel xrLabelDir;
        private DevExpress.XtraReports.UI.XRLabel xrLabelEmp;
        private DevExpress.XtraReports.UI.XRLabel xrLabelTel;
        private DevExpress.XtraReports.UI.XRLabel xrLabelCorreo;
        private DevExpress.XtraReports.UI.XRLabel label1;
        private DevExpress.XtraReports.UI.XRLabel LblEmail;
        private DevExpress.XtraReports.UI.XRLabel LblFecha;
        private DevExpress.XtraReports.UI.XRLabel LblNombre;
        private DevExpress.XtraReports.UI.XRLabel LblMoneda;
        private DevExpress.XtraReports.UI.XRLabel xrLabel37;
        private DevExpress.XtraReports.UI.XRLabel xrLabel39;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
    }
}
