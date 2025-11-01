using System.Windows;
using System.Windows.Controls;

namespace DotEngineEditor.UserControls.InspectorItems
{
    public partial class Vector2Field : UserControl
    {
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(string), typeof(Vector2Field));

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register(nameof(X), typeof(float), typeof(Vector2Field), new PropertyMetadata(0f));

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register(nameof(Y), typeof(float), typeof(Vector2Field), new PropertyMetadata(0f));

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public float X
        {
            get => (float)GetValue(XProperty);
            set => SetValue(XProperty, value);
        }

        public float Y
        {
            get => (float)GetValue(YProperty);
            set => SetValue(YProperty, value);
        }

        public Vector2Field()
        {
            InitializeComponent();
        }
    }
}