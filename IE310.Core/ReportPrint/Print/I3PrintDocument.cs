using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing.Printing;

namespace IE310.Core.ReportPrint
{
    public class I3PrintDocument : PrintDocument
    {
        private I3ReportDatas reportDatas;
        public I3ReportDatas ReportDatas
        {
            get
            {
                return reportDatas;
            }
        }

        private int startPage;
        public int StartPage
        {
            get
            {
                return startPage;
            }
        }

        private int endPage;
        public int EndPage
        {
            get
            {
                return endPage;
            }
        }

        private int nextPage = -1;
        public int NextPage
        {
            get
            {
                return nextPage;
            }
            set
            {
                nextPage = value;
            }
        }

        public I3PrintDocument(I3ReportDatas reportDatas, int startPage, int endPage)
        {
            this.reportDatas = reportDatas;
            this.startPage = startPage;
            this.endPage = endPage;
            this.nextPage = startPage;
        }

        public bool PaintPageIndex
        {
            get;
            set;
        }

    }
}
