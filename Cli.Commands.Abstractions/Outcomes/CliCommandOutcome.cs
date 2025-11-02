using Cli.Commands.Abstractions.Properties;

namespace Cli.Commands.Abstractions.Outcomes;

public abstract class CliCommandOutcome(CliWorkflowRunOutcomeKind kind, List<CliCommandProperty>? properties = null)
{
    public CliWorkflowRunOutcomeKind Kind { get; } = kind;
    
    public List<CliCommandProperty> Properties { get; } = new();
}