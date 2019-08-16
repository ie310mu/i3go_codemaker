

using System;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Win32;


namespace IE310.Table.Editors
{
	/// <summary>
    /// 鼠标消息过滤器
	/// A message filter that filters mouse messages
	/// </summary>
	internal class I3MouseMessageFilter : IMessageFilter
	{
		/// <summary>
        /// client将是一个CellEditor，将实际调用其ProcessMouseMessage()方法对鼠标消息进行过滤
		/// An IMouseMessageFilterClient that wishes to receive mouse events
		/// </summary>
		private II3MouseMessageFilterClient client;
		
		
		/// <summary>
		/// Initializes a new instance of the CellEditor class with the 
		/// specified IMouseMessageFilterClient client
		/// </summary>
		public I3MouseMessageFilter(II3MouseMessageFilterClient client)
		{
			this.client = client;
		}


		/// <summary>
		/// Gets or sets the IMouseMessageFilterClient that wishes to 
		/// receive mouse events
		/// </summary>
		public II3MouseMessageFilterClient Client
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
        /// 过滤消息
		/// Filters out a message before it is dispatched
		/// </summary>
		/// <param name="m">The message to be dispatched. You cannot modify 
		/// this message</param>
		/// <returns>true to filter the message and prevent it from being 
		/// dispatched; false to allow the message to continue to the next 
		/// filter or control</returns>
		public bool PreFilterMessage(ref Message m)
		{
            //确保有一个client
			// make sure we have a client
			if (this.Client == null)
			{
				return false;
			}

            //确保消息是一个鼠标消息
			// make sure the message is a mouse message
			if ((m.Msg >= (int) I3WindowMessage.WM_MOUSEMOVE && m.Msg <= (int) I3WindowMessage.WM_XBUTTONDBLCLK) || 
				(m.Msg >= (int) I3WindowMessage.WM_NCMOUSEMOVE && m.Msg <= (int) I3WindowMessage.WM_NCXBUTTONUP))
			{
                //尝试获取目标控件
				// try to get the target control
				UIPermission uiPermission = new UIPermission(UIPermissionWindow.AllWindows);
				uiPermission.Demand();
				Control target = Control.FromChildHandle(m.HWnd);

                //实际上鼠标消息的过滤，是由client来实现的
				return this.Client.ProcessMouseMessage(target, (I3WindowMessage) m.Msg, m.WParam.ToInt32(), m.LParam.ToInt32());
			}
				
            //不是鼠标消息，返回false，消息可以继续派发
			return false;
		}
	}
}
