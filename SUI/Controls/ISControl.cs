using SUI.Core.Painting;
using SUI.Core.Types;

namespace SUI.Controls;

public interface ISControl
{
    public string Name { get; init; }
    public ScalingType ScalingType { get; init; }
    
    public Size Size { get; set; }

    /// <summary>
    /// Initializes the control
    /// </summary>
    public void Init();
    
    /// <summary>
    /// Updates the control
    /// </summary>
    /// <returns>whether the control needs to be repainted</returns>
    public bool Refresh();
    
    /// <summary>
    /// "Paints" the control by creating an IPainting
    /// </summary>
    /// <returns>Painted control as IPainting</returns>
    public IPainting Paint(Size ControlSize);
}