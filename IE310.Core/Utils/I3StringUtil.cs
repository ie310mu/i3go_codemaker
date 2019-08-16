/*
编程中经常使用的ASCII码
键盘	ASCII码	键盘	ASCII码	键盘	ASCII码	键盘	ASCII码
0	48	    1	49	    2	50	    3	51
4	52	    5	53	    6	54	    7	55
8	56	    9	57	    a	97	    b	98
c	99	    d	100	    e	101	    f	102
g	103	    h	104	    i	105	    j	106
k	107	    l	108	    m	109	    n	110
o	111	    p	112	    q	113	    r	114
s	115	    t	116	    u	117	    v	118
w	119	    x	120	    y	121	    z	122
A	65	    B	66	    C	67	    D	68
E	69	    F	70	    G	71	    H	72
I	73	    J	74	    K	75	    L	76
M	77	    N	78	    O	79	    P	80
Q	81	    R	82	    S	83	    T	84
U	85	    V	86    	W	87	    X	88
Y	89	    Z	90				

*/

using System;
using System.Collections.Generic;

using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace IE310.Core.Utils
{
    public static class I3StringUtil
    {
        /// <summary>
        /// 获取子串，获取失败时不引发异常，而是直接返回 ""
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="lenth"></param>
        /// <returns></returns>
        public static string SubString(string str, int start)
        {
            try
            {
                return str.Substring(start);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取子串，获取失败时不引发异常，而是直接返回 ""
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="lenth"></param>
        /// <returns></returns>
        public static string SubString(string str, int start, int length)
        {
            try
            {
                return str.Substring(start, length);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 将字符转换为ASCII码
        /// </summary>
        /// <param name="aChar"></param>
        /// <returns></returns>
        public static short ConvertCharToASCII(char aChar)
        {
            //return (short)aChar;
            return Convert.ToInt16(aChar);
        }

        /// <summary>
        /// 将ASCII码转换为char
        /// </summary>
        /// <param name="aASCII"></param>
        /// <returns></returns>
        public static char ConvertASCIIToChar(short aASCII)
        {
            //return (char)aASCII;
            return Convert.ToChar(aASCII);
        }

        /// <summary>
        /// 获取汉字的区位码  使用GBK码
        /// GB2312:简体中文编码
        /// BIG5:繁体中文编码
        /// GBK:支持简体及繁体中文
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static string ConvertChineseToNum(string aStr)
        {
            byte[] array = new byte[2];
            array = Encoding.GetEncoding("GBK").GetBytes(aStr);
            //array = Encoding.GetEncoding("GB2312").GetBytes(aStr);
            //array = System.Text.Encoding.Default.GetBytes(aStr);
            short front = (short)(array[0] - 0);
            short back = (short)(array[1] - 0);
            return Convert.ToString(front - 160) + Convert.ToString(back - 160);
        }

        /// <summary>
        /// 由区位码得到汉字
        /// </summary>
        /// <param name="aNum"></param>
        /// <returns></returns>
        public static string ConvertNumToChinese(string aNum)
        {
            byte[] array = new byte[2];
            string str1 = I3StringUtil.SubString(aNum, 0, 2);
            string str2 = I3StringUtil.SubString(aNum, 2, 2);
            int front = Convert.ToInt32(str1) + 160;
            int back = Convert.ToInt32(str2) + 160;
            array[0] = (byte)front;
            array[1] = (byte)back;
            return Encoding.GetEncoding("GBK").GetString(array);
        }

        /// <summary>
        /// 将行字符串转换为列字符串 
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static string ConvertStringToSingleList(string aStr)
        {
            string result = "";
            CharEnumerator CEnumerator = aStr.GetEnumerator();
            while (CEnumerator.MoveNext())
            {
                if (result == "")
                    result = CEnumerator.Current.ToString();
                else
                    result += "\n" + CEnumerator.Current.ToString();
            }

            return result;
        }

        /// <summary>
        /// 将数字转换为日期
        /// </summary>
        /// <param name="aNum"></param>
        /// <returns></returns>
        public static DateTime ConvertNumToDateTime(int aNum)
        {
            DateTime dateTime = new DateTime(aNum);
            return dateTime;
        }

        /// <summary>
        /// 将数字转换为货币格式
        /// aAccuracy：精度
        /// </summary>
        /// <param name="aNum"></param>
        /// <param name="aAccuracy"></param>
        /// <returns></returns>
        public static string ConvertNumToMoney(long aNum, int aAccuracy)
        {
            return aNum.ToString("C" + aAccuracy.ToString());
        }
        /// <summary>
        /// 返回首字母为大写的字符串
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static string ConvertFirstToUpper(string aStr)
        {
            string str1 = I3StringUtil.SubString(aStr, 0, 1);
            string str2 = I3StringUtil.SubString(aStr, 1, aStr.Length - 1);
            return str1.ToUpper() + str2;
        }

        /// <summary>
        /// 返回倒序字符串
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static string ConvertStringToReverse(string aStr)
        {
            char[] charstr = aStr.ToCharArray();
            Array.Reverse(charstr);
            string str2 = new string(charstr);
            return str2;
        }

        /// <summary>
        /// 分割字符串  空字符串不返回，返回的字符串两端空格被去掉
        /// </summary>
        /// <param name="aList"></param>
        /// <param name="aSplitStr"></param>
        /// <returns></returns>
        public static List<string> SplitStringToList(string aStr, string aSplitStr)
        {
            //此函数只是按第一个字符进行分割
            //return aStr.Split(aSplitStr.ToCharArray());

            List<string> result = new List<string>();
            aStr = aStr.Trim();

            int index = aStr.IndexOf(aSplitStr);
            while (index >= 0)
            {
                string tmp = I3StringUtil.SubString(aStr, 0, index).Trim();
                if (!string.IsNullOrEmpty(tmp))
                {
                    result.Add(tmp);
                }
                aStr = I3StringUtil.SubString(aStr, index + aSplitStr.Length, aStr.Length - index - aSplitStr.Length).Trim();
                index = aStr.IndexOf(aSplitStr);
            }
            if (!aStr.Equals(string.Empty))
            {
                result.Add(aStr);
            }


            return result;
        }

        /// <summary>
        /// 获取字符串中数值的个数
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static int GetNumCountInString(string aStr)
        {
            int result = 0;
            CharEnumerator CEnumerator = aStr.GetEnumerator();
            while (CEnumerator.MoveNext())
            {
                byte[] array = new byte[1];
                array = Encoding.ASCII.GetBytes(CEnumerator.Current.ToString());
                int asciicode = (short)(array[0]);
                if (asciicode >= 48 && asciicode <= 57)
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取字符串中数值和英文字母的个数
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static int GetNumAndCharCountInString(string aStr)
        {
            int result = 0;
            CharEnumerator CEnumerator = aStr.GetEnumerator();
            while (CEnumerator.MoveNext())
            {
                byte[] array = new byte[1];
                array = Encoding.ASCII.GetBytes(CEnumerator.Current.ToString());
                int asciicode = (short)(array[0]);
                if ((asciicode >= 48 && asciicode <= 57) || (asciicode >= 65 && asciicode <= 90) || (asciicode >= 97 && asciicode <= 122))
                {
                    result++;
                }
            }

            return result;
        }
        /// <summary>
        /// 获取字符串中大写字母的个数
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static int GetUpperCharCountInString(string aStr)
        {
            int result = 0;
            CharEnumerator CEnumerator = aStr.GetEnumerator();
            while (CEnumerator.MoveNext())
            {
                byte[] array = new byte[1];
                array = Encoding.ASCII.GetBytes(CEnumerator.Current.ToString());
                int asciicode = (short)(array[0]);
                if (asciicode >= 65 && asciicode <= 90)
                {
                    result++;
                }
            }

            return result;
        }
        /// <summary>
        /// 获取字符串中小写字母的个数
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static int GetLowerCharCountInString(string aStr)
        {
            int result = 0;
            CharEnumerator CEnumerator = aStr.GetEnumerator();
            while (CEnumerator.MoveNext())
            {
                byte[] array = new byte[1];
                array = Encoding.ASCII.GetBytes(CEnumerator.Current.ToString());
                int asciicode = (short)(array[0]);
                if (asciicode >= 97 && asciicode <= 122)
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取字符串中汉字的个数
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static int GetChineseCountInString(string aStr)
        {
            int result = 0;
            CharEnumerator CEnumerator = aStr.GetEnumerator();
            Regex regex = new Regex("^[\u4E00-\u9FA5]{0,}$");
            while (CEnumerator.MoveNext())
            {
                if (regex.IsMatch(CEnumerator.Current.ToString(), 0))
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取长度  中文、特殊符号算2个
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static int GetStrFullLength(string s)
        {
            if (s == null)
            {
                return 0;
            }

            int len = 0;
            for (int i = 0; i < s.Length; i++)
            {
                len++;
                char c = s[i];
                if (!isLetter(c))
                {
                    len++;
                }
            }
            return len;
        }

        public static bool isLetter(char c)
        {
            int k = 0x80;
            return c / k == 0 ? true : false;
        }

        /// <summary>
        /// 获取子字符串个数  
        /// 注意，像 ab我我我我我cc 这样的字符串，获取 我我 的个数时，返回2而不是4
        /// </summary>
        /// <param name="aStr"></param>
        /// <param name="aSub"></param>
        /// <returns></returns>
        public static int GetSubCountInString(string aStr, string aSub)
        {
            int result = 0;

            int index = aStr.IndexOf(aSub);
            while (index >= 0)
            {
                result++;
                aStr = I3StringUtil.SubString(aStr, index + aSub.Length, aStr.Length - index - aSub.Length);
                index = aStr.IndexOf(aSub);
            }

            return result;
        }

        /// <summary>
        /// 检查字符串是否是一个数字
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static bool CheckStringIsNum(string aStr)
        {
            try
            {
                double.Parse(aStr);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 获取一个随机验证码
        /// </summary>
        /// <returns></returns>
        public static string GetARandomVerifyCode(int Len)
        {
            string str = "0,1,2,3,4,5,6,7,8,9,";
            str += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            str += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] s = str.Split(new char[] { ',' });//将字符串拆分成字符串数组
            string strNum = "";
            int tag = -1;//用于记录上一个随机数的值，避免产生两个重复值
            Random rnd = new Random();
            //产生一个长度为Len的随机字符串
            for (int i = 1; i <= Len; i++)
            {
                if (tag == -1)
                {
                    rnd = new Random(i * tag * unchecked((int)DateTime.Now.Ticks));//初始化一个Random实例
                }
                int rndNum = rnd.Next(61);//返回小于６１的非负随机数
                //如果产生与前一个随机数相同的数，则重新生成一个新随机数
                if (tag != -1 && tag == rndNum)
                {
                    return GetARandomVerifyCode(1);
                }
                tag = rndNum;
                strNum += s[rndNum];
            }
            return strNum;
        }

        /// <summary>
        /// 返回一个Guid 是小写的，类似于 f2bf77b9-3f54-4aa4-8cf3-3d9b29da9779
        /// 注：Guid长度为 36
        /// </summary>
        /// <returns></returns>
        public static string GetAGuid()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        /// <summary>
        /// 15位身份证升级为18为身份证
        /// </summary>
        /// <param name="aNO"></param>
        /// <returns></returns>
        public static string UpPersonNO15To18(string id)
        {
            int[] w = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
            char[] a = new char[] { '1', '0', 'x', '9', '8', '7', '6', '5', '4', '3', '2' };
            string newID = "";
            if (id.Length == 15)//判断位数
            {
                int s = 0;
                newID = id.Insert(6, "19");//插入字符串
                for (int i = 0; i < 17; i++)
                {
                    int k = Convert.ToInt32(newID[i]) * w[i];
                    s = s + k;
                }
                int h = 0;
                System.Math.DivRem(s, 11, out h);//取余数
                newID = newID + a[h];
            }
            return newID;
        }

        /// <summary>
        /// 校验18位身份证号码是否正确  正确返回false  并在信息中写入类似于 湖北,1983-09-13,男 的字符串
        /// 错误则返回false，并在信息中写入错误信息
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static bool Check18PersonNO(string cid, out string result)
        {
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };
            double iSum = 0;
            Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{17}(\d|x)$");
            Match mc = rg.Match(cid);
            if (!mc.Success)
            {
                result = "";
                return false;
            }
            cid = cid.ToLower();
            cid = cid.Replace("x", "a");
            if (aCity[int.Parse(I3StringUtil.SubString(cid, 0, 2))] == null)
            {
                result = "非法地区";
                return false;
            }
            try
            {
                DateTime.Parse(I3StringUtil.SubString(cid, 6, 4) + "-" + I3StringUtil.SubString(cid, 10, 2) + "-" + I3StringUtil.SubString(cid, 12, 2));
            }
            catch
            {
                result = "非法生日";
                return false;
            }
            for (int i = 17; i >= 0; i--)
            {
                iSum += (System.Math.Pow(2, i) % 11) * int.Parse(cid[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);

            }
            if (iSum % 11 != 1)
            {
                result = "非法证号";
                return false;
            }
            result = aCity[int.Parse(I3StringUtil.SubString(cid, 0, 2))]
                   + "," + I3StringUtil.SubString(cid, 6, 4)
                   + "-" + I3StringUtil.SubString(cid, 10, 2)
                   + "-" + I3StringUtil.SubString(cid, 12, 2)
                   + "," + (int.Parse(I3StringUtil.SubString(cid, 16, 1)) % 2 == 1 ? "男" : "女");
            return true;
        }

        /// <summary>
        /// 相当于delphi中的QuotedStr,用于给字符串两边加',并且原有的'换成两个'
        /// 如:  rt'yu    ----->      'rt''yu'
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string QuotedStr(string value)
        {
            if (value == null)
                return value;

            return "'" + value.Replace("'", "''") + "'";
        }



        /// <summary>
        /// 在首尾附加  "
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AppendDoubleQuotes(string value)
        {
            if (value == null)
                return value;

            return "\"" + value + "\"";
        }

        /// <summary>
        /// 检查字符串是不是合格的email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool CheckStringIsEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 检查字符串是不是合格的ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool CheckStringIsIP(string ip)
        {
            string num = "(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)";
            return Regex.IsMatch(ip, ("^" + num + "\\." + num + "\\." + num + "\\." + num + "$"));
        }

        /*
        /// <summary>
        /// 检查一个字符串是不是合格的IP
        /// </summary>
        /// <param name="aStr"></param>
        /// <returns></returns>
        public static bool CheckStringIsIP(string ip)
        {
            string[] lines;
            string s = ".";
            lines = ip.Split(s.ToCharArray());//分隔字符串

            if (lines.Count() != 4)
            {
                return false;
            }

            for (int i = 0; i < 4; i++)
            {
                try
                {
                    if (Convert.ToInt32(lines[i]) > 255 || Convert.ToInt32(lines[i]) < 0)
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }


            return true;
        }
        */


        /// <summary>
        /// 检查字符串是不是合格的URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool CheckStringIsURL(string url)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(url, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }

        /// <summary>
        /// 检查电话号码是否合格 （坐机号）
        /// 示例：0333-3456789
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool CheckStringIsChinesePhone(string phone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^(\d{3,4}-)?\d{6,8}$");
        }

        /// <summary>
        /// 检查是否是中国手机号
        /// </summary>
        /// <param name="movePhone"></param>
        /// <returns></returns>
        public static bool CheckStringIsChineseMovePhone(string movePhone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(movePhone, @"^[1]+[3,5,8]+\d{9}$");
        }

        /// <summary>
        /// 检查字符串是否是合格的密码：  
        /// 注意：必须是 英文+数字 的组合才返回 true，仅英文仅数字都不行
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool CheckStringIsPassWord(string password)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Za-z]+[0-9]");
        }


        /// <summary>
        /// 检查是否是合格的中国境内的邮政编码(6位数字)
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        public static bool CheckStringIsPostCode(string postcode)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(postcode, @"^\d{6}$");
        }



        /// <summary>
        /// 长度必须大于5
        /// 指定获取的英文的长度，截取字符串，多余部分用..替代
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CutString(string source, int length)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }

            if (length < 5)
            {
                throw new Exception("长度必须大于5");
            }

            string result = "";
            int resultLength = 0;
            int lastLength = 0;
            for (int i = 0; i <= source.Length - 1; i++)
            {
                string tmp = source.Substring(i, 1);
                int tmpLength = I3StringUtil.GetChineseCountInString(tmp) == 1 ? 2 : 1;
                if (resultLength + tmpLength > length)
                {
                    if (lastLength == 2)
                    {
                        result = result.Substring(0, result.Length - 1) + "..";
                    }
                    else
                    {
                        result = result.Substring(0, result.Length - 2) + "..";
                    }
                    return result;
                }
                result += tmp;
                resultLength += tmpLength;
                lastLength = tmpLength;
            }
            return result;
        }

        /// <summary>
        /// 以指定编码将字符串写入到文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="source"></param>
        /// <param name="encoding"></param>
        public static void SaveStringToFile(string fileName, string source, Encoding encoding)
        {
            I3DirectoryUtil.CreateDirectoryByFileName(fileName);
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                byte[] data = encoding.GetBytes(source);
                fs.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// 以指定编码从文件读取字符串
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="encoding"></param>
        public static string ReadStringFromFile(string fileName, Encoding encoding)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                return encoding.GetString(data);
            }
        }

        public static int ParseInt(object objVal, int defValue)
        {
            return ParseInt("" + objVal, defValue);
        }

        public static int ParseInt(string s, int defValue)
        {
            if ((s == null) || (s.Length == 0))
            {
                return defValue;
            }
            if (!IsInt(s))
            {
                return defValue;
            }
            return int.Parse(s);
        }

        public static bool IsInt(string s)
        {
            string pattern = @"^\s*[+-]?\d+\s*$";
            return Regex.IsMatch(s, pattern, RegexOptions.Singleline | RegexOptions.Compiled);
        }


    }
}
