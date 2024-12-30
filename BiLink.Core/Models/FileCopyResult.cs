namespace BiLink.Core.Models;

public class FileCopyResult : FileResult
{
    public FileCopyResult(FileSystemInfo source, FileSystemInfo destination)
    {
        Source = source;
        Destination = destination;
    }

    public FileSystemInfo Source { get; }
    public FileSystemInfo Destination { get; }

    public override string ToString()
    {
        return $"{Source.FullName} -> {Destination.FullName}";
    }
}