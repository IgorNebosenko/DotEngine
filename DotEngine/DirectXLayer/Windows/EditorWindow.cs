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

namespace DirectXLayer.Windows;

public class EditorWindow : HwndHost, IDisposable
{
    private readonly int _width;
    private readonly int _height;

    private float _rotationAngle;
    private Device _device;
    private SwapChain _swapChain;
    private DeviceContext _deviceContext;
    private RenderTargetView _renderTargetView;
    private Texture2D _depthStencilBuffer;
    private DepthStencilView _depthStencilView;
    private Buffer _vertexBuffer;
    private Buffer _indexBuffer;
    private Buffer _constantBuffer;
    private VertexShader _vertexShader;
    private PixelShader _pixelShader;
    private InputLayout _inputLayout;
    private RasterizerState _rasterizerState;
    private Viewport _viewport;

    public EditorWindow(Vector2 windowSize)
    {
        _width = (int)windowSize.X;
        _height = (int)windowSize.Y;
    }
    
    #region WinAPI
    private const int WS_CHILD = 0x40000000;
    private const int WS_VISIBLE = 0x10000000;
    
    [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)]
    private static extern IntPtr CreateWindowEx(int dwExStyle, string lpszClassName, string lpszWindowName,
        int style, int x, int y, int width, int height, IntPtr hwndParent, IntPtr hMenu, IntPtr hInst, IntPtr pvParam);
    #endregion
    
    
    
    protected override HandleRef BuildWindowCore(HandleRef hwndParent)
    {
        var hWnd = CreateWindowEx(0, "static", "",
            WS_CHILD | WS_VISIBLE, 0, 0, _width, _height,
            hwndParent.Handle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
        
        InitializeDirectX(hWnd);
        CompositionTarget.Rendering += OnRendering;
        
        return new HandleRef(this, hWnd);
    }

    protected override void DestroyWindowCore(HandleRef hwnd)
    {
        CompositionTarget.Rendering -= OnRendering;
        Dispose();
    }

    private void InitializeDirectX(IntPtr hwnd)
    {
        try
        {
            var swapCainDescription = new SwapChainDescription
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(_width, _height,
                    new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = hwnd,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
            
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swapCainDescription,
                out _device, out _swapChain);

            _deviceContext = _device.ImmediateContext;

            using (var backBuffer = _swapChain.GetBackBuffer<Texture2D>(0))
            {
                _renderTargetView = new RenderTargetView(_device, backBuffer);
            }

            var depthBufferDesc = new Texture2DDescription()
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
                OptionFlags = ResourceOptionFlags.None,
            };
            
            _depthStencilBuffer = new Texture2D(_device, depthBufferDesc);
            _depthStencilView = new DepthStencilView(_device, _depthStencilBuffer);
            
            _viewport = new Viewport(0, 0, _width, _height, 0.0f, 1.0f);
            _deviceContext.Rasterizer.SetViewport(_viewport);

            CreateVertexBuffer();
            CreteIndexBuffer();
            CreateConstantBuffer();
            CreateShaders();
            CreateRasterizerState();
            
            _deviceContext.OutputMerger.SetTargets(_depthStencilView, _renderTargetView);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to initialize DirectX: {e.Message}");
            throw new Exception("Failed to initialize DirectX", e);
        }
    }

    private void OnRendering(object? sender, EventArgs e)
    {
        Render();
    }

    private void Render()
    {
        if (_deviceContext == null)
        {
            return;
        }

        try
        {
            _deviceContext.ClearRenderTargetView(_renderTargetView, new Color4(0.25f, 0.25f, 0.25f, 1f));
            _deviceContext.ClearDepthStencilView(_depthStencilView, DepthStencilClearFlags.Depth, 1f, 0);

            _rotationAngle += 0.01f;

            UpdateConstantBuffer();
            
            _deviceContext.InputAssembler.SetVertexBuffers(0, 
                new VertexBufferBinding(_vertexBuffer, Utilities.SizeOf<Vertex>(), 0));
            _deviceContext.InputAssembler.SetIndexBuffer(_indexBuffer, Format.R16_UInt, 0);
            _deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            
            _deviceContext.VertexShader.Set(_vertexShader);
            _deviceContext.VertexShader.SetConstantBuffer(0, _constantBuffer);
            _deviceContext.PixelShader.Set(_pixelShader);
            
            _deviceContext.DrawIndexed(18, 0, 0);
            _swapChain.Present(1, PresentFlags.None);
        }
        catch (Exception e)
        {
            Console.WriteLine($"DirectX render exception! {e.Message}");
        }
    }

    private void UpdateConstantBuffer()
    {
        var world = Matrix.RotationY(_rotationAngle);//Rotate only by Y
        var view = Matrix.LookAtLH(new Vector3(0f, 1f, -3f), Vector3.Zero, Vector3.UnitY);
        var projection = Matrix.PerspectiveFovLH((float) Math.PI / 4.0f,
            (float) _width / _height, Single.Epsilon, 1000f);
        
        var worldViewProjection = world * view * projection;
        worldViewProjection.Transpose();
        
        _deviceContext.MapSubresource(_constantBuffer, MapMode.WriteDiscard, MapFlags.None, out var dataStream);
        dataStream.Write(new ConstantBuffer {WorldViewProjection = worldViewProjection});
        _deviceContext.UnmapSubresource(_constantBuffer, 0);
    }

    private void CreateVertexBuffer()
    {
        var vertices = new[]
        {
            new Vertex { position = new Vector3(-0.5f, -0.5f, 0.5f), color = Color.Red },
            new Vertex { position = new Vector3(0.5f, -0.5f, 0.5f), color = Color.Yellow },
            new Vertex { position = new Vector3(0.5f, -0.5f, -0.5f), color = Color.Green },
            new Vertex { position = new Vector3(-0.5f, -0.5f, -0.5f), color = Color.Cyan},
            new Vertex { position = new Vector3(0.0f, 0.5f, 0.0f), color = Color.Blue }
        };

        var vertexBufferDesc = new BufferDescription()
        {
            Usage = ResourceUsage.Default,
            SizeInBytes = Utilities.SizeOf<Vertex>() * vertices.Length,
            BindFlags = BindFlags.VertexBuffer,
            CpuAccessFlags = CpuAccessFlags.None,
            OptionFlags = ResourceOptionFlags.None,
            StructureByteStride = 0
        };

        _vertexBuffer = Buffer.Create(_device, vertices, vertexBufferDesc);
    }

    private void CreteIndexBuffer()
    {
        var indices = new ushort[]
        {
            0, 1, 2,
            0, 2, 3,
            
            0, 4, 1,
            1, 4, 2,
            2, 4, 3,
            3, 4, 0
        };
        
        var indexBufferDesc = new BufferDescription()
        {
            Usage = ResourceUsage.Default,
            SizeInBytes = sizeof(ushort) * indices.Length,
            BindFlags = BindFlags.IndexBuffer,
            CpuAccessFlags = CpuAccessFlags.None,
            OptionFlags = ResourceOptionFlags.None,
            StructureByteStride = 0
        };
        
        _indexBuffer = Buffer.Create(_device, indices, indexBufferDesc);
    }

    private void CreateConstantBuffer()
    {
        var constantBufferDesc = new BufferDescription()
        {
            Usage = ResourceUsage.Dynamic,
            SizeInBytes = Utilities.SizeOf<ConstantBuffer>(),
            BindFlags = BindFlags.ConstantBuffer,
            CpuAccessFlags = CpuAccessFlags.Write,
            OptionFlags = ResourceOptionFlags.None,
            StructureByteStride = 0
        };
        
        _constantBuffer = new Buffer(_device, constantBufferDesc);
    }

    private void CreateShaders()
    {
        var defaultShader = new DefaultShader();

        _vertexShader = new VertexShader(_device, ShaderBytecode.Compile(
            defaultShader.Vertex, "VS", "vs_4_0"));
        
        _pixelShader = new PixelShader(_device, ShaderBytecode.Compile(
            defaultShader.Pixel, "PS", "ps_4_0"));

        var inputElements = new[]
        {
            new InputElement("POSITION", 0, Format.R32G32_Float, 0, 0),
            new InputElement("COLOR", 0, Format.R32G32_Float, 12, 0)
        };

        _inputLayout = new InputLayout(_device,
            ShaderBytecode.Compile(defaultShader.Vertex, "VS", "vs_4_0"),
            inputElements);
    }

    private void CreateRasterizerState()
    {
        var rasterizerDesc = new RasterizerStateDescription
        {
            IsAntialiasedLineEnabled = false,
            CullMode = CullMode.Back,
            DepthBias = 0,
            DepthBiasClamp = 0f,
            IsDepthClipEnabled = true,
            FillMode = FillMode.Solid,
            IsFrontCounterClockwise = false,
            IsMultisampleEnabled = false,
            IsScissorEnabled = false,
            SlopeScaledDepthBias = 0f
        };
        
        _rasterizerState = new RasterizerState(_device, rasterizerDesc);
        _deviceContext.Rasterizer.State = _rasterizerState;
    }


    public void Dispose()
    {
        _rasterizerState?.Dispose();
        _inputLayout?.Dispose();
        _vertexShader?.Dispose();
        _pixelShader?.Dispose();
        _constantBuffer?.Dispose();
        _indexBuffer?.Dispose();
        _vertexBuffer?.Dispose();
        _depthStencilView?.Dispose();
        _depthStencilBuffer?.Dispose();
        _renderTargetView?.Dispose();
        _swapChain?.Dispose();
        _deviceContext?.Dispose();
        _device?.Dispose();
    }
}