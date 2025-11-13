namespace DotEngine;

public struct Quaternion
{
    public float x;
    public float y;
    public float z;
    public float w;
    
    public static implicit operator SharpDX.Quaternion(Quaternion value)
    {
        return new SharpDX.Quaternion(value.x, value.y, value.z, value.w);
    }
}