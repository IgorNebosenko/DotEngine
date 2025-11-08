using System.Runtime.InteropServices;

namespace DirectXLayer.Shaders;

[StructLayout(LayoutKind.Sequential)]
public struct Color32
{
    public byte r;
    public byte g;
    public byte b;
    public byte a;
}