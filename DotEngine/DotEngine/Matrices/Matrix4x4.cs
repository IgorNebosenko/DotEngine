using SharpDX;

namespace DotEngine;

public class Matrix4x4
{
    public float m11;
    public float m12;
    public float m13;
    public float m14;
    public float m21;
    public float m22;
    public float m23;
    public float m24;
    public float m31;
    public float m32;
    public float m33;
    public float m34;
    public float m41;
    public float m42;
    public float m43;
    public float m44;
    
    public static implicit operator SharpDX.Matrix(Matrix4x4 matrix)
    {
        return new Matrix(
            matrix.m11, matrix.m12, matrix.m13, matrix.m14,
            matrix.m21, matrix.m22, matrix.m23, matrix.m24,
            matrix.m31, matrix.m32, matrix.m33, matrix.m34,
            matrix.m41, matrix.m42, matrix.m43, matrix.m44);
    }
}