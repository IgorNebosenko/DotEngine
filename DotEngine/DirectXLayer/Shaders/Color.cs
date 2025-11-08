using System.Runtime.InteropServices;

namespace DirectXLayer.Shaders;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct Color
{
    public float r;
    public float g;
    public float b;
    public float a;
    
    public static readonly Color White = new Color(1f, 1f, 1f, 1f);
    public static readonly Color LightGrey = new Color(0.75f, 0.75f, 0.75f, 1f);
    public static readonly Color Grey = new Color(0.5f, 0.5f, 0.5f, 1f);
    public static readonly Color DarkGrey = new Color(0.25f, 0.25f, 0.25f, 1f);
    public static readonly Color Black = new Color(0f, 0f, 0f, 1f);
    
    public static readonly Color Red = new Color(1f, 0f, 0f, 1f);
    public static readonly Color Orange = new Color(1f, 0.5f, 0f, 1f);
    public static readonly Color Yellow = new Color(1f, 1f, 0f, 1f);
    public static readonly Color Green = new Color(0f, 1f, 0f, 1f);
    public static readonly Color LightBlue = new Color(0f, 1f, 0.5f, 1f);
    public static readonly Color Cyan = new Color(0f, 1f, 1f, 1f);
    public static readonly Color Blue = new Color(0f, 0f, 1f, 1f);
    public static readonly Color Magenta = new Color(1f, 0f, 1f, 1f);
    
    public static readonly Color Clear = new Color(0f, 0f, 0f, 0f);

    public Color(float r, float g, float b, float a)
    {
        this.r  = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }
    
    public Color(float r, float g, float b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        a = 1f;
    }
}