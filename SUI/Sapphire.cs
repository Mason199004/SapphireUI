using System.Runtime.InteropServices;
using SUI.OS;
using SUI.OS.Windows;

namespace SUI;

public static class Sapphire
{
    
    
    public static IOSWindow CreateWindow(string Title, IOSMenubar Menubar = null)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var dpi = Windows.SetProcessDPIAware();
            return Windows.CreateWindow(Title, Menubar);
        }

        throw new PlatformNotSupportedException("Implement support for not Windows");
    }

    private static class Windows
    {
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