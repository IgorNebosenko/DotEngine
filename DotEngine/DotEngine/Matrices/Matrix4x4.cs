using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotEngine
{
    /// <summary>
    /// Represents a 4x4 matrix using float precision.
    /// Layout:
    /// m11 m12 m13 m14
    /// m21 m22 m23 m24
    /// m31 m32 m33 m34
    /// m41 m42 m43 m44
    /// </summary>
    [StructLayout((LayoutKind.Sequential), Pack = 4)]
    public struct Matrix4x4 : IEquatable<Matrix4x4>, IFormattable
    {
        #region Static variables

        private static readonly Matrix4x4 zeroMatrix = new(
            0f, 0f, 0f, 0f,
            0f, 0f, 0f, 0f,
            0f, 0f, 0f, 0f,
            0f, 0f, 0f, 0f
        );

        private static readonly Matrix4x4 identityMatrix = new(
            1f, 0f, 0f, 0f,
            0f, 1f, 0f, 0f,
            0f, 0f, 1f, 0f,
            0f, 0f, 0f, 1f
        );

        /// <summary>
        /// Epsilon used for matrix equality comparison.
        /// </summary>
        public const float kEpsilon = 1e-6f;

        #endregion

        #region Static properties

        /// <summary>
        /// Returns the zero matrix.
        /// </summary>
        public static Matrix4x4 Zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => zeroMatrix;
        }

        /// <summary>
        /// Returns the identity matrix.
        /// </summary>
        public static Matrix4x4 Identity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => identityMatrix;
        }

        #endregion

        #region Fields

        /// <summary>
        /// Matrix element at row 0, column 0.
        /// </summary>
        public float m11;
        /// <summary>
        /// Matrix element at row 0, column 1.
        /// </summary>
        public float m12;
        /// <summary>
        /// Matrix element at row 0, column 2.
        /// </summary>
        public float m13;
        /// <summary>
        /// Matrix element at row 0, column 3.
        /// </summary>
        public float m14;

        /// <summary>
        /// Matrix element at row 1, column 0.
        /// </summary>
        public float m21;
        /// <summary>
        /// Matrix element at row 1, column 1.
        /// </summary>
        public float m22;
        /// <summary>
        /// Matrix element at row 1, column 2.
        /// </summary>
        public float m23;
        /// <summary>
        /// Matrix element at row 1, column 3.
        /// </summary>
        public float m24;

        /// <summary>
        /// Matrix element at row 2, column 0.
        /// </summary>
        public float m31;
        /// <summary>
        /// Matrix element at row 2, column 1.
        /// </summary>
        public float m32;
        /// <summary>
        /// Matrix element at row 2, column 2.
        /// </summary>
        public float m33;
        /// <summary>
        /// Matrix element at row 2, column 3.
        /// </summary>
        public float m34;

        /// <summary>
        /// Matrix element at row 3, column 0.
        /// </summary>
        public float m41;
        /// <summary>
        /// Matrix element at row 3, column 1.
        /// </summary>
        public float m42;
        /// <summary>
        /// Matrix element at row 3, column 2.
        /// </summary>
        public float m43;
        /// <summary>
        /// Matrix element at row 3, column 3.
        /// </summary>
        public float m44;

        #endregion

        #region Indexer

        /// <summary>
        /// Access matrix element by row and column index.
        /// Row and column must be in range [0..3].
        /// </summary>
        /// <param name="row">Row index in range [0..3].</param>
        /// <param name="column">Column index in range [0..3].</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public float this[int row, int column]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (row, column) switch
                {
                    (0, 0) => m11,
                    (0, 1) => m12,
                    (0, 2) => m13,
                    (0, 3) => m14,

                    (1, 0) => m21,
                    (1, 1) => m22,
                    (1, 2) => m23,
                    (1, 3) => m24,

                    (2, 0) => m31,
                    (2, 1) => m32,
                    (2, 2) => m33,
                    (2, 3) => m34,

                    (3, 0) => m41,
                    (3, 1) => m42,
                    (3, 2) => m43,
                    (3, 3) => m44,

                    _ => throw new IndexOutOfRangeException("Invalid Matrix4x4 index!")
                };
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                switch (row, column)
                {
                    case (0, 0): m11 = value; break;
                    case (0, 1): m12 = value; break;
                    case (0, 2): m13 = value; break;
                    case (0, 3): m14 = value; break;

                    case (1, 0): m21 = value; break;
                    case (1, 1): m22 = value; break;
                    case (1, 2): m23 = value; break;
                    case (1, 3): m24 = value; break;

                    case (2, 0): m31 = value; break;
                    case (2, 1): m32 = value; break;
                    case (2, 2): m33 = value; break;
                    case (2, 3): m34 = value; break;

                    case (3, 0): m41 = value; break;
                    case (3, 1): m42 = value; break;
                    case (3, 2): m43 = value; break;
                    case (3, 3): m44 = value; break;

                    default: throw new IndexOutOfRangeException("Invalid Matrix4x4 index!");
                }
            }
        }

        #endregion

        #region Constructors

        public Matrix4x4(float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44
        )
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m14 = m14;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m24 = m24;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
            this.m34 = m34;
            this.m41 = m41;
            this.m42 = m42;
            this.m43 = m43;
            this.m44 = m44;
        }

        public Matrix4x4(float value)
        {
            m11 = m12 = m13 = m14 = m21 = m22 = m23 = m24 = m31 = m32 = m33 = m34 = m41 = m42 = m43 = m44 = value;
        }

        public Matrix4x4(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof (values)); 
            }
            
            if (values.Length != 16)
            {
                throw new ArgumentOutOfRangeException(nameof(values),
                    "There must be sixteen and only sixteen input values for Matrix4x4.");
            }

            m11 = values[0];
            m12 = values[1];
            m13 = values[2];
            m14 = values[3];
            m21 = values[4];
            m22 = values[5];
            m23 = values[6];
            m24 = values[7];
            m31 = values[8];
            m32 = values[9];
            m33 = values[10];
            m34 = values[11];
            m41 = values[12];
            m42 = values[13];
            m43 = values[14];
            m44 = values[15];
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// Returns the determinant of this matrix.
        /// </summary>
        public float Determinant
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ((SharpDX.Matrix)(this)).Determinant();
            }
        }

        /// <summary>
        /// Gets or sets the up Vector3 of the matrix; that is M21, M22, and M23.
        /// </summary>
        public Vector3 Up
        {
            get => new Vector3(m21, m22, m23);
            set
            {
                m21 = value.x;
                m22 = value.y;
                m23 = value.z;
            }
        }

        /// <summary>
        /// Gets or sets the down Vector3 of the matrix; that is -M21, -M22, and -M23.
        /// </summary>
        public Vector3 Down
        {
            get => new Vector3(-m21, -m22, -m23);
            set
            {
                m21 = -value.x;
                m22 = -value.y;
                m23 = -value.z;
            }
        }

        /// <summary>
        /// Gets or sets the right Vector3 of the matrix; that is M11, M12, and M13.
        /// </summary>
        public Vector3 Right
        {
            get => new Vector3(m11, m12, m13);
            set
            {
                m11 = value.x;
                m12 = value.y;
                m13 = value.z;
            }
        }
        
        /// <summary>
        /// Gets or sets the left Vector3 of the matrix; that is M11, M12, and M13.
        /// </summary>
        public Vector3 Left
        {
            get => new Vector3(-m11, -m12, -m13);
            set
            {
                m11 = -value.x;
                m12 = -value.y;
                m13 = -value.z;
            }
        }

        /// <summary>
        /// Gets or sets the forward Vector3 of the matrix; that is -M31, -M32, and -M33.
        /// </summary>
        public Vector3 Forward
        {
            get => new Vector3(m31, m32, m33);
            set
            {
                m31 = value.x;
                m32 = value.y;
                m33 = value.z;
            }
        }
        
        /// <summary>
        /// Gets or sets the backward Vector3 of the matrix; that is -M31, -M32, and -M33.
        /// </summary>
        public Vector3 Backward
        {
            get => new Vector3(-m31, -m32, -m33);
            set
            {
                m31 = -value.x;
                m32 = -value.y;
                m33 = -value.z;
            }
        }
        
        /// <summary>
        /// Gets or sets the translation of the matrix; that is M41, M42, and M43.
        /// </summary>
        public Vector3 TranslationVector
        {
            get => new Vector3(m41, m42, m43);
            set
            {
                m41 = value.x;
                m42 = value.y;
                m43 = value.z;
            }
        }
        
        /// <summary>
        /// Gets or sets the scale of the matrix; that is M11, M22, and M33.
        /// </summary>
        public Vector3 ScaleVector
        {
            get => new Vector3(m11, m22, m33);
            set
            {
                m11 = value.x;
                m22 = value.y;
                m33 = value.z;
            }
        }

        /// <summary>
        /// Gets or sets the first row in the matrix; that is M11, M12, M13, and M14.
        /// </summary>
        public Vector4 Row1
        {
            get => new Vector4(m11, m12, m13, m14);
            set
            {
                m11 = value.x;
                m12 = value.y;
                m13 = value.z;
                m14 = value.w;
            }
        }
        
        /// <summary>
        /// Gets or sets the second row in the matrix; that is M11, M12, M13, and M14.
        /// </summary>
        public Vector4 Row2
        {
            get => new Vector4(m21, m22, m23, m24);
            set
            {
                m21 = value.x;
                m22 = value.y;
                m23 = value.z;
                m24 = value.w;
            }
        }
        
        /// <summary>
        /// Gets or sets the third row in the matrix; that is M11, M12, M13, and M14.
        /// </summary>
        public Vector4 Row3
        {
            get => new Vector4(m31, m32, m33, m34);
            set
            {
                m31 = value.x;
                m32 = value.y;
                m33 = value.z;
                m34 = value.w;
            }
        }
        
        /// <summary>
        /// Gets or sets the fourth row in the matrix; that is M11, M12, M13, and M14.
        /// </summary>
        public Vector4 Row4
        {
            get => new Vector4(m41, m42, m43, m44);
            set
            {
                m41 = value.x;
                m42 = value.y;
                m43 = value.z;
                m44 = value.w;
            }
        }
        
        /// <summary>
        /// Gets or sets the first column in the matrix; that is M11, M12, M13, and M14.
        /// </summary>
        public Vector4 Column1
        {
            get => new Vector4(m11, m21, m31, m41);
            set
            {
                m11 = value.x;
                m21 = value.y;
                m31 = value.z;
                m41 = value.w;
            }
        }
        
        /// <summary>
        /// Gets or sets the second column in the matrix; that is M11, M12, M13, and M14.
        /// </summary>
        public Vector4 Column2
        {
            get => new Vector4(m12, m22, m32, m42);
            set
            {
                m12 = value.x;
                m22 = value.y;
                m32 = value.z;
                m42 = value.w;
            }
        }
        
        /// <summary>
        /// Gets or sets the first column in the matrix; that is M11, M12, M13, and M14.
        /// </summary>
        public Vector4 Column3
        {
            get => new Vector4(m13, m23, m33, m43);
            set
            {
                m13 = value.x;
                m23 = value.y;
                m33 = value.z;
                m43 = value.w;
            }
        }
        
        /// <summary>
        /// Gets or sets the fourth column in the matrix; that is M11, M12, M13, and M14.
        /// </summary>
        public Vector4 Column4
        {
            get => new Vector4(m14, m24, m34, m44);
            set
            {
                m14 = value.x;
                m24 = value.y;
                m34 = value.z;
                m44 = value.w;
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether this instance is an identity matrix.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is an identity matrix; otherwise, <c>false</c>.
        /// </value>
        public bool IsIdentity => Equals(Identity);

        #endregion

        #region Static methods

        /// <summary>
        /// Returns the determinant of the given matrix.
        /// </summary>
        public static float DeterminantOf(Matrix4x4 m)
        {
            return ((SharpDX.Matrix)(m)).Determinant();
        }

        /// <summary>
        /// Returns the transpose of the given matrix.
        /// </summary>
        public static Matrix4x4 Transpose(Matrix4x4 m)
        {
            var sharp = (SharpDX.Matrix)m;
            var t = SharpDX.Matrix.Transpose(sharp);
            return FromSharpDX(t);
        }

        /// <summary>
        /// Returns the inverse of the given matrix.
        /// </summary>
        public static Matrix4x4 Inverse(Matrix4x4 m)
        {
            var sharp = (SharpDX.Matrix)m;
            var inv = SharpDX.Matrix.Invert(sharp);
            return FromSharpDX(inv);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            var sa = (SharpDX.Matrix)a;
            var sb = (SharpDX.Matrix)b;
            var sr = SharpDX.Matrix.Multiply(sa, sb);
            return FromSharpDX(sr);
        }

        /// <summary>
        /// Compares matrices using epsilon tolerance.
        /// </summary>
        public static bool operator ==(Matrix4x4 a, Matrix4x4 b)
        {
            return MathF.Abs(a.m11 - b.m11) < kEpsilon &&
                   MathF.Abs(a.m12 - b.m12) < kEpsilon &&
                   MathF.Abs(a.m13 - b.m13) < kEpsilon &&
                   MathF.Abs(a.m14 - b.m14) < kEpsilon &&

                   MathF.Abs(a.m21 - b.m21) < kEpsilon &&
                   MathF.Abs(a.m22 - b.m22) < kEpsilon &&
                   MathF.Abs(a.m23 - b.m23) < kEpsilon &&
                   MathF.Abs(a.m24 - b.m24) < kEpsilon &&

                   MathF.Abs(a.m31 - b.m31) < kEpsilon &&
                   MathF.Abs(a.m32 - b.m32) < kEpsilon &&
                   MathF.Abs(a.m33 - b.m33) < kEpsilon &&
                   MathF.Abs(a.m34 - b.m34) < kEpsilon &&

                   MathF.Abs(a.m41 - b.m41) < kEpsilon &&
                   MathF.Abs(a.m42 - b.m42) < kEpsilon &&
                   MathF.Abs(a.m43 - b.m43) < kEpsilon &&
                   MathF.Abs(a.m44 - b.m44) < kEpsilon;
        }

        /// <summary>
        /// Compares matrices for inequality.
        /// </summary>
        public static bool operator !=(Matrix4x4 a, Matrix4x4 b) => !(a == b);

        #endregion

        #region Helpers

        /// <summary>
        /// Creates DotEngine.Matrix4x4 from SharpDX.Matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Matrix4x4 FromSharpDX(SharpDX.Matrix m)
        {
            return new Matrix4x4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
            );
        }

        #endregion

        #region Interface implementations

        /// <summary>
        /// Returns string representation of the matrix.
        /// </summary>
        public override string ToString()
        {
            return
                $"({m11}, {m12}, {m13}, {m14}) " +
                $"({m21}, {m22}, {m23}, {m24}) " +
                $"({m31}, {m32}, {m33}, {m34}) " +
                $"({m41}, {m42}, {m43}, {m44})";
        }

        /// <summary>
        /// Returns formatted string representation of the matrix.
        /// </summary>
        public string ToString(string? str, IFormatProvider? formatProvider)
        {
            return
                $"({m11}, {m12}, {m13}, {m14}) " +
                $"({m21}, {m22}, {m23}, {m24}) " +
                $"({m31}, {m32}, {m33}, {m34}) " +
                $"({m41}, {m42}, {m43}, {m44})";
        }

        /// <summary>
        /// Compares matrices for equality.
        /// </summary>
        public bool Equals(Matrix4x4 other) => this == other;

        /// <summary>
        /// Compares matrices for equality.
        /// </summary>
        public override bool Equals(object? obj) => obj is Matrix4x4 m && Equals(m);

        /// <summary>
        /// Returns hash code for this matrix.
        /// </summary>
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(m11); hash.Add(m12); hash.Add(m13); hash.Add(m14);
            hash.Add(m21); hash.Add(m22); hash.Add(m23); hash.Add(m24);
            hash.Add(m31); hash.Add(m32); hash.Add(m33); hash.Add(m34);
            hash.Add(m41); hash.Add(m42); hash.Add(m43); hash.Add(m44);
            return hash.ToHashCode();
        }

        #endregion

        #region SharpDX conversion

        /// <summary>
        /// Converts DotEngine.Matrix4x4 to SharpDX.Matrix.
        /// </summary>
        public static implicit operator SharpDX.Matrix(Matrix4x4 matrix)
        {
            return new SharpDX.Matrix(
                matrix.m11, matrix.m12, matrix.m13, matrix.m14,
                matrix.m21, matrix.m22, matrix.m23, matrix.m24,
                matrix.m31, matrix.m32, matrix.m33, matrix.m34,
                matrix.m41, matrix.m42, matrix.m43, matrix.m44);
        }

        #endregion
    }
}
