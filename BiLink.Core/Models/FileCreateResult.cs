namespace BiLink.Core.Models;

public class FileCreateResult : FileResult
{
    public FileCreateResult(FileSystemInfo path)
    {
        Path = path;
    }

    public FileSystemInfo Path { get; }

    public override string ToString()
    {
        return Path.FullName;
    }
}