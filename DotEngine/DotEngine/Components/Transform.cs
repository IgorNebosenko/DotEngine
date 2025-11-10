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

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}