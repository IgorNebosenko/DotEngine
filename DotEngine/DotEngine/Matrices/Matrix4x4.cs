using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpDX;

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
          /// <summary>Gets or sets the component at the specified index.</summary>
          /// <value>The value of the matrix component, depending on the index.</value>
          /// <param name="index">The zero-based index of the component to access.</param>
          /// <returns>The value of the component at the specified index.</returns>
          /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when the <paramref name="index" /> is out of the range [0, 15].</exception>
          public float this[int index]
          {
            get
            {
              switch (index)
              {
                case 0:
                  return m11;
                case 1:
                  return m12;
                case 2:
                  return m13;
                case 3:
                  return m14;
                case 4:
                  return m21;
                case 5:
                  return m22;
                case 6:
                  return m23;
                case 7:
                  return m24;
                case 8:
                  return m31;
                case 9:
                  return m32;
                case 10:
                  return m33;
                case 11:
                  return m34;
                case 12:
                  return m41;
                case 13:
                  return m42;
                case 14:
                  return m43;
                case 15:
                  return m44;
                default:
                  throw new ArgumentOutOfRangeException(nameof (index), "Indices for Matrix run from 0 to 15, inclusive.");
              }
            }
            set
            {
              switch (index)
              {
                case 0:
                  m11 = value;
                  break;
                case 1:
                  m12 = value;
                  break;
                case 2:
                  m13 = value;
                  break;
                case 3:
                  m14 = value;
                  break;
                case 4:
                  m21 = value;
                  break;
                case 5:
                  m22 = value;
                  break;
                case 6:
                  m23 = value;
                  break;
                case 7:
                  m24 = value;
                  break;
                case 8:
                  m31 = value;
                  break;
                case 9:
                  m32 = value;
                  break;
                case 10:
                  m33 = value;
                  break;
                case 11:
                  m34 = value;
                  break;
                case 12:
                  m41 = value;
                  break;
                case 13:
                  m42 = value;
                  break;
                case 14:
                  m43 = value;
                  break;
                case 15:
                  m44 = value;
                  break;
                default:
                  throw new ArgumentOutOfRangeException(nameof (index), "Indices for Matrix run from 0 to 15, inclusive.");
              }
            }
          }

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
        
        /// <summary>Determines the sum of two matrices.</summary>
        /// <param name="left">The first matrix to add.</param>
        /// <param name="right">The second matrix to add.</param>
        /// <param name="result">When the method completes, contains the sum of the two matrices.</param>
        public static void Add(ref Matrix4x4 left, ref Matrix4x4 right, out Matrix4x4 result)
        {
            result.m11 = left.m11 + right.m11;
            result.m12 = left.m12 + right.m12;
            result.m13 = left.m13 + right.m13;
            result.m14 = left.m14 + right.m14;
            result.m21 = left.m21 + right.m21;
            result.m22 = left.m22 + right.m22;
            result.m23 = left.m23 + right.m23;
            result.m24 = left.m24 + right.m24;
            result.m31 = left.m31 + right.m31;
            result.m32 = left.m32 + right.m32;
            result.m33 = left.m33 + right.m33;
            result.m34 = left.m34 + right.m34;
            result.m41 = left.m41 + right.m41;
            result.m42 = left.m42 + right.m42;
            result.m43 = left.m43 + right.m43;
            result.m44 = left.m44 + right.m44;
        }
        
        /// <summary>Determines the difference between two matrices.</summary>
        /// <param name="left">The first matrix to subtract.</param>
        /// <param name="right">The second matrix to subtract.</param>
        /// <returns>The difference between the two matrices.</returns>
        public static void Subtract(ref Matrix4x4 left, ref Matrix4x4 right, out Matrix4x4 result)
        {
            result.m11 = left.m11 - right.m11;
            result.m12 = left.m12 - right.m12;
            result.m13 = left.m13 - right.m13;
            result.m14 = left.m14 - right.m14;
            result.m21 = left.m21 - right.m21;
            result.m22 = left.m22 - right.m22;
            result.m23 = left.m23 - right.m23;
            result.m24 = left.m24 - right.m24;
            result.m31 = left.m31 - right.m31;
            result.m32 = left.m32 - right.m32;
            result.m33 = left.m33 - right.m33;
            result.m34 = left.m34 - right.m34;
            result.m41 = left.m41 - right.m41;
            result.m42 = left.m42 - right.m42;
            result.m43 = left.m43 - right.m43;
            result.m44 = left.m44 - right.m44;
        }
        
        /// <summary>Scales a matrix by the given value.</summary>
        /// <param name="left">The matrix to scale.</param>
        /// <param name="right">The amount by which to scale.</param>
        /// <param name="result">When the method completes, contains the scaled matrix.</param>
        public static void Multiply(ref Matrix4x4 left, float right, out Matrix4x4 result)
        {
            result.m11 = left.m11 * right;
            result.m12 = left.m12 * right;
            result.m13 = left.m13 * right;
            result.m14 = left.m14 * right;
            result.m21 = left.m21 * right;
            result.m22 = left.m22 * right;
            result.m23 = left.m23 * right;
            result.m24 = left.m24 * right;
            result.m31 = left.m31 * right;
            result.m32 = left.m32 * right;
            result.m33 = left.m33 * right;
            result.m34 = left.m34 * right;
            result.m41 = left.m41 * right;
            result.m42 = left.m42 * right;
            result.m43 = left.m43 * right;
            result.m44 = left.m44 * right;
        }
        
        /// <summary>Scales a matrix by the given value.</summary>
        /// <param name="left">The matrix to scale.</param>
        /// <param name="right">The amount by which to scale.</param>
        /// <returns>The scaled matrix.</returns>
        public static Matrix4x4 Multiply(Matrix4x4 left, float right)
        {
            Multiply(ref left, right, out var result);
            return result;
        }

        /// <summary>Determines the product of two matrices.</summary>
        /// <param name="left">The first matrix to multiply.</param>
        /// <param name="right">The second matrix to multiply.</param>
        /// <param name="result">The product of the two matrices.</param>
        public static void Multiply(ref Matrix4x4 left, ref Matrix4x4 right, out Matrix4x4 result)
        {
            result = new Matrix4x4()
            {
                m11 = (float)((double)left.m11 * right.m11 + (double)left.m12 * right.m21 + (double)left.m13 * right.m31 +
                              (double)left.m14 * right.m41),
                m12 = (float)((double)left.m11 * right.m12 + (double)left.m12 * right.m22 + (double)left.m13 * right.m32 +
                              (double)left.m14 * right.m42),
                m13 = (float)((double)left.m11 * right.m13 + (double)left.m12 * right.m23 + (double)left.m13 * right.m33 +
                              (double)left.m14 * right.m43),
                m14 = (float)((double)left.m11 * right.m14 + (double)left.m12 * right.m24 + (double)left.m13 * right.m34 +
                              (double)left.m14 * right.m44),
                m21 = (float)((double)left.m21 * right.m11 + (double)left.m22 * right.m21 + (double)left.m23 * right.m31 +
                              (double)left.m24 * right.m41),
                m22 = (float)((double)left.m21 * right.m12 + (double)left.m22 * right.m22 + (double)left.m23 * right.m32 +
                              (double)left.m24 * right.m42),
                m23 = (float)((double)left.m21 * right.m13 + (double)left.m22 * right.m23 + (double)left.m23 * right.m33 +
                              (double)left.m24 * right.m43),
                m24 = (float)((double)left.m21 * right.m14 + (double)left.m22 * right.m24 + (double)left.m23 * right.m34 +
                              (double)left.m24 * right.m44),
                m31 = (float)((double)left.m31 * right.m11 + (double)left.m32 * right.m21 + (double)left.m33 * right.m31 +
                              (double)left.m34 * right.m41),
                m32 = (float)((double)left.m31 * right.m12 +(double)left.m32 * right.m22 + (double)left.m33 * right.m32 +
                              (double)left.m34 * right.m42),
                m33 = (float)((double)left.m31 * right.m13 + (double)left.m32 * right.m23 + (double)left.m33 * right.m33 +
                              (double)left.m34 * right.m43),
                m34 = (float)((double)left.m31 * right.m14 + (double) left.m32 * right.m24 + (double)left.m33 * right.m34 +
                              (double)left.m34 * right.m44),
                m41 = (float)((double)left.m41 * right.m11 + (double)left.m42 * right.m21 + (double)left.m43 * right.m31 +
                              (double)left.m44 * right.m41),
                m42 = (float)((double)left.m41 * right.m12 +(double)left.m42 * right.m22 + (double)left.m43 * right.m32 +
                              (double)left.m44 * right.m42),
                m43 = (float)((double)left.m41 * right.m13 + (double)left.m42 * right.m23 + (double)left.m43 * right.m33 +
                              (double)left.m44 * right.m43),
                m44 = (float)((double)left.m41 * right.m14 + (double)left.m42 * right.m24 + (double)left.m43 * right.m34 +
                              (double)left.m44 * right.m44)
            };
        }

        /// <summary>Determines the product of two matrices.</summary>
        /// <param name="left">The first matrix to multiply.</param>
        /// <param name="right">The second matrix to multiply.</param>
        /// <returns>The product of the two matrices.</returns>
        public static Matrix4x4 Multiply(Matrix4x4 left, Matrix4x4 right)
        {
            Multiply(ref left, ref right, out var result);
            return result;
        }
        
        /// <summary>Scales a matrix by the given value.</summary>
        /// <param name="left">The matrix to scale.</param>
        /// <param name="right">The amount by which to scale.</param>
        /// <param name="result">When the method completes, contains the scaled matrix.</param>
        public static void Divide(ref Matrix4x4 left, float right, out Matrix4x4 result)
        {
            var num = 1 / right;
            
            result.m11 = left.m11 * num;
            result.m12 = left.m12 * num;
            result.m13 = left.m13 * num;
            result.m14 = left.m14 * num;
            result.m21 = left.m21 * num;
            result.m22 = left.m22 * num;
            result.m23 = left.m23 * num;
            result.m24 = left.m24 * num;
            result.m31 = left.m31 * num;
            result.m32 = left.m32 * num;
            result.m33 = left.m33 * num;
            result.m34 = left.m34 * num;
            result.m41 = left.m41 * num;
            result.m42 = left.m42 * num;
            result.m43 = left.m43 * num;
            result.m44 = left.m44 * num;
        }
        
        /// <summary>Scales a matrix by the given value.</summary>
        /// <param name="left">The matrix to scale.</param>
        /// <param name="right">The amount by which to scale.</param>
        /// <returns>The scaled matrix.</returns>
        public static Matrix4x4 Divide(Matrix4x4 left, float right)
        {
            Divide(ref left, right, out var result);
            return result;
        }
        
        /// <summary>Determines the quotient of two matrices.</summary>
        /// <param name="left">The first matrix to divide.</param>
        /// <param name="right">The second matrix to divide.</param>
        /// <param name="result">When the method completes, contains the quotient of the two matrices.</param>
        public static void Divide(ref Matrix4x4 left, ref Matrix4x4 right, out Matrix4x4 result)
        {
            result.m11 = left.m11 / right.m11;
            result.m12 = left.m12 / right.m12;
            result.m13 = left.m13 / right.m13;
            result.m14 = left.m14 / right.m14;
            result.m21 = left.m21 / right.m21;
            result.m22 = left.m22 / right.m22;
            result.m23 = left.m23 / right.m23;
            result.m24 = left.m24 / right.m24;
            result.m31 = left.m31 / right.m31;
            result.m32 = left.m32 / right.m32;
            result.m33 = left.m33 / right.m33;
            result.m34 = left.m34 / right.m34;
            result.m41 = left.m41 / right.m41;
            result.m42 = left.m42 / right.m42;
            result.m43 = left.m43 / right.m43;
            result.m44 = left.m44 / right.m44;
        }
        
        /// <summary>Determines the quotient of two matrices.</summary>
        /// <param name="left">The first matrix to divide.</param>
        /// <param name="right">The second matrix to divide.</param>
        /// <returns>The quotient of the two matrices.</returns>
        public static Matrix4x4 Divide(Matrix4x4 left, Matrix4x4 right)
        {
            Divide(ref left, ref right, out var result);
            return result;
        }
        
        /// <summary>Performs the exponential operation on a matrix.</summary>
        /// <param name="value">The matrix to perform the operation on.</param>
        /// <param name="exponent">The exponent to raise the matrix to.</param>
        /// <param name="result">When the method completes, contains the exponential matrix.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when the <paramref name="exponent" /> is negative.</exception>
        public static void Exponent(ref Matrix4x4 value, int exponent, out Matrix4x4 result)
        {
            if (exponent < 0)
                throw new ArgumentOutOfRangeException(nameof (exponent), "The exponent can not be negative.");
            if (exponent == 0)
                result = Matrix4x4.Identity;
            else if (exponent == 1)
            {
                result = value;
            }
            else
            {
                var identity = Matrix4x4.Identity;
                var matrix = value;
                while (true)
                {
                    if ((exponent & 1) != 0)
                        identity *= matrix;
                    exponent /= 2;
                    if (exponent > 0)
                        matrix *= matrix;
                    else
                        break;
                }
                result = identity;
            }
        }
        
        /// <summary>Performs the exponential operation on a matrix.</summary>
        /// <param name="value">The matrix to perform the operation on.</param>
        /// <param name="exponent">The exponent to raise the matrix to.</param>
        /// <returns>The exponential matrix.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown when the <paramref name="exponent" /> is negative.</exception>
        public static Matrix4x4 Exponent(Matrix4x4 value, int exponent)
        {
            Exponent(ref value, exponent, out var result);
            return result;
        }
        
        /// <summary>Negates a matrix.</summary>
        /// <param name="value">The matrix to be negated.</param>
        /// <param name="result">When the method completes, contains the negated matrix.</param>
        public static void Negate(ref Matrix4x4 value, out Matrix4x4 result)
        {
            result.m11 = -value.m11;
            result.m12 = -value.m12;
            result.m13 = -value.m13;
            result.m14 = -value.m14;
            result.m21 = -value.m21;
            result.m22 = -value.m22;
            result.m23 = -value.m23;
            result.m24 = -value.m24;
            result.m31 = -value.m31;
            result.m32 = -value.m32;
            result.m33 = -value.m33;
            result.m34 = -value.m34;
            result.m41 = -value.m41;
            result.m42 = -value.m42;
            result.m43 = -value.m43;
            result.m44 = -value.m44;
        }
        
        /// <summary>Negates a matrix.</summary>
        /// <param name="value">The matrix to be negated.</param>
        /// <returns>The negated matrix.</returns>
        public static Matrix4x4 Negate(Matrix4x4 value)
        {
            Negate(ref value, out var result);
            return result;
        }
        
        /// <summary>Performs a linear interpolation between two matrices.</summary>
        /// <param name="start">Start matrix.</param>
        /// <param name="end">End matrix.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end" />.</param>
        /// <param name="result">When the method completes, contains the linear interpolation of the two matrices.</param>
        /// <remarks>
        /// Passing <paramref name="amount" /> a value of 0 will cause <paramref name="start" /> to be returned; a value of 1 will cause <paramref name="end" /> to be returned.
        /// </remarks>
        public static void Lerp(ref Matrix4x4 start, ref Matrix4x4 end, float amount, out Matrix4x4 result)
        {
            result.m11 = MathUtil.Lerp(start.m11, end.m11, amount);
            result.m12 = MathUtil.Lerp(start.m12, end.m12, amount);
            result.m13 = MathUtil.Lerp(start.m13, end.m13, amount);
            result.m14 = MathUtil.Lerp(start.m14, end.m14, amount);
            result.m21 = MathUtil.Lerp(start.m21, end.m21, amount);
            result.m22 = MathUtil.Lerp(start.m22, end.m22, amount);
            result.m23 = MathUtil.Lerp(start.m23, end.m23, amount);
            result.m24 = MathUtil.Lerp(start.m24, end.m24, amount);
            result.m31 = MathUtil.Lerp(start.m31, end.m31, amount);
            result.m32 = MathUtil.Lerp(start.m32, end.m32, amount);
            result.m33 = MathUtil.Lerp(start.m33, end.m33, amount);
            result.m34 = MathUtil.Lerp(start.m34, end.m34, amount);
            result.m41 = MathUtil.Lerp(start.m41, end.m41, amount);
            result.m42 = MathUtil.Lerp(start.m42, end.m42, amount);
            result.m43 = MathUtil.Lerp(start.m43, end.m43, amount);
            result.m44 = MathUtil.Lerp(start.m44, end.m44, amount);
        }
        
        /// <summary>Performs a linear interpolation between two matrices.</summary>
        /// <param name="start">Start matrix.</param>
        /// <param name="end">End matrix.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end" />.</param>
        /// <returns>The linear interpolation of the two matrices.</returns>
        /// <remarks>
        /// Passing <paramref name="amount" /> a value of 0 will cause <paramref name="start" /> to be returned; a value of 1 will cause <paramref name="end" /> to be returned.
        /// </remarks>
        public static Matrix4x4 Lerp(Matrix4x4 start, Matrix4x4 end, float amount)
        {
            Lerp(ref start, ref end, amount, out var result);
            return result;
        }
        
        /// <summary>Performs a cubic interpolation between two matrices.</summary>
        /// <param name="start">Start matrix.</param>
        /// <param name="end">End matrix.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end" />.</param>
        /// <param name="result">When the method completes, contains the cubic interpolation of the two matrices.</param>
        public static void SmoothStep(ref Matrix4x4 start, ref Matrix4x4 end, float amount, out Matrix4x4 result)
        {
            amount = MathUtil.SmoothStep(amount);
            Lerp(ref start, ref end, amount, out result);
        }
        
        /// <summary>Performs a cubic interpolation between two matrices.</summary>
        /// <param name="start">Start matrix.</param>
        /// <param name="end">End matrix.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end" />.</param>
        /// <returns>The cubic interpolation of the two matrices.</returns>
        public static Matrix4x4 SmoothStep(Matrix4x4 start, Matrix4x4 end, float amount)
        {
            SmoothStep(ref start, ref end, amount, out var result);
            return result;
        }

        /// <summary>Calculates the transpose of the specified matrix.</summary>
        /// <param name="value">The matrix whose transpose is to be calculated.</param>
        /// <param name="result">When the method completes, contains the transpose of the specified matrix.</param>
        public static void Transpose(ref Matrix4x4 value, out Matrix4x4 result)
        {
            result = new Matrix4x4()
            {
                m11 = value.m11,
                m12 = value.m21,
                m13 = value.m31,
                m14 = value.m41,
                m21 = value.m12,
                m22 = value.m22,
                m23 = value.m32,
                m24 = value.m42,
                m31 = value.m13,
                m32 = value.m23,
                m33 = value.m33,
                m34 = value.m43,
                m41 = value.m14,
                m42 = value.m24,
                m43 = value.m34,
                m44 = value.m44
            };
        }
        
        /// <summary>Calculates the transpose of the specified matrix.</summary>
        /// <param name="value">The matrix whose transpose is to be calculated.</param>
        /// <param name="result">When the method completes, contains the transpose of the specified matrix.</param>
        public static void TransposeByRef(ref Matrix4x4 value, ref Matrix4x4 result)
        {
            result.m11 = value.m11;
            result.m12 = value.m21;
            result.m13 = value.m31;
            result.m14 = value.m41;
            result.m21 = value.m12;
            result.m22 = value.m22;
            result.m23 = value.m32;
            result.m24 = value.m42;
            result.m31 = value.m13;
            result.m32 = value.m23;
            result.m33 = value.m33;
            result.m34 = value.m43;
            result.m41 = value.m14;
            result.m42 = value.m24;
            result.m43 = value.m34;
            result.m44 = value.m44;
        }

        /// <summary>Calculates the inverse of the specified matrix.</summary>
        /// <param name="value">The matrix whose inverse is to be calculated.</param>
        /// <param name="result">When the method completes, contains the inverse of the specified matrix.</param>
        public static void Invert(ref Matrix4x4 value, out Matrix4x4 result)
        {
            var num1 = (float)((double)value.m31 * value.m42 - (double)value.m32 * value.m41);
            var num2 = (float)((double)value.m31 * value.m43 - (double)value.m33 * value.m41);
            var num3 = (float)((double)value.m34 * value.m41 - (double)value.m31 * value.m44);
            var num4 = (float)((double)value.m32 * value.m43 - (double)value.m33 * value.m42);
            var num5 = (float)((double)value.m34 * value.m42 - (double)value.m32 * value.m44);
            var num6 = (float)((double)value.m33 * value.m44 - (double)value.m34 * value.m43);
            var num7 = (float)((double)value.m22 * num6 + (double)value.m23 * num5 + (double)value.m24 * num4);
            var num8 = (float)((double)value.m21 * num6 + (double)value.m23 * num3 + (double)value.m24 * num2);
            var num9 = (float)((double)value.m21 * -num5 + (double)value.m22 * num3 + (double)value.m24 * num1);
            var num10 = (float)((double)value.m21 * num4 + (double)value.m22 * -num2 + (double)value.m23 * num1);
            var num11 = (float)((double)value.m11 * num7 - (double)value.m12 * num8 + (double)value.m13 * num9 -
                                (double)value.m14 * num10);
            if (IsCloseToZero(num11))
            {
                result = Matrix4x4.Zero;
            }
            else
            {
                var num12 = 1f / num11;
                var num13 = (float)((double)value.m11 * value.m22 - (double)value.m12 * value.m21);
                var num14 = (float)((double)value.m11 * value.m23 - (double)value.m13 * value.m21);
                var num15 = (float)((double)value.m14 * value.m21 - (double)value.m11 * value.m24);
                var num16 = (float)((double)value.m12 * value.m23 - (double)value.m13 * value.m22);
                var num17 = (float)((double)value.m14 * value.m22 - (double)value.m12 * value.m24);
                var num18 = (float)((double)value.m13 * value.m24 - (double)value.m14 * value.m23);
                var num19 = (float)((double)value.m12 * num6 + (double)value.m13 * num5 +
                                    (double)value.m14 * num4);
                var num20 = (float)((double)value.m11 * num6 + (double)value.m13 * num3 +
                                    (double)value.m14 * num2);
                var num21 = (float)((double)value.m11 * -num5 + (double)value.m12 * num3 +
                                    (double)value.m14 * num1);
                var num22 = (float)((double)value.m11 * num4 + (double)value.m12 * -num2 +
                                    (double)value.m13 * num1);
                var num23 = (float)((double)value.m42 * num18 + (double)value.m43 * num17 +
                                    (double)value.m44 * num16);
                var num24 = (float)((double)value.m41 * num18 + (double)value.m43 * num15 +
                                    (double)value.m44 * num14);
                var num25 = (float)((double)value.m41 * -num17 + (double)value.m42 * num15 +
                                    (double)value.m44 * num13);
                var num26 = (float)((double)value.m41 * num16 + (double)value.m42 * -num14 +
                                    (double)value.m43 * num13);
                var num27 = (float)((double)value.m32 * num18 + (double)value.m33 * num17 +
                                    (double)value.m34 * num16);
                var num28 = (float)((double)value.m31 * num18 + (double)value.m33 * num15 +
                                    (double)value.m34 * num14);
                var num29 = (float)((double)value.m31 * -num17 + (double)value.m32 * num15 +
                                    (double)value.m34 * num13);
                var num30 = (float)((double)value.m31 * num16 + (double)value.m32 * -num14 +
                                    (double)value.m33 * num13);
                result.m11 = num7 * num12;
                result.m12 = -num19 * num12;
                result.m13 = num23 * num12;
                result.m14 = -num27 * num12;
                result.m21 = -num8 * num12;
                result.m22 = num20 * num12;
                result.m23 = -num24 * num12;
                result.m24 = num28 * num12;
                result.m31 = num9 * num12;
                result.m32 = -num21 * num12;
                result.m33 = num25 * num12;
                result.m34 = -num29 * num12;
                result.m41 = -num10 * num12;
                result.m42 = num22 * num12;
                result.m43 = -num26 * num12;
                result.m44 = num30 * num12;
            }
        }
        
        public static Matrix4x4 Invert(Matrix4x4 value)
        {
            value.Invert();
            return value;
        }
        
        public static void Orthogonalize(ref Matrix4x4 value, out Matrix4x4 result)
        {
            result = value;
            result.Row2 -= Vector4.Dot(result.Row1, result.Row2) / Vector4.Dot(result.Row1, result.Row1) * result.Row1;
            result.Row3 -= Vector4.Dot(result.Row1, result.Row3) / Vector4.Dot(result.Row1, result.Row1) * result.Row1;
            result.Row3 -= Vector4.Dot(result.Row2, result.Row3) / Vector4.Dot(result.Row2, result.Row2) * result.Row2;
            result.Row4 -= Vector4.Dot(result.Row1, result.Row4) / Vector4.Dot(result.Row1, result.Row1) * result.Row1;
            result.Row4 -= Vector4.Dot(result.Row2, result.Row4) / Vector4.Dot(result.Row2, result.Row2) * result.Row2;
            result.Row4 -= Vector4.Dot(result.Row3, result.Row4) / Vector4.Dot(result.Row3, result.Row3) * result.Row3;
        }
        
        /// <summary>Orthogonalizes the specified matrix.</summary>
        /// <param name="value">The matrix to orthogonalize.</param>
        /// <returns>The orthogonalized matrix.</returns>
        /// <remarks>
        /// <para>Orthogonalization is the process of making all rows orthogonal to each other. This
        /// means that any given row in the matrix will be orthogonal to any other given row in the
        /// matrix.</para>
        /// <para>Because this method uses the modified Gram-Schmidt process, the resulting matrix
        /// tends to be numerically unstable. The numeric stability decreases according to the rows
        /// so that the first row is the most stable and the last row is the least stable.</para>
        /// <para>This operation is performed on the rows of the matrix rather than the columns.
        /// If you wish for this operation to be performed on the columns, first transpose the
        /// input and than transpose the output.</para>
        /// </remarks>
        public static Matrix4x4 Orthogonalize(Matrix4x4 value)
        {
            Orthogonalize(ref value, out var result);
            return result;
        }
        
        /// <summary>Orthonormalizes the specified matrix.</summary>
        /// <param name="value">The matrix to orthonormalize.</param>
        /// <param name="result">When the method completes, contains the orthonormalized matrix.</param>
        /// <remarks>
        /// <para>Orthonormalization is the process of making all rows and columns orthogonal to each
        /// other and making all rows and columns of unit length. This means that any given row will
        /// be orthogonal to any other given row and any given column will be orthogonal to any other
        /// given column. Any given row will not be orthogonal to any given column. Every row and every
        /// column will be of unit length.</para>
        /// <para>Because this method uses the modified Gram-Schmidt process, the resulting matrix
        /// tends to be numerically unstable. The numeric stability decreases according to the rows
        /// so that the first row is the most stable and the last row is the least stable.</para>
        /// <para>This operation is performed on the rows of the matrix rather than the columns.
        /// If you wish for this operation to be performed on the columns, first transpose the
        /// input and than transpose the output.</para>
        /// </remarks>
        public static void Orthonormalize(ref Matrix4x4 value, out Matrix4x4 result)
        {
            result = value;
            result.Row1 = Vector4.Normalize(result.Row1);
            result.Row2 -= Vector4.Dot(result.Row1, result.Row2) * result.Row1;
            result.Row2 = Vector4.Normalize(result.Row2);
            result.Row3 -= Vector4.Dot(result.Row1, result.Row3) * result.Row1;
            result.Row3 -= Vector4.Dot(result.Row2, result.Row3) * result.Row2;
            result.Row3 = Vector4.Normalize(result.Row3);
            result.Row4 -= Vector4.Dot(result.Row1, result.Row4) * result.Row1;
            result.Row4 -= Vector4.Dot(result.Row2, result.Row4) * result.Row2;
            result.Row4 -= Vector4.Dot(result.Row3, result.Row4) * result.Row3;
            result.Row4 = Vector4.Normalize(result.Row4);
        }
        
        /// <summary>Performs a linear interpolation between two vectors.</summary>
        /// <param name="start">Start vector.</param>
        /// <param name="end">End vector.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end" />.</param>
        /// <param name="result">When the method completes, contains the linear interpolation of the two vectors.</param>
        /// <remarks>
        /// Passing <paramref name="amount" /> a value of 0 will cause <paramref name="start" /> to be returned; a value of 1 will cause <paramref name="end" /> to be returned.
        /// </remarks>
        public static void Lerp(ref Vector4 start, ref Vector4 end, float amount, out Vector4 result)
        {
            result.x = MathUtil.Lerp(start.x, end.x, amount);
            result.y = MathUtil.Lerp(start.y, end.y, amount);
            result.z = MathUtil.Lerp(start.z, end.z, amount);
            result.w = MathUtil.Lerp(start.w, end.w, amount);
        }
        
        /// <summary>Performs a linear interpolation between two vectors.</summary>
        /// <param name="start">Start vector.</param>
        /// <param name="end">End vector.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end" />.</param>
        /// <returns>The linear interpolation of the two vectors.</returns>
        /// <remarks>
        /// Passing <paramref name="amount" /> a value of 0 will cause <paramref name="start" /> to be returned; a value of 1 will cause <paramref name="end" /> to be returned.
        /// </remarks>
        public static Vector4 Lerp(Vector4 start, Vector4 end, float amount)
        {
            Vector4.Lerp(ref start, ref end, amount, out var result);
            return result;
        }
        
        /// <summary>Orthonormalizes the specified matrix.</summary>
        /// <param name="value">The matrix to orthonormalize.</param>
        /// <returns>The orthonormalized matrix.</returns>
        /// <remarks>
        /// <para>Orthonormalization is the process of making all rows and columns orthogonal to each
        /// other and making all rows and columns of unit length. This means that any given row will
        /// be orthogonal to any other given row and any given column will be orthogonal to any other
        /// given column. Any given row will not be orthogonal to any given column. Every row and every
        /// column will be of unit length.</para>
        /// <para>Because this method uses the modified Gram-Schmidt process, the resulting matrix
        /// tends to be numerically unstable. The numeric stability decreases according to the rows
        /// so that the first row is the most stable and the last row is the least stable.</para>
        /// <para>This operation is performed on the rows of the matrix rather than the columns.
        /// If you wish for this operation to be performed on the columns, first transpose the
        /// input and than transpose the output.</para>
        /// </remarks>
        public static Matrix4x4 Orthonormalize(Matrix4x4 value)
        {
            Orthonormalize(ref value, out var result);
            return result;
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

        #region Methods

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

        /// <summary>
        /// Inverts matrix
        /// </summary>
        public void Invert()
        {
            Matrix4x4.Inverse(ref this, out this);
        }

        /// <summary>
        /// Transposes matrix
        /// </summary>
        public void Transpose()
        {
            Matrix4x4.Transpose(ref this, out this);
        }
        
        /// <summary>Orthogonalizes the specified matrix.</summary>
        /// <remarks>
        /// <para>Orthogonalization is the process of making all rows orthogonal to each other. This
        /// means that any given row in the matrix will be orthogonal to any other given row in the
        /// matrix.</para>
        /// <para>Because this method uses the modified Gram-Schmidt process, the resulting matrix
        /// tends to be numerically unstable. The numeric stability decreases according to the rows
        /// so that the first row is the most stable and the last row is the least stable.</para>
        /// <para>This operation is performed on the rows of the matrix rather than the columns.
        /// If you wish for this operation to be performed on the columns, first transpose the
        /// input and than transpose the output.</para>
        /// </remarks>
        public void Orthogonalize() => Matrix4x4.Orthogonalize(ref this, out this);

        /// <summary>Orthonormalizes the specified matrix.</summary>
        /// <remarks>
        /// <para>Orthonormalization is the process of making all rows and columns orthogonal to each
        /// other and making all rows and columns of unit length. This means that any given row will
        /// be orthogonal to any other given row and any given column will be orthogonal to any other
        /// given column. Any given row will not be orthogonal to any given column. Every row and every
        /// column will be of unit length.</para>
        /// <para>Because this method uses the modified Gram-Schmidt process, the resulting matrix
        /// tends to be numerically unstable. The numeric stability decreases according to the rows
        /// so that the first row is the most stable and the last row is the least stable.</para>
        /// <para>This operation is performed on the rows of the matrix rather than the columns.
        /// If you wish for this operation to be performed on the columns, first transpose the
        /// input and than transpose the output.</para>
        /// </remarks>
        public void Orthonormalize() => Matrix4x4.Orthonormalize(ref this, out this);

        /// <summary>
        /// Decomposes a matrix into an orthonormalized matrix Q and a right triangular matrix R.
        /// </summary>
        /// <param name="q">When the method completes, contains the orthonormalized matrix of the decomposition.</param>
        /// <param name="r">When the method completes, contains the right triangular matrix of the decomposition.</param>
        public void DecomposeQR(out Matrix4x4 q, out Matrix4x4 r)
        {
            var matrix = this;
            matrix.Transpose();
            Matrix4x4.Orthonormalize(ref matrix, out q);
            q.Transpose();
            r = new Matrix4x4();

            r.m11 = Vector4.Dot(q.Column1, Column1);
            r.m12 = Vector4.Dot(q.Column1, Column2);
            r.m13 = Vector4.Dot(q.Column1, Column3);
            r.m14 = Vector4.Dot(q.Column1, Column4);
            r.m22 = Vector4.Dot(q.Column2, Column2);
            r.m23 = Vector4.Dot(q.Column2, Column3);
            r.m24 = Vector4.Dot(q.Column2, Column4);
            r.m33 = Vector4.Dot(q.Column3, Column3);
            r.m34 = Vector4.Dot(q.Column3, Column4);
            r.m44 = Vector4.Dot(q.Column4, Column4);
        }
        
        /// <summary>
        /// Decomposes a matrix into a lower triangular matrix L and an orthonormalized matrix Q.
        /// </summary>
        /// <param name="l">When the method completes, contains the lower triangular matrix of the decomposition.</param>
        /// <param name="q">When the method completes, contains the orthonormalized matrix of the decomposition.</param>
        public void DecomposeLQ(out Matrix4x4 l, out Matrix4x4 q)
        {
            Matrix4x4.Orthonormalize(ref this, out q);
            l = new Matrix4x4();
            l.m11 = Vector4.Dot(q.Row1, Row1);
            l.m21 = Vector4.Dot(q.Row1, Row2);
            l.m22 = Vector4.Dot(q.Row2, Row2);
            l.m31 = Vector4.Dot(q.Row1, Row3);
            l.m32 = Vector4.Dot(q.Row2, Row3);
            l.m33 = Vector4.Dot(q.Row3, Row3);
            l.m41 = Vector4.Dot(q.Row1, Row4);
            l.m42 = Vector4.Dot(q.Row2, Row4);
            l.m43 = Vector4.Dot(q.Row3, Row4);
            l.m44 = Vector4.Dot(q.Row4, Row4);
        }
        
        /// <summary>
        /// Decomposes a matrix into a scale, rotation, and translation.
        /// </summary>
        /// <param name="scale">When the method completes, contains the scaling component of the decomposed matrix.</param>
        /// <param name="rotation">When the method completes, contains the rotation component of the decomposed matrix.</param>
        /// <param name="translation">When the method completes, contains the translation component of the decomposed matrix.</param>
        /// <remarks>
        /// This method is designed to decompose an SRT transformation matrix only.
        /// </remarks>
        public bool Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            translation.x = m41;
            translation.y = m42;
            translation.z = m43;
            scale.x = (float) Math.Sqrt((double) m11 * m11 + (double)m12 * m12 + (double)m13 * m13);
            scale.y = (float) Math.Sqrt((double) m21 * m21 + (double)m22 * m22 + (double)m23 * m23);
            scale.z = (float) Math.Sqrt((double) m31 * m31 + (double)m32 * m32 + (double)m33 * m33);
            if (IsCloseToZero(scale.x) || IsCloseToZero(scale.y) || IsCloseToZero(scale.z))
            {
                rotation = Quaternion.Identity;
                return false;
            }

            var matrix = new Matrix4x4()
            {
                m11 = m11 / scale.x,
                m12 = m12 / scale.x,
                m13 = m13 / scale.x,
                m21 = m21 / scale.y,
                m22 = m22 / scale.y,
                m23 = m23 / scale.y,
                m31 = m31 / scale.z,
                m32 = m32 / scale.z,
                m33 = m33 / scale.z,
                m44 = 1f
            };
            Quaternion.RotationMatrix(ref matrix, out rotation);
            return true;
        }
        
        /// <summary>
        /// Decomposes a uniform scale matrix into a scale, rotation, and translation.
        /// A uniform scale matrix has the same scale in every axis.
        /// </summary>
        /// <param name="scale">When the method completes, contains the scaling component of the decomposed matrix.</param>
        /// <param name="rotation">When the method completes, contains the rotation component of the decomposed matrix.</param>
        /// <param name="translation">When the method completes, contains the translation component of the decomposed matrix.</param>
        /// <remarks>
        /// This method is designed to decompose only an SRT transformation matrix that has the same scale in every axis.
        /// </remarks>
        public bool DecomposeUniformScale(
            out float scale,
            out Quaternion rotation,
            out Vector3 translation)
        {
            translation.x = m41;
            translation.y = m42;
            translation.z = m43;
            scale = (float) Math.Sqrt((double) m11 * m11 + (double)m12 * m12 + (double)m13 * m13);
            var num = 1f / scale;
            
            if (IsCloseToZero(scale))
            {
                rotation = Quaternion.Identity;
                return false;
            }

            var matrix = new Matrix4x4()
            {
                m11 = m11 * num,
                m12 = m12 * num,
                m13 = m13 * num,
                m21 = m21 * num,
                m22 = m22 * num,
                m23 = m23 * num,
                m31 = m31 * num,
                m32 = m32 * num,
                m33 = m33 * num,
                m44 = 1f
            };
            Quaternion.RotationMatrix(ref matrix, out rotation);
            return true;
        }
        
        /// <summary>Exchanges two rows in the matrix.</summary>
        /// <param name="firstRow">The first row to exchange. This is an index of the row starting at zero.</param>
        /// <param name="secondRow">The second row to exchange. This is an index of the row starting at zero.</param>
        public void ExchangeRows(int firstRow, int secondRow)
        {
            if (firstRow < 0)
                throw new ArgumentOutOfRangeException(nameof (firstRow), "The parameter firstRow must be greater than or equal to zero.");
            if (firstRow > 3)
                throw new ArgumentOutOfRangeException(nameof (firstRow), "The parameter firstRow must be less than or equal to three.");
            if (secondRow < 0)
                throw new ArgumentOutOfRangeException(nameof (secondRow), "The parameter secondRow must be greater than or equal to zero.");
            if (secondRow > 3)
                throw new ArgumentOutOfRangeException(nameof (secondRow), "The parameter secondRow must be less than or equal to three.");
            if (firstRow == secondRow)
                return;
            
            var num1 = this[secondRow, 0];
            var num2 = this[secondRow, 1];
            var num3 = this[secondRow, 2];
            var num4 = this[secondRow, 3];
            
            this[secondRow, 0] = this[firstRow, 0];
            this[secondRow, 1] = this[firstRow, 1];
            this[secondRow, 2] = this[firstRow, 2];
            this[secondRow, 3] = this[firstRow, 3];
            
            this[firstRow, 0] = num1;
            this[firstRow, 1] = num2;
            this[firstRow, 2] = num3;
            this[firstRow, 3] = num4;
        }
        
        /// <summary>Exchanges two columns in the matrix.</summary>
        /// <param name="firstColumn">The first column to exchange. This is an index of the column starting at zero.</param>
        /// <param name="secondColumn">The second column to exchange. This is an index of the column starting at zero.</param>
        public void ExchangeColumns(int firstColumn, int secondColumn)
        {
            if (firstColumn < 0)
                throw new ArgumentOutOfRangeException(nameof (firstColumn), "The parameter firstColumn must be greater than or equal to zero.");
            if (firstColumn > 3)
                throw new ArgumentOutOfRangeException(nameof (firstColumn), "The parameter firstColumn must be less than or equal to three.");
            if (secondColumn < 0)
                throw new ArgumentOutOfRangeException(nameof (secondColumn), "The parameter secondColumn must be greater than or equal to zero.");
            if (secondColumn > 3)
                throw new ArgumentOutOfRangeException(nameof (secondColumn), "The parameter secondColumn must be less than or equal to three.");
            if (firstColumn == secondColumn)
                return;
            
            var num1 = this[0, secondColumn];
            var num2 = this[1, secondColumn];
            var num3 = this[2, secondColumn];
            var num4 = this[3, secondColumn];
            
            this[0, secondColumn] = this[0, firstColumn];
            this[1, secondColumn] = this[1, firstColumn];
            this[2, secondColumn] = this[2, firstColumn];
            this[3, secondColumn] = this[3, firstColumn];
            
            this[0, firstColumn] = num1;
            this[1, firstColumn] = num2;
            this[2, firstColumn] = num3;
            this[3, firstColumn] = num4;
        }
        
        /// <summary>
        /// Creates an array containing the elements of the matrix.
        /// </summary>
        /// <returns>A sixteen-element array containing the components of the matrix.</returns>
        public float[] ToArray()
        {
            return new []
            {
                m11, m12, m13, m14, m21, m22, m23, m24,
                m31, m32, m33, m34, m41, m42, m43, m44
            };
        }
        
        /// <summary>Determines the sum of two matrices.</summary>
        /// <param name="left">The first matrix to add.</param>
        /// <param name="right">The second matrix to add.</param>
        /// <returns>The sum of the two matrices.</returns>
        public static Matrix4x4 Add(Matrix4x4 left, Matrix4x4 right)
        {
            Add(ref left, ref right, out var result);
            return result;
        }

        /// <summary>Determines the difference between two matrices.</summary>
        /// <param name="left">The first matrix to subtract.</param>
        /// <param name="right">The second matrix to subtract.</param>
        /// <returns>The difference between the two matrices.</returns>
        public static Matrix4x4 Subtract(Matrix4x4 left, Matrix4x4 right)
        {
            Subtract(ref left, ref right, out var result);
            return result;
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
        
        #region Misc methodes

        private static bool IsCloseToZero(float val)
        {
            return (double) Math.Abs(val) < 9.999999974752427E-07;
        }

        #endregion
    }
}
