
using System;
using System.ComponentModel;
using System.Drawing;

using IE310.Table.Themes;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// Contains information about the current state of a Cell's button
	/// </summary>
	public class I3ButtonRendererData
	{
		#region Class Data

		/// <summary>
		/// The current state of the button
		/// </summary>
		private I3PushButtonStates buttonState;
		
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
		/// Initializes a new instance of the ButtonRendererData class
		/// </summary>
		public I3ButtonRendererData()
		{
			this.buttonState = I3PushButtonStates.Normal;
			this.clickX = -1;
			this.clickY = -1;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the current state of the button
		/// </summary>
		public I3PushButtonStates ButtonState
		{
			get
			{
				return this.buttonState;
			}

			set
			{
				if (!Enum.IsDefined(typeof(I3PushButtonStates), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(I3PushButtonStates));
				}
					
				this.buttonState = value;
			}
		}
		

		/// <summary>
		/// Gets or sets the Point that the mouse was last clicked in the button
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
