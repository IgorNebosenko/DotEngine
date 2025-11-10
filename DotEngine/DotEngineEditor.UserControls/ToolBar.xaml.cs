using System.Windows;
using System.Windows.Controls;
using DotEngineEditor.UserControls.Interfaces;

namespace DotEngineEditor.UserControls;

public partial class ToolBar : UserControl
{
    private readonly INavigationHandler _navigationHandler;
    private readonly IHelpHandler _helpHandler;
    private readonly IPlayTabsHandler _playTabsHandler;
    
    public ToolBar()
    {
        InitializeComponent();
        
        _navigationHandler = Application.Current.Windows.OfType<INavigationHandler>().Single();
        _helpHandler = Application.Current.Windows.OfType<IHelpHandler>().Single();
        _playTabsHandler = Application.Current.Windows.OfType<IPlayTabsHandler>().Single();
    }

    #region Navigation buttons
    private void OnLookHandClick(object sender, RoutedEventArgs e)
    {
        _navigationHandler.Hand();
    }
    
    private void OnMoveObjectClick(object sender, RoutedEventArgs e)
    {
        _navigationHandler.Move();
    }

    private void OnRotateObjectClick(object sender, RoutedEventArgs e)
    {
        _navigationHandler.Rotate();
    }

    private void OnScaleObjectClick(object sender, RoutedEventArgs e)
    {
        _navigationHandler.Scale();
    }
    #endregion

    #region Help buttons
    private void OnHelpClick(object sender, RoutedEventArgs e)
    {
        _helpHandler.Documentation();
    }
    #endregion

    #region Play buttons
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
    #endregion
}