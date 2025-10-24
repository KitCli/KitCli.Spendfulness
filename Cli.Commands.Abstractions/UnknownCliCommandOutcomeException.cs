namespace Cli.Commands.Abstractions;

public class UnknownCliCommandOutcomeException(string message)
    : CliCommandException(CliCommandExceptionCode.UnkownCliCommandOutcome, message)
{
}