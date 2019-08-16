using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.Json
{
    /// <summary>
    /// Json转换
    /// </summary>
    public static class I3JsonConvert
    {
        private static II3JsonConverter jsonConverter = new I3NewtonJsonConverter();

        /// <summary>
        /// 获取或设置转换器
        /// </summary>
        public static II3JsonConverter JsonConverter
        {
            get { return I3JsonConvert.jsonConverter; }
            set { I3JsonConvert.jsonConverter = value; }
        }
        
        /// <summary>
        /// 将对象转换为Json
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json</returns>
        public static string ToJson(object obj)
        {
            return jsonConverter.ToJson(obj);
        }
        /// <summary>
        /// 通过Json构建对象
        /// </summary>
        /// <param name="json">Json</param>
        /// <param name="type">类型</param>
        /// <returns>对象</returns>
        public static object FromJson(string json,Type type)
        {
            return jsonConverter.FromJson(json, type);
        }
        /// <summary>
        /// 通过Json构建对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="json">Json</param>
        /// <returns>对象</returns>
        public static T FromJson<T>(string json)
        {
            return (T)jsonConverter.FromJson(json, typeof(T));
        }
    }
}
