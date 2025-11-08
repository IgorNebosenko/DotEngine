using System.Runtime.InteropServices;

namespace DirectXLayer.Shaders;

[StructLayout(LayoutKind.Sequential)]
public struct Color32
{
    public byte r;
    public byte g;
    public byte b;
    public byte a;
    
    public static readonly Color32 White = new Color32(255, 255, 255, 255);
    public static readonly Color32 LightGrey = new Color32(192, 192, 192, 255);
    public static readonly Color32 Grey = new Color32(128, 128, 128, 255);
    public static readonly Color32 DarkGrey = new Color32(92, 92, 92, 255);
    public static readonly Color32 Black = new Color32(0, 0, 0, 255);
    
    public static readonly Color32 Red = new Color32(255, 0, 0, 255);
    public static readonly Color32 Orange = new Color32(255, 128, 0, 255);
    public static readonly Color32 Yellow = new Color32(255, 255, 0, 255);
    public static readonly Color32 Green = new Color32(0, 255, 0, 255);
    public static readonly Color32 LightBlue = new Color32(0, 255, 128, 255);
    public static readonly Color32 Cyan = new Color32(0, 255, 255, 255);
    public static readonly Color32 Blue = new Color32(0, 0, 255, 255);
    public static readonly Color32 Magenta = new Color32(255, 0, 255, 255);
    
    public static readonly Color32 Clear = new Color32(0, 0, 0, 0);

    public Color32(byte r, byte g, byte b, byte a)
    {
        this.r  = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }
    
    public Color32(byte r, byte g, byte b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        a = byte.MaxValue;
    }
}