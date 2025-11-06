namespace Kernel.Project.Configs;

public interface IProjectConfig
{
    string ConfigFile { get; }

    bool Validate(out List<string> errorMessages);
}