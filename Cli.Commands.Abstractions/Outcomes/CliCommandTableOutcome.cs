using Cli.Abstractions;

namespace Cli.Commands.Abstractions.Outcomes;

public class CliCommandTableOutcome(CliTable table) : CliCommandOutcome(CliCommandOutcomeKind.Table)
{
    public CliTable Table = table;
}