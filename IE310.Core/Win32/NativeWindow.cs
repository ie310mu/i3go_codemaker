using System;
using System.Runtime.InteropServices;


namespace IE310.Core.Win32
{
	/// <summary>
    /// 钩子父类
	/// Summary description for NativeWindow
	/// </summary>
    public class I3NativeWindow 
	{
		#region Class Data
		
		/// <summary>
        /// 窗体的句柄，应用于DropDownCellEditor的ActivationListener时，记录的是下拉容器的Owner Form的Handle
		/// </summary>
		private IntPtr handle;

		/// <summary>
        /// 新的WndProc函数，实际上即是本类的WndProc函数
		/// Prevents the delegate being collected
		/// </summary>
		private I3WndProcDelegate wndProcDelegate;

		/// <summary>
		/// 记录了窗体的原始WndProc函数地址
		/// </summary>
		private IntPtr oldWndFunc;

		/// <summary>
        /// WndProc函数委托定义
		/// </summary>
		private delegate IntPtr I3WndProcDelegate(IntPtr hwnd, int Msg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// 
		/// </summary>
		private const int GWL_WNDPROC = -4;

		#endregion
		

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the NativeWindow class
		/// </summary>
		public I3NativeWindow()
		{
			wndProcDelegate = new I3WndProcDelegate(this.WndProc);
		}

		#endregion


		#region Methods

		/// <summary>
        /// 给句柄赋与新的WndProc并记录老的WndProc
		/// Assigns a handle to this window
		/// </summary>
		/// <param name="hWnd">The handle to assign to this window</param>
		public void AssignHandle(IntPtr hWnd)
		{
			handle = hWnd;
			oldWndFunc = SetWindowLong(hWnd, GWL_WNDPROC, wndProcDelegate);
		}


		/// <summary>
        /// 恢复老的WndProc
		/// Releases the handle associated with this window
		/// </summary>
		public void ReleaseHandle()
		{
			SetWindowLong(handle, GWL_WNDPROC, oldWndFunc);
			handle = IntPtr.Zero;
			oldWndFunc = IntPtr.Zero;
		}


		/// <summary>
		/// Invokes the default window procedure associated with this window
		/// </summary>
		/// <param name="msg">A Message that is associated with the current Windows message</param>
		protected virtual void WndProc(ref System.Windows.Forms.Message msg)
		{
			DefWndProc(ref msg);
		}


		/// <summary>
		/// Invokes the default window procedure associated with this window. 
		/// It is an error to call this method when the Handle property is 0
		/// </summary>
		/// <param name="m">A Message that is associated with the current Windows message</param>
		public void DefWndProc(ref System.Windows.Forms.Message m)
		{
			m.Result = CallWindowProc(oldWndFunc, m.HWnd, m.Msg, m.WParam, m.LParam);
		}


		/// <summary>
        /// 用于给Handle赋与新的WndProc函数，实际执行WndProc(Message msg)虚拟函数
		/// Handler for the WndProcDelegate
		/// </summary>
		/// <param name="hWnd">Handle to the window procedure to receive the message</param>
		/// <param name="msg">Specifies the message</param>
		/// <param name="wParam">Specifies additional message-specific information. The contents 
		/// of this parameter depend on the value of the Msg parameter</param>
		/// <param name="lParam">Specifies additional message-specific information. The contents 
		/// of this parameter depend on the value of the Msg parameter</param>
		/// <returns>The return value specifies the result of the message processing and depends 
		/// on the message sent</returns>
		private IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
		{
			System.Windows.Forms.Message m = System.Windows.Forms.Message.Create(hWnd, msg, wParam, lParam);
			WndProc(ref m);
			return m.Result;
		}


		/// <summary>
		/// The SetWindowLong function changes an attribute of the specified window. The 
		/// function also sets the 32-bit (long) value at the specified offset into the 
		/// extra window memory
		/// </summary>
		/// <param name="hWnd">Handle to the window and, indirectly, the class to which 
		/// the window belongs</param>
		/// <param name="nIndex">Specifies the zero-based offset to the value to be set.</param>
		/// <param name="wndProcDelegate">Specifies the replacement value</param>
		/// <returns>If the function succeeds, the return value is the previous value of 
		/// the specified 32-bit integer. If the function fails, the return value is zero</returns>
		[DllImport("User32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, I3WndProcDelegate wndProcDelegate);


		/// <summary>
		/// The SetWindowLong function changes an attribute of the specified window. The 
		/// function also sets the 32-bit (long) value at the specified offset into the 
		/// extra window memory
		/// </summary>
		/// <param name="hWnd">Handle to the window and, indirectly, the class to which 
		/// the window belongs</param>
		/// <param name="nIndex">Specifies the zero-based offset to the value to be set.</param>
		/// <param name="wndFunc">Specifies the replacement value</param>
		/// <returns>If the function succeeds, the return value is the previous value of 
		/// the specified 32-bit integer. If the function fails, the return value is zero</returns>
		[DllImport("User32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr wndFunc);


		/// <summary>
		/// The CallWindowProc function passes message information to the specified window 
		/// procedure
		/// </summary>
		/// <param name="prevWndFunc">Pointer to the previous window procedure. If this value 
		/// is obtained by calling the GetWindowLong function with the nIndex parameter set to 
		/// GWL_WNDPROC or DWL_DLGPROC, it is actually either the address of a window or dialog 
		/// box procedure, or a special internal value meaningful only to CallWindowProc</param>
		/// <param name="hWnd">Handle to the window procedure to receive the message</param>
		/// <param name="iMsg">Specifies the message</param>
		/// <param name="wParam">Specifies additional message-specific information. The contents 
		/// of this parameter depend on the value of the Msg parameter</param>
		/// <param name="lParam">Specifies additional message-specific information. The contents 
		/// of this parameter depend on the value of the Msg parameter</param>
		/// <returns>The return value specifies the result of the message processing and depends 
		/// on the message sent</returns>
		[DllImport("User32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern IntPtr CallWindowProc(IntPtr prevWndFunc, IntPtr hWnd, int iMsg, IntPtr wParam, IntPtr lParam);

		#endregion


		#region Properties

		/// <summary>
        /// 返回被钩的窗口的句柄
		/// Gets the handle for this window
		/// </summary>
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		#endregion
	}
}
