
using System;
using System.Windows.Forms;

using IE310.Table.Events;


namespace IE310.Table.Renderers
{
	/// <summary>
	/// Exposes common methods provided by Cell renderers
	/// </summary>
	public interface II3CellRenderer : II3Renderer
	{
		/// <summary>
		/// Raises the PaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		void OnPaintCell(I3PaintCellEventArgs e);


		/// <summary>
		/// Raises the GotFocus event
		/// </summary>
		/// <param name="e">A CellFocusEventArgs that contains the event data</param>
		void OnGotFocus(I3CellFocusEventArgs e);


		/// <summary>
		/// Raises the LostFocus event
		/// </summary>
		/// <param name="e">A CellFocusEventArgs that contains the event data</param>
		void OnLostFocus(I3CellFocusEventArgs e);


		/// <summary>
		/// Raises the KeyDown event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		void OnKeyDown(I3CellKeyEventArgs e);


		/// <summary>
		/// Raises the KeyUp event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		void OnKeyUp(I3CellKeyEventArgs e);


		/// <summary>
		/// Raises the MouseEnter event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseEnter(I3CellMouseEventArgs e);


		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseLeave(I3CellMouseEventArgs e);


		/// <summary>
		/// Raises the MouseUp event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseUp(I3CellMouseEventArgs e);


		/// <summary>
		/// Raises the MouseDown event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseDown(I3CellMouseEventArgs e);


		/// <summary>
		/// Raises the MouseMove event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseMove(I3CellMouseEventArgs e);


		/// <summary>
		/// Raises the Click event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnClick(I3CellMouseEventArgs e);


		/// <summary>
		/// Raises the DoubleClick event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnDoubleClick(I3CellMouseEventArgs e);
	}
}
