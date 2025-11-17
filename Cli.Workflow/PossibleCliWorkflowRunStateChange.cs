using Cli.Workflow.Abstractions;

namespace Cli.Workflow;

public class PossibleCliWorkflowRunStateChange(ClIWorkflowRunStateType ifStartedAt, ClIWorkflowRunStateType canMoveTo)
{
    public readonly ClIWorkflowRunStateType IfStartedAt = ifStartedAt;
    public readonly ClIWorkflowRunStateType CanMoveTo = canMoveTo;
}