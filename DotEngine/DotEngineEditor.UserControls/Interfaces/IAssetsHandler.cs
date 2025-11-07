namespace DotEngineEditor.UserControls.Interfaces;

public interface IAssetsHandler
{
    void ShowInExplorer();
    void Open();
    void CopyPath();

    void Refresh();

    void ReimportAll();

    void OpenCSharpProject();
}