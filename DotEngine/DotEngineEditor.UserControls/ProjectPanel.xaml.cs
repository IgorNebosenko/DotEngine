using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DotEngineEditor.UserControls
{
    public partial class ProjectPanel : UserControl
    {
        public ProjectPanel()
        {
            InitializeComponent();
            PopulateFiles("Assets");
        }

        private void OnFolderSelected(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem item)
            {
                SelectedPathText.Text = $"Assets/{item.Header}";
                PopulateFiles(item.Header.ToString() ?? string.Empty);
            }
        }

        private void PopulateFiles(string folder)
        {
            FileGrid.Children.Clear();

            var icons = new[]
            {
                ("Scene.unity", "pack://application:,,,/DotEngineEditor.Themes;component/Resources/SceneIcon.png"),
                ("Player.prefab", "pack://application:,,,/DotEngineEditor.Themes;component/Resources/PrefabIcon.png"),
                ("GameManager.cs", "pack://application:,,,/DotEngineEditor.Themes;component/Resources/CsIcon.png"),
                ("Material.mat", "pack://application:,,,/DotEngineEditor.Themes;component/Resources/MatIcon.png")
            };

            foreach (var (name, path) in icons)
            {
                ImageSource? imageSource = null;

                try
                {
                    imageSource = new BitmapImage(new Uri(path, UriKind.Absolute));
                }
                catch
                {
                    continue;
                }

                var image = new Image
                {
                    Source = imageSource,
                    Width = 48,
                    Height = 48,
                    Margin = new Thickness(0, 4, 0, 2)
                };

                var label = new TextBlock
                {
                    Text = name,
                    Foreground = (Brush)FindResource("ForegroundBrush"),
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                var panel = new StackPanel
                {
                    Width = 90,
                    Margin = new Thickness(4),
                    VerticalAlignment = VerticalAlignment.Top
                };

                panel.Children.Add(image);
                panel.Children.Add(label);
                FileGrid.Children.Add(panel);
            }
        }
    }
}