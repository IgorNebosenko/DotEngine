namespace DotEngine;

public class Component : Object
{
    public Transform Transform { get; private set; }
    public GameObject GameObject { get; private set; }
    
    public Component GetComponent(System.Type type) => GameObject.GetComponent(type);

    public T GetComponent<T>()
    {
        throw new NotImplementedException();
    }

    public bool TryGetComponent<T>(System.Type type, out Component component)
    {
        throw new NotImplementedException();
    }

    public bool TryGetComponent<T>(out T component)
    {
        throw new NotImplementedException();
    }

    public Component GetComponentInChildren(System.Type type, bool includeInactive = false)
    {
        throw new NotImplementedException();
    }

    public T GetComponentInChildren<T>(bool includeInactive = false)
    {
        throw new NotImplementedException();
    }

    public Component[] GetComponentsInChildren(System.Type type, bool includeInactive = false)
    {
        throw new NotImplementedException();
    }
    
    public Component[] GetComponentsInChildren(System.Type type)
    {
        return GetComponentsInChildren(type, false);
    }

    public T[] GetComponentsInChildren<T>(bool includeInactive)
    {
        throw new NotImplementedException();
    }

    public void GetComponentsInChildren<T>(bool includeInactive, List<T> result)
    {
        throw new NotImplementedException();
    }

    public T[] GetComponentsInChildren<T>() => GetComponentsInChildren<T>(false);
    
    public void GetComponentsInChildren<T>(List<T> results) => GetComponentsInChildren<T>(false, results);
    
    
}