using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using BiLink.Core.Models;
using BiLink.Core.Utilities;
using CommandLine;

namespace BiLink.CommandLine.Verbs;

[Verb("link", HelpText = "")]
public class LinkVerb : Verb
{
    [Required]
    [Value(0, HelpText = "", MetaName = "path")]
    public string Path { get; init; }

    [Required]
    [Value(1, HelpText = "", MetaName = "target")]
    public string Target { get; init; }

    [Option("force", HelpText = "")]
    public bool Force { get; init; }

    protected override void OnExecute()
    {
        var sourceDir = new DirectoryInfo(Path);
        var targetDir = new DirectoryInfo(Target);

        if (sourceDir.Exists)
        {
            if (targetDir.Exists && !Force)
            {
                try
                {
                    targetDir.Delete();
                }
                catch (IOException)
                {
                    Console.Error.WriteLine("Target directory already exists.");
                    return;
                }
            }

            foreach (var result in sourceDir.MoveFiles(targetDir))
            {
                switch (result)
                {
                    case FileCopyResult copyResult:
                        Console.WriteLine("COPY: {0} -> {1}", copyResult.Source, copyResult.Destination);
                        break;
                    case FileCreateResult createResult:
                        Console.WriteLine("CREATE: {0}", createResult.Path);
                        break;
                    case FileDeleteResult deleteResult:
                        Console.WriteLine("DELETE: {0}", deleteResult.Path);
                        break;
                    default:
                        Debug.Fail("Unexpected file result.");
                        break;
                }
            }
        }
        else
        {
            targetDir.Create();
        }

        sourceDir.CreateAsSymbolicLink(targetDir.FullName);
        Console.WriteLine("LINK: {0} -> {1}", sourceDir.FullName, targetDir.FullName);
    }
}