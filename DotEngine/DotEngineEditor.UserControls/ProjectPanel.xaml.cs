using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DotEngineEditor.UserControls
{
    public partial class ProjectPanel : UserControl
    {
        public ProjectPanel()
        {
            InitializeComponent();
            PopulateFolders();
            SelectRootAndInit();
        }

        private void PopulateFolders()
        {
            FolderTree.Items.Clear();

            var root = new TreeViewItem { Header = "Assets", IsExpanded = true };
            root.Items.Add(new TreeViewItem { Header = "Scenes" });
            root.Items.Add(new TreeViewItem { Header = "Scripts" });
            root.Items.Add(new TreeViewItem { Header = "Prefabs" });
            root.Items.Add(new TreeViewItem { Header = "Materials" });

            FolderTree.Items.Add(root);
        }

        private void SelectRootAndInit()
        {
            if (FolderTree.Items.Count == 0) return;
            var root = FolderTree.Items[0] as TreeViewItem;
            if (root == null) return;
            root.IsSelected = true;
            UpdatePath(root);
            PopulateFiles("Assets");
        }

        private void OnFolderSelected(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is not TreeViewItem item) return;
            UpdatePath(item);
            PopulateFiles(item.Header?.ToString() ?? "Assets");
        }

        private void UpdatePath(TreeViewItem item)
        {
            var parts = new List<string>();
            var current = item;
            while (current != null)
            {
                parts.Insert(0, current.Header?.ToString() ?? "");
                current = ItemsControl.ItemsControlFromItemContainer(current) as TreeViewItem;
            }
            var path = string.Join("/", parts);
            SelectedPathText.Text = string.IsNullOrWhiteSpace(path) ? "Assets/" : path + "/";
        }

        private void PopulateFiles(string folder)
        {
            FileGrid.Children.Clear();

            switch (folder)
            {
                case "Scenes":
                    AddFile("MainScene.unity", "SceneIcon.png");
                    AddFile("Menu.unity", "SceneIcon.png");
                    break;

                case "Scripts":
                    AddFile("GameManager.cs", "CsIcon.png");
                    AddFile("PlayerController.cs", "CsIcon.png");
                    AddFile("UIManager.cs", "CsIcon.png");
                    break;

                case "Prefabs":
                    AddFile("Player.prefab", "PrefabIcon.png");
                    AddFile("Enemy.prefab", "PrefabIcon.png");
                    break;

                case "Materials":
                    AddFile("Default.mat", "MaterialIcon.png");
                    AddFile("Skybox.mat", "MaterialIcon.png");
                    break;

                default:
                    AddFile("MainScene.unity", "SceneIcon.png");
                    AddFile("Player.prefab", "PrefabIcon.png");
                    AddFile("GameManager.cs", "CsIcon.png");
                    AddFile("Default.mat", "MaterialIcon.png");
                    break;
            }
        }

        private void AddFile(string name, string iconFile)
        {
            var iconUri = new Uri($"pack://application:,,,/DotEngineEditor.Themes;component/Resources/{iconFile}", UriKind.Absolute);

            var rect = new Rectangle
            {
                Width = 48,
                Height = 48,
                Margin = new Thickness(0, 4, 0, 2),
                Fill = (Brush)FindResource("IconColorBrush"),
                OpacityMask = new ImageBrush
                {
                    ImageSource = new BitmapImage(iconUri),
                    Stretch = Stretch.Uniform
                }
            };

            var label = new TextBlock
            {
                Text = name,
                Foreground = (Brush)FindResource("HeaderForegroundBrush"),
                HorizontalAlignment = HorizontalAlignment.Center,
                TextTrimming = TextTrimming.CharacterEllipsis
            };

            var panel = new StackPanel
            {
                Width = 120,
                Margin = new Thickness(8, 6, 8, 6),
                VerticalAlignment = VerticalAlignment.Top
            };

            panel.Children.Add(rect);
            panel.Children.Add(label);
            FileGrid.Children.Add(panel);
        }
    }
}
