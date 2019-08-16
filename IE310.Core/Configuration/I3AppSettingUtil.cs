using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using IE310.Core.Utils;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace IE310.Core.Configuration
{
    /// <summary>
    /// 帮助类，用于读取或设置 AppSetting中的设置
    /// 未能成功获取或设置时，将会抛出异常
    /// </summary>
    public static class I3AppSettingUtil
    {
        public static string GetString(string key)
        {
            string result = ConfigurationManager.AppSettings[key];
            if (result == null)
            {
                throw new KeyNotFoundException(string.Format("未在appSettings中找到键{0}", key));
            }

            return result;
        }

        public static int GetInt(string key)
        {
            string tmp = GetString(key);
            int result = 0;
            if (!int.TryParse(tmp, out result))
            {
                throw new InvalidCastException(string.Format("读取配置{0}时出错错误，值{1}不是一个整数", key, tmp));
            }
            return result;
        }

        public static decimal GetDecimal(string key)
        {
            string tmp = GetString(key);
            decimal result = 0;
            if (!decimal.TryParse(tmp, out result))
            {
                throw new InvalidCastException(string.Format("读取配置{0}时出错错误，值{1}不是一个decimal", key, tmp));
            }
            return result;
        }

        public static double GetDouble(string key)
        {
            string tmp = GetString(key);
            double result = 0;
            if (!double.TryParse(tmp, out result))
            {
                throw new InvalidCastException(string.Format("读取配置{0}时出错错误，值{1}不是一个double", key, tmp));
            }
            return result;
        }

        public static float GetFloat(string key)
        {
            string tmp = GetString(key);
            float result = 0;
            if (!float.TryParse(tmp, out result))
            {
                throw new InvalidCastException(string.Format("读取配置{0}时出错错误，值{1}不是一个float", key, tmp));
            }
            return result;
        }

        public static bool GetBool(string key)
        {
            try
            {
                string tmp = GetString(key);
                bool result = true;
                result = bool.Parse(tmp);
                return result;
            }
            catch
            {
                return false;
            }
        }

        public static DateTime GetDateTime(string key)
        {
            string tmp = GetString(key);
            DateTime result;
            try
            {
                result = I3DateTimeUtil.ConvertStringToDateTime(tmp);
            }
            catch
            {
                throw new InvalidCastException(string.Format("读取配置{0}时出错错误，值{1}不是一个DateTime。\r\n其格式必须类似于 2001-01-01 12:00:00", key, tmp));
            }
            return result;
        }

        public static void Set(string key, string value)
        {
            bool find = false;

            XmlDocument xml = new XmlDocument();
            string configFileName = Path.GetFileName(Application.ExecutablePath) + ".config";
            configFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFileName);
            xml.Load(configFileName);
            XmlNodeList dataBaseConfigs = xml.GetElementsByTagName("appSettings");
            foreach (XmlNode node in dataBaseConfigs[0].ChildNodes)
            {
                if (node.Attributes["key"].Value == key)
                {
                    node.Attributes["value"].Value = value;
                    find = true;
                    break;
                }
            }

            if (find)
            {
                xml.Save(configFileName);
            }
            else
            {
                throw new ArgumentNullException(string.Format("设置配置{0}的值时出现错误，未找到此节点", key));
            }
        }

        public static void Set(string key, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(string.Format("设置配置{0}的值时出现错误，值不能为null", key));
            }
            if (value is DateTime)
            {
                Set(key, I3DateTimeUtil.ConvertDateTimeToDateTimeString((DateTime)value));
            }
            else
            {
                Set(key, value.ToString());
            }
        }
    }
}
