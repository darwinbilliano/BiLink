namespace BiLink.Core;

public readonly struct DirectoryLinkAction(DirectoryInfo source, DirectoryInfo target) : IAction
{
    public IEnumerable<IAction> Execute()
    {
        Directory.CreateSymbolicLink(source.FullName, target.FullName);
        return [];
    }

    public override string ToString()
    {
        return $"LNK {source.Name} -> {target.Name}";
    }
}