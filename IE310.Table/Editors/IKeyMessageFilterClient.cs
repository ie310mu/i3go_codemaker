

using System;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Win32;


namespace IE310.Table.Editors
{
	/// <summary>
	/// Indicates that an object is interested in receiving key messages 
	/// before they are sent to their destination
	/// </summary>
	public interface II3KeyMessageFilterClient 
	{
		/// <summary>
		/// Filters out a key message before it is dispatched
		/// </summary>
		/// <param name="target">The Control that will receive the message</param>
		/// <param name="msg">A WindowMessage that represents the message to process</param>
		/// <param name="wParam">Specifies the WParam field of the message</param>
		/// <param name="lParam">Specifies the LParam field of the message</param>
		/// <returns>true to filter the message and prevent it from being dispatched; 
		/// false to allow the message to continue to the next filter or control</returns>
		bool ProcessKeyMessage(Control target, I3WindowMessage msg, int wParam, int lParam);
	}
}
