using Cli.Workflow.Abstractions;

namespace Cli.Workflow;

public class CliWorkflowRunStateChange
{
    public readonly ClIWorkflowRunStateType StartedAt;
    public readonly ClIWorkflowRunStateType MovedTo;
    
    public CliWorkflowRunStateChange(
        ClIWorkflowRunStateType startedAt,
        ClIWorkflowRunStateType movedTo)
    {
        StartedAt = startedAt;
        MovedTo = movedTo;
    }
}