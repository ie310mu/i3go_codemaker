using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using IE310.Table.Models;

namespace IE310.Core.Skin
{
    class I3TableResponser : II3SkinChangedResponser
    {
        public void ResponseSkinChanged(Control control, I3SkinType type)
        {
            I3Table table = control as I3Table;
            switch (type)
            {
                case I3SkinType.黑:
                    table.BackColor = I3SkinChangedManager.DefaultBackColor_Dark;
                    break;
                case I3SkinType.深蓝:
                    table.BackColor = I3SkinChangedManager.DefaultBackColor_DarkBlue;
                    break;
                default:
                    table.BackColor = I3SkinChangedManager.DefaultBackColor_Blue;
                    break;
            }
        }
    }
}
