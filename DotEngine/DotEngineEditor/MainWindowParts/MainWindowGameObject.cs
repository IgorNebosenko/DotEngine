using DotEngineEditor.UserControls.Common;
using DotEngineEditor.UserControls.Interfaces;

namespace DotEngineEditor;

public partial class MainWindow : IGameObjectHandler, ICreate3DPrimitiveHandler
{
    public void CreateEmpty()
    {
        CustomMessageBox.NotImplement("Create empty");
    }

    public void CreateEmptyChild()
    {
        CustomMessageBox.NotImplement("Create empty child");
    }

    public void CenterOnChildren()
    {
        CustomMessageBox.NotImplement("Center on children");
    }

    public void MakeParent()
    {
        CustomMessageBox.NotImplement("Make parent");
    }

    public void CleanParent()
    {
        CustomMessageBox.NotImplement("Clean parent");
    }

    #region 3D Primitives
    public void CreateCube()
    {
        CustomMessageBox.NotImplement("Create sphere");
    }

    public void CreateSphere()
    {
        CustomMessageBox.NotImplement("Create sphere");
    }

    public void CreateCapsule()
    {
        CustomMessageBox.NotImplement("Create capsule");
    }

    public void CreateCylinder()
    {
        CustomMessageBox.NotImplement("Create cylinder");
    }

    public void CreatePyramid()
    {
        CustomMessageBox.NotImplement("Create pyramid");
    }

    public void CreatePlane()
    {
        CustomMessageBox.NotImplement("Create plane");
    }

    public void CreateQuad()
    {
        CustomMessageBox.NotImplement("Create quad");
    }
    #endregion
}