using Cli.Commands.Abstractions.Outcomes;
using Cli.Workflow.Abstractions;
using Cli.Workflow.Abstractions.Run.State.Change;

namespace Cli.Workflow.Run.State.Change;

public class OutcomeCliWorkflowRunStateChange : CliWorkflowRunStateChange, IOutcomeCliWorkflowRunStateChange
{
    public CliCommandOutcome[] Outcomes { get; }
    
    public OutcomeCliWorkflowRunStateChange(
        TimeSpan at,
        ClIWorkflowRunStateStatus from,
        ClIWorkflowRunStateStatus to,
        CliCommandOutcome[] outcomes) : base(at, from, to)
    {
        Outcomes = outcomes;
    }
}