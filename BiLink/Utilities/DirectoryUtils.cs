using System.Diagnostics;

namespace BiLink.Utilities;

public static class DirectoryUtils
{
    public static void Copy(DirectoryInfo sourceDirectory, DirectoryInfo destinationDirectory, bool verbose)
    {
        var sourceFiles = sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories);
        foreach (var sourceFile in sourceFiles)
        {
            var sourceFilePath = sourceFile.FullName[(sourceDirectory.FullName.Length + 1)..];
            var destinationFilePath = Path.Combine(destinationDirectory.FullName, sourceFilePath);
            var destinationFile = new FileInfo(destinationFilePath);

            Debug.Assert(destinationFile.Directory is not null);

            if (!destinationFile.Directory.Exists)
            {
                if (verbose)
                {
                    Logger.LogCreate(destinationFile.Directory.FullName);
                }

                destinationFile.Directory.Create();
            }

            if (verbose)
            {
                Logger.LogCopy(sourceFile.FullName, destinationFile.FullName);
            }

            File.Copy(sourceFile.FullName, destinationFile.FullName);
        }
    }
}