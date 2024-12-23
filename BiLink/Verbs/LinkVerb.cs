using BiLink.Utilities;
using CommandLine;

namespace BiLink.Verbs;

[Verb("link")]
public class LinkVerb : VerbBase
{
    [Option('s', "source", Required = true)]
    public string? Source { get; init; }

    [Option('d', "destination", Required = true)]
    public string? Destination { get; init; }

    [Option("delete-source")] public bool DeleteSource { get; init; }

    [Option("delete-destination")] public bool DeleteDestination { get; init; }

    public override void Execute()
    {
        if (string.IsNullOrWhiteSpace(Source))
        {
            Logger.LogError("Source path is empty");
            return;
        }

        if (string.IsNullOrWhiteSpace(Destination))
        {
            Logger.LogError("Destination path is empty");
            return;
        }

        var sourceDirectory = new DirectoryInfo(Source);
        var destinationDirectory = new DirectoryInfo(Destination);

        if (sourceDirectory.Exists)
        {
            if (destinationDirectory.Exists)
            {
                if (DeleteDestination)
                {
                    Logger.LogDelete(destinationDirectory.FullName);
                    destinationDirectory.Delete();
                }
                else
                {
                    Logger.LogError("Destination directory already exists");
                    return;
                }
            }

            Logger.LogCopy(sourceDirectory.FullName, destinationDirectory.FullName);
            DirectoryUtils.Copy(sourceDirectory, destinationDirectory, Verbose);

            if (DeleteSource)
            {
                Logger.LogDelete(sourceDirectory.FullName);
                sourceDirectory.Delete(true);
            }
            else
            {
                Logger.LogMove(sourceDirectory.FullName, sourceDirectory.FullName + ".bak");
                Directory.Move(sourceDirectory.FullName, sourceDirectory.FullName + ".bak");
            }
        }
        else
        {
            if (!destinationDirectory.Exists)
            {
                Logger.LogError("Destination directory does not exist");
                return;
            }
        }

        Logger.LogLink(sourceDirectory.FullName, destinationDirectory.FullName);
        sourceDirectory.CreateAsSymbolicLink(destinationDirectory.FullName);
    }
}