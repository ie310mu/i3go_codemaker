using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.JsonService
{
    /// <summary>
    /// 结果状态
    /// </summary>
    public enum ServiceResultState
    {
        Success = 0,
        ServiceException = 1,
        LogicException = 2
    }
}
