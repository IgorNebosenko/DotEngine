namespace DotEngineEditor.UserControls.Interfaces;

public interface IGameObjectHandler
{
    void CreateEmpty();
    void CreateEmptyChild();

    void CenterOnChildren();

    void MakeParent();
    void CleanParent();
}