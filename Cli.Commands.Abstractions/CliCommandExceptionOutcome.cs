using Cli.Outcomes;

namespace Cli.Commands.Abstractions;

public class CliCommandExceptionOutcome(Exception exception) : CliCommandOutcome(CliWorkflowRunOutcomeKind.Exception)
{
    public Exception Exception { get; set; } = exception;
}