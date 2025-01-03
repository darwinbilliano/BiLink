namespace BiLink.Core;

public readonly struct FileMoveAction(FileInfo source, FileInfo destination, bool force) : IAction
{
    public IEnumerable<IAction> Execute()
    {
        yield return new FileCopyAction(source, destination, force);
        yield return new FileDeleteAction(source);
    }

    public override string ToString()
    {
        return $"MOV {source.Name} -> {destination.Name}";
    }
}