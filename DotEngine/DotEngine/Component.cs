namespace DotEngine;

public class Component : Object
{
    public Transform Transform { get; private set; }
    public GameObject GameObject { get; private set; }
    public bool Enabled { get; private set; }

    public event Action<bool> EnableStatusChanged; 
    
    public Component GetComponent(System.Type type) => GameObject.GetComponent(type);

    #region TryGetComponent
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
    #endregion

    #region GetComponentInChildren
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
    #endregion
    
    #region GetComponentInParent

    public Component GetComponentInParent(System.Type type, bool includeInactive)
    {
        throw new NotImplementedException();
    }
    
    public Component GetComponentInParent(System.Type type) => GetComponentInParent(type, false);

    public T GetComponentInParent<T>(bool includeInactive) where T : Component
    {
        return (T) GetComponentInParent(typeof (T), includeInactive);
    }

    public T GetComponentInParent<T>() where T : Component
    {
        return (T)GetComponentInParent(typeof(T), false);
    }
    #endregion
    
    #region GetComponentsInParent

    public Component[] GetComponentsInParent(System.Type type, bool includeInactive)
    {
        throw new NotImplementedException();
    }
    
    public Component[] GetComponentsInParent(System.Type t) => GetComponentsInParent(t, false);

    public T[] GetComponentsInParent<T>(bool includeInactive) where T : Component
    {
        return GameObject.GetComponentsInParent<T>(includeInactive);
    }
    
    public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
    {
        throw new NotImplementedException();
    }

    public T[] GetComponentsInParent<T>()
    {
        throw new NotImplementedException();
    }

    public Component[] GetComponents(System.Type type)
    {
        throw new NotImplementedException();
    }

    public void GetComponents(System.Type type, List<Component> results)
    {
        throw new NotImplementedException();
    }

    public void GetComponents<T>(List<T> results)
    {
        throw new NotImplementedException();
    }

    public T[] GetComponents<T>()
    {
        throw new NotImplementedException();
    }

    #endregion

    public int GetComponentIndex()
    {
        throw new NotImplementedException();
    }

    public bool CompareTag(string tag)
    {
        throw new NotImplementedException();
    }

    internal bool IsCoupledComponent()
    {
        throw new NotImplementedException();
    }
    
    public void SetEnableStatus(bool enable)
    {
        Enabled = enable;
        EnableStatusChanged?.Invoke(enable);
    }
}