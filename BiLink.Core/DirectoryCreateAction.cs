namespace BiLink.Core;

public readonly struct DirectoryCreateAction(DirectoryInfo directory) : IAction
{
    public IEnumerable<IAction> Execute()
    {
        Directory.CreateDirectory(directory.FullName);
        return [];
    }

    public override string ToString()
    {
        return $"CRT {directory.Name}";
    }
}