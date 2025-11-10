using DotEngineEditor.UserControls.Common;
using DotEngineEditor.UserControls.Interfaces;

namespace DotEngineEditor;

public partial class MainWindow : IEditTabHandler, IPlayTabsHandler
{
    public void Undo()
    {
        CustomMessageBox.NotImplement("Undo");
    }

    public void Redo()
    {
        CustomMessageBox.NotImplement("Redo");
    }

    public void SelectAll()
    {
        CustomMessageBox.NotImplement("Select all");
    }

    public void DeselectAll()
    {
        CustomMessageBox.NotImplement("Deselect all");
    }

    public void SelectChildren()
    {
        CustomMessageBox.NotImplement("Select children");
    }

    public void SelectPrefabRoot()
    {
        CustomMessageBox.NotImplement("Select prefab root");
    }

    public void InvertSelection()
    {
        CustomMessageBox.NotImplement("Invert selection");
    }

    public void Cut()
    {
        CustomMessageBox.NotImplement("Cut");
    }

    public void Copy()
    {
        CustomMessageBox.NotImplement("Copy");
    }

    public void Paste()
    {
        CustomMessageBox.NotImplement("Paste");
    }

    public void Duplicate()
    {
        CustomMessageBox.NotImplement("Duplicate");
    }

    public void Rename()
    {
        CustomMessageBox.NotImplement("Rename");
    }

    public void Delete()
    {
        CustomMessageBox.NotImplement("Delete");
    }

    public void Play()
    {
        CustomMessageBox.NotImplement("Play");
    }

    public void Pause()
    {
        CustomMessageBox.NotImplement("Pause");
    }

    public void Step()
    {
        CustomMessageBox.NotImplement("Step");
    }

    public void ProjectSettings()
    {
        CustomMessageBox.NotImplement("Project settings");
    }

    public void Preferences()
    {
        CustomMessageBox.NotImplement("Preferences");
    }

    public void ClearPlayerData()
    {
        CustomMessageBox.NotImplement("Clear player data");
    }
}