using System;

namespace IE310.Core.JsonService
{
    /// <summary>
    /// 参数类型特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MethodParameterTypeAttribute : Attribute
    {
        private string parameterName;
        private string relParameterName;
        private Type creatorType;

        /// <summary>
        /// 初始化一个MethodParameterTypeAttribute的新实例。
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="relParameterName">关联参数名称</param>
        /// <param name="creatorType">提供器类型</param>
        public MethodParameterTypeAttribute(string parameterName, string relParameterName, Type creatorType)
        {
            this.parameterName = parameterName;
            this.relParameterName = relParameterName;
            this.creatorType = creatorType;
        }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName
        {
            get { return parameterName; }
            set { parameterName = value; }
        }
        /// <summary>
        /// 关联参数名称
        /// </summary>
        public string RelParameterName
        {
            get { return relParameterName; }
            set { relParameterName = value; }
        }
        /// <summary>
        /// 提供器类型
        /// </summary>
        public Type CreatorType
        {
            get { return creatorType; }
            set { creatorType = value; }
        }
    }
}
