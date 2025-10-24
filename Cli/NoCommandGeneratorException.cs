

namespace Cli;

public class NoCommandGeneratorException : CliException
{
    public NoCommandGeneratorException(string message) : base(CliExceptionCode.NoCommandGenerator, message)
    {
    }
}