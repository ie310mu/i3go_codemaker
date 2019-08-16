using System;
using System.Collections.Generic;

using System.Text;


namespace IE310.Core.ReportPrint
{
    public static class I3CellRendererBuilder
    {
        private static Dictionary<Type, II3CellRenderer> rendererDic = new Dictionary<Type, II3CellRenderer>();

        /// <summary>
        /// 从缓存中获取Renderer
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static II3CellRenderer GetRenderer(I3ReportCell cell)
        {
            if (cell == null)
            {
                return null;
            }

            return GetRenderer(cell.GetType());
        }

        /// <summary>
        /// 从缓存中获取Renderer
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static II3CellRenderer GetRenderer(Type type)
        {
            if (type == null)
            {
                return null;
            }

            if (rendererDic.ContainsKey(type))
            {
                return rendererDic[type];
            }

            II3CellRenderer result = BuildRenderer(type);
            rendererDic.Add(type, result);
            return result;
        }

        /// <summary>
        /// 构造新的Renderer
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static II3CellRenderer BuildRenderer(I3ReportCell cell)
        {
            if (cell == null)
            {
                return null;
            }

            return BuildRenderer(cell.GetType());
        }

        /// <summary>
        /// 构造新的Renderer
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static II3CellRenderer BuildRenderer(Type type)
        {
            if (type == null)
            {
                return null;
            }

            I3CellRendererAttribute[] attrs = (I3CellRendererAttribute[])Attribute.GetCustomAttributes(type, typeof(I3CellRendererAttribute), false);
            if (attrs == null || attrs.Length == 0)
            {
                return new I3ReportCellRenderer();
            }

            return (II3CellRenderer)Activator.CreateInstance(attrs[0].RendererType);
        }
    }
}
