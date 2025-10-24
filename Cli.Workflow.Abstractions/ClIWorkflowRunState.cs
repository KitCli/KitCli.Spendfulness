namespace Cli.Workflow.Abstractions;

public enum ClIWorkflowRunState
{
    Created,
    Running,
    NoInput,
    NoCommand,
    Finished,
    Exceptional,
}