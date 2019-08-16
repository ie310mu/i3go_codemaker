using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using IE310.Core.Controls;
using System.Drawing;

namespace IE310.Core.Skin
{
    public class TreeViewResponser : II3SkinChangedResponser
    {
        public void ResponseSkinChanged(Control control, I3SkinType type)
        {
            TreeView tree = control as I3TreeView;
            tree.BeginUpdate();
            try
            {
                switch (type)
                {
                    case I3SkinType.黑:
                        tree.BackColor = I3SkinChangedManager.DefaultBackColor_Dark;
                        break;
                    case I3SkinType.深蓝:
                        tree.BackColor = I3SkinChangedManager.DefaultBackColor_DarkBlue;
                        break;
                    default:
                        tree.BackColor = I3SkinChangedManager.DefaultBackColor_Blue;
                        break;
                }
            }
            finally
            {
                tree.EndUpdate();
            }
        }
    }
}
