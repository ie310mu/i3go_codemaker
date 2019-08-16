using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;

namespace IE310.Core.Utils
{
    /// <summary>
    /// 定义枚举值转换成的字符串（有的字符串不是合法的枚举名）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class I3EnumNameAttribute : Attribute
    {
        public I3EnumNameAttribute(string name)
        {
            this._name = name;
        }

        string _name = "";
        /// <summary>
        /// 枚举值转换成的字符串
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }
    }

    public static class I3EnumUtil
    {
        /// <summary>
        /// 获取枚举值的名称
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetName(object obj)
        {
            FieldInfo[] fields = obj.GetType().GetFields();
            FieldInfo field = null;
            foreach (FieldInfo item in fields)
            {
                if (item.FieldType != obj.GetType())
                {
                    continue;
                }
                if (item.Name == obj.ToString())
                {
                    field = item;
                    break;
                }
            }
            if (field == null)
            {
                return obj.ToString();
            }

            I3EnumNameAttribute[] attrs = (I3EnumNameAttribute[])field.GetCustomAttributes(typeof(I3EnumNameAttribute), true);
            if (attrs.Length > 0)
            {
                return attrs[0].Name;
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 将枚举类型转换成DataTable
        /// 字段的值添加到id字段(int)，名称添加到Name字段(string)
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static DataTable EnumTypeToDataTable(Type enumType)
        {
            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn("id", typeof(int)));
            result.Columns.Add(new DataColumn("Name", typeof(string)));

            FieldInfo[] fields = enumType.GetFields();
            foreach (int id in Enum.GetValues(enumType))
            {
                FieldInfo field = null;
                foreach (FieldInfo item in fields)
                {
                    if (item.FieldType != enumType)
                    {
                        continue;
                    }
                    if (item.Name == Enum.GetName(enumType, id))
                    {
                        field = item;
                        break;
                    }
                }

                string name = "";
                if (field == null)
                {
                    name = id.ToString();
                }
                else
                {
                    I3EnumNameAttribute[] attrs = (I3EnumNameAttribute[])field.GetCustomAttributes(typeof(I3EnumNameAttribute), true);
                    if (attrs.Length > 0)
                    {
                        name = attrs[0].Name;
                    }
                    else
                    {
                        name = Enum.GetName(enumType, id).ToString();
                    }
                }

                DataRow row = result.NewRow();
                row["id"] = id;
                row["Name"] = name;
                result.Rows.Add(row);
            }

            return result;
        }
    }
}
