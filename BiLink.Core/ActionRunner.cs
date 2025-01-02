namespace BiLink.Core;

public static class ActionRunner
{
    public static void Run(this IAction action, int depth)
    {
        var indent = new string(' ', Math.Max(0, depth * 4));
        Console.WriteLine("{0}{1}", indent, action);
        foreach (var result in action.Execute())
        {
            result.Run(depth + 1);
        }
    }
}