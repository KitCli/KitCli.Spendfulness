using ConsoleTables;

namespace Cli.Commands.Abstractions.Outcomes;

public class CliCommandTableOutcome(ConsoleTable table)
    : CliCommandOutcome(CliWorkflowRunOutcomeKind.Table)
{
    public ConsoleTable Table = table;
}