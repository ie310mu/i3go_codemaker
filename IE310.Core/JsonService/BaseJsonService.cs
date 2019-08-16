using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Web.SessionState;
using IE310.Core.Json;
using IE310.Core.Utils;

namespace IE310.Core.JsonService
{
    /// <summary>
    /// Json服务基础类
    /// </summary>
    public abstract class BaseJsonService : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 获取是否可重用
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        protected HttpContext context;

        protected virtual void BeforeProcessRequest()
        {
        }

        protected virtual void AfterProcessRequest()
        {
        }


        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context">Http上下文</param>
        virtual public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            HttpResponse response = context.Response;
            response.Clear();
            response.ContentType = "application/json";
            //response.ContentType = "text/plain";
            response.ContentEncoding = Encoding.UTF8;
            context.Request.ContentEncoding = Encoding.UTF8;

            bool packageResult = GetPackageResultParam(context.Request);
            string json = null;
            try
            {
                BeforeProcessRequest();
                if (packageResult)
                {
                    ServiceResult result = new ServiceResult();
                    //调用方法
                    result.data = ProcessMethod(context.Request);
                    result.state = (int)ServiceResultState.Success;
                    json = I3JsonConvert.ToJson(result);
                }
                else
                {
                    json = I3JsonConvert.ToJson(ProcessMethod(context.Request));
                }
                AfterProcessRequest();
            }
            catch (JsonServiceException ex)
            {
                //I3LocalLogUtil.Current.LogDir = "locallog";
                I3LocalLogUtil.Current.WriteExceptionLog("", ex);
                I3LocalLogUtil.Current.CompleteLog();
                ServiceResult result = new ServiceResult();
                result.state = (int)ServiceResultState.ServiceException;
#if DebugMode
                result.Message = ex.ToString();
#else
                result.message = ex.Message;
#endif
                json = I3JsonConvert.ToJson(result);
            }
            catch (Exception ex)
            {
                //I3LocalLogUtil.Current.LogDir = "locallog";
                I3LocalLogUtil.Current.WriteExceptionLog("", ex);
                I3LocalLogUtil.Current.CompleteLog();
                ServiceResult result = new ServiceResult();
                result.state = (int)ServiceResultState.LogicException;
#if DebugMode
                result.Message = ex.ToString();
#else
                result.message = ex.Message;
#endif
                json = I3JsonConvert.ToJson(result);
            }
            response.Write(json);
            response.Flush();
            //response.Close();  //加这句chorme浏览器报错

        }

        /// <summary>
        /// 验证非空
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        protected void ValidateNotNull(string name, object value)
        {
            if (value == null)
            {
                throw new JsonServiceException(name + "不能为空！");
            }
            else if (value is string)
            {
                if (((string)value).Length == 0)
                {
                    throw new JsonServiceException(name + "不能为空！");
                }
            }
        }

        /// <summary>
        /// 查找方法  未找到会抛出异常
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual MethodInfo SearchMethod(HttpRequest request)
        {
            string methodName = request["m"];
            //方法为空则返回
            this.ValidateNotNull("m参数", methodName);
            if (methodName[0] == '"')
            {
                methodName = methodName.Substring(1, methodName.Length - 2);
            }
            //查找方法
            Type type = this.GetType();
            MethodInfo method = type.GetMethod(methodName);
            if (method == null)
            {
                MethodInfo[] methods = this.GetType().GetMethods();
                foreach (MethodInfo m in methods)
                {
                    JsonMethodAttribute[] attributes = (JsonMethodAttribute[])m.GetCustomAttributes(typeof(JsonMethodAttribute), false);
                    if (attributes.Length > 0 && attributes[0].Name == methodName)
                    {
                        method = m;
                        break;
                    }
                }
            }
            if (method == null)
            {
                throw new JsonServiceException("方法" + methodName + "不存在！");
            }
            return method;
        }

        /// <summary>
        /// 构建参数  构建失败会抛出异常
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual object[] CreateParameters(HttpRequest request, MethodInfo method)
        {
            object[] ps = null;
            try
            {
                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length > 0)
                {
                    MethodParameterTypeAttribute[] attrs = method.GetCustomAttributes(typeof(MethodParameterTypeAttribute), true) as MethodParameterTypeAttribute[];
                    ps = new object[parameters.Length];
                    int i = 0;
                    foreach (ParameterInfo parameter in parameters)//遍历方法的参数
                    {
                        string val = request[parameter.Name];
                        val = HttpUtility.UrlDecode(val, Encoding.UTF8);
                        if (!string.IsNullOrEmpty(val))//参数值不能为空
                        {
                            //非最终类处理
                            MethodParameterTypeAttribute pta = null;
                            foreach (MethodParameterTypeAttribute attr in attrs)//查找此参数对应的MethodParameterTypeAttribute
                            {
                                if (attr.ParameterName == parameter.Name)
                                {
                                    pta = attr;
                                    break;
                                }
                            }
                            if (pta != null)
                            {
                                IMethodParameterTypeCreator provider = Activator.CreateInstance(pta.CreatorType) as IMethodParameterTypeCreator;
                                foreach (ParameterInfo p in parameters)//查找关联参数
                                {
                                    if (p.Name == pta.RelParameterName)
                                    {
                                        string relVal = request[pta.RelParameterName];
                                        //获取关联参数
                                        object data = string.IsNullOrEmpty(relVal) ? null : I3JsonConvert.FromJson(relVal, p.ParameterType);
                                        //根据本参数的值和关联参数对象 生成本参数的对象
                                        ps[i] = I3JsonConvert.FromJson(val, provider.GetParameterType(data));
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (parameter.ParameterType == typeof(string))
                                {
                                    if (val.StartsWith("\"") && val.EndsWith("\""))
                                    {
                                        //参数为string类型时，参数值两端可加双引号，也可不加（标准json是需要加的）
                                        try
                                        {
                                            ps[i] = I3JsonConvert.FromJson(val, parameter.ParameterType);
                                        }
                                        catch
                                        {
                                            ps[i] = val;
                                        }
                                    }
                                    else
                                    {
                                        ps[i] = val;
                                    }
                                }
                                else
                                {
                                    if (parameter.ParameterType == typeof(bool))
                                    {
                                        val = val == null ? null : val.ToLower();
                                    }
                                    ps[i] = I3JsonConvert.FromJson(val, parameter.ParameterType);
                                }
                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new JsonServiceException("参数获取失败！", ex);
            }
            return ps;
        }

        /// <summary>
        /// 获取参数，用于指示是否封装结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool GetPackageResultParam(HttpRequest request)
        {
            if (request.Params["PackageResult"] != null && request.Params["PackageResult"].ToString().ToUpper().Equals("FALSE"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 处理方法
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        protected virtual object ProcessMethod(HttpRequest request)
        {
            //查找方法
            MethodInfo method = SearchMethod(request);
            //构建参数
            object[] ps = CreateParameters(request, method);

            //调用方法 
            //method.Invoke中发生异常时，会返回TargetInvocationException，原始异常在TargetInvocationException.InnerException中
            try
            {
                //无返回值的方法，会返回null
                return method.Invoke(this, ps);
            }
            catch (TargetInvocationException ex)
            {
                //I3LocalLogUtil.Current.LogDir = "locallog";
                I3LocalLogUtil.Current.WriteExceptionLog("", ex.InnerException);
                I3LocalLogUtil.Current.CompleteLog();
                throw ex.InnerException;
            }
            catch(Exception ex)
            {
                //I3LocalLogUtil.Current.LogDir = "locallog";
                I3LocalLogUtil.Current.WriteExceptionLog("", ex);
                I3LocalLogUtil.Current.CompleteLog();
                throw ex;
            }
        }
    }
}
