using Cli.Workflow.Abstractions;
using Cli.Workflow.Abstractions.Run.State.Change;

namespace Cli.Workflow.Run.State.Change;

public class CliWorkflowRunStateChange : ICliWorkflowRunStateChange
{
    public TimeSpan At { get; }
    public ClIWorkflowRunStateStatus From { get; }
    public ClIWorkflowRunStateStatus To { get; }
    
    public CliWorkflowRunStateChange(
        TimeSpan at,
        ClIWorkflowRunStateStatus from,
        ClIWorkflowRunStateStatus to)
    {
        At = at;
        From = from;
        To = to;
    }
}