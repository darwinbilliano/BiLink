namespace BiLink.Core;

public interface IAction
{
    IEnumerable<IAction> Execute();
}