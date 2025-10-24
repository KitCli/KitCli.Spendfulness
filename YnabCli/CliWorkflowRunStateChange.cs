namespace YnabCli;

public record CliWorkflowRunStateChange(
    ClIWorkflowRunState From,
    ClIWorkflowRunState To);