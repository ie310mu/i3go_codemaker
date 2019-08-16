using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using IE310.Core.Utils;
using IE310.Core.DB;
using IE310.Table.Row;
using IE310.Table.Cell;
using IE310.Core.Components;
using IE310.Core.UI;
using System.Threading;
using IE310.Core.Json;

namespace IE310.Tools.CodeMaker
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private SettingItems settingItems;

        private void LoadItems()
        {
            cbbSchema.Items.Clear();
            foreach (string key in settingItems.Keys)
            {
                cbbSchema.Items.Add(key);
            }
        }

        private SettingItem curItem;
        public SettingItem CurItem
        {
            get
            {
                return curItem;
            }
            set
            {
                if (curItem != value)
                {
                    SaveItem();
                }
                curItem = value;
                LoadItem();
            }
        }

        private void LoadItem()
        {
            cbbSchema.Text = curItem == null ? "" : curItem.SchemaName;

            //输入
            cbbDBServerType.SelectedValue = curItem == null ? -1 : (int)curItem.DBServerType;
            if (curItem != null && cbbDBServerType.SelectedIndex == -1)
            {
                cbbDBServerType.SelectedIndex = 0;
            }
            tbServer.Text = curItem == null ? "" : curItem.Server;
            tbUserName.Text = curItem == null ? "" : curItem.UserName;
            tbPassword.Text = curItem == null ? "" : curItem.Password;
            tbDatabase.Text = curItem == null ? "" : curItem.Database;
            cbTableNeedUnderline.Checked = curItem == null ? false : curItem.TableNeedUnderline;
            cbFieldNeedUnderline.Checked = curItem == null ? false : curItem.FieldNeedUnderline;
            cbUseDbNameWhenGetData.Checked = curItem == null ? false : curItem.UseDbNameWhenGetData;

            //数据表
            RefreshPrefix();

            //输出
            cbCreateData.Checked = curItem == null ? false : curItem.CreateData;
            tbNamespaceData.Text = curItem == null ? "" : curItem.NamespaceNameData;
            tbRefrenceNameSpaceData.Text = curItem == null ? "" : curItem.RefrenceNamespaceData;
            tbOutPathData.Text = curItem == null ? "" : curItem.OutPathData;
            cbCreateData_CheckedChanged(null, null);

            cbCreateDataAccess.Checked = curItem == null ? false : curItem.CreateDataAccess;
            tbNamespaceDataAccess.Text = curItem == null ? "" : curItem.NamespaceNameDataAccess;
            tbRefrenceNameSpaceDataAccess.Text = curItem == null ? "" : curItem.RefrenceNamespaceDataAccess;
            tbOutPathDataAccess.Text = curItem == null ? "" : curItem.OutPathDataAccess;
            cbCreateDataAccess_CheckedChanged(null, null);

            cbCreateBusiness.Checked = curItem == null ? false : curItem.CreateBusiness;
            tbNamespaceBusiness.Text = curItem == null ? "" : curItem.NamespaceNameBusiness;
            tbRefrenceNameSpaceBusiness.Text = curItem == null ? "" : curItem.RefrenceNamespaceBusiness;
            tbOutPathBusiness.Text = curItem == null ? "" : curItem.OutPathBusiness;
            cbCreateBusiness_CheckedChanged(null, null);

            cbCreateServerService.Checked = curItem == null ? false : curItem.CreateServerService;
            tbNamespaceServerService.Text = curItem == null ? "" : curItem.NamespaceNameServerService;
            tbRefrenceNameSpaceServerService.Text = curItem == null ? "" : curItem.RefrenceNamespaceServerService;
            tbOutPathServerService.Text = curItem == null ? "" : curItem.OutPathServerService;
            cbCreateServerService_CheckedChanged(null, null);

            cbCreateClientService.Checked = curItem == null ? false : curItem.CreateClientService;
            tbNamespaceClientService.Text = curItem == null ? "" : curItem.NamespaceNameClientService;
            tbRefrenceNameSpaceClientService.Text = curItem == null ? "" : curItem.RefrenceNamespaceClientService;
            tbOutPathClientService.Text = curItem == null ? "" : curItem.OutPathClientService;
            cbCreateClientService_CheckedChanged(null, null);

            cbCreateModel.Checked = curItem == null ? false : curItem.CreateModel;
            tbNamespaceModel.Text = curItem == null ? "" : curItem.NamespaceNameModel;
            tbRefrenceNameSpaceModel.Text = curItem == null ? "" : curItem.RefrenceNamespaceModel;
            tbJavaModelBaseClass.Text = curItem == null ? "" : curItem.JavaModelBaseClass;
            tbOutPathModel.Text = curItem == null ? "" : curItem.OutPathModel;
            cbCreateModel_CheckedChanged(null, null);

            cbCreateMapper.Checked = curItem == null ? false : curItem.CreateMapper;
            tbNamespaceMapper.Text = curItem == null ? "" : curItem.NamespaceNameMapper;
            tbRefrenceNameSpaceMapper.Text = curItem == null ? "" : curItem.RefrenceNamespaceMapper;
            tbJavaMapperBaseClass.Text = curItem == null ? "" : curItem.JavaMapperBaseClass;
            cbAddMyBatisMapperAnn.Checked = curItem == null ? false : curItem.AddMyBatisMapperAnn;
            tbOutPathMapper.Text = curItem == null ? "" : curItem.OutPathMapper;
            cbCreateMapper_CheckedChanged(null, null);

            cbCreateIService.Checked = curItem == null ? false : curItem.CreateIService;
            tbNamespaceIService.Text = curItem == null ? "" : curItem.NamespaceNameIService;
            tbRefrenceNameSpaceIService.Text = curItem == null ? "" : curItem.RefrenceNamespaceIService;
            tbOutPathIService.Text = curItem == null ? "" : curItem.OutPathIService;
            cbCreateIService_CheckedChanged(null, null);

            cbCreateServiceImpl.Checked = curItem == null ? false : curItem.CreateServiceImpl;
            tbNamespaceServiceImpl.Text = curItem == null ? "" : curItem.NamespaceNameServiceImpl;
            tbRefrenceNameSpaceServiceImpl.Text = curItem == null ? "" : curItem.RefrenceNamespaceServiceImpl;
            tbOutPathServiceImpl.Text = curItem == null ? "" : curItem.OutPathServiceImpl;
            tbProvidersPath.Text = curItem == null ? "" : curItem.ProvidersPath;
            tbConsumerFiles.Text = curItem == null ? "" : curItem.ConsumerFiles;
            tbCustomConsumeFileSyn.Text = curItem == null ? "" : curItem.CustomConsumeFileSyn;
            cbCreateServiceImpl_CheckedChanged(null, null);


            cbCreateJsonService.Checked = curItem == null ? false : curItem.CreateJsonService;
            tbNamespaceJsonService.Text = curItem == null ? "" : curItem.NamespaceNameJsonService;
            tbRefrenceNameSpaceJsonService.Text = curItem == null ? "" : curItem.RefrenceNamespaceJsonService;
            tbOutPathJsonService.Text = curItem == null ? "" : curItem.OutPathJsonService;
            cbCreateJsonService_CheckedChanged(null, null);


            cbCreateJss.Checked = curItem == null ? false : curItem.CreateJss;
            tbSuffixJss.Text = curItem == null ? "" : curItem.SuffixJss;
            tbRefrenceNameSpaceJss.Text = curItem == null ? "" : curItem.RefrenceNamespaceJss;
            tbOutPathJss.Text = curItem == null ? "" : curItem.OutPathJss;
            cbCreateJss_CheckedChanged(null, null);

            createGoModel.Checked = curItem == null ? false : curItem.createGoModel;
            goModelRefrence.Text = curItem == null ? "" : curItem.goModelRefrence;
            goModelOutput.Text = curItem == null ? "" : curItem.goModelOutput;
            createGoMapper.Checked = curItem == null ? false : curItem.createGoMapper;
            goMapperRefrence.Text = curItem == null ? "" : curItem.goMapperRefrence;
            goMapperOutput.Text = curItem == null ? "" : curItem.goMapperOutput;
            createGoService.Checked = curItem == null ? false : curItem.createGoService;
            goServiceRefrence.Text = curItem == null ? "" : curItem.goServiceRefrence;
            goServiceOutput.Text = curItem == null ? "" : curItem.goServiceOutput;
        }

        /// <summary>
        /// 刷新前缀选择数据
        /// </summary>
        private void RefreshPrefix()
        {
            tableModel.Rows.Clear();
            if (curItem != null)
            {
                foreach (string prefix in curItem.PrefixList)
                {
                    I3Row row = tableModel.Rows.Add();
                    I3Cell selectCell = row.Cells.Add();
                    if (curItem.SelectedPrefixList.Contains(prefix))
                    {
                        selectCell.Checked = true;
                    }
                    row.Cells.Add().Data = prefix;
                }
            }
        }

        private void SaveItem()
        {
            if (curItem == null)
            {
                return;
            }

            //输入
            curItem.DBServerType = cbbDBServerType.SelectedIndex == -1 ? DBServerType.MySql : (DBServerType)(int)cbbDBServerType.SelectedValue;
            curItem.Server = tbServer.Text;
            curItem.UserName = tbUserName.Text;
            curItem.Password = tbPassword.Text;
            curItem.Database = tbDatabase.Text;
            curItem.TableNeedUnderline = cbTableNeedUnderline.Checked;
            curItem.FieldNeedUnderline = cbFieldNeedUnderline.Checked;
            curItem.UseDbNameWhenGetData = cbUseDbNameWhenGetData.Checked;

            //数据表
            curItem.PrefixList.Clear();
            curItem.SelectedPrefixList.Clear();
            foreach (I3Row row in tableModel.Rows)
            {
                string prefix = row.Cells[1].Data.ToString();
                curItem.PrefixList.Add(prefix);
                if (row.Cells[0].Checked)
                {
                    curItem.SelectedPrefixList.Add(prefix);
                }
            }

            //输出
            curItem.CreateData = cbCreateData.Checked;
            curItem.NamespaceNameData = tbNamespaceData.Text;
            curItem.RefrenceNamespaceData = tbRefrenceNameSpaceData.Text;
            curItem.OutPathData = tbOutPathData.Text;

            curItem.CreateDataAccess = cbCreateDataAccess.Checked;
            curItem.NamespaceNameDataAccess = tbNamespaceDataAccess.Text;
            curItem.RefrenceNamespaceDataAccess = tbRefrenceNameSpaceDataAccess.Text;
            curItem.OutPathDataAccess = tbOutPathDataAccess.Text;

            curItem.CreateBusiness = cbCreateBusiness.Checked;
            curItem.NamespaceNameBusiness = tbNamespaceBusiness.Text;
            curItem.RefrenceNamespaceBusiness = tbRefrenceNameSpaceBusiness.Text;
            curItem.OutPathBusiness = tbOutPathBusiness.Text;

            curItem.CreateServerService = cbCreateServerService.Checked;
            curItem.NamespaceNameServerService = tbNamespaceServerService.Text;
            curItem.RefrenceNamespaceServerService = tbRefrenceNameSpaceServerService.Text;
            curItem.OutPathServerService = tbOutPathServerService.Text;

            curItem.CreateClientService = cbCreateClientService.Checked;
            curItem.NamespaceNameClientService = tbNamespaceClientService.Text;
            curItem.RefrenceNamespaceClientService = tbRefrenceNameSpaceClientService.Text;
            curItem.OutPathClientService = tbOutPathClientService.Text;

            curItem.CreateModel = cbCreateModel.Checked;
            curItem.NamespaceNameModel = tbNamespaceModel.Text;
            curItem.RefrenceNamespaceModel = tbRefrenceNameSpaceModel.Text;
            curItem.JavaModelBaseClass = tbJavaModelBaseClass.Text;
            curItem.OutPathModel = tbOutPathModel.Text;

            curItem.CreateMapper = cbCreateMapper.Checked;
            curItem.NamespaceNameMapper = tbNamespaceMapper.Text;
            curItem.RefrenceNamespaceMapper = tbRefrenceNameSpaceMapper.Text;
            curItem.JavaMapperBaseClass = tbJavaMapperBaseClass.Text;
            curItem.AddMyBatisMapperAnn = cbAddMyBatisMapperAnn.Checked;
            curItem.OutPathMapper = tbOutPathMapper.Text;

            curItem.CreateIService = cbCreateIService.Checked;
            curItem.NamespaceNameIService = tbNamespaceIService.Text;
            curItem.RefrenceNamespaceIService = tbRefrenceNameSpaceIService.Text;
            curItem.OutPathIService = tbOutPathIService.Text;

            curItem.CreateServiceImpl = cbCreateServiceImpl.Checked;
            curItem.NamespaceNameServiceImpl = tbNamespaceServiceImpl.Text;
            curItem.RefrenceNamespaceServiceImpl = tbRefrenceNameSpaceServiceImpl.Text;
            curItem.OutPathServiceImpl = tbOutPathServiceImpl.Text;
            curItem.ProvidersPath = tbProvidersPath.Text;
            curItem.ConsumerFiles = tbConsumerFiles.Text;
            curItem.CustomConsumeFileSyn = tbCustomConsumeFileSyn.Text;

            curItem.CreateJsonService = cbCreateJsonService.Checked;
            curItem.NamespaceNameJsonService = tbNamespaceJsonService.Text;
            curItem.RefrenceNamespaceJsonService = tbRefrenceNameSpaceJsonService.Text;
            curItem.OutPathJsonService = tbOutPathJsonService.Text;

            curItem.CreateJss = cbCreateJss.Checked;
            curItem.SuffixJss = tbSuffixJss.Text;
            curItem.RefrenceNamespaceJss = tbRefrenceNameSpaceJss.Text;
            curItem.OutPathJss = tbOutPathJss.Text;

            curItem.createGoModel = createGoModel.Checked;
            curItem.goModelRefrence = goModelRefrence.Text;
            curItem.goModelOutput = goModelOutput.Text;
            curItem.createGoMapper = createGoMapper.Checked;
            curItem.goMapperRefrence = goMapperRefrence.Text;
            curItem.goMapperOutput = goMapperOutput.Text;

            curItem.createGoService = createGoService.Checked;
            curItem.goServiceRefrence = goServiceRefrence.Text;
            curItem.goServiceOutput = goServiceOutput.Text;

            SaveCurItemName(curItem.SchemaName);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.RemoveAt(1);
            tabControl1.TabPages.RemoveAt(1);

            DataTable dataTable = I3EnumUtil.EnumTypeToDataTable(typeof(DBServerType2));
            cbbDBServerType.DataSource = dataTable;

            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CodeMakeConfig.dat");
            localSettingManager.Init(fileName, typeof(SettingItems));
            settingItems = (SettingItems)localSettingManager.Read();

            LoadItems();
            SelectDefaultItem();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveItem();
            localSettingManager.Save(settingItems);
        }

        private void btAddSchema_Click(object sender, EventArgs e)
        {
            string schemaName;
            if (!I3GetStringForm.Excute("输入方案名称", "", out schemaName, false, false))
            {
                return;
            }
            if (settingItems.Keys.Contains(schemaName))
            {
                MessageBox.Show("方案名称已存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //保存当前数据
            SaveItem();
            //新增
            SettingItem settingItem = new SettingItem(schemaName);
            settingItems.Add(settingItem.SchemaName, settingItem);
            LoadItems();
            CurItem = settingItem;
        }

        private void btRenameSchema_Click(object sender, EventArgs e)
        {
            if (curItem == null)
            {
                return;
            }

            string schemaName;
            if (!I3GetStringForm.Excute("输入方案名称", curItem.SchemaName, out schemaName, false, false))
            {
                return;
            }
            if (string.Equals(curItem.SchemaName, schemaName))
            {
                return;
            }
            if (settingItems.Keys.Contains(schemaName))
            {
                MessageBox.Show("方案名称已存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //保存当前数据
            settingItems.Remove(curItem.SchemaName);
            curItem.SchemaName = schemaName;
            SaveItem();
            settingItems.Add(curItem.SchemaName, curItem);

            //重新加载
            LoadItems();
            CurItem = curItem;
        }

        private void btDeleteSchema_Click(object sender, EventArgs e)
        {
            if (curItem == null)
            {
                return;
            }

            if (MessageBox.Show("删除此方案？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            settingItems.Remove(curItem.SchemaName);
            LoadItems();
            CurItem = settingItems.Count == 0 ? null : settingItems[settingItems.Keys[0]];
        }

        private void cbbSchema_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSchema.SelectedIndex == -1)
            {
                CurItem = null;
            }

            if (CurItem != null && CurItem.SchemaName == cbbSchema.Text)
            {
                return;
            }

            CurItem = settingItems[cbbSchema.Text];
        }

        private void btTestConnect_Click(object sender, EventArgs e)
        {
            if (curItem == null)
            {
                return;
            }

            I3DBUtil.ConnectionString = GetConnectionString();
            //MessageBox.Show(I3DBUtil.ConnectionString);
            try
            {
                using (I3DBUtil.CreateAndOpenDbConnection())
                {
                }
                MessageBox.Show("OK！", "测试", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "测试", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            if (curItem == null)
            {
                return "";
            }
            SaveItem();

            return curItem.GetConnectionString();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            if (curItem == null)
            {
                return;
            }

            List<string> tableNameList = GetTableNameList();
            List<string> prefixList = new List<string>();
            foreach (string tableName in tableNameList)
            {
                int index = tableName.IndexOf("_");
                if (index > 0)
                {
                    string prefix = tableName.Substring(0, index).ToUpper();
                    if (!prefixList.Contains(prefix))
                    {
                        prefixList.Add(prefix);
                    }
                }
            }
            curItem.PrefixList = prefixList;
            CurItem = curItem;
        }

        private List<string> GetTableNameList()
        {
            if (curItem == null)
            {
                return new List<string>();
            }
            SaveItem();

            I3DBUtil.ConnectionString = GetConnectionString();
            return I3DBUtil.GetTableNameList();
        }

        private void btSelectAll_Click(object sender, EventArgs e)
        {
            foreach (I3Row row in tableModel.Rows)
            {
                row.Cells[0].Checked = true;
            }
        }

        private void btUnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (I3Row row in tableModel.Rows)
            {
                row.Cells[0].Checked = false;
            }
        }

        private void btSetOutPath_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = tbOutPathData.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbOutPathData.Text = fbd.SelectedPath;
        }

        private void llOutPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = tbOutPathData.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void btCompile_Click(object sender, EventArgs e)
        {
            if (curItem == null)
            {
                return;
            }
            SaveItem();

            progressReporter.Visible = true;
            try
            {
                SourceBuildHelper.Build(curItem, progressReporter, Save2);
                btSyn_Click(null, null);
                MessageBox.Show("ok");
            }
            finally
            {
                progressReporter.Visible = false;
            }
        }

        /// <summary>
        /// 标记配置文件过期 
        /// </summary>
        private void Save2()
        {
            if (curItem != null)
            {
                curItem.Flag5 = true;
            }
            localSettingManager.Save(settingItems);
        }

        private void btSetOutPathDataAccess_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = tbOutPathDataAccess.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbOutPathDataAccess.Text = fbd.SelectedPath;
        }

        private void btSetOutPathBusiness_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = tbOutPathBusiness.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbOutPathBusiness.Text = fbd.SelectedPath;
        }

        private void btSetOutPathService_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = tbOutPathServerService.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbOutPathServerService.Text = fbd.SelectedPath;
        }

        private void llOutPathDataAccess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = tbOutPathDataAccess.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void llOutPathBusiness_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = tbOutPathBusiness.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void llOutPathService_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = tbOutPathServerService.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void llOutPathClientService_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = tbOutPathClientService.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void btSetOutPathClientService_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = tbOutPathClientService.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbOutPathClientService.Text = fbd.SelectedPath;
        }

        private void btShowConnectionString_Click(object sender, EventArgs e)
        {
            string cs = GetConnectionString();
            I3GetStringForm.Excute("", cs, out cs, false, true);
        }


        private void llOutPathModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = tbOutPathModel.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void llOutPathMapper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = tbOutPathMapper.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void btSetOutPathModel_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = tbOutPathModel.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbOutPathModel.Text = fbd.SelectedPath;
        }

        private void btSetOutPathMapper_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = tbOutPathMapper.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbOutPathMapper.Text = fbd.SelectedPath;
        }

        private void btCopy_Click(object sender, EventArgs e)
        {
            if (curItem == null)
            {
                return;
            }


            string schemaName;
            if (!I3GetStringForm.Excute("输入方案名称", "", out schemaName, false, false))
            {
                return;
            }
            if (settingItems.Keys.Contains(schemaName))
            {
                MessageBox.Show("方案名称已存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //保存当前数据
            SaveItem();
            //新增
            SettingItem settingItem = new SettingItem(schemaName);
            I3ObjectUtil.DeepCopyProperty(curItem, settingItem);  //复制属性
            settingItem.SchemaName = schemaName;//重新设置名称

            settingItems.Add(settingItem.SchemaName, settingItem);
            LoadItems();
            CurItem = settingItem;
        }

        private void btBackup_Click(object sender, EventArgs e)
        {
            if (sfdBackup.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            string fileName = sfdBackup.FileName;

            SaveItem();
            localSettingManager.Save(settingItems);//保存当前数据
            localSettingManager.Save(settingItems, fileName);//备份
        }

        private void btRestore_Click(object sender, EventArgs e)
        {
            if (ofdResotre.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            string fileName = ofdResotre.FileName;

            SettingItems tmp = (SettingItems)localSettingManager.Read(fileName);
            settingItems = tmp;//恢复
            localSettingManager.Save(settingItems);//保存当前数据
            LoadItems();//重置界面

            SelectDefaultItem();
        }

        private void SelectDefaultItem()
        {
            string curItemName = ReadCurItemName();
            if (string.IsNullOrEmpty(curItemName) || !settingItems.ContainsKey(curItemName))
            {
                CurItem = settingItems.Count == 0 ? null : settingItems[settingItems.Keys[0]];
            }
            else
            {
                CurItem = settingItems[curItemName];
            }
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="item"></param>
        public void SaveCurItemName(string name)
        {
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "curItemName.dat");
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                byte[] data = Encoding.UTF8.GetBytes(name);
                fs.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns></returns>
        public string ReadCurItemName()
        {
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "curItemName.dat");
            if (!File.Exists(file))
            {
                return "";
            }

            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                string name = Encoding.UTF8.GetString(data);
                return name;
            }
        }

        private void llOutPathModelIService_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = tbOutPathIService.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void llOutPathModelServiceImpl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = tbOutPathServiceImpl.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void btSetOutPathMapperIService_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = tbOutPathIService.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbOutPathIService.Text = fbd.SelectedPath;
        }

        private void btSetOutPathMapperServiceImpl_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = tbOutPathServiceImpl.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbOutPathServiceImpl.Text = fbd.SelectedPath;
        }

        private void cbCreateDataAccess_CheckedChanged(object sender, EventArgs e)
        {
            plCreateDataAccess.Visible = cbCreateDataAccess.Checked;
            cbCreateDataAccess.ForeColor = cbCreateDataAccess.Checked ? Color.Black : Color.Gray;
        }

        private void cbCreateData_CheckedChanged(object sender, EventArgs e)
        {
            plCreateData.Visible = cbCreateData.Checked;
            cbCreateData.ForeColor = cbCreateData.Checked ? Color.Black : Color.Gray;
        }

        private void cbCreateBusiness_CheckedChanged(object sender, EventArgs e)
        {
            plCreateBusiness.Visible = cbCreateBusiness.Checked;
            cbCreateBusiness.ForeColor = cbCreateBusiness.Checked ? Color.Black : Color.Gray;
        }

        private void cbCreateServerService_CheckedChanged(object sender, EventArgs e)
        {
            plCreateServerService.Visible = cbCreateServerService.Checked;
            cbCreateServerService.ForeColor = cbCreateServerService.Checked ? Color.Black : Color.Gray;
        }

        private void cbCreateClientService_CheckedChanged(object sender, EventArgs e)
        {
            plCreateClientService.Visible = cbCreateClientService.Checked;
            cbCreateClientService.ForeColor = cbCreateClientService.Checked ? Color.Black : Color.Gray;
        }

        private void cbCreateModel_CheckedChanged(object sender, EventArgs e)
        {
            plCreateModel.Visible = cbCreateModel.Checked;
            cbCreateModel.ForeColor = cbCreateModel.Checked ? Color.Black : Color.Gray;
        }

        private void cbCreateMapper_CheckedChanged(object sender, EventArgs e)
        {
            plCreateMapper.Visible = cbCreateMapper.Checked;
            cbCreateMapper.ForeColor = cbCreateMapper.Checked ? Color.Black : Color.Gray;
        }

        private void cbCreateIService_CheckedChanged(object sender, EventArgs e)
        {
            plCreateIService.Visible = cbCreateIService.Checked;
            cbCreateIService.ForeColor = cbCreateIService.Checked ? Color.Black : Color.Gray;
        }

        private void cbCreateServiceImpl_CheckedChanged(object sender, EventArgs e)
        {
            plCreateServiceImpl.Visible = cbCreateServiceImpl.Checked;
            cbCreateServiceImpl.ForeColor = cbCreateServiceImpl.Checked ? Color.Black : Color.Gray;
        }

        private void btSyn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCustomConsumeFileSyn.Text))
            {
                return;
            }

            string[] strs = tbCustomConsumeFileSyn.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (strs.Length < 2)
            {
                return;
            }

            for (int i = 1; i <= strs.Length - 1; i++)
            {
                File.Copy(strs[0], strs[1], true);
            }

            if (sender != null)
            {
                MessageBox.Show("ok");
            }
        }

        private void cbCreateJsonService_CheckedChanged(object sender, EventArgs e)
        {
            plCreateJsonService.Visible = cbCreateJsonService.Checked;
            cbCreateJsonService.ForeColor = cbCreateJsonService.Checked ? Color.Black : Color.Gray;
        }

        private void llOutPathModelJsonService_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = tbOutPathJsonService.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void btSetOutPathMapperJsonService_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = tbOutPathJsonService.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbOutPathJsonService.Text = fbd.SelectedPath;
        }

        private void llOutPathJss_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = tbOutPathJss.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void btSetOutPathJss_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = tbOutPathJss.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbOutPathJss.Text = fbd.SelectedPath;
        }

        private void cbCreateJss_CheckedChanged(object sender, EventArgs e)
        {
            plCreateJss.Visible = cbCreateJss.Checked;
            cbCreateJss.ForeColor = cbCreateJss.Checked ? Color.Black : Color.Gray;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = goModelOutput.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = goMapperOutput.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = goModelOutput.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            goModelOutput.Text = fbd.SelectedPath;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = goMapperOutput.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            goMapperOutput.Text = fbd.SelectedPath;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = goServiceOutput.Text;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            I3PCUtil.CreateAndWaitProcess("", path, "", false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = goServiceOutput.Text;
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            goServiceOutput.Text = fbd.SelectedPath;
        }
    }
}
