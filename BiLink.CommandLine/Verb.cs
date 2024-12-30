using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BiLink.CommandLine;

public abstract class Verb
{
    public void Execute()
    {
        try
        {
            OnValidate();
            OnExecute();
        }
        catch (Exception ex)
        {
#if DEBUG
            Console.Error.WriteLine(ex);
#else
            Console.Error.WriteLine(ex.Message);
#endif
        }
    }

    protected abstract void OnExecute();

    [SuppressMessage("Trimming", "IL2026")]
    private void OnValidate()
    {
        var context = new ValidationContext(this);
        Validator.ValidateObject(this, context, true);
    }
}