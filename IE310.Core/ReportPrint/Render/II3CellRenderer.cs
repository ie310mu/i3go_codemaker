using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;

namespace IE310.Core.ReportPrint
{
    /// <summary>
    /// 单元格渲染接口
    /// </summary>
    public interface II3CellRenderer
    {
        void DrawBackground(Graphics g, float scale, I3ReportData reportData, I3ReportCell cell, RectangleF rect, I3ReportCellStyle style);
        RectangleF DrawContent(Graphics g, float scale, I3ReportData reportData, I3ReportCell cell, RectangleF rect, I3ReportCellStyle style, I3PrintArea area, bool draw);
        void DrawCellBorder(Graphics g, float scale, I3ReportData reportData, I3ReportCell cell, RectangleF rect, I3ReportCellStyle style);

        /// <summary>
        /// 计算单元格需要的大小
        /// </summary>
        /// <param name="orgWidth"></param>
        /// <param name="orgHeight"></param>
        /// <param name="style"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        SizeF CalCellNeedSize(int orgWidth, int orgHeight, I3ReportCellStyle style, I3ReportCell cell);

        /// <summary>
        /// 调整单元格大小 prepareNarrow 是否处理内容缩小
        /// </summary>
        /// <param name="orgWidth"></param>
        /// <param name="orgHeight"></param>
        /// <param name="needSize"></param>
        /// <param name="style"></param>
        /// <param name="cell"></param>
        /// <param name="range"></param>
        /// <param name="reportData"></param>
        /// <param name="prepareNarrow"></param>
        void AdjustCellSize(int orgWidth, int orgHeight, SizeF needSize, I3ReportCellStyle style, I3ReportCell cell, I3MergeRange range, I3ReportData reportData, bool prepareNarrow);
    }
}
