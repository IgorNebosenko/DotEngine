using Attributes;

namespace DotEngine.Components;

public interface IComponent
{
    ShowInExplorerReference Reference { get; }

    void Load();
}