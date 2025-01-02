using System.ComponentModel.DataAnnotations;
using BiLink.Core;
using CommandLine;

namespace BiLink.CommandLine.Verbs;

[Verb("link", HelpText = "Create a symbolic link pointing to target directory.")]
public record LinkVerb : IVerb
{
    [Required]
    [Value(0, HelpText = "Path to create the symbolic link.", MetaName = "path")]
    public string Path { get; init; }

    [Required]
    [Value(1, HelpText = "Symbolic link target directory.", MetaName = "target")]
    public string Target { get; init; }

    [Option("force", HelpText = "Continue operation even if target already exists.")]
    public bool Force { get; init; }

    public IEnumerable<IAction> Execute()
    {
        var sourceDir = new DirectoryInfo(Path);
        var targetDir = new DirectoryInfo(Target);

        if (sourceDir.Exists)
        {
            if (targetDir.Exists && !Force)
            {
                Console.Error.WriteLine("Target directory already exists.");
                yield break;
            }

            yield return new DirectoryMoveAction(sourceDir, targetDir, Force);
        }
        else
        {
            if (!targetDir.Exists)
            {
                yield return new DirectoryCreateAction(targetDir);
            }
        }

        yield return new DirectoryLinkAction(sourceDir, targetDir);
    }
}