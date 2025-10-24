using Cli.Abstractions;

namespace Cli.Commands.Abstractions.Exceptions;

public class NoCommandGeneratorException : CliException
{
    public NoCommandGeneratorException(string message) : base(CliExceptionCode.NoCommandGenerator, message)
    {
    }
}