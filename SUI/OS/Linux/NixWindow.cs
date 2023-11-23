using SUI.Controls;

namespace SUI.OS.Linux;

public class NixWindow : IOSWindow
{
    public string Title { get; set; } = string.Empty;
    public int Dpi { get; set; }
    public IOSMenubar Menubar { get; set; }
    public ISControl MainControl { get; set; }

    public void Init(string Title)
    {
        throw new NotImplementedException();
    }

    public void Init(string Title, IOSMenubar Menubar = null)
    {
        throw new NotImplementedException();
    }

    public bool Spawn()
    {
        throw new NotImplementedException();
    }
}