using System.Windows;
using DotEngineEditor.Themes;

namespace DotEngineEditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnTestButtonClick(object sender, RoutedEventArgs e)
    {
        var app = (App)Application.Current;
        app.SetTheme(app.Theme == ThemeName.DarkTheme ? ThemeName.LightTheme : ThemeName.DarkTheme);
    }
}