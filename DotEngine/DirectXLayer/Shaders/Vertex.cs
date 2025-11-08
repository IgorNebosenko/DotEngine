using System.Runtime.InteropServices;
using SharpDX;

namespace DirectXLayer.Shaders;

[StructLayout(LayoutKind.Sequential)]
public struct Vertex
{
    public Vector3 position;
    public Color color;
}