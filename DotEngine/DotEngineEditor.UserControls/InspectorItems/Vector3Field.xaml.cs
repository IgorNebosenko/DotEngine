using System.Windows;
using System.Windows.Controls;

namespace DotEngineEditor.UserControls.InspectorItems
{
    public partial class Vector3Field : UserControl
    {
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(string), typeof(Vector3Field));

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register(nameof(X), typeof(float), typeof(Vector3Field), new PropertyMetadata(0f));

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register(nameof(Y), typeof(float), typeof(Vector3Field), new PropertyMetadata(0f));

        public static readonly DependencyProperty ZProperty =
            DependencyProperty.Register(nameof(Z), typeof(float), typeof(Vector3Field), new PropertyMetadata(0f));

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

        public float Z
        {
            get => (float)GetValue(ZProperty);
            set => SetValue(ZProperty, value);
        }

        public Vector3Field()
        {
            InitializeComponent();
        }
    }
}