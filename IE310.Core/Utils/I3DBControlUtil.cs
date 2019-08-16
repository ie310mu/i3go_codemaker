using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Data;

namespace IE310.Core.Utils
{
    public static class I3DBControlUtil
    {
        /// <summary>
        /// 根据控件的Tag设置，自动为控件设置数据绑定
        /// 
        /// 注意：Tag的值必须是Text,MU_Name;Items,Mu_Name这样的形式
        /// 注意：自动绑定时，数据源不能有空值，否则会绑定失败
        /// 
        /// 特殊配置属性：
        /// Control.Text
        /// ComboBox.Items(DataSource为DataTable类型)
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dataSource"></param>
        public static void AddDataBingding(Control control, object dataSource)
        {
            if ((control == null) || (control.Tag == null))
            {
                return;
            }
            if (control.Tag.GetType() != typeof(string))
            {
                return;
            }
            if (string.IsNullOrEmpty(control.Tag.ToString()))
            {
                return;
            }

            string tag = (string)control.Tag.ToString();
            List<string> setList = I3StringUtil.SplitStringToList(tag, ";");
            foreach (string str in setList)
            {
                List<string> set = I3StringUtil.SplitStringToList(str, ",");
                if (set.Count == 2)
                {
                    string propertyName = (string)set[0];
                    string fieldName = (string)set[1];
                    if (string.Equals(propertyName, "Items"))
                    {
                        #region ComboBox.Items
                        if (control.GetType() == typeof(ComboBox))
                        {
                            ComboBox comboBox = (ComboBox)control;
                            comboBox.Items.Clear();
                            DataTable dataTable = (DataTable)dataSource;
                            foreach (DataRow row in dataTable.Rows)
                            {
                                comboBox.Items.Add(row[fieldName].ToString());
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        control.DataBindings.Add(propertyName, dataSource, fieldName);
                    }
                }
            }
        }

        /// <summary>
        /// 清除数据绑定
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dataSource"></param>
        public static void ClearDataBingding(Control control)
        {
            if (control == null)
            {
                return;
            }

            control.DataBindings.Clear();
        }

        /// <summary>
        /// 递归设置DataBinding
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dataSource"></param>
        /// <param name="bindingSelf"></param>
        /// <param name="bindingSub"></param>
        public static void AddSubControlBingdings(Control control, object dataSource, bool bindingSelf, bool bindingSub)
        {
            if (control == null)
            {
                return;
            }

            if (bindingSelf)
            {
                I3DBControlUtil.AddDataBingding(control, dataSource);
            }

            if (bindingSub)
            {
                foreach (Control subControl in control.Controls)
                {
                    AddSubControlBingdings(subControl, dataSource, true, true);
                }
            }
        }

        /// <summary>
        /// 递归清除DataBinding
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dataSource"></param>
        /// <param name="bindingSelf"></param>
        /// <param name="bindingSub"></param>
        public static void ClearSubControlBingdings(Control control, bool clearSelf, bool clearSub)
        {
            if (control == null)
            {
                return;
            }

            if (clearSelf)
            {
                I3DBControlUtil.ClearDataBingding(control);
            }

            if (clearSub)
            {
                foreach (Control subControl in control.Controls)
                {
                    ClearSubControlBingdings(subControl, true, true);
                }
            }
        }

        public static void DisposeSubControls(Control parentControl)
        {
            foreach (Control subConrol in parentControl.Controls)
            {
                DisposeSubControls(subConrol);
            }

            while (parentControl.Controls.Count > 0)
            {
                parentControl.Controls[0].Dispose();
            }
        }

        /// <summary>
        /// 清空控件和其子控件的Text属性
        /// </summary>
        /// <param name="control"></param>
        /// <param name="clearSelf"></param>
        /// <param name="clearSub"></param>
        public static void ClearControlsText(Control control, bool clearSelf, bool clearSub)
        {
            if (control == null)
            {
                return;
            }

            if (clearSelf && control.Tag != null)
            {
                control.Text = "";
            }

            if (clearSub)
            {
                foreach (Control subControl in control.Controls)
                {
                    ClearControlsText(subControl, true, true);
                }
            }
        }


        /// <summary>
        /// 设置控件和其子控件的Enabled属性
        /// </summary>
        /// <param name="control"></param>
        /// <param name="clearSelf"></param>
        /// <param name="clearSub"></param>
        public static void SetControlsEnabled(Control control, bool setSelf, bool setSub, bool enabled)
        {
            if (control == null)
            {
                return;
            }

            if (setSelf)
            {
                control.Enabled = enabled;
            }

            if (setSub)
            {
                foreach (Control subControl in control.Controls)
                {
                    SetControlsEnabled(subControl, true, true, enabled);
                }
            }
        }


        /// <summary>
        /// 递归查找BindingContext
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static BindingManagerBase FindBindingManager(Control control, object dataSource, bool findSelf)
        {
            if (findSelf)
            {
                if (control.BindingContext[dataSource] != null)
                {
                    return control.BindingContext[dataSource];
                }
            }

            foreach (Control sub in control.Controls)
            {
                BindingManagerBase result = FindBindingManager(sub, dataSource, true);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
