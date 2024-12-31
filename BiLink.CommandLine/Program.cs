using BiLink.CommandLine.Utilities;
using CommandLine;

namespace BiLink.CommandLine;

public static class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments(args, ReflectionUtils.GetTypes<IVerb>())
            .WithParsed<IVerb>(VerbUtils.Run);
    }
}