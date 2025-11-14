using Cli.Abstractions;

namespace Cli.Commands.Abstractions.Exceptions;

public class NoInstructionException : CliException
{
    public NoInstructionException()
    {
    }
    
    public NoInstructionException(string message) : base(CliExceptionCode.NoInstruction, message)
    {
    }
}