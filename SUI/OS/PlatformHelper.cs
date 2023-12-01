using System.Runtime.InteropServices;
using SUI.OS.Windows;

namespace SUI.OS;

public static class PlatformHelper
{
    public static T PSelect<T>(T Win, T Mac, T Nix)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return Win;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return Mac;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return Nix;
        }
        else
        {
            throw new PlatformNotSupportedException("Unable to complete Platform Select: Unknown platform!");
        }
    }

    public static int GetDpi(IOSWindow window)
    {
        return PSelect(Windows.GetDpi, ((ref IOSWindow osWindow) => { return 0;}), (ref IOSWindow w) => { return 0;}).Invoke(ref window);
    }

    static class Windows
    {
        private static float GetScalingFactor()
        {
            IntPtr desktop = Win32.GetDC(0);
        
            int LogicalScreenHeight = Win32.GetDeviceCaps(desktop, (int)Win32.DeviceCap.VERTRES);
            int PhysicalScreenHeight = Win32.GetDeviceCaps(desktop, (int)Win32.DeviceCap.DESKTOPVERTRES); 
    
            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

            Win32.ReleaseDC(0, desktop);
            return ScreenScalingFactor; 
        }
        
        public static int GetDpi(ref IOSWindow window)
        {
            var winWindow = (WinWindow)window;
            if (Sapphire.Windows.verinf.dwMajorVersion >= 10)
            {
                return (int)Win32.GetDpiForWindow(winWindow.hWnd);
            }
            else
            {
                IntPtr dc = Win32.GetDC(0);
                var Dpi = (int)(Win32.GetDeviceCaps(dc, (int)Win32.DeviceCap.LOGPIXELSX) * GetScalingFactor());
                Win32.ReleaseDC(0, dc);
                return Dpi;
            }
        }
    }
}