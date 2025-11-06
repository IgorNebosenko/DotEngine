using System.Windows;
using System.Windows.Controls;
using DotEngineEditor.UserControls.Interfaces;

namespace DotEngineEditor.UserControls;

public partial class MenuBar : UserControl
{
    private IWindowTabHandler _windowTabHandler;
    
    public MenuBar()
    {
        InitializeComponent();
        
        _windowTabHandler = Application.Current.Windows.OfType<IWindowTabHandler>().Single();
    }

    #region File
    private void OnNewGameClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
    
    private void OnOpenGameClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnSaveClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnSaveAsClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnNewProjectClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnOpenProjectClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnSaveProjectClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnBuildProfilesClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnBuildAndRunClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnExitClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
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

    #endregion
}