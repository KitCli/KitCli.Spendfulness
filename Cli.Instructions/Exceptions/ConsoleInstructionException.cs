using YnabCli.Abstractions;

namespace Cli.Instructions.Exceptions;

public class ConsoleInstructionException : YnabCliException
{
    public ConsoleInstructionExceptionCode Code { get; }
    
    public ConsoleInstructionException(ConsoleInstructionExceptionCode code, string message) : base(message)
    {
        Code = code;
    }
}