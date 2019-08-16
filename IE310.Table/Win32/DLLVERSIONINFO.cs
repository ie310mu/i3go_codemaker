using System;
using System.Runtime.InteropServices;


namespace IE310.Table.Win32
{
	/// <summary>
	/// Receives dynamic-link library (DLL)-specific version information. 
	/// It is used with the DllGetVersion function
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
    public struct I3DLLVERSIONINFO 
	{
		/// <summary>
		/// Size of the structure, in bytes. This member must be filled 
		/// in before calling the function
		/// </summary>
		public int cbSize;

		/// <summary>
		/// Major version of the DLL. If the DLL's version is 4.0.950, 
		/// this value will be 4
		/// </summary>
		public int dwMajorVersion;

		/// <summary>
		/// Minor version of the DLL. If the DLL's version is 4.0.950, 
		/// this value will be 0
		/// </summary>
		public int dwMinorVersion;

		/// <summary>
		/// Build number of the DLL. If the DLL's version is 4.0.950, 
		/// this value will be 950
		/// </summary>
		public int dwBuildNumber;

		/// <summary>
		/// Identifies the platform for which the DLL was built
		/// </summary>
		public int dwPlatformID;
	}
}
