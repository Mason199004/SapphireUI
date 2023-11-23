namespace SUI.Core.Painting;

public class ComplexPainting : IPainting
{
    public IEnumerable<IPaintObject> GetObjects()
    {
        throw new NotImplementedException();
    }

    public void Join(IPainting painting)
    {
        
    }
}