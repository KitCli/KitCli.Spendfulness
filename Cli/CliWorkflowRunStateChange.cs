using Cli.Workflow.Abstractions;

namespace YnabCli;

public record CliWorkflowRunStateChange(
    ClIWorkflowRunState From,
    ClIWorkflowRunState To);