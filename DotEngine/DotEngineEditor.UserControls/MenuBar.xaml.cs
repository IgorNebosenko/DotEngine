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
    
    private readonly IPackagesHandler _packagesHandler;
    
    private readonly IWindowTabHandler _windowTabHandler;
    
    private readonly IHelpHandler _helpHandler;
    
    public MenuBar()
    {
        InitializeComponent();
        
        _fileTabHandler = Application.Current.Windows.OfType<IFileTabHandler>().Single();
        
        _editTabHandler = Application.Current.Windows.OfType<IEditTabHandler>().Single();
        _playTabsHandler = Application.Current.Windows.OfType<IPlayTabsHandler>().Single();
        
        //_assetsHandler = Application.Current.Windows.OfType<IAssetsHandler>().Single();
        //_createElementHandler = Application.Current.Windows.OfType<ICreateElementHandler>().Single();
        
        //_gameObjectHandler = Application.Current.Windows.OfType<IGameObjectHandler>().Single();
        
        //_packagesHandler = Application.Current.Windows.OfType<IPackagesHandler>().Single();
        
        _windowTabHandler = Application.Current.Windows.OfType<IWindowTabHandler>().Single();
        
        //_helpHandler = Application.Current.Windows.OfType<IHelpHandler>().Single();
    }

    #region File
    private void OnNewGameClicked(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.NewScene();
    }
    
    private void OnOpenGameClicked(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.OpenScene();
    }

    private void OnSaveClicked(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.Save();
    }

    private void OnSaveAsClicked(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.SaveAs();
    }

    private void OnNewProjectClicked(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.NewProject();
    }

    private void OnOpenProjectClicked(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.OpenProject();
    }

    private void OnSaveProjectClicked(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.SaveProject();
    }

    private void OnBuildProfilesClicked(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.BuildProfiles();
    }

    private void OnBuildAndRunClicked(object sender, RoutedEventArgs e)
    {
        _fileTabHandler.BuildAndRun();
    }

    private void OnExitClicked(object sender, RoutedEventArgs e)
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

    #region Window
    private void OnSwapThemeClicked(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.SwapTheme();
    }

    private void OnHierarchyInstantiateClicked(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.InstantiateHierarchy();
    }
    
    private void OnProjectInstantiateClicked(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.InstantiateProject();
    }
    
    private void OnConsoleInstantiateClicked(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.InstantiateConsole();
    }

    private void OnInspectorInstantiateClicked(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.InstantiateInspector();
    }

    private void OnSceneViewInstantiateClicked(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.InstantiateSceneView();
    }

    private void OnGameInstantiateClicked(object sender, RoutedEventArgs e)
    {
        _windowTabHandler.InstantiateGame();
    }
    #endregion
}