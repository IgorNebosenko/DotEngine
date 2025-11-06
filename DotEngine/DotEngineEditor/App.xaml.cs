using System.Windows;
using DotEngineEditor.Themes;
using Kernel.Engine;
using Kernel.Project;
using Microsoft.Win32;

namespace DotEngineEditor
{
    public partial class App : Application
    {
        private EngineMetaDataHolder _engineMetaDataHolder;
        private ProjectInstance _projectInstance;
        
        public ThemeName Theme { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loadBar = new LoadBar();
            loadBar.Show();
            loadBar.Title = "Loading...";

            await Task.Run(() =>
            {
                loadBar.Dispatcher.Invoke(() => loadBar.SetLabel("Metadata init..."));
                MetaDataInit();
                
                loadBar.Dispatcher.Invoke(() => loadBar.SetProgress(0.5f));
                loadBar.Dispatcher.Invoke(() => loadBar.SetLabel("Try project init..."));
                
                if (!TryProjectInit())
                {
                    Dispatcher.Invoke(() => Current.Shutdown());
                    return;
                }
            });

            LoadTheme();

            var mainWindow = new MainWindow();
            mainWindow.Show();
            
            Current.MainWindow = mainWindow;
            
            loadBar.Close();
        }

        private void MetaDataInit()
        {
            _engineMetaDataHolder = new EngineMetaDataHolder();
            _engineMetaDataHolder.HandleMetaData(x => MessageBox.Show(x), CreateMetaDataDialog);
        }
        
        private string CreateMetaDataDialog()
        {
            var dialog = new OpenFolderDialog();
            string folderPath = string.Empty;

            void OnFolderOk(object? o, EventArgs eventArgs) => folderPath = dialog.FolderName;

            dialog.FolderOk += OnFolderOk;
            dialog.ShowDialog();
            dialog.FolderOk -= OnFolderOk;

            return folderPath;
        }

        private bool TryProjectInit()
        {
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