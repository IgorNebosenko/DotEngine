namespace Kernel.Project;

public interface IProjectInstance
{
    string FolderPath { get; }

    void Load();
}