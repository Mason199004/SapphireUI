namespace SUI.OS;
/// <summary>
/// Menubar to display at the top of OS window on Linux/Windows and top of screen on MacOS
/// </summary>
public interface IOSMenubar
{
    public bool Enabled { get; set; }
    IEnumerable<IOSMenubarItem> Items { get; init; }
}