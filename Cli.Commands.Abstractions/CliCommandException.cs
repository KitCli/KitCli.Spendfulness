using Cli.Abstractions;

namespace Cli;

public class CliCommandException : CliException
{
    public new CliCommandExceptionCode Code { get; }

    public CliCommandException(CliCommandExceptionCode code, string message)
        : base(CliExceptionCode.Command, message)
    {
        Code = code;
    }
}