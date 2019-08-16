using System;
using System.Runtime.InteropServices;


namespace IE310.Table.Win32
{
    /// <summary>
    /// 消息结构体定义
    /// </summary>
	[StructLayout(LayoutKind.Sequential)]
    public struct I3MSG 
	{
		public IntPtr hwnd;
		public uint message;
		public IntPtr wParam;
		public IntPtr lParam;
		public uint time;
		public I3POINT pt;
	}
}
