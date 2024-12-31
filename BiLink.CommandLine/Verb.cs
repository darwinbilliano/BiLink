using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BiLink.CommandLine;

public abstract class Verb
{
    public void Execute()
    {
        if (OnValidate())
        {
            OnExecute();
        }
    }

    protected abstract void OnExecute();

    [SuppressMessage("Trimming", "IL2026")]
    private bool OnValidate()
    {
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();

        Validator.TryValidateObject(this, context, results, true);

        foreach (var result in results)
        {
            Console.Error.WriteLine(result.ErrorMessage);
        }

        return results.Count == 0;
    }
}