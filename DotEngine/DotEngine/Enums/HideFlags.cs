namespace DotEngine;

public enum HideFlags
{
    None = 0,
    HideInHierarchy = 1 << 0,
    HideInInspector = 1 << 1,
    DontSaveInEditor = 1 << 2,
    NotEditable = 1 << 3,
    DontSaveInBuild = 1 << 4,
    DontUnloadUnusedAsset = 1 << 5,
    DontSave = DontUnloadUnusedAsset | DontSaveInBuild | DontSaveInEditor,
    HideAndDontSave = DontSave | NotEditable | HideInHierarchy
}