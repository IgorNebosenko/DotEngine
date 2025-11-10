namespace DotEngine;

public class GameObject
{
    public bool IsActive { get; private set; }
    
    public Component GetComponent(Type type)
    {
        throw new NotImplementedException();
    }

    public T[] GetComponentsInParent<T>(bool includeInactive) where T : Component
    {
        throw new NotImplementedException();
    }
}