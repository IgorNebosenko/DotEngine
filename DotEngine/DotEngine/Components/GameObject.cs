namespace DotEngine;

public sealed class GameObject : Object
{
    #region Fields
    private int _layer;
    private bool _isActive;
    private bool _isStatic;
    private bool _isStaticBatchable;

    private string _tag;
    
    private GameObject _parent;
    private List<GameObject> _childs = new List<GameObject>();
    private List<Component> _components = new List<Component>();
    #endregion

    public GameObject(string name = "GameObject", Transform parent = null, params Type[] componentTypes)
    {
        Name = name;
        Transform = parent == null ? Transform.CreateRoot() : Transform.CreateChild(parent);

        for (var i = 0; i < componentTypes.Length; i++)
        {
            AddComponent(componentTypes[i].GetType());
        }
    }

    #region Events
    public event Action<int> LayerChanged;
    public event Action<bool> ActiveChanged;
    public event Action<bool> StaticChanged;
    public event Action<string> TagChanged; 
    #endregion
    
    #region Properties
    public bool IsActive => _isActive;
    
    public Transform Transform { get; private set; }

    public int Layer
    {
        get => _layer;
        set
        {
            _layer = value;
            LayerChanged?.Invoke(value);
        }
    }

    public bool IsStatic
    {
        get => _isStatic;
        set
        {
            _isStatic = value;
            StaticChanged?.Invoke(value);
        }
    }
    
    public bool IsStaticBatchable => _isStaticBatchable;

    public string Tag
    {
        get => _tag;
        set
        {
            _tag = value;
            TagChanged?.Invoke(value);
        }
    }
    
    public ulong SceneCullingMask => throw new NotImplementedException();

    public GameObject GameObjectReference => this;
    #endregion
    
    #region Set active
    public void SetActive(bool active)
    {
        _isActive = active;
        ActiveChanged?.Invoke(active);
    }

    public void SetActiveRecursively(bool active)
    {
        _isActive = active;

        var parent = _parent;
        while (parent != null)
        {
            parent._isActive = active;
            parent = parent._parent;
        }
    }
    #endregion
    
    public bool CompareTag(string tag) => _tag == tag;

    #region Get Component
    public T? GetComponent<T>()
    {
        return _components.OfType<T>().FirstOrDefault();
    }

    public Component? GetComponent(Type type)
    {
        return _components.OfType<Component>().FirstOrDefault(c => c.GetType() == type);
    }
    #endregion

    #region Get Component In Children
    public T? GetComponentInChildren<T>(bool includeInactive = false)
    {
        for (var i = 0; i < _components.Count; i++)
        {
            if (!includeInactive && _components[i].Enabled)
            {
                var result = _components[i].GetComponent<T>();
                if (result != null)
                    return result;
            }
        }

        return default;
    }
    
    public Component? GetComponentInChildren(Type type, bool includeInactive = false)
    {
        for (var i = 0; i < _components.Count; i++)
        {
            if (!includeInactive && _components[i].Enabled)
            {
                var result = _components[i].GetComponent(type);
                if (result != null)
                    return result;
            }
        }

        return default;
    }
    #endregion

    #region Get Compont In Parent
    public Component? GetComponentInParent(Type type, bool includeInactive = false)
    {
        if (_parent == null)
            return null;
        
        var components = _parent.GetComponents(type);
        
        if (components == null || components.Length == 0)
            return null;

        if (includeInactive)
            return components[0];

        return components.FirstOrDefault(x => x.Enabled);

    }

    public T? GetComponentInParent<T>() where T : Component
    {
        if (_parent == null)
            return default;
        
        var components = _parent.GetComponents<T>();
        
        if (components == null || components.Length == 0)
            return default;

        return components[0];
    }
    #endregion

    #region Get Components
    public T[] GetComponentsInParent<T>(bool includeInactive) where T : Component
    {
        if (_parent == null)
            return null;

        var components = new List<T>();
        for (var i = 0; i < _parent._components.Count; i++)
        {
            if (_parent._components[i].GetType() == typeof(T))
            {
                components.Add((T)_parent._components[i]);
            }
        }
        
        return components.ToArray();
    }

    public Component[] GetComponents(Type type)
    {
        var components = new List<Component>();
        
        for (var i = 0; i < _components.Count; i++)
        {
            if (_components[i].GetType() == type)
            {
                components.Add(_components[i]);
            }
        }
        
        return components.ToArray();
    }

    public T[] GetComponents<T>() where T : Component
    {
        var components = new List<T>();
        
        for (var i = 0; i < _components.Count; i++)
        {
            if (_components[i].GetType() == typeof(T))
            {
                components.Add((T)_components[i]);
            }
        }
        
        return components.ToArray();
    }

    public void GetComponents(Type type, List<Component> results)
    {
        for (var i = 0; i < _components.Count; i++)
        {
            if (_components[i].GetType() == type)
            {
                results.Add(_components[i]);
            }
        }
    }

    public void GetComponents<T>(List<T> results) where T : Component
    {
        for (var i = 0; i < _components.Count; i++)
        {
            if (_components[i].GetType() == typeof(T))
            {
                results.Add((T)_components[i]);
            }
        }
    }
    #endregion

    #region Get Components In Children
    public Component[] GetComponentsInChildren(Type type)
    {
        throw new NotImplementedException();
    }

    public Component[] GetComponentsInChildren(Type type, bool includeInactive)
    {
        throw new NotImplementedException();
    }

    public T[] GetComponentsInChildren<T>(bool includeInactive)
    {
        throw new NotImplementedException();
    }

    public void GetComponentsInChildren<T>(bool includeInactive, List<T> results)
    {
        throw new NotImplementedException();
    }
    
    public T[] GetComponentsInChildren<T>() => GetComponentsInChildren<T>(false);

    public void GetComponentsInChildren<T>(List<T> results)
    {
        throw new NotImplementedException();
    }
    #endregion

    public Component[] GetComponentsInParent(Type type)
    {
        throw new NotImplementedException();
    }

    public Component[] GetComponentsInParent<T>(bool includeInactive, List<T> results)
    {
        throw new NotImplementedException();
    }

    public T[] GetComponentsInParent<T>()
    {
        throw new NotImplementedException();
    }

    public bool TryGetComponent<T>(out T component)
    {
        throw new NotImplementedException();
    }

    public bool TryGetComponent(Type type, out Component component)
    {
        throw new NotImplementedException();
    }

    public Component AddComponent(Type type)
    {
        throw new NotImplementedException();
    }
    
    public T AddComponent<T>() where T : Component => AddComponent(typeof (T)) as T;

    public int GetComponentCount()
    {
        throw new NotImplementedException();
    }

    public Component GetComponentAtIndex(int index)
    {
        throw new NotImplementedException();
    }

    public T GetComponentAtIndex<T>(int index) where T : Component
    {
        throw new NotImplementedException();
    }

    public int GetComponentIndex(Component component)
    {
        throw new NotImplementedException();
    }

    public static GameObject CreatePrimitive(PrimitiveType primitiveType)
    {
        throw new NotImplementedException();
    }

    public static void FindGameObjectsWithTag(string tag, List<GameObject> results)
    {
        throw new NotImplementedException();
    }

    public static GameObject FindGameObjectWithTag(string tag)
    {
        throw new NotImplementedException();
    }
    
    public static GameObject FindWithTag(string tag) => FindGameObjectWithTag(tag);

    public static GameObject[] FindGameObjectsWithTag(string tag)
    {
        throw new NotImplementedException();
    }

    public static GameObject Find(string name)
    {
        throw new NotImplementedException();
    }

    public static void SetGameObjectsActive(ReadOnlySpan<int> instanceIDs, bool active)
    {
        throw new NotImplementedException();
    }

    public static Scene GetScene(int instanceID)
    {
        throw new NotImplementedException();
    }
}