using System.Windows;
using DotEngineEditor.Themes;

namespace DotEngineEditor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public void SetTheme(ThemeName theme)
    {
        var newDict = new ResourceDictionary { Source = new Uri(
            $"pack://application:,,,/DotEngineEditor.Themes;component/{nameof(theme)}.xaml)", UriKind.Relative) };
        
        var oldDict = Current.Resources.MergedDictionaries.FirstOrDefault(d =>
            d.Source != null && d.Source.OriginalString.Contains("Theme.xaml"));

        if (oldDict != null)
        {
            Current.Resources.MergedDictionaries.Remove(oldDict);
        }
        
        Current.Resources.MergedDictionaries.Add(newDict);
    }
}