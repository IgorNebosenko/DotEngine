using Buffer = SharpDX.Direct3D11.Buffer;

namespace DxStructures;

[Serializable]
public class VertexModelData
{
    public Buffer vertexBuffer;
    public Buffer indexBuffer;
    public int indexCount;

    public VertexModelData(Buffer vertexBuffer, Buffer indexBuffer, int indexCount)
    {
        this.vertexBuffer = vertexBuffer;
        this.indexBuffer = indexBuffer;
        this.indexCount = indexCount;
    }
}