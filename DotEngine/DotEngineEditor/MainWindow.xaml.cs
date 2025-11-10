using System.Windows;

namespace DotEngineEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.Info("Editor started");
            Console.Warn("Theme system initialized");
        }

        private void OnTestButtonClick(object sender, RoutedEventArgs e)
        {
            SwapTheme();
        }
    }
}