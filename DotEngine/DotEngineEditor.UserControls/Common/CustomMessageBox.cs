using System.Windows;

namespace DotEngineEditor.UserControls.Common;

public static class CustomMessageBox
{
    public static void NotImplement(string message)
    {
        MessageBox.Show($"{message} isn't implemented yet", "Not Implement!", 
            MessageBoxButton.OK, MessageBoxImage.Error);
    }
}