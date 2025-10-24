namespace Cli.Commands.Abstractions.Outcomes;

public class CliCommandExceptionOutcome(Exception exception) : CliCommandOutcome(CliWorkflowRunOutcomeKind.Exception)
{
    public Exception Exception { get; set; } = exception;
}