namespace Cli.Workflow.Abstractions.Run.State.Change;

// TODO: Move this to workflow, mark with interface?
public class CliWorkflowRunStateChange
{
    public readonly TimeSpan At;
    public readonly ClIWorkflowRunStateStatus From;
    public readonly ClIWorkflowRunStateStatus To;
    
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