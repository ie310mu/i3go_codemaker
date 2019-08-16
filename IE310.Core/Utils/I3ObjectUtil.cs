using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using System.Collections;
using System.ComponentModel;

namespace IE310.Core.Utils
{

    public static class I3ObjectUtil
    {
        /// <summary>
        /// 深度复制Array
        /// </summary>
        /// <param name="sourceArray"></param>
        /// <returns></returns>
        private static Array DeepCopyArray(Array sourceArray)
        {
            //获取元素类型
            string fullTypeString = sourceArray.GetType().AssemblyQualifiedName;
            int index = fullTypeString.IndexOf("[]");
            string elementTypeString = fullTypeString.Remove(index, 2);
            Type elementType = Type.GetType(elementTypeString);

            Array result = Array.CreateInstance(elementType, sourceArray.Length);
            int itemIndex = -1;
            foreach (object item in sourceArray)
            {
                itemIndex++;
                if (item == null || item.GetType().IsValueType || item.GetType() == typeof(string))
                {
                    result.SetValue(item, itemIndex);
                }
                else if (item is Array)
                {
                    Array newArray = DeepCopyArray((Array)item);
                    result.SetValue(newArray, itemIndex);
                }
                else
                {
                    object newItem = Activator.CreateInstance(item.GetType());
                    DeepCopyProperty(item, newItem);
                    result.SetValue(newItem, itemIndex);
                }
            }
            return result;
        }

        /// <summary>
        /// 深度复制 IList  对象都不能为空
        /// </summary>
        /// <param name="destList"></param>
        /// <param name="sourceList"></param>
        private static void DeepCopyList(IList sourceList, IList destList)
        {
            destList.Clear();
            foreach (object item in sourceList)
            {
                if (item == null || item.GetType().IsValueType || item.GetType() == typeof(string))
                {
                    destList.Add(item);
                }
                else if (item is Array)
                {
                    Array newArray = DeepCopyArray((Array)item);
                    destList.Add(newArray);
                }
                else
                {
                    object newItem = Activator.CreateInstance(item.GetType());
                    DeepCopyProperty(item, newItem);
                    destList.Add(newItem);
                }
            }
        }


        /// <summary>
        /// 深度复制 IDictionary  对象都不能为空
        /// </summary>
        /// <param name="destList"></param>
        /// <param name="sourceList"></param>
        private static void DeepCopyDictionary(IDictionary sourceList, IDictionary destList)
        {
            destList.Clear();
            foreach (object key in sourceList.Keys)
            {
                object newKey;
                if (key == null || key.GetType().IsValueType || key.GetType() == typeof(string))
                {
                    newKey = key;
                }
                else if (key is Array)
                {
                    newKey = DeepCopyArray((Array)key);
                }
                else
                {
                    newKey = Activator.CreateInstance(key.GetType());
                    DeepCopyProperty(key, newKey);
                }

                object value = sourceList[key];
                object newValue;
                if (value == null || value.GetType().IsValueType || value.GetType() == typeof(string))
                {
                    newValue = value;
                }
                else if (value is Array)
                {
                    newValue = DeepCopyArray((Array)value);
                }
                else
                {
                    newValue = Activator.CreateInstance(value.GetType());
                    DeepCopyProperty(value, newValue);
                }

                destList.Add(newKey, newValue);
            }
        }


        
        /// <summary>
        /// 深度复制引用类型对象的值 ，对象都不能为空，如果是值类型或string，则不起作用
        /// 支持:
        /// 一般值类型
        /// List、多重List、ArrayList(IList)
        /// 一维数组、多维数组(Array)
        /// SortedList、SortedList《T,T》、Dictionary、SortedDictionary(IDictionary)
        /// 注意：结构体属性值类型，其字段不能使用引用类型，否则只会复制此字段的引用，不能实现深度复制
        /// 注意：只复制属性值，不复制字段值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public static void DeepCopyProperty(object source, object dest)
        {
            //为空或引用类型
            if (source == null || source.GetType().IsValueType || source.GetType() == typeof(string)
                || dest == null || dest.GetType().IsValueType || dest.GetType() == typeof(string))
            {
                return;
            }

            //IList支持  目标类型不为IList时，不处理
            if (source.GetType().GetInterface("IList") != null)
            {
                if (dest.GetType().GetInterface("IList") != null)
                {
                    DeepCopyList((IList)source, (IList)dest);
                }
                return;
            }
            //IDictionary
            if (source.GetType().GetInterface("IDictionary") != null)
            {
                if (dest.GetType().GetInterface("IDictionary") != null)
                {
                    DeepCopyDictionary((IDictionary)source, (IDictionary)dest);
                }
                return;
            }

            //引用类型
            PropertyInfo[] sourceProperties = source.GetType().GetProperties();
            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destProperty = dest.GetType().GetProperty(sourceProperty.Name);
                if (destProperty != null)
                {
                    object sourceValue = sourceProperty.GetValue(source, null);
                    if (sourceValue == null || sourceProperty.PropertyType.IsValueType || sourceProperty.PropertyType == typeof(string))
                    {
                        destProperty.SetValue(dest, sourceValue, null);
                    }
                    else if (sourceValue is Array) //Array--->IList---->引用类型   但无构造函数  
                    {
                        Array newArray = DeepCopyArray((Array)sourceValue);
                        destProperty.SetValue(dest, newArray, null);
                    }
                    else //非值、Array类型  有构造函数的特殊类型如IList,SortedList等
                    {
                        object destValue = destProperty.GetValue(dest, null);
                        if (destValue == null)  //目标值为空时才创建新实例(尽量保持原有引用)
                        {
                            destValue = Activator.CreateInstance(sourceProperty.PropertyType);
                            destProperty.SetValue(dest, destValue, null);
                        }
                        DeepCopyProperty(sourceValue, destValue);
                    }
                }
            }
        }



        /// <summary>
        /// 将BindingList转换为数组
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TSource[] ToArray<TSource>(BindingList<TSource> source)
        {
            List<TSource> result = new List<TSource>();
            foreach (TSource item in source)
            {
                result.Add(item);
            }
            return result.ToArray();
        }

        /// <summary>
        /// 属性是值类型时，设置默认值
        /// </summary>
        /// <param name="obj"></param>
        public static void SetDefaultProperty(object obj)
        {
            if (obj == null)
            {
                return;
            }

            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(obj, "", null);
                }
                else if (property.PropertyType == typeof(bool))
                {
                    property.SetValue(obj, false, null);
                }
                else if (property.PropertyType == typeof(Char))
                {
                    property.SetValue(obj, ' ', null);
                }
                else if (property.PropertyType == typeof(sbyte))
                {
                    sbyte sb = 0;
                    property.SetValue(obj, sb, null);
                }
                else if (property.PropertyType == typeof(byte))
                {
                    byte b = 0;
                    property.SetValue(obj, b, null);
                }
                else if (property.PropertyType == typeof(short))
                {
                    short s = 0;
                    property.SetValue(obj, s, null);
                }
                else if (property.PropertyType == typeof(ushort))
                {
                    ushort us = 0;
                    property.SetValue(obj, us, null);
                }
                else if (property.PropertyType == typeof(int))
                {
                    int i = 0;
                    property.SetValue(obj, i, null);
                }
                else if (property.PropertyType == typeof(uint))
                {
                    uint ui = 0;
                    property.SetValue(obj, ui, null);
                }
                else if (property.PropertyType == typeof(long))
                {
                    long l = 0;
                    property.SetValue(obj, l, null);
                }
                else if (property.PropertyType == typeof(ulong))
                {
                    ulong ul = 0;
                    property.SetValue(obj, ul, null);
                }
                else if (property.PropertyType == typeof(Single))
                {
                    Single s = 0;
                    property.SetValue(obj, s, null);
                }
                else if (property.PropertyType == typeof(double))
                {
                    double d = 0D;
                    property.SetValue(obj, d, null);
                }
                else if (property.PropertyType == typeof(decimal))
                {
                    decimal d = new decimal(0);
                    property.SetValue(obj, d, null);
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    DateTime d = new DateTime(1900, 1, 1);
                    property.SetValue(obj, d, null);
                }
            }
        }


        /// <summary>
        /// 比较两个对象的所有属性的值，所有属性相等时，认为两个对象相等
        /// 注意：属性的类型是值类型时，比较实际的值；是引用类型时，比较是否引用同一个对象
        /// 注意：两个对象不能为空，且Type必须一致
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public static bool CompareProperty(object source, object dest)
        {
            PropertyInfo[] properties = source.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object sourceValue = property.GetValue(source, null);
                object destValue = property.GetValue(dest, null);
                if (!object.Equals(sourceValue, destValue))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
