using Attributes;

namespace DotEngine.Components;

public interface IComponent
{
    void LoadFromJson(string json);
}