using System;
using System.Collections.Generic;
using System.Text;

namespace IE310.Core.JsonService
{
    /// <summary>
    /// Json服务特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class JsonServiceAttribute : Attribute
    {
        private string name;
        private string description;

        /// <summary>
        /// 初始化一个JsonServiceAttribute的新实例。
        /// </summary>
        public JsonServiceAttribute()
        {
        }
        /// <summary>
        /// 初始化一个JsonServiceAttribute的新实例。
        /// </summary>
        /// <param name="description">方法描述</param>
        public JsonServiceAttribute(string description)
        {
            this.description = description;
        }
        /// <summary>
        /// 获取或设置服务名称
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        /// 获取或设置服务描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
