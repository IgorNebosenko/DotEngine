using System.Runtime.InteropServices;
using SharpDX;

namespace DirectXLayer.Shaders;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct Vertex
{
    public Vector3 position;
    public Color color;
}