namespace SUI.OS.Linux;

public class NixMenubar : IOSMenubar
{
    public bool Enabled { get; set; }
    public IEnumerable<IOSMenubarItem> Items { get; init; }
}