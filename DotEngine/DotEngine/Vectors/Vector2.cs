using System.Runtime.CompilerServices;

namespace DotEngine
{
    /// <summary>
    /// Represents a 2D vector using float precision.
    /// Provides vector math operations and utility methods.
    /// </summary>
    public struct Vector2(float x, float y) : IEquatable<Vector2>, IFormattable
    {
        #region Static variables

        private static readonly Vector2 _zeroVector = new(0f, 0f);
        private static readonly Vector2 _oneVector = new(1f, 1f);
        private static readonly Vector2 _rightVector = new(1f, 0f);
        private static readonly Vector2 _leftVector = new(-1f, 0f);
        private static readonly Vector2 _upVector = new(0f, 1f);
        private static readonly Vector2 _downVector = new(0f, -1f);
        private static readonly Vector2 _positiveInfinityVector = new(float.PositiveInfinity, float.PositiveInfinity);
        private static readonly Vector2 _negativeInfinityVector = new(float.NegativeInfinity, float.NegativeInfinity);

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
        /// (0, 0)
        /// </summary>
        public static Vector2 Zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _zeroVector;
        }

        /// <summary>
        /// (1, 1)
        /// </summary>
        public static Vector2 One
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _oneVector;
        }

        /// <summary>
        /// (1, 0)
        /// </summary>
        public static Vector2 Right
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _rightVector;
        }

        /// <summary>
        /// (-1, 0)
        /// </summary>
        public static Vector2 Left
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _leftVector;
        }

        /// <summary>
        /// (0, 1)
        /// </summary>
        public static Vector2 Up
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _upVector;
        }

        /// <summary>
        /// (0, -1)
        /// </summary>
        public static Vector2 Down
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _downVector;
        }

        /// <summary>
        /// (PositiveInfinity, PositiveInfinity)
        /// </summary>
        public static Vector2 PositiveInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _positiveInfinityVector;
        }

        /// <summary>
        /// (NegativeInfinity, NegativeInfinity)
        /// </summary>
        public static Vector2 NegativeInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _negativeInfinityVector;
        }

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

        #endregion

        #region Properties

        /// <summary>
        /// Returns a normalized vector based on the current vector.
        /// The normalized vector has a magnitude of 1 and is in the same direction as the current vector.
        /// Returns a zero vector If the current vector is too small to be normalized.
        /// </summary>
        public Vector2 Normalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var normalized = new Vector2(x, y);
                normalized.Normalize();
                return normalized;
            }
        }

        /// <summary>
        /// Returns the length of this vector.
        /// </summary>
        public float Magnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => MathF.Sqrt(x * x + y * y);
        }

        /// <summary>
        /// Returns the squared length of this vector.
        /// </summary>
        public float SqrMagnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => x * x + y * y;
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Access by index.
        /// </summary>
        /// <param name="index">0 → x, 1 → y</param>
        /// <exception cref="IndexOutOfRangeException">If outside [0..1]</exception>
        public float this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get =>
                index switch
                {
                    0 => x,
                    1 => y,
                    _ => throw new IndexOutOfRangeException("Invalid Vector2 index!")
                };
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.x + b.x, a.y + b.y);

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.x - b.x, a.y - b.y);

        /// <summary>
        /// Multiplies vector by scalar.
        /// </summary>
        public static Vector2 operator *(Vector2 v, float s) => new(v.x * s, v.y * s);

        /// <summary>
        /// Divides vector by scalar.
        /// </summary>
        /// <exception cref="DivideByZeroException"></exception>
        public static Vector2 operator /(Vector2 v, float s)
        {
            if (s == 0f) throw new DivideByZeroException();
            return new Vector2(v.x / s, v.y / s);
        }

        /// <summary>
        /// Compares vectors for approximate equality.
        /// </summary>
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return MathF.Abs(a.x - b.x) < kEpsilon &&
                   MathF.Abs(a.y - b.y) < kEpsilon;
        }

        /// <summary>
        /// Compares vectors for inequality.
        /// </summary>
        public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);

        #endregion

        #region Methods

        /// <summary>
        /// Sets x and y components.
        /// </summary>
        public void Set(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Multiplies every component of this vector by the same component of scale.
        /// </summary>
        public void Scale(Vector2 scale)
        {
            x *= scale.x;
            y *= scale.y;
        }

        /// <summary>
        /// Normalizes this vector.
        /// </summary>
        public void Normalize()
        {
            var magnitude = Magnitude;
            if (magnitude > kEpsilon)
            {
                x /= magnitude;
                y /= magnitude;
            }
            else
            {
                this = Zero;
            }
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Interpolates value between vecF and vecS for t clamped [0..1].
        /// </summary>
        public static Vector2 Lerp(Vector2 vecF, Vector2 vecS, float t)
        {
            t = Math.Clamp(t, 0f, 1f);
            return new Vector2(vecF.x + (vecS.x - vecF.x) * t, vecF.y + (vecS.y - vecF.y) * t);
        }

        /// <summary>
        /// Interpolates value between vecF and vecS without clamping.
        /// </summary>
        public static Vector2 LerpUnclamped(Vector2 vecF, Vector2 vecS, float t)
        {
            return new Vector2(vecF.x + (vecS.x - vecF.x) * t, vecF.y + (vecS.y - vecF.y) * t);
        }

        /// <summary>
        /// Moves a point current towards target.
        /// </summary>
        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            var dx = target.x - current.x;
            var dy = target.y - current.y;
            var sqr = dx * dx + dy * dy;

            if (sqr == 0f || sqr <= maxDistanceDelta * maxDistanceDelta)
                return target;

            var dist = MathF.Sqrt(sqr);
            return new Vector2(current.x + dx / dist * maxDistanceDelta, current.y + dy / dist * maxDistanceDelta);
        }

        /// <summary>
        /// Component-wise scaling.
        /// </summary>
        public static Vector2 Scale(Vector2 a, Vector2 b) => new(a.x * b.x, a.y * b.y);

        /// <summary>
        /// Dot product.
        /// </summary>
        public static float Dot(Vector2 a, Vector2 b) => a.x * b.x + a.y * b.y;

        /// <summary>
        /// Reflects a vector off a surface defined by a normal.
        /// </summary>
        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
        {
            var num = -2f * Dot(inNormal, inDirection);
            return new Vector2(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y);
        }

        /// <summary>
        /// Returns a perpendicular vector rotated 90 degrees CCW.
        /// </summary>
        public static Vector2 Perpendicular(Vector2 v) => new(-v.y, v.x);

        /// <summary>
        /// Returns unsigned angle between vectors in degrees.
        /// </summary>
        public static float Angle(Vector2 from, Vector2 to)
        {
            var denom = MathF.Sqrt(from.SqrMagnitude * to.SqrMagnitude);
            if (denom < 1e-12f) return 0f;
            return MathF.Acos(Math.Clamp(Dot(from, to) / denom, -1f, 1f)) * 57.29578f;
        }

        /// <summary>
        /// Returns signed angle in degrees between vectors.
        /// </summary>
        public static float SignedAngle(Vector2 from, Vector2 to)
        {
            return Angle(from, to) * MathF.Sign(from.x * to.y - from.y * to.x);
        }

        /// <summary>
        /// Returns distance between vectors.
        /// </summary>
        public static float Distance(Vector2 a, Vector2 b)
        {
            var dx = a.x - b.x;
            var dy = a.y - b.y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// Clamps magnitude of vector to maxLength.
        /// </summary>
        public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
        {
            var sqr = vector.SqrMagnitude;
            if (sqr <= maxLength * maxLength) return vector;

            var mag = MathF.Sqrt(sqr);
            return new Vector2(vector.x / mag * maxLength, vector.y / mag * maxLength);
        }

        /// <summary>
        /// Component-wise minimum.
        /// </summary>
        public static Vector2 Min(Vector2 a, Vector2 b) => new(MathF.Min(a.x, b.x), MathF.Min(a.y, b.y));

        /// <summary>
        /// Component-wise maximum.
        /// </summary>
        public static Vector2 Max(Vector2 a, Vector2 b) => new(MathF.Max(a.x, b.x), MathF.Max(a.y, b.y));

        #endregion

        #region Interface implementations

        public override string ToString() => $"x: {x}, y: {y}";

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            FormattableString fs = $"x: {x}, y: {y}";
            return fs.ToString(formatProvider);
        }

        public bool Equals(Vector2 other) => this == other;

        public override bool Equals(object? obj) => obj is Vector2 v && Equals(v);

        public override int GetHashCode() => HashCode.Combine(x, y);

        #endregion

        #region SharpDX Conversion

        /// <summary>
        /// Converts DotEngine.Vector2 to SharpDX.Vector2.
        /// </summary>
        public static implicit operator SharpDX.Vector2(Vector2 value)
        {
            return new SharpDX.Vector2(value.x, value.y);
        }

        #endregion
    }
}