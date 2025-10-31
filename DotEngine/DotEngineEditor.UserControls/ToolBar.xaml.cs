using System.Windows;
using System.Windows.Controls;

namespace DotEngineEditor.UserControls;

public partial class ToolBar : UserControl
{
    public ToolBar()
    {
        InitializeComponent();
    }

    #region Navigation buttons
    private void LookHandClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
    
    private void MoveObjectClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void RotateObjectClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ScaleObjectClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Help buttons
    private void HelpClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Play buttons
    private void PlayClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void PauseClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void StepClicked(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion
}