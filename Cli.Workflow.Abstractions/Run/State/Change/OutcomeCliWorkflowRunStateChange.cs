using Cli.Commands.Abstractions.Outcomes;

namespace Cli.Workflow.Abstractions.Run.State.Change;

public class OutcomeCliWorkflowRunStateChange(
    TimeSpan at,
    ClIWorkflowRunStateStatus from,
    ClIWorkflowRunStateStatus to,
    CliCommandOutcome[] outcomes)
    : CliWorkflowRunStateChange(at, from, to)
{
    public readonly CliCommandOutcome[] Outcomes = outcomes;
}