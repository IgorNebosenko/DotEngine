using System.Runtime.InteropServices;
using SharpDX;

namespace DirectXLayer.Data;

[StructLayout(LayoutKind.Sequential)]
public struct ConstantBuffer
{
    public Matrix WorldViewProjection;
}