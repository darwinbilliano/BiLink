using CommandLine;

namespace BiLink.Verbs;

public abstract class VerbBase
{
    [Option("verbose")]
    public bool Verbose { get; init; }

    public abstract void Execute();
}