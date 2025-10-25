namespace Cli.Commands.Abstractions.Outcomes;

public abstract class CliCommandOutcome(CliWorkflowRunOutcomeKind kind)
{
    public CliWorkflowRunOutcomeKind Kind { get; } = kind;
}