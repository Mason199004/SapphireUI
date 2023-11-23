using System.Diagnostics.CodeAnalysis;
using SUI.Controls;

namespace SUI.OS;

/// <summary>
/// OS Window
/// </summary>
public interface IOSWindow
{
    public string Title { get; set; }
    public int Dpi { get; set; }
    public IOSMenubar Menubar { get; set; }
    public ISControl MainControl { get; set; }

    public void Init(string Title);
    public void Init(string Title, IOSMenubar Menubar);

    public bool Spawn();
}