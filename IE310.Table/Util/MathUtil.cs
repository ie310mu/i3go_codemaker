using System;
using System.Collections;

namespace IE310.Table.Util
{
    public class I3MathUtil
    {
        /// <summary>
        /// 10进制转换为2进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Convert10To2(long value)
        {
            I3AnyRadixConvert anyConvert = new I3AnyRadixConvert();
            return anyConvert.X2X(value.ToString(), 10, 2);
        }


        /// <summary>
        /// 10进制转换为8进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Convert10To8(long value)
        {
            I3AnyRadixConvert anyConvert = new I3AnyRadixConvert();
            return anyConvert.X2X(value.ToString(), 10, 8);
        }


        /// <summary>
        /// 10进制转换为16进制  不足4位会在前面补0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Convert10To16(long value)
        {
            I3AnyRadixConvert anyConvert = new I3AnyRadixConvert();
            string result = anyConvert.X2X(value.ToString(), 10, 16);

            int ys = 0;
            Math.DivRem(result.Length, 4, out ys);
            ys = 4 - ys;

            for (int i = 1; i <= ys; i++)
            {
                result = "0" + result;
            }

            return result;
        }

        /// <summary>
        /// 2进制转换为10进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long Convert2To10(string value)
        {
            I3AnyRadixConvert anyConvert = new I3AnyRadixConvert();
            return Convert.ToInt64(anyConvert.X2X(value, 2, 10));
        }

        /// <summary>
        /// 8进制转换为10进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long Convert8To10(string value)
        {
            I3AnyRadixConvert anyConvert = new I3AnyRadixConvert();
            return Convert.ToInt64(anyConvert.X2X(value, 8, 10));
        }

        /// <summary>
        /// 16进制转换为10进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long Convert16To10(string value)
        {
            I3AnyRadixConvert anyConvert = new I3AnyRadixConvert();
            return Convert.ToInt64(anyConvert.X2X(value, 16, 10));
        }

        /// <summary>
        /// 四舍六入五取偶  double类型
        /// Math.Round中，AwayFromZero模式是四舍五入模式，ToEven模式是四舍六入五取偶模式
        /// </summary>
        /// <param name="d"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static double Round(double d, int i)
        {
            return Math.Round(d, i, MidpointRounding.ToEven);
        }

        /// <summary>
        /// 四舍六入五取偶  double类型 返回字符串，不足用0补
        /// </summary>
        /// <param name="d"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string RoundToString(double d, int i)
        {
            return Math.Round(d, i, MidpointRounding.ToEven).ToString("F" + i.ToString());
        }
        /// <summary>
        /// 将金钱转换为大写人民币
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ConvertMoneyToUpper(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖"; //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = ""; //从原num值中取出的值 
            string str4 = ""; //数字的字符串形式 
            string str5 = ""; //人民币大写金额形式 
            int i; //循环变量 
            int j; //num的值乘以100的字符串长度 
            string ch1 = ""; //数字的汉语读法 
            string ch2 = ""; //数字位的汉字读法 
            int nzero = 0; //用来计算连续的零值是几个 
            int temp; //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2); //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString(); //将num乘100并转换成字符串形式 
            j = str4.Length; //找出最高位 
            if (j > 15)
            {
                return "溢出";
            }
            str2 = I3StringUtil.SubString(str2, 15 - j); //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = I3StringUtil.SubString(str4, i, 1); //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3); //转换为数字 
                #region 当所取位数不为元、万、亿、万亿上的数字时
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + I3StringUtil.SubString(str1, temp * 1, 1);
                            ch2 = I3StringUtil.SubString(str2, i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = I3StringUtil.SubString(str1, temp * 1, 1);
                            ch2 = I3StringUtil.SubString(str2, i, 1);
                            nzero = 0;
                        }
                    }
                }
                #endregion
                #region 该位是万亿，亿，万，元位等关键位
                else
                {
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + I3StringUtil.SubString(str1, temp * 1, 1);
                        ch2 = I3StringUtil.SubString(str2, i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = I3StringUtil.SubString(str1, temp * 1, 1);
                            ch2 = I3StringUtil.SubString(str2, i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = I3StringUtil.SubString(str2, i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                #endregion

                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = I3StringUtil.SubString(str2, i, 1);
                }

                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }

            if (num == 0)
            {
                str5 = "零元整";
            }

            return str5;
        }

        /// <summary>
        /// 返回一个随机整数列表，最小值为min(包含)，最大值为max(包含)
        /// 注意：Random random = new Random();不需要另外设置种子，好像默认使用时间作用种子
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static ArrayList GetRandomIntegerList(int min, int max, int count)
        {
            Random random = new Random();
            ArrayList list = new ArrayList();

            for (int i = 0; i < count; i++)
            {
                list.Add(random.Next(min, max + 1));
            }

            return list;
        }
        /// <summary>
        /// 返回一个随机小数(double)列表，最小值为0(包含)，最大值为1(不包含)
        /// 注意：Random random = new Random();不需要另外设置种子，好像默认使用时间作用种子
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static ArrayList GetRandomDoubleList(int count)
        {
            Random random = new Random();
            ArrayList list = new ArrayList();

            for (int i = 0; i < count; i++)
            {
                list.Add(random.NextDouble());
            }

            return list;
        }

        /// <summary>
        /// 字符转ASCII码
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int Asc(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }

        }

        /// <summary>
        /// ASCII码转字符
        /// </summary>
        /// <param name="asciiCode"></param>
        /// <returns></returns>
        public static string Chr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }
    }


    public class I3AnyRadixConvert
    {
        private static char[] rDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };


        /// <summary>
        /// 将指定基数的数字的 System.String 表示形式转换为等效的 64 位有符号整数。
        /// </summary>
        /// <param name="value">包含数字的 System.String。</param>
        /// <param name="fromBase">value 中数字的基数，它必须是[2,36]</param>
        /// <returns>等效于 value 中的数字的 64 位有符号整数。- 或 - 如果 value 为 null，则为零。</returns>
        private long x2h(string value, int fromBase)
        {
            value = value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                return 0L;
            }

            string sDigits = new string(rDigits, 0, fromBase);
            long result = 0;
            //value = reverse(value).ToUpper(); 1
            value = value.ToUpper();// 2
            for (int i = 0; i < value.Length; i++)
            {
                if (!sDigits.Contains(value[i].ToString()))
                {
                    throw new ArgumentException(string.Format("The argument \"{0}\" is not in {1} system.", value[i], fromBase));
                }
                else
                {
                    try
                    {
                        //result += (long)Math.Pow(fromBase, i) * getcharindex(rDigits, value[i]); 1
                        result += (long)Math.Pow(fromBase, i) * getcharindex(rDigits, value[value.Length - i - 1]);//   2
                    }
                    catch
                    {
                        throw new OverflowException("运算溢出.");
                    }
                }
            }

            return result;
        }

        private int getcharindex(char[] arr, char value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == value)
                {
                    return i;
                }
            }
            return 0;
        }

        //long转化为toBase进制
        private string h2x(long value, int toBase)
        {
            int digitIndex = 0;
            long longPositive = Math.Abs(value);
            int radix = toBase;
            char[] outDigits = new char[63];

            for (digitIndex = 0; digitIndex <= 64; digitIndex++)
            {
                if (longPositive == 0)
                {
                    break;
                }

                outDigits[outDigits.Length - digitIndex - 1] =
                    rDigits[longPositive % radix];
                longPositive /= radix;
            }

            return new string(outDigits, outDigits.Length - digitIndex, digitIndex);
        }

        //任意进制转换,将fromBase进制表示的value转换为toBase进制
        public string X2X(string value, int fromBase, int toBase)
        {
            if (string.IsNullOrEmpty(value.Trim()))
            {
                return string.Empty;
            }

            if (fromBase < 2 || fromBase > 36)
            {
                throw new ArgumentException(String.Format("The fromBase radix \"{0}\" is not in the range 2..36.", fromBase));
            }

            if (toBase < 2 || toBase > 36)
            {
                throw new ArgumentException(String.Format("The toBase radix \"{0}\" is not in the range 2..36.", toBase));
            }

            long m = x2h(value, fromBase);
            string r = h2x(m, toBase);
            return r;
        }
    }
}





