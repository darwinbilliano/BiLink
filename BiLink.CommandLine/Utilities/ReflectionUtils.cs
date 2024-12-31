using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace BiLink.CommandLine.Utilities;

[SuppressMessage("Trimming", "IL2026")]
public static class ReflectionUtils
{
    public static Type[] GetTypes<T>()
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsAssignableTo(typeof(T)))
            .ToArray();
    }

    public static bool Validate(object obj)
    {
        var context = new ValidationContext(obj);
        var results = new List<ValidationResult>();

        Validator.TryValidateObject(obj, context, results, true);

        foreach (var result in results)
        {
            Console.Error.WriteLine(result.ErrorMessage);
        }

        return results.Count == 0;
    }
}