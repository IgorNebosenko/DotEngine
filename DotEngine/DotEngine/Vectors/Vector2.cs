namespace DotEngine;

public struct Vector2
{
    public float x;
    public float y;
    
    public static implicit operator SharpDX.Vector2(Vector2 value)
    {
        return new SharpDX.Vector2(value.x, value.y);
    }
}