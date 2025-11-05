using System.ComponentModel.DataAnnotations;

namespace Kernel.Project.Configs;

public interface IProjectConfig
{
    string ConfigFile { get; }
}