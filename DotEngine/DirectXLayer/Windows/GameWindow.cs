using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using DirectXLayer.Shaders;
using DirectXLayer.Shaders.Integrated;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Buffer = SharpDX.Direct3D11.Buffer;
using Color = DirectXLayer.Shaders.Color;
using ConstantBuffer = DirectXLayer.Data.ConstantBuffer;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;
using Matrix = SharpDX.Matrix;

namespace DirectXLayer.Windows
{
    public class GameWindow : HwndHost, IDisposable
    {
        private readonly int _width;
        private readonly int _height;

        private Device _device;
        private SwapChain _swapChain;
        private DeviceContext _deviceContext;
        private RenderTargetView _renderTargetView;
        private Texture2D _depthStencilBuffer;
        private DepthStencilView _depthStencilView;
        private Buffer _vertexBufferPyramid;
        private Buffer _indexBufferPyramid;
        private Buffer _vertexBufferCube;
        private Buffer _indexBufferCube;
        private Buffer _vertexBufferSphere;
        private Buffer _indexBufferSphere;
        private Buffer _constantBuffer;
        private VertexShader _vertexShader;
        private PixelShader _pixelShader;
        private InputLayout _inputLayout;
        private RasterizerState _rasterizerState;
        private Viewport _viewport;
        private float _rotation;
        private float _cubeTime;

        public GameWindow()
        {
            _width = 800;
            _height = 600;
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            var hwnd = CreateWindowEx(0, "static", "",
                WS_CHILD | WS_VISIBLE, 0, 0, _width, _height,
                hwndParent.Handle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            InitializeDirectX(hwnd);
            CompositionTarget.Rendering += OnRendering;
            return new HandleRef(this, hwnd);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            CompositionTarget.Rendering -= OnRendering;
            Dispose();
        }

        private void InitializeDirectX(IntPtr hwnd)
        {
            var swapChainDesc = new SwapChainDescription
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(_width, _height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = hwnd,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swapChainDesc, out _device, out _swapChain);
            _deviceContext = _device.ImmediateContext;

            using (var backBuffer = _swapChain.GetBackBuffer<Texture2D>(0))
                _renderTargetView = new RenderTargetView(_device, backBuffer);

            var depthDesc = new Texture2DDescription
            {
                Width = _width,
                Height = _height,
                MipLevels = 1,
                ArraySize = 1,
                Format = Format.D24_UNorm_S8_UInt,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };

            _depthStencilBuffer = new Texture2D(_device, depthDesc);
            _depthStencilView = new DepthStencilView(_device, _depthStencilBuffer);
            _viewport = new Viewport(0, 0, _width, _height, 0f, 1f);
            _deviceContext.Rasterizer.SetViewport(_viewport);

            CreatePyramid();
            CreateCube();
            CreateSphere();
            CreateConstantBuffer();
            CreateShaders();
            CreateRasterizerState();

            _deviceContext.OutputMerger.SetTargets(_depthStencilView, _renderTargetView);
        }

        private void CreatePyramid()
        {
            var vertices = new[]
            {
                new Vertex { position = new Vector3(-0.5f, -0.5f,  0.5f), color = Color.Red },
                new Vertex { position = new Vector3( 0.5f, -0.5f,  0.5f), color = Color.Green },
                new Vertex { position = new Vector3( 0.5f, -0.5f, -0.5f), color = Color.Blue },
                new Vertex { position = new Vector3(-0.5f, -0.5f, -0.5f), color = Color.Yellow },
                new Vertex { position = new Vector3( 0.0f,  0.5f,  0.0f), color = Color.Magenta }
            };
            var indices = new ushort[] { 0,1,2, 0,2,3, 0,4,1, 1,4,2, 2,4,3, 3,4,0 };
            CreateBuffers(vertices, indices, out _vertexBufferPyramid, out _indexBufferPyramid);
        }

        private void CreateCube()
        {
            var vertices = new[]
            {
                new Vertex { position = new Vector3(-0.5f,-0.5f,-0.5f), color = Color.Red },
                new Vertex { position = new Vector3(-0.5f, 0.5f,-0.5f), color = Color.Green },
                new Vertex { position = new Vector3(0.5f, 0.5f,-0.5f), color = Color.Blue },
                new Vertex { position = new Vector3(0.5f,-0.5f,-0.5f), color = Color.Yellow },
                new Vertex { position = new Vector3(-0.5f,-0.5f,0.5f), color = Color.Magenta },
                new Vertex { position = new Vector3(-0.5f,0.5f,0.5f), color = Color.Cyan },
                new Vertex { position = new Vector3(0.5f,0.5f,0.5f), color = Color.White },
                new Vertex { position = new Vector3(0.5f,-0.5f,0.5f), color = Color.Grey }
            };
            var indices = new ushort[]
            {
                0,1,2, 0,2,3,
                4,6,5, 4,7,6,
                4,5,1, 4,1,0,
                3,2,6, 3,6,7,
                1,5,6, 1,6,2,
                4,0,3, 4,3,7
            };
            CreateBuffers(vertices, indices, out _vertexBufferCube, out _indexBufferCube);
        }

        private void CreateSphere()
        {
            const int segments = 12;
            const int rings = 12;
            var vertices = new Vertex[(segments + 1) * (rings + 1)];
            var indices = new ushort[segments * rings * 6];
            var index = 0;
            for (var y = 0; y <= rings; y++)
            {
                var v = y / (float)rings;
                var phi = v * Math.PI;
                for (var x = 0; x <= segments; x++)
                {
                    var u = x / (float)segments;
                    var theta = u * Math.PI * 2;
                    var px = (float)(Math.Sin(phi) * Math.Cos(theta));
                    var py = (float)Math.Cos(phi);
                    var pz = (float)(Math.Sin(phi) * Math.Sin(theta));
                    vertices[y * (segments + 1) + x] = new Vertex
                    {
                        position = new Vector3(px, py, pz) * 0.5f,
                        color = new Color(0.8f, 0.8f, 0.8f, 1f)
                    };
                }
            }
            for (var y = 0; y < rings; y++)
            {
                for (var x = 0; x < segments; x++)
                {
                    var i0 = (ushort)(y * (segments + 1) + x);
                    var i1 = (ushort)(i0 + 1);
                    var i2 = (ushort)(i0 + segments + 1);
                    var i3 = (ushort)(i2 + 1);
                    indices[index++] = i0; indices[index++] = i2; indices[index++] = i1;
                    indices[index++] = i1; indices[index++] = i2; indices[index++] = i3;
                }
            }
            CreateBuffers(vertices, indices, out _vertexBufferSphere, out _indexBufferSphere);
        }

        private void CreateBuffers(Vertex[] vertices, ushort[] indices, out Buffer vb, out Buffer ib)
        {
            var stride = Utilities.SizeOf<Vector3>() + sizeof(float) * 4;
            var size = stride * vertices.Length;
            using var stream = new DataStream(size, true, true);
            foreach (var v in vertices)
            {
                stream.Write(v.position);
                stream.Write(v.color.r);
                stream.Write(v.color.g);
                stream.Write(v.color.b);
                stream.Write(v.color.a);
            }
            stream.Position = 0;
            var vbDesc = new BufferDescription
            {
                Usage = ResourceUsage.Default,
                SizeInBytes = (int)stream.Length,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };
            vb = new Buffer(_device, stream, vbDesc);
            var ibDesc = new BufferDescription
            {
                Usage = ResourceUsage.Default,
                SizeInBytes = sizeof(ushort) * indices.Length,
                BindFlags = BindFlags.IndexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };
            ib = Buffer.Create(_device, indices, ibDesc);
        }

        private void CreateConstantBuffer()
        {
            var desc = new BufferDescription
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Utilities.SizeOf<ConstantBuffer>(),
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None
            };
            _constantBuffer = new Buffer(_device, desc);
        }

        private void CreateShaders()
        {
            var shader = new DefaultShader();
            var vs = ShaderBytecode.Compile(shader.Vertex, "VS", "vs_4_0");
            var ps = ShaderBytecode.Compile(shader.Pixel, "PS", "ps_4_0");
            _vertexShader = new VertexShader(_device, vs);
            _pixelShader = new PixelShader(_device, ps);
            var elements = new[]
            {
                new InputElement("POSITION",0,Format.R32G32B32_Float,0,0),
                new InputElement("COLOR",0,Format.R32G32B32A32_Float,12,0)
            };
            _inputLayout = new InputLayout(_device, vs, elements);
        }

        private void CreateRasterizerState()
        {
            var desc = new RasterizerStateDescription
            {
                CullMode = CullMode.None,
                FillMode = FillMode.Solid,
                IsDepthClipEnabled = true
            };
            _rasterizerState = new RasterizerState(_device, desc);
            _deviceContext.Rasterizer.State = _rasterizerState;
        }

        private void OnRendering(object sender, EventArgs e)
        {
            Render();
        }

        private void Render()
        {
            if (_deviceContext == null) return;
            _deviceContext.ClearRenderTargetView(_renderTargetView, new Color4(0.05f, 0.05f, 0.1f, 1f));
            _deviceContext.ClearDepthStencilView(_depthStencilView, DepthStencilClearFlags.Depth, 1f, 0);
            _rotation += 0.01f;
            _cubeTime += 0.05f;
            _deviceContext.InputAssembler.InputLayout = _inputLayout;
            _deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            _deviceContext.VertexShader.Set(_vertexShader);
            _deviceContext.PixelShader.Set(_pixelShader);
            _deviceContext.VertexShader.SetConstantBuffer(0, _constantBuffer);
            DrawObject(_vertexBufferPyramid, _indexBufferPyramid, Matrix.Translation(-1.5f, 0f, 0f) * Matrix.RotationY(_rotation));
            DrawObject(_vertexBufferCube, _indexBufferCube, Matrix.Translation(1.5f, (float)Math.Abs(Math.Sin(_cubeTime)) - 0.5f, 0f));
            DrawObject(_vertexBufferSphere, _indexBufferSphere, Matrix.Translation(0f, -0.5f, 1.5f));
            _swapChain.Present(1, PresentFlags.None);
        }

        private void DrawObject(Buffer vb, Buffer ib, Matrix world)
        {
            var view = Matrix.LookAtLH(new Vector3(0f, 1f, -4f), Vector3.Zero, Vector3.UnitY);
            var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4f, (float)_width / _height, 0.1f, 100f);
            var wvp = world * view * proj;
            wvp.Transpose();
            _deviceContext.MapSubresource(_constantBuffer, MapMode.WriteDiscard, MapFlags.None, out var ds);
            ds.Write(new ConstantBuffer { WorldViewProjection = wvp });
            _deviceContext.UnmapSubresource(_constantBuffer, 0);
            _deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vb, 12 + 16, 0));
            _deviceContext.InputAssembler.SetIndexBuffer(ib, Format.R16_UInt, 0);
            _deviceContext.DrawIndexed(ib.Description.SizeInBytes / sizeof(ushort), 0, 0);
        }

        public void Dispose()
        {
            _rasterizerState?.Dispose();
            _inputLayout?.Dispose();
            _vertexShader?.Dispose();
            _pixelShader?.Dispose();
            _constantBuffer?.Dispose();
            _indexBufferPyramid?.Dispose();
            _vertexBufferPyramid?.Dispose();
            _indexBufferCube?.Dispose();
            _vertexBufferCube?.Dispose();
            _indexBufferSphere?.Dispose();
            _vertexBufferSphere?.Dispose();
            _depthStencilView?.Dispose();
            _depthStencilBuffer?.Dispose();
            _renderTargetView?.Dispose();
            _swapChain?.Dispose();
            _deviceContext?.Dispose();
            _device?.Dispose();
        }

        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateWindowEx(int dwExStyle, string lpszClassName, string lpszWindowName,
            int style, int x, int y, int width, int height, IntPtr hwndParent,
            IntPtr hMenu, IntPtr hInst, IntPtr pvParam);
    }
}
