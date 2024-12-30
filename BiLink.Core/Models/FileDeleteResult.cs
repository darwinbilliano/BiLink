namespace BiLink.Core.Models;

public class FileDeleteResult : FileResult
{
    public FileDeleteResult(FileSystemInfo path)
    {
        Path = path;
    }

    public FileSystemInfo Path { get; }

    public override string ToString()
    {
        return Path.FullName;
    }
}