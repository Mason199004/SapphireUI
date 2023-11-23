using SUI.Core.Painting;
using SUI.Core.Types;

namespace SUI.Controls;

public class SParentControl : ISControl
{
    public string Name { get; init; }
    public ScalingType ScalingType { get; init; }

    private List<ISControl> ChildControls;
    
    public SParentControl(string Name, ScalingType ScalingType)
    {
        this.Name = Name;
        this.ScalingType = ScalingType;
        ChildControls = new List<ISControl>();
    }
    
    public void Init()
    {
        
    }

    public bool Refresh()
    {
        return ChildControls.Any(c => c.Refresh());
    }

    public IPainting Paint(Size ControlSize)
    {
        var painting = new ComplexPainting();
        foreach (var control in ChildControls)
        {
            if (control.Refresh()) //TODO: Keep old paintings so that this actually works
            {
                painting.Join(control.Paint(ControlSize/*TODO: fix when can actually have child controls*/));
            }
        }

        return painting;
    }
}