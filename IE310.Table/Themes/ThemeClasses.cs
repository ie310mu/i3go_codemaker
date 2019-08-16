using System;

namespace IE310.Table.Themes
{
	/// <summary>
	/// Represents the different types of objects that can be 
	/// drawn by the Windows XP theme engine
	/// </summary>
	public sealed class I3ThemeClasses
	{
		#region Constructor

		/// <summary>
		/// Private constructor so that the class can't be instantiated
		/// </summary>
		private I3ThemeClasses()
		{
			
		}

		#endregion


		#region Properties

		/// <summary>
		/// Button objects (Button, CheckBox, RadioButton)
		/// </summary>
		public static string Button
		{
			get
			{
				return "BUTTON";
			}
		}


		/// <summary>
		/// ComboBox objects
		/// </summary>
		public static string ComboBox
		{
			get
			{
				return "COMBOBOX";
			}
		}


		/// <summary>
		/// TextBox objects
		/// </summary>
		public static string TextBox
		{
			get
			{
				return "EDIT";
			}
		}


		/// <summary>
		/// ColumnHeader objects
		/// </summary>
		public static string ColumnHeader
		{
			get
			{
				return "HEADER";
			}
		}


		/// <summary>
		/// ListView objects
		/// </summary>
		public static string ListView
		{
			get
			{
				return "LISTVIEW";
			}
		}


		/// <summary>
		/// ProgressBar objects
		/// </summary>
		public static string ProgressBar
		{
			get
			{
				return "PROGRESS";
			}
		}


		/// <summary>
		/// TabControl objects
		/// </summary>
		internal static string TabControl
		{
			get
			{
				return "TAB";
			}
		}


		/// <summary>
		/// UpDown objects
		/// </summary>
		public static string UpDown
		{
			get
			{
				return "SPIN";
			}
		}

		#endregion
	}
}
