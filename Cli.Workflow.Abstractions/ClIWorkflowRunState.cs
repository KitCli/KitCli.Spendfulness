namespace Cli.Workflow.Abstractions;

public enum ClIWorkflowRunState
{
    NotInitialized,
    Created,
    Running,
    InvalidAsk,
    Exceptional,
    Finished,
}