using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DotEngineEditor.UserControls
{
    public partial class ConsolePanel : UserControl
    {
        public enum LogLevel { Info, Warning, Error }

        public class LogEntry
        {
            public string Time { get; set; }
            public LogLevel Level { get; set; }
            public string Message { get; set; }
        }

        public ObservableCollection<string> FilterItems { get; } = new() { "All", "Info", "Warning", "Error" };

        public static readonly DependencyProperty IsAutoScrollProperty =
            DependencyProperty.Register(nameof(IsAutoScroll), typeof(bool), typeof(ConsolePanel), new PropertyMetadata(true));

        public static readonly DependencyProperty SelectedFilterProperty =
            DependencyProperty.Register(nameof(SelectedFilter), typeof(string), typeof(ConsolePanel), new PropertyMetadata("All", OnFilterChanged));

        public static readonly DependencyProperty VisibleLogsProperty =
            DependencyProperty.Register(nameof(VisibleLogs), typeof(ObservableCollection<LogEntry>), typeof(ConsolePanel), new PropertyMetadata(null));

        public bool IsAutoScroll
        {
            get => (bool)GetValue(IsAutoScrollProperty);
            set => SetValue(IsAutoScrollProperty, value);
        }

        public string SelectedFilter
        {
            get => (string)GetValue(SelectedFilterProperty);
            set => SetValue(SelectedFilterProperty, value);
        }

        public ObservableCollection<LogEntry> VisibleLogs
        {
            get => (ObservableCollection<LogEntry>)GetValue(VisibleLogsProperty);
            set => SetValue(VisibleLogsProperty, value);
        }

        private readonly ObservableCollection<LogEntry> _allLogs = new();
        private string _searchQuery = string.Empty;

        public ConsolePanel()
        {
            InitializeComponent();
            VisibleLogs = new ObservableCollection<LogEntry>();
        }

        public void Append(LogLevel level, string message)
        {
            var entry = new LogEntry
            {
                Time = DateTime.Now.ToString("HH:mm:ss"),
                Level = level,
                Message = message
            };
            _allLogs.Add(entry);
            ApplyFilter();
            if (IsAutoScroll) ScrollToEnd();
        }

        public void Info(string message) => Append(LogLevel.Info, message);
        public void Warn(string message) => Append(LogLevel.Warning, message);
        public void Error(string message) => Append(LogLevel.Error, message);

        private void ApplyFilter()
        {
            var filter = SelectedFilter;
            var query = _searchQuery.ToLowerInvariant();
            var items = _allLogs.Where(e =>
                (filter == "All" || e.Level.ToString() == filter) &&
                (string.IsNullOrEmpty(query) || e.Message.ToLowerInvariant().Contains(query))
            );

            VisibleLogs.Clear();
            foreach (var i in items) VisibleLogs.Add(i);
        }

        private void ScrollToEnd()
        {
            if (List.Items.Count == 0) return;
            List.ScrollIntoView(List.Items[^1]);
        }

        private static void OnFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (ConsolePanel)d;
            c.ApplyFilter();
            if (c.IsAutoScroll) c.ScrollToEnd();
        }

        private void OnClear(object sender, RoutedEventArgs e)
        {
            _allLogs.Clear();
            VisibleLogs.Clear();
        }

        private void OnCopy(object sender, RoutedEventArgs e)
        {
            if (List.SelectedItems.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var i in List.SelectedItems.Cast<LogEntry>())
                    sb.AppendLine($"{i.Time}\t{i.Level}\t{i.Message}");
                Clipboard.SetText(sb.ToString());
                return;
            }

            if (VisibleLogs.Count == 0) return;
            var all = string.Join(Environment.NewLine, VisibleLogs.Select(i => $"{i.Time}\t{i.Level}\t{i.Message}"));
            Clipboard.SetText(all);
        }

        private void OnSearchChanged(object sender, TextChangedEventArgs e)
        {
            _searchQuery = SearchBox.Text.Trim();
            ApplyFilter();
        }

        private void OnSearchClear(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = string.Empty;
        }
    }
}
