using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using BiLink.Core.Models;
using BiLink.Core.Utilities;
using CommandLine;

namespace BiLink.CommandLine.Verbs;

[Verb("unlink", HelpText = "")]
public class UnlinkVerb : Verb
{
    [Required]
    [Value(0, HelpText = "", MetaName = "path")]
    public string Path { get; set; }

    protected override void OnExecute()
    {
        var sourceDir = new DirectoryInfo(Path);
        if (!sourceDir.Exists)
        {
            Console.WriteLine("Source directory does not exist.");
            return;
        }

        var targetDir = sourceDir.ResolveLinkTarget(false) as DirectoryInfo;
        if (targetDir is null)
        {
            Console.WriteLine("Source directory is not a symbolic link.");
            return;
        }

        Directory.Delete(sourceDir.FullName);
        Console.WriteLine("DELETE: {0}", sourceDir.FullName);

        foreach (var result in targetDir.MoveFiles(sourceDir))
        {
            switch (result)
            {
                case FileCopyResult copyResult:
                    Console.WriteLine("MOVE: {0} -> {1}", copyResult.Source, copyResult.Destination);
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
}