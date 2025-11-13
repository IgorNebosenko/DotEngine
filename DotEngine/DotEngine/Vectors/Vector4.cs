using System.Runtime.CompilerServices;

namespace DotEngine
{
    /// <summary>
    /// Represents a 4D vector using float precision.
    /// </summary>
    public struct Vector4(float x, float y, float z, float w) : IEquatable<Vector4>, IFormattable
    {
        #region Static variables

        private static readonly Vector4 _zeroVector = new(0f, 0f, 0f, 0f);
        private static readonly Vector4 _oneVector = new(1f, 1f, 1f, 1f);
        private static readonly Vector4 _positiveInfinityVector = new(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        private static readonly Vector4 _negativeInfinityVector = new(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        /// <summary>
        /// Minimum normalized magnitude value.
        /// </summary>
        public const float kEpsilon = 1e-6f;

        #endregion

        #region Static properties

        /// <summary>
        /// (0, 0, 0, 0)
        /// </summary>
        public static Vector4 Zero => _zeroVector;

        /// <summary>
        /// (1, 1, 1, 1)
        /// </summary>
        public static Vector4 One => _oneVector;

        /// <summary>
        /// (Infinity, Infinity, Infinity, Infinity)
        /// </summary>
        public static Vector4 PositiveInfinity => _positiveInfinityVector;

        /// <summary>
        /// (-Infinity, -Infinity, -Infinity, -Infinity)
        /// </summary>
        public static Vector4 NegativeInfinity => _negativeInfinityVector;

        #endregion

        #region Fields

        /// <summary>
        /// X component of the vector.
        /// </summary>
        public float x = x;

        /// <summary>
        /// Y component of the vector.
        /// </summary>
        public float y = y;

        /// <summary>
        /// Z component of the vector.
        /// </summary>
        public float z = z;

        /// <summary>
        /// W component of the vector.
        /// </summary>
        public float w = w;

        #endregion

        #region Properties

        /// <summary>
        /// Returns a normalized vector based on the current vector.
        /// </summary>
        public Vector4 Normalized
        {
            get
            {
                var v = new Vector4(x, y, z, w);
                v.Normalize();
                return v;
            }
        }

        /// <summary>
        /// Returns the magnitude (length) of the vector.
        /// </summary>
        public float Magnitude => MathF.Sqrt(x * x + y * y + z * z + w * w);

        /// <summary>
        /// Returns the squared magnitude of the vector.
        /// </summary>
        public float SqrMagnitude => x * x + y * y + z * z + w * w;

        #endregion

        #region Indexer

        /// <summary>
        /// Access components by index: 0=x, 1=y, 2=z, 3=w.
        /// </summary>
        public float this[int index]
        {
            get =>
                index switch
                {
                    0 => x,
                    1 => y,
                    2 => z,
                    3 => w,
                    _ => throw new IndexOutOfRangeException("Invalid Vector4 index!")
                };
            set
            {
                switch (index)
                {
                    case 0: x = value; return;
                    case 1: y = value; return;
                    case 2: z = value; return;
                    case 3: w = value; return;
                    default: throw new IndexOutOfRangeException("Invalid Vector4 index!");
                }
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        public static Vector4 operator +(Vector4 a, Vector4 b) =>
            new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        public static Vector4 operator -(Vector4 a, Vector4 b) =>
            new(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        public static Vector4 operator *(Vector4 v, float s) =>
            new(v.x * s, v.y * s, v.z * s, v.w * s);

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        public static Vector4 operator /(Vector4 v, float s)
        {
            if (s == 0f) throw new DivideByZeroException();
            return new Vector4(v.x / s, v.y / s, v.z / s, v.w / s);
        }

        /// <summary>
        /// Compares vectors by tolerance.
        /// </summary>
        public static bool operator ==(Vector4 a, Vector4 b)
        {
            return MathF.Abs(a.x - b.x) < kEpsilon &&
                   MathF.Abs(a.y - b.y) < kEpsilon &&
                   MathF.Abs(a.z - b.z) < kEpsilon &&
                   MathF.Abs(a.w - b.w) < kEpsilon;
        }

        /// <summary>
        /// Compares vectors for inequality.
        /// </summary>
        public static bool operator !=(Vector4 a, Vector4 b) => !(a == b);

        #endregion

        #region Methods

        /// <summary>
        /// Sets x, y, z, w components.
        /// </summary>
        public void Set(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Normalizes this vector.
        /// </summary>
        public void Normalize()
        {
            var mag = Magnitude;
            if (mag > kEpsilon)
            {
                x /= mag;
                y /= mag;
                z /= mag;
                w /= mag;
            }
            else
            {
                this = Zero;
            }
        }

        /// <summary>
        /// Scales components individually.
        /// </summary>
        public void Scale(Vector4 s)
        {
            x *= s.x;
            y *= s.y;
            z *= s.z;
            w *= s.w;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Dot product of two vectors.
        /// </summary>
        public static float Dot(Vector4 a, Vector4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        /// <summary>
        /// Linearly interpolates between two vectors.
        /// </summary>
        public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
        {
            t = Math.Clamp(t, 0f, 1f);
            return new Vector4(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t,
                a.w + (b.w - a.w) * t
            );
        }

        /// <summary>
        /// Linearly interpolates without clamping.
        /// </summary>
        public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, float t)
        {
            return new Vector4(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t,
                a.w + (b.w - a.w) * t
            );
        }

        /// <summary>
        /// Component-wise min.
        /// </summary>
        public static Vector4 Min(Vector4 a, Vector4 b) =>
            new(MathF.Min(a.x, b.x), MathF.Min(a.y, b.y), MathF.Min(a.z, b.z), MathF.Min(a.w, b.w));

        /// <summary>
        /// Component-wise max.
        /// </summary>
        public static Vector4 Max(Vector4 a, Vector4 b) =>
            new(MathF.Max(a.x, b.x), MathF.Max(a.y, b.y), MathF.Max(a.z, b.z), MathF.Max(a.w, b.w));

        #endregion

        #region Interface implementations

        public override string ToString() => $"x: {x}, y: {y}, z: {z}, w: {w}";

        public string ToString(string? format, IFormatProvider? provider)
        {
            FormattableString fs = $"x: {x}, y: {y}, z: {z}, w: {w}";
            return fs.ToString(provider);
        }

        public bool Equals(Vector4 other) => this == other;

        public override bool Equals(object? obj) => obj is Vector4 v && Equals(v);

        public override int GetHashCode() => HashCode.Combine(x, y, z, w);

        #endregion

        #region SharpDX Conversion

        /// <summary>
        /// Converts DotEngine.Vector4 to SharpDX.Vector4.
        /// </summary>
        public static implicit operator SharpDX.Vector4(Vector4 v)
        {
            return new SharpDX.Vector4(v.x, v.y, v.z, v.w);
        }

        #endregion
    }
}