using System.Windows;
using DotEngineEditor.Themes;
using Kernel.Engine;
using Kernel.Project;

namespace DotEngineEditor
{
    public partial class App : Application
    {
        private EngineMetaDataHolder _engineMetaDataHolder;
        private ProjectInstance _projectInstance;
        
        public ThemeName Theme { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            MetaDataInit();
            if (!TryProjectInit())
            {
                Current.Shutdown();
                return;
            }

            LoadTheme();
        }

        private void MetaDataInit()
        {
            _engineMetaDataHolder = new EngineMetaDataHolder();
            _engineMetaDataHolder.HandleMetaData();
        }

        private bool TryProjectInit()
        {
            // Not always false! 
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (_engineMetaDataHolder.Data == null || string.IsNullOrEmpty(_engineMetaDataHolder.Data.LastProjectPath))
            {
                return false;
            }

            _projectInstance = new ProjectInstance(_engineMetaDataHolder.Data.LastProjectPath);
            _projectInstance.CheckAllDirectories();
            _projectInstance.Load();
            return true;
        }

        private void LoadTheme()
        {
            SetTheme(ThemeName.DarkTheme);
            SetTheme(ThemeName.LightTheme);
            
            SetTheme(_engineMetaDataHolder.Data.IsDarkTheme ? ThemeName.DarkTheme : ThemeName.LightTheme);
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