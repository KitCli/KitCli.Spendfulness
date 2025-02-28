namespace YnabCli.Commands.Exceptions;

public class CommandException : Exception
{
    public CommandExceptionCode Code { get; }
    
    public CommandException(CommandExceptionCode code, string message) : base(message)
    {
        Code = code;
    }
}