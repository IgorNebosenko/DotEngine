using System.Runtime.InteropServices;

namespace DotEngine;

[StructLayout(LayoutKind.Sequential)]
public class Object
{
    public HideFlags HideFlags { get; private set; }
    public string Name { get; set; }

    public int GetInstanceId()
    {
        throw new NotImplementedException();
    }

    public override string ToString() => Name;

    public static void Destroy()
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

    public static void Instantiate(Object template)
    {
        throw new NotImplementedException();
    }

    public static void Instantiate(Object template, Transform parent)
    {
        throw new NotImplementedException();
    }

    public static void Instantiate(Object template, Transform parent, bool instantiateInWorldSpace)
    {
        throw new NotImplementedException();
    }

    public static void Instantiate(Object template, Vector3 position, Quaternion rotation)
    {
        throw new NotImplementedException();
    }

    public static void Instantiate(Object template, Transform parent, Vector3 position, Quaternion rotation)
    {
        throw new NotImplementedException();
    }
}