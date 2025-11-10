namespace DotEngineEditor.UserControls.Interfaces;

public interface IWindowTabHandler
{
    void SwapTheme();
    
    void InstantiateHierarchy();
    void InstantiateProject();
    void InstantiateConsole();
    void InstantiateInspector();
    void InstantiateSceneView();
    void InstantiateGame();
}