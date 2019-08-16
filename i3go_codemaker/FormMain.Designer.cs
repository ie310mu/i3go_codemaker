namespace IE310.Tools.CodeMaker
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbSchema = new System.Windows.Forms.ComboBox();
            this.btDeleteSchema = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbUseDbNameWhenGetData = new System.Windows.Forms.CheckBox();
            this.btShowConnectionString = new System.Windows.Forms.Button();
            this.cbTableNeedUnderline = new System.Windows.Forms.CheckBox();
            this.cbFieldNeedUnderline = new System.Windows.Forms.CheckBox();
            this.btUnSelectAll = new System.Windows.Forms.Button();
            this.btSelectAll = new System.Windows.Forms.Button();
            this.table = new IE310.Table.Models.I3Table();
            this.columnModel = new IE310.Table.Column.I3ColumnModel();
            this.colSelect = new IE310.Table.Column.I3CheckBoxColumn();
            this.colPre = new IE310.Table.Column.I3TextColumn();
            this.tableModel = new IE310.Table.Row.I3TableModel();
            this.btRefresh = new System.Windows.Forms.Button();
            this.btTestConnect = new System.Windows.Forms.Button();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbDBServerType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGo = new System.Windows.Forms.TabPage();
            this.plGoService = new System.Windows.Forms.Panel();
            this.goServiceOutput = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label34 = new System.Windows.Forms.Label();
            this.goServiceRefrence = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.createGoService = new System.Windows.Forms.CheckBox();
            this.plGoMapper = new System.Windows.Forms.Panel();
            this.goMapperOutput = new System.Windows.Forms.TextBox();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.label47 = new System.Windows.Forms.Label();
            this.goMapperRefrence = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.createGoMapper = new System.Windows.Forms.CheckBox();
            this.plGoModel = new System.Windows.Forms.Panel();
            this.goModelOutput = new System.Windows.Forms.TextBox();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.label50 = new System.Windows.Forms.Label();
            this.goModelRefrence = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.createGoModel = new System.Windows.Forms.CheckBox();
            this.tpJava = new System.Windows.Forms.TabPage();
            this.plCreateJss = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.tbSuffixJss = new System.Windows.Forms.TextBox();
            this.tbOutPathJss = new System.Windows.Forms.TextBox();
            this.llOutPathJss = new System.Windows.Forms.LinkLabel();
            this.label31 = new System.Windows.Forms.Label();
            this.tbRefrenceNameSpaceJss = new System.Windows.Forms.TextBox();
            this.btSetOutPathJss = new System.Windows.Forms.Button();
            this.cbCreateJss = new System.Windows.Forms.CheckBox();
            this.plCreateJsonService = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.tbNamespaceJsonService = new System.Windows.Forms.TextBox();
            this.tbOutPathJsonService = new System.Windows.Forms.TextBox();
            this.llOutPathJsonService = new System.Windows.Forms.LinkLabel();
            this.label29 = new System.Windows.Forms.Label();
            this.tbRefrenceNameSpaceJsonService = new System.Windows.Forms.TextBox();
            this.btSetOutPathJsonService = new System.Windows.Forms.Button();
            this.cbCreateJsonService = new System.Windows.Forms.CheckBox();
            this.plCreateServiceImpl = new System.Windows.Forms.Panel();
            this.btSyn = new System.Windows.Forms.Button();
            this.tbCustomConsumeFileSyn = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tbConsumerFiles = new System.Windows.Forms.TextBox();
            this.tbProvidersPath = new System.Windows.Forms.TextBox();
            this.tbNamespaceServiceImpl = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.tbOutPathServiceImpl = new System.Windows.Forms.TextBox();
            this.llOutPathServiceImpl = new System.Windows.Forms.LinkLabel();
            this.label23 = new System.Windows.Forms.Label();
            this.tbRefrenceNameSpaceServiceImpl = new System.Windows.Forms.TextBox();
            this.btSetOutPathServiceImpl = new System.Windows.Forms.Button();
            this.cbCreateServiceImpl = new System.Windows.Forms.CheckBox();
            this.plCreateIService = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.tbNamespaceIService = new System.Windows.Forms.TextBox();
            this.tbOutPathIService = new System.Windows.Forms.TextBox();
            this.llOutPathIService = new System.Windows.Forms.LinkLabel();
            this.label21 = new System.Windows.Forms.Label();
            this.tbRefrenceNameSpaceIService = new System.Windows.Forms.TextBox();
            this.btSetOutPathIService = new System.Windows.Forms.Button();
            this.cbCreateIService = new System.Windows.Forms.CheckBox();
            this.plCreateMapper = new System.Windows.Forms.Panel();
            this.cbAddMyBatisMapperAnn = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbNamespaceMapper = new System.Windows.Forms.TextBox();
            this.tbOutPathMapper = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.llOutPathMapper = new System.Windows.Forms.LinkLabel();
            this.tbJavaMapperBaseClass = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbRefrenceNameSpaceMapper = new System.Windows.Forms.TextBox();
            this.btSetOutPathMapper = new System.Windows.Forms.Button();
            this.cbCreateMapper = new System.Windows.Forms.CheckBox();
            this.plCreateModel = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.tbNamespaceModel = new System.Windows.Forms.TextBox();
            this.tbOutPathModel = new System.Windows.Forms.TextBox();
            this.llOutPathModel = new System.Windows.Forms.LinkLabel();
            this.label32 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tbJavaModelBaseClass = new System.Windows.Forms.TextBox();
            this.tbRefrenceNameSpaceModel = new System.Windows.Forms.TextBox();
            this.btSetOutPathModel = new System.Windows.Forms.Button();
            this.cbCreateModel = new System.Windows.Forms.CheckBox();
            this.tpNet = new System.Windows.Forms.TabPage();
            this.plCreateClientService = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.tbNamespaceClientService = new System.Windows.Forms.TextBox();
            this.tbOutPathClientService = new System.Windows.Forms.TextBox();
            this.llOutPathClientService = new System.Windows.Forms.LinkLabel();
            this.label15 = new System.Windows.Forms.Label();
            this.tbRefrenceNameSpaceClientService = new System.Windows.Forms.TextBox();
            this.btSetOutPathClientService = new System.Windows.Forms.Button();
            this.cbCreateClientService = new System.Windows.Forms.CheckBox();
            this.plCreateServerService = new System.Windows.Forms.Panel();
            this.tbNamespaceServerService = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbOutPathServerService = new System.Windows.Forms.TextBox();
            this.llOutPathServerService = new System.Windows.Forms.LinkLabel();
            this.label13 = new System.Windows.Forms.Label();
            this.tbRefrenceNameSpaceServerService = new System.Windows.Forms.TextBox();
            this.btSetOutPathServerService = new System.Windows.Forms.Button();
            this.cbCreateServerService = new System.Windows.Forms.CheckBox();
            this.plCreateBusiness = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.tbNamespaceBusiness = new System.Windows.Forms.TextBox();
            this.tbOutPathBusiness = new System.Windows.Forms.TextBox();
            this.llOutPathBusiness = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.tbRefrenceNameSpaceBusiness = new System.Windows.Forms.TextBox();
            this.btSetOutPathBusiness = new System.Windows.Forms.Button();
            this.cbCreateBusiness = new System.Windows.Forms.CheckBox();
            this.plCreateDataAccess = new System.Windows.Forms.Panel();
            this.tbNamespaceDataAccess = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbOutPathDataAccess = new System.Windows.Forms.TextBox();
            this.llOutPathDataAccess = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRefrenceNameSpaceDataAccess = new System.Windows.Forms.TextBox();
            this.btSetOutPathDataAccess = new System.Windows.Forms.Button();
            this.cbCreateDataAccess = new System.Windows.Forms.CheckBox();
            this.plCreateData = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.tbNamespaceData = new System.Windows.Forms.TextBox();
            this.tbOutPathData = new System.Windows.Forms.TextBox();
            this.llOutPathData = new System.Windows.Forms.LinkLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.tbRefrenceNameSpaceData = new System.Windows.Forms.TextBox();
            this.btSetOutPathData = new System.Windows.Forms.Button();
            this.cbCreateData = new System.Windows.Forms.CheckBox();
            this.btAddSchema = new System.Windows.Forms.Button();
            this.btCompile = new System.Windows.Forms.Button();
            this.btRenameSchema = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.btCopy = new System.Windows.Forms.Button();
            this.btRestore = new System.Windows.Forms.Button();
            this.btBackup = new System.Windows.Forms.Button();
            this.sfdBackup = new System.Windows.Forms.SaveFileDialog();
            this.ofdResotre = new System.Windows.Forms.OpenFileDialog();
            this.progressReporter = new IE310.Core.Progressing.SimpleProgressReporterControl();
            this.localSettingManager = new IE310.Core.LocalSetting.LocalSettingManager(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpGo.SuspendLayout();
            this.plGoService.SuspendLayout();
            this.plGoMapper.SuspendLayout();
            this.plGoModel.SuspendLayout();
            this.tpJava.SuspendLayout();
            this.plCreateJss.SuspendLayout();
            this.plCreateJsonService.SuspendLayout();
            this.plCreateServiceImpl.SuspendLayout();
            this.plCreateIService.SuspendLayout();
            this.plCreateMapper.SuspendLayout();
            this.plCreateModel.SuspendLayout();
            this.tpNet.SuspendLayout();
            this.plCreateClientService.SuspendLayout();
            this.plCreateServerService.SuspendLayout();
            this.plCreateBusiness.SuspendLayout();
            this.plCreateDataAccess.SuspendLayout();
            this.plCreateData.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择方案";
            // 
            // cbbSchema
            // 
            this.cbbSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSchema.FormattingEnabled = true;
            this.cbbSchema.Location = new System.Drawing.Point(71, 6);
            this.cbbSchema.Name = "cbbSchema";
            this.cbbSchema.Size = new System.Drawing.Size(261, 20);
            this.cbbSchema.TabIndex = 1;
            this.cbbSchema.SelectedIndexChanged += new System.EventHandler(this.cbbSchema_SelectedIndexChanged);
            // 
            // btDeleteSchema
            // 
            this.btDeleteSchema.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btDeleteSchema.Location = new System.Drawing.Point(496, 4);
            this.btDeleteSchema.Name = "btDeleteSchema";
            this.btDeleteSchema.Size = new System.Drawing.Size(66, 23);
            this.btDeleteSchema.TabIndex = 5;
            this.btDeleteSchema.Text = "删除方案";
            this.btDeleteSchema.UseVisualStyleBackColor = false;
            this.btDeleteSchema.Click += new System.EventHandler(this.btDeleteSchema_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.cbUseDbNameWhenGetData);
            this.groupBox1.Controls.Add(this.btShowConnectionString);
            this.groupBox1.Controls.Add(this.cbTableNeedUnderline);
            this.groupBox1.Controls.Add(this.cbFieldNeedUnderline);
            this.groupBox1.Controls.Add(this.btUnSelectAll);
            this.groupBox1.Controls.Add(this.btSelectAll);
            this.groupBox1.Controls.Add(this.table);
            this.groupBox1.Controls.Add(this.btRefresh);
            this.groupBox1.Controls.Add(this.btTestConnect);
            this.groupBox1.Controls.Add(this.tbDatabase);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbUserName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbServer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbbDBServerType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(14, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 971);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入";
            // 
            // cbUseDbNameWhenGetData
            // 
            this.cbUseDbNameWhenGetData.AutoSize = true;
            this.cbUseDbNameWhenGetData.Location = new System.Drawing.Point(84, 202);
            this.cbUseDbNameWhenGetData.Name = "cbUseDbNameWhenGetData";
            this.cbUseDbNameWhenGetData.Size = new System.Drawing.Size(144, 16);
            this.cbUseDbNameWhenGetData.TabIndex = 18;
            this.cbUseDbNameWhenGetData.Text = "取数据时保留库名前缀";
            this.cbUseDbNameWhenGetData.UseVisualStyleBackColor = true;
            // 
            // btShowConnectionString
            // 
            this.btShowConnectionString.Location = new System.Drawing.Point(15, 230);
            this.btShowConnectionString.Name = "btShowConnectionString";
            this.btShowConnectionString.Size = new System.Drawing.Size(230, 23);
            this.btShowConnectionString.TabIndex = 17;
            this.btShowConnectionString.Text = "查看连接字符串";
            this.btShowConnectionString.UseVisualStyleBackColor = true;
            this.btShowConnectionString.Click += new System.EventHandler(this.btShowConnectionString_Click);
            // 
            // cbTableNeedUnderline
            // 
            this.cbTableNeedUnderline.AutoSize = true;
            this.cbTableNeedUnderline.Location = new System.Drawing.Point(84, 159);
            this.cbTableNeedUnderline.Name = "cbTableNeedUnderline";
            this.cbTableNeedUnderline.Size = new System.Drawing.Size(108, 16);
            this.cbTableNeedUnderline.TabIndex = 16;
            this.cbTableNeedUnderline.Text = "表名保留下划线";
            this.cbTableNeedUnderline.UseVisualStyleBackColor = true;
            // 
            // cbFieldNeedUnderline
            // 
            this.cbFieldNeedUnderline.AutoSize = true;
            this.cbFieldNeedUnderline.Location = new System.Drawing.Point(84, 180);
            this.cbFieldNeedUnderline.Name = "cbFieldNeedUnderline";
            this.cbFieldNeedUnderline.Size = new System.Drawing.Size(120, 16);
            this.cbFieldNeedUnderline.TabIndex = 15;
            this.cbFieldNeedUnderline.Text = "字段名保留下划线";
            this.cbFieldNeedUnderline.UseVisualStyleBackColor = true;
            // 
            // btUnSelectAll
            // 
            this.btUnSelectAll.Location = new System.Drawing.Point(195, 257);
            this.btUnSelectAll.Name = "btUnSelectAll";
            this.btUnSelectAll.Size = new System.Drawing.Size(50, 23);
            this.btUnSelectAll.TabIndex = 13;
            this.btUnSelectAll.Text = "全消";
            this.btUnSelectAll.UseVisualStyleBackColor = true;
            this.btUnSelectAll.Click += new System.EventHandler(this.btUnSelectAll_Click);
            // 
            // btSelectAll
            // 
            this.btSelectAll.Location = new System.Drawing.Point(139, 257);
            this.btSelectAll.Name = "btSelectAll";
            this.btSelectAll.Size = new System.Drawing.Size(50, 23);
            this.btSelectAll.TabIndex = 12;
            this.btSelectAll.Text = "全选";
            this.btSelectAll.UseVisualStyleBackColor = true;
            this.btSelectAll.Click += new System.EventHandler(this.btSelectAll_Click);
            // 
            // table
            // 
            this.table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.table.ColumnHeaderDisplayMode = IE310.Table.Header.I3ColumnHeaderDisplayMode.Text;
            this.table.ColumnModel = this.columnModel;
            this.table.ExtendLastCol = true;
            this.table.ForeColor = System.Drawing.Color.Black;
            this.table.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.table.Location = new System.Drawing.Point(3, 286);
            this.table.Name = "table";
            this.table.RowHeaderDisplayMode = IE310.Table.Header.I3RowHeaderDisplayMode.Num;
            this.table.RowHeaderVisible = false;
            this.table.SelectByRightButton = false;
            this.table.Size = new System.Drawing.Size(256, 682);
            this.table.TabIndex = 14;
            this.table.TableModel = this.tableModel;
            this.table.Text = "i3Table1";
            this.table.UnfocusedSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.table.UnfocusedSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.table.UserData = null;
            // 
            // columnModel
            // 
            this.columnModel.ColumnHeaderHeight = 20;
            this.columnModel.Columns.AddRange(new IE310.Table.Column.I3Column[] {
            this.colSelect,
            this.colPre});
            this.columnModel.Tag = null;
            this.columnModel.UserData = null;
            // 
            // colSelect
            // 
            this.colSelect.Caption = "选择";
            this.colSelect.CellAlignment = IE310.Table.Column.I3ColumnAlignment.Center;
            this.colSelect.CheckBoxColumnStyle = IE310.Table.Column.I3CheckBoxColumnStyle.Image;
            this.colSelect.CustomCheckImage = null;
            this.colSelect.CustomCheckImageFillClient = false;
            this.colSelect.CustomCheckImageSize = new System.Drawing.Size(24, 18);
            this.colSelect.DataMember = "";
            this.colSelect.Dictionary = null;
            this.colSelect.DrawText = false;
            this.colSelect.IsSelected = false;
            this.colSelect.Key = "";
            this.colSelect.NeedWidth = 34.71094F;
            this.colSelect.Sortable = false;
            this.colSelect.Tag = null;
            this.colSelect.Width = 54;
            // 
            // colPre
            // 
            this.colPre.Caption = "数据表前缀";
            this.colPre.DataMember = "";
            this.colPre.Dictionary = null;
            this.colPre.Editable = false;
            this.colPre.IsSelected = false;
            this.colPre.Key = "";
            this.colPre.NeedWidth = 71.7832F;
            this.colPre.Tag = null;
            // 
            // tableModel
            // 
            this.tableModel.DataSource = null;
            this.tableModel.DefaultRowHeight = 20;
            this.tableModel.Tag = null;
            this.tableModel.UserData = null;
            // 
            // btRefresh
            // 
            this.btRefresh.Location = new System.Drawing.Point(71, 257);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(50, 23);
            this.btRefresh.TabIndex = 11;
            this.btRefresh.Text = "刷新";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // btTestConnect
            // 
            this.btTestConnect.Location = new System.Drawing.Point(15, 257);
            this.btTestConnect.Name = "btTestConnect";
            this.btTestConnect.Size = new System.Drawing.Size(50, 23);
            this.btTestConnect.TabIndex = 10;
            this.btTestConnect.Text = "测试";
            this.btTestConnect.UseVisualStyleBackColor = true;
            this.btTestConnect.Click += new System.EventHandler(this.btTestConnect_Click);
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(84, 127);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(161, 21);
            this.tbDatabase.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "数据库";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(84, 100);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(161, 21);
            this.tbPassword.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "密码";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(84, 73);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(161, 21);
            this.tbUserName.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "用户名";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(84, 46);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(161, 21);
            this.tbServer.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "服务器";
            // 
            // cbbDBServerType
            // 
            this.cbbDBServerType.DisplayMember = "Name";
            this.cbbDBServerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDBServerType.FormattingEnabled = true;
            this.cbbDBServerType.Location = new System.Drawing.Point(84, 20);
            this.cbbDBServerType.Name = "cbbDBServerType";
            this.cbbDBServerType.Size = new System.Drawing.Size(161, 20);
            this.cbbDBServerType.TabIndex = 1;
            this.cbbDBServerType.ValueMember = "id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "数据库类型";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Location = new System.Drawing.Point(282, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(996, 971);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpGo);
            this.tabControl1.Controls.Add(this.tpJava);
            this.tabControl1.Controls.Add(this.tpNet);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(990, 951);
            this.tabControl1.TabIndex = 87;
            // 
            // tpGo
            // 
            this.tpGo.Controls.Add(this.plGoService);
            this.tpGo.Controls.Add(this.createGoService);
            this.tpGo.Controls.Add(this.plGoMapper);
            this.tpGo.Controls.Add(this.createGoMapper);
            this.tpGo.Controls.Add(this.plGoModel);
            this.tpGo.Controls.Add(this.createGoModel);
            this.tpGo.Location = new System.Drawing.Point(4, 22);
            this.tpGo.Name = "tpGo";
            this.tpGo.Padding = new System.Windows.Forms.Padding(3);
            this.tpGo.Size = new System.Drawing.Size(982, 925);
            this.tpGo.TabIndex = 2;
            this.tpGo.Text = "go";
            this.tpGo.UseVisualStyleBackColor = true;
            // 
            // plGoService
            // 
            this.plGoService.Controls.Add(this.goServiceOutput);
            this.plGoService.Controls.Add(this.linkLabel1);
            this.plGoService.Controls.Add(this.label34);
            this.plGoService.Controls.Add(this.goServiceRefrence);
            this.plGoService.Controls.Add(this.button1);
            this.plGoService.Dock = System.Windows.Forms.DockStyle.Top;
            this.plGoService.Location = new System.Drawing.Point(3, 151);
            this.plGoService.Name = "plGoService";
            this.plGoService.Size = new System.Drawing.Size(976, 50);
            this.plGoService.TabIndex = 96;
            // 
            // goServiceOutput
            // 
            this.goServiceOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.goServiceOutput.Location = new System.Drawing.Point(64, 26);
            this.goServiceOutput.Name = "goServiceOutput";
            this.goServiceOutput.Size = new System.Drawing.Size(865, 21);
            this.goServiceOutput.TabIndex = 59;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(5, 30);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(53, 12);
            this.linkLabel1.TabIndex = 58;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "输出路径";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(5, 7);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(53, 12);
            this.label34.TabIndex = 60;
            this.label34.Text = "引用空间";
            // 
            // goServiceRefrence
            // 
            this.goServiceRefrence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.goServiceRefrence.Location = new System.Drawing.Point(64, 3);
            this.goServiceRefrence.Name = "goServiceRefrence";
            this.goServiceRefrence.Size = new System.Drawing.Size(909, 21);
            this.goServiceRefrence.TabIndex = 61;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(935, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 23);
            this.button1.TabIndex = 62;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // createGoService
            // 
            this.createGoService.AutoSize = true;
            this.createGoService.Dock = System.Windows.Forms.DockStyle.Top;
            this.createGoService.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.createGoService.Location = new System.Drawing.Point(3, 135);
            this.createGoService.Name = "createGoService";
            this.createGoService.Size = new System.Drawing.Size(976, 16);
            this.createGoService.TabIndex = 95;
            this.createGoService.Text = "Service";
            this.createGoService.UseVisualStyleBackColor = true;
            // 
            // plGoMapper
            // 
            this.plGoMapper.Controls.Add(this.goMapperOutput);
            this.plGoMapper.Controls.Add(this.linkLabel5);
            this.plGoMapper.Controls.Add(this.label47);
            this.plGoMapper.Controls.Add(this.goMapperRefrence);
            this.plGoMapper.Controls.Add(this.button6);
            this.plGoMapper.Dock = System.Windows.Forms.DockStyle.Top;
            this.plGoMapper.Location = new System.Drawing.Point(3, 85);
            this.plGoMapper.Name = "plGoMapper";
            this.plGoMapper.Size = new System.Drawing.Size(976, 50);
            this.plGoMapper.TabIndex = 93;
            // 
            // goMapperOutput
            // 
            this.goMapperOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.goMapperOutput.Location = new System.Drawing.Point(64, 26);
            this.goMapperOutput.Name = "goMapperOutput";
            this.goMapperOutput.Size = new System.Drawing.Size(865, 21);
            this.goMapperOutput.TabIndex = 59;
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(5, 30);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(53, 12);
            this.linkLabel5.TabIndex = 58;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "输出路径";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(5, 7);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(53, 12);
            this.label47.TabIndex = 60;
            this.label47.Text = "引用空间";
            // 
            // goMapperRefrence
            // 
            this.goMapperRefrence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.goMapperRefrence.Location = new System.Drawing.Point(64, 3);
            this.goMapperRefrence.Name = "goMapperRefrence";
            this.goMapperRefrence.Size = new System.Drawing.Size(909, 21);
            this.goMapperRefrence.TabIndex = 61;
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(935, 25);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(38, 23);
            this.button6.TabIndex = 62;
            this.button6.Text = "...";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // createGoMapper
            // 
            this.createGoMapper.AutoSize = true;
            this.createGoMapper.Dock = System.Windows.Forms.DockStyle.Top;
            this.createGoMapper.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.createGoMapper.Location = new System.Drawing.Point(3, 69);
            this.createGoMapper.Name = "createGoMapper";
            this.createGoMapper.Size = new System.Drawing.Size(976, 16);
            this.createGoMapper.TabIndex = 88;
            this.createGoMapper.Text = "Mapper";
            this.createGoMapper.UseVisualStyleBackColor = true;
            // 
            // plGoModel
            // 
            this.plGoModel.Controls.Add(this.goModelOutput);
            this.plGoModel.Controls.Add(this.linkLabel6);
            this.plGoModel.Controls.Add(this.label50);
            this.plGoModel.Controls.Add(this.goModelRefrence);
            this.plGoModel.Controls.Add(this.button7);
            this.plGoModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.plGoModel.Location = new System.Drawing.Point(3, 19);
            this.plGoModel.Name = "plGoModel";
            this.plGoModel.Size = new System.Drawing.Size(976, 50);
            this.plGoModel.TabIndex = 94;
            // 
            // goModelOutput
            // 
            this.goModelOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.goModelOutput.Location = new System.Drawing.Point(69, 26);
            this.goModelOutput.Name = "goModelOutput";
            this.goModelOutput.Size = new System.Drawing.Size(863, 21);
            this.goModelOutput.TabIndex = 51;
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Location = new System.Drawing.Point(10, 30);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(53, 12);
            this.linkLabel6.TabIndex = 50;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "输出路径";
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(10, 7);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(53, 12);
            this.label50.TabIndex = 52;
            this.label50.Text = "引用空间";
            // 
            // goModelRefrence
            // 
            this.goModelRefrence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.goModelRefrence.Location = new System.Drawing.Point(69, 3);
            this.goModelRefrence.Name = "goModelRefrence";
            this.goModelRefrence.Size = new System.Drawing.Size(904, 21);
            this.goModelRefrence.TabIndex = 53;
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Location = new System.Drawing.Point(935, 25);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(38, 23);
            this.button7.TabIndex = 54;
            this.button7.Text = "...";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // createGoModel
            // 
            this.createGoModel.AutoSize = true;
            this.createGoModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.createGoModel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.createGoModel.Location = new System.Drawing.Point(3, 3);
            this.createGoModel.Name = "createGoModel";
            this.createGoModel.Size = new System.Drawing.Size(976, 16);
            this.createGoModel.TabIndex = 87;
            this.createGoModel.Text = "Model   (由于自动格式化，每次生成变化会较大)";
            this.createGoModel.UseVisualStyleBackColor = true;
            // 
            // tpJava
            // 
            this.tpJava.Controls.Add(this.plCreateJss);
            this.tpJava.Controls.Add(this.cbCreateJss);
            this.tpJava.Controls.Add(this.plCreateJsonService);
            this.tpJava.Controls.Add(this.cbCreateJsonService);
            this.tpJava.Controls.Add(this.plCreateServiceImpl);
            this.tpJava.Controls.Add(this.cbCreateServiceImpl);
            this.tpJava.Controls.Add(this.plCreateIService);
            this.tpJava.Controls.Add(this.cbCreateIService);
            this.tpJava.Controls.Add(this.plCreateMapper);
            this.tpJava.Controls.Add(this.cbCreateMapper);
            this.tpJava.Controls.Add(this.plCreateModel);
            this.tpJava.Controls.Add(this.cbCreateModel);
            this.tpJava.Location = new System.Drawing.Point(4, 22);
            this.tpJava.Name = "tpJava";
            this.tpJava.Padding = new System.Windows.Forms.Padding(3);
            this.tpJava.Size = new System.Drawing.Size(982, 925);
            this.tpJava.TabIndex = 1;
            this.tpJava.Text = "java";
            this.tpJava.UseVisualStyleBackColor = true;
            // 
            // plCreateJss
            // 
            this.plCreateJss.Controls.Add(this.label30);
            this.plCreateJss.Controls.Add(this.tbSuffixJss);
            this.plCreateJss.Controls.Add(this.tbOutPathJss);
            this.plCreateJss.Controls.Add(this.llOutPathJss);
            this.plCreateJss.Controls.Add(this.label31);
            this.plCreateJss.Controls.Add(this.tbRefrenceNameSpaceJss);
            this.plCreateJss.Controls.Add(this.btSetOutPathJss);
            this.plCreateJss.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCreateJss.Location = new System.Drawing.Point(3, 430);
            this.plCreateJss.Name = "plCreateJss";
            this.plCreateJss.Size = new System.Drawing.Size(976, 50);
            this.plCreateJss.TabIndex = 86;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(8, 6);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(71, 12);
            this.label30.TabIndex = 64;
            this.label30.Text = "Service后缀";
            // 
            // tbSuffixJss
            // 
            this.tbSuffixJss.Location = new System.Drawing.Point(85, 2);
            this.tbSuffixJss.Name = "tbSuffixJss";
            this.tbSuffixJss.Size = new System.Drawing.Size(172, 21);
            this.tbSuffixJss.TabIndex = 65;
            // 
            // tbOutPathJss
            // 
            this.tbOutPathJss.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutPathJss.Location = new System.Drawing.Point(67, 26);
            this.tbOutPathJss.Name = "tbOutPathJss";
            this.tbOutPathJss.Size = new System.Drawing.Size(863, 21);
            this.tbOutPathJss.TabIndex = 67;
            // 
            // llOutPathJss
            // 
            this.llOutPathJss.AutoSize = true;
            this.llOutPathJss.Location = new System.Drawing.Point(8, 30);
            this.llOutPathJss.Name = "llOutPathJss";
            this.llOutPathJss.Size = new System.Drawing.Size(53, 12);
            this.llOutPathJss.TabIndex = 66;
            this.llOutPathJss.TabStop = true;
            this.llOutPathJss.Text = "输出路径";
            this.llOutPathJss.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOutPathJss_LinkClicked);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(259, 6);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(53, 12);
            this.label31.TabIndex = 68;
            this.label31.Text = "引用空间";
            // 
            // tbRefrenceNameSpaceJss
            // 
            this.tbRefrenceNameSpaceJss.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRefrenceNameSpaceJss.Location = new System.Drawing.Point(318, 2);
            this.tbRefrenceNameSpaceJss.Name = "tbRefrenceNameSpaceJss";
            this.tbRefrenceNameSpaceJss.Size = new System.Drawing.Size(651, 21);
            this.tbRefrenceNameSpaceJss.TabIndex = 69;
            // 
            // btSetOutPathJss
            // 
            this.btSetOutPathJss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetOutPathJss.Location = new System.Drawing.Point(935, 25);
            this.btSetOutPathJss.Name = "btSetOutPathJss";
            this.btSetOutPathJss.Size = new System.Drawing.Size(38, 23);
            this.btSetOutPathJss.TabIndex = 70;
            this.btSetOutPathJss.Text = "...";
            this.btSetOutPathJss.UseVisualStyleBackColor = true;
            this.btSetOutPathJss.Click += new System.EventHandler(this.btSetOutPathJss_Click);
            // 
            // cbCreateJss
            // 
            this.cbCreateJss.AutoSize = true;
            this.cbCreateJss.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbCreateJss.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCreateJss.Location = new System.Drawing.Point(3, 414);
            this.cbCreateJss.Name = "cbCreateJss";
            this.cbCreateJss.Size = new System.Drawing.Size(976, 16);
            this.cbCreateJss.TabIndex = 85;
            this.cbCreateJss.Text = "生成Jss层代码(For H5)   说明：需要先建立对应的文件，才会自动生成代码（sysUserJss.js）";
            this.cbCreateJss.UseVisualStyleBackColor = true;
            this.cbCreateJss.CheckedChanged += new System.EventHandler(this.cbCreateJss_CheckedChanged);
            // 
            // plCreateJsonService
            // 
            this.plCreateJsonService.Controls.Add(this.label28);
            this.plCreateJsonService.Controls.Add(this.tbNamespaceJsonService);
            this.plCreateJsonService.Controls.Add(this.tbOutPathJsonService);
            this.plCreateJsonService.Controls.Add(this.llOutPathJsonService);
            this.plCreateJsonService.Controls.Add(this.label29);
            this.plCreateJsonService.Controls.Add(this.tbRefrenceNameSpaceJsonService);
            this.plCreateJsonService.Controls.Add(this.btSetOutPathJsonService);
            this.plCreateJsonService.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCreateJsonService.Location = new System.Drawing.Point(3, 364);
            this.plCreateJsonService.Name = "plCreateJsonService";
            this.plCreateJsonService.Size = new System.Drawing.Size(976, 50);
            this.plCreateJsonService.TabIndex = 84;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 6);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(53, 12);
            this.label28.TabIndex = 64;
            this.label28.Text = "命名空间";
            // 
            // tbNamespaceJsonService
            // 
            this.tbNamespaceJsonService.Location = new System.Drawing.Point(67, 2);
            this.tbNamespaceJsonService.Name = "tbNamespaceJsonService";
            this.tbNamespaceJsonService.Size = new System.Drawing.Size(190, 21);
            this.tbNamespaceJsonService.TabIndex = 65;
            // 
            // tbOutPathJsonService
            // 
            this.tbOutPathJsonService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutPathJsonService.Location = new System.Drawing.Point(67, 26);
            this.tbOutPathJsonService.Name = "tbOutPathJsonService";
            this.tbOutPathJsonService.Size = new System.Drawing.Size(863, 21);
            this.tbOutPathJsonService.TabIndex = 67;
            // 
            // llOutPathJsonService
            // 
            this.llOutPathJsonService.AutoSize = true;
            this.llOutPathJsonService.Location = new System.Drawing.Point(8, 30);
            this.llOutPathJsonService.Name = "llOutPathJsonService";
            this.llOutPathJsonService.Size = new System.Drawing.Size(53, 12);
            this.llOutPathJsonService.TabIndex = 66;
            this.llOutPathJsonService.TabStop = true;
            this.llOutPathJsonService.Text = "输出路径";
            this.llOutPathJsonService.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOutPathModelJsonService_LinkClicked);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(259, 6);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(53, 12);
            this.label29.TabIndex = 68;
            this.label29.Text = "引用空间";
            // 
            // tbRefrenceNameSpaceJsonService
            // 
            this.tbRefrenceNameSpaceJsonService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRefrenceNameSpaceJsonService.Location = new System.Drawing.Point(318, 2);
            this.tbRefrenceNameSpaceJsonService.Name = "tbRefrenceNameSpaceJsonService";
            this.tbRefrenceNameSpaceJsonService.Size = new System.Drawing.Size(651, 21);
            this.tbRefrenceNameSpaceJsonService.TabIndex = 69;
            // 
            // btSetOutPathJsonService
            // 
            this.btSetOutPathJsonService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetOutPathJsonService.Location = new System.Drawing.Point(935, 25);
            this.btSetOutPathJsonService.Name = "btSetOutPathJsonService";
            this.btSetOutPathJsonService.Size = new System.Drawing.Size(38, 23);
            this.btSetOutPathJsonService.TabIndex = 70;
            this.btSetOutPathJsonService.Text = "...";
            this.btSetOutPathJsonService.UseVisualStyleBackColor = true;
            this.btSetOutPathJsonService.Click += new System.EventHandler(this.btSetOutPathMapperJsonService_Click);
            // 
            // cbCreateJsonService
            // 
            this.cbCreateJsonService.AutoSize = true;
            this.cbCreateJsonService.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbCreateJsonService.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCreateJsonService.Location = new System.Drawing.Point(3, 348);
            this.cbCreateJsonService.Name = "cbCreateJsonService";
            this.cbCreateJsonService.Size = new System.Drawing.Size(976, 16);
            this.cbCreateJsonService.TabIndex = 83;
            this.cbCreateJsonService.Text = "生成JsonService层代码(For java app)   说明：需要先建立对应的文件，才会自动生成代码（SysUserService.java）（Json" +
    "Service层需要做控制）";
            this.cbCreateJsonService.UseVisualStyleBackColor = true;
            this.cbCreateJsonService.CheckedChanged += new System.EventHandler(this.cbCreateJsonService_CheckedChanged);
            // 
            // plCreateServiceImpl
            // 
            this.plCreateServiceImpl.Controls.Add(this.btSyn);
            this.plCreateServiceImpl.Controls.Add(this.tbCustomConsumeFileSyn);
            this.plCreateServiceImpl.Controls.Add(this.label27);
            this.plCreateServiceImpl.Controls.Add(this.tbConsumerFiles);
            this.plCreateServiceImpl.Controls.Add(this.tbProvidersPath);
            this.plCreateServiceImpl.Controls.Add(this.tbNamespaceServiceImpl);
            this.plCreateServiceImpl.Controls.Add(this.label26);
            this.plCreateServiceImpl.Controls.Add(this.label25);
            this.plCreateServiceImpl.Controls.Add(this.label24);
            this.plCreateServiceImpl.Controls.Add(this.tbOutPathServiceImpl);
            this.plCreateServiceImpl.Controls.Add(this.llOutPathServiceImpl);
            this.plCreateServiceImpl.Controls.Add(this.label23);
            this.plCreateServiceImpl.Controls.Add(this.tbRefrenceNameSpaceServiceImpl);
            this.plCreateServiceImpl.Controls.Add(this.btSetOutPathServiceImpl);
            this.plCreateServiceImpl.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCreateServiceImpl.Location = new System.Drawing.Point(3, 217);
            this.plCreateServiceImpl.Name = "plCreateServiceImpl";
            this.plCreateServiceImpl.Size = new System.Drawing.Size(976, 131);
            this.plCreateServiceImpl.TabIndex = 80;
            // 
            // btSyn
            // 
            this.btSyn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSyn.Location = new System.Drawing.Point(933, 96);
            this.btSyn.Name = "btSyn";
            this.btSyn.Size = new System.Drawing.Size(38, 23);
            this.btSyn.TabIndex = 83;
            this.btSyn.Text = "同步";
            this.btSyn.UseVisualStyleBackColor = true;
            this.btSyn.Click += new System.EventHandler(this.btSyn_Click);
            // 
            // tbCustomConsumeFileSyn
            // 
            this.tbCustomConsumeFileSyn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCustomConsumeFileSyn.Location = new System.Drawing.Point(200, 98);
            this.tbCustomConsumeFileSyn.Name = "tbCustomConsumeFileSyn";
            this.tbCustomConsumeFileSyn.Size = new System.Drawing.Size(729, 21);
            this.tbCustomConsumeFileSyn.TabIndex = 82;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(10, 102);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(191, 12);
            this.label27.TabIndex = 81;
            this.label27.Text = "自定义消费者配置文件同步(>=2个)";
            // 
            // tbConsumerFiles
            // 
            this.tbConsumerFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConsumerFiles.Location = new System.Drawing.Point(151, 74);
            this.tbConsumerFiles.Name = "tbConsumerFiles";
            this.tbConsumerFiles.Size = new System.Drawing.Size(815, 21);
            this.tbConsumerFiles.TabIndex = 80;
            // 
            // tbProvidersPath
            // 
            this.tbProvidersPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProvidersPath.Location = new System.Drawing.Point(151, 50);
            this.tbProvidersPath.Name = "tbProvidersPath";
            this.tbProvidersPath.Size = new System.Drawing.Size(815, 21);
            this.tbProvidersPath.TabIndex = 79;
            // 
            // tbNamespaceServiceImpl
            // 
            this.tbNamespaceServiceImpl.Location = new System.Drawing.Point(64, 2);
            this.tbNamespaceServiceImpl.Name = "tbNamespaceServiceImpl";
            this.tbNamespaceServiceImpl.Size = new System.Drawing.Size(190, 21);
            this.tbNamespaceServiceImpl.TabIndex = 73;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(8, 78);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(143, 12);
            this.label26.TabIndex = 72;
            this.label26.Text = "服务消费者配置文件(N个)";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 54);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(137, 12);
            this.label25.TabIndex = 72;
            this.label25.Text = "服务提供者配置文件目录";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(5, 6);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.TabIndex = 72;
            this.label24.Text = "命名空间";
            // 
            // tbOutPathServiceImpl
            // 
            this.tbOutPathServiceImpl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutPathServiceImpl.Location = new System.Drawing.Point(64, 26);
            this.tbOutPathServiceImpl.Name = "tbOutPathServiceImpl";
            this.tbOutPathServiceImpl.Size = new System.Drawing.Size(863, 21);
            this.tbOutPathServiceImpl.TabIndex = 75;
            // 
            // llOutPathServiceImpl
            // 
            this.llOutPathServiceImpl.AutoSize = true;
            this.llOutPathServiceImpl.Location = new System.Drawing.Point(5, 30);
            this.llOutPathServiceImpl.Name = "llOutPathServiceImpl";
            this.llOutPathServiceImpl.Size = new System.Drawing.Size(53, 12);
            this.llOutPathServiceImpl.TabIndex = 74;
            this.llOutPathServiceImpl.TabStop = true;
            this.llOutPathServiceImpl.Text = "输出路径";
            this.llOutPathServiceImpl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOutPathModelServiceImpl_LinkClicked);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(256, 6);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 12);
            this.label23.TabIndex = 76;
            this.label23.Text = "引用空间";
            // 
            // tbRefrenceNameSpaceServiceImpl
            // 
            this.tbRefrenceNameSpaceServiceImpl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRefrenceNameSpaceServiceImpl.Location = new System.Drawing.Point(315, 2);
            this.tbRefrenceNameSpaceServiceImpl.Name = "tbRefrenceNameSpaceServiceImpl";
            this.tbRefrenceNameSpaceServiceImpl.Size = new System.Drawing.Size(651, 21);
            this.tbRefrenceNameSpaceServiceImpl.TabIndex = 77;
            // 
            // btSetOutPathServiceImpl
            // 
            this.btSetOutPathServiceImpl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetOutPathServiceImpl.Location = new System.Drawing.Point(935, 25);
            this.btSetOutPathServiceImpl.Name = "btSetOutPathServiceImpl";
            this.btSetOutPathServiceImpl.Size = new System.Drawing.Size(38, 23);
            this.btSetOutPathServiceImpl.TabIndex = 78;
            this.btSetOutPathServiceImpl.Text = "...";
            this.btSetOutPathServiceImpl.UseVisualStyleBackColor = true;
            this.btSetOutPathServiceImpl.Click += new System.EventHandler(this.btSetOutPathMapperServiceImpl_Click);
            // 
            // cbCreateServiceImpl
            // 
            this.cbCreateServiceImpl.AutoSize = true;
            this.cbCreateServiceImpl.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbCreateServiceImpl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCreateServiceImpl.Location = new System.Drawing.Point(3, 201);
            this.cbCreateServiceImpl.Name = "cbCreateServiceImpl";
            this.cbCreateServiceImpl.Size = new System.Drawing.Size(976, 16);
            this.cbCreateServiceImpl.TabIndex = 71;
            this.cbCreateServiceImpl.Text = "生成ServiceImpl层代码(For java)";
            this.cbCreateServiceImpl.UseVisualStyleBackColor = true;
            this.cbCreateServiceImpl.CheckedChanged += new System.EventHandler(this.cbCreateServiceImpl_CheckedChanged);
            // 
            // plCreateIService
            // 
            this.plCreateIService.Controls.Add(this.label22);
            this.plCreateIService.Controls.Add(this.tbNamespaceIService);
            this.plCreateIService.Controls.Add(this.tbOutPathIService);
            this.plCreateIService.Controls.Add(this.llOutPathIService);
            this.plCreateIService.Controls.Add(this.label21);
            this.plCreateIService.Controls.Add(this.tbRefrenceNameSpaceIService);
            this.plCreateIService.Controls.Add(this.btSetOutPathIService);
            this.plCreateIService.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCreateIService.Location = new System.Drawing.Point(3, 151);
            this.plCreateIService.Name = "plCreateIService";
            this.plCreateIService.Size = new System.Drawing.Size(976, 50);
            this.plCreateIService.TabIndex = 81;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 6);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 12);
            this.label22.TabIndex = 64;
            this.label22.Text = "命名空间";
            // 
            // tbNamespaceIService
            // 
            this.tbNamespaceIService.Location = new System.Drawing.Point(67, 2);
            this.tbNamespaceIService.Name = "tbNamespaceIService";
            this.tbNamespaceIService.Size = new System.Drawing.Size(190, 21);
            this.tbNamespaceIService.TabIndex = 65;
            // 
            // tbOutPathIService
            // 
            this.tbOutPathIService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutPathIService.Location = new System.Drawing.Point(67, 26);
            this.tbOutPathIService.Name = "tbOutPathIService";
            this.tbOutPathIService.Size = new System.Drawing.Size(863, 21);
            this.tbOutPathIService.TabIndex = 67;
            // 
            // llOutPathIService
            // 
            this.llOutPathIService.AutoSize = true;
            this.llOutPathIService.Location = new System.Drawing.Point(8, 30);
            this.llOutPathIService.Name = "llOutPathIService";
            this.llOutPathIService.Size = new System.Drawing.Size(53, 12);
            this.llOutPathIService.TabIndex = 66;
            this.llOutPathIService.TabStop = true;
            this.llOutPathIService.Text = "输出路径";
            this.llOutPathIService.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOutPathModelIService_LinkClicked);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(259, 6);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 12);
            this.label21.TabIndex = 68;
            this.label21.Text = "引用空间";
            // 
            // tbRefrenceNameSpaceIService
            // 
            this.tbRefrenceNameSpaceIService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRefrenceNameSpaceIService.Location = new System.Drawing.Point(318, 2);
            this.tbRefrenceNameSpaceIService.Name = "tbRefrenceNameSpaceIService";
            this.tbRefrenceNameSpaceIService.Size = new System.Drawing.Size(651, 21);
            this.tbRefrenceNameSpaceIService.TabIndex = 69;
            // 
            // btSetOutPathIService
            // 
            this.btSetOutPathIService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetOutPathIService.Location = new System.Drawing.Point(935, 25);
            this.btSetOutPathIService.Name = "btSetOutPathIService";
            this.btSetOutPathIService.Size = new System.Drawing.Size(38, 23);
            this.btSetOutPathIService.TabIndex = 70;
            this.btSetOutPathIService.Text = "...";
            this.btSetOutPathIService.UseVisualStyleBackColor = true;
            this.btSetOutPathIService.Click += new System.EventHandler(this.btSetOutPathMapperIService_Click);
            // 
            // cbCreateIService
            // 
            this.cbCreateIService.AutoSize = true;
            this.cbCreateIService.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbCreateIService.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCreateIService.Location = new System.Drawing.Point(3, 135);
            this.cbCreateIService.Name = "cbCreateIService";
            this.cbCreateIService.Size = new System.Drawing.Size(976, 16);
            this.cbCreateIService.TabIndex = 63;
            this.cbCreateIService.Text = "生成IService层代码(For java)";
            this.cbCreateIService.UseVisualStyleBackColor = true;
            this.cbCreateIService.CheckedChanged += new System.EventHandler(this.cbCreateIService_CheckedChanged);
            // 
            // plCreateMapper
            // 
            this.plCreateMapper.Controls.Add(this.cbAddMyBatisMapperAnn);
            this.plCreateMapper.Controls.Add(this.label20);
            this.plCreateMapper.Controls.Add(this.tbNamespaceMapper);
            this.plCreateMapper.Controls.Add(this.tbOutPathMapper);
            this.plCreateMapper.Controls.Add(this.label33);
            this.plCreateMapper.Controls.Add(this.llOutPathMapper);
            this.plCreateMapper.Controls.Add(this.tbJavaMapperBaseClass);
            this.plCreateMapper.Controls.Add(this.label19);
            this.plCreateMapper.Controls.Add(this.tbRefrenceNameSpaceMapper);
            this.plCreateMapper.Controls.Add(this.btSetOutPathMapper);
            this.plCreateMapper.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCreateMapper.Location = new System.Drawing.Point(3, 85);
            this.plCreateMapper.Name = "plCreateMapper";
            this.plCreateMapper.Size = new System.Drawing.Size(976, 50);
            this.plCreateMapper.TabIndex = 81;
            // 
            // cbAddMyBatisMapperAnn
            // 
            this.cbAddMyBatisMapperAnn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAddMyBatisMapperAnn.AutoSize = true;
            this.cbAddMyBatisMapperAnn.Location = new System.Drawing.Point(545, 6);
            this.cbAddMyBatisMapperAnn.Name = "cbAddMyBatisMapperAnn";
            this.cbAddMyBatisMapperAnn.Size = new System.Drawing.Size(132, 16);
            this.cbAddMyBatisMapperAnn.TabIndex = 63;
            this.cbAddMyBatisMapperAnn.Text = "添加@MyBatisMapper";
            this.cbAddMyBatisMapperAnn.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(5, 6);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 12);
            this.label20.TabIndex = 56;
            this.label20.Text = "命名空间";
            // 
            // tbNamespaceMapper
            // 
            this.tbNamespaceMapper.Location = new System.Drawing.Point(64, 2);
            this.tbNamespaceMapper.Name = "tbNamespaceMapper";
            this.tbNamespaceMapper.Size = new System.Drawing.Size(190, 21);
            this.tbNamespaceMapper.TabIndex = 57;
            // 
            // tbOutPathMapper
            // 
            this.tbOutPathMapper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutPathMapper.Location = new System.Drawing.Point(64, 26);
            this.tbOutPathMapper.Name = "tbOutPathMapper";
            this.tbOutPathMapper.Size = new System.Drawing.Size(865, 21);
            this.tbOutPathMapper.TabIndex = 59;
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(683, 7);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(29, 12);
            this.label33.TabIndex = 52;
            this.label33.Text = "基类";
            // 
            // llOutPathMapper
            // 
            this.llOutPathMapper.AutoSize = true;
            this.llOutPathMapper.Location = new System.Drawing.Point(5, 30);
            this.llOutPathMapper.Name = "llOutPathMapper";
            this.llOutPathMapper.Size = new System.Drawing.Size(53, 12);
            this.llOutPathMapper.TabIndex = 58;
            this.llOutPathMapper.TabStop = true;
            this.llOutPathMapper.Text = "输出路径";
            this.llOutPathMapper.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOutPathMapper_LinkClicked);
            // 
            // tbJavaMapperBaseClass
            // 
            this.tbJavaMapperBaseClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbJavaMapperBaseClass.Location = new System.Drawing.Point(718, 3);
            this.tbJavaMapperBaseClass.Name = "tbJavaMapperBaseClass";
            this.tbJavaMapperBaseClass.Size = new System.Drawing.Size(251, 21);
            this.tbJavaMapperBaseClass.TabIndex = 53;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(256, 6);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 60;
            this.label19.Text = "引用空间";
            // 
            // tbRefrenceNameSpaceMapper
            // 
            this.tbRefrenceNameSpaceMapper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRefrenceNameSpaceMapper.Location = new System.Drawing.Point(315, 2);
            this.tbRefrenceNameSpaceMapper.Name = "tbRefrenceNameSpaceMapper";
            this.tbRefrenceNameSpaceMapper.Size = new System.Drawing.Size(224, 21);
            this.tbRefrenceNameSpaceMapper.TabIndex = 61;
            // 
            // btSetOutPathMapper
            // 
            this.btSetOutPathMapper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetOutPathMapper.Location = new System.Drawing.Point(935, 25);
            this.btSetOutPathMapper.Name = "btSetOutPathMapper";
            this.btSetOutPathMapper.Size = new System.Drawing.Size(38, 23);
            this.btSetOutPathMapper.TabIndex = 62;
            this.btSetOutPathMapper.Text = "...";
            this.btSetOutPathMapper.UseVisualStyleBackColor = true;
            this.btSetOutPathMapper.Click += new System.EventHandler(this.btSetOutPathMapper_Click);
            // 
            // cbCreateMapper
            // 
            this.cbCreateMapper.AutoSize = true;
            this.cbCreateMapper.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbCreateMapper.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCreateMapper.Location = new System.Drawing.Point(3, 69);
            this.cbCreateMapper.Name = "cbCreateMapper";
            this.cbCreateMapper.Size = new System.Drawing.Size(976, 16);
            this.cbCreateMapper.TabIndex = 55;
            this.cbCreateMapper.Text = "生成Mapper层代码(For java mybatis)    关键字备注:version、code";
            this.cbCreateMapper.UseVisualStyleBackColor = true;
            this.cbCreateMapper.CheckedChanged += new System.EventHandler(this.cbCreateMapper_CheckedChanged);
            // 
            // plCreateModel
            // 
            this.plCreateModel.Controls.Add(this.label18);
            this.plCreateModel.Controls.Add(this.tbNamespaceModel);
            this.plCreateModel.Controls.Add(this.tbOutPathModel);
            this.plCreateModel.Controls.Add(this.llOutPathModel);
            this.plCreateModel.Controls.Add(this.label32);
            this.plCreateModel.Controls.Add(this.label17);
            this.plCreateModel.Controls.Add(this.tbJavaModelBaseClass);
            this.plCreateModel.Controls.Add(this.tbRefrenceNameSpaceModel);
            this.plCreateModel.Controls.Add(this.btSetOutPathModel);
            this.plCreateModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCreateModel.Location = new System.Drawing.Point(3, 19);
            this.plCreateModel.Name = "plCreateModel";
            this.plCreateModel.Size = new System.Drawing.Size(976, 50);
            this.plCreateModel.TabIndex = 81;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 6);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 48;
            this.label18.Text = "命名空间";
            // 
            // tbNamespaceModel
            // 
            this.tbNamespaceModel.Location = new System.Drawing.Point(69, 2);
            this.tbNamespaceModel.Name = "tbNamespaceModel";
            this.tbNamespaceModel.Size = new System.Drawing.Size(190, 21);
            this.tbNamespaceModel.TabIndex = 49;
            // 
            // tbOutPathModel
            // 
            this.tbOutPathModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutPathModel.Location = new System.Drawing.Point(69, 26);
            this.tbOutPathModel.Name = "tbOutPathModel";
            this.tbOutPathModel.Size = new System.Drawing.Size(863, 21);
            this.tbOutPathModel.TabIndex = 51;
            // 
            // llOutPathModel
            // 
            this.llOutPathModel.AutoSize = true;
            this.llOutPathModel.Location = new System.Drawing.Point(10, 30);
            this.llOutPathModel.Name = "llOutPathModel";
            this.llOutPathModel.Size = new System.Drawing.Size(53, 12);
            this.llOutPathModel.TabIndex = 50;
            this.llOutPathModel.TabStop = true;
            this.llOutPathModel.Text = "输出路径";
            this.llOutPathModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOutPathModel_LinkClicked);
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(597, 6);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(29, 12);
            this.label32.TabIndex = 52;
            this.label32.Text = "基类";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(261, 6);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 52;
            this.label17.Text = "引用空间";
            // 
            // tbJavaModelBaseClass
            // 
            this.tbJavaModelBaseClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbJavaModelBaseClass.Location = new System.Drawing.Point(632, 2);
            this.tbJavaModelBaseClass.Name = "tbJavaModelBaseClass";
            this.tbJavaModelBaseClass.Size = new System.Drawing.Size(339, 21);
            this.tbJavaModelBaseClass.TabIndex = 53;
            // 
            // tbRefrenceNameSpaceModel
            // 
            this.tbRefrenceNameSpaceModel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRefrenceNameSpaceModel.Location = new System.Drawing.Point(320, 2);
            this.tbRefrenceNameSpaceModel.Name = "tbRefrenceNameSpaceModel";
            this.tbRefrenceNameSpaceModel.Size = new System.Drawing.Size(271, 21);
            this.tbRefrenceNameSpaceModel.TabIndex = 53;
            // 
            // btSetOutPathModel
            // 
            this.btSetOutPathModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetOutPathModel.Location = new System.Drawing.Point(935, 25);
            this.btSetOutPathModel.Name = "btSetOutPathModel";
            this.btSetOutPathModel.Size = new System.Drawing.Size(38, 23);
            this.btSetOutPathModel.TabIndex = 54;
            this.btSetOutPathModel.Text = "...";
            this.btSetOutPathModel.UseVisualStyleBackColor = true;
            this.btSetOutPathModel.Click += new System.EventHandler(this.btSetOutPathModel_Click);
            // 
            // cbCreateModel
            // 
            this.cbCreateModel.AutoSize = true;
            this.cbCreateModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbCreateModel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCreateModel.Location = new System.Drawing.Point(3, 3);
            this.cbCreateModel.Name = "cbCreateModel";
            this.cbCreateModel.Size = new System.Drawing.Size(976, 16);
            this.cbCreateModel.TabIndex = 47;
            this.cbCreateModel.Text = "生成Model层代码(For java)";
            this.cbCreateModel.UseVisualStyleBackColor = true;
            this.cbCreateModel.CheckedChanged += new System.EventHandler(this.cbCreateModel_CheckedChanged);
            // 
            // tpNet
            // 
            this.tpNet.Controls.Add(this.plCreateClientService);
            this.tpNet.Controls.Add(this.cbCreateClientService);
            this.tpNet.Controls.Add(this.plCreateServerService);
            this.tpNet.Controls.Add(this.cbCreateServerService);
            this.tpNet.Controls.Add(this.plCreateBusiness);
            this.tpNet.Controls.Add(this.cbCreateBusiness);
            this.tpNet.Controls.Add(this.plCreateDataAccess);
            this.tpNet.Controls.Add(this.cbCreateDataAccess);
            this.tpNet.Controls.Add(this.plCreateData);
            this.tpNet.Controls.Add(this.cbCreateData);
            this.tpNet.Location = new System.Drawing.Point(4, 22);
            this.tpNet.Name = "tpNet";
            this.tpNet.Padding = new System.Windows.Forms.Padding(3);
            this.tpNet.Size = new System.Drawing.Size(982, 925);
            this.tpNet.TabIndex = 0;
            this.tpNet.Text = ".net";
            this.tpNet.UseVisualStyleBackColor = true;
            // 
            // plCreateClientService
            // 
            this.plCreateClientService.Controls.Add(this.label16);
            this.plCreateClientService.Controls.Add(this.tbNamespaceClientService);
            this.plCreateClientService.Controls.Add(this.tbOutPathClientService);
            this.plCreateClientService.Controls.Add(this.llOutPathClientService);
            this.plCreateClientService.Controls.Add(this.label15);
            this.plCreateClientService.Controls.Add(this.tbRefrenceNameSpaceClientService);
            this.plCreateClientService.Controls.Add(this.btSetOutPathClientService);
            this.plCreateClientService.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCreateClientService.Location = new System.Drawing.Point(3, 283);
            this.plCreateClientService.Name = "plCreateClientService";
            this.plCreateClientService.Size = new System.Drawing.Size(976, 50);
            this.plCreateClientService.TabIndex = 81;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 6);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 40;
            this.label16.Text = "命名空间";
            // 
            // tbNamespaceClientService
            // 
            this.tbNamespaceClientService.Location = new System.Drawing.Point(67, 2);
            this.tbNamespaceClientService.Name = "tbNamespaceClientService";
            this.tbNamespaceClientService.Size = new System.Drawing.Size(190, 21);
            this.tbNamespaceClientService.TabIndex = 41;
            // 
            // tbOutPathClientService
            // 
            this.tbOutPathClientService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutPathClientService.Location = new System.Drawing.Point(67, 26);
            this.tbOutPathClientService.Name = "tbOutPathClientService";
            this.tbOutPathClientService.Size = new System.Drawing.Size(865, 21);
            this.tbOutPathClientService.TabIndex = 43;
            // 
            // llOutPathClientService
            // 
            this.llOutPathClientService.AutoSize = true;
            this.llOutPathClientService.Location = new System.Drawing.Point(8, 30);
            this.llOutPathClientService.Name = "llOutPathClientService";
            this.llOutPathClientService.Size = new System.Drawing.Size(53, 12);
            this.llOutPathClientService.TabIndex = 42;
            this.llOutPathClientService.TabStop = true;
            this.llOutPathClientService.Text = "输出路径";
            this.llOutPathClientService.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOutPathClientService_LinkClicked);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(259, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 44;
            this.label15.Text = "引用空间";
            // 
            // tbRefrenceNameSpaceClientService
            // 
            this.tbRefrenceNameSpaceClientService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRefrenceNameSpaceClientService.Location = new System.Drawing.Point(318, 2);
            this.tbRefrenceNameSpaceClientService.Name = "tbRefrenceNameSpaceClientService";
            this.tbRefrenceNameSpaceClientService.Size = new System.Drawing.Size(653, 21);
            this.tbRefrenceNameSpaceClientService.TabIndex = 45;
            // 
            // btSetOutPathClientService
            // 
            this.btSetOutPathClientService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetOutPathClientService.Location = new System.Drawing.Point(935, 25);
            this.btSetOutPathClientService.Name = "btSetOutPathClientService";
            this.btSetOutPathClientService.Size = new System.Drawing.Size(38, 23);
            this.btSetOutPathClientService.TabIndex = 46;
            this.btSetOutPathClientService.Text = "...";
            this.btSetOutPathClientService.UseVisualStyleBackColor = true;
            this.btSetOutPathClientService.Click += new System.EventHandler(this.btSetOutPathClientService_Click);
            // 
            // cbCreateClientService
            // 
            this.cbCreateClientService.AutoSize = true;
            this.cbCreateClientService.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbCreateClientService.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCreateClientService.Location = new System.Drawing.Point(3, 267);
            this.cbCreateClientService.Name = "cbCreateClientService";
            this.cbCreateClientService.Size = new System.Drawing.Size(976, 16);
            this.cbCreateClientService.TabIndex = 39;
            this.cbCreateClientService.Text = "生成ClientService层代码(For .net remoting)";
            this.cbCreateClientService.UseVisualStyleBackColor = true;
            this.cbCreateClientService.CheckedChanged += new System.EventHandler(this.cbCreateClientService_CheckedChanged);
            // 
            // plCreateServerService
            // 
            this.plCreateServerService.Controls.Add(this.tbNamespaceServerService);
            this.plCreateServerService.Controls.Add(this.label14);
            this.plCreateServerService.Controls.Add(this.tbOutPathServerService);
            this.plCreateServerService.Controls.Add(this.llOutPathServerService);
            this.plCreateServerService.Controls.Add(this.label13);
            this.plCreateServerService.Controls.Add(this.tbRefrenceNameSpaceServerService);
            this.plCreateServerService.Controls.Add(this.btSetOutPathServerService);
            this.plCreateServerService.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCreateServerService.Location = new System.Drawing.Point(3, 217);
            this.plCreateServerService.Name = "plCreateServerService";
            this.plCreateServerService.Size = new System.Drawing.Size(976, 50);
            this.plCreateServerService.TabIndex = 82;
            // 
            // tbNamespaceServerService
            // 
            this.tbNamespaceServerService.Location = new System.Drawing.Point(69, 2);
            this.tbNamespaceServerService.Name = "tbNamespaceServerService";
            this.tbNamespaceServerService.Size = new System.Drawing.Size(190, 21);
            this.tbNamespaceServerService.TabIndex = 33;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 32;
            this.label14.Text = "命名空间";
            // 
            // tbOutPathServerService
            // 
            this.tbOutPathServerService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutPathServerService.Location = new System.Drawing.Point(69, 26);
            this.tbOutPathServerService.Name = "tbOutPathServerService";
            this.tbOutPathServerService.Size = new System.Drawing.Size(863, 21);
            this.tbOutPathServerService.TabIndex = 35;
            // 
            // llOutPathServerService
            // 
            this.llOutPathServerService.AutoSize = true;
            this.llOutPathServerService.Location = new System.Drawing.Point(10, 30);
            this.llOutPathServerService.Name = "llOutPathServerService";
            this.llOutPathServerService.Size = new System.Drawing.Size(53, 12);
            this.llOutPathServerService.TabIndex = 34;
            this.llOutPathServerService.TabStop = true;
            this.llOutPathServerService.Text = "输出路径";
            this.llOutPathServerService.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOutPathService_LinkClicked);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(261, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 37;
            this.label13.Text = "引用空间";
            // 
            // tbRefrenceNameSpaceServerService
            // 
            this.tbRefrenceNameSpaceServerService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRefrenceNameSpaceServerService.Location = new System.Drawing.Point(320, 2);
            this.tbRefrenceNameSpaceServerService.Name = "tbRefrenceNameSpaceServerService";
            this.tbRefrenceNameSpaceServerService.Size = new System.Drawing.Size(653, 21);
            this.tbRefrenceNameSpaceServerService.TabIndex = 38;
            // 
            // btSetOutPathServerService
            // 
            this.btSetOutPathServerService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetOutPathServerService.Location = new System.Drawing.Point(935, 25);
            this.btSetOutPathServerService.Name = "btSetOutPathServerService";
            this.btSetOutPathServerService.Size = new System.Drawing.Size(38, 23);
            this.btSetOutPathServerService.TabIndex = 36;
            this.btSetOutPathServerService.Text = "...";
            this.btSetOutPathServerService.UseVisualStyleBackColor = true;
            this.btSetOutPathServerService.Click += new System.EventHandler(this.btSetOutPathService_Click);
            // 
            // cbCreateServerService
            // 
            this.cbCreateServerService.AutoSize = true;
            this.cbCreateServerService.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbCreateServerService.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCreateServerService.Location = new System.Drawing.Point(3, 201);
            this.cbCreateServerService.Name = "cbCreateServerService";
            this.cbCreateServerService.Size = new System.Drawing.Size(976, 16);
            this.cbCreateServerService.TabIndex = 17;
            this.cbCreateServerService.Text = "生成ServerService层代码(For .net remoting)";
            this.cbCreateServerService.UseVisualStyleBackColor = true;
            this.cbCreateServerService.CheckedChanged += new System.EventHandler(this.cbCreateServerService_CheckedChanged);
            // 
            // plCreateBusiness
            // 
            this.plCreateBusiness.Controls.Add(this.label11);
            this.plCreateBusiness.Controls.Add(this.tbNamespaceBusiness);
            this.plCreateBusiness.Controls.Add(this.tbOutPathBusiness);
            this.plCreateBusiness.Controls.Add(this.llOutPathBusiness);
            this.plCreateBusiness.Controls.Add(this.label10);
            this.plCreateBusiness.Controls.Add(this.tbRefrenceNameSpaceBusiness);
            this.plCreateBusiness.Controls.Add(this.btSetOutPathBusiness);
            this.plCreateBusiness.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCreateBusiness.Location = new System.Drawing.Point(3, 151);
            this.plCreateBusiness.Name = "plCreateBusiness";
            this.plCreateBusiness.Size = new System.Drawing.Size(976, 50);
            this.plCreateBusiness.TabIndex = 81;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "命名空间";
            // 
            // tbNamespaceBusiness
            // 
            this.tbNamespaceBusiness.Location = new System.Drawing.Point(64, 3);
            this.tbNamespaceBusiness.Name = "tbNamespaceBusiness";
            this.tbNamespaceBusiness.Size = new System.Drawing.Size(190, 21);
            this.tbNamespaceBusiness.TabIndex = 26;
            // 
            // tbOutPathBusiness
            // 
            this.tbOutPathBusiness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutPathBusiness.Location = new System.Drawing.Point(64, 27);
            this.tbOutPathBusiness.Name = "tbOutPathBusiness";
            this.tbOutPathBusiness.Size = new System.Drawing.Size(865, 21);
            this.tbOutPathBusiness.TabIndex = 28;
            // 
            // llOutPathBusiness
            // 
            this.llOutPathBusiness.AutoSize = true;
            this.llOutPathBusiness.Location = new System.Drawing.Point(5, 31);
            this.llOutPathBusiness.Name = "llOutPathBusiness";
            this.llOutPathBusiness.Size = new System.Drawing.Size(53, 12);
            this.llOutPathBusiness.TabIndex = 27;
            this.llOutPathBusiness.TabStop = true;
            this.llOutPathBusiness.Text = "输出路径";
            this.llOutPathBusiness.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOutPathBusiness_LinkClicked);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(256, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 30;
            this.label10.Text = "引用空间";
            // 
            // tbRefrenceNameSpaceBusiness
            // 
            this.tbRefrenceNameSpaceBusiness.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRefrenceNameSpaceBusiness.Location = new System.Drawing.Point(315, 3);
            this.tbRefrenceNameSpaceBusiness.Name = "tbRefrenceNameSpaceBusiness";
            this.tbRefrenceNameSpaceBusiness.Size = new System.Drawing.Size(658, 21);
            this.tbRefrenceNameSpaceBusiness.TabIndex = 31;
            // 
            // btSetOutPathBusiness
            // 
            this.btSetOutPathBusiness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetOutPathBusiness.Location = new System.Drawing.Point(935, 24);
            this.btSetOutPathBusiness.Name = "btSetOutPathBusiness";
            this.btSetOutPathBusiness.Size = new System.Drawing.Size(38, 23);
            this.btSetOutPathBusiness.TabIndex = 29;
            this.btSetOutPathBusiness.Text = "...";
            this.btSetOutPathBusiness.UseVisualStyleBackColor = true;
            this.btSetOutPathBusiness.Click += new System.EventHandler(this.btSetOutPathBusiness_Click);
            // 
            // cbCreateBusiness
            // 
            this.cbCreateBusiness.AutoSize = true;
            this.cbCreateBusiness.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbCreateBusiness.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCreateBusiness.Location = new System.Drawing.Point(3, 135);
            this.cbCreateBusiness.Name = "cbCreateBusiness";
            this.cbCreateBusiness.Size = new System.Drawing.Size(976, 16);
            this.cbCreateBusiness.TabIndex = 16;
            this.cbCreateBusiness.Text = "生成Business层代码(For .net)";
            this.cbCreateBusiness.UseVisualStyleBackColor = true;
            this.cbCreateBusiness.CheckedChanged += new System.EventHandler(this.cbCreateBusiness_CheckedChanged);
            // 
            // plCreateDataAccess
            // 
            this.plCreateDataAccess.Controls.Add(this.tbNamespaceDataAccess);
            this.plCreateDataAccess.Controls.Add(this.label9);
            this.plCreateDataAccess.Controls.Add(this.tbOutPathDataAccess);
            this.plCreateDataAccess.Controls.Add(this.llOutPathDataAccess);
            this.plCreateDataAccess.Controls.Add(this.label2);
            this.plCreateDataAccess.Controls.Add(this.tbRefrenceNameSpaceDataAccess);
            this.plCreateDataAccess.Controls.Add(this.btSetOutPathDataAccess);
            this.plCreateDataAccess.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCreateDataAccess.Location = new System.Drawing.Point(3, 85);
            this.plCreateDataAccess.Name = "plCreateDataAccess";
            this.plCreateDataAccess.Size = new System.Drawing.Size(976, 50);
            this.plCreateDataAccess.TabIndex = 79;
            // 
            // tbNamespaceDataAccess
            // 
            this.tbNamespaceDataAccess.Location = new System.Drawing.Point(69, 3);
            this.tbNamespaceDataAccess.Name = "tbNamespaceDataAccess";
            this.tbNamespaceDataAccess.Size = new System.Drawing.Size(190, 21);
            this.tbNamespaceDataAccess.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "命名空间";
            // 
            // tbOutPathDataAccess
            // 
            this.tbOutPathDataAccess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutPathDataAccess.Location = new System.Drawing.Point(69, 27);
            this.tbOutPathDataAccess.Name = "tbOutPathDataAccess";
            this.tbOutPathDataAccess.Size = new System.Drawing.Size(860, 21);
            this.tbOutPathDataAccess.TabIndex = 21;
            // 
            // llOutPathDataAccess
            // 
            this.llOutPathDataAccess.AutoSize = true;
            this.llOutPathDataAccess.Location = new System.Drawing.Point(10, 31);
            this.llOutPathDataAccess.Name = "llOutPathDataAccess";
            this.llOutPathDataAccess.Size = new System.Drawing.Size(53, 12);
            this.llOutPathDataAccess.TabIndex = 20;
            this.llOutPathDataAccess.TabStop = true;
            this.llOutPathDataAccess.Text = "输出路径";
            this.llOutPathDataAccess.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOutPathDataAccess_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(261, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "引用空间";
            // 
            // tbRefrenceNameSpaceDataAccess
            // 
            this.tbRefrenceNameSpaceDataAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRefrenceNameSpaceDataAccess.Location = new System.Drawing.Point(320, 3);
            this.tbRefrenceNameSpaceDataAccess.Name = "tbRefrenceNameSpaceDataAccess";
            this.tbRefrenceNameSpaceDataAccess.Size = new System.Drawing.Size(653, 21);
            this.tbRefrenceNameSpaceDataAccess.TabIndex = 24;
            // 
            // btSetOutPathDataAccess
            // 
            this.btSetOutPathDataAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetOutPathDataAccess.Location = new System.Drawing.Point(935, 26);
            this.btSetOutPathDataAccess.Name = "btSetOutPathDataAccess";
            this.btSetOutPathDataAccess.Size = new System.Drawing.Size(38, 23);
            this.btSetOutPathDataAccess.TabIndex = 22;
            this.btSetOutPathDataAccess.Text = "...";
            this.btSetOutPathDataAccess.UseVisualStyleBackColor = true;
            this.btSetOutPathDataAccess.Click += new System.EventHandler(this.btSetOutPathDataAccess_Click);
            // 
            // cbCreateDataAccess
            // 
            this.cbCreateDataAccess.AutoSize = true;
            this.cbCreateDataAccess.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbCreateDataAccess.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCreateDataAccess.Location = new System.Drawing.Point(3, 69);
            this.cbCreateDataAccess.Name = "cbCreateDataAccess";
            this.cbCreateDataAccess.Size = new System.Drawing.Size(976, 16);
            this.cbCreateDataAccess.TabIndex = 8;
            this.cbCreateDataAccess.Text = "生成DataAccess层代码(For .net)";
            this.cbCreateDataAccess.UseVisualStyleBackColor = true;
            this.cbCreateDataAccess.CheckedChanged += new System.EventHandler(this.cbCreateDataAccess_CheckedChanged);
            // 
            // plCreateData
            // 
            this.plCreateData.Controls.Add(this.label8);
            this.plCreateData.Controls.Add(this.tbNamespaceData);
            this.plCreateData.Controls.Add(this.tbOutPathData);
            this.plCreateData.Controls.Add(this.llOutPathData);
            this.plCreateData.Controls.Add(this.label12);
            this.plCreateData.Controls.Add(this.tbRefrenceNameSpaceData);
            this.plCreateData.Controls.Add(this.btSetOutPathData);
            this.plCreateData.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCreateData.Location = new System.Drawing.Point(3, 19);
            this.plCreateData.Name = "plCreateData";
            this.plCreateData.Size = new System.Drawing.Size(976, 50);
            this.plCreateData.TabIndex = 81;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "命名空间";
            // 
            // tbNamespaceData
            // 
            this.tbNamespaceData.Location = new System.Drawing.Point(67, 3);
            this.tbNamespaceData.Name = "tbNamespaceData";
            this.tbNamespaceData.Size = new System.Drawing.Size(190, 21);
            this.tbNamespaceData.TabIndex = 1;
            // 
            // tbOutPathData
            // 
            this.tbOutPathData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutPathData.Location = new System.Drawing.Point(67, 27);
            this.tbOutPathData.Name = "tbOutPathData";
            this.tbOutPathData.Size = new System.Drawing.Size(860, 21);
            this.tbOutPathData.TabIndex = 5;
            // 
            // llOutPathData
            // 
            this.llOutPathData.AutoSize = true;
            this.llOutPathData.Location = new System.Drawing.Point(8, 31);
            this.llOutPathData.Name = "llOutPathData";
            this.llOutPathData.Size = new System.Drawing.Size(53, 12);
            this.llOutPathData.TabIndex = 4;
            this.llOutPathData.TabStop = true;
            this.llOutPathData.Text = "输出路径";
            this.llOutPathData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOutPath_LinkClicked);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(259, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 14;
            this.label12.Text = "引用空间";
            // 
            // tbRefrenceNameSpaceData
            // 
            this.tbRefrenceNameSpaceData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRefrenceNameSpaceData.Location = new System.Drawing.Point(318, 3);
            this.tbRefrenceNameSpaceData.Name = "tbRefrenceNameSpaceData";
            this.tbRefrenceNameSpaceData.Size = new System.Drawing.Size(653, 21);
            this.tbRefrenceNameSpaceData.TabIndex = 15;
            // 
            // btSetOutPathData
            // 
            this.btSetOutPathData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetOutPathData.Location = new System.Drawing.Point(933, 26);
            this.btSetOutPathData.Name = "btSetOutPathData";
            this.btSetOutPathData.Size = new System.Drawing.Size(38, 23);
            this.btSetOutPathData.TabIndex = 6;
            this.btSetOutPathData.Text = "...";
            this.btSetOutPathData.UseVisualStyleBackColor = true;
            this.btSetOutPathData.Click += new System.EventHandler(this.btSetOutPath_Click);
            // 
            // cbCreateData
            // 
            this.cbCreateData.AutoSize = true;
            this.cbCreateData.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbCreateData.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCreateData.Location = new System.Drawing.Point(3, 3);
            this.cbCreateData.Name = "cbCreateData";
            this.cbCreateData.Size = new System.Drawing.Size(976, 16);
            this.cbCreateData.TabIndex = 7;
            this.cbCreateData.Text = "生成Data层代码(For .net)";
            this.cbCreateData.UseVisualStyleBackColor = true;
            this.cbCreateData.CheckedChanged += new System.EventHandler(this.cbCreateData_CheckedChanged);
            // 
            // btAddSchema
            // 
            this.btAddSchema.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btAddSchema.Location = new System.Drawing.Point(338, 4);
            this.btAddSchema.Name = "btAddSchema";
            this.btAddSchema.Size = new System.Drawing.Size(72, 23);
            this.btAddSchema.TabIndex = 4;
            this.btAddSchema.Text = "新增方案";
            this.btAddSchema.UseVisualStyleBackColor = false;
            this.btAddSchema.Click += new System.EventHandler(this.btAddSchema_Click);
            // 
            // btCompile
            // 
            this.btCompile.BackColor = System.Drawing.Color.Teal;
            this.btCompile.Location = new System.Drawing.Point(647, 4);
            this.btCompile.Name = "btCompile";
            this.btCompile.Size = new System.Drawing.Size(81, 23);
            this.btCompile.TabIndex = 6;
            this.btCompile.Text = "生成";
            this.btCompile.UseVisualStyleBackColor = false;
            this.btCompile.Click += new System.EventHandler(this.btCompile_Click);
            // 
            // btRenameSchema
            // 
            this.btRenameSchema.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btRenameSchema.Location = new System.Drawing.Point(566, 4);
            this.btRenameSchema.Name = "btRenameSchema";
            this.btRenameSchema.Size = new System.Drawing.Size(75, 23);
            this.btRenameSchema.TabIndex = 9;
            this.btRenameSchema.Text = "重命名方案";
            this.btRenameSchema.UseVisualStyleBackColor = false;
            this.btRenameSchema.Click += new System.EventHandler(this.btRenameSchema_Click);
            // 
            // btCopy
            // 
            this.btCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btCopy.Location = new System.Drawing.Point(416, 4);
            this.btCopy.Name = "btCopy";
            this.btCopy.Size = new System.Drawing.Size(74, 23);
            this.btCopy.TabIndex = 12;
            this.btCopy.Text = "复制方案";
            this.btCopy.UseVisualStyleBackColor = false;
            this.btCopy.Click += new System.EventHandler(this.btCopy_Click);
            // 
            // btRestore
            // 
            this.btRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btRestore.Location = new System.Drawing.Point(1229, 6);
            this.btRestore.Name = "btRestore";
            this.btRestore.Size = new System.Drawing.Size(49, 23);
            this.btRestore.TabIndex = 13;
            this.btRestore.Text = "恢复";
            this.btRestore.UseVisualStyleBackColor = false;
            this.btRestore.Click += new System.EventHandler(this.btRestore_Click);
            // 
            // btBackup
            // 
            this.btBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btBackup.Location = new System.Drawing.Point(1174, 6);
            this.btBackup.Name = "btBackup";
            this.btBackup.Size = new System.Drawing.Size(49, 23);
            this.btBackup.TabIndex = 14;
            this.btBackup.Text = "备份";
            this.btBackup.UseVisualStyleBackColor = false;
            this.btBackup.Click += new System.EventHandler(this.btBackup_Click);
            // 
            // sfdBackup
            // 
            this.sfdBackup.DefaultExt = "data";
            this.sfdBackup.Filter = "(*.data)|*.data";
            // 
            // ofdResotre
            // 
            this.ofdResotre.DefaultExt = "data";
            this.ofdResotre.FileName = "openFileDialog1";
            this.ofdResotre.Filter = "(*.data)|*.data";
            // 
            // progressReporter
            // 
            this.progressReporter.CanCloseByUser = false;
            this.progressReporter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressReporter.Location = new System.Drawing.Point(0, 1025);
            this.progressReporter.Name = "progressReporter";
            this.progressReporter.Size = new System.Drawing.Size(1291, 20);
            this.progressReporter.TabIndex = 10;
            this.progressReporter.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 1045);
            this.Controls.Add(this.btBackup);
            this.Controls.Add(this.btRestore);
            this.Controls.Add(this.btCopy);
            this.Controls.Add(this.progressReporter);
            this.Controls.Add(this.btRenameSchema);
            this.Controls.Add(this.btCompile);
            this.Controls.Add(this.btAddSchema);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btDeleteSchema);
            this.Controls.Add(this.cbbSchema);
            this.Controls.Add(this.label1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IE310 代码生成器 ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpGo.ResumeLayout(false);
            this.tpGo.PerformLayout();
            this.plGoService.ResumeLayout(false);
            this.plGoService.PerformLayout();
            this.plGoMapper.ResumeLayout(false);
            this.plGoMapper.PerformLayout();
            this.plGoModel.ResumeLayout(false);
            this.plGoModel.PerformLayout();
            this.tpJava.ResumeLayout(false);
            this.tpJava.PerformLayout();
            this.plCreateJss.ResumeLayout(false);
            this.plCreateJss.PerformLayout();
            this.plCreateJsonService.ResumeLayout(false);
            this.plCreateJsonService.PerformLayout();
            this.plCreateServiceImpl.ResumeLayout(false);
            this.plCreateServiceImpl.PerformLayout();
            this.plCreateIService.ResumeLayout(false);
            this.plCreateIService.PerformLayout();
            this.plCreateMapper.ResumeLayout(false);
            this.plCreateMapper.PerformLayout();
            this.plCreateModel.ResumeLayout(false);
            this.plCreateModel.PerformLayout();
            this.tpNet.ResumeLayout(false);
            this.tpNet.PerformLayout();
            this.plCreateClientService.ResumeLayout(false);
            this.plCreateClientService.PerformLayout();
            this.plCreateServerService.ResumeLayout(false);
            this.plCreateServerService.PerformLayout();
            this.plCreateBusiness.ResumeLayout(false);
            this.plCreateBusiness.PerformLayout();
            this.plCreateDataAccess.ResumeLayout(false);
            this.plCreateDataAccess.PerformLayout();
            this.plCreateData.ResumeLayout(false);
            this.plCreateData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbSchema;
        private System.Windows.Forms.Button btDeleteSchema;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btAddSchema;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbDBServerType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.Button btTestConnect;
        private Table.Models.I3Table table;
        private Table.Column.I3ColumnModel columnModel;
        private Table.Row.I3TableModel tableModel;
        private Table.Column.I3CheckBoxColumn colSelect;
        private Table.Column.I3TextColumn colPre;
        private System.Windows.Forms.Button btUnSelectAll;
        private System.Windows.Forms.Button btSelectAll;
        private System.Windows.Forms.TextBox tbNamespaceData;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.LinkLabel llOutPathData;
        private System.Windows.Forms.TextBox tbOutPathData;
        private System.Windows.Forms.CheckBox cbCreateData;
        private System.Windows.Forms.CheckBox cbCreateDataAccess;
        private System.Windows.Forms.TextBox tbRefrenceNameSpaceData;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btSetOutPathData;
        private System.Windows.Forms.Button btCompile;
        private Core.LocalSetting.LocalSettingManager localSettingManager;
        private System.Windows.Forms.Button btRenameSchema;
        private System.Windows.Forms.CheckBox cbCreateBusiness;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.CheckBox cbCreateServerService;
        private System.Windows.Forms.Button btSetOutPathDataAccess;
        private System.Windows.Forms.TextBox tbRefrenceNameSpaceDataAccess;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel llOutPathDataAccess;
        private System.Windows.Forms.TextBox tbOutPathDataAccess;
        private System.Windows.Forms.TextBox tbNamespaceDataAccess;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btSetOutPathServerService;
        private System.Windows.Forms.TextBox tbRefrenceNameSpaceServerService;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.LinkLabel llOutPathServerService;
        private System.Windows.Forms.TextBox tbOutPathServerService;
        private System.Windows.Forms.TextBox tbNamespaceServerService;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btSetOutPathBusiness;
        private System.Windows.Forms.TextBox tbRefrenceNameSpaceBusiness;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel llOutPathBusiness;
        private System.Windows.Forms.TextBox tbOutPathBusiness;
        private System.Windows.Forms.TextBox tbNamespaceBusiness;
        private System.Windows.Forms.Label label11;
        private Core.Progressing.SimpleProgressReporterControl progressReporter;
        private System.Windows.Forms.TextBox tbRefrenceNameSpaceClientService;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.LinkLabel llOutPathClientService;
        private System.Windows.Forms.TextBox tbOutPathClientService;
        private System.Windows.Forms.TextBox tbNamespaceClientService;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox cbCreateClientService;
        private System.Windows.Forms.Button btSetOutPathClientService;
        private System.Windows.Forms.CheckBox cbFieldNeedUnderline;
        private System.Windows.Forms.CheckBox cbTableNeedUnderline;
        private System.Windows.Forms.Button btShowConnectionString;
        private System.Windows.Forms.Button btSetOutPathModel;
        private System.Windows.Forms.TextBox tbRefrenceNameSpaceModel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.LinkLabel llOutPathModel;
        private System.Windows.Forms.TextBox tbOutPathModel;
        private System.Windows.Forms.TextBox tbNamespaceModel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox cbCreateModel;
        private System.Windows.Forms.Button btSetOutPathMapper;
        private System.Windows.Forms.TextBox tbRefrenceNameSpaceMapper;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.LinkLabel llOutPathMapper;
        private System.Windows.Forms.TextBox tbOutPathMapper;
        private System.Windows.Forms.TextBox tbNamespaceMapper;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox cbCreateMapper;
        private System.Windows.Forms.Button btCopy;
        private System.Windows.Forms.Button btRestore;
        private System.Windows.Forms.Button btBackup;
        private System.Windows.Forms.SaveFileDialog sfdBackup;
        private System.Windows.Forms.OpenFileDialog ofdResotre;
        private System.Windows.Forms.Button btSetOutPathIService;
        private System.Windows.Forms.TextBox tbRefrenceNameSpaceIService;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.LinkLabel llOutPathIService;
        private System.Windows.Forms.TextBox tbOutPathIService;
        private System.Windows.Forms.TextBox tbNamespaceIService;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox cbCreateIService;
        private System.Windows.Forms.Button btSetOutPathServiceImpl;
        private System.Windows.Forms.TextBox tbRefrenceNameSpaceServiceImpl;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.LinkLabel llOutPathServiceImpl;
        private System.Windows.Forms.TextBox tbOutPathServiceImpl;
        private System.Windows.Forms.TextBox tbNamespaceServiceImpl;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.CheckBox cbCreateServiceImpl;
        private System.Windows.Forms.Panel plCreateDataAccess;
        private System.Windows.Forms.Panel plCreateData;
        private System.Windows.Forms.Panel plCreateServiceImpl;
        private System.Windows.Forms.Panel plCreateBusiness;
        private System.Windows.Forms.Panel plCreateClientService;
        private System.Windows.Forms.Panel plCreateServerService;
        private System.Windows.Forms.Panel plCreateIService;
        private System.Windows.Forms.Panel plCreateMapper;
        private System.Windows.Forms.Panel plCreateModel;
        private System.Windows.Forms.TextBox tbConsumerFiles;
        private System.Windows.Forms.TextBox tbProvidersPath;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btSyn;
        private System.Windows.Forms.TextBox tbCustomConsumeFileSyn;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel plCreateJsonService;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox tbNamespaceJsonService;
        private System.Windows.Forms.TextBox tbOutPathJsonService;
        private System.Windows.Forms.LinkLabel llOutPathJsonService;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tbRefrenceNameSpaceJsonService;
        private System.Windows.Forms.Button btSetOutPathJsonService;
        private System.Windows.Forms.CheckBox cbCreateJsonService;
        private System.Windows.Forms.Panel plCreateJss;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox tbSuffixJss;
        private System.Windows.Forms.TextBox tbOutPathJss;
        private System.Windows.Forms.LinkLabel llOutPathJss;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox tbRefrenceNameSpaceJss;
        private System.Windows.Forms.Button btSetOutPathJss;
        private System.Windows.Forms.CheckBox cbCreateJss;
        private System.Windows.Forms.CheckBox cbAddMyBatisMapperAnn;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox tbJavaModelBaseClass;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox tbJavaMapperBaseClass;
        private System.Windows.Forms.CheckBox cbUseDbNameWhenGetData;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpNet;
        private System.Windows.Forms.TabPage tpJava;
        private System.Windows.Forms.TabPage tpGo;
        private System.Windows.Forms.Panel plGoMapper;
        private System.Windows.Forms.TextBox goMapperOutput;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox goMapperRefrence;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckBox createGoMapper;
        private System.Windows.Forms.Panel plGoModel;
        private System.Windows.Forms.TextBox goModelOutput;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox goModelRefrence;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.CheckBox createGoModel;
        private System.Windows.Forms.Panel plGoService;
        private System.Windows.Forms.TextBox goServiceOutput;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox goServiceRefrence;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox createGoService;
    }
}