using System.ComponentModel.DataAnnotations;
using System.Runtime.Versioning;
using BiLink.Core;
using CommandLine;

namespace BiLink.CommandLine.Verbs;

[SupportedOSPlatform("windows")]
[Verb("mlink", HelpText = "Create a symbolic link that mirrors across drive.")]
public record MirrorLinkVerb : IVerb
{
    [Required]
    [Value(0, HelpText = "Path to create the symbolic link.", MetaName = "path")]
    public string Path { get; init; }

    [Required]
    [StringLength(1)]
    [Value(1, HelpText = "The target drive of symbolic link mirror.", MetaName = "target")]
    public string Target { get; init; }

    [Option("force", HelpText = "Continue operation even if target directory already exists.")]
    public bool Force { get; init; }

    public IEnumerable<IAction> Execute()
    {
        var verb = new LinkVerb
        {
            Path = Path,
            Target = Target + Path[1..],
            Force = Force
        };

        return verb.Execute();
    }
}