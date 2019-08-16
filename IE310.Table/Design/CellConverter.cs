
using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;

using IE310.Table.Models;
using IE310.Table.Cell; 


namespace IE310.Table.Design
{
	/// <summary>
	/// A custom TypeConverter used to help convert Cells from 
	/// one Type to another
	/// </summary>
	public class I3CellConverter : TypeConverter
	{
		/// <summary>
        /// 判断能否转换为指定的类型
		/// Returns whether this converter can convert the object to the 
		/// specified type, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <param name="destinationType">A Type that represents the type 
		/// you want to convert to</param>
		/// <returns>true if this converter can perform the conversion; o
		/// therwise, false</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}

			return base.CanConvertTo(context, destinationType);
		}


		/// <summary>
        /// 转换为指定的类型
		/// Converts the given value object to the specified type, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="culture">A CultureInfo object. If a null reference 
		/// is passed, the current culture is assumed</param>
		/// <param name="value">The Object to convert</param>
		/// <param name="destinationType">The Type to convert the value 
		/// parameter to</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor) && value is I3Cell)
			{
				ConstructorInfo ci = typeof(I3Cell).GetConstructor(new Type[] {});

				if (ci != null)
				{
					return new InstanceDescriptor(ci, null, false);
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
