using ConsoleTables;

namespace YnabCli.Abstractions;

public class CliCommandTableOutcome(ConsoleTable table)
    : CliCommandOutcome(CliWorkflowRunOutcomeKind.Table)
{
    public ConsoleTable Table = table;
}