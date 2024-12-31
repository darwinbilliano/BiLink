namespace BiLink.Core;

public static class ActionRunner
{
    public static void Run(this IAction action)
    {
        action.Run(0);
    }

    private static void Run(this IAction action, int depth)
    {
        Console.WriteLine("{0}{1}", new string(' ', depth * 4), action);
        foreach (var result in action.Execute())
        {
            result.Run(depth + 1);
        }
    }
}