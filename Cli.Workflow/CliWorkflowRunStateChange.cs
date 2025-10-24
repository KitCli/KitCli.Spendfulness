using Cli.Workflow.Abstractions;

namespace Cli;

public record CliWorkflowRunStateChange(
    ClIWorkflowRunState StartedAt,
    ClIWorkflowRunState MovedTo);