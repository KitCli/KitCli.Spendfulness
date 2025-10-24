using Cli.Outcomes;
using ConsoleTables;

namespace Cli.Commands.Abstractions;

public class CliCommandTableOutcome(ConsoleTable table)
    : CliCommandOutcome(CliWorkflowRunOutcomeKind.Table)
{
    public ConsoleTable Table = table;
}