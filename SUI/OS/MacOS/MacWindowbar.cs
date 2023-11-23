namespace SUI.OS.MacOS;

public class MacWindowbar : IOSMenubar
{
    public bool Enabled { get; set; }
    public IEnumerable<IOSMenubarItem> Items { get; init; }
}