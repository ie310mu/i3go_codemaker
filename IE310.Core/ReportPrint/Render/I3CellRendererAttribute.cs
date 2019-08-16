using System;
using System.Collections.Generic;

using System.Text;

namespace IE310.Core.ReportPrint
{
    public class I3CellRendererAttribute : Attribute
    {
        public I3CellRendererAttribute(Type rendererType)
        {
            this.rendererType = rendererType;
        }

        private Type rendererType;

        public Type RendererType
        {
            get
            {
                return rendererType;
            }
            set
            {
                rendererType = value;
            }
        }
    }
}
