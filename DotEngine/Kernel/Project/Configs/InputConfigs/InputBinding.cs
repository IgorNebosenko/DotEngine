namespace Kernel.Project.Configs.InputConfigs;

[Serializable]
public class InputBinding
{
    public BindingType bindingType;
    public string binding;
    public string bindingAction;

    public InputBinding()
    {
        bindingType = BindingType.Release;
        binding = "";
        bindingAction = "";
    }
}