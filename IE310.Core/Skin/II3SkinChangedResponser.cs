using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;

namespace IE310.Core.Skin
{
    /// <summary>
    /// 主界面皮肤改变后，控件的响应器
    /// </summary>
    public interface II3SkinChangedResponser
    {
        void ResponseSkinChanged(Control control, I3SkinType type);
    }
}
