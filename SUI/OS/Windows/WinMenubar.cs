namespace SUI.OS.Windows;

public class WinMenubar : IOSMenubar
{
    public bool Enabled { get; set; }
    public IEnumerable<IOSMenubarItem> Items { get; init; } = new List<WinMenubarItem>();
}