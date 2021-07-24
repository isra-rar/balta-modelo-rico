using PaymenteContext.Shared.Commands;

namespace PaymenteContext.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {
        }
        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}