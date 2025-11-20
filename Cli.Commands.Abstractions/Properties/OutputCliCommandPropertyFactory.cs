using Cli.Commands.Abstractions.Outcomes;
using Cli.Commands.Abstractions.Outcomes.Final;

namespace Cli.Commands.Abstractions.Properties;

public class OutputCliCommandPropertyFactory : ICliCommandPropertyFactory
{
    public bool CanCreateProperty(CliCommandOutcome outcome) => outcome is CliCommandOutputOutcome;

    public CliCommandProperty CreateProperty(CliCommandOutcome outcome)
    {
        if (outcome is CliCommandOutputOutcome outputOutcome)
        {
            return new OutputCliCommandProperty(outputOutcome.Output);
        }

        throw new InvalidOperationException("Cannot create property from the given outcome.");
    }
}