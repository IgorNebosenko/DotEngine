namespace DotEngine;

using System;
using System.Collections.Generic;

public sealed class GameObject : Object
{
    #region Fields
    private int _layer;
    private bool _isActive = true;
    private bool _isStatic;
    private bool _isStaticBatchable;
    private string _tag = string.Empty;

    private GameObject? _parent;
    private List<GameObject> _childs = new List<GameObject>();
    private List<Component> _components = new List<Component>();
    #endregion

    public GameObject(string name = "GameObject", Transform parent = null, params Type[] componentTypes)
    {
        Name = name;

        Transform = parent == null
            ? Transform.CreateRoot(this)
            : Transform.CreateChild(parent, this);

        if (Transform.Parent != null)
            _parent = Transform.Parent.GameObject;

        _parent?._childs.Add(this);

        for (var i = 0; i < componentTypes.Length; i++)
            AddComponent(componentTypes[i]);
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

    public ulong SceneCullingMask => 0;

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
        SetActive(active);

        for (var i = 0; i < _childs.Count; i++)
            _childs[i].SetActiveRecursively(active);
    }
    #endregion

    public bool CompareTag(string tag) => _tag == tag;

    #region Get Component
    public T? GetComponent<T>()
    {
        for (var i = 0; i < _components.Count; i++)
        {
            if (_components[i] is T t)
                return t;
        }

        return default;
    }

    public Component? GetComponent(Type type)
    {
        for (var i = 0; i < _components.Count; i++)
        {
            if (type.IsAssignableFrom(_components[i].GetType()))
                return _components[i];
        }

        return null;
    }
    #endregion

    #region Get Component In Children
    public T? GetComponentInChildren<T>(bool includeInactive = false)
    {
        var result = GetComponent<T>();
        if (result != null)
            return result;

        for (var i = 0; i < _childs.Count; i++)
        {
            var child = _childs[i];
            if (!includeInactive && !child._isActive)
                continue;

            var r = child.GetComponentInChildren<T>(includeInactive);
            if (r != null)
                return r;
        }

        return default;
    }

    public Component? GetComponentInChildren(Type type, bool includeInactive = false)
    {
        var result = GetComponent(type);
        if (result != null)
            return result;

        for (var i = 0; i < _childs.Count; i++)
        {
            var child = _childs[i];
            if (!includeInactive && !child._isActive)
                continue;

            var r = child.GetComponentInChildren(type, includeInactive);
            if (r != null)
                return r;
        }

        return null;
    }
    #endregion

    #region Get Compont In Parent
    public Component? GetComponentInParent(Type type, bool includeInactive = false)
    {
        var p = _parent;

        while (p != null)
        {
            if (includeInactive || p._isActive)
            {
                var c = p.GetComponent(type);
                if (c != null)
                    return c;
            }

            p = p._parent;
        }

        return null;
    }

    public T? GetComponentInParent<T>() where T : Component
    {
        var p = _parent;

        while (p != null)
        {
            var c = p.GetComponent<T>();
            if (c != null)
                return c;

            p = p._parent;
        }

        return default;
    }
    #endregion

    #region Get Components
    public T[] GetComponentsInParent<T>(bool includeInactive) where T : Component
    {
        var list = new List<T>();
        var p = _parent;

        while (p != null)
        {
            if (includeInactive || p._isActive)
            {
                for (var i = 0; i < p._components.Count; i++)
                {
                    if (p._components[i] is T t)
                        list.Add(t);
                }
            }

            p = p._parent;
        }

        return list.ToArray();
    }

    public Component[] GetComponents(Type type)
    {
        var list = new List<Component>();

        for (var i = 0; i < _components.Count; i++)
        {
            if (type.IsAssignableFrom(_components[i].GetType()))
                list.Add(_components[i]);
        }

        return list.ToArray();
    }

    public T[] GetComponents<T>() where T : Component
    {
        var list = new List<T>();

        for (var i = 0; i < _components.Count; i++)
        {
            if (_components[i] is T t)
                list.Add(t);
        }

        return list.ToArray();
    }

    public void GetComponents(Type type, List<Component> results)
    {
        for (var i = 0; i < _components.Count; i++)
        {
            if (type.IsAssignableFrom(_components[i].GetType()))
                results.Add(_components[i]);
        }
    }

    public void GetComponents<T>(List<T> results) where T : Component
    {
        for (var i = 0; i < _components.Count; i++)
        {
            if (_components[i] is T t)
                results.Add(t);
        }
    }
    #endregion

    #region Get Components In Children
    public Component[]? GetComponentsInChildren(Type type)
    {
        var list = new List<Component>();

        for (var i = 0; i < _childs.Count; i++)
            list.AddRange(_childs[i].GetComponents(type));

        return list.Count == 0 ? null : list.ToArray();
    }

    public Component[]? GetComponentsInChildren(Type type, bool includeInactive)
    {
        var list = new List<Component>();

        for (var i = 0; i < _childs.Count; i++)
        {
            var child = _childs[i];
            if (!includeInactive && !child._isActive)
                continue;

            list.AddRange(child.GetComponents(type));
        }

        return list.Count == 0 ? null : list.ToArray();
    }

    public T[]? GetComponentsInChildren<T>(bool includeInactive) where T : Component
    {
        var list = new List<T>();

        for (var i = 0; i < _childs.Count; i++)
        {
            var child = _childs[i];
            if (!includeInactive && !child._isActive)
                continue;

            list.AddRange(child.GetComponents<T>());
        }

        return list.Count == 0 ? null : list.ToArray();
    }

    public void GetComponentsInChildren<T>(bool includeInactive, List<T> results) where T : Component
    {
        for (var i = 0; i < _childs.Count; i++)
        {
            var child = _childs[i];
            if (!includeInactive && !child._isActive)
                continue;

            results.AddRange(child.GetComponents<T>());
        }
    }
    #endregion

    #region Get Components In Parent
    public Component[]? GetComponentsInParent(Type type)
    {
        var list = new List<Component>();
        var p = _parent;

        while (p != null)
        {
            list.AddRange(p.GetComponents(type));
            p = p._parent;
        }

        return list.Count == 0 ? null : list.ToArray();
    }

    public void GetComponentsInParent<T>(bool includeInactive, List<T> results) where T : Component
    {
        var p = _parent;

        while (p != null)
        {
            if (includeInactive || p._isActive)
                results.AddRange(p.GetComponents<T>());

            p = p._parent;
        }
    }

    public T[]? GetComponentsInParent<T>() where T : Component
    {
        var list = new List<T>();
        var p = _parent;

        while (p != null)
        {
            list.AddRange(p.GetComponents<T>());
            p = p._parent;
        }

        return list.Count == 0 ? null : list.ToArray();
    }
    #endregion

    #region Try Get Component
    public bool TryGetComponent<T>(out T? component)
    {
        component = GetComponent<T>();
        return component != null;
    }

    public bool TryGetComponent(Type type, out Component? component)
    {
        component = GetComponent(type);
        return component != null;
    }
    #endregion

    #region Add Component
    public Component? AddComponent(Type type)
    {
        if (!typeof(Component).IsAssignableFrom(type))
            return null;

        var component = (Component)Activator.CreateInstance(type);
        component.GameObject = this;
        _components.Add(component);
        return component;
    }

    public T AddComponent<T>() where T : Component, new()
    {
        var component = new T();
        component.GameObject = this;
        _components.Add(component);
        return component;
    }
    #endregion

    public int GetComponentCount()
    {
        return _components.Count;
    }

    public Component? GetComponentAtIndex(int index)
    {
        if (index < 0 || index >= _components.Count)
            return null;

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
                return i;
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
                resultList.Add(rootGameObjects[i]);
        }

        return resultList.ToArray();
    }

    public static GameObject? Find(Scene scene, string name)
    {
        var rootGameObjects = scene.GetRootGameObjects();

        for (var i = 0; i < rootGameObjects.Length; i++)
        {
            if (rootGameObjects[i].Name == name)
                return rootGameObjects[i];
        }

        return null;
    }

    public static void SetGameObjectsActive(ReadOnlySpan<int> instanceIDs, bool active)
    {
        throw new NotImplementedException();
    }
}
