using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IE310.Table.Column
{
    /// <summary>
    /// 列显示帮助类
    /// </summary>
    public static class ColumnDisplayHelper
    {
        /// <summary>
        /// 获取I3ColumnModel的列设置数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<ColumnDisplayItem> GetDisplayData(I3ColumnModel model)
        {

            List<ColumnDisplayItem> result = new List<ColumnDisplayItem>();
            foreach (I3Column column in model.Columns)
            {
                ColumnDisplayItem item = new ColumnDisplayItem(column.Key, column.Visible, column.Width, column.SortOrder);
                result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// 根据列设置数据设置I3ColumnModel的列属性
        /// 注意：在没有行数据时才能执行此功能     如果有排序，在数据加载后，应该重新调用ColumnDisplayHelper.ReSort(I3ColumnModel)方法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="displayData"></param>
        public static void SetDisplayData(I3ColumnModel model, List<ColumnDisplayItem> displayData)
        {
            if (model == null)
            {
                return;
            }
            if (model.Table != null && model.Table.TableModel != null && model.Table.TableModel.Rows.Count > 0)
            {
                throw new Exception("can not set displayData when the tableModel has data.");
            }

            if (model.Table != null)
            {
                model.Table.BeginUpdate();
            }
            try
            {
                //记录所有列
                List<I3Column> tmpList = new List<I3Column>();
                foreach (I3Column column in model.Columns)
                {
                    tmpList.Add(column);
                }

                List<I3Column> list = new List<I3Column>();

                //按displayData中的顺序添加列
                foreach (ColumnDisplayItem item in displayData)
                {
                    foreach (I3Column column in model.Columns)
                    {
                        if (string.Equals(column.Key, item.Key))
                        {
                            column.Visible = item.Visible;
                            column.Width = item.Width;
                            column.InternalSortOrder = item.SortOrder;
                            tmpList.Remove(column);
                            list.Add(column);
                            break;
                        }
                    }
                }

                //添加所有还没有处理的列
                list.AddRange(tmpList);

                //添加到columnModel
                model.Columns.Clear();
                model.Columns.AddRange(list.ToArray());
                model.Columns.RecalcWidthCache();
            }
            finally
            {
                if (model.Table != null)
                {
                    model.Table.EndUpdate();
                }
            }
        }



        /// <summary>
        /// 在加载数据后，重新应用排序选项
        /// </summary>
        /// <param name="model"></param>
        public static void ReSort(I3ColumnModel model)
        {
            if (model == null || model.Table == null || model.Table.TableModel == null || model.Table.TableModel.Rows.Count == 0)
            {
                return;
            }

            foreach (I3Column column in model.Columns)
            {
                if (column.Sortable && column.Visible && column.SortOrder != SortOrder.None)
                {
                    model.Table.Sort(model.Columns.IndexOf(column), column.SortOrder);
                    break;
                }
            }
        }
    }


    public class ColumnDisplayItem
    {
        public ColumnDisplayItem()
        {
        }
        public ColumnDisplayItem(string key, bool visible, int width, SortOrder sortOrder)
        {
            this.key = key;
            this.visible = visible;
            this.width = width;
            this.sortOrder = sortOrder;
        }

        private string key;
        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        private bool visible;
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }

        private int width;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        private SortOrder sortOrder;
        public SortOrder SortOrder
        {
            get
            {
                return sortOrder;
            }
            set
            {
                sortOrder = value;
            }
        }
    }
}
