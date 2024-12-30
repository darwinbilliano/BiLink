using System.Diagnostics;
using BiLink.Core.Models;

namespace BiLink.Core.Utilities;

public static class FileUtils
{
    public static IEnumerable<FileResult> Copy(FileInfo source, FileInfo destination)
    {
        Debug.Assert(destination.Directory is not null);

        if (!destination.Directory.Exists)
        {
            Directory.CreateDirectory(destination.Directory.FullName);
            yield return new FileCreateResult(destination.Directory);
        }

        File.Copy(source.FullName, destination.FullName);
        yield return new FileCopyResult(source, destination);
    }
}