using System.Windows;
using System.Windows.Controls;
using DotEngineEditor.UserControls.Interfaces;

namespace DotEngineEditor.UserControls;

public partial class MenuBar : UserControl
{
    private readonly IFileTabHandler _fileTabHandler;
    private readonly IWindowTabHandler _windowTabHandler;
    
    public MenuBar()
    {
        InitializeComponent();
        
        _fileTabHandler = Application.Current.Windows.OfType<IFileTabHandler>().Single();
        _windowTabHandler = Application.Current.Windows.OfType<IWindowTabHandler>().Single();
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
        throw new NotImplementedException();
    }

    private void OnRedoClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnSelectAllClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnDeselectAllClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnSelectChildrenClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnSelectPrefabRootClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnInputSelectionClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnCutClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnCopyClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnPasteClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnDuplicateClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnRenameClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnDeleteClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnPlayClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnPauseClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnStepClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnProjectSettingsClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnPreferencesClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnPlayerDataClearClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
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