using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Runtime.Serialization;

namespace IE310.Core.ReportPrint
{
    /// <summary>
    /// 报表数据组
    /// 注意，页面设置必须是一样的，否则会打印错误（起码纸张大小要是一样的）
    /// </summary>
    [Serializable]
    public class I3ReportDatas
    {
        private I3ReportData[] datas;
        public I3ReportData[] Datas
        {
            get
            {
                return datas;
            }
            set
            {
                datas = value;
            }
        }


        [NonSerialized]
        private I3PrintAreas printAreas = null;
        /// <summary>
        /// 经过计算后的打印区域数据，一页对应一个PrintArea  不序列化
        /// </summary>
        public I3PrintAreas PrintAreas
        {
            get
            {
                return printAreas;
            }
        }

        public I3ReportDatas()
        {
        }


        public I3ReportDatas(I3ReportData data)
            : this(new I3ReportData[] { data })
        {
        }

        public I3ReportDatas(I3ReportData[] datas)
        {
            this.datas = datas;
        }

        public void ReCalSizeAndPageInfo()
        {
            printAreas = new I3PrintAreas();
            foreach (I3ReportData data in datas)
            {
                data.ReCalSizeAndPageInfo();
                foreach (I3PrintArea area in data.PrintAreas.Dic.Values)
                {
                    area.ReportData = data;
                    area.Index = printAreas.Dic.Count;
                    printAreas.Dic.Add(printAreas.Dic.Count, area);
                }
            }
        }


        private int pageIndexStart = 1;
        /// <summary>
        /// 页码的开始序号
        /// </summary>
        public int PageIndexStart
        {
            get
            {
                return pageIndexStart;
            }
            set
            {
                pageIndexStart = value;
            }
        }

        private int totalPageCount = -1;

        public int TotalPageCount
        {
            get
            {
                if (totalPageCount == -1)
                {
                    //没有指定值，默认返回本数据的总页数
                    return this.PrintAreas.Dic.Count;
                }
                return totalPageCount;
            }
            set
            {
                totalPageCount = value;
            }
        }


        public I3ReportData GetReportDataByAreaIndex(int index)
        {
            return PrintAreas.Dic[index].ReportData;
        }
    }
}
