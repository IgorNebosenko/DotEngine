namespace DotEngine;

public sealed class GameObject : Object
{
    private int _layer;
    private bool _isActive;
    private bool _isStatic;
    private bool _isStaticBatchable;

    private string _tag;
    
    private GameObject _parent;
    private List<GameObject> _childs = new List<GameObject>();
    private List<Component> _components = new List<Component>();

    public GameObject(string name = "GameObject", Transform parent = null, params System.Type[] componentTypes)
    {
        throw new NotImplementedException();
    }

    public event Action<int> LayerChanged;
    public event Action<bool> ActiveChanged;
    public event Action<bool> StaticChanged;
    public event Action<string> TagChanged; 

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
    
    public void SetActive(bool active)
    {
        _isActive = active;
        ActiveChanged?.Invoke(active);
    }

    public void SetActiveRecursively(bool active)
    {
        throw new NotImplementedException();
    }
    
    public bool CompareTag(string tag) => _tag == tag;

    public T GetComponent<T>()
    {
        throw new NotImplementedException();
    }

    public Component GetComponent(System.Type type)
    {
        throw new NotImplementedException();
    }

    public T GetComponentInChildren<T>(bool includeInactive)
    {
        throw new NotImplementedException();
    }
    
    public T GetComponentInChildren<T>() => this.GetComponentInChildren<T>(false);
    
    public Component GetComponentInChildren(System.Type type, bool includeInactive = false)
    {
        throw new NotImplementedException();
    }

    public Component GetComponentInParent(System.Type type, bool includeInactive)
    {
        throw new NotImplementedException();
    }

    public T GetComponentInParent<T>(bool includeInactive)
    {
        throw new NotImplementedException();
    }

    public T GetComponentInParent<T>() => this.GetComponentInParent<T>(false);

    public T[] GetComponentsInParent<T>(bool includeInactive) where T : Component
    {
        throw new NotImplementedException();
    }

    public Component[] GetComponents(System.Type type)
    {
        throw new NotImplementedException();
    }

    public T[] GetComponents<T>()
    {
        throw new NotImplementedException();
    }

    public void GetComponents(System.Type type, List<Component> results)
    {
        throw new NotImplementedException();
    }

    public void GetComponents<T>(List<T> results)
    {
    }

    public Component[] GetComponentsInChildren(System.Type type)
    {
        throw new NotImplementedException();
    }

    public Component[] GetComponentsInChildren(System.Type type, bool includeInactive)
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
    
    public T[] GetComponentsInChildren<T>() => this.GetComponentsInChildren<T>(false);

    public void GetComponentsInChildren<T>(List<T> results)
    {
        throw new NotImplementedException();
    }

    public Component[] GetComponentsInParent(System.Type type)
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

    public bool TryGetComponent(System.Type type, out Component component)
    {
        throw new NotImplementedException();
    }

    public Component AddComponent(System.Type type)
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
    
    public static GameObject FindWithTag(string tag) => GameObject.FindGameObjectWithTag(tag);

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