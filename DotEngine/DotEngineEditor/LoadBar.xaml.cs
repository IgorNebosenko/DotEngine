using System.Windows;

namespace DotEngineEditor
{
    public partial class LoadBar : Window
    {
        public LoadBar()
        {
            InitializeComponent();
            
            SetProgress(0f);
            SetLabel("");
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetProgress(float value)
        {
            ProgressBarControl.Value = value;
        }

        public void SetLabel(string text)
        {
            LabelText.Content = text;
        }
    }
}