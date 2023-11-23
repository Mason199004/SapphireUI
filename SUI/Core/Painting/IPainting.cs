namespace SUI.Core.Painting; 

/// <summary>
/// Platform specific object representing a rendered object
/// </summary>
public interface IPainting
{
    public IEnumerable<IPaintObject> GetObjects();
}