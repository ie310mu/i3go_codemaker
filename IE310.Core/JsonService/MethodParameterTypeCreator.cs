using System;

namespace IE310.Core.JsonService
{
    /// <summary>
    /// 参数类型提供器
    /// </summary>
    public interface IMethodParameterTypeCreator
    {
        /// <summary>
        /// 获取参数类型
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        Type GetParameterType(object data);
    }
}
