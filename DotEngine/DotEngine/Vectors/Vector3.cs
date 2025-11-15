using System.Runtime.CompilerServices;

namespace DotEngine
{
    /// <summary>
    /// Represents a 3D vector using float precision.
    /// Provides vector math operations and utility methods.
    /// </summary>
    public struct Vector3(float x, float y, float z) : IEquatable<Vector3>, IFormattable
    {
        #region Static variables

        private static readonly Vector3 _zeroVector = new(0f, 0f, 0f);
        private static readonly Vector3 _oneVector = new(1f, 1f, 1f);
        private static readonly Vector3 _upVector = new(0f, 1f, 0f);
        private static readonly Vector3 _downVector = new(0f, -1f, 0f);
        private static readonly Vector3 _leftVector = new(-1f, 0f, 0f);
        private static readonly Vector3 _rightVector = new(1f, 0f, 0f);
        private static readonly Vector3 _forwardVector = new(0f, 0f, 1f);
        private static readonly Vector3 _backVector = new(0f, 0f, -1f);
        private static readonly Vector3 _positiveInfinityVector = new(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        private static readonly Vector3 _negativeInfinityVector = new(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        /// <summary>
        /// Minimum normalized magnitude value.
        /// </summary>
        public const float kEpsilon = 1e-6f;

        /// <summary>
        /// Minimum normalized squared magnitude value.
        /// </summary>
        public const float kEpsilonNormalSqrt = 1e-12f;

        #endregion

        #region Static properties

        /// <summary>
        /// (0, 0, 0)
        /// </summary>
        public static Vector3 Zero => _zeroVector;

        /// <summary>
        /// (1, 1, 1)
        /// </summary>
        public static Vector3 One => _oneVector;

        /// <summary>
        /// (0, 1, 0)
        /// </summary>
        public static Vector3 Up => _upVector;

        /// <summary>
        /// (0, -1, 0)
        /// </summary>
        public static Vector3 Down => _downVector;

        /// <summary>
        /// (-1, 0, 0)
        /// </summary>
        public static Vector3 Left => _leftVector;

        /// <summary>
        /// (1, 0, 0)
        /// </summary>
        public static Vector3 Right => _rightVector;

        /// <summary>
        /// (0, 0, 1)
        /// </summary>
        public static Vector3 Forward => _forwardVector;

        /// <summary>
        /// (0, 0, -1)
        /// </summary>
        public static Vector3 Back => _backVector;

        /// <summary>
        /// (Infinity, Infinity, Infinity)
        /// </summary>
        public static Vector3 PositiveInfinity => _positiveInfinityVector;

        /// <summary>
        /// (-Infinity, -Infinity, -Infinity)
        /// </summary>
        public static Vector3 NegativeInfinity => _negativeInfinityVector;

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

        #endregion

        #region Properties

        /// <summary>
        /// Returns a normalized vector based on the current vector.
        /// </summary>
        public Vector3 Normalized
        {
            get
            {
                var v = new Vector3(x, y, z);
                v.Normalize();
                return v;
            }
        }

        /// <summary>
        /// Returns the magnitude (length) of the vector.
        /// </summary>
        public float Magnitude => MathF.Sqrt(x * x + y * y + z * z);

        /// <summary>
        /// Returns the squared magnitude of the vector.
        /// </summary>
        public float SqrMagnitude => x * x + y * y + z * z;

        #endregion

        #region Indexer

        /// <summary>
        /// Access components by index: 0=x, 1=y, 2=z.
        /// </summary>
        public float this[int index]
        {
            get =>
                index switch
                {
                    0 => x,
                    1 => y,
                    2 => z,
                    _ => throw new IndexOutOfRangeException("Invalid Vector3 index!")
                };
            set
            {
                switch (index)
                {
                    case 0: x = value; return;
                    case 1: y = value; return;
                    case 2: z = value; return;
                    default: throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        public static Vector3 operator +(Vector3 a, Vector3 b) =>
            new(a.x + b.x, a.y + b.y, a.z + b.z);

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        public static Vector3 operator -(Vector3 a, Vector3 b) =>
            new(a.x - b.x, a.y - b.y, a.z - b.z);

        /// <summary>
        /// Multiplies vector by scalar.
        /// </summary>
        public static Vector3 operator *(Vector3 v, float s) =>
            new(v.x * s, v.y * s, v.z * s);
        
        /// <summary>Scales a vector by the given value.</summary>
        /// <param name="value">The vector to scale.</param>
        /// <param name="scale">The amount by which to scale the vector.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector3 operator *(float scale, Vector3 value)
        {
            return new Vector3(value.x * scale, value.y * scale, value.z * scale);
        }

        /// <summary>
        /// Divides vector by scalar.
        /// </summary>
        public static Vector3 operator /(Vector3 v, float s)
        {
            if (s == 0f) throw new DivideByZeroException();
            return new Vector3(v.x / s, v.y / s, v.z / s);
        }

        /// <summary>
        /// Compares vectors with tolerance.
        /// </summary>
        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return MathF.Abs(a.x - b.x) < kEpsilon &&
                   MathF.Abs(a.y - b.y) < kEpsilon &&
                   MathF.Abs(a.z - b.z) < kEpsilon;
        }

        /// <summary>
        /// Compares vectors for inequality.
        /// </summary>
        public static bool operator !=(Vector3 a, Vector3 b) => !(a == b);
        
        /// <summary>Reverses the direction of a given vector.</summary>
        /// <param name="value">The vector to negate.</param>
        /// <returns>A vector facing in the opposite direction.</returns>
        public static Vector3 operator -(Vector3 value) => new Vector3(-value.x, -value.y, -value.z);
        
        
        public static explicit operator Vector3(Vector2 v)
        {
            return new Vector3(v.x, v.y, 0f);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets x, y, z components.
        /// </summary>
        public void Set(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Scales components individually.
        /// </summary>
        public void Scale(Vector3 s)
        {
            x *= s.x;
            y *= s.y;
            z *= s.z;
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
            }
            else
            {
                this = Zero;
            }
        }
        
        public float LengthSquared()
        {
            return (float) ((double) x * x + (double) y * y + (double) z * z);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Dot product of two vectors.
        /// </summary>
        public static float Dot(Vector3 a, Vector3 b) =>
            a.x * b.x + a.y * b.y + a.z * b.z;
        
        /// <summary>Calculates the dot product of two vectors.</summary>
        /// <param name="left">First source vector.</param>
        /// <param name="right">Second source vector.</param>
        /// <param name="result">When the method completes, contains the dot product of the two vectors.</param>
        public static void Dot(ref Vector3 left, ref Vector3 right, out float result)
        {
            result = (float) ((double) left.x * right.x + (double) left.y * right.y + (double) left.z * right.z);
        }


        /// <summary>
        /// Cross product of two vectors.
        /// </summary>
        public static Vector3 Cross(Vector3 a, Vector3 b) =>
            new(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x
            );

        /// <summary>
        /// Linearly interpolates between two vectors.
        /// </summary>
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            t = Math.Clamp(t, 0f, 1f);
            return new Vector3(a.x + (b.x - a.x) * t,
                               a.y + (b.y - a.y) * t,
                               a.z + (b.z - a.z) * t);
        }

        /// <summary>
        /// Linearly interpolates between two vectors without clamping.
        /// </summary>
        public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
        {
            return new Vector3(a.x + (b.x - a.x) * t,
                               a.y + (b.y - a.y) * t,
                               a.z + (b.z - a.z) * t);
        }

        /// <summary>
        /// Moves a vector towards a target.
        /// </summary>
        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDelta)
        {
            var dx = target.x - current.x;
            var dy = target.y - current.y;
            var dz = target.z - current.z;

            var sq = dx * dx + dy * dy + dz * dz;

            if (sq == 0f || sq <= maxDelta * maxDelta)
                return target;

            var dist = MathF.Sqrt(sq);

            return new Vector3(
                current.x + dx / dist * maxDelta,
                current.y + dy / dist * maxDelta,
                current.z + dz / dist * maxDelta
            );
        }

        /// <summary>
        /// Returns distance between two vectors.
        /// </summary>
        public static float Distance(Vector3 a, Vector3 b)
        {
            var dx = a.x - b.x;
            var dy = a.y - b.y;
            var dz = a.z - b.z;
            return MathF.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// Clamps magnitude of vector.
        /// </summary>
        public static Vector3 ClampMagnitude(Vector3 v, float maxLength)
        {
            var sq = v.SqrMagnitude;
            if (sq <= maxLength * maxLength) return v;

            var mag = MathF.Sqrt(sq);
            return new Vector3(v.x / mag * maxLength,
                               v.y / mag * maxLength,
                               v.z / mag * maxLength);
        }

        /// <summary>
        /// Component-wise min.
        /// </summary>
        public static Vector3 Min(Vector3 a, Vector3 b) =>
            new(MathF.Min(a.x, b.x), MathF.Min(a.y, b.y), MathF.Min(a.z, b.z));

        /// <summary>
        /// Component-wise max.
        /// </summary>
        public static Vector3 Max(Vector3 a, Vector3 b) =>
            new(MathF.Max(a.x, b.x), MathF.Max(a.y, b.y), MathF.Max(a.z, b.z));
        
        /// <summary>Calculates the cross product of two vectors.</summary>
        /// <param name="left">First source vector.</param>
        /// <param name="right">Second source vector.</param>
        /// <param name="result">When the method completes, contains he cross product of the two vectors.</param>
        public static void Cross(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result = new Vector3((float) ((double) left.y * right.z - (double) left.z *  right.y),
                (float) ((double) left.z * right.x - (double) left.x *  right.z),
                (float) ((double) left.x * right.y - (double) left.y * right.x));
        }
        
        /// <summary>Subtracts two vectors.</summary>
        /// <param name="left">The first vector to subtract.</param>
        /// <param name="right">The second vector to subtract.</param>
        /// <param name="result">When the method completes, contains the difference of the two vectors.</param>
        public static void Subtract(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result = new Vector3(left.x - right.x, left.y - right.y, left.z - right.z);
        }
        
        /// <summary>Converts the vector into a unit vector.</summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        public static Vector3 Normalize(Vector3 value)
        {
            value.Normalize();
            return value;
        }

        #endregion

        #region Interface

        public override string ToString() => $"x: {x}, y: {y}, z: {z}";

        public string ToString(string? format, IFormatProvider? provider)
        {
            FormattableString fs = $"x: {x}, y: {y}, z: {z}";
            return fs.ToString(provider);
        }

        public bool Equals(Vector3 other) => this == other;

        public override bool Equals(object? obj) => obj is Vector3 v && Equals(v);

        public override int GetHashCode() => HashCode.Combine(x, y, z);

        #endregion

        #region SharpDX Conversion

        /// <summary>
        /// Converts DotEngine.Vector3 to SharpDX.Vector3.
        /// </summary>
        public static implicit operator SharpDX.Vector3(Vector3 v)
        {
            return new SharpDX.Vector3(v.x, v.y, v.z);
        }

        #endregion
    }
}