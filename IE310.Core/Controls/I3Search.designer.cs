using IE310.Core.Components;
namespace IE310.Core.Controls
{
    partial class I3Search
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("1111", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("2222", 1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("3333", 0);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("4444", 0);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("5555", 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(I3Search));
            this.iecT_Ini1 = new IE310.Core.Components.I3Ini();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pEqual = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.edEqualString = new System.Windows.Forms.TextBox();
            this.edEqualNum = new System.Windows.Forms.TextBox();
            this.edEqualDate = new System.Windows.Forms.DateTimePicker();
            this.pDim = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.edDimString = new System.Windows.Forms.TextBox();
            this.pInterval = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.edNum1 = new System.Windows.Forms.TextBox();
            this.lbIntervalNum = new System.Windows.Forms.Label();
            this.edNum2 = new System.Windows.Forms.TextBox();
            this.dp = new IE310.Core.Controls.I3DatePanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbSchemeList = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pc = new DevExpress.XtraEditors.PopupContainerControl();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btClear = new System.Windows.Forms.Button();
            this.gridSearch = new DevExpress.XtraGrid.GridControl();
            this.viewSearch = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFieldCaption = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLookString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPopupContainerEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.i3Table = new IE310.Table.Models.I3Table();
            this.i3ColumnModel = new IE310.Table.Models.I3ColumnModel();
            this.i3TextColumn1 = new IE310.Table.Models.I3TextColumn();
            this.i3TextColumn2 = new IE310.Table.Models.I3TextColumn();
            this.i3PopupColumn1 = new IE310.Table.Models.I3PopupColumn();
            this.i3TableModel = new IE310.Table.Models.I3TableModel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btSaveScheme = new System.Windows.Forms.Button();
            this.btReName = new System.Windows.Forms.Button();
            this.btDeleteScheme = new System.Windows.Forms.Button();
            this.btAddScheme = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.pEqual.SuspendLayout();
            this.pDim.SuspendLayout();
            this.pInterval.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pc)).BeginInit();
            this.pc.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.i3Table)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // iecT_Ini1
            // 
            this.iecT_Ini1.Active = false;
            this.iecT_Ini1.CanOnGetValue = false;
            this.iecT_Ini1.CanOnSetValue = false;
            this.iecT_Ini1.FileName = null;
            this.iecT_Ini1.UP = false;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 202);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.pEqual);
            this.tabControl1.Controls.Add(this.pDim);
            this.tabControl1.Controls.Add(this.pInterval);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(493, 143);
            this.tabControl1.TabIndex = 0;
            // 
            // pEqual
            // 
            this.pEqual.Controls.Add(this.label1);
            this.pEqual.Controls.Add(this.edEqualString);
            this.pEqual.Controls.Add(this.edEqualNum);
            this.pEqual.Controls.Add(this.edEqualDate);
            this.pEqual.Location = new System.Drawing.Point(4, 22);
            this.pEqual.Name = "pEqual";
            this.pEqual.Padding = new System.Windows.Forms.Padding(3);
            this.pEqual.Size = new System.Drawing.Size(485, 117);
            this.pEqual.TabIndex = 0;
            this.pEqual.Text = "精确查找";
            this.pEqual.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "查找条件";
            // 
            // edEqualString
            // 
            this.edEqualString.Location = new System.Drawing.Point(76, 14);
            this.edEqualString.Name = "edEqualString";
            this.edEqualString.Size = new System.Drawing.Size(348, 21);
            this.edEqualString.TabIndex = 1;
            // 
            // edEqualNum
            // 
            this.edEqualNum.Location = new System.Drawing.Point(76, 41);
            this.edEqualNum.Name = "edEqualNum";
            this.edEqualNum.Size = new System.Drawing.Size(348, 21);
            this.edEqualNum.TabIndex = 4;
            // 
            // edEqualDate
            // 
            this.edEqualDate.Location = new System.Drawing.Point(76, 68);
            this.edEqualDate.Name = "edEqualDate";
            this.edEqualDate.Size = new System.Drawing.Size(348, 21);
            this.edEqualDate.TabIndex = 3;
            // 
            // pDim
            // 
            this.pDim.Controls.Add(this.label2);
            this.pDim.Controls.Add(this.edDimString);
            this.pDim.Location = new System.Drawing.Point(4, 22);
            this.pDim.Name = "pDim";
            this.pDim.Padding = new System.Windows.Forms.Padding(3);
            this.pDim.Size = new System.Drawing.Size(485, 117);
            this.pDim.TabIndex = 1;
            this.pDim.Text = "模糊查找";
            this.pDim.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "查找条件";
            // 
            // edDimString
            // 
            this.edDimString.Location = new System.Drawing.Point(76, 14);
            this.edDimString.Name = "edDimString";
            this.edDimString.Size = new System.Drawing.Size(348, 21);
            this.edDimString.TabIndex = 3;
            // 
            // pInterval
            // 
            this.pInterval.Controls.Add(this.label3);
            this.pInterval.Controls.Add(this.edNum1);
            this.pInterval.Controls.Add(this.lbIntervalNum);
            this.pInterval.Controls.Add(this.edNum2);
            this.pInterval.Controls.Add(this.dp);
            this.pInterval.Location = new System.Drawing.Point(4, 22);
            this.pInterval.Name = "pInterval";
            this.pInterval.Padding = new System.Windows.Forms.Padding(3);
            this.pInterval.Size = new System.Drawing.Size(485, 117);
            this.pInterval.TabIndex = 2;
            this.pInterval.Text = "区间查找";
            this.pInterval.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "查找条件";
            // 
            // edNum1
            // 
            this.edNum1.Location = new System.Drawing.Point(76, 14);
            this.edNum1.Name = "edNum1";
            this.edNum1.Size = new System.Drawing.Size(92, 21);
            this.edNum1.TabIndex = 8;
            // 
            // lbIntervalNum
            // 
            this.lbIntervalNum.AutoSize = true;
            this.lbIntervalNum.Location = new System.Drawing.Point(179, 19);
            this.lbIntervalNum.Name = "lbIntervalNum";
            this.lbIntervalNum.Size = new System.Drawing.Size(17, 12);
            this.lbIntervalNum.TabIndex = 9;
            this.lbIntervalNum.Text = "至";
            // 
            // edNum2
            // 
            this.edNum2.Location = new System.Drawing.Point(203, 14);
            this.edNum2.Name = "edNum2";
            this.edNum2.Size = new System.Drawing.Size(92, 21);
            this.edNum2.TabIndex = 10;
            // 
            // dp
            // 
            this.dp.BeginDate = new System.DateTime(2012, 9, 1, 0, 0, 0, 0);
            this.dp.CanSendMessage = false;
            this.dp.Caption = "";
            this.dp.EndDate = new System.DateTime(2012, 9, 30, 0, 0, 0, 0);
            this.dp.Location = new System.Drawing.Point(76, 41);
            this.dp.Mode = IE310.Core.Controls.I3DatePanelSelMode.dpsmMonth;
            this.dp.Name = "dp";
            this.dp.Size = new System.Drawing.Size(417, 86);
            this.dp.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 156);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(507, 46);
            this.panel2.TabIndex = 1;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(343, 13);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 0;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(181, 13);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbSchemeList);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(188, 446);
            this.panel3.TabIndex = 0;
            // 
            // lbSchemeList
            // 
            this.lbSchemeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSchemeList.HideSelection = false;
            this.lbSchemeList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.lbSchemeList.LargeImageList = this.imageList1;
            this.lbSchemeList.Location = new System.Drawing.Point(0, 0);
            this.lbSchemeList.Name = "lbSchemeList";
            this.lbSchemeList.Size = new System.Drawing.Size(188, 446);
            this.lbSchemeList.TabIndex = 0;
            this.lbSchemeList.UseCompatibleStateImageBehavior = false;
            this.lbSchemeList.SelectedIndexChanged += new System.EventHandler(this.lbSchemeList_SelectedIndexChanged);
            this.lbSchemeList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbSchemeList_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "6.ico");
            this.imageList1.Images.SetKeyName(1, "9.ico");
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(188, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(584, 446);
            this.panel4.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.pc);
            this.panel6.Controls.Add(this.gridSearch);
            this.panel6.Controls.Add(this.i3Table);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(584, 409);
            this.panel6.TabIndex = 1;
            // 
            // pc
            // 
            this.pc.Controls.Add(this.tabControl1);
            this.pc.Controls.Add(this.panel7);
            this.pc.Location = new System.Drawing.Point(123, 335);
            this.pc.Name = "pc";
            this.pc.Size = new System.Drawing.Size(493, 185);
            this.pc.TabIndex = 3;
            this.pc.VisibleChanged += new System.EventHandler(this.pc_VisibleChanged);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btClear);
            this.panel7.Controls.Add(this.btOK);
            this.panel7.Controls.Add(this.btCancel);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 143);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(493, 42);
            this.panel7.TabIndex = 1;
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(262, 13);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 2;
            this.btClear.Text = "清除";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // gridSearch
            // 
            this.gridSearch.Location = new System.Drawing.Point(0, 0);
            this.gridSearch.MainView = this.viewSearch;
            this.gridSearch.Name = "gridSearch";
            this.gridSearch.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPopupContainerEdit1});
            this.gridSearch.Size = new System.Drawing.Size(581, 126);
            this.gridSearch.TabIndex = 2;
            this.gridSearch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewSearch});
            // 
            // viewSearch
            // 
            this.viewSearch.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFieldCaption,
            this.colType,
            this.colLookString});
            this.viewSearch.GridControl = this.gridSearch;
            this.viewSearch.Name = "viewSearch";
            this.viewSearch.OptionsCustomization.AllowColumnMoving = false;
            this.viewSearch.OptionsCustomization.AllowColumnResizing = false;
            this.viewSearch.OptionsCustomization.AllowFilter = false;
            this.viewSearch.OptionsCustomization.AllowGroup = false;
            this.viewSearch.OptionsCustomization.AllowQuickHideColumns = false;
            this.viewSearch.OptionsCustomization.AllowSort = false;
            this.viewSearch.OptionsView.ColumnAutoWidth = false;
            this.viewSearch.OptionsView.ShowGroupPanel = false;
            // 
            // colFieldCaption
            // 
            this.colFieldCaption.AppearanceHeader.Options.UseTextOptions = true;
            this.colFieldCaption.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFieldCaption.Caption = "属性名";
            this.colFieldCaption.FieldName = "FieldCaption";
            this.colFieldCaption.Name = "colFieldCaption";
            this.colFieldCaption.OptionsColumn.AllowEdit = false;
            this.colFieldCaption.Visible = true;
            this.colFieldCaption.VisibleIndex = 0;
            this.colFieldCaption.Width = 135;
            // 
            // colType
            // 
            this.colType.AppearanceHeader.Options.UseTextOptions = true;
            this.colType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colType.Caption = "类型";
            this.colType.FieldName = "FieldType";
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowEdit = false;
            this.colType.Visible = true;
            this.colType.VisibleIndex = 1;
            // 
            // colLookString
            // 
            this.colLookString.AppearanceHeader.Options.UseTextOptions = true;
            this.colLookString.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLookString.Caption = "查找条件";
            this.colLookString.ColumnEdit = this.repositoryItemPopupContainerEdit1;
            this.colLookString.FieldName = "LookString";
            this.colLookString.Name = "colLookString";
            this.colLookString.Visible = true;
            this.colLookString.VisibleIndex = 2;
            this.colLookString.Width = 250;
            // 
            // repositoryItemPopupContainerEdit1
            // 
            this.repositoryItemPopupContainerEdit1.AutoHeight = false;
            this.repositoryItemPopupContainerEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemPopupContainerEdit1.Name = "repositoryItemPopupContainerEdit1";
            this.repositoryItemPopupContainerEdit1.PopupControl = this.pc;
            this.repositoryItemPopupContainerEdit1.ShowPopupCloseButton = false;
            // 
            // i3Table
            // 
            this.i3Table.ColumnHeaderDisplayMode = IE310.Table.Models.I3ColumnHeaderDisplayMode.Text;
            this.i3Table.ColumnModel = this.i3ColumnModel;
            this.i3Table.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.i3Table.HeaderFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.i3Table.Location = new System.Drawing.Point(6, 153);
            this.i3Table.Name = "i3Table";
            this.i3Table.RowHeaderDisplayMode = IE310.Table.Models.I3RowHeaderDisplayMode.Num;
            this.i3Table.Size = new System.Drawing.Size(575, 150);
            this.i3Table.TabIndex = 4;
            this.i3Table.TableModel = this.i3TableModel;
            this.i3Table.Text = "i3Table1";
            this.i3Table.UnfocusedSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.i3Table.UnfocusedSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            // 
            // i3ColumnModel
            // 
            this.i3ColumnModel.ColumnHeaderHeight = 20;
            this.i3ColumnModel.Columns.AddRange(new IE310.Table.Models.I3Column[] {
            this.i3TextColumn1,
            this.i3TextColumn2,
            this.i3PopupColumn1});
            // 
            // i3TextColumn1
            // 
            this.i3TextColumn1.Caption = "属性名";
            this.i3TextColumn1.CellAlignment = IE310.Table.Models.I3ColumnAlignment.Center;
            this.i3TextColumn1.Editable = false;
            this.i3TextColumn1.HeaderAlignment = IE310.Table.Models.I3ColumnAlignment.Center;
            this.i3TextColumn1.IsSelected = false;
            this.i3TextColumn1.Width = 130;
            // 
            // i3TextColumn2
            // 
            this.i3TextColumn2.Caption = "类型";
            this.i3TextColumn2.CellAlignment = IE310.Table.Models.I3ColumnAlignment.Center;
            this.i3TextColumn2.Editable = false;
            this.i3TextColumn2.HeaderAlignment = IE310.Table.Models.I3ColumnAlignment.Center;
            this.i3TextColumn2.IsSelected = false;
            this.i3TextColumn2.Width = 83;
            // 
            // i3PopupColumn1
            // 
            this.i3PopupColumn1.Caption = "查找条件";
            this.i3PopupColumn1.CellAlignment = IE310.Table.Models.I3ColumnAlignment.Center;
            this.i3PopupColumn1.HeaderAlignment = IE310.Table.Models.I3ColumnAlignment.Center;
            this.i3PopupColumn1.IsSelected = false;
            this.i3PopupColumn1.PopupControl = this.pc;
            this.i3PopupColumn1.Width = 276;
            // 
            // i3TableModel
            // 
            this.i3TableModel.DefaultRowHeight = 20;
            this.i3TableModel.RowHeaderWidth = 20;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btSaveScheme);
            this.panel5.Controls.Add(this.btReName);
            this.panel5.Controls.Add(this.btDeleteScheme);
            this.panel5.Controls.Add(this.btAddScheme);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 409);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(584, 37);
            this.panel5.TabIndex = 0;
            // 
            // btSaveScheme
            // 
            this.btSaveScheme.Location = new System.Drawing.Point(111, 8);
            this.btSaveScheme.Name = "btSaveScheme";
            this.btSaveScheme.Size = new System.Drawing.Size(99, 23);
            this.btSaveScheme.TabIndex = 4;
            this.btSaveScheme.Text = "保存方案";
            this.btSaveScheme.UseVisualStyleBackColor = true;
            this.btSaveScheme.Click += new System.EventHandler(this.btSaveScheme_Click);
            // 
            // btReName
            // 
            this.btReName.Location = new System.Drawing.Point(216, 8);
            this.btReName.Name = "btReName";
            this.btReName.Size = new System.Drawing.Size(100, 23);
            this.btReName.TabIndex = 3;
            this.btReName.Text = "重命名方案";
            this.btReName.UseVisualStyleBackColor = true;
            this.btReName.Click += new System.EventHandler(this.button2_Click);
            // 
            // btDeleteScheme
            // 
            this.btDeleteScheme.Location = new System.Drawing.Point(322, 8);
            this.btDeleteScheme.Name = "btDeleteScheme";
            this.btDeleteScheme.Size = new System.Drawing.Size(75, 23);
            this.btDeleteScheme.TabIndex = 1;
            this.btDeleteScheme.Text = "删除方案";
            this.btDeleteScheme.UseVisualStyleBackColor = true;
            this.btDeleteScheme.Click += new System.EventHandler(this.btDeleteScheme_Click);
            // 
            // btAddScheme
            // 
            this.btAddScheme.Location = new System.Drawing.Point(6, 8);
            this.btAddScheme.Name = "btAddScheme";
            this.btAddScheme.Size = new System.Drawing.Size(99, 23);
            this.btAddScheme.TabIndex = 0;
            this.btAddScheme.Text = "另存为新方案";
            this.btAddScheme.UseVisualStyleBackColor = true;
            this.btAddScheme.Click += new System.EventHandler(this.btAddScheme_Click);
            // 
            // I3Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Name = "I3Search";
            this.Size = new System.Drawing.Size(772, 446);
            this.tabControl1.ResumeLayout(false);
            this.pEqual.ResumeLayout(false);
            this.pEqual.PerformLayout();
            this.pDim.ResumeLayout(false);
            this.pDim.PerformLayout();
            this.pInterval.ResumeLayout(false);
            this.pInterval.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pc)).EndInit();
            this.pc.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.i3Table)).EndInit();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private I3Ini iecT_Ini1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edNum1;
        private System.Windows.Forms.Label lbIntervalNum;
        private System.Windows.Forms.TextBox edNum2;
        private I3DatePanel dp;
        private System.Windows.Forms.TabPage pDim;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edDimString;
        private System.Windows.Forms.TabPage pEqual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edEqualString;
        private System.Windows.Forms.DateTimePicker edEqualDate;
        private System.Windows.Forms.TextBox edEqualNum;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private DevExpress.XtraGrid.GridControl gridSearch;
        private DevExpress.XtraGrid.Views.Grid.GridView viewSearch;
        private DevExpress.XtraGrid.Columns.GridColumn colFieldCaption;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colLookString;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repositoryItemPopupContainerEdit1;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraEditors.PopupContainerControl pc;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btDeleteScheme;
        private System.Windows.Forms.Button btAddScheme;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btReName;
        private System.Windows.Forms.ListView lbSchemeList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btSaveScheme;
        private Table.Models.I3Table i3Table;
        private Table.Models.I3ColumnModel i3ColumnModel;
        private Table.Models.I3TableModel i3TableModel;
        private Table.Models.I3TextColumn i3TextColumn1;
        private Table.Models.I3TextColumn i3TextColumn2;
        private Table.Models.I3PopupColumn i3PopupColumn1;
    }
}
