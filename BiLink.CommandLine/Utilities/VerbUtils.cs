using BiLink.Core;

namespace BiLink.CommandLine.Utilities;

public static class VerbUtils
{
    public static void Run(IVerb verb)
    {
        if (ReflectionUtils.Validate(verb))
        {
            verb.Run();
        }
    }
}