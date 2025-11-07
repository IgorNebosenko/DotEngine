using System.Windows;
using System.Windows.Controls;
using DotEngineEditor.UserControls.Interfaces;

namespace DotEngineEditor.UserControls;

public partial class MenuBar : UserControl
{
    private readonly IFileTabHandler _fileTabHandler;
    
    private readonly IEditTabHandler _editTabHandler;
    private readonly IPlayTabsHandler _playTabsHandler;
    
    private readonly IAssetsHandler _assetsHandler;
    private readonly ICreateElementHandler _createElementHandler;

    private readonly IGameObjectHandler _gameObjectHandler;
    private readonly ICreate3DPrimitiveHandler _create3DPrimitiveHandler;
    
    private readonly IPackagesHandler _packagesHandler;
    
    private readonly IWindowTabHandler _windowTabHandler;
    
    private readonly IHelpHandler _helpHandler;
    
    public MenuBar()
    {
        InitializeComponent();
        
        _fileTabHandler = Application.Current.Windows.OfType<IFileTabHandler>().Single();
        
        _editTabHandler = Application.Current.Windows.OfType<IEditTabHandler>().Single();
        _playTabsHandler = Application.Current.Windows.OfType<IPlayTabsHandler>().Single();
        
        _assetsHandler = Application.Current.Windows.OfType<IAssetsHandler>().Single();
        _createElementHandler = Application.Current.Windows.OfType<ICreateElementHandler>().Single();
        
        _gameObjectHandler = Application.Current.Windows.OfType<IGameObjectHandler>().Single();
        _create3DPrimitiveHandler = Application.Current.Windows.OfType<ICreate3DPrimitiveHandler>().Single();
        
        _packagesHandler = Application.Current.Windows.OfType<IPackagesHandler>().Single();
        
        _windowTabHandler = Application.Current.Windows.OfType<IWindowTabHandler>().Single();
        
        _helpHandler = Application.Current.Windows.OfType<IHelpHandler>().Single();
    }

    #region File
    private void OnNewGameClick(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.NewScene();
    }
    
    private void OnOpenGameClick(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.OpenScene();
    }

    private void OnSaveClick(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.Save();
    }

    private void OnSaveAsClick(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.SaveAs();
    }

    private void OnNewProjectClick(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.NewProject();
    }

    private void OnOpenProjectClick(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.OpenProject();
    }

    private void OnSaveProjectClick(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.SaveProject();
    }

    private void OnBuildProfilesClick(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.BuildProfiles();
    }

    private void OnBuildAndRunClick(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.BuildAndRun();
    }

    private void OnExitClick(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.Exit();
    }
    #endregion

    #region Edit
    private void OnUndoClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.Undo();
    }

    private void OnRedoClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.Redo();
    }

    private void OnSelectAllClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.SelectAll();
    }

    private void OnDeselectAllClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.DeselectAll();
    }

    private void OnSelectChildrenClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.SelectChildren();
    }

    private void OnSelectPrefabRootClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.SelectPrefabRoot();
    }

    private void OnInvertSelectionClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.InvertSelection();
    }

    private void OnCutClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.Cut();
    }

    private void OnCopyClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.Copy();
    }

    private void OnPasteClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.Paste();
    }

    private void OnDuplicateClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.Duplicate();
    }

    private void OnRenameClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.Rename();
    }

    private void OnDeleteClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.Delete();
    }

    private void OnPlayClick(object sender, RoutedEventArgs e)
    {
        _playTabsHandler.Play();
    }

    private void OnPauseClick(object sender, RoutedEventArgs e)
    {
        _playTabsHandler.Pause();
    }

    private void OnStepClick(object sender, RoutedEventArgs e)
    {
        _playTabsHandler.Step();
    }

    private void OnProjectSettingsClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.ProjectSettings();
    }

    private void OnPreferencesClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.Preferences();
    }

    private void OnPlayerDataClearClick(object sender, RoutedEventArgs e)
    {
        _editTabHandler.ClearPlayerData();
    }
    #endregion

    #region Assets
    private void OnShowInExplorerClick(object sender, RoutedEventArgs e)
    {
        _assetsHandler.ShowInExplorer();
    }

    private void OnOpenClick(object sender, RoutedEventArgs e)
    {
        _assetsHandler.Open();
    }

    private void OnCopyPathClick(object sender, RoutedEventArgs e)
    {
        _assetsHandler.CopyPath();
    }

    private void OnRefreshClick(object sender, RoutedEventArgs e)
    {
        _assetsHandler.Refresh();
    }

    private void OnReimportAllClick(object sender, RoutedEventArgs e)
    {
        _assetsHandler.ReimportAll();
    }

    private void OnOpenCSharpProjectClick(object sender, RoutedEventArgs e)
    {
        _assetsHandler.OpenCSharpProject();
    }
    
    #region CreateElement
    private void OnFolderCreateClick(object sender, RoutedEventArgs e)
    {
        _createElementHandler.CreateFolder();
    }

    private void OnMaterialCreateClick(object sender, RoutedEventArgs e)
    {
        _createElementHandler.CreateMaterial();
    }
    #endregion
    #endregion

    #region GameObject
    private void OnCreateEmptyClick(object sender, RoutedEventArgs e)
    {
        _gameObjectHandler.CreateEmpty();
    }

    private void OnCreateEmptyChildClick(object sender, RoutedEventArgs e)
    {
        _gameObjectHandler.CreateEmptyChild();
    }

    private void OnCenterInChildrenClick(object sender, RoutedEventArgs e)
    {
        _gameObjectHandler.CreateEmptyChild();
    }

    private void OnMakeParentClick(object sender, RoutedEventArgs e)
    {
        _gameObjectHandler.MakeParent();
    }

    private void OnClearParentClick(object sender, RoutedEventArgs e)
    {
        _gameObjectHandler.CleanParent();
    }

    #region Create 3D Primitive
    private void OnCreateCubeClick(object sender, RoutedEventArgs e)
    {
        _create3DPrimitiveHandler.CreateCube();
    }

    private void OnCreateSphereClick(object sender, RoutedEventArgs e)
    {
        _create3DPrimitiveHandler.CreateSphere();
    }

    private void OnCreateCapsuleClick(object sender, RoutedEventArgs e)
    {
        _create3DPrimitiveHandler.CreateCapsule();
    }

    private void OnCreateCylinderClick(object sender, RoutedEventArgs e)
    {
        _create3DPrimitiveHandler.CreateCylinder();
    }

    private void OnCreatePyramidClick(object sender, RoutedEventArgs e)
    {
        _create3DPrimitiveHandler.CreatePyramid();
    }

    private void OnCreatePlaneClick(object sender, RoutedEventArgs e)
    {
        _create3DPrimitiveHandler.CreatePlane();
    }

    private void OnCreateQuadClick(object sender, RoutedEventArgs e)
    {
        _create3DPrimitiveHandler.CreateQuad();
    }
    #endregion

    #endregion

    #region Packages
    
    #endregion

    #region Window
    private void OnSwapThemeClick(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.SwapTheme();
    }

    private void OnHierarchyInstantiateClick(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.InstantiateHierarchy();
    }
    
    private void OnProjectInstantiateClick(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.InstantiateProject();
    }
    
    private void OnConsoleInstantiateClick(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.InstantiateConsole();
    }

    private void OnInspectorInstantiateClick(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.InstantiateInspector();
    }

    private void OnSceneViewInstantiateClick(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.InstantiateSceneView();
    }

    private void OnGameInstantiateClick(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.InstantiateGame();
    }
    #endregion

    #region Help
    private void OnDocumentationClick(object sender, RoutedEventArgs e)
    {
        _helpHandler.Documentation();
    }

    private void OnAboutDotEngineClicked(object sender, RoutedEventArgs e)
    {
        _helpHandler.AboutDotEngine();
    }
    #endregion
}