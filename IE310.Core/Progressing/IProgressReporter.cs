using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.Progressing
{
    /// <summary>
    /// 进度报告接口
    /// </summary>
    public interface IProgressReporter
    {
        void ChangeProgress(I3ProgressingEventArgs e);
        event StopByUserEvent StopByUser;
    }

    public delegate void StopByUserEvent();
}
