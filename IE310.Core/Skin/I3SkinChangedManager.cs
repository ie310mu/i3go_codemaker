using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using IE310.Core.Controls;
using System.Drawing;
using IE310.Table.Models;

namespace IE310.Core.Skin
{
    public class I3SkinChangedManager
    {
        public static Color DefaultBackColor_Dark = Color.FromArgb(162, 162, 162);
        public static Color DefaultBackColor_Blue = Color.FromArgb(222, 233, 245);
        public static Color DefaultBackColor_DarkBlue = Color.FromArgb(188, 199, 216);

        private I3SkinChangedManager()
        {
        }

        private static I3SkinChangedManager current;
        public static I3SkinChangedManager Current
        {
            get
            {
                if (I3SkinChangedManager.current == null)
                {
                    I3SkinChangedManager.current = new I3SkinChangedManager();
                    I3SkinChangedManager.current.Init();
                }
                return I3SkinChangedManager.current;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            RegisterResponser(typeof(TreeView), new TreeViewResponser());
            RegisterResponser(typeof(I3TreeView), new TreeViewResponser());
            RegisterResponser(typeof(I3Table), new I3TableResponser());
        }

        /// <summary>
        /// 字典
        /// </summary>
        private Dictionary<Type, II3SkinChangedResponser> responserDic = new Dictionary<Type, II3SkinChangedResponser>();
        private II3SkinChangedResponser defaultResponser = new I3DefaultSkinChangedResponser();

        public void RegisterResponser(Type type, II3SkinChangedResponser responser)
        {
            if (responserDic.ContainsKey(type))
            {
                responserDic.Remove(type);
            }

            responserDic.Add(type, responser);
        }

        public II3SkinChangedResponser GetResponser(Type type)
        {
            if (responserDic.ContainsKey(type))
            {
                return responserDic[type];
            }

            return defaultResponser;
        }


        public void ResponseSkinChanged(Control control, I3SkinType type)
        {
            II3SkinChangedResponser responser = GetResponser(control.GetType());
            if (responser != null)
            {
                responser.ResponseSkinChanged(control, type);
            }

            foreach (Control subControl in control.Controls)
            {
                ResponseSkinChanged(subControl, type);
            }
        }
    }
}
