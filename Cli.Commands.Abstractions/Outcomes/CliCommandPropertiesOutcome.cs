using Cli.Commands.Abstractions.Properties;

namespace Cli.Commands.Abstractions.Outcomes;

public class CliCommandPropertiesOutcome(List<CliCommandProperty> properties) : CliCommandOutcome(CliWorkflowRunOutcomeKind.Properties)
{
    public List<CliCommandProperty> Properties { get; } = properties;
}