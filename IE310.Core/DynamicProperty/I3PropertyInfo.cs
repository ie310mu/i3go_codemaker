using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace IE310.Core.DynamicProperty
{
    public class I3PropertyInfo
    {
        private string name = "";
        private object value = null;


        //1.执行事件
        public event EventHandler<XPropGetValueEventArgs> GetValue;
        public event EventHandler<XPropSetValueEventArgs> SetValue;

        //2.从SourceObject获取 
        private object sourceObject;
        public object SourceObject
        {
            get
            {
                return sourceObject;
            }
            set
            {
                sourceObject = value;
            }
        }
        private PropertyInfo sourceProperty;
        public PropertyInfo SourceProperty
        {
            get
            {
                return sourceProperty;
            }
            set
            {
                sourceProperty = value;
                this.ResetPropertyDescriptorFromSource();
            }
        }

        /// <summary>
        /// 从SourceProperty获取属性描述
        /// </summary>
        private void ResetPropertyDescriptorFromSource()
        {
            if (this.sourceProperty == null)
            {
                return;
            }

            //DisplayName
            DisplayNameAttribute[] dis = (DisplayNameAttribute[])this.sourceProperty.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (dis != null && dis.Length > 0)
            {
                this.name = dis[0].DisplayName;
            }

            //Desciption
            DescriptionAttribute[] ds = (DescriptionAttribute[])this.sourceProperty.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (ds != null && ds.Length > 0)
            {
                this.desciption = ds[0].Description;
            }

            //ReadOnly
            ReadOnlyAttribute[] rs = (ReadOnlyAttribute[])this.sourceProperty.GetCustomAttributes(typeof(ReadOnlyAttribute), true);
            if (rs != null && rs.Length > 0)
            {
                this.readOnly = rs[0].IsReadOnly;
            }

            //Category
            CategoryAttribute[] cs = (CategoryAttribute[])this.sourceProperty.GetCustomAttributes(typeof(CategoryAttribute), true);
            if (cs != null && cs.Length > 0)
            {
                this.category = cs[0].Category;
            }

            //Converter
            TypeConverterAttribute[] ts = (TypeConverterAttribute[])this.sourceProperty.GetCustomAttributes(typeof(TypeConverterAttribute), true);
            if (ts != null && ts.Length > 0)
            {
                this.converter = (TypeConverter)Activator.CreateInstance(Type.GetType(ts[0].ConverterTypeName));
            }


            //Editor
            EditorAttribute[] es = (EditorAttribute[])this.sourceProperty.GetCustomAttributes(typeof(EditorAttribute), true);
            if (es != null && es.Length > 0)
            {
                this.editor = Activator.CreateInstance(Type.GetType(es[0].EditorTypeName));
            }
        }


        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(this.name) && sourceProperty != null)
                {
                    return sourceProperty.Name;
                }
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public object Value
        {
            get
            {
                if (this.GetValue != null)
                {
                    XPropGetValueEventArgs e = new XPropGetValueEventArgs();
                    this.GetValue(this, e);
                    return e.ReturnValue;
                }
                if (this.sourceObject != null && this.sourceProperty != null)
                {
                    return this.sourceProperty.GetValue(this.sourceObject, null);
                }
                return this.value;
            }
            set
            {
                if (this.SetValue != null)
                {
                    XPropSetValueEventArgs e = new XPropSetValueEventArgs(value);
                    this.SetValue(this, e);
                    return;
                }
                if (this.sourceObject != null && this.sourceProperty != null)
                {
                    this.sourceProperty.SetValue(this.sourceObject, value, null);
                    return;
                }
                this.value = value;
            }
        }

        private string desciption;
        public string Desciption
        {
            get
            {
                return desciption;
            }
            set
            {
                desciption = value;
            }
        }

        private bool readOnly;
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                readOnly = value;
            }
        }

        //private bool isBrowsable;
        //public bool IsBrowsable
        //{
        //    get
        //    {
        //        return isBrowsable;
        //    }
        //    set
        //    {
        //        isBrowsable = value;
        //    }
        //}

        private string category;
        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        private TypeConverter converter;
        public TypeConverter Converter
        {
            get
            {
                return converter;
            }
            set
            {
                converter = value;
            }
        }

        private object editor;
        public object Editor
        {
            get
            {
                return editor;
            }
            set
            {
                editor = value;
            }
        }

    }

    public class XPropGetValueEventArgs : EventArgs
    {
        public XPropGetValueEventArgs()
        {
        }


        private object returnValue;
        public object ReturnValue
        {
            get
            {
                return returnValue;
            }
            set
            {
                returnValue = value;
            }
        }
    }

    public class XPropSetValueEventArgs : EventArgs
    {
        public XPropSetValueEventArgs(object value)
        {
            this.value = value;
        }


        private object value;
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                value = value;
            }
        }
    }
}
