using SUI.Core.Painting;
using SUI.Core.Types;
using SUI.OS.Windows;

namespace SUI.Controls.Windows;

enum ButtonStyle : long
{
    //Type
    BS_PUSHBUTTON     =  0x00000000L,
    BS_DEFPUSHBUTTON  =  0x00000001L,
    BS_CHECKBOX       =  0x00000002L,
    BS_AUTOCHECKBOX   =  0x00000003L,
    BS_RADIOBUTTON    =  0x00000004L,
    BS_3STATE         =  0x00000005L,
    BS_AUTO3STATE     =  0x00000006L,
    BS_GROUPBOX       =  0x00000007L,
    BS_USERBUTTON     =  0x00000008L,
    BS_AUTORADIOBUTTON=  0x00000009L,
    BS_PUSHBOX        =  0x0000000AL,
    BS_OWNERDRAW      =  0x0000000BL,
    //Styles
    BS_TYPEMASK       =  0x0000000FL,
    BS_LEFTTEXT       =  0x00000020L,
    BS_TEXT           =  0x00000000L,
    BS_ICON           =  0x00000040L,
    BS_BITMAP         =  0x00000080L,
    BS_LEFT           =  0x00000100L,
    BS_RIGHT          =  0x00000200L,
    BS_CENTER         =  0x00000300L,
    BS_TOP            =  0x00000400L,
    BS_BOTTOM         =  0x00000800L,
    BS_VCENTER        =  0x00000C00L,
    BS_PUSHLIKE       =  0x00001000L,
    BS_MULTILINE      =  0x00002000L,
    BS_NOTIFY         =  0x00004000L,
    BS_FLAT           =  0x00008000L,
    BS_RIGHTBUTTON    =  BS_LEFTTEXT,
}

enum ButtonState
{
    BST_UNCHECKED      = 0x0000,
    BST_CHECKED        = 0x0001,
    BST_INDETERMINATE  = 0x0002,
    BST_PUSHED         = 0x0004,
    BST_FOCUS          = 0x0008,
}

public abstract class WindowsButtonControl : WindowsSpecificControl, ISButtonControl
{
    public string Name { get; init; }
    public ScalingType ScalingType { get; init; }
    public Size Size { get; set; }
    internal ButtonStyle Style { get; }
    internal override Win32.WndProc wndProc => HandleEvent;

    /// <summary>
    /// Child Buttons should set the members and then call this parent function in their Init function
    /// </summary>
    public virtual void Init()
    {
        
    }
    public abstract bool Refresh();
    public abstract IPainting Paint(Size ControlSize);

    public event Action? Clicked;

    /// <summary>
    /// Handles various events for a window.
    /// </summary>
    /// <param name="hWnd">The handle to the window.</param>
    /// <param name="msg">The message code for the event.</param>
    /// <param name="wParam">The wParam parameter for the event.</param>
    /// <param name="lParam">The lParam parameter for the event.</param>
    /// <returns>
    /// 
    /// </returns>
    protected override IntPtr HandleEvent(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        switch (msg) //TODO: wm_command or something
        {
            case Win32.WM_COMMAND:
                // Handle button click event here
                break;
            case Win32.WM_LBUTTONUP:
                // Handle button release event here
                break;
            case Win32.WM_LBUTTONDBLCLK:
                // Handle button double click event here
                break;
            case Win32.WM_PAINT:
                // Handle button paint event here
                break;
            case Win32.WM_DESTROY:
                // Handle window destroy event here
                break;
        }
        return Win32.DefWindowProc(hWnd, msg, wParam, lParam);
    }

    
}