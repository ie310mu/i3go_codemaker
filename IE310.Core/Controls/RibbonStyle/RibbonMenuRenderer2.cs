using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace IE310.Core.Controls.RibbonStyle
{
    public class RibbonMenuRenderer : ToolStripProfessionalRenderer
    {
        int R0 = 255, G0 = 214, B0 = 78;
        public Color StrokeColor = Color.FromArgb(196, 177, 118);
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.HighQuality;
                GraphicsPath pa = new GraphicsPath();
                Rectangle rect = new Rectangle(2, 1, (int)e.Item.Size.Width - 2, e.Item.Size.Height-1);
                DrawArc(rect, pa);
                LinearGradientBrush lgbrush = new LinearGradientBrush(rect, Color.White, Color.White, LinearGradientMode.Vertical);

                float[] pos = new float[4];
                pos[0] = 0.0F; pos[1] = 0.4F; pos[2] = 0.45F; pos[3] = 1.0F;
                Color[] colors = new Color[4];
                colors[0] = GetColor(0, 50, 100);
                colors[1] = GetColor(0, 0, 30);
                colors[2] = Color.FromArgb(R0, G0, B0);
                colors[3] = GetColor(0, 50, 100);

                ColorBlend mix = new ColorBlend();
                mix.Colors = colors;
                mix.Positions = pos;
                lgbrush.InterpolationColors = mix;
                g.FillPath(lgbrush, pa);
                g.DrawPath(new Pen(StrokeColor), pa);
                lgbrush.Dispose();
            }
            else
            {
                base.OnRenderItemBackground(e);
            }
        }
        int offsetx = 3, offsety = 2, imageheight = 0, imagewidth = 0;
        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            if (e.Image != null)
            {
                imageheight = e.Item.Height - offsety * 2;
                imagewidth = (int)((Convert.ToDouble(imageheight) / e.Image.Height) * e.Image.Width);
            }
            e.Graphics.DrawImage(e.Image, new Rectangle(offsetx, offsety, imagewidth, imageheight));
        }
        
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // base.OnRenderToolStripBorder(e);
        }

        #region Paint Methods
        private int _radius = 6;
        public void DrawArc(Rectangle re, GraphicsPath pa)
        {
            int _radiusX0Y0 = _radius, _radiusXFY0 = _radius, _radiusX0YF = _radius, _radiusXFYF = _radius;
            pa.AddArc(re.X, re.Y, _radiusX0Y0, _radiusX0Y0, 180, 90);
            pa.AddArc(re.Width - _radiusXFY0, re.Y, _radiusXFY0, _radiusXFY0, 270, 90);
            pa.AddArc(re.Width - _radiusXFYF, re.Height - _radiusXFYF, _radiusXFYF, _radiusXFYF, 0, 90);
            pa.AddArc(re.X, re.Height - _radiusX0YF, _radiusX0YF, _radiusX0YF, 90, 90);
            pa.CloseFigure();
        }
        public Rectangle Deflate(Rectangle re)
        {
            return new Rectangle(re.X, re.Y, re.Width - 1, re.Height - 1);
        }
        public Color GetColor(int R, int G, int B)
        {
            if (R + R0 > 255) { R = 255; } else { R = R + R0; }
            if (G + G0 > 255) { G = 255; } else { G = G + G0; }
            if (B + B0 > 255) { B = 255; } else { B = B + B0; }

            return Color.FromArgb(R, G, B);
        }
        #endregion

    }
}
