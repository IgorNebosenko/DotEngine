using System.Collections;

namespace DotEngine;

public class Transform : Component, IEnumerable
{
    private Vector3 _localPosition;
    private Vector3 _localEulerAngles;
    private Vector3 _localScale;
    
    private Transform _parent;

    public event Action<Vector3> LocalPositionChanged;
    public event Action<Vector3> GlobalPositionChanged;
    
    public event Action<Vector3> LocalEulerAnglesChanged;
    public event Action<Vector3> GlobalEulerAnglesChanged;
    
    public event Action<Vector3> LocalScaleChanged;
    
    protected Transform()
    {
    }

    public Vector3 LocalPosition
    {
        get => _localPosition;
        set
        {
            _localPosition = value;
            LocalPositionChanged?.Invoke(value);
        }
    }

    //ToDo implement GlobalPosition
    public Vector3 GlobalPosition
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public Vector3 LocalEulerAngles
    {
        get => _localEulerAngles;
        set
        {
            _localEulerAngles = value;
            LocalEulerAnglesChanged?.Invoke(value);
        }
    }
    
    //ToDo implement GlobalEulerAngles
    public Vector3 GlobalEulerAngles
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public Vector3 LocalScale
    {
        get => _localScale;
        set
        {
            _localScale = value;
            LocalScaleChanged?.Invoke(value);
        }
    }

    public Vector3 Right
    {
        get => throw new NotImplementedException();
    }
    
    public Vector3 Up
    {
        get => throw new NotImplementedException();
    }

    public Vector3 Forward
    {
        get => throw new NotImplementedException();
    }

    public Quaternion Rotation
    {
        get => throw new NotImplementedException();
    }

    public Quaternion LocalRotation
    {
        get => throw new NotImplementedException();
    }

    public RotationOrder RotationOrder
    {
        get => throw new NotImplementedException();
    }

    public Transform Parent
    {
        get => _parent;
        set => _parent = value;
    }

    public Matrix4x4 WorldToLocalMatrix
    {
        get => throw new NotImplementedException();
    }

    public Matrix4x4 LocalToWorldMatrix
    {
        get => throw new NotImplementedException();
    }

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        throw new NotImplementedException();
    }

    public void SetLocalPositionAndRotation(Vector3 localPosition, Quaternion localRotation)
    {
        throw new NotImplementedException();
    }

    public void GetPositionAndRotation(out Vector3 position, out Quaternion rotation)
    {
        throw new NotImplementedException();
    }

    public void GetLocalPositionAndRotation(out Vector3 localPosition, out Quaternion localRotation)
    {
        throw new NotImplementedException();
    }

    public void Translate(Vector3 translation, Space relativeTo = Space.Self)
    {
        throw new NotImplementedException();
    }

    public void Translate(float x, float y, float z, Space relativeTo = Space.Self)
    {
        Translate(new Vector3(x, y, z), relativeTo);
    }

    public void Translate(Vector3 translation, Transform relateTo)
    {
        throw new NotImplementedException();
    }
    
    public void Translate(float x, float y, float z, Transform relativeTo)
    {
        Translate(new Vector3(x, y, z), relativeTo);
    }

    public void Rotate(Vector3 eulers, Space relativeTo = Space.Self)
    {
        throw new NotImplementedException();
    }

    public void Rotate(float x, float y, float z, Space relativeTo = Space.Self)
    {
        Rotate(new Vector3(x, y, z), relativeTo);
    }


    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}