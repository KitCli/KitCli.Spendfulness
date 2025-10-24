namespace Cli;

public class UnknownCliCommandOutcomeException(string message)
    : CliCommandException(CliCommandExceptionCode.UnkownCliCommandOutcome, message)
{
}