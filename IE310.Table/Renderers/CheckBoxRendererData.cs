
using System;
using System.ComponentModel;

using IE310.Table.Themes;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// Contains information about the current state of a Cell's check box
	/// </summary>
	public class I3CheckBoxRendererData
	{
		#region Class Data

		/// <summary>
		/// The current state of the Cells check box
		/// </summary>
		private I3CheckBoxStates checkState;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ButtonRendererData class with the 
		/// specified CheckBox state
		/// </summary>
		/// <param name="checkState">The current state of the Cells CheckBox</param>
		public I3CheckBoxRendererData(I3CheckBoxStates checkState)
		{
			this.checkState = checkState;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the current state of the Cells checkbox
		/// </summary>
		public I3CheckBoxStates CheckState
		{
			get
			{
				return this.checkState;
			}

			set
			{
				if (!Enum.IsDefined(typeof(I3CheckBoxStates), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(I3CheckBoxStates));
				}
					
				this.checkState = value;
			}
		}

		#endregion
	}
}