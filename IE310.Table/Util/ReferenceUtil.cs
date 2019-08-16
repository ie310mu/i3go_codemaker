using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IE310.Table.Util
{
    public static class I3ReferenceUtil
    {

        /// <summary>
        /// 根据行号列号返回  A1 表示的字符串
        /// 注意：不支持ZZ以后的列
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static string GetReferenceByIndex(int rowIndex, int columnIndex)
        {
            string result = "";
            int div, mod;
            div = Math.DivRem(columnIndex - 1, 26, out mod);

            if (div > 0)
            {
                result = I3MathUtil.Chr(div + 64);
            }

            result = result + I3MathUtil.Chr(mod + 64 + 1);
            result = result + rowIndex.ToString();

            return result;
        }


        /// <summary>
        /// 从 A1 形式字符串得到行号列号
        /// 注意：不支持ZZ以后的列
        /// </summary>
        /// <param name="aReference"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        public static void GetIndexByReference(string aReference, out int rowIndex, out int columnIndex)
        {
            string columnName = I3ReferenceUtil.GetColumnName(aReference);
            rowIndex = (int)I3ReferenceUtil.GetRowIndex(aReference);

            int div, mod;
            if (columnName.Length == 1)
            {
                div = 0;
            }
            else
            {
                div = I3MathUtil.Asc(I3StringUtil.SubString(columnName, 0, 1)) - 64;
            }


            mod = I3MathUtil.Asc(I3StringUtil.SubString(columnName, columnName.Length - 1, 1)) - 64;

            columnIndex = div * 26 + mod;
        }


        /// <summary>
        /// 从类似于“A1”的单元格表示时，获取其列表示“A”
        /// </summary>
        /// <param name="cellName"></param>
        /// <returns></returns>
        public static string GetColumnName(string cellName)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellName);

            return match.Value;
        }

        /// <summary>
        /// 将列号从数字转换成字母
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetColumnName(int index)
        {
            string reference = I3ReferenceUtil.GetReferenceByIndex(1, index);
            return I3ReferenceUtil.GetColumnName(reference);
        }

        /// <summary>
        /// 从类似于“A1”的单元格表示时，获取其行数
        /// </summary>
        /// <param name="cellName"></param>
        /// <returns></returns>
        public static uint GetRowIndex(string cellName)
        {
            // Create a regular expression to match the row index portion the cell name.
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(cellName);

            return uint.Parse(match.Value);
        }


        /// <summary>
        /// 比较列column1和column2的大小
        /// column1>column2时，返回大于0的整数，column1=column2时，返回0，column1<column2，返回小于0的整数
        /// </summary>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        /// <returns></returns>
        public static int CompareColumn(string column1, string column2)
        {
            if (column1.Length > column2.Length)
            {
                return 1;
            }
            else if (column1.Length < column2.Length)
            {
                return -1;
            }
            else
            {
                return string.Compare(column1, column2, true);
            }
        }
    }
}
