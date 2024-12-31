namespace BiLink.Core;

public readonly struct DirectoryDeleteAction(DirectoryInfo directory) : IAction
{
    public IEnumerable<IAction> Execute()
    {
        Directory.Delete(directory.FullName);
        return [];
    }

    public override string ToString()
    {
        return $"DEL {directory.Name}";
    }
}