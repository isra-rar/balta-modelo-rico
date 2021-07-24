using PaymenteContext.Shared.Commands;

namespace PaymenteContext.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
         ICommandResult Handler(T command);
    }
}