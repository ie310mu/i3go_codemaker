using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace IE310.Core.DynamicProperty
{
    public class I3PropertyDescriptor : PropertyDescriptor
    {
        I3PropertyInfo theProp;

        public I3PropertyDescriptor(I3PropertyInfo prop, Attribute[] attrs)
            : base(prop.Name, attrs)
        {
            theProp = prop;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override System.Type ComponentType
        {
            get
            {
                return this.GetType();
            }
        }

        public override object GetValue(object component)
        {
            return theProp.Value;
        }

        public override string Description
        {
            get
            {
                return this.theProp.Desciption;
            }
        }

        //**********************不起作用，不会执行到这里来，
        //因此，最好的方法是不添加对应的项
        //public override bool IsBrowsable
        //{
        //    get
        //    {
        //        return this.theProp.IsBrowsable;
        //    }
        //}

        public override string Category
        {
            get
            {
                return this.theProp.Category;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return this.theProp.ReadOnly;
            }
        }

        public override TypeConverter Converter
        {
            get
            {
                return this.theProp.Converter;
            }
        }

        public override object GetEditor(Type editorBaseType)
        {
            return this.theProp.Editor;
        }


        public override System.Type PropertyType
        {
            get
            {
                object value = theProp.Value;
                if (value == null)
                {
                    return typeof(object);
                }
                return value.GetType();
            }
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            theProp.Value = value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    } 
}
