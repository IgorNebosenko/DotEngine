using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public ObservableCollection<string> FilterItems { get; } = new ObservableCollection<string> { "All", "Info", "Warning", "Error" };

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

        public event Action<string>? CommandEntered;

        readonly ObservableCollection<LogEntry> _allLogs = new ObservableCollection<LogEntry>();

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

        void ApplyFilter()
        {
            var filter = SelectedFilter;
            var items = filter == "All"
                ? _allLogs
                : new ObservableCollection<LogEntry>(_allLogs.Where(e => e.Level.ToString() == filter));
            VisibleLogs.Clear();
            foreach (var i in items) VisibleLogs.Add(i);
        }

        void ScrollToEnd()
        {
            if (List.Items.Count == 0) return;
            List.ScrollIntoView(List.Items[List.Items.Count - 1]);
        }

        static void OnFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (ConsolePanel)d;
            c.ApplyFilter();
            if (c.IsAutoScroll) c.ScrollToEnd();
        }

        void OnClear(object sender, RoutedEventArgs e)
        {
            _allLogs.Clear();
            VisibleLogs.Clear();
        }

        void OnCopy(object sender, RoutedEventArgs e)
        {
            if (List.SelectedItems.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var i in List.SelectedItems.Cast<LogEntry>()) sb.AppendLine($"{i.Time}\t{i.Level}\t{i.Message}");
                Clipboard.SetText(sb.ToString());
                return;
            }
            if (VisibleLogs.Count == 0) return;
            var all = string.Join(Environment.NewLine, VisibleLogs.Select(i => $"{i.Time}\t{i.Level}\t{i.Message}"));
            Clipboard.SetText(all);
        }

        void OnRun(object sender, RoutedEventArgs e)
        {
            var text = InputBox.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(text)) return;
            CommandEntered?.Invoke(text);
            InputBox.Text = string.Empty;
        }

        void OnInputKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) OnRun(this, new RoutedEventArgs());
        }
    }
}
