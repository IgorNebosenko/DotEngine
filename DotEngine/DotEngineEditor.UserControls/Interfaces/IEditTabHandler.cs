namespace DotEngineEditor.UserControls.Interfaces;

public interface IEditTabHandler
{
    void Undo();
    void Redo();
    
    void SelectAll();
    void DeselectAll();
    void SelectChildren();
    void SelectPrefabRoot();
    void InvertSelection();
    
    void Cut();
    void Copy();
    void Paste();
    void Duplicate();
    void Rename();
    void Delete();

    void ProjectSettings();
    void Preferences();
    void ClearPlayerData();
}