namespace BiLink.Core;

public readonly struct FileCopyAction(FileInfo source, FileInfo destination, bool force) : IAction
{
    public IEnumerable<IAction> Execute()
    {
        File.Copy(source.FullName, destination.FullName, force);
        return [];
    }

    public override string ToString()
    {
        return $"CPY {source.Name} -> {destination.Name}";
    }
}