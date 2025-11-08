using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;

namespace DirectXLayer.Windows;

public class EditorWindow : HwndHost, IDisposable
{
    private readonly int _width;
    private readonly int _height;

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
        
    }
    
    private void OnRendering(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        
    }
}