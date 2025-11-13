namespace DotEngine;

public struct Vector3
{
    public float x;
    public float y;
    public float z;

    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static implicit operator SharpDX.Vector3(Vector3 value)
    {
        return new SharpDX.Vector3(value.x, value.y, value.z);
    }
}