using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.Json
{
    /// <summary>
    /// Json转换接口
    /// </summary>
    public interface II3JsonConverter
    {
        /// <summary>
        /// 将对象转换为Json
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json</returns>
        string ToJson(object obj);

        /// <summary>
        /// 通过Json构建对象
        /// </summary>
        /// <param name="json">Json</param>
        /// <param name="type">对象类型</param>
        /// <returns>对象</returns>
        object FromJson(string json, Type type);
    }
}
