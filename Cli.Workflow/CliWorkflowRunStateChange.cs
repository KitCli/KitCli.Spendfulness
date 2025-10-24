using Cli.Workflow.Abstractions;

namespace Cli;

// TODO: CLI - ADD TIMESTAMP CHANGED TOO.
public record CliWorkflowRunStateChange(
    ClIWorkflowRunState StartedAt,
    ClIWorkflowRunState MovedTo);