using System.Windows;
using DotEngineEditor.UserControls.Common;
using DotEngineEditor.UserControls.Interfaces;

namespace DotEngineEditor;

public partial class MainWindow : IFileTabHandler
{
    public void NewScene()
    {
        CustomMessageBox.NotImplement("New scene");
    }

    public void OpenScene()
    {
        CustomMessageBox.NotImplement("Open scene");
    }

    public void Save()
    {
        CustomMessageBox.NotImplement("Save");
    }

    public void SaveAs()
    {
        CustomMessageBox.NotImplement("Save as");
    }

    public void NewProject()
    {
        CustomMessageBox.NotImplement("New project");
    }

    public void OpenProject()
    {
        CustomMessageBox.NotImplement("Open project");
    }

    public void SaveProject()
    {
        CustomMessageBox.NotImplement("Save project");
    }

    public void BuildProfiles()
    {
        CustomMessageBox.NotImplement("Build profiles");
    }

    public void BuildAndRun()
    {
        CustomMessageBox.NotImplement("Build and run");
    }

    public void Exit()
    {
        Application.Current.Shutdown();
    }
}