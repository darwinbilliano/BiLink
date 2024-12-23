namespace BiLink;

public static class Logger
{
    public static void LogError(string value)
    {
        Console.Error.WriteLine("ERROR: {0}", value);
    }
    
    public static void LogCopy(string sourcePath, string destinationPath)
    {
        Console.WriteLine("COPY: {0} -> {1}", sourcePath, destinationPath);
    }
    
    public static void LogCreate(string path)
    {
        Console.WriteLine("CREATE: {0}", path);
    }

    public static void LogDelete(string path)
    {
        Console.WriteLine("DELETE: {0}", path);
    }
    
    public static void LogLink(string sourcePath, string destinationPath)
    {
        Console.WriteLine("LINK: {0} -> {1}", sourcePath, destinationPath);
    }

    public static void LogMove(string sourcePath, string destinationPath)
    {
        Console.WriteLine("MOVE: {0} -> {1}", sourcePath, destinationPath);
    }
}