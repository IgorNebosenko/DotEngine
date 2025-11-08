using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;
using Matrix = SharpDX.Matrix;

namespace DirectXLayer.Windows;

public class EditorWindow : HwndHost, IDisposable
{
    private readonly int _width;
    private readonly int _height;
    
    private Device _device;
    private SwapChain _swapChain;
    private DeviceContext _deviceContext;
    private RenderTargetView _renderTargetView;
    private Texture2D _depthStencilBuffer;
    private DepthStencilView _depthStencilView;
    private SharpDX.Direct3D11.Buffer _vertexBuffer;
    private SharpDX.Direct3D11.Buffer _indexBuffer;
    private SharpDX.Direct3D11.Buffer _constantBuffer;
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
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to initialize DirectX: {e.Message}");
            throw new Exception("Failed to initialize DirectX", e);
        }
    }
    
    private void OnRendering(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        
    }
}