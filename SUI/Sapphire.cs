using System.Runtime.InteropServices;
using System.Security;
using SUI.OS;
using SUI.OS.Windows;

namespace SUI;

public static class Sapphire
{
    
    
    public static IOSWindow CreateWindow(string Title, IOSMenubar Menubar = null)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return Windows.CreateWindow(Title, Menubar);
        }

        throw new PlatformNotSupportedException("Implement support for not Windows");
    }

    internal static class Windows
    {
        internal unsafe struct _OSVERSIONINFOW {
            public uint dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            public fixed ushort szCSDVersion[128];
        }

        internal static _OSVERSIONINFOW verinf;
        [DllImport("Ntdll")]
        static extern int RtlGetVersion(ref _OSVERSIONINFOW versionInfo);
        
        enum PROCESS_DPI_AWARENESS {
            PROCESS_DPI_UNAWARE = 0,
            PROCESS_SYSTEM_DPI_AWARE = 1,
            PROCESS_PER_MONITOR_DPI_AWARE = 2
        };
        
        [DllImport("Shcore")]
        static extern int SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);
        
        public static void SInit()
        {
            verinf = new _OSVERSIONINFOW();
            unsafe
            {
                verinf.dwOSVersionInfoSize = (uint)sizeof(_OSVERSIONINFOW);
            }
            RtlGetVersion(ref verinf);
            bool is7OrGreater = false;
            if (verinf.dwMajorVersion > 6) //Win10+
            {
                is7OrGreater = true;
            }
            if (verinf.dwMajorVersion == 6 && verinf.dwMinorVersion >= 1) //Win7 or Win8
            {
                is7OrGreater = true;
            }

            if (!is7OrGreater)
            {
                throw new PlatformNotSupportedException("Application requires Windows 7 or greater!");
            }

            bool is81OrGreater = false;
            if (verinf.dwMajorVersion > 6) //Win10+
            {
                is81OrGreater = true;
            }
            if (verinf.dwMajorVersion == 6 && verinf.dwMinorVersion >= 3) //Win8.1
            {
                is81OrGreater = true;
            }

            if (is81OrGreater) //use per-monitor dpi awareness on Windows 8.1+
            {
                SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
            }
            else
            {
                SetProcessDPIAware();
            }

        }
        [DllImport(@"user32")] 
        public static extern bool SetProcessDPIAware();
        public static WinWindow CreateWindow(string Title, IOSMenubar Menubar = null)
        {
            WinWindow window = new WinWindow();
            window.Init(Title, Menubar);
            return window;
        }
    }

    private static class Mac
    {
        
    }
    
    private static class Nix
    {
        
    }
}