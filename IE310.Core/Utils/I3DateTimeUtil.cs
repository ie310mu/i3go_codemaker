using System;
using System.Collections.Generic;

using System.Text;
using System.Globalization;

//return System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fffffff");


namespace IE310.Core.Utils
{
    public static class I3DateTimeUtil
    {
        /// <summary>
        /// 将 2010-01-01 格式的字符串转换成DateTime
        /// </summary>
        /// <param name="aDateTimeStr"></param>
        /// <returns></returns>
        public static DateTime ConvertStringToDate(string aDateTimeStr)
        {
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            info.ShortDatePattern = "yyyy-MM-dd";
            info.DateSeparator = "-";

            return DateTime.Parse(aDateTimeStr, info).Date;
        }


        /// <summary>
        /// 将 2010-01-01 12:12:35 格式的字符串转换成DateTime
        /// </summary>
        /// <param name="aDateTimeStr"></param>
        /// <returns></returns>
        public static DateTime ConvertStringToDateTime(string aDateTimeStr)
        {
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            info.ShortDatePattern = "yyyy-MM-dd";
            info.DateSeparator = "-";
            info.ShortTimePattern = "HH:mm:ss";
            info.TimeSeparator = ":";

            return DateTime.Parse(aDateTimeStr, info);
        }


        /// <summary>
        /// 将 12:12:35 格式的字符串转换成DateTime
        /// </summary>
        /// <param name="aDateTimeStr"></param>
        /// <returns></returns>
        public static DateTime ConvertStringToTime(string aDateTimeStr)
        {
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            info.ShortTimePattern = "HH:mm:ss";
            info.TimeSeparator = ":";

            return DateTime.Parse(aDateTimeStr, info);
        }

        /// <summary>
        /// 将DateTime转换成 201001011212302222222 格式的字符串
        /// </summary>
        /// <param name="aDateTime"></param>
        /// <returns></returns>
        public static string ConvertDateTimeToLongString(DateTime aDateTime)
        {
            return aDateTime.ToString("yyyyMMddHHmmssfffffff");
        }

        /// <summary>
        /// 将DateTime转换成 2010-01-01 格式的字符串
        /// </summary>
        /// <param name="aDateTime"></param>
        /// <returns></returns>
        public static string ConvertDateTimeToDateString(DateTime aDateTime)
        {
            return aDateTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 将DateTime转换成 2010-01-01 18:12:01 格式的字符串
        /// </summary>
        /// <param name="aDateTime"></param>
        /// <returns></returns>
        public static string ConvertDateTimeToDateTimeString(DateTime aDateTime)
        {
            return aDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 将DateTime转换成 2010-01-01 格式的字符串
        /// </summary>
        /// <param name="aDateTime"></param>
        /// <returns></returns>
        public static string ConvertDateTimeToTimeString(DateTime aDateTime)
        {
            return aDateTime.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 从字符串表示的生日中获取年龄  日期字符串的格式必须为 2010-01-01
        /// </summary>
        /// <param name="aDateTime"></param>
        /// <returns></returns>
        public static int GetAge(string aDateTimeStr)
        {
            return DateTime.Now.Year - ConvertStringToDate(aDateTimeStr).Year;
        }


        /// <summary>
        /// 从Date的生日中获取年龄  日期字符串的格式必须为 2010-01-01
        /// </summary>
        /// <param name="aDateTime"></param>
        /// <returns></returns>
        public static int GetAge(DateTime aDateTime)
        {
            return DateTime.Now.Year - aDateTime.Year;
        }

        /// <summary>
        /// 判断是否为闰年
        /// </summary>
        /// <param name="aYear"></param>
        /// <returns></returns>
        public static bool CheckIsLeapYear(int aYear)
        {
            return DateTime.IsLeapYear(aYear);
        }

        /// <summary>
        /// 获取十二生肖
        /// </summary>
        /// <param name="aDateTime"></param>
        /// <returns></returns>
        public static string Get12Lunar(DateTime aDateTime)
        {
            System.Globalization.ChineseLunisolarCalendar chinseCaleander = new System.Globalization.ChineseLunisolarCalendar();
            string TreeYear = "鼠牛虎兔龙蛇马羊猴鸡狗猪";
            int intYear = chinseCaleander.GetSexagenaryYear(aDateTime);
            return I3StringUtil.SubString(TreeYear, chinseCaleander.GetTerrestrialBranch(intYear) - 1, 1);
        }

        //计算
        public static void CalDateTime(DateTime baseDate, DateTimeCalMode calMode, DateTimeGetMode getMode, out DateTime begin, out DateTime end)
        {
            baseDate = baseDate.Date;
            begin = baseDate.Date;
            end = begin;
            int offset = 0;
            int year = 0;
            int month = 0;
            int endMonth = 0;
            int endYear = 0;

            switch (calMode)
            {
                case DateTimeCalMode.天:
                    offset = (int)getMode;
                    begin = baseDate.Date.AddDays(offset);
                    end = begin;
                    break;
                case DateTimeCalMode.周:
                    offset = (int)getMode * 7;
                    DayOfWeek dayOfWeek = baseDate.DayOfWeek;
                    begin = baseDate.AddDays(0 - (int)dayOfWeek).AddDays(offset);
                    end = begin.AddDays(6);
                    break;
                case DateTimeCalMode.季度:
                    offset = (int)getMode * 3;
                    year = baseDate.Year;
                    month = baseDate.Month;
                    endMonth = 0;
                    endYear = year;
                    switch (month)
                    {
                        case 1:
                        case 2:
                        case 3:
                            month = 1;
                            endMonth = 4;
                            break;
                        case 4:
                        case 5:
                        case 6:
                            month = 4;
                            endMonth = 7;
                            break;
                        case 7:
                        case 8:
                        case 9:
                            month = 7;
                            endMonth = 10;
                            break;
                        case 10:
                        case 11:
                        case 12:
                            month = 10;
                            endYear++;
                            endMonth = 1;
                            break;
                    }
                    begin = new DateTime(year, month, 1).AddMonths(offset);
                    end = new DateTime(endYear, endMonth, 1).AddDays(-1).AddMonths(offset);
                    break;
                case DateTimeCalMode.半年:
                    offset = (int)getMode * 6;
                    year = baseDate.Year;
                    month = baseDate.Month;
                    endMonth = 0;
                    endYear = year;
                    switch (month)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            month = 1;
                            endMonth = 7;
                            break;
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                            month = 7;
                            endYear++;
                            endMonth = 1;
                            break;
                    }
                    begin = new DateTime(year, month, 1).AddMonths(offset);
                    end = new DateTime(endYear, endMonth, 1).AddDays(-1).AddMonths(offset);
                    break;
                case DateTimeCalMode.年:
                    offset = (int)getMode;
                    year = baseDate.Year;
                    begin = new DateTime(year, 1, 1).AddYears(offset);
                    end = new DateTime(year + 1, 1, 1).AddDays(-1).AddYears(offset);
                    break;
                default://默认为月
                    offset = (int)getMode;
                    year = baseDate.Year;
                    month = baseDate.Month;
                    if (month == 0)
                    {
                        year--;
                        month = 12;
                    }
                    begin = new DateTime(year, month, 1).AddMonths(offset);
                    if (month == 12)
                    {
                        year++;
                        month = 1;
                    }
                    else
                    {
                        month++;
                    }
                    end = new DateTime(year, month, 1).AddDays(-1).AddMonths(offset);
                    break;
            }
        }

        /// <summary>
        /// 获取星期几（中文）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDayOfWeek(DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "星期日";
                case DayOfWeek.Monday:
                    return "星期一";
                case DayOfWeek.Tuesday:
                    return "星期二";
                case DayOfWeek.Wednesday:
                    return "星期三";
                case DayOfWeek.Thursday:
                    return "星期四";
                case DayOfWeek.Friday:
                    return "星期五";
                case DayOfWeek.Saturday:
                    return "星期六";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取今天是星期几（中文）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDayOfWeek()
        {
            return GetDayOfWeek(DateTime.Now);
        }
    }


    public enum DateTimeCalMode
    {
        天 = 1,
        周 = 2,
        月 = 3,
        季度 = 4,
        半年 = 5,
        年 = 6,
    }

    public enum DateTimeGetMode
    {
        Pre = -1,
        Now = 0,
        Next = 1,
    }
}
