using Cli.Outcomes;

namespace Cli.Commands.Abstractions;

// TODO: Potentially rename to NothingCliCommandOutcome, or NothingOutcome.
public class CliCommandNothingOutcome() : CliCommandOutcome(CliWorkflowRunOutcomeKind.Nothing)
{
}