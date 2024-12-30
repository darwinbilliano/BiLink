using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using CommandLine;

namespace BiLink.CommandLine;

public static class Program
{
    [SuppressMessage("Trimming", "IL2026")]
    private static Type[] Verbs => Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(type => type.IsSubclassOf(typeof(Verb)))
        .ToArray();

    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments(args, Verbs)
            .WithParsed<Verb>(verb => verb.Execute());
    }
}