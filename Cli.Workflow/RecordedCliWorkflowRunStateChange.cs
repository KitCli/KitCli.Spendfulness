using Cli.Workflow.Abstractions;

namespace Cli.Workflow;


public class CliWorkflowRunStateChange(ClIWorkflowRunStateType startedAt, ClIWorkflowRunStateType movedTo)
{
    public readonly ClIWorkflowRunStateType StartedAt = startedAt;
    public readonly ClIWorkflowRunStateType MovedTo = movedTo;
}

public class RecordedCliWorkflowRunStateChange(
    long ticks,
    ClIWorkflowRunStateType startedAt,
    ClIWorkflowRunStateType movedTo)
    : CliWorkflowRunStateChange(startedAt, movedTo)
{
    public readonly long Ticks = ticks;
}

    