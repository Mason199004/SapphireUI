using System.ComponentModel;
using System.Runtime.InteropServices;
using SUI.Controls;

namespace SUI.OS.Windows;



public class WinWindow : IOSWindow
{
    #region Win32
    
    delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    
    const UInt32 WS_OVERLAPPEDWINDOW = 0xcf0000;
    const UInt32 WS_VISIBLE = 0x10000000;
    const UInt32 CS_USEDEFAULT = 0x80000000;
    const UInt32 CS_DBLCLKS = 8;
    const UInt32 CS_VREDRAW = 1;
    const UInt32 CS_HREDRAW = 2;
    const UInt32 COLOR_WINDOW = 5;
    const UInt32 COLOR_BACKGROUND = 1;
    const UInt32 IDC_CROSS = 32515;
    const UInt32 WM_DESTROY = 2;
    const UInt32 WM_PAINT = 0x0f;
    const UInt32 WM_LBUTTONUP = 0x0202;
    const UInt32 WM_LBUTTONDBLCLK = 0x0203;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    struct WNDCLASSEX
    {
        [MarshalAs(UnmanagedType.U4)]
        public int cbSize;
        [MarshalAs(UnmanagedType.U4)]
        public int style;
        public IntPtr lpfnWndProc; 
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        public string lpszMenuName;
        public string lpszClassName;
        public IntPtr hIconSm;
    }


    private WndProc delegWndProc = HandleEvent;

    [DllImport("user32.dll")]
    static extern bool UpdateWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern bool DestroyWindow(IntPtr hWnd);


    [DllImport("user32.dll", SetLastError = true, EntryPoint = "CreateWindowEx")]
    public static extern IntPtr CreateWindowEx(
       int dwExStyle,
       UInt16 regResult,
       //string lpClassName,
       string lpWindowName,
       UInt32 dwStyle,
       int x,
       int y,
       int nWidth,
       int nHeight,
       IntPtr hWndParent,
       IntPtr hMenu,
       IntPtr hInstance,
       IntPtr lpParam);

    [DllImport("user32.dll", SetLastError = true, EntryPoint = "RegisterClassEx")]
    static extern System.UInt16 RegisterClassEx([In] ref WNDCLASSEX lpWndClass);

    [DllImport("kernel32.dll")]
    static extern uint GetLastError();

    [DllImport("user32.dll")]
    static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    static extern void PostQuitMessage(int nExitCode);

    //[DllImport("user32.dll")]
    //static extern sbyte GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin,
    //   uint wMsgFilterMax);

    [DllImport("user32.dll")]
    static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);
    
    [DllImport("user32.dll", SetLastError=true)]
    static extern IntPtr GetDC(IntPtr hWnd);
    
    [DllImport("user32.dll", SetLastError=true)]
    static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr HDC);
    
    [DllImport("gdi32.dll")]
    static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
    
    #region DeviceCap
    public enum DeviceCap
    {
        /// <summary>
        /// Device driver version
        /// </summary>
        DRIVERVERSION = 0,
        /// <summary>
        /// Device classification
        /// </summary>
        TECHNOLOGY = 2,
        /// <summary>
        /// Horizontal size in millimeters
        /// </summary>
        HORZSIZE = 4,
        /// <summary>
        /// Vertical size in millimeters
        /// </summary>
        VERTSIZE = 6,
        /// <summary>
        /// Horizontal width in pixels
        /// </summary>
        HORZRES = 8,
        /// <summary>
        /// Vertical height in pixels
        /// </summary>
        VERTRES = 10,
        /// <summary>
        /// Number of bits per pixel
        /// </summary>
        BITSPIXEL = 12,
        /// <summary>
        /// Number of planes
        /// </summary>
        PLANES = 14,
        /// <summary>
        /// Number of brushes the device has
        /// </summary>
        NUMBRUSHES = 16,
        /// <summary>
        /// Number of pens the device has
        /// </summary>
        NUMPENS = 18,
        /// <summary>
        /// Number of markers the device has
        /// </summary>
        NUMMARKERS = 20,
        /// <summary>
        /// Number of fonts the device has
        /// </summary>
        NUMFONTS = 22,
        /// <summary>
        /// Number of colors the device supports
        /// </summary>
        NUMCOLORS = 24,
        /// <summary>
        /// Size required for device descriptor
        /// </summary>
        PDEVICESIZE = 26,
        /// <summary>
        /// Curve capabilities
        /// </summary>
        CURVECAPS = 28,
        /// <summary>
        /// Line capabilities
        /// </summary>
        LINECAPS = 30,
        /// <summary>
        /// Polygonal capabilities
        /// </summary>
        POLYGONALCAPS = 32,
        /// <summary>
        /// Text capabilities
        /// </summary>
        TEXTCAPS = 34,
        /// <summary>
        /// Clipping capabilities
        /// </summary>
        CLIPCAPS = 36,
        /// <summary>
        /// Bitblt capabilities
        /// </summary>
        RASTERCAPS = 38,
        /// <summary>
        /// Length of the X leg
        /// </summary>
        ASPECTX = 40,
        /// <summary>
        /// Length of the Y leg
        /// </summary>
        ASPECTY = 42,
        /// <summary>
        /// Length of the hypotenuse
        /// </summary>
        ASPECTXY = 44,
        /// <summary>
        /// Shading and Blending caps
        /// </summary>
        SHADEBLENDCAPS = 45,

        /// <summary>
        /// Logical pixels inch in X
        /// </summary>
        LOGPIXELSX = 88,
        /// <summary>
        /// Logical pixels inch in Y
        /// </summary>
        LOGPIXELSY = 90,

        /// <summary>
        /// Number of entries in physical palette
        /// </summary>
        SIZEPALETTE = 104,
        /// <summary>
        /// Number of reserved entries in palette
        /// </summary>
        NUMRESERVED = 106,
        /// <summary>
        /// Actual color resolution
        /// </summary>
        COLORRES = 108,

        // Printing related DeviceCaps. These replace the appropriate Escapes
        /// <summary>
        /// Physical Width in device units
        /// </summary>
        PHYSICALWIDTH = 110,
        /// <summary>
        /// Physical Height in device units
        /// </summary>
        PHYSICALHEIGHT = 111,
        /// <summary>
        /// Physical Printable Area x margin
        /// </summary>
        PHYSICALOFFSETX = 112,
        /// <summary>
        /// Physical Printable Area y margin
        /// </summary>
        PHYSICALOFFSETY = 113,
        /// <summary>
        /// Scaling factor x
        /// </summary>
        SCALINGFACTORX = 114,
        /// <summary>
        /// Scaling factor y
        /// </summary>
        SCALINGFACTORY = 115,

        /// <summary>
        /// Current vertical refresh rate of the display device (for displays only) in Hz
        /// </summary>
        VREFRESH = 116,
        /// <summary>
        /// Vertical height of entire desktop in pixels
        /// </summary>
        DESKTOPVERTRES = 117,
        /// <summary>
        /// Horizontal width of entire desktop in pixels
        /// </summary>
        DESKTOPHORZRES = 118,
        /// <summary>
        /// Preferred blt alignment
        /// </summary>
        BLTALIGNMENT = 119
    }
    #endregion
    
    #endregion
    
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

    WNDCLASSEX wind_class = new WNDCLASSEX();
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
            wind_class.cbSize = Marshal.SizeOf(typeof(WNDCLASSEX));
            wind_class.style = (int)(CS_HREDRAW | CS_VREDRAW | CS_DBLCLKS ) ; //Doubleclicks are active
            wind_class.hbrBackground = (IntPtr) COLOR_BACKGROUND  +1 ; //Black background, +1 is necessary
            wind_class.cbClsExtra = 0;
            wind_class.cbWndExtra = 0;
            wind_class.hInstance = Marshal.GetHINSTANCE(this.GetType().Module); ;// alternative: Process.GetCurrentProcess().Handle;
            wind_class.hIcon = IntPtr.Zero;
            wind_class.hCursor = LoadCursor(IntPtr.Zero, (int)IDC_CROSS);// Crosshair cursor;
            wind_class.lpszMenuName = null;
            wind_class.lpszClassName = "myClass";
            wind_class.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(delegWndProc);
            wind_class.hIconSm = IntPtr.Zero;
            WinClass = RegisterClassEx(ref wind_class);
            
            if (WinClass == 0)
            {
                uint error = GetLastError();
                throw new Win32Exception((int)error);
            }
            
            string wndClass = wind_class.lpszClassName;

            IntPtr dc = GetDC(0);
            Dpi = (int)(GetDeviceCaps(dc, (int)DeviceCap.LOGPIXELSX) * GetScalingFactor());
            ReleaseDC(0, dc);
            int width = 9 * Dpi;
            int height = 6 * Dpi;
            hWnd = CreateWindowEx(0, WinClass, Title, WS_OVERLAPPEDWINDOW | WS_VISIBLE, 0, 0, width, height, IntPtr.Zero, IntPtr.Zero, wind_class.hInstance, IntPtr.Zero);
            
        }
        
    }
    
    private float GetScalingFactor()
    {
        IntPtr desktop = GetDC(0);
        
        int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
        int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES); 
    
        float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

        ReleaseDC(0, desktop);
        return ScreenScalingFactor; // 1.25 = 125%
    }


    private static ushort WinClass = 0;
    private IntPtr hWnd;
    public bool Spawn()
    {
        if (hWnd == ((IntPtr)0))
        {
            uint error = GetLastError();
            return false;
        }
        ShowWindow(hWnd, 1);
        UpdateWindow(hWnd);
        return true;
    }

    private static IntPtr HandleEvent(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        switch (msg)
        {
            
        }
        return DefWindowProc(hWnd, msg, wParam, lParam);
    }
}