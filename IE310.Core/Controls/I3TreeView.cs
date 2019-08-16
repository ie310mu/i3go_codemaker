/*
 *  通过指定数据源和绑定信息，自动维护树
 *  
 *  使用DataTable作为数据源时，会自动响应列表和项目的更改，其中项目的更改要在Position改变时才会体现  ****
 *  
 *  如果使用其他的数据源，则继承自IList或者IListSource才可以
 *  而其中如果要自动响应列表的更改，则应实现IBindingListView或IBindingList，或者使用BindingList<T>或者I3BindingList<T>
 *  (I3BindingList优化了删除事件，使得e.NewIndex有意义，处理时不用循环查找List)
 * 如果要响应属性的更改，<T>中的T，要实现INotifyPropertyChanged接口，则在每一个属性变化时都会引发ListChanged.ItemChanged事件 
 * 
 * 当批量操作数据时，最好的操作方法是使用BeginUpdate/EndUpdate或者DataSource=null/DataSource=source的方式
 * 下面是1000条数据中删除500条数据，各种情况下的大致响应时间：
 * 
 *                              普通   Update   DataSource
 *     I3BingingList      24s   2s            0.5s
 *     BingingList         25s   2.5s         0.5s
 *     DataTable          23s   1.3s         0.3s
 * 
 * 
 *     TreeView的StateImageList会替换默认的CheckBox，值分别为0，1
 *     ImageList则在其后面，具体由TreeNode.ImageIndex,SelectedImageIndex决定，SelectedImageIndex优先级较大
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using IE310.Core.Utils;

namespace IE310.Core.Controls
{
    [ToolboxBitmap(typeof(TreeView))]
    public partial class I3TreeView : TreeView
    {
        #region 选择项常亮，右键选择
        //节点列表
        private Hashtable nodeTable2;
        //选中背景色
        private Color selectedBackColor = SystemColors.Highlight;

        public I3TreeView()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 获取或设置选中背景色
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "Highlight"), Description("选中背景色")]
        public Color SelectedBackColor
        {
            get
            {
                return this.selectedBackColor;
            }
            set
            {
                this.selectedBackColor = value;
            }
        }

        /// <summary>
        /// 获取节点列表
        /// </summary>
        [Browsable(false)]
        internal Hashtable NodeTable2
        {
            get
            {
                if (nodeTable2 == null)
                {
                    nodeTable2 = typeof(TreeView).GetField("nodeTable", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this) as Hashtable;
                }
                return nodeTable2;
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x204e && this.NodeTable2 != null)
            {
                //获取参数
                NMTVCUSTOMDRAW lParam = (NMTVCUSTOMDRAW)m.GetLParam(typeof(NMTVCUSTOMDRAW));
                //获取当前的节点
                TreeNode node = this.NodeTable2[lParam.nmcd.dwItemSpec] as TreeNode;
                if (node != null && node.IsSelected)
                {
                    lParam.clrTextBk = ColorTranslator.ToWin32(this.selectedBackColor);
                    lParam.clrText = ColorTranslator.ToWin32(Color.White);
                    Marshal.StructureToPtr(lParam, m.LParam, false);
                }
            }

            base.WndProc(ref m);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = this.GetNodeAt(e.X, e.Y);
                if (node != null)
                {
                    this.SelectedNode = node;
                }
            }
        }

        #region NativeStruct
        [StructLayout(LayoutKind.Sequential)]
        internal struct NMHDR
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public int code;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct NMCUSTOMDRAW
        {
            public NMHDR nmcd;
            public int dwDrawStage;
            public IntPtr hdc;
            public RECT rc;
            public IntPtr dwItemSpec;
            public int uItemState;
            public IntPtr lItemlParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class NMTVCUSTOMDRAW
        {
            public NMCUSTOMDRAW nmcd;
            public int clrText;
            public int clrTextBk;
            public int iLevel;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
        }

        #endregion


        #region BindingInfo

        private I3TreeViewBindingMemberInfo bindingInfo = new I3TreeViewBindingMemberInfo();
        /// <summary>
        /// 数据字段设置
        /// </summary>
        [Category("DataSource")]
        [DefaultValue(null)]
        public I3TreeViewBindingMemberInfo BindingInfo
        {
            get
            {
                return this.bindingInfo;
            }
            set
            {
                if (!this.bindingInfo.Equals(value))
                {
                    if (this.bindingInfo != null)
                    {
                        this.bindingInfo.PropertyChanged -= new PropertyChangedEventHandler(bindingInfo_PropertyChanged);
                    }

                    this.bindingInfo = value;
                    try
                    {
                        this.SetDataConnection(this.dataSource, this.bindingInfo, false);
                    }
                    catch
                    {
                        //this.bindingInfo = new I3TreeViewBindingMemberInfo();
                    }
                    this.bindingInfo.PropertyChanged += new PropertyChangedEventHandler(bindingInfo_PropertyChanged);
                }
            }
        }

        private void bindingInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                //this.SetDataConnection(this.dataSource, this.bindingInfo, false);  //未使用新的bindingInfo对象
                this.SetDataConnection(this.dataSource, this.bindingInfo, true);
            }
            catch
            {
                //this.bindingInfo = new I3TreeViewBindingMemberInfo();
            }
        }


        private PropertyDescriptor keyDescriptor;
        private PropertyDescriptor parentKeyDescriptor;
        private PropertyDescriptor displayDescriptor;
        private PropertyDescriptor sortDescriptor;
        /// <summary>
        /// 排序属性描述器
        /// </summary>
        public PropertyDescriptor SortDescriptor
        {
            get
            {
                return sortDescriptor;
            }
        }
        private PropertyDescriptor checkDescriptor;
        private PropertyDescriptor imageDescriptor;
        private bool BindingMemberInfoInDataManager(I3TreeViewBindingMemberInfo bindingMemberInfo)
        {
            this.keyDescriptor = null;
            this.parentKeyDescriptor = null;
            this.displayDescriptor = null;
            this.sortDescriptor = null;
            this.checkDescriptor = null;
            this.imageDescriptor = null;

            if (this.dataManager != null)
            {

                PropertyDescriptorCollection itemProperties = this.dataManager.GetItemProperties();
                int count = itemProperties.Count;
                for (int i = 0; i < count; i++)
                {
                    //if (!typeof(IList).IsAssignableFrom(itemProperties[i].PropertyType))//???????????????
                    //{
                    if (itemProperties[i].Name.Equals(bindingMemberInfo.KeyMember))
                    {
                        this.keyDescriptor = itemProperties[i];
                    }
                    if (itemProperties[i].Name.Equals(bindingMemberInfo.ParentKeyMember))
                    {
                        this.parentKeyDescriptor = itemProperties[i];
                    }
                    if (itemProperties[i].Name.Equals(bindingMemberInfo.DisplayMember))
                    {
                        this.displayDescriptor = itemProperties[i];
                    }
                    if (itemProperties[i].Name.Equals(bindingMemberInfo.SortMember))
                    {
                        this.sortDescriptor = itemProperties[i];
                    }
                    if (itemProperties[i].Name.Equals(bindingMemberInfo.CheckMember))
                    {
                        this.checkDescriptor = itemProperties[i];
                    }
                    if (itemProperties[i].Name.Equals(bindingMemberInfo.ImageMember))
                    {
                        this.imageDescriptor = itemProperties[i];
                    }
                    //}
                }


                if (this.keyDescriptor != null && this.parentKeyDescriptor != null && this.displayDescriptor != null)
                {
                    return true;
                }
            }


            return false;
        }

        #endregion


        #region DataSource

        private object dataSource;
        /// <summary>
        /// 获取或设置数据源
        /// 必须实现IList或IListSource
        /// 注意，不能正确响应Insert方法，会以Add形式加到最后面
        /// </summary>
        [Category("DataSource")]
        [Browsable(false)]
        public object DataSource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                if (((value != null) && !(value is IList)) && !(value is IListSource))
                {
                    throw new ArgumentException("DataSource必须实现IList或IListSource");
                }
                if (this.dataSource != value)
                {
                    //try
                    //{
                    this.SetDataConnection(value, this.bindingInfo, false);
                    //}
                    //catch
                    //{
                    //this.bindingInfo = new I3TreeViewBindingMemberInfo();
                    //}
                    //if (value == null)
                    //{
                    //    this.bindingInfo = new I3TreeViewBindingMemberInfo();
                    //}
                }

            }
        }

        private CurrencyManager dataManager;
        protected CurrencyManager DataManager
        {
            get
            {
                return this.dataManager;
            }
        }

        private bool isDataSourceInitEventHooked;
        private void UnwireDataSource()
        {
            if (this.dataSource is IComponent)
            {
                ((IComponent)this.dataSource).Disposed -= new EventHandler(this.DataSourceDisposed);
            }
            ISupportInitializeNotification dataSource = this.dataSource as ISupportInitializeNotification;
            if ((dataSource != null) && this.isDataSourceInitEventHooked)
            {
                dataSource.Initialized -= new EventHandler(this.DataSourceInitialized);
                this.isDataSourceInitEventHooked = false;
            }
        }

        private void DataSourceInitialized(object sender, EventArgs e)
        {
            this.SetDataConnection(this.dataSource, this.bindingInfo, true);
        }

        private void DataSourceDisposed(object sender, EventArgs e)
        {
            this.SetDataConnection(null, new I3TreeViewBindingMemberInfo(), true);
        }

        private bool isDataSourceInitialized;
        private void WireDataSource()
        {
            if (this.dataSource is IComponent)
            {
                ((IComponent)this.dataSource).Disposed += new EventHandler(this.DataSourceDisposed);
            }
            ISupportInitializeNotification dataSource = this.dataSource as ISupportInitializeNotification;
            if ((dataSource != null) && !dataSource.IsInitialized)
            {
                dataSource.Initialized += new EventHandler(this.DataSourceInitialized);
                this.isDataSourceInitEventHooked = true;
                this.isDataSourceInitialized = false;
            }
            else
            {
                this.isDataSourceInitialized = true;
            }
        }

        private bool inSetDataConnection;
        private void SetDataConnection(object newDataSource, I3TreeViewBindingMemberInfo newBindingInfo, bool force)
        {
            bool flag = this.dataSource != newDataSource;
            bool flag2 = !this.bindingInfo.Equals(newBindingInfo);
            if (!this.inSetDataConnection)
            {
                try
                {
                    if ((force || flag) || flag2)
                    {
                        this.inSetDataConnection = true;

                        this.nodeList.Clear();
                        this.Nodes.Clear();

                        IList list = (this.DataManager != null) ? this.DataManager.List : null;
                        bool flag3 = this.DataManager == null;
                        this.UnwireDataSource();
                        this.dataSource = newDataSource;
                        this.bindingInfo = newBindingInfo;
                        this.WireDataSource();
                        if (this.isDataSourceInitialized)
                        {
                            CurrencyManager manager = null;
                            if (((newDataSource != null) && (this.BindingContext != null)) && (newDataSource != Convert.DBNull))
                            {
                                manager = (CurrencyManager)this.BindingContext[newDataSource];
                            }
                            if (this.dataManager != manager)
                            {
                                if (this.dataManager != null)
                                {
                                    this.dataManager.PositionChanged -= new EventHandler(this.DataManager_PositionChanged);
                                    this.dataManager.ListChanged -= new ListChangedEventHandler(dataManager_ListChanged);
                                }
                                this.dataManager = manager;
                                if (this.dataManager != null)
                                {
                                    this.dataManager.PositionChanged += new EventHandler(this.DataManager_PositionChanged);
                                    this.dataManager.ListChanged += new ListChangedEventHandler(dataManager_ListChanged);
                                }
                            }
                            if (((this.dataManager != null) && (flag2 || flag)) && (((this.bindingInfo != null) && this.bindingInfo.Check()) && !this.BindingMemberInfoInDataManager(this.bindingInfo)))
                            {
                                throw new ArgumentException("BindingMemberInfo Error");
                            }
                            //if (((this.dataManager != null) && ((flag || flag2) || force)) && (flag2 || (force && ((list != this.dataManager.List) || flag3))))
                            //{
                            //this.DataManager_ItemChanged(this.dataManager, null);
                            //}
                        }
                        this.InitNodes();
                        if (this.Nodes.Count > 0)
                        {
                            this.SelectedNode = this.Nodes[0];
                        }
                        //this.displayMemberConverter = null;
                    }
                    //if (flag)
                    //{
                    //    this.OnDataSourceChanged(EventArgs.Empty);
                    //}
                    //if (flag2)
                    //{
                    //    this.OnBindingMemberInfoChanged(EventArgs.Empty);
                    //}
                }
                finally
                {
                    this.inSetDataConnection = false;
                }
            }
        }

        #endregion


        #region DataSource Event

        private void DataManager_PositionChanged(object sender, EventArgs e)
        {
            IList list = this.dataManager.List;
            if (this.dataManager.Position < 0 || this.dataManager.Position > list.Count - 1)
            {
                this.SelectedNode = null;
                return;
            }

            object key = this.keyDescriptor.GetValue(list[this.dataManager.Position]);
            if (!this.nodeList.ContainsKey(key) || this.nodeList[key] == null)
            {
                this.SelectedNode = null;
                return;
            }

            if (this.nodeList[key].Equals(this.SelectedNode))
            {
                return;
            }
            this.SelectedNode = this.nodeList[key];
        }

        private bool inDataManagerListChanging = false;
        private void dataManager_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (this.inDataManagerListChanging)
            {
                return;
            }

            this.inDataManagerListChanging = true;
            try
            {
                switch (e.ListChangedType)
                {
                    case ListChangedType.Reset:
                        {
                            #region Reset
                            object oldKey = this.SelectedNode == null ? null : this.keyDescriptor.GetValue(this.SelectedNode.Tag);
                            this.InitNodes();
                            if (this.nodeList.ContainsKey(oldKey))
                            {
                                this.SelectedNodeKey = oldKey;
                            }
                            else
                            {
                                if (this.Nodes.Count > 0)
                                {
                                    this.SelectedNode = this.Nodes[0];
                                }
                            }
                            #endregion
                            break;
                        }
                    case ListChangedType.ItemAdded:
                        {
                            #region ItemAdded
                            TreeNode addNode = this.AddNode(this.dataManager.List[e.NewIndex]);
                            this.SortSubNodes(addNode.Parent);
                            this.SelectedNode = addNode;
                            #endregion
                            break;
                        }
                    case ListChangedType.ItemDeleted:
                        {
                            #region ItemDeleted
                            object deleteKey = null;
                            if (this.dataManager.List.GetType().Name == "I3BindingList`1")
                            {
                                object deleteItem = this.dataManager.List[e.NewIndex];
                                deleteKey = this.keyDescriptor.GetValue(deleteItem);
                            }
                            else
                            {
                                foreach (KeyValuePair<object, TreeNode> pair in this.nodeList)
                                {
                                    if (!this.dataManager.List.Contains(pair.Value.Tag))
                                    {
                                        deleteKey = pair.Key;
                                        break;
                                    }
                                }
                            }
                            if (deleteKey != null)
                            {
                                TreeNode deleteNode = this.nodeList[deleteKey];
                                TreeNode nextNode = this.GetNextNode(deleteNode);
                                this.DeleteNode(deleteKey);
                                if (nextNode != null)
                                {
                                    this.SelectedNode = nextNode;
                                }
                            }
                            #endregion
                            break;
                        }
                    case ListChangedType.ItemChanged:
                        {
                            if (this.inPushingCheckValue)
                            {
                                break;
                            }
                            #region ItemChanged
                            object oldKey = this.SelectedNode == null ? null : this.keyDescriptor.GetValue(this.SelectedNode.Tag);
                            object key = this.keyDescriptor.GetValue(this.dataManager.List[e.NewIndex]);
                            KeyValuePair<object, TreeNode> findPair = new KeyValuePair<object, TreeNode>(null, null);
                            if (!this.nodeList.ContainsKey(key))
                            {
                                foreach (KeyValuePair<object, TreeNode> pair in this.nodeList)
                                {
                                    if (this.dataManager.List[e.NewIndex] == pair.Value.Tag)
                                    {
                                        findPair = pair;
                                        break;
                                    }
                                }
                                if (findPair.Key != null)
                                {
                                    this.nodeList.Remove(findPair.Key);
                                    this.nodeList.Add(key, findPair.Value);
                                }
                            }
                            this.UpdateNode(key);
                            if (oldKey != null)
                            {
                                object nowKey = this.SelectedNode == null ? null : this.keyDescriptor.GetValue(this.SelectedNode.Tag);
                                if (!oldKey.Equals(nowKey))
                                {
                                    this.SelectedNodeKey = oldKey;
                                }
                            }
                            #endregion
                            break;
                        }
                    default:
                        break;
                }
            }
            finally
            {
                this.inDataManagerListChanging = false;
            }
        }

        #endregion


        #region Node

        /// <summary>
        /// 获取下一个结点，顺序：后面的同级结点，前面的同级结点，父结点，第1个子结点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public TreeNode GetNextNode(TreeNode node)
        {
            TreeNode nextNode = node.NextNode;
            if (nextNode == null)
            {
                nextNode = node.PrevNode;
            }
            if (nextNode == null)
            {
                nextNode = node.Parent;
            }
            if (nextNode == null && node.Nodes.Count > 0)
            {
                nextNode = node.Nodes[0];
            }
            return nextNode;
        }

        /// <summary>
        /// 获取选择项的key
        /// 通过设置key来选择对应的结点
        /// </summary>
        public object SelectedNodeKey
        {
            set
            {
                if (this.nodeList.ContainsKey(value))
                {
                    this.SelectedNode = this.nodeList[value];
                }
            }
        }

        /// <summary>
        /// 设置后会强制引发OnAfterSelect
        /// </summary>
        public new TreeNode SelectedNode
        {
            get
            {
                return base.SelectedNode;
            }
            set
            {
                if (base.SelectedNode == value)
                {
                    this.OnAfterSelect(new TreeViewEventArgs(value, TreeViewAction.Unknown));
                }
                else
                {
                    base.SelectedNode = value;
                }
            }
        }

        private Dictionary<object, TreeNode> nodeList = new Dictionary<object, TreeNode>();
        //初始化
        private void InitNodes()
        {
            if (this.dataManager == null)
            {
                return;
            }

            this.nodeList.Clear();
            this.Nodes.Clear();

            List<object> list = this.GetSortedItemList(this.dataManager.List);

            //添加
            this.BeginUpdate();
            try
            {
                foreach (object item in list)
                {
                    this.AddNode(item);
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        //增加
        private TreeNode AddNode(object item)
        {
            object key = this.keyDescriptor.GetValue(item);
            object parentKey = this.parentKeyDescriptor.GetValue(item);
            object display = this.displayDescriptor.GetValue(item);

            //增加
            TreeNodeCollection nodes = this.nodeList.ContainsKey(parentKey) ? this.nodeList[parentKey].Nodes : this.Nodes;
            TreeNode node = nodes.Add(display.ToString());
            if (this.checkDescriptor != null)
            {
                node.Checked = Convert.ToBoolean(this.checkDescriptor.GetValue(item));
            }
            if (this.imageDescriptor != null)
            {
                int oldImageIndex = node.ImageIndex;
                node.ImageIndex = Convert.ToInt32(this.imageDescriptor.GetValue(item));
                if (node.SelectedImageIndex == -1 || node.SelectedImageIndex == oldImageIndex)
                {
                    node.SelectedImageIndex = Convert.ToInt32(this.imageDescriptor.GetValue(item));
                }
            }
            node.Tag = item;
            this.nodeList.Add(key, node);

            //从根结点中查找属于此结点的子结点并移动之
            List<TreeNode> subNodes = new List<TreeNode>();
            foreach (TreeNode subNode in this.Nodes)
            {
                object subParentKey = this.parentKeyDescriptor.GetValue(subNode.Tag);
                if (subParentKey.Equals(key))
                {
                    subNodes.Add(subNode);
                }
            }
            foreach (TreeNode subNode in subNodes)
            {
                subNode.Remove();
                node.Nodes.Add(subNode);
            }

            return node;
        }

        //删除
        private void DeleteNode(object key)
        {
            TreeNode deleteNode = this.nodeList[key];

            this.BeginUpdate();
            try
            {
                //移除结点
                deleteNode.Remove();
                this.nodeList.Remove(key);

                MoveSubNodesToRoot(deleteNode);
            }
            finally
            {
                this.EndUpdate();
            }
        }

        //将子结点移动为根结点
        private void MoveSubNodesToRoot(TreeNode parentNode)
        {
            while (parentNode.Nodes.Count > 0)
            {
                TreeNode node = parentNode.Nodes[0];
                node.Remove();
                this.Nodes.Add(node);
            }

            this.SortSubNodes(null);
        }

        //更新
        private void UpdateNode(object key)
        {
            if (!this.nodeList.ContainsKey(key) || this.nodeList[key] == null)
            {
                return;
            }

            this.BeginUpdate();
            try
            {
                //Text
                TreeNode node = this.nodeList[key];
                node.Text = this.displayDescriptor.GetValue(node.Tag).ToString();

                //check
                if (this.checkDescriptor != null)
                {
                    node.Checked = Convert.ToBoolean(this.checkDescriptor.GetValue(node.Tag));
                }

                //image
                if (this.imageDescriptor != null)
                {
                    int oldImageIndex = node.ImageIndex;
                    node.ImageIndex = Convert.ToInt32(this.imageDescriptor.GetValue(node.Tag));
                    if (node.SelectedImageIndex == -1 || node.SelectedImageIndex == oldImageIndex)
                    {
                        node.SelectedImageIndex = Convert.ToInt32(this.imageDescriptor.GetValue(node.Tag));
                    }
                }

                #region parent
                object parentKey = this.parentKeyDescriptor.GetValue(node.Tag);
                TreeNode parentNode = this.nodeList.ContainsKey(parentKey) ? this.nodeList[parentKey] : null;
                if (parentNode == null)
                {
                    if (node.Parent != null)
                    {
                        node.Remove();
                        this.Nodes.Add(node);
                    }
                }
                else
                {
                    if (!parentNode.Equals(node.Parent))
                    {
                        node.Remove();
                        parentNode.Nodes.Add(node);
                    }
                }
                #endregion

                #region sub
                if (node.Nodes.Count > 0)
                {
                    object oldKey = this.parentKeyDescriptor.GetValue(node.Nodes[0].Tag);
                    if (!key.Equals(oldKey))//添加为根结点
                    {
                        MoveSubNodesToRoot(node);
                    }
                }
                //从this.Nodes中查找子结点并拿过来(ID不能与其他现有结点重复，所以不会是其他结点的子结点)
                //而且是已经经过排序的，不需要重新排序
                List<TreeNode> subNodes = new List<TreeNode>();
                foreach (TreeNode subNode in this.Nodes)
                {
                    if (key.Equals(this.parentKeyDescriptor.GetValue(subNode.Tag)))
                    {
                        subNodes.Add(subNode);
                    }
                }
                foreach (TreeNode subNode in subNodes)
                {
                    subNode.Remove();
                    node.Nodes.Add(subNode);
                }
                #endregion

                //sort
                this.SortSubNodes(node.Parent);
            }
            finally
            {
                this.EndUpdate();
            }
        }

        //选择后
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            if (this.IsInUpdateState)
            {
                return;
            }

            base.OnAfterSelect(e);

            if (this.dataManager == null)
            {
                return;
            }
            IList list = this.dataManager.List;
            if (this.SelectedNode == null)
            {
                //this.dataManager.Position = -1;
                I3CurrencyManagerUtil.UpdatePosition(this.dataManager, -1);
            }
            else
            {
                int index = list.IndexOf(this.SelectedNode.Tag);
                //this.dataManager.Position = index;
                I3CurrencyManagerUtil.UpdatePosition(this.dataManager, index);
            }
        }

        private int updateCount = 0;
        public new void BeginUpdate()
        {
            this.updateCount++;
            if (this.updateCount == 1)
            {
                base.BeginUpdate();
            }
        }
        public new void EndUpdate()
        {
            if (this.updateCount == 0)
            {
                return;
            }
            this.updateCount--;
            if (this.updateCount == 0)
            {
                base.EndUpdate();
                if (this.SelectedNode != null && this.dataManager != null)
                {
                    int index = this.dataManager.List.IndexOf(this.SelectedNode.Tag);
                    I3CurrencyManagerUtil.UpdatePosition(this.dataManager, index);
                }
            }
        }
        public bool IsInUpdateState
        {
            get
            {
                return this.updateCount > 0;
            }
        }

        #endregion


        #region Sort

        /// <summary>
        /// 获取排序后的列表 (无排序信息时返回默认列表)
        /// </summary>
        private List<object> GetSortedItemList(IList list)
        {
            List<object> result = new List<object>();

            if (this.sortDescriptor == null)
            {
                foreach (object item in list)
                {
                    result.Add(item);
                }
            }
            else
            {
                SortedList<object, List<object>> sortedList = new SortedList<object, List<object>>();
                foreach (object item in list)
                {
                    object sort = this.sortDescriptor.GetValue(item);
                    if (sortedList.ContainsKey(sort))
                    {
                        List<object> innerList = sortedList[sort];
                        innerList.Add(item);
                    }
                    else
                    {
                        List<object> innerList = new List<object>();
                        innerList.Add(item);
                        sortedList.Add(sort, innerList);
                    }
                }

                if (this.bindingInfo.SortMode == I3TreeViewSortMode.ASC)
                {
                    foreach (List<object> innerList in sortedList.Values)
                    {
                        foreach (object item in innerList)
                        {
                            result.Add(item);
                        }
                    }
                }
                else
                {
                    for (int i = sortedList.Count - 1; i >= 0; i--)
                    {
                        List<object> innerList = sortedList[sortedList.Keys[i]];
                        foreach (object item in innerList)
                        {
                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 排序子结点
        /// </summary>
        /// <param name="parentNode"></param>
        private void SortSubNodes(TreeNode parentNode)
        {
            this.BeginUpdate();
            try
            {
                //排序
                TreeNodeCollection nodes = parentNode == null ? this.Nodes : parentNode.Nodes;
                List<object> list = new List<object>();
                foreach (TreeNode node in nodes)
                {
                    list.Add(node.Tag);
                }
                list = GetSortedItemList(list);

                List<TreeNode> sortedNodes = new List<TreeNode>();
                foreach (object item in list)
                {
                    TreeNode node = this.nodeList[this.keyDescriptor.GetValue(item)];
                    sortedNodes.Add(node);
                }

                for (int i = 0; i < sortedNodes.Count; i++)
                {
                    TreeNode preNode = i == 0 ? null : nodes[i - 1];
                    if (sortedNodes[i].PrevNode != preNode)
                    {
                        sortedNodes[i].Remove();
                        if (preNode == null)
                        {
                            nodes.Insert(0, sortedNodes[i]);
                        }
                        else
                        {
                            int index = nodes.IndexOf(preNode);
                            nodes.Insert(index + 1, sortedNodes[i]);
                        }
                    }
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        #endregion


        #region Check

        /// <summary>
        /// 全选 
        /// </summary>
        public void CheckAll()
        {
            this.BeginUpdate();
            try
            {
                foreach (TreeNode node in this.Nodes)
                {
                    node.Checked = true;
                    this.PushCheckValue(node);
                    this.ChangeSubNodesCheckState(node);
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }
        /// <summary>
        /// 全不选
        /// </summary>
        public void UnCheckAll()
        {
            this.BeginUpdate();
            try
            {
                foreach (TreeNode node in this.Nodes)
                {
                    node.Checked = false;
                    this.PushCheckValue(node);
                    this.ChangeSubNodesCheckState(node);
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }
        /// <summary>
        /// 反选
        /// </summary>
        public void ReverseAll()
        {
            this.BeginUpdate();
            try
            {
                foreach (TreeNode node in this.Nodes)
                {
                    this.ReverseAllSub(node);
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }
        private void ReverseAllSub(TreeNode node)
        {
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode subNode in node.Nodes)
                {
                    this.ReverseAllSub(subNode);
                }
            }
            else
            {
                node.Checked = !node.Checked;
                this.PushCheckValue(node);
                this.ChangeParentNodesCheckState(node);
            }
        }


        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            if (this.IsInUpdateState)
            {
                return;
            }

            this.BeginUpdate();
            try
            {
                base.OnAfterCheck(e);
                this.PushCheckValue(e.Node);
                this.ChangeSubNodesCheckState(e.Node);
                this.ChangeParentNodesCheckState(e.Node);
            }
            finally
            {
                this.EndUpdate();
            }
        }

        //设置父结点的选择状态
        private void ChangeParentNodesCheckState(TreeNode node)
        {
            if (node == null || node.Parent == null)
            {
                return;
            }

            int checkedCount = 0;
            foreach (TreeNode subNode in node.Parent.Nodes)
            {
                if (subNode.Checked)
                {
                    checkedCount++;
                }
            }

            node.Parent.Checked = checkedCount > 0;
            this.PushCheckValue(node.Parent);
            this.ChangeParentNodesCheckState(node.Parent);
        }

        //设置子结点的选择状态
        private void ChangeSubNodesCheckState(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            foreach (TreeNode subNode in node.Nodes)
            {
                subNode.Checked = node.Checked;
                this.PushCheckValue(subNode);
                this.ChangeSubNodesCheckState(subNode);
            }
        }

        private bool inPushingCheckValue = false;
        //将TreeNode的选择状态推回数据源，此种情况下ItemChagned事件不处理它        
        private void PushCheckValue(TreeNode node)
        {
            if (this.checkDescriptor != null && node != null)
            {
                bool nowValue = Convert.ToBoolean(this.checkDescriptor.GetValue(node.Tag));
                if (nowValue != node.Checked)
                {
                    this.inPushingCheckValue = true;
                    try
                    {
                        this.checkDescriptor.SetValue(node.Tag, node.Checked);
                    }
                    finally
                    {
                        this.inPushingCheckValue = false;
                    }
                }
            }
        }

        #endregion


    }



    #region I3TreeViewBindingMemberInfo

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class I3TreeViewBindingMemberInfo : INotifyPropertyChanged
    {
        public I3TreeViewBindingMemberInfo(string keyMember, string parentKeyMember, string displayMember, string sortMember,
            string checkMember, string imageMember)
        {
            this.keyMember = keyMember;
            this.parentKeyMember = parentKeyMember;
            this.displayMember = displayMember;
            this.sortMember = sortMember;
            this.sortMode = I3TreeViewSortMode.ASC;
            this.checkMember = checkMember;
            this.imageMember = imageMember;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public I3TreeViewBindingMemberInfo()
        {
            this.keyMember = "";
            this.parentKeyMember = "";
            this.displayMember = "";
            this.sortMember = "";
            this.sortMode = I3TreeViewSortMode.ASC;
            this.checkMember = "";
            this.imageMember = "";
        }

        public bool Check()
        {
            return !string.IsNullOrEmpty(keyMember) && !string.IsNullOrEmpty(parentKeyMember) && !string.IsNullOrEmpty(displayMember);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(I3TreeViewBindingMemberInfo))
            {
                return false;
            }
            I3TreeViewBindingMemberInfo dest = obj as I3TreeViewBindingMemberInfo;
            return string.Equals(this.keyMember, dest.keyMember) && string.Equals(this.parentKeyMember, dest.parentKeyMember)
                        && string.Equals(this.displayMember, dest.displayMember) && string.Equals(this.sortMember, dest.sortMember)
                        && this.sortMode == dest.sortMode && string.Equals(this.imageMember, dest.imageMember);
        }

        private string keyMember;
        [NotifyParentProperty(true)]
        public string KeyMember
        {
            get
            {
                return this.keyMember;
            }
            set
            {
                if (!string.Equals(this.keyMember, value))
                {
                    this.keyMember = value;
                    OnPropertyChanged("KeyMember");
                }
            }
        }

        private string parentKeyMember;
        [NotifyParentProperty(true)]
        public string ParentKeyMember
        {
            get
            {
                return this.parentKeyMember;
            }
            set
            {
                if (!string.Equals(this.parentKeyMember, value))
                {
                    this.parentKeyMember = value;
                    OnPropertyChanged("ParentKeyMember");
                }
            }
        }

        private string displayMember;
        [NotifyParentProperty(true)]
        public string DisplayMember
        {
            get
            {
                return this.displayMember;
            }
            set
            {
                if (!string.Equals(this.displayMember, value))
                {
                    this.displayMember = value;
                    OnPropertyChanged("DisplayMember");
                }
            }
        }

        private string sortMember;
        [NotifyParentProperty(true)]
        public string SortMember
        {
            get
            {
                return sortMember;
            }
            set
            {
                if (!string.Equals(this.sortMember, value))
                {
                    this.sortMember = value;
                    OnPropertyChanged("SortMember");
                }
            }
        }

        private I3TreeViewSortMode sortMode;
        [NotifyParentProperty(true)]
        [DefaultValue(I3TreeViewSortMode.ASC)]
        public I3TreeViewSortMode SortMode
        {
            get
            {
                return sortMode;
            }
            set
            {
                if (this.sortMode != value)
                {
                    this.sortMode = value;
                    OnPropertyChanged("SortMode");
                }
            }
        }

        private string checkMember;
        [NotifyParentProperty(true)]
        public string CheckMember
        {
            get
            {
                return checkMember;
            }
            set
            {
                if (!string.Equals(this.checkMember, value))
                {
                    this.checkMember = value;
                    OnPropertyChanged("CheckMember");
                }
            }
        }

        private string imageMember;
        [NotifyParentProperty(true)]
        public string ImageMember
        {
            get
            {
                return this.imageMember;
            }
            set
            {
                if (!string.Equals(this.imageMember, value))
                {
                    this.imageMember = value;
                    OnPropertyChanged("ImageMember");
                }
            }
        }


        public override string ToString()
        {
            return "Key[" + this.keyMember + "],ParentKey[" + this.parentKeyMember + "],Display[" + this.displayMember
                      + "],Sort[" + this.sortMember + "." + this.sortMode.ToString() + "],Check[" + this.checkMember
                      + "],Image[" + this.checkMember + "]";
        }
    }

    #endregion

    public enum I3TreeViewSortMode
    {
        ASC = 1,
        DESC = 2,
    }
}
