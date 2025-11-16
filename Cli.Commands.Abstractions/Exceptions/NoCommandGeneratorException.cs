using Cli.Abstractions;

namespace Cli.Commands.Abstractions.Exceptions;

public class NoCommandGeneratorException : CliException
{
    public NoCommandGeneratorException() : base(CliExceptionCode.NoCommandGenerator, string.Empty)
    {
    }
    
    public NoCommandGeneratorException(string message) : base(CliExceptionCode.NoCommandGenerator, message)
    {
    }
}