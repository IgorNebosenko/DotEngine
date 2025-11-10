namespace DotEngine;

public struct Vector4
{
    public float x;
    public float y;
    public float z;
    public float w;
    
    public static implicit operator SharpDX.Vector4(Vector4 value)
    {
        return new SharpDX.Vector4(value.x, value.y, value.z, value.w);
    }
}