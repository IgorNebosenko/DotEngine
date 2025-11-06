using System.Windows;
using DotEngineEditor.Themes;
using DotEngineEditor.UserControls;
using DotEngineEditor.UserControls.Interfaces;

namespace DotEngineEditor;

public partial class MainWindow : IWindowTabHandler
{
    public void SwapTheme()
    {
        var app = (App)Application.Current;
        var newTheme = app.Theme == ThemeName.DarkTheme ? ThemeName.LightTheme : ThemeName.DarkTheme;
        app.SetTheme(newTheme);
        Console.Info($"Theme switched to {newTheme}");
    }

    public void InstantiateHierarchy()
    {
    }

    public void InstantiateProject()
    {
    }

    public void InstantiateConsole()
    {
    }

    public void InstantiateInspector()
    {
    }

    public void InstantiateSceneView()
    {
    }

    public void InstantiateGame()
    {
    }
}