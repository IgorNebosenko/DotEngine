using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace DotEngine;

public class Transform : Component, IEnumerable
{
    private Vector3 _localPosition;
    private Vector3 _localEulerAngles;
    private Vector3 _localScale = new (1f, 1f, 1f);
    
    private Quaternion _localRotation = new (0f, 0f, 0f, 1f);

    private GameObject _gameObject;
    
    private Transform _parent;
    private Transform _root;

    private bool _hasChanged;

    public event Action<Vector3> LocalPositionChanged;
    public event Action<Vector3> GlobalPositionChanged;
    
    public event Action<Vector3> LocalEulerAnglesChanged;
    public event Action<Vector3> GlobalEulerAnglesChanged;
    
    public event Action<Vector3> LocalScaleChanged;
    
    protected Transform(GameObject gameObject)
    {
        _gameObject = gameObject;
        _root = this;
        _hasChanged = true;
    }

    internal static Transform CreateRoot(GameObject gameObject)
    {
        var transform = new Transform(gameObject);
        transform._root = transform;
        return transform;
    }

    internal static Transform CreateChild(Transform parent, GameObject gameObject)
    {
        var transform = new Transform(gameObject);
        transform._parent = parent;
        transform._root = parent._root ?? parent;
        return transform;
    }

    private void MarkChanged()
    {
        _hasChanged = true;
    }

    public Vector3 LocalPosition
    {
        get => _localPosition;
        set
        {
            _localPosition = value;
            MarkChanged();
            LocalPositionChanged?.Invoke(value);
            GlobalPositionChanged?.Invoke(GlobalPosition);
        }
    }

    public Vector3 GlobalPosition
    {
        get
        {
            if (_parent == null)
                return _localPosition;

            var parentScale = _parent.LocalScale;
            var scaled = new Vector3(
                _localPosition.x * parentScale.x,
                _localPosition.y * parentScale.y,
                _localPosition.z * parentScale.z);

            var rotated = RotateVector(_parent.Rotation, scaled);
            return _parent.GlobalPosition + rotated;
        }
        set
        {
            if (_parent == null)
            {
                LocalPosition = value;
                return;
            }

            var parentGlobal = _parent.GlobalPosition;
            var delta = value - parentGlobal;

            var parentInvRot = InverseQuaternion(_parent.Rotation);
            var local = RotateVector(parentInvRot, delta);

            var parentScale = _parent.LocalScale;
            if (MathF.Abs(parentScale.x) > 1e-6f) local.x /= parentScale.x; else local.x = 0f;
            if (MathF.Abs(parentScale.y) > 1e-6f) local.y /= parentScale.y; else local.y = 0f;
            if (MathF.Abs(parentScale.z) > 1e-6f) local.z /= parentScale.z; else local.z = 0f;

            LocalPosition = local;
        }
    }

    public Vector3 LocalEulerAngles
    {
        get => _localEulerAngles;
        set
        {
            _localEulerAngles = value;
            _localRotation = EulerToQuaternion(_localEulerAngles);
            _localRotation = NormalizeQuaternion(_localRotation);
            MarkChanged();
            LocalEulerAnglesChanged?.Invoke(_localEulerAngles);
            GlobalEulerAnglesChanged?.Invoke(GlobalEulerAngles);
        }
    }
    
    public Vector3 GlobalEulerAngles
    {
        get => QuaternionToEuler(Rotation);
        set => Rotation = EulerToQuaternion(value);
    }

    public Vector3 LocalScale
    {
        get => _localScale;
        set
        {
            _localScale = value;
            MarkChanged();
            LocalScaleChanged?.Invoke(value);
        }
    }

    public Vector3 Right
    {
        get
        {
            var dir = new Vector3(1f, 0f, 0f);
            return RotateVector(Rotation, dir);
        }
    }
    
    public Vector3 Up
    {
        get
        {
            var dir = new Vector3(0f, 1f, 0f);
            return RotateVector(Rotation, dir);
        }
    }

    public Vector3 Forward
    {
        get
        {
            var dir = new Vector3(0f, 0f, 1f);
            return RotateVector(Rotation, dir);
        }
    }

    public Quaternion Rotation
    {
        get
        {
            if (_parent == null)
                return _localRotation;

            return MultiplyQuaternion(_parent.Rotation, _localRotation);
        }
        set
        {
            if (_parent == null)
            {
                LocalRotation = value;
                return;
            }

            var parentInv = InverseQuaternion(_parent.Rotation);
            LocalRotation = MultiplyQuaternion(parentInv, value);
        }
    }

    public Quaternion LocalRotation
    {
        get => _localRotation;
        set
        {
            _localRotation = NormalizeQuaternion(value);
            _localEulerAngles = QuaternionToEuler(_localRotation);
            MarkChanged();
            LocalEulerAnglesChanged?.Invoke(_localEulerAngles);
            GlobalEulerAnglesChanged?.Invoke(GlobalEulerAngles);
        }
    }

    public RotationOrder RotationOrder
    {
        get => RotationOrder.OrderXYZ;
    }

    public Transform Parent
    {
        get => _parent;
        set
        {
            if (_parent == value)
                return;

            var globalPos = GlobalPosition;
            var globalRot = Rotation;

            _parent = value;
            _root = _parent == null ? this : _parent._root ?? _parent;

            GlobalPosition = globalPos;
            Rotation = globalRot;
        }
    }

    public Matrix4x4 WorldToLocalMatrix
    {
        get
        {
            var m = LocalToWorldMatrix;
            return Matrix4x4.Invert(m);
        }
    }

    public Matrix4x4 LocalToWorldMatrix
    {
        get
        {
            var right = RotateVector(_localRotation, new Vector3(_localScale.x, 0f, 0f));
            var up = RotateVector(_localRotation, new Vector3(0f, _localScale.y, 0f));
            var forward = RotateVector(_localRotation, new Vector3(0f, 0f, _localScale.z));

            var local = new Matrix4x4(
                right.x,    up.x,    forward.x, 0f,
                right.y,    up.y,    forward.y, 0f,
                right.z,    up.z,    forward.z, 0f,
                _localPosition.x, _localPosition.y, _localPosition.z, 1f);

            if (_parent == null)
                return local;

            return local * _parent.LocalToWorldMatrix;
        }
    }

    public Transform Root => _root;
    
    public int ChildCount => throw new NotImplementedException();
    public bool HasChanged => _hasChanged;

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        Rotation = rotation;
        GlobalPosition = position;
    }

    public void SetLocalPositionAndRotation(Vector3 localPosition, Quaternion localRotation)
    {
        LocalRotation = localRotation;
        LocalPosition = localPosition;
    }

    public void GetPositionAndRotation(out Vector3 position, out Quaternion rotation)
    {
        position = GlobalPosition;
        rotation = Rotation;
    }

    public void GetLocalPositionAndRotation(out Vector3 localPosition, out Quaternion localRotation)
    {
        localPosition = _localPosition;
        localRotation = _localRotation;
    }

    public void Translate(Vector3 translation, Space relativeTo = Space.Self)
    {
        if (relativeTo == Space.World)
        {
            GlobalPosition += translation;
            return;
        }

        var worldTranslation = TransformDirection(translation);
        GlobalPosition += worldTranslation;
    }

    public void Translate(float x, float y, float z, Space relativeTo = Space.Self)
    {
        Translate(new Vector3(x, y, z), relativeTo);
    }

    public void Translate(Vector3 translation, Transform relateTo)
    {
        var worldTranslation = relateTo.TransformDirection(translation);
        GlobalPosition += worldTranslation;
    }
    
    public void Translate(float x, float y, float z, Transform relativeTo)
    {
        Translate(new Vector3(x, y, z), relativeTo);
    }

    public void Rotate(Vector3 eulers, Space relativeTo = Space.Self)
    {
        var delta = EulerToQuaternion(eulers);

        if (relativeTo == Space.World)
        {
            var worldRot = Rotation;
            worldRot = MultiplyQuaternion(delta, worldRot);
            Rotation = worldRot;
            return;
        }

        _localRotation = MultiplyQuaternion(_localRotation, delta);
        _localRotation = NormalizeQuaternion(_localRotation);
        _localEulerAngles = QuaternionToEuler(_localRotation);
        MarkChanged();
        LocalEulerAnglesChanged?.Invoke(_localEulerAngles);
        GlobalEulerAnglesChanged?.Invoke(GlobalEulerAngles);
    }

    public void Rotate(float x, float y, float z, Space relativeTo = Space.Self)
    {
        Rotate(new Vector3(x, y, z), relativeTo);
    }

    public void Rotate(Vector3 axis, float angle, Space relativeTo = Space.Self)
    {
        var nAxis = NormalizeVector(axis);
        if (IsZeroVector(nAxis))
            return;

        var rad = angle * (MathF.PI / 180f);
        var half = rad * 0.5f;
        var s = MathF.Sin(half);
        var delta = new Quaternion(nAxis.x * s, nAxis.y * s, nAxis.z * s, MathF.Cos(half));

        if (relativeTo == Space.World)
        {
            var worldRot = Rotation;
            worldRot = MultiplyQuaternion(delta, worldRot);
            Rotation = worldRot;
            return;
        }

        _localRotation = MultiplyQuaternion(_localRotation, delta);
        _localRotation = NormalizeQuaternion(_localRotation);
        _localEulerAngles = QuaternionToEuler(_localRotation);
        MarkChanged();
        LocalEulerAnglesChanged?.Invoke(_localEulerAngles);
        GlobalEulerAnglesChanged?.Invoke(GlobalEulerAngles);
    }

    public void RotateAround(Vector3 point, Vector3 axis, float angle)
    {
        var pos = GlobalPosition;
        var dir = pos - point;

        var nAxis = NormalizeVector(axis);
        if (IsZeroVector(nAxis))
            return;

        var rad = angle * (MathF.PI / 180f);
        var half = rad * 0.5f;
        var s = MathF.Sin(half);
        var rot = new Quaternion(nAxis.x * s, nAxis.y * s, nAxis.z * s, MathF.Cos(half));

        var newDir = RotateVector(rot, dir);
        var newPos = point + newDir;

        GlobalPosition = newPos;

        var worldRot = Rotation;
        worldRot = MultiplyQuaternion(rot, worldRot);
        Rotation = worldRot;
    }

    public void LookAt(Transform target, Vector3 worldUp)
    {
        LookAt(target.GlobalPosition, worldUp);
    }

    public void LookAt(Transform target)
    {
        LookAt(target.GlobalPosition, new Vector3(0f, 1f, 0f));
    }

    public void LookAt(Vector3 target, Vector3 worldUp)
    {
        var dir = target - GlobalPosition;
        if (IsZeroVector(dir))
            return;

        var rot = LookRotation(dir, worldUp);
        Rotation = rot;
    }

    public void LookAt(Vector3 target)
    {
        LookAt(target, new Vector3(0f, 1f, 0f));
    }

    public Vector3 TransformDirection(Vector3 direction)
    {
        return RotateVector(Rotation, direction);
    }

    public Vector3 TransformDirection(float x, float y, float z)
    {
        return TransformDirection(new Vector3(x, y, z));
    }

    public void TransformDirections(
        ReadOnlySpan<Vector3> directions,
        Span<Vector3> transformedDirections)
    {
        var rot = Rotation;
        for (var i = 0; i < directions.Length; i++)
            transformedDirections[i] = RotateVector(rot, directions[i]);
    }

    public void TransformDirections(Span<Vector3> directions)
    {
        var rot = Rotation;
        for (var i = 0; i < directions.Length; i++)
            directions[i] = RotateVector(rot, directions[i]);
    }

    public Vector3 InverseTransformDirection(Vector3 direction)
    {
        var inv = InverseQuaternion(Rotation);
        return RotateVector(inv, direction);
    }

    public Vector3 InverseTransformDirection(float x, float y, float z)
    {
        return InverseTransformDirection(new Vector3(x, y, z));
    }

    public void InverseTransformDirections(
        ReadOnlySpan<Vector3> directions,
        Span<Vector3> transformedDirections)
    {
        var inv = InverseQuaternion(Rotation);
        for (var i = 0; i < directions.Length; i++)
            transformedDirections[i] = RotateVector(inv, directions[i]);
    }

    public void InverseTransformDirections(Span<Vector3> directions)
    {
        var inv = InverseQuaternion(Rotation);
        for (var i = 0; i < directions.Length; i++)
            directions[i] = RotateVector(inv, directions[i]);
    }

    public Vector3 TransformVector(Vector3 vector)
    {
        var scaled = new Vector3(
            vector.x * _localScale.x,
            vector.y * _localScale.y,
            vector.z * _localScale.z);

        return RotateVector(Rotation, scaled);
    }

    public Vector3 TransformVector(float x, float y, float z)
    {
        return TransformVector(new Vector3(x, y, z));
    }

    public void TransformVectors(ReadOnlySpan<Vector3> vectors, Span<Vector3> transformedVectors)
    {
        var rot = Rotation;
        var scale = _localScale;
        for (var i = 0; i < vectors.Length; i++)
        {
            var v = vectors[i];
            v = new Vector3(v.x * scale.x, v.y * scale.y, v.z * scale.z);
            transformedVectors[i] = RotateVector(rot, v);
        }
    }

    public void TransformVectors(Span<Vector3> vectors)
    {
        var rot = Rotation;
        var scale = _localScale;
        for (var i = 0; i < vectors.Length; i++)
        {
            var v = vectors[i];
            v = new Vector3(v.x * scale.x, v.y * scale.y, v.z * scale.z);
            vectors[i] = RotateVector(rot, v);
        }
    }

    public Vector3 InverseTransformVector(Vector3 vector)
    {
        var inv = InverseQuaternion(Rotation);
        var v = RotateVector(inv, vector);
        if (MathF.Abs(_localScale.x) > 1e-6f) v.x /= _localScale.x; else v.x = 0f;
        if (MathF.Abs(_localScale.y) > 1e-6f) v.y /= _localScale.y; else v.y = 0f;
        if (MathF.Abs(_localScale.z) > 1e-6f) v.z /= _localScale.z; else v.z = 0f;
        return v;
    }

    public Vector3 InverseTransformVector(float x, float y, float z)
    {
        return InverseTransformVector(new Vector3(x, y, z));
    }

    public void InverseTransformVectors(
        ReadOnlySpan<Vector3> vectors,
        Span<Vector3> transformedVectors)
    {
        var inv = InverseQuaternion(Rotation);
        for (var i = 0; i < vectors.Length; i++)
        {
            var v = RotateVector(inv, vectors[i]);
            if (MathF.Abs(_localScale.x) > 1e-6f) v.x /= _localScale.x; else v.x = 0f;
            if (MathF.Abs(_localScale.y) > 1e-6f) v.y /= _localScale.y; else v.y = 0f;
            if (MathF.Abs(_localScale.z) > 1e-6f) v.z /= _localScale.z; else v.z = 0f;
            transformedVectors[i] = v;
        }
    }

    public void InverseTransformVectors(Span<Vector3> vectors)
    {
        var inv = InverseQuaternion(Rotation);
        for (var i = 0; i < vectors.Length; i++)
        {
            var v = RotateVector(inv, vectors[i]);
            if (MathF.Abs(_localScale.x) > 1e-6f) v.x /= _localScale.x; else v.x = 0f;
            if (MathF.Abs(_localScale.y) > 1e-6f) v.y /= _localScale.y; else v.y = 0f;
            if (MathF.Abs(_localScale.z) > 1e-6f) v.z /= _localScale.z; else v.z = 0f;
            vectors[i] = v;
        }
    }

    public Vector3 TransformPoint(Vector3 position)
    {
        var scaled = new Vector3(
            position.x * _localScale.x,
            position.y * _localScale.y,
            position.z * _localScale.z);

        var rotated = RotateVector(Rotation, scaled);
        return GlobalPosition + rotated;
    }

    public Vector3 TransformPoint(float x, float y, float z)
    {
        return TransformPoint(new  Vector3(x, y, z));
    }

    public void TransformPoints(ReadOnlySpan<Vector3> positions, Span<Vector3> transformedPositions)
    {
        var rot = Rotation;
        var scale = _localScale;
        var global = GlobalPosition;
        for (var i = 0; i < positions.Length; i++)
        {
            var p = positions[i];
            p = new Vector3(p.x * scale.x, p.y * scale.y, p.z * scale.z);
            p = RotateVector(rot, p);
            transformedPositions[i] = global + p;
        }
    }

    public void TransformPoints(Span<Vector3> positions)
    {
        var rot = Rotation;
        var scale = _localScale;
        var global = GlobalPosition;
        for (var i = 0; i < positions.Length; i++)
        {
            var p = positions[i];
            p = new Vector3(p.x * scale.x, p.y * scale.y, p.z * scale.z);
            p = RotateVector(rot, p);
            positions[i] = global + p;
        }
    }

    public Vector3 InverseTransformPoint(Vector3 position)
    {
        var global = GlobalPosition;
        var delta = position - global;
        var inv = InverseQuaternion(Rotation);
        var local = RotateVector(inv, delta);

        if (MathF.Abs(_localScale.x) > 1e-6f) local.x /= _localScale.x; else local.x = 0f;
        if (MathF.Abs(_localScale.y) > 1e-6f) local.y /= _localScale.y; else local.y = 0f;
        if (MathF.Abs(_localScale.z) > 1e-6f) local.z /= _localScale.z; else local.z = 0f;

        return local;
    }

    public Vector3 InverseTransformPoint(float x, float y, float z)
    {
        return InverseTransformPoint(new Vector3(x, y, z));
    }

    public void InverseTransformPoints(
        ReadOnlySpan<Vector3> positions,
        Span<Vector3> transformedPositions)
    {
        var global = GlobalPosition;
        var inv = InverseQuaternion(Rotation);
        for (var i = 0; i < positions.Length; i++)
        {
            var delta = positions[i] - global;
            var local = RotateVector(inv, delta);
            if (MathF.Abs(_localScale.x) > 1e-6f) local.x /= _localScale.x; else local.x = 0f;
            if (MathF.Abs(_localScale.y) > 1e-6f) local.y /= _localScale.y; else local.y = 0f;
            if (MathF.Abs(_localScale.z) > 1e-6f) local.z /= _localScale.z; else local.z = 0f;
            transformedPositions[i] = local;
        }
    }

    public void InverseTransformPoints(Span<Vector3> positions)
    {
        var global = GlobalPosition;
        var inv = InverseQuaternion(Rotation);
        for (var i = 0; i < positions.Length; i++)
        {
            var delta = positions[i] - global;
            var local = RotateVector(inv, delta);
            if (MathF.Abs(_localScale.x) > 1e-6f) local.x /= _localScale.x; else local.x = 0f;
            if (MathF.Abs(_localScale.y) > 1e-6f) local.y /= _localScale.y; else local.y = 0f;
            if (MathF.Abs(_localScale.z) > 1e-6f) local.z /= _localScale.z; else local.z = 0f;
            positions[i] = local;
        }
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

    private static bool IsZeroVector(Vector3 v)
    {
        return MathF.Abs(v.x) < 1e-6f &&
               MathF.Abs(v.y) < 1e-6f &&
               MathF.Abs(v.z) < 1e-6f;
    }

    private static Vector3 NormalizeVector(Vector3 v)
    {
        var lenSq = v.x * v.x + v.y * v.y + v.z * v.z;
        if (lenSq < 1e-12f)
            return new Vector3(0f, 0f, 0f);

        var inv = 1f / MathF.Sqrt(lenSq);
        return new Vector3(v.x * inv, v.y * inv, v.z * inv);
    }

    private static Quaternion NormalizeQuaternion(Quaternion q)
    {
        var lenSq = q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
        if (lenSq < 1e-12f)
            return new Quaternion(0f, 0f, 0f, 1f);

        var inv = 1f / MathF.Sqrt(lenSq);
        return new Quaternion(q.x * inv, q.y * inv, q.z * inv, q.w * inv);
    }

    private static Quaternion MultiplyQuaternion(Quaternion a, Quaternion b)
    {
        return new Quaternion(
            a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
            a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
            a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w,
            a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z);
    }

    private static Quaternion InverseQuaternion(Quaternion q)
    {
        var lenSq = q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
        if (lenSq < 1e-12f)
            return new Quaternion(0f, 0f, 0f, 1f);

        var inv = 1f / lenSq;
        return new Quaternion(-q.x * inv, -q.y * inv, -q.z * inv, q.w * inv);
    }

    private static Vector3 RotateVector(Quaternion q, Vector3 v)
    {
        var ux = q.x;
        var uy = q.y;
        var uz = q.z;
        var uw = q.w;

        var vx = v.x;
        var vy = v.y;
        var vz = v.z;

        var tx = 2f * (uy * vz - uz * vy);
        var ty = 2f * (uz * vx - ux * vz);
        var tz = 2f * (ux * vy - uy * vx);

        return new Vector3(
            vx + uw * tx + (uy * tz - uz * ty),
            vy + uw * ty + (uz * tx - ux * tz),
            vz + uw * tz + (ux * ty - uy * tx));
    }

    private static Quaternion EulerToQuaternion(Vector3 eulerDegrees)
    {
        var deg2Rad = MathF.PI / 180f;
        var rx = eulerDegrees.x * deg2Rad;
        var ry = eulerDegrees.y * deg2Rad;
        var rz = eulerDegrees.z * deg2Rad;

        var cx = MathF.Cos(rx * 0.5f);
        var sx = MathF.Sin(rx * 0.5f);
        var cy = MathF.Cos(ry * 0.5f);
        var sy = MathF.Sin(ry * 0.5f);
        var cz = MathF.Cos(rz * 0.5f);
        var sz = MathF.Sin(rz * 0.5f);

        var x = sx * cy * cz + cx * sy * sz;
        var y = cx * sy * cz - sx * cy * sz;
        var z = cx * cy * sz + sx * sy * cz;
        var w = cx * cy * cz - sx * sy * sz;

        return new Quaternion(x, y, z, w);
    }

    private static Vector3 QuaternionToEuler(Quaternion q)
    {
        q = NormalizeQuaternion(q);

        var sinrCosp = 2f * (q.w * q.x + q.y * q.z);
        var cosrCosp = 1f - 2f * (q.x * q.x + q.y * q.y);
        var rx = MathF.Atan2(sinrCosp, cosrCosp);

        var sinp = 2f * (q.w * q.y - q.z * q.x);
        float ry;
        if (MathF.Abs(sinp) >= 1f)
            ry = MathF.CopySign(MathF.PI / 2f, sinp);
        else
            ry = MathF.Asin(sinp);

        var sinyCosp = 2f * (q.w * q.z + q.x * q.y);
        var cosyCosp = 1f - 2f * (q.y * q.y + q.z * q.z);
        var rz = MathF.Atan2(sinyCosp, cosyCosp);

        var rad2Deg = 180f / MathF.PI;
        return new Vector3(rx * rad2Deg, ry * rad2Deg, rz * rad2Deg);
    }

    private static Quaternion LookRotation(Vector3 forward, Vector3 up)
    {
        var z = NormalizeVector(forward);
        if (IsZeroVector(z))
            return new Quaternion(0f, 0f, 0f, 1f);

        var x = NormalizeVector(Cross(up, z));
        if (IsZeroVector(x))
            x = NormalizeVector(Cross(new Vector3(0f, 1f, 0f), z));

        var y = Cross(z, x);

        var m00 = x.x;
        var m01 = y.x;
        var m02 = z.x;
        var m10 = x.y;
        var m11 = y.y;
        var m12 = z.y;
        var m20 = x.z;
        var m21 = y.z;
        var m22 = z.z;

        var trace = m00 + m11 + m22;

        float qw;
        float qx;
        float qy;
        float qz;

        if (trace > 0f)
        {
            var s = MathF.Sqrt(trace + 1f) * 2f;
            qw = 0.25f * s;
            qx = (m21 - m12) / s;
            qy = (m02 - m20) / s;
            qz = (m10 - m01) / s;
        }
        else if (m00 > m11 && m00 > m22)
        {
            var s = MathF.Sqrt(1f + m00 - m11 - m22) * 2f;
            qw = (m21 - m12) / s;
            qx = 0.25f * s;
            qy = (m01 + m10) / s;
            qz = (m02 + m20) / s;
        }
        else if (m11 > m22)
        {
            var s = MathF.Sqrt(1f + m11 - m00 - m22) * 2f;
            qw = (m02 - m20) / s;
            qx = (m01 + m10) / s;
            qy = 0.25f * s;
            qz = (m12 + m21) / s;
        }
        else
        {
            var s = MathF.Sqrt(1f + m22 - m00 - m11) * 2f;
            qw = (m10 - m01) / s;
            qx = (m02 + m20) / s;
            qy = (m12 + m21) / s;
            qz = 0.25f * s;
        }

        return NormalizeQuaternion(new Quaternion(qx, qy, qz, qw));
    }

    private static Vector3 Cross(Vector3 a, Vector3 b)
    {
        return new Vector3(
            a.y * b.z - a.z * b.y,
            a.z * b.x - a.x * b.z,
            a.x * b.y - a.y * b.x);
    }
}
