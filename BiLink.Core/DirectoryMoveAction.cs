namespace BiLink.Core;

public readonly struct DirectoryMoveAction(DirectoryInfo source, DirectoryInfo destination, bool force) : IAction
{
    public IEnumerable<IAction> Execute()
    {
        yield return new DirectoryCreateAction(destination);

        foreach (var dir in source.EnumerateDirectories())
        {
            var destinationDir = Path.Combine(destination.FullName, dir.FullName[(source.FullName.Length + 1)..]);
            var destinationDirInfo = new DirectoryInfo(destinationDir);

            yield return new DirectoryMoveAction(dir, destinationDirInfo, force);
        }

        foreach (var file in source.EnumerateFiles())
        {
            var destinationFile = Path.Combine(destination.FullName, file.FullName[(source.FullName.Length + 1)..]);
            var destinationFileInfo = new FileInfo(destinationFile);

            yield return new FileMoveAction(file, destinationFileInfo, force);
        }

        yield return new DirectoryDeleteAction(source);
    }

    public override string ToString()
    {
        return $"MOV {source.Name} -> {destination.Name}";
    }
}