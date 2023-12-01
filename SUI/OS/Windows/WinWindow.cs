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

    private Win32.WndProc delegWndProc = HandleEvent;

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

    Win32.WNDCLASSEX wind_class = new Win32.WNDCLASSEX();
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

        if (WinClass == 0)
        {
            wind_class.cbSize = Marshal.SizeOf(typeof(Win32.WNDCLASSEX));
            wind_class.style = (int)(Win32.CS_HREDRAW | Win32.CS_VREDRAW | Win32.CS_DBLCLKS ) ; //Doubleclicks are active
            wind_class.hbrBackground = (IntPtr) Win32.COLOR_BACKGROUND  +1 ; //Black background, +1 is necessary
            wind_class.cbClsExtra = 0;
            wind_class.cbWndExtra = 0;
            wind_class.hInstance = Marshal.GetHINSTANCE(this.GetType().Module); ;// alternative: Process.GetCurrentProcess().Handle;
            wind_class.hIcon = IntPtr.Zero;
            wind_class.hCursor = Win32.LoadCursor(IntPtr.Zero, (int)Win32.IDC_CROSS);// Crosshair cursor;
            wind_class.lpszMenuName = null;
            wind_class.lpszClassName = "myClass";
            wind_class.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(delegWndProc);
            wind_class.hIconSm = IntPtr.Zero;
            WinClass = Win32.RegisterClassEx(ref wind_class);
            
            if (WinClass == 0)
            {
                uint error = Win32.GetLastError();
                throw new Win32Exception((int)error);
            }
            
            string wndClass = wind_class.lpszClassName;
            
            Dpi = 96; //default to 96 until after we het the hWnd
            
            int width = 9 * Dpi; //replace
            int height = 6 * Dpi;//
            hWnd = Win32.CreateWindowEx(0, WinClass, Title, Win32.WS_OVERLAPPEDWINDOW | Win32.WS_VISIBLE, 0, 0, width, height, IntPtr.Zero, IntPtr.Zero, wind_class.hInstance, IntPtr.Zero);
            Dpi = PlatformHelper.GetDpi(this);
        }
        
    }

    private static ushort WinClass = 0;
    internal IntPtr hWnd;
    public bool Spawn()
    {
        if (hWnd == ((IntPtr)0))
        {
            uint error = Win32.GetLastError();
            return false;
        }
        Win32.ShowWindow(hWnd, 1);
        Win32.UpdateWindow(hWnd);
        return true;
    }

    private static IntPtr HandleEvent(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        switch (msg)
        {
            
        }
        return Win32.DefWindowProc(hWnd, msg, wParam, lParam);
    }
}