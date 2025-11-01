using System.Windows;
using DotEngineEditor.Themes;

namespace DotEngineEditor
{
    public partial class App : Application
    {
        public ThemeName Theme { get; private set; }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            
            //Fix tabs
            //TODO need to load it from configs
            SetTheme(ThemeName.DarkTheme);
            SetTheme(ThemeName.LightTheme);
            SetTheme(ThemeName.DarkTheme);
        }

        public void SetTheme(ThemeName theme)
        {
            Theme = theme;
            
            var uri = new Uri($"pack://application:,,,/DotEngineEditor.Themes;component/{theme}.xaml", UriKind.Absolute);
            var newDict = new ResourceDictionary { Source = uri };

            var oldDict = Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Theme.xaml"));

            if (oldDict != null)
                Current.Resources.MergedDictionaries.Remove(oldDict);

            Current.Resources.MergedDictionaries.Add(newDict);
        }

    }
}