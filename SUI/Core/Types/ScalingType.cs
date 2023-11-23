namespace SUI.Core.Types;

/// <summary>
/// Determines how a particular control should be scaled
/// </summary>
public enum ScalingType
{
    Parent, //Inherit scaling from parent
    Dpi, //Control should scale based on dpi, ideally control should remain same physical size on different monitors
    Container, //Control should scale relative to the size of its containing control
}