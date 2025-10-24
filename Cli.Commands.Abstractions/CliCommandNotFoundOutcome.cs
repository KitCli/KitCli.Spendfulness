using Cli.Outcomes;

namespace Cli.Commands.Abstractions;

public class CliCommandNotFoundOutcome() : CliCommandOutcome(CliWorkflowRunOutcomeKind.NoCommand)
{
}