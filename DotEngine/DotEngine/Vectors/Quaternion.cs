using System.Runtime.CompilerServices;

namespace DotEngine
{
    /// <summary>
    /// Represents a rotation in 3D space using a quaternion (x, y, z, w).
    /// </summary>
    public struct Quaternion(float x, float y, float z, float w) : IEquatable<Quaternion>, IFormattable
    {
        #region Static variables

        private static readonly Quaternion _identityQuaternion = new(0f, 0f, 0f, 1f);
        private static readonly Quaternion _zeroQuaternion = new(0f, 0f, 0f, 0f);

        public const float kEpsilon = 1e-6f;

        #endregion

        #region Static properties

        /// <summary>
        /// (0, 0, 0, 0)
        /// </summary>
        public static Quaternion Zero => _zeroQuaternion;

        /// <summary>
        /// Identity rotation (0, 0, 0, 1)
        /// </summary>
        public static Quaternion Identity => _identityQuaternion;

        #endregion

        #region Fields

        /// <summary>
        /// X component.
        /// </summary>
        public float x = x;

        /// <summary>
        /// Y component.
        /// </summary>
        public float y = y;

        /// <summary>
        /// Z component.
        /// </summary>
        public float z = z;

        /// <summary>
        /// W component.
        /// </summary>
        public float w = w;

        #endregion

        #region Indexer

        /// <summary>
        /// Index: 0=x, 1=y, 2=z, 3=w.
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
                    _ => throw new IndexOutOfRangeException()
                };
            set
            {
                switch (index)
                {
                    case 0: x = value; return;
                    case 1: y = value; return;
                    case 2: z = value; return;
                    case 3: w = value; return;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the magnitude of this quaternion.
        /// </summary>
        public float Magnitude => MathF.Sqrt(x * x + y * y + z * z + w * w);

        /// <summary>
        /// Returns the squared magnitude.
        /// </summary>
        public float SqrMagnitude => x * x + y * y + z * z + w * w;

        #endregion

        #region Operators

        /// <summary>
        /// Compares quaternions with tolerance.
        /// </summary>
        public static bool operator ==(Quaternion a, Quaternion b)
        {
            return MathF.Abs(a.x - b.x) < kEpsilon &&
                   MathF.Abs(a.y - b.y) < kEpsilon &&
                   MathF.Abs(a.z - b.z) < kEpsilon &&
                   MathF.Abs(a.w - b.w) < kEpsilon;
        }

        /// <summary>
        /// Compares quaternions for inequality.
        /// </summary>
        public static bool operator !=(Quaternion a, Quaternion b) => !(a == b);

        /// <summary>
        /// Quaternion multiplication (rotation combining).
        /// </summary>
        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            return new Quaternion(
                lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.w * rhs.y - lhs.x * rhs.z + lhs.y * rhs.w + lhs.z * rhs.x,
                lhs.w * rhs.z + lhs.x * rhs.y - lhs.y * rhs.x + lhs.z * rhs.w,
                lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z
            );
        }

        /// <summary>
        /// Rotates a vector by the quaternion.
        /// </summary>
        public static Vector3 operator *(Quaternion rotation, Vector3 point)
        {
            var num = rotation.x * 2f;
            var num2 = rotation.y * 2f;
            var num3 = rotation.z * 2f;
            var num4 = rotation.x * num;
            var num5 = rotation.y * num2;
            var num6 = rotation.z * num3;
            var num7 = rotation.x * num2;
            var num8 = rotation.x * num3;
            var num9 = rotation.y * num3;
            var num10 = rotation.w * num;
            var num11 = rotation.w * num2;
            var num12 = rotation.w * num3;

            return new Vector3(
                (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z,
                (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z,
                (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z
            );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Normalizes this quaternion.
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
                this = Identity;
            }
        }

        /// <summary>
        /// Returns a normalized copy.
        /// </summary>
        public Quaternion Normalized
        {
            get
            {
                var q = new Quaternion(x, y, z, w);
                q.Normalize();
                return q;
            }
        }

        #endregion

        #region Static math

        /// <summary>
        /// Dot product between two quaternions.
        /// </summary>
        public static float Dot(Quaternion a, Quaternion b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        /// <summary>
        /// Returns the inverse of rotation.
        /// </summary>
        public static Quaternion Inverse(Quaternion q)
        {
            var dot = Dot(q, q);
            if (dot < kEpsilon) return Identity;

            var inv = 1f / dot;
            return new Quaternion(-q.x * inv, -q.y * inv, -q.z * inv, q.w * inv);
        }

        /// <summary>
        /// Constructs quaternion from axis-angle.
        /// </summary>
        public static Quaternion AngleAxis(float angle, Vector3 axis)
        {
            var rad = angle * 0.01745329251f;
            axis.Normalize();
            var sin = MathF.Sin(rad * 0.5f);

            return new Quaternion(
                axis.x * sin,
                axis.y * sin,
                axis.z * sin,
                MathF.Cos(rad * 0.5f)
            );
        }

        /// <summary>
        /// Returns angle in degrees between two rotations.
        /// </summary>
        public static float Angle(Quaternion a, Quaternion b)
        {
            var dot = MathF.Abs(Dot(a, b));
            if (dot > 1f) dot = 1f;
            return MathF.Acos(dot) * 2f * 57.29578f;
        }

        /// <summary>
        /// Linear interpolation (not normalized).
        /// </summary>
        public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
        {
            t = Math.Clamp(t, 0f, 1f);
            var q = new Quaternion(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t,
                a.w + (b.w - a.w) * t
            );
            q.Normalize();
            return q;
        }

        /// <summary>
        /// Spherical interpolation.
        /// </summary>
        public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        {
            t = Math.Clamp(t, 0f, 1f);

            var dot = Dot(a, b);
            if (dot < 0f)
            {
                b = new Quaternion(-b.x, -b.y, -b.z, -b.w);
                dot = -dot;
            }

            const float DOT_THRESHOLD = 0.9995f;
            if (dot > DOT_THRESHOLD)
                return Lerp(a, b, t);

            var theta0 = MathF.Acos(dot);
            var theta = theta0 * t;

            var sin0 = MathF.Sin(theta0);
            var sin1 = MathF.Sin(theta);
            var sin2 = MathF.Sin(theta0 - theta);

            return new Quaternion(
                (a.x * sin2 + b.x * sin1) / sin0,
                (a.y * sin2 + b.y * sin1) / sin0,
                (a.z * sin2 + b.z * sin1) / sin0,
                (a.w * sin2 + b.w * sin1) / sin0
            );
        }

        /// <summary>
        /// Converts Euler angles (degrees) to quaternion.
        /// </summary>
        public static Quaternion Euler(Vector3 euler)
        {
            return Euler(euler.x, euler.y, euler.z);
        }

        /// <summary>
        /// Converts Euler angles (degrees) to quaternion.
        /// </summary>
        public static Quaternion Euler(float x, float y, float z)
        {
            x *= 0.01745329251f;
            y *= 0.01745329251f;
            z *= 0.01745329251f;

            var sx = MathF.Sin(x * 0.5f);
            var sy = MathF.Sin(y * 0.5f);
            var sz = MathF.Sin(z * 0.5f);
            var cx = MathF.Cos(x * 0.5f);
            var cy = MathF.Cos(y * 0.5f);
            var cz = MathF.Cos(z * 0.5f);

            return new Quaternion(
                sx * cy * cz + cx * sy * sz,
                cx * sy * cz - sx * cy * sz,
                cx * cy * sz + sx * sy * cz,
                cx * cy * cz - sx * sy * sz
            );
        }

        /// <summary>
        /// Converts quaternion to Euler angles in degrees.
        /// </summary>
        public Vector3 ToEuler()
        {
            var sinr = 2f * (w * x + y * z);
            var cosr = 1f - 2f * (x * x + y * y);
            var roll = MathF.Atan2(sinr, cosr);

            var sinp = 2f * (w * y - z * x);
            var pitch = MathF.Abs(sinp) >= 1f ? MathF.CopySign(MathF.PI * 0.5f, sinp) : MathF.Asin(sinp);

            var siny = 2f * (w * z + x * y);
            var cosy = 1f - 2f * (y * y + z * z);
            var yaw = MathF.Atan2(siny, cosy);

            return new Vector3(
                roll * 57.29578f,
                pitch * 57.29578f,
                yaw * 57.29578f
            );
        }

        /// <summary>
        /// Creates rotation looking in specified direction.
        /// </summary>
        public static Quaternion LookRotation(Vector3 forward, Vector3 up)
        {
            forward.Normalize();
            up.Normalize();

            var right = Vector3.Cross(up, forward);
            up = Vector3.Cross(forward, right);

            var m00 = right.x; var m01 = right.y; var m02 = right.z;
            var m10 = up.x;    var m11 = up.y;    var m12 = up.z;
            var m20 = forward.x; var m21 = forward.y; var m22 = forward.z;

            var num8 = (m00 + m11) + m22;

            Quaternion q;

            if (num8 > 0f)
            {
                var num = MathF.Sqrt(num8 + 1f);
                q.w = num * 0.5f;
                num = 0.5f / num;
                q.x = (m12 - m21) * num;
                q.y = (m20 - m02) * num;
                q.z = (m01 - m10) * num;
            }
            else if (m00 >= m11 && m00 >= m22)
            {
                var num = MathF.Sqrt(1f + m00 - m11 - m22);
                var num2 = 0.5f / num;
                q.x = 0.5f * num;
                q.y = (m01 + m10) * num2;
                q.z = (m02 + m20) * num2;
                q.w = (m12 - m21) * num2;
            }
            else if (m11 > m22)
            {
                var num = MathF.Sqrt(1f + m11 - m00 - m22);
                var num2 = 0.5f / num;
                q.x = (m10 + m01) * num2;
                q.y = 0.5f * num;
                q.z = (m21 + m12) * num2;
                q.w = (m20 - m02) * num2;
            }
            else
            {
                var num = MathF.Sqrt(1f + m22 - m00 - m11);
                var num2 = 0.5f / num;
                q.x = (m20 + m02) * num2;
                q.y = (m21 + m12) * num2;
                q.z = 0.5f * num;
                q.w = (m01 - m10) * num2;
            }

            return q;
        }

        #endregion

        #region Interface

        public override string ToString() =>
            $"x: {x}, y: {y}, z: {z}, w: {w}";

        public string ToString(string? format, IFormatProvider? provider)
        {
            FormattableString fs = $"x: {x}, y: {y}, z: {z}, w: {w}";
            return fs.ToString(provider);
        }

        public bool Equals(Quaternion other) => this == other;

        public override bool Equals(object? obj) =>
            obj is Quaternion q && Equals(q);

        public override int GetHashCode() =>
            HashCode.Combine(x, y, z, w);

        #endregion

        #region SharpDX Conversion

        /// <summary>
        /// Converts DotEngine.Quaternion to SharpDX.Quaternion.
        /// </summary>
        public static implicit operator SharpDX.Quaternion(Quaternion q)
        {
            return new SharpDX.Quaternion(q.x, q.y, q.z, q.w);
        }

        #endregion
    }
}
