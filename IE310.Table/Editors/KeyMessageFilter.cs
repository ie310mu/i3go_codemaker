

using System;
using System.Security.Permissions;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Win32;


namespace IE310.Table.Editors
{
	/// <summary>
    /// 键盘消息过滤器，与鼠标消息过滤器类似
	/// A message filter that filters key messages
	/// </summary>
	internal class I3KeyMessageFilter : IMessageFilter
	{
		/// <summary>
        /// client将是一个CellEditor，将实际调用其ProcessKeyMessage()方法对键盘消息进行过滤
		/// An IKeyMessageFilterClient that wishes to receive key events
		/// </summary>
		private II3KeyMessageFilterClient client;
		
		
		/// <summary>
        /// client将是一个CellEditor
		/// Initializes a new instance of the CellEditor class with the 
		/// specified IKeyMessageFilterClient client
		/// </summary>
		public I3KeyMessageFilter(II3KeyMessageFilterClient client)
		{
			this.client = client;
		}


		/// <summary>
		/// Gets or sets the IKeyMessageFilterClient that wishes to receive 
		/// key events
		/// </summary>
		public II3KeyMessageFilterClient Client
		{
			get
			{
				return this.client;
			}

			set
			{
				this.client = value;
			}
		}
			
			
		/// <summary>
		/// Filters out a message before it is dispatched
		/// </summary>
		/// <param name="m">The message to be dispatched. You cannot modify 
		/// this message</param>
		/// <returns>true to filter the message and prevent it from being 
		/// dispatched; false to allow the message to continue to the next 
		/// filter or control</returns>
		public bool PreFilterMessage(ref Message m)
		{
			// make sure we have a client
			if (this.Client == null)
			{
				return false;
			}
			
			// make sure the message is a key message
			if (m.Msg != (int) I3WindowMessage.WM_KEYDOWN && m.Msg != (int) I3WindowMessage.WM_SYSKEYDOWN && 
				m.Msg != (int) I3WindowMessage.WM_KEYUP && m.Msg != (int) I3WindowMessage.WM_SYSKEYUP)
			{
				return false;
			}

			// try to get the target control
			UIPermission uiPermission = new UIPermission(UIPermissionWindow.AllWindows);
			uiPermission.Demand();
			Control target = Control.FromChildHandle(m.HWnd);

            try
            {
                return this.Client.ProcessKeyMessage(target, (I3WindowMessage)m.Msg, m.WParam.ToInt32(), m.LParam.ToInt32());
            }
            catch
            {
                return true;
            }
		}
	}
}
