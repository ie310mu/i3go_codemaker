using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Data;
using IE310.Core.Components;

namespace IE310.Core.Utils
{
    public static class I3ComboBoxUtil
    {
        //    /// <summary>
        //    /// 从数据库初始化下拉框的选项  aCount表示获取的个数，为0开示全部获取
        //    /// </summary>
        //    /// <param name="fcon"></param>
        //    /// <param name="comboBox"></param>
        //    /// <param name="aTableName"></param>
        //    /// <param name="aFieldName"></param>
        //    /// <param name="aCount"></param>
        //    public static void InitComboBox(I3Data fcon, ComboBox comboBox, string aTableName, string aFieldName, int aCount)
        //    {
        //        string sqlStr = " select DISTINCT ";
        //        if (aCount != 0)
        //        {
        //            sqlStr = sqlStr + " top " + aCount.ToString() + " ";
        //        }
        //        sqlStr = sqlStr + aFieldName + " from " + aTableName + " where " + aFieldName + " is not null " + " order by " + aFieldName + " asc ";

        //        using (DataTable dataTable = new DataTable(aTableName))
        //        {
        //            fcon.FillTable(dataTable, true, sqlStr, null, null);
        //            try
        //            {
        //                comboBox.Items.Clear();
        //                foreach (DataRow row in dataTable.Rows)
        //                {
        //                    comboBox.Items.Add(row[aFieldName].ToString());
        //                }
        //            }
        //            finally
        //            {
        //                fcon.DisposeDataTable(dataTable);
        //            }
        //        }
        //    }

        /// <summary>
        /// 将StringList填充到ini文件，一般用于ComBoBox与ini交换          aNowText指ComBoBOx上的当前的文字,aCount指保存的个数
        /// </summary>
        /// <param name="aFileName"></param>
        /// <param name="aSectionName"></param>
        /// <param name="aNowText"></param>
        /// <param name="comboBox"></param>
        /// <param name="aCount"></param>
        public static void SaveListToIni(string aFileName, string aSectionName, string aNowText, ComboBox comboBox, int aCount)
        {
            //判断是否需要保存
            if (string.IsNullOrEmpty(aNowText))
            {
                return;
            }
            if (comboBox.Items.IndexOf(aNowText) >= 0)
            {
                return;
            }

            //移动
            if (comboBox.Items.Count < aCount)
            {
                comboBox.Items.Add("");
            }
            string[] strList = new string[comboBox.Items.Count];

            for (int i = comboBox.Items.Count - 1; i > 0; i--)
            {
                strList[i] = comboBox.Items[i - 1].ToString();
            }
            strList[0] = aNowText;

            comboBox.Items.Clear();
            foreach (string str in strList)
            {
                comboBox.Items.Add(str);
            }

            //保存
            using (I3Ini ini = new I3Ini())
            {
                ini.FileName = aFileName;
                ini.Active = true;
                if (!ini.Active)
                {
                    return;
                }

                ini.SetInt(aSectionName, "count", comboBox.Items.Count);
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    ini.SetString(aSectionName, i.ToString(), comboBox.Items[i].ToString());
                }
            }
        }

        /// <summary>
        /// 从ini读取StringList
        /// </summary>
        /// <param name="aFileName"></param>
        /// <param name="aSectionName"></param>
        /// <param name="comboBox"></param>
        public static void ReadListFromIni(string aFileName, string aSectionName, ComboBox comboBox)
        {
            using (I3Ini ini = new I3Ini())
            {
                ini.FileName = aFileName;
                ini.Active = true;
                if (!ini.Active)
                {
                    return;
                }

                int count = ini.GetInt(aSectionName, "count", 0);
                comboBox.Items.Clear();
                for (int i = 0; i < count; i++)
                {
                    comboBox.Items.Add(ini.GetString(aSectionName, i.ToString(), ""));
                }
            }
        }
    }
}
