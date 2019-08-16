

using System;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

using IE310.Table.Events;
using IE310.Table.Win32;


namespace IE310.Table.Editors
{
	/// <summary>
    /// �����Ϣ������
	/// A message filter that filters mouse messages
	/// </summary>
	internal class I3MouseMessageFilter : IMessageFilter
	{
		/// <summary>
        /// client����һ��CellEditor����ʵ�ʵ�����ProcessMouseMessage()�����������Ϣ���й���
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
        /// ������Ϣ
		/// Filters out a message before it is dispatched
		/// </summary>
		/// <param name="m">The message to be dispatched. You cannot modify 
		/// this message</param>
		/// <returns>true to filter the message and prevent it from being 
		/// dispatched; false to allow the message to continue to the next 
		/// filter or control</returns>
		public bool PreFilterMessage(ref Message m)
		{
            //ȷ����һ��client
			// make sure we have a client
			if (this.Client == null)
			{
				return false;
			}

            //ȷ����Ϣ��һ�������Ϣ
			// make sure the message is a mouse message
			if ((m.Msg >= (int) I3WindowMessage.WM_MOUSEMOVE && m.Msg <= (int) I3WindowMessage.WM_XBUTTONDBLCLK) || 
				(m.Msg >= (int) I3WindowMessage.WM_NCMOUSEMOVE && m.Msg <= (int) I3WindowMessage.WM_NCXBUTTONUP))
			{
                //���Ի�ȡĿ��ؼ�
				// try to get the target control
				UIPermission uiPermission = new UIPermission(UIPermissionWindow.AllWindows);
				uiPermission.Demand();
				Control target = Control.FromChildHandle(m.HWnd);

                //ʵ���������Ϣ�Ĺ��ˣ�����client��ʵ�ֵ�
				return this.Client.ProcessMouseMessage(target, (I3WindowMessage) m.Msg, m.WParam.ToInt32(), m.LParam.ToInt32());
			}
				
            //���������Ϣ������false����Ϣ���Լ����ɷ�
			return false;
		}
	}
}
