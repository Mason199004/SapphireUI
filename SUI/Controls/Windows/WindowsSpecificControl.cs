using System.Runtime.InteropServices;
using SUI.OS.Windows;

namespace SUI.Controls.Windows;

public abstract class WindowsSpecificControl : IPlatformSpecificControl
{
    public OSPlatform RequiredPlatform => OSPlatform.Windows;
    internal IntPtr hWnd;
    internal abstract Win32.WndProc wndProc { get; }
    
    protected WindowsSpecificControl()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            throw new PlatformNotSupportedException(
                "Attempted to create instance of a Windows only control on a non Windows platform!");
        }
    }

    protected abstract IntPtr HandleEvent(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
}