using System.ComponentModel.DataAnnotations;

namespace Kernel.Project.Configs;

public interface IProjectConfig : IValidatableObject
{
    string ConfigFile { get; }
}