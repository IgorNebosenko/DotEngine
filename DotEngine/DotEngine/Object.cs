using System.Runtime.InteropServices;

namespace DotEngine;

[StructLayout(LayoutKind.Sequential)]
public class Object : IEquatable<Object>
{
    public HideFlags HideFlags { get; private set; }
    public string Name { get; set; }

    public int GetInstanceId()
    {
        throw new NotImplementedException();
    }

    public override string ToString() => Name;

    public static void Destroy(float lifeTime = 0f)
    {
        throw new NotImplementedException();
    }
    
    public static void DestroyImmediate()
    {
        throw new NotImplementedException();
    }
    
    public static void DontDestroyOnLoad()
    {
        throw new NotImplementedException();
    }

    #region Find Object By Type
    public T FindAnyObjectByType<T>() where T : Object
    {
        throw new NotImplementedException();
    }

    public Object FindAnyObjectByType<T>(Type type)
    {
        throw new NotImplementedException();
    }

    public T FindAnyObjectByType<T>(FindObjectsInactive findObjectsInactive) where T : Object
    {
        throw new NotImplementedException();
    }
    
    public Object FindAnyObjectByType<T>(Type type, FindObjectsInactive findObjectsInactive)
    {
        throw new NotImplementedException();
    }
    #endregion
    
    #region Find First Object By Type
    public T FindFirstObjectByType<T>() where T : Object
    {
        throw new NotImplementedException();
    }

    public Object FindFirstObjectByType<T>(Type type)
    {
        throw new NotImplementedException();
    }

    public T FindFirstObjectByType<T>(FindObjectsInactive findObjectsInactive) where T : Object
    {
        throw new NotImplementedException();
    }
    
    public Object FindFirstObjectByType<T>(Type type, FindObjectsInactive findObjectsInactive)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Find Objects By Type
    public T[] FindObjectsByType<T>() where T : Object
    {
        throw new NotImplementedException();
    }

    public Object[] FindObjectsByType<T>(Type type)
    {
        throw new NotImplementedException();
    }

    public T[] FindObjectsByType<T>(FindObjectsInactive findObjectsInactive) where T : Object
    {
        throw new NotImplementedException();
    }
    
    public Object[] FindObjectsByType<T>(Type type, FindObjectsInactive findObjectsInactive)
    {
        throw new NotImplementedException();
    }
    #endregion
    
    #region Instantiate object
    public static Object Instantiate(Object template)
    {
        throw new NotImplementedException();
    }
    
    public static Object Instantiate(Object template, Transform parent)
    {
        throw new NotImplementedException();
    }

    public static Object Instantiate(Object template, Transform parent, bool instantiateInWorldSpace)
    {
        throw new NotImplementedException();
    }

    public static Object Instantiate(Object template, Vector3 position, Quaternion rotation)
    {
        throw new NotImplementedException();
    }

    public static Object Instantiate(Object template, Transform parent, Vector3 position, Quaternion rotation)
    {
        throw new NotImplementedException();
    }
    #endregion
    
    #region Instantiate generic
    public static T Instantiate<T>(T template) where T : Object
    {
        throw new NotImplementedException();
    }

    public static void Instantiate<T>(T template, Transform parent) where T : Object
    {
        throw new NotImplementedException();
    }

    public static T Instantiate<T>(T template, Transform parent, bool instantiateInWorldSpace) where T : Object
    {
        throw new NotImplementedException();
    }

    public static T Instantiate<T>(T template, Vector3 position, Quaternion rotation) where T : Object
    {
        throw new NotImplementedException();
    }

    public static T Instantiate<T>(T template, Transform parent, Vector3 position, Quaternion rotation)  where T : Object
    {
        throw new NotImplementedException();
    }
    #endregion
    
    //ToDo InstantiateAsync
    
    public static implicit operator bool(Object? value)
    {
        return value != null;
    }
    
    public static bool operator !=(Object objMain, Object obj)
    {
        return !objMain.Equals(obj);
    }

    public static bool operator ==(Object objMain, Object obj)
    {
        return objMain.Equals(obj);
    }

    public bool Equals(Object? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return HideFlags == other.HideFlags && Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Object)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)HideFlags, Name);
    }
}