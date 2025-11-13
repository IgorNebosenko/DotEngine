namespace DotEngine;

public struct Scene
{
    private int _handle;
    private int _buildIndex;
    private bool _isDirty;
    public int _rootCount;

    private List<GameObject> _rootObjects;

    public Scene(int handle, int buildIndex, bool isDirty, int rootCount, List<GameObject> rootObjects)
    {
        _handle = handle;
        _buildIndex = buildIndex;
        _isDirty = isDirty;
        _rootCount = rootCount;
        _rootObjects = rootObjects;
    }

    public int Handle => _handle;
    public int BuildIndex => _buildIndex;
    public bool IsDirty => _isDirty;
    public int RootCount => _rootCount;
    
    public string Name { get; set; }
    public string Path { get; set; }
    public bool IsSubScene { get; set; }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public GameObject[] GetRootGameObjects()
    {
        throw new NotImplementedException();
    }

    public void GetRootGameObjects(List<GameObject> gameObjects)
    {
        throw new NotImplementedException();
    }
    
    public static bool operator ==(Scene lhs, Scene rhs) => lhs._handle == rhs._handle;

    public static bool operator !=(Scene lhs, Scene rhs) => lhs._handle != rhs._handle;
    
    public override bool Equals(object other) => other is Scene scene && this._handle == scene._handle;

}