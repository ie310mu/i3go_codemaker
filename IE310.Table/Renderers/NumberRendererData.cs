
using System;
using System.ComponentModel;
using System.Drawing;

using IE310.Table.Themes;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// Contains information about the current state of a number Cell's 
	/// up and down buttons
	/// </summary>
	public class I3NumberRendererData
	{
		#region Class Data

		/// <summary>
		/// The current state of the up button
		/// </summary>
		private I3UpDownStates upState;

		/// <summary>
		/// The current state of the down button
		/// </summary>
		private I3UpDownStates downState;
		
		/// <summary>
		/// The x coordinate of the last mouse click point
		/// </summary>
		private int clickX;

		/// <summary>
		/// The y coordinate of the last mouse click point
		/// </summary>
		private int clickY;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the NumberRendererData class
		/// </summary>
		public I3NumberRendererData()
		{
			this.upState = I3UpDownStates.Normal;
			this.downState = I3UpDownStates.Normal;
			this.clickX = -1;
			this.clickY = -1;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the current state of the up button
		/// </summary>
		public I3UpDownStates UpButtonState
		{
			get
			{
				return this.upState;
			}

			set
			{
				if (!Enum.IsDefined(typeof(I3UpDownStates), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(I3UpDownStates));
				}
					
				this.upState = value;
			}
		}


		/// <summary>
		/// Gets or sets the current state of the down button
		/// </summary>
		public I3UpDownStates DownButtonState
		{
			get
			{
				return this.downState;
			}

			set
			{
				if (!Enum.IsDefined(typeof(I3UpDownStates), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(I3UpDownStates));
				}
					
				this.downState = value;
			}
		}
		

		/// <summary>
		/// Gets or sets the Point that the mouse was last clicked in a button
		/// </summary>
		public Point ClickPoint
		{
			get
			{
				return new Point(this.clickX, this.clickY);
			}

			set
			{
				this.clickX = value.X;
				this.clickY = value.Y;
			}
		}

		#endregion
	}
}
