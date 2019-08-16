using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace IE310.Core.Controls
{
    public partial class I3PercentProgressBar : ProgressBar
    {
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            const int WM_PAINT = 15;
            if (m.Msg == WM_PAINT && this.Value > 0)
            {
                using (Graphics g = this.CreateGraphics())
                {
                    string percentage = this.Tag == null ? Value.ToString() : this.Tag.ToString();
                    StringFormat sf = new StringFormat();
                    sf.Alignment = sf.LineAlignment = StringAlignment.Near;
                    SizeF sizeF = g.MeasureString(percentage, SystemFonts.DefaultFont);
                    float top = (this.Height - sizeF.Height) / 2;
                    g.DrawString(percentage, SystemFonts.DefaultFont, Brushes.Black, new PointF(5, top), sf);
                    //g.DrawString(percentage, SystemFonts.DefaultFont, Brushes.Black, this.ClientRectangle, sf);
                }
            }
        }
 
    }
}
