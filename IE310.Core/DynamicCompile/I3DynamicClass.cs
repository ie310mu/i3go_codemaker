using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;

namespace IE310.Core.DynamicCompile
{
    public class I3DynamicClass
    {
        public I3DynamicClass()
        {
            InnerI3DynamicClass();
        }
        private void InnerI3DynamicClass()
        {
            this.UsingLines.Add(SystemUsing);
            this.UsingLines.Add(TextUsing);
            this.UsingLines.Add(GenericUsing);
            this.UsingLines.Add(ComponentModelUsing);

            this.referencedAssemblies.Add(SystemReferencedDLL);
            this.referencedAssemblies.Add(SystemDrawingDLL);
            this.referencedAssemblies.Add(FormsDll);
        }

        public static readonly string SystemUsing = "using System;";
        public static readonly string TextUsing = "using System.Text;";
        public static readonly string GenericUsing = "using System.Collections.Generic;";
        public static readonly string ComponentModelUsing = "using System.ComponentModel;";
        
        public static readonly string SystemReferencedDLL = "System.dll";
        public static readonly string SystemDrawingDLL = "System.Drawing.dll";
        public static readonly string FormsDll = "System.Windows.Forms.dll";

        private List<string> referencedAssemblies = new List<string>();
        public List<string> ReferencedAssemblies
        {
            get
            {
                return referencedAssemblies;
            }
            set
            {
                referencedAssemblies = value;
            }
        }

        private List<string> usingLines = new List<string>();
        public List<string> UsingLines
        {
            get
            {
                return usingLines;
            }
            set
            {
                usingLines = value;
            }
        }


        private List<string> classAttributes = new List<string>();
        public List<string> ClassAttributes
        {
            get
            {
                return classAttributes;
            }
            set
            {
                classAttributes = value;
            }
        }

        private string nameSapce;
        public string NameSapce
        {
            get
            {
                return nameSapce;
            }
            set
            {
                nameSapce = value;
            }
        }

        private bool isPartial;
        public bool IsPartial
        {
            get
            {
                return isPartial;
            }
            set
            {
                isPartial = value;
            }
        }

        private string className;
        public string ClassName
        {
            get
            {
                return className;
            }
            set
            {
                className = value;
            }
        }

        public string FullClassName
        {
            get
            {
                return this.nameSapce + "." + this.className;
            }
        }

        private List<string> baseClasies = new List<string>();
        public List<string> BaseClasies
        {
            get
            {
                return baseClasies;
            }
            set
            {
                baseClasies = value;
            }
        }

        private List<string> fields = new List<string>();
        public List<string> Fields
        {
            get
            {
                return fields;
            }
            set
            {
                fields = value;
            }
        }

        private List<string> properties = new List<string>();
        public List<string> Properties
        {
            get
            {
                return properties;
            }
            set
            {
                properties = value;
            }
        }

        private List<string> functions = new List<string>();
        public List<string> Functions
        {
            get
            {
                return functions;
            }
            set
            {
                functions = value;
            }
        }

        /// <summary>
        /// 构建源代码
        /// </summary>
        /// <returns></returns>
        public string BuildSource()
        {
            return InnerBuildSource();
        }
        private string InnerBuildSource()
        {
            StringBuilder sb = new StringBuilder();

            //引用 
            foreach (string usingLine in this.usingLines)
            {
                sb.AppendLine(usingLine);
            }
            sb.AppendLine("");

            //基类
            string baseClassStr = "";
            if (this.baseClasies != null && this.baseClasies.Count > 0)
            {
                foreach (string baseClass in this.baseClasies)
                {
                    baseClassStr = baseClassStr + "," + baseClass;
                }
                baseClassStr = " : " + baseClassStr.Substring(1);
            }

            //命名空间开始 
            sb.AppendLine("");
            sb.AppendLine("namespace " + this.nameSapce);
            sb.AppendLine("{");

            //类开始
            foreach (string attribute in this.classAttributes)
            {
                sb.AppendLine(attribute);
            }
            if (this.isPartial)
            {
                sb.AppendLine("public partial class " + this.className + baseClassStr);
            }
            else
            {
                sb.AppendLine("public class " + this.className + baseClassStr);
            }
            sb.AppendLine("{");

            //字段
            foreach (string field in this.fields)
            {
                sb.AppendLine(field);
            }

            //属性
            sb.AppendLine("");
            foreach (string property in this.properties)
            {
                sb.AppendLine("");
                sb.AppendLine(property);
                sb.AppendLine("");
            }

            //方法
            foreach (string function in this.functions)
            {
                sb.AppendLine("");
                sb.AppendLine(function);
            }

            //类结束
            sb.AppendLine("}");

            //命名空间结束
            sb.AppendLine("}");

            return sb.ToString();
        }


        /// <summary>
        /// 编译代码
        /// </summary>
        /// <param name="sClassName"></param>
        /// <param name="sSourceCode"></param>
        public Assembly Compile()
        {
            return InnerCompile();
        }
        public Assembly Compile(string source)
        {
            return InnerCompile(source);
        }
        private Assembly InnerCompile()
        {
            string source = this.BuildSource();
            return InnerCompile(source);
        }
        private Assembly InnerCompile(string source)
        {
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();

            CompilerParameters objCompilerParameters = new CompilerParameters();
            foreach (string referenced in this.referencedAssemblies)
            {
                objCompilerParameters.ReferencedAssemblies.Add(referenced);
            }
            objCompilerParameters.GenerateExecutable = false;
            objCompilerParameters.GenerateInMemory = true;

            CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, source);
            if (cr.Errors.HasErrors)
            {
                StringBuilder strBuf = new StringBuilder("编译错误：");
                foreach (CompilerError err in cr.Errors)
                {
                    strBuf.Append(err.Line).Append(err.ErrorText).Append("\r\n");
                }
                throw new Exception(source + "\r\n" + strBuf.ToString());
            }
            else
            {
                return cr.CompiledAssembly;
            }
        }

        public object CreateInstance()
        {
            return InnerCreateInstance();
        }
        private object InnerCreateInstance()
        {
            Assembly assembly = this.Compile();
            return assembly.CreateInstance(this.nameSapce + "." + this.className);
        }
    }
}
