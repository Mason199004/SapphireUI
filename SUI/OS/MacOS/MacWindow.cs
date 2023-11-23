using SUI.Controls;

namespace SUI.OS.MacOS;

public class MacWindow : IOSWindow
{
    public string Title { get; set; }
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