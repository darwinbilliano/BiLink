using System.ComponentModel.DataAnnotations;
using BiLink.Core;
using CommandLine;

namespace BiLink.CommandLine.Verbs;

[Verb("unlink", HelpText = "Delete symbolic link and move its target to it.")]
public record UnlinkVerb : IVerb
{
    [Required]
    [Value(0, HelpText = "The symbolic link path.", MetaName = "path")]
    public string Path { get; set; }

    public IEnumerable<IAction> Execute()
    {
        var sourceDir = new DirectoryInfo(Path);
        if (!sourceDir.Exists)
        {
            Console.Error.WriteLine("Source directory does not exist.");
            yield break;
        }

        var targetDir = sourceDir.ResolveLinkTarget(false) as DirectoryInfo;
        if (targetDir is null)
        {
            Console.Error.WriteLine("Source directory is not a symbolic link.");
            yield break;
        }

        yield return new DirectoryDeleteAction(sourceDir);
        yield return new DirectoryMoveAction(targetDir, sourceDir);
    }
}