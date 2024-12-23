using BiLink.Verbs;
using CommandLine;

namespace BiLink;

public static class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments<LinkVerb, UnlinkVerb>(args)
            .WithParsed<LinkVerb>(Execute)
            .WithParsed<UnlinkVerb>(Execute);
    }
    
    private static void Execute<T>(T verb) where T : VerbBase
    {
        verb.Execute();
    }
}