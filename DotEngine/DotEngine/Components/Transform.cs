using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace DotEngine;

public class Transform : Component, IEnumerable
{
    private Vector3 _localPosition;
    private Vector3 _localEulerAngles;
    private Vector3 _localScale;
    
    private Transform _parent;
    private Transform _root;

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

    public Transform Root => _root;
    
    public int ChildCount => throw new NotImplementedException();
    public bool HasChanged => throw new NotImplementedException();

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

    public void Rotate(Vector3 axis, float angle, Space relativeTo = Space.Self)
    {
        throw new NotImplementedException();
    }

    public void RotateAround(Vector3 point, Vector3 axis, float angle)
    {
        throw new NotImplementedException();
    }

    public void LookAt(Transform target, Vector3 worldUp)
    {
        throw new NotImplementedException();
    }

    public void LookAt(Transform target)
    {
        throw new NotImplementedException();
    }

    public void LookAt(Vector3 target, Vector3 worldUp)
    {
        throw new NotImplementedException();
    }

    public void LookAt(Vector3 target)
    {
        throw new NotImplementedException();
    }

    public Vector3 TransformDirection(Vector3 direction)
    {
        throw new NotImplementedException();
    }

    public Vector3 TransformDirection(float x, float y, float z)
    {
        return TransformDirection(new Vector3(x, y, z));
    }

    public void TransformDirections(
        ReadOnlySpan<Vector3> directions,
        Span<Vector3> transformedDirections)
    {
        throw new NotImplementedException();
    }

    public void TransformDirections(Span<Vector3> directions)
    {
        throw new NotImplementedException();
    }

    public Vector3 InverseTransformDirection(Vector3 direction)
    {
        throw new NotImplementedException();
    }

    public Vector3 InverseTransformDirection(float x, float y, float z)
    {
        return InverseTransformDirection(new Vector3(x, y, z));
    }

    public void InverseTransformDirections(
        ReadOnlySpan<Vector3> directions,
        Span<Vector3> transformedDirections)
    {
        throw new NotImplementedException();
    }

    public void InverseTransformDirections(Span<Vector3> directions)
    {
        throw new NotImplementedException();
    }

    public Vector3 TransformVector(Vector3 vector)
    {
        throw new NotImplementedException();
    }

    public Vector3 TransformVector(float x, float y, float z)
    {
        return TransformVector(new Vector3(x, y, z));
    }

    public void TransformVectors(ReadOnlySpan<Vector3> vectors, Span<Vector3> transformedVectors)
    {
        throw new NotImplementedException();
    }

    public void TransformVectors(Span<Vector3> vectors)
    {
        throw new NotImplementedException();
    }

    public Vector3 InverseTransformVector(Vector3 vector)
    {
        throw new NotImplementedException();
    }

    public Vector3 InverseTransformVector(float x, float y, float z)
    {
        return InverseTransformVector(new Vector3(x, y, z));
    }

    public void InverseTransformVectors(
        ReadOnlySpan<Vector3> vectors,
        Span<Vector3> transformedVectors)
    {
        throw new NotImplementedException();
    }

    public void InverseTransformVectors(Span<Vector3> vectors)
    {
        throw new NotImplementedException();
    }

    public Vector3 TransformPoint(Vector3 position)
    {
        throw new NotImplementedException();
    }

    public Vector3 TransformPoint(float x, float y, float z)
    {
        return TransformPoint(new  Vector3(x, y, z));
    }

    public void TransformPoints(ReadOnlySpan<Vector3> positions, Span<Vector3> transformedPositions)
    {
        throw new NotImplementedException();
    }

    public void TransformPoints(Span<Vector3> positions)
    {
        throw new NotImplementedException();
    }

    public Vector3 InverseTransformPoint(Vector3 position)
    {
        throw new NotImplementedException();
    }

    public Vector3 InverseTransformPoint(float x, float y, float z)
    {
        return InverseTransformPoint(new Vector3(x, y, z));
    }

    public void InverseTransformPoints(
        ReadOnlySpan<Vector3> positions,
        Span<Vector3> transformedPositions)
    {
        throw new NotImplementedException();
    }

    public void InverseTransformPoints(Span<Vector3> positions)
    {
        throw new NotImplementedException();
    }

    public void DetachChildren()
    {
        throw new NotImplementedException();
    }

    public void SetAsFirstSibling()
    {
        throw new NotImplementedException();
    }

    public void SetAsLastSibling()
    {
        throw new NotImplementedException();
    }

    public int GetSiblingIndex()
    {
        throw new NotImplementedException();
    }

    public Transform Find(string name)
    {
        throw new NotImplementedException();
    }

    public void SetSiblingIndex(int index)
    {
        throw new NotImplementedException();
    }

    public bool IsChildOf([NotNull] Transform parent)
    {
        throw new NotImplementedException();
    }

    public Transform FindChild(string name) => Find(name);
    
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}