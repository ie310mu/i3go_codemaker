using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;


namespace IE310.Table.Win32
{
	/// <summary>
	/// The TRACKMOUSEEVENT structure is used by the TrackMouseEvent function 
	/// to track when the mouse pointer leaves a window or hovers over a window 
	/// for a specified amount of time
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
    public class I3TRACKMOUSEEVENT
	{
		/// <summary>
		/// Specifies the size of the TRACKMOUSEEVENT structure
		/// </summary>
		public int cbSize;

		/// <summary>
		/// Specifies the services requested
		/// </summary>
		public int dwFlags;

		/// <summary>
		/// Specifies a handle to the window to track
		/// </summary>
		public IntPtr hwndTrack;

		/// <summary>
		/// Specifies the hover time-out in milliseconds
		/// </summary>
		public int dwHoverTime;


		/// <summary>
		/// Creates a new TRACKMOUSEEVENT struct with default settings
		/// </summary>
		public I3TRACKMOUSEEVENT()
		{
			// Marshal.SizeOf() uses SecurityAction.LinkDemand to prevent 
			// it from being called from untrusted code, so make sure we 
			// have permission to call it
			SecurityPermission permission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
			permission.Demand();

			this.cbSize = Marshal.SizeOf(typeof(I3TRACKMOUSEEVENT));
			
			this.dwFlags = 0;
			this.hwndTrack = IntPtr.Zero;
			this.dwHoverTime = 100;
		}
	}
}
