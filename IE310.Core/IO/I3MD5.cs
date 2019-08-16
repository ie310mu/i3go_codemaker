using System;
using System.Collections.Generic;

using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace IE310.Core.IO
{
    public class I3MD5
    {

        /// <summary>
        /// 求字符串的MD5结果  以小写 ca43f22eeda7bd5a33beeef782747891 形式返回，长度为32
        /// 默认使用Unicode编码   注意utf-8是通用的，java里面一般也用utf-8
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static string MD5String(string aStr)
        {
            return MD5String(aStr, "Unicode");
        }

        /// <summary>
        /// 求字符串的MD5结果  以小写 ca43f22eeda7bd5a33beeef782747891 形式返回，长度为32
        /// 可以指定编码  注意utf-8是通用的，java里面一般也用utf-8
        /// </summary>
        /// <param name="aStr"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string MD5String(string aStr, string encoding)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.GetEncoding(encoding).GetBytes(aStr);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }

        /// <summary>
        /// 求流的MD5结果  以小写 ca43f22eeda7bd5a33beeef782747891 形式返回，长度为32
        /// 与delphi里面的计算结果一样
        /// 注意，从流的当前位置开始算起
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static string MD5Stream(Stream stream)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] md5data = md5.ComputeHash(stream);
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
        

        /// <summary>
        /// 求文件的MD5结果  以小写 ca43f22eeda7bd5a33beeef782747891 形式返回，长度为32
        /// 与delphi里面的计算结果一样
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static string MD5File(string aFileName)
        {
            if (!File.Exists(aFileName))
            {
                return "";
            }

            using (FileStream fileStream = File.OpenRead(aFileName))
            {
                fileStream.Position = 0;
                return I3MD5.MD5Stream(fileStream);
            }
        }
    }
}
