using ConsoleTables;

namespace Cli.Outcomes;

public class CliCommandTableOutcome(ConsoleTable table)
    : CliCommandOutcome(CliWorkflowRunOutcomeKind.Table)
{
    public ConsoleTable Table = table;
}