namespace DotEngine;

public sealed class GameObject : Object
{
    #region Fields
    private int _layer;
    private bool _isActive;
    private bool _isStatic;
    private bool _isStaticBatchable;

    private string _tag;
    
    private GameObject? _parent;
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
    public Component[]? GetComponentsInChildren(Type type)
    {
        if (_childs.Count == 0)
            return null;

        var listResult = new List<Component>();

        for (var i = 0; i < _childs.Count; i++)
        {
            listResult.AddRange(_childs[i].GetComponents(type));
        }

        return listResult.ToArray();
    }

    public Component[]? GetComponentsInChildren(Type type, bool includeInactive)
    {
        if (_childs.Count == 0)
            return null;

        var listResult = new List<Component>();

        for (var i = 0; i < _childs.Count; i++)
        {
            var sublist = _childs[i].GetComponents(type).ToList();

            if (!includeInactive)
            {
                sublist.RemoveAll(x => !x.Enabled);
            }
            
            listResult.AddRange(sublist);
        }

        return listResult.ToArray();
    }

    public T[]? GetComponentsInChildren<T>(bool includeInactive) where T : Component
    {
        if (_childs.Count == 0)
            return null;

        var listResult = new List<T>();

        for (var i = 0; i < _childs.Count; i++)
        {
            var sublist = _childs[i].GetComponents<T>().ToList();

            if (!includeInactive)
            {
                sublist.RemoveAll(x => !x.Enabled);
            }
            
            listResult.AddRange(sublist);
        }

        return listResult.ToArray();
    }

    public void GetComponentsInChildren<T>(bool includeInactive, List<T> results) where T : Component
    {
        if (_childs.Count == 0)
            return;

        for (var i = 0; i < _childs.Count; i++)
        {
            var sublist = _childs[i].GetComponents<T>().ToList();

            if (!includeInactive)
            {
                sublist.RemoveAll(x => !x.Enabled);
            }
            
            results.AddRange(sublist);
        }
    }
    #endregion

    #region Get Components In Parent
    public Component[]? GetComponentsInParent(Type type)
    {
        if (_parent == null)
            return null;

        return _parent.GetComponents(type);
    }

    public void GetComponentsInParent<T>(bool includeInactive, List<T> results) where T : Component
    {
        if (_parent == null)
            return;

        results.AddRange(_parent.GetComponents<T>());
        results.RemoveAll(x => !x.Enabled);
    }

    public T[]? GetComponentsInParent<T>() where T : Component
    {
        if (_parent == null)
            return null;
        
        return _parent.GetComponents<T>().ToArray();
    }
    #endregion

    #region Try Get Component
    public bool TryGetComponent<T>(out T? component)
    {
        var result = GetComponent<T>();
        component = result;
        
        return result != null;
    }

    public bool TryGetComponent(Type type, out Component? component)
    {
        var result = GetComponent(type);
        component = result;
        
        return result != null;
    }
    #endregion

    #region Add Component
    public Component? AddComponent(Type type)
    {
    }
    
    public T? AddComponent<T>() where T : Component => AddComponent(typeof (T)) as T;
    #endregion
    
    public int GetComponentCount()
    {
        return _components.Count;
    }

    public Component? GetComponentAtIndex(int index)
    {
        if (index < 0 || index >= _components.Count)
        {
            return null;
        }
        
        return _components[index];
    }

    public T? GetComponentAtIndex<T>(int index) where T : Component
    {
        if (index < 0 || index >= _components.Count)
            return null;
        
        return _components[index] as T;
    }

    public int GetComponentIndex(Component component)
    {
        for (var i = 0; i < _components.Count; i++)
        {
            if (_components[i].GetType() == component.GetType())
            {
                return i;
            }
        }

        return -1;
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

    public static GameObject[] FindGameObjectsWithTag(Scene scene, string tag)
    {
        var resultList = new List<GameObject>();
        var rootGameObjects = scene.GetRootGameObjects();

        for (var i = 0; i < rootGameObjects.Length; i++)
        {
            if (rootGameObjects[i]._tag == tag)
            {
                resultList.Add(rootGameObjects[i]);
            }
        }
        
        return resultList.ToArray();
    }

    public static GameObject? Find(Scene scene, string name)
    {
        var rootGameObjects = scene.GetRootGameObjects();

        for (var i = 0; i < rootGameObjects.Length; i++)
        {
            if (rootGameObjects[i].Name == name)
            {
                return rootGameObjects[i];
            }
        }

        return null;
    }

    public static void SetGameObjectsActive(ReadOnlySpan<int> instanceIDs, bool active)
    {
        throw new NotImplementedException();
    }
}