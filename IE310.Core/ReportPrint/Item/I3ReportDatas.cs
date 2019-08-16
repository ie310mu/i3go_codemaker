using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Runtime.Serialization;

namespace IE310.Core.ReportPrint
{
    /// <summary>
    /// ����������
    /// ע�⣬ҳ�����ñ�����һ���ģ�������ӡ��������ֽ�Ŵ�СҪ��һ���ģ�
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
        /// ���������Ĵ�ӡ�������ݣ�һҳ��Ӧһ��PrintArea  �����л�
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
        /// ҳ��Ŀ�ʼ���
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
                    //û��ָ��ֵ��Ĭ�Ϸ��ر����ݵ���ҳ��
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
