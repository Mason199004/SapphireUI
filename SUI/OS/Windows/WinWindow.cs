using System.ComponentModel;
using System.Runtime.InteropServices;
using SUI.Controls;
namespace SUI.OS.Windows;



public class WinWindow : IOSWindow
{
    public string Title { get; set; } = String.Empty;
    public int Dpi { get; set; }
    public IOSMenubar Menubar { get; set; } = new WinMenubar();
    public ISControl MainControl { get; set; }

    

    public WinWindow()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            throw new PlatformNotSupportedException("Attempted to Create WinWindow on a non Windows platform!");
        }
    }

    public void Init(string Title)
    {
        Init(Title, null);
    }

    
    public void Init(string Title, IOSMenubar? Menubar)
    {
        this.Title = Title;
        if (Menubar is not null)
        {
            if (Menubar is not WinMenubar)
            {
                throw new ArgumentException("A non Windows Menubar was used to initialize a Windows Window!");
            }
            this.Menubar = Menubar;
        }

        
        
    }
    
    internal IntPtr hWnd; //figure out
    public bool Spawn()
    {
        
        return true;
    }
}