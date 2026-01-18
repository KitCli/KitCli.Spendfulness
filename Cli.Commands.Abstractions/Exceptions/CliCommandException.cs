using Cli.Abstractions.Exceptions;

namespace Cli.Commands.Abstractions.Exceptions;

public class CliCommandException : CliException
{
    public new CliCommandExceptionCode Code { get; }

    public CliCommandException(CliCommandExceptionCode code, string message)
        : base(CliExceptionCode.Command, message)
    {
        Code = code;
    }
}