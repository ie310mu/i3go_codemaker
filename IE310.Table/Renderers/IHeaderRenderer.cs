
using System;

using IE310.Table.Events;


namespace IE310.Table.Renderers 
{
	/// <summary>
	/// Exposes common methods provided by Column header renderers
	/// </summary>
	public interface II3HeaderRenderer : II3Renderer
	{
		/// <summary>
		/// Raises the PaintColumnHeader event
		/// </summary>
		/// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
		void OnPaintColumnHeader(I3PaintColumnHeaderEventArgs e);


        /// <summary>
        /// Raises the PaintRowHeader event
        /// </summary>
        /// <param name="e">A PaintHeaderEventArgs that contains the event data</param>
        void OnPaintRowHeader(I3PaintRowHeaderEventArgs e);
		
		
		/// <summary>
		/// Raises the MouseEnter event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnMouseEnter(I3ColumnHeaderMouseEventArgs e);


		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnMouseLeave(I3ColumnHeaderMouseEventArgs e);


		/// <summary>
		/// Raises the MouseUp event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnMouseUp(I3ColumnHeaderMouseEventArgs e);


		/// <summary>
		/// Raises the MouseDown event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnMouseDown(I3ColumnHeaderMouseEventArgs e);


		/// <summary>
		/// Raises the MouseMove event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnMouseMove(I3ColumnHeaderMouseEventArgs e);


		/// <summary>
		/// Raises the Click event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnClick(I3ColumnHeaderMouseEventArgs e);


		/// <summary>
		/// Raises the DoubleClick event
		/// </summary>
		/// <param name="e">A HeaderMouseEventArgs that contains the event data</param>
		void OnDoubleClick(I3ColumnHeaderMouseEventArgs e);
	}
}
