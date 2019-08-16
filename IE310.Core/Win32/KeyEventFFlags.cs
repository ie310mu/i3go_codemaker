using System;


namespace IE310.Core.Win32
{
	/// <summary>
	/// Specifies the flags used with the keybd_event function
	/// </summary>
    public enum I3KeyEventFFlags
	{
		/// <summary>
		/// If specified, the scan code was preceded by a prefix byte having the value 0xE0 (224)
		/// </summary>
		KEYEVENTF_EXTENDEDKEY = 0x0001,
		
		/// <summary>
		/// If specified, the key is being released. If not specified, the key is being depressed
		/// </summary>
		KEYEVENTF_KEYUP = 0x0002
	}
}
