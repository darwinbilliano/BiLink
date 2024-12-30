using BiLink.Core.Models;

namespace BiLink.Core.Utilities;

public static class DirectoryUtils
{
    public static IEnumerable<FileResult> CopyFiles(this DirectoryInfo source, DirectoryInfo destination)
    {
        IEnumerable<FileResult> CopyFilesInternal(DirectoryInfo directory)
        {
            foreach (var dir in directory.EnumerateDirectories())
            {
                foreach (var result in CopyFilesInternal(dir))
                {
                    yield return result;
                }

                var destinationPath = Path.Combine(destination.FullName, dir.FullName[(source.FullName.Length + 1)..]);
                var destinationDir = new FileInfo(destinationPath);
                yield return new FileCopyResult(dir, destinationDir);
            }

            foreach (var file in directory.EnumerateFiles())
            {
                var destinationPath = Path.Combine(destination.FullName, file.FullName[(source.FullName.Length + 1)..]);
                var destinationFile = new FileInfo(destinationPath);

                foreach (var result in FileUtils.Copy(file, destinationFile))
                {
                    yield return result;
                }
            }
        }

        return CopyFilesInternal(source);
    }

    public static IEnumerable<FileResult> MoveFiles(this DirectoryInfo source, DirectoryInfo destination)
    {
        foreach (var result in source.CopyFiles(destination))
        {
            yield return result;

            if (result is FileCopyResult copyResult)
            {
                switch (copyResult.Source)
                {
                    case FileInfo file:
                        File.Delete(file.FullName);
                        break;
                    case DirectoryInfo dir:
                        Directory.Delete(dir.FullName);
                        break;
                }

                yield return new FileDeleteResult(copyResult.Source);
            }
        }

        Directory.Delete(source.FullName);
        yield return new FileDeleteResult(source);
    }
}