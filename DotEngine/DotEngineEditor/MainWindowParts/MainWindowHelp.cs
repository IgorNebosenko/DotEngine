using DotEngineEditor.UserControls.Common;
using DotEngineEditor.UserControls.Interfaces;

namespace DotEngineEditor;

public partial class MainWindow : IHelpHandler
{
    public void Documentation()
    {
        CustomMessageBox.NotImplement("Documentation");
    }

    public void AboutDotEngine()
    {
        CustomMessageBox.NotImplement("About DotEngine");
    }
}