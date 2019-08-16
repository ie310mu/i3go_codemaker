using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using Newtonsoft.Json;

namespace IE310.Core.ReportPrint
{
    [Serializable]
    public class I3BorderInfo
    {
        private DashStyle type = DashStyle.Solid;
        [JsonProperty(PropertyName = "t")]
        public DashStyle Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        private float width = 0;
        [JsonProperty(PropertyName = "w")]
        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        private Color color = Color.Black;
        [JsonProperty(PropertyName = "c")]
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (width == 0)
                {
                    return true;
                }
                return false;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Type).Append(Width).Append(Color.ToArgb());
            return sb.ToString();
        }
    }
}
