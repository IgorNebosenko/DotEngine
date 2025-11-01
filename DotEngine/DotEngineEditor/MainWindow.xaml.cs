using System.Windows;
using DotEngineEditor.Themes;
using DotEngineEditor.UserControls;

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
            var app = (App)Application.Current;
            var newTheme = app.Theme == ThemeName.DarkTheme ? ThemeName.LightTheme : ThemeName.DarkTheme;
            app.SetTheme(newTheme);
            Console.Info($"Theme switched to {newTheme}");
        }
    }
}