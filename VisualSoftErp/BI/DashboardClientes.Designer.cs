namespace VisualSoftErp.BI
{
    partial class DashboardClientes
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.DashboardCommon.Dimension dimension1 = new DevExpress.DashboardCommon.Dimension();
            DevExpress.DashboardCommon.Measure measure1 = new DevExpress.DashboardCommon.Measure();
            DevExpress.DashboardCommon.Measure measure2 = new DevExpress.DashboardCommon.Measure();
            DevExpress.DashboardCommon.ChartPane chartPane1 = new DevExpress.DashboardCommon.ChartPane();
            DevExpress.DashboardCommon.SimpleSeries simpleSeries1 = new DevExpress.DashboardCommon.SimpleSeries();
            DevExpress.DashboardCommon.SimpleSeries simpleSeries2 = new DevExpress.DashboardCommon.SimpleSeries();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardClientes));
            DevExpress.DashboardCommon.Dimension dimension2 = new DevExpress.DashboardCommon.Dimension();
            DevExpress.DashboardCommon.Measure measure3 = new DevExpress.DashboardCommon.Measure();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery2 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DashboardCommon.Measure measure4 = new DevExpress.DashboardCommon.Measure();
            DevExpress.DashboardCommon.Measure measure5 = new DevExpress.DashboardCommon.Measure();
            DevExpress.DashboardCommon.Gauge gauge1 = new DevExpress.DashboardCommon.Gauge();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery3 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter6 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery4 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter7 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DashboardCommon.DashboardLayoutGroup dashboardLayoutGroup1 = new DevExpress.DashboardCommon.DashboardLayoutGroup();
            DevExpress.DashboardCommon.DashboardLayoutItem dashboardLayoutItem1 = new DevExpress.DashboardCommon.DashboardLayoutItem();
            DevExpress.DashboardCommon.DashboardLayoutGroup dashboardLayoutGroup2 = new DevExpress.DashboardCommon.DashboardLayoutGroup();
            DevExpress.DashboardCommon.DashboardLayoutItem dashboardLayoutItem2 = new DevExpress.DashboardCommon.DashboardLayoutItem();
            DevExpress.DashboardCommon.DashboardLayoutItem dashboardLayoutItem3 = new DevExpress.DashboardCommon.DashboardLayoutItem();
            DevExpress.DashboardCommon.DashboardParameter dashboardParameter1 = new DevExpress.DashboardCommon.DashboardParameter();
            DevExpress.DashboardCommon.DashboardParameter dashboardParameter2 = new DevExpress.DashboardCommon.DashboardParameter();
            DevExpress.DashboardCommon.DashboardParameter dashboardParameter3 = new DevExpress.DashboardCommon.DashboardParameter();
            this.chartDashboardItem1 = new DevExpress.DashboardCommon.ChartDashboardItem();
            this.dashboardSqlDataSource1 = new DevExpress.DashboardCommon.DashboardSqlDataSource();
            this.pieDashboardItem1 = new DevExpress.DashboardCommon.PieDashboardItem();
            this.dashboardSqlDataSource3 = new DevExpress.DashboardCommon.DashboardSqlDataSource();
            this.gaugeDashboardItem1 = new DevExpress.DashboardCommon.GaugeDashboardItem();
            this.dashboardSqlDataSource4 = new DevExpress.DashboardCommon.DashboardSqlDataSource();
            this.dashboardSqlDataSource2 = new DevExpress.DashboardCommon.DashboardSqlDataSource();
            ((System.ComponentModel.ISupportInitialize)(this.chartDashboardItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dimension1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(measure1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(measure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardSqlDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieDashboardItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dimension2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(measure3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardSqlDataSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gaugeDashboardItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(measure4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(measure5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardSqlDataSource4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardSqlDataSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // chartDashboardItem1
            // 
            dimension1.DataMember = "Mes";
            dimension1.TextGroupInterval = DevExpress.DashboardCommon.TextGroupInterval.Alphabetical;
            this.chartDashboardItem1.Arguments.AddRange(new DevExpress.DashboardCommon.Dimension[] {
            dimension1});
            this.chartDashboardItem1.AxisX.TitleVisible = false;
            this.chartDashboardItem1.ComponentName = "chartDashboardItem1";
            measure1.DataMember = "Ppto";
            measure2.DataMember = "Venta";
            this.chartDashboardItem1.DataItemRepository.Clear();
            this.chartDashboardItem1.DataItemRepository.Add(measure1, "DataItem0");
            this.chartDashboardItem1.DataItemRepository.Add(measure2, "DataItem1");
            this.chartDashboardItem1.DataItemRepository.Add(dimension1, "DataItem2");
            this.chartDashboardItem1.DataMember = "DashBoardVentasPorCliente";
            this.chartDashboardItem1.DataSource = this.dashboardSqlDataSource1;
            this.chartDashboardItem1.InteractivityOptions.IgnoreMasterFilters = false;
            this.chartDashboardItem1.Name = "Chart 1";
            chartPane1.Name = "Pane 1";
            chartPane1.PrimaryAxisY.AlwaysShowZeroLevel = true;
            chartPane1.PrimaryAxisY.ShowGridLines = true;
            chartPane1.PrimaryAxisY.TitleVisible = true;
            chartPane1.SecondaryAxisY.AlwaysShowZeroLevel = true;
            chartPane1.SecondaryAxisY.ShowGridLines = false;
            chartPane1.SecondaryAxisY.TitleVisible = true;
            simpleSeries1.AddDataItem("Value", measure1);
            simpleSeries2.AddDataItem("Value", measure2);
            chartPane1.Series.AddRange(new DevExpress.DashboardCommon.ChartSeries[] {
            simpleSeries1,
            simpleSeries2});
            this.chartDashboardItem1.Panes.AddRange(new DevExpress.DashboardCommon.ChartPane[] {
            chartPane1});
            this.chartDashboardItem1.ShowCaption = true;
            // 
            // dashboardSqlDataSource1
            // 
            this.dashboardSqlDataSource1.ComponentName = "dashboardSqlDataSource1";
            this.dashboardSqlDataSource1.ConnectionName = "VisualSoftErpConnectionString";
            this.dashboardSqlDataSource1.Name = "SQL Data Source 1";
            storedProcQuery1.Name = "DashBoardVentasPorCliente";
            queryParameter1.Name = "@prmCte";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("?Cliente", typeof(int));
            queryParameter2.Name = "@prmAño";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("?Año", typeof(int));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.StoredProcName = "DashBoardVentasPorCliente";
            this.dashboardSqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.dashboardSqlDataSource1.ResultSchemaSerializable = resources.GetString("dashboardSqlDataSource1.ResultSchemaSerializable");
            // 
            // pieDashboardItem1
            // 
            dimension2.DataMember = "Articulo";
            this.pieDashboardItem1.Arguments.AddRange(new DevExpress.DashboardCommon.Dimension[] {
            dimension2});
            this.pieDashboardItem1.ComponentName = "pieDashboardItem1";
            measure3.DataMember = "Importe";
            this.pieDashboardItem1.DataItemRepository.Clear();
            this.pieDashboardItem1.DataItemRepository.Add(measure3, "DataItem0");
            this.pieDashboardItem1.DataItemRepository.Add(dimension2, "DataItem1");
            this.pieDashboardItem1.DataMember = "DashboardVentasPorArticulo";
            this.pieDashboardItem1.DataSource = this.dashboardSqlDataSource3;
            this.pieDashboardItem1.InteractivityOptions.IgnoreMasterFilters = false;
            this.pieDashboardItem1.Name = "Ventas por artículo";
            this.pieDashboardItem1.ShowCaption = true;
            this.pieDashboardItem1.Values.AddRange(new DevExpress.DashboardCommon.Measure[] {
            measure3});
            // 
            // dashboardSqlDataSource3
            // 
            this.dashboardSqlDataSource3.ComponentName = "dashboardSqlDataSource3";
            this.dashboardSqlDataSource3.ConnectionName = "VisualSoftErpConnectionString";
            this.dashboardSqlDataSource3.Name = "SQL Data Source 3";
            storedProcQuery2.Name = "DashboardVentasPorArticulo";
            queryParameter3.Name = "@prmCte";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("?Cliente", typeof(int));
            queryParameter4.Name = "@prmAño";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("?Año", typeof(int));
            storedProcQuery2.Parameters.Add(queryParameter3);
            storedProcQuery2.Parameters.Add(queryParameter4);
            storedProcQuery2.StoredProcName = "DashboardVentasPorArticulo";
            this.dashboardSqlDataSource3.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery2});
            this.dashboardSqlDataSource3.ResultSchemaSerializable = resources.GetString("dashboardSqlDataSource3.ResultSchemaSerializable");
            // 
            // gaugeDashboardItem1
            // 
            this.gaugeDashboardItem1.ComponentName = "gaugeDashboardItem1";
            measure4.DataMember = "Venta";
            measure5.DataMember = "Ppto";
            this.gaugeDashboardItem1.DataItemRepository.Clear();
            this.gaugeDashboardItem1.DataItemRepository.Add(measure4, "DataItem0");
            this.gaugeDashboardItem1.DataItemRepository.Add(measure5, "DataItem1");
            this.gaugeDashboardItem1.DataMember = "DashBoardGaugePorCliente";
            this.gaugeDashboardItem1.DataSource = this.dashboardSqlDataSource4;
            gauge1.AddDataItem("ActualValue", measure4);
            gauge1.AddDataItem("TargetValue", measure5);
            this.gaugeDashboardItem1.Gauges.AddRange(new DevExpress.DashboardCommon.Gauge[] {
            gauge1});
            this.gaugeDashboardItem1.InteractivityOptions.IgnoreMasterFilters = false;
            this.gaugeDashboardItem1.Name = "Meta anual";
            this.gaugeDashboardItem1.ShowCaption = true;
            // 
            // dashboardSqlDataSource4
            // 
            this.dashboardSqlDataSource4.ComponentName = "dashboardSqlDataSource4";
            this.dashboardSqlDataSource4.ConnectionName = "VisualSoftErpConnectionString";
            this.dashboardSqlDataSource4.Name = "SQL Data Source 4";
            storedProcQuery3.Name = "DashBoardGaugePorCliente";
            queryParameter5.Name = "@prmCte";
            queryParameter5.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter5.Value = new DevExpress.DataAccess.Expression("?Cliente", typeof(int));
            queryParameter6.Name = "@prmAño";
            queryParameter6.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter6.Value = new DevExpress.DataAccess.Expression("?Año", typeof(int));
            storedProcQuery3.Parameters.Add(queryParameter5);
            storedProcQuery3.Parameters.Add(queryParameter6);
            storedProcQuery3.StoredProcName = "DashBoardGaugePorCliente";
            this.dashboardSqlDataSource4.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery3});
            this.dashboardSqlDataSource4.ResultSchemaSerializable = resources.GetString("dashboardSqlDataSource4.ResultSchemaSerializable");
            // 
            // dashboardSqlDataSource2
            // 
            this.dashboardSqlDataSource2.ComponentName = "dashboardSqlDataSource2";
            this.dashboardSqlDataSource2.ConnectionName = "VisualSoftErpConnectionString";
            this.dashboardSqlDataSource2.Name = "SQL Data Source 2";
            storedProcQuery4.Name = "infoClienteVentas";
            queryParameter7.Name = "@prmCte";
            queryParameter7.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter7.Value = new DevExpress.DataAccess.Expression("?Cliente", typeof(int));
            storedProcQuery4.Parameters.Add(queryParameter7);
            storedProcQuery4.StoredProcName = "infoClienteVentas";
            this.dashboardSqlDataSource2.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery4});
            this.dashboardSqlDataSource2.ResultSchemaSerializable = resources.GetString("dashboardSqlDataSource2.ResultSchemaSerializable");
            // 
            // DashboardClientes
            // 
            this.DataSources.AddRange(new DevExpress.DashboardCommon.IDashboardDataSource[] {
            this.dashboardSqlDataSource1,
            this.dashboardSqlDataSource2,
            this.dashboardSqlDataSource3,
            this.dashboardSqlDataSource4});
            this.Items.AddRange(new DevExpress.DashboardCommon.DashboardItem[] {
            this.pieDashboardItem1,
            this.chartDashboardItem1,
            this.gaugeDashboardItem1});
            dashboardLayoutItem1.DashboardItem = this.chartDashboardItem1;
            dashboardLayoutItem1.Weight = 49.934980494148242D;
            dashboardLayoutItem2.DashboardItem = this.pieDashboardItem1;
            dashboardLayoutItem2.Weight = 50D;
            dashboardLayoutItem3.DashboardItem = this.gaugeDashboardItem1;
            dashboardLayoutItem3.Weight = 50D;
            dashboardLayoutGroup2.ChildNodes.AddRange(new DevExpress.DashboardCommon.DashboardLayoutNode[] {
            dashboardLayoutItem2,
            dashboardLayoutItem3});
            dashboardLayoutGroup2.DashboardItem = null;
            dashboardLayoutGroup2.Weight = 50.065019505851758D;
            dashboardLayoutGroup1.ChildNodes.AddRange(new DevExpress.DashboardCommon.DashboardLayoutNode[] {
            dashboardLayoutItem1,
            dashboardLayoutGroup2});
            dashboardLayoutGroup1.DashboardItem = null;
            dashboardLayoutGroup1.Orientation = DevExpress.DashboardCommon.DashboardLayoutGroupOrientation.Vertical;
            dashboardLayoutGroup1.Weight = 100D;
            this.LayoutRoot = dashboardLayoutGroup1;
            dashboardParameter1.Name = "Parameter1";
            dashboardParameter1.Type = typeof(int);
            dashboardParameter1.Value = 0;
            dashboardParameter1.Visible = false;
            dashboardParameter2.Name = "Año";
            dashboardParameter2.Type = typeof(int);
            dashboardParameter2.Value = 0;
            dashboardParameter3.Name = "Cliente";
            dashboardParameter3.Type = typeof(int);
            dashboardParameter3.Value = 0;
            this.Parameters.AddRange(new DevExpress.DashboardCommon.DashboardParameter[] {
            dashboardParameter1,
            dashboardParameter2,
            dashboardParameter3});
            this.Title.Text = "DashBoard Ventas";
            this.ConfigureDataConnection += new DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventHandler(this.DashboardClientes_ConfigureDataConnection);
            ((System.ComponentModel.ISupportInitialize)(dimension1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(measure1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(measure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDashboardItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardSqlDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dimension2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(measure3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieDashboardItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardSqlDataSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(measure4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(measure5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gaugeDashboardItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardSqlDataSource4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardSqlDataSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
        private DevExpress.DashboardCommon.PieDashboardItem pieDashboardItem1;
        private DevExpress.DashboardCommon.DashboardSqlDataSource dashboardSqlDataSource1;
        private DevExpress.DashboardCommon.DashboardSqlDataSource dashboardSqlDataSource2;
        private DevExpress.DashboardCommon.ChartDashboardItem chartDashboardItem1;
        private DevExpress.DashboardCommon.DashboardSqlDataSource dashboardSqlDataSource3;
        private DevExpress.DashboardCommon.GaugeDashboardItem gaugeDashboardItem1;
        private DevExpress.DashboardCommon.DashboardSqlDataSource dashboardSqlDataSource4;
    }
}
