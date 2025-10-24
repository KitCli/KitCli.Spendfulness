namespace YnabCli.Abstractions;

public abstract class CliCommandOutcome(CliWorkflowRunOutcomeKind kind)
{
    protected CliWorkflowRunOutcomeKind Kind { get;  set; } = kind;
}

// TODO: Implement a JsonOutputOutcome?