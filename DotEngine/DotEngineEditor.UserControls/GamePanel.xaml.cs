using System.Windows.Controls;
using SharpDX;

namespace DotEngineEditor.UserControls
{
    public partial class GamePanel : UserControl
    {
        public GamePanel()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            while (!GameWindow.IsInitialized)
                await Task.Delay(50);

            GameWindow.LoadModel(@"D:\Test\subFolder\Assets\Models\teapot.fbx",
                Vector3.Zero, Vector3.Zero, Vector3.One);
        }

    }
}