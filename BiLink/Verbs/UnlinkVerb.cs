using BiLink.Utilities;
using CommandLine;

namespace BiLink.Verbs;

[Verb("unlink")]
public class UnlinkVerb : VerbBase
{
    [Option('s', "source", Required = true)]
    public string? Source { get; init; }

    [Option("delete-source")] public bool DeleteSource { get; init; }

    public override void Execute()
    {
        if (string.IsNullOrWhiteSpace(Source))
        {
            Logger.LogError("Source path is empty");
            return;
        }

        var sourceDirectory = new DirectoryInfo(Source);
        if (!sourceDirectory.Exists)
        {
            Logger.LogError("Source directory does not exist");
            return;
        }

        var targetPath = sourceDirectory.ResolveLinkTarget(true) as DirectoryInfo;
        if (targetPath is null)
        {
            Logger.LogError("Source directory is not a symbolic link");
            return;
        }

        Logger.LogDelete(sourceDirectory.FullName);
        sourceDirectory.Delete();

        Logger.LogCopy(targetPath.FullName, sourceDirectory.FullName);
        DirectoryUtils.Copy(targetPath, sourceDirectory, Verbose);

        if (DeleteSource)
        {
            Logger.LogDelete(targetPath.FullName);
            targetPath.Delete(true);
        }
        else
        {
            Logger.LogMove(targetPath.FullName, targetPath.FullName + ".bak");
            Directory.Move(targetPath.FullName, targetPath.FullName + ".bak");
        }
    }
}