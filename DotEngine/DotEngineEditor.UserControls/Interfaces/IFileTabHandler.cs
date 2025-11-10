namespace DotEngineEditor.UserControls.Interfaces;

public interface IFileTabHandler
{
    void NewScene();
    void OpenScene();

    void Save();
    void SaveAs();
    
    void NewProject();
    void OpenProject();
    void SaveProject();

    void BuildProfiles();
    void BuildAndRun();

    void Exit();
}