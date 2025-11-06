using System.Windows;
using System.Windows.Controls;
using AvalonDock.Layout;
using DotEngineEditor.Themes;
using DotEngineEditor.UserControls;
using DotEngineEditor.UserControls.Interfaces;
using SharpDX;

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
        InstantiateUserComponent<HierarchyPanel>("Hierarchy", new Vector2(400f, 600f));
    }

    public void InstantiateProject()
    {
        InstantiateUserComponent<ProjectPanel>("Project", new Vector2(800f, 400f));
    }

    public void InstantiateConsole()
    {
        InstantiateUserComponent<ConsolePanel>("Console", new Vector2(800f, 400f));
    }

    public void InstantiateInspector()
    {
        InstantiateUserComponent<InspectorPanel>("Inspector", new Vector2(400f, 600f));
    }

    public void InstantiateSceneView()
    {
        //ToDo write this!
        //InstantiateUserComponent<SceneViewPanel>("SceneView", new Vector2(600f, 800f));
    }

    public void InstantiateGame()
    {
        //ToDo write this!
        //InstantiateUserComponent<GamePanel>("Game", new Vector2(600f, 800f));
    }

    private void InstantiateUserComponent<T>(string title, Vector2 size) where T : UserControl, new()
    {
        var instance = new T();
        
        var anchorable = new LayoutAnchorable
        {
            Title = title,
            Content = instance,
            CanClose = true,
            CanFloat = true,
            FloatingWidth = size.X,
            FloatingHeight = size.Y
        };
        
        var anchorablePane = new LayoutAnchorablePane(anchorable);
        
        DockManager.Layout.RootPanel.Children.Add(anchorablePane);
        
        anchorable.Float();
    }
}