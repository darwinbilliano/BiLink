namespace BiLink.Core;

public readonly struct FileDeleteAction(FileInfo file) : IAction
{
    public IEnumerable<IAction> Execute()
    {
        File.Delete(file.FullName);
        return [];
    }

    public override string ToString()
    {
        return $"DEL {file.Name}";
    }
}