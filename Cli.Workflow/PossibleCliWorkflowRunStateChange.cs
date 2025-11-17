using Cli.Workflow.Abstractions;

namespace Cli.Workflow;

public class PossibleCliWorkflowRunStateChange(ClIWorkflowRunStateStatus ifStartedAt, ClIWorkflowRunStateStatus canMoveTo)
{
    public readonly ClIWorkflowRunStateStatus IfStartedAt = ifStartedAt;
    public readonly ClIWorkflowRunStateStatus CanMoveTo = canMoveTo;
}