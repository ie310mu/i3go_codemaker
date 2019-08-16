/*
 *   使用此类型为PropertyGrid动态构造数据行
 *   
 *   1.添加I3PropertyInfo，可完全动态构造属性，并设置其行为
 *     Name、Desciption、ReadOnly、Category、Converter、Editor定义属性的设计时行为
 *     属性的值默认从I3PropertyInfo.Value获取，也可通过GetValue，SetValue自定义
 *     也可以通过SourceObject、SourceProperty进行定义
 *     优先级从大到小为：事件，源对象属性，Value
 *   2.设置I3TypeDescriptor.SourceObject将会获取SourceObject的所有属性与相应的特性
 * 
 */


using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Reflection;

namespace IE310.Core.DynamicProperty
{
    public class I3TypeDescriptor : List<I3PropertyInfo>, ICustomTypeDescriptor
    {
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

        #region ICustomTypeDescriptor 成员

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(System.Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(System.Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(System.Attribute[] attributes)
        {
            List<PropertyDescriptor> props = new List<PropertyDescriptor>();

            if (this.sourceObject != null)
            {
                PropertyInfo[] properties = this.sourceObject.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    //Browsable
                    bool browsable = true;
                    BrowsableAttribute[] browsables = (BrowsableAttribute[])property.GetCustomAttributes(typeof(BrowsableAttribute), true);
                    if (browsables != null && browsables.Length > 0)
                    {
                        browsable = browsables[0].Browsable;
                    }

                    if (browsable)
                    {
                        I3PropertyInfo prop = new I3PropertyInfo();
                        prop.SourceObject = this.sourceObject;
                        prop.SourceProperty = property;
                        prop.Name = property.Name;

                        //DisplayName
                        DisplayNameAttribute[] displays = (DisplayNameAttribute[])property.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                        if (displays != null && displays.Length > 0)
                        {
                            prop.Name = displays[0].DisplayName;
                        }
                        //Description
                        DescriptionAttribute[] descriptions = (DescriptionAttribute[])property.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (descriptions != null && descriptions.Length > 0)
                        {
                            prop.Desciption = descriptions[0].Description;
                        }
                        //Category
                        CategoryAttribute[] ctegorys = (CategoryAttribute[])property.GetCustomAttributes(typeof(CategoryAttribute), true);
                        if (ctegorys != null && ctegorys.Length > 0)
                        {
                            prop.Category = ctegorys[0].Category;
                        }
                        //ReadOnly
                        ReadOnlyAttribute[] readOnlys = (ReadOnlyAttribute[])property.GetCustomAttributes(typeof(ReadOnlyAttribute), true);
                        if (readOnlys != null && readOnlys.Length > 0)
                        {
                            prop.ReadOnly = readOnlys[0].IsReadOnly;
                        }
                        //TypeConverter
                        TypeConverterAttribute[] typeConverters = (TypeConverterAttribute[])property.GetCustomAttributes(typeof(TypeConverterAttribute), true);
                        if (typeConverters != null && typeConverters.Length > 0)
                        {
                            Type type = Type.GetType(typeConverters[0].ConverterTypeName);
                            prop.Converter = (TypeConverter)Activator.CreateInstance(type);
                        }
                        //Editor
                        EditorAttribute[] editorrs = (EditorAttribute[])property.GetCustomAttributes(typeof(EditorAttribute), true);
                        if (editorrs != null && editorrs.Length > 0)
                        {
                            Type type = Type.GetType(editorrs[0].EditorTypeName);
                            prop.Editor = Activator.CreateInstance(type);
                        }

                        props.Add(new I3PropertyDescriptor(prop, attributes));
                    }
                }
            }

            for (int i = 0; i < this.Count; i++)
            {
                props.Add(new I3PropertyDescriptor(this[i], attributes));
            }
            return new PropertyDescriptorCollection(props.ToArray());
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

    }

}
