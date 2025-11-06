using System.Windows;
using DotEngineEditor.UserControls.Interfaces;

namespace DotEngineEditor;

public partial class MainWindow : IFileTabHandler
{
    public void NewScene()
    {
        throw new NotImplementedException();
    }

    public void OpenScene()
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public void SaveAs()
    {
        throw new NotImplementedException();
    }

    public void NewProject()
    {
        throw new NotImplementedException();
    }

    public void OpenProject()
    {
        throw new NotImplementedException();
    }

    public void SaveProject()
    {
        throw new NotImplementedException();
    }

    public void BuildProfiles()
    {
        throw new NotImplementedException();
    }

    public void BuildAndRun()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        Application.Current.Shutdown();
    }
}