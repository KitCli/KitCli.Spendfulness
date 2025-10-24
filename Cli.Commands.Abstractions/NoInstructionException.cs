using Cli.Abstractions;

namespace Cli.Commands.Abstractions;

public class NoInstructionException : CliException
{
    public NoInstructionException(string message) : base(CliExceptionCode.NoInstruction, message)
    {
    }
}