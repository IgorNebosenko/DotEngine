using System.Collections;

namespace DotEngine;

public class Transform : Component, IEnumerable
{
    private Vector3 _localPosition;
    private Vector3 _localEulerAngles;

    public event Action<Vector3> LocalPositionChanged;
    public event Action<Vector3> GlobalPositionChanged;
    
    public event Action<Vector3> LocalEulerAnglesChanged;
    public event Action<Vector3> GlobalEulerAnglesChanged;
    
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

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}