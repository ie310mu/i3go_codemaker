using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using IE310.Core.Components;
using IE310.Core.Utils;
using IE310.Core.UI;
using IE310.Table.Models;

namespace IE310.Core.Controls
{
    public partial class I3Search : UserControl
    {
        public I3Search()
        {
            InitializeComponent();
        }

        private I3SearchInfo searchInfo;
        public I3SearchInfo SearchInfo
        {
            get
            {
                return searchInfo;
            }
            set
            {

                searchInfo = value;
                if (searchInfo == null)
                {
                    return;
                }
                gridSearch.DataSource = searchInfo.Items;
                foreach (I3SearchItem item in searchInfo.Items)
                {
                    I3Row row = new I3Row();
                    I3Cell cell = new I3Cell();
                    cell.Text = item.FieldCaption;
                    row.Cells.Add(cell);
                    cell = new I3Cell();
                    cell.Text = item.FieldType.ToString();
                    row.Cells.Add(cell);
                    cell = new I3Cell();
                    cell.Data = item.LookString;
                    row.Cells.Add(cell);
                    this.i3TableModel.Rows.Add(row);
                }
            }
        }

        private string searchName;
        public string SearchName
        {
            get
            {
                return searchName;
            }
            set
            {
                searchName = value;
                if (string.IsNullOrEmpty(searchName))
                {
                    return;
                }
                if (string.IsNullOrEmpty(searchName.Trim()))
                {
                    return;
                }

                string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "searchSet");
                I3DirectoryUtil.CreateDirctory(fileName);
                fileName = Path.Combine(fileName, SearchName + ".set");
                iecT_Ini1.FileName = fileName;
                iecT_Ini1.Active = true;
            }
        }

        public void Init(I3SearchInfo aInfo, string aSearchName)
        {
            SearchInfo = aInfo;
            SearchName = aSearchName;

            RefreshSchemeList(-1);
        }

        private void pc_VisibleChanged(object sender, EventArgs e)
        {
            if (!pc.Visible)
            {
                return;
            }
            if (gridSearch.BindingContext == null)
            {
                return;
            }
            if (gridSearch.BindingContext[searchInfo.Items] == null)
            {
                return;
            }
            if (gridSearch.BindingContext[searchInfo.Items].Current == null)
            {
                return;
            }

            I3SearchItem item = (I3SearchItem)gridSearch.BindingContext[searchInfo.Items].Current;
            tabControl1.TabPages.Clear();
            switch (item.FieldType)
            {
                case I3SearchItemType.sitString:
                    tabControl1.TabPages.Add(pEqual);
                    edEqualString.Visible = true;
                    edEqualString.Text = item.String1;
                    edEqualNum.Visible = false;
                    edEqualDate.Visible = false;
                    tabControl1.TabPages.Add(pDim);
                    edDimString.Visible = true;
                    edDimString.Text = item.String1;
                    if (item.SearchType == I3SearchType.stNone || item.SearchType == I3SearchType.stEqual)
                    {
                        tabControl1.SelectedTab = pEqual;
                        edEqualString.Focus();
                    }
                    else
                    {
                        tabControl1.SelectedTab = pDim;
                        edDimString.Focus();
                    }
                    break;
                case I3SearchItemType.sitNum:
                    tabControl1.TabPages.Add(pEqual);
                    edEqualString.Visible = false;
                    edEqualNum.Visible = true;
                    edEqualNum.Top = edEqualString.Top;
                    edEqualNum.Text = item.Num1.ToString();
                    edEqualDate.Visible = false;
                    tabControl1.TabPages.Add(pInterval);
                    edNum1.Visible = true;
                    edNum1.Text = item.Num1.ToString();
                    lbIntervalNum.Visible = true;
                    edNum2.Visible = true;
                    edNum2.Text = item.Num2.ToString();
                    dp.Visible = false;
                    if (item.SearchType == I3SearchType.stNone || item.SearchType == I3SearchType.stEqual)
                    {
                        tabControl1.SelectedTab = pEqual;
                        edEqualNum.Focus();
                    }
                    else
                    {
                        tabControl1.SelectedTab = pInterval;
                        edNum1.Focus();
                    }
                    break;
                case I3SearchItemType.sitDate:
                    tabControl1.TabPages.Add(pEqual);
                    edEqualString.Visible = false;
                    edEqualNum.Visible = false;
                    edEqualDate.Visible = true;
                    edEqualDate.Value = item.Date1;
                    edEqualDate.Top = edEqualString.Top;
                    tabControl1.TabPages.Add(pInterval);
                    edNum1.Visible = false;
                    lbIntervalNum.Visible = false;
                    edNum2.Visible = false;
                    dp.Visible = true;
                    dp.Top = edNum1.Top;
                    dp.BeginDate = item.Date1;
                    dp.EndDate = item.Date2;
                    if (item.SearchType == I3SearchType.stNone || item.SearchType == I3SearchType.stEqual)
                    {
                        tabControl1.SelectedTab = pEqual;
                        edEqualDate.Focus();
                    }
                    else
                    {
                        tabControl1.SelectedTab = pInterval;
                        dp.Focus();
                    }
                    break;
                default:
                    break;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            viewSearch.UpdateCurrentRow();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            I3SearchItem item = (I3SearchItem)gridSearch.BindingContext[searchInfo.Items].Current;
            switch (item.FieldType)
            {
                case I3SearchItemType.sitString:
                    if (tabControl1.TabPages[tabControl1.SelectedIndex] == pEqual)
                    {
                        item.SearchType = I3SearchType.stEqual;
                        item.String1 = edEqualString.Text;
                    }
                    else
                    {
                        item.SearchType = I3SearchType.stDim;
                        item.String1 = edDimString.Text;
                    }
                    item.RefreshResult();
                    break;
                case I3SearchItemType.sitNum:
                    if (tabControl1.TabPages[tabControl1.SelectedIndex] == pEqual)
                    {
                        item.SearchType = I3SearchType.stEqual;
                        try
                        {
                            item.Num1 = float.Parse(edEqualNum.Text);
                        }
                        catch
                        {
                            MessageBox.Show("请输入数值作为参数！", "输入数值", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        item.SearchType = I3SearchType.stInterval;
                        try
                        {
                            item.Num1 = float.Parse(edNum1.Text);
                            item.Num2 = float.Parse(edNum2.Text);
                        }
                        catch
                        {
                            MessageBox.Show("请输入数值作为参数！", "输入数值", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    item.RefreshResult();
                    break;
                case I3SearchItemType.sitDate:
                    if (tabControl1.TabPages[tabControl1.SelectedIndex] == pEqual)
                    {
                        item.SearchType = I3SearchType.stEqual;
                        item.Date1 = edEqualDate.Value;
                    }
                    else
                    {
                        item.SearchType = I3SearchType.stInterval;
                        item.Date1 = dp.BeginDate;
                        item.Date2 = dp.EndDate;
                    }
                    item.RefreshResult();
                    break;
                default:
                    break;
            }

            viewSearch.UpdateCurrentRow();
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            I3SearchItem item = (I3SearchItem)gridSearch.BindingContext[searchInfo.Items].Current;
            item.SearchType = I3SearchType.stNone;
            item.RefreshResult();

            viewSearch.UpdateCurrentRow();
        }

        private void btAddScheme_Click(object sender, EventArgs e)
        {
            string schemeName = "新建方案";
            if (!I3GetStringForm.Excute("新建方案", schemeName, out schemeName, false))
            {
                return;
            }
            if (string.IsNullOrEmpty(schemeName))
            {
                return;
            }

            int count = iecT_Ini1.GetInt("Total", "Count", 0);
            count++;
            iecT_Ini1.SetInt("Total", "Count", count);
            iecT_Ini1.SetString("Total", "Name_" + count.ToString(), schemeName);
            iecT_Ini1.Updata();

            searchInfo.WriteToIni(iecT_Ini1, "Scheme_" + count.ToString());

            RefreshSchemeList(count - 1);
        }

        private void RefreshSchemeList(int locateIndex)
        {
            int count = iecT_Ini1.GetInt("Total", "Count", 0);
            lbSchemeList.Items.Clear();
            for (int i = 1; i <= count; i++)
            {
                ListViewItem item =                lbSchemeList.Items.Add(iecT_Ini1.GetString("Total", "Name_" + i.ToString(), "默认名称"));
                item.ImageIndex = 0;
            }

            if(locateIndex >= 0 && locateIndex <= lbSchemeList.Items.Count - 1)
            {
                SelectByIndex(locateIndex);
            }
        }

        private void lbSchemeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSchemeList.FocusedItem == null)
            {
                return;
            }

            SelectByIndex(lbSchemeList.FocusedItem.Index);
        }

        private void SelectByIndex(int index)
        {
            foreach (ListViewItem item in lbSchemeList.Items)
            {
                item.ImageIndex = 0;
            }
            lbSchemeList.Items[index].ImageIndex = 1;

            viewSearch.BeginUpdate();
            searchInfo.ReadFromIni(iecT_Ini1, "Scheme_" + (index + 1).ToString());
            viewSearch.EndUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lbSchemeList.FocusedItem == null)
            {
                return;
            }

            string schemeName = lbSchemeList.Items[lbSchemeList.FocusedItem.Index].Text;
            if (!I3GetStringForm.Excute("新建方案", schemeName, out schemeName, false))
            {
                return;
            }
            if (string.IsNullOrEmpty(schemeName))
            {
                return;
            }

            lbSchemeList.Items[lbSchemeList.FocusedItem.Index].Text = schemeName;
            iecT_Ini1.SetString("Total", "Name_" + (lbSchemeList.FocusedItem.Index + 1).ToString(), schemeName);
            iecT_Ini1.Updata();
        }

        private void btDeleteScheme_Click(object sender, EventArgs e)
        {
            if (lbSchemeList.FocusedItem == null)
            {
                return;
            }
            if (MessageBox.Show("确认删除方案？", "删除方案", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            int delIndex = lbSchemeList.FocusedItem.Index;
            int count = iecT_Ini1.GetInt("Total", "Count", 0);
            iecT_Ini1.SetInt("Total", "Count", count - 1);

            for (int i = delIndex + 1; i <= count; i++)
            {
                searchInfo.ReadFromIni(iecT_Ini1, "Secheme_" + i.ToString());
                searchInfo.WriteToIni(iecT_Ini1, "Secheme_" + (i - 1).ToString());
            }

            RefreshSchemeList(0);
        }

        private void btSaveScheme_Click(object sender, EventArgs e)
        {
            if (lbSchemeList.FocusedItem == null)
            {
                return;
            }

            if (MessageBox.Show("覆盖当前方案？", "保存方案", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                searchInfo.WriteToIni(iecT_Ini1, "Scheme_" + (lbSchemeList.FocusedItem.Index + 1).ToString());
                MessageBox.Show("保存成功！", "保存", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！错误信息：" + ex.Message, "保存", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbSchemeList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbSchemeList.FocusedItem != null)
            {
                if (Search != null)
                {
                    if (!this.DesignMode)
                    {
                        Search(this);
                    }
                }
            }
        }

        public event I3ForSearch Search;
    }

    public delegate void I3ForSearch(object sender);


    public class I3SearchInfo
    {
        public void Copy(I3SearchInfo source)
        {
            FirstIsAnd = source.FirstIsAnd;
            Items.Clear();
            foreach (I3SearchItem item in source.Items)
            {
                I3SearchItem tmpItem = new I3SearchItem();
                tmpItem.FieldName = item.FieldName;
                tmpItem.FieldCaption = item.FieldCaption;
                tmpItem.FieldType = item.FieldType;
                tmpItem.SearchType = item.SearchType;
                tmpItem.LookString = item.LookString;
                tmpItem.SearchString = item.SearchString;
                tmpItem.String1 = item.String1;
                tmpItem.Num1 = item.Num1;
                tmpItem.Num2 = item.Num2;
                tmpItem.Date1 = item.Date1;
                tmpItem.Date2 = item.Date2;
                tmpItem.RefreshResult();
                Items.Add(tmpItem);
            }
        }

        /// <summary>
        /// 指示返回的查找语句，第一个单词是where还是and，默认是and
        /// </summary>
        public bool FirstIsAnd;

        /// <summary>
        /// 查找项
        /// </summary>
        public List<I3SearchItem> Items;

        public I3SearchInfo()
        {
            FirstIsAnd = true;
            Items = new List<I3SearchItem>();
        }

        public void WriteToIni(I3Ini aIni, string sectionName)
        {
            bool oldUp = aIni.UP;
            aIni.UP = false;
            try
            {
                foreach (I3SearchItem item in Items)
                {
                    item.WriteToIni(aIni, sectionName + "_" + item.FieldName);
                }
            }
            finally
            {
                aIni.Updata();
                aIni.UP = oldUp;
            }
        }

        public void ReadFromIni(I3Ini aIni, string sectionName)
        {
            foreach (I3SearchItem item in Items)
            {
                item.ReadFromIni(aIni, sectionName + "_" + item.FieldName);
            }
        }

        public override string ToString()
        {
            string result = "";
            if (FirstIsAnd)
            {
                result = "";
            }
            else
            {
                result = " where ";
            }

            foreach(I3SearchItem item in Items)
            {
                if (string.IsNullOrEmpty(item.SearchString.Trim()))
                {
                    continue;
                }

                if (string.Equals(result," where "))
                {
                    result = result + " " + item.SearchString + " ";
                }
                else
                {
                    result = result + " and " + item.SearchString + " ";
                }
            }

            return result;
        }

        public string ToString2()
        {
            string result = "";

            foreach (I3SearchItem item in Items)
            {
                if (string.IsNullOrEmpty(item.SearchString.Trim()))
                {
                    continue;
                }

                result = result + item.LookString + ",";
            }

            if (result.Length > 0)
            {
                result = I3StringUtil.SubString(result, 0, result.Length - 1);
            }

            return result;
        }
    }

    public class I3SearchItem : INotifyPropertyChanged
    {
        // Declare the PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        // NotifyPropertyChanged will raise the PropertyChanged event passing the
        // source property that is being updated.
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        public void WriteToIni(I3Ini aIni, string sectionName)
        {
            aIni.SetBool(sectionName, "HasSaved", true);

            aIni.SetString(sectionName, "FieldName", fieldName);
            aIni.SetString(sectionName, "FieldCaption", fieldCaption);
            aIni.SetString(sectionName, "SearchType", searchType.ToString());
            aIni.SetString(sectionName, "String1", string1);
            aIni.SetFloat(sectionName, "Num1", num1);
            aIni.SetFloat(sectionName, "Num2", num2);
            aIni.SetTime(sectionName, "Date1", date1);
            aIni.SetTime(sectionName, "Date2", date2);
        }

        public void ReadFromIni(I3Ini aIni, string sectionName)
        {
            if (!aIni.GetBool(sectionName, "HasSaved", false))
            {
                return;
            }

            try
            {
                string st = aIni.GetString(sectionName, "SearchType", I3SearchType.stNone.ToString());
                searchType = (I3SearchType)Enum.Parse(typeof(I3SearchType), st);
            }
            catch
            {
                searchType = I3SearchType.stNone;
            }
            string1 = aIni.GetString(sectionName, "String1", "");
            num1 = aIni.GetFloat(sectionName, "Num1", 0);
            num2 = aIni.GetFloat(sectionName, "Num2", 0);
            date1 = aIni.GetTime(sectionName, "Date1", DateTime.MaxValue);
            date2 = aIni.GetTime(sectionName, "Date2", DateTime.MaxValue);

            RefreshResult();
        }

        /// <summary>
        /// 根据设置刷新结果
        /// </summary>
        public void RefreshResult()
        {
            switch (fieldType)
            {
                case I3SearchItemType.sitString:
                    switch (searchType)
                    {
                        case I3SearchType.stNone:
                            lookString = "";
                            searchString = "";
                            break;
                        case I3SearchType.stEqual:
                            lookString = fieldCaption + " 等于 " + String1;
                            searchString = fieldName + " = " + I3DBUtil.QuotedStr(string1);
                            break;
                        case I3SearchType.stDim:
                            lookString = fieldCaption + " 包含 " + String1;
                            searchString = fieldName + " like " + I3DBUtil.QuotedStr("%" + string1 + "%");
                            break;
                        case I3SearchType.stInterval:
                            lookString = "";
                            searchString = "";
                            break;
                        default:
                            lookString = "";
                            searchString = "";
                            break;
                    }
                    break;
                case I3SearchItemType.sitNum:
                    switch (SearchType)
                    {
                        case I3SearchType.stNone:
                            lookString = "";
                            searchString = "";
                            break;
                        case I3SearchType.stEqual:
                            lookString = fieldCaption + " 等于 " + num1.ToString();
                            searchString = fieldName + " = " + num1.ToString();
                            break;
                        case I3SearchType.stDim:
                            lookString = "";
                            searchString = "";
                            break;
                        case I3SearchType.stInterval:
                            lookString = fieldCaption + "在" + num1.ToString() + "和" + num2.ToString() + "之间";
                            searchString = "(" + fieldName + " >= " + num1.ToString()
                                         + " and " + fieldName + " <= " + num2.ToString() + ")";
                            break;
                        default:
                            lookString = "";
                            searchString = "";
                            break;
                    }
                    break;
                case I3SearchItemType.sitDate:
                    switch (SearchType)
                    {
                        case I3SearchType.stNone:
                            lookString = "";
                            searchString = "";
                            break;
                        case I3SearchType.stEqual:
                            lookString = fieldCaption + " 等于 " + I3DateTimeUtil.ConvertDateTimeToDateString(date1);
                            searchString = " Convert(char(10)," + fieldName + ",126) = "
                                         + I3DBUtil.QuotedStr(I3DateTimeUtil.ConvertDateTimeToDateString(date1));
                            break;
                        case I3SearchType.stDim:
                            lookString = "";
                            searchString = "";
                            break;
                        case I3SearchType.stInterval:
                            lookString = fieldCaption + "在" + I3DateTimeUtil.ConvertDateTimeToDateString(date1)
                                       + "和" + I3DateTimeUtil.ConvertDateTimeToDateString(date2) + "之间";
                            searchString = " Convert(char(10)," + fieldName + ",126) between "
                                         + I3DBUtil.QuotedStr(I3DateTimeUtil.ConvertDateTimeToDateString(date1))
                                         + " and "
                                         + I3DBUtil.QuotedStr(I3DateTimeUtil.ConvertDateTimeToDateString(date2));
                            break;
                        default:
                            lookString = "";
                            searchString = "";
                            break;
                    }
                    break;
                default:
                    lookString = "";
                    searchString = "";
                    break;
            }
        }


        /// <summary>
        /// 字段名
        /// </summary>
        private string fieldName;
        /// <summary>
        /// 要与属性绑定。。。。
        /// </summary>
        public string FieldName
        {
            get
            {
                return fieldName;
            }
            set
            {
                fieldName = value;
                NotifyPropertyChanged("FieldName");
            }
        }

        /// <summary>
        /// 字段中文名
        /// </summary>
        private string fieldCaption;
        public string FieldCaption
        {
            get
            {
                return fieldCaption;
            }
            set
            {
                fieldCaption = value;
                NotifyPropertyChanged("FieldCaption");
            }
        }


        /// <summary>
        /// 查找类型
        /// </summary>
        private I3SearchItemType fieldType;
        public I3SearchItemType FieldType
        {
            get
            {
                return fieldType;
            }
            set
            {
                fieldType = value;
                NotifyPropertyChanged("FieldType");
            }
        }

        /// <summary>
        /// 反映给用户的友好字符串
        /// </summary>
        private string lookString;
        public string LookString
        {
            get
            {
                return lookString;
            }
            set
            {
                lookString = value;
                NotifyPropertyChanged("LookString");
            }
        }

        /// <summary>
        /// 查找字符串
        /// </summary>
        private string searchString;
        public string SearchString
        {
            get
            {
                return searchString;
            }
            set
            {
                searchString = value;
                NotifyPropertyChanged("SearchString");
            }
        }

        /// <summary>
        /// 查找类型，相等，模糊，区间
        /// </summary>
        private I3SearchType searchType;
        public I3SearchType SearchType
        {
            get
            {
                return searchType;
            }
            set
            {
                searchType = value;
                NotifyPropertyChanged("SearchType");
            }
        }

        /// <summary>
        /// 字符串类型的参数
        /// </summary>
        private string string1;
        public string String1
        {
            get
            {
                return string1;
            }
            set
            {
                string1 = value;
                NotifyPropertyChanged("String1");
            }
        }

        /// <summary>
        /// 数据类型，区间时的开始值  或者 相等时的值
        /// </summary>
        private double num1;
        public double Num1
        {
            get
            {
                return num1;
            }
            set
            {
                num1 = value;
                NotifyPropertyChanged("Num1");
            }
        }

        /// <summary>
        /// 数据类型，区间时的结束值
        /// </summary>
        private double num2;
        public double Num2
        {
            get
            {
                return num2;
            }
            set
            {
                num2 = value;
                NotifyPropertyChanged("Num2");
            }
        }

        /// <summary>
        /// 日期类型，区间时的开始值  或者 相等时的值
        /// </summary>
        private DateTime date1;
        public DateTime Date1
        {
            get
            {
                return date1;
            }
            set
            {
                date1 = value;
                NotifyPropertyChanged("Date1");
            }
        }

        /// <summary>
        /// 日期类型，区间时的结束值
        /// </summary>
        private DateTime date2;
        public DateTime Date2
        {
            get
            {
                return date2;
            }
            set
            {
                date2 = value;
                NotifyPropertyChanged("Date2");
            }
        }

        public I3SearchItem()
        {
            fieldName = "";
            fieldCaption = "";
            fieldType = I3SearchItemType.sitString;
            lookString = "";
            searchString = "";
            searchType = I3SearchType.stNone;
            string1 = "";
            num1 = 0;
            num2 = 0;
            date1 = DateTime.Now;
            date2 = DateTime.Now;
        }
    }

    public enum I3SearchItemType
    {
        sitString = 0,
        sitNum = 1,
        sitDate = 2,
    }

    public enum I3SearchType
    {
        stNone = 0,    //不查找
        stEqual = 1,   //相等
        stDim = 2,     //模糊
        stInterval =3, //区间
    }
}
